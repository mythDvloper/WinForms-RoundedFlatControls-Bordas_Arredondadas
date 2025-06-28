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
    public class RoundedTextBox : UserControl
    {
        private TextBox textBox = new TextBox();

        private int borderRadius = 15;
        private int borderSize = 2;
        private Color borderColor = Color.Black;
        private Color backgroundColor = Color.FromArgb(42, 59, 77);
        private Color textColor = Color.White;
        private bool isMultiline = false;

        [Browsable(true)]
        [Category("Aparência")]
        public override string Text
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }

        [Category("Aparência")]
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = value; this.Invalidate(); }
        }

        [Category("Aparência")]
        public int BorderSize
        {
            get => borderSize;
            set { borderSize = value; this.Invalidate(); }
        }

        [Category("Aparência")]
        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; this.Invalidate(); }
        }

        [Category("Aparência")]
        public Color BackgroundColor
        {
            get => backgroundColor;
            set { backgroundColor = value; this.Invalidate(); textBox.BackColor = value; }
        }

        [Category("Aparência")]
        public Color TextColor
        {
            get => textColor;
            set { textColor = value; textBox.ForeColor = value; }
        }

        [Category("Aparência")]
        public bool Multiline
        {
            get => isMultiline;
            set
            {
                isMultiline = value;
                textBox.Multiline = value;
                UpdateControl();
            }
        }

        //[Category("Aparência")]
        //public override string Text
        //{
        //    get => textBox.Text;
        //    set { textBox.Text = value; }
        //}

        [Category("Aparência")]
        public override Font Font
        {
            get => base.Font;
            set { base.Font = value; textBox.Font = value; }
        }

        [Browsable(true)]
        [Category("Behavior")]
        public bool ReadOnly
        {
            get => textBox.ReadOnly;
            set => textBox.ReadOnly = value;
        }

        public RoundedTextBox()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent;

            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = backgroundColor;
            textBox.ForeColor = textColor;
            textBox.Location = new Point(10, 7);
            textBox.Width = this.Width - 20;
            textBox.Multiline = isMultiline;
            textBox.TextChanged += (s, e) =>
            {
                OnTextChanged(e);
            };

            this.Controls.Add(textBox);
            this.Padding = new Padding(10);

            this.Resize += (s, e) => UpdateControl();
        }

        private void UpdateControl()
        {
            textBox.Width = this.Width - 20;
            if (isMultiline)
            {
                textBox.Height = this.Height - 14;
                textBox.ScrollBars = ScrollBars.Vertical;
            }
            else
            {
                textBox.Height = this.Font.Height + 8;
                textBox.ScrollBars = ScrollBars.None;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rectSurface = this.ClientRectangle;
            rectSurface.Inflate(-1, -1);

            using (GraphicsPath pathBorder = GetRoundRectangle(rectSurface, borderRadius))
            using (SolidBrush surfaceBrush = new SolidBrush(backgroundColor))
            using (Pen penBorder = new Pen(borderColor, borderSize)) // ✅ Borda sólida (remove DashStyle)
            {
                // Fundo
                e.Graphics.FillPath(surfaceBrush, pathBorder);

                // Borda sólida
                e.Graphics.DrawPath(penBorder, pathBorder);

                // Define a região arredondada
                this.Region = new Region(pathBorder);
            }

            textBox.BackColor = backgroundColor;
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

        public new event EventHandler TextChanged;
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            TextChanged?.Invoke(this, e);
        }
    }
}
