using BusinessLogic;
using Domain.BelongingCreation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
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
    /// Lógica de interacción para SearchBelonging.xaml
    /// </summary>
    public partial class SearchBelonging : Page
    {
        
        
        private ICollectionView collectionView;
        private List<Domain.BelongingCreation.Belonging> belongingsList = new List<Domain.BelongingCreation.Belonging>();
        public SearchBelonging()
        {
            InitializeComponent();
            loadBelongings();
            
            
        }

        private void loadDG()
        {
            collectionView = CollectionViewSource.GetDefaultView(belongingsList);
            collectionView.Filter = (item) => true;
            dgBelonging.ItemsSource = collectionView;
        }

        private void loadBelongings()
        {
            Belonging belonging = new Belonging();
            belongingsList = BelongingDAO.GetAllBelonging();
            ConverterImagesFormat();
            dgBelonging.ItemsSource = belongingsList;
            
            loadDG();
        }

        private void btn_Reload(object sender, RoutedEventArgs e)
        {
            loadDG();
            loadBelongings();
        }
        private void ConverterImagesFormat()

        {
            for (int i = 0; i < belongingsList.Count(); i++)
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = new MemoryStream(belongingsList[i].image);
                    bitmap.EndInit();
                    belongingsList[i].imageConverted = bitmap;
                }
                catch (Exception ex)
                {

                }


            }
        }

    }
}
