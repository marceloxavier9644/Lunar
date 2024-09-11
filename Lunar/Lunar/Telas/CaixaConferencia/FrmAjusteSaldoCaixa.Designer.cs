namespace Lunar.Telas.CaixaConferencia
{
    partial class FrmAjusteSaldoCaixa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAjusteSaldoCaixa));
            this.btnAbrirCaixa = new MaterialSkin.Controls.MaterialButton();
            this.txtObservacoes = new System.Windows.Forms.TextBox();
            this.txtSaldoInicial = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAbrirCaixa
            // 
            this.btnAbrirCaixa.AutoSize = false;
            this.btnAbrirCaixa.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAbrirCaixa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirCaixa.Depth = 0;
            this.btnAbrirCaixa.DrawShadows = true;
            this.btnAbrirCaixa.HighEmphasis = true;
            this.btnAbrirCaixa.Icon = null;
            this.btnAbrirCaixa.Location = new System.Drawing.Point(186, 253);
            this.btnAbrirCaixa.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnAbrirCaixa.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAbrirCaixa.Name = "btnAbrirCaixa";
            this.btnAbrirCaixa.Size = new System.Drawing.Size(190, 50);
            this.btnAbrirCaixa.TabIndex = 2;
            this.btnAbrirCaixa.Text = "Confirmar Valor [F8]";
            this.btnAbrirCaixa.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnAbrirCaixa.UseAccentColor = false;
            this.btnAbrirCaixa.UseVisualStyleBackColor = true;
            this.btnAbrirCaixa.Click += new System.EventHandler(this.btnAbrirCaixa_Click);
            // 
            // txtObservacoes
            // 
            this.txtObservacoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservacoes.Location = new System.Drawing.Point(12, 130);
            this.txtObservacoes.Multiline = true;
            this.txtObservacoes.Name = "txtObservacoes";
            this.txtObservacoes.Size = new System.Drawing.Size(539, 105);
            this.txtObservacoes.TabIndex = 1;
            // 
            // txtSaldoInicial
            // 
            this.txtSaldoInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoInicial.Location = new System.Drawing.Point(129, 49);
            this.txtSaldoInicial.Name = "txtSaldoInicial";
            this.txtSaldoInicial.Size = new System.Drawing.Size(297, 40);
            this.txtSaldoInicial.TabIndex = 0;
            this.txtSaldoInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(125, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Qual valor em dinheiro você tem no caixa?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Observações do Ajuste (Opcional)";
            // 
            // FrmAjusteSaldoCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(563, 318);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSaldoInicial);
            this.Controls.Add(this.txtObservacoes);
            this.Controls.Add(this.btnAbrirCaixa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAjusteSaldoCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajuste de Saldo";
            this.Load += new System.EventHandler(this.FrmAjusteSaldoCaixa_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAjusteSaldoCaixa_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialButton btnAbrirCaixa;
        private System.Windows.Forms.TextBox txtObservacoes;
        private System.Windows.Forms.TextBox txtSaldoInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}