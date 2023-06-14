using BusinessLogic;
using BusinessLogic.Utility;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View.Properties;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : Page
    {
        Staff _staff = new Staff();
        NewLog _log = new NewLog();
        public MenuView()
        {
            InitializeComponent();
            var staff = (App.Current as App)._staffInfo;
            _staff = staff;
            textStaffName.Text = staff.fisrtName + " " + staff.lastName;
            adminAccesibility();
        }

        public void adminAccesibility()
        {

            if (_staff.rol.Equals("Cajero"))
            {

                itemConfiguration.Visibility = Visibility.Collapsed;
                itemRegisterStaff.Visibility = Visibility.Collapsed;
                itemOperationCashRegister.Visibility = Visibility.Collapsed;
                itemReports.Visibility = Visibility.Collapsed;
                itemModifyStaff.Visibility = Visibility.Collapsed;
            }
            else if (_staff.rol.Equals("Gerente"))
            {
                itemRegisterStaff.Visibility = Visibility.Collapsed;
                itemModifyStaff.Visibility = Visibility.Collapsed;
                itemConfiguration.Visibility = Visibility.Collapsed;
            }
        }
        private void itemHome_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = null;
        }

        private void itemExit_Click(object sender, RoutedEventArgs e)
        {
            if (_staff.rol.Equals("Cajero"))
            {
                if ((App.Current as App)._staffShift)
                {
                    ErrorManager.ShowInformation("Para cerrar sesion, es necesario que primero realizes un corte de caja.");
                }
                else
                {
                    var window = (MainWindow)App.Current.MainWindow;
                    window.PrimaryContainer.Navigate(new LoginView());
                }
            }
            else
            {
                var window = (MainWindow)App.Current.MainWindow;
                window.PrimaryContainer.Navigate(new LoginView());
            }
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
            try
            {
                configureMetrics.ShowDialog();
            }
            catch (InvalidOperationException ex)
            {
                _log.Add(ex.ToString());
            }
        }

        private void itemSearchContract_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new SearchContracts());
        }

        private void itemCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;
            window.SecundaryContainer.Navigate(new CreateCustomerRecord());
            window.PrimaryContainer.IsHitTestVisible = false;
        }

        private void ItemRegisterStaff_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new RegisterStaff());
        }
        private void itemSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new SearchCustomer());
        }
        private void ItemOperationCashRegister_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            window.ThirdContainer.Navigate(new CashRegisterOperations());


        }
        private void itemSearchBelongings_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new SearchBelonging());
        }
        private void itemPaySetAside_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new PayOffSetAside());
        }

        private void ItemModifyStaff_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new CheckStaffList());
        }

        private void ItemCheckSales_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new CheckSalesRecords());
        }

        private void ItemMakeSale_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new MakeSale());
        }

        private void itemGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new GenerateReortOSparatedIms());
        }

        private void GenerateContractReport_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new GenerateReportContracts());
        }
        private void itemNewArticles_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new ArticlesSellingPrice());
        }
        private void itemPrintLabel_Click(object sender, RoutedEventArgs e)
        {
            Container.NavigationService.Navigate(new PrintPriceLabel());
        }
    }
}
