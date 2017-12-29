using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThunderProfile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (Store.Load() == null)
            {
                Store store = new Store();
                store.PathToMailBoxes = new List<string>();
                store.PathToMailBoxes.Add(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Thunderbird\Profiles");
                Store.Save(store);
            }
            this.DataContext = new MainWindowVM();
        }
    }
}
