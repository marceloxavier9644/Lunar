namespace Lunar.Telas.CaixaConferencia
{
    partial class FrmAbrirCaixa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbrirCaixa));
            this.txtUsuario = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.txtCodUsuario = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.txtDataAbertura = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.txtSaldoInicial = new MaterialSkin.Controls.MaterialTextBox();
            this.btnAbrirCaixa = new MaterialSkin.Controls.MaterialButton();
            this.btnAjustar = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // txtUsuario
            // 
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsuario.Depth = 0;
            this.txtUsuario.Enabled = false;
            this.txtUsuario.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtUsuario.Location = new System.Drawing.Point(12, 42);
            this.txtUsuario.MaxLength = 50;
            this.txtUsuario.MouseState = MaterialSkin.MouseState.OUT;
            this.txtUsuario.Multiline = false;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(287, 50);
            this.txtUsuario.TabIndex = 0;
            this.txtUsuario.Text = "";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(12, 20);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(127, 19);
            this.materialLabel1.TabIndex = 1;
            this.materialLabel1.Text = "Usuário/Operador";
            // 
            // txtCodUsuario
            // 
            this.txtCodUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCodUsuario.Depth = 0;
            this.txtCodUsuario.Enabled = false;
            this.txtCodUsuario.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtCodUsuario.Location = new System.Drawing.Point(305, 42);
            this.txtCodUsuario.MaxLength = 50;
            this.txtCodUsuario.MouseState = MaterialSkin.MouseState.OUT;
            this.txtCodUsuario.Multiline = false;
            this.txtCodUsuario.Name = "txtCodUsuario";
            this.txtCodUsuario.Size = new System.Drawing.Size(168, 50);
            this.txtCodUsuario.TabIndex = 2;
            this.txtCodUsuario.Text = "";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(302, 20);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(71, 19);
            this.materialLabel2.TabIndex = 3;
            this.materialLabel2.Text = "Código/Id";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(12, 95);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(99, 19);
            this.materialLabel3.TabIndex = 5;
            this.materialLabel3.Text = "Data Abertura";
            // 
            // txtDataAbertura
            // 
            this.txtDataAbertura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtDataAbertura.Location = new System.Drawing.Point(12, 117);
            this.txtDataAbertura.Name = "txtDataAbertura";
            this.txtDataAbertura.Size = new System.Drawing.Size(138, 50);
            this.txtDataAbertura.TabIndex = 6;
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel4.Location = new System.Drawing.Point(156, 95);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(177, 19);
            this.materialLabel4.TabIndex = 8;
            this.materialLabel4.Text = "Saldo Inicial em Dinheiro";
            // 
            // txtSaldoInicial
            // 
            this.txtSaldoInicial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSaldoInicial.Depth = 0;
            this.txtSaldoInicial.Enabled = false;
            this.txtSaldoInicial.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtSaldoInicial.Location = new System.Drawing.Point(156, 118);
            this.txtSaldoInicial.MaxLength = 50;
            this.txtSaldoInicial.MouseState = MaterialSkin.MouseState.OUT;
            this.txtSaldoInicial.Multiline = false;
            this.txtSaldoInicial.Name = "txtSaldoInicial";
            this.txtSaldoInicial.Size = new System.Drawing.Size(317, 50);
            this.txtSaldoInicial.TabIndex = 7;
            this.txtSaldoInicial.Text = "";
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
            this.btnAbrirCaixa.Location = new System.Drawing.Point(153, 231);
            this.btnAbrirCaixa.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnAbrirCaixa.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAbrirCaixa.Name = "btnAbrirCaixa";
            this.btnAbrirCaixa.Size = new System.Drawing.Size(184, 50);
            this.btnAbrirCaixa.TabIndex = 9;
            this.btnAbrirCaixa.Text = "Abrir Caixa [F8]";
            this.btnAbrirCaixa.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnAbrirCaixa.UseAccentColor = false;
            this.btnAbrirCaixa.UseVisualStyleBackColor = true;
            this.btnAbrirCaixa.Click += new System.EventHandler(this.btnAbrirCaixa_Click);
            // 
            // btnAjustar
            // 
            this.btnAjustar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAjustar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjustar.IconChar = FontAwesome.Sharp.IconChar.CashRegister;
            this.btnAjustar.IconColor = System.Drawing.Color.LimeGreen;
            this.btnAjustar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAjustar.IconSize = 35;
            this.btnAjustar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjustar.Location = new System.Drawing.Point(373, 173);
            this.btnAjustar.Name = "btnAjustar";
            this.btnAjustar.Size = new System.Drawing.Size(100, 40);
            this.btnAjustar.TabIndex = 10;
            this.btnAjustar.Text = "Ajustar";
            this.btnAjustar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAjustar.UseVisualStyleBackColor = true;
            // 
            // FrmAbrirCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(490, 296);
            this.Controls.Add(this.btnAjustar);
            this.Controls.Add(this.btnAbrirCaixa);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.txtSaldoInicial);
            this.Controls.Add(this.txtDataAbertura);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.txtCodUsuario);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.txtUsuario);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbrirCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Abertura de Caixa";
            this.Load += new System.EventHandler(this.FrmAbrirCaixa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox txtUsuario;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialTextBox txtCodUsuario;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataAbertura;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialTextBox txtSaldoInicial;
        private MaterialSkin.Controls.MaterialButton btnAbrirCaixa;
        private FontAwesome.Sharp.IconButton btnAjustar;
    }
}