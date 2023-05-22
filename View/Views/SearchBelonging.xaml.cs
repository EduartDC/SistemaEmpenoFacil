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
        private List<Belonging> belongings = new List<Belonging>();
        
        private ICollectionView collectionView;
        public SearchBelonging()
        {
            InitializeComponent();
            cbCategory.Items.Add("Buscar por ID de prenda");
            cbCategory.Items.Add("Buscar por ID de contrato");
            FilterTable();
            
            
        }
        
        
        private void FilterTable()
        {
           
            
            if (cbCategory.SelectedIndex.ToString() =="Buscar por ID de prenda" )
            {
                
               dgBelonging.ItemsSource= (System.Collections.IEnumerable)BelongingDAO.GetBelongingByID(int.Parse(tbID.Text));
                ConverterImagesFormat();

            }
            
            if (cbCategory.SelectedIndex.ToString()== "Buscar por ID de contrato")
            {
                belongings = BelongingDAO.GetBelongingByIdContract(int.Parse(tbID.Text));
                
                ConverterImagesFormat();
                dgBelonging.ItemsSource= belongings;
                
            }
            
        }

        private void btn_FilterArticles(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(tbID.Text);
            Console.WriteLine(tbID.Text);
            FilterTable();
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
    }
}
