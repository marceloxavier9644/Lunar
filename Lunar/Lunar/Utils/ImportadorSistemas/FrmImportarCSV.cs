using FirebirdSql.Data.FirebirdClient;
using Lunar.Telas.Estoques;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Lunar.Utils.ImportadorSistemas
{
    public partial class FrmImportarCSV : Form
    {
        EstoqueController estoqueController = new EstoqueController();
        ProdutoController produtoController = new ProdutoController();
        UnidadeMedidaController unidadeMedidaController = new UnidadeMedidaController();
        MarcaController marcaController = new MarcaController();
        PessoaController pessoaController = new PessoaController();
        DataTable dtEmployee = new DataTable();
        String caminhoBanco = "C:\\Ultra_Inst\\Banco\\Gestao.fdb";
        String conexaoFirebird2 = "";
        public FrmImportarCSV()
        {
            InitializeComponent();
            caminhoBanco = "C:\\DADOS\\Gestao.fdb";
            txtCaminhoBancoUltra.Text = caminhoBanco;
            conexaoFirebird2 = "User=SYSDBA;Password=masterkey;Database="+@txtCaminhoBancoUltra.Text+";DataSource=localhost;Port=3050;Dialect=3;Charset=UTF8;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;";
        }

    private void lerCSV(string path)
        {
            Int32 i = 0;
            string[] Linha = System.IO.File.ReadAllLines(@path);
            try { 
            DataTable dt = new DataTable();
            

            for (i = 0; i < Linha.Length; i++)
            {
                string[] campos = Linha[i].Split(Convert.ToChar(";"));
                if (i == 0)
                {
                    for (Int32 i2 = 0; i2 < campos.Length; i2++)
                    {
                        //Criando uma coluna
                        DataColumn col = new DataColumn();
                        //Adicionando a coluna criada ao datatable
                        dt.Columns.Add(col);
                    }
                }
                dt.Rows.Add(campos);
            }

            //Depois de montado o datatable, vamos para o grid
            //que a fonte de dados para ele exibir, será o datatable que 
            //a gente acabou de criar
            //if(radioClientes.Checked == true)
            dataGridView1.DataSource = dt;
                //else
                //    gridContasReceber.DataSource = dt;
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowAlerta("Erro na Linha: " + Linha[i] + erro.Message);
            }
        }

        private void btnLocalizarArquivo_Click(object sender, EventArgs e)
        {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV Files(*.csv)|*.csv|All Files(*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.ShowDialog();
                if (openFileDialog.CheckFileExists)
                {
                    dataGridView1.Visible = true;
                    txtCaminhoArquivo.Text = @openFileDialog.FileName;
                    lerCSV(txtCaminhoArquivo.Text);
                }
        }

        private void btnConfirmarImportacao_Click(object sender, EventArgs e)
        {
            lblInformacao.Visible = true;
            
            //CSV
            if (radioClientes.Checked == true && !String.IsNullOrEmpty(txtCaminhoArquivo.Text))
            {
                Thread th = new Thread(() => importarClientesFornecedores());
                th.Start();
                Application.DoEvents();
                th.Join();
            }
            //CSV
            if (radioProdutos.Checked == true && !String.IsNullOrEmpty(txtCaminhoArquivo.Text))
            {
                Thread th = new Thread(() => importarProdutos());
                th.Start();
                Application.DoEvents();
                th.Join();
            }
            //CSV
            if (radioContasReceber.Checked == true && !String.IsNullOrEmpty(txtCaminhoArquivo.Text))
            {
                Thread th = new Thread(() => importarContaReceber());
                th.Start();
                Application.DoEvents();
                th.Join();
            }
            //BANCO ULTRA
            //if (radioOrdemServico.Checked == true)
            //{
            //    Thread th = new Thread(() => importarOS(dtEmployee));
            //    th.Start();
            //    Application.DoEvents();
            //    th.Join();
            //}
        }

        private void importarClientesFornecedores()
        {
            int i = 0;
            lblInformacao.Text = "Importação iniciada...";
            foreach (DataGridViewRow col in dataGridView1.Rows)
            {
                i++;
                Pessoa pessoa = new Pessoa();
                lblInformacao.Text = "Importação de Pessoas/Parceiros: " + i + " de " + dataGridView1.Rows.Count;
                pessoa.Id = 0;
                pessoa.CodigoImportacao = col.Cells[0].Value.ToString();
                if (!String.IsNullOrEmpty(pessoa.CodigoImportacao)) 
                { 
                pessoa.RazaoSocial = col.Cells[1].Value.ToString();
                if (String.IsNullOrEmpty(pessoa.RazaoSocial))
                    pessoa.RazaoSocial = "SEM PREENCHIMENTO";
                pessoa.NomeFantasia = col.Cells[2].Value.ToString();
                pessoa.Cnpj = col.Cells[3].Value.ToString().Replace("-", "").Replace(".", "").Replace("/", "");
                if(String.IsNullOrEmpty(pessoa.Cnpj))
                    pessoa.Cnpj = col.Cells[4].Value.ToString().Replace("-", "").Replace(".", "").Replace("/", "");
                pessoa.Rg = col.Cells[5].Value.ToString();
                pessoa.InscricaoEstadual = col.Cells[6].Value.ToString();
                try { pessoa.DataNascimento = DateTime.Parse(col.Cells[7].Value.ToString()); } catch { pessoa.DataNascimento = DateTime.Parse("01-01-1900 00:00:00");}
                pessoa.Sexo = col.Cells[8].Value.ToString();
                pessoa.TipoParceiroImportado = col.Cells[17].Value.ToString();

                pessoa.Cliente = true;
                string retornoFornecedor = col.Cells[18].Value.ToString();
                string retornoVendedor = col.Cells[19].Value.ToString();
                if (retornoFornecedor.Equals("S"))
                {
                    pessoa.Cliente = false;
                    pessoa.Fornecedor = true;
                }
                if (retornoVendedor.Equals("S"))
                {
                    pessoa.Cliente = true;
                    pessoa.Fornecedor = false;
                    pessoa.Vendedor = true;
                }
                pessoa.ReceberLembrete = false;
                pessoa.TipoPessoa = "PF";
                if (pessoa.Cnpj.Length == 14)
                    pessoa.TipoPessoa = "PJ";
                pessoa.EnderecoPrincipal = null;
                pessoa.PessoaTelefone = null;
                pessoa.Pai = col.Cells[20].Value.ToString();
                pessoa.Mae = col.Cells[21].Value.ToString();
                pessoa.Email = col.Cells[22].Value.ToString();
                pessoa.Observacoes = "PESSOA IMPORTADA DE OUTRO SISTEMA";
                pessoa.LocalTrabalho = "";
                pessoa.LimiteCredito = 0;
                pessoa.FuncaoTrabalho = "";
                pessoa.TelefoneTrabalho = "";
                pessoa.TempoTrabalho = "";
                pessoa.SalarioTrabalho = "0";
                pessoa.ContatoTrabalho = "";
                pessoa.RegistradoSpc = false;
                pessoa.EscritorioCobranca = false;

                //funcao para importar da otica vemos unai
                if(pessoa.TipoParceiroImportado.Equals("3"))
                    pessoa.EscritorioCobranca = true;

                if (pessoa.TipoParceiroImportado.Equals("2"))
                    pessoa.RegistradoSpc = true;

                    Controller.getInstance().salvar(pessoa);

                    //apos salvar a pessoa, salva o endereço.
                    if (pessoa.Id > 0)
                    {
                 
                        Endereco endereco = new Endereco();
                        endereco.Cep = col.Cells[9].Value.ToString().Replace("-", "").Replace(".", "").Replace("/", "");
                        string descCidade = col.Cells[10].Value.ToString().Replace("-", "").Replace(".", "").Replace("/", "");
                        CidadeController cidadeController = new CidadeController();
                        Cidade cidade = new Cidade();
                        try { cidade = cidadeController.selecionarCidadePorDescricao(descCidade); }
                        catch { cidade = cidadeController.selecionarCidadePorDescricaoEUf(descCidade, "MG"); }
                        if (cidade != null)
                            endereco.Cidade = cidade;
                        else
                        {
                            cidade = cidadeController.selecionarCidadePorDescricao("UNAI");
                            endereco.Cidade = cidade;
                        }
                        endereco.Logradouro = col.Cells[11].Value.ToString();
                        if (String.IsNullOrEmpty(endereco.Logradouro))
                            endereco.Logradouro = "SEM PREENCHIMENTO";
                        endereco.Numero = col.Cells[12].Value.ToString();
                        if (String.IsNullOrEmpty(endereco.Numero))
                            endereco.Numero = "S/N";
                        endereco.Bairro = col.Cells[13].Value.ToString();
                        endereco.Complemento = col.Cells[14].Value.ToString();
                        endereco.EmpresaFilial = Sessao.empresaFilialLogada;
                        endereco.Referencia = "";
                        endereco.Pessoa = pessoa;

                        Controller.getInstance().salvar(endereco);
                        pessoa.EnderecoPrincipal = endereco;
                        Controller.getInstance().salvar(pessoa);
               

                        //apos salvar endereço salva os telefones
                        if (!String.IsNullOrEmpty(col.Cells[15].Value.ToString()))
                        {
                            PessoaTelefone pessoaTelefone = new PessoaTelefone();
                            pessoaTelefone.Pessoa = pessoa;
                            pessoaTelefone.Ddd = "";
                            pessoaTelefone.Telefone = col.Cells[15].Value.ToString();
                            pessoaTelefone.Observacoes = "IMPORTAÇÃO DE OUTRO SISTEMA";
                            Controller.getInstance().salvar(pessoaTelefone);
                        }
                        if (!String.IsNullOrEmpty(col.Cells[16].Value.ToString()))
                        {
                            PessoaTelefone pessoaTelefone = new PessoaTelefone();
                            pessoaTelefone.Pessoa = pessoa;
                            pessoaTelefone.Ddd = "";
                            pessoaTelefone.Telefone = col.Cells[16].Value.ToString();
                            pessoaTelefone.Observacoes = "IMPORTAÇÃO DE OUTRO SISTEMA";
                            Controller.getInstance().salvar(pessoaTelefone);
                            pessoa.PessoaTelefone = pessoaTelefone;
                            Controller.getInstance().salvar(pessoa);
                        }
                    }
                }
            }
            GenericaDesktop.ShowInfo("Importação de Clientes/Fornecedores Realizada com Sucesso!");
        }

        private void importarProdutos()
        {
            try
            {
                int i = 0;
                  lblInformacao.Text = "Importação iniciada...";
                MessageBox.Show("Para evitar erros remova o auto increment do ID na tabela produto");
                foreach (DataGridViewRow col in dataGridView1.Rows)
                {
                    i++;
                    Produto produto = new Produto();
                       lblInformacao.Text = "Importação de Produtos: " + i + " de " + dataGridView1.Rows.Count;
                    produto.Id = int.Parse(col.Cells[0].Value.ToString());
                    if (!String.IsNullOrEmpty(produto.Id.ToString()))
                    {
                        produto.Descricao = col.Cells[1].Value.ToString();
                        if (String.IsNullOrEmpty(produto.Descricao))
                            produto.Descricao = "SEM PREENCHIMENTO";

                        ProdutoGrupo grupo = new ProdutoGrupo();
                        grupo.Id = 1;
                        grupo = (ProdutoGrupo)ProdutoGrupoController.getInstance().selecionar(grupo);
                        produto.ProdutoGrupo = grupo;

                        UnidadeMedida unidadeMedida = new UnidadeMedida();
                        if (!String.IsNullOrEmpty(col.Cells[3].Value.ToString()))
                        {
                            unidadeMedida = unidadeMedidaController.selecionarUnidadeMedidaPorSigla(col.Cells[3].Value.ToString());
                            if (unidadeMedida != null)
                            {
                                if (unidadeMedida.Id > 0)
                                    produto.UnidadeMedida = unidadeMedida;
                            }
                            else
                            {
                                unidadeMedida = unidadeMedidaController.selecionarUnidadeMedidaPorSigla("UN");
                                if (unidadeMedida.Id > 0)
                                    produto.UnidadeMedida = unidadeMedida;
                            }
                        }
                        else
                        {
                            unidadeMedida = unidadeMedidaController.selecionarUnidadeMedidaPorSigla("UN");
                            if (unidadeMedida.Id > 0)
                                produto.UnidadeMedida = unidadeMedida;
                        }

                        Marca marca = new Marca();
                        marca = marcaController.selecionarMarcaPorDescricao(col.Cells[4].Value.ToString());
                        if (marca != null)
                        {
                            if (marca.Id > 0)
                            {
                                produto.Marca = marca;
                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(col.Cells[4].Value.ToString()))
                                {
                                    marca = new Marca();
                                    marca.Id = 0;
                                    marca.Descricao = col.Cells[4].Value.ToString();
                                    marca.Empresa = Sessao.empresaFilialLogada.Empresa;
                                    Controller.getInstance().salvar(marca);
                                    produto.Marca = marca;
                                }
                                else
                                    produto.Marca = null;
                            }
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(col.Cells[4].Value.ToString()))
                            {
                                marca = new Marca();
                                marca.Id = 0;
                                marca.Descricao = col.Cells[4].Value.ToString();
                                marca.Empresa = Sessao.empresaFilialLogada.Empresa;
                                Controller.getInstance().salvar(marca);
                                produto.Marca = marca;
                            }
                            else
                                produto.Marca = null;
                        }

                        //Grupo fiscal ICMS
                        if (col.Cells[5].Value.ToString().Equals("TRIBUTADO"))
                        {
                            produto.CstIcms = "102";
                            produto.OrigemIcms = "0";
                            produto.PercentualIcms = "18";
                            produto.CfopVenda = "5102";
                            GrupoFiscal grupoFiscal = new GrupoFiscal();
                            grupoFiscal.Id = 1;
                            grupoFiscal = (GrupoFiscal)GrupoFiscalController.getInstance().selecionar(grupoFiscal);
                            produto.GrupoFiscal = grupoFiscal;
                        }
                        //Grupo fiscal ICMS
                        if (col.Cells[5].Value.ToString().Equals("SUBSTITUICAO TRIBUTARIA") || col.Cells[4].Value.ToString().Equals("ST"))
                        {
                            produto.CstIcms = "500";
                            produto.OrigemIcms = "0";
                            produto.PercentualIcms = "0";
                            produto.CfopVenda = "5405";
                            GrupoFiscal grupoFiscal = new GrupoFiscal();
                            grupoFiscal.Id = 2;
                            grupoFiscal = (GrupoFiscal)GrupoFiscalController.getInstance().selecionar(grupoFiscal);
                            produto.GrupoFiscal = grupoFiscal;
                        }

                        produto.Cest = col.Cells[13].Value.ToString();
                        produto.Ncm = col.Cells[6].Value.ToString().Trim().Replace(" ", "").Replace("-", "").Replace(".", "");
                        produto.Ean = col.Cells[7].Value.ToString().Trim().Replace(" ", "").Replace("-", "").Replace(".", "");
                        produto.Referencia = col.Cells[8].Value.ToString();
                        if (!String.IsNullOrEmpty(col.Cells[9].Value.ToString()))
                        {
                            produto.ValorCusto = decimal.Parse(col.Cells[9].Value.ToString());
                        }
                        else
                            produto.ValorCusto = 1;
                        if (!String.IsNullOrEmpty(col.Cells[10].Value.ToString()))
                        {
                            produto.ValorVenda = decimal.Parse(col.Cells[10].Value.ToString());
                        }
                        else
                            produto.ValorVenda = 1;

                        if (double.Parse(col.Cells[11].Value.ToString()) > 0)
                            produto.EstoqueAuxiliar = double.Parse(col.Cells[11].Value.ToString());
                        else
                            produto.EstoqueAuxiliar = 0;
                        if (double.Parse(col.Cells[12].Value.ToString()) > 0)
                            produto.Estoque = double.Parse(col.Cells[12].Value.ToString());
                        else
                            produto.Estoque = 0;
                        produto.Grade = false;
                        produto.IdComplementar = "";
                        produto.Observacoes = "IMPORTADO DE OUTRO SISTEMA";
                        produto.PercentualCofins = "";
                        produto.PercentualIpi = "";
                        produto.PercentualPis = "";
                        produto.PercGlp = 0;
                        produto.PercGni = 0;
                        produto.PercGnn = 0;
                        produto.Pesavel = false;
                        produto.ProdutoSetor = null;
                        produto.ProdutoSubGrupo = null;
                        produto.SolicitaNumeroSerie = false;
                        produto.TipoProduto = "REVENDA";
                        produto.ValorPartida = 0;
                        produto.CodAnp = "";
                        produto.CodSeloIpi = "";
                        produto.ControlaEstoque = true;
                        produto.CstCofins = "99";
                        produto.CstIpi = "99";
                        produto.CstPis = "99";
                        produto.EnqIpi = "999";
                        produto.Empresa = Sessao.empresaFilialLogada.Empresa;
                        produto.EmpresaFilial = Sessao.empresaFilialLogada;

                        Controller.getInstance().atualizar(produto);
                        //ESTOQUE
                        Estoque estoque = new Estoque();

                        if (produto.EstoqueAuxiliar > 0)
                        {
                            estoque.BalancoEstoque = null;
                            estoque.Conciliado = false;
                            estoque.DataEntradaSaida = DateTime.Now;
                            estoque.Descricao = "IMPORTAÇÃO DE SISTEMA";
                            estoque.EmpresaFilial = Sessao.empresaFilialLogada;
                            estoque.Entrada = true;
                            estoque.Id = 0;
                            estoque.Origem = "IMPORTACAO";
                            estoque.Pessoa = null;
                            estoque.Produto = produto;
                            estoque.Quantidade = produto.EstoqueAuxiliar;
                            estoque.QuantidadeInventario = 0;
                            estoque.Saida = false;
                            Controller.getInstance().salvar(estoque);
                        }
                        if (produto.Estoque > 0)
                        {
                            estoque.BalancoEstoque = null;
                            estoque.Conciliado = true;
                            estoque.DataEntradaSaida = DateTime.Now;
                            estoque.Descricao = "IMPORTAÇÃO DE SISTEMA";
                            estoque.EmpresaFilial = Sessao.empresaFilialLogada;
                            estoque.Entrada = true;
                            estoque.Id = 0;
                            estoque.Origem = "IMPORTACAO";
                            estoque.Pessoa = null;
                            estoque.Produto = produto;
                            estoque.Quantidade = produto.Estoque;
                            estoque.QuantidadeInventario = 0;
                            estoque.Saida = false;
                            Controller.getInstance().salvar(estoque);
                        }
                    }
                }
                GenericaDesktop.ShowInfo("Importação de Produtos Realizada com Sucesso!");
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Erro ao Importar Produtos: " + erro.Message);
            }
        }

        private void importarContaReceber()
        {
            try
            {
                int i = 0;
                lblInformacao.Text = "Importação iniciada...";
                foreach (DataGridViewRow col in dataGridView1.Rows)
                {
                    i++;
                    ContaReceber receber = new ContaReceber();
                    lblInformacao.Text = "Importação de Contas a Receber: " + i + " de " + dataGridView1.Rows.Count;
                    receber.Id = 0;
                    receber.AcrescimoRecebidoBaixa = 0;
                    receber.CaixaRecebimento = "";
                    receber.Concluido = true;
                    
                    Pessoa pessoa = new Pessoa();
                    pessoa = pessoaController.selecionarPessoaPorCodigoImportado(col.Cells[0].Value.ToString());
                    if (pessoa != null)
                    {
                        receber.Cliente = pessoa;

                        receber.CnpjCliente = pessoa.Cnpj;
                        receber.DataExclusao = DateTime.Parse("0001-01-01 00:00:00");
                        receber.DataRecebimento = DateTime.Parse("0001-01-01 00:00:00");
                        receber.DescontoRecebidoBaixa = 0;
                        receber.Descricao = "IMPORTADO ULTRA";
                        receber.DescricaoRecebimento = "";
                        receber.Documento = col.Cells[3].Value.ToString();
                        receber.EmpresaFilial = Sessao.empresaFilialLogada;

                        string endereco = "";
                        if (pessoa.EnderecoPrincipal != null)
                        {
                            endereco = pessoa.EnderecoPrincipal.Logradouro + ", " + pessoa.EnderecoPrincipal.Numero;
                        }
                        receber.EnderecoCliente = endereco;
                        FormaPagamento forma = new FormaPagamento();
                        forma.Id = 6;
                        forma = (FormaPagamento)FormaPagamentoController.getInstance().selecionar(forma);
                        receber.FormaPagamento = forma;

                        receber.Juro = 0;
                        receber.Multa = 0;
                        receber.NomeCliente = pessoa.RazaoSocial;
                        receber.OrdemServico = null;
                        receber.Origem = "IMPORTADO";
                        receber.Parcela = col.Cells[4].Value.ToString();
                        receber.PlanoConta = null;
                        receber.Recebido = false;

                        receber.Data = DateTime.Parse(col.Cells[8].Value.ToString());
                        if (!String.IsNullOrEmpty(col.Cells[9].Value.ToString()))
                        {
                            receber.ValorParcela = decimal.Parse(col.Cells[9].Value.ToString());
                            receber.ValorTotal = receber.ValorParcela;
                            receber.ValorTotalOrigem = receber.ValorParcela;
                        }
                        receber.ValorRecebido = 0;
                        receber.ValorRecebimentoParcial = 0;
                        receber.Vencimento = DateTime.Parse(col.Cells[5].Value.ToString());
                        receber.Venda = null;
                        receber.VendaFormaPagamento = null;
                        Controller.getInstance().salvar(receber);
                    }
                }
                GenericaDesktop.ShowInfo("Contas a Receber Importado com Sucesso!");
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Erro ao Importar Contas a Receber: " + erro.Message);
            }
        }

        private void puxarContaReceberUltra()
        {
            try
            {
                int i = 0;
                lblInformacao.Text = "Importação iniciada...";
                foreach (DataGridViewRow col in dataGridView1.Rows)
                {
                    i++;
                    ContaReceber receber = new ContaReceber();
                    lblInformacao.Text = "Importação de Contas a Receber: " + i + " de " + dataGridView1.Rows.Count;
                    receber.Id = 0;
                    receber.AcrescimoRecebidoBaixa = 0;
                    receber.CaixaRecebimento = "";
                    receber.Concluido = true;

                    Pessoa pessoa = new Pessoa();
                    pessoa = pessoaController.selecionarPessoaPorCodigoImportado(col.Cells[0].Value.ToString());
                    receber.Cliente = pessoa;

                    receber.CnpjCliente = pessoa.Cnpj;
                    receber.DataExclusao = DateTime.Parse("0001-01-01 00:00:00");
                    receber.DataRecebimento = DateTime.Parse("0001-01-01 00:00:00");
                    receber.DescontoRecebidoBaixa = 0;
                    receber.Descricao = "IMPORTADO ULTRA";
                    receber.DescricaoRecebimento = "";
                    receber.Documento = col.Cells[3].Value.ToString();
                    receber.EmpresaFilial = Sessao.empresaFilialLogada;

                    string endereco = "";
                    if (pessoa.EnderecoPrincipal != null)
                    {
                        endereco = pessoa.EnderecoPrincipal.Logradouro + ", " + pessoa.EnderecoPrincipal.Numero;
                    }
                    receber.EnderecoCliente = endereco;
                    FormaPagamento forma = new FormaPagamento();
                    forma.Id = 6;
                    forma = (FormaPagamento)FormaPagamentoController.getInstance().selecionar(forma);
                    receber.FormaPagamento = forma;

                    receber.Juro = 0;
                    receber.Multa = 0;
                    receber.NomeCliente = pessoa.RazaoSocial;
                    receber.OrdemServico = null;
                    receber.Origem = "IMPORTADO";
                    receber.Parcela = col.Cells[4].Value.ToString();
                    receber.PlanoConta = null;
                    receber.Recebido = false;

                    receber.Data = DateTime.Parse(col.Cells[8].Value.ToString());
                    if (!String.IsNullOrEmpty(col.Cells[9].Value.ToString()))
                    {
                        receber.ValorParcela = decimal.Parse(col.Cells[9].Value.ToString());
                        receber.ValorTotal = receber.ValorParcela;
                        receber.ValorTotalOrigem = receber.ValorParcela;
                    }
                    receber.ValorRecebido = 0;
                    receber.ValorRecebimentoParcial = 0;
                    receber.Vencimento = DateTime.Parse(col.Cells[5].Value.ToString());
                    receber.Venda = null;
                    receber.VendaFormaPagamento = null;
                    Controller.getInstance().salvar(receber);
                }
                GenericaDesktop.ShowInfo("Contas a Receber Importado com Sucesso!");
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Erro ao Importar Contas a Receber: " + erro.Message);
            }
        }

        private void puxarDadosFirebird_EImportarOS()
        {
            dataGridView1.Visible = true;
            FbConnection fbConn = new FbConnection(conexaoFirebird2);
            FbCommand fbCmd = new FbCommand("Select OS.OS,OS.Parceiro,OS.Data_Abertura, OS.Data_Encerramento, OS.Valor_Liq_Total, OS.Data_Entrega, OS.Vendedor, OS.Idn_Cancelada from OS order by OS.OS", fbConn);

            try
            {
                fbConn.Open();
                FbDataAdapter fbDa = new FbDataAdapter(fbCmd);
                dtEmployee = new DataTable();
                fbDa.Fill(dtEmployee);
                dataGridView1.DataSource = dtEmployee;

                //BANCO ULTRA
                if (radioOrdemServico.Checked == true)
                {
                    Thread th = new Thread(() => importarOS(dtEmployee));
                    th.Start();
                    Application.DoEvents();
                    th.Join();
                }
            }
            catch (FbException fbex)
            {
                MessageBox.Show("Erro ao acessar o FireBird " + fbex.Message, "Erro");
            }
            finally
            {
                fbConn.Close();
            }
        }

        private void btnFirebird_Click(object sender, EventArgs e)
        {
            if(radioOrdemServico.Checked == true)
                puxarDadosFirebird_EImportarOS();
            if (radioOrdemServicoProdutos.Checked == true)
            {
                lblInformacao.Visible = true;
                Thread th = new Thread(() => puxarProdutosOS());
                th.Start();
                Application.DoEvents();
                th.Join();
            }
            if (radioOrdemServicoExame.Checked == true)
            {
                lblInformacao.Visible = true;
                Thread th = new Thread(() => puxarExamesOS());
                th.Start();
                Application.DoEvents();
                th.Join();
            }
            if (radioContasPagar.Checked == true)
            {
                lblInformacao.Visible = true;
                Thread th = new Thread(() => puxarContasPagarUltra());
                th.Start();
                Application.DoEvents();
                th.Join();
            }
            if (radioAnexosOS.Checked == true)
            {
                lblInformacao.Visible = true;
                dataGridView1.Visible = true;
                Thread th = new Thread(() => puxarAnexosOsUltra());
                th.Start();
                Application.DoEvents();
                th.Join();
            }
            //Se nao tem csv selecionado, importa pelo bd
            if (radioClientes.Checked == true && string.IsNullOrEmpty(txtCaminhoArquivo.Text))
            {
                lblInformacao.Visible = true;
                dataGridView1.Visible = true;
                Thread th = new Thread(() => puxarParceirosUltra());
                th.Start();
                Application.DoEvents();
                th.Join();
            }
            if (radioProdutos.Checked == true && string.IsNullOrEmpty(txtCaminhoArquivo.Text))
            {
                lblInformacao.Visible = true;
                dataGridView1.Visible = true;
                Thread th = new Thread(() => puxarProdutosUltra());
                th.Start();
                Application.DoEvents();
                th.Join();
            }
            ////Se nao tem csv selecionado, importa pelo bd (A DESENVOLVER)
            //if (radioContasReceber.Checked == true && string.IsNullOrEmpty(txtCaminhoArquivo.Text))
            //{
            //    lblInformacao.Visible = true;
            //    dataGridView1.Visible = true;
            //    Thread th = new Thread(() => puxarContarReceberUltra());
            //    th.Start();
            //    Application.DoEvents();
            //    th.Join();
            //}
        }
        //selecionar exame: select * from os_otica oo where oo.os = 9150
        //selecionar produtos: select * from os_produtos_alocados osprod where osprod.os = 9150

        private void importarOS(DataTable dtEmployee)
        {
            //Converter em LIST
            try
            {
                int a = 0;
                lblInformacao.Text = "Importação iniciada...";
                for (int i = 0; i < dtEmployee.Rows.Count; i++)
                {
                    a++;
                    lblInformacao.Text = "Ordem de Serviço: " + a + " de " + dtEmployee.Rows.Count;
                    OrdemServico ordemServico = new OrdemServico();
                    ordemServico.Id = Convert.ToInt32(dtEmployee.Rows[i]["OS"]);
                    Pessoa pessoa = new Pessoa();
                    pessoa = pessoaController.selecionarPessoaPorCodigoImportado(dtEmployee.Rows[i]["PARCEIRO"].ToString());
                    ordemServico.Cliente = pessoa;//Idn_Cancelada
                    if (ordemServico.Cliente != null && dtEmployee.Rows[i]["IDN_CANCELADA"].ToString() != "S")
                    {
                        ordemServico.DataAbertura = DateTime.Parse(dtEmployee.Rows[i]["DATA_ABERTURA"].ToString());
                        ordemServico.Entrada = false;
                        if (!String.IsNullOrEmpty(dtEmployee.Rows[i]["DATA_ENCERRAMENTO"].ToString()))
                        {
                            ordemServico.DataEncerramento = DateTime.Parse(dtEmployee.Rows[i]["DATA_ENCERRAMENTO"].ToString());
                            ordemServico.Status = "ENCERRADA";
                        }
                        else
                        {
                            ordemServico.DataEncerramento = DateTime.Parse("1900-01-01 00:00:00");
                            ordemServico.Status = "ABERTA";
                        }
                        ordemServico.Filial = Sessao.empresaFilialLogada;
                        ordemServico.Nfe = null;
                        ordemServico.NumeroSerie = dtEmployee.Rows[i]["OS"].ToString();
                        ordemServico.Observacoes = "IMPORTADO ULTRA";
                        TipoObjeto tipoObjeto = new TipoObjeto();
                        tipoObjeto.Id = 1;
                        tipoObjeto = (TipoObjeto)Controller.getInstance().selecionar(tipoObjeto);
                        ordemServico.TipoObjeto = tipoObjeto;

                        ordemServico.ValorAcrescimo = 0;
                        ordemServico.ValorDesconto = 0;
                        ordemServico.ValorProduto = Decimal.Parse(dtEmployee.Rows[i]["VALOR_LIQ_TOTAL"].ToString());
                        ordemServico.ValorServico = 0;
                        ordemServico.ValorTotal = Decimal.Parse(dtEmployee.Rows[i]["VALOR_LIQ_TOTAL"].ToString());
                        if (!String.IsNullOrEmpty(dtEmployee.Rows[i]["VENDEDOR"].ToString()))
                        {
                            pessoa = new Pessoa();
                            pessoa = pessoaController.selecionarPessoaPorCodigoImportado(dtEmployee.Rows[i]["VENDEDOR"].ToString());
                            ordemServico.Vendedor = pessoa;
                        }
                        else
                            ordemServico.Vendedor = null;

                        Controller.getInstance().salvar(ordemServico);
                    }                
                }
                GenericaDesktop.ShowInfo("Importado com Sucesso!");
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Erro na importação de O.S: " + erro.Message);
            }
        }

        private void puxarProdutosOS()
        {
            OrdemServicoController ordemServicoController = new OrdemServicoController();
            IList<OrdemServico> listaOrdem = ordemServicoController.selecionarTodasOS();
            if(listaOrdem.Count > 0)
            {
                int a = 0;
                foreach (OrdemServico ordemServico in listaOrdem)
                {
                    a++;
                    //dataGridView1.Visible = true;
                    FbConnection fbConn = new FbConnection(conexaoFirebird2);
                    FbCommand fbCmd = new FbCommand("Select * from os_produtos_alocados where os_produtos_alocados.OS = " + ordemServico.NumeroSerie, fbConn);

                    try
                    {
                        fbConn.Open();
                        FbDataAdapter fbDa = new FbDataAdapter(fbCmd);
                        dtEmployee = new DataTable();
                        fbDa.Fill(dtEmployee);

                        //Importar
                        if (dtEmployee.Rows.Count > 0)
                        {
                     
                            for (int i = 0; i < dtEmployee.Rows.Count; i++)
                            {
                       
                                lblInformacao.Text = "Produtos da Ordem de Serviço: " + a + " de " + listaOrdem.Count;
                                OrdemServicoProduto ordemServicoProduto = new OrdemServicoProduto();
                                ordemServicoProduto.Id = 0;
                                ordemServicoProduto.Acrescimo = 0;
                                //Convert.ToInt32(dtEmployee.Rows[i]["OS"]);
                                ordemServicoProduto.Desconto = decimal.Parse(dtEmployee.Rows[i]["DESCONTO"].ToString());
                                ordemServicoProduto.DescricaoProduto = dtEmployee.Rows[i]["DESCRICAO_PRODUTO"].ToString();
                                if (String.IsNullOrEmpty(ordemServicoProduto.DescricaoProduto))
                                    ordemServicoProduto.DescricaoProduto = "NÃO PREENCHIDO";
                                ordemServicoProduto.OrdemServico = ordemServico;
                               
                                int cod = 0;
                                //tratando para nao vir null em produtos possivelmente exluidos!
                                try { cod = Convert.ToInt32(dtEmployee.Rows[i]["PRODUTO"]); } catch { cod = 0; }
                                string b = "";
                                if (cod > 0)
                                {
                                    ordemServicoProduto.Produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(Convert.ToInt32(dtEmployee.Rows[i]["PRODUTO"]), Sessao.empresaFilialLogada);
                                    if (ordemServicoProduto.Produto != null)
                                    {
                                        if (ordemServicoProduto.Produto.GradePrincipal != null)
                                        {
                                            ordemServicoProduto.ProdutoGrade = ordemServicoProduto.Produto.GradePrincipal;
                                            ordemServicoProduto.Quantidade = double.Parse(dtEmployee.Rows[i]["QTD"].ToString());
                                            ordemServicoProduto.ValorTotal = decimal.Parse(dtEmployee.Rows[i]["LIQUIDO"].ToString());
                                            ordemServicoProduto.ValorUnitario = decimal.Parse(dtEmployee.Rows[i]["UNITARIO"].ToString());
                                            Controller.getInstance().salvar(ordemServicoProduto);
                                        }
                                        else
                                        {
                                            Logger logger = new Logger();
                                            logger.WriteLog("Produto sem grade: " + ordemServicoProduto.Produto.Id, "LOGIMPORTACAO");
                                        }
                                    }
                                    else
                                    {
                                        //SO PRA DEBUGAR E VER ERRO
                                        b = "";
                                    }
                                }
                            }
                        }
                    }
                    catch (FbException fbex)
                    {
                        MessageBox.Show("Erro ao acessar o FireBird " + fbex.Message, "Erro");
                    }
                    finally
                    {
                        fbConn.Close();
                    }
                }
                GenericaDesktop.ShowInfo("Importação Realizada com Sucesso!");
            }
        }

        private void puxarExamesOS()
        {
            OrdemServicoController ordemServicoController = new OrdemServicoController();
            IList<OrdemServico> listaOrdem = ordemServicoController.selecionarTodasOS();
            if (listaOrdem.Count > 0)
            {
                foreach (OrdemServico ordemServico in listaOrdem)
                {
                    dataGridView1.Visible = true;
                    FbConnection fbConn = new FbConnection(conexaoFirebird2);
                    FbCommand fbCmd = new FbCommand("Select * from os_otica where os_otica.OS = " + ordemServico.NumeroSerie, fbConn);

                    try
                    {
                        fbConn.Open();
                        FbDataAdapter fbDa = new FbDataAdapter(fbCmd);
                        dtEmployee = new DataTable();
                        fbDa.Fill(dtEmployee);
                        //dataGridView1.DataSource = dtEmployee;

                        //Importar
                        if (dtEmployee.Rows.Count > 0)
                        {
                            int a = 0;
                            for (int i = 0; i < dtEmployee.Rows.Count; i++)
                            {
                                a++;
                                lblInformacao.Text = "Exames da Ordem de Serviço: " + a + " de " + listaOrdem.Count;
                                OrdemServicoExame ordemServicoExame = new OrdemServicoExame();
                                ordemServicoExame.Id = 0;
                                ordemServicoExame.Adicao = dtEmployee.Rows[i]["ADICAO"].ToString();
                                ordemServicoExame.Armacao = dtEmployee.Rows[i]["ARMACAO"].ToString();
                                ordemServicoExame.DataEntrega = DateTime.Now;
                                ordemServicoExame.DataExame = DateTime.Parse(dtEmployee.Rows[i]["DATA_EXAME"].ToString());
                                ordemServicoExame.DataExclusao = DateTime.Parse("1900-01-01 00:00:00");
                                ordemServicoExame.Dependente = null;
                                ordemServicoExame.Examinador = dtEmployee.Rows[i]["NOME_EXAMINADOR"].ToString();
                                
                                //LONGE OLHO DIREITO
                                ordemServicoExame.LdAltura = dtEmployee.Rows[i]["L_OD_ALTURA"].ToString();
                                ordemServicoExame.LdCilindrico = dtEmployee.Rows[i]["L_OD_CILINDRICO"].ToString();
                                ordemServicoExame.LdDp = dtEmployee.Rows[i]["L_OD_DP"].ToString();
                                ordemServicoExame.LdEsferico = dtEmployee.Rows[i]["L_OD_ESFERICO"].ToString();
                                ordemServicoExame.LdPosicao = dtEmployee.Rows[i]["L_OD_POSICAO"].ToString();

                                //LONGE OLHO ESQUERDO
                                ordemServicoExame.LeAltura = dtEmployee.Rows[i]["L_OE_ALTURA"].ToString();
                                ordemServicoExame.LeCilindrico = dtEmployee.Rows[i]["L_OE_CILINDRICO"].ToString();
                                ordemServicoExame.LeDp = dtEmployee.Rows[i]["L_OE_DP"].ToString();
                                ordemServicoExame.LeEsferico = dtEmployee.Rows[i]["L_OE_ESFERICO"].ToString();
                                ordemServicoExame.LePosicao = dtEmployee.Rows[i]["L_OE_POSICAO"].ToString();

                                //PERTO OLHO DIREITO
                                ordemServicoExame.PdAltura = dtEmployee.Rows[i]["P_OD_ALTURA"].ToString();
                                ordemServicoExame.PdCilindrico = dtEmployee.Rows[i]["P_OD_CILINDRICO"].ToString();
                                ordemServicoExame.PdDp = dtEmployee.Rows[i]["P_OD_DP"].ToString();
                                ordemServicoExame.PdEsferico = dtEmployee.Rows[i]["P_OD_ESFERICO"].ToString();
                                ordemServicoExame.PdPosicao = dtEmployee.Rows[i]["P_OD_POSICAO"].ToString();

                                //PERTO OLHO ESQUERDO
                                ordemServicoExame.PeAltura = dtEmployee.Rows[i]["P_OE_ALTURA"].ToString();
                                ordemServicoExame.PeCilindrico = dtEmployee.Rows[i]["P_OE_CILINDRICO"].ToString();
                                ordemServicoExame.PeDp = dtEmployee.Rows[i]["P_OE_DP"].ToString();
                                ordemServicoExame.PeEsferico = dtEmployee.Rows[i]["P_OE_ESFERICO"].ToString();
                                ordemServicoExame.PePosicao = dtEmployee.Rows[i]["P_OE_POSICAO"].ToString();

                                ordemServicoExame.Lente = dtEmployee.Rows[i]["LENTES"].ToString();
                                ordemServicoExame.Observacoes = "IMPORTADO ULTRA";
                                ordemServicoExame.OrdemServico = ordemServico;
                                ordemServicoExame.Pessoa = ordemServico.Cliente;
                                ordemServicoExame.ProximoExame = "";

                                Controller.getInstance().salvar(ordemServicoExame);
                            }
                        }
                    }
                    catch (FbException fbex)
                    {
                        MessageBox.Show("Erro ao acessar o FireBird " + fbex.Message, "Erro");
                    }
                    finally
                    {
                        fbConn.Close();
                    }
                }
                GenericaDesktop.ShowInfo("Importação de Exames Realizada com Sucesso!");
            }
        }

        private void puxarContasPagarUltra()
        {
            dataGridView1.Visible = true;
            FbConnection fbConn = new FbConnection(conexaoFirebird2);
            FbCommand fbCmd = new FbCommand("select fp.docto, fp.parceiro, fp.data_emissao, fpp.parcela, fpp.data_vencimento,fpp.saldo, fpp.data_quitacao, desp.dscdespesa, fp.historico from faturas_pagar fp inner join faturas_pagar_parcelas fpp on fp.faturas_pagar_id = fpp.faturas_pagar_id INNER JOIN despesas desp on fp.despesa_principal = desp.despesa where fpp.data_quitacao is null", fbConn);

            try
            {
                fbConn.Open();
                FbDataAdapter fbDa = new FbDataAdapter(fbCmd);
                dtEmployee = new DataTable();
                fbDa.Fill(dtEmployee);
                //dataGridView1.DataSource = dtEmployee;

                //Importar
                if (dtEmployee.Rows.Count > 0)
                {
                    int a = 0;
                    for (int i = 0; i < dtEmployee.Rows.Count; i++)
                    {
                        a++;
                        lblInformacao.Text = "Contas a Pagar: " + a + " de " + dtEmployee.Rows.Count;
                        ContaPagar contaPagar = new ContaPagar();
                        Pessoa pessoa = new Pessoa();
                        pessoa = pessoaController.selecionarPessoaPorCodigoImportado(dtEmployee.Rows[i]["PARCEIRO"].ToString());
                        if (pessoa != null)
                        {
                            if (pessoa.Id > 0)
                                contaPagar.Pessoa = pessoa;

                            contaPagar.Id = 0;
                            contaPagar.AcrescimoBaixa = 0;
                            contaPagar.CaixaPagamento = "";
                            contaPagar.DataOrigem = DateTime.Parse(dtEmployee.Rows[i]["DATA_EMISSAO"].ToString());
                            contaPagar.DataPagamento = DateTime.Parse("1900-01-01 00:00:00");
                            contaPagar.DescontoBaixa = 0;

                            contaPagar.Descricao = dtEmployee.Rows[i]["HISTORICO"].ToString();
                            if (String.IsNullOrEmpty(contaPagar.Descricao))
                                contaPagar.Descricao = "DESCRIÇÃO NÃO PREENCHIDA";
                            contaPagar.DescricaoPagamento = "";
                            contaPagar.DVenc = DateTime.Parse(dtEmployee.Rows[i]["DATA_VENCIMENTO"].ToString());
                            string despesa = dtEmployee.Rows[i]["DSCDESPESA"].ToString();
                            EmpresaFilialController empresaFilialController = new EmpresaFilialController();
                            IList<EmpresaFilial> listaEmpresa = empresaFilialController.selecionarTodasFiliais();
                            if (listaEmpresa.Count == 4)
                            {
                                if (despesa.Contains("ARINOS"))
                                {
                                    EmpresaFilial empresaFilial = new EmpresaFilial();
                                    empresaFilial.Id = 2;
                                    empresaFilial = (EmpresaFilial)Controller.getInstance().selecionar(empresaFilial);
                                    contaPagar.EmpresaFilial = empresaFilial;
                                }
                                else if (despesa.Contains("BURITIS"))
                                {
                                    EmpresaFilial empresaFilial = new EmpresaFilial();
                                    empresaFilial.Id = 3;
                                    empresaFilial = (EmpresaFilial)Controller.getInstance().selecionar(empresaFilial);
                                    contaPagar.EmpresaFilial = empresaFilial;
                                }
                                else if (despesa.Contains("PARACATU"))
                                {
                                    EmpresaFilial empresaFilial = new EmpresaFilial();
                                    empresaFilial.Id = 4;
                                    empresaFilial = (EmpresaFilial)Controller.getInstance().selecionar(empresaFilial);
                                    contaPagar.EmpresaFilial = empresaFilial;
                                }
                                else
                                    contaPagar.EmpresaFilial = Sessao.empresaFilialLogada;
                            }
                            else
                                contaPagar.EmpresaFilial = Sessao.empresaFilialLogada;

                            FormaPagamento forma = new FormaPagamento();
                            forma.Id = 5;
                            forma = (FormaPagamento)Controller.getInstance().selecionar(forma);
                            contaPagar.FormaPagamento = forma;

                            contaPagar.Historico = "IMPORTADO ULTRA";
                            contaPagar.NDup = dtEmployee.Rows[i]["PARCELA"].ToString();
                            contaPagar.Nfe = null;
                            contaPagar.NumeroDocumento = dtEmployee.Rows[i]["DOCTO"].ToString();
                            contaPagar.Pago = false;

                            contaPagar.Pessoa = pessoa;
                            PlanoConta planoConta = new PlanoConta();
                            planoConta.Id = 2;
                            planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                            contaPagar.PlanoConta = planoConta;
                            contaPagar.ValorPago = 0;
                            contaPagar.ValorTotal = decimal.Parse(dtEmployee.Rows[i]["SALDO"].ToString());
                            contaPagar.VDup = decimal.Parse(dtEmployee.Rows[i]["SALDO"].ToString());
                            Controller.getInstance().salvar(contaPagar);
                        }
                    }
                }
            }
            catch (FbException fbex)
            {
                MessageBox.Show("Erro ao acessar o FireBird " + fbex.Message, "Erro");
            }
            finally
            {
                fbConn.Close();
            }
            
        GenericaDesktop.ShowInfo("Importação de Contas a Pagar Realizada com Sucesso!");
            
        }

        private void btnLocalizarBancoUltra_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "FDB Files(*.fdb)|*.fdb|All Files(*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();
            if (openFileDialog.CheckFileExists)
            {
                txtCaminhoBancoUltra.Text = @openFileDialog.FileName;
                caminhoBanco = txtCaminhoBancoUltra.Text;
                
                conexaoFirebird2 = "User=SYSDBA;Password=masterkey;Database=" + @caminhoBanco + ";DataSource=localhost;Port=3050;Dialect=3;Charset=NONE;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;";
            }
        }

        private void puxarAnexosOsUltra()
        {
            OrdemServicoController ordemServicoController = new OrdemServicoController();
            
            FbConnection fbConn = new FbConnection(conexaoFirebird2);
            FbCommand fbCmd = new FbCommand("select an.arquivo as Descricao, an.datahora as Data, ao.os as OrdemServico from anexos an inner join anexos_os ao on an.anexo_id = ao.anexo_id", fbConn);

            try
            {
                fbConn.Open();
                FbDataAdapter fbDa = new FbDataAdapter(fbCmd);
                dtEmployee = new DataTable();
                fbDa.Fill(dtEmployee);
                dataGridView1.DataSource = dtEmployee;


                if (dtEmployee.Rows.Count > 0)
                {
                    int a = 0;
                    for (int i = 0; i < dtEmployee.Rows.Count; i++)
                    {
                        a++;
                        lblInformacao.Text = "Anexos: " + a + " de " + dtEmployee.Rows.Count;
                        Anexo anexo = new Anexo();
                        anexo.Caminho = Sessao.parametroSistema.CaminhoAnexo + @"\" + dtEmployee.Rows[i]["Descricao"].ToString();
                        Random randNum = new Random();
                        string num = randNum.Next(1000, 1000000).ToString();
                        anexo.Codigo = "OS_IMPORTADA_" + num;
                        anexo.DataCadastro = DateTime.Parse(dtEmployee.Rows[i]["Data"].ToString());
                        anexo.Filial = Sessao.empresaFilialLogada;
                        IList<OrdemServico> listaOrdemServico = ordemServicoController.selecionarOrdemServicoPorSQL("Select * from OrdemServico as Tabela Where Tabela.NumeroSerie = '"+ (dtEmployee.Rows[i]["OrdemServico"].ToString()) + "'");
                        if(listaOrdemServico.Count == 1)
                        {
                            foreach(OrdemServico os in listaOrdemServico)
                            {
                                OrdemServico ordemServico = new OrdemServico();
                                ordemServico = os;
                                anexo.OrdemServico = ordemServico;
                                Controller.getInstance().salvar(anexo);
                            }
                        }
                    }
                    GenericaDesktop.ShowInfo("Anexo importado com sucesso!");
                }
            }
            catch (FbException fbex)
            {
                MessageBox.Show("Erro ao acessar o FireBird " + fbex.Message, "Erro");
            }
            finally
            {
                fbConn.Close();
            }
        }


        private void puxarParceirosUltra()
        {
            //dataGridView1.Visible = true;
            FbConnection fbConn = new FbConnection(conexaoFirebird2);
            FbCommand fbCmd = new FbCommand("select p.parceiro,p.razao_social, p.nome, p.fantasia,p.endereco, p.numero," +
                "p.complemento,p.bairro, p.municipio, p.uf, p.cep, p.celular,p.cnpj,p.telefone,p.pessoa_fj,p.idn_sexo, p.cpf, p.rg,p.data_cadastro," +
                "p.ativo, p.idn_cliente, p.idn_fornecedor,p.idn_transportadora, p.idn_funcionario, p.idn_vendedor, p.pai, p.mae, " +
                "p.nascimento, p.conjuge, p.ie,p.email " +
                "from parceiros p where p.ativo = 'S'", fbConn);

            try
            {
                fbConn.Open();
                FbDataAdapter fbDa = new FbDataAdapter(fbCmd);
                dtEmployee = new DataTable();
                fbDa.Fill(dtEmployee);
                //dataGridView1.DataSource = dtEmployee;

                //Importar
                if (dtEmployee.Rows.Count > 0)
                {
                    int a = 0;
                    for (int i = 0; i < dtEmployee.Rows.Count; i++)
                    {
                        a++;
                        lblInformacao.Text = "Pessoa/Parceiro: " + a + " de " + dtEmployee.Rows.Count;
                        Pessoa pessoa = new Pessoa();
                        pessoa.Id = 0;
                        string teste = dtEmployee.Rows[i]["IDN_CLIENTE"].ToString();
                        if (dtEmployee.Rows[i]["IDN_CLIENTE"].ToString().Equals("S"))
                            pessoa.Cliente = true;
                        if (dtEmployee.Rows[i]["IDN_FORNECEDOR"].ToString().Equals("S"))
                            pessoa.Fornecedor = true;
                        if (dtEmployee.Rows[i]["IDN_VENDEDOR"].ToString().Equals("S"))
                            pessoa.Vendedor = true;
                        if (dtEmployee.Rows[i]["IDN_TRANSPORTADORA"].ToString().Equals("S"))
                            pessoa.Transportadora = true;
                        if (dtEmployee.Rows[i]["IDN_FUNCIONARIO"].ToString().Equals("S"))
                            pessoa.Funcionario = true;

                        if(!String.IsNullOrEmpty(dtEmployee.Rows[i]["CNPJ"].ToString()))
                            pessoa.Cnpj = GenericaDesktop.RemoveCaracteres(dtEmployee.Rows[i]["CNPJ"].ToString());
                        else if(!String.IsNullOrEmpty(dtEmployee.Rows[i]["CPF"].ToString()))
                            pessoa.Cnpj = GenericaDesktop.RemoveCaracteres(dtEmployee.Rows[i]["CPF"].ToString());
                        if (String.IsNullOrEmpty(pessoa.Cnpj))
                            pessoa.Cnpj = "";

                        pessoa.ReceberLembrete = false;
                        pessoa.Email = dtEmployee.Rows[i]["EMAIL"].ToString();
                        pessoa.Mae = dtEmployee.Rows[i]["MAE"].ToString();
                        pessoa.Pai = dtEmployee.Rows[i]["PAI"].ToString();
                        pessoa.Cobrador = false;
                        pessoa.CodigoImportacao = dtEmployee.Rows[i]["PARCEIRO"].ToString();
                        pessoa.ComissaoVendedor = 0;
                        pessoa.ContatoTrabalho = "";
                        if(!String.IsNullOrEmpty(dtEmployee.Rows[i]["NASCIMENTO"].ToString()))
                         pessoa.DataNascimento = DateTime.Parse(dtEmployee.Rows[i]["NASCIMENTO"].ToString());
                        pessoa.EscritorioCobranca = false;
                        pessoa.FlagExcluido = false;
                        pessoa.FuncaoTrabalho = "";
                        pessoa.InscricaoEstadual = dtEmployee.Rows[i]["IE"].ToString();
                        pessoa.LimiteCredito = 0;
                        pessoa.LocalTrabalho = "";
                        if (!String.IsNullOrEmpty(dtEmployee.Rows[i]["FANTASIA"].ToString()))
                            pessoa.NomeFantasia = dtEmployee.Rows[i]["FANTASIA"].ToString();
                        else if(!String.IsNullOrEmpty(dtEmployee.Rows[i]["NOME"].ToString()))
                            pessoa.NomeFantasia = dtEmployee.Rows[i]["NOME"].ToString();
                        else
                            pessoa.NomeFantasia = dtEmployee.Rows[i]["RAZAO_SOCIAL"].ToString();
                        if(!String.IsNullOrEmpty(dtEmployee.Rows[i]["RAZAO_SOCIAL"].ToString()))
                            pessoa.RazaoSocial = dtEmployee.Rows[i]["RAZAO_SOCIAL"].ToString();
                        else
                            pessoa.RazaoSocial = dtEmployee.Rows[i]["NOME"].ToString();

                        pessoa.Observacoes = "IMPORTADO ULTRA";
                        pessoa.PessoaTelefone = null;
                        pessoa.EnderecoPrincipal = null;
                        pessoa.ReceberLembrete = false;
                        pessoa.RegistradoSpc = false;
                        pessoa.Rg = dtEmployee.Rows[i]["RG"].ToString();
                        pessoa.SalarioTrabalho = "";
                        if (!String.IsNullOrEmpty(dtEmployee.Rows[i]["IDN_SEXO"].ToString()))
                            pessoa.Sexo = dtEmployee.Rows[i]["IDN_SEXO"].ToString();
                        pessoa.Tecnico = false;
                        pessoa.TelefoneTrabalho = "";
                        pessoa.TempoTrabalho = "";
                        pessoa.TipoParceiroImportado = "";
                        pessoa.TipoPessoa = "PF";
                        if(dtEmployee.Rows[i]["PESSOA_FJ"].ToString().Equals("J"))
                            pessoa.TipoPessoa = "PJ";
                        Controller.getInstance().salvar(pessoa);

                        Endereco endereco = new Endereco();
                        CidadeController cidadeController = new CidadeController();
                        Cidade cidade = new Cidade();
                        try 
                        { 
                            cidade = cidadeController.selecionarCidadePorDescricaoEUf(dtEmployee.Rows[i]["MUNICIPIO"].ToString(), dtEmployee.Rows[i]["UF"].ToString()); 
                        }
                        catch 
                        { 
                            cidade = cidadeController.selecionarCidadePorDescricaoEUf("UNAI", "MG"); 
                        }
                        if (cidade != null)
                            endereco.Cidade = cidade;
                        else
                        {
                            cidade = cidadeController.selecionarCidadePorDescricao("UNAI");
                            endereco.Cidade = cidade;
                        }
                        endereco.Logradouro = dtEmployee.Rows[i]["ENDERECO"].ToString();
                        if (String.IsNullOrEmpty(endereco.Logradouro))
                            endereco.Logradouro = "SEM PREENCHIMENTO";
                        endereco.Numero = dtEmployee.Rows[i]["NUMERO"].ToString();
                        if (String.IsNullOrEmpty(endereco.Numero))
                            endereco.Numero = "S/N";
                        endereco.Bairro = dtEmployee.Rows[i]["BAIRRO"].ToString();
                        endereco.Complemento = dtEmployee.Rows[i]["COMPLEMENTO"].ToString();
                        endereco.EmpresaFilial = Sessao.empresaFilialLogada;
                        endereco.Referencia = "";
                        endereco.Pessoa = pessoa;
                        if(!String.IsNullOrEmpty(dtEmployee.Rows[i]["CEP"].ToString()))
                            endereco.Cep = Generica.RemoveCaracteres(dtEmployee.Rows[i]["CEP"].ToString());

                        Controller.getInstance().salvar(endereco);
                        pessoa.EnderecoPrincipal = endereco;
                        Controller.getInstance().salvar(pessoa);

                    }
                    GenericaDesktop.ShowInfo("Importação de "+ dtEmployee.Rows.Count + " Parceiros Realizada com Sucesso!");
                }
                
            }
            catch (FbException fbex)
            {
                MessageBox.Show("Erro ao acessar o FireBird " + fbex.Message, "Erro");
            }
            finally
            {
                fbConn.Close();
            }
        }

        private void escreverMensagemTela(string mensagem) 
        {
            if (lblInformacao.InvokeRequired)
            {
                // Define o método que será chamado na thread da UI
                lblInformacao.Invoke(new Action(() =>
                {
                    lblInformacao.Text = mensagem;
                }));
            }
            else
            {
                // Atualiza o controle diretamente se já estiver na thread da UI
                lblInformacao.Text = mensagem;
            }
        }
        private void puxarProdutosUltra()
        {
            //dataGridView1.Visible = true;
            FbConnection fbConn = new FbConnection(conexaoFirebird2);
            FbCommand fbCmd = new FbCommand("SELECT DISTINCT p.codproduto, pc.codproduto_clas, pc.dscproduto_clas, p.dscproduto, p.idn_revenda, p.codgrupo, p.und_medida, p.marca, gf.dscgrupo_fiscal, COALESCE(pc.cod_ncm, p.cod_ncm) AS ncm, pc.codbarras, pc.ref_fabrica, COALESCE((SELECT FIRST 1 custo1.custo_unitario FROM custo_reposicao_atual custo1 WHERE custo1.codproduto = pc.codproduto ORDER BY custo1.data_ultima_entrada DESC), 0.00) AS preco_custo, COALESCE((SELECT FIRST 1 pr1.preco FROM precos pr1 WHERE pr1.codproduto = pc.codproduto ORDER BY pr1.data_criacao_alteracao DESC), 0.00) AS preco, COALESCE(pc.cest, nc.cest) AS cest, COALESCE(NULLIF(sal_agg.qtdc, 0), 0) AS qtdc, CASE WHEN COALESCE(sal_agg.qtd, 0) < 0 THEN 0 ELSE COALESCE(sal_agg.qtd, 0) END AS qtd FROM produtos p INNER JOIN produtos_clas pc ON pc.codproduto = p.codproduto LEFT JOIN ncm_cest nc ON nc.cod_ncm = COALESCE(pc.cod_ncm, p.cod_ncm) LEFT JOIN grupos_fiscais gf ON gf.grupo_fiscal = p.grupo_fiscal LEFT JOIN (SELECT codproduto, codproduto_clas, SUM(qtdc) AS qtdc, SUM(qtd) AS qtd FROM estoque_saldo_atual GROUP BY codproduto, codproduto_clas) sal_agg ON sal_agg.codproduto = p.codproduto AND sal_agg.codproduto_clas = pc.codproduto_clas WHERE p.dtabaixa IS NULL;", fbConn);

            try
            {
                fbConn.Open();
                FbDataAdapter fbDa = new FbDataAdapter(fbCmd);
                dtEmployee = new DataTable();
                fbDa.Fill(dtEmployee);
                //dataGridView1.DataSource = dtEmployee;
                
                //Importar
                if (dtEmployee.Rows.Count > 0)
                {
                    int a = 0;
                    for (int i = 0; i < dtEmployee.Rows.Count; i++)
                    {
                        a++;
                        bool cadastrarGradeExtra = false;
                        lblInformacao.Text = "Produto: " + a + " de " + dtEmployee.Rows.Count + " COD: " + dtEmployee.Rows[i]["CODPRODUTO"].ToString();
                        Produto produto = new Produto();

                        produto.Id = int.Parse(dtEmployee.Rows[i]["CODPRODUTO"].ToString());

                        try { produto = (Produto)Controller.getInstance().selecionar(produto); } catch { produto = new Produto(); }
                        if(produto != null)
                        {
                            if (!String.IsNullOrEmpty(produto.Descricao))
                                cadastrarGradeExtra = true;
                        }

                        produto.Id = int.Parse(dtEmployee.Rows[i]["CODPRODUTO"].ToString());
                        produto.AnoVeiculo = "";
                        produto.CapacidadeTracao = "";

                        string cest = "";
                        if (!String.IsNullOrEmpty(dtEmployee.Rows[i]["CEST"].ToString()))
                            cest = GenericaDesktop.RemoveCaracteres(dtEmployee.Rows[i]["CEST"].ToString());
                        produto.Cest = cest;
                        if (dtEmployee.Rows[i]["DSCGRUPO_FISCAL"].ToString().Equals("TRIBUTADO"))
                        {
                            produto.CfopVenda = "5102";
                            produto.CstIcms = "102";
                            GrupoFiscal grupoFiscal = new GrupoFiscal();
                            grupoFiscal.Id = 1;
                            grupoFiscal = (GrupoFiscal)Controller.getInstance().selecionar(grupoFiscal);
                            produto.GrupoFiscal = grupoFiscal;
                        }
                        else if (dtEmployee.Rows[i]["DSCGRUPO_FISCAL"].ToString().Equals("SUBSTITUICAO TRIBUTARIA"))
                        {
                            produto.CfopVenda = "5405";
                            produto.CstIcms = "500";
                            GrupoFiscal grupoFiscal = new GrupoFiscal();
                            grupoFiscal.Id = 2;
                            grupoFiscal = (GrupoFiscal)Controller.getInstance().selecionar(grupoFiscal);
                            produto.GrupoFiscal = grupoFiscal;
                        }
                        string aaat = "";
                        if (produto.Id == 39)
                            aaat = "";
                        produto.Chassi = "";
                        produto.CilindradaCc = "";
                        produto.CodAnp = "";
                        produto.CodSeloIpi = "";
                        produto.Combustivel = "";
                        produto.CondicaoProduto = "";
                        produto.CondicaoVeiculo = "";
                        produto.ControlaEstoque = true;
                        produto.CorDenatran = "";
                        produto.CorMontadora = "";
                        produto.CstCofins = "99";
                        produto.CstIpi = "99";
                        produto.CstPis = "99";
                        produto.DataCadastro = DateTime.Now;
                        produto.Descricao = dtEmployee.Rows[i]["DSCPRODUTO"].ToString();
                        produto.DistanciaEixo = "";
                        produto.Ean = dtEmployee.Rows[i]["CODBARRAS"].ToString();
                        produto.Ecommerce = false;
                        produto.Empresa = Sessao.empresaFilialLogada.Empresa;
                        produto.EmpresaFilial = Sessao.empresaFilialLogada;
                        produto.EnqIpi = "999";
                        produto.EspecieVeiculo = "";

                        //Estoque
                        //produto.Estoque = double.Parse(dtEmployee.Rows[i]["QTDC"].ToString());
                        //produto.EstoqueAuxiliar = double.Parse(dtEmployee.Rows[i]["QTD"].ToString());
                        produto.Estoque = 0;
                        produto.EstoqueAuxiliar = 0;

                        produto.FlagExcluido = false;
                        produto.Grade = false;
                        produto.GradePrincipal = null;
                        produto.IdComplementar = "";
                        produto.KmEntrada = "";
                        produto.LotacaoVeiculo = "";

                        Marca marca = new Marca();
                        marca = marcaController.selecionarMarcaPorDescricao(dtEmployee.Rows[i]["MARCA"].ToString());
                        if (marca != null)
                        {
                            if (marca.Id > 0)
                            {
                                produto.Marca = marca;
                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(dtEmployee.Rows[i]["MARCA"].ToString()))
                                {
                                    marca = new Marca();
                                    marca.Id = 0;
                                    marca.Descricao = dtEmployee.Rows[i]["MARCA"].ToString();
                                    marca.Empresa = Sessao.empresaFilialLogada.Empresa;
                                    Controller.getInstance().salvar(marca);
                                    produto.Marca = marca;
                                }
                                else
                                    produto.Marca = null;
                            }
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(dtEmployee.Rows[i]["MARCA"].ToString()))
                            {
                                marca = new Marca();
                                marca.Id = 0;
                                marca.Descricao = dtEmployee.Rows[i]["MARCA"].ToString();
                                marca.Empresa = Sessao.empresaFilialLogada.Empresa;
                                Controller.getInstance().salvar(marca);
                                produto.Marca = marca;
                            }
                            else
                                produto.Marca = null;
                        }
                        produto.MarcaModelo = "";
                        produto.Markup = "";
                        produto.ModeloVeiculo = "";
                        string ncm = "";
                        if (!String.IsNullOrEmpty(dtEmployee.Rows[i]["NCM"].ToString()))
                            ncm = GenericaDesktop.RemoveCaracteres(dtEmployee.Rows[i]["NCM"].ToString());
                        produto.Ncm = ncm;
                        produto.NumeroMotor = "";
                        produto.Observacoes = "IMPORTADO ULTRA";
                        produto.OperadorCadastro = "1";
                        produto.OrigemIcms = "0";
                        produto.PercentualCofins = "";
                        produto.PercentualIcms = "";
                        produto.PercentualIpi = "";
                        produto.PercentualPis = "";
                        produto.PercGlp = 0;
                        produto.PercGni = 0;
                        produto.PercGnn = 0;
                        produto.Pesavel = false;
                        produto.PesoBrutoVeiculo = "";
                        produto.PesoLiquidoVeiculo = "";
                        produto.Placa = "";
                        produto.PotenciaCv = "";
                        ProdutoGrupo grupo = new ProdutoGrupo();
                        grupo.Id = 1;
                        grupo = (ProdutoGrupo)ProdutoGrupoController.getInstance().selecionar(grupo);
                        produto.ProdutoGrupo = grupo;
                        produto.ProdutoSetor = null;
                        produto.ProdutoSubGrupo = null;
                        produto.Referencia = dtEmployee.Rows[i]["REF_FABRICA"].ToString();
                        produto.Renavam = "";
                        produto.RestricaoVeiculo = "";
                        produto.SolicitaNumeroSerie = false;
                        produto.TipoCambio = "";
                        produto.TipoEntrada = "";
                        produto.TipoPintura = "";
                        if(dtEmployee.Rows[i]["IDN_REVENDA"].ToString().Equals("S"))
                            produto.TipoProduto = "REVENDA";
                        else
                            produto.TipoProduto = "USO E CONSUMO";
                        produto.TipoVeiculo = "";

                        UnidadeMedida unidadeMedida = new UnidadeMedida();
                        if (!String.IsNullOrEmpty(dtEmployee.Rows[i]["UND_MEDIDA"].ToString()))
                        {
                            unidadeMedida = unidadeMedidaController.selecionarUnidadeMedidaPorSigla(dtEmployee.Rows[i]["UND_MEDIDA"].ToString());
                            if (unidadeMedida != null)
                            {
                                if (unidadeMedida.Id > 0)
                                    produto.UnidadeMedida = unidadeMedida;
                            }
                            else
                            {
                                unidadeMedida = unidadeMedidaController.selecionarUnidadeMedidaPorSigla("UN");
                                if (unidadeMedida.Id > 0)
                                    produto.UnidadeMedida = unidadeMedida;
                            }
                        }
                        else
                        {
                            unidadeMedida = unidadeMedidaController.selecionarUnidadeMedidaPorSigla("UN");
                            if (unidadeMedida.Id > 0)
                                produto.UnidadeMedida = unidadeMedida;
                        }
                        produto.ValorCusto = decimal.Parse(dtEmployee.Rows[i]["PRECO_CUSTO"].ToString());
                        produto.ValorPartida = 0;
                        produto.ValorVenda = decimal.Parse(dtEmployee.Rows[i]["PRECO"].ToString());
                        if (produto.ValorVenda == 0)
                            produto.ValorVenda = 1;
                        produto.Veiculo = false;
                        produto.VeiculoNovo = false;
            
                        Controller.getInstance().salvar(produto);

                
                        ProdutoGrade produtoGrade = new ProdutoGrade();
                        if (cadastrarGradeExtra == false)
                        {
                            produtoGrade.Produto = produto;
                            produtoGrade.Principal = true;
                            produtoGrade.ValorVenda = produto.ValorVenda;
                            string descClass = dtEmployee.Rows[i]["DSCPRODUTO_CLAS"].ToString();
                            if (String.IsNullOrEmpty(descClass))
                                descClass = dtEmployee.Rows[i]["CODPRODUTO_CLAS"].ToString();
                            produtoGrade.Descricao = descClass;
                            produtoGrade.Id = 0;
                            produtoGrade.QuantidadeMedida = 1;
                            produtoGrade.UnidadeMedida = produto.UnidadeMedida;
                            Controller.getInstance().salvar(produtoGrade);
                            produto.GradePrincipal = produtoGrade;
                            Controller.getInstance().salvar(produto);
                        }
                        else
                        {
                            produtoGrade.Produto = produto;
                            produtoGrade.Principal = false;
                            produtoGrade.ValorVenda = produto.ValorVenda;
                            string descClass = dtEmployee.Rows[i]["DSCPRODUTO_CLAS"].ToString();
                            if (String.IsNullOrEmpty(descClass))
                                descClass = dtEmployee.Rows[i]["CODPRODUTO_CLAS"].ToString();
                            produtoGrade.Descricao = descClass;
                            produtoGrade.Id = 0;
                            produtoGrade.QuantidadeMedida = 1;
                            produtoGrade.UnidadeMedida = produto.UnidadeMedida;
                            Controller.getInstance().salvar(produtoGrade);
                            produto.Grade = true;
                            Controller.getInstance().salvar(produto);
                        }
                        if (!String.IsNullOrEmpty(produto.Ean) && produtoGrade != null)
                        {
                            ProdutoCodigoBarras produtoCodigoBarras = new ProdutoCodigoBarras();
                            produtoCodigoBarras.Produto = produto;
                            produtoCodigoBarras.CodigoBarras = produto.Ean;
                            produtoCodigoBarras.ProdutoGrade = produtoGrade;
                            produtoCodigoBarras.OperadorCadastro = "1";
                            produtoCodigoBarras.FlagExcluido = false;
                            produtoCodigoBarras.Id = 0;
                            Controller.getInstance().salvar(produtoCodigoBarras);
                        }
                        else if (produtoGrade != null)
                        {
                            ProdutoCodigoBarras produtoCodigoBarras = new ProdutoCodigoBarras();
                            produtoCodigoBarras.Produto = produto;
                            produtoCodigoBarras.CodigoBarras = gerarBarras();
                            produtoCodigoBarras.ProdutoGrade = produtoGrade;
                            produtoCodigoBarras.OperadorCadastro = "1";
                            produtoCodigoBarras.FlagExcluido = false;
                            produtoCodigoBarras.Id = 0;
                            Controller.getInstance().salvar(produtoCodigoBarras);
                        }
 
                        //Estoque estoque = new Estoque();
                        //if (produto.Estoque > 0)
                        //{
                        //    estoque.Produto = produto;
                        //    estoque.Entrada = true;
                        //    estoque.FlagExcluido = false;
                        //    estoque.EmpresaFilial = Sessao.empresaFilialLogada;
                        //    estoque.Origem = "IMPORTACAO ULTRA";
                        //    estoque.Pessoa = null;
                        //    estoque.BalancoEstoque = null;
                        //    estoque.DataEntradaSaida = DateTime.Now;
                        //    estoque.Conciliado = true;
                        //    estoque.Descricao = "IMPORTADO SISTEMA ANTERIOR";
                        //    estoque.Quantidade = produto.Estoque;
                        //    estoque.QuantidadeInventario = 0;
                        //    estoque.Saida = false;
                        //    Controller.getInstance().salvar(estoque); 
                        //}
                        //if (produto.EstoqueAuxiliar > 0)
                        //{
                        //    estoque = new Estoque();
                        //    estoque.Produto = produto;
                        //    estoque.Entrada = true;
                        //    estoque.FlagExcluido = false;
                        //    estoque.EmpresaFilial = Sessao.empresaFilialLogada;
                        //    estoque.Origem = "IMPORTACAO ULTRA";
                        //    estoque.Pessoa = null;
                        //    estoque.BalancoEstoque = null;
                        //    estoque.DataEntradaSaida = DateTime.Now;
                        //    estoque.Conciliado = false;
                        //    estoque.Descricao = "IMPORTADO SISTEMA ANTERIOR";
                        //    estoque.Quantidade = produto.EstoqueAuxiliar;
                        //    estoque.QuantidadeInventario = 0;
                        //    estoque.Saida = false;
                        //    Controller.getInstance().salvar(estoque);
                        //}
                    }
                    //lblInformacao.Text = "Ajustando estoque, aguarde...";
                    //FrmBalancoEstoqueLista frm = new FrmBalancoEstoqueLista();
                    //frm.ajustarQuantidadeProdutoPeloMovimentoEstoque();
                    GenericaDesktop.ShowInfo("Importação de " + dtEmployee.Rows.Count + " Produtos Realizada com Sucesso!");
                    lblInformacao.Visible = false;
                }

            }
            catch (FbException fbex)
            {
                MessageBox.Show("Erro ao acessar o FireBird " + fbex.Message, "Erro");
            }
            finally
            {
                fbConn.Close();
            }
        }
        private Ean13 ean13 = null;
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public void CreateEan13()
        {
            ean13 = new Ean13();
            ean13.CountryCode = RandomNumber(10, 78).ToString();
            ean13.ManufacturerCode = RandomNumber(79000, 99000).ToString();
            ean13.ProductCode = RandomNumber(10000, 99000).ToString();
            ean13.ChecksumDigit = RandomNumber(1, 9).ToString();
        }
        private string gerarBarras()
        {
            string codigoBarras;
            ProdutoCodigoBarrasController produtoCodigoBarrasController = new ProdutoCodigoBarrasController();

            // Função para gerar um código de barras único
            do
            {
                CreateEan13();
                ean13.Scale = 1;
                codigoBarras = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;

                // Verifica se o código de barras gerado já existe na base de dados
                IList<ProdutoCodigoBarras> listaProd = produtoCodigoBarrasController.selecionarCodigoBarrasPorSQL(
                    "SELECT * FROM ProdutoCodigoBarras AS Tabela WHERE Tabela.FlagExcluido <> true AND Tabela.CodigoBarras = '" + codigoBarras + "'"
                );

                // Se o código de barras não existir, retorna-o
                if (listaProd.Count == 0)
                    return codigoBarras;

            } while (true); // Continua gerando até encontrar um código de barras único
        }

        private void btnImportarSaldoEstoque_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(() => importarSaldo());
            th.Start();
            Application.DoEvents();
            th.Join();
        }

        private void importarSaldo()
        {
            lblInformacao.Visible = true;
            int i = 0;
            lblInformacao.Text = "Importação de Estoque Iniciada...";
            foreach (DataGridViewRow col in dataGridView1.Rows)
            {
                i++;
                Produto produto = new Produto();
                produto.Id = int.Parse(col.Cells[0].Value.ToString());
                produto = (Produto)Controller.getInstance().selecionar(produto);
                if (radioConciliado.Checked == true)
                    produto.Estoque = double.Parse(col.Cells[1].Value.ToString());
                else
                    produto.EstoqueAuxiliar = double.Parse(col.Cells[1].Value.ToString());

                Estoque estoque = new Estoque();
                if (produto.Estoque > 0)
                {
                    estoque.Produto = produto;
                    estoque.Entrada = true;
                    estoque.FlagExcluido = false;
                    estoque.EmpresaFilial = Sessao.empresaFilialLogada;
                    estoque.Origem = "IMPORTACAO ULTRA";
                    estoque.Pessoa = null;
                    estoque.BalancoEstoque = null;
                    estoque.DataEntradaSaida = DateTime.Now;
                    estoque.Conciliado = true;
                    estoque.Descricao = "IMPORTADO SISTEMA ANTERIOR";
                    estoque.Quantidade = produto.Estoque;
                    estoque.QuantidadeInventario = 0;
                    estoque.Saida = false;
                    Controller.getInstance().salvar(estoque);
                }
                if (produto.EstoqueAuxiliar > 0)
                {
                    estoque = new Estoque();
                    estoque.Produto = produto;
                    estoque.Entrada = true;
                    estoque.FlagExcluido = false;
                    estoque.EmpresaFilial = Sessao.empresaFilialLogada;
                    estoque.Origem = "IMPORTACAO ULTRA";
                    estoque.Pessoa = null;
                    estoque.BalancoEstoque = null;
                    estoque.DataEntradaSaida = DateTime.Now;
                    estoque.Conciliado = false;
                    estoque.Descricao = "IMPORTADO SISTEMA ANTERIOR";
                    estoque.Quantidade = produto.EstoqueAuxiliar;
                    estoque.QuantidadeInventario = 0;
                    estoque.Saida = false;
                    Controller.getInstance().salvar(estoque);
                }
                lblInformacao.Text = "Importação de Estoque " + i + " de " + dataGridView1.Rows.Count;
            }
            FrmBalancoEstoqueLista frm = new FrmBalancoEstoqueLista();
            frm.ajustarQuantidadeProdutoPeloMovimentoEstoque();
            lblInformacao.Visible = false;
            GenericaDesktop.ShowInfo("Importação de saldo finalizada!");
        }

        private void importarTelefone()
        {
      
            int i = 0;
            foreach (DataGridViewRow col in dataGridView1.Rows)
            {
                i++;
                Pessoa pessoa = new Pessoa();
                int idImportado = int.Parse(col.Cells[0].Value.ToString());
                pessoa = pessoaController.selecionarPessoaPorCodigoImportado(idImportado.ToString());
               
                if (pessoa.PessoaTelefone == null)
                {
                    PessoaTelefone pessoaTelefone = new PessoaTelefone();
                    pessoaTelefone.Pessoa = pessoa;
                    //captura o celular
                    pessoaTelefone.Telefone = col.Cells[3].Value.ToString();
                    //se nao tiver celular captura o fixo
                    if(String.IsNullOrEmpty(pessoaTelefone.Telefone))
                        pessoaTelefone.Telefone = col.Cells[2].Value.ToString();
                    pessoaTelefone.Observacoes = "";

                    // aqui preciso tratar o telefone para alimentar o ddd
                    string ddd;
                    string numero;
                    ExtrairDDDENumero(pessoaTelefone.Telefone, out ddd, out numero);
                    pessoaTelefone.Ddd = ddd;
                    pessoaTelefone.Telefone = numero;

                    if (!String.IsNullOrEmpty(pessoaTelefone.Telefone))
                    {
                        Controller.getInstance().salvar(pessoaTelefone);
                        pessoa.PessoaTelefone = pessoaTelefone;
                        Controller.getInstance().salvar(pessoa);
                    }
                }
                // Atualiza o lblInformacao na thread principal
                lblInformacao.Text = "Fone: " + i + " de " + dataGridView1.Rows.Count;
            }
        }

        private void ExtrairDDDENumero(string telefone, out string ddd, out string numero)
        {
            // Inicializa os valores de ddd e numero
            ddd = "";
            numero = telefone;

            // Remove caracteres não numéricos para facilitar o processamento
            telefone = new string(telefone.Where(c => char.IsDigit(c)).ToArray());

            // Verifica se o telefone tem 11 dígitos (DDD + 9 dígitos)
            if (telefone.Length == 11)
            {
                // Extrai o DDD e o número
                ddd = telefone.Substring(0, 2);
                numero = telefone.Substring(2);
            }
            else if (telefone.Length == 10)
            {
                // Extrai o DDD e o número
                ddd = telefone.Substring(0, 2);
                numero = telefone.Substring(2);
            }
            else if (telefone.Length == 8)
            {
                // Extrai o DDD e o número
                ddd = Sessao.empresaFilialLogada.DddPrincipal;
                numero = telefone;
            }
        }

        private void btnImportarCsvFones_Click(object sender, EventArgs e)
        {
            lblInformacao.Visible = true;
            Thread th = new Thread(() => importarTelefone());
            th.Start();
            Application.DoEvents();
            th.Join();
        }
    }
}
