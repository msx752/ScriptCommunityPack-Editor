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
        //private MSATabPage _currpage;
        private int _Pagecounter = 0;

        string _selectedPage = "";

        private Color activePageButtonColor = Color.FromArgb(45, 45, 45);

        private Color otherPageButtonColor = Color.FromArgb(16, 12, 16);

        private List<string> PageButtonLineUp = new List<string>();

        private ContextMenuStrip SwitchMenu = new ContextMenuStrip();

        public MSATabControl()
        {
            InitializeComponent();
            BackgroundImage = Properties.Resources.bg1;
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

            SwitchMenu.Name = "switchToolStripMenu";
            SwitchMenu.ShowImageMargin = false;
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
            btSlose.MouseClick += BtSlose_MouseClick;
            Controls.Add(btSlose);
        }

        public event EventHandler<ControlEventArgs> MSATabPageClosed;
        public event EventHandler<ControlEventArgs> MSATabPageOpened;
        public event EventHandler<ControlEventArgs> SelectedPageChanged;
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

        //for page unique ID
        public MSATabPage SelectedPage//current visible page
        {
            get
            {
                if (Pages.Count() > 0)
                    return Controls[_selectedPage] as MSATabPage;
                else
                    return null;
            }
            set
            {
                if (value != null)
                {
                    _selectedPage = value.Name;

                    //_currpage = value;
                    if (SelectedPageChanged != null)
                        SelectedPageChanged.Invoke(this, new ControlEventArgs(Controls[_selectedPage]));

                    for (int i = 0; i < Controls.Count; i++)
                    {
                        if (Controls[i] is MSATabPage)
                        {
                            if (Controls[i] != Controls[_selectedPage])
                            {
                                Controls[i].Visible = false;
                                Controls[i].Enabled = false;
                            }
                            else
                            {
                                Controls[i].Visible = true;
                                Controls[i].Enabled = true;
                            }
                        }
                    }
                    if (_selectedPage != "")
                    {
                        refreshButtons();
                        refreshPages();
                    }
                }

            }
        }
        public void AddPage(MSATabPage newPage)//you mustn't use like "Control.Add(MSATablePage)"
        {
            ContextMenuStrip menuStrip = new ContextMenuStrip();
            menuStrip.Name = "cntMSATabPage" + _Pagecounter;
            menuStrip.Items.Add(new ToolStripMenuItem() { Name = "kapatToolStripMenuItem" + _Pagecounter, Text = "Kapat" });

            newPage.Name = ("MSATPage" + _Pagecounter);//unique ID
            Controls.Add(newPage);
            AddPageTitleButton(newPage, menuStrip);
            _Pagecounter++;
        }

        public void RemovePage(MSATabPage removedPage)
        {
            if (removedPage.ClosePage())
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
                        PageButtonLineUp.Remove(removedPage.ButtonName);
                        refreshButtons();
                        break;
                    }
                }
                if (Pages.Count() == 1)
                {
                    SelectedPage = Pages[0] as MSATabPage;
                }
                else
                {
                    if (SelectedPage == removedPage && currentIndex != -1)
                        SelectedPage = Pages[currentIndex] as MSATabPage;
                }
                if (MSATabPageClosed != null)
                    MSATabPageClosed.Invoke(this, new ControlEventArgs(removedPage));
            }
            else
            {
                (removedPage as MSATabPage).OnMSATabPageClosing(new FormClosingEventArgs(CloseReason.UserClosing, false));
            }
        }

        public override string ToString()
        {
            return Name;
        }
        protected override void OnClientSizeChanged(EventArgs e)///auto resize MSATabPage
        {
            refreshPages();
            refreshButtons();
            base.OnClientSizeChanged(e);
        }

        protected override void OnControlAdded(ControlEventArgs e)//location and size are configuring
        {
            if (e.Control is MSATabPage)
            {
                Rectangle tabcontrolloc = ClientRectangle;
                Point newLoc = new Point(0, 25);
                Size newSize = tabcontrolloc.Size;
                e.Control.Location = newLoc;
                e.Control.Size = newSize;
                SelectedPage = e.Control as MSATabPage;
                refreshButtons();
                if (MSATabPageOpened != null)
                    MSATabPageOpened.Invoke(this, new ControlEventArgs(SelectedPage));
            }
            else if (e.Control is Button && (e.Control.Name.StartsWith("btn")))
            {
                Control changeVisibility = Controls.Find(e.Control.Tag.ToString(), false).First();
                e.Control.Visible = true;
                Controls[changeVisibility.Name].Tag = e.Control.Name;
                PageButtonLineUp.Add(e.Control.Name);
                refreshButtons();
            }
            base.OnControlAdded(e);
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
            bPage.MouseClick += BPage_MouseClick;
            (Controls[page.Name] as MSATabPage).ButtonName = bPage.Name;
            Controls.Add(bPage);
        }

        private void BPage_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string MSATPage = (sender as Button).Tag.ToString();
                SelectedPage = Controls[MSATPage] as MSATabPage;
            }
        }

        private void btClose_MouseClick(object sender, MouseEventArgs e)
        {
            if (SelectedPage != null)
                RemovePage(SelectedPage);
        }

        private void BtSlose_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SwitchMenu.Items.Clear();
                foreach (Button item in PageButtons)
                {
                    if (!item.Visible)
                    {
                        ToolStripMenuItem hiddenButton = new ToolStripMenuItem()
                        {
                            Name = "switch" + item.Name,
                            Text = item.Text,
                            Tag = item.Name,
                            TextAlign = ContentAlignment.MiddleCenter,
                            DisplayStyle = ToolStripItemDisplayStyle.Text
                        };
                        hiddenButton.Click += HiddenButton_Click;
                        SwitchMenu.Items.Add(hiddenButton);
                    }
                }
                if (SwitchMenu.Items.Count > 0)
                {
                    Button btn = sender as Button;
                    Point menuLoc = new Point(0, btn.Height - 1);
                    menuLoc = btn.PointToScreen(menuLoc);
                    SwitchMenu.Show(menuLoc);
                }
            }
        }

        private void ButtonLineUpVisibleControl()
        {
            if (SelectedPage != null)
            {
                if (SelectedPage.ButtonName != "")
                {
                    if (!Controls[SelectedPage.ButtonName].Visible)
                    {
                        int OldSelectedindex = PageButtonLineUp.FindIndex(p => p == SelectedPage.ButtonName);
                        string first_to_old = PageButtonLineUp[0];
                        PageButtonLineUp[0] = SelectedPage.ButtonName;
                        Controls[PageButtonLineUp[0]].Visible = true;
                        PageButtonLineUp[OldSelectedindex] = first_to_old;
                        Controls[PageButtonLineUp[OldSelectedindex]].Visible = false;
                    }
                }
            }
        }

        private void HiddenButton_Click(object sender, EventArgs e)
        {
            string MSATPageTitleButton = (sender as ToolStripMenuItem).Tag.ToString();
            string MSATPage = (Controls[MSATPageTitleButton] as Button).Tag.ToString();
            SelectedPage = Controls[MSATPage] as MSATabPage;
        }
        private void refreshButtons()
        {
            int currentTabControlWidth = Width - 100;
            int currrentButtonWidth = 0;
            ButtonLineUpVisibleControl();
            for (int i = 0; i < PageButtonLineUp.Count; i++)
            {
                if (SelectedPage != null)
                {
                    if (currrentButtonWidth < currentTabControlWidth)
                    {
                        int width = 0;
                        try
                        {
                            width = Controls[PageButtonLineUp[i]].Width;
                        }
                        catch (Exception e)
                        {
                            PageButtonLineUp.RemoveAt(i);
                            break;
                        }
                        Controls[PageButtonLineUp[i]].Location = new Point(currrentButtonWidth, 0);
                        currrentButtonWidth += width;
                        Controls[PageButtonLineUp[i]].Visible = true;
                    }
                    else
                        Controls[PageButtonLineUp[i]].Visible = false;
                    if (Controls[PageButtonLineUp[i]].Tag.ToString() == SelectedPage.Name)
                        Controls[PageButtonLineUp[i]].BackColor = activePageButtonColor;
                    else
                        Controls[PageButtonLineUp[i]].BackColor = otherPageButtonColor;
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is Button && (Controls[i].Name.StartsWith("btc")))
                    Controls[i].Location = new Point(this.Width - 23, 0);
                else if (Controls[i] is Button && (Controls[i].Name.StartsWith("bts")))
                    Controls[i].Location = new Point(this.Width - 46, 0);
                else
                    break;
            }
            ButtonLineUpVisibleControl();
        }

        private void refreshPages()
        {
            for (int i = 0; i < Pages.Count(); i++)
            {
                Rectangle tabcontrolloc = ClientRectangle;
                Point newLoc = new Point(0, 25);
                Size newSize = tabcontrolloc.Size;
                Pages[i].Location = newLoc;
                newSize.Height -= 25;
                Pages[i].Size = newSize;
            }
        }

        private void TspKapat_Click(object sender, EventArgs e)
        {
            string MSATPage = (sender as ToolStripItem).Tag.ToString();
            Control MSATPageClosed = Controls[MSATPage];
            if (MSATPageClosed != null)
                RemovePage(MSATPageClosed as MSATabPage);
        }
    }

    public class MSATabPageClosingEventArgs : FormClosingEventArgs
    {
        private Control _ctrl;
        public MSATabPageClosingEventArgs(CloseReason closeReason, bool cancel, MSATabPage ctrl) : base(closeReason, cancel)
        {
            Control = ctrl;
        }

        public Control Control
        {
            get { return _ctrl; }
            set { _ctrl = value; }
        }
    }
}
