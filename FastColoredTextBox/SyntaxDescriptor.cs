namespace FastColoredTextBoxNS
{
    using System;
    using System.Collections.Generic;

    public class SyntaxDescriptor : IDisposable
    {
        public readonly List<FoldingDesc> foldings = new List<FoldingDesc>();
        public readonly List<RuleDesc> rules = new List<RuleDesc>();
        public readonly List<Style> styles = new List<Style>();
        public char leftBracket = '(';
        public char leftBracket2 = '\0';
        public char rightBracket = ')';
        public char rightBracket2 = '\0';
        public void Dispose()
        {
            foreach (Style style in this.styles)
            {
                style.Dispose();
            }
        }
    }
}