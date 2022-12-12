namespace Lunar.Telas.ContasReceber
{
    partial class FrmValorParcial
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
            this.autoLabel7 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtValorParcial = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
            this.lblValorParcela = new System.Windows.Forms.Label();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel7);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(500, 44);
            this.panelTitleBar.TabIndex = 239;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // autoLabel7
            // 
            this.autoLabel7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel7.ForeColor = System.Drawing.Color.White;
            this.autoLabel7.Location = new System.Drawing.Point(12, 9);
            this.autoLabel7.Name = "autoLabel7";
            this.autoLabel7.Size = new System.Drawing.Size(210, 25);
            this.autoLabel7.TabIndex = 199;
            this.autoLabel7.Text = "Recebimento Parcial";
            // 
            // autoLabel6
            // 
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(98, 110);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(84, 16);
            this.autoLabel6.TabIndex = 244;
            this.autoLabel6.Text = "Valor Parcial";
            // 
            // txtValorParcial
            // 
            this.txtValorParcial.BackColor = System.Drawing.Color.White;
            this.txtValorParcial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValorParcial.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValorParcial.BorderRadius = 8;
            this.txtValorParcial.BorderSize = 2;
            this.txtValorParcial.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorParcial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtValorParcial.Location = new System.Drawing.Point(86, 122);
            this.txtValorParcial.Margin = new System.Windows.Forms.Padding(4);
            this.txtValorParcial.Multiline = false;
            this.txtValorParcial.Name = "txtValorParcial";
            this.txtValorParcial.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValorParcial.PasswordChar = false;
            this.txtValorParcial.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtValorParcial.PlaceholderText = "";
            this.txtValorParcial.ReadOnly = false;
            this.txtValorParcial.Size = new System.Drawing.Size(328, 37);
            this.txtValorParcial.TabIndex = 0;
            this.txtValorParcial.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValorParcial.Texts = "";
            this.txtValorParcial.UnderlinedStyle = false;
            this.txtValorParcial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValorParcial_KeyPress);
            this.txtValorParcial.Leave += new System.EventHandler(this.txtValorParcial_Leave);
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
            this.btnConfirmar.Location = new System.Drawing.Point(142, 195);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(217, 45);
            this.btnConfirmar.TabIndex = 1;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.White;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // lblValorParcela
            // 
            this.lblValorParcela.AutoSize = true;
            this.lblValorParcela.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorParcela.Location = new System.Drawing.Point(143, 61);
            this.lblValorParcela.Name = "lblValorParcela";
            this.lblValorParcela.Size = new System.Drawing.Size(214, 20);
            this.lblValorParcela.TabIndex = 246;
            this.lblValorParcela.Text = "Parcela Selecionada R$ 0,00";
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
            this.btnClose.Location = new System.Drawing.Point(459, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmValorParcial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 252);
            this.Controls.Add(this.lblValorParcela);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.autoLabel6);
            this.Controls.Add(this.txtValorParcial);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmValorParcial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recebimento Parcial";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private FontAwesome.Sharp.IconButton btnClose;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel7;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private RJ_UI.Classes.RJTextBox txtValorParcial;
        private RJ_UI.Classes.RJButton btnConfirmar;
        private System.Windows.Forms.Label lblValorParcela;
    }
}