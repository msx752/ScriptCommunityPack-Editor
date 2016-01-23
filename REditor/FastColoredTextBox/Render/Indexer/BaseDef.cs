using System;
using System.Drawing;
using System.IO;

namespace FastColoredTextBoxNS.Render
{
    //[dialog,function,events]
    //-BaseDefine
    //File: FileLocation
    //Point: IndexOfCommand
    //DateTime: ScpFileLastChangedTime (for refreshing)
    public class BaseDef : IBaseDef
    {
        private string _cmd;
        private CmdDefType _cmdtype;
        private FileInfo _file;
        private Point _rangeofcmd;

        public BaseDef(string newCmd, CmdDefType newCmdDefType)
        {
            Cmd = newCmd;
            CmdType = newCmdDefType;
        }

        public string Cmd { get { return _cmd; } set { _cmd = value; } }

        public CmdDefType CmdType { get { return _cmdtype; } set { _cmdtype = value; } }

        public FileInfo File { get { return _file; } set { _file = value; } }

        public Point RangeOfCommand { get { return _rangeofcmd; } set { _rangeofcmd = value; } }
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