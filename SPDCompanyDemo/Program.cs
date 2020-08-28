using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SPDCompanyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //string value = OperateIniFile.ReadIniData("Server", "Release", "", @"config\DBServer.ini");

            //Config config = new Config(@"config\DBServer.ini");

            IniConfigHelper config = new IniConfigHelper(@"config\DBServer.ini");
            IniConfigHelper.Section server = config["Server"];
            IniConfigHelper.Section db = config["DB"];
            IniConfigHelper.Section user = config["User"];
            server["Test"] = "192.168.100.56";
            db["Devlop"] = "sqlite3";

            IniConfigHelper.Section section = new IniConfigHelper.Section();
            section.KeyValuePairs.Add("test", "127.0.0.1");
            config["Server"] = section;

            Console.WriteLine("---------------------------------------");
            foreach (var item in server.KeyValuePairs)
            {
                Console.WriteLine(item.Key + ":" + item.Value);
            }

            Console.WriteLine("---------------------------------------");
            foreach (var item in db.KeyValuePairs)
            {
                Console.WriteLine(item.Key + ":" + item.Value);
            }

            Console.WriteLine("---------------------------------------");
            foreach (var item in user.KeyValuePairs)
            {
                Console.WriteLine(item.Key + ":" + item.Value);
            }

            Console.ReadKey();

        }
    }

    public class OperateIniFile
    {
        #region API函数声明

        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);


        #endregion

        #region 读Ini文件

        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion

        #region 写Ini文件

        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
                if (OpStation == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion
    }

    public class IniConfigHelper
    {
        public IniConfigHelper(string fileName)
        {
            this.fileName = fileName;
            if (!File.Exists(fileName))
            {
                throw new Exception("this file doesn't exist!");
            }
            Setctions = new Dictionary<string, Section>();
            using (StreamReader sw = new StreamReader(fileName))
            {
                string line = string.Empty;
                line = sw.ReadLine();
                while (!sw.EndOfStream)
                {
                    if (!string.IsNullOrEmpty(line) && line.StartsWith("["))
                    {
                        if (line.StartsWith(";"))//;为ini文件里面的注释语句
                            continue;
                        int index = line.IndexOf("[") + 1;
                        string sectionName = line.Substring(index, line.IndexOf("]") - index);
                        Section section = new Section();
                        while (!string.IsNullOrEmpty(line = sw.ReadLine()) && !line.StartsWith("["))
                        {
                            if (line.StartsWith(";"))
                                continue;
                            string[] keyValues = line.Split('=');
                            if (keyValues.Length != 2)
                                continue;
                            section.KeyValuePairs.Add(keyValues[0], keyValues[1]);
                        }
                        Setctions.Add(sectionName, section);
                    }
                    else
                    {
                        line = sw.ReadLine();
                    }
                }
                sw.Close();
            }
        }

        string fileName = string.Empty;

        public Section this[string key]
        {
            get
            {
                if (!Setctions.ContainsKey(key))
                {
                    return null;
                }
                return Setctions[key];
            }
            set
            {
                Section section = value;
                if (section == null)
                {
                    throw new NullReferenceException();
                }
                foreach (var item in section.KeyValuePairs)
                {
                    OperateIniFile.WriteIniData(key, item.Key, item.Value, fileName);
                }
            }
        }


        public IDictionary<string, Section> Setctions { set; get; }

        public class Section
        {
            public Section()
            {
                KeyValuePairs = new Dictionary<string, string>();
            }
            public IDictionary<string, string> KeyValuePairs { set; get; }

            public string this[string key]
            {
                get
                {
                    if (!KeyValuePairs.ContainsKey(key))
                    {
                        return null;
                    }
                    return KeyValuePairs[key];
                }
                set
                {
                    if (!KeyValuePairs.ContainsKey(key))
                    {
                        throw new Exception("not exsit key " + key);
                    }
                    KeyValuePairs[key] = value;
                }
            }
        }

    }


}
