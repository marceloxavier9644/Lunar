namespace Lunar.Telas.OrdensDeServico
{
    partial class FrmImprimirViasOtica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImprimirViasOtica));
            this.chkLaboratorio = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkCliente = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkLoja = new MaterialSkin.Controls.MaterialCheckbox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.comboImpressoras = new MaterialSkin.Controls.MaterialComboBox();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.txtWhatsappCliente = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.txtWhatsappLaboratorio = new MaterialSkin.Controls.MaterialTextBox();
            this.btnImprimir = new MaterialSkin.Controls.MaterialButton();
            this.btnCancelar = new MaterialSkin.Controls.MaterialButton();
            this.btnEnviarWhatsapp = new MaterialSkin.Controls.MaterialButton();
            this.dsOrdemServico = new Lunar.Telas.OrdensDeServico.DataSetOrdemServico.dsOrdemServico();
            this.dsOrdemServicoExame = new Lunar.Telas.OrdensDeServico.DataSetOrdemServico.dsOrdemServicoExame();
            this.dsOrdemServicoProduto = new Lunar.Telas.OrdensDeServico.DataSetOrdemServico.dsOrdemServicoProduto();
            this.dsOrdemServicoServico = new Lunar.Telas.OrdensDeServico.DataSetOrdemServico.dsOrdemServicoServico();
            this.dsOrdemServicoPagamento = new Lunar.Telas.OrdensDeServico.DataSetOrdemServico.dsOrdemServicoPagamento();
            this.bindingSourceOrdem = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceProd = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceServico = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceExame1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourcePagamento = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServicoExame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServicoProduto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServicoServico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServicoPagamento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOrdem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceProd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceServico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceExame1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePagamento)).BeginInit();
            this.SuspendLayout();
            // 
            // chkLaboratorio
            // 
            this.chkLaboratorio.AutoSize = true;
            this.chkLaboratorio.Checked = true;
            this.chkLaboratorio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLaboratorio.Depth = 0;
            this.chkLaboratorio.Location = new System.Drawing.Point(47, 46);
            this.chkLaboratorio.Margin = new System.Windows.Forms.Padding(0);
            this.chkLaboratorio.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkLaboratorio.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkLaboratorio.Name = "chkLaboratorio";
            this.chkLaboratorio.Ripple = true;
            this.chkLaboratorio.Size = new System.Drawing.Size(144, 37);
            this.chkLaboratorio.TabIndex = 1;
            this.chkLaboratorio.Text = "Via Laboratório";
            this.chkLaboratorio.UseVisualStyleBackColor = true;
            // 
            // chkCliente
            // 
            this.chkCliente.AutoSize = true;
            this.chkCliente.Checked = true;
            this.chkCliente.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCliente.Depth = 0;
            this.chkCliente.Location = new System.Drawing.Point(205, 46);
            this.chkCliente.Margin = new System.Windows.Forms.Padding(0);
            this.chkCliente.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkCliente.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkCliente.Name = "chkCliente";
            this.chkCliente.Ripple = true;
            this.chkCliente.Size = new System.Drawing.Size(110, 37);
            this.chkCliente.TabIndex = 2;
            this.chkCliente.Text = "Via Cliente";
            this.chkCliente.UseVisualStyleBackColor = true;
            // 
            // chkLoja
            // 
            this.chkLoja.AutoSize = true;
            this.chkLoja.Checked = true;
            this.chkLoja.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLoja.Depth = 0;
            this.chkLoja.Location = new System.Drawing.Point(329, 46);
            this.chkLoja.Margin = new System.Windows.Forms.Padding(0);
            this.chkLoja.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkLoja.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkLoja.Name = "chkLoja";
            this.chkLoja.Ripple = true;
            this.chkLoja.Size = new System.Drawing.Size(93, 37);
            this.chkLoja.TabIndex = 3;
            this.chkLoja.Text = "Via Loja";
            this.chkLoja.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.materialLabel3);
            this.groupBox1.Controls.Add(this.comboImpressoras);
            this.groupBox1.Controls.Add(this.materialLabel2);
            this.groupBox1.Controls.Add(this.txtWhatsappCliente);
            this.groupBox1.Controls.Add(this.materialLabel1);
            this.groupBox1.Controls.Add(this.chkLaboratorio);
            this.groupBox1.Controls.Add(this.txtWhatsappLaboratorio);
            this.groupBox1.Controls.Add(this.chkLoja);
            this.groupBox1.Controls.Add(this.chkCliente);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(559, 278);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selecione o que deseja Imprimir";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(44, 175);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(80, 19);
            this.materialLabel3.TabIndex = 261;
            this.materialLabel3.Text = "Impressora";
            // 
            // comboImpressoras
            // 
            this.comboImpressoras.AutoResize = false;
            this.comboImpressoras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboImpressoras.Depth = 0;
            this.comboImpressoras.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboImpressoras.DropDownHeight = 174;
            this.comboImpressoras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboImpressoras.DropDownWidth = 121;
            this.comboImpressoras.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.comboImpressoras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboImpressoras.FormattingEnabled = true;
            this.comboImpressoras.IntegralHeight = false;
            this.comboImpressoras.ItemHeight = 43;
            this.comboImpressoras.Location = new System.Drawing.Point(47, 197);
            this.comboImpressoras.MaxDropDownItems = 4;
            this.comboImpressoras.MouseState = MaterialSkin.MouseState.OUT;
            this.comboImpressoras.Name = "comboImpressoras";
            this.comboImpressoras.Size = new System.Drawing.Size(468, 49);
            this.comboImpressoras.TabIndex = 260;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(281, 100);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(125, 19);
            this.materialLabel2.TabIndex = 8;
            this.materialLabel2.Text = "Whatsapp Cliente";
            // 
            // txtWhatsappCliente
            // 
            this.txtWhatsappCliente.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWhatsappCliente.Depth = 0;
            this.txtWhatsappCliente.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtWhatsappCliente.Location = new System.Drawing.Point(284, 122);
            this.txtWhatsappCliente.MaxLength = 50;
            this.txtWhatsappCliente.MouseState = MaterialSkin.MouseState.OUT;
            this.txtWhatsappCliente.Multiline = false;
            this.txtWhatsappCliente.Name = "txtWhatsappCliente";
            this.txtWhatsappCliente.Size = new System.Drawing.Size(231, 50);
            this.txtWhatsappCliente.TabIndex = 7;
            this.txtWhatsappCliente.Text = "";
            this.txtWhatsappCliente.Leave += new System.EventHandler(this.txtWhatsappCliente_Leave);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(44, 100);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(159, 19);
            this.materialLabel1.TabIndex = 6;
            this.materialLabel1.Text = "Whatsapp Laboratório";
            // 
            // txtWhatsappLaboratorio
            // 
            this.txtWhatsappLaboratorio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWhatsappLaboratorio.Depth = 0;
            this.txtWhatsappLaboratorio.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtWhatsappLaboratorio.Location = new System.Drawing.Point(47, 122);
            this.txtWhatsappLaboratorio.MaxLength = 50;
            this.txtWhatsappLaboratorio.MouseState = MaterialSkin.MouseState.OUT;
            this.txtWhatsappLaboratorio.Multiline = false;
            this.txtWhatsappLaboratorio.Name = "txtWhatsappLaboratorio";
            this.txtWhatsappLaboratorio.Size = new System.Drawing.Size(231, 50);
            this.txtWhatsappLaboratorio.TabIndex = 5;
            this.txtWhatsappLaboratorio.Text = "";
            this.txtWhatsappLaboratorio.Leave += new System.EventHandler(this.txtWhatsappLaboratorio_Leave);
            // 
            // btnImprimir
            // 
            this.btnImprimir.AutoSize = false;
            this.btnImprimir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.Depth = 0;
            this.btnImprimir.DrawShadows = true;
            this.btnImprimir.HighEmphasis = true;
            this.btnImprimir.Icon = null;
            this.btnImprimir.Location = new System.Drawing.Point(394, 299);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnImprimir.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(177, 39);
            this.btnImprimir.TabIndex = 5;
            this.btnImprimir.Text = "Imprimir [F8]";
            this.btnImprimir.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnImprimir.UseAccentColor = false;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = false;
            this.btnCancelar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancelar.BackColor = System.Drawing.Color.IndianRed;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Depth = 0;
            this.btnCancelar.DrawShadows = true;
            this.btnCancelar.HighEmphasis = true;
            this.btnCancelar.Icon = null;
            this.btnCancelar.Location = new System.Drawing.Point(24, 299);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCancelar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(177, 39);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCancelar.UseAccentColor = false;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEnviarWhatsapp
            // 
            this.btnEnviarWhatsapp.AutoSize = false;
            this.btnEnviarWhatsapp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEnviarWhatsapp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviarWhatsapp.Depth = 0;
            this.btnEnviarWhatsapp.DrawShadows = true;
            this.btnEnviarWhatsapp.Enabled = false;
            this.btnEnviarWhatsapp.HighEmphasis = true;
            this.btnEnviarWhatsapp.Icon = null;
            this.btnEnviarWhatsapp.Location = new System.Drawing.Point(209, 299);
            this.btnEnviarWhatsapp.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnEnviarWhatsapp.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnEnviarWhatsapp.Name = "btnEnviarWhatsapp";
            this.btnEnviarWhatsapp.Size = new System.Drawing.Size(177, 39);
            this.btnEnviarWhatsapp.TabIndex = 7;
            this.btnEnviarWhatsapp.Text = "Enviar Whatsapp [F5]";
            this.btnEnviarWhatsapp.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnEnviarWhatsapp.UseAccentColor = false;
            this.btnEnviarWhatsapp.UseVisualStyleBackColor = true;
            this.btnEnviarWhatsapp.Click += new System.EventHandler(this.btnEnviarWhatsapp_Click);
            // 
            // dsOrdemServico
            // 
            this.dsOrdemServico.DataSetName = "dsOrdemServico";
            this.dsOrdemServico.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsOrdemServicoExame
            // 
            this.dsOrdemServicoExame.DataSetName = "dsOrdemServicoExame";
            this.dsOrdemServicoExame.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsOrdemServicoProduto
            // 
            this.dsOrdemServicoProduto.DataSetName = "dsOrdemServicoProduto";
            this.dsOrdemServicoProduto.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsOrdemServicoServico
            // 
            this.dsOrdemServicoServico.DataSetName = "dsOrdemServicoServico";
            this.dsOrdemServicoServico.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsOrdemServicoPagamento
            // 
            this.dsOrdemServicoPagamento.DataSetName = "dsOrdemServicoPagamento";
            this.dsOrdemServicoPagamento.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSourceOrdem
            // 
            this.bindingSourceOrdem.DataMember = "OrdemServico";
            this.bindingSourceOrdem.DataSource = this.dsOrdemServico;
            // 
            // bindingSourceProd
            // 
            this.bindingSourceProd.DataMember = "OrdemServicoProduto";
            this.bindingSourceProd.DataSource = this.dsOrdemServicoProduto;
            // 
            // bindingSourceServico
            // 
            this.bindingSourceServico.DataMember = "OrdemServicoServico";
            this.bindingSourceServico.DataSource = this.dsOrdemServicoServico;
            // 
            // bindingSourceExame1
            // 
            this.bindingSourceExame1.DataMember = "Exame";
            this.bindingSourceExame1.DataSource = this.dsOrdemServicoExame;
            // 
            // bindingSourcePagamento
            // 
            this.bindingSourcePagamento.DataMember = "OrdemServicoPagamento";
            this.bindingSourcePagamento.DataSource = this.dsOrdemServicoPagamento;
            // 
            // FrmImprimirViasOtica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(585, 350);
            this.Controls.Add(this.btnEnviarWhatsapp);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmImprimirViasOtica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impressão de Vias";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmImprimirViasOtica_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServicoExame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServicoProduto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServicoServico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrdemServicoPagamento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOrdem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceProd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceServico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceExame1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePagamento)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialCheckbox chkLaboratorio;
        private MaterialSkin.Controls.MaterialCheckbox chkCliente;
        private MaterialSkin.Controls.MaterialCheckbox chkLoja;
        private System.Windows.Forms.GroupBox groupBox1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialTextBox txtWhatsappCliente;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialTextBox txtWhatsappLaboratorio;
        private MaterialSkin.Controls.MaterialButton btnImprimir;
        private MaterialSkin.Controls.MaterialButton btnCancelar;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialComboBox comboImpressoras;
        private MaterialSkin.Controls.MaterialButton btnEnviarWhatsapp;
        private DataSetOrdemServico.dsOrdemServico dsOrdemServico;
        private DataSetOrdemServico.dsOrdemServicoExame dsOrdemServicoExame;
        private DataSetOrdemServico.dsOrdemServicoProduto dsOrdemServicoProduto;
        private DataSetOrdemServico.dsOrdemServicoServico dsOrdemServicoServico;
        private DataSetOrdemServico.dsOrdemServicoPagamento dsOrdemServicoPagamento;
        private System.Windows.Forms.BindingSource bindingSourceOrdem;
        private System.Windows.Forms.BindingSource bindingSourceProd;
        private System.Windows.Forms.BindingSource bindingSourceServico;
        private System.Windows.Forms.BindingSource bindingSourceExame1;
        private System.Windows.Forms.BindingSource bindingSourcePagamento;
    }
}