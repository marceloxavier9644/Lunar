using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.Estoques
{
    public partial class FrmImprimirSaldoEstoque : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        public FrmImprimirSaldoEstoque(IList<Produto> listaProduto, bool apresentarCusto, bool apresentarVenda)
        {
            InitializeComponent();
            this.reportViewer1.Visible = true;

            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 75;

            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.DisplayName = "Saldo de Estoque em " + DateTime.Now.ToShortDateString();
            Microsoft.Reporting.WinForms.ReportDataSource dsInvent = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsInvent.Name = "dsSaldoEstoque";
            dsInvent.Value = this.bindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(dsInvent);

            EmpresaFilial empresaSelecionada = Sessao.empresaFilialLogada;

            string comp = "";
            if (empresaSelecionada.Endereco != null)
            {
                if (!String.IsNullOrEmpty(empresaSelecionada.Endereco.Complemento))
                    comp = " - " + empresaSelecionada.Endereco.Complemento;
                if (!String.IsNullOrEmpty(empresaSelecionada.Endereco.Bairro))
                    comp = comp + " - " + empresaSelecionada.Endereco.Bairro;
            }

            string fone = "";
            if (!String.IsNullOrEmpty(empresaSelecionada.DddPrincipal) && !String.IsNullOrEmpty(empresaSelecionada.TelefonePrincipal))
            {
                fone = GenericaDesktop.formatarFone(empresaSelecionada.DddPrincipal + empresaSelecionada.TelefonePrincipal.Trim());
            }
             

            ReportParameter[] p = new ReportParameter[6];
            p[0] = (new ReportParameter("RazaoSocial", empresaSelecionada.RazaoSocial));
            p[1] = (new ReportParameter("CnpjFilial", generica.FormatarCNPJ(empresaSelecionada.Cnpj)));
            p[2] = (new ReportParameter("LogradouroFilial", empresaSelecionada.Endereco.Logradouro + ", " + empresaSelecionada.Endereco.Numero + comp));
            p[3] = (new ReportParameter("CidadeFilial", empresaSelecionada.Endereco.Cidade.Descricao + "-" + empresaSelecionada.Endereco.Cidade.Estado.Uf));
            p[4] = (new ReportParameter("FoneFilial", fone));
            p[5] = (new ReportParameter("Logo", Sessao.parametroSistema.Logo));
            reportViewer1.LocalReport.SetParameters(p);
           
            foreach (Produto produto in listaProduto)
            {

                string grupo = "";
                string subGrupo = "";
                string localizacao = "";
                if (produto.ProdutoGrupo != null)
                    grupo = produto.ProdutoGrupo.Descricao;
                if (produto.ProdutoSubGrupo != null)
                    subGrupo = produto.ProdutoSubGrupo.Descricao;
                if (produto.ProdutoSetor != null)
                    localizacao = produto.ProdutoSetor.Descricao;

                decimal custo = produto.ValorCusto;
                if (apresentarCusto == false)
                    custo = 0;

                decimal precoVenda = produto.ValorVenda;
                if (apresentarVenda == false)
                    precoVenda = 0;


                dsSaldoEstoque.Produto.AddProdutoRow(produto.Id + produto.IdComplementar, produto.Descricao, grupo, subGrupo, localizacao,
                    produto.Ncm, produto.CfopVenda, custo, produto.Estoque, produto.EstoqueAuxiliar, precoVenda);
            }

            this.reportViewer1.RefreshReport();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                //formSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
               // this.Size = formSize;
            }
        }
    }
}
