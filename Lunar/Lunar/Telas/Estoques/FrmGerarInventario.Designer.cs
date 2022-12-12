namespace Lunar.Telas.Estoques
{
    partial class FrmGerarInventario
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
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn1 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn2 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn3 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn4 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn5 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn6 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn2 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn3 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.iconPesquisar = new FontAwesome.Sharp.IconButton();
            this.btnPesquisar = new Lunar.RJ_UI.Classes.RJButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioTodos = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radioRevendaEMateriaPrima = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radioSomenteRevenda = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataInventario = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.sfDataPager1 = new Syncfusion.WinForms.DataPager.SfDataPager();
            this.autoLabel15 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtRegistroPorPagina = new Lunar.RJ_UI.Classes.RJTextBox();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.btnReceber = new Lunar.RJ_UI.Classes.RJButton();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtLivro = new Lunar.RJ_UI.Classes.RJTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioTodos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioRevendaEMateriaPrima)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSomenteRevenda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.autoLabel1);
            this.groupBox1.Controls.Add(this.txtLivro);
            this.groupBox1.Controls.Add(this.iconPesquisar);
            this.groupBox1.Controls.Add(this.btnPesquisar);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.autoLabel4);
            this.groupBox1.Controls.Add(this.txtDataInventario);
            this.groupBox1.Controls.Add(this.sfDataPager1);
            this.groupBox1.Controls.Add(this.autoLabel15);
            this.groupBox1.Controls.Add(this.txtRegistroPorPagina);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1119, 216);
            this.groupBox1.TabIndex = 222;
            this.groupBox1.TabStop = false;
            // 
            // iconPesquisar
            // 
            this.iconPesquisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.iconPesquisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconPesquisar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.iconPesquisar.FlatAppearance.BorderSize = 0;
            this.iconPesquisar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.iconPesquisar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.iconPesquisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconPesquisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconPesquisar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.iconPesquisar.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.iconPesquisar.IconColor = System.Drawing.Color.White;
            this.iconPesquisar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.iconPesquisar.IconSize = 40;
            this.iconPesquisar.Location = new System.Drawing.Point(360, 159);
            this.iconPesquisar.Name = "iconPesquisar";
            this.iconPesquisar.Size = new System.Drawing.Size(44, 36);
            this.iconPesquisar.TabIndex = 3;
            this.iconPesquisar.UseVisualStyleBackColor = false;
            this.iconPesquisar.Click += new System.EventHandler(this.iconPesquisar_Click);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnPesquisar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnPesquisar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisar.BorderRadius = 8;
            this.btnPesquisar.BorderSize = 2;
            this.btnPesquisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(90)))));
            this.btnPesquisar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnPesquisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnPesquisar.Location = new System.Drawing.Point(342, 144);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(77, 62);
            this.btnPesquisar.TabIndex = 4;
            this.btnPesquisar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioTodos);
            this.groupBox3.Controls.Add(this.radioRevendaEMateriaPrima);
            this.groupBox3.Controls.Add(this.radioSomenteRevenda);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F);
            this.groupBox3.Location = new System.Drawing.Point(6, 96);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(299, 112);
            this.groupBox3.TabIndex = 227;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo de Produto";
            // 
            // radioTodos
            // 
            this.radioTodos.BeforeTouchSize = new System.Drawing.Size(93, 21);
            this.radioTodos.Location = new System.Drawing.Point(7, 73);
            this.radioTodos.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioTodos.Name = "radioTodos";
            this.radioTodos.Size = new System.Drawing.Size(93, 21);
            this.radioTodos.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2007;
            this.radioTodos.TabIndex = 3;
            this.radioTodos.TabStop = false;
            this.radioTodos.Text = " Todos";
            this.radioTodos.ThemeName = "Office2007";
            // 
            // radioRevendaEMateriaPrima
            // 
            this.radioRevendaEMateriaPrima.BeforeTouchSize = new System.Drawing.Size(269, 21);
            this.radioRevendaEMateriaPrima.Location = new System.Drawing.Point(7, 46);
            this.radioRevendaEMateriaPrima.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioRevendaEMateriaPrima.Name = "radioRevendaEMateriaPrima";
            this.radioRevendaEMateriaPrima.Size = new System.Drawing.Size(269, 21);
            this.radioRevendaEMateriaPrima.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2007;
            this.radioRevendaEMateriaPrima.TabIndex = 1;
            this.radioRevendaEMateriaPrima.TabStop = false;
            this.radioRevendaEMateriaPrima.Text = " Produtos Para Revenda e Matéria Prima";
            this.radioRevendaEMateriaPrima.ThemeName = "Office2007";
            // 
            // radioSomenteRevenda
            // 
            this.radioSomenteRevenda.BeforeTouchSize = new System.Drawing.Size(233, 21);
            this.radioSomenteRevenda.Checked = true;
            this.radioSomenteRevenda.Location = new System.Drawing.Point(6, 22);
            this.radioSomenteRevenda.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioSomenteRevenda.Name = "radioSomenteRevenda";
            this.radioSomenteRevenda.Size = new System.Drawing.Size(233, 21);
            this.radioSomenteRevenda.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2007;
            this.radioSomenteRevenda.TabIndex = 0;
            this.radioSomenteRevenda.Text = " Somente Produtos Para Revenda";
            this.radioSomenteRevenda.ThemeName = "Office2007";
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(7, 37);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(97, 16);
            this.autoLabel4.TabIndex = 223;
            this.autoLabel4.Text = "Data Inventário";
            // 
            // txtDataInventario
            // 
            this.txtDataInventario.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataInventario.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInventario.Location = new System.Drawing.Point(6, 54);
            this.txtDataInventario.Name = "txtDataInventario";
            this.txtDataInventario.Size = new System.Drawing.Size(185, 35);
            this.txtDataInventario.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataInventario.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataInventario.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataInventario.TabIndex = 0;
            this.txtDataInventario.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // sfDataPager1
            // 
            this.sfDataPager1.AccessibleName = "DataPager";
            this.sfDataPager1.BackColor = System.Drawing.Color.White;
            this.sfDataPager1.CanOverrideStyle = true;
            this.sfDataPager1.Location = new System.Drawing.Point(328, 41);
            this.sfDataPager1.Name = "sfDataPager1";
            this.sfDataPager1.Size = new System.Drawing.Size(395, 48);
            this.sfDataPager1.TabIndex = 2;
            this.sfDataPager1.OnDemandLoading += new System.EventHandler<Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs>(this.sfDataPager1_OnDemandLoading);
            // 
            // autoLabel15
            // 
            this.autoLabel15.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel15.ForeColor = System.Drawing.Color.Black;
            this.autoLabel15.Location = new System.Drawing.Point(215, 40);
            this.autoLabel15.Name = "autoLabel15";
            this.autoLabel15.Size = new System.Drawing.Size(90, 16);
            this.autoLabel15.TabIndex = 218;
            this.autoLabel15.Text = "Reg. por Pág.";
            // 
            // txtRegistroPorPagina
            // 
            this.txtRegistroPorPagina.BackColor = System.Drawing.Color.White;
            this.txtRegistroPorPagina.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtRegistroPorPagina.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtRegistroPorPagina.BorderRadius = 8;
            this.txtRegistroPorPagina.BorderSize = 2;
            this.txtRegistroPorPagina.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtRegistroPorPagina.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegistroPorPagina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtRegistroPorPagina.Location = new System.Drawing.Point(207, 52);
            this.txtRegistroPorPagina.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegistroPorPagina.Multiline = false;
            this.txtRegistroPorPagina.Name = "txtRegistroPorPagina";
            this.txtRegistroPorPagina.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtRegistroPorPagina.PasswordChar = false;
            this.txtRegistroPorPagina.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtRegistroPorPagina.PlaceholderText = "";
            this.txtRegistroPorPagina.ReadOnly = false;
            this.txtRegistroPorPagina.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRegistroPorPagina.Size = new System.Drawing.Size(108, 37);
            this.txtRegistroPorPagina.TabIndex = 1;
            this.txtRegistroPorPagina.Tag = "";
            this.txtRegistroPorPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtRegistroPorPagina.Texts = "100";
            this.txtRegistroPorPagina.UnderlinedStyle = false;
            // 
            // grid
            // 
            this.grid.AccessibleName = "Table";
            this.grid.AllowEditing = false;
            this.grid.AllowFiltering = true;
            this.grid.AllowResizingColumns = true;
            this.grid.AllowResizingHiddenColumns = true;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoGenerateColumns = false;
            this.grid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowFiltering = true;
            gridTextColumn1.AllowResizing = true;
            gridTextColumn1.CellStyle.Font.Size = 12F;
            gridTextColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn1.HeaderStyle.Font.Size = 10F;
            gridTextColumn1.HeaderText = "NCM";
            gridTextColumn1.MappingName = "ncm";
            gridNumericColumn1.AllowEditing = false;
            gridNumericColumn1.AllowFiltering = true;
            gridNumericColumn1.AllowResizing = true;
            gridNumericColumn1.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridNumericColumn1.CellStyle.Font.Size = 12F;
            gridNumericColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridNumericColumn1.Format = "N";
            gridNumericColumn1.HeaderStyle.Font.Facename = "Microsoft Sans Serif";
            gridNumericColumn1.HeaderStyle.Font.Size = 10F;
            gridNumericColumn1.HeaderText = "Código";
            gridNumericColumn1.MappingName = "codigo";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.HeaderStyle.Font.Size = 10F;
            gridTextColumn2.HeaderText = "Descrição do Produto";
            gridTextColumn2.MappingName = "descricao";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.AllowResizing = true;
            gridTextColumn3.CellStyle.Font.Size = 12F;
            gridTextColumn3.HeaderStyle.Font.Size = 10F;
            gridTextColumn3.HeaderText = "Código de Barras";
            gridTextColumn3.MappingName = "codigoBarras";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.AllowResizing = true;
            gridTextColumn4.CellStyle.Font.Size = 12F;
            gridTextColumn4.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn4.HeaderStyle.Font.Size = 10F;
            gridTextColumn4.HeaderText = "CSOSN";
            gridTextColumn4.MappingName = "csosn";
            gridTextColumn5.AllowEditing = false;
            gridTextColumn5.AllowFiltering = true;
            gridTextColumn5.AllowResizing = true;
            gridTextColumn5.CellStyle.Font.Size = 12F;
            gridTextColumn5.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn5.HeaderStyle.Font.Size = 10F;
            gridTextColumn5.HeaderText = "Unidade de Medida";
            gridTextColumn5.MappingName = "medida";
            gridTextColumn6.AllowEditing = false;
            gridTextColumn6.AllowFiltering = true;
            gridTextColumn6.AllowResizing = true;
            gridTextColumn6.CellStyle.Font.Size = 12F;
            gridTextColumn6.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn6.HeaderStyle.Font.Size = 10F;
            gridTextColumn6.HeaderText = "Quantidade";
            gridTextColumn6.MappingName = "quantidadeInventario";
            gridNumericColumn2.AllowEditing = false;
            gridNumericColumn2.AllowFiltering = true;
            gridNumericColumn2.AllowResizing = true;
            gridNumericColumn2.CellStyle.Font.Size = 12F;
            gridNumericColumn2.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridNumericColumn2.Format = "N2";
            gridNumericColumn2.HeaderStyle.Font.Facename = "Microsoft Sans Serif";
            gridNumericColumn2.HeaderStyle.Font.Size = 10F;
            gridNumericColumn2.HeaderText = "Custo Unitário";
            gridNumericColumn2.MappingName = "valorCusto";
            gridNumericColumn3.AllowEditing = false;
            gridNumericColumn3.AllowFiltering = true;
            gridNumericColumn3.AllowResizing = true;
            gridNumericColumn3.CellStyle.Font.Size = 12F;
            gridNumericColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridNumericColumn3.Format = "N2";
            gridNumericColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn3.HeaderStyle.Font.Size = 10F;
            gridNumericColumn3.HeaderText = "Valor Total";
            gridNumericColumn3.MappingName = "valorTotal";
            this.grid.Columns.Add(gridTextColumn1);
            this.grid.Columns.Add(gridNumericColumn1);
            this.grid.Columns.Add(gridTextColumn2);
            this.grid.Columns.Add(gridTextColumn3);
            this.grid.Columns.Add(gridTextColumn4);
            this.grid.Columns.Add(gridTextColumn5);
            this.grid.Columns.Add(gridTextColumn6);
            this.grid.Columns.Add(gridNumericColumn2);
            this.grid.Columns.Add(gridNumericColumn3);
            this.grid.Location = new System.Drawing.Point(6, 222);
            this.grid.Name = "grid";
            this.grid.SelectionMode = Syncfusion.WinForms.DataGrid.Enums.GridSelectionMode.Extended;
            this.grid.Size = new System.Drawing.Size(1101, 165);
            this.grid.Style.CellStyle.Font.Size = 10F;
            this.grid.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.grid.Style.HeaderStyle.Font.Size = 12F;
            this.grid.TabIndex = 237;
            this.grid.Text = "Grid Parcelas";
            // 
            // btnReceber
            // 
            this.btnReceber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReceber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnReceber.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnReceber.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnReceber.BorderRadius = 8;
            this.btnReceber.BorderSize = 0;
            this.btnReceber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReceber.FlatAppearance.BorderSize = 0;
            this.btnReceber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReceber.ForeColor = System.Drawing.Color.Transparent;
            this.btnReceber.Location = new System.Drawing.Point(908, 393);
            this.btnReceber.Name = "btnReceber";
            this.btnReceber.Size = new System.Drawing.Size(199, 45);
            this.btnReceber.TabIndex = 232;
            this.btnReceber.Text = "Imprimir [F5]";
            this.btnReceber.TextColor = System.Drawing.Color.Transparent;
            this.btnReceber.UseVisualStyleBackColor = false;
            this.btnReceber.Click += new System.EventHandler(this.btnReceber_Click);
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(320, 90);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(54, 16);
            this.autoLabel1.TabIndex = 247;
            this.autoLabel1.Text = "Nº Livro";
            // 
            // txtLivro
            // 
            this.txtLivro.BackColor = System.Drawing.Color.White;
            this.txtLivro.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtLivro.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtLivro.BorderRadius = 8;
            this.txtLivro.BorderSize = 2;
            this.txtLivro.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtLivro.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLivro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtLivro.Location = new System.Drawing.Point(312, 102);
            this.txtLivro.Margin = new System.Windows.Forms.Padding(4);
            this.txtLivro.Multiline = false;
            this.txtLivro.Name = "txtLivro";
            this.txtLivro.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtLivro.PasswordChar = false;
            this.txtLivro.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtLivro.PlaceholderText = "";
            this.txtLivro.ReadOnly = false;
            this.txtLivro.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtLivro.Size = new System.Drawing.Size(108, 37);
            this.txtLivro.TabIndex = 2;
            this.txtLivro.Tag = "";
            this.txtLivro.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLivro.Texts = "";
            this.txtLivro.UnderlinedStyle = false;
            // 
            // FrmGerarInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1119, 450);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.btnReceber);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmGerarInventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventário de Estoque";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioTodos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioRevendaEMateriaPrima)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSomenteRevenda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioTodos;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioRevendaEMateriaPrima;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioSomenteRevenda;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataInventario;
        private Syncfusion.WinForms.DataPager.SfDataPager sfDataPager1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel15;
        private RJ_UI.Classes.RJTextBox txtRegistroPorPagina;
        private FontAwesome.Sharp.IconButton iconPesquisar;
        private RJ_UI.Classes.RJButton btnPesquisar;
        private RJ_UI.Classes.RJButton btnReceber;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtLivro;
    }
}