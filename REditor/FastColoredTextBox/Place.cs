using System.Runtime.InteropServices;

namespace FastColoredTextBoxNS
{
    [StructLayout(LayoutKind.Sequential)]
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    public struct Place
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    {
        public int iChar;
        public int iLine;

        public Place(int iChar, int iLine)
        {
            this.iChar = iChar;
            this.iLine = iLine;
        }

        public void Offset(int dx, int dy)
        {
            this.iChar += dx;
            this.iLine += dy;
        }

        public static bool operator !=(Place p1, Place p2)
        {
            return !p1.Equals(p2);
        }

        public static bool operator ==(Place p1, Place p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator <(Place p1, Place p2)
        {
            if (p1.iLine < p2.iLine)
            {
                return true;
            }
            if (p1.iLine > p2.iLine)
            {
                return false;
            }
            return (p1.iChar < p2.iChar);
        }

        public static bool operator <=(Place p1, Place p2)
        {
            if (p1.Equals(p2))
            {
                return true;
            }
            if (p1.iLine < p2.iLine)
            {
                return true;
            }
            if (p1.iLine > p2.iLine)
            {
                return false;
            }
            return (p1.iChar < p2.iChar);
        }

        public static bool operator >(Place p1, Place p2)
        {
            if (p1.iLine > p2.iLine)
            {
                return true;
            }
            if (p1.iLine < p2.iLine)
            {
                return false;
            }
            return (p1.iChar > p2.iChar);
        }

        public static bool operator >=(Place p1, Place p2)
        {
            if (p1.Equals(p2))
            {
                return true;
            }
            if (p1.iLine > p2.iLine)
            {
                return true;
            }
            if (p1.iLine < p2.iLine)
            {
                return false;
            }
            return (p1.iChar > p2.iChar);
        }

        public static Place Empty
        {
            get
            {
                return new Place();
            }
        }

        public override string ToString()
        {
            return string.Concat(new object[] { "(", this.iChar, ",", this.iLine, ")" });
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}