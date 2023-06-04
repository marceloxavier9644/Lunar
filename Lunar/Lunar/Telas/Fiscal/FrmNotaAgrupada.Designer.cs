namespace Lunar.Telas.Fiscal
{
    partial class FrmNotaAgrupada
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
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn2 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn3 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn4 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn5 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn6 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNotaAgrupada));
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.lblTitulo = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioOrdemServico = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radioVenda = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataFinal = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.txtDataInicial = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.btnPesquisar = new FontAwesome.Sharp.IconButton();
            this.btnPesquisar2 = new Lunar.RJ_UI.Classes.RJButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gridOrdemServico = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.sfDataGrid1 = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.panelTitleBar.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioOrdemServico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioVenda)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrdemServico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sfDataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.lblTitulo);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(800, 44);
            this.panelTitleBar.TabIndex = 223;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(5, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(260, 25);
            this.lblTitulo.TabIndex = 198;
            this.lblTitulo.Text = "Emissão de Notas Fiscais";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPesquisar);
            this.groupBox1.Controls.Add(this.btnPesquisar2);
            this.groupBox1.Controls.Add(this.autoLabel4);
            this.groupBox1.Controls.Add(this.autoLabel3);
            this.groupBox1.Controls.Add(this.txtDataFinal);
            this.groupBox1.Controls.Add(this.txtDataInicial);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(5, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(395, 132);
            this.groupBox1.TabIndex = 224;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro";
            // 
            // radioOrdemServico
            // 
            this.radioOrdemServico.BeforeTouchSize = new System.Drawing.Size(148, 21);
            this.radioOrdemServico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioOrdemServico.Location = new System.Drawing.Point(6, 33);
            this.radioOrdemServico.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioOrdemServico.Name = "radioOrdemServico";
            this.radioOrdemServico.Size = new System.Drawing.Size(148, 21);
            this.radioOrdemServico.TabIndex = 0;
            this.radioOrdemServico.Text = " Ordem de Serviço";
            // 
            // radioVenda
            // 
            this.radioVenda.BeforeTouchSize = new System.Drawing.Size(124, 21);
            this.radioVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioVenda.Location = new System.Drawing.Point(6, 60);
            this.radioVenda.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioVenda.Name = "radioVenda";
            this.radioVenda.Size = new System.Drawing.Size(124, 21);
            this.radioVenda.TabIndex = 1;
            this.radioVenda.Text = " Venda";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioOrdemServico);
            this.groupBox2.Controls.Add(this.radioVenda);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(7, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 104);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selecione a Origem";
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(173, 67);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(74, 18);
            this.autoLabel4.TabIndex = 227;
            this.autoLabel4.Text = "Data Final";
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(173, 8);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(79, 18);
            this.autoLabel3.TabIndex = 226;
            this.autoLabel3.Text = "Data Inicial";
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataFinal.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Location = new System.Drawing.Point(173, 88);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Size = new System.Drawing.Size(132, 35);
            this.txtDataFinal.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataFinal.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.TabIndex = 225;
            this.txtDataFinal.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataInicial.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicial.Location = new System.Drawing.Point(173, 29);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Size = new System.Drawing.Size(132, 35);
            this.txtDataInicial.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataInicial.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.TabIndex = 224;
            this.txtDataInicial.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
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
            this.btnPesquisar.IconColor = System.Drawing.Color.SteelBlue;
            this.btnPesquisar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisar.IconSize = 38;
            this.btnPesquisar.Location = new System.Drawing.Point(336, 59);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(36, 36);
            this.btnPesquisar.TabIndex = 236;
            this.btnPesquisar.UseVisualStyleBackColor = true;
            // 
            // btnPesquisar2
            // 
            this.btnPesquisar2.BackColor = System.Drawing.Color.White;
            this.btnPesquisar2.BackgroundColor = System.Drawing.Color.White;
            this.btnPesquisar2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisar2.BorderRadius = 8;
            this.btnPesquisar2.BorderSize = 2;
            this.btnPesquisar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisar2.FlatAppearance.BorderSize = 0;
            this.btnPesquisar2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisar2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisar2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisar2.ForeColor = System.Drawing.Color.Black;
            this.btnPesquisar2.Location = new System.Drawing.Point(326, 50);
            this.btnPesquisar2.Name = "btnPesquisar2";
            this.btnPesquisar2.Size = new System.Drawing.Size(53, 50);
            this.btnPesquisar2.TabIndex = 237;
            this.btnPesquisar2.TextColor = System.Drawing.Color.Black;
            this.btnPesquisar2.UseVisualStyleBackColor = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gridOrdemServico);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(408, 48);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(392, 134);
            this.groupBox3.TabIndex = 225;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Vendas";
            // 
            // gridOrdemServico
            // 
            this.gridOrdemServico.AccessibleName = "Table";
            gridTextColumn1.HeaderText = "Nº O.S";
            gridTextColumn1.MappingName = "Id";
            gridNumericColumn1.Format = "N";
            gridNumericColumn1.HeaderText = "Valor Total";
            gridNumericColumn1.MappingName = "ValorTotal";
            gridTextColumn2.Format = "dd/MM/yyyy";
            gridTextColumn2.HeaderText = "Encerramento";
            gridTextColumn2.MappingName = "DataEncerramento";
            gridTextColumn3.HeaderText = "Cliente";
            gridTextColumn3.MappingName = "Cliente.RazaoSocial";
            this.gridOrdemServico.Columns.Add(gridTextColumn1);
            this.gridOrdemServico.Columns.Add(gridNumericColumn1);
            this.gridOrdemServico.Columns.Add(gridTextColumn2);
            this.gridOrdemServico.Columns.Add(gridTextColumn3);
            this.gridOrdemServico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOrdemServico.Location = new System.Drawing.Point(3, 20);
            this.gridOrdemServico.Name = "gridOrdemServico";
            this.gridOrdemServico.Size = new System.Drawing.Size(386, 111);
            this.gridOrdemServico.TabIndex = 0;
            this.gridOrdemServico.Text = "sfDataGrid1";
            // 
            // sfDataGrid1
            // 
            this.sfDataGrid1.AccessibleName = "Table";
            gridTextColumn4.HeaderText = "Cód Produto";
            gridTextColumn4.MappingName = "Produto.Id";
            gridTextColumn5.HeaderText = "Descrição";
            gridTextColumn5.MappingName = "DescricaoProduto";
            gridNumericColumn2.Format = "N";
            gridNumericColumn2.HeaderText = "Valor Unitário";
            gridNumericColumn2.MappingName = "ValorUnitario";
            gridNumericColumn3.Format = "0";
            gridNumericColumn3.HeaderText = "Quantidade";
            gridNumericColumn3.MappingName = "Quantidade";
            gridNumericColumn4.Format = "N";
            gridNumericColumn4.HeaderText = "Desconto";
            gridNumericColumn4.MappingName = "Desconto";
            gridNumericColumn5.Format = "N";
            gridNumericColumn5.HeaderText = "Valor Total";
            gridNumericColumn5.MappingName = "ValorTotal";
            gridNumericColumn6.Format = "0";
            gridNumericColumn6.HeaderText = "Estoque";
            gridNumericColumn6.MappingName = "Produto.Estoque";
            this.sfDataGrid1.Columns.Add(gridTextColumn4);
            this.sfDataGrid1.Columns.Add(gridTextColumn5);
            this.sfDataGrid1.Columns.Add(gridNumericColumn2);
            this.sfDataGrid1.Columns.Add(gridNumericColumn3);
            this.sfDataGrid1.Columns.Add(gridNumericColumn4);
            this.sfDataGrid1.Columns.Add(gridNumericColumn5);
            this.sfDataGrid1.Columns.Add(gridNumericColumn6);
            this.sfDataGrid1.Location = new System.Drawing.Point(5, 201);
            this.sfDataGrid1.Name = "sfDataGrid1";
            this.sfDataGrid1.Size = new System.Drawing.Size(792, 111);
            this.sfDataGrid1.TabIndex = 226;
            this.sfDataGrid1.Text = "sfDataGrid1";
            // 
            // FrmNotaAgrupada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sfDataGrid1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmNotaAgrupada";
            this.Text = "FrmNotaAgrupada";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioOrdemServico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioVenda)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOrdemServico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sfDataGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblTitulo;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioOrdemServico;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioVenda;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataFinal;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataInicial;
        private FontAwesome.Sharp.IconButton btnPesquisar;
        private RJ_UI.Classes.RJButton btnPesquisar2;
        private System.Windows.Forms.GroupBox groupBox3;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridOrdemServico;
        private Syncfusion.WinForms.DataGrid.SfDataGrid sfDataGrid1;
    }
}