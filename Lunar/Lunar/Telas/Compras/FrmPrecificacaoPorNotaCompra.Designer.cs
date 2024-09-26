namespace Lunar.Telas.Compras
{
    partial class FrmPrecificacaoPorNotaCompra
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
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn7 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn8 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrecificacaoPorNotaCompra));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkArredondarCentavos = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.txtCentavos = new System.Windows.Forms.TextBox();
            this.btnAplicar = new MaterialSkin.Controls.MaterialButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMarkup = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridProdutos = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.btnConfirmar = new MaterialSkin.Controls.MaterialButton();
            this.btnImprimir = new FontAwesome.Sharp.IconButton();
            this.chkImprimirCusto = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.chkImprimirReferencia = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkArredondarCentavos)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProdutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImprimirCusto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImprimirReferencia)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkArredondarCentavos);
            this.panel1.Controls.Add(this.txtCentavos);
            this.panel1.Controls.Add(this.btnAplicar);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMarkup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(948, 138);
            this.panel1.TabIndex = 0;
            // 
            // chkArredondarCentavos
            // 
            this.chkArredondarCentavos.BeforeTouchSize = new System.Drawing.Size(197, 30);
            this.chkArredondarCentavos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkArredondarCentavos.Location = new System.Drawing.Point(16, 64);
            this.chkArredondarCentavos.Name = "chkArredondarCentavos";
            this.chkArredondarCentavos.Size = new System.Drawing.Size(197, 30);
            this.chkArredondarCentavos.TabIndex = 1;
            this.chkArredondarCentavos.Text = "Arredondar Centavos";
            this.chkArredondarCentavos.CheckStateChanged += new System.EventHandler(this.chkArredondarCentavos_CheckStateChanged);
            // 
            // txtCentavos
            // 
            this.txtCentavos.Enabled = false;
            this.txtCentavos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCentavos.Location = new System.Drawing.Point(16, 98);
            this.txtCentavos.Name = "txtCentavos";
            this.txtCentavos.Size = new System.Drawing.Size(197, 26);
            this.txtCentavos.TabIndex = 2;
            this.txtCentavos.Text = "0,00";
            this.txtCentavos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnAplicar
            // 
            this.btnAplicar.AutoSize = false;
            this.btnAplicar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAplicar.Depth = 0;
            this.btnAplicar.DrawShadows = true;
            this.btnAplicar.HighEmphasis = true;
            this.btnAplicar.Icon = null;
            this.btnAplicar.Location = new System.Drawing.Point(220, 32);
            this.btnAplicar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnAplicar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(82, 26);
            this.btnAplicar.TabIndex = 3;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnAplicar.UseAccentColor = false;
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Markup %";
            // 
            // txtMarkup
            // 
            this.txtMarkup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarkup.Location = new System.Drawing.Point(16, 32);
            this.txtMarkup.Name = "txtMarkup";
            this.txtMarkup.Size = new System.Drawing.Size(197, 26);
            this.txtMarkup.TabIndex = 0;
            this.txtMarkup.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkImprimirReferencia);
            this.panel2.Controls.Add(this.chkImprimirCusto);
            this.panel2.Controls.Add(this.btnImprimir);
            this.panel2.Controls.Add(this.btnConfirmar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 364);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(948, 86);
            this.panel2.TabIndex = 1;
            // 
            // gridProdutos
            // 
            this.gridProdutos.AccessibleName = "Table";
            this.gridProdutos.AllowFiltering = true;
            this.gridProdutos.AllowResizingColumns = true;
            this.gridProdutos.AllowResizingHiddenColumns = true;
            this.gridProdutos.AllowSorting = false;
            this.gridProdutos.AutoGenerateColumns = false;
            this.gridProdutos.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowFiltering = true;
            gridTextColumn1.AllowResizing = true;
            gridTextColumn1.AllowSorting = false;
            gridTextColumn1.CellStyle.Font.Size = 12F;
            gridTextColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderStyle.Font.Size = 12F;
            gridTextColumn1.HeaderText = "Cód.";
            gridTextColumn1.MappingName = "CodigoInterno";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.AllowSorting = false;
            gridTextColumn2.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn2.HeaderStyle.Font.Size = 12F;
            gridTextColumn2.HeaderText = "Descrição";
            gridTextColumn2.MappingName = "DescricaoInterna";
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.AllowResizing = true;
            gridTextColumn3.AllowSorting = false;
            gridTextColumn3.CellStyle.Font.Size = 12F;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderStyle.Font.Size = 12F;
            gridTextColumn3.HeaderText = "Ref.";
            gridTextColumn3.MappingName = "Produto.Referencia";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.AllowResizing = true;
            gridTextColumn4.AllowSorting = false;
            gridTextColumn4.CellStyle.Font.Size = 12F;
            gridTextColumn4.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn4.CellStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn4.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn4.HeaderStyle.Font.Size = 12F;
            gridTextColumn4.HeaderText = "CFOP";
            gridTextColumn4.MappingName = "CfopEntrada";
            gridTextColumn4.ValidationMode = Syncfusion.WinForms.DataGrid.Enums.GridValidationMode.InEdit;
            gridTextColumn5.AllowEditing = false;
            gridTextColumn5.AllowFiltering = true;
            gridTextColumn5.AllowResizing = true;
            gridTextColumn5.AllowSorting = false;
            gridTextColumn5.CellStyle.Font.Size = 12F;
            gridTextColumn5.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn5.HeaderStyle.Font.Size = 12F;
            gridTextColumn5.HeaderText = "Qtd";
            gridTextColumn5.MappingName = "QuantidadeEntrada";
            gridTextColumn6.AllowEditing = false;
            gridTextColumn6.AllowFiltering = true;
            gridTextColumn6.AllowResizing = true;
            gridTextColumn6.AllowSorting = false;
            gridTextColumn6.CellStyle.Font.Size = 12F;
            gridTextColumn6.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridTextColumn6.Format = "N3";
            gridTextColumn6.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn6.HeaderStyle.Font.Size = 12F;
            gridTextColumn6.HeaderText = "Valor Unitário";
            gridTextColumn6.MappingName = "VUnCom";
            gridTextColumn7.AllowFiltering = true;
            gridTextColumn7.AllowResizing = true;
            gridTextColumn7.AllowSorting = false;
            gridTextColumn7.CellStyle.Font.Size = 12F;
            gridTextColumn7.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn7.Format = "n5";
            gridTextColumn7.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn7.HeaderStyle.Font.Size = 12F;
            gridTextColumn7.HeaderText = "Markup";
            gridTextColumn7.MappingName = "Produto.Markup";
            gridTextColumn8.AllowFiltering = true;
            gridTextColumn8.AllowResizing = true;
            gridTextColumn8.AllowSorting = false;
            gridTextColumn8.CellStyle.Font.Size = 12F;
            gridTextColumn8.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridTextColumn8.Format = "N2";
            gridTextColumn8.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn8.HeaderStyle.Font.Size = 12F;
            gridTextColumn8.HeaderText = "Valor Venda";
            gridTextColumn8.MappingName = "Produto.ValorVenda";
            this.gridProdutos.Columns.Add(gridTextColumn1);
            this.gridProdutos.Columns.Add(gridTextColumn2);
            this.gridProdutos.Columns.Add(gridTextColumn3);
            this.gridProdutos.Columns.Add(gridTextColumn4);
            this.gridProdutos.Columns.Add(gridTextColumn5);
            this.gridProdutos.Columns.Add(gridTextColumn6);
            this.gridProdutos.Columns.Add(gridTextColumn7);
            this.gridProdutos.Columns.Add(gridTextColumn8);
            this.gridProdutos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProdutos.Location = new System.Drawing.Point(0, 138);
            this.gridProdutos.Name = "gridProdutos";
            this.gridProdutos.SelectionUnit = Syncfusion.WinForms.DataGrid.Enums.SelectionUnit.Cell;
            this.gridProdutos.Size = new System.Drawing.Size(948, 226);
            this.gridProdutos.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.gridProdutos.TabIndex = 243;
            this.gridProdutos.Text = "sfDataGrid1";
            this.gridProdutos.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.gridProdutos_QueryRowStyle);
            this.gridProdutos.CurrentCellEndEdit += new Syncfusion.WinForms.DataGrid.Events.CurrentCellEndEditEventHandler(this.gridProdutos_CurrentCellEndEdit);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmar.AutoSize = false;
            this.btnConfirmar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConfirmar.Depth = 0;
            this.btnConfirmar.DrawShadows = true;
            this.btnConfirmar.HighEmphasis = true;
            this.btnConfirmar.Icon = null;
            this.btnConfirmar.Location = new System.Drawing.Point(807, 24);
            this.btnConfirmar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnConfirmar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(128, 36);
            this.btnConfirmar.TabIndex = 4;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnConfirmar.UseAccentColor = false;
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnImprimir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnImprimir.IconColor = System.Drawing.Color.RoyalBlue;
            this.btnImprimir.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnImprimir.IconSize = 38;
            this.btnImprimir.Location = new System.Drawing.Point(755, 26);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(45, 34);
            this.btnImprimir.TabIndex = 226;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // chkImprimirCusto
            // 
            this.chkImprimirCusto.BeforeTouchSize = new System.Drawing.Size(134, 30);
            this.chkImprimirCusto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkImprimirCusto.Location = new System.Drawing.Point(12, 6);
            this.chkImprimirCusto.Name = "chkImprimirCusto";
            this.chkImprimirCusto.Size = new System.Drawing.Size(134, 30);
            this.chkImprimirCusto.TabIndex = 228;
            this.chkImprimirCusto.Text = "Imprimir Custo";
            // 
            // chkImprimirReferencia
            // 
            this.chkImprimirReferencia.BeforeTouchSize = new System.Drawing.Size(179, 30);
            this.chkImprimirReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkImprimirReferencia.Location = new System.Drawing.Point(12, 42);
            this.chkImprimirReferencia.Name = "chkImprimirReferencia";
            this.chkImprimirReferencia.Size = new System.Drawing.Size(179, 30);
            this.chkImprimirReferencia.TabIndex = 229;
            this.chkImprimirReferencia.Text = "Imprimir Referência";
            // 
            // FrmPrecificacaoPorNotaCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(948, 450);
            this.Controls.Add(this.gridProdutos);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmPrecificacaoPorNotaCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Precificação de Produtos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkArredondarCentavos)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProdutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImprimirCusto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImprimirReferencia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMarkup;
        private System.Windows.Forms.Panel panel2;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridProdutos;
        private System.Windows.Forms.TextBox txtCentavos;
        private MaterialSkin.Controls.MaterialButton btnAplicar;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv chkArredondarCentavos;
        private MaterialSkin.Controls.MaterialButton btnConfirmar;
        private FontAwesome.Sharp.IconButton btnImprimir;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv chkImprimirCusto;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv chkImprimirReferencia;
    }
}