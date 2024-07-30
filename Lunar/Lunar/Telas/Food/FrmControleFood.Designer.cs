namespace Lunar.Telas.Food
{
    partial class FrmControleFood
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmControleFood));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPesquisar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelCentro = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNovoAtendimento = new FontAwesome.Sharp.IconButton();
            this.btnComandas = new FontAwesome.Sharp.IconButton();
            this.btnMesas = new FontAwesome.Sharp.IconButton();
            this.btnTodas = new FontAwesome.Sharp.IconButton();
            this.btnConfig = new FontAwesome.Sharp.IconButton();
            this.btnPesquisar = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelCentro.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.btnComandas);
            this.panel1.Controls.Add(this.btnMesas);
            this.panel1.Controls.Add(this.btnTodas);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.btnConfig);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnPesquisar);
            this.panel1.Controls.Add(this.txtPesquisar);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 190);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(794, 31);
            this.panel3.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pesquisar";
            // 
            // txtPesquisar
            // 
            this.txtPesquisar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPesquisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPesquisar.ForeColor = System.Drawing.Color.Gray;
            this.txtPesquisar.Location = new System.Drawing.Point(17, 110);
            this.txtPesquisar.Name = "txtPesquisar";
            this.txtPesquisar.Size = new System.Drawing.Size(317, 31);
            this.txtPesquisar.TabIndex = 1;
            this.txtPesquisar.Enter += new System.EventHandler(this.textBoxPesquisar_Enter);
            this.txtPesquisar.Leave += new System.EventHandler(this.textBoxPesquisar_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mesas";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnNovoAtendimento);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 386);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 64);
            this.panel2.TabIndex = 1;
            // 
            // panelCentro
            // 
            this.panelCentro.Controls.Add(this.flowLayoutPanel1);
            this.panelCentro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCentro.Location = new System.Drawing.Point(0, 190);
            this.panelCentro.Name = "panelCentro";
            this.panelCentro.Size = new System.Drawing.Size(800, 196);
            this.panelCentro.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 196);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnNovoAtendimento
            // 
            this.btnNovoAtendimento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovoAtendimento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnNovoAtendimento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovoAtendimento.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovoAtendimento.ForeColor = System.Drawing.Color.White;
            this.btnNovoAtendimento.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnNovoAtendimento.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnNovoAtendimento.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNovoAtendimento.IconSize = 25;
            this.btnNovoAtendimento.Location = new System.Drawing.Point(599, 8);
            this.btnNovoAtendimento.Name = "btnNovoAtendimento";
            this.btnNovoAtendimento.Size = new System.Drawing.Size(196, 43);
            this.btnNovoAtendimento.TabIndex = 6;
            this.btnNovoAtendimento.Text = "Novo Atendimento [F2]";
            this.btnNovoAtendimento.UseVisualStyleBackColor = false;
            this.btnNovoAtendimento.Click += new System.EventHandler(this.btnNovoAtendimento_Click);
            // 
            // btnComandas
            // 
            this.btnComandas.BackColor = System.Drawing.Color.White;
            this.btnComandas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnComandas.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComandas.ForeColor = System.Drawing.Color.Black;
            this.btnComandas.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnComandas.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnComandas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnComandas.IconSize = 25;
            this.btnComandas.Location = new System.Drawing.Point(263, 151);
            this.btnComandas.Name = "btnComandas";
            this.btnComandas.Size = new System.Drawing.Size(117, 33);
            this.btnComandas.TabIndex = 8;
            this.btnComandas.Text = "Comandas";
            this.btnComandas.UseVisualStyleBackColor = false;
            this.btnComandas.Click += new System.EventHandler(this.btnComandas_Click);
            // 
            // btnMesas
            // 
            this.btnMesas.BackColor = System.Drawing.Color.White;
            this.btnMesas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMesas.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMesas.ForeColor = System.Drawing.Color.Black;
            this.btnMesas.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnMesas.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnMesas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMesas.IconSize = 25;
            this.btnMesas.Location = new System.Drawing.Point(140, 151);
            this.btnMesas.Name = "btnMesas";
            this.btnMesas.Size = new System.Drawing.Size(117, 33);
            this.btnMesas.TabIndex = 7;
            this.btnMesas.Text = "Mesas";
            this.btnMesas.UseVisualStyleBackColor = false;
            this.btnMesas.Click += new System.EventHandler(this.btnMesas_Click);
            // 
            // btnTodas
            // 
            this.btnTodas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnTodas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTodas.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTodas.ForeColor = System.Drawing.Color.White;
            this.btnTodas.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnTodas.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnTodas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnTodas.IconSize = 25;
            this.btnTodas.Location = new System.Drawing.Point(17, 151);
            this.btnTodas.Name = "btnTodas";
            this.btnTodas.Size = new System.Drawing.Size(117, 33);
            this.btnTodas.TabIndex = 6;
            this.btnTodas.Text = "Todas";
            this.btnTodas.UseVisualStyleBackColor = false;
            this.btnTodas.Click += new System.EventHandler(this.btnTodas_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfig.BackgroundImage = global::Lunar.Properties.Resources.settings_24dp_Black;
            this.btnConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfig.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnConfig.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnConfig.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnConfig.IconSize = 25;
            this.btnConfig.Location = new System.Drawing.Point(755, 33);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(33, 33);
            this.btnConfig.TabIndex = 4;
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisar.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPesquisar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnPesquisar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPesquisar.IconSize = 25;
            this.btnPesquisar.Location = new System.Drawing.Point(340, 110);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(40, 33);
            this.btnPesquisar.TabIndex = 2;
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // FrmControleFood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelCentro);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmControleFood";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle de Mesas e Comandas";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmControleFood_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panelCentro.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPesquisar;
        private FontAwesome.Sharp.IconButton btnPesquisar;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton btnConfig;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelCentro;
        private System.Windows.Forms.Panel panel3;
        private FontAwesome.Sharp.IconButton btnNovoAtendimento;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private FontAwesome.Sharp.IconButton btnTodas;
        private FontAwesome.Sharp.IconButton btnComandas;
        private FontAwesome.Sharp.IconButton btnMesas;
    }
}