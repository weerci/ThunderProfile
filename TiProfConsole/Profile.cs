using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiProfConsole
{
    public class Profile
    {

        private IniFile _IniFile;
        public Profile(IniFile iniFile)
        {
            _IniFile = iniFile;
        }

        private List<IniStruct> _ListIniStruct { get; set; }

        public static Profile Load(IniFile iniFile)
        {
            return new Profile(iniFile);
        }

        public static void Save(Profile profile)
        {
            
        }
    }

    internal class IniStruct
    {
        public string UserName { get; set; }
        public string Mail { get; set; }
    }

}
