using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Linq;
using FastColoredTextBoxNS.Render;
using SphereScp;
using System.Globalization;

namespace FastColoredTextBoxNS
{
    [System.ComponentModel.ToolboxItem(false)]
    public class AutocompleteListView : UserControl
    {
        internal ToolTip toolTip = new ToolTip();

        internal List<AutoCompleteItem> visibleItems;

        int focussedItemIndex = 0;

        int hoveredItemIndex = -1;

        int oldItemCount = 0;

        List<AutoCompleteItem> sourceItems = new List<AutoCompleteItem>();

        FastColoredTextBox tb;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        internal AutocompleteListView(FastColoredTextBox _tb)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            base.Font = new Font(FontFamily.GenericSansSerif, 9);
            visibleItems = new List<AutoCompleteItem>();
            VerticalScroll.SmallChange = ItemHeight;
            MaximumSize = new Size(Size.Width, 180);
            toolTip.ShowAlways = false;
            AppearInterval = 500;
            timer.Tick += new EventHandler(timer_Tick);
            SelectedColor = Color.Orange;
            HoveredColor = Color.Red;
            ToolTipDuration = 3000;
            toolTip.OwnerDraw = true;
            toolTip.Draw += ToolTip_Draw;
            toolTip.Popup += ToolTip_Popup;
            tb = _tb;

            tb.KeyDown += new KeyEventHandler(tb_KeyDown);
            tb.SelectionChanged += new EventHandler(tb_SelectionChanged);
            tb.KeyPressed += new KeyPressEventHandler(tb_KeyPressed);

            Form form = tb.FindForm();
            if (form != null)
            {
                form.LocationChanged += (o, e) => Menu.Close();
                form.ResizeBegin += (o, e) => Menu.Close();
                form.FormClosing += (o, e) => Menu.Close();
                form.LostFocus += (o, e) => Menu.Close();
            }

            tb.LostFocus += (o, e) =>
            {
                if (!Menu.Focused) Menu.Close();
            };

            tb.Scroll += (o, e) => Menu.Close();

            VisibleChanged += (o, e) =>
            {
                if (Visible)
                    DoSelectedVisible();
            };
        }

        public event EventHandler FocussedItemIndexChanged;
        public int Count
        {
            get { return visibleItems.Count; }
        }

        public AutoCompleteItem FocussedItem
        {
            get
            {
                if (FocussedItemIndex >= 0 && focussedItemIndex < visibleItems.Count)
                    return visibleItems[focussedItemIndex];
                return null;
            }
            set
            {
                FocussedItemIndex = visibleItems.IndexOf(value);
            }
        }

        public int FocussedItemIndex
        {
            get { return focussedItemIndex; }
            set
            {
                if (focussedItemIndex != value)
                {
                    focussedItemIndex = value;
                    if (FocussedItemIndexChanged != null)
                        FocussedItemIndexChanged(this, EventArgs.Empty);
                }
            }
        }

        public Color HoveredColor { get; set; }

        public ImageList ImageList { get; set; }

        public Color SelectedColor { get; set; }

        internal bool AllowTabKey { get; set; }

        internal int AppearInterval { get { return timer.Interval; } set { timer.Interval = value; } }

        internal int ToolTipDuration { get; set; }

        private int ItemHeight
        {
            get { return Font.Height + 2; }
        }

        AutocompleteMenu Menu { get { return Parent as AutocompleteMenu; } }
        public void SelectNext(int shift)
        {
            FocussedItemIndex = Math.Max(0, Math.Min(FocussedItemIndex + shift, visibleItems.Count - 1));
            DoSelectedVisible();
            //
            Invalidate();
        }

        public void SetAutocompleteItems(ICollection<string> items)
        {
            List<AutoCompleteItem> list = new List<AutoCompleteItem>(items.Count);
            foreach (var item in items)
                list.Add(new AutoCompleteItem(item));
            SetAutocompleteItems(list);
        }

        public void SetAutocompleteItems(List<AutoCompleteItem> items)
        {
            sourceItems = items;
        }

        internal void DoAutocomplete()
        {
            DoAutocomplete(false);
        }
        /// <summary>
        /// WARNING ####### THIS METHOD WORKING ONLY LANGUAGE.SCP TYPE
        /// </summary>
        /// <param name="forced"></param>
        internal void DoAutocomplete(bool forced)
        {
            if (!Menu.Enabled)
            {
                Menu.Close();
            }
            else
            {
                visibleItems.Clear();
                FocussedItemIndex = 0;
                base.VerticalScroll.Value = 0;
                Range fragment = tb.Selection.GetFragment(Menu.SearchPattern);
                string text = fragment.Text;
                Point position = tb.PlaceToPoint(fragment.End);
                position.Offset(2, tb.CharHeight);
                List<AutoCompleteItem> finded = new List<AutoCompleteItem>();
                if (forced || (((text.Length >= Menu.MinFragmentLength) && (tb.Selection.Start == tb.Selection.End))) || text.EndsWith("<"))
                {
                    Menu.Fragment = fragment;

                    if (text.Length == 0)
                        return;
                    bool flag = false;
                    sourceItems
                        .ForEach(val => val.Parent = Menu);//all member sets parent
                                                           ///NEED CHANGES THAT CODES I KNOW. BUT NOW OKAY
                    CultureInfo eng = new CultureInfo("en-US");
                    if (tb.GetLineText(Menu.Fragment.Start.iLine).Replace(" ", "").StartsWith("[") == false)
                    {
                        string[] splited = text.ToLower().Replace("ı", "i").Replace(" ", "").Split('.');//for sourcetree (notimplemented)

                        if (splited.Length == 1
                            && (text.StartsWith("i_", true, eng)
                            || text.StartsWith("c_", true, eng)
                            || text.StartsWith("d_", true, eng)
                            || text.StartsWith("e_", true, eng)
                            || text.StartsWith("f_", true, eng)))
                        {
                            CmdDefType typ = CmdDefType.NONE;
                            if (splited[0].StartsWith("i_", true, eng))
                                typ = CmdDefType.ITEMDEF;
                            else if (splited[0].StartsWith("c_", true, eng))
                                typ = CmdDefType.CHARDEF;
                            else if (splited[0].StartsWith("d_", true, eng))
                                typ = CmdDefType.DIALOG;
                            else if (splited[0].StartsWith("e_", true, eng))
                                typ = CmdDefType.EVENTS;
                            else if (splited[0].StartsWith("f_", true, eng))
                                typ = CmdDefType.FUNCTION;

                            //listing which are all
                            finded = sourceItems
                               .Where(p => p.ToolTipTitle != null).ToList()
                               .Where(p => p.ToolTipTitle.IndexOf("[" + typ.ToString()) != -1)
                               .Where(p => p.Text.IndexOf("_") != -1)
                               .ToList();
                        }
                        else if (splited.Length == 1
                            && text.StartsWith("on=", true, eng))
                        {
                            finded = sourceItems
                                  .Where(p => p.Text.StartsWith("@", true, eng))
                                  .ToList();
                        }
                        else if (splited.Length == 1
                           && text.StartsWith("<", true, eng))
                        {
                            finded = sourceItems
                                  .Where(p => p.Text.StartsWith(splited[0].Replace(">", ""), true, eng))
                                  .ToList();
                        }
                        else if (splited.Length == 1
                          && (text.StartsWith("if", true, eng) || text.StartsWith("str", true, eng) || text.StartsWith("do", true, eng) || text.StartsWith("be", true, eng) || text.StartsWith("wh", true, eng) || text.StartsWith("for", true, eng)))
                        {
                            finded = sourceItems
                                  .Where(p => (p is SnippetAuto) && (p as SnippetAuto).Text.StartsWith(splited[0], true, eng))
                                  .ToList();
                        }
                        else
                        {
                            //listing which are all
                            finded = sourceItems
                               .Where(p => p.ToolTipTitle != null).ToList()
                               .Where(p => p.Text.StartsWith(splited[splited.Length - 1], true, eng))
                               .ToList();
                        }
                    }
                    else if (text.IndexOf(".") == -1)//defane is cannot using DOT
                    {
                        //listing which are all
                        finded = sourceItems
                           .Where(p => p.Text.ToLower()
                           .Replace("ı", "i")//turkish language fix
                           .IndexOf("[" + text.ToLower()) != -1)
                           .ToList();
                    }

                    if (finded.Count > 0)
                    {
                        finded = finded.OrderBy(p => p.Text)
                        .Where(p => !(p is InsertEnterSnippet) && !(p is InsertSpaceSnippet))
                        .ToList();

                        while (finded.Count >= 31)//max listed func
                            finded.RemoveAt(30);
                        visibleItems.AddRange(finded);

                        flag = true;
                        FocussedItemIndex = 0;
                    }
                    if (flag)
                    {
                        AdjustScroll();
                        DoSelectedVisible();
                    }
                }
                if (Count > 0)
                {
                    if (!Menu.Visible)
                    {
                        CancelEventArgs args = new CancelEventArgs();
                        Menu.OnOpening(args);
                        if (!args.Cancel)
                        {
                            Menu.Show(tb, position);
                        }
                    }
                    else
                    {
                        base.Invalidate();
                    }
                }
                else
                {
                    Menu.Close();
                }
            }
        }


        internal virtual void OnSelecting()
        {
            if (FocussedItemIndex < 0 || FocussedItemIndex >= visibleItems.Count)
                return;

            tb.TextSource.Manager.BeginAutoUndoCommands();
            try
            {
                AutoCompleteItem item = FocussedItem;
                SelectingEventArgs args = new SelectingEventArgs()
                {
                    Item = item,
                    SelectedIndex = FocussedItemIndex
                };

                Menu.OnSelecting(args);

                if (args.Cancel)
                {
                    FocussedItemIndex = args.SelectedIndex;
                    Invalidate();
                    return;
                }

                if (!args.Handled)
                {
                    var fragment = Menu.Fragment;
                    DoAutocomplete(item, fragment);
                }

                Menu.Close();
                //
                SelectedEventArgs args2 = new SelectedEventArgs()
                {
                    Item = item,
                    Tb = Menu.Fragment.tb
                };
                item.OnSelected(Menu, args2);
                Menu.OnSelected(args2);
            }
            finally
            {
                tb.TextSource.Manager.EndAutoUndoCommands();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                FocussedItemIndex = PointToItemIndex(e.Location);
                DoSelectedVisible();
                Invalidate();
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            FocussedItemIndex = PointToItemIndex(e.Location);
            Invalidate();
            OnSelecting();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            AdjustScroll();

            var itemHeight = ItemHeight;
            int startI = VerticalScroll.Value / itemHeight - 1;
            int finishI = (VerticalScroll.Value + ClientSize.Height) / itemHeight + 1;
            startI = Math.Max(startI, 0);
            finishI = Math.Min(finishI, visibleItems.Count);
            int y = 0;
            int leftPadding = 18;
            for (int i = startI; i < finishI; i++)
            {
                y = i * itemHeight - VerticalScroll.Value;

                var item = visibleItems[i];

                if (item.BackColor != Color.Transparent)
                    using (var brush = new SolidBrush(item.BackColor))
                        e.Graphics.FillRectangle(brush, 1, y, ClientSize.Width - 1 - 1, itemHeight - 1);

                if (ImageList != null && visibleItems[i].ImageIndex >= 0)
                    e.Graphics.DrawImage(ImageList.Images[item.ImageIndex], 1, y);

                if (i == FocussedItemIndex)
                    using (var selectedBrush = new LinearGradientBrush(new Point(0, y - 3), new Point(0, y + itemHeight), Color.Transparent, SelectedColor))
                    using (var pen = new Pen(SelectedColor))
                    {
                        e.Graphics.FillRectangle(selectedBrush, leftPadding, y, ClientSize.Width - 1 - leftPadding, itemHeight - 1);
                        e.Graphics.DrawRectangle(pen, leftPadding, y, ClientSize.Width - 1 - leftPadding, itemHeight - 1);
                    }

                if (i == hoveredItemIndex)
                    using (var pen = new Pen(HoveredColor))
                        e.Graphics.DrawRectangle(pen, leftPadding, y, ClientSize.Width - 1 - leftPadding, itemHeight - 1);

                using (var brush = new SolidBrush(item.ForeColor != Color.Transparent ? item.ForeColor : ForeColor))
                    e.Graphics.DrawString(item.ToString(), Font, brush, leftPadding, y);
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            ProcessKey(keyData, Keys.None);

            return base.ProcessCmdKey(ref msg, keyData);
        }

        void AdjustScroll()
        {
            if (oldItemCount == visibleItems.Count)
                return;

            int needHeight = ItemHeight * visibleItems.Count + 1;
            Height = Math.Min(needHeight, MaximumSize.Height);
            Menu.CalcSize();

            AutoScrollMinSize = new Size(0, needHeight);
            oldItemCount = visibleItems.Count;
        }

        private void DoAutocomplete(AutoCompleteItem item, Range fragment)
        {
            if (onlyShowToopTip)//ctrl+space onlyshow command
            {
                onlyShowToopTip = false;
                return;
            }

            string newText = item.GetTextForReplace();
            //replace text of fragment
            var tb = fragment.tb;
            tb.BeginAutoUndo();
            tb.TextSource.Manager.ExecuteCommand(new SelectCommand(tb.TextSource));
            if (tb.Selection.ColumnSelectionMode)
            {
                var start = tb.Selection.Start;
                var end = tb.Selection.End;
                start.iChar = fragment.Start.iChar;
                end.iChar = fragment.End.iChar;
                tb.Selection.Start = start;
                tb.Selection.End = end;
            }
            else
            {
                if (newText.StartsWith("["))//defname auto control
                    tb.Selection.Start = new Place(fragment.Start.iChar - 1, fragment.Start.iLine);
                else if (ScriptCommunityPack.keywordsInformation.FindIndex(p => p.Name == newText && p.Properties.Contains(PropertyTypes.SnippetAuto)) != -1)//snippet auto control
                    tb.Selection.Start = fragment.Start;
                else
                    tb.Selection.Start = new Place(tb.Selection.Start.iChar - 1, tb.Selection.Start.iLine);//KEYWORD auto CONTROL

                tb.Selection.End = fragment.End;
                if (newText.ToLower().StartsWith(".@"))//.On=@ (DOT bug fixed) || TRİGGER AUTO CONTROL
                    newText = newText.Replace(".@", "=@");
            }
            tb.InsertText(newText);
            tb.TextSource.Manager.ExecuteCommand(new SelectCommand(tb.TextSource));
            tb.EndAutoUndo();
            tb.Focus();
        }

        private void DoSelectedVisible()
        {
            if (FocussedItem != null)
                SetToolTip(FocussedItem);

            var y = FocussedItemIndex * ItemHeight - VerticalScroll.Value;
            if (y < 0)
                VerticalScroll.Value = FocussedItemIndex * ItemHeight;
            if (y > ClientSize.Height - ItemHeight)
                VerticalScroll.Value = Math.Min(VerticalScroll.Maximum, FocussedItemIndex * ItemHeight - ClientSize.Height + ItemHeight);
            //some magic for update scrolls
            AutoScrollMinSize -= new Size(1, 0);
            AutoScrollMinSize += new Size(1, 0);
        }

        int PointToItemIndex(Point p)
        {
            return (p.Y + VerticalScroll.Value) / ItemHeight;
        }

        private bool ProcessKey(Keys keyData, Keys keyModifiers)
        {
            if (keyModifiers == Keys.None)
                switch (keyData)
                {
                    case Keys.Down:
                        SelectNext(+1);
                        return true;
                    case Keys.PageDown:
                        SelectNext(+10);
                        return true;
                    case Keys.Up:
                        SelectNext(-1);
                        return true;
                    case Keys.PageUp:
                        SelectNext(-10);
                        return true;
                    case Keys.Enter:
                        OnSelecting();
                        return true;
                    case Keys.Tab:
                        if (!AllowTabKey)
                            break;
                        OnSelecting();
                        return true;
                    case Keys.Escape:
                        Menu.Close();
                        return true;
                }

            return false;
        }

        void ResetTimer(System.Windows.Forms.Timer timer)
        {
            timer.Stop();
            timer.Start();
        }

        private void SetToolTip(AutoCompleteItem autocompleteItem)
        {
            string toolTipTitle = autocompleteItem.ToolTipTitle;
            string toolTipText = autocompleteItem.ToolTipText;
            if (string.IsNullOrEmpty(toolTipTitle))
            {
                toolTip.ToolTipTitle = null;
                toolTip.SetToolTip(this, null);
            }
            else if (string.IsNullOrEmpty(toolTipText))
            {
                toolTip.ToolTipTitle = null;
                toolTip.Show(toolTipTitle, this, base.Width + 3, 0, ToolTipDuration);
            }
            else
            {
                toolTip.ToolTipTitle = toolTipTitle;
                toolTip.Show(toolTipText, this, base.Width + 3, 0, ToolTipDuration);
            }
        }
        bool onlyShowToopTip = false;
        void tb_KeyDown(object sender, KeyEventArgs e)
        {
            var tb = sender as FastColoredTextBox;

            if (Menu.Visible)
                if (ProcessKey(e.KeyCode, e.Modifiers))
                    e.Handled = true;

            if (!Menu.Visible)
            {
                if (tb.HotkeysMapping.ContainsKey(e.KeyData) && tb.HotkeysMapping[e.KeyData] == FCTBAction.AutocompleteMenu)
                {
                    onlyShowToopTip = true;
                       DoAutocomplete();
                    e.Handled = true;
                }
                else
                {
                    if (e.KeyCode == Keys.Escape && timer.Enabled)
                        timer.Stop();
                }
            }
        }

        void tb_KeyPressed(object sender, KeyPressEventArgs e)
        {
            bool backspaceORdel = e.KeyChar == '\b' || e.KeyChar == 0xff;

            /*
            if (backspaceORdel)
                prevSelection = tb.Selection.Start;*/

            if (Menu.Visible && !backspaceORdel)
                DoAutocomplete(false);
            else
                ResetTimer(timer);
        }

        void tb_SelectionChanged(object sender, EventArgs e)
        {
            /*
            FastColoredTextBox tb = sender as FastColoredTextBox;
            
            if (Math.Abs(prevSelection.iChar - tb.Selection.Start.iChar) > 1 ||
                        prevSelection.iLine != tb.Selection.Start.iLine)
                Menu.Close();
            prevSelection = tb.Selection.Start;*/
            if (Menu.Visible)
            {
                bool needClose = false;

                if (!tb.Selection.IsEmpty)
                    needClose = true;
                else
                    if (!Menu.Fragment.Contains(tb.Selection.Start))
                {
                    if (tb.Selection.Start.iLine == Menu.Fragment.End.iLine && tb.Selection.Start.iChar == Menu.Fragment.End.iChar + 1)
                    {
                        //user press key at end of fragment
                        char c = tb.Selection.CharBeforeStart;
                        if (!Regex.IsMatch(c.ToString(), Menu.SearchPattern))//check char
                            needClose = true;
                    }
                    else
                        needClose = true;
                }

                if (needClose)
                    Menu.Close();
            }

        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            DoAutocomplete(false);
        }
        private void ToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            ScriptCommunityPack.ToolTip_Draw(sender, e);
        }

        private void ToolTip_Popup(object sender, PopupEventArgs e)
        {
       ScriptCommunityPack.ToolTip_Popup(sender,e);
        }
    }

    /// <summary>
    /// Popup menu for autocomplete
    /// </summary>
    [Browsable(false)]
    public class AutocompleteMenu : ToolStripDropDown
    {
        public ToolStripControlHost host;
        AutocompleteListView listView;
        public AutocompleteMenu(FastColoredTextBox tb)
        {
            // create a new popup and add the list view to it 
            AutoClose = false;
            AutoSize = false;
            Margin = Padding.Empty;
            Padding = Padding.Empty;
            BackColor = Color.White;
            listView = new AutocompleteListView(tb);
            host = new ToolStripControlHost(listView);
            host.Margin = new Padding(2, 2, 2, 2);
            host.Padding = Padding.Empty;
            host.AutoSize = false;
            host.AutoToolTip = false;
            CalcSize();
            base.Items.Add(host);
            listView.Parent = this;
            SearchPattern = @"[\w\.]";
            MinFragmentLength = 2;

        }

        /// <summary>
        /// Occurs when popup menu is opening
        /// </summary>
        public new event EventHandler<CancelEventArgs> Opening;

        /// <summary>
        /// It fires after item inserting
        /// </summary>
        public event EventHandler<SelectedEventArgs> Selected;

        /// <summary>
        /// User selects item
        /// </summary>
        public event EventHandler<SelectingEventArgs> Selecting;

        /// <summary>
        /// Allow TAB for select menu item
        /// </summary>
        public bool AllowTabKey { get { return listView.AllowTabKey; } set { listView.AllowTabKey = value; } }

        /// <summary>
        /// Interval of menu appear (ms)
        /// </summary>
        public int AppearInterval { get { return listView.AppearInterval; } set { listView.AppearInterval = value; } }

        public new Font Font
        {
            get { return listView.Font; }
            set { listView.Font = value; }
        }

        public Range Fragment { get; internal set; }

        /// <summary>
        /// Border color of hovered item
        /// </summary>
        [DefaultValue(typeof(Color), "Red")]
        public Color HoveredColor
        {
            get { return listView.HoveredColor; }
            set { listView.HoveredColor = value; }
        }

        /// <summary>
        /// Image list of menu
        /// </summary>
        public new ImageList ImageList
        {
            get { return Items.ImageList; }
            set { Items.ImageList = value; }
        }

        public new AutocompleteListView Items
        {
            get { return listView; }
        }

        /// <summary>
        /// Minimum fragment length for popup
        /// </summary>
        public int MinFragmentLength { get; set; }

        /// <summary>
        /// Minimal size of menu
        /// </summary>
        public new Size MinimumSize
        {
            get { return Items.MinimumSize; }
            set { Items.MinimumSize = value; }
        }

        /// <summary>
        /// Regex pattern for serach fragment around caret
        /// </summary>
        public string SearchPattern { get; set; }
        /// <summary>
        /// Back color of selected item
        /// </summary>
        [DefaultValue(typeof(Color), "Orange")]
        public Color SelectedColor
        {
            get { return listView.SelectedColor; }
            set { listView.SelectedColor = value; }
        }
        /// <summary>
        /// Tooltip
        /// </summary>
        public ToolTip ToolTip
        {
            get { return Items.toolTip; }
            set { Items.toolTip = value; }
        }

        /// <summary>
        /// Tooltip duration (ms)
        /// </summary>
        public int ToolTipDuration
        {
            get { return Items.ToolTipDuration; }
            set { Items.ToolTipDuration = value; }
        }

        public new void Close()
        {
            listView.toolTip.Hide(listView);
            base.Close();
        }

        public void OnSelected(SelectedEventArgs args)
        {
            if (Selected != null)
                Selected(this, args);
        }

        public virtual void OnSelecting()
        {
            listView.OnSelecting();
        }

        public void SelectNext(int shift)
        {
            listView.SelectNext(shift);
        }

        /// <summary>
        /// Shows popup menu immediately
        /// </summary>
        /// <param name="forced">If True - MinFragmentLength will be ignored</param>
        public void Show(bool forced)
        {
            Items.DoAutocomplete(forced);
        }

        internal void CalcSize()
        {
            host.Size = listView.Size;
            Size = new System.Drawing.Size(listView.Size.Width + 4, listView.Size.Height + 4);
        }

        new internal void OnOpening(CancelEventArgs args)
        {
            if (Opening != null)
                Opening(this, args);
        }
        internal void OnSelecting(SelectingEventArgs args)
        {
            if (Selecting != null)
                Selecting(this, args);
        }
    }
    public class SelectedEventArgs : EventArgs
    {
        public AutoCompleteItem Item { get; internal set; }
        public FastColoredTextBox Tb { get; set; }
    }

    public class SelectingEventArgs : EventArgs
    {
        public bool Cancel { get; set; }

        public bool Handled { get; set; }

        public AutoCompleteItem Item { get; internal set; }
        public int SelectedIndex { get; set; }
    }
}
