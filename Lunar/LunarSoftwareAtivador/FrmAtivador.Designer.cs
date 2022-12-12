namespace LunarSoftwareAtivador
{
    partial class FrmAtivador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAtivador));
            this.autoLabel33 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtSerial = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCNPJ = new Syncfusion.Windows.Forms.Tools.MaskedEditBox();
            this.btnGerarRegistro = new Syncfusion.WinForms.Controls.SfButton();
            this.txtSerialHD = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtData = new Syncfusion.Windows.Forms.Tools.MaskedEditBox();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtSenha = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCNPJ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerialHD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSenha)).BeginInit();
            this.SuspendLayout();
            // 
            // autoLabel33
            // 
            this.autoLabel33.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel33.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel33.ForeColor = System.Drawing.Color.Black;
            this.autoLabel33.Location = new System.Drawing.Point(12, 27);
            this.autoLabel33.Name = "autoLabel33";
            this.autoLabel33.Size = new System.Drawing.Size(118, 20);
            this.autoLabel33.TabIndex = 261;
            this.autoLabel33.Text = "Serial do Painel";
            // 
            // txtSerial
            // 
            this.txtSerial.BeforeTouchSize = new System.Drawing.Size(161, 26);
            this.txtSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerial.Location = new System.Drawing.Point(12, 50);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(371, 26);
            this.txtSerial.TabIndex = 0;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(389, 27);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(102, 20);
            this.autoLabel1.TabIndex = 263;
            this.autoLabel1.Text = "CNPJ Cliente";
            // 
            // txtCNPJ
            // 
            this.txtCNPJ.BeforeTouchSize = new System.Drawing.Size(161, 26);
            this.txtCNPJ.ClipMode = Syncfusion.Windows.Forms.Tools.ClipModes.ExcludeLiterals;
            this.txtCNPJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCNPJ.Location = new System.Drawing.Point(389, 50);
            this.txtCNPJ.Mask = "##,###,###/####-##";
            this.txtCNPJ.MaxLength = 18;
            this.txtCNPJ.Name = "txtCNPJ";
            this.txtCNPJ.Size = new System.Drawing.Size(161, 26);
            this.txtCNPJ.TabIndex = 1;
            this.txtCNPJ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGerarRegistro
            // 
            this.btnGerarRegistro.AccessibleName = "Button";
            this.btnGerarRegistro.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarRegistro.Location = new System.Drawing.Point(182, 178);
            this.btnGerarRegistro.Name = "btnGerarRegistro";
            this.btnGerarRegistro.Size = new System.Drawing.Size(199, 38);
            this.btnGerarRegistro.TabIndex = 5;
            this.btnGerarRegistro.Text = "Gerar Arquivo de Registro";
            this.btnGerarRegistro.Click += new System.EventHandler(this.btnGerarRegistro_Click);
            // 
            // txtSerialHD
            // 
            this.txtSerialHD.BeforeTouchSize = new System.Drawing.Size(161, 26);
            this.txtSerialHD.Enabled = false;
            this.txtSerialHD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialHD.Location = new System.Drawing.Point(12, 103);
            this.txtSerialHD.Name = "txtSerialHD";
            this.txtSerialHD.Size = new System.Drawing.Size(210, 26);
            this.txtSerialHD.TabIndex = 2;
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(12, 80);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(159, 20);
            this.autoLabel2.TabIndex = 266;
            this.autoLabel2.Text = "Serial C (Automático)";
            // 
            // txtData
            // 
            this.txtData.BeforeTouchSize = new System.Drawing.Size(161, 26);
            this.txtData.Enabled = false;
            this.txtData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtData.Location = new System.Drawing.Point(228, 103);
            this.txtData.Mask = "##/##/####";
            this.txtData.MaxLength = 10;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(155, 26);
            this.txtData.TabIndex = 3;
            this.txtData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(228, 80);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(44, 20);
            this.autoLabel3.TabIndex = 268;
            this.autoLabel3.Text = "Data";
            // 
            // txtSenha
            // 
            this.txtSenha.BeforeTouchSize = new System.Drawing.Size(161, 26);
            this.txtSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha.Location = new System.Drawing.Point(389, 103);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '●';
            this.txtSenha.Size = new System.Drawing.Size(161, 26);
            this.txtSenha.TabIndex = 4;
            this.txtSenha.UseSystemPasswordChar = true;
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(395, 80);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(143, 20);
            this.autoLabel4.TabIndex = 270;
            this.autoLabel4.Text = "Senha de Ativação";
            // 
            // FrmAtivador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(562, 228);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.txtSerialHD);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.btnGerarRegistro);
            this.Controls.Add(this.txtCNPJ);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtSerial);
            this.Controls.Add(this.autoLabel33);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmAtivador";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ativador Lunar Software";
            ((System.ComponentModel.ISupportInitialize)(this.txtSerial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCNPJ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerialHD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSenha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel33;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtSerial;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.MaskedEditBox txtCNPJ;
        private Syncfusion.WinForms.Controls.SfButton btnGerarRegistro;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtSerialHD;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.Windows.Forms.Tools.MaskedEditBox txtData;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtSenha;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
    }
}