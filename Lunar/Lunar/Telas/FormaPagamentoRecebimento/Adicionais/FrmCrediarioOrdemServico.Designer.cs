namespace Lunar.Telas.FormaPagamentoRecebimento.Adicionais
{
    partial class FrmCrediarioOrdemServico
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
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtValor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn1 = new System.Data.DataColumn();
            this.Parcelas = new System.Data.DataTable();
            this.txtParcelas = new Lunar.RJ_UI.Classes.RJTextBox();
            this.dsParcelas = new System.Data.DataSet();
            this.btnConfirmaParcelas = new FontAwesome.Sharp.IconButton();
            this.gridParcelas = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.txtDataVencimento = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.autoLabel10 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.lblFaltante = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Parcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsParcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridParcelas)).BeginInit();
            this.SuspendLayout();
            // 
            // autoLabel1
            // 
            this.autoLabel1.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel1.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(20, 109);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(106, 21);
            this.autoLabel1.TabIndex = 221;
            this.autoLabel1.Text = "Valor Crediário";
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
            this.txtValor.Location = new System.Drawing.Point(9, 125);
            this.txtValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtValor.Multiline = false;
            this.txtValor.Name = "txtValor";
            this.txtValor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValor.PasswordChar = false;
            this.txtValor.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtValor.PlaceholderText = "";
            this.txtValor.ReadOnly = false;
            this.txtValor.Size = new System.Drawing.Size(261, 44);
            this.txtValor.TabIndex = 220;
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValor.Texts = "";
            this.txtValor.UnderlinedStyle = false;
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
            this.btnConfirmar.Location = new System.Drawing.Point(85, 406);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(289, 45);
            this.btnConfirmar.TabIndex = 213;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.White;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "VALOR";
            this.dataColumn3.DataType = typeof(decimal);
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "VENCIMENTO";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "PARCELA";
            this.dataColumn1.DataType = typeof(int);
            // 
            // Parcelas
            // 
            this.Parcelas.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3});
            this.Parcelas.TableName = "Parcelas";
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
            this.txtParcelas.Location = new System.Drawing.Point(278, 125);
            this.txtParcelas.Margin = new System.Windows.Forms.Padding(4);
            this.txtParcelas.Multiline = false;
            this.txtParcelas.Name = "txtParcelas";
            this.txtParcelas.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtParcelas.PasswordChar = false;
            this.txtParcelas.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtParcelas.PlaceholderText = "";
            this.txtParcelas.ReadOnly = false;
            this.txtParcelas.Size = new System.Drawing.Size(171, 44);
            this.txtParcelas.TabIndex = 210;
            this.txtParcelas.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtParcelas.Texts = "";
            this.txtParcelas.UnderlinedStyle = false;
            this.txtParcelas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtParcelas_KeyPress);
            this.txtParcelas.Leave += new System.EventHandler(this.txtParcelas_Leave);
            // 
            // dsParcelas
            // 
            this.dsParcelas.DataSetName = "dsParcelas";
            this.dsParcelas.Tables.AddRange(new System.Data.DataTable[] {
            this.Parcelas});
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
            this.btnConfirmaParcelas.Location = new System.Drawing.Point(208, 197);
            this.btnConfirmaParcelas.Name = "btnConfirmaParcelas";
            this.btnConfirmaParcelas.Size = new System.Drawing.Size(38, 37);
            this.btnConfirmaParcelas.TabIndex = 219;
            this.btnConfirmaParcelas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnConfirmaParcelas.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnConfirmaParcelas.UseVisualStyleBackColor = true;
            this.btnConfirmaParcelas.Click += new System.EventHandler(this.btnConfirmaParcelas_Click);
            // 
            // gridParcelas
            // 
            this.gridParcelas.AccessibleName = "Table";
            this.gridParcelas.AllowEditing = false;
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
            this.gridParcelas.Location = new System.Drawing.Point(9, 234);
            this.gridParcelas.Name = "gridParcelas";
            this.gridParcelas.Size = new System.Drawing.Size(438, 166);
            this.gridParcelas.Style.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.gridParcelas.TabIndex = 212;
            this.gridParcelas.Text = "sfDataGrid1";
            this.gridParcelas.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.gridParcelas_QueryRowStyle);
            // 
            // txtDataVencimento
            // 
            this.txtDataVencimento.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataVencimento.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataVencimento.Font = new System.Drawing.Font("Montserrat Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataVencimento.Location = new System.Drawing.Point(9, 197);
            this.txtDataVencimento.Name = "txtDataVencimento";
            this.txtDataVencimento.Size = new System.Drawing.Size(193, 31);
            this.txtDataVencimento.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataVencimento.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataVencimento.Style.HoverBorderColor = System.Drawing.Color.White;
            this.txtDataVencimento.TabIndex = 211;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(7, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 31);
            this.label2.TabIndex = 218;
            this.label2.Text = "1º Vencimento";
            // 
            // autoLabel10
            // 
            this.autoLabel10.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel10.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel10.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel10.ForeColor = System.Drawing.Color.Black;
            this.autoLabel10.Location = new System.Drawing.Point(290, 109);
            this.autoLabel10.Name = "autoLabel10";
            this.autoLabel10.Size = new System.Drawing.Size(93, 21);
            this.autoLabel10.TabIndex = 217;
            this.autoLabel10.Text = "Qtd Parcelas";
            // 
            // lblFaltante
            // 
            this.lblFaltante.AutoSize = true;
            this.lblFaltante.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaltante.ForeColor = System.Drawing.Color.IndianRed;
            this.lblFaltante.Location = new System.Drawing.Point(9, 59);
            this.lblFaltante.Name = "lblFaltante";
            this.lblFaltante.Size = new System.Drawing.Size(144, 31);
            this.lblFaltante.TabIndex = 216;
            this.lblFaltante.Text = "Valor Faltante";
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Image = global::Lunar.Properties.Resources.CloseDark;
            this.btnFechar.Location = new System.Drawing.Point(429, 1);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(30, 31);
            this.btnFechar.TabIndex = 215;
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, -2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 40);
            this.label1.TabIndex = 214;
            this.label1.Text = "Crediário";
            // 
            // FrmCrediarioOrdemServico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(459, 456);
            this.Controls.Add(this.autoLabel10);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtParcelas);
            this.Controls.Add(this.btnConfirmaParcelas);
            this.Controls.Add(this.gridParcelas);
            this.Controls.Add(this.txtDataVencimento);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblFaltante);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCrediarioOrdemServico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crediário";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmCrediarioOrdemServico_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCrediarioOrdemServico_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Parcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsParcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridParcelas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtValor;
        private RJ_UI.Classes.RJButton btnConfirmar;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataTable Parcelas;
        private RJ_UI.Classes.RJTextBox txtParcelas;
        private System.Data.DataSet dsParcelas;
        private FontAwesome.Sharp.IconButton btnConfirmaParcelas;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridParcelas;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataVencimento;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel10;
        private System.Windows.Forms.Label lblFaltante;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label label1;
    }
}