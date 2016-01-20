namespace FastColoredTextBoxNS
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class Style : IDisposable
    {
        public Style()
        {
            this.IsExportable = true;
        }

        public event EventHandler<VisualMarkerEventArgs> VisualMarkerClick;
        public virtual bool IsExportable { get; set; }

        public static GraphicsPath GetRoundedRectangle(Rectangle rect, int d)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180f, 90f);
            path.AddArc((rect.X + rect.Width) - d, rect.Y, d, d, 270f, 90f);
            path.AddArc((rect.X + rect.Width) - d, (rect.Y + rect.Height) - d, d, d, 0f, 90f);
            path.AddArc(rect.X, (rect.Y + rect.Height) - d, d, d, 90f, 90f);
            path.AddLine(rect.X, (rect.Y + rect.Height) - d, rect.X, rect.Y + (d / 2));
            return path;
        }

        public static Size GetSizeOfRange(Range range)
        {
            return new Size((range.End.iChar - range.Start.iChar) * range.tb.CharWidth, range.tb.CharHeight);
        }

        public virtual void Dispose()
        {
        }

        public abstract void Draw(Graphics gr, Point position, Range range);

        public virtual void OnVisualMarkerClick(FastColoredTextBox tb, VisualMarkerEventArgs args)
        {
            if (this.VisualMarkerClick != null)
            {
                this.VisualMarkerClick(tb, args);
            }
        }

        protected virtual void AddVisualMarker(FastColoredTextBox tb, StyleVisualMarker marker)
        {
            tb.AddVisualMarker(marker);
        }
    }
}