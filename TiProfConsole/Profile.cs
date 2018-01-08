using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace TiProfConsole
{
    public class Profile
    {
        private IniFile _IniFiles;
        public Profile(IniFile iniFile)
        {
            _IniFiles = iniFile;

            // Получаем путей к папкам пользователей
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(new SelectQuery("Win32_UserAccount"));
            List<Tuple<String, String>> userLocalPath = new List<Tuple<String, String>>();
            foreach (ManagementObject envVar in searcher.Get())
            {
                string pathToMail = $"C:\\Users\\{envVar["Name"]}\\AppData\\Roaming\\Thunderbird\\profiles.ini";
                if (File.Exists(pathToMail))
                    foreach (var item in File.ReadAllLines(iniFile.PathToProfile).Where(n => n.Contains("Path=")).Select(n => n.Split('=')[1]))
                        userLocalPath.Add(new Tuple<string, string>(envVar["Name"].ToString(), item));
            }

            string userName = "";
            foreach (Tuple<String, String> item in userLocalPath)
            {
                try
                {
                    var foundIdentify = File.ReadAllLines(Path.GetDirectoryName(iniFile.PathToProfile) + "\\" + item.Item2 + "\\prefs.js")
                        .Where(n => n.Contains(".useremail"));

                    foreach (var line in foundIdentify)
                    {
                        if (item.Item1 != userName)
                        {
                            userName = item.Item1;
                            IniStruct iniStruct = new IniStruct();
                            iniStruct.UserName = userName;
                            iniStruct.Mails.Add(line.Split(',')[1].TrimStart(' ', '"').TrimEnd('"', ')', ';').Trim());
                            _ListIniStruct.Add(iniStruct);
                        }
                        else
                            _ListIniStruct.Last().Mails.Add(line.Split(',')[1].TrimStart(' ', '"').TrimEnd('"', ')', ';').Trim());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private List<IniStruct> _ListIniStruct = new List<IniStruct>();
        public List<IniStruct> ListIniStruct => _ListIniStruct;

        public static Profile Load(IniFile iniFile)
        {
            try
            {
                return new Profile(iniFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void Save()
        {
            if (!Directory.Exists(_IniFiles.PathToSave))
                Directory.CreateDirectory(_IniFiles.PathToSave);

            foreach (var item in _ListIniStruct)
            {
                File.WriteAllLines(_IniFiles.PathToSave + $"\\{item.UserName}.csv", item.Mails.Select(n=>item.UserName + ";" + n));
            }

        }
    }

    public class IniStruct
    {
        // Имя пользователя
        public string UserName { get; set; }

        // Список его почтовых ящиков
        private List<string> _Mails = new List<string>();
        public List<string> Mails => _Mails;
    }

}
