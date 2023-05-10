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

        private void showSales()
        {
            List<String> sales = new List<String>();
            sales.Add("Sale 1");
            sales.Add("Sale 2");
            sales.Add("Sale 3");
            sales.Add("Sale 4");
            sales.Add("Sale 5");
            dataGridSales.ItemsSource = sales;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = dataGridSales.SelectedItem.ToString();
            string message = selectedItem;
            string messageTitle = "Objeto seleccionado";
            MessageBoxButton messageBoxButton = MessageBoxButton.OK;
            MessageBoxImage messageBoxImage = MessageBoxImage.Information;
            MessageBoxResult messageBox;
            messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);

        }

        private void EventSaleCodeRadioButton(object sender, RoutedEventArgs e)
        {
            saleCodeTextBox.IsEnabled= true;
            salesDatePicker.IsEnabled = false;
            salesDatePicker.Visibility = Visibility.Hidden;
            saleCodeTextBox.Visibility = Visibility.Visible;
        }

        private void EventSaleDateRadioButton(object sender, RoutedEventArgs e)
        {
            saleCodeTextBox.IsEnabled = false;
            salesDatePicker.IsEnabled = true;
            salesDatePicker.Visibility = Visibility.Visible;
            saleCodeTextBox.Visibility = Visibility.Hidden;
        }

        private void EventSearchButton(object sender, RoutedEventArgs e)
        {
            if(salesDatePicker.IsEnabled)
            {
                if (salesDatePicker.SelectedDate == null)
                {
                    string message = "No ha seleccionado ninguna fecha, por favor seleccione una fecha para continuar";
                    string messageTitle = "Busqueda por fecha de venta";
                    MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                    MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                    MessageBoxResult messageBox;
                    messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                }
                else
                {
                    string message = "Busqueda por fecha de venta realizada correctamente";
                    string messageTitle = "Busqueda por fecha de venta";
                    MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                    MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                    MessageBoxResult messageBox;
                    messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                }
            }
            else
            {
                if (saleCodeTextBox.Text == "")
                {
                    string message = "No se ha detectado ningun codigo de venta por favor escriba uno para continuar";
                    string messageTitle = "Busqueda por codigo de venta incorrecta";
                    MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                    MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                    MessageBoxResult messageBox;
                    messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                }
                else
                {
                    if(Utilities.ValidateInput(saleCodeTextBox.Text))
                    {
                        string message = "Se ha realizado la busqueda por codigo de venta correctamente";
                        string messageTitle = "Busqueda por codigo de venta";
                        MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                        MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                        MessageBoxResult messageBox;
                        messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                    }
                    else
                    {
                        string message = "Por favor no ingrese caracteres especiales como codigo de venta";
                        string messageTitle = "Busqueda por codigo de venta incorrecta";
                        MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                        MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                        MessageBoxResult messageBox;
                        messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                    }
                }
            }
        }
    }
}
