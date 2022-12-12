namespace Lunar.Telas.UsuarioRegistro
{
    partial class FrmPermissaoUsuario
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
            this.tabControlAdv1 = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.tabPageAdv1 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.txtID = new Lunar.RJ_UI.Classes.RJTextBox();
            this.lblRazaoSocial = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtLogin = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnSalvar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnCancelar = new Lunar.RJ_UI.Classes.RJButton();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).BeginInit();
            this.tabControlAdv1.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlAdv1
            // 
            this.tabControlAdv1.ActiveTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tabControlAdv1.BeforeTouchSize = new System.Drawing.Size(786, 382);
            this.tabControlAdv1.Controls.Add(this.tabPageAdv1);
            this.tabControlAdv1.FocusOnTabClick = false;
            this.tabControlAdv1.Location = new System.Drawing.Point(12, 119);
            this.tabControlAdv1.Name = "tabControlAdv1";
            this.tabControlAdv1.Size = new System.Drawing.Size(786, 382);
            this.tabControlAdv1.TabIndex = 0;
            this.tabControlAdv1.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererOffice2016White);
            this.tabControlAdv1.ThemeName = "TabRendererOffice2016White";
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel2);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(800, 44);
            this.panelTitleBar.TabIndex = 202;
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.White;
            this.autoLabel2.Location = new System.Drawing.Point(5, 5);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(351, 35);
            this.autoLabel2.TabIndex = 198;
            this.autoLabel2.Text = "Cadastro de Grupo e Permissões";
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
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Multiply;
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClose.IconSize = 30;
            this.btnClose.Location = new System.Drawing.Point(759, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 41);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabPageAdv1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageAdv1.Image = null;
            this.tabPageAdv1.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPageAdv1.Location = new System.Drawing.Point(1, 29);
            this.tabPageAdv1.Name = "tabPageAdv1";
            this.tabPageAdv1.ShowCloseButton = true;
            this.tabPageAdv1.Size = new System.Drawing.Size(783, 351);
            this.tabPageAdv1.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageAdv1.TabIndex = 1;
            this.tabPageAdv1.Text = "Geral";
            this.tabPageAdv1.ThemesEnabled = false;
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtID.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtID.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtID.BorderRadius = 8;
            this.txtID.BorderSize = 2;
            this.txtID.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtID.Location = new System.Drawing.Point(13, 67);
            this.txtID.Margin = new System.Windows.Forms.Padding(4);
            this.txtID.Multiline = false;
            this.txtID.Name = "txtID";
            this.txtID.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtID.PasswordChar = false;
            this.txtID.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtID.PlaceholderText = "";
            this.txtID.ReadOnly = false;
            this.txtID.Size = new System.Drawing.Size(115, 44);
            this.txtID.TabIndex = 208;
            this.txtID.Tag = "";
            this.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtID.Texts = "";
            this.txtID.UnderlinedStyle = false;
            // 
            // lblRazaoSocial
            // 
            this.lblRazaoSocial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblRazaoSocial.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblRazaoSocial.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRazaoSocial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblRazaoSocial.Location = new System.Drawing.Point(141, 46);
            this.lblRazaoSocial.Name = "lblRazaoSocial";
            this.lblRazaoSocial.Size = new System.Drawing.Size(50, 21);
            this.lblRazaoSocial.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016White;
            this.lblRazaoSocial.TabIndex = 210;
            this.lblRazaoSocial.Text = "Grupo";
            this.lblRazaoSocial.ThemeName = "Office2016White";
            this.lblRazaoSocial.ThemeStyle.BackColor = System.Drawing.Color.Turquoise;
            // 
            // txtLogin
            // 
            this.txtLogin.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.txtLogin.BackColor = System.Drawing.Color.White;
            this.txtLogin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtLogin.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtLogin.BorderRadius = 8;
            this.txtLogin.BorderSize = 2;
            this.txtLogin.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtLogin.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtLogin.Location = new System.Drawing.Point(136, 68);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(4);
            this.txtLogin.Multiline = false;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtLogin.PasswordChar = false;
            this.txtLogin.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtLogin.PlaceholderText = "";
            this.txtLogin.ReadOnly = false;
            this.txtLogin.Size = new System.Drawing.Size(651, 44);
            this.txtLogin.TabIndex = 209;
            this.txtLogin.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLogin.Texts = "";
            this.txtLogin.UnderlinedStyle = false;
            // 
            // autoLabel1
            // 
            this.autoLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel1.Location = new System.Drawing.Point(16, 46);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(56, 21);
            this.autoLabel1.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016White;
            this.autoLabel1.TabIndex = 211;
            this.autoLabel1.Text = "Código";
            this.autoLabel1.ThemeName = "Office2016White";
            this.autoLabel1.ThemeStyle.BackColor = System.Drawing.Color.Turquoise;
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnSalvar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnSalvar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSalvar.BorderRadius = 8;
            this.btnSalvar.BorderSize = 0;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(554, 507);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(234, 45);
            this.btnSalvar.TabIndex = 212;
            this.btnSalvar.Text = "Salvar [F5]";
            this.btnSalvar.TextColor = System.Drawing.Color.White;
            this.btnSalvar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.BackgroundColor = System.Drawing.Color.White;
            this.btnCancelar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnCancelar.BorderRadius = 8;
            this.btnCancelar.BorderSize = 2;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.Location = new System.Drawing.Point(314, 507);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(234, 45);
            this.btnCancelar.TabIndex = 213;
            this.btnCancelar.TabStop = false;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // FrmPermissaoUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 563);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblRazaoSocial);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.tabControlAdv1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPermissaoUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPermissaoUsuario";
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).EndInit();
            this.tabControlAdv1.ResumeLayout(false);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.TabControlAdv tabControlAdv1;
        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private FontAwesome.Sharp.IconButton btnClose;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageAdv1;
        private RJ_UI.Classes.RJTextBox txtID;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblRazaoSocial;
        private RJ_UI.Classes.RJTextBox txtLogin;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJButton btnSalvar;
        private RJ_UI.Classes.RJButton btnCancelar;
    }
}