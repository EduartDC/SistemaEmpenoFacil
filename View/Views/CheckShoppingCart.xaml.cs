using BusinessLogic;
using BusinessLogic.Utility;
using DataAcces;
using Domain;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para CheckShoppingCart.xaml
    /// </summary>
    public partial class CheckShoppingCart : Window, MessageService
    {

        public List<Domain.ArticleDomain> cartItems = new List<Domain.ArticleDomain>();
        string instructionRecieved;
        DataAcces.Customer customerForSale = null;
        int idSale = 0;

        public CheckShoppingCart(List<Domain.ArticleDomain> articlesDomain, string instruction, DataAcces.Customer customer)
        {
            cartItems = articlesDomain;
            instructionRecieved = instruction;
            if(customer != null)
            {
                customerForSale = new DataAcces.Customer();
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
            this.Close();
        }

        private void MakeSaleButtonEvent(object sender, RoutedEventArgs e)
        {
            if(cartItems.Count > 0)
            {
                if (customerForSale != null)
                {
                    int idSale = 0;
                    int resultOperation;
                    Sale sale = new Sale();
                    sale.subtotal = double.Parse(labelFinalSellingAmount.Content.ToString());
                    sale.total = sale.subtotal - (sale.subtotal * 0.16);
                    sale.saleDate = DateTime.Now;
                    sale.Customer_idCustomer = customerForSale.idCustomer;

                    if (Utilities.ValidateFormat(textBoxDiscount.Text, "[0-9]+"))
                    {
                        int discount = int.Parse(textBoxDiscount.Text);
                        sale.total = sale.total - ((sale.total * discount) / 100);
                        sale.discount = discount.ToString() + "%";
                        (resultOperation, idSale) = SaleDAO.MakeSale(sale);
                        this.idSale = idSale;
                        if (resultOperation == 1 && idSale != 0)
                        {
                            MainWindow mainWindow = null;
                            foreach (Window window in Application.Current.Windows)
                            {
                                if (window is MainWindow)
                                {
                                    mainWindow = window as MainWindow;
                                    break;
                                }
                            }
                            BlurEffect blurEffect = new BlurEffect();
                            blurEffect.Radius = 5;
                            mainWindow.PrimaryContainer.Effect = blurEffect;
                            (App.Current as App)._cashOnHand = 100000;
                            TransactionView newOperation = new TransactionView(OperationType.OPERATION_INCREASECASHREGISTERAMOUNT, double.Parse(labelFinalSellingAmount.Content.ToString()), idSale);
                            newOperation.CommunicacionPages(this);
                            mainWindow.SecundaryContainer.Navigate(newOperation);
                            mainWindow.PrimaryContainer.IsHitTestVisible = false;
                        }
                        else
                        {
                            string message = "No se pudo completar la venta por favor vuelva a intentar o intentelo mas tarde";
                            string messageTitle = "Error inesperado al completar la venta";
                            MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                            MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                            MessageBoxResult messageBox;
                            messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                        }
                    }
                    else
                    {
                        string message = "Introduzca solo numeros enteros para indicar el descuento de un producto\nEn caso de no haber escriba cero";
                        string messageTitle = "Formato de descuento no valido";
                        MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                        MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                        MessageBoxResult messageBox;
                        messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                    }
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

        public void Communication(bool result)
        {
            if (result)
            {
                foreach(Domain.ArticleDomain article in cartItems) 
                {
                    int discount = int.Parse(textBoxDiscount.Text);
                    double finalSellingPrice = (article.sellingPrice - ((article.sellingPrice * discount) / 100));
                    BelongingsArticlesDAO.ModifyBelonging_Article(article.idArticle, idSale, finalSellingPrice);
                }
                string message = "Operacion completada con exito\nSe ha realizado la venta correctamente";
                string messageTitle = "Operacion exitosa";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }
        }

        public void ScanCommunication(ArticleDomain article)
        {
            throw new NotImplementedException();
        }
    }
}
