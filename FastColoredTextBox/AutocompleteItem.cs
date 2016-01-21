using FastColoredTextBoxNS.Render;
using System;

namespace FastColoredTextBoxNS
{
    public class AutoCompleteItem
    {
        public int ImageIndex;
        public object Tag;
        public string Text;
        private string menuText;
        private string toolTipText;
        private string toolTipTitle;

        public AutoCompleteItem()
        {
            this.ImageIndex = -1;
        }

        public AutoCompleteItem(PopupToolTip _keyword)
        {
            Text = _keyword.Name;
            toolTipTitle = _keyword.Name;
            toolTipText = _keyword.ToString();
        }

        public AutoCompleteItem(string text)
        {
            this.ImageIndex = -1;
            this.Text = text;
        }

        public AutoCompleteItem(string text, int imageIndex)
            : this(text)
        {
            this.ImageIndex = imageIndex;
        }

        public AutoCompleteItem(string text, int imageIndex, string menuText)
            : this(text, imageIndex)
        {
            this.menuText = menuText;
        }

        public AutoCompleteItem(string text, int imageIndex, string menuText, string toolTipTitle, string toolTipText)
            : this(text, imageIndex, menuText)
        {
            this.toolTipTitle = toolTipTitle;
            this.toolTipText = toolTipText;
        }

        public virtual string MenuText
        {
            get
            {
                return this.menuText;
            }
            set
            {
                this.menuText = value;
            }
        }

        public AutocompleteMenu Parent { get; internal set; }

        public virtual string ToolTipText
        {
            get
            {
                return this.toolTipText;
            }
            set
            {
                this.toolTipText = value;
            }
        }

        public virtual string ToolTipTitle
        {
            get
            {
                return this.toolTipTitle;
            }
            set
            {
                this.toolTipTitle = value;
            }
        }

        public virtual CompareResult Compare(string fragmentText)
        {
            if (this.Text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase) && (this.Text != fragmentText))
            {
                return CompareResult.VisibleAndSelected;
            }
            return CompareResult.Hidden;
        }

        public virtual string GetTextForReplace()
        {
            return this.Text;
        }

        public virtual void OnSelected(AutocompleteMenu popupMenu, SelectedEventArgs e)
        {
        }

        public override string ToString()
        {
            return (this.menuText ?? this.Text);
        }
    }
}