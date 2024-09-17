using Lunar.Utils;
using LunarBase.ClassesDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LunarBase.ClassesDAO.PessoaDAO;

namespace Lunar.Telas.Vendas.Relatorios
{
    public partial class FrmRelatorioReativarClientes : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        public FrmRelatorioReativarClientes()
        {
            InitializeComponent();
        }

        private void txtDiasSemComprar_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtDiasSemComprar.Texts, e);
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDiasSemComprar.Texts))
            {
                string sql = "";

                if (chkIncluirClientesNuncaCompraram.Checked == true)
                    sql = "SELECT c.ID, c.RazaoSocial as RazaoSocial, MAX(v.DataVenda) AS UltimaCompra, SUM(v.ValorFinal) AS TotalCompras, CONCAT(pt.Ddd, pt.Telefone) AS Telefone FROM pessoa c LEFT JOIN venda v ON c.ID = v.cliente LEFT JOIN PessoaTelefone pt ON c.ID = pt.pessoa GROUP BY c.ID, c.RAZAOSOCIAL, pt.ddd, pt.telefone HAVING (UltimaCompra IS NULL OR DATEDIFF(NOW(), UltimaCompra) > " + txtDiasSemComprar.Texts+ ") ORDER BY UltimaCompra;";
                else
                    sql = "SELECT c.ID, c.RazaoSocial as RazaoSocial, MAX(v.DataVenda) AS UltimaCompra, SUM(v.ValorFinal) AS TotalCompras, CONCAT(pt.Ddd, pt.Telefone) AS Telefone FROM pessoa c JOIN venda v ON c.ID = v.cliente LEFT JOIN PessoaTelefone pt ON c.ID = pt.pessoa GROUP BY c.ID, c.RAZAOSOCIAL, pt.ddd, pt.telefone HAVING DATEDIFF(NOW(), UltimaCompra) > " + txtDiasSemComprar.Texts+ " ORDER BY UltimaCompra;";

                PessoaDAO pessoaDAO = new PessoaDAO();
                IList<PessoaConsulta> listaPessoas = pessoaDAO.SelecionarPessoasSemComprasPorSQLNativo(sql);
                if (listaPessoas.Count > 0)
                {
                    gridClient.DataSource = listaPessoas;
                }
                else
                {
                    gridClient.DataSource = null;
                    GenericaDesktop.ShowInfo("Nenhum cliente localizado com mais de " + txtDiasSemComprar.Texts + " dias sem comprar!");
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Preencha o campo de quantos dias!");
            }
        }

        private void gridClient_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }
    }
}
