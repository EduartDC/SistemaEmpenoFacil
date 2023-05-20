using BusinessLogic;
using BusinessLogic.Utility;
using DataAcces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Sale selectedSale= dataGridSales.SelectedItem as Sale;
                
                SaleDetails saleDetails = new SaleDetails();
                saleDetails.showSaleDetails(selectedSale.idSale);
                this.IsHitTestVisible= false;
                saleDetails.Show();
                dataGridSales.SelectedItem = null;
                this.IsHitTestVisible = true;

            }
            catch(NullReferenceException)
            {
                dataGridSales.SelectedItem= null;
            }

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
                    List<Sale> sales = new List<Sale>();
                    DateTime selectedDate = (DateTime) salesDatePicker.SelectedDate;
                    sales = SaleDAO.GetSales(selectedDate);
                    dataGridSales.Items.Clear();
                    if (sales != null && sales.Count == 0)
                    {
                        string message = "No ha encontrado ninguna venta realizada en la fecha solicitada";
                        string messageTitle = "Busqueda por fecha de venta";
                        MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                        MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                        MessageBoxResult messageBox;
                        messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                    }
                    else
                    {
                        foreach (Sale sale in sales)
                        {
                            dataGridSales.Items.Add(sale);
                        }
                    }
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
                        Sale sale = SaleDAO.GetSaleBySaleCode(int.Parse(saleCodeTextBox.Text));
                        if (sale != null)
                        {
                            dataGridSales.SelectedItem = null;
                            dataGridSales.Items.Clear();
                            dataGridSales.Items.Add(sale);
                        }
                        else
                        {
                            string message = "No se encontro ninguna venta con el codigo: " + saleCodeTextBox.Text;
                            string messageTitle = "Busqueda por codigo de venta incorrecta";
                            MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                            MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                            MessageBoxResult messageBox;
                            messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                        }
                    }
                    else
                    {
                        string message = "Por favor ingresa solo numeros para realizar la busqueda por codigo";
                        string messageTitle = "Busqueda por codigo de venta";
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
