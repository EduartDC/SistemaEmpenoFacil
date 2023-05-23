using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para CheckShoppingCart.xaml
    /// </summary>
    public partial class CheckShoppingCart : Window
    {

        public List<Domain.ArticleDomain> cartItems = new List<Domain.ArticleDomain>();
        string instructionRecieved;
        Customer customerForSale = null;

        public CheckShoppingCart(List<Domain.ArticleDomain> articlesDomain, string instruction, Customer customer)
        {
            cartItems = articlesDomain;
            instructionRecieved = instruction;
            if(customer != null)
            {
                customerForSale = new Customer();
                customerForSale = customer;
            }
            InitializeComponent();
            showShoppingItems(cartItems);
            DisableButtonMakeSale();
        }

        private void DisableButtonMakeSale()
        {
            if(instructionRecieved == "checkCart")
            {
                makeSaleButton.IsEnabled = false;
            }
            else
            {
                makeSaleButton.IsEnabled = true;
            }
        }

        private void showShoppingItems(List<Domain.ArticleDomain> articlesList)
        {
            dataGridShoppingCart.Items.Clear();
            foreach(Domain.ArticleDomain article in articlesList)
            {
                dataGridShoppingCart.Items.Add(article);
            }
            getFinalSellingAmount(articlesList);
        }

        private void getFinalSellingAmount(List<Domain.ArticleDomain> articles)
        {
            double finalSellingAmount = 0;
            foreach(Domain.ArticleDomain article in articles)
            {
                finalSellingAmount += article.sellingPrice;
            }
            labelFinalSellingAmount.Content= finalSellingAmount.ToString();
        }

        private void DeleteArticuleButtonEvent(object sender, RoutedEventArgs e)
        {
            Domain.ArticleDomain articleSelected = dataGridShoppingCart.SelectedItem as Domain.ArticleDomain;
            cartItems.Remove(articleSelected);
            showShoppingItems(cartItems);
        }

        private void GoBackButtonEvent(object sender, RoutedEventArgs e)
        {
            
        }

        private void MakeSaleButtonEvent(object sender, RoutedEventArgs e)
        {
            if(cartItems.Count > 0)
            {
                if (customerForSale != null)
                {

                }
                else
                {
                    string message = "No se puede realizar la venta si no se ha seleccionado un cliente";
                    string messageTitle = "No se ha asociado al cliente para la venta";
                    MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                    MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                    MessageBoxResult messageBox;
                    messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                }
            }
            else
            {
                string message = "No se puede realizar la venta si no hay al menos un articulo en el carrito";
                string messageTitle = "No hay articulos en el carrito de compra";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }
        }
    }
}
