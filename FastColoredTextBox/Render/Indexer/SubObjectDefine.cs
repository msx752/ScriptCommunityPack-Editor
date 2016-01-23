using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastColoredTextBoxNS.Render.Indexer
{
    public class SubObjectDefine : ObjectDefine
    {
        public SubObjectDefine(string newCmd, CmdDefType newCmdDefType) 
            : base(newCmd, newCmdDefType)
        {
        }

        //[itemdef,chardef]
        //-SubObjectDefine
        //      string[PARENT]: ID=
        //      string: NAME=
        //OR
        //-ObjectDefine
        //      string[PARENT]: DEFNAME=
        //File: FileLocation
        //Point: IndexOfCommand
        public ObjectDefine ID { get; set; }
        public string Name { get; set; }
    }
}
