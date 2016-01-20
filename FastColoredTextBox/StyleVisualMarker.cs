namespace FastColoredTextBoxNS
{
    using System.Drawing;

    public class StyleVisualMarker : VisualMarker
    {
        public StyleVisualMarker(Rectangle rectangle, Style style) : base(rectangle)
        {
            this.Style = style;
        }

        public Style Style { get; private set; }
    }
}