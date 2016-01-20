using System;
using System.ComponentModel;

namespace FastColoredTextBoxNS.Render
{
    public class Keyword
    {
        public Keyword()
        {
        }

        public Keyword(string _keyword, string _parameter, string _comment, string _destination)
        {
            this.Destination = "[ " + _destination + " ]";
            keywinfo(_keyword, _parameter, _comment);
        }

        public Keyword(string _keyword, string _parameter, string _comment)
        {
            keywinfo(_keyword, _parameter, _comment);
        }

        public string Comment { get; set; }

        [DefaultValue(" ")]
        public string Destination { get; set; }

        public string Keywords { get; set; }

        public string Parameters { get; set; }
        public override string ToString()
        {
            this.Destination = this.Destination.Replace(",", "\r\n");
            string[] lst = this.Destination.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            lst[0] = lst[0].Replace("[ [", "[");
            lst[lst.Length - 1] = lst[lst.Length - 1].Replace("] ]", "]");
            string dest = "";
            string valu = "";

            foreach (string item in lst)
            {
                dest = dest + item + "\r\n";
            }

            valu = this.Keywords + Environment.NewLine + this.Parameters + Environment.NewLine + this.Comment + Environment.NewLine + dest;

            valu = valu.Substring(0, valu.Length - 4);
            return valu;
        }

        private void keywinfo(string _keyword, string _parameter, string _comment)
        {
            //this.Keywords = _keyword
            int index = ScriptCommunityPack.keywordsInfo.FindIndex(x => x.Keywords.ToLower() == _keyword.Replace(" ", "").ToLower());
            if (index != -1)
            {
                Keyword kyw = ScriptCommunityPack.keywordsInfo[index];
                if (kyw.Comment == "")
                {
                    ScriptCommunityPack.keywordsInfo[index].Comment = _comment;
                }
                if (kyw.Destination != " ")
                {
                    if (this.Destination != " ")
                    {
                        ScriptCommunityPack.keywordsInfo[index].Destination = ScriptCommunityPack.keywordsInfo[index].Destination + " " + this.Destination;
                    }
                }
                this.Keywords = "x";
                //MessageBox.Show("bu zaten var sıra numarası: " + index);
                return;
            }
            this.Keywords = _keyword.Replace(" ", "");
            this.Parameters = "{  " + _parameter.Replace(",", ", ").Replace(",  ,  ", ", ") + "  }";
            this.Comment = "" + _comment;
        }
    }
}