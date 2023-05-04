namespace Lunar.Utils.ImportadorSistemas
{
    partial class FrmImportarCSV
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
            this.btnLocalizarArquivo = new System.Windows.Forms.Button();
            this.txtCaminhoArquivo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.radioContasReceber = new System.Windows.Forms.RadioButton();
            this.radioClientes = new System.Windows.Forms.RadioButton();
            this.radioProdutos = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.btnConfirmarImportacao = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLocalizarArquivo
            // 
            this.btnLocalizarArquivo.Location = new System.Drawing.Point(429, 38);
            this.btnLocalizarArquivo.Name = "btnLocalizarArquivo";
            this.btnLocalizarArquivo.Size = new System.Drawing.Size(134, 32);
            this.btnLocalizarArquivo.TabIndex = 0;
            this.btnLocalizarArquivo.Text = "Localizar Arquivo";
            this.btnLocalizarArquivo.UseVisualStyleBackColor = true;
            this.btnLocalizarArquivo.Click += new System.EventHandler(this.btnLocalizarArquivo_Click);
            // 
            // txtCaminhoArquivo
            // 
            this.txtCaminhoArquivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaminhoArquivo.Location = new System.Drawing.Point(12, 41);
            this.txtCaminhoArquivo.Name = "txtCaminhoArquivo";
            this.txtCaminhoArquivo.ReadOnly = true;
            this.txtCaminhoArquivo.Size = new System.Drawing.Size(411, 26);
            this.txtCaminhoArquivo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Caminho do Arquivo CSV";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 107);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(551, 262);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.Visible = false;
            // 
            // radioContasReceber
            // 
            this.radioContasReceber.AutoSize = true;
            this.radioContasReceber.Location = new System.Drawing.Point(80, 84);
            this.radioContasReceber.Name = "radioContasReceber";
            this.radioContasReceber.Size = new System.Drawing.Size(102, 17);
            this.radioContasReceber.TabIndex = 9;
            this.radioContasReceber.TabStop = true;
            this.radioContasReceber.Text = "Contas Receber";
            this.radioContasReceber.UseVisualStyleBackColor = true;
            // 
            // radioClientes
            // 
            this.radioClientes.AutoSize = true;
            this.radioClientes.Checked = true;
            this.radioClientes.Location = new System.Drawing.Point(12, 84);
            this.radioClientes.Name = "radioClientes";
            this.radioClientes.Size = new System.Drawing.Size(62, 17);
            this.radioClientes.TabIndex = 8;
            this.radioClientes.TabStop = true;
            this.radioClientes.Text = "Clientes";
            this.radioClientes.UseVisualStyleBackColor = true;
            // 
            // radioProdutos
            // 
            this.radioProdutos.AutoSize = true;
            this.radioProdutos.Location = new System.Drawing.Point(188, 84);
            this.radioProdutos.Name = "radioProdutos";
            this.radioProdutos.Size = new System.Drawing.Size(67, 17);
            this.radioProdutos.TabIndex = 10;
            this.radioProdutos.TabStop = true;
            this.radioProdutos.Text = "Produtos";
            this.radioProdutos.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblInformacao);
            this.groupBox1.Controls.Add(this.btnConfirmarImportacao);
            this.groupBox1.Location = new System.Drawing.Point(4, 375);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 76);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // lblInformacao
            // 
            this.lblInformacao.AutoSize = true;
            this.lblInformacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformacao.Location = new System.Drawing.Point(8, 36);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(94, 20);
            this.lblInformacao.TabIndex = 12;
            this.lblInformacao.Text = "Importação:";
            this.lblInformacao.Visible = false;
            // 
            // btnConfirmarImportacao
            // 
            this.btnConfirmarImportacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmarImportacao.Location = new System.Drawing.Point(425, 31);
            this.btnConfirmarImportacao.Name = "btnConfirmarImportacao";
            this.btnConfirmarImportacao.Size = new System.Drawing.Size(134, 32);
            this.btnConfirmarImportacao.TabIndex = 12;
            this.btnConfirmarImportacao.Text = "Confirmar";
            this.btnConfirmarImportacao.UseVisualStyleBackColor = true;
            this.btnConfirmarImportacao.Click += new System.EventHandler(this.btnConfirmarImportacao_Click);
            // 
            // FrmImportarCSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(579, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.radioProdutos);
            this.Controls.Add(this.radioContasReceber);
            this.Controls.Add(this.radioClientes);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCaminhoArquivo);
            this.Controls.Add(this.btnLocalizarArquivo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "FrmImportarCSV";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importação de Dados";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLocalizarArquivo;
        private System.Windows.Forms.TextBox txtCaminhoArquivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton radioContasReceber;
        private System.Windows.Forms.RadioButton radioClientes;
        private System.Windows.Forms.RadioButton radioProdutos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConfirmarImportacao;
        private System.Windows.Forms.Label lblInformacao;
    }
}