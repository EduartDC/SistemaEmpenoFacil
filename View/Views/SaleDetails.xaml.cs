using BusinessLogic;
using DataAcces;
using Domain.BelongingCreation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para SaleDetails.xaml
    /// </summary>
    public partial class SaleDetails : Window
    {

        Sale sale = null;
        List<Domain.BelongingCreation.Belonging> belongings = new List<Domain.BelongingCreation.Belonging>();

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

        private void ConverterImagesFormat()
        {
            Console.WriteLine(belongings.Count());
            for (int i = 0; i < belongings.Count(); i++)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(belongings[i].image);
                bitmap.EndInit();
                belongings[i].imageConverted = bitmap;

            }
        }

        private void getArticlesOfSale(int saleID)
        {
            List<Belongings_Articles> belongings_Articles = BelongingsArticlesDAO.GetBelongings_Articles_Selled(saleID);
            foreach (Belongings_Articles selledArticle in belongings_Articles) 
            {
                belongings.Add(BelongingDAO.GetBelongingByID(selledArticle.idBelonging));
            }
            for(int i = 0;i < belongings.Count; i++) 
            {
                belongings.ElementAt(i).sellingPrice = belongings_Articles.ElementAt(i).sellingPrice;
            }
            ConverterImagesFormat();
            dataGridArticlesSelled.ItemsSource = belongings;
            
        }

        private void GoBackButtonEvent(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
