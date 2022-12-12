namespace Lunar.Telas.Cadastros.Cidades
{
    partial class FrmPesquisaCidade
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn2 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn3 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn4 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gridCidade = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.sfDataPager1 = new Syncfusion.WinForms.DataPager.SfDataPager();
            this.dsCidade = new System.Data.DataSet();
            this.Cidade = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExportarPDF = new FontAwesome.Sharp.IconButton();
            this.btnExportarExcel = new FontAwesome.Sharp.IconButton();
            this.btnNovo = new Lunar.RJ_UI.Classes.RJButton();
            this.btnEditar = new Lunar.RJ_UI.Classes.RJButton();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.btnFechar = new FontAwesome.Sharp.IconButton();
            this.folderBrowser1 = new Syncfusion.Windows.Forms.FolderBrowser(this.components);
            this.autoLabel15 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.txtPesquisa = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtRegistroPorPagina = new Lunar.RJ_UI.Classes.RJTextBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cidade)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gridCidade);
            this.groupBox3.Location = new System.Drawing.Point(0, 103);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(868, 429);
            this.groupBox3.TabIndex = 160;
            this.groupBox3.TabStop = false;
            // 
            // gridCidade
            // 
            this.gridCidade.AccessibleName = "Table";
            this.gridCidade.AllowEditing = false;
            this.gridCidade.AllowFiltering = true;
            this.gridCidade.AllowStandardTab = true;
            this.gridCidade.AutoGenerateColumns = false;
            this.gridCidade.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridNumericColumn2.AllowEditing = false;
            gridNumericColumn2.AllowFiltering = true;
            gridNumericColumn2.CellStyle.Font.Facename = "Montserrat";
            gridNumericColumn2.CellStyle.Font.Size = 14F;
            gridNumericColumn2.Format = "0";
            gridNumericColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn2.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn2.HeaderStyle.Font.Size = 14F;
            gridNumericColumn2.HeaderText = "Código";
            gridNumericColumn2.MappingName = "Id";
            gridNumericColumn2.Visible = false;
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.CellStyle.Font.Facename = "Montserrat";
            gridTextColumn3.CellStyle.Font.Size = 14F;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn3.HeaderStyle.Font.Size = 14F;
            gridTextColumn3.HeaderText = "Cidade";
            gridTextColumn3.MappingName = "Descricao";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.CellStyle.Font.Facename = "Montserrat";
            gridTextColumn4.CellStyle.Font.Size = 14F;
            gridTextColumn4.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn4.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn4.HeaderStyle.Font.Size = 14F;
            gridTextColumn4.HeaderText = "Estado";
            gridTextColumn4.MappingName = "Estado";
            this.gridCidade.Columns.Add(gridNumericColumn2);
            this.gridCidade.Columns.Add(gridTextColumn3);
            this.gridCidade.Columns.Add(gridTextColumn4);
            this.gridCidade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCidade.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridCidade.Location = new System.Drawing.Point(3, 16);
            this.gridCidade.Name = "gridCidade";
            this.gridCidade.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.gridCidade.Size = new System.Drawing.Size(862, 410);
            this.gridCidade.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.gridCidade.Style.CellStyle.Font.Facename = "Montserrat";
            this.gridCidade.Style.CellStyle.Font.Size = 14F;
            this.gridCidade.TabIndex = 0;
            this.gridCidade.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.gridCidade_CellDoubleClick);
            this.gridCidade.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridCidade_KeyDown);
            this.gridCidade.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridCidade_MouseDoubleClick);
            // 
            // sfDataPager1
            // 
            this.sfDataPager1.AccessibleName = "DataPager";
            this.sfDataPager1.BackColor = System.Drawing.Color.White;
            this.sfDataPager1.CanOverrideStyle = true;
            this.sfDataPager1.Location = new System.Drawing.Point(462, 24);
            this.sfDataPager1.Name = "sfDataPager1";
            this.sfDataPager1.Size = new System.Drawing.Size(395, 36);
            this.sfDataPager1.TabIndex = 1;
            this.sfDataPager1.OnDemandLoading += new System.EventHandler<Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs>(this.sfDataPager1_OnDemandLoading);
            // 
            // dsCidade
            // 
            this.dsCidade.DataSetName = "dsCidade";
            this.dsCidade.Tables.AddRange(new System.Data.DataTable[] {
            this.Cidade});
            // 
            // Cidade
            // 
            this.Cidade.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2});
            this.Cidade.TableName = "Cidade";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "ID";
            this.dataColumn1.DataType = typeof(int);
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "Cidade";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExportarPDF);
            this.groupBox2.Controls.Add(this.btnExportarExcel);
            this.groupBox2.Controls.Add(this.btnNovo);
            this.groupBox2.Controls.Add(this.btnEditar);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 531);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(868, 69);
            this.groupBox2.TabIndex = 159;
            this.groupBox2.TabStop = false;
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarPDF.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExportarPDF.FlatAppearance.BorderSize = 0;
            this.btnExportarPDF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExportarPDF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExportarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarPDF.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            this.btnExportarPDF.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExportarPDF.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnExportarPDF.IconSize = 38;
            this.btnExportarPDF.Location = new System.Drawing.Point(45, 23);
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(36, 34);
            this.btnExportarPDF.TabIndex = 215;
            this.btnExportarPDF.UseVisualStyleBackColor = true;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExportarExcel.FlatAppearance.BorderSize = 0;
            this.btnExportarExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExportarExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarExcel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnExportarExcel.IconColor = System.Drawing.Color.DarkGreen;
            this.btnExportarExcel.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnExportarExcel.IconSize = 38;
            this.btnExportarExcel.Location = new System.Drawing.Point(3, 23);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(36, 34);
            this.btnExportarExcel.TabIndex = 214;
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnNovo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnNovo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnNovo.BorderRadius = 8;
            this.btnNovo.BorderSize = 0;
            this.btnNovo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovo.FlatAppearance.BorderSize = 0;
            this.btnNovo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovo.ForeColor = System.Drawing.Color.White;
            this.btnNovo.Location = new System.Drawing.Point(564, 12);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(293, 45);
            this.btnNovo.TabIndex = 154;
            this.btnNovo.Text = "Selecionar [F5]";
            this.btnNovo.TextColor = System.Drawing.Color.White;
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.BackColor = System.Drawing.Color.White;
            this.btnEditar.BackgroundColor = System.Drawing.Color.White;
            this.btnEditar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnEditar.BorderRadius = 8;
            this.btnEditar.BorderSize = 2;
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnEditar.FlatAppearance.BorderSize = 2;
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnEditar.Location = new System.Drawing.Point(265, 12);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(293, 45);
            this.btnEditar.TabIndex = 155;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.btnFechar);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(868, 28);
            this.panelTitleBar.TabIndex = 155;
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.BackColor = System.Drawing.Color.Transparent;
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnFechar.IconColor = System.Drawing.Color.White;
            this.btnFechar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFechar.IconSize = 30;
            this.btnFechar.Location = new System.Drawing.Point(831, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(35, 25);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // autoLabel15
            // 
            this.autoLabel15.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel15.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel15.ForeColor = System.Drawing.Color.Black;
            this.autoLabel15.Location = new System.Drawing.Point(355, 9);
            this.autoLabel15.Name = "autoLabel15";
            this.autoLabel15.Size = new System.Drawing.Size(94, 18);
            this.autoLabel15.TabIndex = 208;
            this.autoLabel15.Text = "Reg. por Pág.";
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(19, 8);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(67, 18);
            this.autoLabel1.TabIndex = 209;
            this.autoLabel1.Text = "Pesquisa";
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.autoLabel1);
            this.metroPanel1.Controls.Add(this.autoLabel15);
            this.metroPanel1.Controls.Add(this.sfDataPager1);
            this.metroPanel1.Controls.Add(this.txtPesquisa);
            this.metroPanel1.Controls.Add(this.txtRegistroPorPagina);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 28);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(868, 69);
            this.metroPanel1.TabIndex = 210;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.BackColor = System.Drawing.Color.White;
            this.txtPesquisa.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPesquisa.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPesquisa.BorderRadius = 8;
            this.txtPesquisa.BorderSize = 2;
            this.txtPesquisa.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPesquisa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPesquisa.Location = new System.Drawing.Point(10, 21);
            this.txtPesquisa.Margin = new System.Windows.Forms.Padding(4);
            this.txtPesquisa.Multiline = false;
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtPesquisa.PasswordChar = false;
            this.txtPesquisa.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtPesquisa.PlaceholderText = "Pesquise por Cidade";
            this.txtPesquisa.Size = new System.Drawing.Size(329, 39);
            this.txtPesquisa.TabIndex = 153;
            this.txtPesquisa.Texts = "";
            this.txtPesquisa.UnderlinedStyle = false;
            this.txtPesquisa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPesquisa_KeyPress);
            // 
            // txtRegistroPorPagina
            // 
            this.txtRegistroPorPagina.BackColor = System.Drawing.Color.White;
            this.txtRegistroPorPagina.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtRegistroPorPagina.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtRegistroPorPagina.BorderRadius = 8;
            this.txtRegistroPorPagina.BorderSize = 2;
            this.txtRegistroPorPagina.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtRegistroPorPagina.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegistroPorPagina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtRegistroPorPagina.Location = new System.Drawing.Point(347, 21);
            this.txtRegistroPorPagina.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegistroPorPagina.Multiline = false;
            this.txtRegistroPorPagina.Name = "txtRegistroPorPagina";
            this.txtRegistroPorPagina.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtRegistroPorPagina.PasswordChar = false;
            this.txtRegistroPorPagina.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtRegistroPorPagina.PlaceholderText = "";
            this.txtRegistroPorPagina.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRegistroPorPagina.Size = new System.Drawing.Size(108, 39);
            this.txtRegistroPorPagina.TabIndex = 207;
            this.txtRegistroPorPagina.Tag = "";
            this.txtRegistroPorPagina.Texts = "100";
            this.txtRegistroPorPagina.UnderlinedStyle = false;
            this.txtRegistroPorPagina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegistroPorPagina_KeyPress);
            // 
            // FrmPesquisaCidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(868, 600);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPesquisaCidade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisa Cidade";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmPesquisaCidade_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cidade)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panelTitleBar.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private RJ_UI.Classes.RJTextBox txtPesquisa;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private RJ_UI.Classes.RJButton btnNovo;
        private RJ_UI.Classes.RJButton btnEditar;
        private System.Data.DataSet dsCidade;
        private System.Data.DataTable Cidade;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private Syncfusion.WinForms.DataPager.SfDataPager sfDataPager1;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridCidade;
        private System.Windows.Forms.Panel panelTitleBar;
        private FontAwesome.Sharp.IconButton btnFechar;
        private FontAwesome.Sharp.IconButton btnExportarPDF;
        private FontAwesome.Sharp.IconButton btnExportarExcel;
        private Syncfusion.Windows.Forms.FolderBrowser folderBrowser1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel15;
        private RJ_UI.Classes.RJTextBox txtRegistroPorPagina;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private MetroFramework.Controls.MetroPanel metroPanel1;
    }
}