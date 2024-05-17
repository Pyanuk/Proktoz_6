using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calen
{

    public partial class MainWindow : Window
    {
        public static string sohrnJson = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Sofia.json";

        public datacalendarviv datacalendarviv = new datacalendarviv();
        public MainWindow()
        {
            InitializeComponent(); 

            Content = datacalendarviv;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Filecs.Serialization(DayItems.days, sohrnJson);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (Content is datacalendarviv calenviv)
            {
                datacalendarviv.CreateUserControlsBasedOnDays();
            }
        }
    }
}


