using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastColoredTextBoxNS.Render
{
    public static class ScpIndexer
    {
        //list all files
        //load all defination
        //find base define
        //find sub define
        //compare and add parent define
        static string pathIndex = Path.Combine(Application.StartupPath, "scp_index");
        static string pathScripts = Path.Combine(Application.StartupPath, "scripts");
        static List<IBaseDef> Commands = new List<IBaseDef>();

        public static bool UpdateScpCmd()
        {
            try
            {
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
                        iLine++;
                        if (currLine.ToLower().StartsWith("["))
                        {
                            string Define = currLine.Trim('[').Trim(']').ToLower().Replace("ı", "i");
                            if (Define.Split(' ').Length == 2)
                            {
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
                                            commandList.Add(string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}", defType.ToString(), Cmd, Id, Name, Defname, iLine, (cType.Length + Cmd.Length + 1)));
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

        public static void LoadScpCmd()
        {
            if (UpdateScpCmd())
            {
                
            }
        }
    }
}
