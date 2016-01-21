using System.Text.RegularExpressions;

namespace FastColoredTextBoxNS.Render
{
    /// <summary>
    /// Divides numbers) (words: "123AND456" -> "123) (456"
    /// Or "i=2" -> "i = 2"
    /// </summary>
    public class InsertSpaceSnippet : AutoCompleteItem

    {
        private string pattern;

        public InsertSpaceSnippet(string pattern)
            : base("")
        {
            this.pattern = pattern;
        }

        public InsertSpaceSnippet()
            : this(@"^(\d+)([a-zA-Z_]+)(\d*)$")
        {
        }

        public override string ToolTipTitle
        {
            get
            {
                return Text;
            }
        }

        public override CompareResult Compare(string fragmentText)
        {
            if (Regex.IsMatch(fragmentText, pattern))
            {
                Text = InsertSpaces(fragmentText);
                if (Text != fragmentText)
                    return CompareResult.Visible;
            }
            return CompareResult.Hidden;
        }

        public string InsertSpaces(string fragment)
        {
            var m = Regex.Match(fragment, pattern);
            if (m == null)
                return fragment;
            if (m.Groups[1].Value == "" && m.Groups[3].Value == "")
                return fragment;
            return (m.Groups[1].Value + " " + m.Groups[2].Value + " " + m.Groups[3].Value).Trim();
        }
    }
}