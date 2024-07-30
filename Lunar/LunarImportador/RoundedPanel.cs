using System.Drawing.Drawing2D;

namespace LunarImportador
{
    public class RoundedPanel : Panel
    {
        public int CornerRadius { get; set; } = 20; // Raio dos cantos
        public Color BorderColor { get; set; } = Color.FromArgb(217,217,217); // Cor da borda
        public int BorderThickness { get; set; } = 2; // Espessura da borda
        public Color ShadowColor { get; set; } = Color.FromArgb(100, 0, 0, 0); // Cor da sombra
        public int ShadowOffset { get; set; } = 5; // Deslocamento da sombra

        public RoundedPanel()
        {
            this.BorderStyle = BorderStyle.None; // Remove o estilo de borda padrão
            this.BackColor = Color.White; // Cor de fundo padrão
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Desenhar sombra
            using (GraphicsPath shadowPath = CreateRoundedRectanglePath(new Rectangle(ShadowOffset, ShadowOffset, Width - BorderThickness - ShadowOffset, Height - BorderThickness - ShadowOffset), CornerRadius))
            {
                using (SolidBrush shadowBrush = new SolidBrush(ShadowColor))
                {
                    e.Graphics.FillPath(shadowBrush, shadowPath);
                }
            }

            // Desenhar fundo do painel
            using (GraphicsPath path = CreateRoundedRectanglePath(new Rectangle(0, 0, Width - BorderThickness, Height - BorderThickness), CornerRadius))
            {
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // Desenhar borda
                using (Pen pen = new Pen(BorderColor, BorderThickness))
                {
                    e.Graphics.DrawPath(pen, path);
                }

                this.Region = new Region(path);
            }
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(rect.X + rect.Width - cornerRadius, rect.Y, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(rect.X + rect.Width - cornerRadius, rect.Y + rect.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
