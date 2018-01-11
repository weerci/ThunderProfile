using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace TiProfConsole
{
    public class Profile
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Caption { get; set; }
        public string Status { get; set; }
        public string PathToIni { get; set; }
        private List<string> _PathToProfile = new List<string>();
        public List<string> PathToProfile => _PathToProfile;
        public string SIDType { get; set; }
        public bool Lockout { get; set; }
        private List<string> _Mails = new List<string>();
        public List<string> Mails => _Mails;

        #region Static save and load

        /// <summary>
        /// Загружает профили почтовых
        /// </summary>
        /// <param name="iniFile"></param>
        /// <returns></returns>
        public static List<PostBox> Load()
        {
            List<PostBox> list = new List<PostBox>();

            // Получаем список всех пользователей и выбираем их
            List<Profile> profiles = GetProfiles();

            // Для каждого профиля получаем список его почтовых ящиков
            GetProfilesPostBox(profiles);

            // Формируем список полученных ящиков пользователей
            foreach (var item in profiles)
            {
                list.Add(new PostBox
                {
                    UserName = item.Name,// GetUserName(item),
                    Mails = item.Mails
                });
            }

            return list;
        }

        /// <summary>
        /// Найденные почтовые ящики сохраняются по пути заданному в настройках
        /// </summary>
        /// <param name="list">Список ящиков</param>
        public static void Save(List<PostBox> list)
        {
            if (!Directory.Exists(Setter.PATH_TO_SAVE))
                Directory.CreateDirectory(Setter.PATH_TO_SAVE);

            foreach (var item in list)
            {
                File.WriteAllLines(Setter.PATH_TO_SAVE + $"\\{item.UserName}.csv", item.Mails.Select(n => item.UserName + ";" + n), Encoding.Default);
            }

        }

        #endregion

        #region Helper

        private static string GetUserName(Profile profile)
        {
            return profile.FullName == "" ? profile.Name : profile.FullName;
        }

        private static void GetProfilesPostBox(List<Profile> profiles)
        {
            ToLog.Item.Write("");
            foreach (var item in profiles)
                try
                {
                    foreach (var p in item.PathToProfile)
                        try
                        {
                            foreach (var mail in File.ReadAllLines(p).Where(n => n.Contains(".useremail")))
                                item.Mails.Add(mail.Split(',')[1].TrimStart(' ', '"').TrimEnd('"', ')', ';').Trim());
                        }
                        catch (Exception)
                        {
                            ToLog.Item.Write($"В профиле '{item.Name}', для пути {p} не найдены почтовые ящики Thunderbird");
                        }
                }
                catch (Exception )
                {
                    ToLog.Item.Write($"В профиле '{item.Name}' не найдены почтовые ящики Thunderbird");
                }
        }

        private static List<Profile> GetProfiles()
        {
            List<Profile> list = new List<Profile>();

            // Получаем все профили windows
            List<Profile> allusers = GetAllUsers();

            // Выбираем нужные
            foreach (var item in allusers)
            {
                if (item.Status != "OK") continue;
                if (item.SIDType != "SidTypeUser(1)") continue;
                if (item.Lockout) continue;

                string pathToIni = String.Format(Setter.PATH_TO_PROFILE, item.Name);
                if (!File.Exists(pathToIni)) continue;

                item.PathToIni = pathToIni;
                GetPathToProfile(item);
                list.Add(item);
            }
            ToLog.Item.Write("Список выбранных пользователей:", list
                .Select(n => $"Name='{n.Name}', Caption='{n.Caption}', FullName='{n.FullName}', Status='{n.Status}', " +
                $"SIDType='{n.SIDType}', Lockout='{n.Lockout}', Profile.ini='{n.PathToIni}', " +
                $"Path to profile={n.PathToProfile.Aggregate((i, next)=>i + ";  " + next)}").ToArray());

            return list;
        }

        // Из файла настроек профиля Profile.ini вытаскивает список путей к месту хранения профиля
        private static void GetPathToProfile(Profile profile)
        {
            profile.PathToProfile.Clear();
            try
            {
                var path = File.ReadAllLines(profile.PathToIni).Where(n => n.Contains("Path=")).Select(n => n.Split('=')[1]);
                foreach (var item in path)
                {
                    if (item.Contains(':'))
                        profile.PathToProfile.Add(item + "\\prefs.js");
                    else
                        // Получаем директорию ini файла профиля и к ней прибавляем относительный путь
                        profile.PathToProfile.Add(Path.GetDirectoryName(profile.PathToIni) + $"\\{item}\\prefs.js");
                }
            }
            catch (Exception e)
            {
                ToLog.Item.Write($"Ошибка получения пути из ini файла профиля: '{profile.Name}'");
            }
        }

        private static List<Profile> GetAllUsers()
        {
            List<Profile> list = new List<Profile>();
            ObjectQuery oQuery = new ObjectQuery("SELECT * FROM Win32_UserAccount");
            ManagementObjectSearcher mgmtSearch = new ManagementObjectSearcher(oQuery);
            foreach (ManagementObject item in mgmtSearch.Get())
                list.Add(new Profile
                {
                    Name = item["Name"].ToString(),
                    Caption = item["Caption"].ToString(),
                    FullName = item["FullName"].ToString(),
                    Status = item["Status"].ToString(),
                    SIDType = Setter.SIDType(Convert.ToInt32(item["SIDType"])),
                    Lockout = Convert.ToBoolean(item["Lockout"])
                });

            ToLog.Item.Write("Список всех пользователей:", list
                .Select(n => $"Name='{n.Name}', Caption='{n.Caption}', FullName='{n.FullName}', Status='{n.Status}', SIDType='{n.SIDType}', Lockout='{n.Lockout}'")
                .ToArray());
            return list;
        }

        #endregion
    }

    public class PostBox
    {
        public PostBox()
        {
            Mails = new List<string>();
            UserName = String.Empty;
        }

        // Имя пользователя
        public string UserName { get; set; }

        // Список его почтовых ящиков
        public List<string> Mails { get; set; }
    }

}
