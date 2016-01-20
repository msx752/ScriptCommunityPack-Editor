namespace FastColoredTextBoxNS
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Char
    {
        public char c;
        public StyleIndex style;

        public Char(char c)
        {
            this.c = c;
            this.style = StyleIndex.None;
        }
    }
}