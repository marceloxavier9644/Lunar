namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    partial class FrmCaracteristicaPessoa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCaracteristicaPessoa));
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel112 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.gridVariacao = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.btnAdicionarVariacao = new MaterialSkin.Controls.MaterialButton();
            this.autoLabel111 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtOrdem = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtCodVariacao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.txtDescricaoVariacao = new Lunar.RJ_UI.Classes.RJTextBox();
            this.btnExcluir = new MaterialSkin.Controls.MaterialButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridVariacao)).BeginInit();
            this.SuspendLayout();
            // 
            // autoLabel1
            // 
            this.autoLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel1.Location = new System.Drawing.Point(519, 14);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(122, 16);
            this.autoLabel1.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016White;
            this.autoLabel1.TabIndex = 259;
            this.autoLabel1.Text = "Ordenação (1,2,3...)";
            this.autoLabel1.ThemeName = "Office2016White";
            this.autoLabel1.ThemeStyle.BackColor = System.Drawing.Color.Turquoise;
            // 
            // autoLabel112
            // 
            this.autoLabel112.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel112.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel112.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel112.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel112.Location = new System.Drawing.Point(13, 14);
            this.autoLabel112.Name = "autoLabel112";
            this.autoLabel112.Size = new System.Drawing.Size(20, 16);
            this.autoLabel112.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016White;
            this.autoLabel112.TabIndex = 257;
            this.autoLabel112.Text = "ID";
            this.autoLabel112.ThemeName = "Office2016White";
            this.autoLabel112.ThemeStyle.BackColor = System.Drawing.Color.Turquoise;
            // 
            // gridVariacao
            // 
            this.gridVariacao.AccessibleName = "Table";
            this.gridVariacao.AllowDeleting = true;
            this.gridVariacao.AllowEditing = false;
            this.gridVariacao.AutoGenerateColumns = false;
            this.gridVariacao.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
            gridTextColumn1.AllowEditing = false;
            gridTextColumn1.HeaderStyle.FilterIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            gridTextColumn1.HeaderText = "ID";
            gridTextColumn1.MappingName = "Id";
            gridTextColumn2.AllowEditing = false;
            gridTextColumn2.HeaderText = "Descrição";
            gridTextColumn2.MappingName = "Descricao";
            gridTextColumn3.AllowEditing = false;
            gridTextColumn3.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            gridTextColumn3.HeaderText = "Ordenação";
            gridTextColumn3.MappingName = "Ordenacao";
            this.gridVariacao.Columns.Add(gridTextColumn1);
            this.gridVariacao.Columns.Add(gridTextColumn2);
            this.gridVariacao.Columns.Add(gridTextColumn3);
            this.gridVariacao.Location = new System.Drawing.Point(21, 79);
            this.gridVariacao.Name = "gridVariacao";
            this.gridVariacao.Size = new System.Drawing.Size(854, 359);
            this.gridVariacao.TabIndex = 4;
            this.gridVariacao.Text = "sfDataGrid1";
            this.gridVariacao.QueryRowStyle += new Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventHandler(this.gridVariacao_QueryRowStyle);
            this.gridVariacao.CellDoubleClick += new Syncfusion.WinForms.DataGrid.Events.CellClickEventHandler(this.gridVariacao_CellDoubleClick);
            // 
            // btnAdicionarVariacao
            // 
            this.btnAdicionarVariacao.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAdicionarVariacao.Depth = 0;
            this.btnAdicionarVariacao.DrawShadows = true;
            this.btnAdicionarVariacao.HighEmphasis = true;
            this.btnAdicionarVariacao.Icon = null;
            this.btnAdicionarVariacao.Location = new System.Drawing.Point(689, 35);
            this.btnAdicionarVariacao.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnAdicionarVariacao.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAdicionarVariacao.Name = "btnAdicionarVariacao";
            this.btnAdicionarVariacao.Size = new System.Drawing.Size(98, 36);
            this.btnAdicionarVariacao.TabIndex = 3;
            this.btnAdicionarVariacao.Text = "Adicionar";
            this.btnAdicionarVariacao.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnAdicionarVariacao.UseAccentColor = false;
            this.btnAdicionarVariacao.UseVisualStyleBackColor = true;
            this.btnAdicionarVariacao.Click += new System.EventHandler(this.btnAdicionarVariacao_Click);
            // 
            // autoLabel111
            // 
            this.autoLabel111.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel111.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.autoLabel111.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel111.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel111.Location = new System.Drawing.Point(157, 13);
            this.autoLabel111.Name = "autoLabel111";
            this.autoLabel111.Size = new System.Drawing.Size(181, 16);
            this.autoLabel111.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016White;
            this.autoLabel111.TabIndex = 253;
            this.autoLabel111.Text = "Descrição da Característica *";
            this.autoLabel111.ThemeName = "Office2016White";
            this.autoLabel111.ThemeStyle.BackColor = System.Drawing.Color.Turquoise;
            // 
            // txtOrdem
            // 
            this.txtOrdem.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.txtOrdem.BackColor = System.Drawing.Color.White;
            this.txtOrdem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtOrdem.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtOrdem.BorderRadius = 8;
            this.txtOrdem.BorderSize = 2;
            this.txtOrdem.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtOrdem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrdem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtOrdem.Location = new System.Drawing.Point(519, 33);
            this.txtOrdem.Margin = new System.Windows.Forms.Padding(4);
            this.txtOrdem.Multiline = false;
            this.txtOrdem.Name = "txtOrdem";
            this.txtOrdem.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtOrdem.PasswordChar = false;
            this.txtOrdem.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtOrdem.PlaceholderText = "";
            this.txtOrdem.ReadOnly = false;
            this.txtOrdem.Size = new System.Drawing.Size(162, 37);
            this.txtOrdem.TabIndex = 2;
            this.txtOrdem.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtOrdem.Texts = "";
            this.txtOrdem.UnderlinedStyle = false;
            this.txtOrdem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrdem_KeyPress);
            // 
            // txtCodVariacao
            // 
            this.txtCodVariacao.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.txtCodVariacao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodVariacao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodVariacao.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtCodVariacao.BorderRadius = 8;
            this.txtCodVariacao.BorderSize = 2;
            this.txtCodVariacao.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCodVariacao.Enabled = false;
            this.txtCodVariacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodVariacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCodVariacao.Location = new System.Drawing.Point(13, 34);
            this.txtCodVariacao.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodVariacao.Multiline = false;
            this.txtCodVariacao.Name = "txtCodVariacao";
            this.txtCodVariacao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCodVariacao.PasswordChar = false;
            this.txtCodVariacao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtCodVariacao.PlaceholderText = "";
            this.txtCodVariacao.ReadOnly = false;
            this.txtCodVariacao.Size = new System.Drawing.Size(136, 37);
            this.txtCodVariacao.TabIndex = 0;
            this.txtCodVariacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCodVariacao.Texts = "";
            this.txtCodVariacao.UnderlinedStyle = false;
            // 
            // txtDescricaoVariacao
            // 
            this.txtDescricaoVariacao.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.txtDescricaoVariacao.BackColor = System.Drawing.Color.White;
            this.txtDescricaoVariacao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricaoVariacao.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtDescricaoVariacao.BorderRadius = 8;
            this.txtDescricaoVariacao.BorderSize = 2;
            this.txtDescricaoVariacao.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDescricaoVariacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricaoVariacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDescricaoVariacao.Location = new System.Drawing.Point(157, 33);
            this.txtDescricaoVariacao.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricaoVariacao.Multiline = false;
            this.txtDescricaoVariacao.Name = "txtDescricaoVariacao";
            this.txtDescricaoVariacao.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtDescricaoVariacao.PasswordChar = false;
            this.txtDescricaoVariacao.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtDescricaoVariacao.PlaceholderText = "";
            this.txtDescricaoVariacao.ReadOnly = false;
            this.txtDescricaoVariacao.Size = new System.Drawing.Size(354, 37);
            this.txtDescricaoVariacao.TabIndex = 1;
            this.txtDescricaoVariacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDescricaoVariacao.Texts = "";
            this.txtDescricaoVariacao.UnderlinedStyle = false;
            // 
            // btnExcluir
            // 
            this.btnExcluir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExcluir.Depth = 0;
            this.btnExcluir.DrawShadows = true;
            this.btnExcluir.HighEmphasis = true;
            this.btnExcluir.Icon = null;
            this.btnExcluir.Location = new System.Drawing.Point(795, 35);
            this.btnExcluir.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnExcluir.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(80, 36);
            this.btnExcluir.TabIndex = 260;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnExcluir.UseAccentColor = false;
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // FrmCaracteristicaPessoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(888, 450);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.txtOrdem);
            this.Controls.Add(this.autoLabel112);
            this.Controls.Add(this.txtCodVariacao);
            this.Controls.Add(this.gridVariacao);
            this.Controls.Add(this.btnAdicionarVariacao);
            this.Controls.Add(this.autoLabel111);
            this.Controls.Add(this.txtDescricaoVariacao);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCaracteristicaPessoa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Características";
            ((System.ComponentModel.ISupportInitialize)(this.gridVariacao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private RJ_UI.Classes.RJTextBox txtOrdem;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel112;
        private RJ_UI.Classes.RJTextBox txtCodVariacao;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridVariacao;
        private MaterialSkin.Controls.MaterialButton btnAdicionarVariacao;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel111;
        private RJ_UI.Classes.RJTextBox txtDescricaoVariacao;
        private MaterialSkin.Controls.MaterialButton btnExcluir;
    }
}