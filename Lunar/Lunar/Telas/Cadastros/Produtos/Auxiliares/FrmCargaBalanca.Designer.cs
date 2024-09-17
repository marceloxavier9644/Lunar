namespace Lunar.Telas.Cadastros.Produtos.Auxiliares
{
    partial class FrmCargaBalanca
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCargaBalanca));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnParar = new FontAwesome.Sharp.IconButton();
            this.progressBarBalanca = new System.Windows.Forms.ProgressBar();
            this.btnGerarTxt = new MaterialSkin.Controls.MaterialButton();
            this.btnScan = new MaterialSkin.Controls.MaterialButton();
            this.txtPortaBalanca = new Syncfusion.Windows.Forms.Tools.MaskedEditBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIpBalanca = new Syncfusion.Windows.Forms.Tools.MaskedEditBox();
            this.btnTestarComunicacao = new MaterialSkin.Controls.MaterialButton();
            this.label1 = new System.Windows.Forms.Label();
            this.autoLabel23 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnPesquisaProduto = new FontAwesome.Sharp.IconButton();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEnviarCarga = new MaterialSkin.Controls.MaterialButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnGerarArquivo = new MaterialSkin.Controls.MaterialButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gridProdutos = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.btnConfirmaItem = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtPesquisaProduto = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodProduto = new Lunar.RJ_UI.Classes.RJTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPortaBalanca)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIpBalanca)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProdutos)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnParar);
            this.panel1.Controls.Add(this.progressBarBalanca);
            this.panel1.Controls.Add(this.btnGerarTxt);
            this.panel1.Controls.Add(this.btnScan);
            this.panel1.Controls.Add(this.txtPortaBalanca);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtIpBalanca);
            this.panel1.Controls.Add(this.btnTestarComunicacao);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 136);
            this.panel1.TabIndex = 0;
            // 
            // btnParar
            // 
            this.btnParar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnParar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnParar.FlatAppearance.BorderSize = 0;
            this.btnParar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnParar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnParar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnParar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParar.IconChar = FontAwesome.Sharp.IconChar.StopCircle;
            this.btnParar.IconColor = System.Drawing.Color.Red;
            this.btnParar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnParar.IconSize = 38;
            this.btnParar.Location = new System.Drawing.Point(446, 63);
            this.btnParar.Name = "btnParar";
            this.btnParar.Size = new System.Drawing.Size(36, 30);
            this.btnParar.TabIndex = 237;
            this.btnParar.UseVisualStyleBackColor = true;
            this.btnParar.Visible = false;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);
            // 
            // progressBarBalanca
            // 
            this.progressBarBalanca.Location = new System.Drawing.Point(15, 101);
            this.progressBarBalanca.Name = "progressBarBalanca";
            this.progressBarBalanca.Size = new System.Drawing.Size(424, 23);
            this.progressBarBalanca.TabIndex = 8;
            this.progressBarBalanca.Visible = false;
            // 
            // btnGerarTxt
            // 
            this.btnGerarTxt.AutoSize = false;
            this.btnGerarTxt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGerarTxt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGerarTxt.Depth = 0;
            this.btnGerarTxt.DrawShadows = true;
            this.btnGerarTxt.HighEmphasis = true;
            this.btnGerarTxt.Icon = null;
            this.btnGerarTxt.Location = new System.Drawing.Point(343, 63);
            this.btnGerarTxt.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGerarTxt.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnGerarTxt.Name = "btnGerarTxt";
            this.btnGerarTxt.Size = new System.Drawing.Size(96, 29);
            this.btnGerarTxt.TabIndex = 7;
            this.btnGerarTxt.Text = "Gerar TXT";
            this.btnGerarTxt.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnGerarTxt.UseAccentColor = false;
            this.btnGerarTxt.UseVisualStyleBackColor = true;
            this.btnGerarTxt.Click += new System.EventHandler(this.btnGerarTxt_Click);
            // 
            // btnScan
            // 
            this.btnScan.AutoSize = false;
            this.btnScan.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScan.Depth = 0;
            this.btnScan.DrawShadows = true;
            this.btnScan.HighEmphasis = true;
            this.btnScan.Icon = null;
            this.btnScan.Location = new System.Drawing.Point(239, 63);
            this.btnScan.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnScan.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(96, 29);
            this.btnScan.TabIndex = 6;
            this.btnScan.Text = "Scan";
            this.btnScan.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnScan.UseAccentColor = false;
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // txtPortaBalanca
            // 
            this.txtPortaBalanca.BeforeTouchSize = new System.Drawing.Size(294, 24);
            this.txtPortaBalanca.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPortaBalanca.Location = new System.Drawing.Point(315, 30);
            this.txtPortaBalanca.Name = "txtPortaBalanca";
            this.txtPortaBalanca.Size = new System.Drawing.Size(124, 24);
            this.txtPortaBalanca.TabIndex = 5;
            this.txtPortaBalanca.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPortaBalanca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPortaBalanca_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(312, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Porta Balança";
            // 
            // txtIpBalanca
            // 
            this.txtIpBalanca.BeforeTouchSize = new System.Drawing.Size(294, 24);
            this.txtIpBalanca.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIpBalanca.Location = new System.Drawing.Point(15, 30);
            this.txtIpBalanca.Name = "txtIpBalanca";
            this.txtIpBalanca.Size = new System.Drawing.Size(294, 24);
            this.txtIpBalanca.TabIndex = 3;
            this.txtIpBalanca.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIpBalanca.Leave += new System.EventHandler(this.txtIpBalanca_Leave);
            // 
            // btnTestarComunicacao
            // 
            this.btnTestarComunicacao.AutoSize = false;
            this.btnTestarComunicacao.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnTestarComunicacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestarComunicacao.Depth = 0;
            this.btnTestarComunicacao.DrawShadows = true;
            this.btnTestarComunicacao.HighEmphasis = true;
            this.btnTestarComunicacao.Icon = null;
            this.btnTestarComunicacao.Location = new System.Drawing.Point(15, 63);
            this.btnTestarComunicacao.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnTestarComunicacao.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnTestarComunicacao.Name = "btnTestarComunicacao";
            this.btnTestarComunicacao.Size = new System.Drawing.Size(216, 29);
            this.btnTestarComunicacao.TabIndex = 2;
            this.btnTestarComunicacao.Text = "Testar Comunicação";
            this.btnTestarComunicacao.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnTestarComunicacao.UseAccentColor = false;
            this.btnTestarComunicacao.UseVisualStyleBackColor = true;
            this.btnTestarComunicacao.Click += new System.EventHandler(this.btnTestarComunicacao_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP Balança";
            // 
            // autoLabel23
            // 
            this.autoLabel23.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel23.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel23.ForeColor = System.Drawing.Color.Black;
            this.autoLabel23.Location = new System.Drawing.Point(15, 43);
            this.autoLabel23.Name = "autoLabel23";
            this.autoLabel23.Size = new System.Drawing.Size(101, 16);
            this.autoLabel23.TabIndex = 209;
            this.autoLabel23.Text = "Código Produto";
            // 
            // autoLabel3
            // 
            this.autoLabel3.BackColor = System.Drawing.Color.Transparent;
            this.autoLabel3.Cursor = System.Windows.Forms.Cursors.Default;
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.Black;
            this.autoLabel3.Location = new System.Drawing.Point(226, 44);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(133, 16);
            this.autoLabel3.TabIndex = 208;
            this.autoLabel3.Text = "Pesquisa de Produto";
            // 
            // btnPesquisaProduto
            // 
            this.btnPesquisaProduto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaProduto.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaProduto.FlatAppearance.BorderSize = 0;
            this.btnPesquisaProduto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaProduto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaProduto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaProduto.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaProduto.IconColor = System.Drawing.Color.SlateGray;
            this.btnPesquisaProduto.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaProduto.IconSize = 38;
            this.btnPesquisaProduto.Location = new System.Drawing.Point(183, 67);
            this.btnPesquisaProduto.Name = "btnPesquisaProduto";
            this.btnPesquisaProduto.Size = new System.Drawing.Size(36, 34);
            this.btnPesquisaProduto.TabIndex = 207;
            this.btnPesquisaProduto.UseVisualStyleBackColor = true;
            this.btnPesquisaProduto.Click += new System.EventHandler(this.btnPesquisaProduto_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(262, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Selecionar Produtos Para Envio";
            // 
            // btnEnviarCarga
            // 
            this.btnEnviarCarga.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnviarCarga.AutoSize = false;
            this.btnEnviarCarga.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEnviarCarga.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviarCarga.Depth = 0;
            this.btnEnviarCarga.DrawShadows = true;
            this.btnEnviarCarga.Enabled = false;
            this.btnEnviarCarga.HighEmphasis = true;
            this.btnEnviarCarga.Icon = null;
            this.btnEnviarCarga.Location = new System.Drawing.Point(702, 12);
            this.btnEnviarCarga.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnEnviarCarga.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnEnviarCarga.Name = "btnEnviarCarga";
            this.btnEnviarCarga.Size = new System.Drawing.Size(112, 36);
            this.btnEnviarCarga.TabIndex = 212;
            this.btnEnviarCarga.Text = "Enviar Carga";
            this.btnEnviarCarga.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnEnviarCarga.UseAccentColor = false;
            this.btnEnviarCarga.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnGerarArquivo);
            this.panel4.Controls.Add(this.btnEnviarCarga);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 504);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(818, 54);
            this.panel4.TabIndex = 212;
            // 
            // btnGerarArquivo
            // 
            this.btnGerarArquivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGerarArquivo.AutoSize = false;
            this.btnGerarArquivo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGerarArquivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGerarArquivo.Depth = 0;
            this.btnGerarArquivo.DrawShadows = true;
            this.btnGerarArquivo.HighEmphasis = true;
            this.btnGerarArquivo.Icon = null;
            this.btnGerarArquivo.Location = new System.Drawing.Point(582, 12);
            this.btnGerarArquivo.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGerarArquivo.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnGerarArquivo.Name = "btnGerarArquivo";
            this.btnGerarArquivo.Size = new System.Drawing.Size(112, 36);
            this.btnGerarArquivo.TabIndex = 213;
            this.btnGerarArquivo.Text = "Gerar TXT";
            this.btnGerarArquivo.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnGerarArquivo.UseAccentColor = false;
            this.btnGerarArquivo.UseVisualStyleBackColor = true;
            this.btnGerarArquivo.Click += new System.EventHandler(this.btnGerarArquivo_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.gridProdutos);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 264);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(818, 240);
            this.panel3.TabIndex = 213;
            // 
            // gridProdutos
            // 
            this.gridProdutos.AccessibleName = "Table";
            this.gridProdutos.AllowEditing = false;
            this.gridProdutos.AllowResizingColumns = true;
            this.gridProdutos.AllowSorting = false;
            this.gridProdutos.AutoGenerateColumns = false;
            this.gridProdutos.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            this.gridProdutos.BackColor = System.Drawing.Color.White;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.AllowFiltering = true;
            gridTextColumn1.AllowResizing = true;
            gridTextColumn1.AllowSorting = false;
            gridTextColumn1.CellStyle.Font.Size = 12F;
            gridTextColumn1.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderText = "Código";
            gridTextColumn1.MappingName = "Id";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.AllowFiltering = true;
            gridTextColumn2.AllowResizing = true;
            gridTextColumn2.AllowSorting = false;
            gridTextColumn2.CellStyle.Font.Size = 12F;
            gridTextColumn2.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn2.HeaderText = "Descrição";
            gridTextColumn2.MappingName = "Descricao";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.AllowResizing = true;
            gridTextColumn3.AllowSorting = false;
            gridTextColumn3.CellStyle.Font.Size = 12F;
            gridTextColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            gridTextColumn3.Format = "N2";
            gridTextColumn3.HeaderText = "Valor";
            gridTextColumn3.MappingName = "ValorVenda";
            this.gridProdutos.Columns.Add(gridTextColumn1);
            this.gridProdutos.Columns.Add(gridTextColumn2);
            this.gridProdutos.Columns.Add(gridTextColumn3);
            this.gridProdutos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProdutos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridProdutos.Location = new System.Drawing.Point(0, 0);
            this.gridProdutos.Name = "gridProdutos";
            this.gridProdutos.Size = new System.Drawing.Size(816, 238);
            this.gridProdutos.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.gridProdutos.Style.CellStyle.Font.Facename = "Montserrat";
            this.gridProdutos.Style.CellStyle.Font.Size = 14F;
            this.gridProdutos.Style.HeaderStyle.HoverBackColor = System.Drawing.Color.White;
            this.gridProdutos.TabIndex = 198;
            this.gridProdutos.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.gridProdutos_QueryRowStyle);
            this.gridProdutos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridProdutos_KeyDown);
            // 
            // btnConfirmaItem
            // 
            this.btnConfirmaItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmaItem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnConfirmaItem.FlatAppearance.BorderSize = 0;
            this.btnConfirmaItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnConfirmaItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnConfirmaItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmaItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmaItem.IconChar = FontAwesome.Sharp.IconChar.CircleCheck;
            this.btnConfirmaItem.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnConfirmaItem.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnConfirmaItem.IconSize = 38;
            this.btnConfirmaItem.Location = new System.Drawing.Point(636, 64);
            this.btnConfirmaItem.Name = "btnConfirmaItem";
            this.btnConfirmaItem.Size = new System.Drawing.Size(46, 38);
            this.btnConfirmaItem.TabIndex = 210;
            this.btnConfirmaItem.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnConfirmaItem);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtPesquisaProduto);
            this.panel2.Controls.Add(this.autoLabel23);
            this.panel2.Controls.Add(this.txtCodProduto);
            this.panel2.Controls.Add(this.autoLabel3);
            this.panel2.Controls.Add(this.btnPesquisaProduto);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 136);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(818, 128);
            this.panel2.TabIndex = 210;
            // 
            // txtPesquisaProduto
            // 
            this.txtPesquisaProduto.BackColor = System.Drawing.Color.White;
            this.txtPesquisaProduto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPesquisaProduto.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPesquisaProduto.BorderRadius = 8;
            this.txtPesquisaProduto.BorderSize = 2;
            this.txtPesquisaProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtPesquisaProduto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPesquisaProduto.Location = new System.Drawing.Point(226, 64);
            this.txtPesquisaProduto.Margin = new System.Windows.Forms.Padding(4);
            this.txtPesquisaProduto.Multiline = false;
            this.txtPesquisaProduto.Name = "txtPesquisaProduto";
            this.txtPesquisaProduto.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtPesquisaProduto.PasswordChar = false;
            this.txtPesquisaProduto.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtPesquisaProduto.PlaceholderText = "";
            this.txtPesquisaProduto.ReadOnly = false;
            this.txtPesquisaProduto.Size = new System.Drawing.Size(403, 37);
            this.txtPesquisaProduto.TabIndex = 206;
            this.txtPesquisaProduto.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPesquisaProduto.Texts = "";
            this.txtPesquisaProduto.UnderlinedStyle = false;
            this.txtPesquisaProduto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPesquisaProduto_KeyPress);
            // 
            // txtCodProduto
            // 
            this.txtCodProduto.BackColor = System.Drawing.Color.White;
            this.txtCodProduto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodProduto.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodProduto.BorderRadius = 8;
            this.txtCodProduto.BorderSize = 2;
            this.txtCodProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtCodProduto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodProduto.Location = new System.Drawing.Point(14, 63);
            this.txtCodProduto.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodProduto.Multiline = false;
            this.txtCodProduto.Name = "txtCodProduto";
            this.txtCodProduto.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodProduto.PasswordChar = false;
            this.txtCodProduto.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodProduto.PlaceholderText = "";
            this.txtCodProduto.ReadOnly = false;
            this.txtCodProduto.Size = new System.Drawing.Size(162, 37);
            this.txtCodProduto.TabIndex = 205;
            this.txtCodProduto.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodProduto.Texts = "";
            this.txtCodProduto.UnderlinedStyle = false;
            this.txtCodProduto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodProduto_KeyPress);
            // 
            // FrmCargaBalanca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(818, 558);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmCargaBalanca";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carga Balança";
            this.Load += new System.EventHandler(this.FrmCargaBalanca_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPortaBalanca)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIpBalanca)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProdutos)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private MaterialSkin.Controls.MaterialButton btnTestarComunicacao;
        private Syncfusion.Windows.Forms.Tools.MaskedEditBox txtIpBalanca;
        private Syncfusion.Windows.Forms.Tools.MaskedEditBox txtPortaBalanca;
        private System.Windows.Forms.Label label2;
        private MaterialSkin.Controls.MaterialButton btnScan;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel23;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private FontAwesome.Sharp.IconButton btnPesquisaProduto;
        private RJ_UI.Classes.RJTextBox txtCodProduto;
        private RJ_UI.Classes.RJTextBox txtPesquisaProduto;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialButton btnEnviarCarga;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridProdutos;
        private MaterialSkin.Controls.MaterialButton btnGerarTxt;
        private MaterialSkin.Controls.MaterialButton btnGerarArquivo;
        private System.Windows.Forms.ProgressBar progressBarBalanca;
        private FontAwesome.Sharp.IconButton btnParar;
        private FontAwesome.Sharp.IconButton btnConfirmaItem;
        private System.Windows.Forms.Panel panel2;
    }
}