namespace Lunar.Telas.Condicionais
{
    partial class FrmCondicionalLista
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
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn3 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLimpar = new FontAwesome.Sharp.IconButton();
            this.iconPesquisar = new FontAwesome.Sharp.IconButton();
            this.btnPesquisar = new Lunar.RJ_UI.Classes.RJButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioTodas = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radioFaturadas = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radioAbertas = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkAtivarData = new System.Windows.Forms.CheckBox();
            this.txtDataInicial = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataFinal = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtNumeroDocumento = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaCliente = new FontAwesome.Sharp.IconButton();
            this.txtCodCliente = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel15 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtRegistroPorPagina = new Lunar.RJ_UI.Classes.RJTextBox();
            this.sfDataPager1 = new Syncfusion.WinForms.DataPager.SfDataPager();
            this.txtCliente = new Lunar.RJ_UI.Classes.RJTextBox();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.btnExcluir = new Lunar.RJ_UI.Classes.RJButton();
            this.btnNovo = new Lunar.RJ_UI.Classes.RJButton();
            this.btnDevolver = new Lunar.RJ_UI.Classes.RJButton();
            this.btnImprimir = new Lunar.RJ_UI.Classes.RJButton();
            this.btnFaturar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnEnviarWhats = new Lunar.RJ_UI.Classes.RJButton();
            this.panelTitleBar.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioTodas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioFaturadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioAbertas)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1084, 44);
            this.panelTitleBar.TabIndex = 252;
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
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Multiply;
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClose.IconSize = 30;
            this.btnClose.Location = new System.Drawing.Point(1043, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLimpar);
            this.groupBox1.Controls.Add(this.iconPesquisar);
            this.groupBox1.Controls.Add(this.btnPesquisar);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.autoLabel5);
            this.groupBox1.Controls.Add(this.txtNumeroDocumento);
            this.groupBox1.Controls.Add(this.autoLabel2);
            this.groupBox1.Controls.Add(this.btnPesquisaCliente);
            this.groupBox1.Controls.Add(this.txtCodCliente);
            this.groupBox1.Controls.Add(this.autoLabel1);
            this.groupBox1.Controls.Add(this.autoLabel15);
            this.groupBox1.Controls.Add(this.txtRegistroPorPagina);
            this.groupBox1.Controls.Add(this.sfDataPager1);
            this.groupBox1.Controls.Add(this.txtCliente);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1084, 180);
            this.groupBox1.TabIndex = 253;
            this.groupBox1.TabStop = false;
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
            this.btnLimpar.Location = new System.Drawing.Point(689, 65);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(44, 36);
            this.btnLimpar.TabIndex = 245;
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
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
            this.iconPesquisar.Location = new System.Drawing.Point(692, 124);
            this.iconPesquisar.Name = "iconPesquisar";
            this.iconPesquisar.Size = new System.Drawing.Size(44, 36);
            this.iconPesquisar.TabIndex = 243;
            this.iconPesquisar.UseVisualStyleBackColor = false;
            this.iconPesquisar.Click += new System.EventHandler(this.iconPesquisar_Click);
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
            this.btnPesquisar.Location = new System.Drawing.Point(674, 109);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(77, 62);
            this.btnPesquisar.TabIndex = 242;
            this.btnPesquisar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioTodas);
            this.groupBox3.Controls.Add(this.radioFaturadas);
            this.groupBox3.Controls.Add(this.radioAbertas);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F);
            this.groupBox3.Location = new System.Drawing.Point(399, 121);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(248, 50);
            this.groupBox3.TabIndex = 241;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo de Condicional";
            // 
            // radioTodas
            // 
            this.radioTodas.BeforeTouchSize = new System.Drawing.Size(70, 21);
            this.radioTodas.Location = new System.Drawing.Point(178, 23);
            this.radioTodas.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioTodas.Name = "radioTodas";
            this.radioTodas.Size = new System.Drawing.Size(70, 21);
            this.radioTodas.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2007;
            this.radioTodas.TabIndex = 2;
            this.radioTodas.TabStop = false;
            this.radioTodas.Text = " Todas";
            this.radioTodas.ThemeName = "Office2007";
            this.radioTodas.CheckChanged += new System.EventHandler(this.radioTodas_CheckChanged);
            // 
            // radioFaturadas
            // 
            this.radioFaturadas.BeforeTouchSize = new System.Drawing.Size(93, 21);
            this.radioFaturadas.Location = new System.Drawing.Point(86, 22);
            this.radioFaturadas.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioFaturadas.Name = "radioFaturadas";
            this.radioFaturadas.Size = new System.Drawing.Size(93, 21);
            this.radioFaturadas.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2007;
            this.radioFaturadas.TabIndex = 1;
            this.radioFaturadas.TabStop = false;
            this.radioFaturadas.Text = " Faturadas";
            this.radioFaturadas.ThemeName = "Office2007";
            this.radioFaturadas.CheckChanged += new System.EventHandler(this.radioFaturadas_CheckChanged);
            // 
            // radioAbertas
            // 
            this.radioAbertas.BeforeTouchSize = new System.Drawing.Size(91, 21);
            this.radioAbertas.Checked = true;
            this.radioAbertas.Location = new System.Drawing.Point(6, 22);
            this.radioAbertas.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioAbertas.Name = "radioAbertas";
            this.radioAbertas.Size = new System.Drawing.Size(91, 21);
            this.radioAbertas.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2007;
            this.radioAbertas.TabIndex = 0;
            this.radioAbertas.Text = " Abertas";
            this.radioAbertas.ThemeName = "Office2007";
            this.radioAbertas.CheckChanged += new System.EventHandler(this.radioAbertas_CheckChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkAtivarData);
            this.groupBox2.Controls.Add(this.txtDataInicial);
            this.groupBox2.Controls.Add(this.autoLabel4);
            this.groupBox2.Controls.Add(this.txtDataFinal);
            this.groupBox2.Controls.Add(this.autoLabel3);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(7, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(385, 107);
            this.groupBox2.TabIndex = 238;
            this.groupBox2.TabStop = false;
            // 
            // chkAtivarData
            // 
            this.chkAtivarData.AutoSize = true;
            this.chkAtivarData.Location = new System.Drawing.Point(13, 13);
            this.chkAtivarData.Name = "chkAtivarData";
            this.chkAtivarData.Size = new System.Drawing.Size(189, 22);
            this.chkAtivarData.TabIndex = 229;
            this.chkAtivarData.Text = "Ativar Pesquisa por Data";
            this.chkAtivarData.UseVisualStyleBackColor = true;
            this.chkAtivarData.CheckedChanged += new System.EventHandler(this.chkAtivarData_CheckedChanged);
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataInicial.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataInicial.Enabled = false;
            this.txtDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDataInicial.Location = new System.Drawing.Point(13, 64);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Size = new System.Drawing.Size(179, 35);
            this.txtDataInicial.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.TabIndex = 224;
            this.txtDataInicial.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(198, 45);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(68, 16);
            this.autoLabel4.TabIndex = 228;
            this.autoLabel4.Text = "Data Final";
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataFinal.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataFinal.Enabled = false;
            this.txtDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Location = new System.Drawing.Point(198, 64);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Size = new System.Drawing.Size(174, 35);
            this.txtDataFinal.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataFinal.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.TabIndex = 225;
            this.txtDataFinal.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(13, 47);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(73, 16);
            this.autoLabel3.TabIndex = 227;
            this.autoLabel3.Text = "Data Inicial";
            // 
            // autoLabel5
            // 
            this.autoLabel5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.Black;
            this.autoLabel5.Location = new System.Drawing.Point(407, 65);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(96, 16);
            this.autoLabel5.TabIndex = 233;
            this.autoLabel5.Text = "Nº Condicional";
            // 
            // txtNumeroDocumento
            // 
            this.txtNumeroDocumento.BackColor = System.Drawing.Color.White;
            this.txtNumeroDocumento.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNumeroDocumento.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNumeroDocumento.BorderRadius = 8;
            this.txtNumeroDocumento.BorderSize = 2;
            this.txtNumeroDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroDocumento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNumeroDocumento.Location = new System.Drawing.Point(399, 77);
            this.txtNumeroDocumento.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumeroDocumento.Multiline = false;
            this.txtNumeroDocumento.Name = "txtNumeroDocumento";
            this.txtNumeroDocumento.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtNumeroDocumento.PasswordChar = false;
            this.txtNumeroDocumento.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtNumeroDocumento.PlaceholderText = "";
            this.txtNumeroDocumento.ReadOnly = false;
            this.txtNumeroDocumento.Size = new System.Drawing.Size(248, 37);
            this.txtNumeroDocumento.TabIndex = 232;
            this.txtNumeroDocumento.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNumeroDocumento.Texts = "";
            this.txtNumeroDocumento.UnderlinedStyle = false;
            this.txtNumeroDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroDocumento_KeyPress);
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(450, 8);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(51, 16);
            this.autoLabel2.TabIndex = 231;
            this.autoLabel2.Text = "Código";
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
            this.btnPesquisaCliente.Location = new System.Drawing.Point(399, 23);
            this.btnPesquisaCliente.Name = "btnPesquisaCliente";
            this.btnPesquisaCliente.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaCliente.TabIndex = 230;
            this.btnPesquisaCliente.UseVisualStyleBackColor = true;
            this.btnPesquisaCliente.Click += new System.EventHandler(this.btnPesquisaCliente_Click);
            // 
            // txtCodCliente
            // 
            this.txtCodCliente.BackColor = System.Drawing.Color.White;
            this.txtCodCliente.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodCliente.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodCliente.BorderRadius = 8;
            this.txtCodCliente.BorderSize = 2;
            this.txtCodCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodCliente.Location = new System.Drawing.Point(442, 20);
            this.txtCodCliente.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodCliente.Multiline = false;
            this.txtCodCliente.Name = "txtCodCliente";
            this.txtCodCliente.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodCliente.PasswordChar = false;
            this.txtCodCliente.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodCliente.PlaceholderText = "";
            this.txtCodCliente.ReadOnly = false;
            this.txtCodCliente.Size = new System.Drawing.Size(89, 37);
            this.txtCodCliente.TabIndex = 229;
            this.txtCodCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodCliente.Texts = "";
            this.txtCodCliente.UnderlinedStyle = false;
            this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodCliente_KeyPress);
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(19, 8);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(122, 16);
            this.autoLabel1.TabIndex = 211;
            this.autoLabel1.Text = "Cliente/Fornecedor";
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
            // txtCliente
            // 
            this.txtCliente.BackColor = System.Drawing.Color.White;
            this.txtCliente.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCliente.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCliente.BorderRadius = 8;
            this.txtCliente.BorderSize = 2;
            this.txtCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCliente.Location = new System.Drawing.Point(7, 20);
            this.txtCliente.Margin = new System.Windows.Forms.Padding(4);
            this.txtCliente.Multiline = false;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCliente.PasswordChar = false;
            this.txtCliente.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCliente.PlaceholderText = "";
            this.txtCliente.ReadOnly = false;
            this.txtCliente.Size = new System.Drawing.Size(385, 37);
            this.txtCliente.TabIndex = 153;
            this.txtCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCliente.Texts = "";
            this.txtCliente.UnderlinedStyle = false;
            this.txtCliente.Click += new System.EventHandler(this.txtCliente_Click);
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
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
            gridTextColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn1.HeaderStyle.Font.Size = 10F;
            gridTextColumn1.HeaderText = "Nº Condicional";
            gridTextColumn1.MappingName = "Id";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.ColumnHeader;
            gridTextColumn2.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn2.Format = "dd/MM/yyyy";
            gridTextColumn2.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn2.HeaderStyle.Font.Size = 10F;
            gridTextColumn2.HeaderStyle.VerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            gridTextColumn2.HeaderText = "Emissão";
            gridTextColumn2.MappingName = "Data";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.AllowResizing = true;
            gridTextColumn3.AllowSorting = false;
            gridTextColumn3.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.ColumnHeader;
            gridTextColumn3.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn3.CellStyle.Font.Size = 12F;
            gridTextColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn3.HeaderStyle.Font.Size = 10F;
            gridTextColumn3.HeaderText = "Cliente";
            gridTextColumn3.MappingName = "Cliente.RazaoSocial";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.AllowResizing = true;
            gridTextColumn4.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn4.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn4.CellStyle.Font.Size = 12F;
            gridTextColumn4.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn4.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn4.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn4.HeaderStyle.Font.Size = 10F;
            gridTextColumn4.HeaderText = "Peças Total";
            gridTextColumn4.MappingName = "QtdPeca";
            gridNumericColumn1.AllowEditing = false;
            gridNumericColumn1.AllowFiltering = true;
            gridNumericColumn1.AllowResizing = true;
            gridNumericColumn1.CellStyle.Font.Size = 12F;
            gridNumericColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridNumericColumn1.Format = "N2";
            gridNumericColumn1.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn1.HeaderStyle.Font.Size = 10F;
            gridNumericColumn1.HeaderText = "Vr. Total";
            gridNumericColumn1.MappingName = "ValorTotal";
            gridNumericColumn2.AllowEditing = false;
            gridNumericColumn2.AllowFiltering = true;
            gridNumericColumn2.AllowResizing = true;
            gridNumericColumn2.CellStyle.Font.Size = 12F;
            gridNumericColumn2.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridNumericColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn2.HeaderStyle.Font.Facename = "montserrat";
            gridNumericColumn2.HeaderStyle.Font.Size = 10F;
            gridNumericColumn2.HeaderText = "Qtd Dev.";
            gridNumericColumn2.MappingName = "QuantidadeDevolvida";
            gridNumericColumn3.AllowEditing = false;
            gridNumericColumn3.AllowFiltering = true;
            gridNumericColumn3.AllowResizing = true;
            gridNumericColumn3.CellStyle.Font.Size = 12F;
            gridNumericColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridNumericColumn3.Format = "N2";
            gridNumericColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn3.HeaderStyle.Font.Facename = "montserrat";
            gridNumericColumn3.HeaderStyle.Font.Size = 10F;
            gridNumericColumn3.HeaderText = "Saldo";
            gridNumericColumn3.MappingName = "ValorSaldo";
            this.grid.Columns.Add(gridTextColumn1);
            this.grid.Columns.Add(gridTextColumn2);
            this.grid.Columns.Add(gridTextColumn3);
            this.grid.Columns.Add(gridTextColumn4);
            this.grid.Columns.Add(gridNumericColumn1);
            this.grid.Columns.Add(gridNumericColumn2);
            this.grid.Columns.Add(gridNumericColumn3);
            this.grid.Location = new System.Drawing.Point(12, 240);
            this.grid.Name = "grid";
            this.grid.SelectionMode = Syncfusion.WinForms.DataGrid.Enums.GridSelectionMode.Extended;
            this.grid.Size = new System.Drawing.Size(1063, 92);
            this.grid.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.grid.Style.HeaderStyle.Font.Size = 12F;
            this.grid.TabIndex = 254;
            this.grid.Text = "Grid Parcelas";
            this.grid.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.grid_QueryRowStyle);
            this.grid.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.grid_CellDoubleClick);
            this.grid.Click += new System.EventHandler(this.grid_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcluir.BackColor = System.Drawing.Color.White;
            this.btnExcluir.BackgroundColor = System.Drawing.Color.White;
            this.btnExcluir.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExcluir.BorderRadius = 8;
            this.btnExcluir.BorderSize = 2;
            this.btnExcluir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluir.FlatAppearance.BorderSize = 0;
            this.btnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.ForeColor = System.Drawing.Color.Black;
            this.btnExcluir.Location = new System.Drawing.Point(270, 338);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(193, 45);
            this.btnExcluir.TabIndex = 257;
            this.btnExcluir.Text = "Excluir Condicional";
            this.btnExcluir.TextColor = System.Drawing.Color.Black;
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
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
            this.btnNovo.ForeColor = System.Drawing.Color.Transparent;
            this.btnNovo.Location = new System.Drawing.Point(873, 338);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(199, 45);
            this.btnNovo.TabIndex = 256;
            this.btnNovo.Text = "Nova Condicional [F5]";
            this.btnNovo.TextColor = System.Drawing.Color.Transparent;
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnDevolver
            // 
            this.btnDevolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDevolver.BackColor = System.Drawing.Color.White;
            this.btnDevolver.BackgroundColor = System.Drawing.Color.White;
            this.btnDevolver.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnDevolver.BorderRadius = 8;
            this.btnDevolver.BorderSize = 2;
            this.btnDevolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDevolver.FlatAppearance.BorderSize = 0;
            this.btnDevolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDevolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDevolver.ForeColor = System.Drawing.Color.Black;
            this.btnDevolver.Location = new System.Drawing.Point(668, 338);
            this.btnDevolver.Name = "btnDevolver";
            this.btnDevolver.Size = new System.Drawing.Size(199, 45);
            this.btnDevolver.TabIndex = 255;
            this.btnDevolver.Text = "Devolver Condicional";
            this.btnDevolver.TextColor = System.Drawing.Color.Black;
            this.btnDevolver.UseVisualStyleBackColor = false;
            this.btnDevolver.Click += new System.EventHandler(this.btnDevolver_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.BackColor = System.Drawing.Color.White;
            this.btnImprimir.BackgroundColor = System.Drawing.Color.White;
            this.btnImprimir.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnImprimir.BorderRadius = 8;
            this.btnImprimir.BorderSize = 2;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.Black;
            this.btnImprimir.Location = new System.Drawing.Point(71, 338);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(193, 45);
            this.btnImprimir.TabIndex = 259;
            this.btnImprimir.Text = "Imprimir Condicional";
            this.btnImprimir.TextColor = System.Drawing.Color.Black;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnFaturar
            // 
            this.btnFaturar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFaturar.BackColor = System.Drawing.Color.White;
            this.btnFaturar.BackgroundColor = System.Drawing.Color.White;
            this.btnFaturar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnFaturar.BorderRadius = 8;
            this.btnFaturar.BorderSize = 2;
            this.btnFaturar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaturar.FlatAppearance.BorderSize = 0;
            this.btnFaturar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaturar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaturar.ForeColor = System.Drawing.Color.Black;
            this.btnFaturar.Location = new System.Drawing.Point(469, 338);
            this.btnFaturar.Name = "btnFaturar";
            this.btnFaturar.Size = new System.Drawing.Size(193, 45);
            this.btnFaturar.TabIndex = 260;
            this.btnFaturar.Text = "Faturar (Venda)";
            this.btnFaturar.TextColor = System.Drawing.Color.Black;
            this.btnFaturar.UseVisualStyleBackColor = false;
            this.btnFaturar.Click += new System.EventHandler(this.btnFaturar_Click);
            // 
            // btnEnviarWhats
            // 
            this.btnEnviarWhats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnviarWhats.BackColor = System.Drawing.Color.White;
            this.btnEnviarWhats.BackgroundColor = System.Drawing.Color.White;
            this.btnEnviarWhats.BackgroundImage = global::Lunar.Properties.Resources.whatsapp_logo_icone;
            this.btnEnviarWhats.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEnviarWhats.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnEnviarWhats.BorderRadius = 8;
            this.btnEnviarWhats.BorderSize = 2;
            this.btnEnviarWhats.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviarWhats.FlatAppearance.BorderSize = 0;
            this.btnEnviarWhats.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEnviarWhats.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEnviarWhats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarWhats.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarWhats.ForeColor = System.Drawing.Color.Black;
            this.btnEnviarWhats.Location = new System.Drawing.Point(30, 343);
            this.btnEnviarWhats.Name = "btnEnviarWhats";
            this.btnEnviarWhats.Size = new System.Drawing.Size(35, 37);
            this.btnEnviarWhats.TabIndex = 261;
            this.btnEnviarWhats.TextColor = System.Drawing.Color.Black;
            this.btnEnviarWhats.UseVisualStyleBackColor = false;
            this.btnEnviarWhats.Click += new System.EventHandler(this.btnEnviarWhats_Click);
            // 
            // FrmCondicionalLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 392);
            this.Controls.Add(this.btnEnviarWhats);
            this.Controls.Add(this.btnFaturar);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.btnDevolver);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelTitleBar);
            this.Name = "FrmCondicionalLista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Condicionais";
            this.Load += new System.EventHandler(this.FrmCondicionalLista_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCondicionalLista_KeyDown);
            this.panelTitleBar.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioTodas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioFaturadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioAbertas)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private FontAwesome.Sharp.IconButton btnLimpar;
        private FontAwesome.Sharp.IconButton iconPesquisar;
        private RJ_UI.Classes.RJButton btnPesquisar;
        private System.Windows.Forms.GroupBox groupBox3;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioFaturadas;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioAbertas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkAtivarData;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataInicial;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataFinal;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private RJ_UI.Classes.RJTextBox txtNumeroDocumento;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private FontAwesome.Sharp.IconButton btnPesquisaCliente;
        private RJ_UI.Classes.RJTextBox txtCodCliente;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel15;
        private RJ_UI.Classes.RJTextBox txtRegistroPorPagina;
        private Syncfusion.WinForms.DataPager.SfDataPager sfDataPager1;
        private RJ_UI.Classes.RJTextBox txtCliente;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
        private RJ_UI.Classes.RJButton btnExcluir;
        private RJ_UI.Classes.RJButton btnNovo;
        private RJ_UI.Classes.RJButton btnDevolver;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioTodas;
        private RJ_UI.Classes.RJButton btnImprimir;
        private RJ_UI.Classes.RJButton btnFaturar;
        private RJ_UI.Classes.RJButton btnEnviarWhats;
    }
}