namespace LunarSoftwareAtivador
{
    partial class FrmTermosDeUso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTermosDeUso));
            this.rtbTermos = new System.Windows.Forms.RichTextBox();
            this.cbAceite = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // rtbTermos
            // 
            this.rtbTermos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbTermos.Location = new System.Drawing.Point(12, 12);
            this.rtbTermos.Name = "rtbTermos";
            this.rtbTermos.Size = new System.Drawing.Size(785, 397);
            this.rtbTermos.TabIndex = 0;
            this.rtbTermos.Text = resources.GetString("rtbTermos.Text");
            // 
            // cbAceite
            // 
            this.cbAceite.AutoSize = true;
            this.cbAceite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAceite.Location = new System.Drawing.Point(650, 415);
            this.cbAceite.Name = "cbAceite";
            this.cbAceite.Size = new System.Drawing.Size(147, 24);
            this.cbAceite.TabIndex = 1;
            this.cbAceite.Text = "Aceito os termos";
            this.cbAceite.UseVisualStyleBackColor = true;
            this.cbAceite.CheckedChanged += new System.EventHandler(this.cbAceite_CheckedChanged);
            // 
            // FrmTermosDeUso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbAceite);
            this.Controls.Add(this.rtbTermos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTermosDeUso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Termos de Uso";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbTermos;
        private System.Windows.Forms.CheckBox cbAceite;
    }
}