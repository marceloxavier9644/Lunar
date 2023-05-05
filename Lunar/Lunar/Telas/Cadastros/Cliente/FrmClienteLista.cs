using FiscalBr.Common.Sintegra;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Cliente
{
    public partial class FrmClienteLista : Form
    {
        private IList<Pessoa> listaClientes;
        PessoaController pessoaController = new PessoaController();
        Pessoa pessoa = new Pessoa();
        public FrmClienteLista()
        {
            InitializeComponent();
            this.Opacity = 0.0;
        }

        private void carregarLista()
        {
            txtPesquisaCliente.Texts = "";
            listaClientes = pessoaController.selecionarTodasPessoasPaginando(0,50,"");
            //ajustarLista();        
            //gridClient.DataSource = listaClientes;
            paginacao.DataSource = new List<int>();
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                paginacao.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                paginacao.PageSize = 50;
            paginacao.AllowOnDemandPaging = true;

            Int64 totalPessoas = pessoaController.totalTodasPessoasPaginando("");
            int totalPaginas = (int)Math.Ceiling(Double.Parse((totalPessoas / paginacao.PageSize).ToString()));
            if (totalPaginas < 1)
            {
                totalPaginas = 1;
            }

            paginacao.PageCount = totalPaginas;

            gridClient.DataSource = listaClientes;
            //paginacao.OnDemandLoading += sfDataPager1_OnDemandLoading;
            txtPesquisaCliente.Focus();
        }
        private void ajustarLista()
        {
            dsCliente.Tables[0].Clear();
            foreach (Pessoa pessoa in listaClientes)
            {
                DataRow row = dsCliente.Tables[0].NewRow();
                row.SetField("Codigo", pessoa.Id);
                row.SetField("Nome", pessoa.RazaoSocial);
                row.SetField("Apelido", pessoa.NomeFantasia);
                row.SetField("CNPJ", pessoa.Cnpj);

                string logradouro = "";
                string numero = "";
                if (pessoa.EnderecoPrincipal != null)
                {
                    logradouro = pessoa.EnderecoPrincipal.Logradouro;
                    numero = pessoa.EnderecoPrincipal.Numero;
                }
                row.SetField("Endereco", logradouro);
                row.SetField("Numero", numero);

                string cidade = "";
                if (pessoa.EnderecoPrincipal.Cidade != null)
                    cidade = pessoa.EnderecoPrincipal.Cidade.Descricao;
                row.SetField("Cidade", cidade);

                string bairro = "";
                if (!String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Bairro))
                    bairro = pessoa.EnderecoPrincipal.Bairro;
                row.SetField("Bairro", bairro);
                
                string telefone = "";
                if (pessoa.PessoaTelefone != null)
                {
                    telefone = pessoa.PessoaTelefone.Ddd;
                    telefone = telefone + pessoa.PessoaTelefone.Telefone;
                }
                row.SetField("Telefone", GenericaDesktop.MascaraTelefone(telefone));
              

                dsCliente.Tables[0].Rows.Add(row);
            }
        }

        private void FrmClienteLista_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void PesquisarCliente(string valor, int paginaAtual)
        {
            //paginacao.DataSource = listaClientes;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                paginacao.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                paginacao.PageSize = 50;
            
            Int64 totalPessoas = pessoaController.totalTodasPessoasPaginando(valor);
            double totalPaginas = (double)totalPessoas / paginacao.PageSize;
            if (totalPaginas < 1)
            {
                totalPaginas = 1;
            }

            paginacao.PageCount = (int)Math.Ceiling(totalPaginas);
            paginacao.Refresh();
                

            listaClientes = pessoaController.selecionarTodasPessoasPaginando(paginaAtual * paginacao.PageSize, paginacao.PageSize, valor);
            gridClient.DataSource = listaClientes;

            if (listaClientes.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Nenhum registro encontrado!");
                txtPesquisaCliente.Texts = "";
                txtPesquisaCliente.PlaceholderText = "";
                txtPesquisaCliente.Select();
            }
        }

        private void txtPesquisaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisarCliente(txtPesquisaCliente.Texts.Trim(),0);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            abrirNovoCadastro();
        }

        private void abrirNovoCadastro()
        {
            Form formBackground = new Form();
            try
            {
                using (FrmClienteCadastro uu = new FrmClienteCadastro())
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    //formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    uu.ShowDialog();
                    formBackground.Dispose();
                    carregarLista();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
            //FrmClienteCadastro frm = new FrmClienteCadastro();
            //frm.ShowDialog();
        }

        private void gridClientes_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            paginacao.LoadDynamicData(e.StartRowIndex, listaClientes.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void gridClient_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            selecionarClienteParaEditar();
        }

        private void editarCadastro(Pessoa pessoa)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmClienteCadastro uu = new FrmClienteCadastro(pessoa))
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    //formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    uu.ShowDialog();
                    formBackground.Dispose();
                    carregarLista();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void selecionarClienteParaEditar()
        {
            if (gridClient.SelectedIndex >= 0)
            {
                pessoa = new Pessoa();
                pessoa = (Pessoa)gridClient.SelectedItem;
                editarCadastro(pessoa);
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do cliente que deseja editar!");
        }

        private void gridClient_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            selecionarClienteParaEditar();
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            var options = new PdfExportingOptions();
            var document = new Syncfusion.Pdf.PdfDocument();
            document.PageSettings.Orientation = Syncfusion.Pdf.PdfPageOrientation.Landscape;
            var page = document.Pages.Add();
            var PDFGrid = gridClient.ExportToPdfGrid(gridClient.View, options);
            var format = new PdfGridLayoutFormat()
            {
                Layout = PdfLayoutType.Paginate,
                Break = PdfLayoutBreakType.FitPage
            };
            //Largura da coluna
            foreach (PdfGridCell headerCell in PDFGrid.Headers[0].Cells)
            {
                if (headerCell.Value.ToString() == gridClient.Columns[5].HeaderText)
                {
                    var index = PDFGrid.Headers[0].Cells.IndexOf(headerCell);
                    PDFGrid.Columns[index].Width = 50;
                }
            }

            PDFGrid.Draw(page, new PointF(), format);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "ListaClientes",
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
            var excelEngine = gridClient.ExportToExcel(gridClient.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];

            SaveFileDialog saveFilterDialog = new SaveFileDialog
            {
                FileName = "ListaClientes",
                FilterIndex =3,
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
                if (MessageBox.Show(this.gridClient, "Deseja abrir o arquivo no Excel?", "Arquivo criado com sucesso",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                    //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                    System.Diagnostics.Process.Start(saveFilterDialog.FileName);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) 
                this.Opacity += 0.05;          
        }

        private void FrmClienteLista_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Gray), 0, 0, this.Width - 1, this.Height - 1);
            carregarLista();
        }

        private void paginacao_PageIndexChanged(object sender, Syncfusion.WinForms.DataPager.Events.PageIndexChangedEventArgs e)
        {
            PesquisarCliente(txtPesquisaCliente.Texts.Trim(), e.NewPageIndex);
        }

        private void txtRegistroPorPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisarCliente(txtPesquisaCliente.Texts.Trim(), 0);
            }
        }
    }
}
