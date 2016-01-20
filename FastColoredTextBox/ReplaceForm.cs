using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
    public class ReplaceForm : Form
    {
        public TextBox tbFind;
        private Button btClose;
        private Button btFindNext;
        private Button btReplace;
        private Button btReplaceAll;
        private CheckBox cbMatchCase;
        private CheckBox cbRegex;
        private CheckBox cbWholeWord;
        private IContainer components = null;
        private bool firstSearch = true;
        private Label label1;
        private Label label2;
        private Place startPlace;
        private FastColoredTextBox tb;
        private TextBox tbReplace;

        public ReplaceForm()
        {
            InitializeComponent();
        }

        public ReplaceForm(FastColoredTextBox _tb)
        {
            InitializeComponent();
            this.tb = _tb;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnActivated(EventArgs e)
        {
            this.tbFind.Focus();
            this.ResetSerach();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btFindNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Find())
                {
                    MessageBox.Show("Not found");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btReplace_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tb.SelectionLength != 0)
                {
                    this.tb.InsertText(this.tbReplace.Text);
                }
                this.btFindNext_Click(sender, null);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btReplaceAll_Click(object sender, EventArgs e)
        {
            try
            {
                this.tb.Selection.BeginUpdate();
                this.tb.Selection.Start = new Place(0, 0);
                List<Range> ranges = this.FindAll();
                if (ranges.Count > 0)
                {
                    this.tb.TextSource.Manager.ExecuteCommand(new ReplaceTextCommand(this.tb.TextSource, ranges, this.tbReplace.Text));
                    this.tb.Selection.Start = new Place(0, 0);
                }
                this.tb.Invalidate();
                MessageBox.Show(ranges.Count + " occurrence(s) replaced");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            this.tb.Selection.EndUpdate();
        }

        private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            this.ResetSerach();
        }
        private bool Find()
        {
            string text = this.tbFind.Text;
            RegexOptions options = this.cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
            if (!this.cbRegex.Checked)
            {
                text = Regex.Replace(text, @"[\^\$\[\]\(\)\.\\\*\+\|\?\{\}]", @"\$0");
            }
            if (this.cbWholeWord.Checked)
            {
                text = @"\b" + text + @"\b";
            }
            Range range = this.tb.Selection.Clone();
            range.Normalize();
            if (this.firstSearch)
            {
                this.startPlace = range.Start;
                this.firstSearch = false;
            }
            range.Start = range.End;
            if (range.Start >= this.startPlace)
            {
                range.End = new Place(this.tb.GetLineLength(this.tb.LinesCount - 1), this.tb.LinesCount - 1);
            }
            else
            {
                range.End = this.startPlace;
            }
            foreach (Range range2 in range.GetRangesByLines(text, options))
            {
                this.tb.Selection.Start = range2.Start;
                this.tb.Selection.End = range2.End;
                this.tb.DoSelectionVisible();
                this.tb.Invalidate();
                return true;
            }
            if ((range.Start >= this.startPlace) && (this.startPlace > Place.Empty))
            {
                this.tb.Selection.Start = new Place(0, 0);
                return this.Find();
            }
            return false;
        }

        private List<Range> FindAll()
        {
            string text = this.tbFind.Text;
            RegexOptions options = this.cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
            if (!this.cbRegex.Checked)
            {
                text = Regex.Replace(text, @"[\^\$\[\]\(\)\.\\\*\+\|\?\{\}]", @"\$0");
            }
            if (this.cbWholeWord.Checked)
            {
                text = @"\b" + text + @"\b";
            }
            Range range = this.tb.Selection.Clone();
            range.Normalize();
            range.Start = range.End;
            range.End = new Place(this.tb.GetLineLength(this.tb.LinesCount - 1), this.tb.LinesCount - 1);
            List<Range> list = new List<Range>();
            foreach (Range range2 in range.GetRangesByLines(text, options))
            {
                list.Add(range2);
            }
            return list;
        }

        private void FindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                base.Hide();
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ReplaceForm));
            this.btClose = new Button();
            this.btFindNext = new Button();
            this.tbFind = new TextBox();
            this.cbRegex = new CheckBox();
            this.cbMatchCase = new CheckBox();
            this.label1 = new Label();
            this.cbWholeWord = new CheckBox();
            this.btReplace = new Button();
            this.btReplaceAll = new Button();
            this.label2 = new Label();
            this.tbReplace = new TextBox();
            base.SuspendLayout();
            this.btClose.Location = new Point(320, 0x6f);
            this.btClose.Name = "btClose";
            this.btClose.Size = new Size(0x4b, 0x17);
            this.btClose.TabIndex = 7;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new EventHandler(this.btClose_Click);
            this.btFindNext.Location = new Point(0x4d, 0x6f);
            this.btFindNext.Name = "btFindNext";
            this.btFindNext.Size = new Size(0x4b, 0x17);
            this.btFindNext.TabIndex = 4;
            this.btFindNext.Text = "Find next";
            this.btFindNext.UseVisualStyleBackColor = true;
            this.btFindNext.Click += new EventHandler(this.btFindNext_Click);
            this.tbFind.Font = new Font("Consolas", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.tbFind.Location = new Point(0x4d, 13);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new Size(0x13e, 20);
            this.tbFind.TabIndex = 0;
            this.tbFind.TextChanged += new EventHandler(this.cbMatchCase_CheckedChanged);
            this.tbFind.KeyPress += new KeyPressEventHandler(this.tbFind_KeyPress);
            this.cbRegex.AutoSize = true;
            this.cbRegex.BackColor = Color.Transparent;
            this.cbRegex.Location = new Point(0x152, 0x27);
            this.cbRegex.Name = "cbRegex";
            this.cbRegex.Size = new Size(0x39, 0x11);
            this.cbRegex.TabIndex = 3;
            this.cbRegex.Text = "Regex";
            this.cbRegex.UseVisualStyleBackColor = false;
            this.cbRegex.CheckedChanged += new EventHandler(this.cbMatchCase_CheckedChanged);
            this.cbMatchCase.AutoSize = true;
            this.cbMatchCase.BackColor = Color.Transparent;
            this.cbMatchCase.Location = new Point(0x83, 0x27);
            this.cbMatchCase.Name = "cbMatchCase";
            this.cbMatchCase.Size = new Size(0x52, 0x11);
            this.cbMatchCase.TabIndex = 1;
            this.cbMatchCase.Text = "Match case";
            this.cbMatchCase.UseVisualStyleBackColor = false;
            this.cbMatchCase.CheckedChanged += new EventHandler(this.cbMatchCase_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Location = new Point(0x17, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x21, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Find: ";
            this.cbWholeWord.AutoSize = true;
            this.cbWholeWord.BackColor = Color.Transparent;
            this.cbWholeWord.Location = new Point(0xdb, 0x27);
            this.cbWholeWord.Name = "cbWholeWord";
            this.cbWholeWord.Size = new Size(0x71, 0x11);
            this.cbWholeWord.TabIndex = 2;
            this.cbWholeWord.Text = "Match whole word";
            this.cbWholeWord.UseVisualStyleBackColor = false;
            this.cbWholeWord.CheckedChanged += new EventHandler(this.cbMatchCase_CheckedChanged);
            this.btReplace.Location = new Point(0x9e, 0x6f);
            this.btReplace.Name = "btReplace";
            this.btReplace.Size = new Size(0x4b, 0x17);
            this.btReplace.TabIndex = 5;
            this.btReplace.Text = "Replace";
            this.btReplace.UseVisualStyleBackColor = true;
            this.btReplace.Click += new EventHandler(this.btReplace_Click);
            this.btReplaceAll.Location = new Point(0xef, 0x6f);
            this.btReplaceAll.Name = "btReplaceAll";
            this.btReplaceAll.Size = new Size(0x4b, 0x17);
            this.btReplaceAll.TabIndex = 6;
            this.btReplaceAll.Text = "Replace all";
            this.btReplaceAll.UseVisualStyleBackColor = true;
            this.btReplaceAll.Click += new EventHandler(this.btReplaceAll_Click);
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.Transparent;
            this.label2.Location = new Point(6, 0x4a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(50, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Replace:";
            this.tbReplace.Font = new Font("Consolas", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.tbReplace.Location = new Point(0x4d, 0x48);
            this.tbReplace.Name = "tbReplace";
            this.tbReplace.Size = new Size(0x13e, 20);
            this.tbReplace.TabIndex = 0;
            this.tbReplace.TextChanged += new EventHandler(this.cbMatchCase_CheckedChanged);
            this.tbReplace.KeyPress += new KeyPressEventHandler(this.tbFind_KeyPress);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            //this.BackgroundImage = Resources.lightbluebg;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            base.ClientSize = new Size(0x197, 0x92);
            base.Controls.Add(this.tbFind);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.tbReplace);
            base.Controls.Add(this.btReplaceAll);
            base.Controls.Add(this.btReplace);
            base.Controls.Add(this.cbWholeWord);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.cbMatchCase);
            base.Controls.Add(this.cbRegex);
            base.Controls.Add(this.btFindNext);
            base.Controls.Add(this.btClose);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Icon = (Icon)manager.GetObject("$this.Icon");
            base.Name = "ReplaceForm";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Find and replace";
            base.TopMost = true;
            base.FormClosing += new FormClosingEventHandler(this.FindForm_FormClosing);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        private void ResetSerach()
        {
            this.firstSearch = true;
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btFindNext_Click(sender, null);
            }
            if (e.KeyChar == '\x001b')
            {
                base.Hide();
            }
        }
    }
}