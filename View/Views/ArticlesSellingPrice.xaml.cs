using BusinessLogic;
using DataAcces;
using Domain.BelongingCreation;
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
        private ICollectionView collectionView;
        public ArticlesSellingPrice()
        {
            InitializeComponent();
            InitializeDG(); 
            setFilter();

        }
        private void setFilter()
        {
            collectionView = CollectionViewSource.GetDefaultView(belongingsList);
            collectionView.Filter = (item) => true;
            dgBelonging.ItemsSource= collectionView;
        }
        private void InitializeDG()
        {
            
            var belonging = new Domain.BelongingCreation.Belonging();
            
            if((DateTime.Today - belonging.DeadLine).Days >= 15)
            {
                belongingsList = BelongingDAO.GetBelonging();
                dgBelonging.ItemsSource = belongingsList;
            }
            
        }
        private void loadDG()
        {
            dgBelonging.ItemsSource = belongingsList;
        }

        private void btn_reload(object sender, RoutedEventArgs e)
        {
            loadDG();
        }
        private void Button_Price(object sender, RoutedEventArgs e)
        {
            Button btn= sender as Button;
            if (btn != null)
            {
                var row =DataGridRow.GetRowContainingElement(btn);
                var item = row.Item;
                if (item != null && dgBelonging.Items.Contains(item))
                {
                    
                    
                }
            }
        }
    }
}
