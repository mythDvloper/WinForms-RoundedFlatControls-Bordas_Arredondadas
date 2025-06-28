using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_RoundedFlatControls.Controls
{
    [ToolboxItem(true)]
    [Description("Panel com bordas arredondadas")]
    public class RoundedPanel : Panel
    {
        private int borderRadius = 20;
        private Color borderColor = Color.Black;
        private int borderSize = 2;
        private Size originalSize;
        private Point originalLocation;
        private bool isScaled = false;

        [Category("Aparência")]
        [Description("Raio dos cantos arredondados")]
        public int BorderRadius
        {
            get => borderRadius;
            set
            {
                borderRadius = value;
                Invalidate();
            }
        }

        [Category("Aparência")]
        [Description("Cor da borda")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Aparência")]
        [Description("Tamanho da borda")]
        public int BorderSize
        {
            get => borderSize;
            set
            {
                borderSize = value;
                Invalidate();
            }
        }

        public RoundedPanel()
        {
            BackColor = Color.FromArgb(42, 59, 77); // Cor normal
            DoubleBuffered = true;
            Resize += (s, e) => Invalidate();

            MouseEnter += RoundedPanel_MouseEnter;
            MouseLeave += RoundedPanel_MouseLeave;
            this.MouseDown += RoundedPanel_MouseDown;
            this.MouseUp += RoundedPanel_MouseUp;
        }

        private void RoundedPanel_MouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(54, 85, 121); // Cor no hover
        }

        private void RoundedPanel_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(42, 59, 77); // Cor normal
        }

        private void RoundedPanel_MouseDown(object sender, MouseEventArgs e)
        {
            //this.BackColor = Color.FromArgb(30, 100, 160); // Clique = cor mais escura
            ApplyScale(0.97f); // Leve redução para efeito de pressão
        }

        private void RoundedPanel_MouseUp(object sender, MouseEventArgs e)
        {
            //this.BackColor = Color.FromArgb(60, 130, 200); // Volta para hover
            ResetScale(); // Volta ao tamanho normal
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rectSurface = ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -BorderSize, -BorderSize);

            int smoothSize = BorderSize > 0 ? BorderSize : 1;

            using (GraphicsPath pathSurface = GetRoundRectangle(rectSurface, BorderRadius))
            using (GraphicsPath pathBorder = GetRoundRectangle(rectBorder, BorderRadius - BorderSize))
            using (Pen penSurface = new Pen(Parent?.BackColor ?? Color.White, smoothSize))
            using (Pen penBorder = new Pen(BorderColor, BorderSize))
            {
                // Área interna
                Region = new Region(pathSurface);

                // Desenha a superfície (para suavizar as bordas com o fundo)
                e.Graphics.DrawPath(penSurface, pathSurface);

                // Desenha a borda se BorderSize > 0
                if (BorderSize > 0)
                    e.Graphics.DrawPath(penBorder, pathBorder);
            }
        }

        private GraphicsPath GetRoundRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void ApplyScale(float scale)
        {
            if (isScaled) return;

            originalSize = this.Size;
            originalLocation = this.Location;

            int newWidth = (int)(this.Width * scale);
            int newHeight = (int)(this.Height * scale);

            int offsetX = (this.Width - newWidth) / 2;
            int offsetY = (this.Height - newHeight) / 2;

            this.Size = new Size(newWidth, newHeight);
            this.Location = new Point(this.Location.X + offsetX, this.Location.Y + offsetY);

            isScaled = true;
        }

        private void ResetScale()
        {
            if (!isScaled) return;

            this.Size = originalSize;
            this.Location = originalLocation;
            isScaled = false;
        }
    }
}
