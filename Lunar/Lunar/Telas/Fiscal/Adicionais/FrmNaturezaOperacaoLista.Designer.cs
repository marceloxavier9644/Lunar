namespace Lunar.Telas.Fiscal.Adicionais
{
    partial class FrmNaturezaOperacaoLista
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
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn1 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn2 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn3 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn4 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridCheckBoxColumn gridCheckBoxColumn1 = new Syncfusion.WinForms.DataGrid.GridCheckBoxColumn();
            Syncfusion.WinForms.DataGrid.GridCheckBoxColumn gridCheckBoxColumn2 = new Syncfusion.WinForms.DataGrid.GridCheckBoxColumn();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel15 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.sfDataPager1 = new Syncfusion.WinForms.DataPager.SfDataPager();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPesquisaCliente = new FontAwesome.Sharp.IconButton();
            this.txtRegistroPorPagina = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtNaturezaOperacao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnExportarExcel = new FontAwesome.Sharp.IconButton();
            this.btnExportarPDF = new FontAwesome.Sharp.IconButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnNovo = new Lunar.RJ_UI.Classes.RJButton();
            this.btnEditar = new Lunar.RJ_UI.Classes.RJButton();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1039, 44);
            this.panelTitleBar.TabIndex = 251;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClose.IconSize = 30;
            this.btnClose.Location = new System.Drawing.Point(998, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // grid
            // 
            this.grid.AccessibleName = "Table";
            this.grid.AllowEditing = false;
            this.grid.AllowFiltering = true;
            this.grid.AllowResizingColumns = true;
            this.grid.AllowResizingHiddenColumns = true;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoGenerateColumns = false;
            this.grid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowFiltering = true;
            gridTextColumn1.AllowResizing = true;
            gridTextColumn1.CellStyle.Font.Size = 12F;
            gridTextColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            gridTextColumn1.HeaderText = "Código";
            gridTextColumn1.MappingName = "Id";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.HeaderText = "Descrição";
            gridTextColumn2.MappingName = "Descricao";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.AllowResizing = true;
            gridTextColumn3.CellStyle.Font.Size = 12F;
            gridTextColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn3.HeaderText = "E / S";
            gridTextColumn3.MappingName = "EntradaSaida";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.AllowResizing = true;
            gridTextColumn4.CellStyle.Font.Size = 12F;
            gridTextColumn4.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn4.HeaderText = "Finalidade";
            gridTextColumn4.MappingName = "FinalidadeNfe";
            gridCheckBoxColumn1.AllowEditing = false;
            gridCheckBoxColumn1.AllowFiltering = true;
            gridCheckBoxColumn1.AllowResizing = true;
            gridCheckBoxColumn1.CellStyle.Font.Size = 12F;
            gridCheckBoxColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridCheckBoxColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridCheckBoxColumn1.HeaderText = "Mov. Financeiro";
            gridCheckBoxColumn1.MappingName = "MovimentaFinanceiro";
            gridCheckBoxColumn2.AllowEditing = false;
            gridCheckBoxColumn2.AllowFiltering = true;
            gridCheckBoxColumn2.AllowResizing = true;
            gridCheckBoxColumn2.CellStyle.Font.Size = 12F;
            gridCheckBoxColumn2.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridCheckBoxColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridCheckBoxColumn2.HeaderText = "Mov. Estoque";
            gridCheckBoxColumn2.MappingName = "MovimentaEstoque";
            this.grid.Columns.Add(gridTextColumn1);
            this.grid.Columns.Add(gridTextColumn2);
            this.grid.Columns.Add(gridTextColumn3);
            this.grid.Columns.Add(gridTextColumn4);
            this.grid.Columns.Add(gridCheckBoxColumn1);
            this.grid.Columns.Add(gridCheckBoxColumn2);
            this.grid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid.Location = new System.Drawing.Point(5, 114);
            this.grid.Name = "grid";
            this.grid.SelectionMode = Syncfusion.WinForms.DataGrid.Enums.GridSelectionMode.Extended;
            this.grid.Size = new System.Drawing.Size(1036, 201);
            this.grid.Style.CellStyle.Font.Size = 10F;
            this.grid.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.grid.Style.HeaderStyle.Font.Size = 12F;
            this.grid.TabIndex = 250;
            this.grid.Text = "Grid Parcelas";
            this.grid.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.grid_QueryRowStyle);
            this.grid.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.grid_CellDoubleClick);
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(19, 8);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(125, 16);
            this.autoLabel1.TabIndex = 211;
            this.autoLabel1.Text = "Natureza Operação";
            // 
            // autoLabel15
            // 
            this.autoLabel15.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel15.ForeColor = System.Drawing.Color.Black;
            this.autoLabel15.Location = new System.Drawing.Point(547, 8);
            this.autoLabel15.Name = "autoLabel15";
            this.autoLabel15.Size = new System.Drawing.Size(90, 16);
            this.autoLabel15.TabIndex = 210;
            this.autoLabel15.Text = "Reg. por Pág.";
            // 
            // sfDataPager1
            // 
            this.sfDataPager1.AccessibleName = "DataPager";
            this.sfDataPager1.BackColor = System.Drawing.Color.White;
            this.sfDataPager1.CanOverrideStyle = true;
            this.sfDataPager1.Location = new System.Drawing.Point(654, 11);
            this.sfDataPager1.Name = "sfDataPager1";
            this.sfDataPager1.Size = new System.Drawing.Size(376, 48);
            this.sfDataPager1.TabIndex = 154;
            this.sfDataPager1.OnDemandLoading += new System.EventHandler<Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs>(this.sfDataPager1_OnDemandLoading);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnPesquisaCliente);
            this.groupBox1.Controls.Add(this.autoLabel1);
            this.groupBox1.Controls.Add(this.autoLabel15);
            this.groupBox1.Controls.Add(this.txtRegistroPorPagina);
            this.groupBox1.Controls.Add(this.sfDataPager1);
            this.groupBox1.Controls.Add(this.txtNaturezaOperacao);
            this.groupBox1.Location = new System.Drawing.Point(5, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1036, 71);
            this.groupBox1.TabIndex = 243;
            this.groupBox1.TabStop = false;
            // 
            // btnPesquisaCliente
            // 
            this.btnPesquisaCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaCliente.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaCliente.FlatAppearance.BorderSize = 0;
            this.btnPesquisaCliente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaCliente.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaCliente.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaCliente.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaCliente.IconSize = 38;
            this.btnPesquisaCliente.Location = new System.Drawing.Point(496, 24);
            this.btnPesquisaCliente.Name = "btnPesquisaCliente";
            this.btnPesquisaCliente.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaCliente.TabIndex = 230;
            this.btnPesquisaCliente.UseVisualStyleBackColor = true;
            // 
            // txtRegistroPorPagina
            // 
            this.txtRegistroPorPagina.BackColor = System.Drawing.Color.White;
            this.txtRegistroPorPagina.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtRegistroPorPagina.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtRegistroPorPagina.BorderRadius = 8;
            this.txtRegistroPorPagina.BorderSize = 2;
            this.txtRegistroPorPagina.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtRegistroPorPagina.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegistroPorPagina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtRegistroPorPagina.Location = new System.Drawing.Point(539, 20);
            this.txtRegistroPorPagina.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegistroPorPagina.Multiline = false;
            this.txtRegistroPorPagina.Name = "txtRegistroPorPagina";
            this.txtRegistroPorPagina.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtRegistroPorPagina.PasswordChar = false;
            this.txtRegistroPorPagina.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtRegistroPorPagina.PlaceholderText = "";
            this.txtRegistroPorPagina.ReadOnly = false;
            this.txtRegistroPorPagina.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRegistroPorPagina.Size = new System.Drawing.Size(108, 37);
            this.txtRegistroPorPagina.TabIndex = 209;
            this.txtRegistroPorPagina.Tag = "";
            this.txtRegistroPorPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtRegistroPorPagina.Texts = "100";
            this.txtRegistroPorPagina.UnderlinedStyle = false;
            // 
            // txtNaturezaOperacao
            // 
            this.txtNaturezaOperacao.BackColor = System.Drawing.Color.White;
            this.txtNaturezaOperacao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNaturezaOperacao.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNaturezaOperacao.BorderRadius = 8;
            this.txtNaturezaOperacao.BorderSize = 2;
            this.txtNaturezaOperacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNaturezaOperacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNaturezaOperacao.Location = new System.Drawing.Point(7, 20);
            this.txtNaturezaOperacao.Margin = new System.Windows.Forms.Padding(4);
            this.txtNaturezaOperacao.Multiline = false;
            this.txtNaturezaOperacao.Name = "txtNaturezaOperacao";
            this.txtNaturezaOperacao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtNaturezaOperacao.PasswordChar = false;
            this.txtNaturezaOperacao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtNaturezaOperacao.PlaceholderText = "";
            this.txtNaturezaOperacao.ReadOnly = false;
            this.txtNaturezaOperacao.Size = new System.Drawing.Size(482, 37);
            this.txtNaturezaOperacao.TabIndex = 153;
            this.txtNaturezaOperacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNaturezaOperacao.Texts = "";
            this.txtNaturezaOperacao.UnderlinedStyle = false;
            this.txtNaturezaOperacao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNaturezaOperacao_KeyPress);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.btnExportarExcel.Location = new System.Drawing.Point(5, 321);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(36, 34);
            this.btnExportarExcel.TabIndex = 249;
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.btnExportarPDF.Location = new System.Drawing.Point(47, 321);
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(36, 34);
            this.btnExportarPDF.TabIndex = 248;
            this.btnExportarPDF.UseVisualStyleBackColor = true;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
            this.btnNovo.Location = new System.Drawing.Point(734, 335);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(293, 45);
            this.btnNovo.TabIndex = 252;
            this.btnNovo.Text = "Novo";
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
            this.btnEditar.Location = new System.Drawing.Point(435, 335);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(293, 45);
            this.btnEditar.TabIndex = 253;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // FrmNaturezaOperacaoLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1039, 392);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnExportarExcel);
            this.Controls.Add(this.btnExportarPDF);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmNaturezaOperacaoLista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Natureza Operação";
            this.Load += new System.EventHandler(this.FrmNaturezaOperacaoLista_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmNaturezaOperacaoLista_Paint);
            this.panelTitleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private FontAwesome.Sharp.IconButton btnExportarExcel;
        private FontAwesome.Sharp.IconButton btnExportarPDF;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
        private FontAwesome.Sharp.IconButton btnPesquisaCliente;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel15;
        private RJ_UI.Classes.RJTextBox txtRegistroPorPagina;
        private Syncfusion.WinForms.DataPager.SfDataPager sfDataPager1;
        private RJ_UI.Classes.RJTextBox txtNaturezaOperacao;
        private System.Windows.Forms.GroupBox groupBox1;
        private RJ_UI.Classes.RJButton btnNovo;
        private RJ_UI.Classes.RJButton btnEditar;
        private System.Windows.Forms.Timer timer1;
    }
}