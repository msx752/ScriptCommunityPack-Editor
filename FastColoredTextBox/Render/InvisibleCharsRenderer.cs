﻿using System.Drawing;

namespace FastColoredTextBoxNS.Render
{
    public class InvisibleCharsRenderer : Style
    {
        private Pen pen;

        public InvisibleCharsRenderer(Pen pen)
        {
            this.pen = pen;
        }

        public override void Draw(Graphics gr, Point position, Range range)
        {
            var tb = range.tb;
            using (Brush brush = new SolidBrush(pen.Color))
                foreach (var place in range)
                {
                    switch (tb[place].c)
                    {
                        case ' ':
                            {
                                var point = tb.PlaceToPoint(place);
                                point.Offset(tb.CharWidth / 2, tb.CharHeight / 2);
                                gr.DrawLine(pen, point.X, point.Y, point.X + 1, point.Y);
                                if (tb[place.iLine].Count - 1 == place.iChar)
                                    goto default;
                                break;
                            }
                        default:
                            if (tb[place.iLine].Count - 1 == place.iChar)
                            {
                                var point = tb.PlaceToPoint(place);
                                point.Offset(tb.CharWidth, 0);
                                gr.DrawString("¶", tb.Font, brush, point);
                            }
                            break;
                    }
                }
        }
    }
}