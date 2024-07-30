namespace Lunar.Telas.Food
{
    partial class FrmDetalheMesa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetalheMesa));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnMenosPessoas = new FontAwesome.Sharp.IconButton();
            this.txtQuantidadePessoas = new Syncfusion.Windows.Forms.Tools.IntegerTextBox();
            this.btnMaisPessoas = new FontAwesome.Sharp.IconButton();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantidadePessoas)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(840, 118);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(17, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 63);
            this.button1.TabIndex = 2;
            this.button1.Text = "5";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mesas Ocupadas";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnMenosPessoas);
            this.panel2.Controls.Add(this.txtQuantidadePessoas);
            this.panel2.Controls.Add(this.btnMaisPessoas);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 118);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(840, 120);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Contas da Mesa";
            // 
            // btnMenosPessoas
            // 
            this.btnMenosPessoas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenosPessoas.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.btnMenosPessoas.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnMenosPessoas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMenosPessoas.IconSize = 14;
            this.btnMenosPessoas.Location = new System.Drawing.Point(16, 41);
            this.btnMenosPessoas.Name = "btnMenosPessoas";
            this.btnMenosPessoas.Size = new System.Drawing.Size(36, 35);
            this.btnMenosPessoas.TabIndex = 6;
            this.btnMenosPessoas.UseVisualStyleBackColor = true;
            this.btnMenosPessoas.Click += new System.EventHandler(this.btnMenosPessoas_Click);
            // 
            // txtQuantidadePessoas
            // 
            this.txtQuantidadePessoas.BeforeTouchSize = new System.Drawing.Size(193, 33);
            this.txtQuantidadePessoas.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidadePessoas.IntegerValue = ((long)(1));
            this.txtQuantidadePessoas.Location = new System.Drawing.Point(52, 42);
            this.txtQuantidadePessoas.Name = "txtQuantidadePessoas";
            this.txtQuantidadePessoas.Size = new System.Drawing.Size(193, 33);
            this.txtQuantidadePessoas.TabIndex = 5;
            this.txtQuantidadePessoas.Text = "1";
            this.txtQuantidadePessoas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQuantidadePessoas.ThemeStyle.BackColor = System.Drawing.Color.White;
            // 
            // btnMaisPessoas
            // 
            this.btnMaisPessoas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaisPessoas.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnMaisPessoas.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(19)))), ((int)(((byte)(255)))));
            this.btnMaisPessoas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMaisPessoas.IconSize = 14;
            this.btnMaisPessoas.Location = new System.Drawing.Point(245, 41);
            this.btnMaisPessoas.Name = "btnMaisPessoas";
            this.btnMaisPessoas.Size = new System.Drawing.Size(36, 35);
            this.btnMaisPessoas.TabIndex = 4;
            this.btnMaisPessoas.UseVisualStyleBackColor = true;
            this.btnMaisPessoas.Click += new System.EventHandler(this.btnMaisPessoas_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Quantidade de Pessoas";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 238);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(840, 192);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button3.Location = new System.Drawing.Point(12, 507);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(173, 48);
            this.button3.TabIndex = 9;
            this.button3.Text = "Ocupar Mesa [F8]";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // FrmDetalheMesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(840, 567);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDetalheMesa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalhes da Mesa";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantidadePessoas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton btnMenosPessoas;
        private Syncfusion.Windows.Forms.Tools.IntegerTextBox txtQuantidadePessoas;
        private FontAwesome.Sharp.IconButton btnMaisPessoas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
    }
}