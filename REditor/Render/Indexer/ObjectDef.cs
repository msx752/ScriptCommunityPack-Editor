using System.Collections.Generic;

namespace FastColoredTextBoxNS.Render
{
    public class ObjectDef : BaseDef
    {
        public ObjectDef(string newCmd, CmdDefType newCmdDefType)
            : base(newCmd, newCmdDefType)
        {
        }

        public List<ObjectDef> Child
        {
            get
            {
                List<IBaseDef> val = ScpIndexer.Commands.FindAll(p => (p is ObjectDef)
                && p != this
                && (p as ObjectDef).Defname == Cmd
                && p.CmdType == this.CmdType);
                List<ObjectDef> newList = new List<ObjectDef>();
                foreach (var item in val)
                    newList.Add(item as ObjectDef);

                return newList;
            }
        }

        public string Defname { get; set; }

        public string Name { get; set; }

        public ObjectDef Parent
        {
            get
            {
                IBaseDef val = ScpIndexer.Commands.Find(p => (p is ObjectDef)
                && p != this
                && (p as ObjectDef).Defname == ParentId
                && p.CmdType == this.CmdType);
                return val as ObjectDef;
            }
        }

        //[itemdef,chardef]
        //-SubObjectDefine
        //      string[PARENT]: ID=
        //      string: NAME=
        //-ObjectDefine
        //      string[PARENT]: DEFNAME=
        //File: FileLocation
        //Point: IndexOfCommand
        public string ParentId { get; set; }
    }
}