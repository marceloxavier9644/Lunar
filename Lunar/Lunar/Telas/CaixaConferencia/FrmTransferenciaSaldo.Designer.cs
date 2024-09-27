namespace Lunar.Telas.CaixaConferencia
{
    partial class FrmTransferenciaSaldo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTransferenciaSaldo));
            this.comboOrigem = new MaterialSkin.Controls.MaterialComboBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.comboDestino = new MaterialSkin.Controls.MaterialComboBox();
            this.txtSaldoAtual = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.txtValorTransferir = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.txtObservacoes = new System.Windows.Forms.TextBox();
            this.btnConfirmar = new MaterialSkin.Controls.MaterialButton();
            this.SuspendLayout();
            // 
            // comboOrigem
            // 
            this.comboOrigem.AutoResize = false;
            this.comboOrigem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboOrigem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboOrigem.Depth = 0;
            this.comboOrigem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboOrigem.DropDownHeight = 174;
            this.comboOrigem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOrigem.DropDownWidth = 121;
            this.comboOrigem.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.comboOrigem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboOrigem.FormattingEnabled = true;
            this.comboOrigem.IntegralHeight = false;
            this.comboOrigem.ItemHeight = 43;
            this.comboOrigem.Location = new System.Drawing.Point(21, 36);
            this.comboOrigem.MaxDropDownItems = 4;
            this.comboOrigem.MouseState = MaterialSkin.MouseState.OUT;
            this.comboOrigem.Name = "comboOrigem";
            this.comboOrigem.Size = new System.Drawing.Size(420, 49);
            this.comboOrigem.TabIndex = 1;
            this.comboOrigem.SelectedIndexChanged += new System.EventHandler(this.comboOrigem_SelectedIndexChanged);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(18, 14);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(119, 19);
            this.materialLabel1.TabIndex = 2;
            this.materialLabel1.Text = "Conta de Origem";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(18, 163);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(122, 19);
            this.materialLabel2.TabIndex = 4;
            this.materialLabel2.Text = "Conta de Destino";
            // 
            // comboDestino
            // 
            this.comboDestino.AutoResize = false;
            this.comboDestino.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboDestino.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboDestino.Depth = 0;
            this.comboDestino.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboDestino.DropDownHeight = 174;
            this.comboDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDestino.DropDownWidth = 121;
            this.comboDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.comboDestino.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboDestino.FormattingEnabled = true;
            this.comboDestino.IntegralHeight = false;
            this.comboDestino.ItemHeight = 43;
            this.comboDestino.Location = new System.Drawing.Point(21, 185);
            this.comboDestino.MaxDropDownItems = 4;
            this.comboDestino.MouseState = MaterialSkin.MouseState.OUT;
            this.comboDestino.Name = "comboDestino";
            this.comboDestino.Size = new System.Drawing.Size(420, 49);
            this.comboDestino.TabIndex = 3;
            // 
            // txtSaldoAtual
            // 
            this.txtSaldoAtual.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSaldoAtual.Depth = 0;
            this.txtSaldoAtual.Enabled = false;
            this.txtSaldoAtual.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtSaldoAtual.Location = new System.Drawing.Point(21, 110);
            this.txtSaldoAtual.MaxLength = 50;
            this.txtSaldoAtual.MouseState = MaterialSkin.MouseState.OUT;
            this.txtSaldoAtual.Multiline = false;
            this.txtSaldoAtual.Name = "txtSaldoAtual";
            this.txtSaldoAtual.Size = new System.Drawing.Size(238, 50);
            this.txtSaldoAtual.TabIndex = 5;
            this.txtSaldoAtual.Text = "";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(18, 88);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(83, 19);
            this.materialLabel3.TabIndex = 6;
            this.materialLabel3.Text = "Saldo Atual";
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel4.Location = new System.Drawing.Point(18, 237);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(124, 19);
            this.materialLabel4.TabIndex = 8;
            this.materialLabel4.Text = "Valor a Transferir";
            // 
            // txtValorTransferir
            // 
            this.txtValorTransferir.BackColor = System.Drawing.Color.White;
            this.txtValorTransferir.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtValorTransferir.Depth = 0;
            this.txtValorTransferir.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtValorTransferir.Location = new System.Drawing.Point(21, 259);
            this.txtValorTransferir.MaxLength = 50;
            this.txtValorTransferir.MouseState = MaterialSkin.MouseState.OUT;
            this.txtValorTransferir.Multiline = false;
            this.txtValorTransferir.Name = "txtValorTransferir";
            this.txtValorTransferir.Size = new System.Drawing.Size(238, 50);
            this.txtValorTransferir.TabIndex = 7;
            this.txtValorTransferir.Text = "";
            this.txtValorTransferir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValorTransferir_KeyPress);
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel5.Location = new System.Drawing.Point(18, 332);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(92, 19);
            this.materialLabel5.TabIndex = 10;
            this.materialLabel5.Text = "Observações";
            // 
            // txtObservacoes
            // 
            this.txtObservacoes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservacoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservacoes.Location = new System.Drawing.Point(21, 354);
            this.txtObservacoes.Multiline = true;
            this.txtObservacoes.Name = "txtObservacoes";
            this.txtObservacoes.Size = new System.Drawing.Size(420, 154);
            this.txtObservacoes.TabIndex = 11;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.AutoSize = false;
            this.btnConfirmar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConfirmar.Depth = 0;
            this.btnConfirmar.DrawShadows = true;
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.HighEmphasis = true;
            this.btnConfirmar.Icon = null;
            this.btnConfirmar.Location = new System.Drawing.Point(151, 527);
            this.btnConfirmar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnConfirmar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(158, 46);
            this.btnConfirmar.TabIndex = 12;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnConfirmar.UseAccentColor = false;
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // FrmTransferenciaSaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(461, 588);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtObservacoes);
            this.Controls.Add(this.materialLabel5);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.txtValorTransferir);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.txtSaldoAtual);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.comboDestino);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.comboOrigem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTransferenciaSaldo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transferência de Saldo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialComboBox comboOrigem;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialComboBox comboDestino;
        private MaterialSkin.Controls.MaterialTextBox txtSaldoAtual;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialTextBox txtValorTransferir;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private System.Windows.Forms.TextBox txtObservacoes;
        private MaterialSkin.Controls.MaterialButton btnConfirmar;
    }
}