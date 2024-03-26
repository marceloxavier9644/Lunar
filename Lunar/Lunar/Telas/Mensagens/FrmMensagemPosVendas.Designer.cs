namespace Lunar.Telas.Mensagens
{
    partial class FrmMensagemPosVendas
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
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn1 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn1 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn2 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn3 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn4 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridCheckBoxColumn gridCheckBoxColumn1 = new Syncfusion.WinForms.DataGrid.GridCheckBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPesquisar = new FontAwesome.Sharp.IconButton();
            this.txtDataInicial = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataFinal = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel15 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtRegistroPorPagina = new Lunar.RJ_UI.Classes.RJTextBox();
            this.sfDataPager1 = new Syncfusion.WinForms.DataPager.SfDataPager();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExcluir = new Lunar.RJ_UI.Classes.RJButton();
            this.btnExportarPDF = new FontAwesome.Sharp.IconButton();
            this.btnExportarExcel = new FontAwesome.Sharp.IconButton();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPesquisar);
            this.groupBox1.Controls.Add(this.txtDataInicial);
            this.groupBox1.Controls.Add(this.autoLabel4);
            this.groupBox1.Controls.Add(this.txtDataFinal);
            this.groupBox1.Controls.Add(this.autoLabel3);
            this.groupBox1.Controls.Add(this.autoLabel15);
            this.groupBox1.Controls.Add(this.txtRegistroPorPagina);
            this.groupBox1.Controls.Add(this.sfDataPager1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(978, 116);
            this.groupBox1.TabIndex = 157;
            this.groupBox1.TabStop = false;
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisar.FlatAppearance.BorderSize = 0;
            this.btnPesquisar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisar.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisar.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisar.IconSize = 38;
            this.btnPesquisar.Location = new System.Drawing.Point(387, 56);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisar.TabIndex = 233;
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataInicial.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDataInicial.Location = new System.Drawing.Point(12, 55);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Size = new System.Drawing.Size(179, 35);
            this.txtDataInicial.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.TabIndex = 229;
            this.txtDataInicial.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(197, 36);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(68, 16);
            this.autoLabel4.TabIndex = 232;
            this.autoLabel4.Text = "Data Final";
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataFinal.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Location = new System.Drawing.Point(197, 55);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Size = new System.Drawing.Size(174, 35);
            this.txtDataFinal.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataFinal.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.TabIndex = 230;
            this.txtDataFinal.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            this.txtDataFinal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDataFinal_KeyDown);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(12, 38);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(73, 16);
            this.autoLabel3.TabIndex = 231;
            this.autoLabel3.Text = "Data Inicial";
            // 
            // autoLabel15
            // 
            this.autoLabel15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.autoLabel15.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel15.ForeColor = System.Drawing.Color.Black;
            this.autoLabel15.Location = new System.Drawing.Point(451, 33);
            this.autoLabel15.Name = "autoLabel15";
            this.autoLabel15.Size = new System.Drawing.Size(90, 16);
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
            this.txtRegistroPorPagina.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegistroPorPagina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtRegistroPorPagina.Location = new System.Drawing.Point(451, 53);
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
            // sfDataPager1
            // 
            this.sfDataPager1.AccessibleName = "DataPager";
            this.sfDataPager1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sfDataPager1.BackColor = System.Drawing.Color.White;
            this.sfDataPager1.CanOverrideStyle = true;
            this.sfDataPager1.Location = new System.Drawing.Point(566, 48);
            this.sfDataPager1.Name = "sfDataPager1";
            this.sfDataPager1.Size = new System.Drawing.Size(395, 48);
            this.sfDataPager1.TabIndex = 154;
            this.sfDataPager1.OnDemandLoading += new System.EventHandler<Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs>(this.sfDataPager1_OnDemandLoading);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExcluir);
            this.groupBox2.Controls.Add(this.btnExportarPDF);
            this.groupBox2.Controls.Add(this.btnExportarExcel);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 374);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(978, 76);
            this.groupBox2.TabIndex = 162;
            this.groupBox2.TabStop = false;
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
            this.btnExcluir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExcluir.FlatAppearance.BorderSize = 2;
            this.btnExcluir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnExcluir.Location = new System.Drawing.Point(796, 19);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(165, 45);
            this.btnExcluir.TabIndex = 219;
            this.btnExcluir.Text = "Excluir Mensagem";
            this.btnExcluir.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
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
            // grid
            // 
            this.grid.AccessibleName = "Table";
            this.grid.AllowEditing = false;
            this.grid.AllowFiltering = true;
            this.grid.AllowStandardTab = true;
            this.grid.AutoGenerateColumns = false;
            this.grid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.ColumnHeader;
            this.grid.BackColor = System.Drawing.Color.White;
            gridNumericColumn1.AllowEditing = false;
            gridNumericColumn1.AllowFiltering = true;
            gridNumericColumn1.AllowResizing = true;
            gridNumericColumn1.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridNumericColumn1.CellStyle.Font.Size = 12F;
            gridNumericColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridNumericColumn1.Format = "0";
            gridNumericColumn1.HeaderStyle.Font.Size = 12F;
            gridNumericColumn1.HeaderText = "Código";
            gridNumericColumn1.MappingName = "Id";
            gridNumericColumn1.MaxValue = 1E+16D;
            gridNumericColumn1.MinValue = 0D;
            gridNumericColumn1.NullDisplayText = "0";
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowFiltering = true;
            gridTextColumn1.AllowResizing = true;
            gridTextColumn1.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn1.CellStyle.Font.Size = 12F;
            gridTextColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            gridTextColumn1.HeaderStyle.Font.Size = 12F;
            gridTextColumn1.HeaderText = "Cliente";
            gridTextColumn1.MappingName = "Pessoa.RazaoSocial";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.ColumnHeader;
            gridTextColumn2.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn2.HeaderStyle.Font.Size = 12F;
            gridTextColumn2.HeaderText = "Data Agendada";
            gridTextColumn2.MappingName = "DataAgendamento";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.CellStyle.Font.Size = 12F;
            gridTextColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderStyle.Font.Size = 12F;
            gridTextColumn3.HeaderText = "DDD";
            gridTextColumn3.MappingName = "Pessoa.PessoaTelefone.Ddd";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.AllowResizing = true;
            gridTextColumn4.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn4.CellStyle.Font.Size = 12F;
            gridTextColumn4.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            gridTextColumn4.HeaderStyle.Font.Size = 12F;
            gridTextColumn4.HeaderText = "Telefone";
            gridTextColumn4.MappingName = "Pessoa.PessoaTelefone.Telefone";
            gridCheckBoxColumn1.AllowEditing = false;
            gridCheckBoxColumn1.AllowFiltering = true;
            gridCheckBoxColumn1.CellStyle.Font.Size = 12F;
            gridCheckBoxColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridCheckBoxColumn1.HeaderStyle.Font.Size = 12F;
            gridCheckBoxColumn1.HeaderText = "Enviada?";
            gridCheckBoxColumn1.MappingName = "FlagEnviada";
            this.grid.Columns.Add(gridNumericColumn1);
            this.grid.Columns.Add(gridTextColumn1);
            this.grid.Columns.Add(gridTextColumn2);
            this.grid.Columns.Add(gridTextColumn3);
            this.grid.Columns.Add(gridTextColumn4);
            this.grid.Columns.Add(gridCheckBoxColumn1);
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid.Location = new System.Drawing.Point(0, 116);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(978, 258);
            this.grid.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.grid.Style.CellStyle.Font.Facename = "Microsoft Sans Serif";
            this.grid.Style.CellStyle.Font.Size = 9F;
            this.grid.TabIndex = 163;
            this.grid.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.grid_QueryRowStyle);
            // 
            // FrmMensagemPosVendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(978, 450);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmMensagemPosVendas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mensagens Agendadas";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel15;
        private RJ_UI.Classes.RJTextBox txtRegistroPorPagina;
        private Syncfusion.WinForms.DataPager.SfDataPager sfDataPager1;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataInicial;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataFinal;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private RJ_UI.Classes.RJButton btnExcluir;
        private FontAwesome.Sharp.IconButton btnExportarPDF;
        private FontAwesome.Sharp.IconButton btnExportarExcel;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
        private FontAwesome.Sharp.IconButton btnPesquisar;
    }
}