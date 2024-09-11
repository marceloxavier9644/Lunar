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
            this.radioNaoConciliado = new System.Windows.Forms.RadioButton();
            this.radioConciliado = new System.Windows.Forms.RadioButton();
            this.btnImportarSaldoEstoque = new System.Windows.Forms.Button();
            this.btnFirebird = new System.Windows.Forms.Button();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.btnConfirmarImportacao = new System.Windows.Forms.Button();
            this.radioOrdemServico = new System.Windows.Forms.RadioButton();
            this.radioOrdemServicoProdutos = new System.Windows.Forms.RadioButton();
            this.radioOrdemServicoExame = new System.Windows.Forms.RadioButton();
            this.radioContasPagar = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCaminhoBancoUltra = new System.Windows.Forms.TextBox();
            this.btnLocalizarBancoUltra = new System.Windows.Forms.Button();
            this.radioAnexosOS = new System.Windows.Forms.RadioButton();
            this.btnImportarCsvFones = new System.Windows.Forms.Button();
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
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 186);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(672, 209);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.Visible = false;
            // 
            // radioContasReceber
            // 
            this.radioContasReceber.AutoSize = true;
            this.radioContasReceber.Location = new System.Drawing.Point(84, 122);
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
            this.radioClientes.Location = new System.Drawing.Point(16, 122);
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
            this.radioProdutos.Location = new System.Drawing.Point(192, 122);
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
            this.groupBox1.Controls.Add(this.btnImportarCsvFones);
            this.groupBox1.Controls.Add(this.radioNaoConciliado);
            this.groupBox1.Controls.Add(this.radioConciliado);
            this.groupBox1.Controls.Add(this.btnImportarSaldoEstoque);
            this.groupBox1.Controls.Add(this.btnFirebird);
            this.groupBox1.Controls.Add(this.lblInformacao);
            this.groupBox1.Controls.Add(this.btnConfirmarImportacao);
            this.groupBox1.Location = new System.Drawing.Point(4, 401);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(696, 76);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // radioNaoConciliado
            // 
            this.radioNaoConciliado.AutoSize = true;
            this.radioNaoConciliado.Location = new System.Drawing.Point(355, 19);
            this.radioNaoConciliado.Name = "radioNaoConciliado";
            this.radioNaoConciliado.Size = new System.Drawing.Size(45, 17);
            this.radioNaoConciliado.TabIndex = 16;
            this.radioNaoConciliado.Text = "N/C";
            this.radioNaoConciliado.UseVisualStyleBackColor = true;
            // 
            // radioConciliado
            // 
            this.radioConciliado.AutoSize = true;
            this.radioConciliado.Checked = true;
            this.radioConciliado.Location = new System.Drawing.Point(317, 19);
            this.radioConciliado.Name = "radioConciliado";
            this.radioConciliado.Size = new System.Drawing.Size(32, 17);
            this.radioConciliado.TabIndex = 15;
            this.radioConciliado.TabStop = true;
            this.radioConciliado.Text = "C";
            this.radioConciliado.UseVisualStyleBackColor = true;
            // 
            // btnImportarSaldoEstoque
            // 
            this.btnImportarSaldoEstoque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportarSaldoEstoque.Location = new System.Drawing.Point(243, 36);
            this.btnImportarSaldoEstoque.Name = "btnImportarSaldoEstoque";
            this.btnImportarSaldoEstoque.Size = new System.Drawing.Size(157, 32);
            this.btnImportarSaldoEstoque.TabIndex = 14;
            this.btnImportarSaldoEstoque.Text = "Importar CSV - Saldo Estoque";
            this.btnImportarSaldoEstoque.UseVisualStyleBackColor = true;
            this.btnImportarSaldoEstoque.Click += new System.EventHandler(this.btnImportarSaldoEstoque_Click);
            // 
            // btnFirebird
            // 
            this.btnFirebird.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFirebird.Location = new System.Drawing.Point(546, 36);
            this.btnFirebird.Name = "btnFirebird";
            this.btnFirebird.Size = new System.Drawing.Size(134, 32);
            this.btnFirebird.TabIndex = 13;
            this.btnFirebird.Text = "Importar Pelo Ultra BD";
            this.btnFirebird.UseVisualStyleBackColor = true;
            this.btnFirebird.Click += new System.EventHandler(this.btnFirebird_Click);
            // 
            // lblInformacao
            // 
            this.lblInformacao.AutoSize = true;
            this.lblInformacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformacao.Location = new System.Drawing.Point(6, 9);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(94, 20);
            this.lblInformacao.TabIndex = 12;
            this.lblInformacao.Text = "Importação:";
            this.lblInformacao.Visible = false;
            // 
            // btnConfirmarImportacao
            // 
            this.btnConfirmarImportacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmarImportacao.Location = new System.Drawing.Point(406, 36);
            this.btnConfirmarImportacao.Name = "btnConfirmarImportacao";
            this.btnConfirmarImportacao.Size = new System.Drawing.Size(134, 32);
            this.btnConfirmarImportacao.TabIndex = 12;
            this.btnConfirmarImportacao.Text = "Importar CSV";
            this.btnConfirmarImportacao.UseVisualStyleBackColor = true;
            this.btnConfirmarImportacao.Click += new System.EventHandler(this.btnConfirmarImportacao_Click);
            // 
            // radioOrdemServico
            // 
            this.radioOrdemServico.AutoSize = true;
            this.radioOrdemServico.Location = new System.Drawing.Point(16, 154);
            this.radioOrdemServico.Name = "radioOrdemServico";
            this.radioOrdemServico.Size = new System.Drawing.Size(43, 17);
            this.radioOrdemServico.TabIndex = 12;
            this.radioOrdemServico.TabStop = true;
            this.radioOrdemServico.Text = "O.S";
            this.radioOrdemServico.UseVisualStyleBackColor = true;
            // 
            // radioOrdemServicoProdutos
            // 
            this.radioOrdemServicoProdutos.AutoSize = true;
            this.radioOrdemServicoProdutos.Location = new System.Drawing.Point(84, 154);
            this.radioOrdemServicoProdutos.Name = "radioOrdemServicoProdutos";
            this.radioOrdemServicoProdutos.Size = new System.Drawing.Size(88, 17);
            this.radioOrdemServicoProdutos.TabIndex = 13;
            this.radioOrdemServicoProdutos.TabStop = true;
            this.radioOrdemServicoProdutos.Text = "O.S Produtos";
            this.radioOrdemServicoProdutos.UseVisualStyleBackColor = true;
            // 
            // radioOrdemServicoExame
            // 
            this.radioOrdemServicoExame.AutoSize = true;
            this.radioOrdemServicoExame.Location = new System.Drawing.Point(192, 154);
            this.radioOrdemServicoExame.Name = "radioOrdemServicoExame";
            this.radioOrdemServicoExame.Size = new System.Drawing.Size(83, 17);
            this.radioOrdemServicoExame.TabIndex = 14;
            this.radioOrdemServicoExame.TabStop = true;
            this.radioOrdemServicoExame.Text = "O.S Exames";
            this.radioOrdemServicoExame.UseVisualStyleBackColor = true;
            // 
            // radioContasPagar
            // 
            this.radioContasPagar.AutoSize = true;
            this.radioContasPagar.Location = new System.Drawing.Point(293, 154);
            this.radioContasPagar.Name = "radioContasPagar";
            this.radioContasPagar.Size = new System.Drawing.Size(98, 17);
            this.radioContasPagar.TabIndex = 15;
            this.radioContasPagar.TabStop = true;
            this.radioContasPagar.Text = "Contas a Pagar";
            this.radioContasPagar.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(259, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Caminho do Banco de Dados Ultra ";
            // 
            // txtCaminhoBancoUltra
            // 
            this.txtCaminhoBancoUltra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaminhoBancoUltra.Location = new System.Drawing.Point(12, 90);
            this.txtCaminhoBancoUltra.Name = "txtCaminhoBancoUltra";
            this.txtCaminhoBancoUltra.ReadOnly = true;
            this.txtCaminhoBancoUltra.Size = new System.Drawing.Size(411, 26);
            this.txtCaminhoBancoUltra.TabIndex = 16;
            // 
            // btnLocalizarBancoUltra
            // 
            this.btnLocalizarBancoUltra.Location = new System.Drawing.Point(429, 87);
            this.btnLocalizarBancoUltra.Name = "btnLocalizarBancoUltra";
            this.btnLocalizarBancoUltra.Size = new System.Drawing.Size(134, 32);
            this.btnLocalizarBancoUltra.TabIndex = 18;
            this.btnLocalizarBancoUltra.Text = "Localizar Banco";
            this.btnLocalizarBancoUltra.UseVisualStyleBackColor = true;
            this.btnLocalizarBancoUltra.Click += new System.EventHandler(this.btnLocalizarBancoUltra_Click);
            // 
            // radioAnexosOS
            // 
            this.radioAnexosOS.AutoSize = true;
            this.radioAnexosOS.Location = new System.Drawing.Point(408, 154);
            this.radioAnexosOS.Name = "radioAnexosOS";
            this.radioAnexosOS.Size = new System.Drawing.Size(81, 17);
            this.radioAnexosOS.TabIndex = 19;
            this.radioAnexosOS.TabStop = true;
            this.radioAnexosOS.Text = "Anexos O.S";
            this.radioAnexosOS.UseVisualStyleBackColor = true;
            // 
            // btnImportarCsvFones
            // 
            this.btnImportarCsvFones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportarCsvFones.Location = new System.Drawing.Point(80, 36);
            this.btnImportarCsvFones.Name = "btnImportarCsvFones";
            this.btnImportarCsvFones.Size = new System.Drawing.Size(157, 32);
            this.btnImportarCsvFones.TabIndex = 17;
            this.btnImportarCsvFones.Text = "Importar CSV - Telefones";
            this.btnImportarCsvFones.UseVisualStyleBackColor = true;
            this.btnImportarCsvFones.Click += new System.EventHandler(this.btnImportarCsvFones_Click);
            // 
            // FrmImportarCSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(700, 476);
            this.Controls.Add(this.radioAnexosOS);
            this.Controls.Add(this.btnLocalizarBancoUltra);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCaminhoBancoUltra);
            this.Controls.Add(this.radioContasPagar);
            this.Controls.Add(this.radioOrdemServicoExame);
            this.Controls.Add(this.radioOrdemServicoProdutos);
            this.Controls.Add(this.radioOrdemServico);
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
        private System.Windows.Forms.Button btnFirebird;
        private System.Windows.Forms.RadioButton radioOrdemServico;
        private System.Windows.Forms.RadioButton radioOrdemServicoProdutos;
        private System.Windows.Forms.RadioButton radioOrdemServicoExame;
        private System.Windows.Forms.RadioButton radioContasPagar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCaminhoBancoUltra;
        private System.Windows.Forms.Button btnLocalizarBancoUltra;
        private System.Windows.Forms.RadioButton radioAnexosOS;
        private System.Windows.Forms.Button btnImportarSaldoEstoque;
        private System.Windows.Forms.RadioButton radioNaoConciliado;
        private System.Windows.Forms.RadioButton radioConciliado;
        private System.Windows.Forms.Button btnImportarCsvFones;
    }
}