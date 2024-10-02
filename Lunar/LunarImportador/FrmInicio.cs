using FirebirdSql.Data.FirebirdClient;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Npgsql;
using System.Data;
using System.Text.RegularExpressions;

namespace LunarImportador
{
    public partial class FrmInicio : Form
    {
        string connectionStringLinkPro = "Host=seu_servidor;Port=5432;Username=seu_usuario;Password=sua_senha;Database=seu_banco_de_dados";
        public FrmInicio()
        {
            InitializeComponent();
            PreencherComboBoxDatabasesMysql();

            PanelSistemaAtual();
        }
        private void PanelSistemaAtual()
        {
            gradientPanel1.BackColor = Color.White;
            this.Controls.Add(gradientPanel1);
        }

        private void PreencherComboBoxDatabasesMysql()
        {
            string connectionString = "Server=localhost;User Id=marcelo;Password=mx123;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SHOW DATABASES;", connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comboDestino.Items.Add(reader[0].ToString());
                    }

                    try { comboDestino.SelectedIndex = 1; } catch { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar bancos de dados MySql: " + ex.Message);
            }
        }

        private void btnPesquisaBancoOrigem_Click(object sender, EventArgs e)
        {
            pesquisarBancoOrigem();
        }

        private void pesquisarBancoOrigem()
        {
            if (radioSGBR.Checked == true)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Firebird Database Files (*.fdb)|*.fdb|All Files (*.*)|*.*";
                    openFileDialog.Title = "Selecione o Banco de Dados Firebird";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        txtBancoOrigem.Text = openFileDialog.FileName;
                    }
                }
            }
            if (radioSoftSystemCosmos.Checked == true)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Firebird Database Files (*.fdb;*.gdb)|*.fdb;*.gdb|All Files (*.*)|*.*";
                    openFileDialog.Title = "Selecione o Banco de Dados Firebird";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        txtBancoOrigem.Text = openFileDialog.FileName;
                    }
                }
            }
            else
                MessageBox.Show("Selecione um sistema de origem");


        }

        private void importar()
        {
            string selectedDatabase = comboDestino.SelectedItem?.ToString();
            string firebirdDatabasePath = txtBancoOrigem.Text;

            //Validação
            if (string.IsNullOrEmpty(selectedDatabase))
            {
                MessageBox.Show("Selecione um banco de dados de destino.");
                return;
            }

            if (string.IsNullOrEmpty(firebirdDatabasePath))
            {
                MessageBox.Show("Selecione o banco de dados de origem.");
                return;
            }

            // Importação de dados
            if (radioSGBR.Checked == true)
            {
                if (chkClientes.Checked == true)
                {
                    progressBar1.Visible = true;
                    lblStatus.Visible = true;
                    Thread importarThread = new Thread(() => ImportarClientesEFornecedores(selectedDatabase, firebirdDatabasePath));
                    importarThread.Start();
                }
                if (chkProdutos.Checked == true)
                {
                    progressBar1.Visible = true;
                    lblStatus.Visible = true;
                    Thread importarThread = new Thread(() => ImportarGruposEProdutosSGBR(selectedDatabase, firebirdDatabasePath));
                    importarThread.Start();
                }
                if (chkContasAPagar.Checked == true)
                {
                    progressBar1.Visible = true;
                    lblStatus.Visible = true;
                    Thread importarThread = new Thread(() => ImportarContasPagarSgbr(selectedDatabase, firebirdDatabasePath));
                    importarThread.Start();
                }
            }

            //LINK PRO
            if(radioLinkPro.Checked == true)
            {
                if (chkProdutos.Checked == true)
                {
                    progressBar1.Visible = true;
                    lblStatus.Visible = true;
                    Thread importarThread = new Thread(() => importarProdutosLinkProCervantes());
                    importarThread.Start();
                }
                if (chkClientes.Checked == true)
                {
                    progressBar1.Visible = true;
                    lblStatus.Visible = true;
                    Thread importarThread = new Thread(() => importarClientesLinkProCervantes());
                    importarThread.Start();
                }
            }

            //SoftSystem cosmos
            if (radioSoftSystemCosmos.Checked == true)
            {
                if (chkClientes.Checked == true)
                {
                    progressBar1.Visible = true;
                    lblStatus.Visible = true;
                    Thread importarThread = new Thread(() => ImportarClientesEFornecedoresSoftsystem(selectedDatabase, firebirdDatabasePath));
                    importarThread.Start();
                }
                if (chkProdutos.Checked == true)
                {
                    progressBar1.Visible = true;
                    lblStatus.Visible = true;
                    Thread importarThread = new Thread(() => ImportarProdutosSoftsystem(selectedDatabase, firebirdDatabasePath));
                    importarThread.Start();
                }
                if (chkVendas.Checked == true)
                {
                    progressBar1.Visible = true;
                    lblStatus.Visible = true;
                    Thread importarThread = new Thread(() => ImportarVendasSoftsystem(selectedDatabase, firebirdDatabasePath));
                    importarThread.Start();
                }
            }
        }

        private void ImportarClientesEFornecedores(string database, string firebirdDatabasePath)
        {
            ImportarClientesSgbr(database, firebirdDatabasePath);
            ImportarFornecedoresSgbr(database, firebirdDatabasePath);
        }

        private void ImportarGruposEProdutosSGBR(string database, string firebirdDatabasePath)
        {
            ImportarGrupoProdutosSgbr(database, firebirdDatabasePath);
            ImportarProdutosSgbr(database, firebirdDatabasePath);
        }
        private void UpdateUI(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(action));
            }
            else
            {
                action();
            }
        }

        private void ImportarClientesEFornecedoresSoftsystem(string database, string firebirdDatabasePath)
        {
            ImportarClientesSoftsystem(database, firebirdDatabasePath);
            ImportarFornecedoresSoftsystem(database, firebirdDatabasePath);
        }
        private void ImportarClientesSgbr(string database, string firebirdDatabasePath)
        {
            int i = 0;
            UpdateUI(() =>
            {
                lblStatus.Text = "Importação de Clientes";
            });
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;";
            FbConnection firebirdConnection = new FbConnection(firebirdConnectionString);
            FbCommand firebirdCommand = new FbCommand("SELECT * FROM tCliente", firebirdConnection);

            // Conexão com o banco de dados MySQL
            string mysqlConnectionString = $"Server=localhost;Database={database};User Id=marcelo;Password=mx123;";
            MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);

            try
            {
                firebirdConnection.Open();
                mysqlConnection.Open();

                // Obter a quantidade total de registros
                FbCommand firebirdCountCommand = new FbCommand("SELECT COUNT(*) FROM tCliente", firebirdConnection);
                int totalRegistros = (int)firebirdCountCommand.ExecuteScalar();

                // Configurar a ProgressBar
                UpdateUI(() =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = totalRegistros;
                    progressBar1.Value = 0;
                });

                FbDataReader firebirdReader = firebirdCommand.ExecuteReader();
                while (firebirdReader.Read())
                {
                    i++;
                    Pessoa pessoa = new Pessoa();
                    //ID no SGBR
                    pessoa.CodigoImportacao = firebirdReader["controle"].ToString();
                    pessoa.Observacoes = "";
                    pessoa.RazaoSocial = firebirdReader["cliente"].ToString();
                    if (pessoa.RazaoSocial.Length > 80)
                    {
                        pessoa.RazaoSocial = pessoa.RazaoSocial.Substring(0, 80);
                    }
                    pessoa.NomeFantasia = firebirdReader["fantasia"].ToString();
                    if (pessoa.NomeFantasia.Length > 80)
                    {
                        pessoa.NomeFantasia = pessoa.NomeFantasia.Substring(0, 80);
                    }
                    pessoa.Cnpj = Generica.RemoveCaracteres(firebirdReader["cnpj"].ToString());
                    if (String.IsNullOrEmpty(pessoa.Cnpj))
                        pessoa.Cnpj = Generica.RemoveCaracteres(firebirdReader["cpf"].ToString());
                    pessoa.Email = firebirdReader["email"].ToString();

                    pessoa.ReceberLembrete = false;
                    pessoa.Vendedor = false;
                    pessoa.Cliente = true;
                    pessoa.Fornecedor = false;
                    pessoa.FlagExcluido = false;
                    pessoa.Funcionario = false;
                    pessoa.Transportadora = false;
                    pessoa.EscritorioCobranca = false;
                    pessoa.Tecnico = false;

                    pessoa.FuncaoTrabalho = "";
                    pessoa.InscricaoEstadual = Generica.RemoveCaracteres(firebirdReader["ie"].ToString());
                    pessoa.ComissaoVendedor = 0;
                    pessoa.ContatoTrabalho = "";
                    try { pessoa.DataNascimento = DateTime.Parse(firebirdReader["datanascimento"].ToString()); } catch { }
                    pessoa.LimiteCredito = 0;
                    pessoa.LocalTrabalho = "";
                    pessoa.Mae = firebirdReader["mae"].ToString();
                    pessoa.Pai = firebirdReader["pai"].ToString();
                    pessoa.RegistradoSpc = false;
                    pessoa.Rg = firebirdReader["rg"].ToString();
                    pessoa.SalarioTrabalho = "";
                    pessoa.Sexo = firebirdReader["sexo"].ToString();
                    pessoa.DataCadastro = DateTime.Parse(firebirdReader["datahoracadastro"].ToString());
                    //SGBR valida exclusao é assim:
                    if (firebirdReader["ativo"].ToString().Equals("NÃO") || firebirdReader["ativo"].ToString().Equals("NAO"))
                    {
                        pessoa.FlagExcluido = true;
                    }
                    pessoa.TelefoneTrabalho = "";
                    pessoa.TempoTrabalho = "";
                    pessoa.TipoParceiroImportado = "";
                    pessoa.TipoPessoa = "PF";
                    if (firebirdReader["tipocliente"].ToString().Equals("JURÍDICA"))
                        pessoa.TipoPessoa = "PJ";

                    pessoa.EnderecoPrincipal = null;
                    pessoa.PessoaTelefone = null;

                    Endereco endereco = new Endereco();

                    Cidade cidade = null;
                    string uf = firebirdReader["uf"].ToString();
                    string cidadeDescricao = firebirdReader["cidade"].ToString();

                    string mysqlQueryCidade = @"
                        SELECT c.*
                        FROM Cidade c
                        INNER JOIN Estado e ON c.Estado = e.Id
                        WHERE c.Descricao = @descricao AND e.Uf = @uf
                        LIMIT 1";

                    MySqlCommand mysqlCommandCidade = new MySqlCommand(mysqlQueryCidade, mysqlConnection);
                    mysqlCommandCidade.Parameters.AddWithValue("@descricao", cidadeDescricao);
                    mysqlCommandCidade.Parameters.AddWithValue("@uf", uf);

                    using (MySqlDataReader cidadeReader = mysqlCommandCidade.ExecuteReader())
                    {
                        if (cidadeReader.Read())
                        {
                            cidade = new Cidade
                            {
                                Id = cidadeReader.GetInt32("Id"),
                                Descricao = cidadeReader.GetString("Descricao")
                            };
                        }
                    }
                    if (cidade != null)
                    {
                        if (cidade.Id > 0)
                        {
                            endereco.Referencia = "";
                            endereco.Cep = Generica.RemoveCaracteres(firebirdReader["cep"].ToString());
                            endereco.Numero = firebirdReader["numero"].ToString();
                            endereco.Bairro = firebirdReader["bairro"].ToString();
                            endereco.Cidade = new Cidade();
                            endereco.Cidade = cidade;
                            endereco.Logradouro = firebirdReader["endereco"].ToString();
                            endereco.Complemento = firebirdReader["complemento"].ToString();
                        }
                    }
                    PessoaTelefone pessoaTelefone = new PessoaTelefone();
                    if (!String.IsNullOrEmpty(firebirdReader["telefone"].ToString()))
                    {
                        pessoaTelefone = ProcessarTelefone(firebirdReader["telefone"].ToString());
                    }

                    // Inserir dados no MySQL
                    string mysqlQuery = @"
                        INSERT INTO Pessoa 
                        (CodigoImportacao, Observacoes, RazaoSocial, NomeFantasia, Cnpj, Email, ReceberLembrete, Vendedor, Cliente, Fornecedor, FlagExcluido, Funcionario, Transportadora, EscritorioCobranca, Tecnico, FuncaoTrabalho, InscricaoEstadual, ComissaoVendedor, ContatoTrabalho, DataNascimento, LimiteCredito, LocalTrabalho, Mae, Pai, RegistradoSpc, Rg, SalarioTrabalho, Sexo, TelefoneTrabalho, TempoTrabalho, TipoParceiroImportado, TipoPessoa, DataCadastro)
                        VALUES 
                        (@CodigoImportacao, @Observacoes, @RazaoSocial, @NomeFantasia, @Cnpj, @Email, @ReceberLembrete, @Vendedor, @Cliente, @Fornecedor, @FlagExcluido, @Funcionario, @Transportadora, @EscritorioCobranca, @Tecnico, @FuncaoTrabalho, @InscricaoEstadual, @ComissaoVendedor, @ContatoTrabalho, @DataNascimento, @LimiteCredito, @LocalTrabalho, @Mae, @Pai, @RegistradoSpc, @Rg, @SalarioTrabalho, @Sexo, @TelefoneTrabalho, @TempoTrabalho, @TipoParceiroImportado, @TipoPessoa, @DataCadastro)";

                    MySqlCommand mysqlCommand = new MySqlCommand(mysqlQuery, mysqlConnection);
                    mysqlCommand.Parameters.AddWithValue("@CodigoImportacao", pessoa.CodigoImportacao);
                    mysqlCommand.Parameters.AddWithValue("@Observacoes", pessoa.Observacoes);
                    mysqlCommand.Parameters.AddWithValue("@RazaoSocial", pessoa.RazaoSocial);
                    mysqlCommand.Parameters.AddWithValue("@NomeFantasia", pessoa.NomeFantasia);
                    mysqlCommand.Parameters.AddWithValue("@Cnpj", pessoa.Cnpj);
                    mysqlCommand.Parameters.AddWithValue("@Email", pessoa.Email);
                    mysqlCommand.Parameters.AddWithValue("@ReceberLembrete", pessoa.ReceberLembrete);
                    mysqlCommand.Parameters.AddWithValue("@Vendedor", pessoa.Vendedor);
                    mysqlCommand.Parameters.AddWithValue("@Cliente", pessoa.Cliente);
                    mysqlCommand.Parameters.AddWithValue("@Fornecedor", pessoa.Fornecedor);
                    mysqlCommand.Parameters.AddWithValue("@FlagExcluido", pessoa.FlagExcluido);
                    mysqlCommand.Parameters.AddWithValue("@Funcionario", pessoa.Funcionario);
                    mysqlCommand.Parameters.AddWithValue("@Transportadora", pessoa.Transportadora);
                    mysqlCommand.Parameters.AddWithValue("@EscritorioCobranca", pessoa.EscritorioCobranca);
                    mysqlCommand.Parameters.AddWithValue("@Tecnico", pessoa.Tecnico);
                    mysqlCommand.Parameters.AddWithValue("@FuncaoTrabalho", pessoa.FuncaoTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@InscricaoEstadual", pessoa.InscricaoEstadual);
                    mysqlCommand.Parameters.AddWithValue("@ComissaoVendedor", pessoa.ComissaoVendedor);
                    mysqlCommand.Parameters.AddWithValue("@ContatoTrabalho", pessoa.ContatoTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento == DateTime.MinValue ? (object)DBNull.Value : pessoa.DataNascimento);
                    mysqlCommand.Parameters.AddWithValue("@LimiteCredito", pessoa.LimiteCredito);
                    mysqlCommand.Parameters.AddWithValue("@LocalTrabalho", pessoa.LocalTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@Mae", pessoa.Mae);
                    mysqlCommand.Parameters.AddWithValue("@Pai", pessoa.Pai);
                    mysqlCommand.Parameters.AddWithValue("@RegistradoSpc", pessoa.RegistradoSpc);
                    mysqlCommand.Parameters.AddWithValue("@Rg", pessoa.Rg);
                    mysqlCommand.Parameters.AddWithValue("@SalarioTrabalho", pessoa.SalarioTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@Sexo", pessoa.Sexo);
                    mysqlCommand.Parameters.AddWithValue("@TelefoneTrabalho", pessoa.TelefoneTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@TempoTrabalho", pessoa.TempoTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@TipoParceiroImportado", pessoa.TipoParceiroImportado);
                    mysqlCommand.Parameters.AddWithValue("@TipoPessoa", pessoa.TipoPessoa);
                    mysqlCommand.Parameters.AddWithValue("@DataCadastro", pessoa.DataCadastro);

                    mysqlCommand.ExecuteNonQuery();
                    long pessoaId = mysqlCommand.LastInsertedId;

                    if (pessoaId > 0 && !String.IsNullOrEmpty(endereco.Logradouro))
                    {
                        string mysqlQueryEndereco = "INSERT INTO Endereco (logradouro, numero, bairro, cep, complemento, cidade, pessoa, empresafilial, referencia) VALUES (@logradouro, @numero, @bairro, @cep, @complemento, @cidadeId, @pessoaId, 1, @referencia)";
                        MySqlCommand mysqlCommandEndereco = new MySqlCommand(mysqlQueryEndereco, mysqlConnection);
                        mysqlCommandEndereco.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
                        mysqlCommandEndereco.Parameters.AddWithValue("@numero", endereco.Numero);
                        mysqlCommandEndereco.Parameters.AddWithValue("@bairro", endereco.Bairro);
                        mysqlCommandEndereco.Parameters.AddWithValue("@cep", endereco.Cep);
                        mysqlCommandEndereco.Parameters.AddWithValue("@complemento", endereco.Complemento);
                        mysqlCommandEndereco.Parameters.AddWithValue("@cidadeId", endereco.Cidade.Id);
                        mysqlCommandEndereco.Parameters.AddWithValue("@pessoaId", pessoaId);
                        mysqlCommandEndereco.Parameters.AddWithValue("@referencia", endereco.Referencia);
                        mysqlCommandEndereco.ExecuteNonQuery();
                        long enderecoId = mysqlCommandEndereco.LastInsertedId;

                        // Atualizar Pessoa com o EnderecoPrincipal
                        string mysqlUpdatePessoa = "UPDATE Pessoa SET enderecoPrincipal = @enderecoPrincipal WHERE id = @pessoaId";
                        MySqlCommand mysqlCommandUpdatePessoa = new MySqlCommand(mysqlUpdatePessoa, mysqlConnection);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@enderecoPrincipal", enderecoId);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@pessoaId", pessoaId);

                        mysqlCommandUpdatePessoa.ExecuteNonQuery();
                    }
                    if (!String.IsNullOrEmpty(pessoaTelefone.Telefone))
                    {
                        string mysqlQueryTelefone = "INSERT INTO PessoaTelefone (ddd, telefone, observacoes, pessoa) VALUES (@ddd, @telefone, @observacoes, @pessoa)";
                        MySqlCommand mysqlCommandTelefone = new MySqlCommand(mysqlQueryTelefone, mysqlConnection);
                        mysqlCommandTelefone.Parameters.AddWithValue("@ddd", pessoaTelefone.Ddd);
                        mysqlCommandTelefone.Parameters.AddWithValue("@telefone", pessoaTelefone.Telefone);
                        mysqlCommandTelefone.Parameters.AddWithValue("@observacoes", "IMPORTADO SGBR");
                        mysqlCommandTelefone.Parameters.AddWithValue("@pessoa", pessoaId);
                        mysqlCommandTelefone.ExecuteNonQuery();
                        long pessoaTelefoneId = mysqlCommandTelefone.LastInsertedId;

                        // Atualizar Pessoa com o Telefone Princiapal
                        string mysqlUpdatePessoa = "UPDATE Pessoa SET pessoaTelefone = @pessoaTelefone WHERE id = @pessoaId";
                        MySqlCommand mysqlCommandUpdatePessoa = new MySqlCommand(mysqlUpdatePessoa, mysqlConnection);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@pessoaTelefone", pessoaTelefoneId);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@pessoaId", pessoaId);

                        mysqlCommandUpdatePessoa.ExecuteNonQuery();
                    }
                    UpdateUI(() =>
                    {
                        progressBar1.Value += 1;
                        lblStatus.Text = "Cliente " + i + " de " + totalRegistros;
                    });
                }
                firebirdReader.Close();
                //MessageBox.Show("Importação de clientes concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUI(() =>
                {
                    progressBar1.Value = 0;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao importar dados: " + ex.Message);
            }
            finally
            {
                firebirdConnection.Close();
                mysqlConnection.Close();
            }
        }
        
        private void ImportarFornecedoresSgbr(string database, string firebirdDatabasePath)
        {
            int i = 0;
            UpdateUI(() => { lblStatus.Text = "Importação de Fornecedores"; });
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;";
            FbConnection firebirdConnection = new FbConnection(firebirdConnectionString);
            FbCommand firebirdCommand = new FbCommand("SELECT * FROM tFornecedor", firebirdConnection);

            // Conexão com o banco de dados MySQL
            string mysqlConnectionString = $"Server=localhost;Database={database};User Id=marcelo;Password=mx123;";
            MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);

            try
            {
                firebirdConnection.Open();
                mysqlConnection.Open();

                // Obter a quantidade total de registros
                FbCommand firebirdCountCommand = new FbCommand("SELECT COUNT(*) FROM tFornecedor", firebirdConnection);
                int totalRegistros = (int)firebirdCountCommand.ExecuteScalar();

                // Configurar a ProgressBar
                UpdateUI(() =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = totalRegistros;
                    progressBar1.Value = 0;
                });

                FbDataReader firebirdReader = firebirdCommand.ExecuteReader();
                while (firebirdReader.Read())
                {
                    i++;
                    Pessoa pessoa = new Pessoa();
                    //ID no SGBR
                    pessoa.CodigoImportacao = firebirdReader["controle"].ToString();
                    pessoa.Observacoes = "";
                    pessoa.RazaoSocial = firebirdReader["razaosocial"].ToString();
                    if (pessoa.RazaoSocial.Length > 80)
                    {
                        pessoa.RazaoSocial = pessoa.RazaoSocial.Substring(0, 80);
                    }
                    pessoa.NomeFantasia = firebirdReader["nomefantasia"].ToString();
                    if (pessoa.NomeFantasia.Length > 80)
                    {
                        pessoa.NomeFantasia = pessoa.NomeFantasia.Substring(0, 80);
                    }
                    pessoa.Cnpj = Generica.RemoveCaracteres(firebirdReader["cnpj"].ToString());
                    if (String.IsNullOrEmpty(pessoa.Cnpj))
                        pessoa.Cnpj = Generica.RemoveCaracteres(firebirdReader["cpf"].ToString());
                    pessoa.Email = firebirdReader["email"].ToString();

                    pessoa.ReceberLembrete = false;
                    pessoa.Vendedor = false;
                    pessoa.Cliente = false;
                    pessoa.Fornecedor = true;
                    pessoa.FlagExcluido = false;
                    pessoa.Funcionario = false;
                    pessoa.Transportadora = false;
                    pessoa.EscritorioCobranca = false;
                    pessoa.Tecnico = false;

                    pessoa.FuncaoTrabalho = "";
                    pessoa.InscricaoEstadual = Generica.RemoveCaracteres(firebirdReader["ie"].ToString());
                    pessoa.ComissaoVendedor = 0;
                    pessoa.ContatoTrabalho = "";
                    try { pessoa.DataNascimento = DateTime.Parse("1900-01-01 00:00:00"); } catch { }
                    pessoa.LimiteCredito = 0;
                    pessoa.LocalTrabalho = "";
                    pessoa.Mae = "";
                    pessoa.Pai = "";
                    pessoa.RegistradoSpc = false;
                    pessoa.Rg = firebirdReader["rg"].ToString();
                    pessoa.SalarioTrabalho = "";
                    pessoa.Sexo = "M";
                    pessoa.DataCadastro = DateTime.Parse(firebirdReader["datahoracadastro"].ToString());
                    //SGBR valida exclusao é assim:
                    if (firebirdReader["ativo"].ToString().Equals("NÃO") || firebirdReader["ativo"].ToString().Equals("NAO"))
                    {
                        pessoa.FlagExcluido = true;
                    }
                    pessoa.TelefoneTrabalho = "";
                    pessoa.TempoTrabalho = "";
                    pessoa.TipoParceiroImportado = "";
                    pessoa.TipoPessoa = "PF";
                    if (!String.IsNullOrEmpty(pessoa.Cnpj))
                    {
                        if (pessoa.Cnpj.Length > 11)
                            pessoa.TipoPessoa = "PJ";
                    }
                    pessoa.EnderecoPrincipal = null;
                    pessoa.PessoaTelefone = null;

                    Endereco endereco = new Endereco();

                    Cidade cidade = null;
                    string uf = firebirdReader["uf"].ToString();
                    string cidadeDescricao = firebirdReader["cidade"].ToString();

                    string mysqlQueryCidade = @"
                        SELECT c.*
                        FROM Cidade c
                        INNER JOIN Estado e ON c.Estado = e.Id
                        WHERE c.Descricao = @descricao AND e.Uf = @uf
                        LIMIT 1";

                    MySqlCommand mysqlCommandCidade = new MySqlCommand(mysqlQueryCidade, mysqlConnection);
                    mysqlCommandCidade.Parameters.AddWithValue("@descricao", cidadeDescricao);
                    mysqlCommandCidade.Parameters.AddWithValue("@uf", uf);

                    using (MySqlDataReader cidadeReader = mysqlCommandCidade.ExecuteReader())
                    {
                        if (cidadeReader.Read())
                        {
                            cidade = new Cidade
                            {
                                Id = cidadeReader.GetInt32("Id"),
                                Descricao = cidadeReader.GetString("Descricao")
                            };
                        }
                    }
                    if (cidade != null)
                    {
                        if (cidade.Id > 0)
                        {
                            endereco.Referencia = "";
                            endereco.Cep = Generica.RemoveCaracteres(firebirdReader["cep"].ToString());
                            endereco.Numero = firebirdReader["numero"].ToString();
                            endereco.Bairro = firebirdReader["bairro"].ToString();
                            endereco.Cidade = new Cidade();
                            endereco.Cidade = cidade;
                            endereco.Logradouro = firebirdReader["endereco"].ToString();
                            endereco.Complemento = firebirdReader["complemento"].ToString();
                        }
                    }
                    PessoaTelefone pessoaTelefone = new PessoaTelefone();
                    if (!String.IsNullOrEmpty(firebirdReader["telefone"].ToString()))
                    {
                        pessoaTelefone = ProcessarTelefone(firebirdReader["telefone"].ToString());
                    }

                    // Inserir dados no MySQL
                    string mysqlQuery = @"
                        INSERT INTO Pessoa 
                        (CodigoImportacao, Observacoes, RazaoSocial, NomeFantasia, Cnpj, Email, ReceberLembrete, Vendedor, Cliente, Fornecedor, FlagExcluido, Funcionario, Transportadora, EscritorioCobranca, Tecnico, FuncaoTrabalho, InscricaoEstadual, ComissaoVendedor, ContatoTrabalho, DataNascimento, LimiteCredito, LocalTrabalho, Mae, Pai, RegistradoSpc, Rg, SalarioTrabalho, Sexo, TelefoneTrabalho, TempoTrabalho, TipoParceiroImportado, TipoPessoa, DataCadastro)
                        VALUES 
                        (@CodigoImportacao, @Observacoes, @RazaoSocial, @NomeFantasia, @Cnpj, @Email, @ReceberLembrete, @Vendedor, @Cliente, @Fornecedor, @FlagExcluido, @Funcionario, @Transportadora, @EscritorioCobranca, @Tecnico, @FuncaoTrabalho, @InscricaoEstadual, @ComissaoVendedor, @ContatoTrabalho, @DataNascimento, @LimiteCredito, @LocalTrabalho, @Mae, @Pai, @RegistradoSpc, @Rg, @SalarioTrabalho, @Sexo, @TelefoneTrabalho, @TempoTrabalho, @TipoParceiroImportado, @TipoPessoa, @DataCadastro)";

                    MySqlCommand mysqlCommand = new MySqlCommand(mysqlQuery, mysqlConnection);
                    mysqlCommand.Parameters.AddWithValue("@CodigoImportacao", pessoa.CodigoImportacao);
                    mysqlCommand.Parameters.AddWithValue("@Observacoes", pessoa.Observacoes);
                    mysqlCommand.Parameters.AddWithValue("@RazaoSocial", pessoa.RazaoSocial);
                    mysqlCommand.Parameters.AddWithValue("@NomeFantasia", pessoa.NomeFantasia);
                    mysqlCommand.Parameters.AddWithValue("@Cnpj", pessoa.Cnpj);
                    mysqlCommand.Parameters.AddWithValue("@Email", pessoa.Email);
                    mysqlCommand.Parameters.AddWithValue("@ReceberLembrete", pessoa.ReceberLembrete);
                    mysqlCommand.Parameters.AddWithValue("@Vendedor", pessoa.Vendedor);
                    mysqlCommand.Parameters.AddWithValue("@Cliente", pessoa.Cliente);
                    mysqlCommand.Parameters.AddWithValue("@Fornecedor", pessoa.Fornecedor);
                    mysqlCommand.Parameters.AddWithValue("@FlagExcluido", pessoa.FlagExcluido);
                    mysqlCommand.Parameters.AddWithValue("@Funcionario", pessoa.Funcionario);
                    mysqlCommand.Parameters.AddWithValue("@Transportadora", pessoa.Transportadora);
                    mysqlCommand.Parameters.AddWithValue("@EscritorioCobranca", pessoa.EscritorioCobranca);
                    mysqlCommand.Parameters.AddWithValue("@Tecnico", pessoa.Tecnico);
                    mysqlCommand.Parameters.AddWithValue("@FuncaoTrabalho", pessoa.FuncaoTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@InscricaoEstadual", pessoa.InscricaoEstadual);
                    mysqlCommand.Parameters.AddWithValue("@ComissaoVendedor", pessoa.ComissaoVendedor);
                    mysqlCommand.Parameters.AddWithValue("@ContatoTrabalho", pessoa.ContatoTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento == DateTime.MinValue ? (object)DBNull.Value : pessoa.DataNascimento);
                    mysqlCommand.Parameters.AddWithValue("@LimiteCredito", pessoa.LimiteCredito);
                    mysqlCommand.Parameters.AddWithValue("@LocalTrabalho", pessoa.LocalTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@Mae", pessoa.Mae);
                    mysqlCommand.Parameters.AddWithValue("@Pai", pessoa.Pai);
                    mysqlCommand.Parameters.AddWithValue("@RegistradoSpc", pessoa.RegistradoSpc);
                    mysqlCommand.Parameters.AddWithValue("@Rg", pessoa.Rg);
                    mysqlCommand.Parameters.AddWithValue("@SalarioTrabalho", pessoa.SalarioTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@Sexo", pessoa.Sexo);
                    mysqlCommand.Parameters.AddWithValue("@TelefoneTrabalho", pessoa.TelefoneTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@TempoTrabalho", pessoa.TempoTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@TipoParceiroImportado", pessoa.TipoParceiroImportado);
                    mysqlCommand.Parameters.AddWithValue("@TipoPessoa", pessoa.TipoPessoa);
                    mysqlCommand.Parameters.AddWithValue("@DataCadastro", pessoa.DataCadastro);

                    mysqlCommand.ExecuteNonQuery();
                    long pessoaId = mysqlCommand.LastInsertedId;

                    if (pessoaId > 0 && !String.IsNullOrEmpty(endereco.Logradouro))
                    {
                        string mysqlQueryEndereco = "INSERT INTO Endereco (logradouro, numero, bairro, cep, complemento, cidade, pessoa, empresafilial, referencia) VALUES (@logradouro, @numero, @bairro, @cep, @complemento, @cidadeId, @pessoaId, 1, @referencia)";
                        MySqlCommand mysqlCommandEndereco = new MySqlCommand(mysqlQueryEndereco, mysqlConnection);
                        mysqlCommandEndereco.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
                        mysqlCommandEndereco.Parameters.AddWithValue("@numero", endereco.Numero);
                        mysqlCommandEndereco.Parameters.AddWithValue("@bairro", endereco.Bairro);
                        mysqlCommandEndereco.Parameters.AddWithValue("@cep", endereco.Cep);
                        mysqlCommandEndereco.Parameters.AddWithValue("@complemento", endereco.Complemento);
                        mysqlCommandEndereco.Parameters.AddWithValue("@cidadeId", endereco.Cidade.Id);
                        mysqlCommandEndereco.Parameters.AddWithValue("@pessoaId", pessoaId);
                        mysqlCommandEndereco.Parameters.AddWithValue("@referencia", endereco.Referencia);
                        mysqlCommandEndereco.ExecuteNonQuery();
                        long enderecoId = mysqlCommandEndereco.LastInsertedId;

                        // Atualizar Pessoa com o EnderecoPrincipal
                        string mysqlUpdatePessoa = "UPDATE Pessoa SET enderecoPrincipal = @enderecoPrincipal WHERE id = @pessoaId";
                        MySqlCommand mysqlCommandUpdatePessoa = new MySqlCommand(mysqlUpdatePessoa, mysqlConnection);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@enderecoPrincipal", enderecoId);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@pessoaId", pessoaId);

                        mysqlCommandUpdatePessoa.ExecuteNonQuery();
                    }
                    if (!String.IsNullOrEmpty(pessoaTelefone.Telefone))
                    {
                        string mysqlQueryTelefone = "INSERT INTO PessoaTelefone (ddd, telefone, observacoes, pessoa) VALUES (@ddd, @telefone, @observacoes, @pessoa)";
                        MySqlCommand mysqlCommandTelefone = new MySqlCommand(mysqlQueryTelefone, mysqlConnection);
                        mysqlCommandTelefone.Parameters.AddWithValue("@ddd", pessoaTelefone.Ddd);
                        mysqlCommandTelefone.Parameters.AddWithValue("@telefone", pessoaTelefone.Telefone);
                        mysqlCommandTelefone.Parameters.AddWithValue("@observacoes", "IMPORTADO SGBR");
                        mysqlCommandTelefone.Parameters.AddWithValue("@pessoa", pessoaId);
                        mysqlCommandTelefone.ExecuteNonQuery();
                        long pessoaTelefoneId = mysqlCommandTelefone.LastInsertedId;

                        // Atualizar Pessoa com o Telefone Princiapal
                        string mysqlUpdatePessoa = "UPDATE Pessoa SET pessoaTelefone = @pessoaTelefone WHERE id = @pessoaId";
                        MySqlCommand mysqlCommandUpdatePessoa = new MySqlCommand(mysqlUpdatePessoa, mysqlConnection);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@pessoaTelefone", pessoaTelefoneId);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@pessoaId", pessoaId);

                        mysqlCommandUpdatePessoa.ExecuteNonQuery();
                    }
                    UpdateUI(() =>
                    {
                        progressBar1.Value += 1;
                        lblStatus.Text = "Fornecedor " + i + " de " + totalRegistros;
                    });
                }
                firebirdReader.Close();
                //MessageBox.Show("Importação de fornecedores concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUI(() =>
                {
                    progressBar1.Value = 0;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao importar dados: " + ex.Message);
            }
            finally
            {
                firebirdConnection.Close();
                mysqlConnection.Close();
            }
        }


        private void btnImportarDados_Click(object sender, EventArgs e)
        {
            importar();
        }

        public static PessoaTelefone ProcessarTelefone(string telefone)
        {
            PessoaTelefone resultado = new PessoaTelefone();

            if (string.IsNullOrEmpty(telefone))
            {
                resultado.Ddd = "";
                resultado.Telefone = "";
                return resultado;
            }

            // Regex para extrair o DDD
            Match dddMatch = Regex.Match(telefone, @"\((\d{2})\)");
            if (dddMatch.Success)
            {
                resultado.Ddd = dddMatch.Groups[1].Value;
            }
            else
            {
                resultado.Ddd = "00"; // DDD padrão ou vazio caso não encontrado
                if(telefone.Length > 9)
                    resultado.Ddd = telefone.Substring(0, 2);
            }

            // Regex para extrair o número de telefone sem o DDD
            string numeroTelefone = Regex.Replace(telefone, @"\(\d{2}\)", "").Trim();
            numeroTelefone = numeroTelefone.Replace("-", "").Replace(" ", "");
            if (numeroTelefone.Length > 9)
                numeroTelefone = numeroTelefone.Substring(2);


            resultado.Telefone = numeroTelefone;
            resultado.FlagExcluido = false;
            resultado.DataCadastro = DateTime.Now;
            resultado.OperadorCadastro = "1";

            return resultado;
        }



        private void ImportarProdutosSgbr(string database, string firebirdDatabasePath)
        {
            int i = 0;
            UpdateUI(() => { lblStatus.Text = "Importação de Produtos"; });
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;";
            FbConnection firebirdConnection = new FbConnection(firebirdConnectionString);
            FbCommand firebirdCommand = new FbCommand("SELECT * FROM tEstoque Where aplicacaoproduto <> 'SERVIÇOS' order by controle", firebirdConnection);

            // Conexão com o banco de dados MySQL
            string mysqlConnectionString = $"Server=localhost;Database={database};User Id=marcelo;Password=mx123;";
            MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);

            try
            {
                firebirdConnection.Open();
                mysqlConnection.Open();

                // Obter a quantidade total de registros
                FbCommand firebirdCountCommand = new FbCommand("SELECT COUNT(*) FROM tEstoque Where aplicacaoproduto <> 'SERVIÇOS' ", firebirdConnection);
                int totalRegistros = (int)firebirdCountCommand.ExecuteScalar();

                // Configurar a ProgressBar
                UpdateUI(() =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = totalRegistros;
                    progressBar1.Value = 0;
                });

                FbDataReader firebirdReader = firebirdCommand.ExecuteReader();
                while (firebirdReader.Read())
                {
                    i++;
                    Produto produto = new Produto();
                    //ID no SGBR
                    
                    if (chkIdProduto.Checked == true)
                        produto.Id = int.Parse(firebirdReader["controle"].ToString());
                    else
                        produto.Id = 0;
                    produto.Observacoes = firebirdReader["controle"].ToString() + " SGBR";
                    produto.AnoVeiculo = "";
                    produto.CapacidadeTracao = "";
                    produto.Cest = Generica.RemoveCaracteres(firebirdReader["cest"].ToString());
                    produto.CfopVenda = Generica.RemoveCaracteres(firebirdReader["cfop"].ToString());
                    produto.Chassi = "";
                    produto.CilindradaCc = "";
                    produto.CodAnp = firebirdReader["codigoanp"].ToString();
                    produto.CodSeloIpi = "";
                    produto.Combustivel = "";
                    produto.CondicaoProduto = "";
                    produto.ControlaEstoque = true;
                    produto.CorDenatran = "";
                    produto.CorMontadora = "";
                    produto.CstCofins = firebirdReader["codtributacaocofins"].ToString();
                    produto.CstIcms = firebirdReader["csosn"].ToString();
                    produto.CstIpi = firebirdReader["codtributacaoipi"].ToString();
                    produto.CstPis = firebirdReader["codtributacaopis"].ToString();
                    produto.DataCadastro = DateTime.Parse(firebirdReader["datahoracadastro"].ToString());
                    produto.Descricao = firebirdReader["produto"].ToString();
                    produto.DistanciaEixo = "";
                    produto.Ean = firebirdReader["codbarras"].ToString();
                    produto.Ecommerce = false;
                    produto.EnqIpi = "999";
                    produto.EspecieVeiculo = "";
                    int idGrupo = 1;
                    object grupoIdValue = (idGrupo == 0) ? DBNull.Value : (object)idGrupo;
                    Estoque estoque = new Estoque();
                    try
                    {
                        if (double.Parse(firebirdReader["qtde"].ToString()) > 0)
                        {
                            produto.Estoque = double.Parse(firebirdReader["qtde"].ToString());

                            estoque.BalancoEstoque = null;
                            estoque.Conciliado = true;
                            estoque.DataEntradaSaida = DateTime.Now;
                            estoque.Descricao = "IMPORTACAO SGBR";
                            estoque.Entrada = true;
                            estoque.FlagExcluido = false;
                            estoque.Origem = "SGBR";
                            estoque.Pessoa = null;
                            estoque.Quantidade = double.Parse(firebirdReader["qtde"].ToString());
                            estoque.QuantidadeInventario = 0;
                            estoque.Saida = false;


                            //Grava estoque auxiliar ou apenas o contabil
                            if (chkSaldoEstoque.Checked == true)
                                produto.EstoqueAuxiliar = double.Parse(firebirdReader["qtde"].ToString());
                            else
                                produto.EstoqueAuxiliar = 0;
                        }
                    }
                    catch
                    {
                        produto.Estoque = 0;
                        produto.EstoqueAuxiliar = 0;
                    }
                    produto.FlagExcluido = false;
                    if (firebirdReader["ativo"].ToString().Equals("NÃO") || firebirdReader["ativo"].ToString().Equals("NAO"))
                    {
                        produto.FlagExcluido = true;
                    }
                    produto.Grade = false;
                    string grupoFiscal = "2";
                    if (firebirdReader["cfop"].ToString().Equals("5101") || firebirdReader["cfop"].ToString().Equals("5102") || firebirdReader["cfop"].ToString().Equals("6102") || firebirdReader["csosn"].ToString().Equals("102") || firebirdReader["csosn"].ToString().Equals("101"))
                        grupoFiscal = "1";
                    produto.IdComplementar = "";
                    produto.KmEntrada = "";
                    produto.LotacaoVeiculo = "";
                    int idMarca = 0;
                    if (!String.IsNullOrEmpty(firebirdReader["marca"].ToString()))
                        idMarca = ObterOuCriarMarca(firebirdReader["marca"].ToString(), mysqlConnection);
                    object marcaIdValue = (idMarca == 0) ? DBNull.Value : (object)idMarca;
                    produto.MarcaModelo = "";
                    produto.Markup = "";
                    produto.ModeloVeiculo = "";
                    produto.Ncm = firebirdReader["ncm"].ToString();
                    produto.NumeroMotor = "";
                    produto.OperadorCadastro = "1";
                    produto.OrigemIcms = firebirdReader["codigocstorigem"].ToString();
                    produto.PercentualCofins = "0";
                    produto.PercentualIcms = "0";
                    produto.PercentualIpi = "0";
                    produto.PercentualPis = "0";
                    try { produto.PercGlp = double.Parse(firebirdReader["percglpcomb"].ToString()); } catch { produto.PercGlp = 0; }
                    try { produto.PercGni = double.Parse(firebirdReader["percgnicomb"].ToString()); } catch { produto.PercGni = 0; }
                    try { produto.PercGnn = double.Parse(firebirdReader["percgnncomb"].ToString()); } catch { produto.PercGnn = 0; }
                    produto.Pesavel = false;
                    if (firebirdReader["pesado"].ToString().Equals("NÃO"))
                        produto.Pesavel = true;
                    produto.PesoBrutoVeiculo = "";
                    produto.PesoLiquidoVeiculo = "";
                    produto.Placa = "";
                    produto.PotenciaCv = "";
                    produto.ProdutoSetor = null;
                    produto.ProdutoSubGrupo = null;
                    produto.Referencia = firebirdReader["referencia"].ToString();
                    produto.Renavam = "";
                    produto.RestricaoVeiculo = "";
                    produto.SolicitaNumeroSerie = false;
                    produto.TipoCambio = "";
                    produto.TipoEntrada = "";
                    produto.TipoPintura = "";
                    produto.TipoProduto = "REVENDA";
                    produto.TipoVeiculo = "";
                    int unidadeMedida = ObterOuCriarUnidadeMedida(RemoverCedilha(firebirdReader["unidade"].ToString()), mysqlConnection);
                    try { produto.ValorCusto = decimal.Parse(firebirdReader["precocusto"].ToString()); } catch { produto.ValorCusto = 0; }
                    produto.ValorPartida = 0;
                    try { produto.ValorVenda = decimal.Parse(firebirdReader["precovenda"].ToString()); } catch { produto.ValorVenda = 1; }
                    produto.Veiculo = false;
                    produto.VeiculoNovo = false;

                    string mysqlQuery = @"
                INSERT INTO Produto 
                (Id, Observacoes, AnoVeiculo, CapacidadeTracao, Cest, CfopVenda, Chassi, CilindradaCc, CodAnp, CodSeloIpi, Combustivel, 
                CondicaoProduto, ControlaEstoque, CorDenatran, CorMontadora, CstCofins, CstIcms, CstIpi, CstPis, DataCadastro, Descricao, 
                DistanciaEixo, Ean, Ecommerce, EnqIpi, EspecieVeiculo, Estoque, EstoqueAuxiliar, FlagExcluido, Grade, IdComplementar, 
                KmEntrada, LotacaoVeiculo, Marca, MarcaModelo, Markup, ModeloVeiculo, Ncm, NumeroMotor, OperadorCadastro, OrigemIcms, 
                PercentualCofins, PercentualIcms, PercentualIpi, PercentualPis, PercGlp, PercGni, PercGnn, Pesavel, PesoBrutoVeiculo, 
                PesoLiquidoVeiculo, Placa, PotenciaCv, ProdutoSetor, Referencia, Renavam, RestricaoVeiculo, 
                SolicitaNumeroSerie, TipoCambio, TipoEntrada, TipoPintura, TipoProduto, TipoVeiculo, UnidadeMedida, ValorCusto, 
                ValorPartida, ValorVenda, Veiculo, VeiculoNovo, Empresa, EmpresaFilial, GrupoFiscal, ProdutoGrupo) 
                VALUES 
                (@Id, @Observacoes, @AnoVeiculo, @CapacidadeTracao, @Cest, @CfopVenda, @Chassi, @CilindradaCc, @CodAnp, @CodSeloIpi, @Combustivel, 
                @CondicaoProduto, @ControlaEstoque, @CorDenatran, @CorMontadora, @CstCofins, @CstIcms, @CstIpi, @CstPis, @DataCadastro, @Descricao, 
                @DistanciaEixo, @Ean, @Ecommerce, @EnqIpi, @EspecieVeiculo, @Estoque, @EstoqueAuxiliar, @FlagExcluido, @Grade, @IdComplementar, 
                @KmEntrada, @LotacaoVeiculo, @MarcaId, @MarcaModelo, @Markup, @ModeloVeiculo, @Ncm, @NumeroMotor, @OperadorCadastro, @OrigemIcms, 
                @PercentualCofins, @PercentualIcms, @PercentualIpi, @PercentualPis, @PercGlp, @PercGni, @PercGnn, @Pesavel, @PesoBrutoVeiculo, 
                @PesoLiquidoVeiculo, @Placa, @PotenciaCv, @ProdutoSetor, @Referencia, @Renavam, @RestricaoVeiculo, 
                @SolicitaNumeroSerie, @TipoCambio, @TipoEntrada, @TipoPintura, @TipoProduto, @TipoVeiculo, @UnidadeMedidaId, @ValorCusto, 
                @ValorPartida, @ValorVenda, @Veiculo, @VeiculoNovo, 1,1, " + grupoFiscal + ", @GrupoProduto)";

                    MySqlCommand mysqlCommand = new MySqlCommand(mysqlQuery, mysqlConnection);
                    mysqlCommand.Parameters.AddWithValue("@Id", produto.Id);
                    mysqlCommand.Parameters.AddWithValue("@Observacoes", produto.Observacoes);
                    mysqlCommand.Parameters.AddWithValue("@AnoVeiculo", produto.AnoVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@CapacidadeTracao", produto.CapacidadeTracao);
                    mysqlCommand.Parameters.AddWithValue("@Cest", produto.Cest);
                    mysqlCommand.Parameters.AddWithValue("@CfopVenda", produto.CfopVenda);
                    mysqlCommand.Parameters.AddWithValue("@Chassi", produto.Chassi);
                    mysqlCommand.Parameters.AddWithValue("@CilindradaCc", produto.CilindradaCc);
                    mysqlCommand.Parameters.AddWithValue("@CodAnp", produto.CodAnp);
                    mysqlCommand.Parameters.AddWithValue("@CodSeloIpi", produto.CodSeloIpi);
                    mysqlCommand.Parameters.AddWithValue("@Combustivel", produto.Combustivel);
                    mysqlCommand.Parameters.AddWithValue("@CondicaoProduto", produto.CondicaoProduto);
                    mysqlCommand.Parameters.AddWithValue("@ControlaEstoque", produto.ControlaEstoque);
                    mysqlCommand.Parameters.AddWithValue("@CorDenatran", produto.CorDenatran);
                    mysqlCommand.Parameters.AddWithValue("@CorMontadora", produto.CorMontadora);
                    mysqlCommand.Parameters.AddWithValue("@CstCofins", produto.CstCofins);
                    mysqlCommand.Parameters.AddWithValue("@CstIcms", produto.CstIcms);
                    mysqlCommand.Parameters.AddWithValue("@CstIpi", produto.CstIpi);
                    mysqlCommand.Parameters.AddWithValue("@CstPis", produto.CstPis);
                    mysqlCommand.Parameters.AddWithValue("@DataCadastro", produto.DataCadastro);
                    mysqlCommand.Parameters.AddWithValue("@Descricao", produto.Descricao);
                    mysqlCommand.Parameters.AddWithValue("@DistanciaEixo", produto.DistanciaEixo);
                    mysqlCommand.Parameters.AddWithValue("@Ean", produto.Ean);
                    mysqlCommand.Parameters.AddWithValue("@Ecommerce", produto.Ecommerce);
                    mysqlCommand.Parameters.AddWithValue("@EnqIpi", produto.EnqIpi);
                    mysqlCommand.Parameters.AddWithValue("@EspecieVeiculo", produto.EspecieVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@Estoque", produto.Estoque);
                    mysqlCommand.Parameters.AddWithValue("@EstoqueAuxiliar", produto.EstoqueAuxiliar);
                    mysqlCommand.Parameters.AddWithValue("@FlagExcluido", produto.FlagExcluido);
                    mysqlCommand.Parameters.AddWithValue("@Grade", produto.Grade);
                    mysqlCommand.Parameters.AddWithValue("@IdComplementar", produto.IdComplementar);
                    mysqlCommand.Parameters.AddWithValue("@KmEntrada", produto.KmEntrada);
                    mysqlCommand.Parameters.AddWithValue("@LotacaoVeiculo", produto.LotacaoVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@MarcaId", marcaIdValue);
                    mysqlCommand.Parameters.AddWithValue("@MarcaModelo", produto.MarcaModelo);
                    mysqlCommand.Parameters.AddWithValue("@Markup", produto.Markup);
                    mysqlCommand.Parameters.AddWithValue("@ModeloVeiculo", produto.ModeloVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@Ncm", produto.Ncm);
                    mysqlCommand.Parameters.AddWithValue("@NumeroMotor", produto.NumeroMotor);
                    mysqlCommand.Parameters.AddWithValue("@OperadorCadastro", produto.OperadorCadastro);
                    mysqlCommand.Parameters.AddWithValue("@OrigemIcms", produto.OrigemIcms);
                    mysqlCommand.Parameters.AddWithValue("@PercentualCofins", produto.PercentualCofins);
                    mysqlCommand.Parameters.AddWithValue("@PercentualIcms", produto.PercentualIcms);
                    mysqlCommand.Parameters.AddWithValue("@PercentualIpi", produto.PercentualIpi);
                    mysqlCommand.Parameters.AddWithValue("@PercentualPis", produto.PercentualPis);
                    mysqlCommand.Parameters.AddWithValue("@PercGlp", produto.PercGlp);
                    mysqlCommand.Parameters.AddWithValue("@PercGni", produto.PercGni);
                    mysqlCommand.Parameters.AddWithValue("@PercGnn", produto.PercGnn);
                    mysqlCommand.Parameters.AddWithValue("@Pesavel", produto.Pesavel);
                    mysqlCommand.Parameters.AddWithValue("@PesoBrutoVeiculo", produto.PesoBrutoVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@PesoLiquidoVeiculo", produto.PesoLiquidoVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@Placa", produto.Placa);
                    mysqlCommand.Parameters.AddWithValue("@PotenciaCv", produto.PotenciaCv);
                    mysqlCommand.Parameters.AddWithValue("@ProdutoSetor", produto.ProdutoSetor);
                    mysqlCommand.Parameters.AddWithValue("@ProdutoSubGrupo", null);
                    mysqlCommand.Parameters.AddWithValue("@Referencia", produto.Referencia);
                    mysqlCommand.Parameters.AddWithValue("@Renavam", produto.Renavam);
                    mysqlCommand.Parameters.AddWithValue("@RestricaoVeiculo", produto.RestricaoVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@SolicitaNumeroSerie", produto.SolicitaNumeroSerie);
                    mysqlCommand.Parameters.AddWithValue("@TipoCambio", produto.TipoCambio);
                    mysqlCommand.Parameters.AddWithValue("@TipoEntrada", produto.TipoEntrada);
                    mysqlCommand.Parameters.AddWithValue("@TipoPintura", produto.TipoPintura);
                    mysqlCommand.Parameters.AddWithValue("@TipoProduto", produto.TipoProduto);
                    mysqlCommand.Parameters.AddWithValue("@TipoVeiculo", produto.TipoVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@UnidadeMedidaId", unidadeMedida);
                    mysqlCommand.Parameters.AddWithValue("@ValorCusto", produto.ValorCusto);
                    mysqlCommand.Parameters.AddWithValue("@ValorPartida", produto.ValorPartida);
                    mysqlCommand.Parameters.AddWithValue("@ValorVenda", produto.ValorVenda);
                    mysqlCommand.Parameters.AddWithValue("@Veiculo", produto.Veiculo);
                    mysqlCommand.Parameters.AddWithValue("@VeiculoNovo", produto.VeiculoNovo);
                    mysqlCommand.Parameters.AddWithValue("@GrupoProduto", grupoIdValue);

                    mysqlCommand.ExecuteNonQuery();
                    int idProduto = (int)mysqlCommand.LastInsertedId;
                    if (chkIdProduto.Checked == true)
                        idProduto = produto.Id;

                    string mysqlQueryEstoque = @"
                        INSERT INTO Estoque 
                            (BalancoEstoque, Conciliado, DataEntradaSaida, Descricao, Entrada, FlagExcluido, Origem, Pessoa, Quantidade, QuantidadeInventario, Saida, EmpresaFilial, Produto) 
                        VALUES 
                            (@BalancoEstoque, @Conciliado, @DataEntradaSaida, @Descricao, @Entrada, @FlagExcluido, @Origem, @Pessoa, @Quantidade, @QuantidadeInventario, @Saida, 1, " + idProduto + ")";

                    MySqlCommand mysqlCommandEstoque = new MySqlCommand(mysqlQueryEstoque, mysqlConnection);

                    mysqlCommandEstoque.Parameters.AddWithValue("@BalancoEstoque", (object)estoque.BalancoEstoque ?? DBNull.Value);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Conciliado", estoque.Conciliado);
                    mysqlCommandEstoque.Parameters.AddWithValue("@DataEntradaSaida", estoque.DataEntradaSaida);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Descricao", estoque.Descricao);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Entrada", estoque.Entrada);
                    mysqlCommandEstoque.Parameters.AddWithValue("@FlagExcluido", estoque.FlagExcluido);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Origem", estoque.Origem);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Pessoa", (object)estoque.Pessoa ?? DBNull.Value);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Quantidade", estoque.Quantidade);
                    mysqlCommandEstoque.Parameters.AddWithValue("@QuantidadeInventario", estoque.QuantidadeInventario);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Saida", estoque.Saida);
                    // Execute o comando
                    mysqlCommandEstoque.ExecuteNonQuery();
                    if (chkSaldoEstoque.Checked == true)
                    {
                        //Nao conciliado
                        estoque.Conciliado = false;
                        mysqlQueryEstoque = @"
                        INSERT INTO Estoque 
                            (BalancoEstoque, Conciliado, DataEntradaSaida, Descricao, Entrada, FlagExcluido, Origem, Pessoa, Quantidade, QuantidadeInventario, Saida, EmpresaFilial, Produto) 
                        VALUES 
                            (@BalancoEstoque, @Conciliado, @DataEntradaSaida, @Descricao, @Entrada, @FlagExcluido, @Origem, @Pessoa, @Quantidade, @QuantidadeInventario, @Saida, 1, " + idProduto + ")";

                        mysqlCommandEstoque = new MySqlCommand(mysqlQueryEstoque, mysqlConnection);
                        mysqlCommandEstoque.Parameters.AddWithValue("@BalancoEstoque", (object)estoque.BalancoEstoque ?? DBNull.Value);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Conciliado", estoque.Conciliado);
                        mysqlCommandEstoque.Parameters.AddWithValue("@DataEntradaSaida", estoque.DataEntradaSaida);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Descricao", estoque.Descricao);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Entrada", estoque.Entrada);
                        mysqlCommandEstoque.Parameters.AddWithValue("@FlagExcluido", estoque.FlagExcluido);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Origem", estoque.Origem);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Pessoa", (object)estoque.Pessoa ?? DBNull.Value);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Quantidade", estoque.Quantidade);
                        mysqlCommandEstoque.Parameters.AddWithValue("@QuantidadeInventario", estoque.QuantidadeInventario);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Saida", estoque.Saida);
                        // Execute o comando
                        mysqlCommandEstoque.ExecuteNonQuery();
                    }

                    UpdateUI(() =>
                    {
                        progressBar1.Value += 1;
                        lblStatus.Text = "Produto " + i + " de " + totalRegistros;
                    });
                }
                firebirdReader.Close();
                //MessageBox.Show("Importação de fornecedores concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUI(() =>
                {
                    lblStatus.Text = "Importação de produtos concluída com sucesso!";
                    progressBar1.Value = 0;
                    progressBar1.Visible = false;
                });
            }
            catch (Exception ex)
            {
                //if (ex.ToString().Contains("Duplicate entry"))
                MessageBox.Show("Erro ao importar produtos: " + ex.Message);
            }
            finally
            {
                firebirdConnection.Close();
                mysqlConnection.Close();
            }
        }

        private void ImportarGrupoProdutosSgbr(string database, string firebirdDatabasePath)
        {
            int i = 0;
            UpdateUI(() => { lblStatus.Text = "Importação de Grupos de Produtos"; });
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;";
            FbConnection firebirdConnection = new FbConnection(firebirdConnectionString);
            FbCommand firebirdCommand = new FbCommand("SELECT * FROM tGrupoEstoque", firebirdConnection);

            // Conexão com o banco de dados MySQL
            string mysqlConnectionString = $"Server=localhost;Database={database};User Id=marcelo;Password=mx123;";
            MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);

            try
            {
                firebirdConnection.Open();
                mysqlConnection.Open();

                // Obter a quantidade total de registros
                FbCommand firebirdCountCommand = new FbCommand("SELECT COUNT(*) FROM tGrupoEstoque", firebirdConnection);
                int totalRegistros = (int)firebirdCountCommand.ExecuteScalar();

                // Configurar a ProgressBar
                UpdateUI(() =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = totalRegistros;
                    progressBar1.Value = 0;
                });

                FbDataReader firebirdReader = firebirdCommand.ExecuteReader();
                while (firebirdReader.Read())
                {
                    i++;
                    ProdutoGrupo produtoGrupo = new ProdutoGrupo();
                    //ID no SGBR
                    produtoGrupo.Descricao = firebirdReader["grupo"].ToString();
                    try { produtoGrupo.Id = int.Parse(firebirdReader["controle"].ToString()); } catch { produtoGrupo.Id = 0; }
                    produtoGrupo.FlagExcluido = false;

                    string mysqlQuery = @"
                        INSERT INTO ProdutoGrupo 
                        (Id, Descricao, FlagExcluido, Empresa) 
                        VALUES 
                        (@Id, @Descricao, @FlagExcluido, 1)";

                    MySqlCommand mysqlCommand = new MySqlCommand(mysqlQuery, mysqlConnection);
                    mysqlCommand.Parameters.AddWithValue("@Id", produtoGrupo.Id);
                    mysqlCommand.Parameters.AddWithValue("@Descricao", produtoGrupo.Descricao);
                    mysqlCommand.Parameters.AddWithValue("@FlagExcluido", produtoGrupo.FlagExcluido);

                    mysqlCommand.ExecuteNonQuery();
                    UpdateUI(() =>
                    {
                        progressBar1.Value += 1;
                        lblStatus.Text = "Grupo " + i + " de " + totalRegistros;
                    });
                }
                firebirdReader.Close();
                //MessageBox.Show("Importação de fornecedores concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUI(() =>
                {
                    progressBar1.Value = 0;
                });
            }
            catch (Exception ex)
            {
                if (!ex.ToString().Contains("Duplicate entry"))
                    MessageBox.Show("Erro ao importar dados: " + ex.Message);
            }
            finally
            {
                firebirdConnection.Close();
                mysqlConnection.Close();
            }
        }

        private void ImportarCondicionaisSgbr(string database, string firebirdDatabasePath)
        {
            int i = 0;
            UpdateUI(() => { lblStatus.Text = "Importação de Condicionais"; });
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;";
            FbConnection firebirdConnection = new FbConnection(firebirdConnectionString);
            FbCommand firebirdCommand = new FbCommand("SELECT * FROM tCondicional WHERE tCondicional.Status = 'ABERTO'", firebirdConnection);

            // Conexão com o banco de dados MySQL
            string mysqlConnectionString = $"Server=localhost;Database={database};User Id=marcelo;Password=mx123;";
            MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);

            try
            {
                firebirdConnection.Open();
                mysqlConnection.Open();

                // Obter a quantidade total de registros
                FbCommand firebirdCountCommand = new FbCommand("SELECT COUNT(*) FROM tCondicional WHERE tCondicional.Status = 'ABERTO'", firebirdConnection);
                int totalRegistros = (int)firebirdCountCommand.ExecuteScalar();

                // Configurar a ProgressBar
                UpdateUI(() =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = totalRegistros;
                    progressBar1.Value = 0;
                });

                FbDataReader firebirdReader = firebirdCommand.ExecuteReader();
                while (firebirdReader.Read())
                {
                    i++;
                    Condicional condicional = new Condicional();
                    //ID no SGBR
                    condicional.Cliente = new Pessoa();
                    PessoaDAO pessoaDAO = new PessoaDAO();
                    condicional.Cliente = pessoaDAO.selecionarPessoaPorCodigoImportado(firebirdReader["codcliente"].ToString());

                    condicional.Venda = null;
                    condicional.Vendedor = null;
                    condicional.QuantidadeDevolvida = 0;
                    condicional.Data = DateTime.Parse(firebirdReader["data"].ToString());
                    condicional.DataCadastro = DateTime.Now;
                    condicional.QtdPeca = 0;
                    condicional.Encerrado = false;
                    condicional.DataPrevisao = condicional.Data.AddDays(2);
                    condicional.Filial = new EmpresaFilial();
                    condicional.Filial.Id = 1;
                    condicional.Filial = (EmpresaFilial)Controller.getInstance().selecionar(condicional.Filial);
                    condicional.FlagExcluido = false;
                    condicional.Observacoes = firebirdReader["observacao"].ToString();
                    condicional.Vendedor = new Pessoa();
                    condicional.Vendedor = null;
                    condicional.ValorTotal = decimal.Parse(firebirdReader["valortotal"].ToString());


                    string mysqlQuery = @"
                        INSERT INTO ProdutoGrupo 
                        (Id, Descricao, FlagExcluido, Empresa) 
                        VALUES 
                        (@Id, @Descricao, @FlagExcluido, 1)";

                    //MySqlCommand mysqlCommand = new MySqlCommand(mysqlQuery, mysqlConnection);
                    //mysqlCommand.Parameters.AddWithValue("@Id", produtoGrupo.Id);
                    //mysqlCommand.Parameters.AddWithValue("@Descricao", produtoGrupo.Descricao);
                    //mysqlCommand.Parameters.AddWithValue("@FlagExcluido", produtoGrupo.FlagExcluido);

                    //mysqlCommand.ExecuteNonQuery();
                    //UpdateUI(() =>
                    //{
                    //    progressBar1.Value += 1;
                    //    lblStatus.Text = "Grupo " + i + " de " + totalRegistros;
                    //});
                }
                firebirdReader.Close();
                //MessageBox.Show("Importação de fornecedores concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUI(() =>
                {
                    progressBar1.Value = 0;
                });
            }
            catch (Exception ex)
            {
                if (!ex.ToString().Contains("Duplicate entry"))
                    MessageBox.Show("Erro ao importar dados: " + ex.Message);
            }
            finally
            {
                firebirdConnection.Close();
                mysqlConnection.Close();
            }
        }

        private string RemoverCedilha(string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
                return descricao;

            return descricao.Replace("Ç", "C").Replace("ç", "c");
        }
        private int ObterOuCriarMarca(string nomeMarca, MySqlConnection mysqlConnection)
        {
            if (mysqlConnection == null)
            {
                throw new InvalidOperationException("A conexão MySQL não foi inicializada.");
            }

            // Certifique-se de que a conexão está aberta
            if (mysqlConnection.State != ConnectionState.Open)
            {
                mysqlConnection.Open();
            }
            // Verificar se a marca já existe
            string queryVerificarMarca = "SELECT Id FROM Marca WHERE descricao = @nomeMarca LIMIT 1";
            MySqlCommand commandVerificarMarca = new MySqlCommand(queryVerificarMarca, mysqlConnection);
            commandVerificarMarca.Parameters.AddWithValue("@nomeMarca", nomeMarca);

            object result = commandVerificarMarca.ExecuteScalar();

            if (result != null)
            {
                // Se a marca já existe, retornar o ID
                return Convert.ToInt32(result);
            }
            else
            {
                // Se a marca não existe, inserir a nova marca e retornar o novo ID
                string queryInserirMarca = "INSERT INTO Marca (Descricao,Empresa, FlagExcluido) VALUES (@nomeMarca, 1, 0)";
                MySqlCommand commandInserirMarca = new MySqlCommand(queryInserirMarca, mysqlConnection);
                commandInserirMarca.Parameters.AddWithValue("@nomeMarca", nomeMarca);
                commandInserirMarca.ExecuteNonQuery();

                // Obter o ID da nova marca inserida
                return (int)commandInserirMarca.LastInsertedId;
            }
        }


        private int ObterOuCriarUnidadeMedida(string descricaoUnidade, MySqlConnection mysqlConnection)
        {
            if (mysqlConnection == null)
            {
                throw new InvalidOperationException("A conexão MySQL não foi inicializada.");
            }

            // Certifique-se de que a conexão está aberta
            if (mysqlConnection.State != ConnectionState.Open)
            {
                mysqlConnection.Open();
            }

            // Verificar se a unidade de medida já existe
            string queryVerificarUnidade = "SELECT Id FROM UnidadeMedida WHERE Sigla = @descricaoUnidade LIMIT 1";
            MySqlCommand commandVerificarUnidade = new MySqlCommand(queryVerificarUnidade, mysqlConnection);
            commandVerificarUnidade.Parameters.AddWithValue("@descricaoUnidade", descricaoUnidade);

            object result = commandVerificarUnidade.ExecuteScalar();

            if (result != null)
            {
                // Se a unidade de medida já existe, retornar o ID
                return Convert.ToInt32(result);
            }
            else
            {
                // Se a unidade de medida não existe, inserir a nova unidade e retornar o novo ID
                string queryInserirUnidade = "INSERT INTO UnidadeMedida (Descricao, Sigla, Empresa, FlagExcluido) VALUES (@descricaoUnidade,@sigla, 1, 0)";
                MySqlCommand commandInserirUnidade = new MySqlCommand(queryInserirUnidade, mysqlConnection);
                commandInserirUnidade.Parameters.AddWithValue("@descricaoUnidade", descricaoUnidade);
                commandInserirUnidade.Parameters.AddWithValue("@sigla", descricaoUnidade);
                commandInserirUnidade.ExecuteNonQuery();

                // Obter o ID da nova unidade de medida inserida
                return (int)commandInserirUnidade.LastInsertedId;
            }
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            string selectedDatabase = comboDestino.SelectedItem?.ToString();
            string firebirdDatabasePath = txtBancoOrigem.Text;
            ImportarGrupoProdutosSgbr(selectedDatabase, firebirdDatabasePath);
        }


        private void ImportarContasPagarSgbr(string database, string firebirdDatabasePath)
        {
            int i = 0;
            UpdateUI(() =>
            {
                lblStatus.Text = "Importação de Contas a Pagar";
            });
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;";
            FbConnection firebirdConnection = new FbConnection(firebirdConnectionString);
            FbCommand firebirdCommand = new FbCommand("SELECT * FROM tPagar Where tPagar.Quitada = 'NÃO' AND tPagar.DATAVENCIMENTO > '2024-08-01'", firebirdConnection);

            // Conexão com o banco de dados MySQL
            string mysqlConnectionString = $"Server=localhost;Database={database};User Id=marcelo;Password=mx123;";
            MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);

            try
            {
                firebirdConnection.Open();
                mysqlConnection.Open();

                // Obter a quantidade total de registros
                FbCommand firebirdCountCommand = new FbCommand("SELECT COUNT(*) FROM tPagar Where tPagar.Quitada = 'NÃO' AND tPagar.DATAVENCIMENTO > '2024-08-01'", firebirdConnection);
                int totalRegistros = (int)firebirdCountCommand.ExecuteScalar();

                // Configurar a ProgressBar
                UpdateUI(() =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = totalRegistros;
                    progressBar1.Value = 0;
                });

                FbDataReader firebirdReader = firebirdCommand.ExecuteReader();
                while (firebirdReader.Read())
                {
                    i++;
                    ContaPagar contaPagar = new ContaPagar();
                    //ID no SGBR

                    PessoaController pessoaControlle = new PessoaController();
                    PessoaDAO pessoaDAO = new PessoaDAO();
                    contaPagar.Pessoa = pessoaDAO.selecionarPessoaPorCodigoImportadoEFornecedor(firebirdReader["codfornecedor"].ToString(), true);
                    contaPagar.NDup = firebirdReader["documento"].ToString();
                    contaPagar.VDup = decimal.Parse(firebirdReader["valoraserpago"].ToString());
                    contaPagar.DVenc = DateTime.Parse(firebirdReader["datavencimento"].ToString());
                    contaPagar.Nfe = null;
                    contaPagar.DataExclusao = DateTime.Now.AddYears(-100);
                    contaPagar.DataOrigem = DateTime.Parse(firebirdReader["datahoracadastro"].ToString());
                    contaPagar.AcrescimoBaixa = 0;
                    contaPagar.CaixaPagamento = "";
                    contaPagar.DataPagamento = DateTime.Now.AddYears(-100);
                    contaPagar.DescontoBaixa = 0;
                    contaPagar.Descricao = firebirdReader["descricaolancamento"].ToString();
                    contaPagar.DescricaoPagamento = "";
                    EmpresaFilial empresaFilial = new EmpresaFilial();
                    empresaFilial.Id = 1;
                    empresaFilial = (EmpresaFilial)Controller.getInstance().selecionar(empresaFilial);
                    contaPagar.EmpresaFilial = empresaFilial;
                    contaPagar.FlagExcluido = false;
                    FormaPagamento formaPagamento = new FormaPagamento();
                    formaPagamento.Id = 5;
                    formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                    contaPagar.FormaPagamento = formaPagamento;
                    contaPagar.Historico = "";
                    contaPagar.NumeroDocumento = firebirdReader["documento"].ToString();
                    contaPagar.Pago = false;
                    PlanoConta planoConta = new PlanoConta();
                    planoConta.Id = 2;
                    planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                    contaPagar.PlanoConta = planoConta;
                    contaPagar.ValorPago = 0;
                    contaPagar.ValorTotal = decimal.Parse(firebirdReader["valoraserpago"].ToString());


                    // Inserir dados no MySQL
                    string mysqlQuery = @"
    INSERT INTO contapagar 
    (
        Pessoa, 
        NDup, 
        VDup, 
        DVenc, 
        Nfe, 
        DataExclusao, 
        DataOrigem, 
        AcrescimoBaixa, 
        CaixaPagamento, 
        DataPagamento, 
        DescontoBaixa, 
        Descricao, 
        DescricaoPagamento, 
        EmpresaFilial, 
        FlagExcluido, 
        FormaPagamento, 
        Historico, 
        NumeroDocumento, 
        Pago, 
        PlanoConta, 
        ValorPago, 
        ValorTotal
    ) 
    VALUES 
    (
        @Pessoa, 
        @NDup, 
        @VDup, 
        @DVenc, 
        @Nfe, 
        @DataExclusao, 
        @DataOrigem, 
        @AcrescimoBaixa, 
        @CaixaPagamento, 
        @DataPagamento, 
        @DescontoBaixa, 
        @Descricao, 
        @DescricaoPagamento, 
        @EmpresaFilial, 
        @FlagExcluido, 
        @FormaPagamento, 
        @Historico, 
        @NumeroDocumento, 
        @Pago, 
        @PlanoConta, 
        @ValorPago, 
        @ValorTotal
    )";

                    MySqlCommand mysqlCommand = new MySqlCommand(mysqlQuery, mysqlConnection);
                    mysqlCommand.Parameters.AddWithValue("@Pessoa", contaPagar.Pessoa.Id);
                    mysqlCommand.Parameters.AddWithValue("@NDup", contaPagar.NDup);
                    mysqlCommand.Parameters.AddWithValue("@VDup", contaPagar.VDup);
                    mysqlCommand.Parameters.AddWithValue("@DVenc", contaPagar.DVenc);
                    mysqlCommand.Parameters.AddWithValue("@Nfe", (object)contaPagar.Nfe ?? DBNull.Value);
                    mysqlCommand.Parameters.AddWithValue("@DataExclusao", contaPagar.DataExclusao);
                    mysqlCommand.Parameters.AddWithValue("@DataOrigem", contaPagar.DataOrigem);
                    mysqlCommand.Parameters.AddWithValue("@AcrescimoBaixa", contaPagar.AcrescimoBaixa);
                    mysqlCommand.Parameters.AddWithValue("@CaixaPagamento", contaPagar.CaixaPagamento);
                    mysqlCommand.Parameters.AddWithValue("@DataPagamento", contaPagar.DataPagamento);
                    mysqlCommand.Parameters.AddWithValue("@DescontoBaixa", contaPagar.DescontoBaixa);
                    mysqlCommand.Parameters.AddWithValue("@Descricao", contaPagar.Descricao);
                    mysqlCommand.Parameters.AddWithValue("@DescricaoPagamento", contaPagar.DescricaoPagamento);
                    mysqlCommand.Parameters.AddWithValue("@EmpresaFilial", contaPagar.EmpresaFilial.Id);
                    mysqlCommand.Parameters.AddWithValue("@FlagExcluido", contaPagar.FlagExcluido);
                    mysqlCommand.Parameters.AddWithValue("@FormaPagamento", contaPagar.FormaPagamento.Id);
                    mysqlCommand.Parameters.AddWithValue("@Historico", contaPagar.Historico);
                    mysqlCommand.Parameters.AddWithValue("@NumeroDocumento", contaPagar.NumeroDocumento);
                    mysqlCommand.Parameters.AddWithValue("@Pago", contaPagar.Pago);
                    mysqlCommand.Parameters.AddWithValue("@PlanoConta", contaPagar.PlanoConta.Id);
                    mysqlCommand.Parameters.AddWithValue("@ValorPago", contaPagar.ValorPago);
                    mysqlCommand.Parameters.AddWithValue("@ValorTotal", contaPagar.ValorTotal);

                    mysqlCommand.ExecuteNonQuery();
                    long contaPagarID = mysqlCommand.LastInsertedId;

                    UpdateUI(() =>
                    {
                        progressBar1.Value += 1;
                        lblStatus.Text = "Conta Pagar " + i + " de " + totalRegistros;
                    });
                }
                firebirdReader.Close();
                UpdateUI(() =>
                {
                    progressBar1.Value = 0;
                    lblStatus.Text = "Contas a Pagar Importado com Sucesso";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao importar dados: " + ex.Message);
            }
            finally
            {
                firebirdConnection.Close();
                mysqlConnection.Close();
            }
        }

        private void importarProdutosLinkProCervantes()
        {
            connectionStringLinkPro = "Host="+txtBancoOrigem.Text.Trim()+ ";Port=5432;Username=postgres;Password=postgres;Database=InkDB";
            try
            {
                //connectionStringLinkPro = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=InkDB";

                using (var connection = new NpgsqlConnection(connectionStringLinkPro))
                {
                    connection.Open();
                    string countQuery = "SELECT COUNT(*) FROM produto p INNER JOIN prod_cod_barras pcb ON pcb.id_produto = p.id_produto";

                    int totalRecords;
                    using (var countCommand = new NpgsqlCommand(countQuery, connection))
                    {
                        totalRecords = Convert.ToInt32(countCommand.ExecuteScalar());
                        Console.WriteLine($"Número total de registros: {totalRecords}");
                    }

                    lblStatus.Text = "Conexão estabelecida";
                    using (var command = new NpgsqlCommand("SELECT p.produto_codigo, p.descricao, p.inativo, p.qtd_estoque, " +
                        "p.cod_ncm, p.preco_custo, p.preco_venda, p.cest, pcb.codigo_barra FROM produto p inner join prod_cod_barras pcb on pcb.id_produto = p.id_produto", connection))
                    {
                        command.CommandTimeout = 300; // Aumenta o tempo limite para 5 minutos

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                int rowIndex = 0;
                                int i = 0;
                                while (reader.Read())
                                {
                                    try
                                    {
                                        rowIndex++;
                                        // Exemplo de leitura de dados
                                        var produto_codigo = reader["produto_codigo"].ToString();
                                        var descricao = reader["descricao"].ToString();
                                        var inativo = reader["inativo"].ToString();
                                        var qtd_estoque = reader["qtd_estoque"].ToString();
                                        var cod_ncm = reader["cod_ncm"].ToString();
                                        var preco_custo = reader["preco_custo"].ToString();
                                        var preco_venda = reader["preco_venda"].ToString();
                                        var cest = reader["cest"].ToString();
                                        var codigo_barra = reader["codigo_barra"].ToString();

                                        Produto produto = new Produto();
                                        if (chkIdProduto.Checked == true)
                                            produto.Id = int.Parse(produto_codigo);
                                        else
                                            produto.Id = 0;
                                        produto.Observacoes = produto_codigo + " SGBR";
                                        produto.AnoVeiculo = "";
                                        produto.CapacidadeTracao = "";
                                        if(cest != "null" && cest != "Null" && cest != "NULL")
                                            produto.Cest = Generica.RemoveCaracteres(cest);

                                        produto.CfopVenda = Generica.RemoveCaracteres("");
                                        produto.Chassi = "";
                                        produto.CilindradaCc = "";
                                        produto.CodAnp = "";
                                        produto.CodSeloIpi = "";
                                        produto.Combustivel = "";
                                        produto.CondicaoProduto = "";
                                        produto.ControlaEstoque = true;
                                        produto.CorDenatran = "";
                                        produto.CorMontadora = "";
                                        produto.CstCofins = "99";
                                        produto.CstIcms = "102";
                                        produto.CstIpi = "99";
                                        produto.CstPis = "99";
                                        produto.DataCadastro = DateTime.Now;
                                        produto.Descricao = descricao;
                                        produto.DistanciaEixo = "";
                                        produto.Ean = codigo_barra;
                                        produto.Ecommerce = false;
                                        produto.EnqIpi = "999";
                                        produto.EspecieVeiculo = "";

                                        produto.FlagExcluido = false;
                                        produto.Grade = false;
                                        string grupoFiscal = "1";
                                        produto.IdComplementar = "";
                                        produto.KmEntrada = "";
                                        produto.LotacaoVeiculo = "";
                                        produto.Marca = null;
                                        produto.MarcaModelo = "";
                                        produto.Markup = "";
                                        produto.ModeloVeiculo = "";
                                        produto.Ncm = cod_ncm;
                                        produto.NumeroMotor = "";
                                        produto.OperadorCadastro = "1";
                                        produto.OrigemIcms = "0";
                                        produto.PercentualCofins = "0";
                                        produto.PercentualIcms = "0";
                                        produto.PercentualIpi = "0";
                                        produto.PercentualPis = "0";
                                        try { produto.PercGlp = double.Parse("0"); } catch { produto.PercGlp = 0; }
                                        try { produto.PercGni = double.Parse("0"); } catch { produto.PercGni = 0; }
                                        try { produto.PercGnn = double.Parse("0"); } catch { produto.PercGnn = 0; }
                                        produto.Pesavel = false;
                                        produto.PesoBrutoVeiculo = "";
                                        produto.PesoLiquidoVeiculo = "";
                                        produto.Placa = "";
                                        produto.PotenciaCv = "";
                                        produto.ProdutoSetor = null;
                                        produto.ProdutoSubGrupo = null;
                                        produto.Referencia = "";
                                        produto.Renavam = "";
                                        produto.RestricaoVeiculo = "";
                                        produto.SolicitaNumeroSerie = false;
                                        produto.TipoCambio = "";
                                        produto.TipoEntrada = "";
                                        produto.TipoPintura = "";
                                        produto.TipoProduto = "REVENDA";
                                        produto.TipoVeiculo = "";
                                        string selectedDatabase = "";
                                        if (comboDestino.InvokeRequired)
                                        {
                                            // Se a chamada precisa ser feita na thread da UI, use Invoke
                                            comboDestino.Invoke(new Action(() =>
                                            {
                                                selectedDatabase = comboDestino.SelectedItem?.ToString();
                                                // Agora você pode usar selectedDatabase aqui
                                            }));
                                        }
                                        else
                                        {
                                            // Caso contrário, faça o acesso diretamente
                                            selectedDatabase = comboDestino.SelectedItem?.ToString();
                                            // Agora você pode usar selectedDatabase aqui
                                        }
                                        string mysqlConnectionString = $"Server=localhost;Database={selectedDatabase};User Id=marcelo;Password=mx123;";
                                        MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);

                                        int unidadeMedida = ObterOuCriarUnidadeMedida(RemoverCedilha("PC"), mysqlConnection);
                                        produto.UnidadeMedida = new UnidadeMedida();
                                        produto.UnidadeMedida.Id = unidadeMedida;
                                        produto.GrupoFiscal = new GrupoFiscal();
                                        produto.GrupoFiscal.Id = 1;

                                        try { produto.ValorCusto = decimal.Parse(preco_custo); } catch { produto.ValorCusto = 0; }
                                        produto.ValorPartida = 0;
                                        try { produto.ValorVenda = decimal.Parse(preco_venda); } catch { produto.ValorVenda = 1; }
                                        produto.Veiculo = false;
                                        produto.VeiculoNovo = false;

                                        ProdutoGrade produtoGrade = new ProdutoGrade();
                                        produtoGrade.ValorVenda = produto.ValorVenda;
                                        produtoGrade.DataCadastro = DateTime.Now;
                                        produtoGrade.QuantidadeMedida = 1;
                                        produtoGrade.Descricao = "PRINCIPAL";
                                        produtoGrade.FlagExcluido = false;
                                        produtoGrade.UnidadeMedida = new UnidadeMedida();
                                        produtoGrade.UnidadeMedida.Id = 2;
                                        produtoGrade.ValorVenda = produto.ValorVenda;
                                        produtoGrade.Principal = true;

                                        Estoque estoque = new Estoque();
                                        try
                                        {
                                            if (double.Parse(qtd_estoque) > 0)
                                            {
                                                produto.Estoque = 0;
                                                estoque.BalancoEstoque = null;
                                                estoque.Conciliado = false;
                                                estoque.DataEntradaSaida = DateTime.Now;
                                                estoque.Descricao = "IMPORTACAO LINK";
                                                estoque.Entrada = true;
                                                estoque.FlagExcluido = false;
                                                estoque.Origem = "LINK PRO";
                                                estoque.Pessoa = null;
                                                estoque.Quantidade = double.Parse(qtd_estoque);
                                                estoque.QuantidadeInventario = 0;
                                                estoque.Saida = false;


                                                //Grava estoque auxiliar ou apenas o contabil
                                                if (chkSaldoEstoque.Checked == true)
                                                    produto.EstoqueAuxiliar = double.Parse(qtd_estoque);
                                                else
                                                    produto.EstoqueAuxiliar = 0;
                                            }
                                        }
                                        catch
                                        {
                                            produto.Estoque = 0;
                                            produto.EstoqueAuxiliar = 0;
                                        }
                                        i++;
                                        salvarProdutoLinkNoMysql(produto, estoque, produtoGrade, mysqlConnection);
                                        lblStatus.Text = "Produto " + i + " de " + totalRecords;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Erro ao ler a linha {rowIndex}: {ex.Message}");
                                        // Talvez seja interessante parar o loop se um erro ocorrer
                                        break;
                                    }
                                    lblStatus.Text = "Produtos Importados: " + i;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nenhum dado encontrado.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ou executar a consulta: {ex.Message}");
            }

        }

        private void importarClientesLinkProCervantes()
        {
            connectionStringLinkPro = "Host=" + txtBancoOrigem.Text.Trim() + ";Port=5432;Username=postgres;Password=postgres;Database=InkDB";
            try
            {
                //connectionStringLinkPro = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=InkDB";

                using (var connection = new NpgsqlConnection(connectionStringLinkPro))
                {
                    connection.Open();
                    string countQuery = "SELECT COUNT(*) FROM cliente";

                    int totalRecords;
                    using (var countCommand = new NpgsqlCommand(countQuery, connection))
                    {
                        totalRecords = Convert.ToInt32(countCommand.ExecuteScalar());
                        Console.WriteLine($"Número total de registros: {totalRecords}");
                    }

                    //lblStatus.Text = "Conexão estabelecida";
                    using (var command = new NpgsqlCommand("SELECT c.id_cliente, c.nome, c.fone, cid.descricao,cid.sigla_estado, " +
                        "c.logradouro, c.numero, c.complemento, c.bairro, c.cep, c.email, c.cpf, c.data_nascimento, " +
                        "c.nome_pai, c.nome_mae FROM cliente c inner join cidade cid on cid.id_cidade = c.id_cidade", connection))
                    {
                        command.CommandTimeout = 300; // Aumenta o tempo limite para 5 minutos

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                int rowIndex = 0;
                                int i = 0;
                                while (reader.Read())
                                {
                                    try
                                    {
                                        rowIndex++;
                                        Pessoa pessoa = new Pessoa();
                                        // Exemplo de leitura de dados
                                        pessoa.Id = 0;
                                        pessoa.Cliente = true;
                                        pessoa.Email = reader["email"].ToString();
                                        if(reader["cpf"].ToString() != "null")
                                            pessoa.Cnpj = reader["cpf"].ToString();
                                        else
                                            pessoa.Cnpj = "";
                                        pessoa.Cobrador = false;
                                        pessoa.CodigoImportacao = reader["id_cliente"].ToString();
                                        pessoa.ComissaoVendedor = 0;
                                        pessoa.ContatoTrabalho = "";
                                        pessoa.DataCadastro = DateTime.Now;
                                        if(reader["data_nascimento"].ToString() == "Null") 
                                            pessoa.DataNascimento = DateTime.Parse("1900-01-01 00:00:00");
                                        else
                                            pessoa.DataNascimento = DateTime.Parse(reader["data_nascimento"].ToString());
                                        pessoa.EnderecoPrincipal = null;
                                        pessoa.EscritorioCobranca = false;
                                        pessoa.FlagExcluido = false;
                                        pessoa.Fornecedor = false;
                                        pessoa.FuncaoTrabalho = "";
                                        pessoa.Funcionario = false;
                                        pessoa.InscricaoEstadual = "";
                                        pessoa.LimiteCredito = 0;
                                        pessoa.LocalTrabalho = "";
                                        pessoa.Mae = reader["nome_mae"].ToString();
                                        pessoa.NomeFantasia = reader["nome"].ToString();
                                        pessoa.Observacoes = "IMPORTADO SISTEMA LINK";
                                        pessoa.PessoaTelefone = null;
                                        pessoa.RazaoSocial = reader["nome"].ToString();
                                        pessoa.ReceberLembrete = false;
                                        pessoa.RegistradoSpc = false;
                                        pessoa.Rg = "";
                                        pessoa.SalarioTrabalho = "";
                                        pessoa.Sexo = "";
                                        pessoa.Tecnico = false;
                                        pessoa.TelefoneTrabalho = "";
                                        pessoa.TempoTrabalho = "";
                                        pessoa.TipoParceiroImportado = "";
                                        if (pessoa.Cnpj.Length == 11)
                                            pessoa.TipoPessoa = "PF";
                                        else if (pessoa.Cnpj.Length == 14)
                                            pessoa.TipoPessoa = "PJ";
                                        else pessoa.TipoPessoa = "PF";
                                        pessoa.Transportadora = false;
                                        pessoa.Vendedor = false;

                                       //Conecta com banco lunar
                                        string selectedDatabase = "";
                                        if (comboDestino.InvokeRequired)
                                        {
                                            // Se a chamada precisa ser feita na thread da UI, use Invoke
                                            comboDestino.Invoke(new Action(() =>
                                            {
                                                selectedDatabase = comboDestino.SelectedItem?.ToString();
                                                // Agora você pode usar selectedDatabase aqui
                                            }));
                                        }
                                        else
                                        {
                                            // Caso contrário, faça o acesso diretamente
                                            selectedDatabase = comboDestino.SelectedItem?.ToString();
                                            // Agora você pode usar selectedDatabase aqui
                                        }
                                        string mysqlConnectionString = $"Server=localhost;Database={selectedDatabase};User Id=marcelo;Password=mx123;";
                                        MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);



                                        //Ajustar cidade e endereco
                                        Endereco endereco = new Endereco();
                                        Cidade cidade = null;

                                        string uf = reader["sigla_estado"].ToString();
                                        string cidadeDescricao = reader["descricao"].ToString();

                                        string mysqlQueryCidade = @"
                        SELECT c.*
                        FROM Cidade c
                        INNER JOIN Estado e ON c.Estado = e.Id
                        WHERE c.Descricao = @descricao AND e.Uf = @uf
                        LIMIT 1";

                                        MySqlCommand mysqlCommandCidade = new MySqlCommand(mysqlQueryCidade, mysqlConnection);
                                        mysqlCommandCidade.Parameters.AddWithValue("@descricao", cidadeDescricao);
                                        mysqlCommandCidade.Parameters.AddWithValue("@uf", uf);

                                        using (MySqlDataReader cidadeReader = mysqlCommandCidade.ExecuteReader())
                                        {
                                            if (cidadeReader.Read())
                                            {
                                                cidade = new Cidade
                                                {
                                                    Id = cidadeReader.GetInt32("Id"),
                                                    Descricao = cidadeReader.GetString("Descricao")
                                                };
                                            }
                                        }
                                        if (cidade != null)
                                        {
                                            if (cidade.Id > 0)
                                            {
                                                endereco.Referencia = "";
                                                if(reader["cep"].ToString() != "" || reader["cep"].ToString() != "Null")
                                                    endereco.Cep = reader["cep"].ToString();
                                                else
                                                    endereco.Cep = "38610000";
                                                endereco.Numero = reader["numero"].ToString();
                                                endereco.Bairro = reader["bairro"].ToString();
                                                endereco.Cidade = new Cidade();
                                                endereco.Cidade = cidade;
                                                endereco.Logradouro = reader["logradouro"].ToString();
                                                endereco.Complemento = reader["complemento"].ToString();
                                            }
                                        }
                                        PessoaTelefone pessoaTelefone = new PessoaTelefone();
                                        if (!String.IsNullOrEmpty(reader["fone"].ToString()))
                                        {
                                            pessoaTelefone = ProcessarTelefone(reader["fone"].ToString());
                                        }


                                        i++;
                                        salvarClienteLinkProCervantes(mysqlConnectionString, pessoa,endereco,pessoaTelefone);
                                        //salvarProdutoLinkNoMysql(produto, estoque, produtoGrade, mysqlConnection);
                                        //lblStatus.Text = "Pessoa " + i + " de " + totalRecords;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Erro ao ler a linha {rowIndex}: {ex.Message}");
                                        // Talvez seja interessante parar o loop se um erro ocorrer
                                        break;
                                    }
                                    //lblStatus.Text = "Pessoa Importados: " + i;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nenhum dado encontrado.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ou executar a consulta: {ex.Message}");
            }

        }

        private void salvarClienteLinkProCervantes(String mySqlConnectionString, Pessoa pessoa, Endereco endereco, PessoaTelefone pessoaTelefone)
        {
            using (var connection = new MySqlConnection(mySqlConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert into Pessoa
                        string mysqlQueryPessoa = @"
                INSERT INTO Pessoa 
                (CodigoImportacao, Observacoes, RazaoSocial, NomeFantasia, Cnpj, Email, ReceberLembrete, Vendedor, Cliente, Fornecedor, FlagExcluido, Funcionario, Transportadora, EscritorioCobranca, Tecnico, FuncaoTrabalho, InscricaoEstadual, ComissaoVendedor, ContatoTrabalho, DataNascimento, LimiteCredito, LocalTrabalho, Mae, Pai, RegistradoSpc, Rg, SalarioTrabalho, Sexo, TelefoneTrabalho, TempoTrabalho, TipoParceiroImportado, TipoPessoa, DataCadastro)
                VALUES 
                (@CodigoImportacao, @Observacoes, @RazaoSocial, @NomeFantasia, @Cnpj, @Email, @ReceberLembrete, @Vendedor, @Cliente, @Fornecedor, @FlagExcluido, @Funcionario, @Transportadora, @EscritorioCobranca, @Tecnico, @FuncaoTrabalho, @InscricaoEstadual, @ComissaoVendedor, @ContatoTrabalho, @DataNascimento, @LimiteCredito, @LocalTrabalho, @Mae, @Pai, @RegistradoSpc, @Rg, @SalarioTrabalho, @Sexo, @TelefoneTrabalho, @TempoTrabalho, @TipoParceiroImportado, @TipoPessoa, @DataCadastro)";

                        var mysqlCommandPessoa = new MySqlCommand(mysqlQueryPessoa, connection, transaction);
                        mysqlCommandPessoa.Parameters.AddWithValue("@CodigoImportacao", pessoa.CodigoImportacao);
                        mysqlCommandPessoa.Parameters.AddWithValue("@Observacoes", pessoa.Observacoes);
                        mysqlCommandPessoa.Parameters.AddWithValue("@RazaoSocial", pessoa.RazaoSocial);
                        mysqlCommandPessoa.Parameters.AddWithValue("@NomeFantasia", pessoa.NomeFantasia);
                        mysqlCommandPessoa.Parameters.AddWithValue("@Cnpj", pessoa.Cnpj);
                        mysqlCommandPessoa.Parameters.AddWithValue("@Email", pessoa.Email);
                        mysqlCommandPessoa.Parameters.AddWithValue("@ReceberLembrete", pessoa.ReceberLembrete);
                        mysqlCommandPessoa.Parameters.AddWithValue("@Vendedor", pessoa.Vendedor);
                        mysqlCommandPessoa.Parameters.AddWithValue("@Cliente", pessoa.Cliente);
                        mysqlCommandPessoa.Parameters.AddWithValue("@Fornecedor", pessoa.Fornecedor);
                        mysqlCommandPessoa.Parameters.AddWithValue("@FlagExcluido", pessoa.FlagExcluido);
                        mysqlCommandPessoa.Parameters.AddWithValue("@Funcionario", pessoa.Funcionario);
                        mysqlCommandPessoa.Parameters.AddWithValue("@Transportadora", pessoa.Transportadora);
                        mysqlCommandPessoa.Parameters.AddWithValue("@EscritorioCobranca", pessoa.EscritorioCobranca);
                        mysqlCommandPessoa.Parameters.AddWithValue("@Tecnico", pessoa.Tecnico);
                        mysqlCommandPessoa.Parameters.AddWithValue("@FuncaoTrabalho", pessoa.FuncaoTrabalho);
                        mysqlCommandPessoa.Parameters.AddWithValue("@InscricaoEstadual", pessoa.InscricaoEstadual);
                        mysqlCommandPessoa.Parameters.AddWithValue("@ComissaoVendedor", pessoa.ComissaoVendedor);
                        mysqlCommandPessoa.Parameters.AddWithValue("@ContatoTrabalho", pessoa.ContatoTrabalho);
                        mysqlCommandPessoa.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento == DateTime.MinValue ? (object)DBNull.Value : pessoa.DataNascimento);
                        mysqlCommandPessoa.Parameters.AddWithValue("@LimiteCredito", pessoa.LimiteCredito);
                        mysqlCommandPessoa.Parameters.AddWithValue("@LocalTrabalho", pessoa.LocalTrabalho);
                        mysqlCommandPessoa.Parameters.AddWithValue("@Mae", pessoa.Mae);
                        mysqlCommandPessoa.Parameters.AddWithValue("@Pai", pessoa.Pai);
                        mysqlCommandPessoa.Parameters.AddWithValue("@RegistradoSpc", pessoa.RegistradoSpc);
                        mysqlCommandPessoa.Parameters.AddWithValue("@Rg", pessoa.Rg);
                        mysqlCommandPessoa.Parameters.AddWithValue("@SalarioTrabalho", pessoa.SalarioTrabalho);
                        mysqlCommandPessoa.Parameters.AddWithValue("@Sexo", pessoa.Sexo);
                        mysqlCommandPessoa.Parameters.AddWithValue("@TelefoneTrabalho", pessoa.TelefoneTrabalho);
                        mysqlCommandPessoa.Parameters.AddWithValue("@TempoTrabalho", pessoa.TempoTrabalho);
                        mysqlCommandPessoa.Parameters.AddWithValue("@TipoParceiroImportado", pessoa.TipoParceiroImportado);
                        mysqlCommandPessoa.Parameters.AddWithValue("@TipoPessoa", pessoa.TipoPessoa);
                        mysqlCommandPessoa.Parameters.AddWithValue("@DataCadastro", pessoa.DataCadastro);

                        mysqlCommandPessoa.ExecuteNonQuery();
                        long pessoaId = mysqlCommandPessoa.LastInsertedId;

                        // Insert Endereco if necessary
                        if (pessoaId > 0 && !string.IsNullOrEmpty(endereco.Logradouro))
                        {
                            string mysqlQueryEndereco = @"
                    INSERT INTO Endereco (logradouro, numero, bairro, cep, complemento, cidade, pessoa, empresafilial, referencia) 
                    VALUES (@logradouro, @numero, @bairro, @cep, @complemento, @cidadeId, @pessoaId, 1, @referencia)";

                            var mysqlCommandEndereco = new MySqlCommand(mysqlQueryEndereco, connection, transaction);
                            mysqlCommandEndereco.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
                            mysqlCommandEndereco.Parameters.AddWithValue("@numero", endereco.Numero);
                            mysqlCommandEndereco.Parameters.AddWithValue("@bairro", endereco.Bairro);
                            mysqlCommandEndereco.Parameters.AddWithValue("@cep", endereco.Cep);
                            mysqlCommandEndereco.Parameters.AddWithValue("@complemento", endereco.Complemento);
                            mysqlCommandEndereco.Parameters.AddWithValue("@cidadeId", endereco.Cidade.Id);
                            mysqlCommandEndereco.Parameters.AddWithValue("@pessoaId", pessoaId);
                            mysqlCommandEndereco.Parameters.AddWithValue("@referencia", endereco.Referencia);
                            mysqlCommandEndereco.ExecuteNonQuery();
                            long enderecoId = mysqlCommandEndereco.LastInsertedId;

                            // Update Pessoa with EnderecoPrincipal
                            string mysqlUpdatePessoaEndereco = "UPDATE Pessoa SET enderecoPrincipal = @enderecoPrincipal WHERE id = @pessoaId";
                            var mysqlCommandUpdatePessoaEndereco = new MySqlCommand(mysqlUpdatePessoaEndereco, connection, transaction);
                            mysqlCommandUpdatePessoaEndereco.Parameters.AddWithValue("@enderecoPrincipal", enderecoId);
                            mysqlCommandUpdatePessoaEndereco.Parameters.AddWithValue("@pessoaId", pessoaId);
                            mysqlCommandUpdatePessoaEndereco.ExecuteNonQuery();
                        }

                        // Insert PessoaTelefone if necessary
                        if (!string.IsNullOrEmpty(pessoaTelefone.Telefone))
                        {
                            string mysqlQueryTelefone = @"
                    INSERT INTO PessoaTelefone (ddd, telefone, observacoes, pessoa) 
                    VALUES (@ddd, @telefone, @observacoes, @pessoa)";

                            var mysqlCommandTelefone = new MySqlCommand(mysqlQueryTelefone, connection, transaction);
                            mysqlCommandTelefone.Parameters.AddWithValue("@ddd", pessoaTelefone.Ddd);
                            mysqlCommandTelefone.Parameters.AddWithValue("@telefone", pessoaTelefone.Telefone);
                            mysqlCommandTelefone.Parameters.AddWithValue("@observacoes", "IMPORTADO SGBR");
                            mysqlCommandTelefone.Parameters.AddWithValue("@pessoa", pessoaId);
                            mysqlCommandTelefone.ExecuteNonQuery();
                            long pessoaTelefoneId = mysqlCommandTelefone.LastInsertedId;

                            // Update Pessoa with PessoaTelefone
                            string mysqlUpdatePessoaTelefone = "UPDATE Pessoa SET pessoaTelefone = @pessoaTelefone WHERE id = @pessoaId";
                            var mysqlCommandUpdatePessoaTelefone = new MySqlCommand(mysqlUpdatePessoaTelefone, connection, transaction);
                            mysqlCommandUpdatePessoaTelefone.Parameters.AddWithValue("@pessoaTelefone", pessoaTelefoneId);
                            mysqlCommandUpdatePessoaTelefone.Parameters.AddWithValue("@pessoaId", pessoaId);
                            mysqlCommandUpdatePessoaTelefone.ExecuteNonQuery();
                        }

                        // Commit transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction on error
                        transaction.Rollback();
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }

        private void radioLinkPro_CheckedChanged(object sender, EventArgs e)
        {
            if (radioLinkPro.Checked == true)
            {
                lblOrigem.Text = "Digite aqui o Nome do Servidor do LinkPro (Apenas Importação de Produtos Disponível)";
                txtBancoOrigem.Enabled = true;
                txtBancoOrigem.ReadOnly = false;
                txtBancoOrigem.Focus();
                txtBancoOrigem.SelectAll();
                //chkClientes.Checked = false;
                chkContasAPagar.Checked = false;
                chkContasAReceber.Checked = false;
                chkServicos.Checked = false;
                //chkClientes.Enabled = false;
                chkContasAPagar.Enabled = false;
                chkContasAReceber.Enabled = false;
                chkServicos.Enabled = false;
            }
            else
            {
                lblOrigem.Text = "Banco de Dados Origem(Sistema Antigo do Cliente)";
                txtBancoOrigem.Enabled = false;
                txtBancoOrigem.ReadOnly = true;
                txtBancoOrigem.Text = "";
                chkClientes.Checked = true;
                chkContasAPagar.Checked = true;
                chkContasAReceber.Checked = true;
                chkServicos.Checked = true;
                chkClientes.Enabled = true;
                chkContasAPagar.Enabled = true;
                chkContasAReceber.Enabled = true;
                chkServicos.Enabled = true;
            }
        }

        private void salvarProdutoLinkNoMysql(Produto produto,Estoque estoque,ProdutoGrade produtoGrade, MySqlConnection mysqlConnection)
        {
            string mysqlQuery = @"
                INSERT INTO Produto 
                (Id, Observacoes, AnoVeiculo, CapacidadeTracao, Cest, CfopVenda, Chassi, CilindradaCc, CodAnp, CodSeloIpi, Combustivel, 
                CondicaoProduto, ControlaEstoque, CorDenatran, CorMontadora, CstCofins, CstIcms, CstIpi, CstPis, DataCadastro, Descricao, 
                DistanciaEixo, Ean, Ecommerce, EnqIpi, EspecieVeiculo, Estoque, EstoqueAuxiliar, FlagExcluido, Grade, IdComplementar, 
                KmEntrada, LotacaoVeiculo, Marca, MarcaModelo, Markup, ModeloVeiculo, Ncm, NumeroMotor, OperadorCadastro, OrigemIcms, 
                PercentualCofins, PercentualIcms, PercentualIpi, PercentualPis, PercGlp, PercGni, PercGnn, Pesavel, PesoBrutoVeiculo, 
                PesoLiquidoVeiculo, Placa, PotenciaCv, ProdutoSetor, Referencia, Renavam, RestricaoVeiculo, 
                SolicitaNumeroSerie, TipoCambio, TipoEntrada, TipoPintura, TipoProduto, TipoVeiculo, UnidadeMedida, ValorCusto, 
                ValorPartida, ValorVenda, Veiculo, VeiculoNovo, Empresa, EmpresaFilial, GrupoFiscal, ProdutoGrupo) 
                VALUES 
                (@Id, @Observacoes, @AnoVeiculo, @CapacidadeTracao, @Cest, @CfopVenda, @Chassi, @CilindradaCc, @CodAnp, @CodSeloIpi, @Combustivel, 
                @CondicaoProduto, @ControlaEstoque, @CorDenatran, @CorMontadora, @CstCofins, @CstIcms, @CstIpi, @CstPis, @DataCadastro, @Descricao, 
                @DistanciaEixo, @Ean, @Ecommerce, @EnqIpi, @EspecieVeiculo, @Estoque, @EstoqueAuxiliar, @FlagExcluido, @Grade, @IdComplementar, 
                @KmEntrada, @LotacaoVeiculo, @MarcaId, @MarcaModelo, @Markup, @ModeloVeiculo, @Ncm, @NumeroMotor, @OperadorCadastro, @OrigemIcms, 
                @PercentualCofins, @PercentualIcms, @PercentualIpi, @PercentualPis, @PercGlp, @PercGni, @PercGnn, @Pesavel, @PesoBrutoVeiculo, 
                @PesoLiquidoVeiculo, @Placa, @PotenciaCv, @ProdutoSetor, @Referencia, @Renavam, @RestricaoVeiculo, 
                @SolicitaNumeroSerie, @TipoCambio, @TipoEntrada, @TipoPintura, @TipoProduto, @TipoVeiculo, @UnidadeMedidaId, @ValorCusto, 
                @ValorPartida, @ValorVenda, @Veiculo, @VeiculoNovo, 1,1, "+produto.GrupoFiscal.Id+", @GrupoProduto)";

            MySqlCommand mysqlCommand = new MySqlCommand(mysqlQuery, mysqlConnection);
            mysqlCommand.Parameters.AddWithValue("@Id", produto.Id);
            mysqlCommand.Parameters.AddWithValue("@Observacoes", produto.Observacoes);
            mysqlCommand.Parameters.AddWithValue("@AnoVeiculo", produto.AnoVeiculo);
            mysqlCommand.Parameters.AddWithValue("@CapacidadeTracao", produto.CapacidadeTracao);
            mysqlCommand.Parameters.AddWithValue("@Cest", produto.Cest);
            mysqlCommand.Parameters.AddWithValue("@CfopVenda", produto.CfopVenda);
            mysqlCommand.Parameters.AddWithValue("@Chassi", produto.Chassi);
            mysqlCommand.Parameters.AddWithValue("@CilindradaCc", produto.CilindradaCc);
            mysqlCommand.Parameters.AddWithValue("@CodAnp", produto.CodAnp);
            mysqlCommand.Parameters.AddWithValue("@CodSeloIpi", produto.CodSeloIpi);
            mysqlCommand.Parameters.AddWithValue("@Combustivel", produto.Combustivel);
            mysqlCommand.Parameters.AddWithValue("@CondicaoProduto", produto.CondicaoProduto);
            mysqlCommand.Parameters.AddWithValue("@ControlaEstoque", produto.ControlaEstoque);
            mysqlCommand.Parameters.AddWithValue("@CorDenatran", produto.CorDenatran);
            mysqlCommand.Parameters.AddWithValue("@CorMontadora", produto.CorMontadora);
            mysqlCommand.Parameters.AddWithValue("@CstCofins", produto.CstCofins);
            mysqlCommand.Parameters.AddWithValue("@CstIcms", produto.CstIcms);
            mysqlCommand.Parameters.AddWithValue("@CstIpi", produto.CstIpi);
            mysqlCommand.Parameters.AddWithValue("@CstPis", produto.CstPis);
            mysqlCommand.Parameters.AddWithValue("@DataCadastro", produto.DataCadastro);
            mysqlCommand.Parameters.AddWithValue("@Descricao", produto.Descricao);
            mysqlCommand.Parameters.AddWithValue("@DistanciaEixo", produto.DistanciaEixo);
            mysqlCommand.Parameters.AddWithValue("@Ean", produto.Ean);
            mysqlCommand.Parameters.AddWithValue("@Ecommerce", produto.Ecommerce);
            mysqlCommand.Parameters.AddWithValue("@EnqIpi", produto.EnqIpi);
            mysqlCommand.Parameters.AddWithValue("@EspecieVeiculo", produto.EspecieVeiculo);
            mysqlCommand.Parameters.AddWithValue("@Estoque", produto.Estoque);
            mysqlCommand.Parameters.AddWithValue("@EstoqueAuxiliar", produto.EstoqueAuxiliar);
            mysqlCommand.Parameters.AddWithValue("@FlagExcluido", produto.FlagExcluido);
            mysqlCommand.Parameters.AddWithValue("@Grade", produto.Grade);
            mysqlCommand.Parameters.AddWithValue("@IdComplementar", produto.IdComplementar);
            mysqlCommand.Parameters.AddWithValue("@KmEntrada", produto.KmEntrada);
            mysqlCommand.Parameters.AddWithValue("@LotacaoVeiculo", produto.LotacaoVeiculo);
            mysqlCommand.Parameters.AddWithValue("@MarcaId", null);
            mysqlCommand.Parameters.AddWithValue("@MarcaModelo", produto.MarcaModelo);
            mysqlCommand.Parameters.AddWithValue("@Markup", produto.Markup);
            mysqlCommand.Parameters.AddWithValue("@ModeloVeiculo", produto.ModeloVeiculo);
            mysqlCommand.Parameters.AddWithValue("@Ncm", produto.Ncm);
            mysqlCommand.Parameters.AddWithValue("@NumeroMotor", produto.NumeroMotor);
            mysqlCommand.Parameters.AddWithValue("@OperadorCadastro", produto.OperadorCadastro);
            mysqlCommand.Parameters.AddWithValue("@OrigemIcms", produto.OrigemIcms);
            mysqlCommand.Parameters.AddWithValue("@PercentualCofins", produto.PercentualCofins);
            mysqlCommand.Parameters.AddWithValue("@PercentualIcms", produto.PercentualIcms);
            mysqlCommand.Parameters.AddWithValue("@PercentualIpi", produto.PercentualIpi);
            mysqlCommand.Parameters.AddWithValue("@PercentualPis", produto.PercentualPis);
            mysqlCommand.Parameters.AddWithValue("@PercGlp", produto.PercGlp);
            mysqlCommand.Parameters.AddWithValue("@PercGni", produto.PercGni);
            mysqlCommand.Parameters.AddWithValue("@PercGnn", produto.PercGnn);
            mysqlCommand.Parameters.AddWithValue("@Pesavel", produto.Pesavel);
            mysqlCommand.Parameters.AddWithValue("@PesoBrutoVeiculo", produto.PesoBrutoVeiculo);
            mysqlCommand.Parameters.AddWithValue("@PesoLiquidoVeiculo", produto.PesoLiquidoVeiculo);
            mysqlCommand.Parameters.AddWithValue("@Placa", produto.Placa);
            mysqlCommand.Parameters.AddWithValue("@PotenciaCv", produto.PotenciaCv);
            mysqlCommand.Parameters.AddWithValue("@ProdutoSetor", produto.ProdutoSetor);
            mysqlCommand.Parameters.AddWithValue("@ProdutoSubGrupo", null);
            mysqlCommand.Parameters.AddWithValue("@Referencia", produto.Referencia);
            mysqlCommand.Parameters.AddWithValue("@Renavam", produto.Renavam);
            mysqlCommand.Parameters.AddWithValue("@RestricaoVeiculo", produto.RestricaoVeiculo);
            mysqlCommand.Parameters.AddWithValue("@SolicitaNumeroSerie", produto.SolicitaNumeroSerie);
            mysqlCommand.Parameters.AddWithValue("@TipoCambio", produto.TipoCambio);
            mysqlCommand.Parameters.AddWithValue("@TipoEntrada", produto.TipoEntrada);
            mysqlCommand.Parameters.AddWithValue("@TipoPintura", produto.TipoPintura);
            mysqlCommand.Parameters.AddWithValue("@TipoProduto", produto.TipoProduto);
            mysqlCommand.Parameters.AddWithValue("@TipoVeiculo", produto.TipoVeiculo);
            mysqlCommand.Parameters.AddWithValue("@UnidadeMedidaId", produto.UnidadeMedida.Id);
            mysqlCommand.Parameters.AddWithValue("@ValorCusto", produto.ValorCusto);
            mysqlCommand.Parameters.AddWithValue("@ValorPartida", produto.ValorPartida);
            mysqlCommand.Parameters.AddWithValue("@ValorVenda", produto.ValorVenda);
            mysqlCommand.Parameters.AddWithValue("@Veiculo", produto.Veiculo);
            mysqlCommand.Parameters.AddWithValue("@VeiculoNovo", produto.VeiculoNovo);
            mysqlCommand.Parameters.AddWithValue("@GrupoProduto", 1);

            mysqlCommand.ExecuteNonQuery();
            int idProduto = (int)mysqlCommand.LastInsertedId;
            if (chkIdProduto.Checked == true)
                idProduto = produto.Id;


            // Defina a consulta SQL para inserir dados na tabela ProdutoGrade
            string mysqlQueryProdutoGrade = @"
    INSERT INTO ProdutoGrade 
        (ValorVenda, DataCadastro, QuantidadeMedida, Descricao, FlagExcluido, UnidadeMedida, Principal, Produto) 
    VALUES 
        (@ValorVenda, @DataCadastro, @QuantidadeMedida, @Descricao, @FlagExcluido, @UnidadeMedida, @Principal, @Produto);
    SELECT LAST_INSERT_ID();";  // Adiciona a consulta para obter o ID inserido

            int idGradePrincipal = 0;
            // Crie o comando MySqlCommand
            using (MySqlCommand mysqlCommandProdutoGrade = new MySqlCommand(mysqlQueryProdutoGrade, mysqlConnection))
            {
                // Adicione os parâmetros com os valores do objeto produtoGrade
                mysqlCommandProdutoGrade.Parameters.AddWithValue("@ValorVenda", produtoGrade.ValorVenda);
                mysqlCommandProdutoGrade.Parameters.AddWithValue("@DataCadastro", produtoGrade.DataCadastro);
                mysqlCommandProdutoGrade.Parameters.AddWithValue("@QuantidadeMedida", produtoGrade.QuantidadeMedida);
                mysqlCommandProdutoGrade.Parameters.AddWithValue("@Descricao", produtoGrade.Descricao);
                mysqlCommandProdutoGrade.Parameters.AddWithValue("@FlagExcluido", produtoGrade.FlagExcluido);
                mysqlCommandProdutoGrade.Parameters.AddWithValue("@UnidadeMedida", 2);
                mysqlCommandProdutoGrade.Parameters.AddWithValue("@Principal", produtoGrade.Principal);
                mysqlCommandProdutoGrade.Parameters.AddWithValue("@Produto", idProduto);

                // Execute o comando e obtenha o ID inserido
                var idInserido = mysqlCommandProdutoGrade.ExecuteScalar();  // ExecuteScalar é usado para retornar um valor único
                idGradePrincipal = int.Parse(idInserido.ToString());
            }

            string mysqlQueryUpdateProduto = @"
    UPDATE Produto 
    SET GradePrincipal = @GradePrincipal 
    WHERE Id = @IdProduto";

            // Crie o comando MySqlCommand para o UPDATE
            using (MySqlCommand mysqlCommandUpdateProduto = new MySqlCommand(mysqlQueryUpdateProduto, mysqlConnection))
            {
                // Adicione os parâmetros com os valores apropriados
                mysqlCommandUpdateProduto.Parameters.AddWithValue("@GradePrincipal", idGradePrincipal);
                mysqlCommandUpdateProduto.Parameters.AddWithValue("@IdProduto", idProduto);  // Supondo que idProduto é o identificador do produto a ser atualizado

                // Execute o comando
                int rowsAffected = mysqlCommandUpdateProduto.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Produto atualizado com sucesso.");
                }
                else
                {
                    Console.WriteLine("Nenhum produto foi atualizado. Verifique o IdProduto.");
                }
            }


            if (String.IsNullOrEmpty(produto.Ean))
            {
                string codigoBarras;
                bool codigoExiste;

                do
                {
                    CreateEan13();
                    ean13.Scale = 1;
                    codigoBarras = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
                    codigoExiste = CodigoBarrasExiste(codigoBarras);
                } while (codigoExiste);

                produto.Ean = codigoBarras;
            }
            ProdutoCodigoBarras produtoCodigoBarras = new ProdutoCodigoBarras();
            produtoCodigoBarras.CodigoBarras = produto.Ean;
            produtoCodigoBarras.ProdutoGrade = new ProdutoGrade();
            produtoCodigoBarras.ProdutoGrade.Id = idGradePrincipal;
            produtoCodigoBarras.DataCadastro = DateTime.Now;
            produtoCodigoBarras.OperadorCadastro = "1";
            produtoCodigoBarras.FlagExcluido = false;
            produtoCodigoBarras.Produto = new Produto();
            produtoCodigoBarras.Produto.Id = idProduto;

            string mysqlQueryProdutoCodigoBarras = @"
                INSERT INTO ProdutoCodigoBarras 
                    (CodigoBarras, ProdutoGrade, DataCadastro, OperadorCadastro, FlagExcluido, Produto) 
                VALUES 
                    (@CodigoBarras, @ProdutoGrade, @DataCadastro, @OperadorCadastro, @FlagExcluido, @Produto);
                SELECT LAST_INSERT_ID();";  // Adiciona a consulta para obter o ID inserido

            using (MySqlCommand mysqlCommandProdutoCodigoBarras = new MySqlCommand(mysqlQueryProdutoCodigoBarras, mysqlConnection))
            {
                // Adicione os parâmetros com os valores do objeto produtoCodigoBarras
                mysqlCommandProdutoCodigoBarras.Parameters.AddWithValue("@CodigoBarras", produtoCodigoBarras.CodigoBarras);
                mysqlCommandProdutoCodigoBarras.Parameters.AddWithValue("@ProdutoGrade", produtoCodigoBarras.ProdutoGrade.Id);
                mysqlCommandProdutoCodigoBarras.Parameters.AddWithValue("@DataCadastro", produtoCodigoBarras.DataCadastro);
                mysqlCommandProdutoCodigoBarras.Parameters.AddWithValue("@OperadorCadastro", produtoCodigoBarras.OperadorCadastro);
                mysqlCommandProdutoCodigoBarras.Parameters.AddWithValue("@FlagExcluido", produtoCodigoBarras.FlagExcluido);
                mysqlCommandProdutoCodigoBarras.Parameters.AddWithValue("@Produto", idProduto);

                // Execute o comando e obtenha o ID inserido
                var idInserido = mysqlCommandProdutoCodigoBarras.ExecuteScalar();  // ExecuteScalar é usado para retornar um valor único
                int idProdutoCodigoBarras = int.Parse(idInserido.ToString());
            }


            if (chkSaldoEstoque.Checked == true && produto.EstoqueAuxiliar > 0)
            {
                //Nao conciliado
                estoque.Conciliado = false;
                string mysqlQueryEstoque = @"
                        INSERT INTO Estoque 
                            (BalancoEstoque, Conciliado, DataEntradaSaida, Descricao, Entrada, FlagExcluido, Origem, Pessoa, Quantidade, QuantidadeInventario, Saida, EmpresaFilial, Produto) 
                        VALUES 
                            (@BalancoEstoque, @Conciliado, @DataEntradaSaida, @Descricao, @Entrada, @FlagExcluido, @Origem, @Pessoa, @Quantidade, @QuantidadeInventario, @Saida, 1, " + idProduto + ")";
     

                MySqlCommand mysqlCommandEstoque = new MySqlCommand(mysqlQueryEstoque, mysqlConnection);
                mysqlCommandEstoque = new MySqlCommand(mysqlQueryEstoque, mysqlConnection);
                mysqlCommandEstoque.Parameters.AddWithValue("@BalancoEstoque", (object)estoque.BalancoEstoque ?? DBNull.Value);
                mysqlCommandEstoque.Parameters.AddWithValue("@Conciliado", estoque.Conciliado);
                mysqlCommandEstoque.Parameters.AddWithValue("@DataEntradaSaida", estoque.DataEntradaSaida);
                mysqlCommandEstoque.Parameters.AddWithValue("@Descricao", estoque.Descricao);
                mysqlCommandEstoque.Parameters.AddWithValue("@Entrada", estoque.Entrada);
                mysqlCommandEstoque.Parameters.AddWithValue("@FlagExcluido", estoque.FlagExcluido);
                mysqlCommandEstoque.Parameters.AddWithValue("@Origem", estoque.Origem);
                mysqlCommandEstoque.Parameters.AddWithValue("@Pessoa", (object)estoque.Pessoa ?? DBNull.Value);
                mysqlCommandEstoque.Parameters.AddWithValue("@Quantidade", estoque.Quantidade);
                mysqlCommandEstoque.Parameters.AddWithValue("@QuantidadeInventario", estoque.QuantidadeInventario);
                mysqlCommandEstoque.Parameters.AddWithValue("@Saida", estoque.Saida);
                // Execute o comando
                mysqlCommandEstoque.ExecuteNonQuery();
            }
        }
        private Ean13 ean13 = null;
        public void CreateEan13()
        {
            ean13 = new Ean13();
            ean13.CountryCode = RandomNumber(10, 78).ToString();
            ean13.ManufacturerCode = RandomNumber(79000, 99000).ToString();
            ean13.ProductCode = RandomNumber(10000, 99000).ToString();
            ean13.ChecksumDigit = RandomNumber(1, 9).ToString();
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public bool CodigoBarrasExiste(string codigoBarras)
        {
            ProdutoCodigoBarrasController produtoCodigoBarrasController = new ProdutoCodigoBarrasController();
            string sql = $"SELECT * FROM ProdutoCodigoBarras WHERE CodigoBarras = '{codigoBarras}' and FlagExcluido <> True";
            IList<ProdutoCodigoBarras> resultado = produtoCodigoBarrasController.selecionarCodigoBarrasPorSQL(sql);
            return resultado.Count > 0;
        }

        private void ImportarClientesSoftsystem(string database, string firebirdDatabasePath)
        {
            int i = 0;
            UpdateUI(() =>
            {
                lblStatus.Text = "Importação de Clientes";
            });
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;Charset=ISO8859_1;";
            FbConnection firebirdConnection = new FbConnection(firebirdConnectionString);
            FbCommand firebirdCommand = new FbCommand("SELECT * FROM Clientes", firebirdConnection);

            // Conexão com o banco de dados MySQL
            string mysqlConnectionString = $"Server=localhost;Database={database};User Id=marcelo;Password=mx123;";
            MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);

            try
            {
                firebirdConnection.Open();
                mysqlConnection.Open();

                // Obter a quantidade total de registros
                FbCommand firebirdCountCommand = new FbCommand("SELECT COUNT(*) FROM Clientes", firebirdConnection);
                long totalRegistros = (long)firebirdCountCommand.ExecuteScalar();

                // Configurar a ProgressBar
                UpdateUI(() =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = (int)Math.Min(totalRegistros, int.MaxValue);
                    progressBar1.Value = 0;
                });

                FbDataReader firebirdReader = firebirdCommand.ExecuteReader();
                while (firebirdReader.Read())
                {
                    i++;
                    Pessoa pessoa = new Pessoa();
                    //ID no SGBR
                    pessoa.CodigoImportacao = firebirdReader["cgc"].ToString();
                    pessoa.Observacoes = "";
                    pessoa.RazaoSocial = firebirdReader["razaosocial"].ToString();
                    if (pessoa.RazaoSocial.Length > 80)
                    {
                        pessoa.RazaoSocial = pessoa.RazaoSocial.Substring(0, 80);
                    }
                    pessoa.NomeFantasia = firebirdReader["nomefantasia"].ToString();
                    if (pessoa.NomeFantasia.Length > 80)
                    {
                        pessoa.NomeFantasia = pessoa.NomeFantasia.Substring(0, 80);
                    }
                    //remove letras e deixa somente numeros
                    pessoa.Cnpj = Regex.Replace(firebirdReader["cgc"].ToString(), @"[^\d]", "");
                    pessoa.Email = firebirdReader["email"].ToString();

                    pessoa.ReceberLembrete = false;
                    pessoa.Vendedor = false;
                    pessoa.Cliente = true;
                    pessoa.Fornecedor = false;
                    pessoa.FlagExcluido = false;
                    pessoa.Funcionario = false;
                    pessoa.Transportadora = false;
                    pessoa.EscritorioCobranca = false;
                    pessoa.Tecnico = false;
                    pessoa.FuncaoTrabalho = "";
                    pessoa.InscricaoEstadual = Generica.RemoveCaracteres(firebirdReader["inscestadual"].ToString());
                    if (pessoa.InscricaoEstadual.Equals("ISENTO"))
                        pessoa.InscricaoEstadual = "";
                    pessoa.ComissaoVendedor = 0;
                    pessoa.ContatoTrabalho = "";
                    try
                    {
                        pessoa.DataNascimento = string.IsNullOrEmpty(firebirdReader["aniversario"].ToString())
                                                ? new DateTime(1900, 1, 1)
                                                : DateTime.Parse(firebirdReader["aniversario"].ToString());
                    }
                    catch
                    {
                        pessoa.DataNascimento = new DateTime(1900, 1, 1); // Define a data padrão em caso de erro
                    }
                    pessoa.LimiteCredito = 0;
                    pessoa.LocalTrabalho = "";
                    pessoa.Mae = firebirdReader["nomemae"].ToString();
                    pessoa.Pai = firebirdReader["nomepai"].ToString();
                    pessoa.RegistradoSpc = false;
                    pessoa.Rg = "";
                    pessoa.SalarioTrabalho = "";
                    pessoa.Sexo = firebirdReader["sexo"]?.ToString() ?? "";
                    pessoa.DataCadastro = DateTime.Parse(firebirdReader["datacadastro"].ToString());
                    pessoa.FlagExcluido = firebirdReader["inativo"] != DBNull.Value && firebirdReader["inativo"].ToString().Equals("T");
                    pessoa.TelefoneTrabalho = "";
                    pessoa.TempoTrabalho = "";
                    pessoa.TipoParceiroImportado = "";
                    pessoa.TipoPessoa = "PF";
                    if (firebirdReader["cgc"].ToString().Length == 14)
                        pessoa.TipoPessoa = "PJ";

                    pessoa.EnderecoPrincipal = null;
                    pessoa.PessoaTelefone = null;

                    Endereco endereco = new Endereco();

                    Cidade cidade = null;
                    string uf = firebirdReader["uf"].ToString();
                    string cidadeDescricao = firebirdReader["cidade"].ToString();

                    string mysqlQueryCidade = @"
                        SELECT c.*
                        FROM Cidade c
                        INNER JOIN Estado e ON c.Estado = e.Id
                        WHERE c.Descricao = @descricao AND e.Uf = @uf
                        LIMIT 1";

                    MySqlCommand mysqlCommandCidade = new MySqlCommand(mysqlQueryCidade, mysqlConnection);
                    mysqlCommandCidade.Parameters.AddWithValue("@descricao", cidadeDescricao);
                    mysqlCommandCidade.Parameters.AddWithValue("@uf", uf);

                    using (MySqlDataReader cidadeReader = mysqlCommandCidade.ExecuteReader())
                    {
                        if (cidadeReader.Read())
                        {
                            cidade = new Cidade
                            {
                                Id = cidadeReader.GetInt32("Id"),
                                Descricao = cidadeReader.GetString("Descricao")
                            };
                        }
                    }
                    if (cidade != null)
                    {
                        if (cidade.Id > 0)
                        {
                            string num = extrairNumeroString(firebirdReader["endereco"].ToString());
                            endereco.Referencia = "";
                            endereco.Cep = Generica.RemoveCaracteres(firebirdReader["cep"].ToString());
                            endereco.Numero = num;
                            endereco.Bairro = firebirdReader["bairro"].ToString();
                            endereco.Cidade = new Cidade();
                            endereco.Cidade = cidade;
                            endereco.Logradouro = firebirdReader["endereco"].ToString();
                            endereco.Complemento = "";
                        }
                    }
                    PessoaTelefone pessoaTelefone = new PessoaTelefone();
                    PessoaTelefone pessoaTelefoneCelular = new PessoaTelefone();
                    if (!String.IsNullOrEmpty(firebirdReader["fone"].ToString()))
                        pessoaTelefone = ProcessarTelefone(firebirdReader["ddd"].ToString() + firebirdReader["fone"].ToString());
                    if (!String.IsNullOrEmpty(firebirdReader["celular"].ToString()))
                        pessoaTelefoneCelular = ProcessarTelefone(firebirdReader["ddd"].ToString() + firebirdReader["celular"].ToString());
                    
                    // Inserir dados no MySQL
                    string mysqlQuery = @"
                        INSERT INTO Pessoa 
                        (CodigoImportacao, Observacoes, RazaoSocial, NomeFantasia, Cnpj, Email, ReceberLembrete, Vendedor, Cliente, Fornecedor, FlagExcluido, Funcionario, Transportadora, EscritorioCobranca, Tecnico, FuncaoTrabalho, InscricaoEstadual, ComissaoVendedor, ContatoTrabalho, DataNascimento, LimiteCredito, LocalTrabalho, Mae, Pai, RegistradoSpc, Rg, SalarioTrabalho, Sexo, TelefoneTrabalho, TempoTrabalho, TipoParceiroImportado, TipoPessoa, DataCadastro)
                        VALUES 
                        (@CodigoImportacao, @Observacoes, @RazaoSocial, @NomeFantasia, @Cnpj, @Email, @ReceberLembrete, @Vendedor, @Cliente, @Fornecedor, @FlagExcluido, @Funcionario, @Transportadora, @EscritorioCobranca, @Tecnico, @FuncaoTrabalho, @InscricaoEstadual, @ComissaoVendedor, @ContatoTrabalho, @DataNascimento, @LimiteCredito, @LocalTrabalho, @Mae, @Pai, @RegistradoSpc, @Rg, @SalarioTrabalho, @Sexo, @TelefoneTrabalho, @TempoTrabalho, @TipoParceiroImportado, @TipoPessoa, @DataCadastro)";

                    MySqlCommand mysqlCommand = new MySqlCommand(mysqlQuery, mysqlConnection);
                    mysqlCommand.Parameters.AddWithValue("@CodigoImportacao", pessoa.CodigoImportacao);
                    mysqlCommand.Parameters.AddWithValue("@Observacoes", pessoa.Observacoes);
                    mysqlCommand.Parameters.AddWithValue("@RazaoSocial", pessoa.RazaoSocial);
                    mysqlCommand.Parameters.AddWithValue("@NomeFantasia", pessoa.NomeFantasia);
                    mysqlCommand.Parameters.AddWithValue("@Cnpj", pessoa.Cnpj);
                    mysqlCommand.Parameters.AddWithValue("@Email", pessoa.Email);
                    mysqlCommand.Parameters.AddWithValue("@ReceberLembrete", pessoa.ReceberLembrete);
                    mysqlCommand.Parameters.AddWithValue("@Vendedor", pessoa.Vendedor);
                    mysqlCommand.Parameters.AddWithValue("@Cliente", pessoa.Cliente);
                    mysqlCommand.Parameters.AddWithValue("@Fornecedor", pessoa.Fornecedor);
                    mysqlCommand.Parameters.AddWithValue("@FlagExcluido", pessoa.FlagExcluido);
                    mysqlCommand.Parameters.AddWithValue("@Funcionario", pessoa.Funcionario);
                    mysqlCommand.Parameters.AddWithValue("@Transportadora", pessoa.Transportadora);
                    mysqlCommand.Parameters.AddWithValue("@EscritorioCobranca", pessoa.EscritorioCobranca);
                    mysqlCommand.Parameters.AddWithValue("@Tecnico", pessoa.Tecnico);
                    mysqlCommand.Parameters.AddWithValue("@FuncaoTrabalho", pessoa.FuncaoTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@InscricaoEstadual", pessoa.InscricaoEstadual);
                    mysqlCommand.Parameters.AddWithValue("@ComissaoVendedor", pessoa.ComissaoVendedor);
                    mysqlCommand.Parameters.AddWithValue("@ContatoTrabalho", pessoa.ContatoTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento == DateTime.MinValue ? (object)DBNull.Value : pessoa.DataNascimento);
                    mysqlCommand.Parameters.AddWithValue("@LimiteCredito", pessoa.LimiteCredito);
                    mysqlCommand.Parameters.AddWithValue("@LocalTrabalho", pessoa.LocalTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@Mae", pessoa.Mae);
                    mysqlCommand.Parameters.AddWithValue("@Pai", pessoa.Pai);
                    mysqlCommand.Parameters.AddWithValue("@RegistradoSpc", pessoa.RegistradoSpc);
                    mysqlCommand.Parameters.AddWithValue("@Rg", pessoa.Rg);
                    mysqlCommand.Parameters.AddWithValue("@SalarioTrabalho", pessoa.SalarioTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@Sexo", pessoa.Sexo);
                    mysqlCommand.Parameters.AddWithValue("@TelefoneTrabalho", pessoa.TelefoneTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@TempoTrabalho", pessoa.TempoTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@TipoParceiroImportado", pessoa.TipoParceiroImportado);
                    mysqlCommand.Parameters.AddWithValue("@TipoPessoa", pessoa.TipoPessoa);
                    mysqlCommand.Parameters.AddWithValue("@DataCadastro", pessoa.DataCadastro);

                    mysqlCommand.ExecuteNonQuery();
                    long pessoaId = mysqlCommand.LastInsertedId;

                    if (pessoaId > 0 && !String.IsNullOrEmpty(endereco.Logradouro))
                    {
                        string mysqlQueryEndereco = "INSERT INTO Endereco (logradouro, numero, bairro, cep, complemento, cidade, pessoa, empresafilial, referencia) VALUES (@logradouro, @numero, @bairro, @cep, @complemento, @cidadeId, @pessoaId, 1, @referencia)";
                        MySqlCommand mysqlCommandEndereco = new MySqlCommand(mysqlQueryEndereco, mysqlConnection);
                        mysqlCommandEndereco.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
                        mysqlCommandEndereco.Parameters.AddWithValue("@numero", endereco.Numero);
                        mysqlCommandEndereco.Parameters.AddWithValue("@bairro", endereco.Bairro);
                        mysqlCommandEndereco.Parameters.AddWithValue("@cep", endereco.Cep);
                        mysqlCommandEndereco.Parameters.AddWithValue("@complemento", endereco.Complemento);
                        mysqlCommandEndereco.Parameters.AddWithValue("@cidadeId", endereco.Cidade.Id);
                        mysqlCommandEndereco.Parameters.AddWithValue("@pessoaId", pessoaId);
                        mysqlCommandEndereco.Parameters.AddWithValue("@referencia", endereco.Referencia);
                        mysqlCommandEndereco.ExecuteNonQuery();
                        long enderecoId = mysqlCommandEndereco.LastInsertedId;

                        // Atualizar Pessoa com o EnderecoPrincipal
                        string mysqlUpdatePessoa = "UPDATE Pessoa SET enderecoPrincipal = @enderecoPrincipal WHERE id = @pessoaId";
                        MySqlCommand mysqlCommandUpdatePessoa = new MySqlCommand(mysqlUpdatePessoa, mysqlConnection);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@enderecoPrincipal", enderecoId);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@pessoaId", pessoaId);

                        mysqlCommandUpdatePessoa.ExecuteNonQuery();
                    }
                    // Salvar o telefone celular
                    long pessoaTelefoneId = 0;
                    if (!String.IsNullOrEmpty(pessoaTelefoneCelular.Telefone))
                    {
                        string mysqlQueryTelefoneCelular = "INSERT INTO PessoaTelefone (ddd, telefone, observacoes, pessoa) VALUES (@ddd, @telefone, @observacoes, @pessoa)";
                        MySqlCommand mysqlCommandTelefoneCelular = new MySqlCommand(mysqlQueryTelefoneCelular, mysqlConnection);
                        mysqlCommandTelefoneCelular.Parameters.AddWithValue("@ddd", pessoaTelefoneCelular.Ddd);
                        mysqlCommandTelefoneCelular.Parameters.AddWithValue("@telefone", pessoaTelefoneCelular.Telefone);
                        mysqlCommandTelefoneCelular.Parameters.AddWithValue("@observacoes", "IMPORTADO SOFTSYSTEM");
                        mysqlCommandTelefoneCelular.Parameters.AddWithValue("@pessoa", pessoaId);
                        mysqlCommandTelefoneCelular.ExecuteNonQuery();
                        pessoaTelefoneId = mysqlCommandTelefoneCelular.LastInsertedId; // Define o celular como telefone principal
                    }
                    //Salvar fixo
                    if (!String.IsNullOrEmpty(pessoaTelefone.Telefone))
                    {
                        string mysqlQueryTelefone = "INSERT INTO PessoaTelefone (ddd, telefone, observacoes, pessoa) VALUES (@ddd, @telefone, @observacoes, @pessoa)";
                        MySqlCommand mysqlCommandTelefone = new MySqlCommand(mysqlQueryTelefone, mysqlConnection);
                        mysqlCommandTelefone.Parameters.AddWithValue("@ddd", pessoaTelefone.Ddd);
                        mysqlCommandTelefone.Parameters.AddWithValue("@telefone", pessoaTelefone.Telefone);
                        mysqlCommandTelefone.Parameters.AddWithValue("@observacoes", "IMPORTADO SOFTSYSTEM");
                        mysqlCommandTelefone.Parameters.AddWithValue("@pessoa", pessoaId);
                        mysqlCommandTelefone.ExecuteNonQuery();
                        if(pessoaTelefoneId == 0)
                            pessoaTelefoneId = mysqlCommandTelefone.LastInsertedId; // Define o telefone fixo como telefone principal
                    }

                    // Atualizar Pessoa com o Telefone Principal
                    if (pessoaTelefoneId > 0)
                    {
                        string mysqlUpdatePessoa = "UPDATE Pessoa SET pessoaTelefone = @pessoaTelefone WHERE id = @pessoaId";
                        MySqlCommand mysqlCommandUpdatePessoa = new MySqlCommand(mysqlUpdatePessoa, mysqlConnection);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@pessoaTelefone", pessoaTelefoneId);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@pessoaId", pessoaId);
                        mysqlCommandUpdatePessoa.ExecuteNonQuery();
                    }

                    UpdateUI(() =>
                    {
                        progressBar1.Value += 1;
                        lblStatus.Text = "Cliente " + i + " de " + totalRegistros;
                    });
                }
                firebirdReader.Close();
                //MessageBox.Show("Importação de clientes concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUI(() =>
                {
                    progressBar1.Value = 0;
                    lblStatus.Text = "Importação de clientes realizada com sucesso!";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao importar dados: " + ex.Message);
            }
            finally
            {
                firebirdConnection.Close();
                mysqlConnection.Close();
            }
        }

        private void ImportarFornecedoresSoftsystem(string database, string firebirdDatabasePath)
        {
            int i = 0;
            UpdateUI(() =>
            {
                lblStatus.Text = "Importação de Fornecedores";
            });
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;Charset=ISO8859_1;";
            FbConnection firebirdConnection = new FbConnection(firebirdConnectionString);
            FbCommand firebirdCommand = new FbCommand("SELECT * FROM Fornecedores", firebirdConnection);

            // Conexão com o banco de dados MySQL
            string mysqlConnectionString = $"Server=localhost;Database={database};User Id=marcelo;Password=mx123;";
            MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);

            try
            {
                firebirdConnection.Open();
                mysqlConnection.Open();

                // Obter a quantidade total de registros
                FbCommand firebirdCountCommand = new FbCommand("SELECT COUNT(*) FROM Fornecedores", firebirdConnection);
                long totalRegistros = (long)firebirdCountCommand.ExecuteScalar();

                // Configurar a ProgressBar
                UpdateUI(() =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = (int)Math.Min(totalRegistros, int.MaxValue);
                    progressBar1.Value = 0;
                });

                FbDataReader firebirdReader = firebirdCommand.ExecuteReader();
                while (firebirdReader.Read())
                {
                    i++;
                    Pessoa pessoa = new Pessoa();
                    //ID no SGBR
                    pessoa.CodigoImportacao = firebirdReader["codfornecedor"].ToString();
                    pessoa.Observacoes = "";
                    pessoa.RazaoSocial = firebirdReader["razaosocial"].ToString();
                    if (pessoa.RazaoSocial.Length > 80)
                    {
                        pessoa.RazaoSocial = pessoa.RazaoSocial.Substring(0, 80);
                    }
                    pessoa.NomeFantasia = firebirdReader["nomefantasia"].ToString();
                    if (pessoa.NomeFantasia.Length > 80)
                    {
                        pessoa.NomeFantasia = pessoa.NomeFantasia.Substring(0, 80);
                    }
                    //remove letras e deixa somente numeros
                    pessoa.Cnpj = Regex.Replace(firebirdReader["cgc"].ToString(), @"[^\d]", "");
                    pessoa.Email = firebirdReader["email"].ToString();

                    pessoa.ReceberLembrete = false;
                    pessoa.Vendedor = false;
                    pessoa.Cliente = false;
                    pessoa.Fornecedor = true;
                    pessoa.FlagExcluido = false;
                    pessoa.Funcionario = false;
                    pessoa.Transportadora = false;
                    pessoa.EscritorioCobranca = false;
                    pessoa.Tecnico = false;
                    pessoa.FuncaoTrabalho = "";
                    pessoa.InscricaoEstadual = Generica.RemoveCaracteres(firebirdReader["ie"].ToString());
                    if (pessoa.InscricaoEstadual.Equals("ISENTO"))
                        pessoa.InscricaoEstadual = "";
                    pessoa.ComissaoVendedor = 0;
                    pessoa.ContatoTrabalho = "";
                    pessoa.DataNascimento = new DateTime(1900, 1, 1); // Define a data padrão no cosmos nao tem data de abertura da empresa
                    
                    pessoa.LimiteCredito = 0;
                    pessoa.LocalTrabalho = "";
                    pessoa.Mae = "";
                    pessoa.Pai = "";
                    pessoa.RegistradoSpc = false;
                    pessoa.Rg = "";
                    pessoa.SalarioTrabalho = "";
                    pessoa.Sexo = "";
                    pessoa.DataCadastro = DateTime.Parse(firebirdReader["datacadastro"].ToString());
                    pessoa.FlagExcluido = firebirdReader["inativo"] != DBNull.Value && firebirdReader["inativo"].ToString().Equals("T");
                    pessoa.TelefoneTrabalho = "";
                    pessoa.TempoTrabalho = "";
                    pessoa.TipoParceiroImportado = "";
                    pessoa.TipoPessoa = "PF";
                    if (firebirdReader["cgc"].ToString().Length == 14)
                        pessoa.TipoPessoa = "PJ";

                    pessoa.EnderecoPrincipal = null;
                    pessoa.PessoaTelefone = null;

                    Endereco endereco = new Endereco();

                    Cidade cidade = null;
                    string uf = firebirdReader["uf"].ToString();
                    string cidadeDescricao = firebirdReader["cidade"].ToString();

                    string mysqlQueryCidade = @"
                        SELECT c.*
                        FROM Cidade c
                        INNER JOIN Estado e ON c.Estado = e.Id
                        WHERE c.Descricao = @descricao AND e.Uf = @uf
                        LIMIT 1";

                    MySqlCommand mysqlCommandCidade = new MySqlCommand(mysqlQueryCidade, mysqlConnection);
                    mysqlCommandCidade.Parameters.AddWithValue("@descricao", cidadeDescricao);
                    mysqlCommandCidade.Parameters.AddWithValue("@uf", uf);

                    using (MySqlDataReader cidadeReader = mysqlCommandCidade.ExecuteReader())
                    {
                        if (cidadeReader.Read())
                        {
                            cidade = new Cidade
                            {
                                Id = cidadeReader.GetInt32("Id"),
                                Descricao = cidadeReader.GetString("Descricao")
                            };
                        }
                    }
                    if (cidade != null)
                    {
                        if (cidade.Id > 0)
                        {
                            string num = extrairNumeroString(firebirdReader["endereco"].ToString());
                            endereco.Referencia = "";
                            endereco.Cep = Generica.RemoveCaracteres(firebirdReader["cep"].ToString());
                            endereco.Numero = num;
                            endereco.Bairro = firebirdReader["bairro"].ToString();
                            endereco.Cidade = new Cidade();
                            endereco.Cidade = cidade;
                            endereco.Logradouro = firebirdReader["endereco"].ToString();
                            endereco.Complemento = "";
                        }
                    }
                    PessoaTelefone pessoaTelefone = new PessoaTelefone();
                    PessoaTelefone pessoaTelefoneCelular = new PessoaTelefone();
                    if (!String.IsNullOrEmpty(firebirdReader["fone"].ToString()))
                        pessoaTelefone = ProcessarTelefone(firebirdReader["ddd"].ToString() + firebirdReader["fone"].ToString());
                    if (!String.IsNullOrEmpty(firebirdReader["celular"].ToString()))
                        pessoaTelefoneCelular = ProcessarTelefone(firebirdReader["ddd"].ToString() + firebirdReader["fone2"].ToString());

                    // Inserir dados no MySQL
                    string mysqlQuery = @"
                        INSERT INTO Pessoa 
                        (CodigoImportacao, Observacoes, RazaoSocial, NomeFantasia, Cnpj, Email, ReceberLembrete, Vendedor, Cliente, Fornecedor, FlagExcluido, Funcionario, Transportadora, EscritorioCobranca, Tecnico, FuncaoTrabalho, InscricaoEstadual, ComissaoVendedor, ContatoTrabalho, DataNascimento, LimiteCredito, LocalTrabalho, Mae, Pai, RegistradoSpc, Rg, SalarioTrabalho, Sexo, TelefoneTrabalho, TempoTrabalho, TipoParceiroImportado, TipoPessoa, DataCadastro)
                        VALUES 
                        (@CodigoImportacao, @Observacoes, @RazaoSocial, @NomeFantasia, @Cnpj, @Email, @ReceberLembrete, @Vendedor, @Cliente, @Fornecedor, @FlagExcluido, @Funcionario, @Transportadora, @EscritorioCobranca, @Tecnico, @FuncaoTrabalho, @InscricaoEstadual, @ComissaoVendedor, @ContatoTrabalho, @DataNascimento, @LimiteCredito, @LocalTrabalho, @Mae, @Pai, @RegistradoSpc, @Rg, @SalarioTrabalho, @Sexo, @TelefoneTrabalho, @TempoTrabalho, @TipoParceiroImportado, @TipoPessoa, @DataCadastro)";

                    MySqlCommand mysqlCommand = new MySqlCommand(mysqlQuery, mysqlConnection);
                    mysqlCommand.Parameters.AddWithValue("@CodigoImportacao", pessoa.CodigoImportacao);
                    mysqlCommand.Parameters.AddWithValue("@Observacoes", pessoa.Observacoes);
                    mysqlCommand.Parameters.AddWithValue("@RazaoSocial", pessoa.RazaoSocial);
                    mysqlCommand.Parameters.AddWithValue("@NomeFantasia", pessoa.NomeFantasia);
                    mysqlCommand.Parameters.AddWithValue("@Cnpj", pessoa.Cnpj);
                    mysqlCommand.Parameters.AddWithValue("@Email", pessoa.Email);
                    mysqlCommand.Parameters.AddWithValue("@ReceberLembrete", pessoa.ReceberLembrete);
                    mysqlCommand.Parameters.AddWithValue("@Vendedor", pessoa.Vendedor);
                    mysqlCommand.Parameters.AddWithValue("@Cliente", pessoa.Cliente);
                    mysqlCommand.Parameters.AddWithValue("@Fornecedor", pessoa.Fornecedor);
                    mysqlCommand.Parameters.AddWithValue("@FlagExcluido", pessoa.FlagExcluido);
                    mysqlCommand.Parameters.AddWithValue("@Funcionario", pessoa.Funcionario);
                    mysqlCommand.Parameters.AddWithValue("@Transportadora", pessoa.Transportadora);
                    mysqlCommand.Parameters.AddWithValue("@EscritorioCobranca", pessoa.EscritorioCobranca);
                    mysqlCommand.Parameters.AddWithValue("@Tecnico", pessoa.Tecnico);
                    mysqlCommand.Parameters.AddWithValue("@FuncaoTrabalho", pessoa.FuncaoTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@InscricaoEstadual", pessoa.InscricaoEstadual);
                    mysqlCommand.Parameters.AddWithValue("@ComissaoVendedor", pessoa.ComissaoVendedor);
                    mysqlCommand.Parameters.AddWithValue("@ContatoTrabalho", pessoa.ContatoTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento == DateTime.MinValue ? (object)DBNull.Value : pessoa.DataNascimento);
                    mysqlCommand.Parameters.AddWithValue("@LimiteCredito", pessoa.LimiteCredito);
                    mysqlCommand.Parameters.AddWithValue("@LocalTrabalho", pessoa.LocalTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@Mae", pessoa.Mae);
                    mysqlCommand.Parameters.AddWithValue("@Pai", pessoa.Pai);
                    mysqlCommand.Parameters.AddWithValue("@RegistradoSpc", pessoa.RegistradoSpc);
                    mysqlCommand.Parameters.AddWithValue("@Rg", pessoa.Rg);
                    mysqlCommand.Parameters.AddWithValue("@SalarioTrabalho", pessoa.SalarioTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@Sexo", pessoa.Sexo);
                    mysqlCommand.Parameters.AddWithValue("@TelefoneTrabalho", pessoa.TelefoneTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@TempoTrabalho", pessoa.TempoTrabalho);
                    mysqlCommand.Parameters.AddWithValue("@TipoParceiroImportado", pessoa.TipoParceiroImportado);
                    mysqlCommand.Parameters.AddWithValue("@TipoPessoa", pessoa.TipoPessoa);
                    mysqlCommand.Parameters.AddWithValue("@DataCadastro", pessoa.DataCadastro);

                    mysqlCommand.ExecuteNonQuery();
                    long pessoaId = mysqlCommand.LastInsertedId;

                    if (pessoaId > 0 && !String.IsNullOrEmpty(endereco.Logradouro))
                    {
                        string mysqlQueryEndereco = "INSERT INTO Endereco (logradouro, numero, bairro, cep, complemento, cidade, pessoa, empresafilial, referencia) VALUES (@logradouro, @numero, @bairro, @cep, @complemento, @cidadeId, @pessoaId, 1, @referencia)";
                        MySqlCommand mysqlCommandEndereco = new MySqlCommand(mysqlQueryEndereco, mysqlConnection);
                        mysqlCommandEndereco.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
                        mysqlCommandEndereco.Parameters.AddWithValue("@numero", endereco.Numero);
                        mysqlCommandEndereco.Parameters.AddWithValue("@bairro", endereco.Bairro);
                        mysqlCommandEndereco.Parameters.AddWithValue("@cep", endereco.Cep);
                        mysqlCommandEndereco.Parameters.AddWithValue("@complemento", endereco.Complemento);
                        mysqlCommandEndereco.Parameters.AddWithValue("@cidadeId", endereco.Cidade.Id);
                        mysqlCommandEndereco.Parameters.AddWithValue("@pessoaId", pessoaId);
                        mysqlCommandEndereco.Parameters.AddWithValue("@referencia", endereco.Referencia);
                        mysqlCommandEndereco.ExecuteNonQuery();
                        long enderecoId = mysqlCommandEndereco.LastInsertedId;

                        // Atualizar Pessoa com o EnderecoPrincipal
                        string mysqlUpdatePessoa = "UPDATE Pessoa SET enderecoPrincipal = @enderecoPrincipal WHERE id = @pessoaId";
                        MySqlCommand mysqlCommandUpdatePessoa = new MySqlCommand(mysqlUpdatePessoa, mysqlConnection);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@enderecoPrincipal", enderecoId);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@pessoaId", pessoaId);

                        mysqlCommandUpdatePessoa.ExecuteNonQuery();
                    }
                    // Salvar o telefone celular
                    long pessoaTelefoneId = 0;
                    if (!String.IsNullOrEmpty(pessoaTelefoneCelular.Telefone))
                    {
                        string mysqlQueryTelefoneCelular = "INSERT INTO PessoaTelefone (ddd, telefone, observacoes, pessoa) VALUES (@ddd, @telefone, @observacoes, @pessoa)";
                        MySqlCommand mysqlCommandTelefoneCelular = new MySqlCommand(mysqlQueryTelefoneCelular, mysqlConnection);
                        mysqlCommandTelefoneCelular.Parameters.AddWithValue("@ddd", pessoaTelefoneCelular.Ddd);
                        mysqlCommandTelefoneCelular.Parameters.AddWithValue("@telefone", pessoaTelefoneCelular.Telefone);
                        mysqlCommandTelefoneCelular.Parameters.AddWithValue("@observacoes", "IMPORTADO SOFTSYSTEM");
                        mysqlCommandTelefoneCelular.Parameters.AddWithValue("@pessoa", pessoaId);
                        mysqlCommandTelefoneCelular.ExecuteNonQuery();
                        pessoaTelefoneId = mysqlCommandTelefoneCelular.LastInsertedId; // Define o celular como telefone principal
                    }
                    //Salvar fixo
                    if (!String.IsNullOrEmpty(pessoaTelefone.Telefone))
                    {
                        string mysqlQueryTelefone = "INSERT INTO PessoaTelefone (ddd, telefone, observacoes, pessoa) VALUES (@ddd, @telefone, @observacoes, @pessoa)";
                        MySqlCommand mysqlCommandTelefone = new MySqlCommand(mysqlQueryTelefone, mysqlConnection);
                        mysqlCommandTelefone.Parameters.AddWithValue("@ddd", pessoaTelefone.Ddd);
                        mysqlCommandTelefone.Parameters.AddWithValue("@telefone", pessoaTelefone.Telefone);
                        mysqlCommandTelefone.Parameters.AddWithValue("@observacoes", "IMPORTADO SOFTSYSTEM");
                        mysqlCommandTelefone.Parameters.AddWithValue("@pessoa", pessoaId);
                        mysqlCommandTelefone.ExecuteNonQuery();
                        if (pessoaTelefoneId == 0)
                            pessoaTelefoneId = mysqlCommandTelefone.LastInsertedId; // Define o telefone fixo como telefone principal
                    }

                    // Atualizar Pessoa com o Telefone Principal
                    if (pessoaTelefoneId > 0)
                    {
                        string mysqlUpdatePessoa = "UPDATE Pessoa SET pessoaTelefone = @pessoaTelefone WHERE id = @pessoaId";
                        MySqlCommand mysqlCommandUpdatePessoa = new MySqlCommand(mysqlUpdatePessoa, mysqlConnection);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@pessoaTelefone", pessoaTelefoneId);
                        mysqlCommandUpdatePessoa.Parameters.AddWithValue("@pessoaId", pessoaId);
                        mysqlCommandUpdatePessoa.ExecuteNonQuery();
                    }

                    UpdateUI(() =>
                    {
                        progressBar1.Value += 1;
                        lblStatus.Text = "Fornecedor " + i + " de " + totalRegistros;
                    });
                }
                firebirdReader.Close();
                //MessageBox.Show("Importação de clientes concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUI(() =>
                {
                    progressBar1.Value = 0;
                    lblStatus.Text = "Importação de fornecedores realizada com sucesso!";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao importar dados: " + ex.Message);
            }
            finally
            {
                firebirdConnection.Close();
                mysqlConnection.Close();
            }
        }

        private string extrairNumeroString(string valor)
        {
            Regex regex = new Regex(@"(?:Nº|N)?\s*(\d+)");
            Match match = regex.Match(valor);

            // Verifica se encontrou um número
            string numero = match.Success ? match.Groups[1].Value : "";
            return numero;
        }

        private void ImportarProdutosSoftsystem(string database, string firebirdDatabasePath)
        {
            ImportarGruposSoftsystem(database, firebirdDatabasePath);
            ImportarMarcasSoftsystem(database, firebirdDatabasePath);
       
            int i = 0;
            UpdateUI(() => { lblStatus.Text = "Importação de Produtos"; });
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;Charset=ISO8859_1;";
            FbConnection firebirdConnection = new FbConnection(firebirdConnectionString);
            FbCommand firebirdCommand = new FbCommand("SELECT * FROM produtos order by codproduto", firebirdConnection);

            // Conexão com o banco de dados MySQL
            string mysqlConnectionString = $"Server=localhost;Database={database};User Id=marcelo;Password=mx123;";
            MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);

            try
            {
                firebirdConnection.Open();
                mysqlConnection.Open();

                // Obter a quantidade total de registros
                FbCommand firebirdCountCommand = new FbCommand("SELECT COUNT(*) FROM produtos", firebirdConnection);
                long totalRegistros = (long)firebirdCountCommand.ExecuteScalar();

                // Configurar a ProgressBar
                UpdateUI(() =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = (int)Math.Min(totalRegistros, int.MaxValue);
                    progressBar1.Value = 0;
                });

                FbDataReader firebirdReader = firebirdCommand.ExecuteReader();
                while (firebirdReader.Read())
                {
                    i++;
                    Produto produto = new Produto();
                    //ID no SGBR

                    if (chkIdProduto.Checked == true)
                        produto.Id = int.Parse(firebirdReader["codproduto"].ToString());
                    else
                        produto.Id = 0;
                    produto.Observacoes = firebirdReader["codproduto"].ToString() + " SOFTSYSTEM";
                    produto.AnoVeiculo = "";
                    produto.CapacidadeTracao = "";
                    produto.Cest = Generica.RemoveCaracteres(firebirdReader["cest"].ToString());
                    produto.CfopVenda = VerificarCFOPPorProdutoSoftsystem(firebirdReader["CODCLFISCAL"].ToString(), firebirdDatabasePath);
                    if (String.IsNullOrEmpty(produto.CfopVenda))
                        produto.CfopVenda = "5102";
                    produto.Chassi = "";
                    produto.CilindradaCc = "";
                    produto.CodAnp = firebirdReader["codigoanp"].ToString();
                    produto.CodSeloIpi = "";
                    produto.Combustivel = "";
                    produto.CondicaoProduto = "";
                    produto.ControlaEstoque = true;
                    produto.CorDenatran = "";
                    produto.CorMontadora = "";
                    produto.CstCofins = "99";
                    if(produto.CfopVenda.Equals("5102"))
                        produto.CstIcms = "102";
                    if (produto.CfopVenda.Equals("5405"))
                        produto.CstIcms = "500";
                    produto.CstIpi = "99"; 
                    produto.CstPis = "99";
                    if (!String.IsNullOrEmpty(firebirdReader["datacadastro"].ToString()))
                        produto.DataCadastro = DateTime.Parse(firebirdReader["datacadastro"].ToString());
                    else if (!String.IsNullOrEmpty(firebirdReader["dataatualizacao"].ToString()))
                        produto.DataCadastro = DateTime.Parse(firebirdReader["dataatualizacao"].ToString());
                    else
                        produto.DataCadastro = DateTime.Now;
                    produto.Descricao = firebirdReader["descricao"].ToString();
                    produto.DistanciaEixo = "";
                    produto.Ean = firebirdReader["codbarras"].ToString();
                    produto.Ecommerce = false;
                    produto.EnqIpi = "999";
                    produto.EspecieVeiculo = "";
                    int idGrupo = 1;
                    object grupoIdValue = (idGrupo == 0) ? DBNull.Value : (object)idGrupo;
                    Estoque estoque = new Estoque();
                    try
                    {
                        String quantidade = VerificarEstoquePorProdutoSoftsystem(produto.Id.ToString(), firebirdDatabasePath);
                        if (double.Parse(quantidade) > 0)
                        {
                            produto.Estoque = double.Parse(quantidade);

                            estoque.BalancoEstoque = null;
                            estoque.Conciliado = true;
                            estoque.DataEntradaSaida = DateTime.Now;
                            estoque.Descricao = "IMPORTACAO SOFTSYSTEM";
                            estoque.Entrada = true;
                            estoque.FlagExcluido = false;
                            estoque.Origem = "SOFTSYSTEM";
                            estoque.Pessoa = null;
                            estoque.Quantidade = double.Parse(quantidade);
                            estoque.QuantidadeInventario = 0;
                            estoque.Saida = false;

                            //Grava estoque auxiliar ou apenas o contabil
                            if (chkSaldoEstoque.Checked == true)
                                produto.EstoqueAuxiliar = double.Parse(quantidade);
                            else
                                produto.EstoqueAuxiliar = 0;
                        }
                    }
                    catch
                    {
                        produto.Estoque = 0;
                        produto.EstoqueAuxiliar = 0;
                    }
                    produto.FlagExcluido = false;
                    if (firebirdReader["INATIVO"].ToString().Equals("T"))
                    {
                        produto.FlagExcluido = true;
                    }
                    produto.Grade = false;
                    string grupoFiscal = "2";
                    if (produto.CfopVenda.Equals("5101") || produto.CfopVenda.Equals("5102") || produto.CfopVenda.Equals("6102") || produto.CstIcms.Equals("102") || produto.CstIcms.Equals("101"))
                        grupoFiscal = "1";
                    produto.IdComplementar = "";
                    produto.KmEntrada = "";
                    produto.LotacaoVeiculo = "";
                    string idMarca = "0";
                    if (!String.IsNullOrEmpty(firebirdReader["codmarca"].ToString()))
                        idMarca = firebirdReader["codmarca"].ToString();
                    else
                        idMarca = null;
                    produto.MarcaModelo = "";
                    produto.Markup = "";
                    produto.ModeloVeiculo = "";
                    produto.Ncm = firebirdReader["nbm"].ToString();
                    produto.NumeroMotor = "";
                    produto.OperadorCadastro = "1";
                    produto.OrigemIcms = "0";
                    produto.PercentualCofins = "0";
                    produto.PercentualIcms = "0";
                    produto.PercentualIpi = "0";
                    produto.PercentualPis = "0";
                    try { produto.PercGlp = 0; } catch { produto.PercGlp = 0; }
                    try { produto.PercGni = 0; } catch { produto.PercGni = 0; }
                    try { produto.PercGnn = 0; } catch { produto.PercGnn = 0; }
                    produto.Pesavel = false;
                    produto.PesoBrutoVeiculo = "";
                    produto.PesoLiquidoVeiculo = "";
                    produto.Placa = "";
                    produto.PotenciaCv = "";
                    produto.ProdutoSetor = null;
                    produto.ProdutoSubGrupo = null;
                    produto.Referencia = firebirdReader["codreferencia"].ToString();
                    produto.Renavam = "";
                    produto.RestricaoVeiculo = "";
                    produto.SolicitaNumeroSerie = false;
                    produto.TipoCambio = "";
                    produto.TipoEntrada = "";
                    produto.TipoPintura = "";
                    produto.TipoProduto = "REVENDA";
                    produto.TipoVeiculo = "";
                    int unidadeMedida = ObterOuCriarUnidadeMedida(RemoverCedilha(firebirdReader["codembalagem"].ToString()), mysqlConnection);
                    try { produto.ValorCusto = decimal.Parse(firebirdReader["PRECOAQUISICAO"].ToString()); } catch { produto.ValorCusto = 0; }
                    produto.ValorPartida = 0;
                    try { produto.ValorVenda = decimal.Parse(firebirdReader["preco"].ToString()); } catch { produto.ValorVenda = 1; }
                    produto.Veiculo = false;
                    produto.VeiculoNovo = false;

                    string mysqlQuery = @"
                INSERT INTO Produto 
                (Id, Observacoes, AnoVeiculo, CapacidadeTracao, Cest, CfopVenda, Chassi, CilindradaCc, CodAnp, CodSeloIpi, Combustivel, 
                CondicaoProduto, ControlaEstoque, CorDenatran, CorMontadora, CstCofins, CstIcms, CstIpi, CstPis, DataCadastro, Descricao, 
                DistanciaEixo, Ean, Ecommerce, EnqIpi, EspecieVeiculo, Estoque, EstoqueAuxiliar, FlagExcluido, Grade, IdComplementar, 
                KmEntrada, LotacaoVeiculo, Marca, MarcaModelo, Markup, ModeloVeiculo, Ncm, NumeroMotor, OperadorCadastro, OrigemIcms, 
                PercentualCofins, PercentualIcms, PercentualIpi, PercentualPis, PercGlp, PercGni, PercGnn, Pesavel, PesoBrutoVeiculo, 
                PesoLiquidoVeiculo, Placa, PotenciaCv, ProdutoSetor, Referencia, Renavam, RestricaoVeiculo, 
                SolicitaNumeroSerie, TipoCambio, TipoEntrada, TipoPintura, TipoProduto, TipoVeiculo, UnidadeMedida, ValorCusto, 
                ValorPartida, ValorVenda, Veiculo, VeiculoNovo, Empresa, EmpresaFilial, GrupoFiscal, ProdutoGrupo) 
                VALUES 
                (@Id, @Observacoes, @AnoVeiculo, @CapacidadeTracao, @Cest, @CfopVenda, @Chassi, @CilindradaCc, @CodAnp, @CodSeloIpi, @Combustivel, 
                @CondicaoProduto, @ControlaEstoque, @CorDenatran, @CorMontadora, @CstCofins, @CstIcms, @CstIpi, @CstPis, @DataCadastro, @Descricao, 
                @DistanciaEixo, @Ean, @Ecommerce, @EnqIpi, @EspecieVeiculo, @Estoque, @EstoqueAuxiliar, @FlagExcluido, @Grade, @IdComplementar, 
                @KmEntrada, @LotacaoVeiculo, @MarcaId, @MarcaModelo, @Markup, @ModeloVeiculo, @Ncm, @NumeroMotor, @OperadorCadastro, @OrigemIcms, 
                @PercentualCofins, @PercentualIcms, @PercentualIpi, @PercentualPis, @PercGlp, @PercGni, @PercGnn, @Pesavel, @PesoBrutoVeiculo, 
                @PesoLiquidoVeiculo, @Placa, @PotenciaCv, @ProdutoSetor, @Referencia, @Renavam, @RestricaoVeiculo, 
                @SolicitaNumeroSerie, @TipoCambio, @TipoEntrada, @TipoPintura, @TipoProduto, @TipoVeiculo, @UnidadeMedidaId, @ValorCusto, 
                @ValorPartida, @ValorVenda, @Veiculo, @VeiculoNovo, 1,1, " + grupoFiscal + ", @GrupoProduto)";

                    MySqlCommand mysqlCommand = new MySqlCommand(mysqlQuery, mysqlConnection);
                    mysqlCommand.Parameters.AddWithValue("@Id", produto.Id);
                    mysqlCommand.Parameters.AddWithValue("@Observacoes", produto.Observacoes);
                    mysqlCommand.Parameters.AddWithValue("@AnoVeiculo", produto.AnoVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@CapacidadeTracao", produto.CapacidadeTracao);
                    mysqlCommand.Parameters.AddWithValue("@Cest", produto.Cest);
                    mysqlCommand.Parameters.AddWithValue("@CfopVenda", produto.CfopVenda);
                    mysqlCommand.Parameters.AddWithValue("@Chassi", produto.Chassi);
                    mysqlCommand.Parameters.AddWithValue("@CilindradaCc", produto.CilindradaCc);
                    mysqlCommand.Parameters.AddWithValue("@CodAnp", produto.CodAnp);
                    mysqlCommand.Parameters.AddWithValue("@CodSeloIpi", produto.CodSeloIpi);
                    mysqlCommand.Parameters.AddWithValue("@Combustivel", produto.Combustivel);
                    mysqlCommand.Parameters.AddWithValue("@CondicaoProduto", produto.CondicaoProduto);
                    mysqlCommand.Parameters.AddWithValue("@ControlaEstoque", produto.ControlaEstoque);
                    mysqlCommand.Parameters.AddWithValue("@CorDenatran", produto.CorDenatran);
                    mysqlCommand.Parameters.AddWithValue("@CorMontadora", produto.CorMontadora);
                    mysqlCommand.Parameters.AddWithValue("@CstCofins", produto.CstCofins);
                    mysqlCommand.Parameters.AddWithValue("@CstIcms", produto.CstIcms);
                    mysqlCommand.Parameters.AddWithValue("@CstIpi", produto.CstIpi);
                    mysqlCommand.Parameters.AddWithValue("@CstPis", produto.CstPis);
                    mysqlCommand.Parameters.AddWithValue("@DataCadastro", produto.DataCadastro);
                    mysqlCommand.Parameters.AddWithValue("@Descricao", produto.Descricao);
                    mysqlCommand.Parameters.AddWithValue("@DistanciaEixo", produto.DistanciaEixo);
                    mysqlCommand.Parameters.AddWithValue("@Ean", produto.Ean);
                    mysqlCommand.Parameters.AddWithValue("@Ecommerce", produto.Ecommerce);
                    mysqlCommand.Parameters.AddWithValue("@EnqIpi", produto.EnqIpi);
                    mysqlCommand.Parameters.AddWithValue("@EspecieVeiculo", produto.EspecieVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@Estoque", produto.Estoque);
                    mysqlCommand.Parameters.AddWithValue("@EstoqueAuxiliar", produto.EstoqueAuxiliar);
                    mysqlCommand.Parameters.AddWithValue("@FlagExcluido", produto.FlagExcluido);
                    mysqlCommand.Parameters.AddWithValue("@Grade", produto.Grade);
                    mysqlCommand.Parameters.AddWithValue("@IdComplementar", produto.IdComplementar);
                    mysqlCommand.Parameters.AddWithValue("@KmEntrada", produto.KmEntrada);
                    mysqlCommand.Parameters.AddWithValue("@LotacaoVeiculo", produto.LotacaoVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@MarcaId", idMarca);
                    mysqlCommand.Parameters.AddWithValue("@MarcaModelo", produto.MarcaModelo);
                    mysqlCommand.Parameters.AddWithValue("@Markup", produto.Markup);
                    mysqlCommand.Parameters.AddWithValue("@ModeloVeiculo", produto.ModeloVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@Ncm", produto.Ncm);
                    mysqlCommand.Parameters.AddWithValue("@NumeroMotor", produto.NumeroMotor);
                    mysqlCommand.Parameters.AddWithValue("@OperadorCadastro", produto.OperadorCadastro);
                    mysqlCommand.Parameters.AddWithValue("@OrigemIcms", produto.OrigemIcms);
                    mysqlCommand.Parameters.AddWithValue("@PercentualCofins", produto.PercentualCofins);
                    mysqlCommand.Parameters.AddWithValue("@PercentualIcms", produto.PercentualIcms);
                    mysqlCommand.Parameters.AddWithValue("@PercentualIpi", produto.PercentualIpi);
                    mysqlCommand.Parameters.AddWithValue("@PercentualPis", produto.PercentualPis);
                    mysqlCommand.Parameters.AddWithValue("@PercGlp", produto.PercGlp);
                    mysqlCommand.Parameters.AddWithValue("@PercGni", produto.PercGni);
                    mysqlCommand.Parameters.AddWithValue("@PercGnn", produto.PercGnn);
                    mysqlCommand.Parameters.AddWithValue("@Pesavel", produto.Pesavel);
                    mysqlCommand.Parameters.AddWithValue("@PesoBrutoVeiculo", produto.PesoBrutoVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@PesoLiquidoVeiculo", produto.PesoLiquidoVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@Placa", produto.Placa);
                    mysqlCommand.Parameters.AddWithValue("@PotenciaCv", produto.PotenciaCv);
                    mysqlCommand.Parameters.AddWithValue("@ProdutoSetor", produto.ProdutoSetor);
                    mysqlCommand.Parameters.AddWithValue("@ProdutoSubGrupo", null);
                    mysqlCommand.Parameters.AddWithValue("@Referencia", produto.Referencia);
                    mysqlCommand.Parameters.AddWithValue("@Renavam", produto.Renavam);
                    mysqlCommand.Parameters.AddWithValue("@RestricaoVeiculo", produto.RestricaoVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@SolicitaNumeroSerie", produto.SolicitaNumeroSerie);
                    mysqlCommand.Parameters.AddWithValue("@TipoCambio", produto.TipoCambio);
                    mysqlCommand.Parameters.AddWithValue("@TipoEntrada", produto.TipoEntrada);
                    mysqlCommand.Parameters.AddWithValue("@TipoPintura", produto.TipoPintura);
                    mysqlCommand.Parameters.AddWithValue("@TipoProduto", produto.TipoProduto);
                    mysqlCommand.Parameters.AddWithValue("@TipoVeiculo", produto.TipoVeiculo);
                    mysqlCommand.Parameters.AddWithValue("@UnidadeMedidaId", unidadeMedida);
                    mysqlCommand.Parameters.AddWithValue("@ValorCusto", produto.ValorCusto);
                    mysqlCommand.Parameters.AddWithValue("@ValorPartida", produto.ValorPartida);
                    mysqlCommand.Parameters.AddWithValue("@ValorVenda", produto.ValorVenda);
                    mysqlCommand.Parameters.AddWithValue("@Veiculo", produto.Veiculo);
                    mysqlCommand.Parameters.AddWithValue("@VeiculoNovo", produto.VeiculoNovo);
                    mysqlCommand.Parameters.AddWithValue("@GrupoProduto", grupoIdValue);

                    mysqlCommand.ExecuteNonQuery();
                    int idProduto = (int)mysqlCommand.LastInsertedId;
                    if (chkIdProduto.Checked == true)
                        idProduto = produto.Id;

                    string mysqlQueryEstoque = @"
                        INSERT INTO Estoque 
                            (BalancoEstoque, Conciliado, DataEntradaSaida, Descricao, Entrada, FlagExcluido, Origem, Pessoa, Quantidade, QuantidadeInventario, Saida, EmpresaFilial, Produto) 
                        VALUES 
                            (@BalancoEstoque, @Conciliado, @DataEntradaSaida, @Descricao, @Entrada, @FlagExcluido, @Origem, @Pessoa, @Quantidade, @QuantidadeInventario, @Saida, 1, " + idProduto + ")";

                    MySqlCommand mysqlCommandEstoque = new MySqlCommand(mysqlQueryEstoque, mysqlConnection);

                    mysqlCommandEstoque.Parameters.AddWithValue("@BalancoEstoque", (object)estoque.BalancoEstoque ?? DBNull.Value);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Conciliado", estoque.Conciliado);
                    mysqlCommandEstoque.Parameters.AddWithValue("@DataEntradaSaida", estoque.DataEntradaSaida);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Descricao", estoque.Descricao);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Entrada", estoque.Entrada);
                    mysqlCommandEstoque.Parameters.AddWithValue("@FlagExcluido", estoque.FlagExcluido);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Origem", estoque.Origem);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Pessoa", (object)estoque.Pessoa ?? DBNull.Value);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Quantidade", estoque.Quantidade);
                    mysqlCommandEstoque.Parameters.AddWithValue("@QuantidadeInventario", estoque.QuantidadeInventario);
                    mysqlCommandEstoque.Parameters.AddWithValue("@Saida", estoque.Saida);
                    // Execute o comando
                    mysqlCommandEstoque.ExecuteNonQuery();
                    if (chkSaldoEstoque.Checked == true)
                    {
                        //Nao conciliado
                        estoque.Conciliado = false;
                        mysqlQueryEstoque = @"
                        INSERT INTO Estoque 
                            (BalancoEstoque, Conciliado, DataEntradaSaida, Descricao, Entrada, FlagExcluido, Origem, Pessoa, Quantidade, QuantidadeInventario, Saida, EmpresaFilial, Produto) 
                        VALUES 
                            (@BalancoEstoque, @Conciliado, @DataEntradaSaida, @Descricao, @Entrada, @FlagExcluido, @Origem, @Pessoa, @Quantidade, @QuantidadeInventario, @Saida, 1, " + idProduto + ")";

                        mysqlCommandEstoque = new MySqlCommand(mysqlQueryEstoque, mysqlConnection);
                        mysqlCommandEstoque.Parameters.AddWithValue("@BalancoEstoque", (object)estoque.BalancoEstoque ?? DBNull.Value);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Conciliado", estoque.Conciliado);
                        mysqlCommandEstoque.Parameters.AddWithValue("@DataEntradaSaida", estoque.DataEntradaSaida);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Descricao", estoque.Descricao);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Entrada", estoque.Entrada);
                        mysqlCommandEstoque.Parameters.AddWithValue("@FlagExcluido", estoque.FlagExcluido);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Origem", estoque.Origem);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Pessoa", (object)estoque.Pessoa ?? DBNull.Value);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Quantidade", estoque.Quantidade);
                        mysqlCommandEstoque.Parameters.AddWithValue("@QuantidadeInventario", estoque.QuantidadeInventario);
                        mysqlCommandEstoque.Parameters.AddWithValue("@Saida", estoque.Saida);
                        // Execute o comando
                        mysqlCommandEstoque.ExecuteNonQuery();
                    }

                    UpdateUI(() =>
                    {
                        progressBar1.Value += 1;
                        lblStatus.Text = "Produto " + i + " de " + totalRegistros;
                    });
                }
                firebirdReader.Close();
                //MessageBox.Show("Importação de fornecedores concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUI(() =>
                {
                    lblStatus.Text = "Importação de produtos concluída com sucesso!";
                    progressBar1.Value = 0;
                    progressBar1.Visible = false;
                });
            }
            catch (Exception ex)
            {
                //if (ex.ToString().Contains("Duplicate entry"))
                MessageBox.Show("Erro ao importar produtos: " + ex.Message);
            }
            finally
            {
                firebirdConnection.Close();
                mysqlConnection.Close();
            }
        }

        private void ImportarGruposSoftsystem(string database, string firebirdDatabasePath)
        {
            int i = 0;
            UpdateUI(() => { lblStatus.Text = "Importação de Grupos de Produtos"; });
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;Charset=ISO8859_1;";
            FbConnection firebirdConnection = new FbConnection(firebirdConnectionString);
            FbCommand firebirdCommand = new FbCommand("SELECT * FROM aplicacao order by codaplicacao", firebirdConnection);

            // Conexão com o banco de dados MySQL
            string mysqlConnectionString = $"Server=localhost;Database={database};User Id=marcelo;Password=mx123;";
            MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);

            try
            {
                firebirdConnection.Open();
                mysqlConnection.Open();

                // Obter a quantidade total de registros
                FbCommand firebirdCountCommand = new FbCommand("SELECT COUNT(*) FROM aplicacao", firebirdConnection);
                long totalRegistros = (long)firebirdCountCommand.ExecuteScalar();

                // Configurar a ProgressBar
                UpdateUI(() =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = (int)Math.Min(totalRegistros, int.MaxValue);
                    progressBar1.Value = 0;
                });

                FbDataReader firebirdReader = firebirdCommand.ExecuteReader();
                while (firebirdReader.Read())
                {
                    i++;
                    ProdutoGrupo produtoGrupo = new ProdutoGrupo();
                    produtoGrupo.Descricao = firebirdReader["aplicacao"].ToString();
                    produtoGrupo.Id = 0;
                    produtoGrupo.FlagExcluido = false;

                    string mysqlQuery = @"
                        INSERT INTO ProdutoGrupo 
                        (Id, Descricao, FlagExcluido, Empresa) 
                        VALUES 
                        (@Id, @Descricao, @FlagExcluido, 1)";

                    MySqlCommand mysqlCommand = new MySqlCommand(mysqlQuery, mysqlConnection);
                    mysqlCommand.Parameters.AddWithValue("@Id", produtoGrupo.Id);
                    mysqlCommand.Parameters.AddWithValue("@Descricao", produtoGrupo.Descricao);
                    mysqlCommand.Parameters.AddWithValue("@FlagExcluido", produtoGrupo.FlagExcluido);

                    mysqlCommand.ExecuteNonQuery();
                    UpdateUI(() =>
                    {
                        progressBar1.Value += 1;
                        lblStatus.Text = "Grupo " + i + " de " + totalRegistros;
                    });
                }
                firebirdReader.Close();
                //MessageBox.Show("Importação de fornecedores concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUI(() =>
                {
                    progressBar1.Value = 0;
                });
            }
            catch (Exception ex)
            {
                if (!ex.ToString().Contains("Duplicate entry"))
                    MessageBox.Show("Erro ao importar dados: " + ex.Message);
            }
            finally
            {
                firebirdConnection.Close();
                mysqlConnection.Close();
            }
        }

        private void ImportarMarcasSoftsystem(string database, string firebirdDatabasePath)
        {
            int i = 0;
            UpdateUI(() => { lblStatus.Text = "Importação de Marcas de Produtos"; });
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;Charset=ISO8859_1;";
            FbConnection firebirdConnection = new FbConnection(firebirdConnectionString);
            FbCommand firebirdCommand = new FbCommand("SELECT * FROM Marcas order by codmarca", firebirdConnection);

            // Conexão com o banco de dados MySQL
            string mysqlConnectionString = $"Server=localhost;Database={database};User Id=marcelo;Password=mx123;";
            MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);

            try
            {
                firebirdConnection.Open();
                mysqlConnection.Open();

                // Obter a quantidade total de registros
                FbCommand firebirdCountCommand = new FbCommand("SELECT COUNT(*) FROM Marcas", firebirdConnection);
                long totalRegistros = (long)firebirdCountCommand.ExecuteScalar();

                // Configurar a ProgressBar
                UpdateUI(() =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = (int)Math.Min(totalRegistros, int.MaxValue);
                    progressBar1.Value = 0;
                });

                FbDataReader firebirdReader = firebirdCommand.ExecuteReader();
                while (firebirdReader.Read())
                {
                    i++;
                    Marca marca = new Marca();
                    marca.Descricao = firebirdReader["marca"].ToString();
                    marca.Id = 0;
                    marca.FlagExcluido = false;

                    string mysqlQuery = @"
                        INSERT INTO Marca 
                        (Id, Descricao, FlagExcluido, Empresa) 
                        VALUES 
                        (@Id, @Descricao, @FlagExcluido, 1)";

                    MySqlCommand mysqlCommand = new MySqlCommand(mysqlQuery, mysqlConnection);
                    mysqlCommand.Parameters.AddWithValue("@Id", marca.Id);
                    mysqlCommand.Parameters.AddWithValue("@Descricao", marca.Descricao);
                    mysqlCommand.Parameters.AddWithValue("@FlagExcluido", marca.FlagExcluido);

                    mysqlCommand.ExecuteNonQuery();
                    UpdateUI(() =>
                    {
                        progressBar1.Value += 1;
                        lblStatus.Text = "Marca " + i + " de " + totalRegistros;
                    });
                }
                firebirdReader.Close();
                //MessageBox.Show("Importação de fornecedores concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUI(() =>
                {
                    progressBar1.Value = 0;
                    lblStatus.Text = "Marcas importadas com sucesso!";
                });
            }
            catch (Exception ex)
            {
                if (!ex.ToString().Contains("Duplicate entry"))
                    MessageBox.Show("Erro ao importar dados: " + ex.Message);
            }
            finally
            {
                firebirdConnection.Close();
                mysqlConnection.Close();
            }
        }

        private string VerificarCFOPPorProdutoSoftsystem(string codClFiscal, string firebirdDatabasePath)
        {
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;Charset=ISO8859_1;";
            using (FbConnection firebirdConnection = new FbConnection(firebirdConnectionString))
            {
                try
                {
                    firebirdConnection.Open();

                    // Comando para selecionar todos os CFOPs relacionados ao CODCLFISCAL fornecido
                    string query = @"
                SELECT CODCFOP 
                FROM CLFISCAL 
                WHERE CODCLFISCAL = @codClFiscal";
                    using (FbCommand firebirdCommand = new FbCommand(query, firebirdConnection))
                    {
                        firebirdCommand.Parameters.AddWithValue("@codClFiscal", codClFiscal);

                        using (FbDataReader firebirdReader = firebirdCommand.ExecuteReader())
                        {
                            while (firebirdReader.Read())
                            {
                                string codCFOP = firebirdReader["CODCFOP"].ToString();

                                // Verifica se o CFOP é um dos desejados
                                if (codCFOP == "5102" || codCFOP == "5405")
                                {
                                    return codCFOP; // Retorna o CFOP encontrado
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao verificar CFOPs: " + ex.Message);
                }
            }

            return null; // Retorna null se nenhum CFOP encontrado
        }

        private string VerificarEstoquePorProdutoSoftsystem(string codproduto, string firebirdDatabasePath)
        {
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;Charset=ISO8859_1;";
            using (FbConnection firebirdConnection = new FbConnection(firebirdConnectionString))
            {
                try
                {
                    firebirdConnection.Open();

                    // Comando para selecionar todos os CFOPs relacionados ao CODCLFISCAL fornecido
                    string query = @"
                SELECT Estoque 
                FROM Estoque 
                WHERE CODPRODUTO = @codProduto and Loja = 1";
                    using (FbCommand firebirdCommand = new FbCommand(query, firebirdConnection))
                    {
                        firebirdCommand.Parameters.AddWithValue("@codProduto", codproduto);

                        using (FbDataReader firebirdReader = firebirdCommand.ExecuteReader())
                        {
                            while (firebirdReader.Read())
                            {
                                return firebirdReader["ESTOQUE"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao verificar ESTOQUE: " + ex.Message);
                }
            }

            return "0"; // Retorna null se nenhum CFOP encontrado
        }


        private void ImportarVendasSoftsystem(string database, string firebirdDatabasePath)
        {
            int i = 0;
            UpdateUI(() => { lblStatus.Text = "Importação de Vendas/Orcamentos"; });
            // Conexão com o banco de dados Firebird
            string firebirdConnectionString = $"User=SYSDBA;Password=masterkey;Database={firebirdDatabasePath};DataSource=localhost;Port=3050;Dialect=3;Charset=ISO8859_1;";
            FbConnection firebirdConnection = new FbConnection(firebirdConnectionString);
            FbCommand firebirdCommand = new FbCommand("SELECT * FROM Orcamentos order by codorcamento", firebirdConnection);

            // Conexão com o banco de dados MySQL
            string mysqlConnectionString = $"Server=localhost;Database={database};User Id=marcelo;Password=mx123;";
            MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString);

            try
            {
                firebirdConnection.Open();
                mysqlConnection.Open();

                // Obter a quantidade total de registros
                FbCommand firebirdCountCommand = new FbCommand("SELECT COUNT(*) FROM Orcamentos", firebirdConnection);
                long totalRegistros = (long)firebirdCountCommand.ExecuteScalar();

                // Configurar a ProgressBar
                UpdateUI(() =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = (int)Math.Min(totalRegistros, int.MaxValue);
                    progressBar1.Value = 0;
                });

                FbDataReader firebirdReader = firebirdCommand.ExecuteReader();
                while (firebirdReader.Read())
                {
                    i++;
                    Venda venda = new Venda();
                    Controller controller = new Controller();
                    PessoaDAO pessoaDAO = new PessoaDAO();
                    venda.Cancelado = false;
                    venda.Cliente = pessoaDAO.selecionarPessoaPorCodigoImportado(firebirdReader["cgc"].ToString());
                    venda.Concluida = true;
                    venda.FlagExcluido = false;
                    venda.Condicional = null;
                    venda.DataCadastro = DateTime.Parse(firebirdReader["data"].ToString());
                    venda.DataVenda = DateTime.Parse(firebirdReader["data"].ToString());
                    EmpresaFilial empresaFilial = new EmpresaFilial();
                    empresaFilial.Id = 1;
                    venda.EmpresaFilial = (EmpresaFilial)Controller.getInstance().selecionar(empresaFilial);
                    venda.EnderecoCliente = firebirdReader["endereco"].ToString();
                    venda.Nfe = null;
                    venda.NomeCliente = venda.Cliente.RazaoSocial;
                    venda.NomeComputador = "";
                    venda.Observacoes = firebirdReader["obspedido"].ToString();
                    venda.OperadorCadastro = "1";
                    venda.PessoaDependente = null;
                    PlanoConta planoConta = new PlanoConta();
                    planoConta.Id = 3;
                    planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                    venda.PlanoConta = planoConta;
                    venda.QrCodePix = "";
                    venda.Quantidade = 0;
                    venda.ValorAcrescimo = 0;
                    venda.ValorDesconto = decimal.Parse(firebirdReader["descontor"].ToString());
                    venda.ValorProdutos = decimal.Parse(firebirdReader["totalprodutos"].ToString());
                    venda.ValorFinal = decimal.Parse(firebirdReader["totalpago"].ToString());
                    venda.Vendedor = null;

                    string mysqlQuery = @"
    INSERT INTO Venda 
    (Cancelado, Cliente, Concluida, FlagExcluido, Condicional, DataCadastro, 
     DataVenda, EmpresaFilial, EnderecoCliente, Nfe, NomeCliente, NomeComputador, 
     Observacoes, OperadorCadastro, PessoaDependente, PlanoConta, QrCodePix, Quantidade, 
     ValorAcrescimo, ValorDesconto, ValorProdutos, ValorFinal, Vendedor) 
    VALUES 
    (@Cancelado, @ClienteId, @Concluida, @FlagExcluido, @Condicional, @DataCadastro, 
     @DataVenda, @EmpresaFilialId, @EnderecoCliente, @Nfe, @NomeCliente, @NomeComputador, 
     @Observacoes, @OperadorCadastro, @PessoaDependenteId, @PlanoContaId, @QrCodePix, @Quantidade, 
     @ValorAcrescimo, @ValorDesconto, @ValorProdutos, @ValorFinal, @VendedorId)";

                    MySqlCommand mysqlCommand = new MySqlCommand(mysqlQuery, mysqlConnection);
                    mysqlCommand.Parameters.AddWithValue("@Cancelado", venda.Cancelado);
                    mysqlCommand.Parameters.AddWithValue("@ClienteId", venda.Cliente.Id);  
                    mysqlCommand.Parameters.AddWithValue("@Concluida", venda.Concluida);
                    mysqlCommand.Parameters.AddWithValue("@FlagExcluido", venda.FlagExcluido);
                    mysqlCommand.Parameters.AddWithValue("@Condicional", venda.Condicional != null ? venda.Condicional : (object)DBNull.Value);
                    mysqlCommand.Parameters.AddWithValue("@DataCadastro", venda.DataCadastro);
                    mysqlCommand.Parameters.AddWithValue("@DataVenda", venda.DataVenda);
                    mysqlCommand.Parameters.AddWithValue("@EmpresaFilialId", venda.EmpresaFilial.Id); 
                    mysqlCommand.Parameters.AddWithValue("@EnderecoCliente", venda.EnderecoCliente);
                    mysqlCommand.Parameters.AddWithValue("@Nfe", venda.Nfe != null ? venda.Nfe : (object)DBNull.Value);
                    mysqlCommand.Parameters.AddWithValue("@NomeCliente", venda.NomeCliente);
                    mysqlCommand.Parameters.AddWithValue("@NomeComputador", venda.NomeComputador);
                    mysqlCommand.Parameters.AddWithValue("@Observacoes", venda.Observacoes);
                    mysqlCommand.Parameters.AddWithValue("@OperadorCadastro", venda.OperadorCadastro);
                    mysqlCommand.Parameters.AddWithValue("@PessoaDependenteId", venda.PessoaDependente != null ? venda.PessoaDependente.Id : (object)DBNull.Value);
                    mysqlCommand.Parameters.AddWithValue("@PlanoContaId", venda.PlanoConta.Id); 
                    mysqlCommand.Parameters.AddWithValue("@QrCodePix", venda.QrCodePix);
                    mysqlCommand.Parameters.AddWithValue("@Quantidade", venda.Quantidade);
                    mysqlCommand.Parameters.AddWithValue("@ValorAcrescimo", venda.ValorAcrescimo);
                    mysqlCommand.Parameters.AddWithValue("@ValorDesconto", venda.ValorDesconto);
                    mysqlCommand.Parameters.AddWithValue("@ValorProdutos", venda.ValorProdutos);
                    mysqlCommand.Parameters.AddWithValue("@ValorFinal", venda.ValorFinal);
                    mysqlCommand.Parameters.AddWithValue("@VendedorId", venda.Vendedor != null ? venda.Vendedor.Id : (object)DBNull.Value);

                    mysqlCommand.ExecuteNonQuery();

                    UpdateUI(() =>
                    {
                        progressBar1.Value += 1;
                        lblStatus.Text = "Venda " + i + " de " + totalRegistros;
                    });
                }
                firebirdReader.Close();
                //MessageBox.Show("Importação de fornecedores concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateUI(() =>
                {
                    progressBar1.Value = 0;
                    lblStatus.Text = "Vendas importadas com sucesso!";
                });
            }
            catch (Exception ex)
            {
                if (!ex.ToString().Contains("Duplicate entry"))
                    MessageBox.Show("Erro ao importar dados: " + ex.Message);
            }
            finally
            {
                firebirdConnection.Close();
                mysqlConnection.Close();
            }
        }

    }
}
