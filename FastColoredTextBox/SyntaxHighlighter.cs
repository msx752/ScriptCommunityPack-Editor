using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
namespace FastColoredTextBoxNS
{

    public class SyntaxHighlighter : IDisposable
    {
        public readonly Style BlackBoldStyle = new TextStyle(Brushes.Black, null, FontStyle.Bold);
        public readonly Style BlueStyle = new TextStyle(Brushes.Yellow, null, FontStyle.Regular);
        public readonly Style BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        public readonly Style DarkBlueStyle = new TextStyle(Brushes.Cyan, null, FontStyle.Regular);
        public readonly Style DarkCyanBoldStyle = new TextStyle(Brushes.DarkCyan, null, FontStyle.Regular);
        public readonly Style DarkCyanStyle = new TextStyle(Brushes.DarkCyan, null, FontStyle.Regular);
        public readonly Style DarkVioletStyle = new TextStyle(Brushes.DarkViolet, null, FontStyle.Regular);
        public readonly Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        public readonly Style MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        public readonly Style Olive = new TextStyle(Brushes.Olive, null, FontStyle.Regular);
        private readonly Platform platformType = PlatformType.GetOperationSystemPlatform();
        private Dictionary<string, SyntaxDescriptor> descByXMLfileNames = new Dictionary<string, SyntaxDescriptor>();
        private Regex vSCPAttributeRegex;
        private Regex vSCPCommentRegex;
        private Regex vSCPKeywordRegex;
        private Regex vSCPKeywordRegex2;
        private Regex vSCPKeywordRegex3;
        private Regex vSCPOperatorRegex;
        private Regex vSCPStringRegex;
        private Regex vSCPTriggerRegex;
        private Regex vSCPTriggerSymbolRegex;
        private Regex vSCPVariablesRegex;

        private RegexOptions RegexCompiledOption
        {
            get
            {
                if (this.platformType == Platform.X86)
                {
                    return RegexOptions.Compiled;
                }
                return RegexOptions.None;
            }
        }

        public static SyntaxDescriptor ParseXmlDescription(XmlDocument doc)
        {
            SyntaxDescriptor descriptor = new SyntaxDescriptor();
            XmlNode node = doc.SelectSingleNode("doc/brackets");
            if (node != null)
            {
                if ((((node.Attributes["left"] == null) || (node.Attributes["right"] == null)) || (node.Attributes["left"].Value == "")) || (node.Attributes["right"].Value == ""))
                {
                    descriptor.leftBracket = '\0';
                    descriptor.rightBracket = '\0';
                }
                else
                {
                    descriptor.leftBracket = node.Attributes["left"].Value[0];
                    descriptor.rightBracket = node.Attributes["right"].Value[0];
                }
                if ((((node.Attributes["left2"] == null) || (node.Attributes["right2"] == null)) || (node.Attributes["left2"].Value == "")) || (node.Attributes["right2"].Value == ""))
                {
                    descriptor.leftBracket2 = '\0';
                    descriptor.rightBracket2 = '\0';
                }
                else
                {
                    descriptor.leftBracket2 = node.Attributes["left2"].Value[0];
                    descriptor.rightBracket2 = node.Attributes["right2"].Value[0];
                }
            }
            Dictionary<string, Style> styles = new Dictionary<string, Style>();
            foreach (XmlNode node2 in doc.SelectNodes("doc/style"))
            {
                Style item = ParseStyle(node2);
                styles[node2.Attributes["name"].Value] = item;
                descriptor.styles.Add(item);
            }
            foreach (XmlNode node3 in doc.SelectNodes("doc/rule"))
            {
                descriptor.rules.Add(ParseRule(node3, styles));
            }
            foreach (XmlNode node4 in doc.SelectNodes("doc/folding"))
            {
                descriptor.foldings.Add(ParseFolding(node4));
            }
            return descriptor;
        }

        public virtual void AutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            FastColoredTextBox box = sender as FastColoredTextBox;
            if (box.Language == Language.Scp)
            {
                this.vSCPAutoIndentNeeded(sender, args);
            }
        }

        public void Dispose()
        {
            foreach (SyntaxDescriptor descriptor in this.descByXMLfileNames.Values)
            {
                descriptor.Dispose();
            }
        }

        public virtual void HighlightSyntax(Language language, Range range)
        {
            if (language == Language.Scp)
            {
                this.vSCPSyntaxHighlight(range);
            }
        }

        public void HighlightSyntax(SyntaxDescriptor desc, Range range)
        {
            range.tb.ClearStylesBuffer();
            for (int i = 0; i < desc.styles.Count; i++)
            {
                range.tb.Styles[i] = desc.styles[i];
            }
            range.tb.LeftBracket = desc.leftBracket;
            range.tb.RightBracket = desc.rightBracket;
            range.tb.LeftBracket2 = desc.leftBracket2;
            range.tb.RightBracket2 = desc.rightBracket2;
            range.ClearStyle(desc.styles.ToArray());
            foreach (RuleDesc desc2 in desc.rules)
            {
                range.SetStyle(desc2.style, desc2.Regex);
            }
            range.ClearFoldingMarkers();
            foreach (FoldingDesc desc3 in desc.foldings)
            {
                range.SetFoldingMarkers(desc3.startMarkerRegex, desc3.finishMarkerRegex, desc3.options);
            }
        }

        public virtual void HighlightSyntax(string XMLdescriptionFile, Range range)
        {
            SyntaxDescriptor descriptor = null;
            if (!this.descByXMLfileNames.TryGetValue(XMLdescriptionFile, out descriptor))
            {
                XmlDocument doc = new XmlDocument();
                string path = XMLdescriptionFile;
                if (!File.Exists(path))
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(path));
                }
                doc.LoadXml(File.ReadAllText(path));
                descriptor = ParseXmlDescription(doc);
                this.descByXMLfileNames[XMLdescriptionFile] = descriptor;
            }
            this.HighlightSyntax(descriptor, range);
        }

        public virtual void vSCPSyntaxHighlight(Range range)
        {
            range.tb.CommentPrefix = "//";
            range.tb.LeftBracket = '(';
            range.tb.RightBracket = ')';
            range.tb.LeftBracket2 = '<';
            range.tb.RightBracket2 = '>';
            range.ClearStyle(new Style[] { this.BlueStyle, this.DarkBlueStyle, this.DarkCyanBoldStyle, this.DarkCyanStyle, this.GreenStyle, this.BrownStyle, this.Olive, this.DarkVioletStyle, this.BlackBoldStyle });
            if (this.vSCPStringRegex == null)
            {
                this.InitvSCPRegex();
            }
            range.SetStyle(this.BrownStyle, this.vSCPStringRegex);
            range.SetStyle(this.GreenStyle, this.vSCPCommentRegex);
            range.SetStyle(this.DarkCyanBoldStyle, this.vSCPAttributeRegex);
            range.SetStyle(this.DarkBlueStyle, this.vSCPVariablesRegex);
            range.SetStyle(this.BlueStyle, this.vSCPKeywordRegex);
            range.SetStyle(this.DarkCyanStyle, this.vSCPKeywordRegex2);
            range.SetStyle(this.BlackBoldStyle, this.vSCPKeywordRegex3);
            range.SetStyle(this.Olive, this.vSCPOperatorRegex);
            range.SetStyle(this.DarkVioletStyle, this.vSCPTriggerRegex);
            range.SetStyle(this.DarkVioletStyle, this.vSCPTriggerSymbolRegex);
            range.ClearFoldingMarkers();

            range.SetFoldingMarkers(@"/\*", @"\*/");
            range.SetFoldingMarkers(@"(?<range>(\[\w+\s+))", @"(?<range>(\[\w+\s+))");
            //range.SetFoldingMarkers(@"\b((?i)for)\b", @"\b((?i)endfor)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            range.SetFoldingMarkers(@"\b((?i)[i,ı]f)\b", @"\b((?i)end[i,ı]f)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            range.SetFoldingMarkers(@"(?<range>([o,O][n,N]=@))", @"(?<range>([o,O][n,N]=@))");
            range.SetFoldingMarkers(@"//(?i)region\b", @"//(?i)endregion\b");
        }

        private static Color ParseColor(string s)
        {
            if (s.StartsWith("#"))
            {
                if (s.Length <= 7)
                {
                    return Color.FromArgb(0xff, Color.FromArgb(int.Parse(s.Substring(1), NumberStyles.AllowHexSpecifier)));
                }
                return Color.FromArgb(int.Parse(s.Substring(1), NumberStyles.AllowHexSpecifier));
            }
            return Color.FromName(s);
        }

        private static FoldingDesc ParseFolding(XmlNode foldingNode)
        {
            FoldingDesc desc = new FoldingDesc
            {
                startMarkerRegex = foldingNode.Attributes["start"].Value,
                finishMarkerRegex = foldingNode.Attributes["finish"].Value
            };
            XmlAttribute attribute = foldingNode.Attributes["options"];
            if (attribute != null)
            {
                desc.options = (RegexOptions)Enum.Parse(typeof(RegexOptions), attribute.Value);
            }
            return desc;
        }

        private static RuleDesc ParseRule(XmlNode ruleNode, Dictionary<string, Style> styles)
        {
            RuleDesc desc = new RuleDesc
            {
                pattern = ruleNode.InnerText
            };
            XmlAttribute attribute = ruleNode.Attributes["style"];
            XmlAttribute attribute2 = ruleNode.Attributes["options"];
            if (attribute == null)
            {
                throw new Exception("Rule must contain style name.");
            }
            if (!styles.ContainsKey(attribute.Value))
            {
                throw new Exception("Style '" + attribute.Value + "' is not found.");
            }
            desc.style = styles[attribute.Value];
            if (attribute2 != null)
            {
                desc.options = (RegexOptions)Enum.Parse(typeof(RegexOptions), attribute2.Value);
            }
            return desc;
        }

        private static Style ParseStyle(XmlNode styleNode)
        {
            XmlAttribute attribute = styleNode.Attributes["type"];
            XmlAttribute attribute2 = styleNode.Attributes["color"];
            XmlAttribute attribute3 = styleNode.Attributes["backColor"];
            XmlAttribute attribute4 = styleNode.Attributes["fontStyle"];
            XmlAttribute attribute5 = styleNode.Attributes["name"];
            SolidBrush foreBrush = null;
            if (attribute2 != null)
            {
                foreBrush = new SolidBrush(ParseColor(attribute2.Value));
            }
            SolidBrush backgroundBrush = null;
            if (attribute3 != null)
            {
                backgroundBrush = new SolidBrush(ParseColor(attribute3.Value));
            }
            FontStyle regular = FontStyle.Regular;
            if (attribute4 != null)
            {
                regular = (FontStyle)Enum.Parse(typeof(FontStyle), attribute4.Value);
            }
            return new TextStyle(foreBrush, backgroundBrush, regular);
        }

        private void InitvSCPRegex()
        {
            this.vSCPCommentRegex = new Regex("//.*$", RegexOptions.Multiline | this.RegexCompiledOption);
            this.vSCPStringRegex = new Regex("\"\"|@\"\"|''|@\".*?\"|(?<!@)(?<range>\".*?[^\\\\]\")|'.*?[^\\\\]'", this.RegexCompiledOption);
            this.vSCPAttributeRegex = new Regex(@"(?<range>(\[\w+\s+\w+(\s+\w+)?\]))", RegexOptions.Multiline | this.RegexCompiledOption);
            this.vSCPVariablesRegex = new Regex(@"\b((?i)uid|(?i)src|(?i)tag|(?i)tag0|(?i)ctag|(?i)ctag0|(?i)var|(?i)var0|(?i)local|(?i)float|(?i)ref\d+|(?i)def|(?i)def0|(?i)act|(?i)targ|(?i)serv|(?i)new|(?i)cont|(?i)list|(?i)obj|(?i)topobj|(?i)account|(?i)region|(?i)sector|(?i)link|(?i)findlayer|(?i)findcont|(?i)findid|(?i)findtype|(?i)file|(?i)db)\.+(?<range>\w+?)\b", this.RegexCompiledOption);
            this.vSCPTriggerRegex = new Regex(@"\@+(?<range>\w+?)\b", this.RegexCompiledOption);
            this.vSCPKeywordRegex = new Regex(@"\b((?i)[i,ı]f|(?i)el[i,ı]f|(?i)else[i,ı]f|(?i)end[i,ı]f|(?i)for|(?i)forcharlayer|(?i)forcharmemorytype|(?i)forchars|(?i)forclients|(?i)forcont|(?i)forcontid|(?i)forconttype|(?i)forinstances|(?i)foritems|(?i)forobjs|(?i)forplayers|(?i)endfor|(?i)wh[i,ı]le|(?i)endwh[i,ı]le|(?i)beg[i,ı]n|(?i)end|(?i)dorand|(?i)dosw[i,ı]tch|(?i)enddo|(?i)return|(?i)else|(?i)eval|(?i)floatval|(?i)qval|(?i)uval|(?i)fval|(?i)feval|(?i)fhval|(?i)hval|(?i)try|(?i)tryp|(?i)trysrc|(?i)trysrv|(?i)argn|(?i)argn\d+|(?i)args|(?i)argo|(?i)argv|(?i)strcmp|(?i)strcmpi|(?i)strindexof|(?i)strlen|(?i)strmatch|(?i)strregex|(?i)flags|(?i)button|(?i)buttontileart|(?i)checkbox|(?i)checkertrans|(?i)croppedtext|(?i)dcroppedtext|(?i)dorigin|(?i)dhtmlgump|(?i)dtext|(?i)dtextentry|(?i)dtextentrylimited|(?i)group|(?i)gumppic|(?i)gumppictiled|(?i)htmlgump|(?i)noclose|(?i)nodispose|(?i)nomove|(?i)page|(?i)radio|(?i)resizepic|(?i)text|(?i)textentry|(?i)textentrylimited|(?i)tilepic|(?i)tilepichue|(?i)tooltip|(?i)xmfhtmlgump|(?i)xmfhtmlgumpcolor|(?i)xmfhtmltok|(?i)argchk|(?i)argchkid|(?i)argtxt)\b|//(?i)region\b|//(?i)endregion\b", this.RegexCompiledOption);
            this.vSCPKeywordRegex2 = new Regex(@"\b((?i)uid|(?i)src|(?i)tag|(?i)tag0|(?i)ctag|(?i)ctag0|(?i)var|(?i)var0|(?i)local|(?i)float|(?i)ref\d+|(?i)def|(?i)def0|(?i)act|(?i)targ|(?i)serv|(?i)new|(?i)cont|(?i)list|(?i)obj|(?i)topobj|(?i)account|(?i)region|(?i)sector|(?i)link|(?i)findlayer|(?i)findcont|(?i)findid|(?i)findtype|(?i)file|(?i)db)\b", this.RegexCompiledOption);
            this.vSCPKeywordRegex3 = new Regex(@"\-\s(?i)Author\b\:|\-\s(?i)Contributors\b\:|\-\s(?i)Last edited by\b\:|\-\s(?i)Script last updated\b\:|\-\s(?i)Language\b\:|\-\s(?i)Script version\b\:", this.RegexCompiledOption);
            this.vSCPOperatorRegex = new Regex(@"\<|\>|\[|\]|\(|\)|{|}", this.RegexCompiledOption);
            this.vSCPTriggerSymbolRegex = new Regex(@"\@", this.RegexCompiledOption);
        }
        private void vSCPAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            if (!Regex.IsMatch(args.LineText, @"^\s*((?i)[i,ı]f).*((?i)end[i,ı]f)\b[^{]*$"))
            {
                if (Regex.IsMatch(args.LineText, @"^\s*((?i)[i,ı]f)"))
                {
                    args.ShiftNextLines = args.TabLength;
                }
                else if (Regex.IsMatch(args.LineText, @"((?i)end[i,ı]f)\b[^{]*$"))
                {
                    args.Shift = -args.TabLength;
                    args.ShiftNextLines = -args.TabLength;
                }
                else if (Regex.IsMatch(args.PrevLineText, @"^\s*((?i)else[i,ı]f|(?i)el[i,ı]f|[\}\s]*(?i)else)\b[^{]*$") && !Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)"))
                {
                    args.Shift = args.TabLength;
                }
                else if (!Regex.IsMatch(args.LineText, @"^\s*((?i)for).*((?i)endfor)\b[^{]*$"))
                {
                    if (Regex.IsMatch(args.LineText, @"^\s*((?i)for)"))
                    {
                        args.ShiftNextLines = args.TabLength;
                    }
                    else if (Regex.IsMatch(args.LineText, @"((?i)endfor)\b[^{]*$"))
                    {
                        args.Shift = -args.TabLength;
                        args.ShiftNextLines = -args.TabLength;
                    }
                    else if (!Regex.IsMatch(args.LineText, @"^\s*((?i)dorand).*((?i)enddo)\b[^{]*$"))
                    {
                        if (Regex.IsMatch(args.LineText, @"^\s*((?i)dorand)"))
                        {
                            args.ShiftNextLines = args.TabLength;
                        }
                        else if (Regex.IsMatch(args.LineText, @"((?i)enddo)\b[^{]*$"))
                        {
                            args.Shift = -args.TabLength;
                            args.ShiftNextLines = -args.TabLength;
                        }
                        else if (!Regex.IsMatch(args.LineText, @"^\s*((?i)begin).*((?i)end)\b[^{]*$"))
                        {
                            if (Regex.IsMatch(args.LineText, @"^\s*((?i)begin)"))
                            {
                                args.ShiftNextLines = args.TabLength;
                            }
                            else if (Regex.IsMatch(args.LineText, @"((?i)end)\b[^{]*$"))
                            {
                                args.Shift = -args.TabLength;
                                args.ShiftNextLines = -args.TabLength;
                            }
                        }
                    }
                }
            }
        }
    }
}