namespace Lunar.Telas.ContasPagar
{
    partial class FrmNovaFaturaPagar
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
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn1 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn2 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn3 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel7 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.autoLabel14 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel12 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaPlanoContas = new FontAwesome.Sharp.IconButton();
            this.autoLabel13 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel10 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisarFormaPagamento = new FontAwesome.Sharp.IconButton();
            this.autoLabel11 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.chkVencimentoFixo = new System.Windows.Forms.CheckBox();
            this.autoLabel8 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtPrimeiroVencimento = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel9 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataEmissao = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaClienteFornecedor = new FontAwesome.Sharp.IconButton();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.gridParcelas = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.dsParcelas = new System.Data.DataSet();
            this.Parcelas = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.autoLabel15 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaEmpresa = new FontAwesome.Sharp.IconButton();
            this.autoLabel16 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
            this.txtCodEmpresa = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtEmpresa = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnGerar = new Lunar.RJ_UI.Classes.RJButton();
            this.txtDescricao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodPlanoContas = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtPlanoContas = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodFormaPagamento = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtFormaPagamento = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtIntervalo = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtParcelas = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtValorTotal = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtNumeroDocumento = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodClienteFornecedor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtClienteFornecedor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.panelTitleBar.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridParcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsParcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Parcelas)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel7);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(578, 44);
            this.panelTitleBar.TabIndex = 240;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // autoLabel7
            // 
            this.autoLabel7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel7.ForeColor = System.Drawing.Color.White;
            this.autoLabel7.Location = new System.Drawing.Point(5, 7);
            this.autoLabel7.Name = "autoLabel7";
            this.autoLabel7.Size = new System.Drawing.Size(278, 25);
            this.autoLabel7.TabIndex = 198;
            this.autoLabel7.Text = "Cadastro de Fatura a Pagar";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Multiply;
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClose.IconSize = 30;
            this.btnClose.Location = new System.Drawing.Point(537, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.autoLabel15);
            this.groupBox1.Controls.Add(this.btnPesquisaEmpresa);
            this.groupBox1.Controls.Add(this.txtCodEmpresa);
            this.groupBox1.Controls.Add(this.autoLabel16);
            this.groupBox1.Controls.Add(this.txtEmpresa);
            this.groupBox1.Controls.Add(this.btnGerar);
            this.groupBox1.Controls.Add(this.autoLabel14);
            this.groupBox1.Controls.Add(this.txtDescricao);
            this.groupBox1.Controls.Add(this.autoLabel12);
            this.groupBox1.Controls.Add(this.btnPesquisaPlanoContas);
            this.groupBox1.Controls.Add(this.txtCodPlanoContas);
            this.groupBox1.Controls.Add(this.autoLabel13);
            this.groupBox1.Controls.Add(this.txtPlanoContas);
            this.groupBox1.Controls.Add(this.autoLabel10);
            this.groupBox1.Controls.Add(this.btnPesquisarFormaPagamento);
            this.groupBox1.Controls.Add(this.txtCodFormaPagamento);
            this.groupBox1.Controls.Add(this.autoLabel11);
            this.groupBox1.Controls.Add(this.txtFormaPagamento);
            this.groupBox1.Controls.Add(this.chkVencimentoFixo);
            this.groupBox1.Controls.Add(this.autoLabel8);
            this.groupBox1.Controls.Add(this.txtIntervalo);
            this.groupBox1.Controls.Add(this.autoLabel6);
            this.groupBox1.Controls.Add(this.txtParcelas);
            this.groupBox1.Controls.Add(this.txtPrimeiroVencimento);
            this.groupBox1.Controls.Add(this.autoLabel9);
            this.groupBox1.Controls.Add(this.autoLabel4);
            this.groupBox1.Controls.Add(this.txtValorTotal);
            this.groupBox1.Controls.Add(this.autoLabel5);
            this.groupBox1.Controls.Add(this.txtNumeroDocumento);
            this.groupBox1.Controls.Add(this.txtDataEmissao);
            this.groupBox1.Controls.Add(this.autoLabel3);
            this.groupBox1.Controls.Add(this.autoLabel2);
            this.groupBox1.Controls.Add(this.btnPesquisaClienteFornecedor);
            this.groupBox1.Controls.Add(this.txtCodClienteFornecedor);
            this.groupBox1.Controls.Add(this.autoLabel1);
            this.groupBox1.Controls.Add(this.txtClienteFornecedor);
            this.groupBox1.Location = new System.Drawing.Point(1, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(576, 444);
            this.groupBox1.TabIndex = 241;
            this.groupBox1.TabStop = false;
            // 
            // autoLabel14
            // 
            this.autoLabel14.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel14.ForeColor = System.Drawing.Color.Black;
            this.autoLabel14.Location = new System.Drawing.Point(19, 381);
            this.autoLabel14.Name = "autoLabel14";
            this.autoLabel14.Size = new System.Drawing.Size(190, 16);
            this.autoLabel14.TabIndex = 264;
            this.autoLabel14.Text = "Descrição Opcional (Histórico)";
            // 
            // autoLabel12
            // 
            this.autoLabel12.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel12.ForeColor = System.Drawing.Color.Black;
            this.autoLabel12.Location = new System.Drawing.Point(488, 323);
            this.autoLabel12.Name = "autoLabel12";
            this.autoLabel12.Size = new System.Drawing.Size(51, 16);
            this.autoLabel12.TabIndex = 262;
            this.autoLabel12.Text = "Código";
            // 
            // btnPesquisaPlanoContas
            // 
            this.btnPesquisaPlanoContas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaPlanoContas.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaPlanoContas.FlatAppearance.BorderSize = 0;
            this.btnPesquisaPlanoContas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPlanoContas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPlanoContas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaPlanoContas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaPlanoContas.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaPlanoContas.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaPlanoContas.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaPlanoContas.IconSize = 38;
            this.btnPesquisaPlanoContas.Location = new System.Drawing.Point(438, 338);
            this.btnPesquisaPlanoContas.Name = "btnPesquisaPlanoContas";
            this.btnPesquisaPlanoContas.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaPlanoContas.TabIndex = 17;
            this.btnPesquisaPlanoContas.UseVisualStyleBackColor = true;
            this.btnPesquisaPlanoContas.Click += new System.EventHandler(this.btnPesquisaPlanoContas_Click);
            // 
            // autoLabel13
            // 
            this.autoLabel13.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel13.ForeColor = System.Drawing.Color.Black;
            this.autoLabel13.Location = new System.Drawing.Point(19, 323);
            this.autoLabel13.Name = "autoLabel13";
            this.autoLabel13.Size = new System.Drawing.Size(172, 16);
            this.autoLabel13.TabIndex = 259;
            this.autoLabel13.Text = "Plano de Contas (Receita) *";
            // 
            // autoLabel10
            // 
            this.autoLabel10.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel10.ForeColor = System.Drawing.Color.Black;
            this.autoLabel10.Location = new System.Drawing.Point(488, 265);
            this.autoLabel10.Name = "autoLabel10";
            this.autoLabel10.Size = new System.Drawing.Size(51, 16);
            this.autoLabel10.TabIndex = 257;
            this.autoLabel10.Text = "Código";
            // 
            // btnPesquisarFormaPagamento
            // 
            this.btnPesquisarFormaPagamento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisarFormaPagamento.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisarFormaPagamento.FlatAppearance.BorderSize = 0;
            this.btnPesquisarFormaPagamento.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisarFormaPagamento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisarFormaPagamento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisarFormaPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisarFormaPagamento.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisarFormaPagamento.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisarFormaPagamento.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisarFormaPagamento.IconSize = 38;
            this.btnPesquisarFormaPagamento.Location = new System.Drawing.Point(438, 280);
            this.btnPesquisarFormaPagamento.Name = "btnPesquisarFormaPagamento";
            this.btnPesquisarFormaPagamento.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisarFormaPagamento.TabIndex = 11;
            this.btnPesquisarFormaPagamento.UseVisualStyleBackColor = true;
            this.btnPesquisarFormaPagamento.Click += new System.EventHandler(this.btnPesquisarFormaPagamento_Click);
            // 
            // autoLabel11
            // 
            this.autoLabel11.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel11.ForeColor = System.Drawing.Color.Black;
            this.autoLabel11.Location = new System.Drawing.Point(19, 265);
            this.autoLabel11.Name = "autoLabel11";
            this.autoLabel11.Size = new System.Drawing.Size(146, 16);
            this.autoLabel11.TabIndex = 254;
            this.autoLabel11.Text = "Forma de Pagamento *";
            // 
            // chkVencimentoFixo
            // 
            this.chkVencimentoFixo.AutoSize = true;
            this.chkVencimentoFixo.Checked = true;
            this.chkVencimentoFixo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVencimentoFixo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkVencimentoFixo.Location = new System.Drawing.Point(421, 227);
            this.chkVencimentoFixo.Name = "chkVencimentoFixo";
            this.chkVencimentoFixo.Size = new System.Drawing.Size(146, 24);
            this.chkVencimentoFixo.TabIndex = 9;
            this.chkVencimentoFixo.Text = "Vencimento Fixo";
            this.chkVencimentoFixo.UseVisualStyleBackColor = true;
            // 
            // autoLabel8
            // 
            this.autoLabel8.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel8.ForeColor = System.Drawing.Color.Black;
            this.autoLabel8.Location = new System.Drawing.Point(124, 208);
            this.autoLabel8.Name = "autoLabel8";
            this.autoLabel8.Size = new System.Drawing.Size(89, 16);
            this.autoLabel8.TabIndex = 251;
            this.autoLabel8.Text = "Intervalo Dias";
            // 
            // autoLabel6
            // 
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(17, 208);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(69, 16);
            this.autoLabel6.TabIndex = 249;
            this.autoLabel6.Text = "Parcelas *";
            // 
            // txtPrimeiroVencimento
            // 
            this.txtPrimeiroVencimento.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtPrimeiroVencimento.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtPrimeiroVencimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtPrimeiroVencimento.Location = new System.Drawing.Point(231, 222);
            this.txtPrimeiroVencimento.Name = "txtPrimeiroVencimento";
            this.txtPrimeiroVencimento.Size = new System.Drawing.Size(179, 35);
            this.txtPrimeiroVencimento.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtPrimeiroVencimento.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtPrimeiroVencimento.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtPrimeiroVencimento.TabIndex = 8;
            this.txtPrimeiroVencimento.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel9
            // 
            this.autoLabel9.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel9.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel9.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel9.ForeColor = System.Drawing.Color.Black;
            this.autoLabel9.Location = new System.Drawing.Point(231, 200);
            this.autoLabel9.Name = "autoLabel9";
            this.autoLabel9.Size = new System.Drawing.Size(103, 21);
            this.autoLabel9.TabIndex = 245;
            this.autoLabel9.Text = "1º Vencimento";
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(410, 141);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(81, 16);
            this.autoLabel4.TabIndex = 242;
            this.autoLabel4.Text = "Valor Total *";
            // 
            // autoLabel5
            // 
            this.autoLabel5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.Black;
            this.autoLabel5.Location = new System.Drawing.Point(203, 141);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(94, 16);
            this.autoLabel5.TabIndex = 240;
            this.autoLabel5.Text = "Nº Documento";
            // 
            // txtDataEmissao
            // 
            this.txtDataEmissao.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataEmissao.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataEmissao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDataEmissao.Location = new System.Drawing.Point(9, 153);
            this.txtDataEmissao.Name = "txtDataEmissao";
            this.txtDataEmissao.Size = new System.Drawing.Size(179, 35);
            this.txtDataEmissao.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataEmissao.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataEmissao.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataEmissao.TabIndex = 3;
            this.txtDataEmissao.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            this.txtDataEmissao.Leave += new System.EventHandler(this.txtDataEmissao_Leave);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(9, 136);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(111, 16);
            this.autoLabel3.TabIndex = 238;
            this.autoLabel3.Text = "Data de Emissão";
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(490, 77);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(51, 16);
            this.autoLabel2.TabIndex = 236;
            this.autoLabel2.Text = "Código";
            // 
            // btnPesquisaClienteFornecedor
            // 
            this.btnPesquisaClienteFornecedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaClienteFornecedor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaClienteFornecedor.FlatAppearance.BorderSize = 0;
            this.btnPesquisaClienteFornecedor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaClienteFornecedor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaClienteFornecedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaClienteFornecedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaClienteFornecedor.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaClienteFornecedor.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaClienteFornecedor.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaClienteFornecedor.IconSize = 38;
            this.btnPesquisaClienteFornecedor.Location = new System.Drawing.Point(438, 92);
            this.btnPesquisaClienteFornecedor.Name = "btnPesquisaClienteFornecedor";
            this.btnPesquisaClienteFornecedor.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaClienteFornecedor.TabIndex = 1;
            this.btnPesquisaClienteFornecedor.UseVisualStyleBackColor = true;
            this.btnPesquisaClienteFornecedor.Click += new System.EventHandler(this.btnPesquisaClienteFornecedor_Click);
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(21, 77);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(130, 16);
            this.autoLabel1.TabIndex = 233;
            this.autoLabel1.Text = "Cliente/Fornecedor *";
            // 
            // gridParcelas
            // 
            this.gridParcelas.AccessibleName = "Table";
            this.gridParcelas.AllowSorting = false;
            gridTextColumn1.AllowSorting = false;
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderText = "Parcela";
            gridTextColumn1.MappingName = "PARCELA";
            gridTextColumn2.AdvancedFilterType = Syncfusion.WinForms.GridCommon.AdvancedFilterType.DateFilter;
            gridTextColumn2.AllowSorting = false;
            gridTextColumn2.Format = "dd/MM/yyyy";
            gridTextColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn2.HeaderText = "Vencimento";
            gridTextColumn2.MappingName = "VENCIMENTO";
            gridTextColumn3.AllowSorting = false;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderText = "Valor";
            gridTextColumn3.MappingName = "VALOR";
            this.gridParcelas.Columns.Add(gridTextColumn1);
            this.gridParcelas.Columns.Add(gridTextColumn2);
            this.gridParcelas.Columns.Add(gridTextColumn3);
            this.gridParcelas.Location = new System.Drawing.Point(5, 500);
            this.gridParcelas.Name = "gridParcelas";
            this.gridParcelas.Size = new System.Drawing.Size(567, 127);
            this.gridParcelas.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.gridParcelas.TabIndex = 243;
            this.gridParcelas.Text = "sfDataGrid1";
            this.gridParcelas.CurrentCellValidating += new Syncfusion.WinForms.DataGrid.Events.CurrentCellValidatingEventHandler(this.gridParcelas_CurrentCellValidating);
            // 
            // dsParcelas
            // 
            this.dsParcelas.DataSetName = "dsParcelas";
            this.dsParcelas.Tables.AddRange(new System.Data.DataTable[] {
            this.Parcelas});
            // 
            // Parcelas
            // 
            this.Parcelas.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3});
            this.Parcelas.TableName = "Parcelas";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "PARCELA";
            this.dataColumn1.DataType = typeof(int);
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "VENCIMENTO";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "VALOR";
            this.dataColumn3.DataType = typeof(decimal);
            // 
            // autoLabel15
            // 
            this.autoLabel15.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel15.ForeColor = System.Drawing.Color.Black;
            this.autoLabel15.Location = new System.Drawing.Point(490, 21);
            this.autoLabel15.Name = "autoLabel15";
            this.autoLabel15.Size = new System.Drawing.Size(51, 16);
            this.autoLabel15.TabIndex = 269;
            this.autoLabel15.Text = "Código";
            // 
            // btnPesquisaEmpresa
            // 
            this.btnPesquisaEmpresa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaEmpresa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaEmpresa.FlatAppearance.BorderSize = 0;
            this.btnPesquisaEmpresa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaEmpresa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaEmpresa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaEmpresa.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaEmpresa.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaEmpresa.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaEmpresa.IconSize = 38;
            this.btnPesquisaEmpresa.Location = new System.Drawing.Point(438, 36);
            this.btnPesquisaEmpresa.Name = "btnPesquisaEmpresa";
            this.btnPesquisaEmpresa.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaEmpresa.TabIndex = 266;
            this.btnPesquisaEmpresa.UseVisualStyleBackColor = true;
            this.btnPesquisaEmpresa.Click += new System.EventHandler(this.btnPesquisaEmpresa_Click);
            // 
            // autoLabel16
            // 
            this.autoLabel16.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel16.ForeColor = System.Drawing.Color.Black;
            this.autoLabel16.Location = new System.Drawing.Point(21, 21);
            this.autoLabel16.Name = "autoLabel16";
            this.autoLabel16.Size = new System.Drawing.Size(101, 16);
            this.autoLabel16.TabIndex = 268;
            this.autoLabel16.Text = "Empresa (Filial)";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnConfirmar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnConfirmar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnConfirmar.BorderRadius = 8;
            this.btnConfirmar.BorderSize = 0;
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Location = new System.Drawing.Point(145, 633);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(289, 45);
            this.btnConfirmar.TabIndex = 242;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.White;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // txtCodEmpresa
            // 
            this.txtCodEmpresa.BackColor = System.Drawing.Color.White;
            this.txtCodEmpresa.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodEmpresa.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodEmpresa.BorderRadius = 8;
            this.txtCodEmpresa.BorderSize = 2;
            this.txtCodEmpresa.Enabled = false;
            this.txtCodEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodEmpresa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodEmpresa.Location = new System.Drawing.Point(481, 33);
            this.txtCodEmpresa.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodEmpresa.Multiline = false;
            this.txtCodEmpresa.Name = "txtCodEmpresa";
            this.txtCodEmpresa.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodEmpresa.PasswordChar = false;
            this.txtCodEmpresa.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodEmpresa.PlaceholderText = "";
            this.txtCodEmpresa.ReadOnly = false;
            this.txtCodEmpresa.Size = new System.Drawing.Size(89, 37);
            this.txtCodEmpresa.TabIndex = 267;
            this.txtCodEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodEmpresa.Texts = "";
            this.txtCodEmpresa.UnderlinedStyle = false;
            // 
            // txtEmpresa
            // 
            this.txtEmpresa.BackColor = System.Drawing.Color.White;
            this.txtEmpresa.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEmpresa.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEmpresa.BorderRadius = 8;
            this.txtEmpresa.BorderSize = 2;
            this.txtEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpresa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEmpresa.Location = new System.Drawing.Point(9, 33);
            this.txtEmpresa.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmpresa.Multiline = false;
            this.txtEmpresa.Name = "txtEmpresa";
            this.txtEmpresa.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtEmpresa.PasswordChar = false;
            this.txtEmpresa.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtEmpresa.PlaceholderText = "";
            this.txtEmpresa.ReadOnly = false;
            this.txtEmpresa.Size = new System.Drawing.Size(422, 37);
            this.txtEmpresa.TabIndex = 265;
            this.txtEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEmpresa.Texts = "";
            this.txtEmpresa.UnderlinedStyle = false;
            this.txtEmpresa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpresa_KeyPress);
            // 
            // btnGerar
            // 
            this.btnGerar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnGerar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnGerar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnGerar.BorderRadius = 8;
            this.btnGerar.BorderSize = 2;
            this.btnGerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGerar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnGerar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(90)))));
            this.btnGerar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnGerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerar.ForeColor = System.Drawing.Color.White;
            this.btnGerar.Location = new System.Drawing.Point(438, 393);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(132, 37);
            this.btnGerar.TabIndex = 20;
            this.btnGerar.Text = "Gerar [F2]";
            this.btnGerar.TextColor = System.Drawing.Color.White;
            this.btnGerar.UseVisualStyleBackColor = false;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // txtDescricao
            // 
            this.txtDescricao.BackColor = System.Drawing.Color.White;
            this.txtDescricao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricao.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricao.BorderRadius = 8;
            this.txtDescricao.BorderSize = 2;
            this.txtDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDescricao.Location = new System.Drawing.Point(9, 393);
            this.txtDescricao.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricao.Multiline = false;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescricao.PasswordChar = false;
            this.txtDescricao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDescricao.PlaceholderText = "";
            this.txtDescricao.ReadOnly = false;
            this.txtDescricao.Size = new System.Drawing.Size(422, 37);
            this.txtDescricao.TabIndex = 19;
            this.txtDescricao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescricao.Texts = "";
            this.txtDescricao.UnderlinedStyle = false;
            // 
            // txtCodPlanoContas
            // 
            this.txtCodPlanoContas.BackColor = System.Drawing.Color.White;
            this.txtCodPlanoContas.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodPlanoContas.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodPlanoContas.BorderRadius = 8;
            this.txtCodPlanoContas.BorderSize = 2;
            this.txtCodPlanoContas.Enabled = false;
            this.txtCodPlanoContas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodPlanoContas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodPlanoContas.Location = new System.Drawing.Point(481, 335);
            this.txtCodPlanoContas.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodPlanoContas.Multiline = false;
            this.txtCodPlanoContas.Name = "txtCodPlanoContas";
            this.txtCodPlanoContas.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodPlanoContas.PasswordChar = false;
            this.txtCodPlanoContas.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodPlanoContas.PlaceholderText = "";
            this.txtCodPlanoContas.ReadOnly = false;
            this.txtCodPlanoContas.Size = new System.Drawing.Size(89, 37);
            this.txtCodPlanoContas.TabIndex = 18;
            this.txtCodPlanoContas.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodPlanoContas.Texts = "";
            this.txtCodPlanoContas.UnderlinedStyle = false;
            // 
            // txtPlanoContas
            // 
            this.txtPlanoContas.BackColor = System.Drawing.Color.White;
            this.txtPlanoContas.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPlanoContas.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPlanoContas.BorderRadius = 8;
            this.txtPlanoContas.BorderSize = 2;
            this.txtPlanoContas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlanoContas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPlanoContas.Location = new System.Drawing.Point(9, 335);
            this.txtPlanoContas.Margin = new System.Windows.Forms.Padding(4);
            this.txtPlanoContas.Multiline = false;
            this.txtPlanoContas.Name = "txtPlanoContas";
            this.txtPlanoContas.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtPlanoContas.PasswordChar = false;
            this.txtPlanoContas.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtPlanoContas.PlaceholderText = "";
            this.txtPlanoContas.ReadOnly = false;
            this.txtPlanoContas.Size = new System.Drawing.Size(422, 37);
            this.txtPlanoContas.TabIndex = 16;
            this.txtPlanoContas.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPlanoContas.Texts = "";
            this.txtPlanoContas.UnderlinedStyle = false;
            this.txtPlanoContas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlanoContas_KeyPress);
            // 
            // txtCodFormaPagamento
            // 
            this.txtCodFormaPagamento.BackColor = System.Drawing.Color.White;
            this.txtCodFormaPagamento.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodFormaPagamento.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodFormaPagamento.BorderRadius = 8;
            this.txtCodFormaPagamento.BorderSize = 2;
            this.txtCodFormaPagamento.Enabled = false;
            this.txtCodFormaPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodFormaPagamento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodFormaPagamento.Location = new System.Drawing.Point(481, 277);
            this.txtCodFormaPagamento.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodFormaPagamento.Multiline = false;
            this.txtCodFormaPagamento.Name = "txtCodFormaPagamento";
            this.txtCodFormaPagamento.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodFormaPagamento.PasswordChar = false;
            this.txtCodFormaPagamento.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodFormaPagamento.PlaceholderText = "";
            this.txtCodFormaPagamento.ReadOnly = false;
            this.txtCodFormaPagamento.Size = new System.Drawing.Size(89, 37);
            this.txtCodFormaPagamento.TabIndex = 12;
            this.txtCodFormaPagamento.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodFormaPagamento.Texts = "";
            this.txtCodFormaPagamento.UnderlinedStyle = false;
            // 
            // txtFormaPagamento
            // 
            this.txtFormaPagamento.BackColor = System.Drawing.Color.White;
            this.txtFormaPagamento.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtFormaPagamento.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtFormaPagamento.BorderRadius = 8;
            this.txtFormaPagamento.BorderSize = 2;
            this.txtFormaPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormaPagamento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtFormaPagamento.Location = new System.Drawing.Point(9, 277);
            this.txtFormaPagamento.Margin = new System.Windows.Forms.Padding(4);
            this.txtFormaPagamento.Multiline = false;
            this.txtFormaPagamento.Name = "txtFormaPagamento";
            this.txtFormaPagamento.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtFormaPagamento.PasswordChar = false;
            this.txtFormaPagamento.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtFormaPagamento.PlaceholderText = "";
            this.txtFormaPagamento.ReadOnly = false;
            this.txtFormaPagamento.Size = new System.Drawing.Size(422, 37);
            this.txtFormaPagamento.TabIndex = 10;
            this.txtFormaPagamento.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFormaPagamento.Texts = "";
            this.txtFormaPagamento.UnderlinedStyle = false;
            this.txtFormaPagamento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFormaPagamento_KeyPress);
            // 
            // txtIntervalo
            // 
            this.txtIntervalo.BackColor = System.Drawing.Color.White;
            this.txtIntervalo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtIntervalo.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtIntervalo.BorderRadius = 8;
            this.txtIntervalo.BorderSize = 2;
            this.txtIntervalo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIntervalo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtIntervalo.Location = new System.Drawing.Point(117, 220);
            this.txtIntervalo.Margin = new System.Windows.Forms.Padding(4);
            this.txtIntervalo.Multiline = false;
            this.txtIntervalo.Name = "txtIntervalo";
            this.txtIntervalo.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtIntervalo.PasswordChar = false;
            this.txtIntervalo.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtIntervalo.PlaceholderText = "";
            this.txtIntervalo.ReadOnly = false;
            this.txtIntervalo.Size = new System.Drawing.Size(100, 37);
            this.txtIntervalo.TabIndex = 7;
            this.txtIntervalo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtIntervalo.Texts = "30";
            this.txtIntervalo.UnderlinedStyle = false;
            this.txtIntervalo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIntervalo_KeyPress);
            // 
            // txtParcelas
            // 
            this.txtParcelas.BackColor = System.Drawing.Color.White;
            this.txtParcelas.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtParcelas.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtParcelas.BorderRadius = 8;
            this.txtParcelas.BorderSize = 2;
            this.txtParcelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParcelas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtParcelas.Location = new System.Drawing.Point(9, 220);
            this.txtParcelas.Margin = new System.Windows.Forms.Padding(4);
            this.txtParcelas.Multiline = false;
            this.txtParcelas.Name = "txtParcelas";
            this.txtParcelas.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtParcelas.PasswordChar = false;
            this.txtParcelas.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtParcelas.PlaceholderText = "";
            this.txtParcelas.ReadOnly = false;
            this.txtParcelas.Size = new System.Drawing.Size(100, 37);
            this.txtParcelas.TabIndex = 6;
            this.txtParcelas.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtParcelas.Texts = "";
            this.txtParcelas.UnderlinedStyle = false;
            this.txtParcelas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtParcelas_KeyPress);
            // 
            // txtValorTotal
            // 
            this.txtValorTotal.BackColor = System.Drawing.Color.White;
            this.txtValorTotal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValorTotal.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValorTotal.BorderRadius = 8;
            this.txtValorTotal.BorderSize = 2;
            this.txtValorTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtValorTotal.Location = new System.Drawing.Point(402, 153);
            this.txtValorTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtValorTotal.Multiline = false;
            this.txtValorTotal.Name = "txtValorTotal";
            this.txtValorTotal.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValorTotal.PasswordChar = false;
            this.txtValorTotal.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtValorTotal.PlaceholderText = "";
            this.txtValorTotal.ReadOnly = false;
            this.txtValorTotal.Size = new System.Drawing.Size(168, 37);
            this.txtValorTotal.TabIndex = 5;
            this.txtValorTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValorTotal.Texts = "";
            this.txtValorTotal.UnderlinedStyle = false;
            this.txtValorTotal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValorTotal_KeyPress);
            this.txtValorTotal.Leave += new System.EventHandler(this.txtValorTotal_Leave);
            // 
            // txtNumeroDocumento
            // 
            this.txtNumeroDocumento.BackColor = System.Drawing.Color.White;
            this.txtNumeroDocumento.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNumeroDocumento.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNumeroDocumento.BorderRadius = 8;
            this.txtNumeroDocumento.BorderSize = 2;
            this.txtNumeroDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroDocumento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNumeroDocumento.Location = new System.Drawing.Point(195, 153);
            this.txtNumeroDocumento.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumeroDocumento.Multiline = false;
            this.txtNumeroDocumento.Name = "txtNumeroDocumento";
            this.txtNumeroDocumento.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtNumeroDocumento.PasswordChar = false;
            this.txtNumeroDocumento.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtNumeroDocumento.PlaceholderText = "";
            this.txtNumeroDocumento.ReadOnly = false;
            this.txtNumeroDocumento.Size = new System.Drawing.Size(199, 37);
            this.txtNumeroDocumento.TabIndex = 4;
            this.txtNumeroDocumento.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNumeroDocumento.Texts = "";
            this.txtNumeroDocumento.UnderlinedStyle = false;
            // 
            // txtCodClienteFornecedor
            // 
            this.txtCodClienteFornecedor.BackColor = System.Drawing.Color.White;
            this.txtCodClienteFornecedor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodClienteFornecedor.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodClienteFornecedor.BorderRadius = 8;
            this.txtCodClienteFornecedor.BorderSize = 2;
            this.txtCodClienteFornecedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodClienteFornecedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodClienteFornecedor.Location = new System.Drawing.Point(481, 89);
            this.txtCodClienteFornecedor.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodClienteFornecedor.Multiline = false;
            this.txtCodClienteFornecedor.Name = "txtCodClienteFornecedor";
            this.txtCodClienteFornecedor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodClienteFornecedor.PasswordChar = false;
            this.txtCodClienteFornecedor.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodClienteFornecedor.PlaceholderText = "";
            this.txtCodClienteFornecedor.ReadOnly = false;
            this.txtCodClienteFornecedor.Size = new System.Drawing.Size(89, 37);
            this.txtCodClienteFornecedor.TabIndex = 2;
            this.txtCodClienteFornecedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodClienteFornecedor.Texts = "";
            this.txtCodClienteFornecedor.UnderlinedStyle = false;
            this.txtCodClienteFornecedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodClienteFornecedor_KeyPress);
            // 
            // txtClienteFornecedor
            // 
            this.txtClienteFornecedor.BackColor = System.Drawing.Color.White;
            this.txtClienteFornecedor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtClienteFornecedor.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtClienteFornecedor.BorderRadius = 8;
            this.txtClienteFornecedor.BorderSize = 2;
            this.txtClienteFornecedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClienteFornecedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtClienteFornecedor.Location = new System.Drawing.Point(9, 89);
            this.txtClienteFornecedor.Margin = new System.Windows.Forms.Padding(4);
            this.txtClienteFornecedor.Multiline = false;
            this.txtClienteFornecedor.Name = "txtClienteFornecedor";
            this.txtClienteFornecedor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtClienteFornecedor.PasswordChar = false;
            this.txtClienteFornecedor.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtClienteFornecedor.PlaceholderText = "";
            this.txtClienteFornecedor.ReadOnly = false;
            this.txtClienteFornecedor.Size = new System.Drawing.Size(422, 37);
            this.txtClienteFornecedor.TabIndex = 0;
            this.txtClienteFornecedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtClienteFornecedor.Texts = "";
            this.txtClienteFornecedor.UnderlinedStyle = false;
            this.txtClienteFornecedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClienteFornecedor_KeyPress);
            // 
            // FrmNovaFaturaPagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(578, 686);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.gridParcelas);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmNovaFaturaPagar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nova Fatura";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridParcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsParcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Parcelas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel7;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private RJ_UI.Classes.RJButton btnGerar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel14;
        private RJ_UI.Classes.RJTextBox txtDescricao;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel12;
        private FontAwesome.Sharp.IconButton btnPesquisaPlanoContas;
        private RJ_UI.Classes.RJTextBox txtCodPlanoContas;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel13;
        private RJ_UI.Classes.RJTextBox txtPlanoContas;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel10;
        private FontAwesome.Sharp.IconButton btnPesquisarFormaPagamento;
        private RJ_UI.Classes.RJTextBox txtCodFormaPagamento;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel11;
        private RJ_UI.Classes.RJTextBox txtFormaPagamento;
        private System.Windows.Forms.CheckBox chkVencimentoFixo;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel8;
        private RJ_UI.Classes.RJTextBox txtIntervalo;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private RJ_UI.Classes.RJTextBox txtParcelas;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtPrimeiroVencimento;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel9;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private RJ_UI.Classes.RJTextBox txtValorTotal;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private RJ_UI.Classes.RJTextBox txtNumeroDocumento;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataEmissao;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private FontAwesome.Sharp.IconButton btnPesquisaClienteFornecedor;
        private RJ_UI.Classes.RJTextBox txtCodClienteFornecedor;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtClienteFornecedor;
        private RJ_UI.Classes.RJButton btnConfirmar;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridParcelas;
        private System.Data.DataSet dsParcelas;
        private System.Data.DataTable Parcelas;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel15;
        private FontAwesome.Sharp.IconButton btnPesquisaEmpresa;
        private RJ_UI.Classes.RJTextBox txtCodEmpresa;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel16;
        private RJ_UI.Classes.RJTextBox txtEmpresa;
    }
}