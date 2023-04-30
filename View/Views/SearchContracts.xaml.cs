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
    /// Lógica de interacción para SearchContracts.xaml
    /// </summary>
    public partial class SearchContracts : Page
    {
        public SearchContracts()
        {
            InitializeComponent();
            comBox_TypeSearch.Items.Add("Numero del cliente");
            comBox_TypeSearch.Items.Add("Nombre del cliente");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Reactivate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Restore_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
