using BusinessLogic;
using DataAcces;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para SaleDetails.xaml
    /// </summary>
    public partial class SaleDetails : Window
    {

        Sale sale = null;
        public SaleDetails()
        {
            InitializeComponent();
        }

        public void showSaleDetails(int saleId)
        {
            sale = SaleDAO.GetSaleBySaleCode(saleId);
            labelSaleCode.Content = sale.idSale.ToString();
            labelSaleDate.Content = sale.saleDate.ToString();
            labelFinalSaleAmount.Content = sale.total.ToString();
            getArticlesOfSale(saleId);
        }

        private void getArticlesOfSale(int saleID)
        {
            List<Belongings_Articles> belongings_Articles = BelongingsArticlesDAO.GetBelongings_Articles_Selled(saleID);
            List<Belonging> belongings = new List<Belonging>();
            foreach (Belongings_Articles selledArticle in belongings_Articles) 
            {
                belongings.Add(BelongingDAO.GetBelongingByID(selledArticle.idBelonging));
            }
            dataGridArticlesSelled.ItemsSource = belongings;
        }
        private void GoBackButtonEvent(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
