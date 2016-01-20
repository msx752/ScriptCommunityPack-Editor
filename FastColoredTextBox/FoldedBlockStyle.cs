using System.Drawing;
namespace FastColoredTextBoxNS
{
    public class FoldedBlockStyle : TextStyle
    {
        public FoldedBlockStyle(Brush foreBrush, Brush backgroundBrush, FontStyle fontStyle) : base(foreBrush, backgroundBrush, fontStyle)
        {
        }

        public override void Draw(Graphics gr, Point position, Range range)
        {
            if (range.End.iChar <= range.Start.iChar)
            {
                using (Font font = new Font(range.tb.Font, base.FontStyle))
                {
                    gr.DrawString("...", font, base.ForeBrush, (float)range.tb.LeftIndent, (float)(position.Y - 2));
                }
                range.tb.AddVisualMarker(new FoldedAreaMarker(range.Start.iLine, new Rectangle(range.tb.LeftIndent + 2, position.Y, 2 * range.tb.CharHeight, range.tb.CharHeight)));
            }
            else
            {
                base.Draw(gr, position, range);
                int x = position.X;
                for (int i = range.Start.iChar; i < range.End.iChar; i++)
                {
                    if (range.tb[range.Start.iLine][i].c != ' ')
                    {
                        break;
                    }
                    x += range.tb.CharWidth;
                }
                range.tb.AddVisualMarker(new FoldedAreaMarker(range.Start.iLine, new Rectangle(x, position.Y, (position.X + ((range.End.iChar - range.Start.iChar) * range.tb.CharWidth)) - x, range.tb.CharHeight)));
            }
        }
    }
}