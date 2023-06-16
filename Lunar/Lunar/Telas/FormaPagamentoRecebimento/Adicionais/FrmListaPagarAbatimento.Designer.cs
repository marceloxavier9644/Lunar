namespace Lunar.Telas.FormaPagamentoRecebimento.Adicionais
{
    partial class FrmListaPagarAbatimento
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
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn1 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn2 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn3 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn4 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn5 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn6 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn4 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn5 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn7 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn6 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn8 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn9 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn10 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn7 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListaPagarAbatimento));
            this.btnPagar = new Lunar.RJ_UI.Classes.RJButton();
            this.grid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPagar
            // 
            this.btnPagar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPagar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnPagar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnPagar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnPagar.BorderRadius = 8;
            this.btnPagar.BorderSize = 0;
            this.btnPagar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPagar.FlatAppearance.BorderSize = 0;
            this.btnPagar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagar.ForeColor = System.Drawing.Color.Transparent;
            this.btnPagar.Location = new System.Drawing.Point(589, 305);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(199, 45);
            this.btnPagar.TabIndex = 253;
            this.btnPagar.Text = "Confirmar [F5]";
            this.btnPagar.TextColor = System.Drawing.Color.Transparent;
            this.btnPagar.UseVisualStyleBackColor = false;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
            // 
            // grid
            // 
            this.grid.AccessibleName = "Table";
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
            gridTextColumn1.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.ColumnHeader;
            gridTextColumn1.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn1.CellStyle.Font.Size = 12F;
            gridTextColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn1.Format = "dd/MM/yyyy";
            gridTextColumn1.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn1.HeaderStyle.Font.Size = 10F;
            gridTextColumn1.HeaderStyle.VerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            gridTextColumn1.HeaderText = "Emissão";
            gridTextColumn1.MappingName = "Data";
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn2.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn2.Format = "dd/MM/yyyy";
            gridTextColumn2.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn2.HeaderStyle.Font.Size = 10F;
            gridTextColumn2.HeaderText = "Venc.";
            gridTextColumn2.MappingName = "Vencimento";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.AllowResizing = true;
            gridTextColumn3.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn3.CellStyle.Font.Size = 12F;
            gridTextColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn3.HeaderStyle.Font.Size = 10F;
            gridTextColumn3.HeaderText = "Doc";
            gridTextColumn3.MappingName = "Documento";
            gridNumericColumn1.AllowEditing = false;
            gridNumericColumn1.AllowFiltering = true;
            gridNumericColumn1.AllowResizing = true;
            gridNumericColumn1.CellStyle.Font.Size = 12F;
            gridNumericColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridNumericColumn1.Format = "0";
            gridNumericColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn1.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn1.HeaderStyle.Font.Size = 10F;
            gridNumericColumn1.HeaderText = "Parcela";
            gridNumericColumn1.MappingName = "Parcela";
            gridNumericColumn2.AllowEditing = false;
            gridNumericColumn2.AllowFiltering = true;
            gridNumericColumn2.AllowResizing = true;
            gridNumericColumn2.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridNumericColumn2.CellStyle.Font.Size = 12F;
            gridNumericColumn2.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridNumericColumn2.Format = "N2";
            gridNumericColumn2.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn2.HeaderStyle.Font.Size = 10F;
            gridNumericColumn2.HeaderText = "Vr. Parcela";
            gridNumericColumn2.MappingName = "ValorParcela";
            gridNumericColumn3.AllowEditing = false;
            gridNumericColumn3.AllowFiltering = true;
            gridNumericColumn3.AllowResizing = true;
            gridNumericColumn3.CellStyle.Font.Size = 12F;
            gridNumericColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridNumericColumn3.Format = "N2";
            gridNumericColumn3.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn3.HeaderStyle.Font.Size = 10F;
            gridNumericColumn3.HeaderText = "Multa";
            gridNumericColumn3.MappingName = "Multa";
            gridNumericColumn4.AllowEditing = false;
            gridNumericColumn4.AllowFiltering = true;
            gridNumericColumn4.AllowResizing = true;
            gridNumericColumn4.CellStyle.Font.Size = 12F;
            gridNumericColumn4.Format = "N2";
            gridNumericColumn4.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn4.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn4.HeaderStyle.Font.Size = 10F;
            gridNumericColumn4.HeaderText = "Juro";
            gridNumericColumn4.MappingName = "Juro";
            gridNumericColumn5.AllowEditing = false;
            gridNumericColumn5.AllowFiltering = true;
            gridNumericColumn5.AllowResizing = true;
            gridNumericColumn5.CellStyle.Font.Size = 12F;
            gridNumericColumn5.Format = "N2";
            gridNumericColumn5.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn5.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn5.HeaderStyle.Font.Size = 10F;
            gridNumericColumn5.HeaderText = "- Parciais";
            gridNumericColumn5.MappingName = "ValorRecebimentoParcial";
            gridNumericColumn6.AllowEditing = false;
            gridNumericColumn6.AllowFiltering = true;
            gridNumericColumn6.AllowResizing = true;
            gridNumericColumn6.CellStyle.Font.Size = 12F;
            gridNumericColumn6.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridNumericColumn6.Format = "N2";
            gridNumericColumn6.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn6.HeaderStyle.Font.Size = 10F;
            gridNumericColumn6.HeaderText = "Vr. Total";
            gridNumericColumn6.MappingName = "ValorTotal";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.AllowResizing = true;
            gridTextColumn4.AllowSorting = false;
            gridTextColumn4.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn4.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn4.CellStyle.Font.Size = 12F;
            gridTextColumn4.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn4.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn4.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn4.HeaderStyle.Font.Size = 10F;
            gridTextColumn4.HeaderText = "CPF/CNPJ";
            gridTextColumn4.MappingName = "Cliente.Cnpj";
            gridTextColumn5.AllowEditing = false;
            gridTextColumn5.AllowFiltering = true;
            gridTextColumn5.AllowResizing = true;
            gridTextColumn5.CellStyle.Font.Size = 12F;
            gridTextColumn5.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn5.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn5.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn5.HeaderStyle.Font.Size = 10F;
            gridTextColumn5.HeaderText = "Cód.";
            gridTextColumn5.MappingName = "Cliente.Id";
            gridNumericColumn7.AllowEditing = false;
            gridNumericColumn7.AllowFiltering = true;
            gridNumericColumn7.AllowResizing = true;
            gridNumericColumn7.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridNumericColumn7.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridNumericColumn7.CellStyle.Font.Size = 12F;
            gridNumericColumn7.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            gridNumericColumn7.Format = "N";
            gridNumericColumn7.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn7.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn7.HeaderStyle.Font.Size = 10F;
            gridNumericColumn7.HeaderText = "Cliente";
            gridNumericColumn7.MappingName = "Cliente.RazaoSocial";
            gridTextColumn6.AllowEditing = false;
            gridTextColumn6.AllowFiltering = true;
            gridTextColumn6.AllowResizing = true;
            gridTextColumn6.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.ColumnHeader;
            gridTextColumn6.CellStyle.Font.Facename = "Microsoft Sans Serif";
            gridTextColumn6.CellStyle.Font.Size = 12F;
            gridTextColumn6.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            gridTextColumn6.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn6.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn6.HeaderStyle.Font.Size = 10F;
            gridTextColumn6.HeaderText = "FP Origem";
            gridTextColumn6.MappingName = "FormaPagamento.Descricao";
            gridNumericColumn8.AllowEditing = false;
            gridNumericColumn8.AllowFiltering = true;
            gridNumericColumn8.AllowResizing = true;
            gridNumericColumn8.CellStyle.Font.Size = 12F;
            gridNumericColumn8.Format = "N2";
            gridNumericColumn8.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn8.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn8.HeaderStyle.Font.Size = 10F;
            gridNumericColumn8.HeaderText = "Desconto Baixa";
            gridNumericColumn8.MappingName = "DescontoRecebidoBaixa";
            gridNumericColumn9.AllowEditing = false;
            gridNumericColumn9.AllowFiltering = true;
            gridNumericColumn9.AllowResizing = true;
            gridNumericColumn9.CellStyle.Font.Size = 12F;
            gridNumericColumn9.Format = "N2";
            gridNumericColumn9.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn9.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn9.HeaderStyle.Font.Size = 10F;
            gridNumericColumn9.HeaderText = "Acréscimo Baixa";
            gridNumericColumn9.MappingName = "AcrescimoRecebidoBaixa";
            gridNumericColumn10.AllowEditing = false;
            gridNumericColumn10.AllowFiltering = true;
            gridNumericColumn10.AllowResizing = true;
            gridNumericColumn10.CellStyle.Font.Size = 12F;
            gridNumericColumn10.Format = "N2";
            gridNumericColumn10.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridNumericColumn10.HeaderStyle.Font.Facename = "Montserrat";
            gridNumericColumn10.HeaderStyle.Font.Size = 10F;
            gridNumericColumn10.HeaderText = "Valor Recebido";
            gridNumericColumn10.MappingName = "ValorRecebido";
            gridTextColumn7.AllowEditing = false;
            gridTextColumn7.AllowFiltering = true;
            gridTextColumn7.AllowResizing = true;
            gridTextColumn7.CellStyle.Font.Size = 12F;
            gridTextColumn7.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn7.Format = "dd/MM/yyyy";
            gridTextColumn7.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn7.HeaderStyle.Font.Facename = "Montserrat";
            gridTextColumn7.HeaderStyle.Font.Size = 10F;
            gridTextColumn7.HeaderText = "Data Recebimento";
            gridTextColumn7.MappingName = "DataRecebimento";
            this.grid.Columns.Add(gridTextColumn1);
            this.grid.Columns.Add(gridTextColumn2);
            this.grid.Columns.Add(gridTextColumn3);
            this.grid.Columns.Add(gridNumericColumn1);
            this.grid.Columns.Add(gridNumericColumn2);
            this.grid.Columns.Add(gridNumericColumn3);
            this.grid.Columns.Add(gridNumericColumn4);
            this.grid.Columns.Add(gridNumericColumn5);
            this.grid.Columns.Add(gridNumericColumn6);
            this.grid.Columns.Add(gridTextColumn4);
            this.grid.Columns.Add(gridTextColumn5);
            this.grid.Columns.Add(gridNumericColumn7);
            this.grid.Columns.Add(gridTextColumn6);
            this.grid.Columns.Add(gridNumericColumn8);
            this.grid.Columns.Add(gridNumericColumn9);
            this.grid.Columns.Add(gridNumericColumn10);
            this.grid.Columns.Add(gridTextColumn7);
            this.grid.Location = new System.Drawing.Point(2, 2);
            this.grid.Name = "grid";
            this.grid.SelectionMode = Syncfusion.WinForms.DataGrid.Enums.GridSelectionMode.Extended;
            this.grid.Size = new System.Drawing.Size(786, 297);
            this.grid.Style.CellStyle.Font.Size = 10F;
            this.grid.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.grid.Style.HeaderStyle.Font.Size = 12F;
            this.grid.TabIndex = 254;
            this.grid.Text = "Grid Parcelas";
            // 
            // FrmListaPagarAbatimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(800, 362);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.btnPagar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "FrmListaPagarAbatimento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Abatimento";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmListaPagarAbatimento_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private RJ_UI.Classes.RJButton btnPagar;
        private Syncfusion.WinForms.DataGrid.SfDataGrid grid;
    }
}