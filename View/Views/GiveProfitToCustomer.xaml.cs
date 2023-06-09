﻿/*
 * Autor: Jonathan Hernandez martínez
 */
using BusinessLogic;
using BusinessLogic.Utility;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class GiveProfitToCustomer : Page , MessageService
    {
        private List<Domain.CustomerProfitDomain> customersList = new List<Domain.CustomerProfitDomain>();
        private ICollectionView collectionView;
        private int idCustomerSelected = 0;
        private string articleCustomer="";
        public GiveProfitToCustomer()
        {
            InitializeComponent();
            LoadCustomers();
            setFilter();
        }
      
        private void setFilter()
        {
            collectionView = CollectionViewSource.GetDefaultView(customersList);
            collectionView.Filter = (item) => true;
            dgCustomers.ItemsSource = collectionView;
        }

        private void LoadCustomers()
        {
            int code;
            (code, customersList) = CustomerDAO.GetCustomersProfit();
            if (code == MessageCode.CONNECTION_ERROR)
            {
                ErrorManager.ShowWarning(MessageError.CONNECTION_ERROR);
                this.Content = null;
            }

        }

        private void LoadTable()
        {
            
            dgCustomers.ItemsSource = customersList;
        }

        private void Btn_FindCustomer(object sender, RoutedEventArgs e)
        {

                string filter = tbSearchCurp.Text.ToUpper();
                collectionView.Filter = (item) =>
                {
                    Domain.CustomerProfitDomain obj = item as Domain.CustomerProfitDomain;
                    if (obj != null)
                    {
                        return string.IsNullOrEmpty(filter) || obj.curp.Contains(filter);
                    }
                    return false;
                };
        }

        private double amount = 0;

        private void Btn_GiveProfitCustomer(object sender, RoutedEventArgs e)
        {
            if(dgCustomers.SelectedIndex < 0)
                ErrorManager.ShowError("Favor de seleccionar un cliente para realizar la operacion.");
            else
            {
                var customerSelected = dgCustomers.SelectedItem as Domain.CustomerProfitDomain;
                idCustomerSelected = customerSelected.idCustomer;
                articleCustomer = customerSelected.articlesProfit;
                amount = customerSelected.profitCustomer;
                CallTtransactionView(customerSelected);
            }


        }

        private void CallTtransactionView(CustomerProfitDomain customerSelected)
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
            TransactionView newOperation = new TransactionView(OperationType.OPERATION_PROFIT,customerSelected.profitCustomer, customerSelected.idCustomer);
            newOperation.CommunicacionPages(this);
            mainWindow.SecundaryContainer.Navigate(newOperation);
            mainWindow.PrimaryContainer.IsHitTestVisible = false;
        }

        private void Btn_ClosePage(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private bool VerificateSpecialCaracters()
        {
            string pattern = "^[a-zA-Z0-9 ]+$";
            string text = tbSearchCurp.Text;
            if (!Regex.IsMatch(text, pattern))
            {
                ErrorManager.ShowWarning("El campo debe contener únicamente letras y números.");
                tbSearchCurp.Clear();
                return false;
            }
            return true;
        }

        //Metodo de interfaz para transactionView
        public void Communication(bool result)
        {
            if (result)
            {
                
                int resultArticleDao =  ArticleDAO.GiveProfitArticlesToCustomer(idCustomerSelected);
                int resultCustomerDAO = CustomerDAO.GiveProfitCustomer(idCustomerSelected);
                if (resultArticleDao == MessageCode.SUCCESS && resultCustomerDAO == MessageCode.SUCCESS)
                {
                    MessageBox.Show("Generando ticket");
                    CallTicket();
                }else
                {
                    ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
                }
            }

        }

        private void CallTicket()
        {
            CreateTickets.TicketProfitCustomer(idCustomerSelected, articleCustomer,amount);
        }

        //metodo de interfaz que no se usa
        public void ScanCommunication(ArticleDomain article)
        {

        }
    }
}
