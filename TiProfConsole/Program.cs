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
            List<PostBox> list = Profile.Load();
            Profile.Save(list);

            // Сохраняем лог
            ToLog.Item.Save();
        }
    }
}
