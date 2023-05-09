/*
 * Autor: Jonathan Hernandez martínez
 */
using BusinessLogic;
using BusinessLogic.Utility;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View.Properties;

namespace View.Views
{
    public partial class GiveProfitToCustomer : Page
    {
        private List<Domain.CustomerProfitDomain> customersList = new List<Domain.CustomerProfitDomain>();
        private ICollectionView collectionView;
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
            dgCustomers.ItemsSource = collectionView;//pasando la lissta con funcion de filtro al dg
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
            //else
            //{
            //    LoadTable();

            //}
        }

        private void LoadTable()
        {
            Console.WriteLine("Load table" + customersList.Count());
            dgCustomers.ItemsSource = customersList;
        }

        private void Btn_FindCustomer(object sender, RoutedEventArgs e)
        {

//            if (VerificateSpecialCaracters())
           // {
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
           // }

            //  }

        }



        private void Btn_GiveProfitCustomer(object sender, RoutedEventArgs e)
        {
            if(dgCustomers.SelectedIndex < 0)
                ErrorManager.ShowError("Favor de seleccionar un cliente para realizar la operacion.");
            else
            {
                var c = dgCustomers.SelectedItem as Domain.CustomerProfitDomain;
                //codigo para llamar a CU transaccion
            }


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
    }
}
