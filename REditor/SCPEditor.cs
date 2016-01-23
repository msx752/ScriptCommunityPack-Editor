using FarsiLibrary.Win;
using FastColoredTextBoxNS;
using FastColoredTextBoxNS.Render;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SphereScp
{
    public partial class MAIN : Form
    {
        private Color changedLineColor = Color.FromArgb(255, 230, 230, 255);
        private Color currentLineColor = Color.FromArgb(100, 210, 210, 255);
        private List<ExplorerItem> explorerList = new List<ExplorerItem>();
        private Style invisibleCharsStyle = new InvisibleCharsRenderer(Pens.Gray);
        private List<AutoCompleteItem> items = new List<AutoCompleteItem>();

        private DateTime lastNavigatedDateTime = DateTime.Now;

        private Computer myComputer = new Computer();

        private bool tbFindChanged = false;

        public MAIN()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            ScpIndexer.LoadScpCmd();
            ScriptCommunityPack.LoadKeywords();
        }

        private FastColoredTextBox CurrentTB
        {
            get
            {
                if (tsFiles.SelectedItem == null)
                    return null;
                return (tsFiles.SelectedItem.Controls[0] as FastColoredTextBox);
            }

            set
            {
                tsFiles.SelectedItem = (value.Parent as FATabStripItem);
                value.Focus();
            }
        }

        public void CreateTab(string fileName)
        {
            try
            {
                FastColoredTextBox tb = new FastColoredTextBox();
                tb.Font = new Font("Consolas", 9.75f);
                tb.ForeColor = Color.White;
                tb.BackColor = Color.Black;
                tb.ContextMenuStrip = cmMain;
                tb.Dock = DockStyle.Fill;
                tb.BorderStyle = BorderStyle.Fixed3D;
                tb.LeftPadding = 17;
                tb.Language = Language.Scp;
                tb.AddStyle(new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Gray))));
                FATabStripItem tab = new FATabStripItem(fileName != null ? Path.GetFileName(fileName) : "[new]", tb);
                tab.Tag = fileName;
                if (fileName != null)
                    tb.Text = File.ReadAllText(fileName, Encoding.Default);
                tb.ClearUndo();
                tb.Tag = new PopupMenu();
                tb.IsChanged = false;
                tab.Width = 250;
                tsFiles.AddTab(tab);
                tsFiles.SelectedItem = tab;
                tb.Focus();
                tb.DelayedTextChangedInterval = 2500;
                tb.DelayedEventsInterval = 500;
                tb.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(tb_TextChangedDelayed);
                tb.SelectionChangedDelayed += new EventHandler(tb_SelectionChangedDelayed);
                tb.ToolTipNeeded += tb_ToolTipNeeded;
                tb.KeyDown += new KeyEventHandler(tb_KeyDown);
                tb.MouseMove += new MouseEventHandler(tb_MouseMove);
                tb.ChangedLineColor = changedLineColor;
                tb.CurrentLineColor = currentLineColor;
                tb.HighlightingRangeType = HighlightingRangeType.VisibleRange;
                AutocompleteMenu _popupMenu = new AutocompleteMenu(tb);
                _popupMenu.Items.ImageList = ilAutocomplete;
                _popupMenu.Opening += new EventHandler<CancelEventArgs>(popupMenu_Opening);
                BuildAutocompleteMenu(_popupMenu);
                (tb.Tag as PopupMenu).popupMenu = _popupMenu;
                tb.ToolTip.OwnerDraw = true;
                tb.ToolTip.Draw += ToolTip_Draw;
                tb.ToolTip.Popup += ToolTip_Popup;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                    CreateTab(fileName);
            }
        }

        private void _sscripteditor_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            for (int i = 0; i < files.Length; i++)
            {
                if (File.Exists(files[i]))
                {
                    bool isOpened = false;
                    for (int i2 = 0; i2 < tsFiles.Items.Count; i2++)
                    {
                        if ((tsFiles.Items[i2] as FATabStripItem).Tag == null)
                            continue;

                        string currFile = (tsFiles.Items[i2] as FATabStripItem).Tag.ToString();

                        if (currFile == files[i])
                        {
                            isOpened = true;
                        }
                    }
                    if (!isOpened)
                        CreateTab(files[i]);
                }
            }
        }

        private void _sscripteditor_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void backStripButton_Click(object sender, EventArgs e)
        {
            NavigateBackward();
        }

        private void btHighlightCurrentLine_Click(object sender, EventArgs e)
        {
            foreach (FATabStripItem tab in tsFiles.Items)
            {
                if (true)//current line
                    (tab.Controls[0] as FastColoredTextBox).CurrentLineColor = currentLineColor;
                else
#pragma warning disable CS0162 // Unreachable code detected
                    (tab.Controls[0] as FastColoredTextBox).CurrentLineColor = Color.Transparent;
#pragma warning restore CS0162 // Unreachable code detected
            }
            if (CurrentTB != null)
                CurrentTB.Invalidate();
        }

        private void BuildAutocompleteMenu(AutocompleteMenu popupMenu)
        {
            if (items.Count == 0)
            {
                items.AddRange(ScriptCommunityPack.snippets);
                items.AddRange(ScriptCommunityPack.declaration);
                //items.AddRange(ScriptCommunityPack.methods);//not necessary
                items.AddRange(ScriptCommunityPack.keywords);
                items.AddRange(ScriptCommunityPack.fileCommands);//NEW

                items.Add(new InsertSpaceSnippet());
                items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
                items.Add(new InsertEnterSnippet());
            }
            else
            {
                //set as autocomplete source
                popupMenu.Items.SetAutocompleteItems(items);
                popupMenu.SearchPattern = @"[\w\.:=!<>]";
            }
        }

        private void cloneLinesAndCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //start autoUndo block
            CurrentTB.BeginAutoUndo();
            //expand selection
            CurrentTB.Selection.Expand();
            //get text of selected lines
            string text = Environment.NewLine + CurrentTB.Selection.Text;
            //comment lines
            CurrentTB.InsertLinePrefix("//");
            //move caret to end of selected lines
            CurrentTB.Selection.Start = CurrentTB.Selection.End;
            //insert text
            CurrentTB.InsertText(text);
            //end of autoUndo block
            CurrentTB.EndAutoUndo();
        }

        private void cloneLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //expand selection
            CurrentTB.Selection.Expand();
            //get text of selected lines
            string text = Environment.NewLine + CurrentTB.Selection.Text;
            //move caret to end of selected lines
            CurrentTB.Selection.Start = CurrentTB.Selection.End;
            //insert text
            CurrentTB.InsertText(text);
        }

        private void commentSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.InsertLinePrefix("//");
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(CurrentTB.SelectedText);
        }

        private void createDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void dgvObjectExplorer_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (CurrentTB != null)
            {
                var item = explorerList[e.RowIndex];
                CurrentTB.GoEnd();
                CurrentTB.SelectionStart = item.position;
                CurrentTB.DoSelectionVisible();
                CurrentTB.Focus();
            }
        }

        private void dgvObjectExplorer_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                ExplorerItem item = explorerList[e.RowIndex];
                if (e.ColumnIndex == 1)
                    e.Value = item.title;
                else
                    switch (item.type)
                    {
                        case ExplorerItemType.Class:
                            e.Value = global::SphereScp.Properties.Resources.class_libraries;
                            return;

                        case ExplorerItemType.Method:
                            e.Value = global::SphereScp.Properties.Resources.box;
                            return;

                        case ExplorerItemType.Event:
                            e.Value = global::SphereScp.Properties.Resources.lightning;
                            return;

                        case ExplorerItemType.Property:
                            e.Value = global::SphereScp.Properties.Resources.property;
                            return;
                    }
            }
            catch {; }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.ShowFindDialog();
        }

        private void forwardStripButton_Click(object sender, EventArgs e)
        {
            NavigateForward();
        }

        private void gotoButton_DropDownOpening(object sender, EventArgs e)
        {
        }

        private void HighlightInvisibleChars(Range range)
        {
            range.ClearStyle(invisibleCharsStyle);
            //if (btInvisibleChars.Checked)
            //    range.SetStyle(invisibleCharsStyle, @".$|.\r\n|\s");
        }

        private void loadDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private bool NavigateBackward()
        {
            DateTime max = new DateTime();
            int iLine = -1;
            FastColoredTextBox tb = null;
            for (int iTab = 0; iTab < tsFiles.Items.Count; iTab++)
            {
                var t = (tsFiles.Items[iTab].Controls[0] as FastColoredTextBox);
                for (int i = 0; i < t.LinesCount; i++)
                    if (t[i].LastVisit < lastNavigatedDateTime && t[i].LastVisit > max)
                    {
                        max = t[i].LastVisit;
                        iLine = i;
                        tb = t;
                    }
            }
            if (iLine >= 0)
            {
                tsFiles.SelectedItem = (tb.Parent as FATabStripItem);
                tb.Navigate(iLine);
                lastNavigatedDateTime = tb[iLine].LastVisit;
                Console.WriteLine("Backward: " + lastNavigatedDateTime);
                tb.Focus();
                tb.Invalidate();
                return true;
            }
            else
                return false;
        }

        private bool NavigateForward()
        {
            DateTime min = DateTime.Now;
            int iLine = -1;
            FastColoredTextBox tb = null;
            for (int iTab = 0; iTab < tsFiles.Items.Count; iTab++)
            {
                var t = (tsFiles.Items[iTab].Controls[0] as FastColoredTextBox);
                for (int i = 0; i < t.LinesCount; i++)
                    if (t[i].LastVisit > lastNavigatedDateTime && t[i].LastVisit < min)
                    {
                        min = t[i].LastVisit;
                        iLine = i;
                        tb = t;
                    }
            }
            if (iLine >= 0)
            {
                tsFiles.SelectedItem = (tb.Parent as FATabStripItem);
                tb.Navigate(iLine);
                lastNavigatedDateTime = tb[iLine].LastVisit;
                Console.WriteLine("Forward: " + lastNavigatedDateTime);
                tb.Focus();
                tb.Invalidate();
                return true;
            }
            else
                return false;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTab(null);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdMain.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                CreateTab(ofdMain.FileName);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.InsertText(Clipboard.GetText());
        }

        private void popupMenu_Opening(object sender, CancelEventArgs e)
        {
            //---block autocomplete menu for comments
            //get index of green style (used for comments)
            var iGreenStyle = CurrentTB.GetStyleIndex(CurrentTB.SyntaxHighlighter.GreenStyle);
            if (iGreenStyle >= 0)
                if (CurrentTB.Selection.Start.iChar > 0)
                {
                    //current char (before caret)
                    var c = CurrentTB[CurrentTB.Selection.Start.iLine][CurrentTB.Selection.Start.iChar - 1];
                    //green Style
                    var greenStyleIndex = Range.ToStyleIndex(iGreenStyle);
                    //if char contains green style then block popup menu
                    if ((c.style & greenStyleIndex) != 0)
                        e.Cancel = true;
                }
        }

        private void PowerfulCSharpEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<FATabStripItem> list = new List<FATabStripItem>();
            foreach (FATabStripItem tab in tsFiles.Items)
                list.Add(tab);
            foreach (var tab in list)
            {
                TabStripItemClosingEventArgs args = new TabStripItemClosingEventArgs(tab);
                tsFiles_TabStripItemClosing(args);
                if (args.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                tsFiles.RemoveTab(tab);
            }
        }

        private void PowerfulCSharpEditor_Load(object sender, EventArgs e)
        {
            //fixing
            CreateTab(null);
            this.tsFiles.Items.Clear();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ReBuildObjectExplorer(string text)
        {
            try
            {
                List<ExplorerItem> list = new List<ExplorerItem>();
                int lastClassIndex = -1;
                Regex regex = new Regex(@"(?<range>(\[\w+\s+\w+(\s+\w+)?\]))|(?<range>((\bon=@)([a-z]?)+))", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                foreach (Match r in regex.Matches(text))
                {
                    try
                    {
                        string s = r.Value;
                        s = s.Trim();
                        ExplorerItem item = new ExplorerItem() { title = s, position = r.Index };
                        if (item.title.ToLower().StartsWith("["))
                        {
                            string[] parts = item.title.Split(' ');
                            if (parts.Length <= 2)
                            {
                                string tit = parts[0].ToUpper() + " ";

                                for (int i = 1; i < parts.Length; i++)
                                    tit = tit + parts[i] + " ";

                                item.title = tit;
                                list.Sort(lastClassIndex + 1, list.Count - (lastClassIndex + 1), new ExplorerItemComparer());
                                lastClassIndex = list.Count;
                                if (parts[0].ToLower() == "[events")
                                    item.type = ExplorerItemType.Event;
                                else
                                    item.type = ExplorerItemType.Class;

                                list.Add(item);
                            }
                        }
                        else
                        {
                            if (item.title.ToLower().Contains("on=@"))
                            {
                                string[] parts = item.title.Split('@');
                                if (parts[1].Length > 0)
                                {
                                    item.title = parts[0].ToUpper() + "@" + parts[1];
                                    item.type = ExplorerItemType.Method;
                                    list.Add(item);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
                BeginInvoke(
                    new Action(() =>
                    {
                        explorerList = list;
                        dgvObjectExplorer.RowCount = explorerList.Count;
                        dgvObjectExplorer.Invalidate();
                    })
                );
            }
            catch (Exception e)
            {
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB.RedoEnabled)
                CurrentTB.Redo();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.ShowReplaceDialog();
        }

        private bool Save(FATabStripItem tab)
        {
            var tb = (tab.Controls[0] as FastColoredTextBox);
            if (tab.Tag == null)
            {
                if (sfdMain.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return false;
                tab.Title = Path.GetFileName(sfdMain.FileName);
                tab.Tag = sfdMain.FileName;
            }

            try
            {
                File.WriteAllText(tab.Tag as string, tb.Text, ASCIIEncoding.Default);
                tb.IsChanged = false;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    return Save(tab);
                else
                    return false;
            }

            tb.Invalidate();

            return true;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tsFiles.SelectedItem != null)
            {
                string oldFile = tsFiles.SelectedItem.Tag as string;
                tsFiles.SelectedItem.Tag = null;
                if (!Save(tsFiles.SelectedItem))
                    if (oldFile != null)
                    {
                        tsFiles.SelectedItem.Tag = oldFile;
                        tsFiles.SelectedItem.Title = Path.GetFileName(oldFile);
                    }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tsFiles.SelectedItem != null)
                Save(tsFiles.SelectedItem);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Selection.SelectAll();
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.OemMinus)
            {
                NavigateBackward();
                e.Handled = true;
            }

            if (e.Modifiers == (Keys.Control | Keys.Shift) && e.KeyCode == Keys.OemMinus)
            {
                NavigateForward();
                e.Handled = true;
            }

            if (e.KeyData == (Keys.K | Keys.Control))
            {
                //forced show (MinFragmentLength will be ignored)
                (CurrentTB.Tag as PopupMenu).popupMenu.Show(true);
                e.Handled = true;
            }
        }

        private void tb_MouseMove(object sender, MouseEventArgs e)
        {
            var tb = sender as FastColoredTextBox;
            var place = tb.PointToPlace(e.Location);
            var r = new Range(tb, place, place);

            string text = r.GetFragment("[a-zA-Z]").Text;
            lbWordUnderMouse.Text = text;
        }

        private void tb_SelectionChangedDelayed(object sender, EventArgs e)
        {
            var tb = sender as FastColoredTextBox;
            //remember last visit time

            if (tb.Selection.Start.iLine < tb.LinesCount)
            {
                if (lastNavigatedDateTime != tb[tb.Selection.Start.iLine].LastVisit)
                {
                    tb[tb.Selection.Start.iLine].LastVisit = DateTime.Now;
                    lastNavigatedDateTime = tb[tb.Selection.Start.iLine].LastVisit;
                }
            }

            //highlight same words
            tb.VisibleRange.ClearStyle(tb.Styles[0]);
            //if (!tb.Selection.IsEmpty)
            //    return;//user selected diapason
            //get fragment around caret
            var fragment = tb.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;
            //highlight same words
            Range[] ranges = tb.VisibleRange.GetRanges("\\b" + text + "\\b").ToArray();

            if (ranges.Length > 1)
                foreach (var r in ranges)
                    r.SetStyle(tb.Styles[0]);
        }

        private void tb_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((o) => ReBuildObjectExplorer((sender as FastColoredTextBox).Text));//rebuild object explorer
        }

        private void tb_ToolTipNeeded(object sender, FastColoredTextBox.ToolTipNeededEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.HoveredWord))
            {
                if (e.HoveredWord != null | e.HoveredWord != "")
                {
                    PopupToolTip keyw = ScriptCommunityPack.KeywordsInformation.Find(x => x.Name.ToLower() == e.HoveredWord.ToLower());
                    if (keyw != null)
                    {
                        e.ToolTipTitle = keyw.Name;

                        e.ToolTipText = keyw.ToString();
                    }
                }
            }
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && CurrentTB != null)
            {
                Range r = tbFindChanged ? CurrentTB.Range.Clone() : CurrentTB.Selection.Clone();
                tbFindChanged = false;
                r.End = new Place(CurrentTB[CurrentTB.LinesCount - 1].Count, CurrentTB.LinesCount - 1);
                var pattern = Regex.Escape(tbFind.Text);
                foreach (var found in r.GetRanges(pattern))
                {
                    found.Inverse();
                    CurrentTB.Selection = found;
                    CurrentTB.DoSelectionVisible();
                    return;
                }
                MessageBox.Show("Not found.");
            }
            else
                tbFindChanged = true;
        }

        private void tmUpdateInterface_Tick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentTB != null && tsFiles.Items.Count > 0)
                {
                    var tb = CurrentTB;
                    undoStripButton.Enabled = undoToolStripMenuItem.Enabled = tb.UndoEnabled;
                    redoStripButton.Enabled = redoToolStripMenuItem.Enabled = tb.RedoEnabled;
                    saveToolStripButton.Enabled = saveToolStripMenuItem.Enabled = tb.IsChanged;
                    saveAsToolStripMenuItem.Enabled = true;
                }
                else
                {
                    saveToolStripButton.Enabled = saveToolStripMenuItem.Enabled = false;
                    saveAsToolStripMenuItem.Enabled = false;

                    undoStripButton.Enabled = undoToolStripMenuItem.Enabled = false;
                    redoStripButton.Enabled = redoToolStripMenuItem.Enabled = false;
                    dgvObjectExplorer.RowCount = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            ToolTip tp = sender as ToolTip;

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
                Graphics g = e.Graphics;
                SolidBrush b = new SolidBrush(Color.FromArgb(240, 255, 255));
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
                    g.DrawString(texti[3], destinationFONT, Brushes.Blue, new PointF(rct.X + 4, rct.Y + 30)); // bot layer
                }
                else if (texti.Length > 4)
                {
                    for (int i = 3; i < texti.Length; i++)
                    {
                        if (i == texti.Length - 1)
                        {
                            texti[i] = texti[i];
                        }
                        g.DrawString(texti[i], destinationFONT, Brushes.Blue, new PointF(rct.X + 4, rct.Y + 30 + ((i - 3) * 15))); // bot layer
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

        private void tsFiles_TabStripItemClosing(TabStripItemClosingEventArgs e)
        {
            if ((e.Item.Controls[0] as FastColoredTextBox).IsChanged)
            {
                switch (MessageBox.Show("Do you want save " + e.Item.Title + " ?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        if (!Save(e.Item))
                            e.Cancel = true;
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void tsFiles_TabStripItemSelectionChanged(TabStripItemChangedEventArgs e)
        {
            if (CurrentTB != null)
            {
                CurrentTB.Focus();
                string text = CurrentTB.Text;
                ThreadPool.QueueUserWorkItem(
                    (o) => ReBuildObjectExplorer(text)
                );
            }
        }

        private void uncommentSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.RemoveLinePrefix("//");
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB.UndoEnabled)
                CurrentTB.Undo();
        }
        
        private void loadScpIndexIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("disabled[not implemented]: primarily, you need update keyword information about local script command");
            return;
            ScpIndexer.LoadScpCmd();
        }
    }
}