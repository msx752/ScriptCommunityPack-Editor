namespace FastColoredTextBoxNS
{
    using System.Collections.Generic;
    using System.Drawing;

    public class WavyLineStyle : Style
    {
        public WavyLineStyle(int alpha, Color color)
        {
            this.Pen = new System.Drawing.Pen(Color.FromArgb(alpha, color));
        }

        private System.Drawing.Pen Pen { get; set; }

        public override void Dispose()
        {
            base.Dispose();
            if (this.Pen != null)
            {
                this.Pen.Dispose();
            }
        }

        public override void Draw(Graphics gr, Point pos, Range range)
        {
            Size sizeOfRange = Style.GetSizeOfRange(range);
            Point start = new Point(pos.X, (pos.Y + sizeOfRange.Height) - 1);
            Point end = new Point(pos.X + sizeOfRange.Width, (pos.Y + sizeOfRange.Height) - 1);
            this.DrawWavyLine(gr, start, end);
        }

        private void DrawWavyLine(Graphics graphics, Point start, Point end)
        {
            if ((end.X - start.X) < 2)
            {
                graphics.DrawLine(this.Pen, start, end);
            }
            else
            {
                int num = -1;
                List<Point> list = new List<Point>();
                for (int i = start.X; i <= end.X; i += 2)
                {
                    list.Add(new Point(i, start.Y + num));
                    num = -num;
                }
                graphics.DrawLines(this.Pen, list.ToArray());
            }
        }
    }
}