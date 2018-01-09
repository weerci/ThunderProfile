using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiProfConsole
{
    public static class Setter
    {
        public static string PATH_TO_PROFILE = "C:\\Users\\{0}\\AppData\\Roaming\\Thunderbird\\profiles.ini";
        public static string PATH_TO_INI = AppDomain.CurrentDomain.BaseDirectory + "\\TiProf.ini";
        public static string PATH_TO_LOG = AppDomain.CurrentDomain.BaseDirectory + "\\TiProf.log";
        public static string PATH_TO_SAVE = "C:\\mail\\logs";

        public static string SIDType(int typeId)
        {
            switch (typeId)
            {
                case 1: return "SidTypeUser(1)";
                case 2: return "SidTypeGroup(2)";
                case 3: return "SidTypeDomain(3)";
                case 4: return "SidTypeAlias(4)";
                case 5: return "SidTypeWellKnownGroup(5)";
                case 6: return "SidTypeDeletedAccount(6)";
                case 7: return "SidTypeInvalid(7)";
                case 8: return "SidTypeUnknown(8)";
                case 9: return "SidTypeComputer(9)";
                default:
                return "Unknown";
            }
        }
    }
}
