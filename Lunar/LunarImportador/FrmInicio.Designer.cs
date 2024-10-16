namespace LunarImportador
{
    partial class FrmInicio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInicio));
            label1 = new Label();
            btnImportarDados = new Button();
            comboDestino = new ComboBox();
            btnPesquisaBancoOrigem = new Button();
            txtBancoOrigem = new TextBox();
            lblOrigem = new Label();
            label2 = new Label();
            gradientPanel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            radioCheef = new RadioButton();
            radioSoftSystemCosmos = new RadioButton();
            radioLinkPro = new RadioButton();
            radioSGBR = new RadioButton();
            radioUltra = new RadioButton();
            label3 = new Label();
            gradientPanel2 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            gradientPanel3 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            chkClientes = new CheckBox();
            chkProdutos = new CheckBox();
            chkSaldoEstoque = new CheckBox();
            chkContasAReceber = new CheckBox();
            chkContasAPagar = new CheckBox();
            progressBar1 = new ProgressBar();
            lblStatus = new Label();
            sfButton1 = new Syncfusion.WinForms.Controls.SfButton();
            chkIdProduto = new CheckBox();
            chkServicos = new CheckBox();
            chkVendas = new CheckBox();
            chkGrupos = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)gradientPanel1).BeginInit();
            gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gradientPanel2).BeginInit();
            gradientPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gradientPanel3).BeginInit();
            gradientPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(32, 20);
            label1.Name = "label1";
            label1.Size = new Size(325, 31);
            label1.TabIndex = 1;
            label1.Text = "Importador Lunar Software";
            // 
            // btnImportarDados
            // 
            btnImportarDados.BackColor = Color.FromArgb(191, 0, 239);
            btnImportarDados.Cursor = Cursors.Hand;
            btnImportarDados.Font = new Font("Microsoft JhengHei", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnImportarDados.ForeColor = Color.White;
            btnImportarDados.Location = new Point(32, 541);
            btnImportarDados.Name = "btnImportarDados";
            btnImportarDados.Size = new Size(820, 55);
            btnImportarDados.TabIndex = 8;
            btnImportarDados.Text = "Converter Dados";
            btnImportarDados.UseVisualStyleBackColor = false;
            btnImportarDados.Click += btnImportarDados_Click;
            // 
            // comboDestino
            // 
            comboDestino.BackColor = SystemColors.Control;
            comboDestino.Font = new Font("Microsoft JhengHei", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboDestino.FormattingEnabled = true;
            comboDestino.Location = new Point(8, 44);
            comboDestino.Name = "comboDestino";
            comboDestino.Size = new Size(798, 29);
            comboDestino.TabIndex = 7;
            // 
            // btnPesquisaBancoOrigem
            // 
            btnPesquisaBancoOrigem.Font = new Font("Microsoft JhengHei", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPesquisaBancoOrigem.Location = new Point(707, 40);
            btnPesquisaBancoOrigem.Name = "btnPesquisaBancoOrigem";
            btnPesquisaBancoOrigem.Size = new Size(99, 33);
            btnPesquisaBancoOrigem.TabIndex = 2;
            btnPesquisaBancoOrigem.Text = "Pesquisar";
            btnPesquisaBancoOrigem.UseVisualStyleBackColor = true;
            btnPesquisaBancoOrigem.Click += btnPesquisaBancoOrigem_Click;
            // 
            // txtBancoOrigem
            // 
            txtBancoOrigem.Font = new Font("Microsoft JhengHei", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBancoOrigem.Location = new Point(12, 42);
            txtBancoOrigem.Name = "txtBancoOrigem";
            txtBancoOrigem.ReadOnly = true;
            txtBancoOrigem.Size = new Size(689, 29);
            txtBancoOrigem.TabIndex = 1;
            // 
            // lblOrigem
            // 
            lblOrigem.AutoSize = true;
            lblOrigem.Font = new Font("Microsoft JhengHei", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOrigem.Location = new Point(12, 9);
            lblOrigem.Name = "lblOrigem";
            lblOrigem.Size = new Size(407, 21);
            lblOrigem.TabIndex = 9;
            lblOrigem.Text = "Banco de Dados Origem (Sistema Antigo do Cliente)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(8, 9);
            label2.Name = "label2";
            label2.Size = new Size(202, 21);
            label2.TabIndex = 9;
            label2.Text = "Banco de Destino (Lunar)";
            // 
            // gradientPanel1
            // 
            gradientPanel1.Border3DStyle = Border3DStyle.Raised;
            gradientPanel1.BorderColor = Color.FromArgb(217, 217, 217);
            gradientPanel1.BorderStyle = BorderStyle.FixedSingle;
            gradientPanel1.Controls.Add(radioCheef);
            gradientPanel1.Controls.Add(radioSoftSystemCosmos);
            gradientPanel1.Controls.Add(radioLinkPro);
            gradientPanel1.Controls.Add(radioSGBR);
            gradientPanel1.Controls.Add(radioUltra);
            gradientPanel1.Controls.Add(label3);
            gradientPanel1.Location = new Point(32, 74);
            gradientPanel1.Name = "gradientPanel1";
            gradientPanel1.Size = new Size(820, 100);
            gradientPanel1.TabIndex = 15;
            // 
            // radioCheef
            // 
            radioCheef.AutoSize = true;
            radioCheef.Font = new Font("Microsoft JhengHei", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radioCheef.Location = new Point(588, 47);
            radioCheef.Name = "radioCheef";
            radioCheef.Size = new Size(67, 23);
            radioCheef.TabIndex = 16;
            radioCheef.TabStop = true;
            radioCheef.Text = "Cheef";
            radioCheef.UseVisualStyleBackColor = true;
            radioCheef.CheckedChanged += radioCheef_CheckedChanged;
            // 
            // radioSoftSystemCosmos
            // 
            radioSoftSystemCosmos.AutoSize = true;
            radioSoftSystemCosmos.Font = new Font("Microsoft JhengHei", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radioSoftSystemCosmos.Location = new Point(406, 47);
            radioSoftSystemCosmos.Name = "radioSoftSystemCosmos";
            radioSoftSystemCosmos.Size = new Size(176, 23);
            radioSoftSystemCosmos.TabIndex = 15;
            radioSoftSystemCosmos.TabStop = true;
            radioSoftSystemCosmos.Text = "SoftSystem - Cosmos";
            radioSoftSystemCosmos.UseVisualStyleBackColor = true;
            // 
            // radioLinkPro
            // 
            radioLinkPro.AutoSize = true;
            radioLinkPro.Font = new Font("Microsoft JhengHei", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radioLinkPro.Location = new Point(237, 47);
            radioLinkPro.Name = "radioLinkPro";
            radioLinkPro.Size = new Size(163, 23);
            radioLinkPro.TabIndex = 14;
            radioLinkPro.TabStop = true;
            radioLinkPro.Text = "LinkPro (Cervantes)";
            radioLinkPro.UseVisualStyleBackColor = true;
            radioLinkPro.CheckedChanged += radioLinkPro_CheckedChanged;
            // 
            // radioSGBR
            // 
            radioSGBR.AutoSize = true;
            radioSGBR.Font = new Font("Microsoft JhengHei", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radioSGBR.Location = new Point(12, 47);
            radioSGBR.Name = "radioSGBR";
            radioSGBR.Size = new Size(66, 23);
            radioSGBR.TabIndex = 13;
            radioSGBR.TabStop = true;
            radioSGBR.Text = "SGBR";
            radioSGBR.UseVisualStyleBackColor = true;
            // 
            // radioUltra
            // 
            radioUltra.AutoSize = true;
            radioUltra.Font = new Font("Microsoft JhengHei", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radioUltra.Location = new Point(104, 47);
            radioUltra.Name = "radioUltra";
            radioUltra.Size = new Size(127, 23);
            radioUltra.TabIndex = 12;
            radioUltra.TabStop = true;
            radioUltra.Text = "Ultra Sistemas";
            radioUltra.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 11);
            label3.Name = "label3";
            label3.Size = new Size(115, 21);
            label3.TabIndex = 10;
            label3.Text = "Sistema Atual";
            // 
            // gradientPanel2
            // 
            gradientPanel2.Border3DStyle = Border3DStyle.Raised;
            gradientPanel2.BorderColor = Color.FromArgb(217, 217, 217);
            gradientPanel2.BorderStyle = BorderStyle.FixedSingle;
            gradientPanel2.Controls.Add(lblOrigem);
            gradientPanel2.Controls.Add(txtBancoOrigem);
            gradientPanel2.Controls.Add(btnPesquisaBancoOrigem);
            gradientPanel2.Location = new Point(32, 190);
            gradientPanel2.Name = "gradientPanel2";
            gradientPanel2.Size = new Size(820, 100);
            gradientPanel2.TabIndex = 16;
            // 
            // gradientPanel3
            // 
            gradientPanel3.Border3DStyle = Border3DStyle.Raised;
            gradientPanel3.BorderColor = Color.FromArgb(217, 217, 217);
            gradientPanel3.BorderStyle = BorderStyle.FixedSingle;
            gradientPanel3.Controls.Add(label2);
            gradientPanel3.Controls.Add(comboDestino);
            gradientPanel3.Location = new Point(32, 306);
            gradientPanel3.Name = "gradientPanel3";
            gradientPanel3.Size = new Size(820, 100);
            gradientPanel3.TabIndex = 17;
            // 
            // chkClientes
            // 
            chkClientes.AutoSize = true;
            chkClientes.Checked = true;
            chkClientes.CheckState = CheckState.Checked;
            chkClientes.Location = new Point(89, 417);
            chkClientes.Name = "chkClientes";
            chkClientes.Size = new Size(144, 19);
            chkClientes.TabIndex = 18;
            chkClientes.Text = "Clientes/Fornecedores";
            chkClientes.UseVisualStyleBackColor = true;
            // 
            // chkProdutos
            // 
            chkProdutos.AutoSize = true;
            chkProdutos.Checked = true;
            chkProdutos.CheckState = CheckState.Checked;
            chkProdutos.Location = new Point(239, 417);
            chkProdutos.Name = "chkProdutos";
            chkProdutos.Size = new Size(74, 19);
            chkProdutos.TabIndex = 19;
            chkProdutos.Text = "Produtos";
            chkProdutos.UseVisualStyleBackColor = true;
            // 
            // chkSaldoEstoque
            // 
            chkSaldoEstoque.AutoSize = true;
            chkSaldoEstoque.Checked = true;
            chkSaldoEstoque.CheckState = CheckState.Checked;
            chkSaldoEstoque.Location = new Point(319, 417);
            chkSaldoEstoque.Name = "chkSaldoEstoque";
            chkSaldoEstoque.Size = new Size(167, 19);
            chkSaldoEstoque.TabIndex = 20;
            chkSaldoEstoque.Text = "Saldo de Estoque [Auxiliar]";
            chkSaldoEstoque.UseVisualStyleBackColor = true;
            // 
            // chkContasAReceber
            // 
            chkContasAReceber.AutoSize = true;
            chkContasAReceber.Checked = true;
            chkContasAReceber.CheckState = CheckState.Checked;
            chkContasAReceber.Location = new Point(567, 417);
            chkContasAReceber.Name = "chkContasAReceber";
            chkContasAReceber.Size = new Size(117, 19);
            chkContasAReceber.TabIndex = 21;
            chkContasAReceber.Text = "Contas a Receber";
            chkContasAReceber.UseVisualStyleBackColor = true;
            // 
            // chkContasAPagar
            // 
            chkContasAPagar.AutoSize = true;
            chkContasAPagar.Checked = true;
            chkContasAPagar.CheckState = CheckState.Checked;
            chkContasAPagar.Location = new Point(690, 417);
            chkContasAPagar.Name = "chkContasAPagar";
            chkContasAPagar.Size = new Size(105, 19);
            chkContasAPagar.TabIndex = 22;
            chkContasAPagar.Text = "Contas a Pagar";
            chkContasAPagar.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            progressBar1.ForeColor = Color.FromArgb(191, 0, 239);
            progressBar1.Location = new Point(37, 512);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(810, 23);
            progressBar1.TabIndex = 23;
            progressBar1.Visible = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Microsoft JhengHei", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.Location = new Point(34, 487);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(77, 17);
            lblStatus.TabIndex = 13;
            lblStatus.Text = "Informação";
            lblStatus.Visible = false;
            // 
            // sfButton1
            // 
            sfButton1.Font = new Font("Segoe UI Semibold", 9F);
            sfButton1.Location = new Point(756, 40);
            sfButton1.Name = "sfButton1";
            sfButton1.Size = new Size(96, 28);
            sfButton1.TabIndex = 24;
            sfButton1.Text = "Teste";
            sfButton1.Visible = false;
            sfButton1.Click += sfButton1_Click;
            // 
            // chkIdProduto
            // 
            chkIdProduto.AutoSize = true;
            chkIdProduto.Checked = true;
            chkIdProduto.CheckState = CheckState.Checked;
            chkIdProduto.Location = new Point(239, 442);
            chkIdProduto.Name = "chkIdProduto";
            chkIdProduto.Size = new Size(210, 19);
            chkIdProduto.TabIndex = 25;
            chkIdProduto.Text = "Gravar o Produto com o mesmo ID";
            chkIdProduto.UseVisualStyleBackColor = true;
            // 
            // chkServicos
            // 
            chkServicos.AutoSize = true;
            chkServicos.Checked = true;
            chkServicos.CheckState = CheckState.Checked;
            chkServicos.Location = new Point(492, 417);
            chkServicos.Name = "chkServicos";
            chkServicos.Size = new Size(69, 19);
            chkServicos.TabIndex = 26;
            chkServicos.Text = "Serviços";
            chkServicos.UseVisualStyleBackColor = true;
            // 
            // chkVendas
            // 
            chkVendas.AutoSize = true;
            chkVendas.Checked = true;
            chkVendas.CheckState = CheckState.Checked;
            chkVendas.Location = new Point(492, 442);
            chkVendas.Name = "chkVendas";
            chkVendas.Size = new Size(63, 19);
            chkVendas.TabIndex = 27;
            chkVendas.Text = "Vendas";
            chkVendas.UseVisualStyleBackColor = true;
            // 
            // chkGrupos
            // 
            chkGrupos.AutoSize = true;
            chkGrupos.Checked = true;
            chkGrupos.CheckState = CheckState.Checked;
            chkGrupos.Location = new Point(89, 442);
            chkGrupos.Name = "chkGrupos";
            chkGrupos.Size = new Size(115, 19);
            chkGrupos.TabIndex = 28;
            chkGrupos.Text = "Grupos Produtos";
            chkGrupos.UseVisualStyleBackColor = true;
            chkGrupos.Visible = false;
            // 
            // FrmInicio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(884, 608);
            Controls.Add(chkGrupos);
            Controls.Add(chkVendas);
            Controls.Add(chkServicos);
            Controls.Add(chkIdProduto);
            Controls.Add(sfButton1);
            Controls.Add(lblStatus);
            Controls.Add(progressBar1);
            Controls.Add(chkContasAPagar);
            Controls.Add(chkContasAReceber);
            Controls.Add(chkSaldoEstoque);
            Controls.Add(chkProdutos);
            Controls.Add(chkClientes);
            Controls.Add(gradientPanel3);
            Controls.Add(gradientPanel2);
            Controls.Add(gradientPanel1);
            Controls.Add(btnImportarDados);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmInicio";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lunar Software - Tecnologia para Todos";
            ((System.ComponentModel.ISupportInitialize)gradientPanel1).EndInit();
            gradientPanel1.ResumeLayout(false);
            gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gradientPanel2).EndInit();
            gradientPanel2.ResumeLayout(false);
            gradientPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gradientPanel3).EndInit();
            gradientPanel3.ResumeLayout(false);
            gradientPanel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button btnPesquisaBancoOrigem;
        private TextBox txtBancoOrigem;
        private ComboBox comboDestino;
        private Button btnImportarDados;
        private Label lblOrigem;
        private Label label2;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel1;
        private Label label3;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel2;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel3;
        private RadioButton radioUltra;
        private CheckBox chkClientes;
        private CheckBox chkProdutos;
        private CheckBox chkSaldoEstoque;
        private CheckBox chkContasAReceber;
        private CheckBox chkContasAPagar;
        private ProgressBar progressBar1;
        private Label lblStatus;
        private RadioButton radioSGBR;
        private Syncfusion.WinForms.Controls.SfButton sfButton1;
        private CheckBox chkIdProduto;
        private CheckBox chkServicos;
        private RadioButton radioLinkPro;
        private RadioButton radioSoftSystemCosmos;
        private CheckBox chkVendas;
        private CheckBox chkGrupos;
        private RadioButton radioCheef;
    }
}