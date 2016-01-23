using FastColoredTextBoxNS.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
    public class AutocompleteListView : UserControl
    {
        internal ToolTip toolTip;
        internal List<AutoCompleteItem> visibleItems;
        private int hoveredItemIndex;
        private int itemHeight;
        private int oldItemCount;
        private int selectedItemIndex;
        private List<AutoCompleteItem> sourceItems;
        private FastColoredTextBox tb;
        private Timer timer;

        internal AutocompleteListView(FastColoredTextBox tb)
        {
            EventHandler handler = null;
            EventHandler handler2 = null;
            FormClosingEventHandler handler3 = null;
            EventHandler handler4 = null;
            EventHandler handler5 = null;
            ScrollEventHandler handler6 = null;
            this.sourceItems = new List<AutoCompleteItem>();
            this.selectedItemIndex = 0;
            this.hoveredItemIndex = -1;
            this.oldItemCount = 0;
            this.toolTip = new ToolTip();
            this.timer = new Timer();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.Font = new Font(FontFamily.GenericSansSerif, 9f);
            this.visibleItems = new List<AutoCompleteItem>();
            this.itemHeight = this.Font.Height + 2;
            base.VerticalScroll.SmallChange = this.itemHeight;
            this.BackColor = Color.White;
            this.MaximumSize = new Size(base.Size.Width, 180);
            this.toolTip.ShowAlways = false;
            this.toolTip.OwnerDraw = true;
            this.toolTip.Draw += ToolTip_Draw;
            this.toolTip.Popup += ToolTip_Popup;

            this.AppearInterval = 500;
            this.timer.Tick += new EventHandler(this.timer_Tick);
            this.tb = tb;
            tb.KeyDown += new KeyEventHandler(this.tb_KeyDown);
            tb.SelectionChanged += new EventHandler(this.tb_SelectionChanged);
            tb.KeyPressed += new KeyPressEventHandler(this.tb_KeyPressed);
            Form form = tb.FindForm();
            if (form != null)
            {
                if (handler == null)
                {
                    handler = (o, e) => this.Menu.Close();
                }
                form.LocationChanged += handler;
                if (handler2 == null)
                {
                    handler2 = (o, e) => this.Menu.Close();
                }
                form.ResizeBegin += handler2;
                if (handler3 == null)
                {
                    handler3 = (o, e) => this.Menu.Close();
                }
                form.FormClosing += handler3;
                if (handler4 == null)
                {
                    handler4 = (o, e) => this.Menu.Close();
                }
                form.LostFocus += handler4;
            }
            if (handler5 == null)
            {
                handler5 = delegate (object o, EventArgs e)
                {
                    if (!this.Menu.Focused)
                    {
                        this.Menu.Close();
                    }
                };
            }
            tb.LostFocus += handler5;
            if (handler6 == null)
            {
                handler6 = (o, e) => this.Menu.Close();
            }
            tb.Scroll += handler6;
        }

        public int Count
        {
            get
            {
                return this.visibleItems.Count;
            }
        }

        public System.Windows.Forms.ImageList ImageList { get; set; }

        internal bool AllowTabKey { get; set; }

        internal int AppearInterval
        {
            get
            {
                return this.timer.Interval;
            }
            set
            {
                this.timer.Interval = value;
            }
        }

        private AutocompleteMenu Menu
        {
            get
            {
                return (base.Parent as AutocompleteMenu);
            }
        }

        public void SelectNext(int shift)
        {
            this.selectedItemIndex = Math.Max(0, Math.Min((int)(this.selectedItemIndex + shift), (int)(this.visibleItems.Count - 1)));
            this.DoSelectedVisible();
            base.Invalidate();
        }

        public void SetAutocompleteItems(List<AutoCompleteItem> items)
        {
            this.sourceItems = items;
        }

        public void SetAutocompleteItems(ICollection<string> items)
        {
            List<AutoCompleteItem> list = new List<AutoCompleteItem>(items.Count);
            foreach (string str in items)
            {
                list.Add(new AutoCompleteItem(str));
            }
            this.SetAutocompleteItems(list);
        }

        internal void DoAutocomplete()
        {
            this.DoAutocomplete(false);
        }

        internal void DoAutocomplete(bool forced)
        {
            if (!this.Menu.Enabled)
            {
                this.Menu.Close();
            }
            else
            {
                this.visibleItems.Clear();
                this.selectedItemIndex = 0;
                base.VerticalScroll.Value = 0;
                Range fragment = this.tb.Selection.GetFragment(this.Menu.SearchPattern);
                string text = fragment.Text;
                Point position = this.tb.PlaceToPoint(fragment.End);
                position.Offset(2, this.tb.CharHeight);
                if (forced || ((text.Length >= this.Menu.MinFragmentLength) && (this.tb.Selection.Start == this.tb.Selection.End)))
                {
                    this.Menu.Fragment = fragment;

                    if (text.Length == 0)
                        return;
                    bool flag = false;
                    this.sourceItems
                        .ForEach(val => val.Parent = this.Menu);//all member sets parent
                    List<AutoCompleteItem> finded;
                    if (this.tb.GetLineText(this.Menu.Fragment.Start.iLine).Replace(" ", "").StartsWith("[") == false)
                    {
                        string[] splited = text.Replace(" ", "").Split('.');//for sourcetree (notimplemented)

                        if (splited.Length == 1
                            && (text.StartsWith("i_")
                            || text.StartsWith("c_")
                            || text.StartsWith("d_")
                            || text.StartsWith("e_")
                            || text.StartsWith("f_")))
                        {
                            CmdDefType typ = CmdDefType.NONE;
                            if (splited[0].StartsWith("i_"))
                                typ = CmdDefType.ITEMDEF;
                            else if (splited[0].StartsWith("c_"))
                                typ = CmdDefType.CHARDEF;
                            else if (splited[0].StartsWith("d_"))
                                typ = CmdDefType.DIALOG;
                            else if (splited[0].StartsWith("e_"))
                                typ = CmdDefType.EVENTS;
                            else if (splited[0].StartsWith("f_"))
                                typ = CmdDefType.FUNCTION;

                            //listing which are all
                            finded = this.sourceItems
                               .Where(p => p.ToolTipTitle != null).ToList()
                               .Where(p => p.ToolTipTitle.IndexOf("[" + typ.ToString()) != -1)
                               .Where(p => p.Text.IndexOf("_") != -1)
                               .ToList();
                            if (finded.Count > 0)
                            {
                                while (finded.Count >= 31)//max listed func
                                    finded.RemoveAt(30);
                                this.visibleItems.AddRange(finded);
                            }
                        }
                        else
                        {
                            //listing which are all
                            finded = this.sourceItems
                               .Where(p => p.ToolTipTitle != null).ToList()
                               .Where(p => p.Compare(text) == CompareResult.Visible)
                               .ToList();
                            if (finded.Count > 0)
                            {
                                while (finded.Count >= 31)//max listed func
                                    finded.RemoveAt(30);
                                this.visibleItems.AddRange(finded);
                            }

                            //listing which is searching
                            finded = this.sourceItems
                                .Where(p => p.ToolTipTitle != null).ToList()
                                .Where(p => p.Compare(text) == CompareResult.VisibleAndSelected)
                                .ToList();
                            if (finded.Count > 0)
                            {
                                while (finded.Count >= 31)//max listed func
                                    finded.RemoveAt(30);
                                this.visibleItems.AddRange(finded);

                                this.visibleItems = this.visibleItems
                                    .OrderBy(p => p.Text.IndexOf(splited[splited.Length - 1]))
                                    .ToList();
                                flag = true;
                                this.selectedItemIndex = 0;
                            }
                        }
                    }
                    else if (text.IndexOf(".") == -1)//defane is cannot using DOT
                    {
                        //listing which are all
                        finded = this.sourceItems
                           .Where(p => p.Text.ToLower()
                           .Replace("ı", "i")//turkish language fix
                           .IndexOf("[" + text.ToLower()) != -1)
                           .ToList();
                        if (finded.Count > 0)
                        {
                            while (finded.Count >= 31)//max listed func
                                finded.RemoveAt(30);
                            this.visibleItems.AddRange(finded);
                        }
                    }
                    if (flag)
                    {
                        this.AdjustScroll();
                        this.DoSelectedVisible();
                    }
                }
                if (this.Count > 0)
                {
                    if (!this.Menu.Visible)
                    {
                        CancelEventArgs args = new CancelEventArgs();
                        this.Menu.OnOpening(args);
                        if (!args.Cancel)
                        {
                            this.Menu.Show(this.tb, position);
                        }
                    }
                    else
                    {
                        base.Invalidate();
                    }
                }
                else
                {
                    this.Menu.Close();
                }
            }
        }

        internal virtual void OnSelecting()
        {
            if ((this.selectedItemIndex >= 0) && (this.selectedItemIndex < this.visibleItems.Count))
            {
                this.tb.TextSource.Manager.BeginAutoUndoCommands();
                try
                {
                    AutoCompleteItem item = this.visibleItems[this.selectedItemIndex];
                    SelectingEventArgs args = new SelectingEventArgs
                    {
                        Item = item,
                        SelectedIndex = this.selectedItemIndex
                    };
                    this.Menu.OnSelecting(args);
                    if (args.Cancel)
                    {
                        this.selectedItemIndex = args.SelectedIndex;
                        base.Invalidate();
                    }
                    else
                    {
                        if (!args.Handled)
                        {
                            Range fragment = this.Menu.Fragment;
                            this.DoAutocomplete(item, fragment);
                        }
                        this.Menu.Close();
                        SelectedEventArgs e = new SelectedEventArgs
                        {
                            Item = item,
                            Tb = this.Menu.Fragment.tb
                        };
                        item.OnSelected(this.Menu, e);
                        this.Menu.OnSelected(e);
                    }
                }
                finally
                {
                    this.tb.TextSource.Manager.EndAutoUndoCommands();
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left)
            {
                this.selectedItemIndex = this.PointToItemIndex(e.Location);
                this.DoSelectedVisible();
                base.Invalidate();
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            this.selectedItemIndex = this.PointToItemIndex(e.Location);
            base.Invalidate();
            this.OnSelecting();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.AdjustScroll();
            int num = (base.VerticalScroll.Value / this.itemHeight) - 1;
            int num2 = ((base.VerticalScroll.Value + base.ClientSize.Height) / this.itemHeight) + 1;
            num = Math.Max(num, 0);
            num2 = Math.Min(num2, this.visibleItems.Count);
            int y = 0;
            int x = 0x12;
            for (int i = num; i < num2; i++)
            {
                y = (i * this.itemHeight) - base.VerticalScroll.Value;
                if ((this.ImageList != null) && (this.visibleItems[i].ImageIndex >= 0))
                {
                    e.Graphics.DrawImage(this.ImageList.Images[this.visibleItems[i].ImageIndex], 1, y);
                }
                if (i == this.selectedItemIndex)
                {
                    Brush brush = new LinearGradientBrush(new Point(0, y - 3), new Point(0, y + this.itemHeight), Color.White, Color.Orange);
                    e.Graphics.FillRectangle(brush, x, y, (base.ClientSize.Width - 1) - x, this.itemHeight - 1);
                    e.Graphics.DrawRectangle(Pens.Orange, x, y, (base.ClientSize.Width - 1) - x, this.itemHeight - 1);
                }
                if (i == this.hoveredItemIndex)
                {
                    e.Graphics.DrawRectangle(Pens.Red, x, y, (base.ClientSize.Width - 1) - x, this.itemHeight - 1);
                }
                e.Graphics.DrawString(this.visibleItems[i].ToString(), this.Font, Brushes.Black, (float)x, (float)y);
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            base.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            this.ProcessKey(keyData, Keys.None);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AdjustScroll()
        {
            if (this.oldItemCount != this.visibleItems.Count)
            {
                int num = (this.itemHeight * this.visibleItems.Count) + 1;
                base.Height = Math.Min(num, this.MaximumSize.Height);
                this.Menu.CalcSize();
                base.AutoScrollMinSize = new Size(0, num);
                this.oldItemCount = this.visibleItems.Count;
            }
        }

        private void DoAutocomplete(AutoCompleteItem item, Range fragment)
        {
            string textForReplace = item.GetTextForReplace();
            FastColoredTextBox tb = fragment.tb;
            if (textForReplace.StartsWith("["))
                tb.Selection.Start = new Place(fragment.Start.iChar - 1, fragment.Start.iLine);
            //else if (item.ToolTipTitle.Split(' ')[0].StartsWith("["))
            //{
            //    tb.Selection.Start = new Place(tb.Selection.Start.iChar- item.ToolTipTitle.Split(' ')[0].Length, tb.Selection.Start.iLine);
            //}
            else
                tb.Selection.Start = fragment.Start;
            tb.Selection.End = fragment.End;
            tb.InsertText(textForReplace);
            tb.Focus();
        }

        private void DoSelectedVisible()
        {
            if ((this.selectedItemIndex >= 0) && (this.selectedItemIndex < this.visibleItems.Count))
            {
                this.SetToolTip(this.visibleItems[this.selectedItemIndex]);
            }
            int num = (this.selectedItemIndex * this.itemHeight) - base.VerticalScroll.Value;
            if (num < 0)
            {
                base.VerticalScroll.Value = this.selectedItemIndex * this.itemHeight;
            }
            if (num > (base.ClientSize.Height - this.itemHeight))
            {
                base.VerticalScroll.Value = Math.Min(base.VerticalScroll.Maximum, ((this.selectedItemIndex * this.itemHeight) - base.ClientSize.Height) + this.itemHeight);
            }
            base.AutoScrollMinSize -= new Size(1, 0);
            base.AutoScrollMinSize += new Size(1, 0);
        }

        private int PointToItemIndex(Point p)
        {
            return ((p.Y + base.VerticalScroll.Value) / this.itemHeight);
        }

        private bool ProcessKey(Keys keyData, Keys keyModifiers)
        {
            if (keyModifiers == Keys.None)
            {
                switch (keyData)
                {
                    case Keys.Tab:
                        if (this.AllowTabKey)
                        {
                            this.OnSelecting();
                            return true;
                        }
                        break;

                    case Keys.Enter:
                        this.OnSelecting();
                        return true;

                    case Keys.PageUp:
                        this.SelectNext(-10);
                        return true;

                    case Keys.Next:
                        this.SelectNext(10);
                        return true;

                    case Keys.Escape:
                        this.Menu.Close();
                        return true;

                    case Keys.Up:
                        this.SelectNext(-1);
                        return true;

                    case Keys.Down:
                        this.SelectNext(1);
                        return true;
                }
            }
            return false;
        }

        private void ResetTimer(Timer timer)
        {
            timer.Stop();
            timer.Start();
        }

        private void SetToolTip(AutoCompleteItem autocompleteItem)
        {
            string toolTipTitle = this.visibleItems[this.selectedItemIndex].ToolTipTitle;
            string toolTipText = this.visibleItems[this.selectedItemIndex].ToolTipText;
            if (string.IsNullOrEmpty(toolTipTitle))
            {
                this.toolTip.ToolTipTitle = null;
                this.toolTip.SetToolTip(this, null);
            }
            else if (string.IsNullOrEmpty(toolTipText))
            {
                this.toolTip.ToolTipTitle = null;
                this.toolTip.Show(toolTipTitle, this, base.Width + 3, 0, 5000);
            }
            else
            {
                this.toolTip.ToolTipTitle = toolTipTitle;
                this.toolTip.Show(toolTipText, this, base.Width + 3, 0, 5000);
            }
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Menu.Visible && this.ProcessKey(e.KeyCode, e.Modifiers))
            {
                e.Handled = true;
            }
            if (!this.Menu.Visible && ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Space)))
            {
                this.DoAutocomplete();
                e.Handled = true;
            }
        }

        private void tb_KeyPressed(object sender, KeyPressEventArgs e)
        {
            bool flag = (e.KeyChar == '\b') || (e.KeyChar == '\x00ff');
            if (!(!this.Menu.Visible || flag))
            {
                this.DoAutocomplete(false);
            }
            else
            {
                this.ResetTimer(this.timer);
            }
        }

        private void tb_SelectionChanged(object sender, EventArgs e)
        {
            if (this.Menu.Visible)
            {
                bool flag = false;
                if (this.tb.Selection.Start != this.tb.Selection.End)
                {
                    flag = true;
                }
                else if (!this.Menu.Fragment.Contains(this.tb.Selection.Start))
                {
                    if ((this.tb.Selection.Start.iLine == this.Menu.Fragment.End.iLine) && (this.tb.Selection.Start.iChar == (this.Menu.Fragment.End.iChar + 1)))
                    {
                        if (!Regex.IsMatch(this.tb.Selection.CharBeforeStart.ToString(), this.Menu.SearchPattern))
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    this.Menu.Close();
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.DoAutocomplete(false);
        }

        private void ToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            ToolTip tp = sender as ToolTip;
            Graphics g = e.Graphics;
            SolidBrush b = new SolidBrush(Color.FromArgb(240, 255, 255));

            string str = e.ToolTipText;
            string[] texti = str.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            if (texti.Length == 3)
            {
                if (texti[1] == "" & texti[2] == "")
                {
                    return;
                }
            }
            if (texti.Length > 1)
            {
                Rectangle rct = e.Bounds;
                //rct.Height = 48;
                g.FillRectangle(b, rct);

                g.DrawString(texti[0], new Font(e.Font.FontFamily, 9f, FontStyle.Bold), Brushes.Black,
                    new PointF(rct.X + 6, rct.Y + 2)); // top layer

                SizeF stringSize = new SizeF();
                stringSize = e.Graphics.MeasureString(texti[0], e.Font, 0);
                g.DrawString(texti[1], new Font(e.Font.FontFamily, 8.5f, FontStyle.Regular), Brushes.Black,
                    new PointF(rct.X + 4 + stringSize.Width, rct.Y + 3)); // mid layer

                g.DrawString(texti[2], new Font(e.Font.FontFamily, 8.5f, FontStyle.Regular | FontStyle.Italic), Brushes.Green,
                   new PointF(rct.X + 4, rct.Y + 16)); // bot layer

                Font destinationFONT = new Font(e.Font.FontFamily, 7.5f, FontStyle.Regular);
                if (texti.Length == 4)
                {
                    g.DrawString(texti[3],
                        destinationFONT, Brushes.Blue, new PointF(rct.X + 4, rct.Y + 30)); // bot layer
                }
                else if (texti.Length > 4)
                {
                    //texti[3]=texti[3] + " ]";
                    for (int i = 3; i < texti.Length; i++)
                    {
                        if (i == texti.Length - 1)
                        {
                            texti[i] = texti[i];
                        }
                        g.DrawString(texti[i],
                            destinationFONT, Brushes.Blue, new PointF(rct.X + 4, rct.Y + 30 + ((i - 3) * 15))); // bot layer
                    }
                }
                g.Dispose();
            }
        }

        private void ToolTip_Popup(object sender, PopupEventArgs e)
        {
            ToolTip tp = sender as ToolTip;
            Size sz = e.ToolTipSize;
            string[] title = tp.GetToolTip(e.AssociatedControl).Split(new string[] { "\r\n" }, StringSplitOptions.None);
            if (title.Length == 3)
            {
                if (title[1] == "" & title[2] == "")
                {
                    sz.Width = 0;
                    sz.Height = 0;
                    e.ToolTipSize = sz;
                    return;
                }
            }
            if (title.Length > 1)
            {
                string[] it = new string[title.Length - 1];
                it[0] = title[0] + " " + title[1];
                for (int i = 1; i < it.Length; i++)
                {
                    it[i] = title[i + 1];
                }
                it = it.OrderBy(x => x.Length).ToArray();
                sz.Width = (it[it.Length - 1].Length * 5) + 15;
            }

            if (title.Length <= 3)
            {
                sz.Height = 36;
            }
            else
            {
                sz.Height = 14 * title.Length;
            }
            e.ToolTipSize = sz;
        }
    }
}