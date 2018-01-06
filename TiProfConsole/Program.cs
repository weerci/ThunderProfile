using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiProfConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Получаем файл с настройками
            IniFile iniFile = IniFile.Load();

            // Загружаем профили
            Profile profile = Profile.Load(iniFile);
            if (profile != null)
                profile.Save();
            else
                Console.WriteLine("Почтовые ящики не найдены");
        }
    }
}
