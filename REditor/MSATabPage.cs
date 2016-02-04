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
        public event EventHandler<FormClosingEventArgs> MSATabPageClosing = null;
        private string _pagetitle = "newPage";
        public bool isNeedSave { get; set; } = false;
        internal string ButtonName { get; set; } = "";
        public MSATabPage()
        {
            InitializeComponent();
            MSATabPageClosing += MSATabPage_MSATabPageClosing;
        }
        public virtual void OnMSATabPageClosing(FormClosingEventArgs e)
        {
            if (MSATabPageClosing != null)
                MSATabPageClosing.Invoke(this, e);
        }
        private void MSATabPage_MSATabPageClosing(object sender, FormClosingEventArgs e)
        {

        }

        public MSATabPage(Control page, string Title) : this(page)
        {
            PageTitle = Title;
        }

        public MSATabPage(Control page)
        {
            InitializeComponent();
            page.Dock = DockStyle.Fill;
            Margin = new Padding(0);
            Padding = new Padding(0);
            BorderStyle = BorderStyle.None;
            Controls.Add(page);
        }
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
        public bool ClosePage()
        {
            if (isNeedSave)
                return false;
            else
                return true;
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

        public override string ToString()
        {
            return Name;
        }
    }
}
