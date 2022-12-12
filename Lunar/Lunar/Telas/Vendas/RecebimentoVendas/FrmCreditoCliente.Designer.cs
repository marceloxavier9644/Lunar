namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    partial class FrmCreditoCliente
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
            this.btnFechar = new System.Windows.Forms.Button();
            this.lblFormaPagamento = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtValor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.lblCreditoDisponivel = new System.Windows.Forms.Label();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
            this.SuspendLayout();
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Image = global::Lunar.Properties.Resources.CloseDark;
            this.btnFechar.Location = new System.Drawing.Point(294, 2);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(30, 31);
            this.btnFechar.TabIndex = 158;
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // lblFormaPagamento
            // 
            this.lblFormaPagamento.AutoSize = true;
            this.lblFormaPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormaPagamento.ForeColor = System.Drawing.Color.Black;
            this.lblFormaPagamento.Location = new System.Drawing.Point(12, 9);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(187, 25);
            this.lblFormaPagamento.TabIndex = 157;
            this.lblFormaPagamento.Text = "Crédito Disponível";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(10, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 31);
            this.label3.TabIndex = 161;
            this.label3.Text = "Valor Utilizado";
            // 
            // txtValor
            // 
            this.txtValor.BackColor = System.Drawing.Color.White;
            this.txtValor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderRadius = 8;
            this.txtValor.BorderSize = 2;
            this.txtValor.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtValor.Location = new System.Drawing.Point(13, 164);
            this.txtValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtValor.Multiline = false;
            this.txtValor.Name = "txtValor";
            this.txtValor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValor.PasswordChar = false;
            this.txtValor.PlaceholderColor = System.Drawing.Color.Black;
            this.txtValor.PlaceholderText = "R$";
            this.txtValor.ReadOnly = false;
            this.txtValor.Size = new System.Drawing.Size(288, 44);
            this.txtValor.TabIndex = 160;
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValor.Texts = "";
            this.txtValor.UnderlinedStyle = false;
            // 
            // lblCreditoDisponivel
            // 
            this.lblCreditoDisponivel.AutoSize = true;
            this.lblCreditoDisponivel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditoDisponivel.ForeColor = System.Drawing.Color.Black;
            this.lblCreditoDisponivel.Location = new System.Drawing.Point(68, 74);
            this.lblCreditoDisponivel.Name = "lblCreditoDisponivel";
            this.lblCreditoDisponivel.Size = new System.Drawing.Size(162, 24);
            this.lblCreditoDisponivel.TabIndex = 162;
            this.lblCreditoDisponivel.Text = "Crédito Disponível";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnConfirmar.Location = new System.Drawing.Point(19, 243);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(285, 45);
            this.btnConfirmar.TabIndex = 170;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.White;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // FrmCreditoCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(322, 308);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.lblCreditoDisponivel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.lblFormaPagamento);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmCreditoCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crédito Disponível";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCreditoCliente_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label lblFormaPagamento;
        private System.Windows.Forms.Label label3;
        private RJ_UI.Classes.RJTextBox txtValor;
        private System.Windows.Forms.Label lblCreditoDisponivel;
        private RJ_UI.Classes.RJButton btnConfirmar;
    }
}