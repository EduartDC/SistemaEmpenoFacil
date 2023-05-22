using BusinessLogic;
using DataAcces;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using View.Properties;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para SearchCustomer.xaml
    /// </summary>
    public partial class SearchCustomer : Page
    {
        private List<Domain.Customer> customersList= new List<Domain.Customer>();
        private ICollectionView collectionView;
        private List<string> _listNamesCustomers = new List<string>();
        private List<int> _listNumberCustomers = new List<int>();
        private List<string> _listCurpsCustomers = new List<string>();
        public SearchCustomer()
        {
            InitializeComponent();
            InitializeTable();
            setFilter();
        }

        private void setFilter()
        {
            collectionView = CollectionViewSource.GetDefaultView(customersList);
            collectionView.Filter = (item) => true;
            dgCustomers.ItemsSource = collectionView;
        }
        private void InitializeTable()
        {
            customersList = CustomerDAO.RecoverAllCustomers();
            customersList.ForEach(customer => _listNamesCustomers.Add(customer.firstName));
            customersList.ForEach(customer => _listNumberCustomers.Add(customer.idCustomer));
            customersList.ForEach(customer => _listCurpsCustomers.Add(customer.curp));
            
            dgCustomers.ItemsSource= customersList;
            if (_listNamesCustomers.Count == 0)
            {
                MessageBox.Show("Error al recuperar los registros de la base de datos, favor de intentarlo más tarde");
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
                Domain.Customer obj = item as Domain.Customer;
                if (obj != null)
                {
                    return string.IsNullOrEmpty(filter) || obj.curp.Contains(filter);
                }
                return false;
            }; 
            

        }

        private void Button_AddBlackList(object sender, RoutedEventArgs e)
        {
            AddCustomerBlackList customerBlackList = new AddCustomerBlackList();
            customerBlackList.ShowDialog();
        }

        
        private void Button_ModifyClient_Click(object sender, RoutedEventArgs e)
        {
            
            Button btn = sender as Button;

            if (btn != null)
            {
                var row = DataGridRow.GetRowContainingElement(btn);
                var item = row.Item;
                if (item != null && dgCustomers.Items.Contains(item))
                {
                    
                    Domain.Customer customer = (Domain.Customer)item;
                    
                    var window = (MainWindow)Application.Current.MainWindow;
                    BlurEffect blurEffect = new BlurEffect();
                    blurEffect.Radius = 5;
                    window.PrimaryContainer.Effect = blurEffect;

                    window.SecundaryContainer.Navigate(new CustomerView(customer.idCustomer));
                    window.PrimaryContainer.IsHitTestVisible = false;

                }
            }
            
        }
    }
}
