namespace Lunar.Telas.Compras
{
    partial class FrmImprimirEtiquetasOtica
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImprimirEtiquetasOtica));
            this.dsEtiquetaOticaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsEtiquetaOtica = new Lunar.Telas.Compras.Reports.dsEtiquetaOtica();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dsEtiquetaOticaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsEtiquetaOtica)).BeginInit();
            this.SuspendLayout();
            // 
            // dsEtiquetaOticaBindingSource
            // 
            this.dsEtiquetaOticaBindingSource.DataMember = "Etiqueta";
            this.dsEtiquetaOticaBindingSource.DataSource = this.dsEtiquetaOtica;
            // 
            // dsEtiquetaOtica
            // 
            this.dsEtiquetaOtica.DataSetName = "dsEtiquetaOtica";
            this.dsEtiquetaOtica.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "dsEtiquetaOtica";
            reportDataSource2.Value = this.dsEtiquetaOticaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Lunar.Telas.Compras.Reports.ReportEtiquetasOtica.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // FrmImprimirEtiquetasOtica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmImprimirEtiquetasOtica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Imprimir Etiquetas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmImprimirEtiquetasOtica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsEtiquetaOticaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsEtiquetaOtica)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dsEtiquetaOticaBindingSource;
        private Reports.dsEtiquetaOtica dsEtiquetaOtica;
    }
}