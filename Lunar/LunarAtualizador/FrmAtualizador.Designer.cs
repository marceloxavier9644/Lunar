namespace LunarAtualizador
{
    partial class FrmAtualizador
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAtualizador));
            this.lblVersaoAtualizador = new System.Windows.Forms.Label();
            this.btnVerificarAtualização = new System.Windows.Forms.Button();
            this.lblNovaVersaoLocalizada = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblAgora = new System.Windows.Forms.Label();
            this.progressBarAdv1 = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.btnWts = new System.Windows.Forms.Button();
            this.timerExportImport = new System.Windows.Forms.Timer(this.components);
            this.btnBackup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersaoAtualizador
            // 
            this.lblVersaoAtualizador.AutoSize = true;
            this.lblVersaoAtualizador.Location = new System.Drawing.Point(290, 141);
            this.lblVersaoAtualizador.Name = "lblVersaoAtualizador";
            this.lblVersaoAtualizador.Size = new System.Drawing.Size(77, 13);
            this.lblVersaoAtualizador.TabIndex = 0;
            this.lblVersaoAtualizador.Text = "Atualizador 1.0";
            // 
            // btnVerificarAtualização
            // 
            this.btnVerificarAtualização.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerificarAtualização.Location = new System.Drawing.Point(100, 52);
            this.btnVerificarAtualização.Name = "btnVerificarAtualização";
            this.btnVerificarAtualização.Size = new System.Drawing.Size(179, 57);
            this.btnVerificarAtualização.TabIndex = 1;
            this.btnVerificarAtualização.Text = "Verificar Atualizações";
            this.btnVerificarAtualização.UseVisualStyleBackColor = true;
            this.btnVerificarAtualização.Click += new System.EventHandler(this.btnVerificarAtualização_Click);
            // 
            // lblNovaVersaoLocalizada
            // 
            this.lblNovaVersaoLocalizada.AutoSize = true;
            this.lblNovaVersaoLocalizada.Location = new System.Drawing.Point(9, 141);
            this.lblNovaVersaoLocalizada.Name = "lblNovaVersaoLocalizada";
            this.lblNovaVersaoLocalizada.Size = new System.Drawing.Size(150, 13);
            this.lblNovaVersaoLocalizada.TabIndex = 3;
            this.lblNovaVersaoLocalizada.Text = "Nova Versão Localizada 1.0.1";
            this.lblNovaVersaoLocalizada.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblAgora
            // 
            this.lblAgora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAgora.AutoSize = true;
            this.lblAgora.Location = new System.Drawing.Point(318, 7);
            this.lblAgora.Name = "lblAgora";
            this.lblAgora.Size = new System.Drawing.Size(49, 13);
            this.lblAgora.TabIndex = 4;
            this.lblAgora.Text = "20:05:00";
            // 
            // progressBarAdv1
            // 
            this.progressBarAdv1.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv1.BackSegments = false;
            this.progressBarAdv1.CustomText = null;
            this.progressBarAdv1.CustomWaitingRender = false;
            this.progressBarAdv1.ForeColor = System.Drawing.Color.Purple;
            this.progressBarAdv1.ForegroundImage = null;
            this.progressBarAdv1.Location = new System.Drawing.Point(12, 115);
            this.progressBarAdv1.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBarAdv1.Name = "progressBarAdv1";
            this.progressBarAdv1.SegmentWidth = 12;
            this.progressBarAdv1.Size = new System.Drawing.Size(355, 23);
            this.progressBarAdv1.TabIndex = 5;
            this.progressBarAdv1.Text = "progressBarAdv1";
            this.progressBarAdv1.Visible = false;
            this.progressBarAdv1.WaitingGradientWidth = 400;
            // 
            // btnWts
            // 
            this.btnWts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWts.Location = new System.Drawing.Point(12, 22);
            this.btnWts.Name = "btnWts";
            this.btnWts.Size = new System.Drawing.Size(179, 26);
            this.btnWts.TabIndex = 6;
            this.btnWts.Text = "Disparar Mensagens Manualmente";
            this.btnWts.UseVisualStyleBackColor = true;
            this.btnWts.Click += new System.EventHandler(this.btnWts_Click);
            // 
            // timerExportImport
            // 
            this.timerExportImport.Tick += new System.EventHandler(this.timerExportImport_Tick);
            // 
            // btnBackup
            // 
            this.btnBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackup.Location = new System.Drawing.Point(197, 22);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(170, 26);
            this.btnBackup.TabIndex = 7;
            this.btnBackup.Text = "Backup/Nuvem";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // FrmAtualizador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(379, 163);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.btnWts);
            this.Controls.Add(this.progressBarAdv1);
            this.Controls.Add(this.lblAgora);
            this.Controls.Add(this.lblNovaVersaoLocalizada);
            this.Controls.Add(this.btnVerificarAtualização);
            this.Controls.Add(this.lblVersaoAtualizador);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAtualizador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atualizador Lunar Software";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAtualizador_FormClosing);
            this.Load += new System.EventHandler(this.FrmAtualizador_Load);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersaoAtualizador;
        private System.Windows.Forms.Button btnVerificarAtualização;
        private System.Windows.Forms.Label lblNovaVersaoLocalizada;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblAgora;
        private Syncfusion.Windows.Forms.Tools.ProgressBarAdv progressBarAdv1;
        private System.Windows.Forms.Button btnWts;
        private System.Windows.Forms.Timer timerExportImport;
        private System.Windows.Forms.Button btnBackup;
    }
}