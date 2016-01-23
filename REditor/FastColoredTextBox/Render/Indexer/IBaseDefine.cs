using System.Drawing;
using System.IO;

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

        CmdDefType CmdType { get; set; }

        FileInfo File { get; set; }

        Point RangeOfCommand { get; set; }
        bool checkFileChanges();
    }
}