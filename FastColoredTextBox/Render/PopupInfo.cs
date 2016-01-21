using System;
using System.Collections.Generic;

namespace FastColoredTextBoxNS.Render
{
    public class PopupToolTip
    {
        public string Comment { get; set; } = "none";

        public string Name { get; set; } = "";

        public String Parameters { get; set; } = "";
        public List<PropertyTypes> Properties { get; set; } = new List<PropertyTypes>();

        public override string ToString()
        {
            string propList = "";
            for (int i = 0; i < Properties.Count; i++)
                propList += " , " + Properties[i].ToString();

            string newName = Name;
            if (newName.IndexOf("@") != -1)
                newName += "\n";

            if (propList.Length > 0)
                propList = propList.Substring(3);

            string valu = string.Format("{0}\r\n {{ {1} }}\r\n{2}\r\nProperties [ {3} ]", newName, Parameters.Replace(",", ", "), Comment, propList);
            return valu;
        }
    }
}