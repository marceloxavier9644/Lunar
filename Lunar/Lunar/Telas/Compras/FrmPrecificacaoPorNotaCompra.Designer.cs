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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrecificacaoPorNotaCompra));
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridProdutos = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.materialButton1 = new MaterialSkin.Controls.MaterialButton();
            this.txtCentavos = new System.Windows.Forms.TextBox();
            this.checkBoxAdv1 = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProdutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxAdv1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxAdv1);
            this.panel1.Controls.Add(this.txtCentavos);
            this.panel1.Controls.Add(this.materialButton1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(948, 138);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(16, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(197, 26);
            this.textBox1.TabIndex = 0;
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
            // panel2
            // 
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
            gridTextColumn1.HeaderText = "Cód. Sistema";
            gridTextColumn1.MappingName = "CodigoInterno";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.AllowSorting = false;
            gridTextColumn2.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn2.HeaderStyle.Font.Size = 12F;
            gridTextColumn2.HeaderText = "Descrição Sistema";
            gridTextColumn2.MappingName = "DescricaoInterna";
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.AllowResizing = true;
            gridTextColumn3.AllowSorting = false;
            gridTextColumn3.CellStyle.Font.Size = 12F;
            gridTextColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn3.CellStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderStyle.Font.Size = 12F;
            gridTextColumn3.HeaderText = "CFOP de Entrada";
            gridTextColumn3.MappingName = "CfopEntrada";
            gridTextColumn3.ValidationMode = Syncfusion.WinForms.DataGrid.Enums.GridValidationMode.InEdit;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.AllowResizing = true;
            gridTextColumn4.AllowSorting = false;
            gridTextColumn4.CellStyle.Font.Size = 12F;
            gridTextColumn4.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn4.HeaderStyle.Font.Size = 12F;
            gridTextColumn4.HeaderText = "Quantidade Entrada";
            gridTextColumn4.MappingName = "QuantidadeEntrada";
            gridTextColumn5.AllowEditing = false;
            gridTextColumn5.AllowFiltering = true;
            gridTextColumn5.AllowResizing = true;
            gridTextColumn5.AllowSorting = false;
            gridTextColumn5.CellStyle.Font.Size = 12F;
            gridTextColumn5.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridTextColumn5.Format = "N3";
            gridTextColumn5.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn5.HeaderStyle.Font.Size = 12F;
            gridTextColumn5.HeaderText = "Valor Unitário";
            gridTextColumn5.MappingName = "VUnCom";
            gridTextColumn6.AllowFiltering = true;
            gridTextColumn6.AllowResizing = true;
            gridTextColumn6.AllowSorting = false;
            gridTextColumn6.CellStyle.Font.Size = 12F;
            gridTextColumn6.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn6.Format = "n5";
            gridTextColumn6.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn6.HeaderStyle.Font.Size = 12F;
            gridTextColumn6.HeaderText = "Markup";
            gridTextColumn6.MappingName = "Produto.Markup";
            gridTextColumn7.AllowFiltering = true;
            gridTextColumn7.AllowResizing = true;
            gridTextColumn7.AllowSorting = false;
            gridTextColumn7.CellStyle.Font.Size = 12F;
            gridTextColumn7.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridTextColumn7.Format = "N2";
            gridTextColumn7.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn7.HeaderStyle.Font.Size = 12F;
            gridTextColumn7.HeaderText = "Valor Venda";
            gridTextColumn7.MappingName = "Produto.ValorVenda";
            this.gridProdutos.Columns.Add(gridTextColumn1);
            this.gridProdutos.Columns.Add(gridTextColumn2);
            this.gridProdutos.Columns.Add(gridTextColumn3);
            this.gridProdutos.Columns.Add(gridTextColumn4);
            this.gridProdutos.Columns.Add(gridTextColumn5);
            this.gridProdutos.Columns.Add(gridTextColumn6);
            this.gridProdutos.Columns.Add(gridTextColumn7);
            this.gridProdutos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProdutos.Location = new System.Drawing.Point(0, 138);
            this.gridProdutos.Name = "gridProdutos";
            this.gridProdutos.SelectionUnit = Syncfusion.WinForms.DataGrid.Enums.SelectionUnit.Cell;
            this.gridProdutos.Size = new System.Drawing.Size(948, 226);
            this.gridProdutos.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.gridProdutos.TabIndex = 243;
            this.gridProdutos.Text = "sfDataGrid1";
            this.gridProdutos.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.gridProdutos_QueryRowStyle);
            // 
            // materialButton1
            // 
            this.materialButton1.AutoSize = false;
            this.materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton1.Depth = 0;
            this.materialButton1.DrawShadows = true;
            this.materialButton1.HighEmphasis = true;
            this.materialButton1.Icon = null;
            this.materialButton1.Location = new System.Drawing.Point(220, 32);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.Size = new System.Drawing.Size(82, 26);
            this.materialButton1.TabIndex = 3;
            this.materialButton1.Text = "Aplicar";
            this.materialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton1.UseAccentColor = false;
            this.materialButton1.UseVisualStyleBackColor = true;
            // 
            // txtCentavos
            // 
            this.txtCentavos.Enabled = false;
            this.txtCentavos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCentavos.Location = new System.Drawing.Point(16, 98);
            this.txtCentavos.Name = "txtCentavos";
            this.txtCentavos.Size = new System.Drawing.Size(197, 26);
            this.txtCentavos.TabIndex = 4;
            this.txtCentavos.Text = "0,90";
            this.txtCentavos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBoxAdv1
            // 
            this.checkBoxAdv1.BeforeTouchSize = new System.Drawing.Size(234, 30);
            this.checkBoxAdv1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAdv1.Location = new System.Drawing.Point(16, 64);
            this.checkBoxAdv1.Name = "checkBoxAdv1";
            this.checkBoxAdv1.Size = new System.Drawing.Size(234, 30);
            this.checkBoxAdv1.TabIndex = 6;
            this.checkBoxAdv1.Text = "Arredondar Centavos";
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProdutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxAdv1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel2;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridProdutos;
        private System.Windows.Forms.TextBox txtCentavos;
        private MaterialSkin.Controls.MaterialButton materialButton1;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv checkBoxAdv1;
    }
}