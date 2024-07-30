namespace Lunar.Telas.Food
{
    partial class FrmConfigFood
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigFood));
            this.txtMesaInicial = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMesaFinal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGravar = new FontAwesome.Sharp.IconButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIpServidor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPortaServidor = new System.Windows.Forms.TextBox();
            this.chkModificarMesa = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtMesaInicial
            // 
            this.txtMesaInicial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMesaInicial.Enabled = false;
            this.txtMesaInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMesaInicial.ForeColor = System.Drawing.Color.Gray;
            this.txtMesaInicial.Location = new System.Drawing.Point(31, 124);
            this.txtMesaInicial.Name = "txtMesaInicial";
            this.txtMesaInicial.Size = new System.Drawing.Size(190, 31);
            this.txtMesaInicial.TabIndex = 0;
            this.txtMesaInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMesaInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMesaInicial_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Número Mesa Inicial";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(223, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Número Mesa Final";
            // 
            // txtMesaFinal
            // 
            this.txtMesaFinal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMesaFinal.Enabled = false;
            this.txtMesaFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMesaFinal.ForeColor = System.Drawing.Color.Gray;
            this.txtMesaFinal.Location = new System.Drawing.Point(227, 124);
            this.txtMesaFinal.Name = "txtMesaFinal";
            this.txtMesaFinal.Size = new System.Drawing.Size(190, 31);
            this.txtMesaFinal.TabIndex = 1;
            this.txtMesaFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMesaFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMesaFinal_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(144, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "Configuração Food";
            // 
            // btnGravar
            // 
            this.btnGravar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGravar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnGravar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGravar.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGravar.ForeColor = System.Drawing.Color.White;
            this.btnGravar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnGravar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnGravar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGravar.IconSize = 25;
            this.btnGravar.Location = new System.Drawing.Point(133, 303);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(178, 43);
            this.btnGravar.TabIndex = 4;
            this.btnGravar.Text = "Gravar [F5]";
            this.btnGravar.UseVisualStyleBackColor = false;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(231, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "IP Servidor ou Nome Servidor";
            // 
            // txtIpServidor
            // 
            this.txtIpServidor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIpServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIpServidor.ForeColor = System.Drawing.Color.Gray;
            this.txtIpServidor.Location = new System.Drawing.Point(31, 181);
            this.txtIpServidor.Name = "txtIpServidor";
            this.txtIpServidor.Size = new System.Drawing.Size(386, 31);
            this.txtIpServidor.TabIndex = 2;
            this.txtIpServidor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Porta API Servidor";
            // 
            // txtPortaServidor
            // 
            this.txtPortaServidor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPortaServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPortaServidor.ForeColor = System.Drawing.Color.Gray;
            this.txtPortaServidor.Location = new System.Drawing.Point(31, 238);
            this.txtPortaServidor.Name = "txtPortaServidor";
            this.txtPortaServidor.Size = new System.Drawing.Size(386, 31);
            this.txtPortaServidor.TabIndex = 3;
            this.txtPortaServidor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkModificarMesa
            // 
            this.chkModificarMesa.AutoSize = true;
            this.chkModificarMesa.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkModificarMesa.Location = new System.Drawing.Point(31, 60);
            this.chkModificarMesa.Name = "chkModificarMesa";
            this.chkModificarMesa.Size = new System.Drawing.Size(291, 24);
            this.chkModificarMesa.TabIndex = 13;
            this.chkModificarMesa.Text = "Permitir Alterar Número das Mesas";
            this.chkModificarMesa.UseVisualStyleBackColor = true;
            this.chkModificarMesa.CheckedChanged += new System.EventHandler(this.chkModificarMesa_CheckedChanged);
            // 
            // FrmConfigFood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(444, 372);
            this.Controls.Add(this.chkModificarMesa);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPortaServidor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtIpServidor);
            this.Controls.Add(this.btnGravar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMesaFinal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMesaInicial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConfigFood";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config Food";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmConfigFood_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMesaInicial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMesaFinal;
        private System.Windows.Forms.Label label3;
        private FontAwesome.Sharp.IconButton btnGravar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIpServidor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPortaServidor;
        private System.Windows.Forms.CheckBox chkModificarMesa;
    }
}