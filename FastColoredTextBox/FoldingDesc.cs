namespace FastColoredTextBoxNS
{
    using System.Text.RegularExpressions;

    public class FoldingDesc
    {
        public string finishMarkerRegex;
        public RegexOptions options = RegexOptions.None;
        public string startMarkerRegex;
    }
}