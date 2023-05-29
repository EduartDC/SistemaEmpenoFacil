using BusinessLogic;
using BusinessLogic.Utility;
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
using View.Properties;
using static iTextSharp.text.pdf.AcroFields;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para PayOffSetAside.xaml
    /// </summary>
    public partial class PayOffSetAside : Page, MessageService
    {
        private List<DataAcces.SetAside> setAsidesList = new List<DataAcces.SetAside>();
        private ICollectionView collectionView;
        DataAcces.SetAside setAside;
        DataAcces.Customer customer;
        int IdSetAside;
        private List<Domain.ArticleDomain> articlesList = new List<Domain.ArticleDomain>();
        public PayOffSetAside()
        {
            InitializeComponent();
            loadDG();
        }



        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            string curp = tbSearch.Text;
            customer = CustomerDAO.GetCustomerByCURP(curp);

            if (customer != null)
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
                MessageBox.Show(MessageError.ERROR_USER_NOT_FOUND);
            }
        }
        public void BtnPay_Click(object sender, RoutedEventArgs e)
        {
            DateTime actualTime = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Button btn = sender as Button;

            if (btn != null)
            {
                var row = DataGridRow.GetRowContainingElement(btn);
                var item = row.Item;
                if (item != null && dgSetAside.Items.Contains(item))
                {
                     setAside = (DataAcces.SetAside)item;
                    if (setAside.deadlineDate > actualTime )
                    {
                        if (setAside.reaminingAmount>0)
                        {
                            IdSetAside= setAside.idSetAside;
                            Operation operation = new Operation();
                            OperationType operationType = new OperationType();
                            var window = (MainWindow)Application.Current.MainWindow;
                            BlurEffect blurEffect = new BlurEffect();
                            blurEffect.Radius = 5;
                            window.PrimaryContainer.Effect = blurEffect;
                            TransactionView newOperation = new TransactionView(OperationType.OPERATION_SETASIDE, setAside.reaminingAmount, setAside.idSetAside);
                            newOperation.CommunicacionPages(this);
                            window.SecundaryContainer.Navigate(newOperation);
                            window.PrimaryContainer.IsHitTestVisible = false;
                        }
                        else
                        {
                            MessageBox.Show(MessageError.SETASIDE_ALREADY_PAID);
                        }
                       
                        
                    }else
                    {
                        MessageBox.Show(MessageError.SETASIDE_OVERDUE);
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
        public void Communication(bool result)
        {
           if (result== true)
            {
                List<ArticleDomain> articles = ArticleDAO.GetArticlesListByIdSetAside(IdSetAside);
                int returnValue, idSale, idArticle;

                Sale sale = new Sale();

                sale.total = setAside.totalAmount;
                sale.Customer_idCustomer = customer.idCustomer;
                sale.saleDate = DateTime.Now;
                sale.discount = "0";
                sale.subtotal = setAside.totalAmount;
                (returnValue, idSale) = SaleDAO.MakeSale(sale);

                foreach (ArticleDomain article in articles)
                {
                    
                    idArticle = article.idArticle;
                    Belongings_Articles belongings_Articles = new Belongings_Articles();
                    belongings_Articles.stateArticle= article.stateArticle; 
                    belongings_Articles.idArticle = article.idBelonging;
                    belongings_Articles.storeProfit = setAside.totalAmount;
                    
                    
                    Console.WriteLine("sale "+idSale);

                    BelongingsArticlesDAO.ModifyBelonging_Article(belongings_Articles.idArticle, idSale, belongings_Articles.storeProfit);
                    SetAsideDAO.PayOffSetAside(StatesAside.COMPLETED_ASIDE, setAside.idSetAside);
                }

                
                loadSetAsides(customer.idCustomer);
                

                Operation operation = new Operation();

                
                
            }
        }

        public void ScanCommunication(ArticleDomain article){}

    }
}
