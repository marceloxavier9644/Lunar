namespace Lunar.Telas.IntegracoesBancarias
{
    partial class FrmRetornoBoleto
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
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn5 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn6 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn2 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn3 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn4 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn5 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn6 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn7 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn7 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn8 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRetornoBoleto));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPesquisar = new FontAwesome.Sharp.IconButton();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataAberturaInicial = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataAberturaFinal = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.btnImprimir = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Controls.Add(this.btnPesquisar);
            this.panel1.Controls.Add(this.autoLabel1);
            this.panel1.Controls.Add(this.txtDataAberturaInicial);
            this.panel1.Controls.Add(this.autoLabel4);
            this.panel1.Controls.Add(this.txtDataAberturaFinal);
            this.panel1.Controls.Add(this.autoLabel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 134);
            this.panel1.TabIndex = 0;
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
            this.btnPesquisar.Location = new System.Drawing.Point(377, 79);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisar.TabIndex = 255;
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(12, 25);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(278, 24);
            this.autoLabel1.TabIndex = 233;
            this.autoLabel1.Text = "Selecione uma Data de Retorno";
            // 
            // txtDataAberturaInicial
            // 
            this.txtDataAberturaInicial.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataAberturaInicial.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataAberturaInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDataAberturaInicial.Location = new System.Drawing.Point(12, 78);
            this.txtDataAberturaInicial.Name = "txtDataAberturaInicial";
            this.txtDataAberturaInicial.Size = new System.Drawing.Size(179, 35);
            this.txtDataAberturaInicial.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataAberturaInicial.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataAberturaInicial.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataAberturaInicial.TabIndex = 229;
            this.txtDataAberturaInicial.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(197, 59);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(68, 16);
            this.autoLabel4.TabIndex = 232;
            this.autoLabel4.Text = "Data Final";
            // 
            // txtDataAberturaFinal
            // 
            this.txtDataAberturaFinal.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataAberturaFinal.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataAberturaFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataAberturaFinal.Location = new System.Drawing.Point(197, 78);
            this.txtDataAberturaFinal.Name = "txtDataAberturaFinal";
            this.txtDataAberturaFinal.Size = new System.Drawing.Size(174, 35);
            this.txtDataAberturaFinal.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataAberturaFinal.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataAberturaFinal.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataAberturaFinal.TabIndex = 230;
            this.txtDataAberturaFinal.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(12, 61);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(73, 16);
            this.autoLabel3.TabIndex = 231;
            this.autoLabel3.Text = "Data Inicial";
            // 
            // grid
            // 
            this.grid.AccessibleName = "Table";
            this.grid.AllowDraggingColumns = true;
            this.grid.AllowEditing = false;
            this.grid.AllowFiltering = true;
            this.grid.AllowResizingColumns = true;
            this.grid.AllowStandardTab = true;
            this.grid.AutoGenerateColumns = false;
            this.grid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            this.grid.BackColor = System.Drawing.Color.White;
            gridNumericColumn1.AllowDragging = true;
            gridNumericColumn1.AllowEditing = false;
            gridNumericColumn1.AllowFiltering = true;
            gridNumericColumn1.AllowResizing = true;
            gridNumericColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridNumericColumn1.Format = "N";
            gridNumericColumn1.HeaderText = "ID";
            gridNumericColumn1.MappingName = "Id";
            gridTextColumn1.AllowDragging = true;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowFiltering = true;
            gridTextColumn1.AllowResizing = true;
            gridTextColumn1.HeaderText = "Cliente";
            gridTextColumn1.MappingName = "ContaReceber.Cliente.RazaoSocial";
            gridTextColumn2.AllowDragging = true;
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.HeaderText = "Cooperativa";
            gridTextColumn2.MappingName = "Cooperativa";
            gridTextColumn3.AllowDragging = true;
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.AllowResizing = true;
            gridTextColumn3.HeaderText = "Cód.Beneficiário";
            gridTextColumn3.MappingName = "CodigoBeneficiario";
            gridTextColumn4.AllowDragging = true;
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.AllowResizing = true;
            gridTextColumn4.HeaderText = "Nosso Numero";
            gridTextColumn4.MappingName = "NossoNumero";
            gridTextColumn5.AllowDragging = true;
            gridTextColumn5.AllowEditing = false;
            gridTextColumn5.AllowFiltering = true;
            gridTextColumn5.AllowResizing = true;
            gridTextColumn5.HeaderText = "Seu Numero";
            gridTextColumn5.MappingName = "SeuNumero";
            gridTextColumn6.AllowDragging = true;
            gridTextColumn6.AllowEditing = false;
            gridTextColumn6.AllowFiltering = true;
            gridTextColumn6.AllowResizing = true;
            gridTextColumn6.Format = "dd/MM/yyyy";
            gridTextColumn6.HeaderText = "Data Pagamento";
            gridTextColumn6.MappingName = "DataPagamento";
            gridNumericColumn2.AllowDragging = true;
            gridNumericColumn2.AllowEditing = false;
            gridNumericColumn2.AllowFiltering = true;
            gridNumericColumn2.AllowResizing = true;
            gridNumericColumn2.Format = "C";
            gridNumericColumn2.HeaderText = "Valor";
            gridNumericColumn2.MappingName = "Valor";
            gridNumericColumn3.AllowDragging = true;
            gridNumericColumn3.AllowEditing = false;
            gridNumericColumn3.AllowFiltering = true;
            gridNumericColumn3.AllowResizing = true;
            gridNumericColumn3.Format = "C";
            gridNumericColumn3.HeaderText = "Valor Liquidado";
            gridNumericColumn3.MappingName = "ValorLiquidado";
            gridNumericColumn4.AllowDragging = true;
            gridNumericColumn4.AllowEditing = false;
            gridNumericColumn4.AllowFiltering = true;
            gridNumericColumn4.AllowResizing = true;
            gridNumericColumn4.Format = "C";
            gridNumericColumn4.HeaderText = "Juros Liquido";
            gridNumericColumn4.MappingName = "JurosLiquido";
            gridNumericColumn5.AllowDragging = true;
            gridNumericColumn5.AllowEditing = false;
            gridNumericColumn5.AllowFiltering = true;
            gridNumericColumn5.AllowResizing = true;
            gridNumericColumn5.Format = "C";
            gridNumericColumn5.HeaderText = "Desconto Liquido";
            gridNumericColumn5.MappingName = "DescontoLiquido";
            gridNumericColumn6.AllowDragging = true;
            gridNumericColumn6.AllowEditing = false;
            gridNumericColumn6.AllowFiltering = true;
            gridNumericColumn6.AllowResizing = true;
            gridNumericColumn6.Format = "C";
            gridNumericColumn6.HeaderText = "Multa Liquida";
            gridNumericColumn6.MappingName = "MultaLiquida";
            gridNumericColumn7.AllowDragging = true;
            gridNumericColumn7.AllowEditing = false;
            gridNumericColumn7.AllowFiltering = true;
            gridNumericColumn7.AllowResizing = true;
            gridNumericColumn7.Format = "C";
            gridNumericColumn7.HeaderText = "Abatimento Liquido";
            gridNumericColumn7.MappingName = "AbatimentoLiquido";
            gridTextColumn7.AllowDragging = true;
            gridTextColumn7.AllowEditing = false;
            gridTextColumn7.AllowFiltering = true;
            gridTextColumn7.AllowResizing = true;
            gridTextColumn7.HeaderText = "Descrição";
            gridTextColumn7.MappingName = "Descricao";
            gridTextColumn8.AllowDragging = true;
            gridTextColumn8.AllowEditing = false;
            gridTextColumn8.AllowFiltering = true;
            gridTextColumn8.AllowResizing = true;
            gridTextColumn8.HeaderText = "ID Cliente";
            gridTextColumn8.MappingName = "ContaReceber.Cliente.Id";
            this.grid.Columns.Add(gridNumericColumn1);
            this.grid.Columns.Add(gridTextColumn1);
            this.grid.Columns.Add(gridTextColumn2);
            this.grid.Columns.Add(gridTextColumn3);
            this.grid.Columns.Add(gridTextColumn4);
            this.grid.Columns.Add(gridTextColumn5);
            this.grid.Columns.Add(gridTextColumn6);
            this.grid.Columns.Add(gridNumericColumn2);
            this.grid.Columns.Add(gridNumericColumn3);
            this.grid.Columns.Add(gridNumericColumn4);
            this.grid.Columns.Add(gridNumericColumn5);
            this.grid.Columns.Add(gridNumericColumn6);
            this.grid.Columns.Add(gridNumericColumn7);
            this.grid.Columns.Add(gridTextColumn7);
            this.grid.Columns.Add(gridTextColumn8);
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid.Location = new System.Drawing.Point(0, 134);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(800, 316);
            this.grid.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.grid.Style.CellStyle.Font.Facename = "Montserrat";
            this.grid.Style.CellStyle.Font.Size = 12F;
            this.grid.TabIndex = 3;
            this.grid.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.grid_QueryRowStyle);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnImprimir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnImprimir.IconColor = System.Drawing.Color.SlateGray;
            this.btnImprimir.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnImprimir.IconSize = 38;
            this.btnImprimir.Location = new System.Drawing.Point(419, 79);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(45, 34);
            this.btnImprimir.TabIndex = 256;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrmRetornoBoleto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmRetornoBoleto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Retorno de Boletos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataAberturaInicial;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataAberturaFinal;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private FontAwesome.Sharp.IconButton btnPesquisar;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
        private FontAwesome.Sharp.IconButton btnImprimir;
    }
}