namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    partial class FrmPix
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
            this.dsConta = new System.Data.DataSet();
            this.ContaBancaria = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picQRCode = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtValor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnPixQR = new FontAwesome.Sharp.IconButton();
            this.btnFechar = new System.Windows.Forms.Button();
            this.txtCodigoQrCode = new Lunar.RJ_UI.Classes.RJTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.comboBanco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsConta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContaBancaria)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFormaPagamento
            // 
            this.lblFormaPagamento.AutoSize = true;
            this.lblFormaPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormaPagamento.ForeColor = System.Drawing.Color.Black;
            this.lblFormaPagamento.Location = new System.Drawing.Point(12, 9);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(45, 25);
            this.lblFormaPagamento.TabIndex = 118;
            this.lblFormaPagamento.Text = "PIX";
            // 
            // lblFaltante
            // 
            this.lblFaltante.AutoSize = true;
            this.lblFaltante.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaltante.ForeColor = System.Drawing.Color.IndianRed;
            this.lblFaltante.Location = new System.Drawing.Point(77, 54);
            this.lblFaltante.Name = "lblFaltante";
            this.lblFaltante.Size = new System.Drawing.Size(125, 24);
            this.lblFaltante.TabIndex = 163;
            this.lblFaltante.Text = "Valor Faltante";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(79, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 24);
            this.label3.TabIndex = 162;
            this.label3.Text = "Valor Recebido";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(79, 251);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 24);
            this.label6.TabIndex = 168;
            this.label6.Text = "Conta Bancária";
            // 
            // comboBanco
            // 
            this.comboBanco.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBanco.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboBanco.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBanco.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.comboBanco.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.comboBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBanco.Location = new System.Drawing.Point(84, 280);
            this.comboBanco.MaxDropDownItems = 15;
            this.comboBanco.Name = "comboBanco";
            this.comboBanco.Size = new System.Drawing.Size(349, 28);
            this.comboBanco.Style.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboBanco.Style.ClearButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboBanco.Style.DropDownButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboBanco.Style.DropDownStyle.BorderColor = System.Drawing.Color.White;
            this.comboBanco.Style.DropDownStyle.GripperForeColor = System.Drawing.Color.White;
            this.comboBanco.Style.EditorStyle.BorderColor = System.Drawing.Color.White;
            this.comboBanco.Style.EditorStyle.DisabledBackColor = System.Drawing.Color.White;
            this.comboBanco.Style.EditorStyle.FocusedBorderColor = System.Drawing.Color.White;
            this.comboBanco.Style.EditorStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBanco.Style.ReadOnlyEditorStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBanco.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboBanco.Style.TokenStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBanco.Style.ToolTipStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBanco.Style.ToolTipStyle.SeparatorColor = System.Drawing.Color.White;
            this.comboBanco.TabIndex = 166;
            this.comboBanco.ToolTipOption.ShadowVisible = false;
            // 
            // txtData
            // 
            this.txtData.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtData.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtData.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtData.Location = new System.Drawing.Point(71, 214);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(365, 31);
            this.txtData.Style.BorderColor = System.Drawing.Color.White;
            this.txtData.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtData.Style.HoverBorderColor = System.Drawing.Color.White;
            this.txtData.TabIndex = 165;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(79, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 24);
            this.label2.TabIndex = 167;
            this.label2.Text = "Data";
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
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DodgerBlue;
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.picQRCode);
            this.groupBox1.Location = new System.Drawing.Point(477, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 336);
            this.groupBox1.TabIndex = 171;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(92, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 20);
            this.label5.TabIndex = 174;
            this.label5.Text = "Lunar Software";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(10, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(286, 24);
            this.label4.TabIndex = 173;
            this.label4.Text = "é muito mais prático e seguro";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(82, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 24);
            this.label1.TabIndex = 172;
            this.label1.Text = "Pague com PIX";
            // 
            // picQRCode
            // 
            this.picQRCode.Location = new System.Drawing.Point(66, 85);
            this.picQRCode.Name = "picQRCode";
            this.picQRCode.Size = new System.Drawing.Size(190, 190);
            this.picQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picQRCode.TabIndex = 170;
            this.picQRCode.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(391, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 20);
            this.label7.TabIndex = 173;
            this.label7.Text = "[F3]";
            // 
            // txtValor
            // 
            this.txtValor.BackColor = System.Drawing.Color.White;
            this.txtValor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtValor.BorderRadius = 8;
            this.txtValor.BorderSize = 2;
            this.txtValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtValor.Location = new System.Drawing.Point(84, 131);
            this.txtValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtValor.Multiline = false;
            this.txtValor.Name = "txtValor";
            this.txtValor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValor.PasswordChar = false;
            this.txtValor.PlaceholderColor = System.Drawing.Color.Black;
            this.txtValor.PlaceholderText = "";
            this.txtValor.ReadOnly = false;
            this.txtValor.Size = new System.Drawing.Size(288, 38);
            this.txtValor.TabIndex = 174;
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValor.Texts = "";
            this.txtValor.UnderlinedStyle = false;
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValor_KeyPress_1);
            this.txtValor.Leave += new System.EventHandler(this.txtValor_Leave);
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
            this.btnConfirmar.Location = new System.Drawing.Point(106, 332);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(298, 45);
            this.btnConfirmar.TabIndex = 169;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.White;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnPixQR
            // 
            this.btnPixQR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPixQR.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPixQR.FlatAppearance.BorderSize = 0;
            this.btnPixQR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPixQR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPixQR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPixQR.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPixQR.IconChar = FontAwesome.Sharp.IconChar.Qrcode;
            this.btnPixQR.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(44)))));
            this.btnPixQR.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPixQR.IconSize = 35;
            this.btnPixQR.Location = new System.Drawing.Point(386, 139);
            this.btnPixQR.Name = "btnPixQR";
            this.btnPixQR.Size = new System.Drawing.Size(47, 31);
            this.btnPixQR.TabIndex = 175;
            this.btnPixQR.TabStop = false;
            this.btnPixQR.UseVisualStyleBackColor = true;
            this.btnPixQR.Click += new System.EventHandler(this.btnPixQR_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Image = global::Lunar.Properties.Resources.CloseDark;
            this.btnFechar.Location = new System.Drawing.Point(812, -3);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(30, 31);
            this.btnFechar.TabIndex = 156;
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // txtCodigoQrCode
            // 
            this.txtCodigoQrCode.BackColor = System.Drawing.Color.White;
            this.txtCodigoQrCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodigoQrCode.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodigoQrCode.BorderRadius = 8;
            this.txtCodigoQrCode.BorderSize = 2;
            this.txtCodigoQrCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoQrCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodigoQrCode.Location = new System.Drawing.Point(477, 18);
            this.txtCodigoQrCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigoQrCode.Multiline = false;
            this.txtCodigoQrCode.Name = "txtCodigoQrCode";
            this.txtCodigoQrCode.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodigoQrCode.PasswordChar = false;
            this.txtCodigoQrCode.PlaceholderColor = System.Drawing.Color.Black;
            this.txtCodigoQrCode.PlaceholderText = "";
            this.txtCodigoQrCode.ReadOnly = false;
            this.txtCodigoQrCode.Size = new System.Drawing.Size(323, 29);
            this.txtCodigoQrCode.TabIndex = 176;
            this.txtCodigoQrCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodigoQrCode.Texts = "";
            this.txtCodigoQrCode.UnderlinedStyle = false;
            this.txtCodigoQrCode.Visible = false;
            // 
            // FrmPix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(838, 402);
            this.Controls.Add(this.txtCodigoQrCode);
            this.Controls.Add(this.btnPixQR);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBanco);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblFaltante);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.lblFormaPagamento);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmPix";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPix";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmPix_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPix_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.comboBanco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsConta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContaBancaria)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFormaPagamento;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label lblFaltante;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private Syncfusion.WinForms.ListView.SfComboBox comboBanco;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtData;
        private System.Windows.Forms.Label label2;
        private RJ_UI.Classes.RJButton btnConfirmar;
        private System.Data.DataSet dsConta;
        private System.Data.DataTable ContaBancaria;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Windows.Forms.PictureBox picQRCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private RJ_UI.Classes.RJTextBox txtValor;
        private FontAwesome.Sharp.IconButton btnPixQR;
        private RJ_UI.Classes.RJTextBox txtCodigoQrCode;
    }
}