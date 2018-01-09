using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiProfConsole
{
    public class ToLog
    {
        private StringBuilder _StringBuilder = new StringBuilder();

        public void Save()
        {
            File.WriteAllText(Setter.PATH_TO_LOG, _StringBuilder.ToString());
        }

        public void Write(string message)
        {
            _StringBuilder.Append(message + '\n');
        }

        public void Write(string title, string message)
        {
            _StringBuilder.Append('\n');
            _StringBuilder.Append(title + '\n');
            _StringBuilder.Append(message + '\n');
        }

        public void Write(string title, string[] messages)
        {
            _StringBuilder.Append('\n');
            _StringBuilder.Append(title + '\n');
            foreach (var item in messages)
                _StringBuilder.Append(item + '\n');
        }

        public void Write(string[] messages)
        {
            foreach (var item in messages)
                _StringBuilder.Append(item + '\n');
        }

        #region Constructor

        private static object obj = new Object();
        private ToLog()
        {
            _StringBuilder.Append($"Лог обработки данных почтовых ящиков TiProf {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}\n");
        }
        private static ToLog _ToLog;
        public static ToLog Item
        {
            get
            {
                lock (obj)
                {
                    if (_ToLog == null)
                        _ToLog = new ToLog();
                }
                return _ToLog;
            }
        }

        #endregion
    }
}
