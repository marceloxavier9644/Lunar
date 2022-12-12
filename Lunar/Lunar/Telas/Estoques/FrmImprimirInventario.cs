using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static LunarBase.Classes.Estoque;

namespace Lunar.Telas.Estoques
{
    public partial class FrmImprimirInventario : Form
    {
        IList<Inventario> listaEstoque = new List<Inventario>();
        string dataInventario = "";
        string numeroLivro = "";
        GenericaDesktop generica = new GenericaDesktop();
        public FrmImprimirInventario(IList<Inventario> listaEstoque, string dataInventario, string numeroLivro)
        {
            InitializeComponent();
            this.listaEstoque = listaEstoque;
            this.dataInventario = dataInventario;
            this.numeroLivro = numeroLivro;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmImprimirInventario_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.DisplayName = "Inventário de Estoque em " + dataInventario;
            Microsoft.Reporting.WinForms.ReportDataSource dsInvent = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsInvent.Name = "dsInventario";
            dsInvent.Value = this.bindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(dsInvent);

            string comp = "";
            if (!String.IsNullOrEmpty(Sessao.empresaFilialLogada.Endereco.Complemento))
                comp = " - " + Sessao.empresaFilialLogada.Endereco.Complemento;
            if (!String.IsNullOrEmpty(Sessao.empresaFilialLogada.Endereco.Bairro))
                comp = comp + " - " + Sessao.empresaFilialLogada.Endereco.Bairro;

            string fone = GenericaDesktop.formatarFone(Sessao.empresaFilialLogada.DddPrincipal+Sessao.empresaFilialLogada.TelefonePrincipal.Trim());

            ReportParameter[] p = new ReportParameter[8];
            p[0] = (new ReportParameter("RazaoFilial", Sessao.empresaFilialLogada.RazaoSocial));
            p[1] = (new ReportParameter("CnpjFilial",  generica.FormatarCNPJ(Sessao.empresaFilialLogada.Cnpj)));
            p[2] = (new ReportParameter("LogradouroFilial", Sessao.empresaFilialLogada.Endereco.Logradouro + ", " + Sessao.empresaFilialLogada.Endereco.Numero + comp));
            p[3] = (new ReportParameter("CidadeFilial", Sessao.empresaFilialLogada.Endereco.Cidade.Descricao + "-"+ Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf));
            p[4] = (new ReportParameter("FoneFilial", fone));
            p[5] = (new ReportParameter("DataInventario", dataInventario));
            p[6] = (new ReportParameter("Logo", Sessao.parametroSistema.Logo));
            p[7] = (new ReportParameter("NumeroLivro", numeroLivro));
            reportViewer1.LocalReport.SetParameters(p);

            foreach (Inventario inventario in listaEstoque)
            {
                dsInventario.Inventario.AddInventarioRow(inventario.produto.Ncm, inventario.produto.Id.ToString() + inventario.produto.IdComplementar, inventario.produto.Descricao, inventario.produto.Ean, inventario.produto.CstIcms, inventario.produto.UnidadeMedida.Sigla, inventario.quantidadeInventario, inventario.produto.ValorCusto, inventario.produto.ValorCusto * decimal.Parse(inventario.quantidadeInventario.ToString()));
            }
                this.reportViewer1.RefreshReport();
        }
}
}
