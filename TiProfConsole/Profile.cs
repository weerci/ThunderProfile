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

        public Profile(IniFile iniFile)
        {
            // выбираем все доступные профили
            var found = File.ReadAllLines(iniFile.PathToIni)
                .Where(n => n.Contains("Path="))
                .ToString().Split('=').Take(2);

            //
            foreach (var item in collection)
            {

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
            
        }
    }

    public class IniStruct
    {
        public string UserName { get; set; }
        public string Mail { get; set; }
    }

}
