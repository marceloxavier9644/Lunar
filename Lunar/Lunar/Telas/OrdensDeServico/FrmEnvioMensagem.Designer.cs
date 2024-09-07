namespace Lunar.Telas.OrdensDeServico
{
    partial class FrmEnvioMensagem
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
            this.txtNumeroCliente = new System.Windows.Forms.TextBox();
            this.autoLabel17 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnEnviar = new Lunar.RJ_UI.Classes.RJButton();
            this.txtNomeCliente = new System.Windows.Forms.TextBox();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.radioPdfOs = new System.Windows.Forms.RadioButton();
            this.radioTecnicoCaminho = new System.Windows.Forms.RadioButton();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioProdutoDisponivel = new System.Windows.Forms.RadioButton();
            this.radioMensagemLivre = new System.Windows.Forms.RadioButton();
            this.panelTitleBar.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.panelTitleBar.Size = new System.Drawing.Size(516, 38);
            this.panelTitleBar.TabIndex = 258;
            // 
            // autoLabel25
            // 
            this.autoLabel25.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel25.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel25.ForeColor = System.Drawing.Color.White;
            this.autoLabel25.Location = new System.Drawing.Point(12, 0);
            this.autoLabel25.Name = "autoLabel25";
            this.autoLabel25.Size = new System.Drawing.Size(199, 25);
            this.autoLabel25.TabIndex = 198;
            this.autoLabel25.Text = "Envio de Whatsapp";
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
            this.btnFechar.Location = new System.Drawing.Point(475, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(39, 41);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // txtNumeroCliente
            // 
            this.txtNumeroCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroCliente.Location = new System.Drawing.Point(12, 82);
            this.txtNumeroCliente.Name = "txtNumeroCliente";
            this.txtNumeroCliente.Size = new System.Drawing.Size(255, 29);
            this.txtNumeroCliente.TabIndex = 0;
            // 
            // autoLabel17
            // 
            this.autoLabel17.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel17.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel17.ForeColor = System.Drawing.Color.Black;
            this.autoLabel17.Location = new System.Drawing.Point(12, 52);
            this.autoLabel17.Name = "autoLabel17";
            this.autoLabel17.Size = new System.Drawing.Size(140, 20);
            this.autoLabel17.TabIndex = 259;
            this.autoLabel17.Text = "Número do Cliente";
            // 
            // btnEnviar
            // 
            this.btnEnviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(55)))));
            this.btnEnviar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(55)))));
            this.btnEnviar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnEnviar.BorderRadius = 8;
            this.btnEnviar.BorderSize = 0;
            this.btnEnviar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviar.FlatAppearance.BorderSize = 0;
            this.btnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviar.ForeColor = System.Drawing.Color.White;
            this.btnEnviar.Location = new System.Drawing.Point(177, 378);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(163, 38);
            this.btnEnviar.TabIndex = 2;
            this.btnEnviar.Text = "Enviar [F5]";
            this.btnEnviar.TextColor = System.Drawing.Color.White;
            this.btnEnviar.UseVisualStyleBackColor = false;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // txtNomeCliente
            // 
            this.txtNomeCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeCliente.Location = new System.Drawing.Point(273, 82);
            this.txtNomeCliente.Name = "txtNomeCliente";
            this.txtNomeCliente.Size = new System.Drawing.Size(231, 29);
            this.txtNomeCliente.TabIndex = 1;
            // 
            // autoLabel1
            // 
            this.autoLabel1.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(273, 52);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(126, 20);
            this.autoLabel1.TabIndex = 262;
            this.autoLabel1.Text = "Nome do Cliente";
            // 
            // autoLabel2
            // 
            this.autoLabel2.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(12, 126);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(249, 24);
            this.autoLabel2.TabIndex = 264;
            this.autoLabel2.Text = "Escolha o que deseja enviar";
            // 
            // radioPdfOs
            // 
            this.radioPdfOs.AutoSize = true;
            this.radioPdfOs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioPdfOs.Location = new System.Drawing.Point(43, 139);
            this.radioPdfOs.Name = "radioPdfOs";
            this.radioPdfOs.Size = new System.Drawing.Size(215, 24);
            this.radioPdfOs.TabIndex = 3;
            this.radioPdfOs.Text = "Ordem de Serviço em PDF";
            this.radioPdfOs.UseVisualStyleBackColor = true;
            this.radioPdfOs.CheckedChanged += new System.EventHandler(this.radioPdfOs_CheckedChanged);
            // 
            // radioTecnicoCaminho
            // 
            this.radioTecnicoCaminho.AutoSize = true;
            this.radioTecnicoCaminho.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioTecnicoCaminho.Location = new System.Drawing.Point(264, 109);
            this.radioTecnicoCaminho.Name = "radioTecnicoCaminho";
            this.radioTecnicoCaminho.Size = new System.Drawing.Size(210, 24);
            this.radioTecnicoCaminho.TabIndex = 2;
            this.radioTecnicoCaminho.Text = "Aviso - Técnico a caminho";
            this.radioTecnicoCaminho.UseVisualStyleBackColor = true;
            this.radioTecnicoCaminho.CheckedChanged += new System.EventHandler(this.radioTecnicoCaminho_CheckedChanged);
            // 
            // txtMensagem
            // 
            this.txtMensagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMensagem.Location = new System.Drawing.Point(12, 169);
            this.txtMensagem.Multiline = true;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(492, 142);
            this.txtMensagem.TabIndex = 0;
            // 
            // autoLabel3
            // 
            this.autoLabel3.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Red;
            this.autoLabel3.Location = new System.Drawing.Point(174, 58);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(86, 13);
            this.autoLabel3.TabIndex = 266;
            this.autoLabel3.Text = "DDI + DDD + Nº";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioMensagemLivre);
            this.groupBox1.Controls.Add(this.radioProdutoDisponivel);
            this.groupBox1.Controls.Add(this.txtMensagem);
            this.groupBox1.Controls.Add(this.radioTecnicoCaminho);
            this.groupBox1.Controls.Add(this.radioPdfOs);
            this.groupBox1.Location = new System.Drawing.Point(0, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 385);
            this.groupBox1.TabIndex = 267;
            this.groupBox1.TabStop = false;
            // 
            // radioProdutoDisponivel
            // 
            this.radioProdutoDisponivel.AutoSize = true;
            this.radioProdutoDisponivel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioProdutoDisponivel.Location = new System.Drawing.Point(264, 139);
            this.radioProdutoDisponivel.Name = "radioProdutoDisponivel";
            this.radioProdutoDisponivel.Size = new System.Drawing.Size(210, 24);
            this.radioProdutoDisponivel.TabIndex = 4;
            this.radioProdutoDisponivel.Text = "Aviso - Produto Disponível";
            this.radioProdutoDisponivel.UseVisualStyleBackColor = true;
            this.radioProdutoDisponivel.CheckedChanged += new System.EventHandler(this.radioProdutoDisponivel_CheckedChanged);
            // 
            // radioMensagemLivre
            // 
            this.radioMensagemLivre.AutoSize = true;
            this.radioMensagemLivre.Checked = true;
            this.radioMensagemLivre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioMensagemLivre.Location = new System.Drawing.Point(43, 109);
            this.radioMensagemLivre.Name = "radioMensagemLivre";
            this.radioMensagemLivre.Size = new System.Drawing.Size(143, 24);
            this.radioMensagemLivre.TabIndex = 1;
            this.radioMensagemLivre.Text = "Mensagem Livre";
            this.radioMensagemLivre.UseVisualStyleBackColor = true;
            this.radioMensagemLivre.CheckedChanged += new System.EventHandler(this.radioMensagemLivre_CheckedChanged);
            // 
            // FrmEnvioMensagem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(516, 428);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.txtNomeCliente);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtNumeroCliente);
            this.Controls.Add(this.autoLabel17);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmEnvioMensagem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmEnvioMensagem";
            this.Load += new System.EventHandler(this.FrmEnvioMensagem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmEnvioMensagem_KeyDown);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel25;
        private FontAwesome.Sharp.IconButton btnFechar;
        private System.Windows.Forms.TextBox txtNumeroCliente;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel17;
        private RJ_UI.Classes.RJButton btnEnviar;
        private System.Windows.Forms.TextBox txtNomeCliente;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private System.Windows.Forms.RadioButton radioPdfOs;
        private System.Windows.Forms.RadioButton radioTecnicoCaminho;
        private System.Windows.Forms.TextBox txtMensagem;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioProdutoDisponivel;
        private System.Windows.Forms.RadioButton radioMensagemLivre;
    }
}