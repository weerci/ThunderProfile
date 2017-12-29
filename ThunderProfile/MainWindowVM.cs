using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ThunderProfile
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        #region Constructors

        private int _Count;
        public MainWindowVM()
        {
            _Count = getMailBoxes();
            _MailBoxCount = String.Format(Properties.Resources.MailBoxCount, _Count);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Имя приложения
        /// </summary>
        public string Caption => Properties.Resources.AppTItle;

        /// <summary>
        /// Количество найденных ящиков
        /// </summary>
        private string _MailBoxCount;
        public string MailBoxCount { get => _MailBoxCount; set { _MailBoxCount = value; OnPropertyChanged("MailBoxCount"); } }

        private string _MailBoxes;
        public string MailBoxes { get => _MailBoxes; set { _MailBoxes = value; OnPropertyChanged("MailBoxes"); } }

        #endregion

        #region Command

        /// <summary>
        /// Обновление списка созданных почтовых профилей
        /// </summary>
        private RelayCommand _RefreshCommand;
        public RelayCommand RefreshCommand => _RefreshCommand ?? (_RefreshCommand = new RelayCommand(
            obj =>
            {
                try
                {
                    getMailBoxes();
                }
                finally
                {
                    
                }
            }));

        private RelayCommand _AboutBoxCommand;
        public RelayCommand AboutBoxCommand => _AboutBoxCommand ?? (_AboutBoxCommand = new RelayCommand(
            obj =>
            {
                new AboutBox().ShowDialog();
            }));

        #endregion

        #region Helper

        private int getMailBoxes()
        {
            try
            {
                List<String> path = Store.Load().PathToMailBoxes;
                int cnt = 0;
                foreach (var item in path)
                {
                    var txtFiles = Directory.GetFiles(item, "prefs.js", SearchOption.AllDirectories);
                    foreach (var file in txtFiles)
                    {
                        var found = File.ReadAllLines(file).Where(n => n.Contains("useremail"));
                        cnt = found.Count();
                        if (cnt > 0)
                            MailBoxes = found.Select(n => n.Split(',').LastOrDefault().TrimStart(' ', '"').TrimEnd('"', ')', ';').Trim())
                                .Aggregate((n, next) => n + '\n' + next);
                    }
                }
                return cnt;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
