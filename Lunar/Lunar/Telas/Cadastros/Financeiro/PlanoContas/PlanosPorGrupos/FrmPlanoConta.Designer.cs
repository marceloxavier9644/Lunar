namespace Lunar.Telas.Cadastros.Financeiro.PlanoContas.PlanosPorGrupos
{
    partial class FrmPlanoConta
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
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn6 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.autoLabel9 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioFixo = new System.Windows.Forms.RadioButton();
            this.radioVariavel = new System.Windows.Forms.RadioButton();
            this.btnEditar = new Lunar.RJ_UI.Classes.RJButton();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtNovaClass = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnNovo = new Lunar.RJ_UI.Classes.RJButton();
            this.btnGravar = new Lunar.RJ_UI.Classes.RJButton();
            this.txtDescricao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtNivelAcima = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtIdPai = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodPlanoConta = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnExcluirPlano = new Lunar.RJ_UI.Classes.RJButton();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.panelTitleBar.Size = new System.Drawing.Size(860, 44);
            this.panelTitleBar.TabIndex = 205;
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.White;
            this.autoLabel2.Location = new System.Drawing.Point(5, 9);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(171, 25);
            this.autoLabel2.TabIndex = 198;
            this.autoLabel2.Text = "Plano de Contas";
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
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClose.IconSize = 30;
            this.btnClose.Location = new System.Drawing.Point(819, 3);
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
            this.grid.AllowStandardTab = true;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoGenerateColumns = false;
            this.grid.AutoGenerateRelations = true;
            this.grid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            this.grid.BackColor = System.Drawing.Color.White;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowFiltering = true;
            gridTextColumn1.AllowResizing = true;
            gridTextColumn1.CellStyle.Font.Size = 12F;
            gridTextColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderStyle.Font.Size = 12F;
            gridTextColumn1.HeaderText = "Id";
            gridTextColumn1.MappingName = "Id";
            gridTextColumn1.Visible = false;
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn2.HeaderStyle.Font.Size = 12F;
            gridTextColumn2.HeaderText = "Plano Principal";
            gridTextColumn2.MappingName = "IdPai";
            gridTextColumn2.Visible = false;
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.AllowResizing = true;
            gridTextColumn3.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn3.CellStyle.Font.Size = 12F;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderStyle.Font.Size = 12F;
            gridTextColumn3.HeaderText = "Descrição";
            gridTextColumn3.MappingName = "Descricao";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.AllowResizing = true;
            gridTextColumn4.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn4.CellStyle.Font.Size = 12F;
            gridTextColumn4.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn4.HeaderStyle.Font.Size = 12F;
            gridTextColumn4.HeaderText = "Classificação";
            gridTextColumn4.MappingName = "Classificacao";
            gridTextColumn5.AllowEditing = false;
            gridTextColumn5.AllowFiltering = true;
            gridTextColumn5.AllowResizing = true;
            gridTextColumn5.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn5.HeaderText = "Tipo Plano";
            gridTextColumn5.MappingName = "TipoConta";
            gridTextColumn5.Visible = false;
            gridTextColumn6.AllowEditing = false;
            gridTextColumn6.AllowFiltering = true;
            gridTextColumn6.AllowResizing = true;
            gridTextColumn6.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn6.HeaderText = "ID Acima";
            gridTextColumn6.MappingName = "IdAcima";
            gridTextColumn6.Visible = false;
            this.grid.Columns.Add(gridTextColumn1);
            this.grid.Columns.Add(gridTextColumn2);
            this.grid.Columns.Add(gridTextColumn3);
            this.grid.Columns.Add(gridTextColumn4);
            this.grid.Columns.Add(gridTextColumn5);
            this.grid.Columns.Add(gridTextColumn6);
            this.grid.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid.Location = new System.Drawing.Point(5, 50);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(855, 147);
            this.grid.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.grid.Style.CellStyle.Font.Facename = "Montserrat";
            this.grid.Style.CellStyle.Font.Size = 12F;
            this.grid.TabIndex = 206;
            this.grid.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.grid_QueryRowStyle);
            this.grid.CellClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.grid_CellClick);
            this.grid.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.grid_CellDoubleClick);
            // 
            // autoLabel9
            // 
            this.autoLabel9.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel9.ForeColor = System.Drawing.Color.Black;
            this.autoLabel9.Location = new System.Drawing.Point(16, 56);
            this.autoLabel9.Name = "autoLabel9";
            this.autoLabel9.Size = new System.Drawing.Size(69, 16);
            this.autoLabel9.TabIndex = 254;
            this.autoLabel9.Text = "Descrição";
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(16, 109);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(79, 16);
            this.autoLabel1.TabIndex = 256;
            this.autoLabel1.Text = "Nível Acima";
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(476, 3);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(67, 16);
            this.autoLabel4.TabIndex = 260;
            this.autoLabel4.Text = "Código ID";
            this.autoLabel4.Visible = false;
            // 
            // autoLabel6
            // 
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(319, 109);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(41, 16);
            this.autoLabel6.TabIndex = 264;
            this.autoLabel6.Text = "Id Pai";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExcluirPlano);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnEditar);
            this.panel1.Controls.Add(this.autoLabel3);
            this.panel1.Controls.Add(this.txtNovaClass);
            this.panel1.Controls.Add(this.btnNovo);
            this.panel1.Controls.Add(this.autoLabel1);
            this.panel1.Controls.Add(this.btnGravar);
            this.panel1.Controls.Add(this.autoLabel6);
            this.panel1.Controls.Add(this.autoLabel4);
            this.panel1.Controls.Add(this.autoLabel9);
            this.panel1.Controls.Add(this.txtDescricao);
            this.panel1.Controls.Add(this.txtNivelAcima);
            this.panel1.Controls.Add(this.txtIdPai);
            this.panel1.Controls.Add(this.txtCodPlanoConta);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 203);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 247);
            this.panel1.TabIndex = 267;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioFixo);
            this.groupBox1.Controls.Add(this.radioVariavel);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(630, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 101);
            this.groupBox1.TabIndex = 274;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de Receita/Despesa";
            // 
            // radioFixo
            // 
            this.radioFixo.AutoSize = true;
            this.radioFixo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioFixo.Location = new System.Drawing.Point(17, 42);
            this.radioFixo.Name = "radioFixo";
            this.radioFixo.Size = new System.Drawing.Size(60, 24);
            this.radioFixo.TabIndex = 271;
            this.radioFixo.Text = " Fixo";
            this.radioFixo.UseVisualStyleBackColor = true;
            // 
            // radioVariavel
            // 
            this.radioVariavel.AutoSize = true;
            this.radioVariavel.Checked = true;
            this.radioVariavel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioVariavel.Location = new System.Drawing.Point(100, 42);
            this.radioVariavel.Name = "radioVariavel";
            this.radioVariavel.Size = new System.Drawing.Size(87, 24);
            this.radioVariavel.TabIndex = 272;
            this.radioVariavel.TabStop = true;
            this.radioVariavel.Text = " Variável";
            this.radioVariavel.UseVisualStyleBackColor = true;
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.White;
            this.btnEditar.BackgroundColor = System.Drawing.Color.White;
            this.btnEditar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnEditar.BorderRadius = 8;
            this.btnEditar.BorderSize = 2;
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnEditar.FlatAppearance.BorderSize = 2;
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnEditar.Location = new System.Drawing.Point(150, 8);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(139, 33);
            this.btnEditar.TabIndex = 270;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(476, 56);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(80, 16);
            this.autoLabel3.TabIndex = 269;
            this.autoLabel3.Text = "Nova Class.";
            // 
            // txtNovaClass
            // 
            this.txtNovaClass.BackColor = System.Drawing.Color.White;
            this.txtNovaClass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNovaClass.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNovaClass.BorderRadius = 8;
            this.txtNovaClass.BorderSize = 2;
            this.txtNovaClass.Enabled = false;
            this.txtNovaClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNovaClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNovaClass.Location = new System.Drawing.Point(464, 68);
            this.txtNovaClass.Margin = new System.Windows.Forms.Padding(4);
            this.txtNovaClass.Multiline = false;
            this.txtNovaClass.Name = "txtNovaClass";
            this.txtNovaClass.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtNovaClass.PasswordChar = false;
            this.txtNovaClass.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtNovaClass.PlaceholderText = "";
            this.txtNovaClass.ReadOnly = false;
            this.txtNovaClass.Size = new System.Drawing.Size(159, 37);
            this.txtNovaClass.TabIndex = 2;
            this.txtNovaClass.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNovaClass.Texts = "";
            this.txtNovaClass.UnderlinedStyle = false;
            // 
            // btnNovo
            // 
            this.btnNovo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnNovo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnNovo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnNovo.BorderRadius = 8;
            this.btnNovo.BorderSize = 0;
            this.btnNovo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovo.FlatAppearance.BorderSize = 0;
            this.btnNovo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovo.ForeColor = System.Drawing.Color.White;
            this.btnNovo.Location = new System.Drawing.Point(5, 8);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(139, 33);
            this.btnNovo.TabIndex = 267;
            this.btnNovo.Text = "Novo [F2]";
            this.btnNovo.TextColor = System.Drawing.Color.White;
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
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
            this.btnGravar.ForeColor = System.Drawing.Color.White;
            this.btnGravar.Location = new System.Drawing.Point(629, 190);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(217, 45);
            this.btnGravar.TabIndex = 5;
            this.btnGravar.Text = "Gravar [F5]";
            this.btnGravar.TextColor = System.Drawing.Color.White;
            this.btnGravar.UseVisualStyleBackColor = false;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
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
            this.txtDescricao.Location = new System.Drawing.Point(4, 68);
            this.txtDescricao.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricao.Multiline = false;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescricao.PasswordChar = false;
            this.txtDescricao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDescricao.PlaceholderText = "";
            this.txtDescricao.ReadOnly = false;
            this.txtDescricao.Size = new System.Drawing.Size(452, 37);
            this.txtDescricao.TabIndex = 0;
            this.txtDescricao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescricao.Texts = "";
            this.txtDescricao.UnderlinedStyle = false;
            // 
            // txtNivelAcima
            // 
            this.txtNivelAcima.BackColor = System.Drawing.Color.White;
            this.txtNivelAcima.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNivelAcima.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNivelAcima.BorderRadius = 8;
            this.txtNivelAcima.BorderSize = 2;
            this.txtNivelAcima.Enabled = false;
            this.txtNivelAcima.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNivelAcima.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNivelAcima.Location = new System.Drawing.Point(4, 121);
            this.txtNivelAcima.Margin = new System.Windows.Forms.Padding(4);
            this.txtNivelAcima.Multiline = false;
            this.txtNivelAcima.Name = "txtNivelAcima";
            this.txtNivelAcima.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtNivelAcima.PasswordChar = false;
            this.txtNivelAcima.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtNivelAcima.PlaceholderText = "";
            this.txtNivelAcima.ReadOnly = false;
            this.txtNivelAcima.Size = new System.Drawing.Size(295, 37);
            this.txtNivelAcima.TabIndex = 3;
            this.txtNivelAcima.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNivelAcima.Texts = "";
            this.txtNivelAcima.UnderlinedStyle = false;
            // 
            // txtIdPai
            // 
            this.txtIdPai.BackColor = System.Drawing.Color.White;
            this.txtIdPai.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtIdPai.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtIdPai.BorderRadius = 8;
            this.txtIdPai.BorderSize = 2;
            this.txtIdPai.Enabled = false;
            this.txtIdPai.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdPai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtIdPai.Location = new System.Drawing.Point(307, 121);
            this.txtIdPai.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdPai.Multiline = false;
            this.txtIdPai.Name = "txtIdPai";
            this.txtIdPai.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtIdPai.PasswordChar = false;
            this.txtIdPai.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtIdPai.PlaceholderText = "";
            this.txtIdPai.ReadOnly = false;
            this.txtIdPai.Size = new System.Drawing.Size(316, 37);
            this.txtIdPai.TabIndex = 4;
            this.txtIdPai.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtIdPai.Texts = "";
            this.txtIdPai.UnderlinedStyle = false;
            // 
            // txtCodPlanoConta
            // 
            this.txtCodPlanoConta.BackColor = System.Drawing.Color.White;
            this.txtCodPlanoConta.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodPlanoConta.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodPlanoConta.BorderRadius = 8;
            this.txtCodPlanoConta.BorderSize = 2;
            this.txtCodPlanoConta.Enabled = false;
            this.txtCodPlanoConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodPlanoConta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodPlanoConta.Location = new System.Drawing.Point(464, 15);
            this.txtCodPlanoConta.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodPlanoConta.Multiline = false;
            this.txtCodPlanoConta.Name = "txtCodPlanoConta";
            this.txtCodPlanoConta.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodPlanoConta.PasswordChar = false;
            this.txtCodPlanoConta.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodPlanoConta.PlaceholderText = "";
            this.txtCodPlanoConta.ReadOnly = false;
            this.txtCodPlanoConta.Size = new System.Drawing.Size(116, 37);
            this.txtCodPlanoConta.TabIndex = 1;
            this.txtCodPlanoConta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodPlanoConta.Texts = "";
            this.txtCodPlanoConta.UnderlinedStyle = false;
            this.txtCodPlanoConta.Visible = false;
            // 
            // btnExcluirPlano
            // 
            this.btnExcluirPlano.BackColor = System.Drawing.Color.White;
            this.btnExcluirPlano.BackgroundColor = System.Drawing.Color.White;
            this.btnExcluirPlano.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExcluirPlano.BorderRadius = 8;
            this.btnExcluirPlano.BorderSize = 2;
            this.btnExcluirPlano.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluirPlano.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExcluirPlano.FlatAppearance.BorderSize = 2;
            this.btnExcluirPlano.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExcluirPlano.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluirPlano.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluirPlano.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnExcluirPlano.Location = new System.Drawing.Point(295, 8);
            this.btnExcluirPlano.Name = "btnExcluirPlano";
            this.btnExcluirPlano.Size = new System.Drawing.Size(139, 33);
            this.btnExcluirPlano.TabIndex = 275;
            this.btnExcluirPlano.Text = "Excluir";
            this.btnExcluirPlano.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnExcluirPlano.UseVisualStyleBackColor = false;
            this.btnExcluirPlano.Click += new System.EventHandler(this.btnExcluirPlano_Click);
            // 
            // FrmPlanoConta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmPlanoConta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plano de Contas";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPlanoConta_KeyDown);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private FontAwesome.Sharp.IconButton btnClose;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel9;
        private RJ_UI.Classes.RJTextBox txtDescricao;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtNivelAcima;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private RJ_UI.Classes.RJTextBox txtCodPlanoConta;
        private RJ_UI.Classes.RJButton btnGravar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private RJ_UI.Classes.RJTextBox txtIdPai;
        private System.Windows.Forms.Panel panel1;
        private RJ_UI.Classes.RJButton btnNovo;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private RJ_UI.Classes.RJTextBox txtNovaClass;
        private RJ_UI.Classes.RJButton btnEditar;
        private System.Windows.Forms.RadioButton radioVariavel;
        private System.Windows.Forms.RadioButton radioFixo;
        private System.Windows.Forms.GroupBox groupBox1;
        private RJ_UI.Classes.RJButton btnExcluirPlano;
    }
}