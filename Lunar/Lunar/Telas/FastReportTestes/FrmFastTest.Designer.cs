﻿namespace Lunar.Telas.FastReportTestes
{
    partial class FrmFastTest
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
            this.dsOrdemServico = new Lunar.Telas.OrdensDeServico.DataSetOrdemServico.dsOrdemServico();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dsOrdemServico
            // 
            this.dsOrdemServico.DataSetName = "dsOrdemServico";
            this.dsOrdemServico.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSource
            // 
            this.bindingSource.DataMember = "OrdemServico";
            this.bindingSource.DataSource = this.dsOrdemServico;
            // 
            // FrmFastTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "FrmFastTest";
            this.Text = "FrmFastTest";
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OrdensDeServico.DataSetOrdemServico.dsOrdemServico dsOrdemServico;
        private System.Windows.Forms.BindingSource bindingSource;
    }
}