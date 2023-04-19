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

namespace View.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : Page
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void itemHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void itemExit_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new CustomerView());
        }

        private void BtmCreateContract(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new CreateContract());
        }

        private void itemSetAside_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new SetAsideView());
        }

        private void itemFindArticles_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new FindArticles());
        }
    }
}
