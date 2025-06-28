using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_RoundedFlatControls.Forms
{
    public class RoundedForm
    {
        // Evento de redimensionamento do formulário
        public void SetRoundedRegion(Form form)
        {
            form.FormBorderStyle = FormBorderStyle.None;

            int borderRadius = 10; // Raio dos cantos arredondados
            Rectangle bounds = new Rectangle(0, 0, form.Width, form.Height);
            GraphicsPath path = new GraphicsPath();

            // Canto superior esquerdo
            path.AddArc(bounds.X, bounds.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            // Canto superior direito
            path.AddArc(bounds.X + bounds.Width - borderRadius * 2, bounds.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            // Canto inferior direito
            path.AddArc(bounds.X + bounds.Width - borderRadius * 2, bounds.Y + bounds.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            // Canto inferior esquerdo
            path.AddArc(bounds.X, bounds.Y + bounds.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.CloseAllFigures();

            form.Region = new Region(path);

            //if (form.Name != "FrmHome")
            //{
            //    form.ShowInTaskbar = false;
            //}

        }
    }
}
