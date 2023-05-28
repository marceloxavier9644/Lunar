namespace Lunar.Telas.CaixaConferencia
{
    partial class FrmLancamentoCaixa
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
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel25 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnFechar = new FontAwesome.Sharp.IconButton();
            this.tabControlAdv1 = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            this.tabPageAdv1 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaPlanoConta = new FontAwesome.Sharp.IconButton();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaConta = new FontAwesome.Sharp.IconButton();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataMovimento = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel13 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel14 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel15 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaUsuario1 = new FontAwesome.Sharp.IconButton();
            this.autoLabel16 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCodPlanoConta = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtPlanoContas = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
            this.txtDescricaoResumida = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodConta = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtConta = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtValor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodUsuario = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtUsuario = new Lunar.RJ_UI.Classes.RJTextBox();
            this.radioPix = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radioDeposito = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radioDinheiro = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).BeginInit();
            this.tabControlAdv1.SuspendLayout();
            this.tabPageAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioPix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioDeposito)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioDinheiro)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel25);
            this.panelTitleBar.Controls.Add(this.btnFechar);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(800, 38);
            this.panelTitleBar.TabIndex = 257;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // autoLabel25
            // 
            this.autoLabel25.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel25.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel25.ForeColor = System.Drawing.Color.White;
            this.autoLabel25.Location = new System.Drawing.Point(5, 1);
            this.autoLabel25.Name = "autoLabel25";
            this.autoLabel25.Size = new System.Drawing.Size(237, 35);
            this.autoLabel25.TabIndex = 198;
            this.autoLabel25.Text = "Lançamento de Caixa";
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
            this.btnFechar.IconChar = FontAwesome.Sharp.IconChar.Multiply;
            this.btnFechar.IconColor = System.Drawing.Color.White;
            this.btnFechar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFechar.IconSize = 30;
            this.btnFechar.Location = new System.Drawing.Point(759, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(39, 41);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // tabControlAdv1
            // 
            this.tabControlAdv1.ActiveTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tabControlAdv1.BeforeTouchSize = new System.Drawing.Size(800, 321);
            this.tabControlAdv1.Controls.Add(this.tabPageAdv1);
            this.tabControlAdv1.FocusOnTabClick = false;
            this.tabControlAdv1.Location = new System.Drawing.Point(0, 44);
            this.tabControlAdv1.Name = "tabControlAdv1";
            this.tabControlAdv1.Size = new System.Drawing.Size(800, 321);
            this.tabControlAdv1.TabIndex = 258;
            this.tabControlAdv1.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererOffice2016White);
            this.tabControlAdv1.ThemeName = "TabRendererOffice2016White";
            this.tabControlAdv1.ThemeStyle.PrimitiveButtonStyle.DisabledNextPageImage = null;
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageAdv1.Controls.Add(this.radioDinheiro);
            this.tabPageAdv1.Controls.Add(this.radioDeposito);
            this.tabPageAdv1.Controls.Add(this.radioPix);
            this.tabPageAdv1.Controls.Add(this.autoLabel5);
            this.tabPageAdv1.Controls.Add(this.btnPesquisaPlanoConta);
            this.tabPageAdv1.Controls.Add(this.autoLabel6);
            this.tabPageAdv1.Controls.Add(this.autoLabel2);
            this.tabPageAdv1.Controls.Add(this.autoLabel3);
            this.tabPageAdv1.Controls.Add(this.btnPesquisaConta);
            this.tabPageAdv1.Controls.Add(this.autoLabel4);
            this.tabPageAdv1.Controls.Add(this.txtDataMovimento);
            this.tabPageAdv1.Controls.Add(this.autoLabel13);
            this.tabPageAdv1.Controls.Add(this.autoLabel14);
            this.tabPageAdv1.Controls.Add(this.autoLabel15);
            this.tabPageAdv1.Controls.Add(this.btnPesquisaUsuario1);
            this.tabPageAdv1.Controls.Add(this.autoLabel16);
            this.tabPageAdv1.Controls.Add(this.txtCodPlanoConta);
            this.tabPageAdv1.Controls.Add(this.txtPlanoContas);
            this.tabPageAdv1.Controls.Add(this.btnConfirmar);
            this.tabPageAdv1.Controls.Add(this.txtDescricaoResumida);
            this.tabPageAdv1.Controls.Add(this.txtCodConta);
            this.tabPageAdv1.Controls.Add(this.txtConta);
            this.tabPageAdv1.Controls.Add(this.txtValor);
            this.tabPageAdv1.Controls.Add(this.txtCodUsuario);
            this.tabPageAdv1.Controls.Add(this.txtUsuario);
            this.tabPageAdv1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageAdv1.Image = null;
            this.tabPageAdv1.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPageAdv1.Location = new System.Drawing.Point(1, 29);
            this.tabPageAdv1.Name = "tabPageAdv1";
            this.tabPageAdv1.ShowCloseButton = true;
            this.tabPageAdv1.Size = new System.Drawing.Size(797, 290);
            this.tabPageAdv1.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageAdv1.TabIndex = 1;
            this.tabPageAdv1.Text = "Caixa";
            this.tabPageAdv1.ThemesEnabled = false;
            // 
            // autoLabel5
            // 
            this.autoLabel5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.Black;
            this.autoLabel5.Location = new System.Drawing.Point(679, 158);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(51, 16);
            this.autoLabel5.TabIndex = 313;
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
            this.btnPesquisaPlanoConta.Location = new System.Drawing.Point(632, 175);
            this.btnPesquisaPlanoConta.Name = "btnPesquisaPlanoConta";
            this.btnPesquisaPlanoConta.Size = new System.Drawing.Size(40, 34);
            this.btnPesquisaPlanoConta.TabIndex = 10;
            this.btnPesquisaPlanoConta.UseVisualStyleBackColor = true;
            this.btnPesquisaPlanoConta.Click += new System.EventHandler(this.btnPesquisaPlanoConta_Click);
            // 
            // autoLabel6
            // 
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(12, 158);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(106, 16);
            this.autoLabel6.TabIndex = 312;
            this.autoLabel6.Text = "Plano de Contas";
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(404, 87);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(134, 16);
            this.autoLabel2.TabIndex = 307;
            this.autoLabel2.Text = "Descrição Resumida";
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(679, 21);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(51, 16);
            this.autoLabel3.TabIndex = 306;
            this.autoLabel3.Text = "Código";
            // 
            // btnPesquisaConta
            // 
            this.btnPesquisaConta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaConta.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaConta.FlatAppearance.BorderSize = 0;
            this.btnPesquisaConta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaConta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaConta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaConta.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaConta.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaConta.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaConta.IconSize = 38;
            this.btnPesquisaConta.Location = new System.Drawing.Point(632, 38);
            this.btnPesquisaConta.Name = "btnPesquisaConta";
            this.btnPesquisaConta.Size = new System.Drawing.Size(40, 34);
            this.btnPesquisaConta.TabIndex = 4;
            this.btnPesquisaConta.UseVisualStyleBackColor = true;
            this.btnPesquisaConta.Click += new System.EventHandler(this.btnPesquisaConta_Click);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(394, 21);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(99, 16);
            this.autoLabel4.TabIndex = 305;
            this.autoLabel4.Text = "Conta Bancária";
            // 
            // txtDataMovimento
            // 
            this.txtDataMovimento.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataMovimento.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataMovimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDataMovimento.Location = new System.Drawing.Point(12, 108);
            this.txtDataMovimento.Name = "txtDataMovimento";
            this.txtDataMovimento.Size = new System.Drawing.Size(179, 33);
            this.txtDataMovimento.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataMovimento.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataMovimento.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataMovimento.TabIndex = 6;
            this.txtDataMovimento.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel13
            // 
            this.autoLabel13.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel13.ForeColor = System.Drawing.Color.Black;
            this.autoLabel13.Location = new System.Drawing.Point(12, 89);
            this.autoLabel13.Name = "autoLabel13";
            this.autoLabel13.Size = new System.Drawing.Size(147, 16);
            this.autoLabel13.TabIndex = 304;
            this.autoLabel13.Text = "Data da Movimentação";
            // 
            // autoLabel14
            // 
            this.autoLabel14.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel14.ForeColor = System.Drawing.Color.Black;
            this.autoLabel14.Location = new System.Drawing.Point(198, 87);
            this.autoLabel14.Name = "autoLabel14";
            this.autoLabel14.Size = new System.Drawing.Size(39, 16);
            this.autoLabel14.TabIndex = 303;
            this.autoLabel14.Text = "Valor";
            // 
            // autoLabel15
            // 
            this.autoLabel15.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel15.ForeColor = System.Drawing.Color.Black;
            this.autoLabel15.Location = new System.Drawing.Point(297, 21);
            this.autoLabel15.Name = "autoLabel15";
            this.autoLabel15.Size = new System.Drawing.Size(51, 16);
            this.autoLabel15.TabIndex = 302;
            this.autoLabel15.Text = "Código";
            // 
            // btnPesquisaUsuario1
            // 
            this.btnPesquisaUsuario1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaUsuario1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaUsuario1.FlatAppearance.BorderSize = 0;
            this.btnPesquisaUsuario1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaUsuario1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaUsuario1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaUsuario1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaUsuario1.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaUsuario1.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaUsuario1.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaUsuario1.IconSize = 38;
            this.btnPesquisaUsuario1.Location = new System.Drawing.Point(250, 38);
            this.btnPesquisaUsuario1.Name = "btnPesquisaUsuario1";
            this.btnPesquisaUsuario1.Size = new System.Drawing.Size(40, 34);
            this.btnPesquisaUsuario1.TabIndex = 1;
            this.btnPesquisaUsuario1.UseVisualStyleBackColor = true;
            this.btnPesquisaUsuario1.Click += new System.EventHandler(this.btnPesquisaUsuario1_Click);
            // 
            // autoLabel16
            // 
            this.autoLabel16.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel16.ForeColor = System.Drawing.Color.Black;
            this.autoLabel16.Location = new System.Drawing.Point(12, 21);
            this.autoLabel16.Name = "autoLabel16";
            this.autoLabel16.Size = new System.Drawing.Size(54, 16);
            this.autoLabel16.TabIndex = 301;
            this.autoLabel16.Text = "Usuário";
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
            this.txtCodPlanoConta.Location = new System.Drawing.Point(679, 172);
            this.txtCodPlanoConta.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodPlanoConta.Multiline = false;
            this.txtCodPlanoConta.Name = "txtCodPlanoConta";
            this.txtCodPlanoConta.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodPlanoConta.PasswordChar = false;
            this.txtCodPlanoConta.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodPlanoConta.PlaceholderText = "";
            this.txtCodPlanoConta.ReadOnly = false;
            this.txtCodPlanoConta.Size = new System.Drawing.Size(89, 37);
            this.txtCodPlanoConta.TabIndex = 11;
            this.txtCodPlanoConta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodPlanoConta.Texts = "";
            this.txtCodPlanoConta.UnderlinedStyle = false;
            this.txtCodPlanoConta.Leave += new System.EventHandler(this.txtCodPlanoConta_Leave);
            // 
            // txtPlanoContas
            // 
            this.txtPlanoContas.BackColor = System.Drawing.Color.White;
            this.txtPlanoContas.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPlanoContas.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPlanoContas.BorderRadius = 8;
            this.txtPlanoContas.BorderSize = 2;
            this.txtPlanoContas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlanoContas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPlanoContas.Location = new System.Drawing.Point(12, 172);
            this.txtPlanoContas.Margin = new System.Windows.Forms.Padding(4);
            this.txtPlanoContas.Multiline = false;
            this.txtPlanoContas.Name = "txtPlanoContas";
            this.txtPlanoContas.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtPlanoContas.PasswordChar = false;
            this.txtPlanoContas.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtPlanoContas.PlaceholderText = "";
            this.txtPlanoContas.ReadOnly = false;
            this.txtPlanoContas.Size = new System.Drawing.Size(613, 37);
            this.txtPlanoContas.TabIndex = 9;
            this.txtPlanoContas.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPlanoContas.Texts = "";
            this.txtPlanoContas.UnderlinedStyle = false;
            this.txtPlanoContas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlanoContas_KeyPress);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnConfirmar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnConfirmar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnConfirmar.BorderRadius = 8;
            this.btnConfirmar.BorderSize = 0;
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Location = new System.Drawing.Point(596, 228);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(172, 45);
            this.btnConfirmar.TabIndex = 13;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.White;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // txtDescricaoResumida
            // 
            this.txtDescricaoResumida.BackColor = System.Drawing.Color.White;
            this.txtDescricaoResumida.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricaoResumida.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricaoResumida.BorderRadius = 8;
            this.txtDescricaoResumida.BorderSize = 2;
            this.txtDescricaoResumida.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricaoResumida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDescricaoResumida.Location = new System.Drawing.Point(404, 104);
            this.txtDescricaoResumida.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricaoResumida.Multiline = false;
            this.txtDescricaoResumida.Name = "txtDescricaoResumida";
            this.txtDescricaoResumida.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescricaoResumida.PasswordChar = false;
            this.txtDescricaoResumida.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDescricaoResumida.PlaceholderText = "";
            this.txtDescricaoResumida.ReadOnly = false;
            this.txtDescricaoResumida.Size = new System.Drawing.Size(364, 37);
            this.txtDescricaoResumida.TabIndex = 8;
            this.txtDescricaoResumida.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescricaoResumida.Texts = "";
            this.txtDescricaoResumida.UnderlinedStyle = false;
            // 
            // txtCodConta
            // 
            this.txtCodConta.BackColor = System.Drawing.Color.White;
            this.txtCodConta.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodConta.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodConta.BorderRadius = 8;
            this.txtCodConta.BorderSize = 2;
            this.txtCodConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodConta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodConta.Location = new System.Drawing.Point(679, 35);
            this.txtCodConta.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodConta.Multiline = false;
            this.txtCodConta.Name = "txtCodConta";
            this.txtCodConta.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodConta.PasswordChar = false;
            this.txtCodConta.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodConta.PlaceholderText = "";
            this.txtCodConta.ReadOnly = false;
            this.txtCodConta.Size = new System.Drawing.Size(89, 37);
            this.txtCodConta.TabIndex = 5;
            this.txtCodConta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodConta.Texts = "";
            this.txtCodConta.UnderlinedStyle = false;
            this.txtCodConta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodConta_KeyPress);
            this.txtCodConta.Leave += new System.EventHandler(this.txtCodConta_Leave);
            // 
            // txtConta
            // 
            this.txtConta.BackColor = System.Drawing.Color.White;
            this.txtConta.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtConta.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtConta.BorderRadius = 8;
            this.txtConta.BorderSize = 2;
            this.txtConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtConta.Location = new System.Drawing.Point(394, 35);
            this.txtConta.Margin = new System.Windows.Forms.Padding(4);
            this.txtConta.Multiline = false;
            this.txtConta.Name = "txtConta";
            this.txtConta.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtConta.PasswordChar = false;
            this.txtConta.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtConta.PlaceholderText = "";
            this.txtConta.ReadOnly = false;
            this.txtConta.Size = new System.Drawing.Size(231, 37);
            this.txtConta.TabIndex = 3;
            this.txtConta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtConta.Texts = "";
            this.txtConta.UnderlinedStyle = false;
            this.txtConta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConta_KeyPress);
            // 
            // txtValor
            // 
            this.txtValor.BackColor = System.Drawing.Color.White;
            this.txtValor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderRadius = 8;
            this.txtValor.BorderSize = 2;
            this.txtValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtValor.Location = new System.Drawing.Point(198, 104);
            this.txtValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtValor.Multiline = false;
            this.txtValor.Name = "txtValor";
            this.txtValor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValor.PasswordChar = false;
            this.txtValor.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtValor.PlaceholderText = "";
            this.txtValor.ReadOnly = false;
            this.txtValor.Size = new System.Drawing.Size(198, 37);
            this.txtValor.TabIndex = 7;
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValor.Texts = "0";
            this.txtValor.UnderlinedStyle = false;
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValor_KeyPress);
            this.txtValor.Leave += new System.EventHandler(this.txtValor_Leave);
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
            this.txtCodUsuario.Location = new System.Drawing.Point(297, 35);
            this.txtCodUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodUsuario.Multiline = false;
            this.txtCodUsuario.Name = "txtCodUsuario";
            this.txtCodUsuario.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodUsuario.PasswordChar = false;
            this.txtCodUsuario.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodUsuario.PlaceholderText = "";
            this.txtCodUsuario.ReadOnly = false;
            this.txtCodUsuario.Size = new System.Drawing.Size(89, 37);
            this.txtCodUsuario.TabIndex = 2;
            this.txtCodUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodUsuario.Texts = "";
            this.txtCodUsuario.UnderlinedStyle = false;
            this.txtCodUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodUsuario_KeyPress);
            this.txtCodUsuario.Leave += new System.EventHandler(this.txtCodUsuario_Leave);
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
            this.txtUsuario.Location = new System.Drawing.Point(12, 35);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsuario.Multiline = false;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtUsuario.PasswordChar = false;
            this.txtUsuario.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtUsuario.PlaceholderText = "";
            this.txtUsuario.ReadOnly = false;
            this.txtUsuario.Size = new System.Drawing.Size(231, 37);
            this.txtUsuario.TabIndex = 0;
            this.txtUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUsuario.Texts = "";
            this.txtUsuario.UnderlinedStyle = false;
            this.txtUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsuario_KeyPress);
            // 
            // radioPix
            // 
            this.radioPix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.radioPix.BeforeTouchSize = new System.Drawing.Size(70, 26);
            this.radioPix.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.radioPix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.radioPix.HotBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.radioPix.Location = new System.Drawing.Point(124, 247);
            this.radioPix.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioPix.Name = "radioPix";
            this.radioPix.Size = new System.Drawing.Size(70, 26);
            this.radioPix.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2016Colorful;
            this.radioPix.TabIndex = 315;
            this.radioPix.TabStop = false;
            this.radioPix.Text = " PIX";
            this.radioPix.ThemeName = "Office2016Colorful";
            this.radioPix.Visible = false;
            this.radioPix.CheckChanged += new System.EventHandler(this.radioPix_CheckChanged);
            // 
            // radioDeposito
            // 
            this.radioDeposito.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.radioDeposito.BeforeTouchSize = new System.Drawing.Size(172, 26);
            this.radioDeposito.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.radioDeposito.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.radioDeposito.HotBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.radioDeposito.Location = new System.Drawing.Point(200, 247);
            this.radioDeposito.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioDeposito.Name = "radioDeposito";
            this.radioDeposito.Size = new System.Drawing.Size(172, 26);
            this.radioDeposito.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2016Colorful;
            this.radioDeposito.TabIndex = 316;
            this.radioDeposito.TabStop = false;
            this.radioDeposito.Text = " Depósito Bancário";
            this.radioDeposito.ThemeName = "Office2016Colorful";
            this.radioDeposito.Visible = false;
            this.radioDeposito.CheckChanged += new System.EventHandler(this.radioDeposito_CheckChanged);
            // 
            // radioDinheiro
            // 
            this.radioDinheiro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.radioDinheiro.BeforeTouchSize = new System.Drawing.Size(101, 26);
            this.radioDinheiro.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.radioDinheiro.Checked = true;
            this.radioDinheiro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.radioDinheiro.HotBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.radioDinheiro.Location = new System.Drawing.Point(17, 247);
            this.radioDinheiro.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioDinheiro.Name = "radioDinheiro";
            this.radioDinheiro.Size = new System.Drawing.Size(101, 26);
            this.radioDinheiro.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2016Colorful;
            this.radioDinheiro.TabIndex = 317;
            this.radioDinheiro.Text = " Dinheiro";
            this.radioDinheiro.ThemeName = "Office2016Colorful";
            this.radioDinheiro.Visible = false;
            // 
            // FrmLancamentoCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 369);
            this.Controls.Add(this.tabControlAdv1);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLancamentoCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLancamentoCaixa";
            this.Load += new System.EventHandler(this.FrmLancamentoCaixa_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmLancamentoCaixa_Paint);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).EndInit();
            this.tabControlAdv1.ResumeLayout(false);
            this.tabPageAdv1.ResumeLayout(false);
            this.tabPageAdv1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioPix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioDeposito)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioDinheiro)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel25;
        private FontAwesome.Sharp.IconButton btnFechar;
        private Syncfusion.Windows.Forms.Tools.TabControlAdv tabControlAdv1;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageAdv1;
        private RJ_UI.Classes.RJButton btnConfirmar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private RJ_UI.Classes.RJTextBox txtDescricaoResumida;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private RJ_UI.Classes.RJTextBox txtCodConta;
        private FontAwesome.Sharp.IconButton btnPesquisaConta;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private RJ_UI.Classes.RJTextBox txtConta;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataMovimento;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel13;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel14;
        private RJ_UI.Classes.RJTextBox txtValor;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel15;
        private RJ_UI.Classes.RJTextBox txtCodUsuario;
        private FontAwesome.Sharp.IconButton btnPesquisaUsuario1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel16;
        private RJ_UI.Classes.RJTextBox txtUsuario;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private FontAwesome.Sharp.IconButton btnPesquisaPlanoConta;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private RJ_UI.Classes.RJTextBox txtCodPlanoConta;
        private RJ_UI.Classes.RJTextBox txtPlanoContas;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioDeposito;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioPix;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioDinheiro;
    }
}