namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    partial class FrmDeposito
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
            this.lblFormaPagamento = new System.Windows.Forms.Label();
            this.lblFaltante = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBanco = new Syncfusion.WinForms.ListView.SfComboBox();
            this.txtData = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.autoLabel10 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.dsConta = new System.Data.DataSet();
            this.ContaBancaria = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
            this.txtAutorizacao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtValor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnFechar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.comboBanco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsConta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContaBancaria)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFormaPagamento
            // 
            this.lblFormaPagamento.AutoSize = true;
            this.lblFormaPagamento.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormaPagamento.ForeColor = System.Drawing.Color.Black;
            this.lblFormaPagamento.Location = new System.Drawing.Point(12, 9);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(257, 29);
            this.lblFormaPagamento.TabIndex = 117;
            this.lblFormaPagamento.Text = "Depósito/Transferência";
            // 
            // lblFaltante
            // 
            this.lblFaltante.AutoSize = true;
            this.lblFaltante.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaltante.ForeColor = System.Drawing.Color.IndianRed;
            this.lblFaltante.Location = new System.Drawing.Point(67, 57);
            this.lblFaltante.Name = "lblFaltante";
            this.lblFaltante.Size = new System.Drawing.Size(144, 26);
            this.lblFaltante.TabIndex = 160;
            this.lblFaltante.Text = "Valor Faltante";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(67, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 26);
            this.label3.TabIndex = 159;
            this.label3.Text = "Valor Recebido";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(67, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 26);
            this.label6.TabIndex = 164;
            this.label6.Text = "Conta Bancária";
            // 
            // comboBanco
            // 
            this.comboBanco.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBanco.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboBanco.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBanco.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.comboBanco.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.comboBanco.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBanco.Location = new System.Drawing.Point(72, 287);
            this.comboBanco.MaxDropDownItems = 15;
            this.comboBanco.Name = "comboBanco";
            this.comboBanco.Size = new System.Drawing.Size(351, 28);
            this.comboBanco.Style.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboBanco.Style.ClearButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboBanco.Style.DropDownButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboBanco.Style.DropDownStyle.BorderColor = System.Drawing.Color.White;
            this.comboBanco.Style.DropDownStyle.GripperForeColor = System.Drawing.Color.White;
            this.comboBanco.Style.EditorStyle.BorderColor = System.Drawing.Color.White;
            this.comboBanco.Style.EditorStyle.DisabledBackColor = System.Drawing.Color.White;
            this.comboBanco.Style.EditorStyle.FocusedBorderColor = System.Drawing.Color.White;
            this.comboBanco.Style.EditorStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBanco.Style.ReadOnlyEditorStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBanco.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboBanco.Style.TokenStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBanco.Style.ToolTipStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBanco.Style.ToolTipStyle.SeparatorColor = System.Drawing.Color.White;
            this.comboBanco.TabIndex = 2;
            this.comboBanco.ToolTipOption.ShadowVisible = false;
            // 
            // txtData
            // 
            this.txtData.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtData.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtData.Font = new System.Drawing.Font("Montserrat Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtData.Location = new System.Drawing.Point(59, 221);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(365, 31);
            this.txtData.Style.BorderColor = System.Drawing.Color.White;
            this.txtData.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtData.Style.HoverBorderColor = System.Drawing.Color.White;
            this.txtData.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(67, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 26);
            this.label2.TabIndex = 163;
            this.label2.Text = "Data";
            // 
            // autoLabel10
            // 
            this.autoLabel10.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel10.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel10.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel10.ForeColor = System.Drawing.Color.Black;
            this.autoLabel10.Location = new System.Drawing.Point(84, 330);
            this.autoLabel10.Name = "autoLabel10";
            this.autoLabel10.Size = new System.Drawing.Size(91, 18);
            this.autoLabel10.TabIndex = 204;
            this.autoLabel10.Text = "Observações";
            // 
            // dsConta
            // 
            this.dsConta.DataSetName = "dsConta";
            this.dsConta.Tables.AddRange(new System.Data.DataTable[] {
            this.ContaBancaria});
            // 
            // ContaBancaria
            // 
            this.ContaBancaria.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2});
            this.ContaBancaria.TableName = "ContaBancaria";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "ID";
            this.dataColumn1.DataType = typeof(int);
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "CONTA";
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
            this.btnConfirmar.Location = new System.Drawing.Point(97, 405);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(289, 45);
            this.btnConfirmar.TabIndex = 4;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.White;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // txtAutorizacao
            // 
            this.txtAutorizacao.BackColor = System.Drawing.Color.White;
            this.txtAutorizacao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtAutorizacao.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtAutorizacao.BorderRadius = 8;
            this.txtAutorizacao.BorderSize = 2;
            this.txtAutorizacao.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAutorizacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtAutorizacao.Location = new System.Drawing.Point(72, 342);
            this.txtAutorizacao.Margin = new System.Windows.Forms.Padding(4);
            this.txtAutorizacao.Multiline = false;
            this.txtAutorizacao.Name = "txtAutorizacao";
            this.txtAutorizacao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtAutorizacao.PasswordChar = false;
            this.txtAutorizacao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtAutorizacao.PlaceholderText = "";
            this.txtAutorizacao.Size = new System.Drawing.Size(351, 39);
            this.txtAutorizacao.TabIndex = 3;
            this.txtAutorizacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAutorizacao.Texts = "";
            this.txtAutorizacao.UnderlinedStyle = false;
            // 
            // txtValor
            // 
            this.txtValor.BackColor = System.Drawing.Color.White;
            this.txtValor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderRadius = 8;
            this.txtValor.BorderSize = 2;
            this.txtValor.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtValor.Location = new System.Drawing.Point(70, 138);
            this.txtValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtValor.Multiline = false;
            this.txtValor.Name = "txtValor";
            this.txtValor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValor.PasswordChar = false;
            this.txtValor.PlaceholderColor = System.Drawing.Color.Black;
            this.txtValor.PlaceholderText = "R$";
            this.txtValor.Size = new System.Drawing.Size(351, 39);
            this.txtValor.TabIndex = 0;
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValor.Texts = "";
            this.txtValor.UnderlinedStyle = false;
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValor_KeyPress);
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Image = global::Lunar.Properties.Resources.CloseDark;
            this.btnFechar.Location = new System.Drawing.Point(485, -2);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(30, 31);
            this.btnFechar.TabIndex = 205;
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // FrmDeposito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(515, 457);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.autoLabel10);
            this.Controls.Add(this.txtAutorizacao);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBanco);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblFaltante);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.lblFormaPagamento);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmDeposito";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Depósito/Transferência";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmDeposito_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDeposito_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.comboBanco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsConta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContaBancaria)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFormaPagamento;
        private System.Windows.Forms.Label lblFaltante;
        private System.Windows.Forms.Label label3;
        private RJ_UI.Classes.RJTextBox txtValor;
        private System.Windows.Forms.Label label6;
        private Syncfusion.WinForms.ListView.SfComboBox comboBanco;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtData;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel10;
        private RJ_UI.Classes.RJTextBox txtAutorizacao;
        private RJ_UI.Classes.RJButton btnConfirmar;
        private System.Data.DataSet dsConta;
        private System.Data.DataTable ContaBancaria;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Windows.Forms.Button btnFechar;
    }
}