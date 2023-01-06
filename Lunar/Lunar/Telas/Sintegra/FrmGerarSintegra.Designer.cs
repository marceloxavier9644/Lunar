namespace Lunar.Telas.Sintegra
{
    partial class FrmGerarSintegra
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
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataInicial = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataFinal = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.chkRegistro74 = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataInventario = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel28 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnCaminhoAnexos = new FontAwesome.Sharp.IconButton();
            this.txtCaminho = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnGerar = new Lunar.RJ_UI.Classes.RJButton();
            ((System.ComponentModel.ISupportInitialize)(this.chkRegistro74)).BeginInit();
            this.SuspendLayout();
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(12, 7);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(84, 21);
            this.autoLabel3.TabIndex = 226;
            this.autoLabel3.Text = "Data Inicial";
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.BackColor = System.Drawing.Color.White;
            this.txtDataInicial.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataInicial.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataInicial.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.txtDataInicial.Location = new System.Drawing.Point(12, 31);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Size = new System.Drawing.Size(185, 35);
            this.txtDataInicial.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.TabIndex = 225;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(203, 7);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(76, 21);
            this.autoLabel1.TabIndex = 228;
            this.autoLabel1.Text = "Data Final";
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.BackColor = System.Drawing.Color.White;
            this.txtDataFinal.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataFinal.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataFinal.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.txtDataFinal.Location = new System.Drawing.Point(203, 31);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Size = new System.Drawing.Size(185, 35);
            this.txtDataFinal.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.Style.DropDown.HoverForeColor = System.Drawing.Color.Black;
            this.txtDataFinal.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.TabIndex = 227;
            // 
            // chkRegistro74
            // 
            this.chkRegistro74.BeforeTouchSize = new System.Drawing.Size(352, 24);
            this.chkRegistro74.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRegistro74.Location = new System.Drawing.Point(12, 156);
            this.chkRegistro74.Name = "chkRegistro74";
            this.chkRegistro74.Size = new System.Drawing.Size(352, 24);
            this.chkRegistro74.TabIndex = 248;
            this.chkRegistro74.Text = " Registro 74 (Inserir Inventário de Estoque)";
            this.chkRegistro74.CheckStateChanged += new System.EventHandler(this.chkRegistro74_CheckStateChanged);
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Enabled = false;
            this.autoLabel2.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(12, 193);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(111, 21);
            this.autoLabel2.TabIndex = 250;
            this.autoLabel2.Text = "Data Inventário";
            // 
            // txtDataInventario
            // 
            this.txtDataInventario.BackColor = System.Drawing.Color.White;
            this.txtDataInventario.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataInventario.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataInventario.Enabled = false;
            this.txtDataInventario.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.txtDataInventario.Location = new System.Drawing.Point(12, 217);
            this.txtDataInventario.Name = "txtDataInventario";
            this.txtDataInventario.Size = new System.Drawing.Size(185, 35);
            this.txtDataInventario.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataInventario.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataInventario.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataInventario.TabIndex = 249;
            // 
            // autoLabel28
            // 
            this.autoLabel28.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel28.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel28.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel28.ForeColor = System.Drawing.Color.Black;
            this.autoLabel28.Location = new System.Drawing.Point(13, 76);
            this.autoLabel28.Name = "autoLabel28";
            this.autoLabel28.Size = new System.Drawing.Size(200, 21);
            this.autoLabel28.TabIndex = 253;
            this.autoLabel28.Text = "Caminho para Salvar Arquivo";
            // 
            // btnCaminhoAnexos
            // 
            this.btnCaminhoAnexos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaminhoAnexos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnCaminhoAnexos.FlatAppearance.BorderSize = 0;
            this.btnCaminhoAnexos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCaminhoAnexos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCaminhoAnexos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaminhoAnexos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaminhoAnexos.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnCaminhoAnexos.IconColor = System.Drawing.Color.SlateGray;
            this.btnCaminhoAnexos.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnCaminhoAnexos.IconSize = 38;
            this.btnCaminhoAnexos.Location = new System.Drawing.Point(395, 104);
            this.btnCaminhoAnexos.Name = "btnCaminhoAnexos";
            this.btnCaminhoAnexos.Size = new System.Drawing.Size(36, 34);
            this.btnCaminhoAnexos.TabIndex = 252;
            this.btnCaminhoAnexos.UseVisualStyleBackColor = true;
            this.btnCaminhoAnexos.Click += new System.EventHandler(this.btnCaminhoAnexos_Click);
            // 
            // txtCaminho
            // 
            this.txtCaminho.BackColor = System.Drawing.Color.White;
            this.txtCaminho.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCaminho.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCaminho.BorderRadius = 8;
            this.txtCaminho.BorderSize = 2;
            this.txtCaminho.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtCaminho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCaminho.Location = new System.Drawing.Point(12, 101);
            this.txtCaminho.Margin = new System.Windows.Forms.Padding(4);
            this.txtCaminho.Multiline = false;
            this.txtCaminho.Name = "txtCaminho";
            this.txtCaminho.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCaminho.PasswordChar = false;
            this.txtCaminho.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCaminho.PlaceholderText = "";
            this.txtCaminho.ReadOnly = false;
            this.txtCaminho.Size = new System.Drawing.Size(375, 37);
            this.txtCaminho.TabIndex = 251;
            this.txtCaminho.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCaminho.Texts = "";
            this.txtCaminho.UnderlinedStyle = false;
            // 
            // btnGerar
            // 
            this.btnGerar.BackColor = System.Drawing.Color.PaleGreen;
            this.btnGerar.BackgroundColor = System.Drawing.Color.PaleGreen;
            this.btnGerar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnGerar.BorderRadius = 8;
            this.btnGerar.BorderSize = 0;
            this.btnGerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGerar.FlatAppearance.BorderSize = 0;
            this.btnGerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerar.ForeColor = System.Drawing.Color.Black;
            this.btnGerar.Location = new System.Drawing.Point(139, 290);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(170, 43);
            this.btnGerar.TabIndex = 247;
            this.btnGerar.Text = "Gerar [F5]";
            this.btnGerar.TextColor = System.Drawing.Color.Black;
            this.btnGerar.UseVisualStyleBackColor = false;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // FrmGerarSintegra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(449, 364);
            this.Controls.Add(this.autoLabel28);
            this.Controls.Add(this.btnCaminhoAnexos);
            this.Controls.Add(this.txtCaminho);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.txtDataInventario);
            this.Controls.Add(this.chkRegistro74);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtDataFinal);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.txtDataInicial);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGerarSintegra";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerar Sintegra";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGerarSintegra_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.chkRegistro74)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataInicial;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataFinal;
        private RJ_UI.Classes.RJButton btnGerar;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv chkRegistro74;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataInventario;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel28;
        private FontAwesome.Sharp.IconButton btnCaminhoAnexos;
        private RJ_UI.Classes.RJTextBox txtCaminho;
    }
}