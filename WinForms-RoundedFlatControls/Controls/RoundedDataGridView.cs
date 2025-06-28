using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_RoundedFlatControls.Controls
{
    public class RoundedDataGridView : UserControl
    {
        public DataGridView DataGridView { get; private set; } = new DataGridView();
        public Color BorderColor { get; set; } = Color.Black;
        public int BorderRadius { get; set; } = 10;
        public Color BackPanelColor { get; set; } = Color.FromArgb(43, 50, 60);

        public RoundedDataGridView()
        {
            this.DoubleBuffered = true;
            this.BackColor = BackPanelColor;
            this.Padding = new Padding(2);

            DataGridView.Parent = this;
            DataGridView.Dock = DockStyle.Fill;
            DataGridView.BorderStyle = BorderStyle.None;
            DataGridView.BackgroundColor = BackPanelColor;
            DataGridView.GridColor = Color.FromArgb(60, 60, 60);

            StyleGrid();

            this.Controls.Add(DataGridView);
        }

        private void StyleGrid()
        {
            DataGridView.EnableHeadersVisualStyles = false;

            // Cabeçalho
            DataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 70, 80);
            DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Linhas
            DataGridView.DefaultCellStyle.BackColor = Color.FromArgb(50, 58, 68);
            DataGridView.DefaultCellStyle.ForeColor = Color.White;
            DataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 120, 180);
            DataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            DataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            DataGridView.RowHeadersVisible = false;
            DataGridView.AllowUserToAddRows = false;
            DataGridView.AllowUserToDeleteRows = false;
            DataGridView.AllowUserToResizeRows = false;
            DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            SetRoundedRegion();
            this.Invalidate();
        }

        private void SetRoundedRegion()
        {
            using (var path = RoundedRect(new Rectangle(0, 0, this.Width, this.Height), BorderRadius))
            {
                this.Region = new Region(path);
                this.Invalidate();
                this.Update();
                DataGridView.ClearSelection();
            }
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
