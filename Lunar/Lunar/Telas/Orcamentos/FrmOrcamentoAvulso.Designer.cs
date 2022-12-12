namespace Lunar.Telas.Orcamentos
{
    partial class FrmOrcamentoAvulso
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
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn4 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel8 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.gridProdutos = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.btnConfirmarProduto = new FontAwesome.Sharp.IconButton();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.dsProduto = new System.Data.DataSet();
            this.Prods = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.autoLabel7 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.lblTotal = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel10 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtObservacoes = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtDDD = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtFone = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCliente = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnRemoverProduto = new Lunar.RJ_UI.Classes.RJButton();
            this.btnImprimir = new Lunar.RJ_UI.Classes.RJButton();
            this.txtDescricao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtQuantidade = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtValorTotal = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtValorUnitario = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel9 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProdutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProduto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Prods)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel2);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(800, 44);
            this.panelTitleBar.TabIndex = 203;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.White;
            this.autoLabel2.Location = new System.Drawing.Point(5, 7);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(117, 25);
            this.autoLabel2.TabIndex = 198;
            this.autoLabel2.Text = "Orçamento";
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
            this.btnClose.Location = new System.Drawing.Point(759, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // autoLabel1
            // 
            this.autoLabel1.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(504, 165);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(73, 16);
            this.autoLabel1.TabIndex = 209;
            this.autoLabel1.Text = "Valor Total";
            // 
            // autoLabel3
            // 
            this.autoLabel3.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(256, 163);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(88, 16);
            this.autoLabel3.TabIndex = 208;
            this.autoLabel3.Text = "Valor Unitário";
            // 
            // autoLabel8
            // 
            this.autoLabel8.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel8.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel8.ForeColor = System.Drawing.Color.Black;
            this.autoLabel8.Location = new System.Drawing.Point(22, 165);
            this.autoLabel8.Name = "autoLabel8";
            this.autoLabel8.Size = new System.Drawing.Size(77, 16);
            this.autoLabel8.TabIndex = 207;
            this.autoLabel8.Text = "Quantidade";
            // 
            // autoLabel4
            // 
            this.autoLabel4.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(22, 106);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(69, 16);
            this.autoLabel4.TabIndex = 211;
            this.autoLabel4.Text = "Descrição";
            // 
            // gridProdutos
            // 
            this.gridProdutos.AccessibleName = "Table";
            this.gridProdutos.AllowEditing = false;
            this.gridProdutos.AllowResizingColumns = true;
            this.gridProdutos.AllowSorting = false;
            this.gridProdutos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridProdutos.AutoGenerateColumns = false;
            this.gridProdutos.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCellsWithLastColumnFill;
            this.gridProdutos.BackColor = System.Drawing.Color.White;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowMultiline = true;
            gridTextColumn1.AllowResizing = true;
            gridTextColumn1.AllowSorting = false;
            gridTextColumn1.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn1.CellStyle.Font.Size = 10F;
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderStyle.Font.Size = 12F;
            gridTextColumn1.HeaderText = "Descrição do Produto";
            gridTextColumn1.MappingName = "Descricao";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.AllowSorting = false;
            gridTextColumn2.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn2.CellStyle.Font.Size = 10F;
            gridTextColumn2.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn2.HeaderStyle.Font.Size = 12F;
            gridTextColumn2.HeaderText = "Quantidade";
            gridTextColumn2.MappingName = "Quantidade";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowResizing = true;
            gridTextColumn3.AllowSorting = false;
            gridTextColumn3.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn3.CellStyle.Font.Size = 10F;
            gridTextColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderStyle.Font.Size = 12F;
            gridTextColumn3.HeaderText = "         Valor Unit.       ";
            gridTextColumn3.MappingName = "ValorUnitario";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowResizing = true;
            gridTextColumn4.AllowSorting = false;
            gridTextColumn4.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn4.CellStyle.Font.Size = 10F;
            gridTextColumn4.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridTextColumn4.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn4.HeaderStyle.Font.Size = 12F;
            gridTextColumn4.HeaderText = "          Valor Total      ";
            gridTextColumn4.MappingName = "ValorTotal";
            this.gridProdutos.Columns.Add(gridTextColumn1);
            this.gridProdutos.Columns.Add(gridTextColumn2);
            this.gridProdutos.Columns.Add(gridTextColumn3);
            this.gridProdutos.Columns.Add(gridTextColumn4);
            this.gridProdutos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridProdutos.Location = new System.Drawing.Point(13, 221);
            this.gridProdutos.Name = "gridProdutos";
            this.gridProdutos.Size = new System.Drawing.Size(774, 212);
            this.gridProdutos.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.gridProdutos.Style.CellStyle.Font.Facename = "Montserrat";
            this.gridProdutos.Style.CellStyle.Font.Size = 14F;
            this.gridProdutos.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.White;
            this.gridProdutos.Style.HeaderStyle.HoverBackColor = System.Drawing.Color.White;
            this.gridProdutos.TabIndex = 212;
            this.gridProdutos.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.gridProdutos_QueryRowStyle);
            this.gridProdutos.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.gridProdutos_CellDoubleClick);
            // 
            // btnConfirmarProduto
            // 
            this.btnConfirmarProduto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmarProduto.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnConfirmarProduto.FlatAppearance.BorderSize = 0;
            this.btnConfirmarProduto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnConfirmarProduto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnConfirmarProduto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarProduto.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnConfirmarProduto.IconColor = System.Drawing.Color.DarkSeaGreen;
            this.btnConfirmarProduto.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnConfirmarProduto.IconSize = 38;
            this.btnConfirmarProduto.Location = new System.Drawing.Point(753, 180);
            this.btnConfirmarProduto.Name = "btnConfirmarProduto";
            this.btnConfirmarProduto.Size = new System.Drawing.Size(34, 34);
            this.btnConfirmarProduto.TabIndex = 7;
            this.btnConfirmarProduto.UseVisualStyleBackColor = true;
            this.btnConfirmarProduto.Click += new System.EventHandler(this.btnConfirmarProduto_Click);
            // 
            // autoLabel5
            // 
            this.autoLabel5.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel5.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.Black;
            this.autoLabel5.Location = new System.Drawing.Point(22, 50);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(48, 16);
            this.autoLabel5.TabIndex = 277;
            this.autoLabel5.Text = "Cliente";
            // 
            // autoLabel6
            // 
            this.autoLabel6.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(591, 50);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(127, 16);
            this.autoLabel6.TabIndex = 279;
            this.autoLabel6.Text = "Telefone/Whatsapp";
            // 
            // dsProduto
            // 
            this.dsProduto.DataSetName = "dsProduto";
            this.dsProduto.Tables.AddRange(new System.Data.DataTable[] {
            this.Prods});
            // 
            // Prods
            // 
            this.Prods.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4});
            this.Prods.TableName = "Prods";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "Descricao";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "Quantidade";
            this.dataColumn2.DataType = typeof(double);
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "ValorUnitario";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "ValorTotal";
            // 
            // autoLabel7
            // 
            this.autoLabel7.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel7.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel7.ForeColor = System.Drawing.Color.Black;
            this.autoLabel7.Location = new System.Drawing.Point(468, 50);
            this.autoLabel7.Name = "autoLabel7";
            this.autoLabel7.Size = new System.Drawing.Size(37, 16);
            this.autoLabel7.TabIndex = 283;
            this.autoLabel7.Text = "DDD";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = false;
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Black;
            this.lblTotal.Location = new System.Drawing.Point(583, 436);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(205, 25);
            this.lblTotal.TabIndex = 284;
            this.lblTotal.Text = "R$ 0,00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // autoLabel10
            // 
            this.autoLabel10.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel10.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel10.ForeColor = System.Drawing.Color.Black;
            this.autoLabel10.Location = new System.Drawing.Point(13, 445);
            this.autoLabel10.Name = "autoLabel10";
            this.autoLabel10.Size = new System.Drawing.Size(289, 16);
            this.autoLabel10.TabIndex = 286;
            this.autoLabel10.Text = "Observações/Forma de Pagamento (Opcional)";
            // 
            // txtObservacoes
            // 
            this.txtObservacoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservacoes.BackColor = System.Drawing.Color.White;
            this.txtObservacoes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtObservacoes.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtObservacoes.BorderRadius = 8;
            this.txtObservacoes.BorderSize = 2;
            this.txtObservacoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservacoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtObservacoes.Location = new System.Drawing.Point(13, 465);
            this.txtObservacoes.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservacoes.Multiline = true;
            this.txtObservacoes.Name = "txtObservacoes";
            this.txtObservacoes.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtObservacoes.PasswordChar = false;
            this.txtObservacoes.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtObservacoes.PlaceholderText = "";
            this.txtObservacoes.ReadOnly = false;
            this.txtObservacoes.Size = new System.Drawing.Size(774, 69);
            this.txtObservacoes.TabIndex = 8;
            this.txtObservacoes.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtObservacoes.Texts = "";
            this.txtObservacoes.UnderlinedStyle = false;
            // 
            // txtDDD
            // 
            this.txtDDD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDDD.BackColor = System.Drawing.Color.White;
            this.txtDDD.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDDD.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDDD.BorderRadius = 8;
            this.txtDDD.BorderSize = 2;
            this.txtDDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDDD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDDD.Location = new System.Drawing.Point(461, 62);
            this.txtDDD.Margin = new System.Windows.Forms.Padding(4);
            this.txtDDD.Multiline = false;
            this.txtDDD.Name = "txtDDD";
            this.txtDDD.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDDD.PasswordChar = false;
            this.txtDDD.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDDD.PlaceholderText = "";
            this.txtDDD.ReadOnly = false;
            this.txtDDD.Size = new System.Drawing.Size(113, 37);
            this.txtDDD.TabIndex = 1;
            this.txtDDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDDD.Texts = "";
            this.txtDDD.UnderlinedStyle = false;
            // 
            // txtFone
            // 
            this.txtFone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFone.BackColor = System.Drawing.Color.White;
            this.txtFone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtFone.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtFone.BorderRadius = 8;
            this.txtFone.BorderSize = 2;
            this.txtFone.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtFone.Location = new System.Drawing.Point(582, 62);
            this.txtFone.Margin = new System.Windows.Forms.Padding(4);
            this.txtFone.Multiline = false;
            this.txtFone.Name = "txtFone";
            this.txtFone.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtFone.PasswordChar = false;
            this.txtFone.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtFone.PlaceholderText = "";
            this.txtFone.ReadOnly = false;
            this.txtFone.Size = new System.Drawing.Size(205, 37);
            this.txtFone.TabIndex = 2;
            this.txtFone.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFone.Texts = "";
            this.txtFone.UnderlinedStyle = false;
            this.txtFone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFone_KeyPress);
            this.txtFone.Leave += new System.EventHandler(this.txtFone_Leave);
            // 
            // txtCliente
            // 
            this.txtCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCliente.BackColor = System.Drawing.Color.White;
            this.txtCliente.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCliente.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCliente.BorderRadius = 8;
            this.txtCliente.BorderSize = 2;
            this.txtCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCliente.Location = new System.Drawing.Point(13, 62);
            this.txtCliente.Margin = new System.Windows.Forms.Padding(4);
            this.txtCliente.Multiline = false;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCliente.PasswordChar = false;
            this.txtCliente.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCliente.PlaceholderText = "";
            this.txtCliente.ReadOnly = false;
            this.txtCliente.Size = new System.Drawing.Size(440, 37);
            this.txtCliente.TabIndex = 0;
            this.txtCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCliente.Texts = "";
            this.txtCliente.UnderlinedStyle = false;
            // 
            // btnRemoverProduto
            // 
            this.btnRemoverProduto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoverProduto.BackColor = System.Drawing.Color.White;
            this.btnRemoverProduto.BackgroundColor = System.Drawing.Color.White;
            this.btnRemoverProduto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnRemoverProduto.BorderRadius = 8;
            this.btnRemoverProduto.BorderSize = 2;
            this.btnRemoverProduto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoverProduto.FlatAppearance.BorderSize = 0;
            this.btnRemoverProduto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoverProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoverProduto.ForeColor = System.Drawing.Color.Black;
            this.btnRemoverProduto.Location = new System.Drawing.Point(417, 560);
            this.btnRemoverProduto.Name = "btnRemoverProduto";
            this.btnRemoverProduto.Size = new System.Drawing.Size(182, 43);
            this.btnRemoverProduto.TabIndex = 10;
            this.btnRemoverProduto.Text = "Remover Produto";
            this.btnRemoverProduto.TextColor = System.Drawing.Color.Black;
            this.btnRemoverProduto.UseVisualStyleBackColor = false;
            this.btnRemoverProduto.Click += new System.EventHandler(this.btnRemoverProduto_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnImprimir.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnImprimir.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnImprimir.BorderRadius = 8;
            this.btnImprimir.BorderSize = 0;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Location = new System.Drawing.Point(605, 560);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(182, 43);
            this.btnImprimir.TabIndex = 9;
            this.btnImprimir.Text = "Imprimir [F8]";
            this.btnImprimir.TextColor = System.Drawing.Color.White;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // txtDescricao
            // 
            this.txtDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescricao.BackColor = System.Drawing.Color.White;
            this.txtDescricao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricao.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricao.BorderRadius = 8;
            this.txtDescricao.BorderSize = 2;
            this.txtDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDescricao.Location = new System.Drawing.Point(13, 118);
            this.txtDescricao.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricao.Multiline = false;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescricao.PasswordChar = false;
            this.txtDescricao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDescricao.PlaceholderText = "";
            this.txtDescricao.ReadOnly = false;
            this.txtDescricao.Size = new System.Drawing.Size(774, 37);
            this.txtDescricao.TabIndex = 3;
            this.txtDescricao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescricao.Texts = "";
            this.txtDescricao.UnderlinedStyle = false;
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuantidade.BackColor = System.Drawing.Color.White;
            this.txtQuantidade.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtQuantidade.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtQuantidade.BorderRadius = 8;
            this.txtQuantidade.BorderSize = 2;
            this.txtQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidade.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtQuantidade.Location = new System.Drawing.Point(13, 177);
            this.txtQuantidade.Margin = new System.Windows.Forms.Padding(4);
            this.txtQuantidade.Multiline = false;
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtQuantidade.PasswordChar = false;
            this.txtQuantidade.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtQuantidade.PlaceholderText = "";
            this.txtQuantidade.ReadOnly = false;
            this.txtQuantidade.Size = new System.Drawing.Size(226, 37);
            this.txtQuantidade.TabIndex = 4;
            this.txtQuantidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtQuantidade.Texts = "1";
            this.txtQuantidade.UnderlinedStyle = false;
            this.txtQuantidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantidade_KeyPress);
            this.txtQuantidade.Leave += new System.EventHandler(this.txtQuantidade_Leave);
            // 
            // txtValorTotal
            // 
            this.txtValorTotal.BackColor = System.Drawing.Color.White;
            this.txtValorTotal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValorTotal.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValorTotal.BorderRadius = 8;
            this.txtValorTotal.BorderSize = 2;
            this.txtValorTotal.Enabled = false;
            this.txtValorTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtValorTotal.Location = new System.Drawing.Point(495, 177);
            this.txtValorTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtValorTotal.Multiline = false;
            this.txtValorTotal.Name = "txtValorTotal";
            this.txtValorTotal.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValorTotal.PasswordChar = false;
            this.txtValorTotal.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtValorTotal.PlaceholderText = "";
            this.txtValorTotal.ReadOnly = false;
            this.txtValorTotal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtValorTotal.Size = new System.Drawing.Size(251, 37);
            this.txtValorTotal.TabIndex = 6;
            this.txtValorTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValorTotal.Texts = "0,00";
            this.txtValorTotal.UnderlinedStyle = false;
            // 
            // txtValorUnitario
            // 
            this.txtValorUnitario.BackColor = System.Drawing.Color.White;
            this.txtValorUnitario.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValorUnitario.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValorUnitario.BorderRadius = 8;
            this.txtValorUnitario.BorderSize = 2;
            this.txtValorUnitario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorUnitario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtValorUnitario.Location = new System.Drawing.Point(247, 177);
            this.txtValorUnitario.Margin = new System.Windows.Forms.Padding(4);
            this.txtValorUnitario.Multiline = false;
            this.txtValorUnitario.Name = "txtValorUnitario";
            this.txtValorUnitario.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValorUnitario.PasswordChar = false;
            this.txtValorUnitario.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtValorUnitario.PlaceholderText = "";
            this.txtValorUnitario.ReadOnly = false;
            this.txtValorUnitario.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtValorUnitario.Size = new System.Drawing.Size(240, 37);
            this.txtValorUnitario.TabIndex = 5;
            this.txtValorUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValorUnitario.Texts = "0,00";
            this.txtValorUnitario.UnderlinedStyle = false;
            this.txtValorUnitario._TextChanged += new System.EventHandler(this.txtValorUnitario__TextChanged);
            this.txtValorUnitario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValorUnitario_KeyPress);
            this.txtValorUnitario.Leave += new System.EventHandler(this.txtValorUnitario_Leave);
            // 
            // autoLabel9
            // 
            this.autoLabel9.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel9.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel9.ForeColor = System.Drawing.Color.Red;
            this.autoLabel9.Location = new System.Drawing.Point(22, 541);
            this.autoLabel9.Name = "autoLabel9";
            this.autoLabel9.Size = new System.Drawing.Size(498, 16);
            this.autoLabel9.TabIndex = 287;
            this.autoLabel9.Text = "Atenção: esse orçamento é para simples impressão, não gera controle no sistema";
            // 
            // FrmOrcamentoAvulso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 611);
            this.Controls.Add(this.autoLabel9);
            this.Controls.Add(this.autoLabel10);
            this.Controls.Add(this.txtObservacoes);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.autoLabel7);
            this.Controls.Add(this.txtDDD);
            this.Controls.Add(this.autoLabel6);
            this.Controls.Add(this.txtFone);
            this.Controls.Add(this.autoLabel5);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.btnRemoverProduto);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnConfirmarProduto);
            this.Controls.Add(this.gridProdutos);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.autoLabel8);
            this.Controls.Add(this.txtQuantidade);
            this.Controls.Add(this.txtValorTotal);
            this.Controls.Add(this.txtValorUnitario);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmOrcamentoAvulso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Orçamento";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmOrcamentoAvulso_KeyDown);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProdutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProduto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Prods)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private FontAwesome.Sharp.IconButton btnClose;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel8;
        private RJ_UI.Classes.RJTextBox txtQuantidade;
        private RJ_UI.Classes.RJTextBox txtValorTotal;
        private RJ_UI.Classes.RJTextBox txtValorUnitario;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private RJ_UI.Classes.RJTextBox txtDescricao;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridProdutos;
        private FontAwesome.Sharp.IconButton btnConfirmarProduto;
        private RJ_UI.Classes.RJButton btnRemoverProduto;
        private RJ_UI.Classes.RJButton btnImprimir;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private RJ_UI.Classes.RJTextBox txtCliente;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private RJ_UI.Classes.RJTextBox txtFone;
        private System.Data.DataSet dsProduto;
        private System.Data.DataTable Prods;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel7;
        private RJ_UI.Classes.RJTextBox txtDDD;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblTotal;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel10;
        private RJ_UI.Classes.RJTextBox txtObservacoes;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel9;
    }
}