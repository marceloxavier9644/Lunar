namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    partial class FrmAlertaMensagemCliente
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
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.autoLabel25 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnFechar = new FontAwesome.Sharp.IconButton();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCPF = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCodCliente = new Lunar.RJ_UI.Classes.RJTextBox();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCliente = new Lunar.RJ_UI.Classes.RJTextBox();
            this.gridAlerta = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDescricao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnConfirmar = new FontAwesome.Sharp.IconButton();
            this.txtID = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnExcluir = new Lunar.RJ_UI.Classes.RJButton();
            this.btnFechar2 = new Lunar.RJ_UI.Classes.RJButton();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAlerta)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTitleBar.Controls.Add(this.autoLabel25);
            this.panelTitleBar.Controls.Add(this.btnFechar);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(797, 38);
            this.panelTitleBar.TabIndex = 258;
            // 
            // autoLabel25
            // 
            this.autoLabel25.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel25.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel25.ForeColor = System.Drawing.Color.White;
            this.autoLabel25.Location = new System.Drawing.Point(5, 1);
            this.autoLabel25.Name = "autoLabel25";
            this.autoLabel25.Size = new System.Drawing.Size(328, 35);
            this.autoLabel25.TabIndex = 198;
            this.autoLabel25.Text = "Cadastro de Mensagem/Alerta";
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
            this.btnFechar.Location = new System.Drawing.Point(756, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(39, 41);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // autoLabel3
            // 
            this.autoLabel3.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel3.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(584, 40);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(73, 21);
            this.autoLabel3.TabIndex = 265;
            this.autoLabel3.Text = "CPF/CNPJ";
            // 
            // txtCPF
            // 
            this.txtCPF.BackColor = System.Drawing.Color.White;
            this.txtCPF.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCPF.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCPF.BorderRadius = 8;
            this.txtCPF.BorderSize = 2;
            this.txtCPF.Enabled = false;
            this.txtCPF.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtCPF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCPF.Location = new System.Drawing.Point(584, 60);
            this.txtCPF.Margin = new System.Windows.Forms.Padding(4);
            this.txtCPF.Multiline = false;
            this.txtCPF.Name = "txtCPF";
            this.txtCPF.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCPF.PasswordChar = false;
            this.txtCPF.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCPF.PlaceholderText = "";
            this.txtCPF.ReadOnly = false;
            this.txtCPF.Size = new System.Drawing.Size(199, 37);
            this.txtCPF.TabIndex = 264;
            this.txtCPF.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCPF.Texts = "";
            this.txtCPF.UnderlinedStyle = false;
            // 
            // autoLabel1
            // 
            this.autoLabel1.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel1.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.Black;
            this.autoLabel1.Location = new System.Drawing.Point(478, 40);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(56, 21);
            this.autoLabel1.TabIndex = 263;
            this.autoLabel1.Text = "Código";
            // 
            // txtCodCliente
            // 
            this.txtCodCliente.BackColor = System.Drawing.Color.White;
            this.txtCodCliente.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodCliente.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodCliente.BorderRadius = 8;
            this.txtCodCliente.BorderSize = 2;
            this.txtCodCliente.Enabled = false;
            this.txtCodCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtCodCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodCliente.Location = new System.Drawing.Point(478, 60);
            this.txtCodCliente.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodCliente.Multiline = false;
            this.txtCodCliente.Name = "txtCodCliente";
            this.txtCodCliente.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodCliente.PasswordChar = false;
            this.txtCodCliente.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodCliente.PlaceholderText = "";
            this.txtCodCliente.ReadOnly = false;
            this.txtCodCliente.Size = new System.Drawing.Size(98, 37);
            this.txtCodCliente.TabIndex = 262;
            this.txtCodCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodCliente.Texts = "";
            this.txtCodCliente.UnderlinedStyle = false;
            // 
            // autoLabel2
            // 
            this.autoLabel2.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel2.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.Black;
            this.autoLabel2.Location = new System.Drawing.Point(13, 40);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(57, 21);
            this.autoLabel2.TabIndex = 261;
            this.autoLabel2.Text = "Cliente";
            // 
            // txtCliente
            // 
            this.txtCliente.BackColor = System.Drawing.Color.White;
            this.txtCliente.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCliente.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCliente.BorderRadius = 8;
            this.txtCliente.BorderSize = 2;
            this.txtCliente.Enabled = false;
            this.txtCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCliente.Location = new System.Drawing.Point(13, 60);
            this.txtCliente.Margin = new System.Windows.Forms.Padding(4);
            this.txtCliente.Multiline = false;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCliente.PasswordChar = false;
            this.txtCliente.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCliente.PlaceholderText = "";
            this.txtCliente.ReadOnly = false;
            this.txtCliente.Size = new System.Drawing.Size(457, 37);
            this.txtCliente.TabIndex = 260;
            this.txtCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCliente.Texts = "";
            this.txtCliente.UnderlinedStyle = false;
            // 
            // gridAlerta
            // 
            this.gridAlerta.AccessibleName = "Table";
            this.gridAlerta.AllowEditing = false;
            this.gridAlerta.AllowSorting = false;
            this.gridAlerta.AutoGenerateColumns = false;
            this.gridAlerta.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowSorting = false;
            gridTextColumn1.HeaderText = "ID";
            gridTextColumn1.MappingName = "Id";
            gridTextColumn1.Visible = false;
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowSorting = false;
            gridTextColumn2.Format = "dd/MM/yyyy";
            gridTextColumn2.HeaderText = "Data";
            gridTextColumn2.MappingName = "Data";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowSorting = false;
            gridTextColumn3.HeaderText = "Mensagem";
            gridTextColumn3.MappingName = "Descricao";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowSorting = false;
            gridTextColumn4.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn4.HeaderText = "Operador Cadastro";
            gridTextColumn4.MappingName = "OperadorCadastro";
            gridTextColumn5.AllowEditing = false;
            gridTextColumn5.AllowSorting = false;
            gridTextColumn5.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn5.HeaderText = "Operador Alteração";
            gridTextColumn5.MappingName = "OperadorAlteracao";
            this.gridAlerta.Columns.Add(gridTextColumn1);
            this.gridAlerta.Columns.Add(gridTextColumn2);
            this.gridAlerta.Columns.Add(gridTextColumn3);
            this.gridAlerta.Columns.Add(gridTextColumn4);
            this.gridAlerta.Columns.Add(gridTextColumn5);
            this.gridAlerta.Location = new System.Drawing.Point(13, 187);
            this.gridAlerta.Name = "gridAlerta";
            this.gridAlerta.Size = new System.Drawing.Size(770, 150);
            this.gridAlerta.TabIndex = 266;
            this.gridAlerta.Text = "sfDataGrid1";
            this.gridAlerta.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.gridAlerta_QueryRowStyle);
            this.gridAlerta.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.gridAlerta_CellDoubleClick);
            // 
            // autoLabel4
            // 
            this.autoLabel4.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel4.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(13, 101);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(83, 21);
            this.autoLabel4.TabIndex = 268;
            this.autoLabel4.Text = "Mensagem";
            // 
            // txtDescricao
            // 
            this.txtDescricao.BackColor = System.Drawing.Color.White;
            this.txtDescricao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricao.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricao.BorderRadius = 8;
            this.txtDescricao.BorderSize = 2;
            this.txtDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDescricao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDescricao.Location = new System.Drawing.Point(13, 123);
            this.txtDescricao.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescricao.PasswordChar = false;
            this.txtDescricao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDescricao.PlaceholderText = "";
            this.txtDescricao.ReadOnly = false;
            this.txtDescricao.Size = new System.Drawing.Size(710, 57);
            this.txtDescricao.TabIndex = 267;
            this.txtDescricao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescricao.Texts = "";
            this.txtDescricao.UnderlinedStyle = false;
            this.txtDescricao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescricao_KeyPress);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnConfirmar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnConfirmar.IconColor = System.Drawing.Color.Green;
            this.btnConfirmar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnConfirmar.IconSize = 38;
            this.btnConfirmar.Location = new System.Drawing.Point(730, 124);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(53, 57);
            this.btnConfirmar.TabIndex = 269;
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.White;
            this.txtID.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtID.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtID.BorderRadius = 8;
            this.txtID.BorderSize = 2;
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtID.Location = new System.Drawing.Point(13, 344);
            this.txtID.Margin = new System.Windows.Forms.Padding(4);
            this.txtID.Multiline = false;
            this.txtID.Name = "txtID";
            this.txtID.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtID.PasswordChar = false;
            this.txtID.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtID.PlaceholderText = "";
            this.txtID.ReadOnly = false;
            this.txtID.Size = new System.Drawing.Size(98, 37);
            this.txtID.TabIndex = 270;
            this.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtID.Texts = "";
            this.txtID.UnderlinedStyle = false;
            this.txtID.Visible = false;
            // 
            // btnExcluir
            // 
            this.btnExcluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcluir.BackColor = System.Drawing.Color.White;
            this.btnExcluir.BackgroundColor = System.Drawing.Color.White;
            this.btnExcluir.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExcluir.BorderRadius = 8;
            this.btnExcluir.BorderSize = 2;
            this.btnExcluir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExcluir.FlatAppearance.BorderSize = 2;
            this.btnExcluir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnExcluir.Location = new System.Drawing.Point(441, 344);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(168, 45);
            this.btnExcluir.TabIndex = 271;
            this.btnExcluir.Text = "Excluir Mensagem";
            this.btnExcluir.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnFechar2
            // 
            this.btnFechar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnFechar2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnFechar2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnFechar2.BorderRadius = 8;
            this.btnFechar2.BorderSize = 0;
            this.btnFechar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar2.FlatAppearance.BorderSize = 0;
            this.btnFechar2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar2.ForeColor = System.Drawing.Color.White;
            this.btnFechar2.Location = new System.Drawing.Point(615, 344);
            this.btnFechar2.Name = "btnFechar2";
            this.btnFechar2.Size = new System.Drawing.Size(168, 45);
            this.btnFechar2.TabIndex = 272;
            this.btnFechar2.Text = "Fechar";
            this.btnFechar2.TextColor = System.Drawing.Color.White;
            this.btnFechar2.UseVisualStyleBackColor = false;
            this.btnFechar2.Click += new System.EventHandler(this.btnFechar2_Click);
            // 
            // FrmAlertaMensagemCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(797, 395);
            this.Controls.Add(this.btnFechar2);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.gridAlerta);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.txtCPF);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtCodCliente);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmAlertaMensagemCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAlertaMensagemCliente";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAlerta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel25;
        private FontAwesome.Sharp.IconButton btnFechar;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private RJ_UI.Classes.RJTextBox txtCPF;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtCodCliente;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private RJ_UI.Classes.RJTextBox txtCliente;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridAlerta;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private RJ_UI.Classes.RJTextBox txtDescricao;
        private FontAwesome.Sharp.IconButton btnConfirmar;
        private RJ_UI.Classes.RJTextBox txtID;
        private RJ_UI.Classes.RJButton btnExcluir;
        private RJ_UI.Classes.RJButton btnFechar2;
    }
}