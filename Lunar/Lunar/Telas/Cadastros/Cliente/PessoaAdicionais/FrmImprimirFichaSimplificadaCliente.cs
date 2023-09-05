using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.Utilidades.NFe40Modelo;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    public partial class FrmImprimirFichaSimplificadaCliente : Form
    {
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        Pessoa pessoa = new Pessoa();
        public FrmImprimirFichaSimplificadaCliente(Pessoa pessoa)
        {
            InitializeComponent();
            this.pessoa = pessoa;
        }

        private void FrmImprimirFichaSimplificadaCliente_Load(object sender, EventArgs e)
        {
            string endereco = "";
            string cidade = "";
            string telefone = "";
            string bairro = "";
            string cep = "";
            string uf = "";
            string cnpj = "";
            if (!String.IsNullOrEmpty(pessoa.Cnpj)) 
            {
                if (pessoa.Cnpj.Length == 11)
                    cnpj = genericaDesktop.FormatarCPF(pessoa.Cnpj);
                else if (pessoa.Cnpj.Length == 14)
                    cnpj = genericaDesktop.FormatarCNPJ(pessoa.Cnpj);
            }
            if (pessoa.EnderecoPrincipal != null)
            {
                endereco = pessoa.EnderecoPrincipal.Logradouro + ", " + pessoa.EnderecoPrincipal.Numero + " " + pessoa.EnderecoPrincipal.Complemento;
                cep = pessoa.EnderecoPrincipal.Cep;
                bairro = pessoa.EnderecoPrincipal.Bairro;
                if(pessoa.EnderecoPrincipal.Cidade != null)
                {
                    cidade = pessoa.EnderecoPrincipal.Cidade.Descricao;
                    if (pessoa.EnderecoPrincipal.Cidade.Estado != null)
                        uf = pessoa.EnderecoPrincipal.Cidade.Estado.Uf;
                }
            }
            if(pessoa.PessoaTelefone != null)
            {
                telefone = GenericaDesktop.formatarFone(pessoa.PessoaTelefone.Ddd + pessoa.PessoaTelefone.Telefone);
            }
            dsClienteSimplificado.Cliente.AddClienteRow(pessoa.Id, pessoa.RazaoSocial, cnpj, endereco, "", bairro, cidade, uf, telefone, pessoa.Pai, pessoa.Mae, cep, pessoa.DataCadastro.ToShortDateString());
            this.reportViewer1.RefreshReport();
        }
    }
}
