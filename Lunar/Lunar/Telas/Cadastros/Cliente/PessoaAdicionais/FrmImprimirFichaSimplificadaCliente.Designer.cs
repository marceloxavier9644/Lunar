namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    partial class FrmImprimirFichaSimplificadaCliente
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImprimirFichaSimplificadaCliente));
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dsClienteSimplificado = new Lunar.Telas.Cadastros.Cliente.PessoaAdicionais.dsClienteSimplificado();
            this.dsClienteSimplificadoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dsClienteSimplificado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsClienteSimplificadoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource4.Name = "dsClienteSimplificado";
            reportDataSource4.Value = this.dsClienteSimplificadoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Lunar.Telas.Cadastros.Cliente.PessoaAdicionais.FichaClienteSimples.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // dsClienteSimplificado
            // 
            this.dsClienteSimplificado.DataSetName = "dsClienteSimplificado";
            this.dsClienteSimplificado.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsClienteSimplificadoBindingSource
            // 
            this.dsClienteSimplificadoBindingSource.DataMember = "Cliente";
            this.dsClienteSimplificadoBindingSource.DataSource = this.dsClienteSimplificado;
            // 
            // FrmImprimirFichaSimplificadaCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmImprimirFichaSimplificadaCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impressão de Cadastro Simplificado";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmImprimirFichaSimplificadaCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsClienteSimplificado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsClienteSimplificadoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dsClienteSimplificadoBindingSource;
        private dsClienteSimplificado dsClienteSimplificado;
    }
}