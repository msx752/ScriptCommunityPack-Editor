using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastColoredTextBoxNS.Render
{
    //[dialog,function,events]
    //-BaseDefine
    //File: FileLocation
    //Point: IndexOfCommand
    //DateTime: ScpFileLastChangedTime (for refreshing)
    public class BaseDef : IBaseDef
    {
        string _cmd;
        CmdDefType _cmdtype;
        private FileInfo _file;
        private Point _rangeofcmd;

        public string Cmd { get { return _cmd; } set { _cmd = value; } }
        public CmdDefType CmdType { get { return _cmdtype; } set { _cmdtype = value; } }
        public FileInfo File { get { return _file; } set { _file = value; } }
        public Point RangeOfCommand { get { return _rangeofcmd; } set { _rangeofcmd = value; } }

        public BaseDef(string newCmd, CmdDefType newCmdDefType)
        {
            Cmd = newCmd;
            CmdType = newCmdDefType;
        }

        public bool checkFileChanges()
        {
            int snc = DateTime.Compare(_file.LastWriteTime, DateTime.Now);
            if (snc == -1)
                return false;
            else if (snc == 1)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return Cmd.Replace("I", "i").Replace("ı", "i").ToLower();
        }
    }
}
