namespace Lunar.Telas.OrdensDeServico
{
    partial class FrmImpressaoOrdemServico
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel25 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnFechar = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dsOrdemServico1 = new Lunar.Telas.OrdensDeServico.DataSetOrdemServico.dsOrdemServico();
            this.bindingSourceOrdem = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceProd = new System.Windows.Forms.BindingSource(this.components);
            this.dsOrdemServicoProduto = new Lunar.Telas.OrdensDeServico.DataSetOrdemServico.dsOrdemServicoProduto();
            this.bindingSourceServico = new System.Windows.Forms.BindingSource(this.components);
            this.dsOrdemServicoServico = new Lunar.Telas.OrdensDeServico.DataSetOrdemServico.dsOrdemServicoServico();
            this.bindingSourceExame = new System.Windows.Forms.BindingSource(this.components);
            this.panelTitleBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServico1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOrdem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceProd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServicoProduto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceServico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServicoServico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceExame)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Lunar.Telas.OrdensDeServico.ReportOrdemServico.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 406);
            this.reportViewer1.TabIndex = 0;
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel25);
            this.panelTitleBar.Controls.Add(this.btnFechar);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(800, 38);
            this.panelTitleBar.TabIndex = 257;
            // 
            // autoLabel25
            // 
            this.autoLabel25.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel25.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel25.ForeColor = System.Drawing.Color.White;
            this.autoLabel25.Location = new System.Drawing.Point(12, 0);
            this.autoLabel25.Name = "autoLabel25";
            this.autoLabel25.Size = new System.Drawing.Size(202, 35);
            this.autoLabel25.TabIndex = 198;
            this.autoLabel25.Text = "Ordem de Serviço";
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
            this.btnFechar.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnFechar.IconColor = System.Drawing.Color.White;
            this.btnFechar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFechar.IconSize = 30;
            this.btnFechar.Location = new System.Drawing.Point(759, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(39, 41);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelTitleBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 44);
            this.panel1.TabIndex = 258;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.reportViewer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 406);
            this.panel2.TabIndex = 259;
            // 
            // dsOrdemServico1
            // 
            this.dsOrdemServico1.DataSetName = "dsOrdemServico";
            this.dsOrdemServico1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSourceOrdem
            // 
            this.bindingSourceOrdem.DataMember = "OrdemServico";
            this.bindingSourceOrdem.DataSource = this.dsOrdemServico1;
            // 
            // bindingSourceProd
            // 
            this.bindingSourceProd.DataMember = "OrdemServicoProduto";
            this.bindingSourceProd.DataSource = this.dsOrdemServicoProduto;
            // 
            // dsOrdemServicoProduto
            // 
            this.dsOrdemServicoProduto.DataSetName = "dsOrdemServicoProduto";
            this.dsOrdemServicoProduto.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSourceServico
            // 
            this.bindingSourceServico.DataMember = "OrdemServicoServico";
            this.bindingSourceServico.DataSource = this.dsOrdemServicoServico;
            // 
            // dsOrdemServicoServico
            // 
            this.dsOrdemServicoServico.DataSetName = "dsOrdemServicoServico";
            this.dsOrdemServicoServico.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSourceExame
            // 
            this.bindingSourceExame.DataMember = "OrdemServicoExame";
            this.bindingSourceExame.DataSource = this.dsOrdemServico1;
            // 
            // FrmImpressaoOrdemServico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmImpressaoOrdemServico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ordem de Serviço";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmImpressaoOrdemServico_Load);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServico1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOrdem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceProd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServicoProduto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceServico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServicoServico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceExame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel25;
        private FontAwesome.Sharp.IconButton btnFechar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DataSetOrdemServico.dsOrdemServico dsOrdemServico1;
        private System.Windows.Forms.BindingSource bindingSourceOrdem;
        private System.Windows.Forms.BindingSource bindingSourceProd;
        private System.Windows.Forms.BindingSource bindingSourceServico;
        private System.Windows.Forms.BindingSource bindingSourceExame;
        private DataSetOrdemServico.dsOrdemServicoProduto dsOrdemServicoProduto;
        private DataSetOrdemServico.dsOrdemServicoServico dsOrdemServicoServico;
    }
}