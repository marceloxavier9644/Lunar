namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    partial class FrmAniversariantes
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
            Syncfusion.WinForms.DataGrid.GridNumericColumn gridNumericColumn1 = new Syncfusion.WinForms.DataGrid.GridNumericColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn1 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridDateTimeColumn gridDateTimeColumn1 = new Syncfusion.WinForms.DataGrid.GridDateTimeColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn2 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn3 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn4 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn5 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn6 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn7 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn8 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridTextColumn gridTextColumn9 = new Syncfusion.WinForms.DataGrid.GridTextColumn();
            Syncfusion.WinForms.DataGrid.GridCheckBoxColumn gridCheckBoxColumn1 = new Syncfusion.WinForms.DataGrid.GridCheckBoxColumn();
            Syncfusion.WinForms.DataGrid.GridCheckBoxColumn gridCheckBoxColumn2 = new Syncfusion.WinForms.DataGrid.GridCheckBoxColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAniversariantes));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtDataInicial = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtDataFinal = new Syncfusion.WinForms.Input.SfDateTimeEdit();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPesquisa = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridClient = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.btnExportarExcel = new FontAwesome.Sharp.IconButton();
            this.btnEnviarWhats = new Lunar.RJ_UI.Classes.RJButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClient)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Lunar.Properties.Resources.Niver2;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Location = new System.Drawing.Point(708, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(265, 151);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.AllowNull = true;
            this.txtDataInicial.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataInicial.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataInicial.DateTimePattern = Syncfusion.WinForms.Input.Enums.DateTimePattern.Custom;
            this.txtDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtDataInicial.Format = "dd/MM";
            this.txtDataInicial.Location = new System.Drawing.Point(18, 104);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Size = new System.Drawing.Size(110, 35);
            this.txtDataInicial.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.FocusedBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataInicial.TabIndex = 229;
            this.txtDataInicial.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.Black;
            this.autoLabel4.Location = new System.Drawing.Point(134, 85);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(90, 16);
            this.autoLabel4.TabIndex = 232;
            this.autoLabel4.Text = "Dia/Mês Final";
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.AllowNull = true;
            this.txtDataFinal.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDataFinal.DateTimeEditingMode = Syncfusion.WinForms.Input.Enums.DateTimeEditingMode.Mask;
            this.txtDataFinal.DateTimePattern = Syncfusion.WinForms.Input.Enums.DateTimePattern.Custom;
            this.txtDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Format = "dd/MM";
            this.txtDataFinal.Location = new System.Drawing.Point(134, 104);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Size = new System.Drawing.Size(102, 35);
            this.txtDataFinal.Style.BorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.Style.FocusedBorderColor = System.Drawing.Color.White;
            this.txtDataFinal.Style.HoverBorderColor = System.Drawing.Color.Silver;
            this.txtDataFinal.TabIndex = 230;
            this.txtDataFinal.Value = new System.DateTime(2022, 4, 2, 0, 0, 0, 0);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(18, 87);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(95, 16);
            this.autoLabel3.TabIndex = 231;
            this.autoLabel3.Text = "Dia/Mês Inicial";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPesquisa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.txtDataInicial);
            this.panel1.Controls.Add(this.txtDataFinal);
            this.panel1.Controls.Add(this.autoLabel4);
            this.panel1.Controls.Add(this.autoLabel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 151);
            this.panel1.TabIndex = 233;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 25);
            this.label1.TabIndex = 233;
            this.label1.Text = "Pesquisar Aniversariantes";
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisa.FlatAppearance.BorderSize = 0;
            this.btnPesquisa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisa.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisa.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisa.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisa.IconSize = 38;
            this.btnPesquisa.Location = new System.Drawing.Point(256, 105);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisa.TabIndex = 247;
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnEnviarWhats);
            this.panel2.Controls.Add(this.btnExportarExcel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 455);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(973, 100);
            this.panel2.TabIndex = 234;
            // 
            // gridClient
            // 
            this.gridClient.AccessibleName = "Table";
            this.gridClient.AllowEditing = false;
            this.gridClient.AllowFiltering = true;
            this.gridClient.AllowStandardTab = true;
            this.gridClient.AutoGenerateColumns = false;
            this.gridClient.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            this.gridClient.BackColor = System.Drawing.Color.White;
            gridNumericColumn1.AllowEditing = false;
            gridNumericColumn1.AllowFiltering = true;
            gridNumericColumn1.CellStyle.Font.Size = 12F;
            gridNumericColumn1.Format = "0";
            gridNumericColumn1.HeaderStyle.Font.Size = 12F;
            gridNumericColumn1.HeaderText = "Código";
            gridNumericColumn1.MappingName = "Id";
            gridNumericColumn1.MaxValue = 1E+16D;
            gridNumericColumn1.MinValue = 0D;
            gridNumericColumn1.NullDisplayText = "0";
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowFiltering = true;
            gridTextColumn1.CellStyle.Font.Size = 12F;
            gridTextColumn1.HeaderStyle.Font.Size = 12F;
            gridTextColumn1.HeaderText = "Nome";
            gridTextColumn1.MappingName = "RazaoSocial";
            gridDateTimeColumn1.AllowEditing = false;
            gridDateTimeColumn1.AllowFiltering = true;
            gridDateTimeColumn1.CellStyle.Font.Size = 12F;
            gridDateTimeColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridDateTimeColumn1.Format = "dd/MM/yyyy";
            gridDateTimeColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridDateTimeColumn1.HeaderStyle.Font.Size = 12F;
            gridDateTimeColumn1.HeaderText = "Nascimento";
            gridDateTimeColumn1.HeaderTextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            gridDateTimeColumn1.MappingName = "DataNascimento";
            gridDateTimeColumn1.MaxDateTime = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.HeaderStyle.Font.Size = 12F;
            gridTextColumn2.HeaderText = "Telefone";
            gridTextColumn2.MappingName = "PessoaTelefone.Telefone";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowFiltering = true;
            gridTextColumn3.CellStyle.Font.Size = 12F;
            gridTextColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn3.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn3.HeaderStyle.Font.Size = 12F;
            gridTextColumn3.HeaderText = "Idade Hoje";
            gridTextColumn3.MappingName = "Idade";
            gridTextColumn4.AllowEditing = false;
            gridTextColumn4.AllowFiltering = true;
            gridTextColumn4.CellStyle.Font.Size = 12F;
            gridTextColumn4.HeaderStyle.Font.Size = 12F;
            gridTextColumn4.HeaderText = "Apelido/Fantasia";
            gridTextColumn4.MappingName = "NomeFantasia";
            gridTextColumn5.AllowEditing = false;
            gridTextColumn5.AllowFiltering = true;
            gridTextColumn5.CellStyle.Font.Size = 12F;
            gridTextColumn5.HeaderStyle.Font.Size = 12F;
            gridTextColumn5.HeaderText = "CPF/CNPJ";
            gridTextColumn5.MappingName = "Cnpj";
            gridTextColumn6.AllowEditing = false;
            gridTextColumn6.AllowFiltering = true;
            gridTextColumn6.CellStyle.Font.Size = 12F;
            gridTextColumn6.HeaderStyle.Font.Size = 12F;
            gridTextColumn6.HeaderText = "Endereco";
            gridTextColumn6.MappingName = "EnderecoPrincipal.Logradouro";
            gridTextColumn7.AllowEditing = false;
            gridTextColumn7.AllowFiltering = true;
            gridTextColumn7.CellStyle.Font.Size = 12F;
            gridTextColumn7.HeaderStyle.Font.Size = 12F;
            gridTextColumn7.HeaderText = "Nº";
            gridTextColumn7.MappingName = "EnderecoPrincipal.Numero";
            gridTextColumn8.AllowEditing = false;
            gridTextColumn8.AllowFiltering = true;
            gridTextColumn8.CellStyle.Font.Size = 12F;
            gridTextColumn8.HeaderStyle.Font.Size = 12F;
            gridTextColumn8.HeaderText = "Cidade";
            gridTextColumn8.MappingName = "EnderecoPrincipal.Cidade.Descricao";
            gridTextColumn9.AllowEditing = false;
            gridTextColumn9.AllowFiltering = true;
            gridTextColumn9.CellStyle.Font.Size = 12F;
            gridTextColumn9.HeaderStyle.Font.Size = 12F;
            gridTextColumn9.HeaderText = "Bairro";
            gridTextColumn9.MappingName = "EnderecoPrincipal.Bairro";
            gridCheckBoxColumn1.AllowEditing = false;
            gridCheckBoxColumn1.AllowFiltering = true;
            gridCheckBoxColumn1.CellStyle.Font.Size = 12F;
            gridCheckBoxColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridCheckBoxColumn1.HeaderStyle.Font.Size = 12F;
            gridCheckBoxColumn1.HeaderText = "SPC/SERASA";
            gridCheckBoxColumn1.MappingName = "RegistradoSpc";
            gridCheckBoxColumn2.AllowEditing = false;
            gridCheckBoxColumn2.AllowFiltering = true;
            gridCheckBoxColumn2.CellStyle.Font.Size = 12F;
            gridCheckBoxColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridCheckBoxColumn2.HeaderStyle.Font.Size = 12F;
            gridCheckBoxColumn2.HeaderText = "Escritório Cobrança";
            gridCheckBoxColumn2.MappingName = "EscritorioCobranca";
            this.gridClient.Columns.Add(gridNumericColumn1);
            this.gridClient.Columns.Add(gridTextColumn1);
            this.gridClient.Columns.Add(gridDateTimeColumn1);
            this.gridClient.Columns.Add(gridTextColumn2);
            this.gridClient.Columns.Add(gridTextColumn3);
            this.gridClient.Columns.Add(gridTextColumn4);
            this.gridClient.Columns.Add(gridTextColumn5);
            this.gridClient.Columns.Add(gridTextColumn6);
            this.gridClient.Columns.Add(gridTextColumn7);
            this.gridClient.Columns.Add(gridTextColumn8);
            this.gridClient.Columns.Add(gridTextColumn9);
            this.gridClient.Columns.Add(gridCheckBoxColumn1);
            this.gridClient.Columns.Add(gridCheckBoxColumn2);
            this.gridClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridClient.Location = new System.Drawing.Point(0, 151);
            this.gridClient.Name = "gridClient";
            this.gridClient.Size = new System.Drawing.Size(973, 304);
            this.gridClient.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.gridClient.Style.CellStyle.Font.Facename = "Montserrat";
            this.gridClient.Style.CellStyle.Font.Size = 12F;
            this.gridClient.TabIndex = 235;
            this.gridClient.QueryCellStyle += new Syncfusion.WinForms.DataGrid.Events.QueryCellStyleEventHandler(this.gridClient_QueryCellStyle);
            this.gridClient.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.gridClient_QueryRowStyle);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnExportarExcel.FlatAppearance.BorderSize = 0;
            this.btnExportarExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExportarExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarExcel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnExportarExcel.IconColor = System.Drawing.Color.DarkGreen;
            this.btnExportarExcel.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnExportarExcel.IconSize = 38;
            this.btnExportarExcel.Location = new System.Drawing.Point(18, 36);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(36, 34);
            this.btnExportarExcel.TabIndex = 217;
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // btnEnviarWhats
            // 
            this.btnEnviarWhats.BackColor = System.Drawing.Color.White;
            this.btnEnviarWhats.BackgroundColor = System.Drawing.Color.White;
            this.btnEnviarWhats.BackgroundImage = global::Lunar.Properties.Resources.whatsapp_logo_icone;
            this.btnEnviarWhats.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEnviarWhats.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnEnviarWhats.BorderRadius = 8;
            this.btnEnviarWhats.BorderSize = 2;
            this.btnEnviarWhats.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviarWhats.FlatAppearance.BorderSize = 0;
            this.btnEnviarWhats.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEnviarWhats.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEnviarWhats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarWhats.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarWhats.ForeColor = System.Drawing.Color.Black;
            this.btnEnviarWhats.Location = new System.Drawing.Point(60, 33);
            this.btnEnviarWhats.Name = "btnEnviarWhats";
            this.btnEnviarWhats.Size = new System.Drawing.Size(35, 37);
            this.btnEnviarWhats.TabIndex = 259;
            this.btnEnviarWhats.TextColor = System.Drawing.Color.Black;
            this.btnEnviarWhats.UseVisualStyleBackColor = false;
            this.btnEnviarWhats.Click += new System.EventHandler(this.btnEnviarWhats_Click);
            // 
            // FrmAniversariantes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(973, 555);
            this.Controls.Add(this.gridClient);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAniversariantes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aniversariantes";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridClient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataInicial;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.WinForms.Input.SfDateTimeEdit txtDataFinal;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnPesquisa;
        private System.Windows.Forms.Panel panel2;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridClient;
        private FontAwesome.Sharp.IconButton btnExportarExcel;
        private RJ_UI.Classes.RJButton btnEnviarWhats;
    }
}