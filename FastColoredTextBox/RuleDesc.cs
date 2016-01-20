using System.Text.RegularExpressions;
namespace FastColoredTextBoxNS
{
    public class RuleDesc
    {
        public RegexOptions options = RegexOptions.None;
        public string pattern;
        public Style style;
        private Regex regex;
        public Regex Regex
        {
            get
            {
                if (this.regex == null)
                {
                    this.regex = new Regex(this.pattern, RegexOptions.Compiled | this.options);
                }
                return this.regex;
            }
        }
    }
}