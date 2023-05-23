using BusinessLogic;
using DataAcces;
using Domain.BelongingCreation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// Lógica de interacción para ArticlesSellingPrice.xaml
    /// </summary>
    public partial class ArticlesSellingPrice : Page
    {
        private List<Domain.BelongingCreation.Belonging> belongingsList = new List<Domain.BelongingCreation.Belonging>();
        List<DateTime> dateList = new List<DateTime>();
        private ICollectionView collectionView;
        public ArticlesSellingPrice()
        {
            InitializeComponent();
            loadBelongings();
            InitializeDG(); 
            

        }
        private void loadDG()
        {
            collectionView = CollectionViewSource.GetDefaultView(belongingsList);
            collectionView.Filter = (item) => true;
            dgBelonging.ItemsSource= collectionView;
        }
        private void InitializeDG()
        {
            belongingsList = BelongingDAO.GetBelonging();
            belongingsList.ForEach(belongings =>dateList.Add(belongings.DeadLine));
            
        }
        private void loadBelongings()
        {
            belongingsList = BelongingDAO.GetBelonging();
            dgBelonging.ItemsSource = belongingsList;
            //ConverterImagesFormat();
            loadDG();

        }

        private void btn_reload(object sender, RoutedEventArgs e)
        {
            loadDG();
            loadBelongings();
        }
        private void Button_Price(object sender, RoutedEventArgs e)
        {
            Button btn= sender as Button;
            if (btn != null )
            {
                var row = DataGridRow.GetRowContainingElement(btn);
                var item = row.Item;
                
                if (item != null && dgBelonging.Items.Contains(item))
                {

                    Domain.BelongingCreation.Belonging belonging = (Domain.BelongingCreation.Belonging)item;

                
                    SetBelongingArticlePrice setBelongingArticlePrice = new SetBelongingArticlePrice(belonging.idBelonging);
                    setBelongingArticlePrice.Show();
                    
                }
            }
        }
        private void ConverterImagesFormat()

        {
            for (int i = 0; i < belongingsList.Count(); i++)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(belongingsList[i].image);
                bitmap.EndInit();
                belongingsList[i].imageConverted = bitmap;

            }
        }
    }
}
