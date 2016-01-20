namespace FastColoredTextBoxNS
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [Browsable(false)]
    public class AutocompleteMenu : ToolStripDropDown
    {
        public ToolStripControlHost host;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

        private AutocompleteListView listView;

        public AutocompleteMenu(FastColoredTextBox tb)
        {
            base.AutoClose = false;
            this.AutoSize = false;
            base.Margin = Padding.Empty;
            base.Padding = Padding.Empty;
            this.listView = new AutocompleteListView(tb);
            this.host = new ToolStripControlHost(this.listView);
            this.host.Margin = new Padding(2, 2, 2, 2);
            this.host.Padding = Padding.Empty;
            this.host.AutoSize = false;
            this.host.AutoToolTip = false;
            this.CalcSize();
            base.Items.Add(this.host);
            this.listView.Parent = this;
            this.SearchPattern = @"[\w\.]";
            this.MinFragmentLength = 2;
        }

        public event EventHandler<CancelEventArgs> Opening;

#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        public event EventHandler<SelectedEventArgs> Selected;

        public event EventHandler<SelectingEventArgs> Selecting;
        public bool AllowTabKey
        {
            get
            {
                return this.listView.AllowTabKey;
            }
            set
            {
                this.listView.AllowTabKey = value;
            }
        }

        public int AppearInterval
        {
            get
            {
                return this.listView.AppearInterval;
            }
            set
            {
                this.listView.AppearInterval = value;
            }
        }

        public Range Fragment { get; internal set; }

        public AutocompleteListView Items
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            get
            {
                return this.listView;
            }
        }

        public int MinFragmentLength { get; set; }

        public string SearchPattern { get; set; }

        public new void Close()
        {
            this.listView.toolTip.Hide(this.listView);
            base.Close();
        }

        public new void OnOpening(CancelEventArgs args)
        {
            if (this.Opening != null)
            {
                this.Opening(this, args);
            }
        }

        public void OnSelected(SelectedEventArgs args)
        {
            if (this.Selected != null)
            {
                this.Selected(this, args);
            }
        }

        public virtual void OnSelecting()
        {
            this.listView.OnSelecting();
        }

        public void SelectNext(int shift)
        {
            this.listView.SelectNext(shift);
        }

        public void Show(bool forced)
        {
            this.Items.DoAutocomplete(forced);
        }

        internal void CalcSize()
        {
            this.host.Size = this.listView.Size;
            base.Size = new Size(this.listView.Size.Width + 4, this.listView.Size.Height + 4);
        }
        internal void OnSelecting(SelectingEventArgs args)
        {
            if (this.Selecting != null)
            {
                this.Selecting(this, args);
            }
        }
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
    }
}