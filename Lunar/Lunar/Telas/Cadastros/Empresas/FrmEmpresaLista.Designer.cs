namespace Lunar.Telas.Cadastros.Empresas
{
    partial class FrmEmpresaLista
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
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn1 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn1 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn2 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn3 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn4 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn5 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn6 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn7 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn8 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn9 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gridEmpresa = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.btnNovo = new Lunar.RJ_UI.Classes.RJButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExportarPDF = new FontAwesome.Sharp.IconButton();
            this.btnExportarExcel = new FontAwesome.Sharp.IconButton();
            this.btnEditar = new Lunar.RJ_UI.Classes.RJButton();
            this.autoLabel15 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtRegistroPorPagina = new Lunar.RJ_UI.Classes.RJTextBox();
            this.sfDataPager1 = new Syncfusion.WinForms.DataPager.SfDataPager();
            this.txtPesquisa = new Lunar.RJ_UI.Classes.RJTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn1 = new System.Data.DataColumn();
            this.Cliente = new System.Data.DataTable();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dsCliente = new System.Data.DataSet();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmpresa)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCliente)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gridEmpresa);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 100);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1001, 359);
            this.groupBox3.TabIndex = 161;
            this.groupBox3.TabStop = false;
            // 
            // gridEmpresa
            // 
            this.gridEmpresa.AccessibleName = "Table";
            this.gridEmpresa.AllowEditing = false;
            this.gridEmpresa.AllowFiltering = true;
            this.gridEmpresa.AllowStandardTab = true;
            this.gridEmpresa.AutoGenerateColumns = false;
            this.gridEmpresa.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            this.gridEmpresa.BackColor = System.Drawing.Color.White;
            gridNumericColumn1.AllowEditing = false;
            gridNumericColumn1.AllowFiltering = true;
            gridNumericColumn1.Format = "0";
            gridNumericColumn1.HeaderText = "Código";
            gridNumericColumn1.MappingName = "Id";
            gridNumericColumn1.MaxValue = 1E+16D;
            gridNumericColumn1.MinValue = 0D;
            gridNumericColumn1.NullDisplayText = "0";
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowFiltering = true;
            gridTextColumn1.HeaderText = "Razão Social";
            gridTextColumn1.MappingName = "RazaoSocial";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.HeaderText = "Fantasia";
            gridTextColumn2.MappingName = "NomeFantasia";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.HeaderText = "CPF/CNPJ";
            gridTextColumn3.MappingName = "Cnpj";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.HeaderText = "Endereco";
            gridTextColumn4.MappingName = "Endereco.Logradouro";
            gridTextColumn5.AllowEditing = false;
            gridTextColumn5.AllowFiltering = true;
            gridTextColumn5.HeaderText = "Nº";
            gridTextColumn5.MappingName = "Endereco.Numero";
            gridTextColumn6.AllowEditing = false;
            gridTextColumn6.AllowFiltering = true;
            gridTextColumn6.HeaderText = "Cidade";
            gridTextColumn6.MappingName = "Endereco.Cidade.Descricao";
            gridTextColumn7.AllowEditing = false;
            gridTextColumn7.AllowFiltering = true;
            gridTextColumn7.HeaderText = "Bairro";
            gridTextColumn7.MappingName = "Endereco.Bairro";
            gridTextColumn8.AllowEditing = false;
            gridTextColumn8.AllowFiltering = true;
            gridTextColumn8.HeaderText = "DDD";
            gridTextColumn8.MappingName = "DddPrincipal";
            gridTextColumn9.AllowEditing = false;
            gridTextColumn9.AllowFiltering = true;
            gridTextColumn9.HeaderText = "Telefone";
            gridTextColumn9.MappingName = "TelefonePrincipal";
            this.gridEmpresa.Columns.Add(gridNumericColumn1);
            this.gridEmpresa.Columns.Add(gridTextColumn1);
            this.gridEmpresa.Columns.Add(gridTextColumn2);
            this.gridEmpresa.Columns.Add(gridTextColumn3);
            this.gridEmpresa.Columns.Add(gridTextColumn4);
            this.gridEmpresa.Columns.Add(gridTextColumn5);
            this.gridEmpresa.Columns.Add(gridTextColumn6);
            this.gridEmpresa.Columns.Add(gridTextColumn7);
            this.gridEmpresa.Columns.Add(gridTextColumn8);
            this.gridEmpresa.Columns.Add(gridTextColumn9);
            this.gridEmpresa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEmpresa.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridEmpresa.Location = new System.Drawing.Point(3, 16);
            this.gridEmpresa.Name = "gridEmpresa";
            this.gridEmpresa.SelectionUnit = Syncfusion.WinForms.DataGrid.Enums.SelectionUnit.Cell;
            this.gridEmpresa.Size = new System.Drawing.Size(995, 340);
            this.gridEmpresa.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.gridEmpresa.Style.CellStyle.Font.Facename = "Montserrat";
            this.gridEmpresa.Style.CellStyle.Font.Size = 14F;
            this.gridEmpresa.TabIndex = 2;
            this.gridEmpresa.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.gridEmpresa_QueryRowStyle);
            this.gridEmpresa.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.gridEmpresa_CellDoubleClick);
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
            this.btnNovo.ForeColor = System.Drawing.Color.White;
            this.btnNovo.Location = new System.Drawing.Point(697, 19);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(293, 45);
            this.btnNovo.TabIndex = 154;
            this.btnNovo.Text = "Novo";
            this.btnNovo.TextColor = System.Drawing.Color.White;
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExportarPDF);
            this.groupBox2.Controls.Add(this.btnExportarExcel);
            this.groupBox2.Controls.Add(this.btnNovo);
            this.groupBox2.Controls.Add(this.btnEditar);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 459);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1001, 76);
            this.groupBox2.TabIndex = 160;
            this.groupBox2.TabStop = false;
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarPDF.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExportarPDF.FlatAppearance.BorderSize = 0;
            this.btnExportarPDF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExportarPDF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExportarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarPDF.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            this.btnExportarPDF.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExportarPDF.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnExportarPDF.IconSize = 38;
            this.btnExportarPDF.Location = new System.Drawing.Point(54, 23);
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(36, 34);
            this.btnExportarPDF.TabIndex = 217;
            this.btnExportarPDF.UseVisualStyleBackColor = true;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExportarExcel.FlatAppearance.BorderSize = 0;
            this.btnExportarExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExportarExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarExcel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnExportarExcel.IconColor = System.Drawing.Color.DarkGreen;
            this.btnExportarExcel.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnExportarExcel.IconSize = 38;
            this.btnExportarExcel.Location = new System.Drawing.Point(12, 23);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(36, 34);
            this.btnExportarExcel.TabIndex = 216;
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnEditar.Location = new System.Drawing.Point(398, 19);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(293, 45);
            this.btnEditar.TabIndex = 155;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // autoLabel15
            // 
            this.autoLabel15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.autoLabel15.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel15.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel15.ForeColor = System.Drawing.Color.Black;
            this.autoLabel15.Location = new System.Drawing.Point(482, 36);
            this.autoLabel15.Name = "autoLabel15";
            this.autoLabel15.Size = new System.Drawing.Size(94, 18);
            this.autoLabel15.TabIndex = 210;
            this.autoLabel15.Text = "Reg. por Pág.";
            // 
            // txtRegistroPorPagina
            // 
            this.txtRegistroPorPagina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegistroPorPagina.BackColor = System.Drawing.Color.White;
            this.txtRegistroPorPagina.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtRegistroPorPagina.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtRegistroPorPagina.BorderRadius = 8;
            this.txtRegistroPorPagina.BorderSize = 2;
            this.txtRegistroPorPagina.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtRegistroPorPagina.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegistroPorPagina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtRegistroPorPagina.Location = new System.Drawing.Point(474, 48);
            this.txtRegistroPorPagina.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegistroPorPagina.Multiline = false;
            this.txtRegistroPorPagina.Name = "txtRegistroPorPagina";
            this.txtRegistroPorPagina.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtRegistroPorPagina.PasswordChar = false;
            this.txtRegistroPorPagina.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtRegistroPorPagina.PlaceholderText = "";
            this.txtRegistroPorPagina.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRegistroPorPagina.Size = new System.Drawing.Size(108, 39);
            this.txtRegistroPorPagina.TabIndex = 209;
            this.txtRegistroPorPagina.Tag = "";
            this.txtRegistroPorPagina.Texts = "100";
            this.txtRegistroPorPagina.UnderlinedStyle = false;
            // 
            // sfDataPager1
            // 
            this.sfDataPager1.AccessibleName = "DataPager";
            this.sfDataPager1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sfDataPager1.BackColor = System.Drawing.Color.White;
            this.sfDataPager1.CanOverrideStyle = true;
            this.sfDataPager1.Location = new System.Drawing.Point(589, 43);
            this.sfDataPager1.Name = "sfDataPager1";
            this.sfDataPager1.Size = new System.Drawing.Size(395, 48);
            this.sfDataPager1.TabIndex = 154;
            this.sfDataPager1.OnDemandLoading += new System.EventHandler<Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs>(this.sfDataPager1_OnDemandLoading);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPesquisa.BackColor = System.Drawing.Color.White;
            this.txtPesquisa.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPesquisa.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPesquisa.BorderRadius = 8;
            this.txtPesquisa.BorderSize = 2;
            this.txtPesquisa.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPesquisa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPesquisa.Location = new System.Drawing.Point(10, 48);
            this.txtPesquisa.Margin = new System.Windows.Forms.Padding(4);
            this.txtPesquisa.Multiline = false;
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtPesquisa.PasswordChar = false;
            this.txtPesquisa.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtPesquisa.PlaceholderText = "Pesquise por Nome, CPF/CNPJ ou E-mail";
            this.txtPesquisa.Size = new System.Drawing.Size(456, 39);
            this.txtPesquisa.TabIndex = 153;
            this.txtPesquisa.Texts = "";
            this.txtPesquisa.UnderlinedStyle = false;
            this.txtPesquisa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPesquisaCliente_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.autoLabel15);
            this.groupBox1.Controls.Add(this.txtRegistroPorPagina);
            this.groupBox1.Controls.Add(this.sfDataPager1);
            this.groupBox1.Controls.Add(this.txtPesquisa);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1001, 100);
            this.groupBox1.TabIndex = 159;
            this.groupBox1.TabStop = false;
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "Telefone";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "Cidade";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "Numero";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "Endereco";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "CNPJ";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "Apelido";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "Nome";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "Codigo";
            this.dataColumn1.DataType = typeof(int);
            // 
            // Cliente
            // 
            this.Cliente.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9});
            this.Cliente.TableName = "Cliente";
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "Bairro";
            // 
            // dsCliente
            // 
            this.dsCliente.DataSetName = "dsCliente";
            this.dsCliente.Tables.AddRange(new System.Data.DataTable[] {
            this.Cliente});
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmEmpresaLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1001, 535);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmEmpresaLista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Empresas";
            this.Load += new System.EventHandler(this.FrmEmpresaLista_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmEmpresaLista_Paint);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridEmpresa)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCliente)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridEmpresa;
        private FontAwesome.Sharp.IconButton btnExportarPDF;
        private FontAwesome.Sharp.IconButton btnExportarExcel;
        private RJ_UI.Classes.RJButton btnNovo;
        private System.Windows.Forms.GroupBox groupBox2;
        private RJ_UI.Classes.RJButton btnEditar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel15;
        private RJ_UI.Classes.RJTextBox txtRegistroPorPagina;
        private Syncfusion.WinForms.DataPager.SfDataPager sfDataPager1;
        private RJ_UI.Classes.RJTextBox txtPesquisa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Data.DataColumn dataColumn9;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataTable Cliente;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataSet dsCliente;
        private System.Windows.Forms.Timer timer1;
    }
}