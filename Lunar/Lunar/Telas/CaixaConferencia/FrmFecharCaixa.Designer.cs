namespace Lunar.Telas.CaixaConferencia
{
    partial class FrmFecharCaixa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFecharCaixa));
            this.label1 = new System.Windows.Forms.Label();
            this.txtValorDinheiro = new System.Windows.Forms.TextBox();
            this.lblDataCaixaFechar = new System.Windows.Forms.Label();
            this.btnFecharCaixa = new MaterialSkin.Controls.MaterialButton();
            this.lblValorSistema = new System.Windows.Forms.Label();
            this.btnCalcular = new FontAwesome.Sharp.IconButton();
            this.lblDiferenca = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(349, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Qual valor em dinheiro você tem no caixa agora?";
            // 
            // txtValorDinheiro
            // 
            this.txtValorDinheiro.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorDinheiro.Location = new System.Drawing.Point(28, 92);
            this.txtValorDinheiro.Name = "txtValorDinheiro";
            this.txtValorDinheiro.Size = new System.Drawing.Size(450, 40);
            this.txtValorDinheiro.TabIndex = 0;
            this.txtValorDinheiro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtValorDinheiro.Leave += new System.EventHandler(this.txtValorDinheiro_Leave);
            // 
            // lblDataCaixaFechar
            // 
            this.lblDataCaixaFechar.AutoSize = true;
            this.lblDataCaixaFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataCaixaFechar.Location = new System.Drawing.Point(166, 18);
            this.lblDataCaixaFechar.Name = "lblDataCaixaFechar";
            this.lblDataCaixaFechar.Size = new System.Drawing.Size(158, 20);
            this.lblDataCaixaFechar.TabIndex = 24;
            this.lblDataCaixaFechar.Text = "CAIXA 11/09/2024";
            // 
            // btnFecharCaixa
            // 
            this.btnFecharCaixa.AutoSize = false;
            this.btnFecharCaixa.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFecharCaixa.BackColor = System.Drawing.Color.Gray;
            this.btnFecharCaixa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFecharCaixa.Depth = 0;
            this.btnFecharCaixa.DrawShadows = true;
            this.btnFecharCaixa.HighEmphasis = true;
            this.btnFecharCaixa.Icon = null;
            this.btnFecharCaixa.Location = new System.Drawing.Point(153, 217);
            this.btnFecharCaixa.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnFecharCaixa.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnFecharCaixa.Name = "btnFecharCaixa";
            this.btnFecharCaixa.Size = new System.Drawing.Size(184, 50);
            this.btnFecharCaixa.TabIndex = 1;
            this.btnFecharCaixa.Text = "Fechar Caixa [Enter]";
            this.btnFecharCaixa.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnFecharCaixa.UseAccentColor = false;
            this.btnFecharCaixa.UseVisualStyleBackColor = false;
            this.btnFecharCaixa.Click += new System.EventHandler(this.btnFecharCaixa_Click);
            // 
            // lblValorSistema
            // 
            this.lblValorSistema.AutoSize = true;
            this.lblValorSistema.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorSistema.ForeColor = System.Drawing.Color.Blue;
            this.lblValorSistema.Location = new System.Drawing.Point(24, 148);
            this.lblValorSistema.Name = "lblValorSistema";
            this.lblValorSistema.Size = new System.Drawing.Size(269, 20);
            this.lblValorSistema.TabIndex = 25;
            this.lblValorSistema.Text = "Pelo Sistema você deve ter R$ 50,00";
            // 
            // btnCalcular
            // 
            this.btnCalcular.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCalcular.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalcular.IconChar = FontAwesome.Sharp.IconChar.Calculator;
            this.btnCalcular.IconColor = System.Drawing.Color.IndianRed;
            this.btnCalcular.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCalcular.IconSize = 35;
            this.btnCalcular.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCalcular.Location = new System.Drawing.Point(370, 138);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(108, 40);
            this.btnCalcular.TabIndex = 26;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // lblDiferenca
            // 
            this.lblDiferenca.AutoSize = true;
            this.lblDiferenca.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiferenca.ForeColor = System.Drawing.Color.Blue;
            this.lblDiferenca.Location = new System.Drawing.Point(24, 178);
            this.lblDiferenca.Name = "lblDiferenca";
            this.lblDiferenca.Size = new System.Drawing.Size(269, 20);
            this.lblDiferenca.TabIndex = 27;
            this.lblDiferenca.Text = "Pelo Sistema você deve ter R$ 50,00";
            this.lblDiferenca.Visible = false;
            // 
            // FrmFecharCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(490, 286);
            this.Controls.Add(this.lblDiferenca);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.lblValorSistema);
            this.Controls.Add(this.btnFecharCaixa);
            this.Controls.Add(this.lblDataCaixaFechar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtValorDinheiro);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFecharCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fechar Caixa";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmFecharCaixa_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtValorDinheiro;
        private System.Windows.Forms.Label lblDataCaixaFechar;
        private MaterialSkin.Controls.MaterialButton btnFecharCaixa;
        private System.Windows.Forms.Label lblValorSistema;
        private FontAwesome.Sharp.IconButton btnCalcular;
        private System.Windows.Forms.Label lblDiferenca;
    }
}