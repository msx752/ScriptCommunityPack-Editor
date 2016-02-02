using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SphereScp
{
    [Browsable(false)]
    public partial class MSATabPage : Panel
    {
        public MSATabPage()
        {
            InitializeComponent();
        }
        private string _pagetitle = "newPage";
        public string PageTitle
        {
            get { return _pagetitle; }
            set
            {
                if (value.Length > 25)
                    _pagetitle = value.Substring(0, 25);
                else
                    _pagetitle = value;
            }
        }

        public MSATabPage(Control page, string Title) : this(page)
        {
            PageTitle = Title;
        }
        public MSATabPage(Control page)
        {
            InitializeComponent();
            page.Dock = DockStyle.Fill;
            Controls.Add(page);
        }

        public T GetPage<T>() where T : Control
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is T)
                    return Controls[i] as T;
            }
            return null;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
