namespace FastColoredTextBoxNS
{
    using System.Drawing;

    internal class CollapseFoldingMarker : VisualMarker
    {
        public readonly int iLine;

        public CollapseFoldingMarker(int iLine, Rectangle rectangle) : base(rectangle)
        {
            this.iLine = iLine;
        }

        public override void Draw(Graphics gr, Pen pen)
        {
            gr.FillRectangle(Brushes.White, base.rectangle);
            gr.DrawRectangle(pen, base.rectangle);
            gr.DrawLine(pen, (int)(base.rectangle.Left + 2), (int)(base.rectangle.Top + (base.rectangle.Height / 2)), (int)(base.rectangle.Right - 2), (int)(base.rectangle.Top + (base.rectangle.Height / 2)));
        }
    }
}