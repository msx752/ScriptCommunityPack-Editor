using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
    public class FindForm : Form
    {
        private Button btClose;
        private Button btFindNext;
        private CheckBox cbMatchCase;
        private CheckBox cbRegex;
        private CheckBox cbWholeWord;
        private bool firstSearch = true;
        private Label label1;
        public static string RegexSpecSymbolsPattern = @"[\^\$\[\]\(\)\.\\\*\+\|\?\{\}]";
        private Place startPlace;
        private FastColoredTextBox tb;
        public TextBox tbFind;

        public FindForm(FastColoredTextBox tb)
        {
            InitializeComponent();
            this.tb = tb;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btFindNext_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        private void FindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            this.tb.Focus();
        }

        public void FindNext()
        {
            try
            {
                string text = this.tbFind.Text;
                RegexOptions options = this.cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
                if (!this.cbRegex.Checked)
                {
                    text = Regex.Replace(text, RegexSpecSymbolsPattern, @"\$0");
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
                    this.tb.Selection = range2;
                    this.tb.DoSelectionVisible();
                    this.tb.Invalidate();
                    return;
                }
                if ((range.Start >= this.startPlace) && (this.startPlace > Place.Empty))
                {
                    this.tb.Selection.Start = new Place(0, 0);
                    this.FindNext();
                }
                else
                {
                    MessageBox.Show("Not found");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FindForm));
            this.btClose = new Button();
            this.btFindNext = new Button();
            this.tbFind = new TextBox();
            this.cbRegex = new CheckBox();
            this.cbMatchCase = new CheckBox();
            this.label1 = new Label();
            this.cbWholeWord = new CheckBox();
            this.SuspendLayout();
            this.btClose.Location = new Point(0x111, 0x48);
            this.btClose.Name = "btClose";
            this.btClose.Size = new Size(0x4b, 0x17);
            this.btClose.TabIndex = 5;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new EventHandler(this.btClose_Click);
            this.btFindNext.Location = new Point(0xc0, 0x48);
            this.btFindNext.Name = "btFindNext";
            this.btFindNext.Size = new Size(0x4b, 0x17);
            this.btFindNext.TabIndex = 4;
            this.btFindNext.Text = "Find next";
            this.btFindNext.UseVisualStyleBackColor = true;
            this.btFindNext.Click += new EventHandler(this.btFindNext_Click);
            this.tbFind.Font = new Font("Consolas", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.tbFind.Location = new Point(0x2a, 12);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new Size(0x132, 20);
            this.tbFind.TabIndex = 0;
            this.tbFind.TextChanged += new EventHandler(this.cbMatchCase_CheckedChanged);
            this.tbFind.KeyPress += new KeyPressEventHandler(this.tbFind_KeyPress);
            this.cbRegex.AutoSize = true;
            this.cbRegex.BackColor = Color.Transparent;
            this.cbRegex.Location = new Point(0xf9, 0x26);
            this.cbRegex.Name = "cbRegex";
            this.cbRegex.Size = new Size(0x39, 0x11);
            this.cbRegex.TabIndex = 3;
            this.cbRegex.Text = "Regex";
            this.cbRegex.UseVisualStyleBackColor = false;
            this.cbRegex.CheckedChanged += new EventHandler(this.cbMatchCase_CheckedChanged);
            this.cbMatchCase.AutoSize = true;
            this.cbMatchCase.BackColor = Color.Transparent;
            this.cbMatchCase.Location = new Point(0x2a, 0x26);
            this.cbMatchCase.Name = "cbMatchCase";
            this.cbMatchCase.Size = new Size(0x52, 0x11);
            this.cbMatchCase.TabIndex = 1;
            this.cbMatchCase.Text = "Match case";
            this.cbMatchCase.UseVisualStyleBackColor = false;
            this.cbMatchCase.CheckedChanged += new EventHandler(this.cbMatchCase_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Location = new Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x21, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Find: ";
            this.cbWholeWord.AutoSize = true;

            this.cbWholeWord.BackColor = Color.Transparent;
            this.cbWholeWord.Location = new Point(130, 0x26);
            this.cbWholeWord.Name = "cbWholeWord";
            this.cbWholeWord.Size = new Size(0x71, 0x11);
            this.cbWholeWord.TabIndex = 2;
            this.cbWholeWord.Text = "Match whole word";
            this.cbWholeWord.UseVisualStyleBackColor = false;
            this.cbWholeWord.CheckedChanged += new EventHandler(this.cbMatchCase_CheckedChanged);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.BackgroundImage = Resources.lightbluebg;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ClientSize = new Size(360, 0x67);
            this.Controls.Add(this.cbWholeWord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbMatchCase);
            this.Controls.Add(this.cbRegex);
            this.Controls.Add(this.tbFind);
            this.Controls.Add(this.btFindNext);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            //base.Icon = (Icon) manager.GetObject("$this.Icon");
            this.Name = "FindForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find";
            this.TopMost = true;
            this.FormClosing += new FormClosingEventHandler(this.FindForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        protected override void OnActivated(EventArgs e)
        {
            tbFind.Focus();
            ResetSerach();
        }

        private void ResetSerach()
        {
            firstSearch = true;
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btFindNext.PerformClick();
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '\x1b')
            {
                Hide();
                e.Handled = true;
                return;
            }
        }

        private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            ResetSerach();
        }
    }
}