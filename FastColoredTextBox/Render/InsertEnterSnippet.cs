using System;

namespace FastColoredTextBoxNS.Render
{
    /// <summary>
    /// Inerts line break after '}'
    /// </summary>
    public class InsertEnterSnippet : AutocompleteItem
    {
        private Place enterPlace = Place.Empty;

        public InsertEnterSnippet()
            : base("[Line break]")
        {
        }

        public override string ToolTipTitle
        {
            get
            {
                return "Insert line break after '}'";
            }
        }

        public override CompareResult Compare(string fragmentText)
        {
            var r = Parent.Fragment.Clone();
            while (r.Start.iChar > 0)
            {
                if (r.CharBeforeStart == '}')
                {
                    enterPlace = r.Start;
                    return CompareResult.Visible;
                }

                r.GoLeftThroughFolded();
            }

            return CompareResult.Hidden;
        }

        public override string GetTextForReplace()
        {
            //extend range
            Range r = Parent.Fragment;
            Place end = r.End;
            r.Start = enterPlace;
            r.End = r.End;
            //insert line break
            return Environment.NewLine + r.Text;
        }

        public override void OnSelected(AutocompleteMenu popupMenu, SelectedEventArgs e)
        {
            base.OnSelected(popupMenu, e);
            if (Parent.Fragment.tb.AutoIndent)
                Parent.Fragment.tb.DoAutoIndent();
        }
    }
}