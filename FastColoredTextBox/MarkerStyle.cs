namespace FastColoredTextBoxNS
{
    using System.Drawing;

    public class MarkerStyle : Style
    {
        public MarkerStyle(Brush backgroundBrush)
        {
            this.BackgroundBrush = backgroundBrush;
            this.IsExportable = false;
        }

        public Brush BackgroundBrush { get; set; }

        public override void Dispose()
        {
            base.Dispose();
            if (this.BackgroundBrush != null)
            {
                this.BackgroundBrush.Dispose();
            }
        }

        public override void Draw(Graphics gr, Point position, Range range)
        {
            if (this.BackgroundBrush != null)
            {
                Rectangle rect = new Rectangle(position.X, position.Y, (range.End.iChar - range.Start.iChar) * range.tb.CharWidth, range.tb.CharHeight);
                if (rect.Width != 0)
                {
                    gr.FillRectangle(this.BackgroundBrush, rect);
                }
            }
        }
    }
}