namespace Lunar.Telas.Food
{
    partial class ProdutoItemControl
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNomeProduto = new System.Windows.Forms.Label();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.lblPrecoUnitario = new System.Windows.Forms.Label();
            this.lblDesconto = new System.Windows.Forms.Label();
            this.lblPrecoFinal = new System.Windows.Forms.Label();
            this.lblIdProduto = new System.Windows.Forms.Label();
            this.lblObservacoes = new System.Windows.Forms.Label();
            this.btnRemover = new FontAwesome.Sharp.IconButton();
            this.lblItem = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNomeProduto
            // 
            this.lblNomeProduto.AutoSize = true;
            this.lblNomeProduto.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F);
            this.lblNomeProduto.Location = new System.Drawing.Point(72, 7);
            this.lblNomeProduto.Name = "lblNomeProduto";
            this.lblNomeProduto.Size = new System.Drawing.Size(58, 17);
            this.lblNomeProduto.TabIndex = 0;
            this.lblNomeProduto.Text = "Produto";
            this.lblNomeProduto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantidade.Location = new System.Drawing.Point(32, 7);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(32, 17);
            this.lblQuantidade.TabIndex = 1;
            this.lblQuantidade.Text = "Qtd";
            this.lblQuantidade.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPrecoUnitario
            // 
            this.lblPrecoUnitario.AutoSize = true;
            this.lblPrecoUnitario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecoUnitario.Location = new System.Drawing.Point(200, 27);
            this.lblPrecoUnitario.Name = "lblPrecoUnitario";
            this.lblPrecoUnitario.Size = new System.Drawing.Size(89, 16);
            this.lblPrecoUnitario.TabIndex = 2;
            this.lblPrecoUnitario.Text = "PrecoUnitario";
            this.lblPrecoUnitario.Visible = false;
            // 
            // lblDesconto
            // 
            this.lblDesconto.AutoSize = true;
            this.lblDesconto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesconto.Location = new System.Drawing.Point(295, 27);
            this.lblDesconto.Name = "lblDesconto";
            this.lblDesconto.Size = new System.Drawing.Size(65, 16);
            this.lblDesconto.TabIndex = 3;
            this.lblDesconto.Text = "Desconto";
            this.lblDesconto.Visible = false;
            // 
            // lblPrecoFinal
            // 
            this.lblPrecoFinal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrecoFinal.AutoSize = true;
            this.lblPrecoFinal.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecoFinal.Location = new System.Drawing.Point(366, 7);
            this.lblPrecoFinal.Name = "lblPrecoFinal";
            this.lblPrecoFinal.Size = new System.Drawing.Size(70, 17);
            this.lblPrecoFinal.TabIndex = 4;
            this.lblPrecoFinal.Text = "PrecoFinal";
            this.lblPrecoFinal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIdProduto
            // 
            this.lblIdProduto.AutoSize = true;
            this.lblIdProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdProduto.Location = new System.Drawing.Point(366, 27);
            this.lblIdProduto.Name = "lblIdProduto";
            this.lblIdProduto.Size = new System.Drawing.Size(70, 16);
            this.lblIdProduto.TabIndex = 5;
            this.lblIdProduto.Text = "ID Produto";
            this.lblIdProduto.Visible = false;
            // 
            // lblObservacoes
            // 
            this.lblObservacoes.AutoSize = true;
            this.lblObservacoes.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObservacoes.Location = new System.Drawing.Point(72, 27);
            this.lblObservacoes.Name = "lblObservacoes";
            this.lblObservacoes.Size = new System.Drawing.Size(73, 15);
            this.lblObservacoes.TabIndex = 6;
            this.lblObservacoes.Text = "Observacoes";
            // 
            // btnRemover
            // 
            this.btnRemover.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnRemover.FlatAppearance.BorderSize = 0;
            this.btnRemover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemover.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnRemover.IconColor = System.Drawing.Color.Red;
            this.btnRemover.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRemover.IconSize = 20;
            this.btnRemover.Location = new System.Drawing.Point(0, 7);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(27, 17);
            this.btnRemover.TabIndex = 7;
            this.btnRemover.UseVisualStyleBackColor = true;
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItem.Location = new System.Drawing.Point(5, 27);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(39, 15);
            this.lblItem.TabIndex = 8;
            this.lblItem.Text = "Item 1";
            // 
            // ProdutoItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblItem);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.lblObservacoes);
            this.Controls.Add(this.lblIdProduto);
            this.Controls.Add(this.lblPrecoFinal);
            this.Controls.Add(this.lblDesconto);
            this.Controls.Add(this.lblPrecoUnitario);
            this.Controls.Add(this.lblQuantidade);
            this.Controls.Add(this.lblNomeProduto);
            this.Name = "ProdutoItemControl";
            this.Size = new System.Drawing.Size(439, 52);
            this.Load += new System.EventHandler(this.ProdutoItemControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ProdutoItemControl_Paint);
            this.ParentChanged += new System.EventHandler(this.ProdutoItemControl_ParentChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNomeProduto;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.Label lblPrecoUnitario;
        private System.Windows.Forms.Label lblDesconto;
        private System.Windows.Forms.Label lblPrecoFinal;
        private System.Windows.Forms.Label lblIdProduto;
        private System.Windows.Forms.Label lblObservacoes;
        private FontAwesome.Sharp.IconButton btnRemover;
        private System.Windows.Forms.Label lblItem;
    }
}
