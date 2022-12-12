namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    partial class FrmCadastroDependente
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
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnFechar = new FontAwesome.Sharp.IconButton();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCodigo = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnSalvar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnCancelar = new Lunar.RJ_UI.Classes.RJButton();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtObservacoes = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtTelefone = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel18 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtNome = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCPF = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtGrauParentesco = new Lunar.RJ_UI.Classes.RJTextBox();
            this.lblDataNascimento = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataNascimento = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.rjTextBox3 = new Lunar.RJ_UI.Classes.RJTextBox();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel4);
            this.panelTitleBar.Controls.Add(this.btnFechar);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(800, 44);
            this.panelTitleBar.TabIndex = 199;
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.White;
            this.autoLabel4.Location = new System.Drawing.Point(5, 5);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(153, 35);
            this.autoLabel4.TabIndex = 198;
            this.autoLabel4.Text = "Dependentes";
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
            this.btnFechar.Location = new System.Drawing.Point(759, 3);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(39, 41);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(25, 249);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(56, 21);
            this.autoLabel3.TabIndex = 220;
            this.autoLabel3.Text = "Código";
            this.autoLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel3.Visible = false;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.White;
            this.txtCodigo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodigo.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodigo.BorderRadius = 8;
            this.txtCodigo.BorderSize = 2;
            this.txtCodigo.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodigo.Location = new System.Drawing.Point(13, 264);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.Multiline = false;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodigo.PasswordChar = false;
            this.txtCodigo.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCodigo.PlaceholderText = "";
            this.txtCodigo.ReadOnly = false;
            this.txtCodigo.Size = new System.Drawing.Size(102, 44);
            this.txtCodigo.TabIndex = 216;
            this.txtCodigo.TabStop = false;
            this.txtCodigo.Tag = "";
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodigo.Texts = "";
            this.txtCodigo.UnderlinedStyle = false;
            this.txtCodigo.Visible = false;
            // 
            // btnSalvar
            // 
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
            this.btnSalvar.Location = new System.Drawing.Point(489, 264);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(298, 45);
            this.btnSalvar.TabIndex = 6;
            this.btnSalvar.Text = "Salvar [F5]";
            this.btnSalvar.TextColor = System.Drawing.Color.White;
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
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
            this.btnCancelar.Location = new System.Drawing.Point(185, 264);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(298, 45);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.TabStop = false;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(25, 176);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(93, 21);
            this.autoLabel2.TabIndex = 219;
            this.autoLabel2.Text = "Observações";
            this.autoLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObservacoes
            // 
            this.txtObservacoes.BackColor = System.Drawing.Color.White;
            this.txtObservacoes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtObservacoes.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtObservacoes.BorderRadius = 8;
            this.txtObservacoes.BorderSize = 2;
            this.txtObservacoes.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtObservacoes.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservacoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtObservacoes.Location = new System.Drawing.Point(13, 192);
            this.txtObservacoes.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservacoes.Multiline = false;
            this.txtObservacoes.Name = "txtObservacoes";
            this.txtObservacoes.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtObservacoes.PasswordChar = false;
            this.txtObservacoes.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtObservacoes.PlaceholderText = "";
            this.txtObservacoes.ReadOnly = false;
            this.txtObservacoes.Size = new System.Drawing.Size(774, 44);
            this.txtObservacoes.TabIndex = 5;
            this.txtObservacoes.Tag = "";
            this.txtObservacoes.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtObservacoes.Texts = "";
            this.txtObservacoes.UnderlinedStyle = false;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(252, 114);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(65, 21);
            this.autoLabel1.TabIndex = 218;
            this.autoLabel1.Text = "Telefone";
            this.autoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTelefone
            // 
            this.txtTelefone.BackColor = System.Drawing.Color.White;
            this.txtTelefone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtTelefone.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtTelefone.BorderRadius = 8;
            this.txtTelefone.BorderSize = 2;
            this.txtTelefone.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtTelefone.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTelefone.Location = new System.Drawing.Point(240, 130);
            this.txtTelefone.Margin = new System.Windows.Forms.Padding(4);
            this.txtTelefone.Multiline = false;
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtTelefone.PasswordChar = false;
            this.txtTelefone.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtTelefone.PlaceholderText = "";
            this.txtTelefone.ReadOnly = false;
            this.txtTelefone.Size = new System.Drawing.Size(257, 44);
            this.txtTelefone.TabIndex = 3;
            this.txtTelefone.Tag = "";
            this.txtTelefone.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTelefone.Texts = "";
            this.txtTelefone.UnderlinedStyle = false;
            this.txtTelefone.Leave += new System.EventHandler(this.txtTelefone_Leave);
            // 
            // autoLabel18
            // 
            this.autoLabel18.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel18.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel18.ForeColor = System.Drawing.Color.Black;
            this.autoLabel18.Location = new System.Drawing.Point(28, 52);
            this.autoLabel18.Name = "autoLabel18";
            this.autoLabel18.Size = new System.Drawing.Size(50, 21);
            this.autoLabel18.TabIndex = 217;
            this.autoLabel18.Text = "Nome";
            // 
            // txtNome
            // 
            this.txtNome.BackColor = System.Drawing.Color.White;
            this.txtNome.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNome.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNome.BorderRadius = 8;
            this.txtNome.BorderSize = 2;
            this.txtNome.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtNome.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNome.Location = new System.Drawing.Point(13, 69);
            this.txtNome.Margin = new System.Windows.Forms.Padding(4);
            this.txtNome.Multiline = false;
            this.txtNome.Name = "txtNome";
            this.txtNome.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtNome.PasswordChar = false;
            this.txtNome.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtNome.PlaceholderText = "";
            this.txtNome.ReadOnly = false;
            this.txtNome.Size = new System.Drawing.Size(544, 44);
            this.txtNome.TabIndex = 0;
            this.txtNome.Tag = "";
            this.txtNome.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNome.Texts = "";
            this.txtNome.UnderlinedStyle = false;
            // 
            // autoLabel5
            // 
            this.autoLabel5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel5.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.Black;
            this.autoLabel5.Location = new System.Drawing.Point(578, 52);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(35, 21);
            this.autoLabel5.TabIndex = 222;
            this.autoLabel5.Text = "CPF";
            this.autoLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCPF
            // 
            this.txtCPF.BackColor = System.Drawing.Color.White;
            this.txtCPF.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCPF.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCPF.BorderRadius = 8;
            this.txtCPF.BorderSize = 2;
            this.txtCPF.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCPF.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCPF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCPF.Location = new System.Drawing.Point(565, 69);
            this.txtCPF.Margin = new System.Windows.Forms.Padding(4);
            this.txtCPF.Multiline = false;
            this.txtCPF.Name = "txtCPF";
            this.txtCPF.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCPF.PasswordChar = false;
            this.txtCPF.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCPF.PlaceholderText = "";
            this.txtCPF.ReadOnly = false;
            this.txtCPF.Size = new System.Drawing.Size(222, 44);
            this.txtCPF.TabIndex = 1;
            this.txtCPF.Tag = "";
            this.txtCPF.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCPF.Texts = "";
            this.txtCPF.UnderlinedStyle = false;
            // 
            // autoLabel6
            // 
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel6.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(516, 115);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(117, 21);
            this.autoLabel6.TabIndex = 224;
            this.autoLabel6.Text = "Grau Parentesco";
            this.autoLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtGrauParentesco
            // 
            this.txtGrauParentesco.BackColor = System.Drawing.Color.White;
            this.txtGrauParentesco.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtGrauParentesco.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtGrauParentesco.BorderRadius = 8;
            this.txtGrauParentesco.BorderSize = 2;
            this.txtGrauParentesco.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtGrauParentesco.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrauParentesco.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtGrauParentesco.Location = new System.Drawing.Point(505, 130);
            this.txtGrauParentesco.Margin = new System.Windows.Forms.Padding(4);
            this.txtGrauParentesco.Multiline = false;
            this.txtGrauParentesco.Name = "txtGrauParentesco";
            this.txtGrauParentesco.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtGrauParentesco.PasswordChar = false;
            this.txtGrauParentesco.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtGrauParentesco.PlaceholderText = "";
            this.txtGrauParentesco.ReadOnly = false;
            this.txtGrauParentesco.Size = new System.Drawing.Size(282, 44);
            this.txtGrauParentesco.TabIndex = 4;
            this.txtGrauParentesco.Tag = "";
            this.txtGrauParentesco.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtGrauParentesco.Texts = "";
            this.txtGrauParentesco.UnderlinedStyle = false;
            // 
            // lblDataNascimento
            // 
            this.lblDataNascimento.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblDataNascimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataNascimento.ForeColor = System.Drawing.Color.Black;
            this.lblDataNascimento.Location = new System.Drawing.Point(25, 119);
            this.lblDataNascimento.Name = "lblDataNascimento";
            this.lblDataNascimento.Size = new System.Drawing.Size(111, 16);
            this.lblDataNascimento.TabIndex = 226;
            this.lblDataNascimento.Text = "Data Nascimento";
            // 
            // txtDataNascimento
            // 
            this.txtDataNascimento.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataNascimento.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataNascimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataNascimento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDataNascimento.Location = new System.Drawing.Point(21, 140);
            this.txtDataNascimento.Name = "txtDataNascimento";
            this.txtDataNascimento.Size = new System.Drawing.Size(183, 21);
            this.txtDataNascimento.Style.BackColor = System.Drawing.Color.White;
            this.txtDataNascimento.Style.BorderColor = System.Drawing.Color.White;
            this.txtDataNascimento.Style.DropDown.BackColor = System.Drawing.Color.Transparent;
            this.txtDataNascimento.Style.DropDown.HoverBackColor = System.Drawing.Color.White;
            this.txtDataNascimento.Style.DropDown.PressedBackColor = System.Drawing.Color.White;
            this.txtDataNascimento.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataNascimento.Style.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDataNascimento.Style.HoverBorderColor = System.Drawing.Color.White;
            this.txtDataNascimento.TabIndex = 2;
            this.txtDataNascimento.Value = new System.DateTime(2021, 11, 20, 0, 0, 0, 0);
            // 
            // rjTextBox3
            // 
            this.rjTextBox3.BackColor = System.Drawing.Color.White;
            this.rjTextBox3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.rjTextBox3.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.rjTextBox3.BorderRadius = 8;
            this.rjTextBox3.BorderSize = 2;
            this.rjTextBox3.Cursor = System.Windows.Forms.Cursors.Default;
            this.rjTextBox3.Enabled = false;
            this.rjTextBox3.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjTextBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rjTextBox3.Location = new System.Drawing.Point(13, 130);
            this.rjTextBox3.Margin = new System.Windows.Forms.Padding(4);
            this.rjTextBox3.Multiline = false;
            this.rjTextBox3.Name = "rjTextBox3";
            this.rjTextBox3.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.rjTextBox3.PasswordChar = false;
            this.rjTextBox3.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rjTextBox3.PlaceholderText = "";
            this.rjTextBox3.ReadOnly = false;
            this.rjTextBox3.Size = new System.Drawing.Size(219, 44);
            this.rjTextBox3.TabIndex = 227;
            this.rjTextBox3.Tag = "";
            this.rjTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.rjTextBox3.Texts = "";
            this.rjTextBox3.UnderlinedStyle = false;
            // 
            // FrmCadastroDependente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 331);
            this.Controls.Add(this.lblDataNascimento);
            this.Controls.Add(this.txtDataNascimento);
            this.Controls.Add(this.autoLabel6);
            this.Controls.Add(this.txtGrauParentesco);
            this.Controls.Add(this.autoLabel5);
            this.Controls.Add(this.txtCPF);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.txtObservacoes);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.autoLabel18);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.rjTextBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmCadastroDependente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dependentes";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCadastroDependente_KeyDown);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private FontAwesome.Sharp.IconButton btnFechar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private RJ_UI.Classes.RJTextBox txtCodigo;
        private RJ_UI.Classes.RJButton btnSalvar;
        private RJ_UI.Classes.RJButton btnCancelar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private RJ_UI.Classes.RJTextBox txtObservacoes;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtTelefone;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel18;
        private RJ_UI.Classes.RJTextBox txtNome;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private RJ_UI.Classes.RJTextBox txtCPF;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private RJ_UI.Classes.RJTextBox txtGrauParentesco;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblDataNascimento;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataNascimento;
        private RJ_UI.Classes.RJTextBox rjTextBox3;
    }
}