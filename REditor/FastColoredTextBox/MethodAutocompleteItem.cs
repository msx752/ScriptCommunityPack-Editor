using FastColoredTextBoxNS.Render;
using System;

namespace FastColoredTextBoxNS
{
    public class MethodAuto : AutoCompleteItem
    {
        private string firstPart;
        private string lowercaseText;

        public MethodAuto(string text)
            : base(text)
        {
            this.lowercaseText = base.Text.ToLower();
        }

        public MethodAuto(PopupToolTip _keyword)
            : base(_keyword)
        {
            this.lowercaseText = base.Text.ToLower();
        }

        public override string MenuText
        {
            get
            {
                return base.MenuText;
            }
            set
            {
                base.MenuText = value;
            }
        }

        public new AutocompleteMenu Parent { get; internal set; }

        public override string ToolTipText
        {
            get
            {
                return base.ToolTipText;
            }
            set
            {
                base.ToolTipText = value;
            }
        }

        public override string ToolTipTitle
        {
            get
            {
                return base.ToolTipTitle;
            }
            set
            {
                base.ToolTipTitle = value;
            }
        }

        public override CompareResult Compare(string fragmentText)
        {
            int length = fragmentText.LastIndexOf('.');
            if (length >= 0)
            {
                string str = fragmentText.Substring(length + 1);
                this.firstPart = fragmentText.Substring(0, length);
                if (str == "")
                {
                    return CompareResult.Visible;
                }
                if (base.Text.StartsWith(str, StringComparison.InvariantCultureIgnoreCase))
                {
                    return CompareResult.VisibleAndSelected;
                }
                if (this.lowercaseText.Contains(str.ToLower()))
                {
                    return CompareResult.Visible;
                }
            }
            return CompareResult.Hidden;
        }

        public override string GetTextForReplace()
        {
            return (this.firstPart + "." + base.Text);
        }

        public override void OnSelected(AutocompleteMenu popupMenu, SelectedEventArgs e)
        {
        }

        public override string ToString()
        {
            return (base.MenuText ?? base.Text);
        }
    }
}