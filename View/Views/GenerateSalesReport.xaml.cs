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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para GenerateSalesReport.xaml
    /// </summary>
    public partial class GenerateSalesReport : Page
    {
        private List<string> filter = new List<string> {"Selecciona una opcion","Articulos en stock", "articulos vendidos"};
        public GenerateSalesReport()
        {
            InitializeComponent();
            LoadFilter();
        }

        private void LoadFilter()
        {
            cbFilter.ItemsSource = filter;
            cbFilter.SelectedIndex = 0;
            
        }

        private void ClosePage_btn(object sender, RoutedEventArgs e)
        {

            this.Content = null;
        }
    }
}
