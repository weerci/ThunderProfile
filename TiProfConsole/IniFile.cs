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
                PathToProfile = Setter.PATH_TO_PROFILE,
                PathToSave = Setter.PATH_TO_SAVE
            };

            try
            {
                ToLog.Item.Write($"Считываем настойки из файла: {Setter.PATH_TO_INI}");
                using (FileStream fs = new FileStream(Setter.PATH_TO_INI, FileMode.Open))
                {
                    return (IniFile)formatter.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                ToLog.Item.Write($"Попытка не удалась. Error: {e.Message}");
                ToLog.Item.Write("Устанавливаются данные по умолчанию:");
                ToLog.Item.Write($"PathToProfile = {Setter.PATH_TO_PROFILE}");
                ToLog.Item.Write($"PathToSave = {Setter.PATH_TO_SAVE}");

                IniFile.Save(iniFile);
                return iniFile;
            }
        }
        public static void Save(IniFile iniFile)
        {
            using (FileStream fs = new FileStream(Setter.PATH_TO_INI, FileMode.Create))
            {
                formatter.Serialize(fs, iniFile);
            }
        }

    }
}
