using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarsiLibrary.Win;
using FastColoredTextBoxNS;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Drawing.Drawing2D;
using FastColoredTextBoxNS.Render;

namespace SphereScp
{
    public partial class MAIN : Form
    {
        public static Language currentLang = Language.Scp;
        Color changedLineColor = Color.FromArgb(255, 230, 230, 255);
        Color currentLineColor = Color.FromArgb(215, 0, 0, 0);


        //List<ExplorerItem> explorerList = new List<ExplorerItem>();
        Style invisibleCharsStyle = new InvisibleCharsRenderer(Pens.Gray);
        DateTime lastNavigatedDateTime = DateTime.Now;
        MarkerStyle sameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Gray)));
        bool tbFindChanged = false;

        public MAIN()
        {
            InitializeComponent();
            //init menu images
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MAIN));
            copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
            cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
            //ScpIndexer.LoadScpCmd();
            //ScriptCommunityPack.LoadKeywords();
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

        public void RefreshObjectExplorer(FastColoredTextBox ftp)
        {
            ThreadPool.QueueUserWorkItem((o) => ReBuildObjectExplorer(ftp.Text));//rebuild object explorer
        }

        private void autoIndentSelectedTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.DoAutoIndent();
        }

        private void backStripButton_Click(object sender, EventArgs e)
        {
            NavigateBackward();
        }

        private void bookmarkMinusButton_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
                return;
            CurrentTB.UnbookmarkLine(CurrentTB.Selection.Start.iLine);
        }

        private void bookmarkPlusButton_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
                return;
            CurrentTB.BookmarkLine(CurrentTB.Selection.Start.iLine);
        }

        private void btHighlightCurrentLine_Click(object sender, EventArgs e)
        {
            foreach (FATabStripItem tab in tsFiles.Items)
            {
                if (btHighlightCurrentLine.Checked)
                    (tab.Controls[0] as FastColoredTextBox).CurrentLineColor = currentLineColor;
                else
                    (tab.Controls[0] as FastColoredTextBox).CurrentLineColor = Color.Transparent;
            }
            if (CurrentTB != null)
                CurrentTB.Invalidate();
        }

        private void btInvisibleChars_Click(object sender, EventArgs e)
        {
            foreach (FATabStripItem tab in tsFiles.Items)
                HighlightInvisibleChars((tab.Controls[0] as FastColoredTextBox).Range);
            if (CurrentTB != null)
                CurrentTB.Invalidate();
        }

        private void btShowFoldingLines_Click(object sender, EventArgs e)
        {
            foreach (FATabStripItem tab in tsFiles.Items)
                (tab.Controls[0] as FastColoredTextBox).ShowFoldingLines = btShowFoldingLines.Checked;
            if (CurrentTB != null)
                CurrentTB.Invalidate();
        }

        private void BuildAutocompleteMenu(AutocompleteMenu popupMenu)
        {
            List<AutoCompleteItem> items = new List<AutoCompleteItem>();
            items.AddRange(ScriptCommunityPack.loadKwCommand<MethodAuto>(PropertyTypes.TriggerAuto));//improved
            items.AddRange(ScriptCommunityPack.loadKwCommand<DeclarationAuto>(PropertyTypes.DeclarationAuto));//improved
            items.AddRange(ScriptCommunityPack.loadKwCommand<SnippetAuto>(PropertyTypes.SnippetAuto));//improved
            items.AddRange(ScriptCommunityPack.loadKwCommand<MethodAuto>(true, PropertyTypes.SnippetAuto, PropertyTypes.DeclarationAuto, PropertyTypes.TriggerAuto));//improved
            items.AddRange(ScriptCommunityPack.fileScpCommands);//NEW scp commands

            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            //set as autocomplete source
            popupMenu.AllowTabKey = true;
            popupMenu.Items.SetAutocompleteItems(items);
            popupMenu.SearchPattern = @"[\w\.@:=!<>]";
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
            CurrentTB.Copy();
        }

        private void CreateTab(string fileName)
        {
            if (ScriptCommunityPack.keywordsInformation.Count == 0)
            {
                MessageBox.Show("primarily load keywords");
                return;
            }
            if (currentLang != Language.Scp)
            {
                MessageBox.Show("this editor working with Language.Scp format. please dont change it");
                return;
            }
            try
            {
                FastColoredTextBox tb = new FastColoredTextBox();
                tb.AutoScroll = true;
                tb.BorderStyle = BorderStyle.None;
                tb.Font = new Font("Consolas", 9.75f);
                tb.BackColor = Color.FromArgb(16,12,16);
                tb.ForeColor = Color.White;
                tb.LineNumberColor = Color.Gray;
                tb.IndentBackColor = tb.BackColor;
                tb.ContextMenuStrip = cmMain;
                tb.Dock = DockStyle.Fill;
                tb.BorderStyle = BorderStyle.Fixed3D;
                tb.LeftPadding = 5;
                tb.Language = currentLang;
                tb.AddStyle(new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Gray))));//same words style
                FATabStripItem tab = new FATabStripItem(fileName != null ? Path.GetFileName(fileName) : "[new]", tb);
                tab.ForeColor = Color.Black;
                tab.Tag = fileName;
                if (fileName != null)
                    tb.OpenFile(fileName);
                tb.Name = Path.GetFileName(fileName);
                tb.ClearUndo();
                tb.Tag = new PopupMenu();
                tsFiles.AddTab(tab);
                tsFiles.TabStripItemSelectionChanged += TsFiles_TabStripItemSelectionChanged;
                tsFiles.SelectedItem = tab;
                tsFiles.ForeColor = Color.Black;
                tb.Focus();
                tb.DelayedTextChangedInterval = 1;
                tb.DelayedEventsInterval = 500;
                tb.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(tb_TextChangedDelayed);
                tb.SelectionChangedDelayed += new EventHandler(tb_SelectionChangedDelayed);
                tb.ToolTipNeeded += tb_ToolTipNeeded;
                tb.KeyDown += new KeyEventHandler(tb_KeyDown);
                tb.MouseMove += new MouseEventHandler(tb_MouseMove);
                tb.ChangedLineColor = changedLineColor;
                if (btHighlightCurrentLine.Checked)
                    tb.CurrentLineColor = currentLineColor;
                tb.ShowFoldingLines = btShowFoldingLines.Checked;
                tb.HighlightingRangeType = HighlightingRangeType.VisibleRange;
                AutocompleteMenu popupMenu = new AutocompleteMenu(tb);
                popupMenu.Items.ImageList = ilAutocomplete;
                popupMenu.Opening += new EventHandler<CancelEventArgs>(popupMenu_Opening);
                BuildAutocompleteMenu(popupMenu);
                (tb.Tag as PopupMenu).popupMenu = popupMenu;
                tb.ToolTip.OwnerDraw = true;
                tb.ToolTip.Draw += ToolTip_Draw;
                tb.ToolTip.Popup += ToolTip_Popup;
                documentMap1.Target = tb;
                documentMap1.BackColor = tb.BackColor;
                tsFiles.TabStripItemClosed += TsFiles_TabStripItemClosed;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                    CreateTab(fileName);
            }
        }

        private void TsFiles_TabStripItemClosed(object sender, EventArgs e)
        {
            if (tsFiles.Controls.Count == 0)
            {
                ExpandedList.Clear();
                treeView3.Nodes.Clear();
                documentMap1.Target = null;
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Cut();
        }

        private void exportHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToHTML export = new ExportToHTML();
            string val = export.GetHtml(CurrentTB);
            val = val.Replace(".fctbNone{", "body{background-color:" + CurrentTB.BackColor.Name + ";}.fctbNone{");
            val = val.Replace("font-size: " + CurrentTB.Font.Size + "pt;", "font-size: 9pt;");
            StreamWriter sw = new StreamWriter(Path.Combine(Application.StartupPath, CurrentTB.Name + ".html"), false, Encoding.UTF8);
            sw.Write(val);
            sw.Close();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.ShowFindDialog();
        }

        //    }
        //}
        private void forwardStripButton_Click(object sender, EventArgs e)
        {
            NavigateForward();
        }
        private void gotoButton_DropDownOpening(object sender, EventArgs e)
        {
            gotoButton.DropDownItems.Clear();
            foreach (Control tab in tsFiles.Items)
            {
                FastColoredTextBox tb = tab.Controls[0] as FastColoredTextBox;
                foreach (var bookmark in tb.Bookmarks)
                {
                    var item = gotoButton.DropDownItems.Add(bookmark.Name + " [" + Path.GetFileNameWithoutExtension(tab.Tag as String) + "]");
                    item.Tag = bookmark;
                    item.Click += (o, a) =>
                    {
                        var b = (Bookmark)(o as ToolStripItem).Tag;
                        try
                        {
                            CurrentTB = b.TB;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                        b.DoVisible();
                    };
                }
            }
        }
        private void HighlightInvisibleChars(Range range)
        {
            range.ClearStyle(invisibleCharsStyle);
            if (btInvisibleChars.Checked)
                range.SetStyle(invisibleCharsStyle, @".$|.\r\n|\s");
        }

        private void loadKeywordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsFiles.Enabled = false;
            ScriptCommunityPack.LoadKeywords();
            tsFiles.Enabled = true;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ScriptCommunityPack.keywordsInformation.Count == 0)
            {
                MessageBox.Show("primarily load keywords");
                return;
            }
            tsFiles.Enabled = false;
            ScpIndexer.LoadScpCmd();
            tsFiles.Enabled = true;
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
            ofdMain.Filter = string.Format("FileType (*.{0} )|*.{0}", currentLang.ToString().ToLower());
            if (ofdMain.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                CreateTab(ofdMain.FileName);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Paste();
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

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (CurrentTB != null)
            {
                var settings = new PrintDialogSettings();
                settings.Title = tsFiles.SelectedItem.Title;
                settings.Header = "&b&w&b";
                settings.Footer = "&b&p";
                CurrentTB.Print(settings);
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ReBuildObjectExplorer(string text)
        {
            try
            {
                List<TreeNode> newNodes = new List<TreeNode>();
                Regex regex = new Regex(@"(?<range>(\[\w+\s+\w+(\s+\w+)?\]))|(?<range>((\bon=@)([a-z]?)+))|(((?:[a-z][a-z]+))(=)((?:[a-z][a-z0-9_]*)))", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                bool isDefineValue = true;
                TreeNode newNode = null;
                foreach (Match r in regex.Matches(text))
                {
                    try
                    {
                        string s = r.Value;
                        s = s.Trim();

                        if (s.ToLower().StartsWith("["))
                        {
                            string[] parts = s.Split(' ');
                            if (parts.Length <= 2)
                            {
                                isDefineValue = true;
                                string tit = parts[0].ToUpper().Replace("İ", "I") + " ";

                                for (int i = 1; i < parts.Length; i++)
                                    tit = tit + parts[i] + " ";
                                newNode = new TreeNode(tit) { ImageIndex = 0, Tag = r.Index };
                                newNodes.Add(newNode);
                            }
                        }
                        else
                        {
                            if (s.ToLower().Contains("on=@"))
                            {
                                string[] parts = s.Split('@');
                                if (parts[1].Length > 0)
                                {
                                    isDefineValue = false;
                                    TreeNode newTrigger = new TreeNode(parts[0].ToUpper() + "@ " + parts[1]) { ImageIndex = 7, Tag = r.Index };
                                    newNodes[newNodes.Count - 1].Nodes.Add(newTrigger);
                                }
                            }
                            else
                            {
                                string[] parts = s.Split('=');
                                if (parts.Length == 2 && isDefineValue)//isDefineValue is only define's propertie not trigger's 
                                {
                                    TreeNode newProperty = new TreeNode(parts[0].ToUpper() + "= " + parts[1]) { ImageIndex = 6, Tag = r.Index };
                                    newNodes[newNodes.Count - 1].Nodes.Add(newProperty);
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
                        if (treeView3.Nodes.Count == 0)
                            CurrentTB.DelayedTextChangedInterval = 2000;
                        treeView3.Nodes.Clear();
                        treeView3.Nodes.Add(new TreeNode("EXPAND ALL") { BackColor = Color.Black, ForeColor = Color.Red, ImageIndex = 4 });
                        treeView3.Nodes.AddRange(newNodes.ToArray());
                        for (int i = 0; i < ExpandedList.Count; i++)
                            treeView3.Nodes[ExpandedList[i]].Expand();
                    })
                );
            }
            catch (Exception e)
            {
            }
        }

        private void REditor_Load(object sender, EventArgs e)
        {
            treeView3.ImageList = ilAutocomplete;
            treeView3.BeforeSelect += TreeView3_BeforeSelect;
            treeView3.NodeMouseClick += TreeView1_NodeMouseClick;
            treeView3.NodeMouseDoubleClick += TreeView3_NodeMouseDoubleClick;
            treeView3.BeforeCollapse += TreeView3_BeforeCollapse;
            treeView3.BeforeExpand += TreeView3_BeforeExpand;
        }

        private void TreeView3_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (!ExpandedList.Contains(e.Node.Index))
                ExpandedList.Add(e.Node.Index);
        }

        bool canCancel = true;
        private void TreeView3_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (canCollapse == null)
                e.Cancel = canCancel;
            else if (canCollapse == e.Node)
                e.Cancel = false;

            int index = ExpandedList.FindIndex(p => p == e.Node.Index);
            if (index != -1)
                ExpandedList.RemoveAt(index);
        }

        private void TreeView3_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Action == TreeViewAction.Unknown)
                e.Cancel = canCancel;
            else if (e.Action == TreeViewAction.ByMouse)
                e.Cancel = canCancel;
        }
        TreeNode canCollapse;
        private void TreeView3_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Node == treeView3.Nodes[0])
                {
                    canCollapse = null;
                    canCancel = false;
                    if (e.Node.Text.ToLower() == "collapse all")
                    {
                        ExpandedList.Clear();
                        e.Node.SelectedImageIndex = 4;
                        treeView3.CollapseAll();
                        e.Node.Text = "EXPAND ALL";
                    }
                    else
                    {
                        e.Node.SelectedImageIndex = 5;
                        treeView3.ExpandAll();
                        e.Node.Text = "COLLAPSE ALL";
                    }
                }
                else
                {
                    canCollapse = e.Node;
                }
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
                File.WriteAllText(tab.Tag as string, tb.Text);
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
            if (tb.Selection.IsEmpty && tb.Selection.Start.iLine < tb.LinesCount)
            {
                if (lastNavigatedDateTime != tb[tb.Selection.Start.iLine].LastVisit)
                {
                    tb[tb.Selection.Start.iLine].LastVisit = DateTime.Now;
                    lastNavigatedDateTime = tb[tb.Selection.Start.iLine].LastVisit;
                }
            }

            //highlight same words
            tb.VisibleRange.ClearStyle(sameWordsStyle);
            if (!tb.Selection.IsEmpty)
                return;//user selected diapason
            //get fragment around caret
            var fragment = tb.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;
            //highlight same words
            Range[] ranges = tb.VisibleRange.GetRanges("\\b" + text + "\\b").ToArray();

            if (ranges.Length > 1)
                foreach (var r in ranges)
                    r.SetStyle(sameWordsStyle);
        }

        private void tb_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            RefreshObjectExplorer((sender as FastColoredTextBox));
            //show invisible chars
            HighlightInvisibleChars(e.ChangedRange);
        }

        private void tb_ToolTipNeeded(object sender, ToolTipNeededEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.HoveredWord))
            {
                if (e.HoveredWord != null | e.HoveredWord != "")
                {
                    PopupToolTip keyw = ScriptCommunityPack.keywordsInformation.Find(x => x.Name.ToLower().StartsWith(e.HoveredWord.ToLower()));
                    if (keyw != null)
                    {
                        e.ToolTipTitle = keyw.Name;
                        e.ToolTipText = keyw.ToString();
                    }
                    else
                    {
                        //scp command tooltip
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
                    pasteToolStripButton.Enabled = pasteToolStripMenuItem.Enabled = true;
                    cutToolStripButton.Enabled = cutToolStripMenuItem.Enabled =
                    copyToolStripButton.Enabled = copyToolStripMenuItem.Enabled = !tb.Selection.IsEmpty;
                    printToolStripButton.Enabled = true;
                }
                else
                {
                    saveToolStripButton.Enabled = saveToolStripMenuItem.Enabled = false;
                    saveAsToolStripMenuItem.Enabled = false;
                    cutToolStripButton.Enabled = cutToolStripMenuItem.Enabled =
                    copyToolStripButton.Enabled = copyToolStripMenuItem.Enabled = false;
                    pasteToolStripButton.Enabled = pasteToolStripMenuItem.Enabled = false;
                    printToolStripButton.Enabled = false;
                    undoStripButton.Enabled = undoToolStripMenuItem.Enabled = false;
                    redoStripButton.Enabled = redoToolStripMenuItem.Enabled = false;
                    //dgvObjectExplorer.RowCount = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            ScriptCommunityPack.ToolTip_Draw(sender, e);
        }

        private void ToolTip_Popup(object sender, PopupEventArgs e)
        {
            ScriptCommunityPack.ToolTip_Popup(sender, e);
        }

        List<int> ExpandedList = new List<int>();

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node == treeView3.Nodes[0])
                    return;

                int line = int.Parse(e.Node.Tag.ToString());
                CurrentTB.GoEnd();
                CurrentTB.SelectionStart = line;
                CurrentTB.DoSelectionVisible();
                CurrentTB.Focus();
                canCancel = true;
                treeView3.SelectedNode = e.Node;
            }
            else
            {
                canCancel = true;
            }
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

        private void TsFiles_TabStripItemSelectionChanged(TabStripItemChangedEventArgs e)
        {
            if (e.ChangeType == FATabStripItemChangeTypes.SelectionChanged)
            {
                FastColoredTextBox ftb = e.Item.Controls[0] as FastColoredTextBox;
                documentMap1.Target = ftb;
                RefreshObjectExplorer(ftb);
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

        private void Zoom_click(object sender, EventArgs e)
        {
            if (CurrentTB != null)
                CurrentTB.Zoom = int.Parse((sender as ToolStripItem).Tag.ToString());
        }


        private void fileToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
                (sender as ToolStripMenuItem).ForeColor = Color.White;

        }

        private void fileToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
                (sender as ToolStripMenuItem).ForeColor = Color.Black;

        }

    }
}
