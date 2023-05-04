using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Utils.ImportadorSistemas
{
    public partial class FrmImportarCSV : Form
    {
        UnidadeMedidaController unidadeMedidaController = new UnidadeMedidaController();
        MarcaController marcaController = new MarcaController();
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
        }

        private void importarClientesFornecedores()
        {
         //   lblInformacao.Visible = true;
            int i = 0;
          //  lblInformacao.Text = "Importação iniciada...";
            foreach (DataGridViewRow col in dataGridView1.Rows)
            {
                i++;
                Pessoa pessoa = new Pessoa();
             //   lblInformacao.Text = "Importação de Pessoas/Parceiros: " + i + " de " + dataGridView1.Rows.Count;
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
                    pessoa.Fornecedor = true;
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
                   lblInformacao.Visible = true;
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
    }
}
