namespace Lunar.Telas.Vendas.Adicionais
{
    partial class FrmSelecionarGrade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelecionarGrade));
            this.panel1 = new System.Windows.Forms.Panel();
            this.sfDataGrid1 = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.lblCodProduto = new System.Windows.Forms.Label();
            this.lblDescricaoProduto = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfDataGrid1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panel1.Controls.Add(this.lblDescricaoProduto);
            this.panel1.Controls.Add(this.lblCodProduto);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 72);
            this.panel1.TabIndex = 0;
            // 
            // sfDataGrid1
            // 
            this.sfDataGrid1.AccessibleName = "Table";
            this.sfDataGrid1.AllowEditing = false;
            this.sfDataGrid1.AutoGenerateColumns = false;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.HeaderText = "ID";
            gridTextColumn1.MappingName = "Id";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.HeaderText = "Descrição";
            gridTextColumn2.MappingName = "Descricao";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn3.HeaderText = "Unidade";
            gridTextColumn3.MappingName = "UnidadeMedida.Sigla";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn4.HeaderText = "Quantidade";
            gridTextColumn4.MappingName = "QuantidadeMedida";
            gridTextColumn5.AllowEditing = false;
            gridTextColumn5.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridTextColumn5.Format = "N2";
            gridTextColumn5.HeaderText = "Valor";
            gridTextColumn5.MappingName = "ValorVenda";
            this.sfDataGrid1.Columns.Add(gridTextColumn1);
            this.sfDataGrid1.Columns.Add(gridTextColumn2);
            this.sfDataGrid1.Columns.Add(gridTextColumn3);
            this.sfDataGrid1.Columns.Add(gridTextColumn4);
            this.sfDataGrid1.Columns.Add(gridTextColumn5);
            this.sfDataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfDataGrid1.Location = new System.Drawing.Point(0, 72);
            this.sfDataGrid1.Name = "sfDataGrid1";
            this.sfDataGrid1.Size = new System.Drawing.Size(561, 237);
            this.sfDataGrid1.TabIndex = 1;
            this.sfDataGrid1.Text = "sfDataGrid1";
            this.sfDataGrid1.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.sfDataGrid1_QueryRowStyle);
            this.sfDataGrid1.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.sfDataGrid1_CellDoubleClick);
            // 
            // lblCodProduto
            // 
            this.lblCodProduto.AutoSize = true;
            this.lblCodProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodProduto.ForeColor = System.Drawing.Color.White;
            this.lblCodProduto.Location = new System.Drawing.Point(12, 9);
            this.lblCodProduto.Name = "lblCodProduto";
            this.lblCodProduto.Size = new System.Drawing.Size(119, 20);
            this.lblCodProduto.TabIndex = 0;
            this.lblCodProduto.Text = "Código Produto";
            // 
            // lblDescricaoProduto
            // 
            this.lblDescricaoProduto.AutoSize = true;
            this.lblDescricaoProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricaoProduto.ForeColor = System.Drawing.Color.White;
            this.lblDescricaoProduto.Location = new System.Drawing.Point(12, 40);
            this.lblDescricaoProduto.Name = "lblDescricaoProduto";
            this.lblDescricaoProduto.Size = new System.Drawing.Size(162, 20);
            this.lblDescricaoProduto.TabIndex = 1;
            this.lblDescricaoProduto.Text = "Descrição do Produto";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnConfirmar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 254);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(561, 55);
            this.panel2.TabIndex = 2;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.Location = new System.Drawing.Point(408, 8);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(141, 35);
            this.btnConfirmar.TabIndex = 0;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // FrmSelecionarGrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(561, 309);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.sfDataGrid1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelecionarGrade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selecionar Classificação";
            this.Load += new System.EventHandler(this.FrmSelecionarGrade_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSelecionarGrade_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfDataGrid1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Syncfusion.WinForms.DataGrid.SfDataGrid sfDataGrid1;
        private System.Windows.Forms.Label lblCodProduto;
        private System.Windows.Forms.Label lblDescricaoProduto;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnConfirmar;
    }
}