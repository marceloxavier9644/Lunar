namespace Lunar.Telas.ArquivosContabilidade
{
    partial class FrmEnviarArquivosContabilidade
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
            this.autoLabel25 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnFechar = new FontAwesome.Sharp.IconButton();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel59 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaPasta = new FontAwesome.Sharp.IconButton();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtAno = new System.Windows.Forms.TextBox();
            this.txtMes = new System.Windows.Forms.TextBox();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataInventario = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.chkRegistro74 = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.progressBarAdv1 = new MetroFramework.Controls.MetroProgressBar();
            this.txtPasta = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnEnviar = new Lunar.RJ_UI.Classes.RJButton();
            this.lblInfo = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkRegistro74)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel25);
            this.panelTitleBar.Controls.Add(this.btnFechar);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(626, 38);
            this.panelTitleBar.TabIndex = 258;
            // 
            // autoLabel25
            // 
            this.autoLabel25.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel25.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel25.ForeColor = System.Drawing.Color.White;
            this.autoLabel25.Location = new System.Drawing.Point(5, 7);
            this.autoLabel25.Name = "autoLabel25";
            this.autoLabel25.Size = new System.Drawing.Size(301, 25);
            this.autoLabel25.TabIndex = 198;
            this.autoLabel25.Text = "Enviar Arquivos Contabilidade";
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
            this.btnFechar.IconChar = FontAwesome.Sharp.IconChar.Multiply;
            this.btnFechar.IconColor = System.Drawing.Color.White;
            this.btnFechar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFechar.IconSize = 30;
            this.btnFechar.Location = new System.Drawing.Point(585, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(39, 41);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F);
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(106, 63);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(31, 16);
            this.autoLabel3.TabIndex = 265;
            this.autoLabel3.Text = "Ano";
            // 
            // autoLabel59
            // 
            this.autoLabel59.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel59.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F);
            this.autoLabel59.ForeColor = System.Drawing.Color.Black;
            this.autoLabel59.Location = new System.Drawing.Point(22, 63);
            this.autoLabel59.Name = "autoLabel59";
            this.autoLabel59.Size = new System.Drawing.Size(33, 16);
            this.autoLabel59.TabIndex = 264;
            this.autoLabel59.Text = "Mês";
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(222, 65);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(132, 16);
            this.autoLabel1.TabIndex = 267;
            this.autoLabel1.Text = "E-mail Contabilidade";
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(23, 126);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(163, 16);
            this.autoLabel2.TabIndex = 290;
            this.autoLabel2.Text = "Local Para Salvar Arquivo";
            // 
            // btnPesquisaPasta
            // 
            this.btnPesquisaPasta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaPasta.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaPasta.FlatAppearance.BorderSize = 0;
            this.btnPesquisaPasta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPasta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPasta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaPasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaPasta.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaPasta.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaPasta.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaPasta.IconSize = 38;
            this.btnPesquisaPasta.Location = new System.Drawing.Point(566, 141);
            this.btnPesquisaPasta.Name = "btnPesquisaPasta";
            this.btnPesquisaPasta.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaPasta.TabIndex = 4;
            this.btnPesquisaPasta.UseVisualStyleBackColor = true;
            this.btnPesquisaPasta.Click += new System.EventHandler(this.btnPesquisaPasta_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtEmail.Location = new System.Drawing.Point(222, 84);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(337, 29);
            this.txtEmail.TabIndex = 291;
            // 
            // txtAno
            // 
            this.txtAno.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtAno.Location = new System.Drawing.Point(106, 84);
            this.txtAno.Name = "txtAno";
            this.txtAno.Size = new System.Drawing.Size(110, 29);
            this.txtAno.TabIndex = 292;
            // 
            // txtMes
            // 
            this.txtMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtMes.Location = new System.Drawing.Point(13, 84);
            this.txtMes.Name = "txtMes";
            this.txtMes.Size = new System.Drawing.Size(87, 29);
            this.txtMes.TabIndex = 293;
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Enabled = false;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(12, 207);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(97, 16);
            this.autoLabel4.TabIndex = 297;
            this.autoLabel4.Text = "Data Inventário";
            // 
            // txtDataInventario
            // 
            this.txtDataInventario.BackColor = System.Drawing.Color.White;
            this.txtDataInventario.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataInventario.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataInventario.Enabled = false;
            this.txtDataInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDataInventario.Location = new System.Drawing.Point(12, 226);
            this.txtDataInventario.Name = "txtDataInventario";
            this.txtDataInventario.Size = new System.Drawing.Size(185, 35);
            this.txtDataInventario.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataInventario.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataInventario.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataInventario.TabIndex = 296;
            // 
            // chkRegistro74
            // 
            this.chkRegistro74.BeforeTouchSize = new System.Drawing.Size(352, 24);
            this.chkRegistro74.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRegistro74.Location = new System.Drawing.Point(13, 180);
            this.chkRegistro74.Name = "chkRegistro74";
            this.chkRegistro74.Size = new System.Drawing.Size(352, 24);
            this.chkRegistro74.TabIndex = 295;
            this.chkRegistro74.Text = " Registro 74 (Inserir Inventário de Estoque)";
            this.chkRegistro74.CheckStateChanged += new System.EventHandler(this.chkRegistro74_CheckStateChanged);
            // 
            // progressBarAdv1
            // 
            this.progressBarAdv1.Location = new System.Drawing.Point(22, 335);
            this.progressBarAdv1.Name = "progressBarAdv1";
            this.progressBarAdv1.ProgressBarStyle = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarAdv1.Size = new System.Drawing.Size(590, 23);
            this.progressBarAdv1.Style = MetroFramework.MetroColorStyle.Blue;
            this.progressBarAdv1.TabIndex = 298;
            this.progressBarAdv1.Visible = false;
            // 
            // txtPasta
            // 
            this.txtPasta.BackColor = System.Drawing.Color.White;
            this.txtPasta.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPasta.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPasta.BorderRadius = 8;
            this.txtPasta.BorderSize = 2;
            this.txtPasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPasta.Location = new System.Drawing.Point(13, 138);
            this.txtPasta.Margin = new System.Windows.Forms.Padding(4);
            this.txtPasta.Multiline = false;
            this.txtPasta.Name = "txtPasta";
            this.txtPasta.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtPasta.PasswordChar = false;
            this.txtPasta.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtPasta.PlaceholderText = "";
            this.txtPasta.ReadOnly = false;
            this.txtPasta.Size = new System.Drawing.Size(546, 37);
            this.txtPasta.TabIndex = 3;
            this.txtPasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPasta.Texts = "";
            this.txtPasta.UnderlinedStyle = false;
            // 
            // btnEnviar
            // 
            this.btnEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnEnviar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnEnviar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnEnviar.BorderRadius = 8;
            this.btnEnviar.BorderSize = 0;
            this.btnEnviar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviar.FlatAppearance.BorderSize = 0;
            this.btnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviar.ForeColor = System.Drawing.Color.White;
            this.btnEnviar.Location = new System.Drawing.Point(205, 284);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(217, 45);
            this.btnEnviar.TabIndex = 5;
            this.btnEnviar.Text = "Enviar/Download [F5]";
            this.btnEnviar.TextColor = System.Drawing.Color.White;
            this.btnEnviar.UseVisualStyleBackColor = false;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblInfo.Enabled = false;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.Black;
            this.lblInfo.Location = new System.Drawing.Point(23, 313);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(97, 16);
            this.lblInfo.TabIndex = 299;
            this.lblInfo.Text = "Data Inventário";
            this.lblInfo.Visible = false;
            // 
            // FrmEnviarArquivosContabilidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(626, 361);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.progressBarAdv1);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.txtDataInventario);
            this.Controls.Add(this.chkRegistro74);
            this.Controls.Add(this.txtMes);
            this.Controls.Add(this.txtAno);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnPesquisaPasta);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.txtPasta);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.autoLabel59);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmEnviarArquivosContabilidade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmEnviarArquivosContabilidade";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkRegistro74)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel25;
        private FontAwesome.Sharp.IconButton btnFechar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel59;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJButton btnEnviar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private RJ_UI.Classes.RJTextBox txtPasta;
        private FontAwesome.Sharp.IconButton btnPesquisaPasta;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtAno;
        private System.Windows.Forms.TextBox txtMes;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataInventario;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv chkRegistro74;
        private MetroFramework.Controls.MetroProgressBar progressBarAdv1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblInfo;
    }
}