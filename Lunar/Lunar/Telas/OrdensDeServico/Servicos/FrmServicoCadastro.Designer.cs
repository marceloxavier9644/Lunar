namespace Lunar.Telas.OrdensDeServico.Servicos
{
    partial class FrmServicoCadastro
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
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCodigo = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnSalvar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnCancelar = new Lunar.RJ_UI.Classes.RJButton();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDescricao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnFechar = new FontAwesome.Sharp.IconButton();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtValor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(25, 128);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(56, 21);
            this.autoLabel3.TabIndex = 239;
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
            this.txtCodigo.Location = new System.Drawing.Point(13, 144);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.Multiline = false;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodigo.PasswordChar = false;
            this.txtCodigo.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCodigo.PlaceholderText = "";
            this.txtCodigo.ReadOnly = false;
            this.txtCodigo.Size = new System.Drawing.Size(102, 44);
            this.txtCodigo.TabIndex = 0;
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
            this.btnSalvar.Location = new System.Drawing.Point(489, 172);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(298, 45);
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
            this.btnCancelar.Location = new System.Drawing.Point(185, 172);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(298, 45);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.TabStop = false;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // autoLabel6
            // 
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel6.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(25, 60);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(143, 21);
            this.autoLabel6.TabIndex = 238;
            this.autoLabel6.Text = "Descrição do Serviço";
            // 
            // txtDescricao
            // 
            this.txtDescricao.BackColor = System.Drawing.Color.White;
            this.txtDescricao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricao.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricao.BorderRadius = 8;
            this.txtDescricao.BorderSize = 2;
            this.txtDescricao.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDescricao.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDescricao.Location = new System.Drawing.Point(13, 77);
            this.txtDescricao.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricao.Multiline = false;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescricao.PasswordChar = false;
            this.txtDescricao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDescricao.PlaceholderText = "";
            this.txtDescricao.ReadOnly = false;
            this.txtDescricao.Size = new System.Drawing.Size(602, 44);
            this.txtDescricao.TabIndex = 0;
            this.txtDescricao.Tag = "";
            this.txtDescricao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescricao.Texts = "";
            this.txtDescricao.UnderlinedStyle = false;
            this.txtDescricao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescricao_KeyPress);
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
            this.panelTitleBar.TabIndex = 237;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.White;
            this.autoLabel4.Location = new System.Drawing.Point(5, 7);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(91, 35);
            this.autoLabel4.TabIndex = 198;
            this.autoLabel4.Text = "Serviço";
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
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(635, 60);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(43, 21);
            this.autoLabel1.TabIndex = 241;
            this.autoLabel1.Text = "Valor";
            // 
            // txtValor
            // 
            this.txtValor.BackColor = System.Drawing.Color.White;
            this.txtValor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderRadius = 8;
            this.txtValor.BorderSize = 2;
            this.txtValor.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtValor.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtValor.Location = new System.Drawing.Point(623, 77);
            this.txtValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtValor.Multiline = false;
            this.txtValor.Name = "txtValor";
            this.txtValor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValor.PasswordChar = false;
            this.txtValor.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtValor.PlaceholderText = "";
            this.txtValor.ReadOnly = false;
            this.txtValor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtValor.Size = new System.Drawing.Size(164, 44);
            this.txtValor.TabIndex = 1;
            this.txtValor.Tag = "";
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValor.Texts = "";
            this.txtValor.UnderlinedStyle = false;
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValor_KeyPress);
            this.txtValor.Leave += new System.EventHandler(this.txtValor_Leave);
            // 
            // FrmServicoCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 243);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.autoLabel6);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.panelTitleBar);
            this.KeyPreview = true;
            this.Name = "FrmServicoCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Serviço";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmServicoCadastro_KeyDown);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private RJ_UI.Classes.RJTextBox txtCodigo;
        private RJ_UI.Classes.RJButton btnSalvar;
        private RJ_UI.Classes.RJButton btnCancelar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private RJ_UI.Classes.RJTextBox txtDescricao;
        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private FontAwesome.Sharp.IconButton btnFechar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtValor;
    }
}