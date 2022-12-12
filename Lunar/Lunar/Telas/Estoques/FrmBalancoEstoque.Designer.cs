namespace Lunar.Telas.Estoques
{
    partial class FrmBalancoEstoque
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
            Syncfusion.WinForms.DataGrid.GridCheckBoxColumn gridCheckBoxColumn1 = new Syncfusion.WinForms.DataGrid.GridCheckBoxColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn4 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn5 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn6 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn1 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBalancoEstoque));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel9 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.chkParaCampoQuantidade = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.autoLabel8 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtId = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel7 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDescricao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnAtualizarEstoque = new FontAwesome.Sharp.IconButton();
            this.lblEstoqueAuxiliarData = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtEstoqueAuxiliarData = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtEstoqueAuxiliar = new Lunar.RJ_UI.Classes.RJTextBox();
            this.lblEstoqueData = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtEstoqueData = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtDataAjuste = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.radioNaoConciliado = new System.Windows.Forms.RadioButton();
            this.radioConciliado = new System.Windows.Forms.RadioButton();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtEstoqueAtual = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnConfirmar = new FontAwesome.Sharp.IconButton();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtQuantidadeEfetiva = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaProduto = new FontAwesome.Sharp.IconButton();
            this.txtCodProduto = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtProduto = new Lunar.RJ_UI.Classes.RJTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExcluirItem = new Lunar.RJ_UI.Classes.RJButton();
            this.btnGravar = new Lunar.RJ_UI.Classes.RJButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.dsBalanco = new System.Data.DataSet();
            this.Balanco = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.BalancoEstoqueProduto = new System.Data.DataColumn();
            this.chkZerarEstoque = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsBalanco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Balanco)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkZerarEstoque);
            this.groupBox1.Controls.Add(this.panelTitleBar);
            this.groupBox1.Controls.Add(this.chkParaCampoQuantidade);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.autoLabel8);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.autoLabel7);
            this.groupBox1.Controls.Add(this.txtDescricao);
            this.groupBox1.Controls.Add(this.btnAtualizarEstoque);
            this.groupBox1.Controls.Add(this.lblEstoqueAuxiliarData);
            this.groupBox1.Controls.Add(this.txtEstoqueAuxiliarData);
            this.groupBox1.Controls.Add(this.autoLabel5);
            this.groupBox1.Controls.Add(this.txtEstoqueAuxiliar);
            this.groupBox1.Controls.Add(this.lblEstoqueData);
            this.groupBox1.Controls.Add(this.txtEstoqueData);
            this.groupBox1.Controls.Add(this.txtDataAjuste);
            this.groupBox1.Controls.Add(this.autoLabel6);
            this.groupBox1.Controls.Add(this.radioNaoConciliado);
            this.groupBox1.Controls.Add(this.radioConciliado);
            this.groupBox1.Controls.Add(this.autoLabel4);
            this.groupBox1.Controls.Add(this.txtEstoqueAtual);
            this.groupBox1.Controls.Add(this.btnConfirmar);
            this.groupBox1.Controls.Add(this.autoLabel3);
            this.groupBox1.Controls.Add(this.txtQuantidadeEfetiva);
            this.groupBox1.Controls.Add(this.autoLabel2);
            this.groupBox1.Controls.Add(this.btnPesquisaProduto);
            this.groupBox1.Controls.Add(this.txtCodProduto);
            this.groupBox1.Controls.Add(this.autoLabel1);
            this.groupBox1.Controls.Add(this.txtProduto);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(873, 306);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel9);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(3, 16);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(867, 44);
            this.panelTitleBar.TabIndex = 262;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // autoLabel9
            // 
            this.autoLabel9.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel9.ForeColor = System.Drawing.Color.White;
            this.autoLabel9.Location = new System.Drawing.Point(9, 9);
            this.autoLabel9.Name = "autoLabel9";
            this.autoLabel9.Size = new System.Drawing.Size(205, 25);
            this.autoLabel9.TabIndex = 200;
            this.autoLabel9.Text = "Balanço de Estoque";
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
            this.btnClose.Location = new System.Drawing.Point(826, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkParaCampoQuantidade
            // 
            this.chkParaCampoQuantidade.AutoSize = true;
            this.chkParaCampoQuantidade.Location = new System.Drawing.Point(13, 280);
            this.chkParaCampoQuantidade.Name = "chkParaCampoQuantidade";
            this.chkParaCampoQuantidade.Size = new System.Drawing.Size(199, 17);
            this.chkParaCampoQuantidade.TabIndex = 261;
            this.chkParaCampoQuantidade.Text = "Sempre Parar no Campo Quantidade";
            this.chkParaCampoQuantidade.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkParaCampoQuantidade.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(460, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(362, 13);
            this.label1.TabIndex = 260;
            this.label1.Text = "Atenção, para realizar um balanço contábil sempre consulte o seu contador";
            // 
            // autoLabel8
            // 
            this.autoLabel8.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel8.ForeColor = System.Drawing.Color.Black;
            this.autoLabel8.Location = new System.Drawing.Point(741, 74);
            this.autoLabel8.Name = "autoLabel8";
            this.autoLabel8.Size = new System.Drawing.Size(20, 16);
            this.autoLabel8.TabIndex = 259;
            this.autoLabel8.Text = "ID";
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtId.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtId.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtId.BorderRadius = 8;
            this.txtId.BorderSize = 2;
            this.txtId.Enabled = false;
            this.txtId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtId.Location = new System.Drawing.Point(733, 90);
            this.txtId.Margin = new System.Windows.Forms.Padding(4);
            this.txtId.Multiline = false;
            this.txtId.Name = "txtId";
            this.txtId.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtId.PasswordChar = false;
            this.txtId.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtId.PlaceholderText = "";
            this.txtId.ReadOnly = false;
            this.txtId.Size = new System.Drawing.Size(89, 37);
            this.txtId.TabIndex = 258;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtId.Texts = "";
            this.txtId.UnderlinedStyle = false;
            // 
            // autoLabel7
            // 
            this.autoLabel7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel7.ForeColor = System.Drawing.Color.Black;
            this.autoLabel7.Location = new System.Drawing.Point(21, 74);
            this.autoLabel7.Name = "autoLabel7";
            this.autoLabel7.Size = new System.Drawing.Size(141, 16);
            this.autoLabel7.TabIndex = 257;
            this.autoLabel7.Text = "Descrição do Balanço";
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
            this.txtDescricao.Location = new System.Drawing.Point(13, 90);
            this.txtDescricao.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricao.Multiline = false;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescricao.PasswordChar = false;
            this.txtDescricao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDescricao.PlaceholderText = "";
            this.txtDescricao.ReadOnly = false;
            this.txtDescricao.Size = new System.Drawing.Size(712, 37);
            this.txtDescricao.TabIndex = 256;
            this.txtDescricao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescricao.Texts = "";
            this.txtDescricao.UnderlinedStyle = false;
            // 
            // btnAtualizarEstoque
            // 
            this.btnAtualizarEstoque.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizarEstoque.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnAtualizarEstoque.FlatAppearance.BorderSize = 0;
            this.btnAtualizarEstoque.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAtualizarEstoque.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAtualizarEstoque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizarEstoque.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizarEstoque.IconChar = FontAwesome.Sharp.IconChar.ArrowRotateForward;
            this.btnAtualizarEstoque.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnAtualizarEstoque.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnAtualizarEstoque.IconSize = 38;
            this.btnAtualizarEstoque.Location = new System.Drawing.Point(174, 209);
            this.btnAtualizarEstoque.Name = "btnAtualizarEstoque";
            this.btnAtualizarEstoque.Size = new System.Drawing.Size(36, 34);
            this.btnAtualizarEstoque.TabIndex = 255;
            this.btnAtualizarEstoque.UseVisualStyleBackColor = true;
            this.btnAtualizarEstoque.Click += new System.EventHandler(this.btnAtualizarEstoque_Click);
            // 
            // lblEstoqueAuxiliarData
            // 
            this.lblEstoqueAuxiliarData.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblEstoqueAuxiliarData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstoqueAuxiliarData.ForeColor = System.Drawing.Color.Black;
            this.lblEstoqueAuxiliarData.Location = new System.Drawing.Point(529, 189);
            this.lblEstoqueAuxiliarData.Name = "lblEstoqueAuxiliarData";
            this.lblEstoqueAuxiliarData.Size = new System.Drawing.Size(143, 16);
            this.lblEstoqueAuxiliarData.TabIndex = 253;
            this.lblEstoqueAuxiliarData.Text = "Est. Aux em 30/08/2022";
            // 
            // txtEstoqueAuxiliarData
            // 
            this.txtEstoqueAuxiliarData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEstoqueAuxiliarData.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEstoqueAuxiliarData.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEstoqueAuxiliarData.BorderRadius = 8;
            this.txtEstoqueAuxiliarData.BorderSize = 2;
            this.txtEstoqueAuxiliarData.Enabled = false;
            this.txtEstoqueAuxiliarData.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstoqueAuxiliarData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEstoqueAuxiliarData.Location = new System.Drawing.Point(523, 205);
            this.txtEstoqueAuxiliarData.Margin = new System.Windows.Forms.Padding(4);
            this.txtEstoqueAuxiliarData.Multiline = false;
            this.txtEstoqueAuxiliarData.Name = "txtEstoqueAuxiliarData";
            this.txtEstoqueAuxiliarData.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtEstoqueAuxiliarData.PasswordChar = false;
            this.txtEstoqueAuxiliarData.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtEstoqueAuxiliarData.PlaceholderText = "";
            this.txtEstoqueAuxiliarData.ReadOnly = false;
            this.txtEstoqueAuxiliarData.Size = new System.Drawing.Size(149, 37);
            this.txtEstoqueAuxiliarData.TabIndex = 254;
            this.txtEstoqueAuxiliarData.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEstoqueAuxiliarData.Texts = "";
            this.txtEstoqueAuxiliarData.UnderlinedStyle = false;
            // 
            // autoLabel5
            // 
            this.autoLabel5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.Black;
            this.autoLabel5.Location = new System.Drawing.Point(686, 189);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(118, 16);
            this.autoLabel5.TabIndex = 251;
            this.autoLabel5.Text = "Estoque Aux. Atual";
            // 
            // txtEstoqueAuxiliar
            // 
            this.txtEstoqueAuxiliar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEstoqueAuxiliar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEstoqueAuxiliar.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEstoqueAuxiliar.BorderRadius = 8;
            this.txtEstoqueAuxiliar.BorderSize = 2;
            this.txtEstoqueAuxiliar.Enabled = false;
            this.txtEstoqueAuxiliar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstoqueAuxiliar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEstoqueAuxiliar.Location = new System.Drawing.Point(680, 205);
            this.txtEstoqueAuxiliar.Margin = new System.Windows.Forms.Padding(4);
            this.txtEstoqueAuxiliar.Multiline = false;
            this.txtEstoqueAuxiliar.Name = "txtEstoqueAuxiliar";
            this.txtEstoqueAuxiliar.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtEstoqueAuxiliar.PasswordChar = false;
            this.txtEstoqueAuxiliar.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtEstoqueAuxiliar.PlaceholderText = "";
            this.txtEstoqueAuxiliar.ReadOnly = false;
            this.txtEstoqueAuxiliar.Size = new System.Drawing.Size(142, 37);
            this.txtEstoqueAuxiliar.TabIndex = 252;
            this.txtEstoqueAuxiliar.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEstoqueAuxiliar.Texts = "";
            this.txtEstoqueAuxiliar.UnderlinedStyle = false;
            // 
            // lblEstoqueData
            // 
            this.lblEstoqueData.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblEstoqueData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstoqueData.ForeColor = System.Drawing.Color.Black;
            this.lblEstoqueData.Location = new System.Drawing.Point(225, 189);
            this.lblEstoqueData.Name = "lblEstoqueData";
            this.lblEstoqueData.Size = new System.Drawing.Size(118, 16);
            this.lblEstoqueData.TabIndex = 249;
            this.lblEstoqueData.Text = "Est. em 30/08/2022";
            // 
            // txtEstoqueData
            // 
            this.txtEstoqueData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEstoqueData.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEstoqueData.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEstoqueData.BorderRadius = 8;
            this.txtEstoqueData.BorderSize = 2;
            this.txtEstoqueData.Enabled = false;
            this.txtEstoqueData.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstoqueData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEstoqueData.Location = new System.Drawing.Point(219, 205);
            this.txtEstoqueData.Margin = new System.Windows.Forms.Padding(4);
            this.txtEstoqueData.Multiline = false;
            this.txtEstoqueData.Name = "txtEstoqueData";
            this.txtEstoqueData.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtEstoqueData.PasswordChar = false;
            this.txtEstoqueData.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtEstoqueData.PlaceholderText = "";
            this.txtEstoqueData.ReadOnly = false;
            this.txtEstoqueData.Size = new System.Drawing.Size(154, 37);
            this.txtEstoqueData.TabIndex = 250;
            this.txtEstoqueData.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEstoqueData.Texts = "";
            this.txtEstoqueData.UnderlinedStyle = false;
            // 
            // txtDataAjuste
            // 
            this.txtDataAjuste.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataAjuste.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataAjuste.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDataAjuste.Location = new System.Drawing.Point(13, 207);
            this.txtDataAjuste.Name = "txtDataAjuste";
            this.txtDataAjuste.Size = new System.Drawing.Size(155, 35);
            this.txtDataAjuste.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataAjuste.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataAjuste.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataAjuste.TabIndex = 247;
            this.txtDataAjuste.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            this.txtDataAjuste.Leave += new System.EventHandler(this.txtVencimentoInicial_Leave);
            // 
            // autoLabel6
            // 
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(13, 190);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(95, 16);
            this.autoLabel6.TabIndex = 248;
            this.autoLabel6.Text = "Data de Ajuste";
            // 
            // radioNaoConciliado
            // 
            this.radioNaoConciliado.AutoSize = true;
            this.radioNaoConciliado.Checked = true;
            this.radioNaoConciliado.Location = new System.Drawing.Point(135, 257);
            this.radioNaoConciliado.Name = "radioNaoConciliado";
            this.radioNaoConciliado.Size = new System.Drawing.Size(197, 17);
            this.radioNaoConciliado.TabIndex = 246;
            this.radioNaoConciliado.TabStop = true;
            this.radioNaoConciliado.Text = "Contagem Para Simples Conferência";
            this.radioNaoConciliado.UseVisualStyleBackColor = true;
            // 
            // radioConciliado
            // 
            this.radioConciliado.AutoSize = true;
            this.radioConciliado.Location = new System.Drawing.Point(13, 257);
            this.radioConciliado.Name = "radioConciliado";
            this.radioConciliado.Size = new System.Drawing.Size(105, 17);
            this.radioConciliado.TabIndex = 245;
            this.radioConciliado.Text = "Estoque Contábil";
            this.radioConciliado.UseVisualStyleBackColor = true;
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(387, 189);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(90, 16);
            this.autoLabel4.TabIndex = 241;
            this.autoLabel4.Text = "Estoque Atual";
            // 
            // txtEstoqueAtual
            // 
            this.txtEstoqueAtual.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEstoqueAtual.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEstoqueAtual.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtEstoqueAtual.BorderRadius = 8;
            this.txtEstoqueAtual.BorderSize = 2;
            this.txtEstoqueAtual.Enabled = false;
            this.txtEstoqueAtual.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstoqueAtual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEstoqueAtual.Location = new System.Drawing.Point(381, 205);
            this.txtEstoqueAtual.Margin = new System.Windows.Forms.Padding(4);
            this.txtEstoqueAtual.Multiline = false;
            this.txtEstoqueAtual.Name = "txtEstoqueAtual";
            this.txtEstoqueAtual.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtEstoqueAtual.PasswordChar = false;
            this.txtEstoqueAtual.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtEstoqueAtual.PlaceholderText = "";
            this.txtEstoqueAtual.ReadOnly = false;
            this.txtEstoqueAtual.Size = new System.Drawing.Size(134, 37);
            this.txtEstoqueAtual.TabIndex = 244;
            this.txtEstoqueAtual.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEstoqueAtual.Texts = "";
            this.txtEstoqueAtual.UnderlinedStyle = false;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnConfirmar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnConfirmar.IconColor = System.Drawing.Color.Green;
            this.btnConfirmar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnConfirmar.IconSize = 38;
            this.btnConfirmar.Location = new System.Drawing.Point(786, 150);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(36, 34);
            this.btnConfirmar.TabIndex = 239;
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(633, 131);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(77, 16);
            this.autoLabel3.TabIndex = 238;
            this.autoLabel3.Text = "Quantidade";
            // 
            // txtQuantidadeEfetiva
            // 
            this.txtQuantidadeEfetiva.BackColor = System.Drawing.Color.White;
            this.txtQuantidadeEfetiva.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtQuantidadeEfetiva.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtQuantidadeEfetiva.BorderRadius = 8;
            this.txtQuantidadeEfetiva.BorderSize = 2;
            this.txtQuantidadeEfetiva.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidadeEfetiva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtQuantidadeEfetiva.Location = new System.Drawing.Point(625, 147);
            this.txtQuantidadeEfetiva.Margin = new System.Windows.Forms.Padding(4);
            this.txtQuantidadeEfetiva.Multiline = false;
            this.txtQuantidadeEfetiva.Name = "txtQuantidadeEfetiva";
            this.txtQuantidadeEfetiva.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtQuantidadeEfetiva.PasswordChar = false;
            this.txtQuantidadeEfetiva.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtQuantidadeEfetiva.PlaceholderText = "";
            this.txtQuantidadeEfetiva.ReadOnly = false;
            this.txtQuantidadeEfetiva.Size = new System.Drawing.Size(154, 37);
            this.txtQuantidadeEfetiva.TabIndex = 237;
            this.txtQuantidadeEfetiva.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtQuantidadeEfetiva.Texts = "";
            this.txtQuantidadeEfetiva.UnderlinedStyle = false;
            this.txtQuantidadeEfetiva.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantidadeEfetiva_KeyPress);
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(536, 131);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(51, 16);
            this.autoLabel2.TabIndex = 236;
            this.autoLabel2.Text = "Código";
            // 
            // btnPesquisaProduto
            // 
            this.btnPesquisaProduto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaProduto.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaProduto.FlatAppearance.BorderSize = 0;
            this.btnPesquisaProduto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaProduto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaProduto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaProduto.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaProduto.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaProduto.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaProduto.IconSize = 38;
            this.btnPesquisaProduto.Location = new System.Drawing.Point(485, 150);
            this.btnPesquisaProduto.Name = "btnPesquisaProduto";
            this.btnPesquisaProduto.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaProduto.TabIndex = 235;
            this.btnPesquisaProduto.UseVisualStyleBackColor = true;
            this.btnPesquisaProduto.Click += new System.EventHandler(this.btnPesquisaProduto_Click);
            // 
            // txtCodProduto
            // 
            this.txtCodProduto.BackColor = System.Drawing.Color.White;
            this.txtCodProduto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodProduto.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodProduto.BorderRadius = 8;
            this.txtCodProduto.BorderSize = 2;
            this.txtCodProduto.Enabled = false;
            this.txtCodProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodProduto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodProduto.Location = new System.Drawing.Point(528, 147);
            this.txtCodProduto.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodProduto.Multiline = false;
            this.txtCodProduto.Name = "txtCodProduto";
            this.txtCodProduto.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodProduto.PasswordChar = false;
            this.txtCodProduto.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodProduto.PlaceholderText = "";
            this.txtCodProduto.ReadOnly = false;
            this.txtCodProduto.Size = new System.Drawing.Size(89, 37);
            this.txtCodProduto.TabIndex = 234;
            this.txtCodProduto.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodProduto.Texts = "";
            this.txtCodProduto.UnderlinedStyle = false;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(21, 131);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(54, 16);
            this.autoLabel1.TabIndex = 233;
            this.autoLabel1.Text = "Produto";
            // 
            // txtProduto
            // 
            this.txtProduto.BackColor = System.Drawing.Color.White;
            this.txtProduto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtProduto.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtProduto.BorderRadius = 8;
            this.txtProduto.BorderSize = 2;
            this.txtProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProduto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtProduto.Location = new System.Drawing.Point(13, 147);
            this.txtProduto.Margin = new System.Windows.Forms.Padding(4);
            this.txtProduto.Multiline = false;
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtProduto.PasswordChar = false;
            this.txtProduto.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtProduto.PlaceholderText = "";
            this.txtProduto.ReadOnly = false;
            this.txtProduto.Size = new System.Drawing.Size(465, 37);
            this.txtProduto.TabIndex = 232;
            this.txtProduto.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtProduto.Texts = "";
            this.txtProduto.UnderlinedStyle = false;
            this.txtProduto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProduto_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExcluirItem);
            this.groupBox2.Controls.Add(this.btnGravar);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 556);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(873, 70);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnExcluirItem
            // 
            this.btnExcluirItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcluirItem.BackColor = System.Drawing.Color.White;
            this.btnExcluirItem.BackgroundColor = System.Drawing.Color.White;
            this.btnExcluirItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExcluirItem.BorderRadius = 8;
            this.btnExcluirItem.BorderSize = 2;
            this.btnExcluirItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluirItem.FlatAppearance.BorderSize = 0;
            this.btnExcluirItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluirItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluirItem.ForeColor = System.Drawing.Color.Black;
            this.btnExcluirItem.Location = new System.Drawing.Point(463, 19);
            this.btnExcluirItem.Name = "btnExcluirItem";
            this.btnExcluirItem.Size = new System.Drawing.Size(193, 45);
            this.btnExcluirItem.TabIndex = 254;
            this.btnExcluirItem.Text = "Excluir Item";
            this.btnExcluirItem.TextColor = System.Drawing.Color.Black;
            this.btnExcluirItem.UseVisualStyleBackColor = false;
            this.btnExcluirItem.Click += new System.EventHandler(this.btnExcluirItem_Click);
            // 
            // btnGravar
            // 
            this.btnGravar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGravar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnGravar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnGravar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnGravar.BorderRadius = 8;
            this.btnGravar.BorderSize = 0;
            this.btnGravar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGravar.FlatAppearance.BorderSize = 0;
            this.btnGravar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGravar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGravar.ForeColor = System.Drawing.Color.Transparent;
            this.btnGravar.Location = new System.Drawing.Point(662, 19);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(199, 45);
            this.btnGravar.TabIndex = 247;
            this.btnGravar.Text = "Gravar [F5]";
            this.btnGravar.TextColor = System.Drawing.Color.Transparent;
            this.btnGravar.UseVisualStyleBackColor = false;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grid);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 306);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(873, 250);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // grid
            // 
            this.grid.AccessibleName = "Table";
            this.grid.AllowEditing = false;
            this.grid.AllowGrouping = false;
            this.grid.AllowResizingColumns = true;
            this.grid.AllowSorting = false;
            this.grid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowGrouping = false;
            gridTextColumn1.AllowResizing = true;
            gridTextColumn1.AllowSorting = false;
            gridTextColumn1.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn1.CellStyle.Font.Size = 12F;
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn1.HeaderStyle.Font.Size = 12F;
            gridTextColumn1.HeaderText = "Cód Produto";
            gridTextColumn1.MappingName = "IdProduto";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowGrouping = false;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.AllowSorting = false;
            gridTextColumn2.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn2.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn2.HeaderStyle.Font.Size = 12F;
            gridTextColumn2.HeaderText = "Descrição";
            gridTextColumn2.MappingName = "DescricaoProduto";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowGrouping = false;
            gridTextColumn3.AllowResizing = true;
            gridTextColumn3.AllowSorting = false;
            gridTextColumn3.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn3.CellStyle.Font.Size = 12F;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn3.HeaderStyle.Font.Size = 12F;
            gridTextColumn3.HeaderText = "Data Ajuste";
            gridTextColumn3.MappingName = "DataAjuste";
            gridTextColumn3.Visible = false;
            gridCheckBoxColumn1.AllowEditing = false;
            gridCheckBoxColumn1.AllowGrouping = false;
            gridCheckBoxColumn1.AllowResizing = true;
            gridCheckBoxColumn1.AllowSorting = false;
            gridCheckBoxColumn1.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridCheckBoxColumn1.CellStyle.Font.Size = 12F;
            gridCheckBoxColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridCheckBoxColumn1.HeaderStyle.Font.Facename = "Montserrat";
            gridCheckBoxColumn1.HeaderStyle.Font.Size = 12F;
            gridCheckBoxColumn1.HeaderText = "Conciliado";
            gridCheckBoxColumn1.MappingName = "Conciliado";
            gridCheckBoxColumn1.Visible = false;
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowGrouping = false;
            gridTextColumn4.AllowResizing = true;
            gridTextColumn4.AllowSorting = false;
            gridTextColumn4.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn4.CellStyle.Font.Size = 12F;
            gridTextColumn4.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn4.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn4.HeaderStyle.Font.Size = 12F;
            gridTextColumn4.HeaderText = "Estoque Data";
            gridTextColumn4.MappingName = "QuantidadeData";
            gridTextColumn5.AllowEditing = false;
            gridTextColumn5.AllowGrouping = false;
            gridTextColumn5.AllowResizing = true;
            gridTextColumn5.AllowSorting = false;
            gridTextColumn5.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn5.CellStyle.Font.Size = 12F;
            gridTextColumn5.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn5.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn5.HeaderStyle.Font.Size = 12F;
            gridTextColumn5.HeaderText = "Quantidade";
            gridTextColumn5.MappingName = "NovaQuantidade";
            gridTextColumn6.AllowEditing = false;
            gridTextColumn6.AllowGrouping = false;
            gridTextColumn6.AllowResizing = true;
            gridTextColumn6.AllowSorting = false;
            gridTextColumn6.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn6.CellStyle.Font.Size = 12F;
            gridTextColumn6.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn6.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn6.HeaderStyle.Font.Size = 12F;
            gridTextColumn6.HeaderText = "Linha";
            gridTextColumn6.MappingName = "LinhaDataSet";
            gridTextColumn6.Visible = false;
            gridNumericColumn1.AllowEditing = false;
            gridNumericColumn1.AllowGrouping = false;
            gridNumericColumn1.AllowResizing = true;
            gridNumericColumn1.AllowSorting = false;
            gridNumericColumn1.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridNumericColumn1.CellStyle.Font.Size = 12F;
            gridNumericColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn1.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn1.HeaderStyle.Font.Size = 12F;
            gridNumericColumn1.HeaderText = "ID";
            gridNumericColumn1.MappingName = "Id";
            gridNumericColumn1.Visible = false;
            this.grid.Columns.Add(gridTextColumn1);
            this.grid.Columns.Add(gridTextColumn2);
            this.grid.Columns.Add(gridTextColumn3);
            this.grid.Columns.Add(gridCheckBoxColumn1);
            this.grid.Columns.Add(gridTextColumn4);
            this.grid.Columns.Add(gridTextColumn5);
            this.grid.Columns.Add(gridTextColumn6);
            this.grid.Columns.Add(gridNumericColumn1);
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid.Location = new System.Drawing.Point(3, 16);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(867, 231);
            this.grid.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.grid.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.grid.TabIndex = 216;
            this.grid.Text = "sfDataGrid1";
            this.grid.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.grid_QueryRowStyle);
            // 
            // dsBalanco
            // 
            this.dsBalanco.DataSetName = "dsBalanco";
            this.dsBalanco.Tables.AddRange(new System.Data.DataTable[] {
            this.Balanco});
            // 
            // Balanco
            // 
            this.Balanco.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn3,
            this.dataColumn2,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.BalancoEstoqueProduto});
            this.Balanco.TableName = "Balanco";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "IdProduto";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "DescricaoProduto";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "Conciliado";
            this.dataColumn2.DataType = typeof(bool);
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "DataAjuste";
            // 
            // dataColumn5
            // 
            this.dataColumn5.Caption = "QuantidadeData";
            this.dataColumn5.ColumnName = "QuantidadeData";
            this.dataColumn5.DataType = typeof(double);
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "NovaQuantidade";
            this.dataColumn6.DataType = typeof(double);
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "LinhaDataSet";
            // 
            // BalancoEstoqueProduto
            // 
            this.BalancoEstoqueProduto.ColumnName = "Id";
            this.BalancoEstoqueProduto.DataType = typeof(int);
            // 
            // chkZerarEstoque
            // 
            this.chkZerarEstoque.AutoSize = true;
            this.chkZerarEstoque.Location = new System.Drawing.Point(218, 280);
            this.chkZerarEstoque.Name = "chkZerarEstoque";
            this.chkZerarEstoque.Size = new System.Drawing.Size(278, 17);
            this.chkZerarEstoque.TabIndex = 263;
            this.chkZerarEstoque.Text = "Zerar Estoque Não Contabilizado ao Efetivar Balanço";
            this.chkZerarEstoque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkZerarEstoque.UseVisualStyleBackColor = true;
            // 
            // FrmBalancoEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(873, 626);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBalancoEstoque";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Balanço de Estoque";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsBalanco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Balanco)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private FontAwesome.Sharp.IconButton btnPesquisaProduto;
        private RJ_UI.Classes.RJTextBox txtCodProduto;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtProduto;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private RJ_UI.Classes.RJTextBox txtQuantidadeEfetiva;
        private FontAwesome.Sharp.IconButton btnConfirmar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private RJ_UI.Classes.RJTextBox txtEstoqueAtual;
        private System.Windows.Forms.RadioButton radioNaoConciliado;
        private System.Windows.Forms.RadioButton radioConciliado;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataAjuste;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblEstoqueData;
        private RJ_UI.Classes.RJTextBox txtEstoqueData;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private RJ_UI.Classes.RJTextBox txtEstoqueAuxiliar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblEstoqueAuxiliarData;
        private RJ_UI.Classes.RJTextBox txtEstoqueAuxiliarData;
        private FontAwesome.Sharp.IconButton btnAtualizarEstoque;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel7;
        private RJ_UI.Classes.RJTextBox txtDescricao;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel8;
        private RJ_UI.Classes.RJTextBox txtId;
        private System.Windows.Forms.Label label1;
        private System.Data.DataSet dsBalanco;
        private System.Data.DataTable Balanco;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
        private System.Data.DataColumn dataColumn7;
        private RJ_UI.Classes.RJButton btnGravar;
        private System.Windows.Forms.CheckBox chkParaCampoQuantidade;
        private System.Windows.Forms.Panel panelTitleBar;
        private FontAwesome.Sharp.IconButton btnClose;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel9;
        private RJ_UI.Classes.RJButton btnExcluirItem;
        private System.Data.DataColumn BalancoEstoqueProduto;
        private System.Windows.Forms.CheckBox chkZerarEstoque;
    }
}