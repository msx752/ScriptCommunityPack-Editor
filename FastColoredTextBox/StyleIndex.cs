namespace FastColoredTextBoxNS
{
    using System;

    [Flags]
    public enum StyleIndex : ushort
    {
        All = 0xffff,
        None = 0,
        Style0 = 1,
        Style1 = 2,
        Style10 = 0x400,
        Style11 = 0x800,
        Style12 = 0x1000,
        Style13 = 0x2000,
        Style14 = 0x4000,
        Style15 = 0x8000,
        Style2 = 4,
        Style3 = 8,
        Style4 = 0x10,
        Style5 = 0x20,
        Style6 = 0x40,
        Style7 = 0x80,
        Style8 = 0x100,
        Style9 = 0x200
    }
}