using BusinessLogic;
using DataAcces;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// Lógica de interacción para PayOffSetAside.xaml
    /// </summary>
    public partial class PayOffSetAside : Page
    {
        private List<DataAcces.SetAside> setAsidesList=new List<DataAcces.SetAside>();
        private ICollectionView collectionView;
        public PayOffSetAside()
        {
            InitializeComponent();
            loadDG();
        }

        

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            string curp = tbSearch.Text; 
            DataAcces.Customer customer = CustomerDAO.GetCustomerByCURP(curp);
            
            if (customer!=null)
            {
                if (customer.blackList == false)
                {
                  loadSetAsides(customer.idCustomer);
                }
                else
                {
                    MessageBox.Show(MessageError.CUSTOMER_IN_BLACK_LIST);
                }
                
            }
            else
            {
               MessageBox.Show (MessageError.ERROR_USER_NOT_FOUND);
            }
        }
        public void BtnPay_Click(object sender, RoutedEventArgs e)
        {
            DateTime actualTime = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Button btn= sender as Button;

            if (btn!=null)
            {
                var row = DataGridRow.GetRowContainingElement(btn);
                var item = row.Item;
                if(item!=null && dgSetAside.Items.Contains(item))
                {
                    DataAcces.SetAside setAside = (DataAcces.SetAside)item;
                    if(setAside.deadlineDate> actualTime)
                    {
                        Operation operation = new Operation();
                        var window = (MainWindow)Application.Current.MainWindow;
                        BlurEffect blurEffect = new BlurEffect();
                        blurEffect.Radius = 5;
                        window.PrimaryContainer.Effect = blurEffect;
                        window.SecundaryContainer.Navigate(new TransactionView(operation.idOperation,setAside.reaminingAmount,setAside.idSetAside));
                        window.PrimaryContainer.IsHitTestVisible = false;
                    }
                    else
                    {
                        MessageBox.Show("No se puede liquidar el apartado porque ya venció");
                    }
                }
            }
        }
        private void loadDG()
        {
            collectionView = CollectionViewSource.GetDefaultView(setAsidesList);
            collectionView.Filter = (item) => true;
            dgSetAside.ItemsSource = collectionView;
        }

        private void loadSetAsides(int id)
        {
            
            setAsidesList = SetAsideDAO.GetSetAsidesByIdCustomer(id);
            dgSetAside.ItemsSource= setAsidesList;
            if(setAsidesList.Count > 0)
            {
                loadDG();
            }
            else
            {
                MessageBox.Show("El usuario no tiene apartados");
            }
            

            
        }
    }
}
