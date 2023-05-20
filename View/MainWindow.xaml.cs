using BusinessLogic;
using DataAcces;
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
using View.Views;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowState = WindowState.Maximized;
<<<<<<< HEAD
            PrimaryContainer.NavigationService.Navigate(new CheckSalesRecords());
=======
>>>>>>> 09aecce4aba545e277b6176d78454a5d1e80abbb
            PrimaryContainer.NavigationService.Navigate(new LoginView());
            this.MinWidth = 1366;
            this.MinHeight = 750;
        }

    }
}
