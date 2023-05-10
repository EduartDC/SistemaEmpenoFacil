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
    /// Lógica de interacción para SaleDetails.xaml
    /// </summary>
    public partial class SaleDetails : Page
    {
        Sale sale = null;
        public SaleDetails()
        {
            InitializeComponent();
        }

        private void showSaleDetails(int saleId)
        {
            sale = SaleDAO.GetSaleBySaleCode(saleId);
            labelSaleCode.Content = sale.idSale.ToString();
            labelSaleDate.Content = sale.saleDate.ToString();
            labelFinalSaleAmount.Content = sale.total.ToString();
        }

        private void GoBackButtonEvent(object sender, RoutedEventArgs e)
        {

        }
    }
}
