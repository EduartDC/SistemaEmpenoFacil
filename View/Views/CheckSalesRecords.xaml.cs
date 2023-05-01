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
    /// Lógica de interacción para CheckSalesRecords.xaml
    /// </summary>
    public partial class CheckSalesRecords : Page
    {
        public CheckSalesRecords()
        {
            InitializeComponent();
            showSales();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void showSales()
        {
            Label[] sales = { new Label(), new Label(), new Label(), new Label() };
            sales[0].Content = "Prueba de venta 1";
            sales[1].Content = "Prueba de venta 2";
            sales[2].Content = "Prueba de venta 3";
            sales[3].Content = "Prueba de venta 4";
            dataGridSales.ItemsSource = sales;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
