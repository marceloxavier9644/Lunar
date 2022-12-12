namespace Lunar.Telas.ContasReceber
{
    partial class FrmDescontoLiquidacaoReceber
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
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.lblTotalVendaOuItem = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSomaMultaJuro = new System.Windows.Forms.Label();
            this.txtDescontoEmValor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
            this.txtDescontoPercentual = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtValorPrincipal = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtJuro = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtMulta = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtTotalComDesconto = new Lunar.RJ_UI.Classes.RJTextBox();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel2);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(341, 44);
            this.panelTitleBar.TabIndex = 205;
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.White;
            this.autoLabel2.Location = new System.Drawing.Point(5, 9);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(209, 25);
            this.autoLabel2.TabIndex = 198;
            this.autoLabel2.Text = "Desconto/Acréscimo";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClose.IconSize = 30;
            this.btnClose.Location = new System.Drawing.Point(300, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTotalVendaOuItem
            // 
            this.lblTotalVendaOuItem.AutoSize = true;
            this.lblTotalVendaOuItem.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVendaOuItem.ForeColor = System.Drawing.Color.Black;
            this.lblTotalVendaOuItem.Location = new System.Drawing.Point(17, 434);
            this.lblTotalVendaOuItem.Name = "lblTotalVendaOuItem";
            this.lblTotalVendaOuItem.Size = new System.Drawing.Size(60, 31);
            this.lblTotalVendaOuItem.TabIndex = 226;
            this.lblTotalVendaOuItem.Text = "Total";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(17, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 31);
            this.label1.TabIndex = 228;
            this.label1.Text = "Multa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(17, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 31);
            this.label2.TabIndex = 230;
            this.label2.Text = "Juro";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(17, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 31);
            this.label3.TabIndex = 232;
            this.label3.Text = "Valor Principal";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(17, 347);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 31);
            this.label4.TabIndex = 234;
            this.label4.Text = "Desconto %";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(168, 347);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 31);
            this.label5.TabIndex = 237;
            this.label5.Text = "Desconto R$";
            // 
            // lblSomaMultaJuro
            // 
            this.lblSomaMultaJuro.AutoSize = true;
            this.lblSomaMultaJuro.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSomaMultaJuro.ForeColor = System.Drawing.Color.Red;
            this.lblSomaMultaJuro.Location = new System.Drawing.Point(31, 320);
            this.lblSomaMultaJuro.Name = "lblSomaMultaJuro";
            this.lblSomaMultaJuro.Size = new System.Drawing.Size(255, 27);
            this.lblSomaMultaJuro.TabIndex = 238;
            this.lblSomaMultaJuro.Text = "Soma de Multa e Juros R$ 0,00";
            this.lblSomaMultaJuro.Visible = false;
            // 
            // txtDescontoEmValor
            // 
            this.txtDescontoEmValor.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.txtDescontoEmValor.BackColor = System.Drawing.Color.White;
            this.txtDescontoEmValor.BorderColor = System.Drawing.Color.Silver;
            this.txtDescontoEmValor.BorderFocusColor = System.Drawing.Color.Silver;
            this.txtDescontoEmValor.BorderRadius = 8;
            this.txtDescontoEmValor.BorderSize = 2;
            this.txtDescontoEmValor.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescontoEmValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDescontoEmValor.Location = new System.Drawing.Point(169, 377);
            this.txtDescontoEmValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescontoEmValor.Multiline = false;
            this.txtDescontoEmValor.Name = "txtDescontoEmValor";
            this.txtDescontoEmValor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescontoEmValor.PasswordChar = false;
            this.txtDescontoEmValor.PlaceholderColor = System.Drawing.Color.Black;
            this.txtDescontoEmValor.PlaceholderText = "";
            this.txtDescontoEmValor.ReadOnly = false;
            this.txtDescontoEmValor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDescontoEmValor.Size = new System.Drawing.Size(147, 48);
            this.txtDescontoEmValor.TabIndex = 0;
            this.txtDescontoEmValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescontoEmValor.Texts = "0";
            this.txtDescontoEmValor.UnderlinedStyle = false;
            this.txtDescontoEmValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescontoEmValor_KeyPress);
            this.txtDescontoEmValor.Leave += new System.EventHandler(this.txtDescontoEmValor_Leave);
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
            this.btnConfirmar.ForeColor = System.Drawing.Color.Transparent;
            this.btnConfirmar.Location = new System.Drawing.Point(21, 536);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(298, 45);
            this.btnConfirmar.TabIndex = 1;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.Transparent;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // txtDescontoPercentual
            // 
            this.txtDescontoPercentual.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.txtDescontoPercentual.BackColor = System.Drawing.Color.White;
            this.txtDescontoPercentual.BorderColor = System.Drawing.Color.Silver;
            this.txtDescontoPercentual.BorderFocusColor = System.Drawing.Color.Silver;
            this.txtDescontoPercentual.BorderRadius = 8;
            this.txtDescontoPercentual.BorderSize = 2;
            this.txtDescontoPercentual.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescontoPercentual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDescontoPercentual.Location = new System.Drawing.Point(18, 377);
            this.txtDescontoPercentual.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescontoPercentual.Multiline = false;
            this.txtDescontoPercentual.Name = "txtDescontoPercentual";
            this.txtDescontoPercentual.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescontoPercentual.PasswordChar = false;
            this.txtDescontoPercentual.PlaceholderColor = System.Drawing.Color.Black;
            this.txtDescontoPercentual.PlaceholderText = "";
            this.txtDescontoPercentual.ReadOnly = false;
            this.txtDescontoPercentual.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDescontoPercentual.Size = new System.Drawing.Size(143, 48);
            this.txtDescontoPercentual.TabIndex = 4;
            this.txtDescontoPercentual.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescontoPercentual.Texts = "0";
            this.txtDescontoPercentual.UnderlinedStyle = false;
            this.txtDescontoPercentual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescontoPercentual_KeyPress);
            this.txtDescontoPercentual.Leave += new System.EventHandler(this.txtDescontoPercentual_Leave);
            // 
            // txtValorPrincipal
            // 
            this.txtValorPrincipal.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.txtValorPrincipal.BackColor = System.Drawing.Color.White;
            this.txtValorPrincipal.BorderColor = System.Drawing.Color.Silver;
            this.txtValorPrincipal.BorderFocusColor = System.Drawing.Color.Silver;
            this.txtValorPrincipal.BorderRadius = 8;
            this.txtValorPrincipal.BorderSize = 2;
            this.txtValorPrincipal.Enabled = false;
            this.txtValorPrincipal.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorPrincipal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtValorPrincipal.Location = new System.Drawing.Point(18, 97);
            this.txtValorPrincipal.Margin = new System.Windows.Forms.Padding(4);
            this.txtValorPrincipal.Multiline = false;
            this.txtValorPrincipal.Name = "txtValorPrincipal";
            this.txtValorPrincipal.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValorPrincipal.PasswordChar = false;
            this.txtValorPrincipal.PlaceholderColor = System.Drawing.Color.Black;
            this.txtValorPrincipal.PlaceholderText = "";
            this.txtValorPrincipal.ReadOnly = false;
            this.txtValorPrincipal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtValorPrincipal.Size = new System.Drawing.Size(298, 48);
            this.txtValorPrincipal.TabIndex = 0;
            this.txtValorPrincipal.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValorPrincipal.Texts = "0";
            this.txtValorPrincipal.UnderlinedStyle = false;
            // 
            // txtJuro
            // 
            this.txtJuro.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.txtJuro.BackColor = System.Drawing.Color.White;
            this.txtJuro.BorderColor = System.Drawing.Color.Silver;
            this.txtJuro.BorderFocusColor = System.Drawing.Color.Silver;
            this.txtJuro.BorderRadius = 8;
            this.txtJuro.BorderSize = 2;
            this.txtJuro.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJuro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtJuro.Location = new System.Drawing.Point(18, 269);
            this.txtJuro.Margin = new System.Windows.Forms.Padding(4);
            this.txtJuro.Multiline = false;
            this.txtJuro.Name = "txtJuro";
            this.txtJuro.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtJuro.PasswordChar = false;
            this.txtJuro.PlaceholderColor = System.Drawing.Color.Black;
            this.txtJuro.PlaceholderText = "";
            this.txtJuro.ReadOnly = false;
            this.txtJuro.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtJuro.Size = new System.Drawing.Size(298, 48);
            this.txtJuro.TabIndex = 3;
            this.txtJuro.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtJuro.Texts = "0";
            this.txtJuro.UnderlinedStyle = false;
            this.txtJuro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJuro_KeyPress);
            this.txtJuro.Leave += new System.EventHandler(this.txtJuro_Leave);
            // 
            // txtMulta
            // 
            this.txtMulta.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.txtMulta.BackColor = System.Drawing.Color.White;
            this.txtMulta.BorderColor = System.Drawing.Color.Silver;
            this.txtMulta.BorderFocusColor = System.Drawing.Color.Silver;
            this.txtMulta.BorderRadius = 8;
            this.txtMulta.BorderSize = 2;
            this.txtMulta.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMulta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtMulta.Location = new System.Drawing.Point(18, 182);
            this.txtMulta.Margin = new System.Windows.Forms.Padding(4);
            this.txtMulta.Multiline = false;
            this.txtMulta.Name = "txtMulta";
            this.txtMulta.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtMulta.PasswordChar = false;
            this.txtMulta.PlaceholderColor = System.Drawing.Color.Black;
            this.txtMulta.PlaceholderText = "";
            this.txtMulta.ReadOnly = false;
            this.txtMulta.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMulta.Size = new System.Drawing.Size(298, 48);
            this.txtMulta.TabIndex = 2;
            this.txtMulta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtMulta.Texts = "0";
            this.txtMulta.UnderlinedStyle = false;
            this.txtMulta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMulta_KeyPress);
            this.txtMulta.Leave += new System.EventHandler(this.txtMulta_Leave);
            // 
            // txtTotalComDesconto
            // 
            this.txtTotalComDesconto.BackColor = System.Drawing.Color.White;
            this.txtTotalComDesconto.BorderColor = System.Drawing.Color.Gray;
            this.txtTotalComDesconto.BorderFocusColor = System.Drawing.Color.Silver;
            this.txtTotalComDesconto.BorderRadius = 8;
            this.txtTotalComDesconto.BorderSize = 2;
            this.txtTotalComDesconto.Enabled = false;
            this.txtTotalComDesconto.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalComDesconto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTotalComDesconto.Location = new System.Drawing.Point(18, 464);
            this.txtTotalComDesconto.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalComDesconto.Multiline = false;
            this.txtTotalComDesconto.Name = "txtTotalComDesconto";
            this.txtTotalComDesconto.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtTotalComDesconto.PasswordChar = false;
            this.txtTotalComDesconto.PlaceholderColor = System.Drawing.Color.Black;
            this.txtTotalComDesconto.PlaceholderText = "";
            this.txtTotalComDesconto.ReadOnly = false;
            this.txtTotalComDesconto.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalComDesconto.Size = new System.Drawing.Size(298, 48);
            this.txtTotalComDesconto.TabIndex = 5;
            this.txtTotalComDesconto.TabStop = false;
            this.txtTotalComDesconto.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTotalComDesconto.Texts = "0,00";
            this.txtTotalComDesconto.UnderlinedStyle = false;
            // 
            // FrmDescontoLiquidacaoReceber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(341, 593);
            this.Controls.Add(this.lblSomaMultaJuro);
            this.Controls.Add(this.txtDescontoEmValor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtDescontoPercentual);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtValorPrincipal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtJuro);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMulta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTotalComDesconto);
            this.Controls.Add(this.lblTotalVendaOuItem);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmDescontoLiquidacaoReceber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmDescontoLiquidacaoReceber";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private FontAwesome.Sharp.IconButton btnClose;
        private RJ_UI.Classes.RJTextBox txtTotalComDesconto;
        private System.Windows.Forms.Label lblTotalVendaOuItem;
        private RJ_UI.Classes.RJTextBox txtMulta;
        private System.Windows.Forms.Label label1;
        private RJ_UI.Classes.RJTextBox txtJuro;
        private System.Windows.Forms.Label label2;
        private RJ_UI.Classes.RJTextBox txtValorPrincipal;
        private System.Windows.Forms.Label label3;
        private RJ_UI.Classes.RJTextBox txtDescontoPercentual;
        private System.Windows.Forms.Label label4;
        private RJ_UI.Classes.RJButton btnConfirmar;
        private RJ_UI.Classes.RJTextBox txtDescontoEmValor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSomaMultaJuro;
    }
}