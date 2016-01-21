using System.Text.RegularExpressions;

namespace FastColoredTextBoxNS.Render
{
    /// <summary>
    /// This item appears when any part of snippet text is typed
    /// </summary>
    public class DeclarationAuto : SnippetAuto
    {
        public DeclarationAuto(string snippet)
            : base(snippet)
        {
        }

        public override CompareResult Compare(string fragmentText)
        {
            var pattern = Regex.Escape(fragmentText);
            if (Regex.IsMatch(Text, "\\b" + pattern, RegexOptions.IgnoreCase))
                return CompareResult.Visible;
            return CompareResult.Hidden;
        }
    }
}