using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiProfConsole
{
    public class IniFile
    {
        public string PathToIni { get; set; }
        public string PathToSave { get; set; }

        public static IniFile Load()
        {
            // читаем файл настроек

            // если не найден создаем класс, с данными по умолчанию
            if
            return new IniFile
            {
                 PathToIni = PATH_TO_INI,
                 PathToSave = PATH_TO_SAVE
            };

            // пытаемся создать ini файл

        }

        private static string PATH_TO_INI = Directory.GetCurrentDirectory() + "\\TiProf.ini";
        private static string PATH_TO_SAVE = "с:\\mail\\logs";
    }
}
