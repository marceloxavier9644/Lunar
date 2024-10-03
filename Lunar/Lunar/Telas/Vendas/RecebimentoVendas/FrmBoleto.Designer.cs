namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    partial class FrmBoleto
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
            this.btnFechar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnConfirmaParcelas = new FontAwesome.Sharp.IconButton();
            this.gridParcelas = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.txtDataVencimento = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.autoLabel10 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.lblFaltante = new System.Windows.Forms.Label();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
            this.dsParcelas = new System.Data.DataSet();
            this.Parcelas = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.txtCodContaBancaria = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnPesquisaContaBancaria = new FontAwesome.Sharp.IconButton();
            this.txtContaBancaria = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtParcelas = new System.Windows.Forms.TextBox();
            this.txtValor = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridParcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsParcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Parcelas)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Image = global::Lunar.Properties.Resources.CloseDark;
            this.btnFechar.Location = new System.Drawing.Point(430, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(30, 31);
            this.btnFechar.TabIndex = 156;
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 29);
            this.label1.TabIndex = 155;
            this.label1.Text = "Boleto";
            // 
            // autoLabel1
            // 
            this.autoLabel1.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(13, 96);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(73, 16);
            this.autoLabel1.TabIndex = 218;
            this.autoLabel1.Text = "Valor Total";
            // 
            // btnConfirmaParcelas
            // 
            this.btnConfirmaParcelas.FlatAppearance.BorderSize = 0;
            this.btnConfirmaParcelas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmaParcelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmaParcelas.IconChar = FontAwesome.Sharp.IconChar.CircleCheck;
            this.btnConfirmaParcelas.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnConfirmaParcelas.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnConfirmaParcelas.IconSize = 40;
            this.btnConfirmaParcelas.Location = new System.Drawing.Point(209, 258);
            this.btnConfirmaParcelas.Name = "btnConfirmaParcelas";
            this.btnConfirmaParcelas.Size = new System.Drawing.Size(38, 37);
            this.btnConfirmaParcelas.TabIndex = 6;
            this.btnConfirmaParcelas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnConfirmaParcelas.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnConfirmaParcelas.UseVisualStyleBackColor = true;
            this.btnConfirmaParcelas.Click += new System.EventHandler(this.btnConfirmaParcelas_Click);
            // 
            // gridParcelas
            // 
            this.gridParcelas.AccessibleName = "Table";
            this.gridParcelas.AllowSorting = false;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowSorting = false;
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderText = "Parcela";
            gridTextColumn1.MappingName = "PARCELA";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowSorting = false;
            gridTextColumn2.Format = "dd/MM/yyyy";
            gridTextColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn2.HeaderText = "Vencimento";
            gridTextColumn2.MappingName = "VENCIMENTO";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowSorting = false;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderText = "Valor";
            gridTextColumn3.MappingName = "VALOR";
            this.gridParcelas.Columns.Add(gridTextColumn1);
            this.gridParcelas.Columns.Add(gridTextColumn2);
            this.gridParcelas.Columns.Add(gridTextColumn3);
            this.gridParcelas.Location = new System.Drawing.Point(10, 295);
            this.gridParcelas.Name = "gridParcelas";
            this.gridParcelas.Size = new System.Drawing.Size(438, 178);
            this.gridParcelas.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.gridParcelas.TabIndex = 7;
            this.gridParcelas.Text = "sfDataGrid1";
            this.gridParcelas.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.gridParcelas_QueryRowStyle);
            this.gridParcelas.CurrentCellValidating += new Syncfusion.WinForms.DataGrid.Events.CurrentCellValidatingEventHandler(this.gridParcelas_CurrentCellValidating);
            // 
            // txtDataVencimento
            // 
            this.txtDataVencimento.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataVencimento.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataVencimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataVencimento.Location = new System.Drawing.Point(10, 258);
            this.txtDataVencimento.Name = "txtDataVencimento";
            this.txtDataVencimento.Size = new System.Drawing.Size(193, 31);
            this.txtDataVencimento.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataVencimento.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataVencimento.Style.HoverBorderColor = System.Drawing.Color.White;
            this.txtDataVencimento.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(7, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 24);
            this.label2.TabIndex = 215;
            this.label2.Text = "1º Vencimento";
            // 
            // autoLabel10
            // 
            this.autoLabel10.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel10.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel10.ForeColor = System.Drawing.Color.Black;
            this.autoLabel10.Location = new System.Drawing.Point(310, 96);
            this.autoLabel10.Name = "autoLabel10";
            this.autoLabel10.Size = new System.Drawing.Size(85, 16);
            this.autoLabel10.TabIndex = 214;
            this.autoLabel10.Text = "Qtd Parcelas";
            // 
            // lblFaltante
            // 
            this.lblFaltante.AutoSize = true;
            this.lblFaltante.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaltante.ForeColor = System.Drawing.Color.IndianRed;
            this.lblFaltante.Location = new System.Drawing.Point(13, 55);
            this.lblFaltante.Name = "lblFaltante";
            this.lblFaltante.Size = new System.Drawing.Size(125, 24);
            this.lblFaltante.TabIndex = 213;
            this.lblFaltante.Text = "Valor Faltante";
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
            this.btnConfirmar.Location = new System.Drawing.Point(85, 479);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(289, 45);
            this.btnConfirmar.TabIndex = 8;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.White;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
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
            this.dataColumn3});
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
            // txtCodContaBancaria
            // 
            this.txtCodContaBancaria.BackColor = System.Drawing.Color.White;
            this.txtCodContaBancaria.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodContaBancaria.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodContaBancaria.BorderRadius = 8;
            this.txtCodContaBancaria.BorderSize = 2;
            this.txtCodContaBancaria.Enabled = false;
            this.txtCodContaBancaria.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodContaBancaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodContaBancaria.Location = new System.Drawing.Point(357, 177);
            this.txtCodContaBancaria.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodContaBancaria.Multiline = false;
            this.txtCodContaBancaria.Name = "txtCodContaBancaria";
            this.txtCodContaBancaria.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodContaBancaria.PasswordChar = false;
            this.txtCodContaBancaria.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodContaBancaria.PlaceholderText = "";
            this.txtCodContaBancaria.ReadOnly = false;
            this.txtCodContaBancaria.Size = new System.Drawing.Size(89, 37);
            this.txtCodContaBancaria.TabIndex = 4;
            this.txtCodContaBancaria.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodContaBancaria.Texts = "";
            this.txtCodContaBancaria.UnderlinedStyle = false;
            // 
            // btnPesquisaContaBancaria
            // 
            this.btnPesquisaContaBancaria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaContaBancaria.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaContaBancaria.FlatAppearance.BorderSize = 0;
            this.btnPesquisaContaBancaria.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaContaBancaria.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaContaBancaria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaContaBancaria.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaContaBancaria.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaContaBancaria.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaContaBancaria.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaContaBancaria.IconSize = 38;
            this.btnPesquisaContaBancaria.Location = new System.Drawing.Point(310, 180);
            this.btnPesquisaContaBancaria.Name = "btnPesquisaContaBancaria";
            this.btnPesquisaContaBancaria.Size = new System.Drawing.Size(40, 34);
            this.btnPesquisaContaBancaria.TabIndex = 3;
            this.btnPesquisaContaBancaria.UseVisualStyleBackColor = true;
            this.btnPesquisaContaBancaria.Click += new System.EventHandler(this.btnPesquisaContaBancaria_Click);
            // 
            // txtContaBancaria
            // 
            this.txtContaBancaria.BackColor = System.Drawing.Color.White;
            this.txtContaBancaria.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtContaBancaria.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtContaBancaria.BorderRadius = 8;
            this.txtContaBancaria.BorderSize = 2;
            this.txtContaBancaria.Enabled = false;
            this.txtContaBancaria.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContaBancaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtContaBancaria.Location = new System.Drawing.Point(12, 177);
            this.txtContaBancaria.Margin = new System.Windows.Forms.Padding(4);
            this.txtContaBancaria.Multiline = false;
            this.txtContaBancaria.Name = "txtContaBancaria";
            this.txtContaBancaria.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtContaBancaria.PasswordChar = false;
            this.txtContaBancaria.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtContaBancaria.PlaceholderText = "";
            this.txtContaBancaria.ReadOnly = false;
            this.txtContaBancaria.Size = new System.Drawing.Size(291, 37);
            this.txtContaBancaria.TabIndex = 2;
            this.txtContaBancaria.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtContaBancaria.Texts = "";
            this.txtContaBancaria.UnderlinedStyle = false;
            // 
            // autoLabel2
            // 
            this.autoLabel2.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(20, 166);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(99, 16);
            this.autoLabel2.TabIndex = 266;
            this.autoLabel2.Text = "Conta Bancária";
            // 
            // txtParcelas
            // 
            this.txtParcelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtParcelas.Location = new System.Drawing.Point(310, 115);
            this.txtParcelas.Name = "txtParcelas";
            this.txtParcelas.Size = new System.Drawing.Size(136, 29);
            this.txtParcelas.TabIndex = 1;
            // 
            // txtValor
            // 
            this.txtValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtValor.Location = new System.Drawing.Point(12, 115);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(291, 29);
            this.txtValor.TabIndex = 0;
            this.txtValor.TabStop = false;
            // 
            // FrmBoleto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(459, 532);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.txtParcelas);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.txtCodContaBancaria);
            this.Controls.Add(this.btnPesquisaContaBancaria);
            this.Controls.Add(this.txtContaBancaria);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.btnConfirmaParcelas);
            this.Controls.Add(this.gridParcelas);
            this.Controls.Add(this.txtDataVencimento);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.autoLabel10);
            this.Controls.Add(this.lblFaltante);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmBoleto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Boleto";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmBoleto_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBoleto_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gridParcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsParcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Parcelas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private FontAwesome.Sharp.IconButton btnConfirmaParcelas;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridParcelas;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataVencimento;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel10;
        private System.Windows.Forms.Label lblFaltante;
        private RJ_UI.Classes.RJButton btnConfirmar;
        private System.Data.DataSet dsParcelas;
        private System.Data.DataTable Parcelas;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private RJ_UI.Classes.RJTextBox txtCodContaBancaria;
        private FontAwesome.Sharp.IconButton btnPesquisaContaBancaria;
        private RJ_UI.Classes.RJTextBox txtContaBancaria;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private System.Windows.Forms.TextBox txtParcelas;
        private System.Windows.Forms.TextBox txtValor;
    }
}