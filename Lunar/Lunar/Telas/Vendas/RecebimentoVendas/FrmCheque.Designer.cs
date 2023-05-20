namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    partial class FrmCheque
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
            this.lblFaltante = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.autoLabel10 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataVencimento = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaBanco = new FontAwesome.Sharp.IconButton();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel7 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel8 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel9 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.gridParcelas = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.dsParcelas = new System.Data.DataSet();
            this.Parcelas = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.autoLabel11 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnConfirmarCheques = new FontAwesome.Sharp.IconButton();
            this.btnFechar = new System.Windows.Forms.Button();
            this.txtParcelas = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtRazaoSocial = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCpf = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtNumeroCheque = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtDvConta = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtConta = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtAgencia = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodBanco = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
            this.txtValor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtBanco = new Lunar.RJ_UI.Classes.RJTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridParcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsParcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Parcelas)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFaltante
            // 
            this.lblFaltante.AutoSize = true;
            this.lblFaltante.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaltante.ForeColor = System.Drawing.Color.IndianRed;
            this.lblFaltante.Location = new System.Drawing.Point(13, 70);
            this.lblFaltante.Name = "lblFaltante";
            this.lblFaltante.Size = new System.Drawing.Size(144, 31);
            this.lblFaltante.TabIndex = 162;
            this.lblFaltante.Text = "Valor Faltante";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 40);
            this.label1.TabIndex = 161;
            this.label1.Text = "Cheque";
            // 
            // autoLabel10
            // 
            this.autoLabel10.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel10.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel10.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel10.ForeColor = System.Drawing.Color.Black;
            this.autoLabel10.Location = new System.Drawing.Point(27, 187);
            this.autoLabel10.Name = "autoLabel10";
            this.autoLabel10.Size = new System.Drawing.Size(51, 21);
            this.autoLabel10.TabIndex = 211;
            this.autoLabel10.Text = "Banco";
            // 
            // autoLabel1
            // 
            this.autoLabel1.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel1.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(27, 130);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(131, 21);
            this.autoLabel1.TabIndex = 213;
            this.autoLabel1.Text = "Valor Total Cheque";
            // 
            // txtDataVencimento
            // 
            this.txtDataVencimento.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataVencimento.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataVencimento.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataVencimento.Location = new System.Drawing.Point(286, 149);
            this.txtDataVencimento.Name = "txtDataVencimento";
            this.txtDataVencimento.Size = new System.Drawing.Size(185, 42);
            this.txtDataVencimento.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDataVencimento.Style.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDataVencimento.Style.HoverBorderColor = System.Drawing.Color.White;
            this.txtDataVencimento.TabIndex = 2;
            // 
            // autoLabel4
            // 
            this.autoLabel4.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel4.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(539, 189);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(56, 21);
            this.autoLabel4.TabIndex = 220;
            this.autoLabel4.Text = "Código";
            // 
            // btnPesquisaBanco
            // 
            this.btnPesquisaBanco.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaBanco.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaBanco.FlatAppearance.BorderSize = 0;
            this.btnPesquisaBanco.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaBanco.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaBanco.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaBanco.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaBanco.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaBanco.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaBanco.IconSize = 38;
            this.btnPesquisaBanco.Location = new System.Drawing.Point(490, 209);
            this.btnPesquisaBanco.Name = "btnPesquisaBanco";
            this.btnPesquisaBanco.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaBanco.TabIndex = 5;
            this.btnPesquisaBanco.UseVisualStyleBackColor = true;
            this.btnPesquisaBanco.Click += new System.EventHandler(this.btnPesquisaBanco_Click);
            // 
            // autoLabel2
            // 
            this.autoLabel2.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel2.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(30, 246);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(63, 21);
            this.autoLabel2.TabIndex = 223;
            this.autoLabel2.Text = "Agência";
            // 
            // autoLabel3
            // 
            this.autoLabel3.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel3.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(154, 246);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(49, 21);
            this.autoLabel3.TabIndex = 225;
            this.autoLabel3.Text = "Conta";
            // 
            // autoLabel5
            // 
            this.autoLabel5.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel5.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel5.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.Black;
            this.autoLabel5.Location = new System.Drawing.Point(331, 246);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(69, 21);
            this.autoLabel5.TabIndex = 227;
            this.autoLabel5.Text = "Dv Conta";
            // 
            // autoLabel6
            // 
            this.autoLabel6.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel6.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(487, 130);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(79, 21);
            this.autoLabel6.TabIndex = 229;
            this.autoLabel6.Text = "Nº Cheque";
            // 
            // autoLabel7
            // 
            this.autoLabel7.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel7.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel7.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel7.ForeColor = System.Drawing.Color.Black;
            this.autoLabel7.Location = new System.Drawing.Point(435, 246);
            this.autoLabel7.Name = "autoLabel7";
            this.autoLabel7.Size = new System.Drawing.Size(73, 21);
            this.autoLabel7.TabIndex = 231;
            this.autoLabel7.Text = "CPF/CNPJ";
            // 
            // autoLabel8
            // 
            this.autoLabel8.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel8.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel8.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel8.ForeColor = System.Drawing.Color.Black;
            this.autoLabel8.Location = new System.Drawing.Point(30, 303);
            this.autoLabel8.Name = "autoLabel8";
            this.autoLabel8.Size = new System.Drawing.Size(153, 21);
            this.autoLabel8.TabIndex = 233;
            this.autoLabel8.Text = "NOME/RAZÃO SOCIAL";
            // 
            // autoLabel9
            // 
            this.autoLabel9.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel9.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel9.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel9.ForeColor = System.Drawing.Color.Black;
            this.autoLabel9.Location = new System.Drawing.Point(286, 130);
            this.autoLabel9.Name = "autoLabel9";
            this.autoLabel9.Size = new System.Drawing.Size(103, 21);
            this.autoLabel9.TabIndex = 234;
            this.autoLabel9.Text = "1º Vencimento";
            // 
            // gridParcelas
            // 
            this.gridParcelas.AccessibleName = "Table";
            this.gridParcelas.AllowSorting = false;
            gridTextColumn1.AllowSorting = false;
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderText = "Parcela";
            gridTextColumn1.MappingName = "PARCELA";
            gridTextColumn2.AllowSorting = false;
            gridTextColumn2.Format = "dd/MM/yyyy";
            gridTextColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn2.HeaderText = "Vencimento";
            gridTextColumn2.MappingName = "VENCIMENTO";
            gridTextColumn3.AllowSorting = false;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderText = "Valor";
            gridTextColumn3.MappingName = "VALOR";
            gridTextColumn4.AllowSorting = false;
            gridTextColumn4.HeaderText = "Cheque";
            gridTextColumn4.MappingName = "NUMEROCHEQUE";
            this.gridParcelas.Columns.Add(gridTextColumn1);
            this.gridParcelas.Columns.Add(gridTextColumn2);
            this.gridParcelas.Columns.Add(gridTextColumn3);
            this.gridParcelas.Columns.Add(gridTextColumn4);
            this.gridParcelas.Location = new System.Drawing.Point(18, 371);
            this.gridParcelas.Name = "gridParcelas";
            this.gridParcelas.Size = new System.Drawing.Size(613, 170);
            this.gridParcelas.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.gridParcelas.TabIndex = 13;
            this.gridParcelas.Text = "sfDataGrid1";
            this.gridParcelas.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.gridParcelas_QueryRowStyle);
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
            this.dataColumn3,
            this.dataColumn4});
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
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "NUMEROCHEQUE";
            // 
            // autoLabel11
            // 
            this.autoLabel11.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel11.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel11.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel11.ForeColor = System.Drawing.Color.Black;
            this.autoLabel11.Location = new System.Drawing.Point(200, 130);
            this.autoLabel11.Name = "autoLabel11";
            this.autoLabel11.Size = new System.Drawing.Size(65, 21);
            this.autoLabel11.TabIndex = 237;
            this.autoLabel11.Text = "Parcelas";
            // 
            // btnConfirmarCheques
            // 
            this.btnConfirmarCheques.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnConfirmarCheques.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmarCheques.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnConfirmarCheques.FlatAppearance.BorderSize = 2;
            this.btnConfirmarCheques.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnConfirmarCheques.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnConfirmarCheques.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarCheques.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarCheques.IconChar = FontAwesome.Sharp.IconChar.CircleCheck;
            this.btnConfirmarCheques.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnConfirmarCheques.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnConfirmarCheques.IconSize = 30;
            this.btnConfirmarCheques.Location = new System.Drawing.Point(498, 322);
            this.btnConfirmarCheques.Name = "btnConfirmarCheques";
            this.btnConfirmarCheques.Size = new System.Drawing.Size(133, 41);
            this.btnConfirmarCheques.TabIndex = 12;
            this.btnConfirmarCheques.Text = "Confirmar";
            this.btnConfirmarCheques.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfirmarCheques.UseVisualStyleBackColor = false;
            this.btnConfirmarCheques.Click += new System.EventHandler(this.btnConfirmarCheques_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Image = global::Lunar.Properties.Resources.CloseDark;
            this.btnFechar.Location = new System.Drawing.Point(610, -1);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(30, 31);
            this.btnFechar.TabIndex = 238;
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // txtParcelas
            // 
            this.txtParcelas.BackColor = System.Drawing.Color.White;
            this.txtParcelas.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtParcelas.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtParcelas.BorderRadius = 8;
            this.txtParcelas.BorderSize = 2;
            this.txtParcelas.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParcelas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtParcelas.Location = new System.Drawing.Point(191, 147);
            this.txtParcelas.Margin = new System.Windows.Forms.Padding(4);
            this.txtParcelas.Multiline = false;
            this.txtParcelas.Name = "txtParcelas";
            this.txtParcelas.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtParcelas.PasswordChar = false;
            this.txtParcelas.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtParcelas.PlaceholderText = "";
            this.txtParcelas.ReadOnly = false;
            this.txtParcelas.Size = new System.Drawing.Size(88, 44);
            this.txtParcelas.TabIndex = 1;
            this.txtParcelas.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtParcelas.Texts = "";
            this.txtParcelas.UnderlinedStyle = false;
            // 
            // txtRazaoSocial
            // 
            this.txtRazaoSocial.BackColor = System.Drawing.Color.White;
            this.txtRazaoSocial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtRazaoSocial.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtRazaoSocial.BorderRadius = 8;
            this.txtRazaoSocial.BorderSize = 2;
            this.txtRazaoSocial.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRazaoSocial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtRazaoSocial.Location = new System.Drawing.Point(18, 320);
            this.txtRazaoSocial.Margin = new System.Windows.Forms.Padding(4);
            this.txtRazaoSocial.Multiline = false;
            this.txtRazaoSocial.Name = "txtRazaoSocial";
            this.txtRazaoSocial.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtRazaoSocial.PasswordChar = false;
            this.txtRazaoSocial.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtRazaoSocial.PlaceholderText = "";
            this.txtRazaoSocial.ReadOnly = false;
            this.txtRazaoSocial.Size = new System.Drawing.Size(473, 44);
            this.txtRazaoSocial.TabIndex = 11;
            this.txtRazaoSocial.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtRazaoSocial.Texts = "";
            this.txtRazaoSocial.UnderlinedStyle = false;
            // 
            // txtCpf
            // 
            this.txtCpf.BackColor = System.Drawing.Color.White;
            this.txtCpf.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCpf.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCpf.BorderRadius = 8;
            this.txtCpf.BorderSize = 2;
            this.txtCpf.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCpf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCpf.Location = new System.Drawing.Point(426, 259);
            this.txtCpf.Margin = new System.Windows.Forms.Padding(4);
            this.txtCpf.Multiline = false;
            this.txtCpf.Name = "txtCpf";
            this.txtCpf.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCpf.PasswordChar = false;
            this.txtCpf.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCpf.PlaceholderText = "";
            this.txtCpf.ReadOnly = false;
            this.txtCpf.Size = new System.Drawing.Size(205, 44);
            this.txtCpf.TabIndex = 10;
            this.txtCpf.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCpf.Texts = "";
            this.txtCpf.UnderlinedStyle = false;
            this.txtCpf.Leave += new System.EventHandler(this.txtCpf_Leave);
            // 
            // txtNumeroCheque
            // 
            this.txtNumeroCheque.BackColor = System.Drawing.Color.White;
            this.txtNumeroCheque.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNumeroCheque.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtNumeroCheque.BorderRadius = 8;
            this.txtNumeroCheque.BorderSize = 2;
            this.txtNumeroCheque.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroCheque.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNumeroCheque.Location = new System.Drawing.Point(478, 147);
            this.txtNumeroCheque.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumeroCheque.Multiline = false;
            this.txtNumeroCheque.Name = "txtNumeroCheque";
            this.txtNumeroCheque.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtNumeroCheque.PasswordChar = false;
            this.txtNumeroCheque.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtNumeroCheque.PlaceholderText = "";
            this.txtNumeroCheque.ReadOnly = false;
            this.txtNumeroCheque.Size = new System.Drawing.Size(153, 44);
            this.txtNumeroCheque.TabIndex = 3;
            this.txtNumeroCheque.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNumeroCheque.Texts = "";
            this.txtNumeroCheque.UnderlinedStyle = false;
            // 
            // txtDvConta
            // 
            this.txtDvConta.BackColor = System.Drawing.Color.White;
            this.txtDvConta.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDvConta.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDvConta.BorderRadius = 8;
            this.txtDvConta.BorderSize = 2;
            this.txtDvConta.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDvConta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDvConta.Location = new System.Drawing.Point(321, 259);
            this.txtDvConta.Margin = new System.Windows.Forms.Padding(4);
            this.txtDvConta.Multiline = false;
            this.txtDvConta.Name = "txtDvConta";
            this.txtDvConta.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDvConta.PasswordChar = false;
            this.txtDvConta.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDvConta.PlaceholderText = "";
            this.txtDvConta.ReadOnly = false;
            this.txtDvConta.Size = new System.Drawing.Size(97, 44);
            this.txtDvConta.TabIndex = 9;
            this.txtDvConta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDvConta.Texts = "";
            this.txtDvConta.UnderlinedStyle = false;
            // 
            // txtConta
            // 
            this.txtConta.BackColor = System.Drawing.Color.White;
            this.txtConta.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtConta.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtConta.BorderRadius = 8;
            this.txtConta.BorderSize = 2;
            this.txtConta.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtConta.Location = new System.Drawing.Point(144, 259);
            this.txtConta.Margin = new System.Windows.Forms.Padding(4);
            this.txtConta.Multiline = false;
            this.txtConta.Name = "txtConta";
            this.txtConta.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtConta.PasswordChar = false;
            this.txtConta.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtConta.PlaceholderText = "";
            this.txtConta.ReadOnly = false;
            this.txtConta.Size = new System.Drawing.Size(169, 44);
            this.txtConta.TabIndex = 8;
            this.txtConta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtConta.Texts = "";
            this.txtConta.UnderlinedStyle = false;
            // 
            // txtAgencia
            // 
            this.txtAgencia.BackColor = System.Drawing.Color.White;
            this.txtAgencia.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtAgencia.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtAgencia.BorderRadius = 8;
            this.txtAgencia.BorderSize = 2;
            this.txtAgencia.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAgencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtAgencia.Location = new System.Drawing.Point(18, 259);
            this.txtAgencia.Margin = new System.Windows.Forms.Padding(4);
            this.txtAgencia.Multiline = false;
            this.txtAgencia.Name = "txtAgencia";
            this.txtAgencia.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtAgencia.PasswordChar = false;
            this.txtAgencia.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtAgencia.PlaceholderText = "";
            this.txtAgencia.ReadOnly = false;
            this.txtAgencia.Size = new System.Drawing.Size(118, 44);
            this.txtAgencia.TabIndex = 7;
            this.txtAgencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAgencia.Texts = "";
            this.txtAgencia.UnderlinedStyle = false;
            // 
            // txtCodBanco
            // 
            this.txtCodBanco.BackColor = System.Drawing.Color.White;
            this.txtCodBanco.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodBanco.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodBanco.BorderRadius = 8;
            this.txtCodBanco.BorderSize = 2;
            this.txtCodBanco.Enabled = false;
            this.txtCodBanco.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodBanco.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodBanco.Location = new System.Drawing.Point(533, 202);
            this.txtCodBanco.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodBanco.Multiline = false;
            this.txtCodBanco.Name = "txtCodBanco";
            this.txtCodBanco.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodBanco.PasswordChar = false;
            this.txtCodBanco.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodBanco.PlaceholderText = "";
            this.txtCodBanco.ReadOnly = false;
            this.txtCodBanco.Size = new System.Drawing.Size(98, 44);
            this.txtCodBanco.TabIndex = 6;
            this.txtCodBanco.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodBanco.Texts = "";
            this.txtCodBanco.UnderlinedStyle = false;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnConfirmar.Location = new System.Drawing.Point(175, 556);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(289, 45);
            this.btnConfirmar.TabIndex = 14;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.White;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // txtValor
            // 
            this.txtValor.BackColor = System.Drawing.Color.White;
            this.txtValor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderRadius = 8;
            this.txtValor.BorderSize = 2;
            this.txtValor.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtValor.Location = new System.Drawing.Point(18, 147);
            this.txtValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtValor.Multiline = false;
            this.txtValor.Name = "txtValor";
            this.txtValor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValor.PasswordChar = false;
            this.txtValor.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtValor.PlaceholderText = "";
            this.txtValor.ReadOnly = false;
            this.txtValor.Size = new System.Drawing.Size(165, 44);
            this.txtValor.TabIndex = 0;
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValor.Texts = "";
            this.txtValor.UnderlinedStyle = false;
            // 
            // txtBanco
            // 
            this.txtBanco.BackColor = System.Drawing.Color.White;
            this.txtBanco.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtBanco.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtBanco.BorderRadius = 8;
            this.txtBanco.BorderSize = 2;
            this.txtBanco.Enabled = false;
            this.txtBanco.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBanco.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtBanco.Location = new System.Drawing.Point(18, 202);
            this.txtBanco.Margin = new System.Windows.Forms.Padding(4);
            this.txtBanco.Multiline = false;
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtBanco.PasswordChar = false;
            this.txtBanco.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtBanco.PlaceholderText = "";
            this.txtBanco.ReadOnly = false;
            this.txtBanco.Size = new System.Drawing.Size(465, 44);
            this.txtBanco.TabIndex = 4;
            this.txtBanco.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBanco.Texts = "";
            this.txtBanco.UnderlinedStyle = false;
            this.txtBanco.Click += new System.EventHandler(this.txtBanco_Click);
            // 
            // FrmCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(638, 613);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.btnConfirmarCheques);
            this.Controls.Add(this.autoLabel11);
            this.Controls.Add(this.txtParcelas);
            this.Controls.Add(this.gridParcelas);
            this.Controls.Add(this.autoLabel9);
            this.Controls.Add(this.autoLabel8);
            this.Controls.Add(this.txtRazaoSocial);
            this.Controls.Add(this.autoLabel7);
            this.Controls.Add(this.txtCpf);
            this.Controls.Add(this.autoLabel6);
            this.Controls.Add(this.txtNumeroCheque);
            this.Controls.Add(this.autoLabel5);
            this.Controls.Add(this.txtDvConta);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.txtConta);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.txtAgencia);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.txtCodBanco);
            this.Controls.Add(this.btnPesquisaBanco);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtDataVencimento);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.autoLabel10);
            this.Controls.Add(this.txtBanco);
            this.Controls.Add(this.lblFaltante);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmCheque";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCheque";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmCheque_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCheque_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gridParcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsParcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Parcelas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFaltante;
        private System.Windows.Forms.Label label1;
        private RJ_UI.Classes.RJTextBox txtValor;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel10;
        private RJ_UI.Classes.RJTextBox txtBanco;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataVencimento;
        private RJ_UI.Classes.RJButton btnConfirmar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private RJ_UI.Classes.RJTextBox txtCodBanco;
        private FontAwesome.Sharp.IconButton btnPesquisaBanco;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private RJ_UI.Classes.RJTextBox txtAgencia;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private RJ_UI.Classes.RJTextBox txtConta;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private RJ_UI.Classes.RJTextBox txtDvConta;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private RJ_UI.Classes.RJTextBox txtNumeroCheque;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel7;
        private RJ_UI.Classes.RJTextBox txtCpf;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel8;
        private RJ_UI.Classes.RJTextBox txtRazaoSocial;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel9;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridParcelas;
        private System.Data.DataSet dsParcelas;
        private System.Data.DataTable Parcelas;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel11;
        private RJ_UI.Classes.RJTextBox txtParcelas;
        private FontAwesome.Sharp.IconButton btnConfirmarCheques;
        private System.Windows.Forms.Button btnFechar;
    }
}