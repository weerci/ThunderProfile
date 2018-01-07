using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TiProfConsole
{
    [Serializable]
    public class IniFile
    {
        public IniFile() { }
        private static XmlSerializer formatter = new XmlSerializer(typeof(IniFile));

        public string PathToProfile { get; set; }
        public string PathToSave { get; set; }

        public static IniFile Load()
        {
            IniFile iniFile = new IniFile
            {
                PathToProfile = PATH_TO_PROFILE,
                PathToSave = PATH_TO_SAVE
            };

            try
            {
                using (FileStream fs = new FileStream(PATH_TO_INI, FileMode.Open))
                {
                    return (IniFile)formatter.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                IniFile.Save(iniFile);
                return iniFile;
            }
        }
        public static void Save(IniFile iniFile)
        {
            using (FileStream fs = new FileStream(PATH_TO_INI, FileMode.Create))
            {
                formatter.Serialize(fs, iniFile);
            }
        }

        public static string PATH_TO_PROFILE = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Thunderbird\profiles.ini";
        public static string PATH_TO_INI = AppDomain.CurrentDomain.BaseDirectory + "\\TiProf.ini";
        public static string PATH_TO_SAVE = "C:\\mail\\logs"; // "с:\\mail\\logs";
    }
}
