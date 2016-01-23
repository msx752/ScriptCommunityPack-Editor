namespace FastColoredTextBoxNS
{
    using System.Drawing;
    using System.Windows.Forms;

    public class PaintLineEventArgs : PaintEventArgs
    {
        public PaintLineEventArgs(int iLine, Rectangle rect, Graphics gr, Rectangle clipRect) : base(gr, clipRect)
        {
            this.LineIndex = iLine;
            this.LineRect = rect;
        }

        public int LineIndex { get; private set; }

        public Rectangle LineRect { get; private set; }
    }
}