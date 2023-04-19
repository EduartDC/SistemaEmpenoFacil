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
    /// Lógica de interacción para AddCustomerBlackList.xaml
    /// </summary>
    public partial class AddCustomerBlackList : Window
    {
        public AddCustomerBlackList()
        {
            InitializeComponent();
        }

        private void btn_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
