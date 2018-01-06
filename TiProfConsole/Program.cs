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

            Profile profile = Profile.Load(iniFile);
            if (profile == null)
            {
                Console.WriteLine("Почтовые ящики не найдены");
            }
        }
    }
}
