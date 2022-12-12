namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    partial class FrmCadastroTelefone
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
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtTelefone = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDDD = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtObservacoes = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnSalvar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnCancelar = new Lunar.RJ_UI.Classes.RJButton();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCodigo = new Lunar.RJ_UI.Classes.RJTextBox();
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
            this.autoLabel4.Location = new System.Drawing.Point(5, 7);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(204, 35);
            this.autoLabel4.TabIndex = 198;
            this.autoLabel4.Text = "Telefone Adicional";
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
            // autoLabel6
            // 
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel6.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(136, 60);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(65, 21);
            this.autoLabel6.TabIndex = 203;
            this.autoLabel6.Text = "Telefone";
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
            this.txtTelefone.Location = new System.Drawing.Point(123, 76);
            this.txtTelefone.Margin = new System.Windows.Forms.Padding(4);
            this.txtTelefone.Multiline = false;
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtTelefone.PasswordChar = false;
            this.txtTelefone.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtTelefone.PlaceholderText = "";
            this.txtTelefone.ReadOnly = false;
            this.txtTelefone.Size = new System.Drawing.Size(248, 44);
            this.txtTelefone.TabIndex = 1;
            this.txtTelefone.Tag = "";
            this.txtTelefone.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTelefone.Texts = "";
            this.txtTelefone.UnderlinedStyle = false;
            this.txtTelefone.Leave += new System.EventHandler(this.txtTelefone_Leave);
            // 
            // autoLabel5
            // 
            this.autoLabel5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel5.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.Black;
            this.autoLabel5.Location = new System.Drawing.Point(26, 60);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(40, 21);
            this.autoLabel5.TabIndex = 202;
            this.autoLabel5.Text = "DDD";
            // 
            // txtDDD
            // 
            this.txtDDD.BackColor = System.Drawing.Color.White;
            this.txtDDD.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDDD.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDDD.BorderRadius = 8;
            this.txtDDD.BorderSize = 2;
            this.txtDDD.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDDD.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDDD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDDD.Location = new System.Drawing.Point(13, 76);
            this.txtDDD.Margin = new System.Windows.Forms.Padding(4);
            this.txtDDD.Multiline = false;
            this.txtDDD.Name = "txtDDD";
            this.txtDDD.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDDD.PasswordChar = false;
            this.txtDDD.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDDD.PlaceholderText = "";
            this.txtDDD.ReadOnly = false;
            this.txtDDD.Size = new System.Drawing.Size(102, 44);
            this.txtDDD.TabIndex = 0;
            this.txtDDD.Tag = "";
            this.txtDDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDDD.Texts = "";
            this.txtDDD.UnderlinedStyle = false;
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
            this.txtObservacoes.Location = new System.Drawing.Point(379, 76);
            this.txtObservacoes.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservacoes.Multiline = false;
            this.txtObservacoes.Name = "txtObservacoes";
            this.txtObservacoes.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtObservacoes.PasswordChar = false;
            this.txtObservacoes.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtObservacoes.PlaceholderText = "";
            this.txtObservacoes.ReadOnly = false;
            this.txtObservacoes.Size = new System.Drawing.Size(408, 44);
            this.txtObservacoes.TabIndex = 2;
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
            this.autoLabel1.Location = new System.Drawing.Point(391, 60);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(93, 21);
            this.autoLabel1.TabIndex = 205;
            this.autoLabel1.Text = "Observações";
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
            this.btnSalvar.Location = new System.Drawing.Point(489, 186);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(298, 45);
            this.btnSalvar.TabIndex = 3;
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
            this.btnCancelar.Location = new System.Drawing.Point(185, 186);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(298, 45);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.TabStop = false;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(26, 125);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(56, 21);
            this.autoLabel3.TabIndex = 212;
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
            this.txtCodigo.Location = new System.Drawing.Point(14, 134);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.Multiline = false;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodigo.PasswordChar = false;
            this.txtCodigo.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCodigo.PlaceholderText = "";
            this.txtCodigo.ReadOnly = false;
            this.txtCodigo.Size = new System.Drawing.Size(102, 44);
            this.txtCodigo.TabIndex = 211;
            this.txtCodigo.Tag = "";
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodigo.Texts = "";
            this.txtCodigo.UnderlinedStyle = false;
            this.txtCodigo.Visible = false;
            // 
            // FrmCadastroTelefone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 278);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtObservacoes);
            this.Controls.Add(this.autoLabel6);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.autoLabel5);
            this.Controls.Add(this.txtDDD);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmCadastroTelefone";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCadastroTelefone";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCadastroTelefone_KeyDown);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private FontAwesome.Sharp.IconButton btnFechar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private RJ_UI.Classes.RJTextBox txtTelefone;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private RJ_UI.Classes.RJTextBox txtDDD;
        private RJ_UI.Classes.RJTextBox txtObservacoes;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJButton btnSalvar;
        private RJ_UI.Classes.RJButton btnCancelar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private RJ_UI.Classes.RJTextBox txtCodigo;
    }
}