using BusinessLogic;
using BusinessLogic.Utility;
using DataAcces;
using Domain;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
        int _discount;
        double _percentage;
        int _idCustomer;
        int _idSetAside;
        List<ArticlesSetAside> _listArticles;
        SetAside _SetAside;


        public SetAsideView()
        {
            InitializeComponent();
            var date = DateTime.Now;
            textDateLine.Text = date.AddDays(15).ToString("dd/MM/yyyy");
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

            if (btn != null)
            {
                var row = DataGridRow.GetRowContainingElement(btn);
                var item = row.Item;
                if (item != null && tableArticles.Items.Contains(item))
                {
                    list.Remove((ArticleDomain)item);
                    UpdatePage();
                }
            }
        }

        private void UpdatePage()
        {
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    _subtotal += item.sellingPrice;
                }
                string selectedValue = comBoxPercentage.SelectedItem.ToString();
                int selectedPercentage = Convert.ToInt32(selectedValue);
                double percentage = selectedPercentage / 100.0;

                string selectedDiscount = comBoxDiscount.SelectedItem.ToString();
                int selectedDiscountPercentage = Convert.ToInt32(selectedDiscount);
                _discount = selectedDiscountPercentage;
                var discount = selectedDiscountPercentage / 100.0;
                _subtotal = (_subtotal * (1 - discount));
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
            else
            {
                _subtotal = 0;
                _total = 0;
                _amount = 0;
                _remaining = 0;
                labelSubTotal.Content = "SubTotal: ";
                labelIva.Content = "IVA(16%): ";
                labelTotal.Content = "Total: ";
                labelPercentage.Content = "Porcentaje de Apartado: ";
                labelAmount.Content = "Cantidad para Apartado: ";
                labelRemaining.Content = "Restante para Liquidar: ";
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
            try
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
            catch (Exception)
            {
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
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
                _idCustomer = customerInfo.idCustomer;
                textCustomerName.Text = customerInfo.firstName + " " + customerInfo.lastName;
            }
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count == 0)
            {
                ErrorManager.ShowWarning("No hay articulos en la lista");
            }
            else if (string.IsNullOrEmpty(textCustomerName.Text))
            {
                ErrorManager.ShowWarning("Primero busque un cliente");
            }
            else if (comBoxPercentage.SelectedItem == null)
            {
                ErrorManager.ShowWarning("Seleccione un porcentaje de apartado");
            }
            else
            {
                CreateSetAside();
                OpenPayPage();
            }

        }

        private void CreateSetAside()
        {
            var date = DateTime.Now;
            var dateExpiration = date.AddDays(15);
            var idCustomer = _idCustomer;
            var totalAmount = _total;
            var percentage = _percentage.ToString();
            var remaining = _remaining;
            var stateSetAside = StatesAside.PENDING_ASIDE;

            var newSetAside = new SetAside
            {
                creationDate = date,
                deadlineDate = dateExpiration,
                Customer_idCustomer = idCustomer,
                totalAmount = totalAmount,
                stateAside = stateSetAside,
                percentage = percentage,
                reaminingAmount = remaining,
            };
            SaveInformation(newSetAside, StatesArticle.PENDING_ARTICLE);
        }

        private void SaveInformation(SetAside newSetAside, string state)
        {
            List<ArticlesSetAside> listArticles = new List<ArticlesSetAside>();
            try
            {
                //crear apartado
                (var idSetAside, var result) = SetAsideDAO.CreateSetAside(newSetAside);
                _idSetAside = idSetAside;
                //crear union
                if (result == MessageCode.SUCCESS)
                {
                    foreach (var item in list)
                    {
                        var newArticle = new ArticlesSetAside
                        {
                            Article_idBelonging = item.idBelonging,
                            SetAside_idSetAside = idSetAside,
                            discount = _discount,
                        };
                        listArticles.Add(newArticle);
                    }
                    SetAsideDAO.AddArticlesInSetAside(listArticles);
                    //actualizar articulos
                    foreach (var item in list)
                    {
                        ArticleDAO.UpdateArticleState(item.idArticle, state);
                    }
                    _listArticles = listArticles;
                    _SetAside = newSetAside;
                }
                else
                {
                    //error
                }
            }
            catch (DbEntityValidationException)
            {
                ErrorManager.ShowError("Error al registrar un nuevo contrato");
            }
            catch (DbUpdateException)
            {
                ErrorManager.ShowError("Error al registrar un nuevo contrato");
            }
            catch (Exception)
            {
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
            }

        }

        private void OpenPayPage()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;
            //campos estaticos para pruebas
            (App.Current as App)._cashOnHand = 1000;
            TransactionView newOperation = new TransactionView(OperationType.OPERATION_SETASIDE, _amount, _idSetAside);
            newOperation.CommunicacionPages(this);
            window.SecundaryContainer.Navigate(newOperation);
            window.PrimaryContainer.IsHitTestVisible = false;
        }
        /// <summary>
        /// Metodo que se encarga de la comunicacion entre las paginas
        /// Se activa cuando se escanea un codigo
        /// </summary>
        /// <param name="article"></param>
        public void ScanCommunication(ArticleDomain article)
        {
            if (article != null && article.idArticle != 0)
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
                //error
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
            if (list.Count == 0)
            {
                ErrorManager.ShowWarning("No hay articulos en la lista");
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
            _percentage = percentage;
            _amount = (_total * percentage);
            _remaining = (_total - (_total * percentage));
            labelPercentage.Content = "Porcentaje de Apartado: " + selectedPercentage + "%";
            labelAmount.Content = "Cantidad para Apartado: " + _amount;
            labelRemaining.Content = "Restante para Liquidar: " + _remaining;
        }
        private void comBoxDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list.Count == 0)
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
            _percentage = percentage;
            string selectedDiscount = comBoxDiscount.SelectedItem.ToString();
            int selectedDiscountPercentage = Convert.ToInt32(selectedDiscount);
            _discount = selectedDiscountPercentage;
            var discount = selectedDiscountPercentage / 100.0;
            _subtotal = (_subtotal * (1 - discount));
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

        public void Communication(bool result)
        {
            if (result)
            {
                //actualizar estado de apartado
                UpdateStates(true);
                ErrorManager.ShowInformation("Operacion Exitosa");
            }
            else
            {
                UpdateStates(false);
                ErrorManager.ShowInformation("Operacion Cancelada");
            }
        }

        private void UpdateStates(bool option)
        {
            try
            {
                if (option)
                {
                    SetAsideDAO.UpdateSetAsideState(StatesAside.ACTIVED_ASIDE, _idSetAside);
                    foreach (var item in _listArticles)
                    {
                        ArticleDAO.UpdateArticleState(item.Article_idBelonging, StatesArticle.ASIDE_ARTICLE);
                        ClosePage();
                    }
                }
                else
                {
                    SetAsideDAO.UpdateSetAsideState(StatesAside.CANCELED_ASIDE, _idSetAside);
                    foreach (var item in _listArticles)
                    {
                        ArticleDAO.UpdateArticleState(item.Article_idBelonging, StatesArticle.SALE_ARTICLE);
                    }
                }
            }
            catch (Exception)
            {
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
            }

        }

        private void ClosePage()
        {
            list.Clear();
            labelAmount.Content = "Cantidad para Apartado: ";
            labelIva.Content = "IVA(16%): ";
            labelPercentage.Content = "Porcentaje de Apartado: ";
            labelRemaining.Content = "Restante para Liquidar: ";
            labelSubTotal.Content = "SubTotal: ";
            labelTotal.Content = "Total: ";
            textCURP.Text = "";
            textCustomerName.Text = "";
            textDateLine.Text = "";
            comBoxDiscount.SelectedItem = null;
            comBoxPercentage.SelectedItem = null;

        }

        private void textCURP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
