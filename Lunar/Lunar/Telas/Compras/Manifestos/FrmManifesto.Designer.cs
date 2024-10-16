﻿namespace Lunar.Telas.Compras.Manifestos
{
    partial class FrmManifesto
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
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn7 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn2 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridCheckBoxColumn gridCheckBoxColumn2 = new Syncfusion.WinForms.DataGrid.GridCheckBoxColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn8 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn9 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn10 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn11 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn12 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.dsManifesto = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.autoLabel15 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.sfDataPager1 = new Syncfusion.WinForms.DataPager.SfDataPager();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPesquisar = new FontAwesome.Sharp.IconButton();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnAtualizarNotas = new FontAwesome.Sharp.IconButton();
            this.txtDataFinal = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.txtDataInicial = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.txtPesquisa = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtRegistroPorPagina = new Lunar.RJ_UI.Classes.RJTextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnExportarPDF = new FontAwesome.Sharp.IconButton();
            this.btnExportarExcel = new FontAwesome.Sharp.IconButton();
            this.btnImportarXML = new Lunar.RJ_UI.Classes.RJButton();
            this.btnCancelarLancamento = new Lunar.RJ_UI.Classes.RJButton();
            this.btnImprimir = new Lunar.RJ_UI.Classes.RJButton();
            this.btnRejeitarNota = new Lunar.RJ_UI.Classes.RJButton();
            this.btnLancarNota = new Lunar.RJ_UI.Classes.RJButton();
            this.btnConfirmarNota = new Lunar.RJ_UI.Classes.RJButton();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsManifesto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
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
            this.panelTitleBar.Size = new System.Drawing.Size(1163, 38);
            this.panelTitleBar.TabIndex = 201;
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
            this.btnClose.Location = new System.Drawing.Point(1122, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            gridTextColumn7.AllowEditing = false;
            gridTextColumn7.AllowFiltering = true;
            gridTextColumn7.AllowResizing = true;
            gridTextColumn7.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.ColumnHeader;
            gridTextColumn7.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn7.CellStyle.Font.Size = 10F;
            gridTextColumn7.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn7.Format = "dd/MM/yyyy";
            gridTextColumn7.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn7.HeaderStyle.Font.Size = 10F;
            gridTextColumn7.HeaderStyle.VerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            gridTextColumn7.HeaderText = "Data Emissão";
            gridTextColumn7.MappingName = "DataEmissao";
            gridNumericColumn2.AllowEditing = false;
            gridNumericColumn2.AllowFiltering = true;
            gridNumericColumn2.AllowResizing = true;
            gridNumericColumn2.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridNumericColumn2.CellStyle.Font.Size = 10F;
            gridNumericColumn2.Format = "0";
            gridNumericColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn2.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn2.HeaderStyle.Font.Size = 10F;
            gridNumericColumn2.HeaderText = "Nº NFe";
            gridNumericColumn2.MappingName = "NNf";
            gridCheckBoxColumn2.AllowEditing = false;
            gridCheckBoxColumn2.AllowFiltering = true;
            gridCheckBoxColumn2.AllowResizing = true;
            gridCheckBoxColumn2.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridCheckBoxColumn2.CellStyle.Font.Size = 10F;
            gridCheckBoxColumn2.HeaderStyle.Font.Facename = "Montserrat";
            gridCheckBoxColumn2.HeaderStyle.Font.Size = 10F;
            gridCheckBoxColumn2.HeaderText = "Lançada?";
            gridCheckBoxColumn2.MappingName = "Lancada";
            gridTextColumn8.AllowEditing = false;
            gridTextColumn8.AllowFiltering = true;
            gridTextColumn8.AllowResizing = true;
            gridTextColumn8.AllowSorting = false;
            gridTextColumn8.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn8.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn8.CellStyle.Font.Size = 10F;
            gridTextColumn8.Format = "00\\.000\\.000\\/0000\\-00";
            gridTextColumn8.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn8.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn8.HeaderStyle.Font.Size = 10F;
            gridTextColumn8.HeaderText = "CNPJ";
            gridTextColumn8.MappingName = "CnpjEmitente";
            gridTextColumn9.AllowEditing = false;
            gridTextColumn9.AllowFiltering = true;
            gridTextColumn9.AllowResizing = true;
            gridTextColumn9.AllowSorting = false;
            gridTextColumn9.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn9.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn9.CellStyle.Font.Size = 10F;
            gridTextColumn9.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn9.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn9.HeaderStyle.Font.Size = 10F;
            gridTextColumn9.HeaderText = "Razão Social";
            gridTextColumn9.MappingName = "RazaoEmitente";
            gridTextColumn10.AllowEditing = false;
            gridTextColumn10.AllowFiltering = true;
            gridTextColumn10.AllowResizing = true;
            gridTextColumn10.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn10.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn10.CellStyle.Font.Size = 10F;
            gridTextColumn10.Format = "C";
            gridTextColumn10.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn10.HeaderStyle.Font.Size = 10F;
            gridTextColumn10.HeaderText = "Valor";
            gridTextColumn10.MappingName = "VNf";
            gridTextColumn11.AllowEditing = false;
            gridTextColumn11.AllowFiltering = true;
            gridTextColumn11.AllowResizing = true;
            gridTextColumn11.AllowSorting = false;
            gridTextColumn11.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn11.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn11.CellStyle.Font.Size = 10F;
            gridTextColumn11.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn11.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn11.HeaderStyle.Font.Size = 10F;
            gridTextColumn11.HeaderText = "Chave";
            gridTextColumn11.MappingName = "Chave";
            gridTextColumn12.AllowEditing = false;
            gridTextColumn12.AllowFiltering = true;
            gridTextColumn12.AllowResizing = true;
            gridTextColumn12.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn12.CellStyle.Font.Size = 10F;
            gridTextColumn12.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn12.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn12.HeaderStyle.Font.Size = 10F;
            gridTextColumn12.HeaderText = "Manifesto";
            gridTextColumn12.MappingName = "Manifesto";
            this.grid.Columns.Add(gridTextColumn7);
            this.grid.Columns.Add(gridNumericColumn2);
            this.grid.Columns.Add(gridCheckBoxColumn2);
            this.grid.Columns.Add(gridTextColumn8);
            this.grid.Columns.Add(gridTextColumn9);
            this.grid.Columns.Add(gridTextColumn10);
            this.grid.Columns.Add(gridTextColumn11);
            this.grid.Columns.Add(gridTextColumn12);
            this.grid.Location = new System.Drawing.Point(12, 170);
            this.grid.Name = "grid";
            this.grid.SelectionUnit = Syncfusion.WinForms.DataGrid.Enums.SelectionUnit.Cell;
            this.grid.Size = new System.Drawing.Size(1146, 174);
            this.grid.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.grid.TabIndex = 202;
            this.grid.Text = "sfDataGrid1";
            this.grid.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.grid_QueryRowStyle);
            // 
            // dsManifesto
            // 
            this.dsManifesto.DataSetName = "dsManifesto";
            this.dsManifesto.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8});
            this.dataTable1.TableName = "Nota";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "CHAVE";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "CNPJ";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "RAZAOSOCIAL";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "VALOR";
            this.dataColumn4.DataType = typeof(decimal);
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "DATA";
            this.dataColumn5.DataType = typeof(System.DateTime);
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "NUMERO";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "LANCADA";
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "MANIFESTO";
            // 
            // autoLabel15
            // 
            this.autoLabel15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.autoLabel15.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel15.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel15.ForeColor = System.Drawing.Color.Black;
            this.autoLabel15.Location = new System.Drawing.Point(597, 8);
            this.autoLabel15.Name = "autoLabel15";
            this.autoLabel15.Size = new System.Drawing.Size(94, 21);
            this.autoLabel15.TabIndex = 218;
            this.autoLabel15.Text = "Reg. por Pág.";
            // 
            // sfDataPager1
            // 
            this.sfDataPager1.AccessibleName = "DataPager";
            this.sfDataPager1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sfDataPager1.BackColor = System.Drawing.Color.White;
            this.sfDataPager1.CanOverrideStyle = true;
            this.sfDataPager1.Location = new System.Drawing.Point(704, 15);
            this.sfDataPager1.Name = "sfDataPager1";
            this.sfDataPager1.Size = new System.Drawing.Size(395, 48);
            this.sfDataPager1.TabIndex = 216;
            this.sfDataPager1.OnDemandLoading += new System.EventHandler<Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs>(this.sfDataPager1_OnDemandLoading);
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(19, 8);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(68, 21);
            this.autoLabel1.TabIndex = 219;
            this.autoLabel1.Text = "Pesquisa";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnPesquisar);
            this.groupBox1.Controls.Add(this.autoLabel4);
            this.groupBox1.Controls.Add(this.autoLabel3);
            this.groupBox1.Controls.Add(this.btnAtualizarNotas);
            this.groupBox1.Controls.Add(this.txtDataFinal);
            this.groupBox1.Controls.Add(this.txtDataInicial);
            this.groupBox1.Controls.Add(this.sfDataPager1);
            this.groupBox1.Controls.Add(this.autoLabel1);
            this.groupBox1.Controls.Add(this.txtPesquisa);
            this.groupBox1.Controls.Add(this.autoLabel15);
            this.groupBox1.Controls.Add(this.txtRegistroPorPagina);
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1146, 120);
            this.groupBox1.TabIndex = 220;
            this.groupBox1.TabStop = false;
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisar.FlatAppearance.BorderSize = 0;
            this.btnPesquisar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisar.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisar.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisar.IconSize = 38;
            this.btnPesquisar.Location = new System.Drawing.Point(411, 80);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisar.TabIndex = 224;
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(224, 62);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(153, 21);
            this.autoLabel4.TabIndex = 223;
            this.autoLabel4.Text = "Data de Emissão Final";
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(17, 62);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(161, 21);
            this.autoLabel3.TabIndex = 222;
            this.autoLabel3.Text = "Data de Emissão Inicial";
            // 
            // btnAtualizarNotas
            // 
            this.btnAtualizarNotas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAtualizarNotas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizarNotas.FlatAppearance.BorderSize = 0;
            this.btnAtualizarNotas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAtualizarNotas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAtualizarNotas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizarNotas.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizarNotas.IconChar = FontAwesome.Sharp.IconChar.Redo;
            this.btnAtualizarNotas.IconColor = System.Drawing.Color.Gray;
            this.btnAtualizarNotas.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnAtualizarNotas.IconSize = 40;
            this.btnAtualizarNotas.Location = new System.Drawing.Point(992, 76);
            this.btnAtualizarNotas.Name = "btnAtualizarNotas";
            this.btnAtualizarNotas.Size = new System.Drawing.Size(147, 38);
            this.btnAtualizarNotas.TabIndex = 214;
            this.btnAtualizarNotas.Text = "Atualizar";
            this.btnAtualizarNotas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAtualizarNotas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAtualizarNotas.UseVisualStyleBackColor = true;
            this.btnAtualizarNotas.Click += new System.EventHandler(this.btnAtualizarNotas_Click);
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataFinal.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataFinal.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Location = new System.Drawing.Point(218, 79);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Size = new System.Drawing.Size(185, 35);
            this.txtDataFinal.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataFinal.Style.HoverBorderColor = System.Drawing.Color.White;
            this.txtDataFinal.TabIndex = 221;
            this.txtDataFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataFinal_KeyPress);
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataInicial.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataInicial.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.txtDataInicial.Location = new System.Drawing.Point(12, 79);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Size = new System.Drawing.Size(185, 35);
            this.txtDataInicial.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataInicial.Style.HoverBorderColor = System.Drawing.Color.White;
            this.txtDataInicial.TabIndex = 220;
            this.txtDataInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataInicial_KeyPress);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPesquisa.BackColor = System.Drawing.Color.White;
            this.txtPesquisa.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPesquisa.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPesquisa.BorderRadius = 8;
            this.txtPesquisa.BorderSize = 2;
            this.txtPesquisa.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPesquisa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPesquisa.Location = new System.Drawing.Point(8, 20);
            this.txtPesquisa.Margin = new System.Windows.Forms.Padding(4);
            this.txtPesquisa.Multiline = false;
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtPesquisa.PasswordChar = false;
            this.txtPesquisa.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtPesquisa.PlaceholderText = "";
            this.txtPesquisa.ReadOnly = false;
            this.txtPesquisa.Size = new System.Drawing.Size(573, 44);
            this.txtPesquisa.TabIndex = 215;
            this.txtPesquisa.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPesquisa.Texts = "";
            this.txtPesquisa.UnderlinedStyle = false;
            this.txtPesquisa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPesquisa_KeyPress);
            // 
            // txtRegistroPorPagina
            // 
            this.txtRegistroPorPagina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegistroPorPagina.BackColor = System.Drawing.Color.White;
            this.txtRegistroPorPagina.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtRegistroPorPagina.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtRegistroPorPagina.BorderRadius = 8;
            this.txtRegistroPorPagina.BorderSize = 2;
            this.txtRegistroPorPagina.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtRegistroPorPagina.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegistroPorPagina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtRegistroPorPagina.Location = new System.Drawing.Point(589, 20);
            this.txtRegistroPorPagina.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegistroPorPagina.Multiline = false;
            this.txtRegistroPorPagina.Name = "txtRegistroPorPagina";
            this.txtRegistroPorPagina.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtRegistroPorPagina.PasswordChar = false;
            this.txtRegistroPorPagina.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtRegistroPorPagina.PlaceholderText = "";
            this.txtRegistroPorPagina.ReadOnly = false;
            this.txtRegistroPorPagina.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRegistroPorPagina.Size = new System.Drawing.Size(108, 44);
            this.txtRegistroPorPagina.TabIndex = 217;
            this.txtRegistroPorPagina.Tag = "";
            this.txtRegistroPorPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtRegistroPorPagina.Texts = "100";
            this.txtRegistroPorPagina.UnderlinedStyle = false;
            this.txtRegistroPorPagina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegistroPorPagina_KeyPress);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
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
            this.btnExportarPDF.Location = new System.Drawing.Point(54, 350);
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(36, 34);
            this.btnExportarPDF.TabIndex = 222;
            this.btnExportarPDF.UseVisualStyleBackColor = true;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
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
            this.btnExportarExcel.Location = new System.Drawing.Point(12, 350);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(36, 34);
            this.btnExportarExcel.TabIndex = 221;
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // btnImportarXML
            // 
            this.btnImportarXML.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnImportarXML.BackColor = System.Drawing.Color.White;
            this.btnImportarXML.BackgroundColor = System.Drawing.Color.White;
            this.btnImportarXML.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnImportarXML.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnImportarXML.BorderRadius = 8;
            this.btnImportarXML.BorderSize = 0;
            this.btnImportarXML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportarXML.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnImportarXML.FlatAppearance.BorderSize = 2;
            this.btnImportarXML.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnImportarXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportarXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportarXML.ForeColor = System.Drawing.Color.White;
            this.btnImportarXML.Image = global::Lunar.Properties.Resources.XML64x64;
            this.btnImportarXML.Location = new System.Drawing.Point(1039, 393);
            this.btnImportarXML.Name = "btnImportarXML";
            this.btnImportarXML.Size = new System.Drawing.Size(90, 45);
            this.btnImportarXML.TabIndex = 224;
            this.btnImportarXML.TextColor = System.Drawing.Color.White;
            this.btnImportarXML.UseVisualStyleBackColor = false;
            this.btnImportarXML.Click += new System.EventHandler(this.btnImportarXML_Click);
            // 
            // btnCancelarLancamento
            // 
            this.btnCancelarLancamento.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelarLancamento.BackColor = System.Drawing.Color.Firebrick;
            this.btnCancelarLancamento.BackgroundColor = System.Drawing.Color.Firebrick;
            this.btnCancelarLancamento.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnCancelarLancamento.BorderRadius = 8;
            this.btnCancelarLancamento.BorderSize = 0;
            this.btnCancelarLancamento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarLancamento.FlatAppearance.BorderSize = 0;
            this.btnCancelarLancamento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarLancamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarLancamento.ForeColor = System.Drawing.Color.White;
            this.btnCancelarLancamento.Location = new System.Drawing.Point(33, 393);
            this.btnCancelarLancamento.Name = "btnCancelarLancamento";
            this.btnCancelarLancamento.Size = new System.Drawing.Size(193, 45);
            this.btnCancelarLancamento.TabIndex = 223;
            this.btnCancelarLancamento.Text = "Cancelar Lançamento";
            this.btnCancelarLancamento.TextColor = System.Drawing.Color.White;
            this.btnCancelarLancamento.UseVisualStyleBackColor = false;
            this.btnCancelarLancamento.Click += new System.EventHandler(this.btnCancelarLancamento_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(52)))));
            this.btnImprimir.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(52)))));
            this.btnImprimir.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnImprimir.BorderRadius = 8;
            this.btnImprimir.BorderSize = 0;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Location = new System.Drawing.Point(642, 393);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(199, 45);
            this.btnImprimir.TabIndex = 213;
            this.btnImprimir.Text = "Imprimir Nota [F8]";
            this.btnImprimir.TextColor = System.Drawing.Color.White;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnRejeitarNota
            // 
            this.btnRejeitarNota.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRejeitarNota.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.btnRejeitarNota.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.btnRejeitarNota.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnRejeitarNota.BorderRadius = 8;
            this.btnRejeitarNota.BorderSize = 0;
            this.btnRejeitarNota.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRejeitarNota.FlatAppearance.BorderSize = 0;
            this.btnRejeitarNota.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRejeitarNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRejeitarNota.ForeColor = System.Drawing.Color.White;
            this.btnRejeitarNota.Location = new System.Drawing.Point(437, 393);
            this.btnRejeitarNota.Name = "btnRejeitarNota";
            this.btnRejeitarNota.Size = new System.Drawing.Size(199, 45);
            this.btnRejeitarNota.TabIndex = 205;
            this.btnRejeitarNota.Text = "Rejeitar Nota [F7]";
            this.btnRejeitarNota.TextColor = System.Drawing.Color.White;
            this.btnRejeitarNota.UseVisualStyleBackColor = false;
            this.btnRejeitarNota.Click += new System.EventHandler(this.btnRejeitarNota_Click);
            // 
            // btnLancarNota
            // 
            this.btnLancarNota.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLancarNota.BackColor = System.Drawing.Color.SeaGreen;
            this.btnLancarNota.BackgroundColor = System.Drawing.Color.SeaGreen;
            this.btnLancarNota.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnLancarNota.BorderRadius = 8;
            this.btnLancarNota.BorderSize = 0;
            this.btnLancarNota.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLancarNota.FlatAppearance.BorderSize = 0;
            this.btnLancarNota.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLancarNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLancarNota.ForeColor = System.Drawing.Color.White;
            this.btnLancarNota.Location = new System.Drawing.Point(847, 393);
            this.btnLancarNota.Name = "btnLancarNota";
            this.btnLancarNota.Size = new System.Drawing.Size(186, 45);
            this.btnLancarNota.TabIndex = 204;
            this.btnLancarNota.Text = "Lançar Nota [F5]";
            this.btnLancarNota.TextColor = System.Drawing.Color.White;
            this.btnLancarNota.UseVisualStyleBackColor = false;
            this.btnLancarNota.Click += new System.EventHandler(this.btnLancarNota_Click);
            // 
            // btnConfirmarNota
            // 
            this.btnConfirmarNota.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnConfirmarNota.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(55)))));
            this.btnConfirmarNota.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(55)))));
            this.btnConfirmarNota.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnConfirmarNota.BorderRadius = 8;
            this.btnConfirmarNota.BorderSize = 0;
            this.btnConfirmarNota.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmarNota.FlatAppearance.BorderSize = 0;
            this.btnConfirmarNota.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarNota.ForeColor = System.Drawing.Color.White;
            this.btnConfirmarNota.Location = new System.Drawing.Point(232, 393);
            this.btnConfirmarNota.Name = "btnConfirmarNota";
            this.btnConfirmarNota.Size = new System.Drawing.Size(199, 45);
            this.btnConfirmarNota.TabIndex = 203;
            this.btnConfirmarNota.Text = "Confirmar Operação [F6]";
            this.btnConfirmarNota.TextColor = System.Drawing.Color.White;
            this.btnConfirmarNota.UseVisualStyleBackColor = false;
            this.btnConfirmarNota.Click += new System.EventHandler(this.btnConfirmarNota_Click);
            // 
            // FrmManifesto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1163, 450);
            this.Controls.Add(this.btnImportarXML);
            this.Controls.Add(this.btnCancelarLancamento);
            this.Controls.Add(this.btnExportarPDF);
            this.Controls.Add(this.btnExportarExcel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnRejeitarNota);
            this.Controls.Add(this.btnLancarNota);
            this.Controls.Add(this.btnConfirmarNota);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmManifesto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta Notas e Manifesto";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmManifesto_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmManifesto_KeyDown);
            this.panelTitleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsManifesto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private FontAwesome.Sharp.IconButton btnClose;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
        private System.Data.DataSet dsManifesto;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private RJ_UI.Classes.RJButton btnConfirmarNota;
        private RJ_UI.Classes.RJButton btnLancarNota;
        private RJ_UI.Classes.RJButton btnRejeitarNota;
        private System.Data.DataColumn dataColumn5;
        private RJ_UI.Classes.RJButton btnImprimir;
        private FontAwesome.Sharp.IconButton btnAtualizarNotas;
        private System.Data.DataColumn dataColumn6;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel15;
        private RJ_UI.Classes.RJTextBox txtRegistroPorPagina;
        private Syncfusion.WinForms.DataPager.SfDataPager sfDataPager1;
        private RJ_UI.Classes.RJTextBox txtPesquisa;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataFinal;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataInicial;
        private FontAwesome.Sharp.IconButton btnPesquisar;
        private FontAwesome.Sharp.IconButton btnExportarPDF;
        private FontAwesome.Sharp.IconButton btnExportarExcel;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private RJ_UI.Classes.RJButton btnCancelarLancamento;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private RJ_UI.Classes.RJButton btnImportarXML;
    }
}