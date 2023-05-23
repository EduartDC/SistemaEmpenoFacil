using BusinessLogic;
using DataAcces;
using Domain.BelongingCreation;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para MakeSale.xaml
    /// </summary>
    public partial class MakeSale : Page
    {

        List<Domain.ArticleDomain> articlesDomain = new List<Domain.ArticleDomain>();
        Customer customerForSale = null;

        public MakeSale()
        {
            InitializeComponent();
        }

        private void ConverterImagesFormat(Domain.ArticleDomain  article)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(article.imageOne);
            bitmap.EndInit();
            article.imageConverted = bitmap;
        }

        private void SearchArticleButtonEvent(object sender, RoutedEventArgs e)
        {
            string idArticle = textBoxBarCode.Text;
            Domain.ArticleDomain articleDomain =  BelongingsArticlesDAO.GetBelonging_Article(int.Parse(idArticle));
            ConverterImagesFormat(articleDomain);
            if(articlesDomain != null)
            {
                dataGridArticles.Items.Clear();
                dataGridArticles.Items.Add(articleDomain);
            }
            else
            {
                string message = "No se encontro el articulo solicitado\nIntenta de nuevo o verifica que la informacion es correcta";
                string messageTitle = "Busqueda de articulo";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }

        }

        private void SearchClientButtonEvent(object sender, RoutedEventArgs e)
        {
            string customerCurp = textBoxClientName.Text;
            Customer customer = CustomerDAO.GetCustomerByCURP(customerCurp);

            if(customer.curp == null || customer.curp.Equals(""))
            {
                string message = "No se encontro el cliente solicitado\nIntenta de nuevo o verifica que la informacion es correcta";
                string messageTitle = "Busqueda de cliente";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }
            else{
                dataGridClients.Items.Clear();
                dataGridClients.Items.Add(customer);
            }
        }

        private void AddToCartButtonEvent(object sender, RoutedEventArgs e)
        {
            if(dataGridArticles.SelectedItem == null)
            {
                string message = "Por favor seleccione el articulo que desea añadir al carrito de compra";
                string messageTitle = "Seleccion no valida";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }
            else
            {
                Domain.ArticleDomain articleSelected = dataGridArticles.SelectedItem as Domain.ArticleDomain;
                articlesDomain.Add(articleSelected);
            }
            
        }

        private void CheckCarButtonEvent(object sender, RoutedEventArgs e)
        {
            CheckShoppingCart checkShoppingCart = new CheckShoppingCart(articlesDomain, "checkCart", customerForSale);
            checkShoppingCart.Show();
            articlesDomain = checkShoppingCart.cartItems;
        }

        private void CancelButtonEvent(object sender, RoutedEventArgs e)
        {

        }

        private void MakeSaleButtonEvent(object sender, RoutedEventArgs e)
        {
            CheckShoppingCart checkShoppingCart = new CheckShoppingCart(articlesDomain, "makeSale", customerForSale);
            checkShoppingCart.Show();
        }

        private void SelectClientForSaleEvent(object sender, SelectionChangedEventArgs e)
        {
            if(customerForSale == null)
            {
                customerForSale= new Customer();
                customerForSale = dataGridClients.SelectedItem as Customer;

                string message = "El cliente " + customerForSale.firstName + " " + customerForSale.lastName + "ha sido asociado a la venta";
                string messageTitle = "Cliente asociado a la venta";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }
            else
            {
                try
                {
                    customerForSale = dataGridClients.SelectedItem as Customer;

                    string message = "El cliente " + customerForSale.firstName + " " + customerForSale.lastName + "ha sido asociado a la venta";
                    string messageTitle = "Cliente asociado a la venta";
                    MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                    MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                    MessageBoxResult messageBox;
                    messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                }catch(NullReferenceException)
                {
                    dataGridClients.SelectedItem = null;
                }
            }
        }
    }
}
