using BusinessLogic;
using DataAcces;
using Domain;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using System.Xml.Linq;
using View.Properties;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for SetAsideView.xaml
    /// </summary>
    public partial class SetAsideView : Page, MessageService
    {

        ObservableCollection<ArticleDomain> list = new ObservableCollection<ArticleDomain>();
        double _subtotal;
        double _total;
        double _amount;
        double _remaining;
        double _discount;
        public SetAsideView()
        {
            InitializeComponent();
            var date = DateTime.Now;
            labelDateLine.Text = date.AddDays(15).ToString("dd/MM/yyyy");
            for (int i = 10; i <= 90; i += 10)
            {
                comBoxPercentage.Items.Add(i);
            }
            for (int i = 0; i <= 95; i += 5)
            {
                comBoxDiscount.Items.Add(i);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            // Obtener el objeto correspondiente en el DataGrid
            if (btn != null)
            {
                // Obtener la fila correspondiente al botón pulsado
                var row = DataGridRow.GetRowContainingElement(btn);

                // Obtener el objeto correspondiente a la fila
                var item = row.Item;

                // Eliminar el objeto del DataGrid
                if (item != null && tableArticles.Items.Contains(item))
                {

                    list.Remove((ArticleDomain)item);
                }
            }
        }
        private void btnAddArticle_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textCustomerName.Text))
            {
                ErrorManager.ShowWarning("Primero busque un cliente");
            }
            else
            {
                OpenAddArticlePage();
            }

        }
        private void OpenAddArticlePage()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;
            ScanCodeView scan = new ScanCodeView();
            scan.CommunicacionPages(this);
            window.SecundaryContainer.Navigate(scan);
            window.PrimaryContainer.IsHitTestVisible = false;
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            OpenAddCustomerPage();
        }
        private void OpenAddCustomerPage()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;
            window.SecundaryContainer.Navigate(new CreateCustomerRecord());
            window.PrimaryContainer.IsHitTestVisible = false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var curp = textCURP.Text;
            if (!string.IsNullOrEmpty(curp))
            {
                var customerInfo = CustomerDAO.GetCustomerByCURP(curp);
                if (customerInfo.idCustomer != 0)
                {
                    SetInformationCustomer(customerInfo);
                }
            }
        }

        private void SetInformationCustomer(DataAcces.Customer customerInfo)
        {
            if ((bool)customerInfo.blackList)
            {
                ErrorManager.ShowInformation("El cliente esta en lista negra");
            }
            else
            {
                textCustomerName.Text = customerInfo.firstName + " " + customerInfo.lastName;
            }
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            //se crea apartado
            //si no se concreta el pago, se elimina el apartado
            OpenPayPage();
        }
        private void OpenPayPage()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;
            //campos estaticos para pruebas
            (App.Current as App)._cashOnHand = 1000;
            TransactionView newOperation = new TransactionView(OperationType.OPERATION_SETASIDE, 658.50, 0);
            newOperation.CommunicacionPages(this);
            window.PrimaryContainer.IsHitTestVisible = false;
        }
        /// <summary>
        /// Metodo que se encarga de la comunicacion entre las paginas
        /// Se activa cuando se escanea un codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="result"></param>
        public void Communication(ArticleDomain article, bool result)
        {
            if (result)
            {
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        if (article.idArticle != item.idArticle)
                        {
                            list.Add(article);
                            tableArticles.ItemsSource = list;
                            tableArticles.Items.Refresh();
                            _subtotal += article.sellingPrice;
                        }
                        else
                        {
                            ErrorManager.ShowInformation("El articulo ya esta en la lista");
                        }
                    }
                }
                else
                {
                    list.Add(article);
                    tableArticles.ItemsSource = list;
                    tableArticles.Items.Refresh();
                    _subtotal += article.sellingPrice;
                }
            }
            else
            {
                ErrorManager.ShowError("No pagado");
            }
            SetInformation();
        }

        private void SetInformation()
        {
            labelSubTotal.Content = "SubTotal: " + _subtotal;
            labelIva.Content = "IVA(16%): " + (_subtotal * 0.16);
            _total = _subtotal * 1.16;
            labelTotal.Content = "Total: " + _total;
        }

        private void comBoxPercentage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comBoxPercentage.SelectedItem == null && list.Count <= 0)
            {
                ErrorManager.ShowError("No hay articulos en la lista");
            }
            else
            {
                CalculateSetAsideAmount();
            }
        }

        private void CalculateSetAsideAmount()
        {
            string selectedValue = comBoxPercentage.SelectedItem.ToString();
            int selectedPercentage = Convert.ToInt32(selectedValue);
            double percentage = selectedPercentage / 100.0;
            _amount = (_total * percentage);
            _remaining = (_total - (_total * percentage));
            labelPercentage.Content = "Porcentaje de Apartado: " + selectedPercentage + "%";
            labelAmount.Content = "Cantidad para Apartado: " + _amount;
            labelRemaining.Content = "Restante para Liquidar: " + _remaining;
        }

        private void comBoxDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list.Count <= 0)
            {
                ErrorManager.ShowError("No hay articulos en la lista");
            }
            else if (_amount == 0)
            {
                ErrorManager.ShowError("Seleccione un procentaje de apartado");
            }
            else
            {
                CalculateDiscount();

            }
        }

        private void CalculateDiscount()
        {
            string selectedValue = comBoxPercentage.SelectedItem.ToString();
            int selectedPercentage = Convert.ToInt32(selectedValue);
            double percentage = selectedPercentage / 100.0;

            string selectedDiscount = comBoxDiscount.SelectedItem.ToString();
            int selectedDiscountPercentage = Convert.ToInt32(selectedDiscount);

            _discount = selectedDiscountPercentage / 100.0;
            _subtotal = (_subtotal * (1 - _discount));
            _total = _subtotal * 1.16;

            _amount = (_total * percentage);
            _remaining = (_total - (_total * percentage));

            labelSubTotal.Content = "SubTotal: " + _subtotal;
            labelIva.Content = "IVA(16%): " + (_subtotal * 0.16);
            labelTotal.Content = "Total: " + _total;
            labelPercentage.Content = "Porcentaje de Apartado: " + selectedPercentage + "%";
            labelAmount.Content = "Cantidad para Apartado: " + _amount;
            labelRemaining.Content = "Restante para Liquidar: " + _remaining;
        }
    }
}
