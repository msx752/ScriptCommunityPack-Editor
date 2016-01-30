using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FastColoredTextBoxNS.Render
{
    public static class ScpIndexer
    {
        public static List<IBaseDef> Commands = new List<IBaseDef>();

        //list all files
        //load all defination
        //find base define
        //find sub define
        //compare and add parent define
        private static string pathIndex = Path.Combine(Application.StartupPath, "scp_index");

        private static string pathScripts = Path.Combine(Application.StartupPath, "scripts");

        public static void LoadScpCmd()
        {
            bool bl = UpdateScpCmd();//if filechanged call
            if (bl)
            {
                ScriptCommunityPack.fileScpCommands.Clear();
                //Commands.Clear();
                string[] FileList = Directory.GetFiles(pathIndex, "*.scp", SearchOption.AllDirectories);
                foreach (string item in FileList)
                {
                    FileInfo fif = new FileInfo(item);
                    StreamReader sr = new StreamReader(fif.FullName, Encoding.UTF8);
                    string currLine;
                    sr.ReadLine();
                    sr.ReadLine();
                    while ((currLine = sr.ReadLine()) != null)
                    {
                        //define
                        //define value
                        //id
                        //name
                        //defname
                        //line
                        //selected chars count
                        string[] definatin = currLine.Split(':');
                        IBaseDef valuedef = null;
                        CmdDefType deftyp = (CmdDefType)Enum.Parse(typeof(CmdDefType), definatin[0], true);
                        if (deftyp != CmdDefType.NONE)
                        {
                            if (deftyp == CmdDefType.CHARDEF || deftyp == CmdDefType.ITEMDEF)
                            {
                                if (definatin[2] != "")//is children
                                {
                                    valuedef = new ObjectDef(definatin[1], deftyp);
                                    (valuedef as ObjectDef).ParentId = definatin[2];
                                    (valuedef as ObjectDef).Name = definatin[3];
                                }
                                else if (definatin[4] != "")//is parent
                                {
                                    valuedef = new ObjectDef(definatin[1], deftyp);
                                    (valuedef as ObjectDef).Defname = definatin[4];
                                }
                                else//is parent (base parent is hardcoding)
                                {
                                    valuedef = new ObjectDef(definatin[1], deftyp);
                                    (valuedef as ObjectDef).Defname = definatin[4];
                                }
                            }
                            else
                            {
                                valuedef = new BaseDef(definatin[1], deftyp);
                            }
                            valuedef.File = new FileInfo(item.Replace(pathIndex, pathScripts));
                            valuedef.RangeOfCommand = new System.Drawing.Point(int.Parse(definatin[6]), int.Parse(definatin[5]));
                        }
                        if (valuedef != null)
                        {
                            //Commands.Add(valuedef);
                            MethodAuto newScpCmd = new MethodAuto(valuedef.Cmd);
                            string define = string.Format("[{0} {1}]", valuedef.CmdType.ToString(), valuedef.Cmd);
                            if (valuedef is ObjectDef)
                            {
                                newScpCmd.ImageIndex = 8;
                                newScpCmd.ToolTipTitle = define;
                                string children = "";
                                foreach (ObjectDef item2 in (valuedef as ObjectDef).Child)
                                    children += "," + item2;
                                if (children.Length > 0)
                                    children = children.Substring(1);
                                string titletext = string.Format("{0}\r\n{1}\r\n File: {2}    {{Line: {3}}}\r\nParent: {4}\r\nChild: {5}", (valuedef as ObjectDef).Name, "", valuedef.File.Name, valuedef.RangeOfCommand.Y, (valuedef as ObjectDef).ParentId, children);
                                newScpCmd.ToolTipText = titletext;
                                ScriptCommunityPack.fileScpCommands.Add(newScpCmd);
                            }
                        }
                    }
                }
            }
        }

        //if filechanged call
        public static bool UpdateScpCmd()
        {
            try
            {
                ;
                if (MessageBox.Show("Please select Scripts Folder\n or \nif you have \"Startup Folder\\scripts\" press Cancel", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    pathScripts = Path.Combine(Application.StartupPath, "scripts");//DEBUG MOD FAST LOAD
                }
                else
                {
                    FolderBrowserDialog opf = new FolderBrowserDialog();
                    if (opf.ShowDialog()!= DialogResult.OK)
                    {
                        MessageBox.Show("please select correct folder");
                        return false;
                    }
                    else
                    {
                        pathScripts = opf.SelectedPath;
                    }
                }

                if (!Directory.Exists(pathScripts))
                {
                    MessageBox.Show("error:: scripts folder couldn't find. if you don't have you can download here:\r\nhttps://github.com/MSAlih1/Scripts");
                    return false;
                }
                string[] FileList = Directory.GetFiles(pathScripts, "*.scp", SearchOption.AllDirectories);
                for (int i = 0; i < FileList.Length; i++)
                {
                    List<string> commandList = new List<string>();
                    FileInfo fif = new FileInfo(FileList[i]);
                    StreamReader sr = new StreamReader(fif.FullName, Encoding.UTF8);
                    commandList.Add(string.Format("LastWriteTime:{0}", fif.LastWriteTime.Ticks));
                    string currLine;
                    int iLine = -1;
                    while ((currLine = sr.ReadLine()) != null)
                    {
                        int currentMethodLine = 0;
                        iLine++;
                        if (currLine.ToLower().StartsWith("["))
                        {
                            string Define = currLine.Trim('[').Trim(']').ToLower().Replace("ı", "i");
                            if (Define.Split(' ').Length == 2)
                            {
                                currentMethodLine = iLine + 1;//method line number
                                string cType = Define.Split(' ')[0];//defname
                                CmdDefType defType;
                                switch (cType)
                                {
                                    case "itemdef":
                                        defType = CmdDefType.ITEMDEF;
                                        break;

                                    case "chardef":
                                        defType = CmdDefType.CHARDEF;
                                        break;

                                    case "dialog":
                                        defType = CmdDefType.DIALOG;
                                        break;

                                    case "events":
                                        defType = CmdDefType.EVENTS;
                                        break;

                                    case "function":
                                        defType = CmdDefType.FUNCTION;
                                        break;

                                    default:
                                        defType = CmdDefType.NONE;
                                        break;
                                }
                                if (defType != CmdDefType.NONE)
                                {
                                    string Cmd = Define.Split(' ')[1];//defname's value
                                    string Id = "";
                                    string Name = "";
                                    string Defname = "";
                                    while ((currLine = sr.ReadLine()) != null)
                                    {
                                        iLine++;
                                        Define = currLine.ToLower().Replace("ı", "i");
                                        if (Define.Replace(" ", "").StartsWith("[") || defType == CmdDefType.FUNCTION || defType == CmdDefType.DIALOG || defType == CmdDefType.EVENTS)
                                        {
                                            //deftype:id:name:defname::indexNumber:indexLenght
                                            commandList.Add(string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}", defType.ToString(), Cmd, Id, Name, Defname, currentMethodLine, (cType.Length + Cmd.Length + 1)));
                                            break;
                                        }
                                        else if (Define.StartsWith("id="))
                                            Id = currLine.Replace(" ", "").Substring(3);
                                        else if (Define.StartsWith("name="))
                                            Name = currLine.Substring(5);
                                        else if (Define.StartsWith("defname="))
                                            Defname = currLine.Substring(8);
                                    }
                                }
                            }
                        }
                    }
                    sr.Close();
                    FileInfo newFilePath = new FileInfo(FileList[i].Replace(pathScripts, pathIndex));
                    if (!newFilePath.Directory.Exists)
                        Directory.CreateDirectory(newFilePath.Directory.FullName);
                    if (commandList.Count > 1)
                    {
                        StreamWriter sw = new StreamWriter(newFilePath.FullName, false, Encoding.UTF8);
                        sw.WriteLine("//deftype:id:name:defname::indexNumber:indexLenght:lastchangestime");
                        foreach (string item in commandList)
                            sw.WriteLine(item);
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}