namespace Lunar.Telas.Cadastros.Financeiro.Cartoes
{
    partial class FrmAdquirenteCartao
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
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnFechar = new FontAwesome.Sharp.IconButton();
            this.autoLabel18 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnSalvar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnCancelar = new Lunar.RJ_UI.Classes.RJButton();
            this.txtCodigo = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCNPJ = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtAdquirente = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel10 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCodContaBancaria = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel9 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtContaBancaria = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnPesquisaCon = new Lunar.RJ_UI.Classes.RJButton();
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
            this.panelTitleBar.Size = new System.Drawing.Size(595, 44);
            this.panelTitleBar.TabIndex = 200;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.White;
            this.autoLabel4.Location = new System.Drawing.Point(5, 7);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(317, 25);
            this.autoLabel4.TabIndex = 198;
            this.autoLabel4.Text = "Adquirente Cartão - Maquininha";
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
            this.btnFechar.Location = new System.Drawing.Point(554, 3);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(39, 41);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // autoLabel18
            // 
            this.autoLabel18.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel18.ForeColor = System.Drawing.Color.Black;
            this.autoLabel18.Location = new System.Drawing.Point(14, 48);
            this.autoLabel18.Name = "autoLabel18";
            this.autoLabel18.Size = new System.Drawing.Size(192, 16);
            this.autoLabel18.TabIndex = 210;
            this.autoLabel18.Text = "Adquirente (Nome Maquininha)";
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
            this.btnSalvar.Location = new System.Drawing.Point(367, 207);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(216, 45);
            this.btnSalvar.TabIndex = 2;
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
            this.btnCancelar.Location = new System.Drawing.Point(145, 207);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(216, 45);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.TabStop = false;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodigo.Location = new System.Drawing.Point(14, 214);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.Multiline = false;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodigo.PasswordChar = false;
            this.txtCodigo.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodigo.PlaceholderText = "";
            this.txtCodigo.ReadOnly = false;
            this.txtCodigo.Size = new System.Drawing.Size(97, 37);
            this.txtCodigo.TabIndex = 213;
            this.txtCodigo.TabStop = false;
            this.txtCodigo.Tag = "";
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodigo.Texts = "";
            this.txtCodigo.UnderlinedStyle = false;
            this.txtCodigo.Visible = false;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(342, 48);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(42, 16);
            this.autoLabel1.TabIndex = 215;
            this.autoLabel1.Text = "CNPJ";
            // 
            // txtCNPJ
            // 
            this.txtCNPJ.BackColor = System.Drawing.Color.White;
            this.txtCNPJ.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCNPJ.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCNPJ.BorderRadius = 8;
            this.txtCNPJ.BorderSize = 2;
            this.txtCNPJ.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCNPJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCNPJ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCNPJ.Location = new System.Drawing.Point(342, 68);
            this.txtCNPJ.Margin = new System.Windows.Forms.Padding(4);
            this.txtCNPJ.Multiline = false;
            this.txtCNPJ.Name = "txtCNPJ";
            this.txtCNPJ.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCNPJ.PasswordChar = false;
            this.txtCNPJ.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCNPJ.PlaceholderText = "";
            this.txtCNPJ.ReadOnly = false;
            this.txtCNPJ.Size = new System.Drawing.Size(240, 37);
            this.txtCNPJ.TabIndex = 1;
            this.txtCNPJ.Tag = "";
            this.txtCNPJ.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCNPJ.Texts = "";
            this.txtCNPJ.UnderlinedStyle = false;
            this.txtCNPJ.Leave += new System.EventHandler(this.txtCNPJ_Leave);
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(22, 194);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(51, 16);
            this.autoLabel2.TabIndex = 216;
            this.autoLabel2.Text = "Codigo";
            this.autoLabel2.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtAdquirente
            // 
            this.txtAdquirente.BackColor = System.Drawing.Color.White;
            this.txtAdquirente.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtAdquirente.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtAdquirente.BorderRadius = 8;
            this.txtAdquirente.BorderSize = 2;
            this.txtAdquirente.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtAdquirente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdquirente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtAdquirente.Location = new System.Drawing.Point(13, 68);
            this.txtAdquirente.Margin = new System.Windows.Forms.Padding(4);
            this.txtAdquirente.Multiline = false;
            this.txtAdquirente.Name = "txtAdquirente";
            this.txtAdquirente.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtAdquirente.PasswordChar = false;
            this.txtAdquirente.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtAdquirente.PlaceholderText = "";
            this.txtAdquirente.ReadOnly = false;
            this.txtAdquirente.Size = new System.Drawing.Size(321, 37);
            this.txtAdquirente.TabIndex = 217;
            this.txtAdquirente.Tag = "";
            this.txtAdquirente.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAdquirente.Texts = "";
            this.txtAdquirente.UnderlinedStyle = false;
            // 
            // autoLabel10
            // 
            this.autoLabel10.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel10.ForeColor = System.Drawing.Color.Black;
            this.autoLabel10.Location = new System.Drawing.Point(493, 109);
            this.autoLabel10.Name = "autoLabel10";
            this.autoLabel10.Size = new System.Drawing.Size(51, 16);
            this.autoLabel10.TabIndex = 262;
            this.autoLabel10.Text = "Código";
            // 
            // txtCodContaBancaria
            // 
            this.txtCodContaBancaria.BackColor = System.Drawing.Color.White;
            this.txtCodContaBancaria.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodContaBancaria.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodContaBancaria.BorderRadius = 8;
            this.txtCodContaBancaria.BorderSize = 2;
            this.txtCodContaBancaria.Enabled = false;
            this.txtCodContaBancaria.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodContaBancaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodContaBancaria.Location = new System.Drawing.Point(493, 129);
            this.txtCodContaBancaria.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodContaBancaria.Multiline = false;
            this.txtCodContaBancaria.Name = "txtCodContaBancaria";
            this.txtCodContaBancaria.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodContaBancaria.PasswordChar = false;
            this.txtCodContaBancaria.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodContaBancaria.PlaceholderText = "";
            this.txtCodContaBancaria.ReadOnly = false;
            this.txtCodContaBancaria.Size = new System.Drawing.Size(89, 37);
            this.txtCodContaBancaria.TabIndex = 261;
            this.txtCodContaBancaria.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodContaBancaria.Texts = "";
            this.txtCodContaBancaria.UnderlinedStyle = false;
            // 
            // autoLabel9
            // 
            this.autoLabel9.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel9.ForeColor = System.Drawing.Color.Black;
            this.autoLabel9.Location = new System.Drawing.Point(14, 109);
            this.autoLabel9.Name = "autoLabel9";
            this.autoLabel9.Size = new System.Drawing.Size(99, 16);
            this.autoLabel9.TabIndex = 259;
            this.autoLabel9.Text = "Conta Bancária";
            // 
            // txtContaBancaria
            // 
            this.txtContaBancaria.BackColor = System.Drawing.Color.White;
            this.txtContaBancaria.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtContaBancaria.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtContaBancaria.BorderRadius = 8;
            this.txtContaBancaria.BorderSize = 2;
            this.txtContaBancaria.Enabled = false;
            this.txtContaBancaria.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContaBancaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtContaBancaria.Location = new System.Drawing.Point(12, 129);
            this.txtContaBancaria.Margin = new System.Windows.Forms.Padding(4);
            this.txtContaBancaria.Multiline = false;
            this.txtContaBancaria.Name = "txtContaBancaria";
            this.txtContaBancaria.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtContaBancaria.PasswordChar = false;
            this.txtContaBancaria.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtContaBancaria.PlaceholderText = "";
            this.txtContaBancaria.ReadOnly = false;
            this.txtContaBancaria.Size = new System.Drawing.Size(372, 37);
            this.txtContaBancaria.TabIndex = 258;
            this.txtContaBancaria.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtContaBancaria.Texts = "";
            this.txtContaBancaria.UnderlinedStyle = false;
            // 
            // btnPesquisaCon
            // 
            this.btnPesquisaCon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnPesquisaCon.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnPesquisaCon.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnPesquisaCon.BorderRadius = 8;
            this.btnPesquisaCon.BorderSize = 0;
            this.btnPesquisaCon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaCon.FlatAppearance.BorderSize = 0;
            this.btnPesquisaCon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaCon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaCon.ForeColor = System.Drawing.Color.White;
            this.btnPesquisaCon.Location = new System.Drawing.Point(391, 129);
            this.btnPesquisaCon.Name = "btnPesquisaCon";
            this.btnPesquisaCon.Size = new System.Drawing.Size(95, 37);
            this.btnPesquisaCon.TabIndex = 263;
            this.btnPesquisaCon.Text = "Pesquisar";
            this.btnPesquisaCon.TextColor = System.Drawing.Color.White;
            this.btnPesquisaCon.UseVisualStyleBackColor = false;
            this.btnPesquisaCon.Click += new System.EventHandler(this.btnPesquisaCon_Click);
            // 
            // FrmAdquirenteCartao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(595, 264);
            this.Controls.Add(this.btnPesquisaCon);
            this.Controls.Add(this.autoLabel10);
            this.Controls.Add(this.txtCodContaBancaria);
            this.Controls.Add(this.autoLabel9);
            this.Controls.Add(this.txtContaBancaria);
            this.Controls.Add(this.autoLabel18);
            this.Controls.Add(this.txtAdquirente);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtCNPJ);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.panelTitleBar);
            this.KeyPreview = true;
            this.Name = "FrmAdquirenteCartao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adquirente Cartão";
            this.Load += new System.EventHandler(this.FrmAdquirenteCartao_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmAdquirenteCartao_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAdquirenteCartao_KeyDown);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private FontAwesome.Sharp.IconButton btnFechar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel18;
        private RJ_UI.Classes.RJButton btnSalvar;
        private RJ_UI.Classes.RJButton btnCancelar;
        private RJ_UI.Classes.RJTextBox txtCodigo;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtCNPJ;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private System.Windows.Forms.Timer timer1;
        private RJ_UI.Classes.RJTextBox txtAdquirente;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel10;
        private RJ_UI.Classes.RJTextBox txtCodContaBancaria;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel9;
        private RJ_UI.Classes.RJTextBox txtContaBancaria;
        private RJ_UI.Classes.RJButton btnPesquisaCon;
    }
}