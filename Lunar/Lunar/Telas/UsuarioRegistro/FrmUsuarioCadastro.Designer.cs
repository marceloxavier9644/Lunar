namespace Lunar.Telas.UsuarioRegistro
{
    partial class FrmUsuarioCadastro
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
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.lblRazaoSocial = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaEmpresa = new FontAwesome.Sharp.IconButton();
            this.autoLabel15 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel14 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaGrupo = new FontAwesome.Sharp.IconButton();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblAutomatico = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCodGrupo = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtGrupo = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnSalvar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnCancelar = new Lunar.RJ_UI.Classes.RJButton();
            this.txtEmail = new Lunar.RJ_UI.Classes.RJTextBoxLower();
            this.txtSenha = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodEmpresa = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtEmpresa = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtID = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtLogin = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel7 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtTelefonePrincipal = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel8 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDDD = new Lunar.RJ_UI.Classes.RJTextBox();
            this.chkReceberNotificacoes = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkReceberNotificacoes)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel2);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(774, 44);
            this.panelTitleBar.TabIndex = 201;
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.White;
            this.autoLabel2.Location = new System.Drawing.Point(5, 7);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(209, 25);
            this.autoLabel2.TabIndex = 198;
            this.autoLabel2.Text = "Cadastro de Usuário";
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
            this.btnClose.Location = new System.Drawing.Point(733, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 41);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblRazaoSocial
            // 
            this.lblRazaoSocial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblRazaoSocial.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblRazaoSocial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRazaoSocial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblRazaoSocial.Location = new System.Drawing.Point(171, 53);
            this.lblRazaoSocial.Name = "lblRazaoSocial";
            this.lblRazaoSocial.Size = new System.Drawing.Size(89, 16);
            this.lblRazaoSocial.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016White;
            this.lblRazaoSocial.TabIndex = 204;
            this.lblRazaoSocial.Text = "Nome/Login *";
            this.lblRazaoSocial.ThemeName = "Office2016White";
            this.lblRazaoSocial.ThemeStyle.BackColor = System.Drawing.Color.Turquoise;
            // 
            // autoLabel3
            // 
            this.autoLabel3.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(13, 53);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(51, 16);
            this.autoLabel3.TabIndex = 207;
            this.autoLabel3.Text = "Código";
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
            this.btnPesquisaEmpresa.Location = new System.Drawing.Point(582, 208);
            this.btnPesquisaEmpresa.Name = "btnPesquisaEmpresa";
            this.btnPesquisaEmpresa.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaEmpresa.TabIndex = 8;
            this.btnPesquisaEmpresa.UseVisualStyleBackColor = true;
            this.btnPesquisaEmpresa.Click += new System.EventHandler(this.btnPesquisaEmpresa_Click);
            // 
            // autoLabel15
            // 
            this.autoLabel15.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel15.ForeColor = System.Drawing.Color.Black;
            this.autoLabel15.Location = new System.Drawing.Point(626, 184);
            this.autoLabel15.Name = "autoLabel15";
            this.autoLabel15.Size = new System.Drawing.Size(51, 16);
            this.autoLabel15.TabIndex = 214;
            this.autoLabel15.Text = "Código";
            // 
            // autoLabel14
            // 
            this.autoLabel14.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel14.ForeColor = System.Drawing.Color.Black;
            this.autoLabel14.Location = new System.Drawing.Point(13, 184);
            this.autoLabel14.Name = "autoLabel14";
            this.autoLabel14.Size = new System.Drawing.Size(147, 16);
            this.autoLabel14.TabIndex = 213;
            this.autoLabel14.Text = "Empresa de Trabalho *";
            // 
            // autoLabel1
            // 
            this.autoLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel1.Location = new System.Drawing.Point(484, 53);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(54, 16);
            this.autoLabel1.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016White;
            this.autoLabel1.TabIndex = 216;
            this.autoLabel1.Text = "Senha *";
            this.autoLabel1.ThemeName = "Office2016White";
            this.autoLabel1.ThemeStyle.BackColor = System.Drawing.Color.Turquoise;
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(13, 117);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(45, 16);
            this.autoLabel4.TabIndex = 218;
            this.autoLabel4.Text = "E-mail";
            // 
            // autoLabel5
            // 
            this.autoLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel5.Location = new System.Drawing.Point(374, 117);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(139, 16);
            this.autoLabel5.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016White;
            this.autoLabel5.TabIndex = 220;
            this.autoLabel5.Text = "Grupo de Permissão *";
            this.autoLabel5.ThemeName = "Office2016White";
            this.autoLabel5.ThemeStyle.BackColor = System.Drawing.Color.Turquoise;
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
            this.btnPesquisaGrupo.Location = new System.Drawing.Point(582, 142);
            this.btnPesquisaGrupo.Name = "btnPesquisaGrupo";
            this.btnPesquisaGrupo.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaGrupo.TabIndex = 5;
            this.btnPesquisaGrupo.UseVisualStyleBackColor = true;
            this.btnPesquisaGrupo.Click += new System.EventHandler(this.btnPesquisaGrupo_Click);
            // 
            // autoLabel6
            // 
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(626, 117);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(51, 16);
            this.autoLabel6.TabIndex = 223;
            this.autoLabel6.Text = "Código";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblAutomatico
            // 
            this.lblAutomatico.BackColor = System.Drawing.Color.Transparent;
            this.lblAutomatico.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblAutomatico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutomatico.ForeColor = System.Drawing.Color.LightGray;
            this.lblAutomatico.Location = new System.Drawing.Point(52, 84);
            this.lblAutomatico.Name = "lblAutomatico";
            this.lblAutomatico.Size = new System.Drawing.Size(74, 16);
            this.lblAutomatico.TabIndex = 224;
            this.lblAutomatico.Text = "Automático";
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
            this.txtCodGrupo.Location = new System.Drawing.Point(626, 138);
            this.txtCodGrupo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodGrupo.Multiline = false;
            this.txtCodGrupo.Name = "txtCodGrupo";
            this.txtCodGrupo.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodGrupo.PasswordChar = false;
            this.txtCodGrupo.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodGrupo.PlaceholderText = "";
            this.txtCodGrupo.ReadOnly = false;
            this.txtCodGrupo.Size = new System.Drawing.Size(137, 37);
            this.txtCodGrupo.TabIndex = 6;
            this.txtCodGrupo.Tag = "";
            this.txtCodGrupo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodGrupo.Texts = "";
            this.txtCodGrupo.UnderlinedStyle = false;
            // 
            // txtGrupo
            // 
            this.txtGrupo.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.txtGrupo.BackColor = System.Drawing.Color.White;
            this.txtGrupo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtGrupo.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtGrupo.BorderRadius = 8;
            this.txtGrupo.BorderSize = 2;
            this.txtGrupo.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrupo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtGrupo.Location = new System.Drawing.Point(374, 137);
            this.txtGrupo.Margin = new System.Windows.Forms.Padding(4);
            this.txtGrupo.Multiline = false;
            this.txtGrupo.Name = "txtGrupo";
            this.txtGrupo.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtGrupo.PasswordChar = false;
            this.txtGrupo.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtGrupo.PlaceholderText = "";
            this.txtGrupo.ReadOnly = false;
            this.txtGrupo.Size = new System.Drawing.Size(201, 37);
            this.txtGrupo.TabIndex = 4;
            this.txtGrupo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtGrupo.Texts = "";
            this.txtGrupo.UnderlinedStyle = false;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnSalvar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnSalvar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSalvar.BorderRadius = 8;
            this.btnSalvar.BorderSize = 0;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(461, 361);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(298, 45);
            this.btnSalvar.TabIndex = 13;
            this.btnSalvar.Text = "Salvar [F5]";
            this.btnSalvar.TextColor = System.Drawing.Color.White;
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.BackgroundColor = System.Drawing.Color.White;
            this.btnCancelar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnCancelar.BorderRadius = 8;
            this.btnCancelar.BorderSize = 2;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.Location = new System.Drawing.Point(157, 361);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(298, 45);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.TabStop = false;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.SystemColors.Window;
            this.txtEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEmail.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEmail.BorderRadius = 8;
            this.txtEmail.BorderSize = 2;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEmail.Location = new System.Drawing.Point(13, 137);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Multiline = false;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Padding = new System.Windows.Forms.Padding(7);
            this.txtEmail.PasswordChar = false;
            this.txtEmail.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtEmail.PlaceholderText = "";
            this.txtEmail.Size = new System.Drawing.Size(353, 39);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.Texts = "";
            this.txtEmail.UnderlinedStyle = false;
            // 
            // txtSenha
            // 
            this.txtSenha.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.txtSenha.BackColor = System.Drawing.Color.White;
            this.txtSenha.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtSenha.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtSenha.BorderRadius = 8;
            this.txtSenha.BorderSize = 2;
            this.txtSenha.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSenha.Location = new System.Drawing.Point(484, 73);
            this.txtSenha.Margin = new System.Windows.Forms.Padding(4);
            this.txtSenha.Multiline = false;
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtSenha.PasswordChar = true;
            this.txtSenha.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtSenha.PlaceholderText = "";
            this.txtSenha.ReadOnly = false;
            this.txtSenha.Size = new System.Drawing.Size(279, 37);
            this.txtSenha.TabIndex = 2;
            this.txtSenha.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSenha.Texts = "";
            this.txtSenha.UnderlinedStyle = false;
            // 
            // txtCodEmpresa
            // 
            this.txtCodEmpresa.BackColor = System.Drawing.Color.White;
            this.txtCodEmpresa.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodEmpresa.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodEmpresa.BorderRadius = 8;
            this.txtCodEmpresa.BorderSize = 2;
            this.txtCodEmpresa.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCodEmpresa.Enabled = false;
            this.txtCodEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodEmpresa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodEmpresa.Location = new System.Drawing.Point(626, 204);
            this.txtCodEmpresa.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodEmpresa.Multiline = false;
            this.txtCodEmpresa.Name = "txtCodEmpresa";
            this.txtCodEmpresa.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodEmpresa.PasswordChar = false;
            this.txtCodEmpresa.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodEmpresa.PlaceholderText = "";
            this.txtCodEmpresa.ReadOnly = false;
            this.txtCodEmpresa.Size = new System.Drawing.Size(137, 37);
            this.txtCodEmpresa.TabIndex = 9;
            this.txtCodEmpresa.Tag = "";
            this.txtCodEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodEmpresa.Texts = "";
            this.txtCodEmpresa.UnderlinedStyle = false;
            // 
            // txtEmpresa
            // 
            this.txtEmpresa.BackColor = System.Drawing.Color.White;
            this.txtEmpresa.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEmpresa.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEmpresa.BorderRadius = 8;
            this.txtEmpresa.BorderSize = 2;
            this.txtEmpresa.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpresa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEmpresa.Location = new System.Drawing.Point(13, 204);
            this.txtEmpresa.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmpresa.Multiline = false;
            this.txtEmpresa.Name = "txtEmpresa";
            this.txtEmpresa.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtEmpresa.PasswordChar = false;
            this.txtEmpresa.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtEmpresa.PlaceholderText = "";
            this.txtEmpresa.ReadOnly = false;
            this.txtEmpresa.Size = new System.Drawing.Size(562, 37);
            this.txtEmpresa.TabIndex = 7;
            this.txtEmpresa.Tag = "";
            this.txtEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEmpresa.Texts = "";
            this.txtEmpresa.UnderlinedStyle = false;
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.White;
            this.txtID.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtID.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtID.BorderRadius = 8;
            this.txtID.BorderSize = 2;
            this.txtID.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtID.Location = new System.Drawing.Point(13, 73);
            this.txtID.Margin = new System.Windows.Forms.Padding(4);
            this.txtID.Multiline = false;
            this.txtID.Name = "txtID";
            this.txtID.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtID.PasswordChar = false;
            this.txtID.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtID.PlaceholderText = "";
            this.txtID.ReadOnly = false;
            this.txtID.Size = new System.Drawing.Size(150, 37);
            this.txtID.TabIndex = 0;
            this.txtID.Tag = "";
            this.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtID.Texts = "";
            this.txtID.UnderlinedStyle = false;
            // 
            // txtLogin
            // 
            this.txtLogin.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.txtLogin.BackColor = System.Drawing.Color.White;
            this.txtLogin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtLogin.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtLogin.BorderRadius = 8;
            this.txtLogin.BorderSize = 2;
            this.txtLogin.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtLogin.Location = new System.Drawing.Point(171, 73);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(4);
            this.txtLogin.Multiline = false;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtLogin.PasswordChar = false;
            this.txtLogin.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtLogin.PlaceholderText = "";
            this.txtLogin.ReadOnly = false;
            this.txtLogin.Size = new System.Drawing.Size(305, 37);
            this.txtLogin.TabIndex = 1;
            this.txtLogin.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLogin.Texts = "";
            this.txtLogin.UnderlinedStyle = false;
            // 
            // autoLabel7
            // 
            this.autoLabel7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel7.ForeColor = System.Drawing.Color.Black;
            this.autoLabel7.Location = new System.Drawing.Point(123, 245);
            this.autoLabel7.Name = "autoLabel7";
            this.autoLabel7.Size = new System.Drawing.Size(69, 16);
            this.autoLabel7.TabIndex = 228;
            this.autoLabel7.Text = "Whatsapp";
            // 
            // txtTelefonePrincipal
            // 
            this.txtTelefonePrincipal.BackColor = System.Drawing.Color.White;
            this.txtTelefonePrincipal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtTelefonePrincipal.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtTelefonePrincipal.BorderRadius = 8;
            this.txtTelefonePrincipal.BorderSize = 2;
            this.txtTelefonePrincipal.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtTelefonePrincipal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefonePrincipal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTelefonePrincipal.Location = new System.Drawing.Point(123, 265);
            this.txtTelefonePrincipal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTelefonePrincipal.Multiline = false;
            this.txtTelefonePrincipal.Name = "txtTelefonePrincipal";
            this.txtTelefonePrincipal.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtTelefonePrincipal.PasswordChar = false;
            this.txtTelefonePrincipal.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtTelefonePrincipal.PlaceholderText = "";
            this.txtTelefonePrincipal.ReadOnly = false;
            this.txtTelefonePrincipal.Size = new System.Drawing.Size(248, 37);
            this.txtTelefonePrincipal.TabIndex = 11;
            this.txtTelefonePrincipal.Tag = "";
            this.txtTelefonePrincipal.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTelefonePrincipal.Texts = "";
            this.txtTelefonePrincipal.UnderlinedStyle = false;
            this.txtTelefonePrincipal.Leave += new System.EventHandler(this.txtTelefonePrincipal_Leave);
            // 
            // autoLabel8
            // 
            this.autoLabel8.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel8.ForeColor = System.Drawing.Color.Black;
            this.autoLabel8.Location = new System.Drawing.Point(13, 245);
            this.autoLabel8.Name = "autoLabel8";
            this.autoLabel8.Size = new System.Drawing.Size(37, 16);
            this.autoLabel8.TabIndex = 227;
            this.autoLabel8.Text = "DDD";
            // 
            // txtDDD
            // 
            this.txtDDD.BackColor = System.Drawing.Color.White;
            this.txtDDD.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDDD.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDDD.BorderRadius = 8;
            this.txtDDD.BorderSize = 2;
            this.txtDDD.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDDD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDDD.Location = new System.Drawing.Point(13, 265);
            this.txtDDD.Margin = new System.Windows.Forms.Padding(4);
            this.txtDDD.Multiline = false;
            this.txtDDD.Name = "txtDDD";
            this.txtDDD.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDDD.PasswordChar = false;
            this.txtDDD.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDDD.PlaceholderText = "";
            this.txtDDD.ReadOnly = false;
            this.txtDDD.Size = new System.Drawing.Size(102, 37);
            this.txtDDD.TabIndex = 10;
            this.txtDDD.Tag = "";
            this.txtDDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDDD.Texts = "";
            this.txtDDD.UnderlinedStyle = false;
            // 
            // chkReceberNotificacoes
            // 
            this.chkReceberNotificacoes.BeforeTouchSize = new System.Drawing.Size(270, 30);
            this.chkReceberNotificacoes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkReceberNotificacoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReceberNotificacoes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkReceberNotificacoes.Location = new System.Drawing.Point(13, 309);
            this.chkReceberNotificacoes.Name = "chkReceberNotificacoes";
            this.chkReceberNotificacoes.Size = new System.Drawing.Size(270, 30);
            this.chkReceberNotificacoes.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            this.chkReceberNotificacoes.TabIndex = 12;
            this.chkReceberNotificacoes.TabStop = false;
            this.chkReceberNotificacoes.Text = "Receber Notificações do Sistema";
            this.chkReceberNotificacoes.ThemeName = "Metro";
            // 
            // FrmUsuarioCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(774, 418);
            this.Controls.Add(this.chkReceberNotificacoes);
            this.Controls.Add(this.autoLabel7);
            this.Controls.Add(this.txtTelefonePrincipal);
            this.Controls.Add(this.autoLabel8);
            this.Controls.Add(this.txtDDD);
            this.Controls.Add(this.lblAutomatico);
            this.Controls.Add(this.btnPesquisaGrupo);
            this.Controls.Add(this.autoLabel6);
            this.Controls.Add(this.txtCodGrupo);
            this.Controls.Add(this.autoLabel5);
            this.Controls.Add(this.txtGrupo);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.btnPesquisaEmpresa);
            this.Controls.Add(this.autoLabel15);
            this.Controls.Add(this.txtCodEmpresa);
            this.Controls.Add(this.autoLabel14);
            this.Controls.Add(this.txtEmpresa);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblRazaoSocial);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.panelTitleBar);
            this.KeyPreview = true;
            this.Name = "FrmUsuarioCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Usuario";
            this.Load += new System.EventHandler(this.FrmUsuarioCadastro_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmUsuarioCadastro_KeyDown);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkReceberNotificacoes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private FontAwesome.Sharp.IconButton btnClose;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblRazaoSocial;
        private RJ_UI.Classes.RJTextBox txtLogin;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private RJ_UI.Classes.RJTextBox txtID;
        private FontAwesome.Sharp.IconButton btnPesquisaEmpresa;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel15;
        private RJ_UI.Classes.RJTextBox txtCodEmpresa;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel14;
        private RJ_UI.Classes.RJTextBox txtEmpresa;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtSenha;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private RJ_UI.Classes.RJTextBoxLower txtEmail;
        private RJ_UI.Classes.RJButton btnSalvar;
        private RJ_UI.Classes.RJButton btnCancelar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private RJ_UI.Classes.RJTextBox txtGrupo;
        private FontAwesome.Sharp.IconButton btnPesquisaGrupo;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private RJ_UI.Classes.RJTextBox txtCodGrupo;
        private System.Windows.Forms.Timer timer1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblAutomatico;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel7;
        private RJ_UI.Classes.RJTextBox txtTelefonePrincipal;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel8;
        private RJ_UI.Classes.RJTextBox txtDDD;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv chkReceberNotificacoes;
    }
}