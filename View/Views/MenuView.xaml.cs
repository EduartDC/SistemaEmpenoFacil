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

namespace View.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : Page
    {
        Staff _staff = new Staff();
        public MenuView()
        {
            InitializeComponent();
            var staff = (App.Current as App)._staffInfo;
            _staff = staff;
            textStaffName.Text = staff.fisrtName + " " + staff.lastName;
        }

        public void adminAccesibility()
        {

            if (!_staff.rol.Equals("Admin"))
            {

                itemOptions.Visibility = Visibility.Collapsed;
            }
            else
                itemOptions.Visibility = Visibility.Visible;

        }
        private void itemHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void itemExit_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)App.Current.MainWindow;
            window.PrimaryContainer.Navigate(new LoginView());
        }

        private void BtmCreateContract(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new CreateContract());
        }

        private void itemNewSetAside_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new SetAsideView());
        }

        private void itemFindArticles_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new FindArticles());
        }

        private void itemGiveProfitToCustomer_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new GiveProfitToCustomer());
        }


        private void GenerateSalesReport_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new GenerateSalesReport());
        }

        private void itemCutOff_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new CutoffOperations());
        }

        private void itemBlackList_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new ConsultBlackList1());
        }
        private void itemOptions_Click(object sender, RoutedEventArgs e)
        {
            ConfigureMetrics configureMetrics = new ConfigureMetrics();
            configureMetrics.ShowDialog();
        }

        private void itemSearchContract_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new SearchContracts());
        }

        private void itemCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new CreateCustomerRecord());
        }
    }
}
