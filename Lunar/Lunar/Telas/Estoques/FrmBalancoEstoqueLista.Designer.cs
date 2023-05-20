namespace Lunar.Telas.Estoques
{
    partial class FrmBalancoEstoqueLista
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
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn5 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioTodos = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radioEmDigitacao = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radioEfetivado = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkAtivarData = new System.Windows.Forms.CheckBox();
            this.txtDataInicial = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataFinal = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel15 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.sfDataPager1 = new Syncfusion.WinForms.DataPager.SfDataPager();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblEfetivacao = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.iconPesquisar = new FontAwesome.Sharp.IconButton();
            this.btnPesquisar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnPesquisaCliente = new FontAwesome.Sharp.IconButton();
            this.txtCodAjuste = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtRegistroPorPagina = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtDescricaoAjuste = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnEfetivar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnExcluir = new Lunar.RJ_UI.Classes.RJButton();
            this.btnImprimir = new Lunar.RJ_UI.Classes.RJButton();
            this.btnNovo = new Lunar.RJ_UI.Classes.RJButton();
            this.btnCancelarEfetivacao = new Lunar.RJ_UI.Classes.RJButton();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioTodos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioEmDigitacao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioEfetivado)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioTodos);
            this.groupBox3.Controls.Add(this.radioEmDigitacao);
            this.groupBox3.Controls.Add(this.radioEfetivado);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F);
            this.groupBox3.Location = new System.Drawing.Point(399, 65);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(291, 53);
            this.groupBox3.TabIndex = 241;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo de Ajuste";
            // 
            // radioTodos
            // 
            this.radioTodos.BeforeTouchSize = new System.Drawing.Size(72, 21);
            this.radioTodos.Checked = true;
            this.radioTodos.Location = new System.Drawing.Point(216, 22);
            this.radioTodos.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioTodos.Name = "radioTodos";
            this.radioTodos.Size = new System.Drawing.Size(72, 21);
            this.radioTodos.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2007;
            this.radioTodos.TabIndex = 2;
            this.radioTodos.Text = "Todos";
            this.radioTodos.ThemeName = "Office2007";
            // 
            // radioEmDigitacao
            // 
            this.radioEmDigitacao.BeforeTouchSize = new System.Drawing.Size(112, 21);
            this.radioEmDigitacao.Location = new System.Drawing.Point(103, 22);
            this.radioEmDigitacao.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioEmDigitacao.Name = "radioEmDigitacao";
            this.radioEmDigitacao.Size = new System.Drawing.Size(112, 21);
            this.radioEmDigitacao.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2007;
            this.radioEmDigitacao.TabIndex = 1;
            this.radioEmDigitacao.TabStop = false;
            this.radioEmDigitacao.Text = " Em Digitação";
            this.radioEmDigitacao.ThemeName = "Office2007";
            // 
            // radioEfetivado
            // 
            this.radioEfetivado.BeforeTouchSize = new System.Drawing.Size(91, 21);
            this.radioEfetivado.Location = new System.Drawing.Point(6, 22);
            this.radioEfetivado.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioEfetivado.Name = "radioEfetivado";
            this.radioEfetivado.Size = new System.Drawing.Size(91, 21);
            this.radioEfetivado.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2007;
            this.radioEfetivado.TabIndex = 0;
            this.radioEfetivado.TabStop = false;
            this.radioEfetivado.Text = " Efetivado";
            this.radioEfetivado.ThemeName = "Office2007";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkAtivarData);
            this.groupBox2.Controls.Add(this.txtDataInicial);
            this.groupBox2.Controls.Add(this.autoLabel4);
            this.groupBox2.Controls.Add(this.txtDataFinal);
            this.groupBox2.Controls.Add(this.autoLabel3);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(7, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(385, 107);
            this.groupBox2.TabIndex = 238;
            this.groupBox2.TabStop = false;
            // 
            // chkAtivarData
            // 
            this.chkAtivarData.AutoSize = true;
            this.chkAtivarData.Location = new System.Drawing.Point(13, 13);
            this.chkAtivarData.Name = "chkAtivarData";
            this.chkAtivarData.Size = new System.Drawing.Size(189, 22);
            this.chkAtivarData.TabIndex = 229;
            this.chkAtivarData.Text = "Ativar Pesquisa por Data";
            this.chkAtivarData.UseVisualStyleBackColor = true;
            this.chkAtivarData.CheckedChanged += new System.EventHandler(this.chkAtivarData_CheckedChanged);
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataInicial.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataInicial.Enabled = false;
            this.txtDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDataInicial.Location = new System.Drawing.Point(13, 64);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Size = new System.Drawing.Size(179, 35);
            this.txtDataInicial.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.TabIndex = 224;
            this.txtDataInicial.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(198, 45);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(127, 16);
            this.autoLabel4.TabIndex = 228;
            this.autoLabel4.Text = "Data de Ajuste Final";
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataFinal.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataFinal.Enabled = false;
            this.txtDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Location = new System.Drawing.Point(198, 64);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Size = new System.Drawing.Size(174, 35);
            this.txtDataFinal.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataFinal.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.TabIndex = 225;
            this.txtDataFinal.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(13, 47);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(132, 16);
            this.autoLabel3.TabIndex = 227;
            this.autoLabel3.Text = "Data de Ajuste Inicial";
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1055, 44);
            this.panelTitleBar.TabIndex = 251;
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
            this.btnClose.Location = new System.Drawing.Point(1014, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = false;
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
            gridTextColumn1.Format = "dd/MM/yyyy";
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn1.HeaderStyle.Font.Size = 10F;
            gridTextColumn1.HeaderText = "Data de Ajuste";
            gridTextColumn1.MappingName = "DataAjuste";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            gridTextColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn2.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn2.HeaderStyle.Font.Size = 10F;
            gridTextColumn2.HeaderText = "Descrição";
            gridTextColumn2.MappingName = "Descricao";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.AllowResizing = true;
            gridTextColumn3.CellStyle.Font.Size = 12F;
            gridTextColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn3.HeaderStyle.Font.Size = 10F;
            gridTextColumn3.HeaderText = "Usuário Abertura";
            gridTextColumn3.MappingName = "OperadorCadastro";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.AllowResizing = true;
            gridTextColumn4.CellStyle.Font.Size = 12F;
            gridTextColumn4.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn4.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn4.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn4.HeaderStyle.Font.Size = 10F;
            gridTextColumn4.HeaderText = "Id_Balanço";
            gridTextColumn4.MappingName = "Id";
            gridTextColumn5.AllowEditing = false;
            gridTextColumn5.AllowFiltering = true;
            gridTextColumn5.AllowResizing = true;
            gridTextColumn5.CellStyle.Font.Size = 12F;
            gridTextColumn5.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn5.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn5.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn5.HeaderStyle.Font.Size = 10F;
            gridTextColumn5.HeaderText = "OperadorEfetivacao";
            gridTextColumn5.MappingName = "OperadorEfetivacao.Id";
            this.grid.Columns.Add(gridTextColumn1);
            this.grid.Columns.Add(gridTextColumn2);
            this.grid.Columns.Add(gridTextColumn3);
            this.grid.Columns.Add(gridTextColumn4);
            this.grid.Columns.Add(gridTextColumn5);
            this.grid.Location = new System.Drawing.Point(7, 242);
            this.grid.Name = "grid";
            this.grid.SelectionMode = Syncfusion.WinForms.DataGrid.Enums.GridSelectionMode.Extended;
            this.grid.Size = new System.Drawing.Size(1036, 366);
            this.grid.Style.CellStyle.Font.Size = 10F;
            this.grid.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.grid.Style.HeaderStyle.Font.Size = 12F;
            this.grid.TabIndex = 250;
            this.grid.Text = "Grid Parcelas";
            this.grid.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.grid_QueryRowStyle);
            this.grid.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.grid_CellDoubleClick);
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(450, 8);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(51, 16);
            this.autoLabel2.TabIndex = 231;
            this.autoLabel2.Text = "Código";
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(19, 8);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(128, 16);
            this.autoLabel1.TabIndex = 211;
            this.autoLabel1.Text = "Descrição do Ajuste";
            // 
            // autoLabel15
            // 
            this.autoLabel15.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel15.ForeColor = System.Drawing.Color.Black;
            this.autoLabel15.Location = new System.Drawing.Point(547, 8);
            this.autoLabel15.Name = "autoLabel15";
            this.autoLabel15.Size = new System.Drawing.Size(90, 16);
            this.autoLabel15.TabIndex = 210;
            this.autoLabel15.Text = "Reg. por Pág.";
            // 
            // sfDataPager1
            // 
            this.sfDataPager1.AccessibleName = "DataPager";
            this.sfDataPager1.BackColor = System.Drawing.Color.White;
            this.sfDataPager1.CanOverrideStyle = true;
            this.sfDataPager1.Location = new System.Drawing.Point(654, 11);
            this.sfDataPager1.Name = "sfDataPager1";
            this.sfDataPager1.Size = new System.Drawing.Size(376, 48);
            this.sfDataPager1.TabIndex = 154;
            this.sfDataPager1.OnDemandLoading += new System.EventHandler<Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs>(this.sfDataPager1_OnDemandLoading);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblEfetivacao);
            this.groupBox1.Controls.Add(this.iconPesquisar);
            this.groupBox1.Controls.Add(this.btnPesquisar);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.autoLabel2);
            this.groupBox1.Controls.Add(this.btnPesquisaCliente);
            this.groupBox1.Controls.Add(this.txtCodAjuste);
            this.groupBox1.Controls.Add(this.autoLabel1);
            this.groupBox1.Controls.Add(this.autoLabel15);
            this.groupBox1.Controls.Add(this.txtRegistroPorPagina);
            this.groupBox1.Controls.Add(this.sfDataPager1);
            this.groupBox1.Controls.Add(this.txtDescricaoAjuste);
            this.groupBox1.Location = new System.Drawing.Point(7, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1036, 180);
            this.groupBox1.TabIndex = 244;
            this.groupBox1.TabStop = false;
            // 
            // lblEfetivacao
            // 
            this.lblEfetivacao.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblEfetivacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEfetivacao.ForeColor = System.Drawing.Color.Blue;
            this.lblEfetivacao.Location = new System.Drawing.Point(696, 147);
            this.lblEfetivacao.Name = "lblEfetivacao";
            this.lblEfetivacao.Size = new System.Drawing.Size(183, 24);
            this.lblEfetivacao.TabIndex = 230;
            this.lblEfetivacao.Text = "Data de Ajuste Inicial";
            this.lblEfetivacao.Visible = false;
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
            this.iconPesquisar.Location = new System.Drawing.Point(642, 132);
            this.iconPesquisar.Name = "iconPesquisar";
            this.iconPesquisar.Size = new System.Drawing.Size(33, 35);
            this.iconPesquisar.TabIndex = 243;
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
            this.btnPesquisar.Location = new System.Drawing.Point(624, 120);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(66, 54);
            this.btnPesquisar.TabIndex = 242;
            this.btnPesquisar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnPesquisaCliente
            // 
            this.btnPesquisaCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaCliente.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaCliente.FlatAppearance.BorderSize = 0;
            this.btnPesquisaCliente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaCliente.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaCliente.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaCliente.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaCliente.IconSize = 38;
            this.btnPesquisaCliente.Location = new System.Drawing.Point(399, 23);
            this.btnPesquisaCliente.Name = "btnPesquisaCliente";
            this.btnPesquisaCliente.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaCliente.TabIndex = 230;
            this.btnPesquisaCliente.UseVisualStyleBackColor = true;
            // 
            // txtCodAjuste
            // 
            this.txtCodAjuste.BackColor = System.Drawing.Color.White;
            this.txtCodAjuste.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodAjuste.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodAjuste.BorderRadius = 8;
            this.txtCodAjuste.BorderSize = 2;
            this.txtCodAjuste.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodAjuste.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodAjuste.Location = new System.Drawing.Point(442, 20);
            this.txtCodAjuste.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodAjuste.Multiline = false;
            this.txtCodAjuste.Name = "txtCodAjuste";
            this.txtCodAjuste.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodAjuste.PasswordChar = false;
            this.txtCodAjuste.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodAjuste.PlaceholderText = "";
            this.txtCodAjuste.ReadOnly = false;
            this.txtCodAjuste.Size = new System.Drawing.Size(89, 37);
            this.txtCodAjuste.TabIndex = 229;
            this.txtCodAjuste.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodAjuste.Texts = "";
            this.txtCodAjuste.UnderlinedStyle = false;
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
            this.txtRegistroPorPagina.Location = new System.Drawing.Point(539, 20);
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
            this.txtRegistroPorPagina.TabIndex = 209;
            this.txtRegistroPorPagina.Tag = "";
            this.txtRegistroPorPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtRegistroPorPagina.Texts = "100";
            this.txtRegistroPorPagina.UnderlinedStyle = false;
            // 
            // txtDescricaoAjuste
            // 
            this.txtDescricaoAjuste.BackColor = System.Drawing.Color.White;
            this.txtDescricaoAjuste.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricaoAjuste.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricaoAjuste.BorderRadius = 8;
            this.txtDescricaoAjuste.BorderSize = 2;
            this.txtDescricaoAjuste.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricaoAjuste.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDescricaoAjuste.Location = new System.Drawing.Point(7, 20);
            this.txtDescricaoAjuste.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricaoAjuste.Multiline = false;
            this.txtDescricaoAjuste.Name = "txtDescricaoAjuste";
            this.txtDescricaoAjuste.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescricaoAjuste.PasswordChar = false;
            this.txtDescricaoAjuste.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDescricaoAjuste.PlaceholderText = "";
            this.txtDescricaoAjuste.ReadOnly = false;
            this.txtDescricaoAjuste.Size = new System.Drawing.Size(385, 37);
            this.txtDescricaoAjuste.TabIndex = 153;
            this.txtDescricaoAjuste.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescricaoAjuste.Texts = "";
            this.txtDescricaoAjuste.UnderlinedStyle = false;
            // 
            // btnEfetivar
            // 
            this.btnEfetivar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEfetivar.BackColor = System.Drawing.Color.White;
            this.btnEfetivar.BackgroundColor = System.Drawing.Color.White;
            this.btnEfetivar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnEfetivar.BorderRadius = 8;
            this.btnEfetivar.BorderSize = 2;
            this.btnEfetivar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEfetivar.FlatAppearance.BorderSize = 0;
            this.btnEfetivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEfetivar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEfetivar.ForeColor = System.Drawing.Color.Black;
            this.btnEfetivar.Location = new System.Drawing.Point(416, 614);
            this.btnEfetivar.Name = "btnEfetivar";
            this.btnEfetivar.Size = new System.Drawing.Size(199, 45);
            this.btnEfetivar.TabIndex = 254;
            this.btnEfetivar.Text = "Efetivar Balanço";
            this.btnEfetivar.TextColor = System.Drawing.Color.Black;
            this.btnEfetivar.UseVisualStyleBackColor = false;
            this.btnEfetivar.Click += new System.EventHandler(this.btnEfetivar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcluir.BackColor = System.Drawing.Color.White;
            this.btnExcluir.BackgroundColor = System.Drawing.Color.White;
            this.btnExcluir.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExcluir.BorderRadius = 8;
            this.btnExcluir.BorderSize = 2;
            this.btnExcluir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluir.FlatAppearance.BorderSize = 0;
            this.btnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.ForeColor = System.Drawing.Color.Black;
            this.btnExcluir.Location = new System.Drawing.Point(12, 614);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(193, 45);
            this.btnExcluir.TabIndex = 253;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.TextColor = System.Drawing.Color.Black;
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.BackColor = System.Drawing.Color.White;
            this.btnImprimir.BackgroundColor = System.Drawing.Color.White;
            this.btnImprimir.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnImprimir.BorderRadius = 8;
            this.btnImprimir.BorderSize = 2;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.Black;
            this.btnImprimir.Location = new System.Drawing.Point(621, 614);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(199, 45);
            this.btnImprimir.TabIndex = 245;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextColor = System.Drawing.Color.Black;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnNovo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnNovo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnNovo.BorderRadius = 8;
            this.btnNovo.BorderSize = 0;
            this.btnNovo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovo.FlatAppearance.BorderSize = 0;
            this.btnNovo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovo.ForeColor = System.Drawing.Color.Transparent;
            this.btnNovo.Location = new System.Drawing.Point(826, 614);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(199, 45);
            this.btnNovo.TabIndex = 246;
            this.btnNovo.Text = "Novo [F5]";
            this.btnNovo.TextColor = System.Drawing.Color.Transparent;
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnCancelarEfetivacao
            // 
            this.btnCancelarEfetivacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarEfetivacao.BackColor = System.Drawing.Color.White;
            this.btnCancelarEfetivacao.BackgroundColor = System.Drawing.Color.White;
            this.btnCancelarEfetivacao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnCancelarEfetivacao.BorderRadius = 8;
            this.btnCancelarEfetivacao.BorderSize = 2;
            this.btnCancelarEfetivacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarEfetivacao.FlatAppearance.BorderSize = 0;
            this.btnCancelarEfetivacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarEfetivacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarEfetivacao.ForeColor = System.Drawing.Color.Black;
            this.btnCancelarEfetivacao.Location = new System.Drawing.Point(211, 614);
            this.btnCancelarEfetivacao.Name = "btnCancelarEfetivacao";
            this.btnCancelarEfetivacao.Size = new System.Drawing.Size(199, 45);
            this.btnCancelarEfetivacao.TabIndex = 255;
            this.btnCancelarEfetivacao.Text = "Desfazer Efetivação";
            this.btnCancelarEfetivacao.TextColor = System.Drawing.Color.Black;
            this.btnCancelarEfetivacao.UseVisualStyleBackColor = false;
            this.btnCancelarEfetivacao.Click += new System.EventHandler(this.btnCancelarEfetivacao_Click);
            // 
            // FrmBalancoEstoqueLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1055, 665);
            this.Controls.Add(this.btnCancelarEfetivacao);
            this.Controls.Add(this.btnEfetivar);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmBalancoEstoqueLista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Balanço de Estoque - Lista";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBalancoEstoqueLista_KeyDown);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioTodos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioEmDigitacao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioEfetivado)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelTitleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RJ_UI.Classes.RJButton btnExcluir;
        private FontAwesome.Sharp.IconButton iconPesquisar;
        private RJ_UI.Classes.RJButton btnPesquisar;
        private System.Windows.Forms.GroupBox groupBox3;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioEmDigitacao;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioEfetivado;
        private RJ_UI.Classes.RJButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkAtivarData;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataInicial;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataFinal;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private FontAwesome.Sharp.IconButton btnPesquisaCliente;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel15;
        private RJ_UI.Classes.RJTextBox txtRegistroPorPagina;
        private Syncfusion.WinForms.DataPager.SfDataPager sfDataPager1;
        private RJ_UI.Classes.RJTextBox txtDescricaoAjuste;
        private RJ_UI.Classes.RJButton btnNovo;
        private RJ_UI.Classes.RJTextBox txtCodAjuste;
        private System.Windows.Forms.GroupBox groupBox1;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioTodos;
        private RJ_UI.Classes.RJButton btnEfetivar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblEfetivacao;
        private RJ_UI.Classes.RJButton btnCancelarEfetivacao;
    }
}