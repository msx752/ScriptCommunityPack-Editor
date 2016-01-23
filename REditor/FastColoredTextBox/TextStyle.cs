namespace FastColoredTextBoxNS
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class TextStyle : Style
    {
        public StringFormat stringFormat;

        public TextStyle(Brush foreBrush, Brush backgroundBrush, System.Drawing.FontStyle fontStyle)
        {
            this.ForeBrush = foreBrush;
            this.BackgroundBrush = backgroundBrush;
            this.FontStyle = fontStyle;
            this.stringFormat = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);
        }

        public Brush BackgroundBrush { get; set; }

        public System.Drawing.FontStyle FontStyle { get; set; }

        public Brush ForeBrush { get; set; }

        public override void Dispose()
        {
            base.Dispose();
            if (this.ForeBrush != null)
            {
                this.ForeBrush.Dispose();
            }
            if (this.BackgroundBrush != null)
            {
                this.BackgroundBrush.Dispose();
            }
        }

        public override void Draw(Graphics gr, Point position, Range range)
        {
            int num4;
            if (this.BackgroundBrush != null)
            {
                gr.FillRectangle(this.BackgroundBrush, position.X, position.Y, (range.End.iChar - range.Start.iChar) * range.tb.CharWidth, range.tb.CharHeight);
            }
            Font font = new Font(range.tb.Font, this.FontStyle);
            Line line = range.tb[range.Start.iLine];
            float charWidth = range.tb.CharWidth;
            float y = position.Y + (range.tb.LineInterval / 2);
            float dx = position.X - (range.tb.CharWidth / 3);
            if (this.ForeBrush == null)
            {
                this.ForeBrush = new SolidBrush(range.tb.ForeColor);
            }
            if (range.tb.ImeAllowed)
            {
                for (num4 = range.Start.iChar; num4 < range.End.iChar; num4++)
                {
                    SizeF charSize = FastColoredTextBox.GetCharSize(font, line[num4].c);
                    GraphicsState gstate = gr.Save();
                    float sx = (charSize.Width > (range.tb.CharWidth + 1)) ? (((float)range.tb.CharWidth) / charSize.Width) : 1f;
                    gr.TranslateTransform(dx, y + (((1f - sx) * range.tb.CharHeight) / 2f));
                    gr.ScaleTransform(sx, (float)Math.Sqrt((double)sx));
                    gr.DrawString(line[num4].c.ToString(), font, this.ForeBrush, 0f, 0f, this.stringFormat);
                    gr.Restore(gstate);
                    dx += charWidth;
                }
            }
            else
            {
                for (num4 = range.Start.iChar; num4 < range.End.iChar; num4++)
                {
                    gr.DrawString(line[num4].c.ToString(), font, this.ForeBrush, dx, y, this.stringFormat);
                    dx += charWidth;
                }
            }
            font.Dispose();
        }
    }
}