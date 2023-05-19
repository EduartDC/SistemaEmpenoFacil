using BusinessLogic;
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
    /// Lógica de interacción para SearchBelonging.xaml
    /// </summary>
    public partial class SearchBelonging : Page
    {
        private List<Belonging> belongings = new List<Belonging>();
        private Boolean datePickerEnabled = true;
        private ICollectionView collectionView;
        public SearchBelonging()
        {
            InitializeComponent();
            cbCategory.Items.Add()

        }
        
        private void loadDate()
        {
            dpDate.Text = DateTime.Now.ToString();
        }

        private void loadBelongingId()
        {
            int code = 0;
            belongings.Clear();
            (code, articles) = BelongingDAO.GetBelongingByID
            
        }
        
    }
}
