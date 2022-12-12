namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    partial class FrmDinheiro
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
            this.txtValor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFormaPagamento = new System.Windows.Forms.Label();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnFechar = new System.Windows.Forms.Button();
            this.lblFaltante = new System.Windows.Forms.Label();
            this.lblTroco = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtValor
            // 
            this.txtValor.BackColor = System.Drawing.Color.White;
            this.txtValor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderRadius = 8;
            this.txtValor.BorderSize = 2;
            this.txtValor.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtValor.Location = new System.Drawing.Point(16, 170);
            this.txtValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtValor.Multiline = false;
            this.txtValor.Name = "txtValor";
            this.txtValor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValor.PasswordChar = false;
            this.txtValor.PlaceholderColor = System.Drawing.Color.Black;
            this.txtValor.PlaceholderText = "";
            this.txtValor.ReadOnly = false;
            this.txtValor.Size = new System.Drawing.Size(317, 48);
            this.txtValor.TabIndex = 112;
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValor.Texts = "";
            this.txtValor.UnderlinedStyle = false;
            this.txtValor._TextChanged += new System.EventHandler(this.txtValor__TextChanged);
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValor_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(88, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 35);
            this.label3.TabIndex = 113;
            this.label3.Text = "Valor Recebido";
            // 
            // lblFormaPagamento
            // 
            this.lblFormaPagamento.AutoSize = true;
            this.lblFormaPagamento.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormaPagamento.ForeColor = System.Drawing.Color.Black;
            this.lblFormaPagamento.Location = new System.Drawing.Point(22, 9);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(104, 35);
            this.lblFormaPagamento.TabIndex = 116;
            this.lblFormaPagamento.Text = "Dinheiro";
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
            this.btnConfirmar.Location = new System.Drawing.Point(25, 306);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(298, 45);
            this.btnConfirmar.TabIndex = 145;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.White;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Image = global::Lunar.Properties.Resources.CloseDark;
            this.btnFechar.Location = new System.Drawing.Point(316, 9);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(30, 31);
            this.btnFechar.TabIndex = 155;
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // lblFaltante
            // 
            this.lblFaltante.AutoSize = true;
            this.lblFaltante.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaltante.ForeColor = System.Drawing.Color.IndianRed;
            this.lblFaltante.Location = new System.Drawing.Point(88, 68);
            this.lblFaltante.Name = "lblFaltante";
            this.lblFaltante.Size = new System.Drawing.Size(157, 35);
            this.lblFaltante.TabIndex = 156;
            this.lblFaltante.Text = "Valor Faltante";
            // 
            // lblTroco
            // 
            this.lblTroco.AutoSize = true;
            this.lblTroco.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTroco.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTroco.Location = new System.Drawing.Point(137, 244);
            this.lblTroco.Name = "lblTroco";
            this.lblTroco.Size = new System.Drawing.Size(76, 35);
            this.lblTroco.TabIndex = 157;
            this.lblTroco.Text = "Troco:";
            this.lblTroco.Visible = false;
            // 
            // FrmDinheiro
            // 
            this.AcceptButton = this.btnConfirmar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(349, 370);
            this.Controls.Add(this.lblTroco);
            this.Controls.Add(this.lblFaltante);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.lblFormaPagamento);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtValor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmDinheiro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmDinheiro";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmDinheiro_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmDinheiro_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDinheiro_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RJ_UI.Classes.RJTextBox txtValor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFormaPagamento;
        private RJ_UI.Classes.RJButton btnConfirmar;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label lblFaltante;
        private System.Windows.Forms.Label lblTroco;
    }
}