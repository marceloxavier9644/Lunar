namespace Lunar.Telas.Estoques
{
    partial class FrmEtiquetas
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
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn1 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn2 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEtiquetas));
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel9 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.tabControlAdv1 = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            this.tabPageAdv1 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.btnConfirmaItem = new FontAwesome.Sharp.IconButton();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.autoLabel15 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaProduto = new FontAwesome.Sharp.IconButton();
            this.autoLabel16 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtQuantidade = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtNotaCompra = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodProduto = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtProduto = new Lunar.RJ_UI.Classes.RJTextBox();
            this.dsProdutos = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.btnExcluirProduto = new FontAwesome.Sharp.IconButton();
            this.btnLimpar = new FontAwesome.Sharp.IconButton();
            this.comboModeloEtiqueta = new System.Windows.Forms.ComboBox();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.comboImpressoras = new System.Windows.Forms.ComboBox();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.chkPreview = new System.Windows.Forms.CheckBox();
            this.txtObservacoes = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtParcelas = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnImprimir = new Lunar.RJ_UI.Classes.RJButton();
            this.btnSalvarConfig = new FontAwesome.Sharp.IconButton();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).BeginInit();
            this.tabControlAdv1.SuspendLayout();
            this.tabPageAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProdutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel9);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(850, 44);
            this.panelTitleBar.TabIndex = 263;
            // 
            // autoLabel9
            // 
            this.autoLabel9.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel9.ForeColor = System.Drawing.Color.White;
            this.autoLabel9.Location = new System.Drawing.Point(9, 9);
            this.autoLabel9.Name = "autoLabel9";
            this.autoLabel9.Size = new System.Drawing.Size(183, 25);
            this.autoLabel9.TabIndex = 200;
            this.autoLabel9.Text = "Imprimir Etiquetas";
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
            this.btnClose.Location = new System.Drawing.Point(809, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabControlAdv1
            // 
            this.tabControlAdv1.ActiveTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tabControlAdv1.BeforeTouchSize = new System.Drawing.Size(838, 335);
            this.tabControlAdv1.Controls.Add(this.tabPageAdv1);
            this.tabControlAdv1.FocusOnTabClick = false;
            this.tabControlAdv1.Location = new System.Drawing.Point(0, 49);
            this.tabControlAdv1.Name = "tabControlAdv1";
            this.tabControlAdv1.Size = new System.Drawing.Size(838, 335);
            this.tabControlAdv1.TabIndex = 264;
            this.tabControlAdv1.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererOffice2016White);
            this.tabControlAdv1.ThemeName = "TabRendererOffice2016White";
            this.tabControlAdv1.ThemeStyle.PrimitiveButtonStyle.DisabledNextPageImage = null;
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageAdv1.Controls.Add(this.btnConfirmaItem);
            this.tabPageAdv1.Controls.Add(this.autoLabel2);
            this.tabPageAdv1.Controls.Add(this.autoLabel1);
            this.tabPageAdv1.Controls.Add(this.grid);
            this.tabPageAdv1.Controls.Add(this.autoLabel15);
            this.tabPageAdv1.Controls.Add(this.btnPesquisaProduto);
            this.tabPageAdv1.Controls.Add(this.autoLabel16);
            this.tabPageAdv1.Controls.Add(this.txtQuantidade);
            this.tabPageAdv1.Controls.Add(this.txtNotaCompra);
            this.tabPageAdv1.Controls.Add(this.txtCodProduto);
            this.tabPageAdv1.Controls.Add(this.txtProduto);
            this.tabPageAdv1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageAdv1.Image = null;
            this.tabPageAdv1.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPageAdv1.Location = new System.Drawing.Point(1, 29);
            this.tabPageAdv1.Name = "tabPageAdv1";
            this.tabPageAdv1.ShowCloseButton = true;
            this.tabPageAdv1.Size = new System.Drawing.Size(835, 304);
            this.tabPageAdv1.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageAdv1.TabIndex = 1;
            this.tabPageAdv1.Text = "Etiquetas Avulsas";
            this.tabPageAdv1.ThemesEnabled = false;
            // 
            // btnConfirmaItem
            // 
            this.btnConfirmaItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmaItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmaItem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnConfirmaItem.FlatAppearance.BorderSize = 0;
            this.btnConfirmaItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnConfirmaItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnConfirmaItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmaItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmaItem.IconChar = FontAwesome.Sharp.IconChar.CircleCheck;
            this.btnConfirmaItem.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnConfirmaItem.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnConfirmaItem.IconSize = 38;
            this.btnConfirmaItem.Location = new System.Drawing.Point(778, 34);
            this.btnConfirmaItem.Name = "btnConfirmaItem";
            this.btnConfirmaItem.Size = new System.Drawing.Size(46, 38);
            this.btnConfirmaItem.TabIndex = 308;
            this.btnConfirmaItem.UseVisualStyleBackColor = true;
            this.btnConfirmaItem.Click += new System.EventHandler(this.btnConfirmaItem_Click);
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(478, 21);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(77, 16);
            this.autoLabel2.TabIndex = 307;
            this.autoLabel2.Text = "Quantidade";
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(591, 21);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(106, 16);
            this.autoLabel1.TabIndex = 305;
            this.autoLabel1.Text = "Nota de Compra";
            // 
            // grid
            // 
            this.grid.AccessibleName = "Table";
            this.grid.AllowFiltering = true;
            this.grid.AllowResizingColumns = true;
            this.grid.AllowResizingHiddenColumns = true;
            this.grid.AutoGenerateColumns = false;
            this.grid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowFiltering = true;
            gridTextColumn1.AllowResizing = true;
            gridTextColumn1.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn1.CellStyle.Font.Size = 12F;
            gridTextColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn1.HeaderStyle.Font.Size = 10F;
            gridTextColumn1.HeaderText = "Código Produto";
            gridTextColumn1.MappingName = "Id";
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.AllowSorting = false;
            gridTextColumn2.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn2.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn2.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn2.HeaderStyle.Font.Size = 10F;
            gridTextColumn2.HeaderText = "Descrição";
            gridTextColumn2.MappingName = "Descricao";
            gridNumericColumn1.AllowFiltering = true;
            gridNumericColumn1.AllowResizing = true;
            gridNumericColumn1.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridNumericColumn1.CellStyle.Font.Size = 12F;
            gridNumericColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridNumericColumn1.Format = "N2";
            gridNumericColumn1.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn1.HeaderStyle.Font.Size = 10F;
            gridNumericColumn1.HeaderText = "Valor Venda";
            gridNumericColumn1.MappingName = "Valor";
            gridNumericColumn2.AllowFiltering = true;
            gridNumericColumn2.AllowResizing = true;
            gridNumericColumn2.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridNumericColumn2.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridNumericColumn2.CellStyle.Font.Size = 12F;
            gridNumericColumn2.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            gridNumericColumn2.Format = "0";
            gridNumericColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn2.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn2.HeaderStyle.Font.Size = 10F;
            gridNumericColumn2.HeaderText = "Quantidade";
            gridNumericColumn2.MappingName = "Quantidade";
            this.grid.Columns.Add(gridTextColumn1);
            this.grid.Columns.Add(gridTextColumn2);
            this.grid.Columns.Add(gridNumericColumn1);
            this.grid.Columns.Add(gridNumericColumn2);
            this.grid.Location = new System.Drawing.Point(12, 79);
            this.grid.Name = "grid";
            this.grid.SelectionMode = Syncfusion.WinForms.DataGrid.Enums.GridSelectionMode.Extended;
            this.grid.Size = new System.Drawing.Size(782, 222);
            this.grid.Style.CellStyle.Font.Size = 10F;
            this.grid.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.grid.Style.HeaderStyle.Font.Size = 12F;
            this.grid.TabIndex = 303;
            this.grid.Text = "Grid Parcelas";
            this.grid.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.grid_QueryRowStyle);
            // 
            // autoLabel15
            // 
            this.autoLabel15.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel15.ForeColor = System.Drawing.Color.Black;
            this.autoLabel15.Location = new System.Drawing.Point(354, 21);
            this.autoLabel15.Name = "autoLabel15";
            this.autoLabel15.Size = new System.Drawing.Size(51, 16);
            this.autoLabel15.TabIndex = 302;
            this.autoLabel15.Text = "Código";
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
            this.btnPesquisaProduto.Location = new System.Drawing.Point(307, 39);
            this.btnPesquisaProduto.Name = "btnPesquisaProduto";
            this.btnPesquisaProduto.Size = new System.Drawing.Size(40, 34);
            this.btnPesquisaProduto.TabIndex = 1;
            this.btnPesquisaProduto.UseVisualStyleBackColor = true;
            this.btnPesquisaProduto.Click += new System.EventHandler(this.btnPesquisaProduto_Click);
            // 
            // autoLabel16
            // 
            this.autoLabel16.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel16.ForeColor = System.Drawing.Color.Black;
            this.autoLabel16.Location = new System.Drawing.Point(12, 21);
            this.autoLabel16.Name = "autoLabel16";
            this.autoLabel16.Size = new System.Drawing.Size(54, 16);
            this.autoLabel16.TabIndex = 301;
            this.autoLabel16.Text = "Produto";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.BackColor = System.Drawing.Color.White;
            this.txtQuantidade.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtQuantidade.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtQuantidade.BorderRadius = 8;
            this.txtQuantidade.BorderSize = 2;
            this.txtQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidade.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtQuantidade.Location = new System.Drawing.Point(478, 35);
            this.txtQuantidade.Margin = new System.Windows.Forms.Padding(4);
            this.txtQuantidade.Multiline = false;
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtQuantidade.PasswordChar = false;
            this.txtQuantidade.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtQuantidade.PlaceholderText = "";
            this.txtQuantidade.ReadOnly = false;
            this.txtQuantidade.Size = new System.Drawing.Size(105, 37);
            this.txtQuantidade.TabIndex = 306;
            this.txtQuantidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtQuantidade.Texts = "";
            this.txtQuantidade.UnderlinedStyle = false;
            this.txtQuantidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantidade_KeyPress);
            // 
            // txtNotaCompra
            // 
            this.txtNotaCompra.BackColor = System.Drawing.Color.White;
            this.txtNotaCompra.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNotaCompra.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNotaCompra.BorderRadius = 8;
            this.txtNotaCompra.BorderSize = 2;
            this.txtNotaCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotaCompra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNotaCompra.Location = new System.Drawing.Point(591, 35);
            this.txtNotaCompra.Margin = new System.Windows.Forms.Padding(4);
            this.txtNotaCompra.Multiline = false;
            this.txtNotaCompra.Name = "txtNotaCompra";
            this.txtNotaCompra.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtNotaCompra.PasswordChar = false;
            this.txtNotaCompra.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtNotaCompra.PlaceholderText = "";
            this.txtNotaCompra.ReadOnly = false;
            this.txtNotaCompra.Size = new System.Drawing.Size(142, 37);
            this.txtNotaCompra.TabIndex = 304;
            this.txtNotaCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNotaCompra.Texts = "";
            this.txtNotaCompra.UnderlinedStyle = false;
            // 
            // txtCodProduto
            // 
            this.txtCodProduto.BackColor = System.Drawing.Color.White;
            this.txtCodProduto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodProduto.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodProduto.BorderRadius = 8;
            this.txtCodProduto.BorderSize = 2;
            this.txtCodProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodProduto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodProduto.Location = new System.Drawing.Point(354, 35);
            this.txtCodProduto.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodProduto.Multiline = false;
            this.txtCodProduto.Name = "txtCodProduto";
            this.txtCodProduto.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodProduto.PasswordChar = false;
            this.txtCodProduto.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodProduto.PlaceholderText = "";
            this.txtCodProduto.ReadOnly = false;
            this.txtCodProduto.Size = new System.Drawing.Size(116, 37);
            this.txtCodProduto.TabIndex = 2;
            this.txtCodProduto.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodProduto.Texts = "";
            this.txtCodProduto.UnderlinedStyle = false;
            this.txtCodProduto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodProduto_KeyPress);
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
            this.txtProduto.Location = new System.Drawing.Point(12, 35);
            this.txtProduto.Margin = new System.Windows.Forms.Padding(4);
            this.txtProduto.Multiline = false;
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtProduto.PasswordChar = false;
            this.txtProduto.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtProduto.PlaceholderText = "";
            this.txtProduto.ReadOnly = false;
            this.txtProduto.Size = new System.Drawing.Size(288, 37);
            this.txtProduto.TabIndex = 0;
            this.txtProduto.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtProduto.Texts = "";
            this.txtProduto.UnderlinedStyle = false;
            this.txtProduto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProduto_KeyPress);
            // 
            // dsProdutos
            // 
            this.dsProdutos.DataSetName = "dsProdutos";
            this.dsProdutos.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn7});
            this.dataTable1.TableName = "Produto";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "Id";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "Descricao";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "Quantidade";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "Valor";
            // 
            // btnExcluirProduto
            // 
            this.btnExcluirProduto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcluirProduto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluirProduto.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExcluirProduto.FlatAppearance.BorderSize = 0;
            this.btnExcluirProduto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExcluirProduto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnExcluirProduto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluirProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluirProduto.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this.btnExcluirProduto.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnExcluirProduto.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnExcluirProduto.IconSize = 30;
            this.btnExcluirProduto.Location = new System.Drawing.Point(626, 469);
            this.btnExcluirProduto.Name = "btnExcluirProduto";
            this.btnExcluirProduto.Size = new System.Drawing.Size(34, 36);
            this.btnExcluirProduto.TabIndex = 265;
            this.btnExcluirProduto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcluirProduto.UseVisualStyleBackColor = true;
            this.btnExcluirProduto.Click += new System.EventHandler(this.btnExcluirProduto_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = System.Drawing.SystemColors.Control;
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLimpar.FlatAppearance.BorderSize = 0;
            this.btnLimpar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnLimpar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnLimpar.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            this.btnLimpar.IconColor = System.Drawing.Color.DarkOliveGreen;
            this.btnLimpar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnLimpar.IconSize = 40;
            this.btnLimpar.Location = new System.Drawing.Point(576, 469);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(44, 36);
            this.btnLimpar.TabIndex = 309;
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // comboModeloEtiqueta
            // 
            this.comboModeloEtiqueta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboModeloEtiqueta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboModeloEtiqueta.FormattingEnabled = true;
            this.comboModeloEtiqueta.Location = new System.Drawing.Point(13, 467);
            this.comboModeloEtiqueta.Name = "comboModeloEtiqueta";
            this.comboModeloEtiqueta.Size = new System.Drawing.Size(270, 28);
            this.comboModeloEtiqueta.TabIndex = 309;
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(13, 447);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(124, 16);
            this.autoLabel3.TabIndex = 310;
            this.autoLabel3.Text = "Modelo da Etiqueta";
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(290, 447);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(61, 16);
            this.autoLabel4.TabIndex = 312;
            this.autoLabel4.Text = "Parcelas";
            // 
            // autoLabel5
            // 
            this.autoLabel5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.Black;
            this.autoLabel5.Location = new System.Drawing.Point(13, 396);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(75, 16);
            this.autoLabel5.TabIndex = 314;
            this.autoLabel5.Text = "Impressora";
            // 
            // comboImpressoras
            // 
            this.comboImpressoras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboImpressoras.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboImpressoras.FormattingEnabled = true;
            this.comboImpressoras.Location = new System.Drawing.Point(13, 416);
            this.comboImpressoras.Name = "comboImpressoras";
            this.comboImpressoras.Size = new System.Drawing.Size(270, 28);
            this.comboImpressoras.TabIndex = 313;
            // 
            // autoLabel6
            // 
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(290, 396);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(89, 16);
            this.autoLabel6.TabIndex = 316;
            this.autoLabel6.Text = "Observações";
            // 
            // chkPreview
            // 
            this.chkPreview.AutoSize = true;
            this.chkPreview.Location = new System.Drawing.Point(402, 474);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(87, 17);
            this.chkPreview.TabIndex = 317;
            this.chkPreview.Text = "Preview/Edit";
            this.chkPreview.UseVisualStyleBackColor = true;
            this.chkPreview.CheckedChanged += new System.EventHandler(this.chkPreview_CheckedChanged);
            // 
            // txtObservacoes
            // 
            this.txtObservacoes.BackColor = System.Drawing.Color.White;
            this.txtObservacoes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtObservacoes.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtObservacoes.BorderRadius = 8;
            this.txtObservacoes.BorderSize = 2;
            this.txtObservacoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservacoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtObservacoes.Location = new System.Drawing.Point(290, 412);
            this.txtObservacoes.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservacoes.Multiline = false;
            this.txtObservacoes.Name = "txtObservacoes";
            this.txtObservacoes.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtObservacoes.PasswordChar = false;
            this.txtObservacoes.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtObservacoes.PlaceholderText = "";
            this.txtObservacoes.ReadOnly = false;
            this.txtObservacoes.Size = new System.Drawing.Size(548, 33);
            this.txtObservacoes.TabIndex = 315;
            this.txtObservacoes.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtObservacoes.Texts = "";
            this.txtObservacoes.UnderlinedStyle = false;
            // 
            // txtParcelas
            // 
            this.txtParcelas.BackColor = System.Drawing.Color.White;
            this.txtParcelas.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtParcelas.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtParcelas.BorderRadius = 8;
            this.txtParcelas.BorderSize = 2;
            this.txtParcelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParcelas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtParcelas.Location = new System.Drawing.Point(290, 464);
            this.txtParcelas.Margin = new System.Windows.Forms.Padding(4);
            this.txtParcelas.Multiline = false;
            this.txtParcelas.Name = "txtParcelas";
            this.txtParcelas.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtParcelas.PasswordChar = false;
            this.txtParcelas.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtParcelas.PlaceholderText = "";
            this.txtParcelas.ReadOnly = false;
            this.txtParcelas.Size = new System.Drawing.Size(105, 33);
            this.txtParcelas.TabIndex = 311;
            this.txtParcelas.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtParcelas.Texts = "1";
            this.txtParcelas.UnderlinedStyle = false;
            this.txtParcelas._TextChanged += new System.EventHandler(this.txtParcelas__TextChanged);
            this.txtParcelas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtParcelas_KeyPress);
            // 
            // btnImprimir
            // 
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
            this.btnImprimir.Location = new System.Drawing.Point(666, 464);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(172, 45);
            this.btnImprimir.TabIndex = 13;
            this.btnImprimir.Text = "Imprimir [F5]";
            this.btnImprimir.TextColor = System.Drawing.Color.White;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnSalvarConfig
            // 
            this.btnSalvarConfig.BackColor = System.Drawing.SystemColors.Control;
            this.btnSalvarConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvarConfig.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSalvarConfig.FlatAppearance.BorderSize = 0;
            this.btnSalvarConfig.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSalvarConfig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSalvarConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvarConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvarConfig.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnSalvarConfig.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnSalvarConfig.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalvarConfig.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnSalvarConfig.IconSize = 40;
            this.btnSalvarConfig.Location = new System.Drawing.Point(526, 469);
            this.btnSalvarConfig.Name = "btnSalvarConfig";
            this.btnSalvarConfig.Size = new System.Drawing.Size(44, 36);
            this.btnSalvarConfig.TabIndex = 318;
            this.btnSalvarConfig.UseVisualStyleBackColor = false;
            this.btnSalvarConfig.Click += new System.EventHandler(this.btnSalvarConfig_Click);
            // 
            // FrmEtiquetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 511);
            this.Controls.Add(this.btnSalvarConfig);
            this.Controls.Add(this.chkPreview);
            this.Controls.Add(this.autoLabel6);
            this.Controls.Add(this.txtObservacoes);
            this.Controls.Add(this.autoLabel5);
            this.Controls.Add(this.comboImpressoras);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.txtParcelas);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.comboModeloEtiqueta);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnExcluirProduto);
            this.Controls.Add(this.tabControlAdv1);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.btnImprimir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmEtiquetas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impressão de Etiquetas";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).EndInit();
            this.tabControlAdv1.ResumeLayout(false);
            this.tabPageAdv1.ResumeLayout(false);
            this.tabPageAdv1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProdutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel9;
        private FontAwesome.Sharp.IconButton btnClose;
        private Syncfusion.Windows.Forms.Tools.TabControlAdv tabControlAdv1;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageAdv1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel15;
        private FontAwesome.Sharp.IconButton btnPesquisaProduto;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel16;
        private RJ_UI.Classes.RJButton btnImprimir;
        private RJ_UI.Classes.RJTextBox txtCodProduto;
        private RJ_UI.Classes.RJTextBox txtProduto;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtNotaCompra;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private RJ_UI.Classes.RJTextBox txtQuantidade;
        private System.Data.DataSet dsProdutos;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn7;
        private FontAwesome.Sharp.IconButton btnConfirmaItem;
        private FontAwesome.Sharp.IconButton btnExcluirProduto;
        private FontAwesome.Sharp.IconButton btnLimpar;
        private System.Windows.Forms.ComboBox comboModeloEtiqueta;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private RJ_UI.Classes.RJTextBox txtParcelas;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private System.Windows.Forms.ComboBox comboImpressoras;
        private RJ_UI.Classes.RJTextBox txtObservacoes;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private System.Windows.Forms.CheckBox chkPreview;
        private FontAwesome.Sharp.IconButton btnSalvarConfig;
    }
}