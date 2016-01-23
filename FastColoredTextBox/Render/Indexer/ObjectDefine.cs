using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastColoredTextBoxNS.Render.Indexer
{
    public class ObjectDef : BaseDef
    {
        public ObjectDef(string newCmd, CmdDefType newCmdDefType) 
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

        public string Name { get; set; }
        public ObjectDef Parent { get; set; }
    }
}
