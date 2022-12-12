namespace Lunar.Telas.Vendas.Adicionais
{
    partial class FrmDescontoTotal
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
            this.txtTotalVenda = new Lunar.RJ_UI.Classes.RJTextBox();
            this.lblTotalVendaOuItem = new System.Windows.Forms.Label();
            this.txtDescontoPercentual = new Lunar.RJ_UI.Classes.RJTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescontoEmValor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalComDesconto = new Lunar.RJ_UI.Classes.RJTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
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
            this.panelTitleBar.Size = new System.Drawing.Size(410, 44);
            this.panelTitleBar.TabIndex = 220;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.White;
            this.autoLabel4.Location = new System.Drawing.Point(5, 7);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(113, 35);
            this.autoLabel4.TabIndex = 198;
            this.autoLabel4.Text = "Desconto";
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
            this.btnFechar.Location = new System.Drawing.Point(369, 3);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(39, 41);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.TabStop = false;
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // txtTotalVenda
            // 
            this.txtTotalVenda.BackColor = System.Drawing.Color.White;
            this.txtTotalVenda.BorderColor = System.Drawing.Color.Gray;
            this.txtTotalVenda.BorderFocusColor = System.Drawing.Color.Silver;
            this.txtTotalVenda.BorderRadius = 8;
            this.txtTotalVenda.BorderSize = 2;
            this.txtTotalVenda.Enabled = false;
            this.txtTotalVenda.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalVenda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTotalVenda.Location = new System.Drawing.Point(89, 89);
            this.txtTotalVenda.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalVenda.Multiline = false;
            this.txtTotalVenda.Name = "txtTotalVenda";
            this.txtTotalVenda.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtTotalVenda.PasswordChar = false;
            this.txtTotalVenda.PlaceholderColor = System.Drawing.Color.Black;
            this.txtTotalVenda.PlaceholderText = "";
            this.txtTotalVenda.ReadOnly = false;
            this.txtTotalVenda.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalVenda.Size = new System.Drawing.Size(234, 48);
            this.txtTotalVenda.TabIndex = 0;
            this.txtTotalVenda.TabStop = false;
            this.txtTotalVenda.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTotalVenda.Texts = "0,00";
            this.txtTotalVenda.UnderlinedStyle = false;
            // 
            // lblTotalVendaOuItem
            // 
            this.lblTotalVendaOuItem.AutoSize = true;
            this.lblTotalVendaOuItem.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVendaOuItem.ForeColor = System.Drawing.Color.Black;
            this.lblTotalVendaOuItem.Location = new System.Drawing.Point(88, 59);
            this.lblTotalVendaOuItem.Name = "lblTotalVendaOuItem";
            this.lblTotalVendaOuItem.Size = new System.Drawing.Size(154, 31);
            this.lblTotalVendaOuItem.TabIndex = 224;
            this.lblTotalVendaOuItem.Text = "Total da Venda";
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
            this.txtDescontoPercentual.Location = new System.Drawing.Point(89, 165);
            this.txtDescontoPercentual.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescontoPercentual.Multiline = false;
            this.txtDescontoPercentual.Name = "txtDescontoPercentual";
            this.txtDescontoPercentual.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescontoPercentual.PasswordChar = false;
            this.txtDescontoPercentual.PlaceholderColor = System.Drawing.Color.Black;
            this.txtDescontoPercentual.PlaceholderText = "";
            this.txtDescontoPercentual.ReadOnly = false;
            this.txtDescontoPercentual.Size = new System.Drawing.Size(234, 48);
            this.txtDescontoPercentual.TabIndex = 1;
            this.txtDescontoPercentual.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescontoPercentual.Texts = "0";
            this.txtDescontoPercentual.UnderlinedStyle = false;
            this.txtDescontoPercentual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescontoPercentual_KeyPress);
            this.txtDescontoPercentual.Leave += new System.EventHandler(this.txtDescontoPercentual_Leave);
            this.txtDescontoPercentual.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtDescontoPercentual_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(88, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 31);
            this.label1.TabIndex = 226;
            this.label1.Text = "Desconto em %";
            // 
            // txtDescontoEmValor
            // 
            this.txtDescontoEmValor.BackColor = System.Drawing.Color.White;
            this.txtDescontoEmValor.BorderColor = System.Drawing.Color.Silver;
            this.txtDescontoEmValor.BorderFocusColor = System.Drawing.Color.Silver;
            this.txtDescontoEmValor.BorderRadius = 8;
            this.txtDescontoEmValor.BorderSize = 2;
            this.txtDescontoEmValor.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescontoEmValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDescontoEmValor.Location = new System.Drawing.Point(89, 241);
            this.txtDescontoEmValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescontoEmValor.Multiline = false;
            this.txtDescontoEmValor.Name = "txtDescontoEmValor";
            this.txtDescontoEmValor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescontoEmValor.PasswordChar = false;
            this.txtDescontoEmValor.PlaceholderColor = System.Drawing.Color.Black;
            this.txtDescontoEmValor.PlaceholderText = "";
            this.txtDescontoEmValor.ReadOnly = false;
            this.txtDescontoEmValor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDescontoEmValor.Size = new System.Drawing.Size(234, 48);
            this.txtDescontoEmValor.TabIndex = 2;
            this.txtDescontoEmValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescontoEmValor.Texts = "0,00";
            this.txtDescontoEmValor.UnderlinedStyle = false;
            this.txtDescontoEmValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescontoEmValor_KeyPress);
            this.txtDescontoEmValor.Leave += new System.EventHandler(this.txtDescontoEmValor_Leave);
            this.txtDescontoEmValor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtDescontoEmValor_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(88, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 31);
            this.label2.TabIndex = 228;
            this.label2.Text = "Desconto em R$";
            // 
            // txtTotalComDesconto
            // 
            this.txtTotalComDesconto.BackColor = System.Drawing.Color.White;
            this.txtTotalComDesconto.BorderColor = System.Drawing.Color.Silver;
            this.txtTotalComDesconto.BorderFocusColor = System.Drawing.Color.Silver;
            this.txtTotalComDesconto.BorderRadius = 8;
            this.txtTotalComDesconto.BorderSize = 2;
            this.txtTotalComDesconto.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalComDesconto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTotalComDesconto.Location = new System.Drawing.Point(89, 317);
            this.txtTotalComDesconto.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalComDesconto.Multiline = false;
            this.txtTotalComDesconto.Name = "txtTotalComDesconto";
            this.txtTotalComDesconto.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtTotalComDesconto.PasswordChar = false;
            this.txtTotalComDesconto.PlaceholderColor = System.Drawing.Color.Black;
            this.txtTotalComDesconto.PlaceholderText = "";
            this.txtTotalComDesconto.ReadOnly = false;
            this.txtTotalComDesconto.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalComDesconto.Size = new System.Drawing.Size(234, 48);
            this.txtTotalComDesconto.TabIndex = 3;
            this.txtTotalComDesconto.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTotalComDesconto.Texts = "0,00";
            this.txtTotalComDesconto.UnderlinedStyle = false;
            this.txtTotalComDesconto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalComDesconto_KeyPress);
            this.txtTotalComDesconto.Leave += new System.EventHandler(this.txtTotalComDesconto_Leave);
            this.txtTotalComDesconto.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtTotalComDesconto_MouseClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(88, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 31);
            this.label4.TabIndex = 230;
            this.label4.Text = "Total Com Desconto";
            // 
            // btnConfirmar
            // 
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
            this.btnConfirmar.Location = new System.Drawing.Point(89, 381);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(234, 45);
            this.btnConfirmar.TabIndex = 4;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.White;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // FrmDescontoTotal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(410, 438);
            this.Controls.Add(this.txtTotalComDesconto);
            this.Controls.Add(this.txtDescontoEmValor);
            this.Controls.Add(this.txtDescontoPercentual);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTotalVenda);
            this.Controls.Add(this.lblTotalVendaOuItem);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmDescontoTotal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Desconto";
            this.Load += new System.EventHandler(this.FrmDescontoTotal_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDescontoTotal_KeyDown);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private FontAwesome.Sharp.IconButton btnFechar;
        private RJ_UI.Classes.RJTextBox txtTotalVenda;
        private System.Windows.Forms.Label lblTotalVendaOuItem;
        private RJ_UI.Classes.RJTextBox txtDescontoPercentual;
        private System.Windows.Forms.Label label1;
        private RJ_UI.Classes.RJTextBox txtDescontoEmValor;
        private System.Windows.Forms.Label label2;
        private RJ_UI.Classes.RJTextBox txtTotalComDesconto;
        private System.Windows.Forms.Label label4;
        private RJ_UI.Classes.RJButton btnConfirmar;
    }
}