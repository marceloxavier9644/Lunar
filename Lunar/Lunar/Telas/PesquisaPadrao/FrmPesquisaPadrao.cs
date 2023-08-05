using Lunar.Utils;
using LunarBase.Anotations;
using LunarBase.ConexaoBD;
using LunarBase.ControllerBO;
using NHibernate;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.PesquisaPadrao
{
    public partial class FrmPesquisaPadrao : Form
    {
        String Sql = "";
        String SqlInicial = "";
        String SqlAdicional = "";
        private String TabelaPesquisa = "";
        private IList<object> Resultado = null;

        public DialogResult showModal(string Tabela, string sqlAdicional, ref Object objeto)
        {
            TabelaPesquisa = Tabela;
            DialogResult = ShowDialog();
            try
            {
                objeto = gridPesquisa.SelectedItem;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return DialogResult;
        }

        public FrmPesquisaPadrao(string Tabela, string sqlAdicional)
        {
            InitializeComponent();
            Sql = "FROM " + Tabela + " as Tabela WHERE Tabela.FlagExcluido <> true ";
            SqlInicial = Sql;
            this.SqlAdicional = sqlAdicional;
            //if(Tabela != "Pessoa") /*&& Tabela != "Produto"*/
            pesquisar(Tabela, Sql);
            txtPesquisa.Select();
            //txtPesquisa.Select();
        }

        private void pesquisar(string Tabela, string Sql)
        {
            Controller.getInstance();
            String tela = Type.GetType("LunarBase.Classes." + Tabela + ",LunarBase").GetCustomAttributes(typeof(Anotacao), false)[0].ToString();
            object obj = Type.GetType("LunarBase.Classes." + Tabela + ",LunarBase");
            
            ISession Sessao = Conexao.GetSession();
            TabelaPesquisa = Tabela;
            Resultado = Sessao.CreateQuery(Sql + SqlAdicional).List<object>();
            //this.SqlAdicional = "";
            if (Resultado.Count <= 0)
            {
                return;
            }
            sfDataPager1.DataSource = Resultado;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            gridPesquisa.DataSource = sfDataPager1.PagedSource;
            sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;

            try
            {
                gridPesquisa.Columns["FlagExcluido"].Visible = false;
                gridPesquisa.Columns["DataCadastro"].Visible = false;
                gridPesquisa.Columns["DataAlteracao"].Visible = false;
                gridPesquisa.Columns["DataExclusao"].Visible = false;
                gridPesquisa.Columns["OperadorCadastro"].Visible = false;
                gridPesquisa.Columns["OperadorAlteracao"].Visible = false;
                gridPesquisa.Columns["OperadorExclusao"].Visible = false;

                for (int i = 0; i < gridPesquisa.Columns.Count; i++)
                {
                    if (Type.GetType("LunarBase.Classes." + Tabela + ",LunarBase").GetProperty(gridPesquisa.Columns[i].MappingName).GetCustomAttributes(typeof(OcultarEmGridsEPesquisas), false).Length <= 0)
                    {
                        gridPesquisa.Columns[i].HeaderText = Type.GetType("LunarBase.Classes." + Tabela + ",LunarBase").GetProperty(gridPesquisa.Columns[i].MappingName).GetCustomAttributes(typeof(Anotacao), false)[0].ToString();
                        if (String.IsNullOrEmpty(gridPesquisa.Columns[i].HeaderText) || gridPesquisa.Columns[i].HeaderText.Equals("Descrição") || gridPesquisa.Columns[i].HeaderText.Equals("Descricao"))
                            gridPesquisa.Columns[i].HeaderText = gridPesquisa.Columns[i].MappingName;

                    }
                    else
                    {
                        gridPesquisa.Columns[i].Visible = false;
                    }
                }
                txtPesquisa.Focus();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            Conexao.FechaConexaoBD();

            for (int I = 0; I < gridPesquisa.Columns.Count; I++)
            {
                if (gridPesquisa.Columns[I].MappingName != "DataCadastro"
                    && gridPesquisa.Columns[I].MappingName != "DataAlteracao"
                    && gridPesquisa.Columns[I].MappingName != "DataExclusao"
                    && gridPesquisa.Columns[I].MappingName != "OperadorCadastro"
                    && gridPesquisa.Columns[I].MappingName != "OperadorAlteracao"
                    && gridPesquisa.Columns[I].MappingName != "OperadorExclusao"
                    && gridPesquisa.Columns[I].MappingName != "FlagExcluido")
                {
                    if (Type.GetType("LunarBase.Classes." + Tabela + ",LunarBase").GetProperty(gridPesquisa.Columns[I].MappingName).GetCustomAttributes(typeof(OcultarEmGridsEPesquisas), false).Length <= 0)
                    {
                        String property = "";
                        if (Type.GetType("LunarBase.Classes." + TabelaPesquisa + ",LunarBase").GetProperty(gridPesquisa.Columns[I].MappingName).GetCustomAttributes(typeof(FK), false).Length > 0)
                        {
                            property = Type.GetType("LunarBase.Classes." + TabelaPesquisa + ",LunarBase").GetProperty(gridPesquisa.Columns[I].MappingName).GetCustomAttributes(typeof(FK), false)[0].ToString();
                        }
                    }
                }
                if (Resultado.Count >= 1)
                {
                    //var record = this.gridPesquisa.View.Records[0];
                    //var column = this.gridPesquisa.Columns[0];
                    //this.gridPesquisa.SelectCell(record, column);
                    this.gridPesquisa.TableControl.Select();
                    this.gridPesquisa.MoveToCurrentCell(new Syncfusion.WinForms.GridCommon.ScrollAxis.RowColumnIndex(1, 0));
                }
                else
                {
                    txtPesquisa.Focus();
                }
            }
        }

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, Resultado.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void FrmPesquisaPadrao_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            lblTelaPesquisa.Text = "Pesquisa na Tabela " + TabelaPesquisa;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            selecionarObjeto();
        }
        private void gridPesquisa_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            selecionarObjeto();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void selecionarObjeto()
        {
            if (gridPesquisa.SelectedIndex >= 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do item que deseja selecionar!");
        }

        private void FrmPesquisaPadrao_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //case Keys.Enter:
                //    btnSelecionar.PerformClick();
                //    break;
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F2:
                    this.DialogResult = DialogResult.Ignore;
                    break;
                case Keys.F5:
                    btnSelecionar.PerformClick();
                    break;
            }
        }

        private void txtPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Limpa o SQL Adicional
                //SqlAdicional = "";
                Pesquisar(txtPesquisa.Text.Trim());
            }
        }

        private void Pesquisar(String valor)
        {
            Controller.getInstance();

            String colunas = "";
            for (int i = 0; i < gridPesquisa.Columns.Count; i++)
            {
                if (gridPesquisa.Columns[i].MappingName != "FlagExcluido" && gridPesquisa.Columns[i].MappingName != "OperadorCadastro" 
                    && gridPesquisa.Columns[i].MappingName != "OperadorExclusao" && gridPesquisa.Columns[i].MappingName != "OperadorAlteracao" 
                    && gridPesquisa.Columns[i].MappingName != "DataCadastro" && gridPesquisa.Columns[i].MappingName != "DataExclusao" 
                    && gridPesquisa.Columns[i].MappingName != "DataAlteracao")
                {
                    string col = "";
                    if (Type.GetType("LunarBase.Classes." + TabelaPesquisa + ",LunarBase").GetProperty(gridPesquisa.Columns[i].MappingName).GetCustomAttributes(typeof(OcultarEmGridsEPesquisas), false).Length <= 0)
                    {
                        col = Type.GetType("LunarBase.Classes." + TabelaPesquisa + ",LunarBase").GetProperty(gridPesquisa.Columns[i].MappingName).GetCustomAttributes(typeof(Anotacao), false)[0].ToString();
                    }
                    if (col.Contains("Dh") || col.Contains("Data"))
                        col = "";

                    if (!String.IsNullOrEmpty(col))
                        colunas = colunas + ", ' ',Tabela." + gridPesquisa.Columns[i].MappingName;
                    else if(String.IsNullOrEmpty(col) && TabelaPesquisa != "Nfe")
                        colunas = colunas + ", ' ',Tabela." + gridPesquisa.Columns[i].MappingName;
                }
            }
            colunas = "CONCAT(" + colunas + ") like '%" + valor + "%'";
            if(TabelaPesquisa.Equals("Produto"))
                colunas = "CONCAT(Tabela.Id, ' ',Tabela.Descricao, ' ',Tabela.Ean, ' ',Tabela.ValorVenda, ' ', Tabela.Ncm, ' ', Tabela.Referencia) like '%" + valor + "%'";

            //CONCAT(, ' ',Tabela.Id, ' ',Tabela.Descricao, ' ',Tabela.Ean, ' ',Tabela.ValorVenda
            string val = colunas.Substring(7, 5);
            if (val.Equals(", ' '"))
                colunas = colunas.Remove(7, 6);
                Sql = "FROM " + TabelaPesquisa + " Tabela WHERE " + colunas + " AND Tabela.FlagExcluido <> true ";
            if (string.IsNullOrEmpty(colunas) || colunas.Substring(0, 11).Contains("CONCAT()"))
                Sql = "FROM " + TabelaPesquisa + " Tabela WHERE Tabela.FlagExcluido <> true ";

            ISession Sessao = Conexao.GetSession();
            Resultado = Sessao.CreateQuery(Sql + SqlAdicional).List<object>();

            if (Resultado.Count <= 0)
            {
                GenericaDesktop.ShowAlerta("Nenhum registro encontrado");
                txtPesquisa.Text = "";
                txtPesquisa.Select();
                return;
            }
            else
            {
                txtPesquisa.Text = "";
                gridPesquisa.SelectedIndex = 0;
            }

            sfDataPager1.DataSource = Resultado;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            gridPesquisa.DataSource = sfDataPager1.PagedSource;
            sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;

            try
            {
                gridPesquisa.Columns["FlagExcluido"].Visible = false;
                gridPesquisa.Columns["DataCadastro"].Visible = false;
                gridPesquisa.Columns["DataAlteracao"].Visible = false;
                gridPesquisa.Columns["DataExclusao"].Visible = false;
                gridPesquisa.Columns["OperadorCadastro"].Visible = false;
                gridPesquisa.Columns["OperadorAlteracao"].Visible = false;
                gridPesquisa.Columns["OperadorExclusao"].Visible = false;

                for (int i = 0; i < gridPesquisa.Columns.Count; i++)
                {
                    if (Type.GetType("LunarBase.Classes." + TabelaPesquisa + ",LunarBase").GetProperty(gridPesquisa.Columns[i].MappingName).GetCustomAttributes(typeof(OcultarEmGridsEPesquisas), false).Length <= 0)
                    {
                        gridPesquisa.Columns[i].HeaderText = Type.GetType("LunarBase.Classes." + TabelaPesquisa + ",LunarBase").GetProperty(gridPesquisa.Columns[i].MappingName).GetCustomAttributes(typeof(Anotacao), false)[0].ToString();
                        if (String.IsNullOrEmpty(gridPesquisa.Columns[i].HeaderText))
                            gridPesquisa.Columns[i].HeaderText = gridPesquisa.Columns[i].MappingName;
                    }
                    else
                    {
                        gridPesquisa.Columns[i].Visible = false;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            Conexao.FechaConexaoBD();

            for (int I = 0; I < gridPesquisa.Columns.Count; I++)
            {
                if (gridPesquisa.Columns[I].MappingName != "DataCadastro"
                    && gridPesquisa.Columns[I].MappingName != "DataAlteracao"
                    && gridPesquisa.Columns[I].MappingName != "DataExclusao"
                    && gridPesquisa.Columns[I].MappingName != "OperadorCadastro"
                    && gridPesquisa.Columns[I].MappingName != "OperadorAlteracao"
                    && gridPesquisa.Columns[I].MappingName != "OperadorExclusao"
                    && gridPesquisa.Columns[I].MappingName != "FlagExcluido")
                {
                    if (Type.GetType("LunarBase.Classes." + TabelaPesquisa + ",LunarBase").GetProperty(gridPesquisa.Columns[I].MappingName).GetCustomAttributes(typeof(OcultarEmGridsEPesquisas), false).Length <= 0)
                    {
                        String property = "";
                        if (Type.GetType("LunarBase.Classes." + TabelaPesquisa + ",LunarBase").GetProperty(gridPesquisa.Columns[I].MappingName).GetCustomAttributes(typeof(FK), false).Length > 0)
                        {
                            property = Type.GetType("LunarBase.Classes." + TabelaPesquisa + ",LunarBase").GetProperty(gridPesquisa.Columns[I].MappingName).GetCustomAttributes(typeof(FK), false)[0].ToString();
                        }
                    }
                }
                if (Resultado.Count >= 1)
                {
                    //var record = this.gridPesquisa.View.Records[0];
                    //var column = this.gridPesquisa.Columns[0];
                    //this.gridPesquisa.SelectCell(record, column);
                    this.gridPesquisa.TableControl.Select();
                    this.gridPesquisa.MoveToCurrentCell(new Syncfusion.WinForms.GridCommon.ScrollAxis.RowColumnIndex(1, 0));
                }
                else
                {
                    txtPesquisa.Focus();
                }
            }
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            var options = new PdfExportingOptions();
            var document = new Syncfusion.Pdf.PdfDocument();
            document.PageSettings.Orientation = Syncfusion.Pdf.PdfPageOrientation.Landscape;
            var page = document.Pages.Add();
            var PDFGrid = gridPesquisa.ExportToPdfGrid(gridPesquisa.View, options);
            var format = new PdfGridLayoutFormat()
            {
                Layout = PdfLayoutType.Paginate,
                Break = PdfLayoutBreakType.FitPage
            };
            //Largura da coluna
            foreach (PdfGridCell headerCell in PDFGrid.Headers[0].Cells)
            {
                if (headerCell.Value.ToString() == gridPesquisa.Columns[5].HeaderText)
                {
                    var index = PDFGrid.Headers[0].Cells.IndexOf(headerCell);
                    PDFGrid.Columns[index].Width = 50;
                }
            }

            PDFGrid.Draw(page, new PointF(), format);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "Lista" + TabelaPesquisa,
                Filter = "PDF Files(*.pdf)|*.pdf"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = saveFileDialog.OpenFile())
                {
                    document.Save(stream);
                }
                //Message box confirmation to view the created Pdf file.
                if (MessageBox.Show("Deseja abrir o arquivo Pdf?", "Pdf criado com sucesso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //Launching the Pdf file using the default Application.
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            var excelEngine = gridPesquisa.ExportToExcel(gridPesquisa.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];

            SaveFileDialog saveFilterDialog = new SaveFileDialog
            {
                FileName = "Lista" + TabelaPesquisa,
                FilterIndex = 3,
                Filter = "Excel 97 a 2003 Files(*.xls)|*.xls|Excel 2007 a 2010 Files(*.xlsx)|*.xlsx|Excel 2013 a 2022 Files(*.xlsx)|*.xlsx"
            };

            if (saveFilterDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (Stream stream = saveFilterDialog.OpenFile())
                {

                    if (saveFilterDialog.FilterIndex == 1)
                        workBook.Version = ExcelVersion.Excel97to2003;
                    else if (saveFilterDialog.FilterIndex == 2)
                        workBook.Version = ExcelVersion.Excel2010;
                    else
                        workBook.Version = ExcelVersion.Excel2013;
                    workBook.SaveAs(stream);
                }

                //Message box confirmation to view the created workbook.
                if (MessageBox.Show(this.gridPesquisa, "Deseja abrir o arquivo no Excel?", "Arquivo criado com sucesso",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                    //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                    System.Diagnostics.Process.Start(saveFilterDialog.FileName);
                }
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
        }

        private void gridPesquisa_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        //private void gridPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == 13)
        //    {
        //        if(gridPesquisa.SelectedIndex >= 0)
        //            btnSelecionar.PerformClick();
        //    }
        //}

        private void gridPesquisa_CurrentCellKeyDown(object sender, Syncfusion.WinForms.DataGrid.Events.CurrentCellKeyEventArgs e)
        {
           if (e.KeyEventArgs.KeyCode == Keys.Enter )  
              {
                if (this.gridPesquisa.SelectedItem != null)
                {
                    this.DialogResult = DialogResult.OK;
                }
                e.KeyEventArgs.SuppressKeyPress = true;
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            SqlAdicional = "";
            txtPesquisa.Text = "";
            pesquisar(TabelaPesquisa, SqlInicial);
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar(txtPesquisa.Text.Trim());
        }

        private void txtPesquisa_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Pesquisar(txtPesquisa.Text.Trim());
            }
        }
    }
}
