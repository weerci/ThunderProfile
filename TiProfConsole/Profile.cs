using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            // выбираем все доступные профили
            var foundProfile = File.ReadAllLines(iniFile.PathToProfile)
                .Where(n => n.Contains("Path="))
                .Select(n => n.Split('=')[1]);

            //
            foreach (var item in foundProfile)
            {
                var foundIdentify = File.ReadAllLines(Path.GetDirectoryName(iniFile.PathToProfile) + "\\" + item + "\\prefs.js")
                    .Where(n => n.Contains(".fullName") || n.Contains(".useremail"));
                foreach (var line in foundIdentify)
                {
                    if (line.Contains("fullName"))
                        _ListIniStruct.Add(new IniStruct { UserName = line.Split(',')[1].TrimStart(' ', '"').TrimEnd('"', ')', ';').Trim() });
                    else
                        _ListIniStruct.Last().Mail = line.Split(',')[1].TrimStart(' ', '"').TrimEnd('"', ')', ';').Trim();
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
            catch (Exception)
            {
                return null;
            }
        }

        public void Save()
        {
            if (!Directory.Exists(_IniFiles.PathToSave))
                Directory.CreateDirectory(_IniFiles.PathToSave);

            File.WriteAllLines(_IniFiles.PathToSave + "\\UserName.csv", _ListIniStruct.Select(n => n.UserName + ";" + n.Mail));
        }
    }

    public class IniStruct
    {
        public string UserName { get; set; }
        public string Mail { get; set; }
    }

}
