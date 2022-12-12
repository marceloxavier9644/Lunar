namespace Lunar.Telas.Cadastros.BandeirasCartao
{
    partial class FrmBandeiraCartaoCadastro
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
            this.autoLabel18 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtBandeiraCartao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnSalvar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnCancelar = new Lunar.RJ_UI.Classes.RJButton();
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
            this.panelTitleBar.Size = new System.Drawing.Size(579, 44);
            this.panelTitleBar.TabIndex = 199;
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.White;
            this.autoLabel4.Location = new System.Drawing.Point(5, 7);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(216, 29);
            this.autoLabel4.TabIndex = 198;
            this.autoLabel4.Text = "Bandeira de Cartão";
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
            this.btnFechar.Location = new System.Drawing.Point(538, 3);
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
            this.autoLabel3.Location = new System.Drawing.Point(25, 116);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(55, 18);
            this.autoLabel3.TabIndex = 214;
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
            this.txtCodigo.Location = new System.Drawing.Point(13, 125);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.Multiline = false;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodigo.PasswordChar = false;
            this.txtCodigo.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCodigo.PlaceholderText = "";
            this.txtCodigo.Size = new System.Drawing.Size(102, 39);
            this.txtCodigo.TabIndex = 212;
            this.txtCodigo.TabStop = false;
            this.txtCodigo.Tag = "";
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodigo.Texts = "";
            this.txtCodigo.UnderlinedStyle = false;
            this.txtCodigo.Visible = false;
            // 
            // autoLabel18
            // 
            this.autoLabel18.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel18.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel18.ForeColor = System.Drawing.Color.Black;
            this.autoLabel18.Location = new System.Drawing.Point(28, 58);
            this.autoLabel18.Name = "autoLabel18";
            this.autoLabel18.Size = new System.Drawing.Size(69, 18);
            this.autoLabel18.TabIndex = 213;
            this.autoLabel18.Text = "Bandeira";
            // 
            // txtBandeiraCartao
            // 
            this.txtBandeiraCartao.BackColor = System.Drawing.Color.White;
            this.txtBandeiraCartao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtBandeiraCartao.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtBandeiraCartao.BorderRadius = 8;
            this.txtBandeiraCartao.BorderSize = 2;
            this.txtBandeiraCartao.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtBandeiraCartao.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBandeiraCartao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtBandeiraCartao.Location = new System.Drawing.Point(13, 72);
            this.txtBandeiraCartao.Margin = new System.Windows.Forms.Padding(4);
            this.txtBandeiraCartao.Multiline = false;
            this.txtBandeiraCartao.Name = "txtBandeiraCartao";
            this.txtBandeiraCartao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtBandeiraCartao.PasswordChar = false;
            this.txtBandeiraCartao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtBandeiraCartao.PlaceholderText = "";
            this.txtBandeiraCartao.Size = new System.Drawing.Size(551, 39);
            this.txtBandeiraCartao.TabIndex = 211;
            this.txtBandeiraCartao.Tag = "";
            this.txtBandeiraCartao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBandeiraCartao.Texts = "";
            this.txtBandeiraCartao.UnderlinedStyle = false;
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
            this.btnSalvar.Location = new System.Drawing.Point(348, 149);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(216, 45);
            this.btnSalvar.TabIndex = 215;
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
            this.btnCancelar.Location = new System.Drawing.Point(126, 149);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(216, 45);
            this.btnCancelar.TabIndex = 216;
            this.btnCancelar.TabStop = false;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmBandeiraCartaoCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(579, 207);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.autoLabel18);
            this.Controls.Add(this.txtBandeiraCartao);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmBandeiraCartaoCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bandeiras de Cartão";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmBandeiraCartaoCadastro_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBandeiraCartaoCadastro_KeyDown);
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
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel18;
        private RJ_UI.Classes.RJTextBox txtBandeiraCartao;
        private RJ_UI.Classes.RJButton btnSalvar;
        private RJ_UI.Classes.RJButton btnCancelar;
    }
}