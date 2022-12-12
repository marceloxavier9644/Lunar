using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using Syncfusion.WinForms.DataGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Financeiro.PlanoContas.PlanosPorGrupos
{
    public partial class FrmPlanoConta : Form
    {
        PlanoConta planoConta = new PlanoConta();
        PlanoContaController planoContaController = new PlanoContaController();
        String tipoConta = "";
        public FrmPlanoConta()
        {
            InitializeComponent();
            selecionaTodosPlanos();
        }

        private void selecionaTodosPlanos()
        {
            IList<PlanoConta> listaPlano = planoContaController.selecionarTodosPlanosConta();
            if(listaPlano.Count > 0)
            {
                grid.DataSource = listaPlano;
                this.grid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
                this.grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.grid.AutoSizeController.Refresh();
                grid.Refresh();
                //this.grid.GroupColumnDescriptions.Add(new GroupColumnDescription() { ColumnName = "IdAcima" });
            }
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if ((e.RowData as PlanoConta) != null)
            {
                if ((e.RowData as PlanoConta).TipoConta == "RECEITA")
                {
                    e.Style.TextColor = Color.Blue;
                    if ((e.RowData as PlanoConta).IdPai == "")
                    {
                        e.Style.Font.Bold = true;
                    }
                }
                else
                {
                    e.Style.TextColor = Color.Red;
                    if ((e.RowData as PlanoConta).IdPai == "")
                    {
                        e.Style.Font.Bold = true;
                    }
                }
            }
        }

        private void grid_CellClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            txtIdPai.Texts = "";
            txtNivelAcima.Texts = "";
            txtNovaClass.Texts = "";
            txtCodPlanoConta.Texts = "";
            txtDescricao.Texts = "";
            tipoConta = "";
            planoConta = new PlanoConta();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtIdPai.Texts = "";
            txtNivelAcima.Texts = "";
            txtNovaClass.Texts = "";
            txtCodPlanoConta.Texts = "";
            txtDescricao.Texts = "";

            if (grid.SelectedIndex >= 0)
            {
                planoConta = new PlanoConta();
                planoConta = (PlanoConta)grid.SelectedItem;
                txtIdPai.Texts = planoConta.IdPai;
                if (String.IsNullOrEmpty(txtIdPai.Texts))
                {
                    if (String.IsNullOrEmpty(planoConta.IdPai))
                        txtIdPai.Texts = planoConta.Id.ToString();
                }
                txtNivelAcima.Texts = planoConta.Classificacao;
                txtDescricao.Focus();

                PlanoContaDAO planoContaDAO = new PlanoContaDAO(); 
                IList<PlanoConta> listaPlano = new List<PlanoConta>();
                if (!String.IsNullOrEmpty(planoConta.IdPai))
                {
                    listaPlano = planoContaDAO.selecionarPlanoContaPeloIdPai(planoConta.IdPai, txtNivelAcima.Texts);
                }
              
                if(listaPlano.Count > 0)
                {
                    foreach(PlanoConta plano in listaPlano)
                    {
                        double ultimaClass = double.Parse(plano.Classificacao);
                        if(ultimaClass.ToString().Length > 2)
                            ultimaClass = double.Parse(plano.Classificacao.Substring(plano.Classificacao.Length -2,2));

                        ultimaClass = ultimaClass + 1;
                        tipoConta = plano.TipoConta;
                        txtNovaClass.Texts = plano.Classificacao.Substring(plano.Classificacao.Length - 2) + ultimaClass.ToString().PadLeft(2,'0');
                    }
                }
                else
                {
                    listaPlano = planoContaDAO.selecionarPlanoContaPeloIdPai(txtNivelAcima.Texts, "");
                    if (listaPlano.Count > 0)
                    {
                        foreach(PlanoConta plan in listaPlano)
                        {
                            decimal ultimaClass = decimal.Parse(plan.Classificacao);
                            if (ultimaClass.ToString().Length > 2)
                                ultimaClass = decimal.Parse(plan.Classificacao.Substring(plan.Classificacao.Length - 2, 2));
                            tipoConta = plan.TipoConta;
                            ultimaClass = ultimaClass + 1;
                            txtNovaClass.Texts = plan.IdPai +"."+ ultimaClass.ToString().PadLeft(2, '0');
                        }
                    }
                    else
                        txtNovaClass.Texts = txtNivelAcima.Texts + ".01";
                }
            }
            else
                GenericaDesktop.ShowAlerta("Cliente no Plano de Contas que deseja inserir o novo dentro");
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtDescricao.Texts) && !String.IsNullOrEmpty(txtNovaClass.Texts))
            {
                planoConta = new PlanoConta();
                if (!String.IsNullOrEmpty(txtCodPlanoConta.Texts))
                    planoConta.Id = int.Parse(txtCodPlanoConta.Texts);
                else
                    planoConta.Id = 0;
                planoConta.Classificacao = txtNovaClass.Texts;
                planoConta.Descricao = txtDescricao.Texts;
                planoConta.IdPai = txtIdPai.Texts;
                planoConta.IdAcima = txtNivelAcima.Texts;
                planoConta.Tipo = "VARIAVEL";
                if (radioFixo.Checked == true)
                    planoConta.Tipo = "FIXO";
                planoConta.TipoConta = tipoConta;
                Controller.getInstance().salvar(planoConta);
                selecionaTodosPlanos();
                GenericaDesktop.ShowInfo("Registrado com sucesso");
                txtIdPai.Texts = "";
                txtNivelAcima.Texts = "";
                txtNovaClass.Texts = "";
                txtCodPlanoConta.Texts = "";
                txtDescricao.Texts = "";
            }
        }

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            editar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            editar();
        }

        private void editar()
        {
            if (grid.SelectedIndex >= 0)
            {
                planoConta = new PlanoConta();
                planoConta = (PlanoConta)grid.SelectedItem;
                txtIdPai.Texts = planoConta.IdPai;
                txtNivelAcima.Texts = planoConta.IdAcima;
                txtNovaClass.Texts = planoConta.Classificacao;
                txtCodPlanoConta.Texts = planoConta.Id.ToString();
                txtDescricao.Texts = planoConta.Descricao;
                tipoConta = planoConta.TipoConta;
                txtDescricao.Focus();
            }
        }

        private void FrmPlanoConta_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F2:
                    btnNovo.PerformClick();
                    break;
                case Keys.F5:
                    btnGravar.PerformClick();
                    break;
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
          
            
        }

        private void btnExcluirPlano_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                if (GenericaDesktop.ShowConfirmacao("Deseja realmente excluir este registro?"))
                {
                    planoConta = new PlanoConta();
                    planoConta = (PlanoConta)grid.SelectedItem;
                    Controller.getInstance().excluir(planoConta);
                    GenericaDesktop.ShowInfo("Registro excluído com sucesso");
                    selecionaTodosPlanos();
                }
            }
        }
    }
}
