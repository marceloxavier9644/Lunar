﻿using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static LunarBase.ClassesDAO.VendaDAO;

namespace Lunar.Telas.RelatoriosDiversos
{
    public partial class FrmComissaoRelatorio01 : Form
    {
        PessoaController pessoaController = new PessoaController();
        OrdemServicoController ordemServicoController = new OrdemServicoController();
        OrdemServicoDAO ordemServicoDAO = new OrdemServicoDAO();
        string dataInicial = "";
        string dataFinal = "";
        VendaDAO vendaDAO = new VendaDAO();
        double comissaoVendedor = 0;
        public FrmComissaoRelatorio01(string dataInicial, string dataFinal)
        {
            InitializeComponent();
            this.dataInicial = dataInicial;
            this.dataFinal = dataFinal;
        }

        private void gerarRelatorio()
        {
            string sql = "SELECT pv.Id, pv.RazaoSocial, sum(os.ValorTotal) as Valor, os.Id as OrdemServico " +
                "FROM ordemservico as os INNER JOIN pessoa pv on pv.ID = os.VENDEDOR " +
                "WHERE os.FLAGEXCLUIDO <> true and os.VENDEDOR is not null and " +
                "os.dataabertura between '"+dataInicial+" 00:00:00' and '"+dataFinal+" 23:59:59' " +
                "group by os.VENDEDOR";

            IList<OsComissao> listaOsComissao = ordemServicoDAO.selecionarOSComissaoPorSQL(sql);
            if(listaOsComissao.Count > 0)
            {
                Microsoft.Reporting.WinForms.ReportDataSource ds = new Microsoft.Reporting.WinForms.ReportDataSource();
                ds.Name = "dsComissaoVendedor";
                ds.Value = this.bindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(ds);

                ReportParameter[] p = new ReportParameter[1];
                p[0] = (new ReportParameter("Filtro", DateTime.Parse(dataInicial).ToString("dd/MM/yyyy") + " a " + DateTime.Parse(dataFinal).ToString("dd/MM/yyyy")));
                reportViewer1.LocalReport.SetParameters(p);
                foreach (OsComissao osComissao in listaOsComissao)
                {
                    Pessoa vendedor = new Pessoa();
                    vendedor.Id = osComissao.Id;
                    vendedor = (Pessoa)pessoaController.selecionar(vendedor);
                    if (vendedor != null)
                    {
                        if (vendedor.ComissaoVendedor > 0)
                            comissaoVendedor = vendedor.ComissaoVendedor;
                        else
                            comissaoVendedor = Sessao.parametroSistema.Comissao;
                    }
                    else
                        comissaoVendedor = Sessao.parametroSistema.Comissao;
                    decimal valorVendido = vendaDAO.selecionarValorVendidoPorVendedor(osComissao.Id, dataInicial, dataFinal);
                    dsComissaoVendedor.Comissao.AddComissaoRow(osComissao.Id.ToString(), osComissao.RazaoSocial, valorVendido, osComissao.Valor, comissaoVendedor, (valorVendido + osComissao.Valor) * decimal.Parse(comissaoVendedor.ToString()) / 100);
                }
                string sqlVenda = "SELECT p.Id as ID, p.RazaoSocial as Nome, v.VENDEDOR as Vendedor, sum(v.VALORFINAL) as ValorTotal, (select count(*) from ordemservico WHERE ordemservico.DATAABERTURA between '"+dataInicial+" 00:00:00' and '"+dataFinal+" 23:59:59' and ordemservico.FLAGEXCLUIDO <> true and ordemservico.VENDEDOR = v.VENDEDOR) as 'TOTALOS' FROM venda v Inner JOIN Pessoa p on v.VENDEDOR = p.ID WHERE v.DATAVENDA between '"+dataInicial+" 00:00:00' and '"+dataFinal+" 23:59:59' and v.FLAGEXCLUIDO <> true and v.CONCLUIDA = true and 'TOTALOS' = 0";
                IList<ComissaoVenda> listComissaoVenda =  vendaDAO.selecionarComissaoVendaPorSQL(sqlVenda);
                if(listComissaoVenda.Count > 0)
                {
                    foreach (ComissaoVenda comissaoVenda in listComissaoVenda)
                    {
                        if (comissaoVenda != null)
                        {
                            Pessoa vendedor = new Pessoa();
                            vendedor.Id = comissaoVenda.Id;
                            vendedor = (Pessoa)pessoaController.selecionar(vendedor);
                            if (vendedor.ComissaoVendedor > 0)
                                comissaoVendedor = vendedor.ComissaoVendedor;
                            else
                                comissaoVendedor = Sessao.parametroSistema.Comissao;

                            dsComissaoVendedor.Comissao.AddComissaoRow(comissaoVenda.Id.ToString(), comissaoVenda.Nome, comissaoVenda.ValorTotal, 0, comissaoVendedor, (comissaoVenda.ValorTotal) * decimal.Parse(comissaoVendedor.ToString()) / 100);
                        }
                    }
                }
            this.reportViewer1.RefreshReport();
        }
    }



        private void FrmComissaoRelatorio01_Load(object sender, EventArgs e)
        {
            gerarRelatorio();
        }
    }
}