using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
                        string mat1 = @"(.*?(:).*?(:).*?(:).*?(:).*?(:).*?(:).*?)";
                        Regex rg1 = new Regex(mat1, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        bool isCorrect = rg1.IsMatch(currLine);
                        //define
                        //define value
                        //id
                        //name
                        //defname
                        //line
                        //selected chars count
                        if (isCorrect)
                        {
                            string[] definatin = currLine.Split(':');
                            IBaseDef valuedef = null;
                            CmdDefType deftyp = (CmdDefType)Enum.Parse(typeof(CmdDefType), definatin[0], true);
                            if (deftyp != CmdDefType.UNDEFINED)
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
                    if (opf.ShowDialog() != DialogResult.OK)
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

                string regex_define = @"(?<range>(\[\w+\s+\w+(\s+\w+)?\]))";//[ITEMDEF I_ITEM]
                string regex_id = @"(?<range>(ID(\s|)=(\s|)([a-z0-9_]+)))";//ID = I_SWORD94 89384 _
                string regex_name = @"(?<range>(NAME(\s|)=(\s|)([a-z0-9_( )]+)(\s|)))";//NAME = 33FULL SPELLBOOK  _
                string regex_defname = @"(?<range>(DEFNAME(\s|)=(\s|)([a-z0-9_( )]+)(\s|)))";//DEFNAME =  _

                Regex regx1 = new Regex(regex_define + "|" + regex_id + "|" + regex_name + "|" + regex_defname + "|(\r\n)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                for (int i = 0; i < FileList.Length; i++)
                {
                    List<string> commandList = new List<string>();
                    FileInfo fif = new FileInfo(FileList[i]);
                    StreamReader sr = new StreamReader(fif.FullName, Encoding.UTF8);
                    commandList.Add(string.Format("LastWriteTime:{0}", fif.LastWriteTime.Ticks));
                    //////////////////////////////////////////////////////////////////////////////
                    string text = sr.ReadToEnd();
                    CmdDefType defType0 = CmdDefType.UNDEFINED;
                    int currentMethodLine0 = 1;
                    string Define0 = "";
                    string Id0 = "";
                    string Name0 = "";
                    string Defname0 = "";
                    string Cmd0 = "";
                    int before_line = 0;
                    int before_lenght = 0;
                    foreach (Match r1 in regx1.Matches(text))
                    {
                        if (r1.Value == "\r\n")
                        {
                            currentMethodLine0++;
                            continue;
                        }
                        string value = r1.Value.Replace("\r", "");
                        Regex rg1 = new Regex(regex_define, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        bool isDefine = rg1.IsMatch(value);
                        Regex rg4 = new Regex(regex_id, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        bool isID = rg4.IsMatch(value);
                        Regex rg5 = new Regex(regex_name, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        bool isName = rg5.IsMatch(value);
                        Regex rg6 = new Regex(regex_defname, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        bool isDefname = rg6.IsMatch(value);

                        if (isDefine)
                        {
                            if (defType0 == CmdDefType.ITEMDEF || defType0 == CmdDefType.CHARDEF)
                            {
                                //deftype:id:name:defname::indexNumber:indexLenght
                                commandList.Add(string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}", defType0.ToString(), Cmd0, Id0, Name0, Defname0, before_line, before_lenght));
                                Id0 = "";
                                Name0 = "";
                                Defname0 = "";
                            }
                            before_line = currentMethodLine0;
                            before_lenght = r1.Length;
                            Define0 = value.Trim('[').Trim(']').ToLower().Replace("ı", "i");
                            string cType = Define0.Split(' ')[0];//defname
                            switch (cType)
                            {
                                case "itemdef":
                                    defType0 = CmdDefType.ITEMDEF;
                                    break;
                                case "chardef":
                                    defType0 = CmdDefType.CHARDEF;
                                    break;
                                case "dialog":
                                    defType0 = CmdDefType.DIALOG;
                                    break;
                                case "events":
                                    defType0 = CmdDefType.EVENTS;
                                    break;
                                case "function":
                                    defType0 = CmdDefType.FUNCTION;
                                    break;
                                default:
                                    defType0 = CmdDefType.UNDEFINED;
                                    break;
                            }
                            Cmd0 = Define0.Split(' ')[1];//defname's value

                            if (defType0 == CmdDefType.FUNCTION || defType0 == CmdDefType.DIALOG || defType0 == CmdDefType.EVENTS)
                            {
                                //deftype:id:name:defname::indexNumber:indexLenght
                                commandList.Add(string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}", defType0.ToString(), Cmd0, "", "", "", currentMethodLine0, r1.Length));
                                continue;
                            }
                        }
                        else if (isID)
                            Id0 = value.Split('=')[1];
                        else if (isName)
                            Name0 = value.Split('=')[1];
                        else if (isDefname)
                            Defname0 = value.Split('=')[1];
                        else
                        {
                            //UNDEFINED VALUE
                        }
                    }
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
            catch (Exception ef)
            {
                return false;
            }
            return true;
        }
    }
}