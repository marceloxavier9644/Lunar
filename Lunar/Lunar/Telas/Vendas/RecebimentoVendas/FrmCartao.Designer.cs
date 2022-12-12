namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    partial class FrmCartao
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
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtData = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboCondicaoRecebimento = new Syncfusion.WinForms.ListView.SfComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dsCondicao = new System.Data.DataSet();
            this.Condicao = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBandeiraCartao = new Syncfusion.WinForms.ListView.SfComboBox();
            this.dsBandeira = new System.Data.DataSet();
            this.Bandeira = new System.Data.DataTable();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.comboMaquininha = new Syncfusion.WinForms.ListView.SfComboBox();
            this.dsAdquirente = new System.Data.DataSet();
            this.Adquirente = new System.Data.DataTable();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.btnFechar = new System.Windows.Forms.Button();
            this.lblFaltante = new System.Windows.Forms.Label();
            this.autoLabel10 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtAutorizacao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnConfirmar = new Lunar.RJ_UI.Classes.RJButton();
            this.txtValor = new Lunar.RJ_UI.Classes.RJTextBox();
            this.radioDebito = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radioCredito = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            ((System.ComponentModel.ISupportInitialize)(this.comboCondicaoRecebimento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCondicao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Condicao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBandeiraCartao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsBandeira)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bandeira)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboMaquininha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAdquirente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Adquirente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioDebito)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioCredito)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(37, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 31);
            this.label3.TabIndex = 113;
            this.label3.Text = "Valor Recebido";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 40);
            this.label1.TabIndex = 116;
            this.label1.Text = "Cartão";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(37, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 31);
            this.label2.TabIndex = 146;
            this.label2.Text = "Data";
            // 
            // txtData
            // 
            this.txtData.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtData.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtData.Font = new System.Drawing.Font("Montserrat Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtData.Location = new System.Drawing.Point(32, 200);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(362, 31);
            this.txtData.Style.BorderColor = System.Drawing.Color.White;
            this.txtData.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtData.Style.HoverBorderColor = System.Drawing.Color.White;
            this.txtData.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(42, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(353, 2);
            this.groupBox2.TabIndex = 116;
            this.groupBox2.TabStop = false;
            // 
            // comboCondicaoRecebimento
            // 
            this.comboCondicaoRecebimento.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboCondicaoRecebimento.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboCondicaoRecebimento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboCondicaoRecebimento.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.comboCondicaoRecebimento.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.comboCondicaoRecebimento.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.comboCondicaoRecebimento.Location = new System.Drawing.Point(40, 374);
            this.comboCondicaoRecebimento.MaxDropDownItems = 15;
            this.comboCondicaoRecebimento.Name = "comboCondicaoRecebimento";
            this.comboCondicaoRecebimento.Size = new System.Drawing.Size(353, 28);
            this.comboCondicaoRecebimento.Style.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboCondicaoRecebimento.Style.ClearButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboCondicaoRecebimento.Style.DropDownButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboCondicaoRecebimento.Style.DropDownStyle.BorderColor = System.Drawing.Color.White;
            this.comboCondicaoRecebimento.Style.DropDownStyle.GripperForeColor = System.Drawing.Color.White;
            this.comboCondicaoRecebimento.Style.EditorStyle.BorderColor = System.Drawing.Color.White;
            this.comboCondicaoRecebimento.Style.EditorStyle.DisabledBackColor = System.Drawing.Color.White;
            this.comboCondicaoRecebimento.Style.EditorStyle.FocusedBorderColor = System.Drawing.Color.White;
            this.comboCondicaoRecebimento.Style.EditorStyle.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.comboCondicaoRecebimento.Style.ReadOnlyEditorStyle.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.comboCondicaoRecebimento.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboCondicaoRecebimento.Style.TokenStyle.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.comboCondicaoRecebimento.Style.ToolTipStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCondicaoRecebimento.Style.ToolTipStyle.SeparatorColor = System.Drawing.Color.White;
            this.comboCondicaoRecebimento.TabIndex = 5;
            this.comboCondicaoRecebimento.ToolTipOption.ShadowVisible = false;
            this.comboCondicaoRecebimento.SelectedIndexChanged += new System.EventHandler(this.comboCondicaoRecebimento_SelectedIndexChanged);
            this.comboCondicaoRecebimento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboCondicaoRecebimento_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(36, 311);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(254, 31);
            this.label4.TabIndex = 149;
            this.label4.Text = "Condição (Parcelamento)";
            // 
            // dsCondicao
            // 
            this.dsCondicao.DataSetName = "dsCondicao";
            this.dsCondicao.Tables.AddRange(new System.Data.DataTable[] {
            this.Condicao});
            // 
            // Condicao
            // 
            this.Condicao.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2});
            this.Condicao.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "Codigo";
            this.dataColumn1.DataType = typeof(int);
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "CondicaoRecebimento";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(35, 407);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 31);
            this.label5.TabIndex = 151;
            this.label5.Text = "Bandeira do Cartão";
            // 
            // comboBandeiraCartao
            // 
            this.comboBandeiraCartao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBandeiraCartao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboBandeiraCartao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBandeiraCartao.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.comboBandeiraCartao.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.comboBandeiraCartao.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.comboBandeiraCartao.Location = new System.Drawing.Point(40, 436);
            this.comboBandeiraCartao.MaxDropDownItems = 15;
            this.comboBandeiraCartao.Name = "comboBandeiraCartao";
            this.comboBandeiraCartao.Size = new System.Drawing.Size(353, 28);
            this.comboBandeiraCartao.Style.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboBandeiraCartao.Style.ClearButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.DropDownButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.DropDownStyle.BorderColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.DropDownStyle.GripperForeColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.EditorStyle.BorderColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.EditorStyle.DisabledBackColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.EditorStyle.FocusedBorderColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.EditorStyle.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.comboBandeiraCartao.Style.ReadOnlyEditorStyle.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.comboBandeiraCartao.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboBandeiraCartao.Style.TokenStyle.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.comboBandeiraCartao.Style.ToolTipStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBandeiraCartao.Style.ToolTipStyle.SeparatorColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.TabIndex = 6;
            this.comboBandeiraCartao.ToolTipOption.ShadowVisible = false;
            // 
            // dsBandeira
            // 
            this.dsBandeira.DataSetName = "dsBandeira";
            this.dsBandeira.Tables.AddRange(new System.Data.DataTable[] {
            this.Bandeira});
            // 
            // Bandeira
            // 
            this.Bandeira.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn3,
            this.dataColumn4});
            this.Bandeira.TableName = "Table1";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "Codigo";
            this.dataColumn3.DataType = typeof(int);
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "Bandeira";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(37, 245);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(251, 31);
            this.label6.TabIndex = 153;
            this.label6.Text = "Adquirente (Maquininha)";
            // 
            // comboMaquininha
            // 
            this.comboMaquininha.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboMaquininha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboMaquininha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboMaquininha.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.comboMaquininha.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.comboMaquininha.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboMaquininha.Location = new System.Drawing.Point(40, 274);
            this.comboMaquininha.MaxDropDownItems = 15;
            this.comboMaquininha.Name = "comboMaquininha";
            this.comboMaquininha.Size = new System.Drawing.Size(353, 28);
            this.comboMaquininha.Style.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboMaquininha.Style.ClearButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboMaquininha.Style.DropDownButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboMaquininha.Style.DropDownStyle.BorderColor = System.Drawing.Color.White;
            this.comboMaquininha.Style.DropDownStyle.GripperForeColor = System.Drawing.Color.White;
            this.comboMaquininha.Style.EditorStyle.BorderColor = System.Drawing.Color.White;
            this.comboMaquininha.Style.EditorStyle.DisabledBackColor = System.Drawing.Color.White;
            this.comboMaquininha.Style.EditorStyle.FocusedBorderColor = System.Drawing.Color.White;
            this.comboMaquininha.Style.EditorStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboMaquininha.Style.ReadOnlyEditorStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboMaquininha.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboMaquininha.Style.TokenStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboMaquininha.Style.ToolTipStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboMaquininha.Style.ToolTipStyle.SeparatorColor = System.Drawing.Color.White;
            this.comboMaquininha.TabIndex = 2;
            this.comboMaquininha.ToolTipOption.ShadowVisible = false;
            // 
            // dsAdquirente
            // 
            this.dsAdquirente.DataSetName = "dsAdquirente";
            this.dsAdquirente.Tables.AddRange(new System.Data.DataTable[] {
            this.Adquirente});
            // 
            // Adquirente
            // 
            this.Adquirente.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn5,
            this.dataColumn6});
            this.Adquirente.TableName = "Adquirente";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "Codigo";
            this.dataColumn5.DataType = typeof(int);
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "Adquirente";
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Image = global::Lunar.Properties.Resources.CloseDark;
            this.btnFechar.Location = new System.Drawing.Point(408, 9);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(30, 31);
            this.btnFechar.TabIndex = 154;
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // lblFaltante
            // 
            this.lblFaltante.AutoSize = true;
            this.lblFaltante.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaltante.ForeColor = System.Drawing.Color.IndianRed;
            this.lblFaltante.Location = new System.Drawing.Point(35, 55);
            this.lblFaltante.Name = "lblFaltante";
            this.lblFaltante.Size = new System.Drawing.Size(144, 31);
            this.lblFaltante.TabIndex = 157;
            this.lblFaltante.Text = "Valor Faltante";
            // 
            // autoLabel10
            // 
            this.autoLabel10.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel10.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel10.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel10.ForeColor = System.Drawing.Color.Black;
            this.autoLabel10.Location = new System.Drawing.Point(52, 473);
            this.autoLabel10.Name = "autoLabel10";
            this.autoLabel10.Size = new System.Drawing.Size(151, 21);
            this.autoLabel10.TabIndex = 202;
            this.autoLabel10.Text = "Nº Autorização Cartão";
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
            this.txtAutorizacao.Location = new System.Drawing.Point(40, 485);
            this.txtAutorizacao.Margin = new System.Windows.Forms.Padding(4);
            this.txtAutorizacao.Multiline = false;
            this.txtAutorizacao.Name = "txtAutorizacao";
            this.txtAutorizacao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtAutorizacao.PasswordChar = false;
            this.txtAutorizacao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtAutorizacao.PlaceholderText = "";
            this.txtAutorizacao.ReadOnly = false;
            this.txtAutorizacao.Size = new System.Drawing.Size(353, 44);
            this.txtAutorizacao.TabIndex = 7;
            this.txtAutorizacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAutorizacao.Texts = "";
            this.txtAutorizacao.UnderlinedStyle = false;
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
            this.btnConfirmar.Location = new System.Drawing.Point(75, 547);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(289, 45);
            this.btnConfirmar.TabIndex = 8;
            this.btnConfirmar.Text = "Confirmar [F5]";
            this.btnConfirmar.TextColor = System.Drawing.Color.White;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
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
            this.txtValor.Location = new System.Drawing.Point(40, 121);
            this.txtValor.Margin = new System.Windows.Forms.Padding(4);
            this.txtValor.Multiline = false;
            this.txtValor.Name = "txtValor";
            this.txtValor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtValor.PasswordChar = false;
            this.txtValor.PlaceholderColor = System.Drawing.Color.Black;
            this.txtValor.PlaceholderText = "R$";
            this.txtValor.ReadOnly = false;
            this.txtValor.Size = new System.Drawing.Size(355, 44);
            this.txtValor.TabIndex = 0;
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValor.Texts = "";
            this.txtValor.UnderlinedStyle = false;
            this.txtValor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValor_KeyDown);
            this.txtValor.Leave += new System.EventHandler(this.txtValor_Leave);
            // 
            // radioDebito
            // 
            this.radioDebito.BeforeTouchSize = new System.Drawing.Size(91, 21);
            this.radioDebito.Checked = true;
            this.radioDebito.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioDebito.Location = new System.Drawing.Point(131, 347);
            this.radioDebito.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioDebito.Name = "radioDebito";
            this.radioDebito.Size = new System.Drawing.Size(91, 21);
            this.radioDebito.TabIndex = 4;
            this.radioDebito.Text = " Débito";
            this.radioDebito.CheckChanged += new System.EventHandler(this.radioDebito_CheckChanged);
            // 
            // radioCredito
            // 
            this.radioCredito.BeforeTouchSize = new System.Drawing.Size(91, 21);
            this.radioCredito.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioCredito.Location = new System.Drawing.Point(228, 347);
            this.radioCredito.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioCredito.Name = "radioCredito";
            this.radioCredito.Size = new System.Drawing.Size(91, 21);
            this.radioCredito.TabIndex = 3;
            this.radioCredito.TabStop = false;
            this.radioCredito.Text = " Crédito";
            this.radioCredito.CheckChanged += new System.EventHandler(this.radioCredito_CheckChanged);
            // 
            // FrmCartao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(450, 602);
            this.Controls.Add(this.radioCredito);
            this.Controls.Add(this.radioDebito);
            this.Controls.Add(this.autoLabel10);
            this.Controls.Add(this.txtAutorizacao);
            this.Controls.Add(this.lblFaltante);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboMaquininha);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBandeiraCartao);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboCondicaoRecebimento);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtValor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmCartao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmDinheiro";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmCartao_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmCartao_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCartao_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.comboCondicaoRecebimento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCondicao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Condicao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBandeiraCartao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsBandeira)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bandeira)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboMaquininha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAdquirente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Adquirente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioDebito)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioCredito)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RJ_UI.Classes.RJTextBox txtValor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private RJ_UI.Classes.RJButton btnConfirmar;
        private System.Windows.Forms.Label label2;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtData;
        private System.Windows.Forms.GroupBox groupBox2;
        private Syncfusion.WinForms.ListView.SfComboBox comboCondicaoRecebimento;
        private System.Windows.Forms.Label label4;
        private System.Data.DataSet dsCondicao;
        private System.Data.DataTable Condicao;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Windows.Forms.Label label5;
        private Syncfusion.WinForms.ListView.SfComboBox comboBandeiraCartao;
        private System.Data.DataSet dsBandeira;
        private System.Data.DataTable Bandeira;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Windows.Forms.Label label6;
        private Syncfusion.WinForms.ListView.SfComboBox comboMaquininha;
        private System.Data.DataSet dsAdquirente;
        private System.Data.DataTable Adquirente;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label lblFaltante;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel10;
        private RJ_UI.Classes.RJTextBox txtAutorizacao;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioDebito;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioCredito;
    }
}