namespace Lunar.Telas.ValeFuncionarios
{
    partial class FrmVale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVale));
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel25 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnFechar = new FontAwesome.Sharp.IconButton();
            this.autoLabel15 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaFuncionario = new FontAwesome.Sharp.IconButton();
            this.autoLabel16 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCodFuncionario = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtFuncionario = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtObservacoes = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtValor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtDataMovimento = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel13 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaPlanoConta = new FontAwesome.Sharp.IconButton();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCodPlanoConta = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtPlanoConta = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataVencimento = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.panelTitleBar.SuspendLayout();
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
            this.panelTitleBar.Size = new System.Drawing.Size(491, 38);
            this.panelTitleBar.TabIndex = 258;
            // 
            // autoLabel25
            // 
            this.autoLabel25.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel25.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel25.ForeColor = System.Drawing.Color.White;
            this.autoLabel25.Location = new System.Drawing.Point(5, 1);
            this.autoLabel25.Name = "autoLabel25";
            this.autoLabel25.Size = new System.Drawing.Size(164, 35);
            this.autoLabel25.TabIndex = 198;
            this.autoLabel25.Text = "Adiantamento";
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
            this.btnFechar.Location = new System.Drawing.Point(450, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(39, 41);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // autoLabel15
            // 
            this.autoLabel15.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel15.ForeColor = System.Drawing.Color.Black;
            this.autoLabel15.Location = new System.Drawing.Point(384, 63);
            this.autoLabel15.Name = "autoLabel15";
            this.autoLabel15.Size = new System.Drawing.Size(51, 16);
            this.autoLabel15.TabIndex = 307;
            this.autoLabel15.Text = "Código";
            // 
            // btnPesquisaFuncionario
            // 
            this.btnPesquisaFuncionario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaFuncionario.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaFuncionario.FlatAppearance.BorderSize = 0;
            this.btnPesquisaFuncionario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaFuncionario.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaFuncionario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaFuncionario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaFuncionario.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaFuncionario.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaFuncionario.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaFuncionario.IconSize = 38;
            this.btnPesquisaFuncionario.Location = new System.Drawing.Point(337, 80);
            this.btnPesquisaFuncionario.Name = "btnPesquisaFuncionario";
            this.btnPesquisaFuncionario.Size = new System.Drawing.Size(40, 34);
            this.btnPesquisaFuncionario.TabIndex = 1;
            this.btnPesquisaFuncionario.UseVisualStyleBackColor = true;
            this.btnPesquisaFuncionario.Click += new System.EventHandler(this.btnPesquisaFuncionario_Click);
            // 
            // autoLabel16
            // 
            this.autoLabel16.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel16.ForeColor = System.Drawing.Color.Black;
            this.autoLabel16.Location = new System.Drawing.Point(18, 57);
            this.autoLabel16.Name = "autoLabel16";
            this.autoLabel16.Size = new System.Drawing.Size(77, 16);
            this.autoLabel16.TabIndex = 306;
            this.autoLabel16.Text = "Funcionário";
            // 
            // txtCodFuncionario
            // 
            this.txtCodFuncionario.BackColor = System.Drawing.Color.White;
            this.txtCodFuncionario.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodFuncionario.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodFuncionario.BorderRadius = 8;
            this.txtCodFuncionario.BorderSize = 2;
            this.txtCodFuncionario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodFuncionario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodFuncionario.Location = new System.Drawing.Point(384, 77);
            this.txtCodFuncionario.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodFuncionario.Multiline = false;
            this.txtCodFuncionario.Name = "txtCodFuncionario";
            this.txtCodFuncionario.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodFuncionario.PasswordChar = false;
            this.txtCodFuncionario.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodFuncionario.PlaceholderText = "";
            this.txtCodFuncionario.ReadOnly = false;
            this.txtCodFuncionario.Size = new System.Drawing.Size(89, 37);
            this.txtCodFuncionario.TabIndex = 2;
            this.txtCodFuncionario.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodFuncionario.Texts = "";
            this.txtCodFuncionario.UnderlinedStyle = false;
            this.txtCodFuncionario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodFuncionario_KeyPress);
            // 
            // txtFuncionario
            // 
            this.txtFuncionario.BackColor = System.Drawing.Color.White;
            this.txtFuncionario.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtFuncionario.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtFuncionario.BorderRadius = 8;
            this.txtFuncionario.BorderSize = 2;
            this.txtFuncionario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFuncionario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtFuncionario.Location = new System.Drawing.Point(13, 77);
            this.txtFuncionario.Margin = new System.Windows.Forms.Padding(4);
            this.txtFuncionario.Multiline = false;
            this.txtFuncionario.Name = "txtFuncionario";
            this.txtFuncionario.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtFuncionario.PasswordChar = false;
            this.txtFuncionario.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtFuncionario.PlaceholderText = "";
            this.txtFuncionario.ReadOnly = false;
            this.txtFuncionario.Size = new System.Drawing.Size(317, 37);
            this.txtFuncionario.TabIndex = 0;
            this.txtFuncionario.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFuncionario.Texts = "";
            this.txtFuncionario.UnderlinedStyle = false;
            this.txtFuncionario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFuncionario_KeyPress);
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(19, 249);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(89, 16);
            this.autoLabel1.TabIndex = 309;
            this.autoLabel1.Text = "Observações";
            // 
            // txtObservacoes
            // 
            this.txtObservacoes.BackColor = System.Drawing.Color.White;
            this.txtObservacoes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtObservacoes.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtObservacoes.BorderRadius = 8;
            this.txtObservacoes.BorderSize = 2;
            this.txtObservacoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservacoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtObservacoes.Location = new System.Drawing.Point(13, 269);
            this.txtObservacoes.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservacoes.Multiline = true;
            this.txtObservacoes.Name = "txtObservacoes";
            this.txtObservacoes.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtObservacoes.PasswordChar = false;
            this.txtObservacoes.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtObservacoes.PlaceholderText = "";
            this.txtObservacoes.ReadOnly = false;
            this.txtObservacoes.Size = new System.Drawing.Size(460, 61);
            this.txtObservacoes.TabIndex = 9;
            this.txtObservacoes.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtObservacoes.Texts = "";
            this.txtObservacoes.UnderlinedStyle = false;
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(176, 123);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(39, 16);
            this.autoLabel2.TabIndex = 311;
            this.autoLabel2.Text = "Valor";
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
            this.txtValor.Location = new System.Drawing.Point(176, 143);
            this.txtValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtValor.Multiline = false;
            this.txtValor.Name = "txtValor";
            this.txtValor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValor.PasswordChar = false;
            this.txtValor.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtValor.PlaceholderText = "";
            this.txtValor.ReadOnly = false;
            this.txtValor.Size = new System.Drawing.Size(133, 37);
            this.txtValor.TabIndex = 4;
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValor.Texts = "";
            this.txtValor.UnderlinedStyle = false;
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValor_KeyPress);
            this.txtValor.Leave += new System.EventHandler(this.txtValor_Leave);
            // 
            // txtDataMovimento
            // 
            this.txtDataMovimento.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataMovimento.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataMovimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDataMovimento.Location = new System.Drawing.Point(18, 147);
            this.txtDataMovimento.Name = "txtDataMovimento";
            this.txtDataMovimento.Size = new System.Drawing.Size(151, 33);
            this.txtDataMovimento.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataMovimento.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataMovimento.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataMovimento.TabIndex = 3;
            this.txtDataMovimento.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel13
            // 
            this.autoLabel13.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel13.ForeColor = System.Drawing.Color.Black;
            this.autoLabel13.Location = new System.Drawing.Point(13, 123);
            this.autoLabel13.Name = "autoLabel13";
            this.autoLabel13.Size = new System.Drawing.Size(147, 16);
            this.autoLabel13.TabIndex = 313;
            this.autoLabel13.Text = "Data da Movimentação";
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
            this.btnConfirmar.Location = new System.Drawing.Point(159, 339);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(172, 45);
            this.btnConfirmar.TabIndex = 10;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.White;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(384, 194);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(51, 16);
            this.autoLabel3.TabIndex = 319;
            this.autoLabel3.Text = "Código";
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
            this.btnPesquisaPlanoConta.Location = new System.Drawing.Point(337, 211);
            this.btnPesquisaPlanoConta.Name = "btnPesquisaPlanoConta";
            this.btnPesquisaPlanoConta.Size = new System.Drawing.Size(40, 34);
            this.btnPesquisaPlanoConta.TabIndex = 7;
            this.btnPesquisaPlanoConta.UseVisualStyleBackColor = true;
            this.btnPesquisaPlanoConta.Click += new System.EventHandler(this.btnPesquisaPlanoConta_Click);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(18, 188);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(106, 16);
            this.autoLabel4.TabIndex = 318;
            this.autoLabel4.Text = "Plano de Contas";
            // 
            // txtCodPlanoConta
            // 
            this.txtCodPlanoConta.BackColor = System.Drawing.Color.White;
            this.txtCodPlanoConta.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodPlanoConta.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodPlanoConta.BorderRadius = 8;
            this.txtCodPlanoConta.BorderSize = 2;
            this.txtCodPlanoConta.Enabled = false;
            this.txtCodPlanoConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodPlanoConta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodPlanoConta.Location = new System.Drawing.Point(384, 208);
            this.txtCodPlanoConta.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodPlanoConta.Multiline = false;
            this.txtCodPlanoConta.Name = "txtCodPlanoConta";
            this.txtCodPlanoConta.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodPlanoConta.PasswordChar = false;
            this.txtCodPlanoConta.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodPlanoConta.PlaceholderText = "";
            this.txtCodPlanoConta.ReadOnly = false;
            this.txtCodPlanoConta.Size = new System.Drawing.Size(89, 37);
            this.txtCodPlanoConta.TabIndex = 8;
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
            this.txtPlanoConta.Enabled = false;
            this.txtPlanoConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlanoConta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPlanoConta.Location = new System.Drawing.Point(13, 208);
            this.txtPlanoConta.Margin = new System.Windows.Forms.Padding(4);
            this.txtPlanoConta.Multiline = false;
            this.txtPlanoConta.Name = "txtPlanoConta";
            this.txtPlanoConta.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtPlanoConta.PasswordChar = false;
            this.txtPlanoConta.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtPlanoConta.PlaceholderText = "";
            this.txtPlanoConta.ReadOnly = false;
            this.txtPlanoConta.Size = new System.Drawing.Size(317, 37);
            this.txtPlanoConta.TabIndex = 6;
            this.txtPlanoConta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPlanoConta.Texts = "";
            this.txtPlanoConta.UnderlinedStyle = false;
            // 
            // autoLabel5
            // 
            this.autoLabel5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.Black;
            this.autoLabel5.Location = new System.Drawing.Point(316, 123);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(145, 16);
            this.autoLabel5.TabIndex = 321;
            this.autoLabel5.Text = "Vencimento a Receber";
            // 
            // txtDataVencimento
            // 
            this.txtDataVencimento.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataVencimento.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataVencimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDataVencimento.Location = new System.Drawing.Point(316, 147);
            this.txtDataVencimento.Name = "txtDataVencimento";
            this.txtDataVencimento.Size = new System.Drawing.Size(151, 33);
            this.txtDataVencimento.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataVencimento.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataVencimento.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataVencimento.TabIndex = 5;
            this.txtDataVencimento.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // FrmVale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(491, 394);
            this.Controls.Add(this.txtDataVencimento);
            this.Controls.Add(this.autoLabel5);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.btnPesquisaPlanoConta);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.txtCodPlanoConta);
            this.Controls.Add(this.txtPlanoConta);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtDataMovimento);
            this.Controls.Add(this.autoLabel13);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtObservacoes);
            this.Controls.Add(this.autoLabel15);
            this.Controls.Add(this.btnPesquisaFuncionario);
            this.Controls.Add(this.autoLabel16);
            this.Controls.Add(this.txtCodFuncionario);
            this.Controls.Add(this.txtFuncionario);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmVale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmVale";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel25;
        private FontAwesome.Sharp.IconButton btnFechar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel15;
        private FontAwesome.Sharp.IconButton btnPesquisaFuncionario;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel16;
        private RJ_UI.Classes.RJTextBox txtCodFuncionario;
        private RJ_UI.Classes.RJTextBox txtFuncionario;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtObservacoes;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private RJ_UI.Classes.RJTextBox txtValor;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataMovimento;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel13;
        private RJ_UI.Classes.RJButton btnConfirmar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private FontAwesome.Sharp.IconButton btnPesquisaPlanoConta;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private RJ_UI.Classes.RJTextBox txtCodPlanoConta;
        private RJ_UI.Classes.RJTextBox txtPlanoConta;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataVencimento;
    }
}