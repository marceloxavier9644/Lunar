using FirebirdSql.Data.FirebirdClient;
using LunarBase.Classes;
using LunarBase.Utilidades;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace LunarImportador
{
    public partial class FrmInicio : Form
    {
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
                if(chkProdutos.Checked == true)
                {
                    progressBar1.Visible = true;
                    lblStatus.Visible = true;
                    Thread importarThread = new Thread(() => ImportarGruposEProdutosSGBR(selectedDatabase, firebirdDatabasePath));
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
            }

            // Regex para extrair o número de telefone sem o DDD
            string numeroTelefone = Regex.Replace(telefone, @"\(\d{2}\)", "").Trim();
            numeroTelefone = numeroTelefone.Replace("-", "").Replace(" ", "");
            resultado.Telefone = numeroTelefone;

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
                    int idGrupo = 0;
                    try { idGrupo = int.Parse(firebirdReader["codgrupo"].ToString()); } catch { idGrupo = 0;}
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

                    string mysqlQueryEstoque = @"
                        INSERT INTO Estoque 
                            (BalancoEstoque, Conciliado, DataEntradaSaida, Descricao, Entrada, FlagExcluido, Origem, Pessoa, Quantidade, QuantidadeInventario, Saida, EmpresaFilial, Produto) 
                        VALUES 
                            (@BalancoEstoque, @Conciliado, @DataEntradaSaida, @Descricao, @Entrada, @FlagExcluido, @Origem, @Pessoa, @Quantidade, @QuantidadeInventario, @Saida, 1, "+ idProduto + ")";

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
                    if(chkSaldoEstoque.Checked == true)
                    {
                        //Nao conciliado
                        estoque.Conciliado = false;
                        mysqlQueryEstoque = @"
                        INSERT INTO Estoque 
                            (BalancoEstoque, Conciliado, DataEntradaSaida, Descricao, Entrada, FlagExcluido, Origem, Pessoa, Quantidade, QuantidadeInventario, Saida, EmpresaFilial, Produto) 
                        VALUES 
                            (@BalancoEstoque, @Conciliado, @DataEntradaSaida, @Descricao, @Entrada, @FlagExcluido, @Origem, @Pessoa, @Quantidade, @QuantidadeInventario, @Saida, 1, "+ idProduto + ")";

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
                if(!ex.ToString().Contains("Duplicate entry"))
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
    }
}
