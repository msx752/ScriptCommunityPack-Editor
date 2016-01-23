namespace FastColoredTextBoxNS
{
    using System.Drawing;

    internal class ExpandFoldingMarker : VisualMarker
    {
        public readonly int iLine;

        public ExpandFoldingMarker(int iLine, Rectangle rectangle) : base(rectangle)
        {
            this.iLine = iLine;
        }

        public override void Draw(Graphics gr, Pen pen)
        {
            gr.FillRectangle(Brushes.White, base.rectangle);
            gr.DrawRectangle(pen, base.rectangle);
            gr.DrawLine(Pens.Red, (int)(base.rectangle.Left + 2), (int)(base.rectangle.Top + (base.rectangle.Height / 2)), (int)(base.rectangle.Right - 2), (int)(base.rectangle.Top + (base.rectangle.Height / 2)));
            gr.DrawLine(Pens.Red, (int)(base.rectangle.Left + (base.rectangle.Width / 2)), (int)(base.rectangle.Top + 2), (int)(base.rectangle.Left + (base.rectangle.Width / 2)), (int)(base.rectangle.Bottom - 2));
        }
    }
}