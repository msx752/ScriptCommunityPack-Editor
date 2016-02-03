using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SphereScp
{
    [Browsable(false)]
    public partial class MSATabControl : Control
    {
        Color activePageColor = Color.FromArgb(45, 45, 45);
        Color otherPageColor = Color.FromArgb(16, 12, 16);
        public MSATabControl()
        {
            InitializeComponent();
            //ContextMenuStrip stp = new ContextMenuStrip();
            //stp.Name = "cntMSATabControl";

            Button btClose = new Button();
            btClose.Name = "btcClose";
            btClose.BackgroundImage = Properties.Resources.closePage;
            btClose.TabStop = false;
            btClose.FlatAppearance.BorderSize = 0;
            btClose.AutoSize = true;
            btClose.Margin = new Padding(0);
            btClose.Padding = new Padding(0);
            btClose.FlatStyle = FlatStyle.Flat;
            btClose.Size = new Size(20, 25);
            btClose.Location = new Point(-20, 0);
            btClose.ImageAlign = ContentAlignment.MiddleCenter;
            btClose.BackgroundImageLayout = ImageLayout.Stretch;
            btClose.MouseClick += btClose_MouseClick;
            Controls.Add(btClose);

            Button btSlose = new Button();
            btSlose.Name = "btsSwitch";
            btSlose.BackgroundImage = Properties.Resources.SwitchPage;
            btSlose.TabStop = false;
            btSlose.FlatAppearance.BorderSize = 0;
            btSlose.AutoSize = true;
            btSlose.Margin = new Padding(0);
            btSlose.Padding = new Padding(0);
            btSlose.FlatStyle = FlatStyle.Flat;
            btSlose.Size = new Size(20, 25);
            btSlose.Location = new Point(-20, 0);
            btSlose.ImageAlign = ContentAlignment.MiddleCenter;
            btSlose.BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(btSlose);
        }

        private void btClose_MouseClick(object sender, MouseEventArgs e)
        {
            if (CurrentPage != null)
                RemovePage(CurrentPage);
        }

        private int _Pagecounter = 0;//for page unique ID
        private MSATabPage _currpage;
        public MSATabPage CurrentPage//current visible page
        {
            get
            {
                if (Pages.Count() != 0)
                    return _currpage;
                else
                    return null;
            }
            private set
            {
                _currpage = value;
                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i] is MSATabPage)
                    {
                        if (Controls[i] != _currpage)
                            Controls[i].Visible = false;
                        else
                            Controls[i].Visible = true;
                    }
                }
            }
        }

        public Control[] PageButtons //BUTTON LIST
        {
            get
            {
                Control[] ctl = new Control[Controls.Count];
                int index = 0;
                for (int i = 0; i < Controls.Count; i++)
                {
                    if ((Controls[i] is Button) && (Controls[i].Name.StartsWith("btn")))
                    {
                        ctl[index] = Controls[i];
                        index++;
                    }
                }
                Array.Resize(ref ctl, index);
                return ctl;
            }
        }
        public Control[] Pages //BUTTON LIST
        {
            get
            {
                Control[] ctl = new Control[Controls.Count];
                int index = 0;
                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i] is MSATabPage)
                    {
                        ctl[index] = Controls[i];
                        index++;
                    }
                }
                Array.Resize(ref ctl, index);
                return ctl;
            }
        }

        public void AddPage(MSATabPage newPage, ContextMenuStrip menuStrip)//you mustn't use like "Control.Add(MSATablePage)"
        {
            Controls.Add(newPage);
            AddPageTitleButton(newPage, menuStrip);
        }
        public void RemovePage(Control removedPage)
        {
            Controls.Remove(removedPage);
            int currentIndex = 0;
            for (int i = 0; i < PageButtons.Count(); i++)
            {
                if (PageButtons[i].Tag.ToString() == removedPage.Name)
                {
                    Controls.Remove(PageButtons[i]);
                    currentIndex = i;
                    if (currentIndex == PageButtons.Count())
                        currentIndex--;
                    break;
                }
            }
            if (Pages.Count() == 1)
            {
                CurrentPage = Pages[0] as MSATabPage;
            }
            else
            {
                if (CurrentPage == removedPage && currentIndex != -1)
                    CurrentPage = Pages[currentIndex] as MSATabPage;
            }
            refrestPages();
        }
        private void AddPageTitleButton(Control page, ContextMenuStrip menuStrip)
        {
            ToolStripItem tspKapat = menuStrip.Items[0];
            tspKapat.Click += new System.EventHandler(TspKapat_Click);
            tspKapat.Tag = page.Name;
            Button bPage = new Button();
            bPage.ContextMenuStrip = menuStrip;
            bPage.TabStop = false;
            bPage.FlatAppearance.BorderSize = 0;
            bPage.AutoSize = true;
            bPage.Name = "btn" + page.Name;
            bPage.BackgroundImage = Properties.Resources.btnBg;
            bPage.ImageAlign = ContentAlignment.MiddleCenter;
            bPage.BackgroundImageLayout = ImageLayout.Stretch;
            bPage.Text = (page as MSATabPage).PageTitle;
            bPage.Tag = page.Name;
            bPage.TextAlign = ContentAlignment.MiddleCenter;
            bPage.ForeColor = Color.White;
            bPage.MinimumSize = new Size(20, 25);
            bPage.Height = 25;
            bPage.Font = new Font(new FontFamily("consolas"), 9.75f);
            bPage.Margin = new Padding(0);
            bPage.Padding = new Padding(0);
            bPage.FlatStyle = FlatStyle.Flat;
            Controls.Add(bPage);
        }

        private void TspKapat_Click(object sender, EventArgs e)
        {
            string MSATPage = (sender as ToolStripItem).Tag.ToString();
            Control MSATPageClosed = Controls[MSATPage];
            if (MSATPageClosed != null)
                RemovePage(MSATPageClosed);
        }

        protected override void OnControlAdded(ControlEventArgs e)//location and size are configuring
        {
            if (e.Control is MSATabPage)
            {
                Rectangle tabcontrolloc = ClientRectangle;
                Point newLoc = new Point(0, 25);
                Size newSize = tabcontrolloc.Size;
                e.Control.Name = ("MSATPage" + _Pagecounter);//unique ID
                e.Control.Location = newLoc;
                e.Control.Size = newSize;
                CurrentPage = e.Control as MSATabPage;
                _Pagecounter++;
            }
            else if (e.Control is Button && (e.Control.Name.StartsWith("btn")))
            {
                Control changeVisibility = Controls.Find(e.Control.Tag.ToString(), false).First();
                e.Control.Visible = true;
                refreshButtons();
            }
            base.OnControlAdded(e);
        }
        private void refreshButtons()
        {
            int currentTabControlWidth = Width - 100;
            int currrentButtonWidth = 0;
            for (int i = 0; i < PageButtons.Count(); i++)
            {
                if (currrentButtonWidth < currentTabControlWidth)
                {
                    int width = PageButtons[i].Width;
                    PageButtons[i].Location = new Point(currrentButtonWidth, 0);
                    currrentButtonWidth += width;
                    PageButtons[i].Visible = true;
                }
                else
                {
                    PageButtons[i].Visible = false;
                }
                if (PageButtons[i].Tag.ToString() == CurrentPage.Name)
                {
                    PageButtons[i].BackColor = activePageColor;
                }
                else
                {
                    PageButtons[i].BackColor = otherPageColor;
                }
            }
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is Button && (Controls[i].Name.StartsWith("btc")))
                    Controls[i].Location = new Point(this.Width - 23, 0);
                else if (Controls[i] is Button && (Controls[i].Name.StartsWith("bts")))
                    Controls[i].Location = new Point(this.Width - 46, 0);
            }
        }

        private void refrestPages()
        {
            for (int i = 0; i < Pages.Count(); i++)
            {
                Rectangle tabcontrolloc = ClientRectangle;
                Point newLoc = new Point(0, 25);
                Size newSize = tabcontrolloc.Size;
                Pages[i].Location = newLoc;
                Pages[i].Size = newSize;
            }
        }
        protected override void OnClientSizeChanged(EventArgs e)///auto resize MSATabPage
        {
            Rectangle tabcontrolloc = ClientRectangle;
            Point newLoc = new Point(0, 25);
            Size newSize = tabcontrolloc.Size;
            foreach (Control item in Controls)
            {
                if (item is MSATabPage)
                {
                    if (item == _currpage)
                    {
                        item.Location = newLoc;
                        item.Size = newSize;
                    }
                }
            }
            refreshButtons();
            base.OnClientSizeChanged(e);
        }
    }
}
