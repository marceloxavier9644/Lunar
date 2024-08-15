namespace Lunar.Telas.Fiscal
{
    partial class FrmInutilizar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInutilizar));
            this.txtNumeroInicial = new Syncfusion.Windows.Forms.Grid.GridAwareTextBox();
            this.txtNumeroFinal = new Syncfusion.Windows.Forms.Grid.GridAwareTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJustificativa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnInutilizar = new System.Windows.Forms.Button();
            this.lblSerie = new System.Windows.Forms.Label();
            this.txtSerie = new Syncfusion.Windows.Forms.Grid.GridAwareTextBox();
            this.radio55 = new System.Windows.Forms.RadioButton();
            this.radio65 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // txtNumeroInicial
            // 
            this.txtNumeroInicial.AutoSuggestFormula = false;
            this.txtNumeroInicial.DisabledBackColor = System.Drawing.SystemColors.Window;
            this.txtNumeroInicial.EnabledBackColor = System.Drawing.SystemColors.Window;
            this.txtNumeroInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroInicial.Location = new System.Drawing.Point(182, 86);
            this.txtNumeroInicial.Name = "txtNumeroInicial";
            this.txtNumeroInicial.Size = new System.Drawing.Size(155, 26);
            this.txtNumeroInicial.TabIndex = 0;
            // 
            // txtNumeroFinal
            // 
            this.txtNumeroFinal.AutoSuggestFormula = false;
            this.txtNumeroFinal.DisabledBackColor = System.Drawing.SystemColors.Window;
            this.txtNumeroFinal.EnabledBackColor = System.Drawing.SystemColors.Window;
            this.txtNumeroFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroFinal.Location = new System.Drawing.Point(352, 86);
            this.txtNumeroFinal.Name = "txtNumeroFinal";
            this.txtNumeroFinal.Size = new System.Drawing.Size(155, 26);
            this.txtNumeroFinal.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(178, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Número Inicial";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(348, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Número Final";
            // 
            // txtJustificativa
            // 
            this.txtJustificativa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJustificativa.Location = new System.Drawing.Point(12, 144);
            this.txtJustificativa.Multiline = true;
            this.txtJustificativa.Name = "txtJustificativa";
            this.txtJustificativa.Size = new System.Drawing.Size(495, 123);
            this.txtJustificativa.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Justificativa";
            // 
            // btnInutilizar
            // 
            this.btnInutilizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInutilizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInutilizar.Location = new System.Drawing.Point(192, 291);
            this.btnInutilizar.Name = "btnInutilizar";
            this.btnInutilizar.Size = new System.Drawing.Size(135, 37);
            this.btnInutilizar.TabIndex = 6;
            this.btnInutilizar.Text = "Inutilizar [F5]";
            this.btnInutilizar.UseVisualStyleBackColor = true;
            this.btnInutilizar.Click += new System.EventHandler(this.btnInutilizar_Click);
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerie.Location = new System.Drawing.Point(8, 63);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(46, 20);
            this.lblSerie.TabIndex = 8;
            this.lblSerie.Text = "Série";
            // 
            // txtSerie
            // 
            this.txtSerie.AutoSuggestFormula = false;
            this.txtSerie.DisabledBackColor = System.Drawing.SystemColors.Window;
            this.txtSerie.EnabledBackColor = System.Drawing.SystemColors.Window;
            this.txtSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerie.Location = new System.Drawing.Point(12, 86);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(155, 26);
            this.txtSerie.TabIndex = 7;
            // 
            // radio55
            // 
            this.radio55.AutoSize = true;
            this.radio55.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radio55.Location = new System.Drawing.Point(89, 18);
            this.radio55.Name = "radio55";
            this.radio55.Size = new System.Drawing.Size(149, 24);
            this.radio55.TabIndex = 9;
            this.radio55.Text = "Modelo 55 - NF-e";
            this.radio55.UseVisualStyleBackColor = true;
            this.radio55.CheckedChanged += new System.EventHandler(this.radio55_CheckedChanged);
            // 
            // radio65
            // 
            this.radio65.AutoSize = true;
            this.radio65.Checked = true;
            this.radio65.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radio65.Location = new System.Drawing.Point(269, 18);
            this.radio65.Name = "radio65";
            this.radio65.Size = new System.Drawing.Size(160, 24);
            this.radio65.TabIndex = 10;
            this.radio65.TabStop = true;
            this.radio65.Text = "Modelo 65 - NFC-e";
            this.radio65.UseVisualStyleBackColor = true;
            // 
            // FrmInutilizar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(519, 345);
            this.Controls.Add(this.radio65);
            this.Controls.Add(this.radio55);
            this.Controls.Add(this.lblSerie);
            this.Controls.Add(this.txtSerie);
            this.Controls.Add(this.btnInutilizar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtJustificativa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNumeroFinal);
            this.Controls.Add(this.txtNumeroInicial);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInutilizar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inutilização de Sequência";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmInutilizar_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Grid.GridAwareTextBox txtNumeroInicial;
        private Syncfusion.Windows.Forms.Grid.GridAwareTextBox txtNumeroFinal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJustificativa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnInutilizar;
        private System.Windows.Forms.Label lblSerie;
        private Syncfusion.Windows.Forms.Grid.GridAwareTextBox txtSerie;
        private System.Windows.Forms.RadioButton radio55;
        private System.Windows.Forms.RadioButton radio65;
    }
}