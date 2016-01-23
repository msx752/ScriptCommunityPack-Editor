namespace FastColoredTextBoxNS
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    public class ExportToHTML
    {
        private FastColoredTextBox tb;

        public ExportToHTML()
        {
            this.UseNbsp = true;
            this.UseOriginalFont = true;
            this.UseStyleTag = true;
            this.UseBr = true;
        }

        public bool UseBr { get; set; }

        public bool UseForwardNbsp { get; set; }

        public bool UseNbsp { get; set; }

        public bool UseOriginalFont { get; set; }

        public bool UseStyleTag { get; set; }

        public string GetHtml(FastColoredTextBox tb)
        {
            this.tb = tb;
            Range r = new Range(tb);
            r.SelectAll();
            return this.GetHtml(r);
        }

        public string GetHtml(Range r)
        {
            this.tb = r.tb;
            Dictionary<StyleIndex, object> dictionary = new Dictionary<StyleIndex, object>();
            StringBuilder sb = new StringBuilder();
            StringBuilder tempSB = new StringBuilder();
            StyleIndex none = StyleIndex.None;
            r.Normalize();
            int iLine = r.Start.iLine;
            dictionary[none] = null;
            if (this.UseOriginalFont)
            {
                sb.AppendFormat("<font style=\"font-family: {0}, monospace; font-size: {1}px; line-height: {2}px;\">", r.tb.Font.Name, r.tb.CharHeight - r.tb.LineInterval, r.tb.CharHeight);
            }
            bool flag = false;
            foreach (Place place in (IEnumerable<Place>)r)
            {
                Char ch = r.tb[place.iLine][place.iChar];
                if (ch.style != none)
                {
                    this.Flush(sb, tempSB, none);
                    none = ch.style;
                    dictionary[none] = null;
                }
                if (place.iLine != iLine)
                {
                    for (int i = iLine; i < place.iLine; i++)
                    {
                        tempSB.AppendLine(this.UseBr ? "<br>" : "");
                    }
                    iLine = place.iLine;
                    flag = false;
                }
                char c = ch.c;
                if (c != ' ')
                {
                    switch (c)
                    {
                        case '<':
                            {
                                tempSB.Append("&lt;");
                                continue;
                            }
                        case '>':
                            {
                                tempSB.Append("&gt;");
                                continue;
                            }
                        case '&':
                            goto Label_01C8;
                    }
                    goto Label_01D6;
                }
                if (!((!flag && this.UseForwardNbsp) || this.UseNbsp))
                {
                    goto Label_01D6;
                }
                tempSB.Append("&nbsp;");
                continue;
                Label_01C8:
                tempSB.Append("&amp;");
                continue;
                Label_01D6:
                flag = true;
                tempSB.Append(ch.c);
            }
            this.Flush(sb, tempSB, none);
            if (this.UseOriginalFont)
            {
                sb.AppendLine("</font>");
            }
            if (this.UseStyleTag)
            {
                tempSB.Length = 0;
                tempSB.AppendLine("<style type=\"text/css\">");
                foreach (StyleIndex index2 in dictionary.Keys)
                {
                    tempSB.AppendFormat(".fctb{0}{{ {1} }}\r\n", this.GetStyleName(index2), this.GetCss(index2));
                }
                tempSB.AppendLine("</style>");
                sb.Insert(0, tempSB.ToString());
            }
            return sb.ToString();
        }

        private static string GetColorAsString(Color color)
        {
            if (color == Color.Transparent)
            {
                return "";
            }
            return string.Format("#{0:x2}{1:x2}{2:x2}", color.R, color.G, color.B);
        }

        private void Flush(StringBuilder sb, StringBuilder tempSB, StyleIndex currentStyle)
        {
            if (tempSB.Length != 0)
            {
                if (this.UseStyleTag)
                {
                    sb.AppendFormat("<font class=fctb{0}>{1}</font>", this.GetStyleName(currentStyle), tempSB.ToString());
                }
                else
                {
                    string css = this.GetCss(currentStyle);
                    if (css != "")
                    {
                        sb.AppendFormat("<font style=\"{0}\">", css);
                    }
                    sb.Append(tempSB.ToString());
                    if (css != "")
                    {
                        sb.Append("</font>");
                    }
                }
                tempSB.Length = 0;
            }
        }

        private string GetCss(StyleIndex styleIndex)
        {
            TextStyle defaultStyle = null;
            int num = 1;
            bool flag = false;
            for (int i = 0; i < this.tb.Styles.Length; i++)
            {
                if ((this.tb.Styles[i] != null) && ((((int)styleIndex) & num) != ((int)StyleIndex.None)))
                {
                    Style style2 = this.tb.Styles[i];
                    bool flag2 = style2 is TextStyle;
                    if (flag2 && !(flag && !this.tb.AllowSeveralTextStyleDrawing))
                    {
                        flag = true;
                        defaultStyle = style2 as TextStyle;
                    }
                }
                num = num << 1;
            }
            if (!flag)
            {
                defaultStyle = this.tb.DefaultStyle;
            }
            string str = "";
            string colorAsString = "";
            if (defaultStyle.BackgroundBrush is SolidBrush)
            {
                colorAsString = GetColorAsString((defaultStyle.BackgroundBrush as SolidBrush).Color);
                if (colorAsString != "")
                {
                    str = str + "background-color:" + colorAsString + ";";
                }
            }
            if (defaultStyle.ForeBrush is SolidBrush)
            {
                colorAsString = GetColorAsString((defaultStyle.ForeBrush as SolidBrush).Color);
                if (colorAsString != "")
                {
                    str = str + "color:" + colorAsString + ";";
                }
            }
            if ((defaultStyle.FontStyle & FontStyle.Bold) != FontStyle.Regular)
            {
                str = str + "font-weight:bold;";
            }
            if ((defaultStyle.FontStyle & FontStyle.Italic) != FontStyle.Regular)
            {
                str = str + "font-style:oblique;";
            }
            if ((defaultStyle.FontStyle & FontStyle.Strikeout) != FontStyle.Regular)
            {
                str = str + "text-decoration:line-through;";
            }
            if ((defaultStyle.FontStyle & FontStyle.Underline) != FontStyle.Regular)
            {
                str = str + "text-decoration:underline;";
            }
            return str;
        }

        private string GetStyleName(StyleIndex styleIndex)
        {
            return styleIndex.ToString().Replace(" ", "").Replace(",", "");
        }
    }
}