using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ThunderProfile
{
    [Serializable]
    public class Store
    {
        public Store() { }

        /// <summary>
        /// Пути к профилям пользователя
        /// </summary>
        public List<string> PathToMailBoxes { get; set; }


        private static XmlSerializer formatter = new XmlSerializer(typeof(Store));

        /// <summary>
        /// Сохраняет проекты на локальном диске
        /// </summary>        
        public static void Save(Store store)
        {
            using (FileStream fs = new FileStream(FILE_SET, FileMode.Create))
            {
                formatter.Serialize(fs, store);
            }
        }

        /// <summary>
        /// Загружает проекты в систему
        /// </summary>
        public static Store Load()
        {
            try
            {
                using (FileStream fs = new FileStream(FILE_SET, FileMode.Open))
                {
                    return (Store)formatter.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string FILE_SET = AppDomain.CurrentDomain.BaseDirectory + "Settings.xml";
    }
}
