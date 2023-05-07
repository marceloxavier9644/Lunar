using FirebirdSql.Data.FirebirdClient;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace Lunar.Utils.ImportadorSistemas
{
    public partial class FrmImportarCSV : Form
    {
        ProdutoController produtoController = new ProdutoController();
        UnidadeMedidaController unidadeMedidaController = new UnidadeMedidaController();
        MarcaController marcaController = new MarcaController();
        PessoaController pessoaController = new PessoaController();
        DataTable dtEmployee = new DataTable();
        //String conexaoFirebird = "DataSource=localhost; Database=C:\\Ultra_Inst\\Banco\\Gestao.FDB; UserId=SYSDBA; Pwd=masterkey";
        String conexaoFirebird2 = "User=SYSDBA;Password=masterkey;Database=C:\\Ultra_Inst\\Banco\\Gestao.fdb;DataSource=localhost;Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;\r\nServerType=0;";
        public FrmImportarCSV()
        {
            InitializeComponent();
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
            if (radioClientes.Checked == true)
            {
                Thread th = new Thread(() => importarClientesFornecedores());
                th.Start();
                Application.DoEvents();
                th.Join();
            }
            if (radioProdutos.Checked == true)
            {
                Thread th = new Thread(() => importarProdutos());
                th.Start();
                Application.DoEvents();
                th.Join();
            }
            if (radioContasReceber.Checked == true)
            {
                Thread th = new Thread(() => importarContaReceber());
                th.Start();
                Application.DoEvents();
                th.Join();
            }
            if (radioOrdemServico.Checked == true)
            {
                Thread th = new Thread(() => importarOS(dtEmployee));
                th.Start();
                Application.DoEvents();
                th.Join();
            }
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
                    if(pessoa.EnderecoPrincipal != null)
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

        private void puxarDadosFirebird()
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
                puxarDadosFirebird();
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
                foreach(OrdemServico ordemServico in listaOrdem)
                {
                    dataGridView1.Visible = true;
                    FbConnection fbConn = new FbConnection(conexaoFirebird2);
                    FbCommand fbCmd = new FbCommand("Select * from os_produtos_alocados where os_produtos_alocados.OS = " + ordemServico.NumeroSerie, fbConn);

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
                                ordemServicoProduto.Produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(Convert.ToInt32(dtEmployee.Rows[i]["PRODUTO"]), Sessao.empresaFilialLogada);
                                if (ordemServicoProduto.Produto != null)
                                {
                                    ordemServicoProduto.Quantidade = double.Parse(dtEmployee.Rows[i]["QTD"].ToString());
                                    ordemServicoProduto.ValorTotal = decimal.Parse(dtEmployee.Rows[i]["LIQUIDO"].ToString());
                                    ordemServicoProduto.ValorUnitario = decimal.Parse(dtEmployee.Rows[i]["UNITARIO"].ToString());
                                    Controller.getInstance().salvar(ordemServicoProduto);
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
    }
}
