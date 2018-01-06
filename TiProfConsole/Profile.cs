using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiProfConsole
{
    public class Profile
    {

        public Profile(IniFile iniFile)
        {
            var found = File.ReadAllLines(file).Where(n => n.Contains("useremail"));
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
