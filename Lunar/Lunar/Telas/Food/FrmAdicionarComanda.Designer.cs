namespace Lunar.Telas.Food
{
    partial class FrmAdicionarComanda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAdicionarComanda));
            this.txtNumeroComanda = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNomeCliente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.btnConfirmar = new FontAwesome.Sharp.IconButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtQuantidadePessoas = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtNumeroComanda
            // 
            this.txtNumeroComanda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumeroComanda.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroComanda.ForeColor = System.Drawing.Color.Gray;
            this.txtNumeroComanda.Location = new System.Drawing.Point(71, 50);
            this.txtNumeroComanda.Name = "txtNumeroComanda";
            this.txtNumeroComanda.Size = new System.Drawing.Size(200, 31);
            this.txtNumeroComanda.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(67, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nº Comanda";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Cliente";
            // 
            // txtNomeCliente
            // 
            this.txtNomeCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeCliente.ForeColor = System.Drawing.Color.Gray;
            this.txtNomeCliente.Location = new System.Drawing.Point(71, 109);
            this.txtNomeCliente.Name = "txtNomeCliente";
            this.txtNomeCliente.Size = new System.Drawing.Size(430, 31);
            this.txtNomeCliente.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(67, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Observações";
            // 
            // txtObservacao
            // 
            this.txtObservacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservacao.ForeColor = System.Drawing.Color.Gray;
            this.txtObservacao.Location = new System.Drawing.Point(71, 166);
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Size = new System.Drawing.Size(430, 86);
            this.txtObservacao.TabIndex = 3;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.IconChar = FontAwesome.Sharp.IconChar.CircleCheck;
            this.btnConfirmar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnConfirmar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnConfirmar.IconSize = 25;
            this.btnConfirmar.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnConfirmar.Location = new System.Drawing.Point(198, 282);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(172, 37);
            this.btnConfirmar.TabIndex = 4;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(273, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Qtd Pessoas";
            // 
            // txtQuantidadePessoas
            // 
            this.txtQuantidadePessoas.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQuantidadePessoas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidadePessoas.ForeColor = System.Drawing.Color.Gray;
            this.txtQuantidadePessoas.Location = new System.Drawing.Point(277, 50);
            this.txtQuantidadePessoas.Name = "txtQuantidadePessoas";
            this.txtQuantidadePessoas.Size = new System.Drawing.Size(224, 31);
            this.txtQuantidadePessoas.TabIndex = 1;
            // 
            // FrmAdicionarComanda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(569, 359);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtQuantidadePessoas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNomeCliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtNumeroComanda);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAdicionarComanda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adicionar Comanda";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnConfirmar;
        private System.Windows.Forms.TextBox txtNumeroComanda;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNomeCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtQuantidadePessoas;
    }
}