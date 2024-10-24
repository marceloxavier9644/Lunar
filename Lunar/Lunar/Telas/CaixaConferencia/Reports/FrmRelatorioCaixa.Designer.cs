﻿namespace Lunar.Telas.CaixaConferencia.Reports
{
    partial class FrmRelatorioCaixa
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel25 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnFechar = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.iconPesquisar = new FontAwesome.Sharp.IconButton();
            this.autoLabel7 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaEmpresa = new FontAwesome.Sharp.IconButton();
            this.autoLabel8 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaPlanoConta = new FontAwesome.Sharp.IconButton();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataInicial = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataFinal = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaUsuario = new FontAwesome.Sharp.IconButton();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dsCaixa = new Lunar.Telas.CaixaConferencia.Reports.Dados.dsCaixa();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnPesquisaContaBancaria = new FontAwesome.Sharp.IconButton();
            this.autoLabel9 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel10 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCodContaBancaria = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtContaBancaria = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnPesquisar = new Lunar.RJ_UI.Classes.RJButton();
            this.txtCodEmpresa = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtEmpresa = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodPlanoConta = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtPlanoConta = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodUsuario = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtUsuario = new Lunar.RJ_UI.Classes.RJTextBox();
            this.panel1.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsCaixa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelTitleBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 40);
            this.panel1.TabIndex = 0;
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel25);
            this.panelTitleBar.Controls.Add(this.btnFechar);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1024, 38);
            this.panelTitleBar.TabIndex = 259;
            // 
            // autoLabel25
            // 
            this.autoLabel25.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel25.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel25.ForeColor = System.Drawing.Color.White;
            this.autoLabel25.Location = new System.Drawing.Point(12, 0);
            this.autoLabel25.Name = "autoLabel25";
            this.autoLabel25.Size = new System.Drawing.Size(70, 35);
            this.autoLabel25.TabIndex = 198;
            this.autoLabel25.Text = "Caixa";
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.BackColor = System.Drawing.Color.Transparent;
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnFechar.IconColor = System.Drawing.Color.White;
            this.btnFechar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFechar.IconSize = 30;
            this.btnFechar.Location = new System.Drawing.Point(983, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(39, 41);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.reportViewer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1024, 650);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.autoLabel10);
            this.panel3.Controls.Add(this.txtCodContaBancaria);
            this.panel3.Controls.Add(this.btnPesquisaContaBancaria);
            this.panel3.Controls.Add(this.autoLabel9);
            this.panel3.Controls.Add(this.txtContaBancaria);
            this.panel3.Controls.Add(this.iconPesquisar);
            this.panel3.Controls.Add(this.btnPesquisar);
            this.panel3.Controls.Add(this.autoLabel7);
            this.panel3.Controls.Add(this.btnPesquisaEmpresa);
            this.panel3.Controls.Add(this.txtCodEmpresa);
            this.panel3.Controls.Add(this.autoLabel8);
            this.panel3.Controls.Add(this.txtEmpresa);
            this.panel3.Controls.Add(this.autoLabel5);
            this.panel3.Controls.Add(this.btnPesquisaPlanoConta);
            this.panel3.Controls.Add(this.txtCodPlanoConta);
            this.panel3.Controls.Add(this.autoLabel6);
            this.panel3.Controls.Add(this.txtPlanoConta);
            this.panel3.Controls.Add(this.txtDataInicial);
            this.panel3.Controls.Add(this.autoLabel4);
            this.panel3.Controls.Add(this.txtDataFinal);
            this.panel3.Controls.Add(this.autoLabel3);
            this.panel3.Controls.Add(this.autoLabel2);
            this.panel3.Controls.Add(this.btnPesquisaUsuario);
            this.panel3.Controls.Add(this.txtCodUsuario);
            this.panel3.Controls.Add(this.autoLabel1);
            this.panel3.Controls.Add(this.txtUsuario);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1024, 141);
            this.panel3.TabIndex = 1;
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
            this.iconPesquisar.Location = new System.Drawing.Point(824, 77);
            this.iconPesquisar.Name = "iconPesquisar";
            this.iconPesquisar.Size = new System.Drawing.Size(44, 36);
            this.iconPesquisar.TabIndex = 252;
            this.iconPesquisar.UseVisualStyleBackColor = false;
            this.iconPesquisar.Click += new System.EventHandler(this.iconPesquisar_Click);
            // 
            // autoLabel7
            // 
            this.autoLabel7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel7.ForeColor = System.Drawing.Color.Black;
            this.autoLabel7.Location = new System.Drawing.Point(718, 75);
            this.autoLabel7.Name = "autoLabel7";
            this.autoLabel7.Size = new System.Drawing.Size(51, 16);
            this.autoLabel7.TabIndex = 250;
            this.autoLabel7.Text = "Código";
            // 
            // btnPesquisaEmpresa
            // 
            this.btnPesquisaEmpresa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaEmpresa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaEmpresa.FlatAppearance.BorderSize = 0;
            this.btnPesquisaEmpresa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaEmpresa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaEmpresa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaEmpresa.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaEmpresa.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaEmpresa.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaEmpresa.IconSize = 38;
            this.btnPesquisaEmpresa.Location = new System.Drawing.Point(667, 90);
            this.btnPesquisaEmpresa.Name = "btnPesquisaEmpresa";
            this.btnPesquisaEmpresa.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaEmpresa.TabIndex = 249;
            this.btnPesquisaEmpresa.UseVisualStyleBackColor = true;
            this.btnPesquisaEmpresa.Click += new System.EventHandler(this.btnPesquisaEmpresa_Click);
            // 
            // autoLabel8
            // 
            this.autoLabel8.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel8.ForeColor = System.Drawing.Color.Black;
            this.autoLabel8.Location = new System.Drawing.Point(387, 75);
            this.autoLabel8.Name = "autoLabel8";
            this.autoLabel8.Size = new System.Drawing.Size(62, 16);
            this.autoLabel8.TabIndex = 247;
            this.autoLabel8.Text = "Empresa";
            // 
            // autoLabel5
            // 
            this.autoLabel5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.Black;
            this.autoLabel5.Location = new System.Drawing.Point(718, 8);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(51, 16);
            this.autoLabel5.TabIndex = 245;
            this.autoLabel5.Text = "Código";
            // 
            // btnPesquisaPlanoConta
            // 
            this.btnPesquisaPlanoConta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaPlanoConta.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaPlanoConta.FlatAppearance.BorderSize = 0;
            this.btnPesquisaPlanoConta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPlanoConta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPlanoConta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaPlanoConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaPlanoConta.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaPlanoConta.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaPlanoConta.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaPlanoConta.IconSize = 38;
            this.btnPesquisaPlanoConta.Location = new System.Drawing.Point(667, 23);
            this.btnPesquisaPlanoConta.Name = "btnPesquisaPlanoConta";
            this.btnPesquisaPlanoConta.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaPlanoConta.TabIndex = 244;
            this.btnPesquisaPlanoConta.UseVisualStyleBackColor = true;
            this.btnPesquisaPlanoConta.Click += new System.EventHandler(this.btnPesquisaPlanoConta_Click);
            // 
            // autoLabel6
            // 
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(420, 8);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(106, 16);
            this.autoLabel6.TabIndex = 242;
            this.autoLabel6.Text = "Plano de Contas";
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataInicial.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDataInicial.Location = new System.Drawing.Point(9, 89);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Size = new System.Drawing.Size(179, 35);
            this.txtDataInicial.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.TabIndex = 237;
            this.txtDataInicial.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(194, 70);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(105, 16);
            this.autoLabel4.TabIndex = 240;
            this.autoLabel4.Text = "Data Caixa Final";
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataFinal.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Location = new System.Drawing.Point(194, 89);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Size = new System.Drawing.Size(174, 35);
            this.txtDataFinal.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataFinal.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.TabIndex = 238;
            this.txtDataFinal.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(9, 72);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(110, 16);
            this.autoLabel3.TabIndex = 239;
            this.autoLabel3.Text = "Data Caixa Inicial";
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(319, 8);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(51, 16);
            this.autoLabel2.TabIndex = 236;
            this.autoLabel2.Text = "Código";
            // 
            // btnPesquisaUsuario
            // 
            this.btnPesquisaUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaUsuario.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaUsuario.FlatAppearance.BorderSize = 0;
            this.btnPesquisaUsuario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaUsuario.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaUsuario.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaUsuario.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaUsuario.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaUsuario.IconSize = 38;
            this.btnPesquisaUsuario.Location = new System.Drawing.Point(268, 23);
            this.btnPesquisaUsuario.Name = "btnPesquisaUsuario";
            this.btnPesquisaUsuario.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaUsuario.TabIndex = 235;
            this.btnPesquisaUsuario.UseVisualStyleBackColor = true;
            this.btnPesquisaUsuario.Click += new System.EventHandler(this.btnPesquisaUsuario_Click);
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(21, 8);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(54, 16);
            this.autoLabel1.TabIndex = 233;
            this.autoLabel1.Text = "Usuário";
            // 
            // reportViewer1
            // 
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Lunar.Telas.CaixaConferencia.Reports.RelatorioCaixa01.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 147);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1018, 491);
            this.reportViewer1.TabIndex = 0;
            // 
            // dsCaixa
            // 
            this.dsCaixa.DataSetName = "dsCaixa";
            this.dsCaixa.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "Caixa";
            this.bindingSource1.DataSource = this.dsCaixa;
            // 
            // btnPesquisaContaBancaria
            // 
            this.btnPesquisaContaBancaria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaContaBancaria.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaContaBancaria.FlatAppearance.BorderSize = 0;
            this.btnPesquisaContaBancaria.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaContaBancaria.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaContaBancaria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaContaBancaria.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaContaBancaria.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaContaBancaria.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaContaBancaria.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaContaBancaria.IconSize = 38;
            this.btnPesquisaContaBancaria.Location = new System.Drawing.Point(979, 23);
            this.btnPesquisaContaBancaria.Name = "btnPesquisaContaBancaria";
            this.btnPesquisaContaBancaria.Size = new System.Drawing.Size(40, 34);
            this.btnPesquisaContaBancaria.TabIndex = 255;
            this.btnPesquisaContaBancaria.UseVisualStyleBackColor = true;
            this.btnPesquisaContaBancaria.Click += new System.EventHandler(this.btnPesquisaContaBancaria_Click);
            // 
            // autoLabel9
            // 
            this.autoLabel9.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel9.ForeColor = System.Drawing.Color.Black;
            this.autoLabel9.Location = new System.Drawing.Point(819, 8);
            this.autoLabel9.Name = "autoLabel9";
            this.autoLabel9.Size = new System.Drawing.Size(99, 16);
            this.autoLabel9.TabIndex = 254;
            this.autoLabel9.Text = "Conta Bancária";
            // 
            // autoLabel10
            // 
            this.autoLabel10.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel10.ForeColor = System.Drawing.Color.Black;
            this.autoLabel10.Location = new System.Drawing.Point(930, 58);
            this.autoLabel10.Name = "autoLabel10";
            this.autoLabel10.Size = new System.Drawing.Size(51, 16);
            this.autoLabel10.TabIndex = 257;
            this.autoLabel10.Text = "Código";
            this.autoLabel10.Visible = false;
            // 
            // txtCodContaBancaria
            // 
            this.txtCodContaBancaria.BackColor = System.Drawing.Color.White;
            this.txtCodContaBancaria.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodContaBancaria.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodContaBancaria.BorderRadius = 8;
            this.txtCodContaBancaria.BorderSize = 2;
            this.txtCodContaBancaria.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodContaBancaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodContaBancaria.Location = new System.Drawing.Point(922, 70);
            this.txtCodContaBancaria.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodContaBancaria.Multiline = false;
            this.txtCodContaBancaria.Name = "txtCodContaBancaria";
            this.txtCodContaBancaria.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodContaBancaria.PasswordChar = false;
            this.txtCodContaBancaria.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodContaBancaria.PlaceholderText = "";
            this.txtCodContaBancaria.ReadOnly = false;
            this.txtCodContaBancaria.Size = new System.Drawing.Size(89, 37);
            this.txtCodContaBancaria.TabIndex = 256;
            this.txtCodContaBancaria.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodContaBancaria.Texts = "";
            this.txtCodContaBancaria.UnderlinedStyle = false;
            this.txtCodContaBancaria.Visible = false;
            // 
            // txtContaBancaria
            // 
            this.txtContaBancaria.BackColor = System.Drawing.Color.White;
            this.txtContaBancaria.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtContaBancaria.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtContaBancaria.BorderRadius = 8;
            this.txtContaBancaria.BorderSize = 2;
            this.txtContaBancaria.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContaBancaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtContaBancaria.Location = new System.Drawing.Point(807, 20);
            this.txtContaBancaria.Margin = new System.Windows.Forms.Padding(4);
            this.txtContaBancaria.Multiline = false;
            this.txtContaBancaria.Name = "txtContaBancaria";
            this.txtContaBancaria.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtContaBancaria.PasswordChar = false;
            this.txtContaBancaria.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtContaBancaria.PlaceholderText = "";
            this.txtContaBancaria.ReadOnly = false;
            this.txtContaBancaria.Size = new System.Drawing.Size(165, 37);
            this.txtContaBancaria.TabIndex = 253;
            this.txtContaBancaria.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtContaBancaria.Texts = "";
            this.txtContaBancaria.UnderlinedStyle = false;
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
            this.btnPesquisar.Location = new System.Drawing.Point(806, 62);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(77, 62);
            this.btnPesquisar.TabIndex = 251;
            this.btnPesquisar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // txtCodEmpresa
            // 
            this.txtCodEmpresa.BackColor = System.Drawing.Color.White;
            this.txtCodEmpresa.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodEmpresa.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodEmpresa.BorderRadius = 8;
            this.txtCodEmpresa.BorderSize = 2;
            this.txtCodEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodEmpresa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodEmpresa.Location = new System.Drawing.Point(710, 87);
            this.txtCodEmpresa.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodEmpresa.Multiline = false;
            this.txtCodEmpresa.Name = "txtCodEmpresa";
            this.txtCodEmpresa.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodEmpresa.PasswordChar = false;
            this.txtCodEmpresa.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodEmpresa.PlaceholderText = "";
            this.txtCodEmpresa.ReadOnly = false;
            this.txtCodEmpresa.Size = new System.Drawing.Size(89, 37);
            this.txtCodEmpresa.TabIndex = 248;
            this.txtCodEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodEmpresa.Texts = "";
            this.txtCodEmpresa.UnderlinedStyle = false;
            this.txtCodEmpresa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodEmpresa_KeyPress);
            // 
            // txtEmpresa
            // 
            this.txtEmpresa.BackColor = System.Drawing.Color.White;
            this.txtEmpresa.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEmpresa.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEmpresa.BorderRadius = 8;
            this.txtEmpresa.BorderSize = 2;
            this.txtEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpresa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEmpresa.Location = new System.Drawing.Point(375, 87);
            this.txtEmpresa.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmpresa.Multiline = false;
            this.txtEmpresa.Name = "txtEmpresa";
            this.txtEmpresa.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtEmpresa.PasswordChar = false;
            this.txtEmpresa.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtEmpresa.PlaceholderText = "";
            this.txtEmpresa.ReadOnly = false;
            this.txtEmpresa.Size = new System.Drawing.Size(285, 37);
            this.txtEmpresa.TabIndex = 246;
            this.txtEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEmpresa.Texts = "";
            this.txtEmpresa.UnderlinedStyle = false;
            this.txtEmpresa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpresa_KeyPress);
            // 
            // txtCodPlanoConta
            // 
            this.txtCodPlanoConta.BackColor = System.Drawing.Color.White;
            this.txtCodPlanoConta.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodPlanoConta.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodPlanoConta.BorderRadius = 8;
            this.txtCodPlanoConta.BorderSize = 2;
            this.txtCodPlanoConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodPlanoConta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodPlanoConta.Location = new System.Drawing.Point(710, 20);
            this.txtCodPlanoConta.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodPlanoConta.Multiline = false;
            this.txtCodPlanoConta.Name = "txtCodPlanoConta";
            this.txtCodPlanoConta.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodPlanoConta.PasswordChar = false;
            this.txtCodPlanoConta.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodPlanoConta.PlaceholderText = "";
            this.txtCodPlanoConta.ReadOnly = false;
            this.txtCodPlanoConta.Size = new System.Drawing.Size(89, 37);
            this.txtCodPlanoConta.TabIndex = 243;
            this.txtCodPlanoConta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodPlanoConta.Texts = "";
            this.txtCodPlanoConta.UnderlinedStyle = false;
            // 
            // txtPlanoConta
            // 
            this.txtPlanoConta.BackColor = System.Drawing.Color.White;
            this.txtPlanoConta.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPlanoConta.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPlanoConta.BorderRadius = 8;
            this.txtPlanoConta.BorderSize = 2;
            this.txtPlanoConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlanoConta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPlanoConta.Location = new System.Drawing.Point(408, 20);
            this.txtPlanoConta.Margin = new System.Windows.Forms.Padding(4);
            this.txtPlanoConta.Multiline = false;
            this.txtPlanoConta.Name = "txtPlanoConta";
            this.txtPlanoConta.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtPlanoConta.PasswordChar = false;
            this.txtPlanoConta.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtPlanoConta.PlaceholderText = "";
            this.txtPlanoConta.ReadOnly = false;
            this.txtPlanoConta.Size = new System.Drawing.Size(252, 37);
            this.txtPlanoConta.TabIndex = 241;
            this.txtPlanoConta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPlanoConta.Texts = "";
            this.txtPlanoConta.UnderlinedStyle = false;
            // 
            // txtCodUsuario
            // 
            this.txtCodUsuario.BackColor = System.Drawing.Color.White;
            this.txtCodUsuario.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodUsuario.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodUsuario.BorderRadius = 8;
            this.txtCodUsuario.BorderSize = 2;
            this.txtCodUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodUsuario.Location = new System.Drawing.Point(311, 20);
            this.txtCodUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodUsuario.Multiline = false;
            this.txtCodUsuario.Name = "txtCodUsuario";
            this.txtCodUsuario.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodUsuario.PasswordChar = false;
            this.txtCodUsuario.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodUsuario.PlaceholderText = "";
            this.txtCodUsuario.ReadOnly = false;
            this.txtCodUsuario.Size = new System.Drawing.Size(89, 37);
            this.txtCodUsuario.TabIndex = 234;
            this.txtCodUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodUsuario.Texts = "";
            this.txtCodUsuario.UnderlinedStyle = false;
            this.txtCodUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodUsuario_KeyPress);
            // 
            // txtUsuario
            // 
            this.txtUsuario.BackColor = System.Drawing.Color.White;
            this.txtUsuario.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtUsuario.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtUsuario.BorderRadius = 8;
            this.txtUsuario.BorderSize = 2;
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtUsuario.Location = new System.Drawing.Point(9, 20);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsuario.Multiline = false;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtUsuario.PasswordChar = false;
            this.txtUsuario.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtUsuario.PlaceholderText = "";
            this.txtUsuario.ReadOnly = false;
            this.txtUsuario.Size = new System.Drawing.Size(252, 37);
            this.txtUsuario.TabIndex = 232;
            this.txtUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUsuario.Texts = "";
            this.txtUsuario.UnderlinedStyle = false;
            this.txtUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsuario_KeyPress);
            // 
            // FrmRelatorioCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 690);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmRelatorioCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Caixa";
            this.Load += new System.EventHandler(this.FrmRelatorioCaixa_Load);
            this.panel1.ResumeLayout(false);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsCaixa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel25;
        private FontAwesome.Sharp.IconButton btnFechar;

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private CaixaConferencia.Reports.Dados.dsCaixa dsCaixa;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Panel panel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private FontAwesome.Sharp.IconButton btnPesquisaUsuario;
        private RJ_UI.Classes.RJTextBox txtCodUsuario;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtUsuario;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataInicial;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataFinal;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private FontAwesome.Sharp.IconButton btnPesquisaPlanoConta;
        private RJ_UI.Classes.RJTextBox txtCodPlanoConta;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private RJ_UI.Classes.RJTextBox txtPlanoConta;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel7;
        private FontAwesome.Sharp.IconButton btnPesquisaEmpresa;
        private RJ_UI.Classes.RJTextBox txtCodEmpresa;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel8;
        private RJ_UI.Classes.RJTextBox txtEmpresa;
        private RJ_UI.Classes.RJButton btnPesquisar;
        private FontAwesome.Sharp.IconButton iconPesquisar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel10;
        private RJ_UI.Classes.RJTextBox txtCodContaBancaria;
        private FontAwesome.Sharp.IconButton btnPesquisaContaBancaria;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel9;
        private RJ_UI.Classes.RJTextBox txtContaBancaria;
    }
}