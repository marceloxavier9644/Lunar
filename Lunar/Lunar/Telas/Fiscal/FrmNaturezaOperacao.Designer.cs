namespace Lunar.Telas.Fiscal
{
    partial class FrmNaturezaOperacao
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
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.btnSalvar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnCancelar = new Lunar.RJ_UI.Classes.RJButton();
            this.autoLabel7 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDescricao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboFinalidade = new Syncfusion.WinForms.ListView.SfComboBox();
            this.chkGerarFinanceiro = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.chkMovimentaEstoque = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioSaida = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radioEntrada = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.txtId = new Lunar.RJ_UI.Classes.RJTextBox();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboFinalidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGerarFinanceiro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMovimentaEstoque)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioSaida)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioEntrada)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel2);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(800, 44);
            this.panelTitleBar.TabIndex = 206;
            // 
            // autoLabel2
            // 
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.White;
            this.autoLabel2.Location = new System.Drawing.Point(5, 9);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(229, 25);
            this.autoLabel2.TabIndex = 198;
            this.autoLabel2.Text = "Natureza da Operação";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClose.IconSize = 30;
            this.btnClose.Location = new System.Drawing.Point(759, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.btnSalvar.Location = new System.Drawing.Point(488, 295);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(298, 45);
            this.btnSalvar.TabIndex = 4;
            this.btnSalvar.Text = "Salvar [F5]";
            this.btnSalvar.TextColor = System.Drawing.Color.White;
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
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
            this.btnCancelar.Location = new System.Drawing.Point(184, 295);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(298, 45);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.TabStop = false;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // autoLabel7
            // 
            this.autoLabel7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.autoLabel7.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel7.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel7.Font = new System.Drawing.Font("Montserrat", 9.749999F);
            this.autoLabel7.ForeColor = System.Drawing.Color.Black;
            this.autoLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.autoLabel7.Location = new System.Drawing.Point(23, 53);
            this.autoLabel7.Name = "autoLabel7";
            this.autoLabel7.Size = new System.Drawing.Size(202, 21);
            this.autoLabel7.TabIndex = 267;
            this.autoLabel7.Text = "Descrição Natureza Operação";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescricao.BackColor = System.Drawing.Color.White;
            this.txtDescricao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricao.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricao.BorderRadius = 8;
            this.txtDescricao.BorderSize = 2;
            this.txtDescricao.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDescricao.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.txtDescricao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDescricao.Location = new System.Drawing.Point(13, 70);
            this.txtDescricao.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricao.Multiline = false;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescricao.PasswordChar = false;
            this.txtDescricao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDescricao.PlaceholderText = "";
            this.txtDescricao.ReadOnly = false;
            this.txtDescricao.Size = new System.Drawing.Size(773, 44);
            this.txtDescricao.TabIndex = 0;
            this.txtDescricao.Tag = "";
            this.txtDescricao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescricao.Texts = "";
            this.txtDescricao.UnderlinedStyle = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Montserrat", 9.749999F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(13, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 21);
            this.label6.TabIndex = 269;
            this.label6.Text = "Finalidade";
            // 
            // comboFinalidade
            // 
            this.comboFinalidade.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboFinalidade.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboFinalidade.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboFinalidade.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.comboFinalidade.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.comboFinalidade.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboFinalidade.Location = new System.Drawing.Point(13, 142);
            this.comboFinalidade.MaxDropDownItems = 15;
            this.comboFinalidade.Name = "comboFinalidade";
            this.comboFinalidade.Size = new System.Drawing.Size(556, 34);
            this.comboFinalidade.Style.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboFinalidade.Style.ClearButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboFinalidade.Style.DropDownButtonStyle.DisabledForeColor = System.Drawing.Color.White;
            this.comboFinalidade.Style.DropDownStyle.BorderColor = System.Drawing.Color.White;
            this.comboFinalidade.Style.DropDownStyle.GripperForeColor = System.Drawing.Color.White;
            this.comboFinalidade.Style.EditorStyle.BorderColor = System.Drawing.Color.White;
            this.comboFinalidade.Style.EditorStyle.DisabledBackColor = System.Drawing.Color.White;
            this.comboFinalidade.Style.EditorStyle.FocusedBorderColor = System.Drawing.Color.White;
            this.comboFinalidade.Style.EditorStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboFinalidade.Style.ReadOnlyEditorStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboFinalidade.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboFinalidade.Style.TokenStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboFinalidade.Style.ToolTipStyle.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboFinalidade.Style.ToolTipStyle.SeparatorColor = System.Drawing.Color.White;
            this.comboFinalidade.TabIndex = 1;
            this.comboFinalidade.ToolTipOption.ShadowVisible = false;
            // 
            // chkGerarFinanceiro
            // 
            this.chkGerarFinanceiro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkGerarFinanceiro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkGerarFinanceiro.BeforeTouchSize = new System.Drawing.Size(235, 33);
            this.chkGerarFinanceiro.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.chkGerarFinanceiro.Checked = true;
            this.chkGerarFinanceiro.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGerarFinanceiro.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGerarFinanceiro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.chkGerarFinanceiro.HotBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.chkGerarFinanceiro.Location = new System.Drawing.Point(13, 197);
            this.chkGerarFinanceiro.Name = "chkGerarFinanceiro";
            this.chkGerarFinanceiro.Size = new System.Drawing.Size(235, 33);
            this.chkGerarFinanceiro.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Office2016White;
            this.chkGerarFinanceiro.TabIndex = 2;
            this.chkGerarFinanceiro.Text = " Movimenta Financeiro";
            this.chkGerarFinanceiro.ThemeName = "Office2016White";
            // 
            // chkMovimentaEstoque
            // 
            this.chkMovimentaEstoque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMovimentaEstoque.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkMovimentaEstoque.BeforeTouchSize = new System.Drawing.Size(217, 33);
            this.chkMovimentaEstoque.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.chkMovimentaEstoque.Checked = true;
            this.chkMovimentaEstoque.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMovimentaEstoque.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMovimentaEstoque.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.chkMovimentaEstoque.HotBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.chkMovimentaEstoque.Location = new System.Drawing.Point(265, 197);
            this.chkMovimentaEstoque.Name = "chkMovimentaEstoque";
            this.chkMovimentaEstoque.Size = new System.Drawing.Size(217, 33);
            this.chkMovimentaEstoque.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Office2016White;
            this.chkMovimentaEstoque.TabIndex = 3;
            this.chkMovimentaEstoque.Text = " Movimenta Estoque";
            this.chkMovimentaEstoque.ThemeName = "Office2016White";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioSaida);
            this.groupBox3.Controls.Add(this.radioEntrada);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(575, 121);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(211, 60);
            this.groupBox3.TabIndex = 272;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo de Emissão";
            // 
            // radioSaida
            // 
            this.radioSaida.BeforeTouchSize = new System.Drawing.Size(76, 21);
            this.radioSaida.Checked = true;
            this.radioSaida.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioSaida.Location = new System.Drawing.Point(122, 26);
            this.radioSaida.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioSaida.Name = "radioSaida";
            this.radioSaida.Size = new System.Drawing.Size(76, 21);
            this.radioSaida.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2007;
            this.radioSaida.TabIndex = 1;
            this.radioSaida.Text = " Saída";
            this.radioSaida.ThemeName = "Office2007";
            // 
            // radioEntrada
            // 
            this.radioEntrada.BeforeTouchSize = new System.Drawing.Size(91, 21);
            this.radioEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioEntrada.Location = new System.Drawing.Point(12, 26);
            this.radioEntrada.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.radioEntrada.Name = "radioEntrada";
            this.radioEntrada.Size = new System.Drawing.Size(91, 21);
            this.radioEntrada.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2007;
            this.radioEntrada.TabIndex = 0;
            this.radioEntrada.TabStop = false;
            this.radioEntrada.Text = " Entrada";
            this.radioEntrada.ThemeName = "Office2007";
            // 
            // txtId
            // 
            this.txtId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtId.BackColor = System.Drawing.Color.White;
            this.txtId.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtId.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtId.BorderRadius = 8;
            this.txtId.BorderSize = 2;
            this.txtId.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtId.Font = new System.Drawing.Font("Montserrat", 14.25F);
            this.txtId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtId.Location = new System.Drawing.Point(649, 197);
            this.txtId.Margin = new System.Windows.Forms.Padding(4);
            this.txtId.Multiline = false;
            this.txtId.Name = "txtId";
            this.txtId.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtId.PasswordChar = false;
            this.txtId.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtId.PlaceholderText = "";
            this.txtId.ReadOnly = false;
            this.txtId.Size = new System.Drawing.Size(137, 44);
            this.txtId.TabIndex = 273;
            this.txtId.Tag = "";
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtId.Texts = "";
            this.txtId.UnderlinedStyle = false;
            this.txtId.Visible = false;
            // 
            // FrmNaturezaOperacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 355);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.chkMovimentaEstoque);
            this.Controls.Add(this.chkGerarFinanceiro);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboFinalidade);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.autoLabel7);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmNaturezaOperacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Natureza Operação";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmNaturezaOperacao_KeyDown);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboFinalidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGerarFinanceiro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMovimentaEstoque)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioSaida)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioEntrada)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private FontAwesome.Sharp.IconButton btnClose;
        private RJ_UI.Classes.RJButton btnSalvar;
        private RJ_UI.Classes.RJButton btnCancelar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel7;
        private RJ_UI.Classes.RJTextBox txtDescricao;
        private System.Windows.Forms.Label label6;
        private Syncfusion.WinForms.ListView.SfComboBox comboFinalidade;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv chkGerarFinanceiro;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv chkMovimentaEstoque;
        private System.Windows.Forms.GroupBox groupBox3;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioSaida;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radioEntrada;
        private RJ_UI.Classes.RJTextBox txtId;
    }
}