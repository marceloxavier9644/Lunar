namespace Lunar.Telas.Vendas.Relatorios
{
    partial class FrmVendaProdutos
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
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn1 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn2 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn3 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn4 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn1 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn2 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn5 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVendaProdutos));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioVendedor = new System.Windows.Forms.RadioButton();
            this.radioGrupo = new System.Windows.Forms.RadioButton();
            this.radioMarca = new System.Windows.Forms.RadioButton();
            this.lblInformacao = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnLimpar = new FontAwesome.Sharp.IconButton();
            this.autoLabel40 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaMarca = new FontAwesome.Sharp.IconButton();
            this.autoLabel41 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaGrupo = new FontAwesome.Sharp.IconButton();
            this.autoLabel32 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel33 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnExportarExcel = new FontAwesome.Sharp.IconButton();
            this.chkOrdemServico = new System.Windows.Forms.CheckBox();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaVender = new FontAwesome.Sharp.IconButton();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataInicial = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.iconPesquisar = new FontAwesome.Sharp.IconButton();
            this.txtDataFinal = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.tabControlAdv1 = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            this.tabPageAdv1 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.tabPageAdv2 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.radioProduto = new System.Windows.Forms.RadioButton();
            this.txtCodMarca = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtMarca = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodGrupo = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtGrupo = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodVendedor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtVendedor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnPesquisar = new Lunar.RJ_UI.Classes.RJButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).BeginInit();
            this.tabControlAdv1.SuspendLayout();
            this.tabPageAdv1.SuspendLayout();
            this.tabPageAdv2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lblInformacao);
            this.groupBox1.Controls.Add(this.btnLimpar);
            this.groupBox1.Controls.Add(this.autoLabel40);
            this.groupBox1.Controls.Add(this.txtCodMarca);
            this.groupBox1.Controls.Add(this.btnPesquisaMarca);
            this.groupBox1.Controls.Add(this.autoLabel41);
            this.groupBox1.Controls.Add(this.txtMarca);
            this.groupBox1.Controls.Add(this.btnPesquisaGrupo);
            this.groupBox1.Controls.Add(this.autoLabel32);
            this.groupBox1.Controls.Add(this.txtCodGrupo);
            this.groupBox1.Controls.Add(this.autoLabel33);
            this.groupBox1.Controls.Add(this.txtGrupo);
            this.groupBox1.Controls.Add(this.btnExportarExcel);
            this.groupBox1.Controls.Add(this.chkOrdemServico);
            this.groupBox1.Controls.Add(this.autoLabel1);
            this.groupBox1.Controls.Add(this.txtCodVendedor);
            this.groupBox1.Controls.Add(this.btnPesquisaVender);
            this.groupBox1.Controls.Add(this.autoLabel6);
            this.groupBox1.Controls.Add(this.txtVendedor);
            this.groupBox1.Controls.Add(this.txtDataInicial);
            this.groupBox1.Controls.Add(this.autoLabel4);
            this.groupBox1.Controls.Add(this.iconPesquisar);
            this.groupBox1.Controls.Add(this.txtDataFinal);
            this.groupBox1.Controls.Add(this.btnPesquisar);
            this.groupBox1.Controls.Add(this.autoLabel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(999, 310);
            this.groupBox1.TabIndex = 245;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.radioProduto);
            this.groupBox2.Controls.Add(this.radioVendedor);
            this.groupBox2.Controls.Add(this.radioGrupo);
            this.groupBox2.Controls.Add(this.radioMarca);
            this.groupBox2.Location = new System.Drawing.Point(660, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 210);
            this.groupBox2.TabIndex = 270;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Agrupamento";
            // 
            // radioVendedor
            // 
            this.radioVendedor.AutoSize = true;
            this.radioVendedor.Location = new System.Drawing.Point(17, 25);
            this.radioVendedor.Name = "radioVendedor";
            this.radioVendedor.Size = new System.Drawing.Size(129, 17);
            this.radioVendedor.TabIndex = 266;
            this.radioVendedor.Text = "Agrupar por Vendedor";
            this.radioVendedor.UseVisualStyleBackColor = true;
            this.radioVendedor.CheckedChanged += new System.EventHandler(this.radioVendedor_CheckedChanged);
            // 
            // radioGrupo
            // 
            this.radioGrupo.AutoSize = true;
            this.radioGrupo.Location = new System.Drawing.Point(18, 71);
            this.radioGrupo.Name = "radioGrupo";
            this.radioGrupo.Size = new System.Drawing.Size(112, 17);
            this.radioGrupo.TabIndex = 267;
            this.radioGrupo.Text = "Agrupar por Grupo";
            this.radioGrupo.UseVisualStyleBackColor = true;
            this.radioGrupo.CheckedChanged += new System.EventHandler(this.radioGrupo_CheckedChanged);
            // 
            // radioMarca
            // 
            this.radioMarca.AutoSize = true;
            this.radioMarca.Location = new System.Drawing.Point(17, 117);
            this.radioMarca.Name = "radioMarca";
            this.radioMarca.Size = new System.Drawing.Size(113, 17);
            this.radioMarca.TabIndex = 268;
            this.radioMarca.Text = "Agrupar por Marca";
            this.radioMarca.UseVisualStyleBackColor = true;
            this.radioMarca.CheckedChanged += new System.EventHandler(this.radioMarca_CheckedChanged);
            // 
            // lblInformacao
            // 
            this.lblInformacao.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblInformacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformacao.ForeColor = System.Drawing.Color.Red;
            this.lblInformacao.Location = new System.Drawing.Point(526, 209);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(59, 20);
            this.lblInformacao.TabIndex = 265;
            this.lblInformacao.Text = "Código";
            this.lblInformacao.Visible = false;
            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = System.Drawing.Color.White;
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLimpar.FlatAppearance.BorderSize = 0;
            this.btnLimpar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnLimpar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLimpar.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            this.btnLimpar.IconColor = System.Drawing.Color.Black;
            this.btnLimpar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnLimpar.IconSize = 40;
            this.btnLimpar.Location = new System.Drawing.Point(541, 100);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(44, 36);
            this.btnLimpar.TabIndex = 264;
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // autoLabel40
            // 
            this.autoLabel40.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel40.Enabled = false;
            this.autoLabel40.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel40.ForeColor = System.Drawing.Color.Black;
            this.autoLabel40.Location = new System.Drawing.Point(374, 202);
            this.autoLabel40.Name = "autoLabel40";
            this.autoLabel40.Size = new System.Drawing.Size(76, 16);
            this.autoLabel40.TabIndex = 263;
            this.autoLabel40.Text = "Cód. Marca";
            // 
            // btnPesquisaMarca
            // 
            this.btnPesquisaMarca.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaMarca.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaMarca.FlatAppearance.BorderSize = 0;
            this.btnPesquisaMarca.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaMarca.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaMarca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaMarca.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaMarca.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaMarca.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaMarca.IconSize = 38;
            this.btnPesquisaMarca.Location = new System.Drawing.Point(331, 225);
            this.btnPesquisaMarca.Name = "btnPesquisaMarca";
            this.btnPesquisaMarca.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaMarca.TabIndex = 260;
            this.btnPesquisaMarca.UseVisualStyleBackColor = true;
            this.btnPesquisaMarca.Click += new System.EventHandler(this.btnPesquisaMarca_Click);
            // 
            // autoLabel41
            // 
            this.autoLabel41.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel41.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel41.ForeColor = System.Drawing.Color.Black;
            this.autoLabel41.Location = new System.Drawing.Point(13, 202);
            this.autoLabel41.Name = "autoLabel41";
            this.autoLabel41.Size = new System.Drawing.Size(45, 16);
            this.autoLabel41.TabIndex = 262;
            this.autoLabel41.Text = "Marca";
            // 
            // btnPesquisaGrupo
            // 
            this.btnPesquisaGrupo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaGrupo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaGrupo.FlatAppearance.BorderSize = 0;
            this.btnPesquisaGrupo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaGrupo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaGrupo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaGrupo.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaGrupo.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaGrupo.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaGrupo.IconSize = 38;
            this.btnPesquisaGrupo.Location = new System.Drawing.Point(330, 164);
            this.btnPesquisaGrupo.Name = "btnPesquisaGrupo";
            this.btnPesquisaGrupo.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaGrupo.TabIndex = 255;
            this.btnPesquisaGrupo.UseVisualStyleBackColor = true;
            this.btnPesquisaGrupo.Click += new System.EventHandler(this.btnPesquisaGrupo_Click);
            // 
            // autoLabel32
            // 
            this.autoLabel32.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel32.Enabled = false;
            this.autoLabel32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel32.ForeColor = System.Drawing.Color.Black;
            this.autoLabel32.Location = new System.Drawing.Point(374, 142);
            this.autoLabel32.Name = "autoLabel32";
            this.autoLabel32.Size = new System.Drawing.Size(75, 16);
            this.autoLabel32.TabIndex = 258;
            this.autoLabel32.Text = "Cód. Grupo";
            // 
            // autoLabel33
            // 
            this.autoLabel33.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel33.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel33.ForeColor = System.Drawing.Color.Black;
            this.autoLabel33.Location = new System.Drawing.Point(13, 141);
            this.autoLabel33.Name = "autoLabel33";
            this.autoLabel33.Size = new System.Drawing.Size(113, 16);
            this.autoLabel33.TabIndex = 257;
            this.autoLabel33.Text = "Grupo de Produto";
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnExportarExcel.Location = new System.Drawing.Point(951, 267);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(36, 34);
            this.btnExportarExcel.TabIndex = 253;
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Visible = false;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // chkOrdemServico
            // 
            this.chkOrdemServico.AutoSize = true;
            this.chkOrdemServico.Checked = true;
            this.chkOrdemServico.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOrdemServico.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOrdemServico.Location = new System.Drawing.Point(24, 274);
            this.chkOrdemServico.Name = "chkOrdemServico";
            this.chkOrdemServico.Size = new System.Drawing.Size(365, 24);
            this.chkOrdemServico.TabIndex = 252;
            this.chkOrdemServico.Text = "Incluir Produtos Vendidos em Ordem de Serviço";
            this.chkOrdemServico.UseVisualStyleBackColor = true;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(374, 81);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(51, 16);
            this.autoLabel1.TabIndex = 251;
            this.autoLabel1.Text = "Código";
            // 
            // btnPesquisaVender
            // 
            this.btnPesquisaVender.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaVender.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaVender.FlatAppearance.BorderSize = 0;
            this.btnPesquisaVender.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaVender.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaVender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaVender.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaVender.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaVender.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaVender.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaVender.IconSize = 38;
            this.btnPesquisaVender.Location = new System.Drawing.Point(331, 104);
            this.btnPesquisaVender.Name = "btnPesquisaVender";
            this.btnPesquisaVender.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaVender.TabIndex = 3;
            this.btnPesquisaVender.UseVisualStyleBackColor = true;
            this.btnPesquisaVender.Click += new System.EventHandler(this.btnPesquisaVender_Click);
            // 
            // autoLabel6
            // 
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(12, 80);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(67, 16);
            this.autoLabel6.TabIndex = 248;
            this.autoLabel6.Text = "Vendedor";
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.AllowNull = true;
            this.txtDataInicial.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataInicial.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDataInicial.Location = new System.Drawing.Point(12, 35);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Size = new System.Drawing.Size(190, 35);
            this.txtDataInicial.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.TabIndex = 0;
            this.txtDataInicial.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(208, 16);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(68, 16);
            this.autoLabel4.TabIndex = 228;
            this.autoLabel4.Text = "Data Final";
            // 
            // iconPesquisar
            // 
            this.iconPesquisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.iconPesquisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconPesquisar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.iconPesquisar.FlatAppearance.BorderSize = 0;
            this.iconPesquisar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.iconPesquisar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.iconPesquisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconPesquisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconPesquisar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.iconPesquisar.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.iconPesquisar.IconColor = System.Drawing.Color.White;
            this.iconPesquisar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.iconPesquisar.IconSize = 40;
            this.iconPesquisar.Location = new System.Drawing.Point(541, 159);
            this.iconPesquisar.Name = "iconPesquisar";
            this.iconPesquisar.Size = new System.Drawing.Size(44, 36);
            this.iconPesquisar.TabIndex = 5;
            this.iconPesquisar.UseVisualStyleBackColor = false;
            this.iconPesquisar.Click += new System.EventHandler(this.iconPesquisar_Click);
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.AllowNull = true;
            this.txtDataFinal.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataFinal.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Location = new System.Drawing.Point(208, 35);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Size = new System.Drawing.Size(193, 35);
            this.txtDataFinal.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataFinal.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.TabIndex = 1;
            this.txtDataFinal.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            this.txtDataFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataFinal_KeyPress);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(12, 18);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(73, 16);
            this.autoLabel3.TabIndex = 227;
            this.autoLabel3.Text = "Data Inicial";
            // 
            // grid
            // 
            this.grid.AccessibleName = "Table";
            this.grid.AllowEditing = false;
            this.grid.AllowFiltering = true;
            this.grid.AllowResizingColumns = true;
            this.grid.AllowResizingHiddenColumns = true;
            this.grid.AutoGenerateColumns = false;
            this.grid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowFiltering = true;
            gridTextColumn1.AllowResizing = true;
            gridTextColumn1.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn1.CellStyle.Font.Size = 12F;
            gridTextColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn1.HeaderStyle.Font.Size = 10F;
            gridTextColumn1.HeaderText = "Cód. Produto";
            gridTextColumn1.MappingName = "ID";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.ColumnHeader;
            gridTextColumn2.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            gridTextColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn2.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn2.HeaderStyle.Font.Size = 10F;
            gridTextColumn2.HeaderText = "Produto Descrição";
            gridTextColumn2.MappingName = "Descricao";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.AllowResizing = true;
            gridTextColumn3.CellStyle.Font.Size = 12F;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderStyle.Font.Facename = "montserrat";
            gridTextColumn3.HeaderStyle.Font.Size = 10F;
            gridTextColumn3.HeaderText = "Marca";
            gridTextColumn3.MappingName = "NomeMarca";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.AllowResizing = true;
            gridTextColumn4.CellStyle.Font.Size = 12F;
            gridTextColumn4.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn4.HeaderStyle.Font.Facename = "montserrat";
            gridTextColumn4.HeaderStyle.Font.Size = 10F;
            gridTextColumn4.HeaderText = "Grupo";
            gridTextColumn4.MappingName = "Grupo";
            gridNumericColumn1.AllowEditing = false;
            gridNumericColumn1.AllowFiltering = true;
            gridNumericColumn1.AllowResizing = true;
            gridNumericColumn1.CellStyle.Font.Size = 12F;
            gridNumericColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn1.HeaderStyle.Font.Facename = "montserrat";
            gridNumericColumn1.HeaderStyle.Font.Size = 10F;
            gridNumericColumn1.HeaderText = "Quantidade";
            gridNumericColumn1.MappingName = "Quantidade";
            gridNumericColumn2.AllowEditing = false;
            gridNumericColumn2.AllowFiltering = true;
            gridNumericColumn2.AllowResizing = true;
            gridNumericColumn2.CellStyle.Font.Size = 12F;
            gridNumericColumn2.Format = "N2";
            gridNumericColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn2.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn2.HeaderStyle.Font.Size = 10F;
            gridNumericColumn2.HeaderText = "Valor Produtos";
            gridNumericColumn2.MappingName = "ValorTotal";
            gridTextColumn5.AllowEditing = false;
            gridTextColumn5.AllowFiltering = true;
            gridTextColumn5.AllowResizing = true;
            gridTextColumn5.CellStyle.Font.Size = 12F;
            gridTextColumn5.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn5.HeaderStyle.Font.Facename = "montserrat";
            gridTextColumn5.HeaderStyle.Font.Size = 10F;
            gridTextColumn5.HeaderText = "Vendedor";
            gridTextColumn5.MappingName = "CodVendedor";
            gridTextColumn5.NullDisplayText = "-";
            this.grid.Columns.Add(gridTextColumn1);
            this.grid.Columns.Add(gridTextColumn2);
            this.grid.Columns.Add(gridTextColumn3);
            this.grid.Columns.Add(gridTextColumn4);
            this.grid.Columns.Add(gridNumericColumn1);
            this.grid.Columns.Add(gridNumericColumn2);
            this.grid.Columns.Add(gridTextColumn5);
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(996, 311);
            this.grid.Style.CellStyle.Font.Size = 10F;
            this.grid.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.grid.Style.HeaderStyle.Font.Size = 12F;
            this.grid.TabIndex = 246;
            this.grid.Text = "Grid Parcelas";
            this.grid.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.grid_QueryRowStyle);
            // 
            // tabControlAdv1
            // 
            this.tabControlAdv1.BeforeTouchSize = new System.Drawing.Size(999, 351);
            this.tabControlAdv1.Controls.Add(this.tabPageAdv1);
            this.tabControlAdv1.Controls.Add(this.tabPageAdv2);
            this.tabControlAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAdv1.Location = new System.Drawing.Point(0, 310);
            this.tabControlAdv1.Name = "tabControlAdv1";
            this.tabControlAdv1.Size = new System.Drawing.Size(999, 351);
            this.tabControlAdv1.TabIndex = 247;
            this.tabControlAdv1.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererVS2008);
            this.tabControlAdv1.ThemeName = "TabRendererVS2008";
            this.tabControlAdv1.ThemeStyle.PrimitiveButtonStyle.DisabledNextPageImage = null;
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.Controls.Add(this.grid);
            this.tabPageAdv1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPageAdv1.Image = null;
            this.tabPageAdv1.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPageAdv1.Location = new System.Drawing.Point(1, 38);
            this.tabPageAdv1.Name = "tabPageAdv1";
            this.tabPageAdv1.ShowCloseButton = true;
            this.tabPageAdv1.Size = new System.Drawing.Size(996, 311);
            this.tabPageAdv1.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPageAdv1.TabIndex = 1;
            this.tabPageAdv1.Text = "Dados";
            this.tabPageAdv1.ThemesEnabled = false;
            // 
            // tabPageAdv2
            // 
            this.tabPageAdv2.Controls.Add(this.chart1);
            this.tabPageAdv2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageAdv2.Image = null;
            this.tabPageAdv2.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPageAdv2.Location = new System.Drawing.Point(1, 38);
            this.tabPageAdv2.Name = "tabPageAdv2";
            this.tabPageAdv2.ShowCloseButton = true;
            this.tabPageAdv2.Size = new System.Drawing.Size(996, 311);
            this.tabPageAdv2.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPageAdv2.TabIndex = 2;
            this.tabPageAdv2.Text = "Gráfico";
            this.tabPageAdv2.ThemesEnabled = false;
            // 
            // chart1
            // 
            this.chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.FrameThin1;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(996, 311);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // radioProduto
            // 
            this.radioProduto.AutoSize = true;
            this.radioProduto.Checked = true;
            this.radioProduto.Location = new System.Drawing.Point(18, 162);
            this.radioProduto.Name = "radioProduto";
            this.radioProduto.Size = new System.Drawing.Size(120, 17);
            this.radioProduto.TabIndex = 269;
            this.radioProduto.TabStop = true;
            this.radioProduto.Text = "Agrupar por Produto";
            this.radioProduto.UseVisualStyleBackColor = true;
            this.radioProduto.CheckedChanged += new System.EventHandler(this.radioProduto_CheckedChanged);
            // 
            // txtCodMarca
            // 
            this.txtCodMarca.BackColor = System.Drawing.Color.White;
            this.txtCodMarca.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodMarca.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodMarca.BorderRadius = 8;
            this.txtCodMarca.BorderSize = 2;
            this.txtCodMarca.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCodMarca.Enabled = false;
            this.txtCodMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodMarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodMarca.Location = new System.Drawing.Point(374, 222);
            this.txtCodMarca.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodMarca.Multiline = false;
            this.txtCodMarca.Name = "txtCodMarca";
            this.txtCodMarca.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodMarca.PasswordChar = false;
            this.txtCodMarca.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodMarca.PlaceholderText = "";
            this.txtCodMarca.ReadOnly = false;
            this.txtCodMarca.Size = new System.Drawing.Size(137, 37);
            this.txtCodMarca.TabIndex = 261;
            this.txtCodMarca.Tag = "";
            this.txtCodMarca.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodMarca.Texts = "";
            this.txtCodMarca.UnderlinedStyle = false;
            // 
            // txtMarca
            // 
            this.txtMarca.BackColor = System.Drawing.Color.White;
            this.txtMarca.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtMarca.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtMarca.BorderRadius = 8;
            this.txtMarca.BorderSize = 2;
            this.txtMarca.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtMarca.Location = new System.Drawing.Point(13, 221);
            this.txtMarca.Margin = new System.Windows.Forms.Padding(4);
            this.txtMarca.Multiline = false;
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtMarca.PasswordChar = false;
            this.txtMarca.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtMarca.PlaceholderText = "";
            this.txtMarca.ReadOnly = false;
            this.txtMarca.Size = new System.Drawing.Size(310, 37);
            this.txtMarca.TabIndex = 259;
            this.txtMarca.Tag = "";
            this.txtMarca.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtMarca.Texts = "";
            this.txtMarca.UnderlinedStyle = false;
            // 
            // txtCodGrupo
            // 
            this.txtCodGrupo.BackColor = System.Drawing.Color.White;
            this.txtCodGrupo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodGrupo.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodGrupo.BorderRadius = 8;
            this.txtCodGrupo.BorderSize = 2;
            this.txtCodGrupo.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCodGrupo.Enabled = false;
            this.txtCodGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodGrupo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodGrupo.Location = new System.Drawing.Point(374, 161);
            this.txtCodGrupo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodGrupo.Multiline = false;
            this.txtCodGrupo.Name = "txtCodGrupo";
            this.txtCodGrupo.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodGrupo.PasswordChar = false;
            this.txtCodGrupo.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodGrupo.PlaceholderText = "";
            this.txtCodGrupo.ReadOnly = false;
            this.txtCodGrupo.Size = new System.Drawing.Size(137, 37);
            this.txtCodGrupo.TabIndex = 256;
            this.txtCodGrupo.Tag = "";
            this.txtCodGrupo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodGrupo.Texts = "";
            this.txtCodGrupo.UnderlinedStyle = false;
            // 
            // txtGrupo
            // 
            this.txtGrupo.BackColor = System.Drawing.Color.White;
            this.txtGrupo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtGrupo.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtGrupo.BorderRadius = 8;
            this.txtGrupo.BorderSize = 2;
            this.txtGrupo.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrupo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtGrupo.Location = new System.Drawing.Point(13, 161);
            this.txtGrupo.Margin = new System.Windows.Forms.Padding(4);
            this.txtGrupo.Multiline = false;
            this.txtGrupo.Name = "txtGrupo";
            this.txtGrupo.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtGrupo.PasswordChar = false;
            this.txtGrupo.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtGrupo.PlaceholderText = "";
            this.txtGrupo.ReadOnly = false;
            this.txtGrupo.Size = new System.Drawing.Size(310, 37);
            this.txtGrupo.TabIndex = 254;
            this.txtGrupo.Tag = "";
            this.txtGrupo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtGrupo.Texts = "";
            this.txtGrupo.UnderlinedStyle = false;
            // 
            // txtCodVendedor
            // 
            this.txtCodVendedor.BackColor = System.Drawing.Color.White;
            this.txtCodVendedor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodVendedor.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodVendedor.BorderRadius = 8;
            this.txtCodVendedor.BorderSize = 2;
            this.txtCodVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodVendedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodVendedor.Location = new System.Drawing.Point(374, 101);
            this.txtCodVendedor.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodVendedor.Multiline = false;
            this.txtCodVendedor.Name = "txtCodVendedor";
            this.txtCodVendedor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodVendedor.PasswordChar = false;
            this.txtCodVendedor.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodVendedor.PlaceholderText = "";
            this.txtCodVendedor.ReadOnly = false;
            this.txtCodVendedor.Size = new System.Drawing.Size(137, 37);
            this.txtCodVendedor.TabIndex = 4;
            this.txtCodVendedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodVendedor.Texts = "";
            this.txtCodVendedor.UnderlinedStyle = false;
            this.txtCodVendedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodVendedor_KeyPress);
            // 
            // txtVendedor
            // 
            this.txtVendedor.BackColor = System.Drawing.Color.White;
            this.txtVendedor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtVendedor.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtVendedor.BorderRadius = 8;
            this.txtVendedor.BorderSize = 2;
            this.txtVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVendedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtVendedor.Location = new System.Drawing.Point(13, 100);
            this.txtVendedor.Margin = new System.Windows.Forms.Padding(4);
            this.txtVendedor.Multiline = false;
            this.txtVendedor.Name = "txtVendedor";
            this.txtVendedor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtVendedor.PasswordChar = false;
            this.txtVendedor.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtVendedor.PlaceholderText = "";
            this.txtVendedor.ReadOnly = false;
            this.txtVendedor.Size = new System.Drawing.Size(311, 37);
            this.txtVendedor.TabIndex = 2;
            this.txtVendedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtVendedor.Texts = "";
            this.txtVendedor.UnderlinedStyle = false;
            this.txtVendedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVendedor_KeyPress);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnPesquisar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnPesquisar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisar.BorderRadius = 8;
            this.btnPesquisar.BorderSize = 2;
            this.btnPesquisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(90)))));
            this.btnPesquisar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnPesquisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnPesquisar.Location = new System.Drawing.Point(523, 144);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(77, 62);
            this.btnPesquisar.TabIndex = 242;
            this.btnPesquisar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // FrmVendaProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(999, 661);
            this.Controls.Add(this.tabControlAdv1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVendaProdutos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Produtos Vendidos";
            this.Load += new System.EventHandler(this.FrmVendaProdutos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).EndInit();
            this.tabControlAdv1.ResumeLayout(false);
            this.tabPageAdv1.ResumeLayout(false);
            this.tabPageAdv2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FontAwesome.Sharp.IconButton iconPesquisar;
        private RJ_UI.Classes.RJButton btnPesquisar;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataInicial;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataFinal;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtCodVendedor;
        private FontAwesome.Sharp.IconButton btnPesquisaVender;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private RJ_UI.Classes.RJTextBox txtVendedor;
        private System.Windows.Forms.CheckBox chkOrdemServico;
        private FontAwesome.Sharp.IconButton btnExportarExcel;
        private FontAwesome.Sharp.IconButton btnPesquisaGrupo;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel32;
        private RJ_UI.Classes.RJTextBox txtCodGrupo;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel33;
        private RJ_UI.Classes.RJTextBox txtGrupo;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel40;
        private RJ_UI.Classes.RJTextBox txtCodMarca;
        private FontAwesome.Sharp.IconButton btnPesquisaMarca;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel41;
        private RJ_UI.Classes.RJTextBox txtMarca;
        private FontAwesome.Sharp.IconButton btnLimpar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblInformacao;
        private System.Windows.Forms.RadioButton radioMarca;
        private System.Windows.Forms.RadioButton radioGrupo;
        private System.Windows.Forms.RadioButton radioVendedor;
        private System.Windows.Forms.GroupBox groupBox2;
        private Syncfusion.Windows.Forms.Tools.TabControlAdv tabControlAdv1;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageAdv1;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageAdv2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.RadioButton radioProduto;
    }
}