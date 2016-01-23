using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastColoredTextBoxNS.Render
{
    //[itemdef,chardef]
    //-SubObjectDefine
    //      string[PARENT]: ID=
    //      string: NAME=
    //OR
    //-ObjectDefine
    //      string[PARENT]: DEFNAME=
    //File: FileLocation
    //Point: IndexOfCommand


    //[dialog,function,events]
    //-BaseDefine
    //File: FileLocation
    //Point: IndexOfCommand
    //DateTime: ScpFileLastChangedTime (for refreshing)
    public interface IBaseDef
    {
        string Cmd { get; set; }
        FileInfo File { get; set; }
        Range RangeOfCommand { get; set; }
        CmdDefType CmdType { get; set; }
        bool checkFileChanges();
    }
}
