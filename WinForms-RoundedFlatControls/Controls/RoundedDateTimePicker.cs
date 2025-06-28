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
    public class RoundedDateTimePicker : UserControl
    {
        private DateTimePicker dateTimePicker = new DateTimePicker();

        public Color BorderColor { get; set; } = Color.Black;
        public Color BackPanelColor { get; set; } = Color.FromArgb(43, 50, 60);
        public Color TextColor { get; set; } = Color.White;
        public int BorderRadius { get; set; } = 10;

        [Browsable(true)]
        public DateTime Value
        {
            get => dateTimePicker.Value;
            set => dateTimePicker.Value = value;
        }

        public RoundedDateTimePicker()
        {
            this.DoubleBuffered = true;
            this.BackColor = BackPanelColor;
            this.Size = new Size(200, 35);

            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dateTimePicker.ShowUpDown = false;
            dateTimePicker.Width = this.Width - 20;
            dateTimePicker.Height = this.Height - 10;
            dateTimePicker.Location = new Point(10, 5);
            dateTimePicker.CalendarMonthBackground = BackPanelColor;
            dateTimePicker.CalendarForeColor = TextColor;

            // Estilo do texto
            dateTimePicker.CalendarTitleBackColor = BackPanelColor;
            dateTimePicker.CalendarTitleForeColor = TextColor;
            dateTimePicker.CalendarTrailingForeColor = Color.Gray;

            // Remove borda do DateTimePicker nativo
            //dateTimePicker.FlatStyle = FlatStyle.Flat;

            this.Controls.Add(dateTimePicker);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int radius = BorderRadius;
            using (var pen = new Pen(BorderColor, 2))
            using (var brush = new SolidBrush(BackPanelColor))
            {
                var rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                var path = RoundedRect(rect, radius);

                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            dateTimePicker.Width = this.Width - 20;
            dateTimePicker.Height = this.Height - 10;
            this.Invalidate();
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        // Evento ValueChanged
        public event EventHandler ValueChanged
        {
            add { dateTimePicker.ValueChanged += value; }
            remove { dateTimePicker.ValueChanged -= value; }
        }
    }
}
