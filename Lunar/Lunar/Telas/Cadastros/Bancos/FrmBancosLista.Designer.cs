namespace Lunar.Telas.Cadastros.Bancos
{
    partial class FrmBancosLista
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
            this.components = new System.ComponentModel.Container();
            this.txtPesquisaBanco = new Lunar.RJ_UI.Classes.RJTextBox();
            this.gridBagLayout1 = new Syncfusion.Windows.Forms.Tools.GridBagLayout(this.components);
            this.btnNovo = new Lunar.RJ_UI.Classes.RJButton();
            this.btnEditar = new Lunar.RJ_UI.Classes.RJButton();
            this.btnPesquisaUnidadeMedida = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridBagLayout1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPesquisaBanco
            // 
            this.txtPesquisaBanco.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPesquisaBanco.BackColor = System.Drawing.Color.White;
            this.txtPesquisaBanco.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPesquisaBanco.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.txtPesquisaBanco.BorderRadius = 8;
            this.txtPesquisaBanco.BorderSize = 2;
            this.txtPesquisaBanco.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPesquisaBanco.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPesquisaBanco.Location = new System.Drawing.Point(13, 13);
            this.txtPesquisaBanco.Margin = new System.Windows.Forms.Padding(4);
            this.txtPesquisaBanco.Multiline = false;
            this.txtPesquisaBanco.Name = "txtPesquisaBanco";
            this.txtPesquisaBanco.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtPesquisaBanco.PasswordChar = false;
            this.txtPesquisaBanco.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtPesquisaBanco.PlaceholderText = "Pesquisar";
            this.txtPesquisaBanco.ReadOnly = false;
            this.txtPesquisaBanco.Size = new System.Drawing.Size(456, 39);
            this.txtPesquisaBanco.TabIndex = 210;
            this.txtPesquisaBanco.TabStop = false;
            this.txtPesquisaBanco.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPesquisaBanco.Texts = "";
            this.txtPesquisaBanco.UnderlinedStyle = false;
            this.txtPesquisaBanco._TextChanged += new System.EventHandler(this.txtPesquisaCliente__TextChanged);
            // 
            // btnNovo
            // 
            this.btnNovo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnNovo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnNovo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnNovo.BorderRadius = 8;
            this.btnNovo.BorderSize = 0;
            this.btnNovo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovo.FlatAppearance.BorderSize = 0;
            this.btnNovo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovo.ForeColor = System.Drawing.Color.White;
            this.btnNovo.Location = new System.Drawing.Point(696, 469);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(293, 45);
            this.btnNovo.TabIndex = 213;
            this.btnNovo.Text = "Novo";
            this.btnNovo.TextColor = System.Drawing.Color.White;
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.BackColor = System.Drawing.Color.White;
            this.btnEditar.BackgroundColor = System.Drawing.Color.White;
            this.btnEditar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnEditar.BorderRadius = 8;
            this.btnEditar.BorderSize = 2;
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnEditar.FlatAppearance.BorderSize = 2;
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnEditar.Location = new System.Drawing.Point(397, 469);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(293, 45);
            this.btnEditar.TabIndex = 214;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(69)))));
            this.btnEditar.UseVisualStyleBackColor = false;
            // 
            // btnPesquisaUnidadeMedida
            // 
            this.btnPesquisaUnidadeMedida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaUnidadeMedida.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnPesquisaUnidadeMedida.FlatAppearance.BorderSize = 0;
            this.btnPesquisaUnidadeMedida.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaUnidadeMedida.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaUnidadeMedida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaUnidadeMedida.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisaUnidadeMedida.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisaUnidadeMedida.IconColor = System.Drawing.Color.MidnightBlue;
            this.btnPesquisaUnidadeMedida.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnPesquisaUnidadeMedida.IconSize = 38;
            this.btnPesquisaUnidadeMedida.Location = new System.Drawing.Point(483, 15);
            this.btnPesquisaUnidadeMedida.Margin = new System.Windows.Forms.Padding(0);
            this.btnPesquisaUnidadeMedida.Name = "btnPesquisaUnidadeMedida";
            this.btnPesquisaUnidadeMedida.Size = new System.Drawing.Size(36, 37);
            this.btnPesquisaUnidadeMedida.TabIndex = 211;
            this.btnPesquisaUnidadeMedida.UseVisualStyleBackColor = true;
            this.btnPesquisaUnidadeMedida.Click += new System.EventHandler(this.btnPesquisaUnidadeMedida_Click);
            // 
            // FrmBancosLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1001, 535);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnPesquisaUnidadeMedida);
            this.Controls.Add(this.txtPesquisaBanco);
            this.Name = "FrmBancosLista";
            this.Text = "Bancos";
            this.Load += new System.EventHandler(this.FrmBancosLista_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridBagLayout1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private RJ_UI.Classes.RJTextBox txtPesquisaBanco;
        private FontAwesome.Sharp.IconButton btnPesquisaUnidadeMedida;
        private Syncfusion.Windows.Forms.Tools.GridBagLayout gridBagLayout1;
        private RJ_UI.Classes.RJButton btnNovo;
        private RJ_UI.Classes.RJButton btnEditar;
    }
}