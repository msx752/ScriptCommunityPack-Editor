using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SphereScp.Render
{
    public class StripMenuRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs _menu)
        {
            Rectangle _menuRectangle = new Rectangle(Point.Empty, _menu.Item.Size);
            if (!_menu.Item.Selected)
            {
                _menu.Graphics.FillRectangle(Brushes.Black, _menuRectangle);
            }
            else
            {
                _menu.Graphics.FillRectangle(Brushes.DarkSlateGray, _menuRectangle);
                _menu.Graphics.DrawRectangle(Pens.Gray, 0.1f, 0, _menuRectangle.Width - 2, _menuRectangle.Height - 2);
            }
        }
    }
}
