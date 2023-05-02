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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Utils.ImportadorSistemas
{
    public partial class FrmImportarCSV : Form
    {
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
            if(radioClientes.Checked == true)
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
                importarClientesFornecedores();
        }

        private void importarClientesFornecedores()
        {
            foreach (DataGridViewRow col in dataGridView1.Rows)
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Id = 0;
                pessoa.CodigoImportacao = col.Cells[0].Value.ToString();
                pessoa.RazaoSocial = col.Cells[1].Value.ToString();
                if (String.IsNullOrEmpty(pessoa.RazaoSocial))
                    pessoa.RazaoSocial = "SEM PREENCHIMENTO";
                pessoa.NomeFantasia = col.Cells[2].Value.ToString();
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

                Controller.getInstance().salvar(pessoa);

                //apos salvar a pessoa, salva o endereço.
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
                endereco.Bairro = col.Cells[13].Value.ToString();
                endereco.Complemento = col.Cells[14].Value.ToString();
                endereco.EmpresaFilial = Sessao.empresaFilialLogada;
                endereco.Pessoa = pessoa;
                
                Controller.getInstance().salvar(endereco);
                pessoa.EnderecoPrincipal = endereco;
                Controller.getInstance().salvar(pessoa);

                //apos salvar endereço salva os telefones
                if (!String.IsNullOrEmpty(col.Cells[15].Value.ToString()))
                {
                    PessoaTelefone pessoaTelefone = new PessoaTelefone();
                    pessoaTelefone.Pessoa = pessoa;
                    pessoaTelefone.Telefone = col.Cells[15].Value.ToString();
                    pessoaTelefone.Observacoes = "IMPORTAÇÃO DE OUTRO SISTEMA";
                    Controller.getInstance().salvar(pessoaTelefone);
                }
                if (!String.IsNullOrEmpty(col.Cells[16].Value.ToString()))
                {
                    PessoaTelefone pessoaTelefone = new PessoaTelefone();
                    pessoaTelefone.Pessoa = pessoa;
                    pessoaTelefone.Telefone = col.Cells[16].Value.ToString();
                    pessoaTelefone.Observacoes = "IMPORTAÇÃO DE OUTRO SISTEMA";
                    Controller.getInstance().salvar(pessoaTelefone);
                    pessoa.PessoaTelefone = pessoaTelefone;
                    Controller.getInstance().salvar(pessoa);
                }

            }
            GenericaDesktop.ShowInfo("Importação de Clientes/Fornecedores Realizada com Sucesso!");
        }
    }
}
