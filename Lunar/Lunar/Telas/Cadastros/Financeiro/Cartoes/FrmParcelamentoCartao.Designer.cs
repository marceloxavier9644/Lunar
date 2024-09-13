namespace Lunar.Telas.Cadastros.Financeiro.ParcelamentoCartoes
{
    partial class FrmParcelamentoCartao
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
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnFechar = new FontAwesome.Sharp.IconButton();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.comboBandeiraCartao = new Syncfusion.WinForms.ListView.SfComboBox();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel7 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.comboAdquirente = new Syncfusion.WinForms.ListView.SfComboBox();
            this.radioDebito = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radioCredito = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.dsBandeira = new System.Data.DataSet();
            this.Bandeira = new System.Data.DataTable();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.autoLabel8 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.chkAtivarAntecipacao = new MaterialSkin.Controls.MaterialCheckbox();
            this.lbl1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.lbl2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.autoLabel9 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDiasRecebimentoAntecipacao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtTaxaAntecipacao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtAdicionalPorPacela = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnSalvar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnCancelar = new Lunar.RJ_UI.Classes.RJButton();
            this.txtTarifaAdicional = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtDiasRecebimento = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtTaxa = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtQtdParcela = new Lunar.RJ_UI.Classes.RJTextBox();
            this.rjTextBox5 = new Lunar.RJ_UI.Classes.RJTextBox();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBandeiraCartao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboAdquirente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioDebito)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioCredito)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsBandeira)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bandeira)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel4);
            this.panelTitleBar.Controls.Add(this.btnFechar);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(784, 44);
            this.panelTitleBar.TabIndex = 199;
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.White;
            this.autoLabel4.Location = new System.Drawing.Point(5, 7);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(344, 25);
            this.autoLabel4.TabIndex = 198;
            this.autoLabel4.Text = "Parcelamento - Taxas dos Cartões";
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.BackColor = System.Drawing.Color.Transparent;
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.IconChar = FontAwesome.Sharp.IconChar.Multiply;
            this.btnFechar.IconColor = System.Drawing.Color.White;
            this.btnFechar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFechar.IconSize = 30;
            this.btnFechar.Location = new System.Drawing.Point(743, 3);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(39, 41);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.UseVisualStyleBackColor = false;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(13, 115);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(85, 16);
            this.autoLabel1.TabIndex = 210;
            this.autoLabel1.Text = "Qtd Parcelas";
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(150, 115);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(53, 16);
            this.autoLabel2.TabIndex = 212;
            this.autoLabel2.Text = "Taxa %";
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(620, 115);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(119, 16);
            this.autoLabel3.TabIndex = 214;
            this.autoLabel3.Text = "Dias Recebimento";
            // 
            // autoLabel5
            // 
            this.autoLabel5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.Black;
            this.autoLabel5.Location = new System.Drawing.Point(261, 115);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(121, 16);
            this.autoLabel5.TabIndex = 216;
            this.autoLabel5.Text = "Tarifa Adicional R$";
            // 
            // comboBandeiraCartao
            // 
            this.comboBandeiraCartao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBandeiraCartao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboBandeiraCartao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBandeiraCartao.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.comboBandeiraCartao.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.comboBandeiraCartao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.comboBandeiraCartao.Location = new System.Drawing.Point(372, 79);
            this.comboBandeiraCartao.MaxDropDownItems = 15;
            this.comboBandeiraCartao.Name = "comboBandeiraCartao";
            this.comboBandeiraCartao.Size = new System.Drawing.Size(398, 34);
            this.comboBandeiraCartao.Style.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboBandeiraCartao.Style.ClearButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.DropDownButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.DropDownStyle.BorderColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.DropDownStyle.GripperForeColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.EditorStyle.BorderColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.EditorStyle.DisabledBackColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.EditorStyle.FocusedBorderColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.Style.EditorStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.comboBandeiraCartao.Style.ReadOnlyEditorStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.comboBandeiraCartao.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboBandeiraCartao.Style.TokenStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.comboBandeiraCartao.Style.ToolTipStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBandeiraCartao.Style.ToolTipStyle.SeparatorColor = System.Drawing.Color.White;
            this.comboBandeiraCartao.TabIndex = 1;
            this.comboBandeiraCartao.ToolTipOption.ShadowVisible = false;
            // 
            // autoLabel6
            // 
            this.autoLabel6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.Black;
            this.autoLabel6.Location = new System.Drawing.Point(372, 55);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(124, 16);
            this.autoLabel6.TabIndex = 218;
            this.autoLabel6.Text = "Bandeira do Cartão";
            // 
            // autoLabel7
            // 
            this.autoLabel7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel7.ForeColor = System.Drawing.Color.Black;
            this.autoLabel7.Location = new System.Drawing.Point(12, 55);
            this.autoLabel7.Name = "autoLabel7";
            this.autoLabel7.Size = new System.Drawing.Size(152, 16);
            this.autoLabel7.TabIndex = 222;
            this.autoLabel7.Text = "Adquirente (Maquininha)";
            // 
            // comboAdquirente
            // 
            this.comboAdquirente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboAdquirente.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboAdquirente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboAdquirente.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.comboAdquirente.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.comboAdquirente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.comboAdquirente.Location = new System.Drawing.Point(12, 79);
            this.comboAdquirente.MaxDropDownItems = 15;
            this.comboAdquirente.Name = "comboAdquirente";
            this.comboAdquirente.Size = new System.Drawing.Size(352, 33);
            this.comboAdquirente.Style.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboAdquirente.Style.ClearButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboAdquirente.Style.DropDownButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboAdquirente.Style.DropDownStyle.BorderColor = System.Drawing.Color.White;
            this.comboAdquirente.Style.DropDownStyle.GripperForeColor = System.Drawing.Color.White;
            this.comboAdquirente.Style.EditorStyle.BorderColor = System.Drawing.Color.White;
            this.comboAdquirente.Style.EditorStyle.DisabledBackColor = System.Drawing.Color.White;
            this.comboAdquirente.Style.EditorStyle.FocusedBorderColor = System.Drawing.Color.White;
            this.comboAdquirente.Style.EditorStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.comboAdquirente.Style.ReadOnlyEditorStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.comboAdquirente.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboAdquirente.Style.TokenStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.comboAdquirente.Style.ToolTipStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboAdquirente.Style.ToolTipStyle.SeparatorColor = System.Drawing.Color.White;
            this.comboAdquirente.TabIndex = 0;
            this.comboAdquirente.ToolTipOption.ShadowVisible = false;
            // 
            // radioDebito
            // 
            this.radioDebito.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.radioDebito.BeforeTouchSize = new System.Drawing.Size(150, 29);
            this.radioDebito.Border3DStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.radioDebito.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.radioDebito.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioDebito.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioDebito.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.radioDebito.HotBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.radioDebito.Location = new System.Drawing.Point(23, 306);
            this.radioDebito.MetroColor = System.Drawing.Color.Black;
            this.radioDebito.Name = "radioDebito";
            this.radioDebito.Size = new System.Drawing.Size(150, 29);
            this.radioDebito.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Metro;
            this.radioDebito.TabIndex = 7;
            this.radioDebito.Text = "  Débito";
            this.radioDebito.ThemeName = "Metro";
            // 
            // radioCredito
            // 
            this.radioCredito.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.radioCredito.BeforeTouchSize = new System.Drawing.Size(116, 29);
            this.radioCredito.Border3DStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.radioCredito.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.radioCredito.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioCredito.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.radioCredito.HotBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.radioCredito.Location = new System.Drawing.Point(175, 306);
            this.radioCredito.MetroColor = System.Drawing.Color.Black;
            this.radioCredito.Name = "radioCredito";
            this.radioCredito.Size = new System.Drawing.Size(116, 29);
            this.radioCredito.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Metro;
            this.radioCredito.TabIndex = 8;
            this.radioCredito.TabStop = false;
            this.radioCredito.Text = "  Crédito";
            this.radioCredito.ThemeName = "Metro";
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
            // autoLabel8
            // 
            this.autoLabel8.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel8.ForeColor = System.Drawing.Color.Black;
            this.autoLabel8.Location = new System.Drawing.Point(13, 282);
            this.autoLabel8.Name = "autoLabel8";
            this.autoLabel8.Size = new System.Drawing.Size(97, 16);
            this.autoLabel8.TabIndex = 226;
            this.autoLabel8.Text = "Tipo de Cartão";
            // 
            // chkAtivarAntecipacao
            // 
            this.chkAtivarAntecipacao.AutoSize = true;
            this.chkAtivarAntecipacao.Depth = 0;
            this.chkAtivarAntecipacao.Location = new System.Drawing.Point(10, 39);
            this.chkAtivarAntecipacao.Margin = new System.Windows.Forms.Padding(0);
            this.chkAtivarAntecipacao.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkAtivarAntecipacao.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkAtivarAntecipacao.Name = "chkAtivarAntecipacao";
            this.chkAtivarAntecipacao.Ripple = true;
            this.chkAtivarAntecipacao.Size = new System.Drawing.Size(168, 37);
            this.chkAtivarAntecipacao.TabIndex = 0;
            this.chkAtivarAntecipacao.Text = "Ativar Antecipação";
            this.chkAtivarAntecipacao.UseVisualStyleBackColor = true;
            this.chkAtivarAntecipacao.CheckedChanged += new System.EventHandler(this.chkAtivarAntecipacao_CheckedChanged);
            // 
            // lbl1
            // 
            this.lbl1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.ForeColor = System.Drawing.Color.Black;
            this.lbl1.Location = new System.Drawing.Point(256, 19);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(159, 16);
            this.lbl1.TabIndex = 229;
            this.lbl1.Text = "Dias Recebimento Antec.";
            this.lbl1.Visible = false;
            // 
            // lbl2
            // 
            this.lbl2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.ForeColor = System.Drawing.Color.Black;
            this.lbl2.Location = new System.Drawing.Point(438, 19);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(161, 16);
            this.lbl2.TabIndex = 231;
            this.lbl2.Text = "Taxa Antecipação Mês %";
            this.lbl2.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAtivarAntecipacao);
            this.groupBox1.Controls.Add(this.lbl2);
            this.groupBox1.Controls.Add(this.txtDiasRecebimentoAntecipacao);
            this.groupBox1.Controls.Add(this.txtTaxaAntecipacao);
            this.groupBox1.Controls.Add(this.lbl1);
            this.groupBox1.Location = new System.Drawing.Point(13, 179);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(757, 100);
            this.groupBox1.TabIndex = 232;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Antecipação";
            // 
            // autoLabel9
            // 
            this.autoLabel9.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel9.ForeColor = System.Drawing.Color.Black;
            this.autoLabel9.Location = new System.Drawing.Point(419, 115);
            this.autoLabel9.Name = "autoLabel9";
            this.autoLabel9.Size = new System.Drawing.Size(152, 16);
            this.autoLabel9.TabIndex = 233;
            this.autoLabel9.Text = "Adicional Por Parcela %";
            this.autoLabel9.Visible = false;
            // 
            // txtDiasRecebimentoAntecipacao
            // 
            this.txtDiasRecebimentoAntecipacao.BackColor = System.Drawing.Color.White;
            this.txtDiasRecebimentoAntecipacao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDiasRecebimentoAntecipacao.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDiasRecebimentoAntecipacao.BorderRadius = 8;
            this.txtDiasRecebimentoAntecipacao.BorderSize = 2;
            this.txtDiasRecebimentoAntecipacao.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDiasRecebimentoAntecipacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiasRecebimentoAntecipacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDiasRecebimentoAntecipacao.Location = new System.Drawing.Point(256, 39);
            this.txtDiasRecebimentoAntecipacao.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiasRecebimentoAntecipacao.Multiline = false;
            this.txtDiasRecebimentoAntecipacao.Name = "txtDiasRecebimentoAntecipacao";
            this.txtDiasRecebimentoAntecipacao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDiasRecebimentoAntecipacao.PasswordChar = false;
            this.txtDiasRecebimentoAntecipacao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDiasRecebimentoAntecipacao.PlaceholderText = "";
            this.txtDiasRecebimentoAntecipacao.ReadOnly = false;
            this.txtDiasRecebimentoAntecipacao.Size = new System.Drawing.Size(174, 37);
            this.txtDiasRecebimentoAntecipacao.TabIndex = 1;
            this.txtDiasRecebimentoAntecipacao.Tag = "";
            this.txtDiasRecebimentoAntecipacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDiasRecebimentoAntecipacao.Texts = "1";
            this.txtDiasRecebimentoAntecipacao.UnderlinedStyle = false;
            this.txtDiasRecebimentoAntecipacao.Visible = false;
            this.txtDiasRecebimentoAntecipacao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiasRecebimentoAntecipacao_KeyPress);
            // 
            // txtTaxaAntecipacao
            // 
            this.txtTaxaAntecipacao.BackColor = System.Drawing.Color.White;
            this.txtTaxaAntecipacao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtTaxaAntecipacao.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtTaxaAntecipacao.BorderRadius = 8;
            this.txtTaxaAntecipacao.BorderSize = 2;
            this.txtTaxaAntecipacao.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtTaxaAntecipacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxaAntecipacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTaxaAntecipacao.Location = new System.Drawing.Point(438, 39);
            this.txtTaxaAntecipacao.Margin = new System.Windows.Forms.Padding(4);
            this.txtTaxaAntecipacao.Multiline = false;
            this.txtTaxaAntecipacao.Name = "txtTaxaAntecipacao";
            this.txtTaxaAntecipacao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtTaxaAntecipacao.PasswordChar = false;
            this.txtTaxaAntecipacao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtTaxaAntecipacao.PlaceholderText = "";
            this.txtTaxaAntecipacao.ReadOnly = false;
            this.txtTaxaAntecipacao.Size = new System.Drawing.Size(200, 37);
            this.txtTaxaAntecipacao.TabIndex = 2;
            this.txtTaxaAntecipacao.Tag = "";
            this.txtTaxaAntecipacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTaxaAntecipacao.Texts = "";
            this.txtTaxaAntecipacao.UnderlinedStyle = false;
            this.txtTaxaAntecipacao.Visible = false;
            this.txtTaxaAntecipacao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTaxaAntecipacao_KeyPress);
            // 
            // txtAdicionalPorPacela
            // 
            this.txtAdicionalPorPacela.BackColor = System.Drawing.Color.White;
            this.txtAdicionalPorPacela.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtAdicionalPorPacela.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtAdicionalPorPacela.BorderRadius = 8;
            this.txtAdicionalPorPacela.BorderSize = 2;
            this.txtAdicionalPorPacela.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtAdicionalPorPacela.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdicionalPorPacela.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtAdicionalPorPacela.Location = new System.Drawing.Point(419, 135);
            this.txtAdicionalPorPacela.Margin = new System.Windows.Forms.Padding(4);
            this.txtAdicionalPorPacela.Multiline = false;
            this.txtAdicionalPorPacela.Name = "txtAdicionalPorPacela";
            this.txtAdicionalPorPacela.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtAdicionalPorPacela.PasswordChar = false;
            this.txtAdicionalPorPacela.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtAdicionalPorPacela.PlaceholderText = "";
            this.txtAdicionalPorPacela.ReadOnly = false;
            this.txtAdicionalPorPacela.Size = new System.Drawing.Size(193, 37);
            this.txtAdicionalPorPacela.TabIndex = 5;
            this.txtAdicionalPorPacela.Tag = "";
            this.txtAdicionalPorPacela.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAdicionalPorPacela.Texts = "";
            this.txtAdicionalPorPacela.UnderlinedStyle = false;
            this.txtAdicionalPorPacela.Visible = false;
            this.txtAdicionalPorPacela.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdicionalPorPacela_KeyPress);
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnSalvar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnSalvar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSalvar.BorderRadius = 8;
            this.btnSalvar.BorderSize = 0;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(554, 386);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(216, 45);
            this.btnSalvar.TabIndex = 9;
            this.btnSalvar.Text = "Salvar [F5]";
            this.btnSalvar.TextColor = System.Drawing.Color.White;
            this.btnSalvar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.BackgroundColor = System.Drawing.Color.White;
            this.btnCancelar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnCancelar.BorderRadius = 8;
            this.btnCancelar.BorderSize = 2;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.Location = new System.Drawing.Point(332, 386);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(216, 45);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.TabStop = false;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // txtTarifaAdicional
            // 
            this.txtTarifaAdicional.BackColor = System.Drawing.Color.White;
            this.txtTarifaAdicional.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtTarifaAdicional.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtTarifaAdicional.BorderRadius = 8;
            this.txtTarifaAdicional.BorderSize = 2;
            this.txtTarifaAdicional.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtTarifaAdicional.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTarifaAdicional.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTarifaAdicional.Location = new System.Drawing.Point(261, 135);
            this.txtTarifaAdicional.Margin = new System.Windows.Forms.Padding(4);
            this.txtTarifaAdicional.Multiline = false;
            this.txtTarifaAdicional.Name = "txtTarifaAdicional";
            this.txtTarifaAdicional.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtTarifaAdicional.PasswordChar = false;
            this.txtTarifaAdicional.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtTarifaAdicional.PlaceholderText = "";
            this.txtTarifaAdicional.ReadOnly = false;
            this.txtTarifaAdicional.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTarifaAdicional.Size = new System.Drawing.Size(150, 37);
            this.txtTarifaAdicional.TabIndex = 4;
            this.txtTarifaAdicional.Tag = "";
            this.txtTarifaAdicional.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTarifaAdicional.Texts = "0,00";
            this.txtTarifaAdicional.UnderlinedStyle = false;
            this.txtTarifaAdicional.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTarifaAdicional_KeyPress);
            // 
            // txtDiasRecebimento
            // 
            this.txtDiasRecebimento.BackColor = System.Drawing.Color.White;
            this.txtDiasRecebimento.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDiasRecebimento.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDiasRecebimento.BorderRadius = 8;
            this.txtDiasRecebimento.BorderSize = 2;
            this.txtDiasRecebimento.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDiasRecebimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiasRecebimento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDiasRecebimento.Location = new System.Drawing.Point(620, 135);
            this.txtDiasRecebimento.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiasRecebimento.Multiline = false;
            this.txtDiasRecebimento.Name = "txtDiasRecebimento";
            this.txtDiasRecebimento.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDiasRecebimento.PasswordChar = false;
            this.txtDiasRecebimento.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDiasRecebimento.PlaceholderText = "";
            this.txtDiasRecebimento.ReadOnly = false;
            this.txtDiasRecebimento.Size = new System.Drawing.Size(150, 37);
            this.txtDiasRecebimento.TabIndex = 6;
            this.txtDiasRecebimento.Tag = "";
            this.txtDiasRecebimento.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDiasRecebimento.Texts = "30";
            this.txtDiasRecebimento.UnderlinedStyle = false;
            this.txtDiasRecebimento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiasRecebimento_KeyPress);
            // 
            // txtTaxa
            // 
            this.txtTaxa.BackColor = System.Drawing.Color.White;
            this.txtTaxa.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtTaxa.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtTaxa.BorderRadius = 8;
            this.txtTaxa.BorderSize = 2;
            this.txtTaxa.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtTaxa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTaxa.Location = new System.Drawing.Point(150, 135);
            this.txtTaxa.Margin = new System.Windows.Forms.Padding(4);
            this.txtTaxa.Multiline = false;
            this.txtTaxa.Name = "txtTaxa";
            this.txtTaxa.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtTaxa.PasswordChar = false;
            this.txtTaxa.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtTaxa.PlaceholderText = "";
            this.txtTaxa.ReadOnly = false;
            this.txtTaxa.Size = new System.Drawing.Size(103, 37);
            this.txtTaxa.TabIndex = 3;
            this.txtTaxa.Tag = "";
            this.txtTaxa.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTaxa.Texts = "";
            this.txtTaxa.UnderlinedStyle = false;
            this.txtTaxa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTaxa_KeyPress);
            // 
            // txtQtdParcela
            // 
            this.txtQtdParcela.BackColor = System.Drawing.Color.White;
            this.txtQtdParcela.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtQtdParcela.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtQtdParcela.BorderRadius = 8;
            this.txtQtdParcela.BorderSize = 2;
            this.txtQtdParcela.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtQtdParcela.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQtdParcela.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtQtdParcela.Location = new System.Drawing.Point(13, 135);
            this.txtQtdParcela.Margin = new System.Windows.Forms.Padding(4);
            this.txtQtdParcela.Multiline = false;
            this.txtQtdParcela.Name = "txtQtdParcela";
            this.txtQtdParcela.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtQtdParcela.PasswordChar = false;
            this.txtQtdParcela.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtQtdParcela.PlaceholderText = "";
            this.txtQtdParcela.ReadOnly = false;
            this.txtQtdParcela.Size = new System.Drawing.Size(129, 37);
            this.txtQtdParcela.TabIndex = 2;
            this.txtQtdParcela.Tag = "";
            this.txtQtdParcela.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtQtdParcela.Texts = "";
            this.txtQtdParcela.UnderlinedStyle = false;
            this.txtQtdParcela.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQtdParcela_KeyPress);
            // 
            // rjTextBox5
            // 
            this.rjTextBox5.BackColor = System.Drawing.Color.White;
            this.rjTextBox5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.rjTextBox5.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.rjTextBox5.BorderRadius = 8;
            this.rjTextBox5.BorderSize = 2;
            this.rjTextBox5.Cursor = System.Windows.Forms.Cursors.Default;
            this.rjTextBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjTextBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rjTextBox5.Location = new System.Drawing.Point(13, 302);
            this.rjTextBox5.Margin = new System.Windows.Forms.Padding(4);
            this.rjTextBox5.Multiline = false;
            this.rjTextBox5.Name = "rjTextBox5";
            this.rjTextBox5.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.rjTextBox5.PasswordChar = false;
            this.rjTextBox5.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.rjTextBox5.PlaceholderText = "";
            this.rjTextBox5.ReadOnly = false;
            this.rjTextBox5.Size = new System.Drawing.Size(757, 37);
            this.rjTextBox5.TabIndex = 225;
            this.rjTextBox5.TabStop = false;
            this.rjTextBox5.Tag = "";
            this.rjTextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.rjTextBox5.Texts = "";
            this.rjTextBox5.UnderlinedStyle = false;
            // 
            // FrmParcelamentoCartao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 457);
            this.Controls.Add(this.autoLabel9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtAdicionalPorPacela);
            this.Controls.Add(this.autoLabel8);
            this.Controls.Add(this.radioDebito);
            this.Controls.Add(this.radioCredito);
            this.Controls.Add(this.autoLabel7);
            this.Controls.Add(this.comboAdquirente);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.autoLabel6);
            this.Controls.Add(this.comboBandeiraCartao);
            this.Controls.Add(this.autoLabel5);
            this.Controls.Add(this.txtTarifaAdicional);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.txtDiasRecebimento);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.txtTaxa);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtQtdParcela);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.rjTextBox5);
            this.Name = "FrmParcelamentoCartao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parcelamento";
            this.Load += new System.EventHandler(this.FrmParcelamentoCartao_Load);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBandeiraCartao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboAdquirente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioDebito)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioCredito)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsBandeira)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bandeira)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private FontAwesome.Sharp.IconButton btnFechar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtQtdParcela;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private RJ_UI.Classes.RJTextBox txtTaxa;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private RJ_UI.Classes.RJTextBox txtDiasRecebimento;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private RJ_UI.Classes.RJTextBox txtTarifaAdicional;
        private Syncfusion.WinForms.ListView.SfComboBox comboBandeiraCartao;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private RJ_UI.Classes.RJButton btnSalvar;
        private RJ_UI.Classes.RJButton btnCancelar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel7;
        private Syncfusion.WinForms.ListView.SfComboBox comboAdquirente;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioDebito;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioCredito;
        private RJ_UI.Classes.RJTextBox rjTextBox5;
        private System.Data.DataSet dsBandeira;
        private System.Data.DataTable Bandeira;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel8;
        private MaterialSkin.Controls.MaterialCheckbox chkAtivarAntecipacao;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lbl1;
        private RJ_UI.Classes.RJTextBox txtDiasRecebimentoAntecipacao;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lbl2;
        private RJ_UI.Classes.RJTextBox txtTaxaAntecipacao;
        private System.Windows.Forms.GroupBox groupBox1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel9;
        private RJ_UI.Classes.RJTextBox txtAdicionalPorPacela;
    }
}