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
    /// Lógica de interacción para CreateCustomerRecord.xaml
    /// </summary>
    public partial class CreateCustomerRecord : Page
    {

        private bool image1 = false;
        private bool image2 = false;
        BitmapImage bitMap3 = new BitmapImage();
        BitmapImage bitMap4 = new BitmapImage();

        public CreateCustomerRecord()
        {
            InitializeComponent();
            comBox_Identification.Items.Add("INE");
            comBox_Identification.Items.Add("CURP");
            comBox_Identification.Items.Add("Cartilla militar");
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_AddImage1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            bool? result = ofd.ShowDialog();
            if (result == true)
            {
                string ruta = ofd.FileName;
                Image imgOne = new Image();
                BitmapImage bitMap = new BitmapImage();
                bitMap.BeginInit();
                bitMap.UriSource = new Uri(ruta);
                bitMap.EndInit();
                componentImageOne.Source = bitMap;
                image1 = false;
            }

            }

        private void Btn_AddImage2_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            bool? result = ofd.ShowDialog();
            if (result == true)
            {
                string ruta = ofd.FileName;
                Image imgOne = new Image();
                BitmapImage bitMap = new BitmapImage();
                bitMap.BeginInit();
                bitMap.UriSource = new Uri(ruta);
                bitMap.EndInit();
                componentImageTwo.Source = bitMap;
                image2 = false;
            }
        }

        
    }
}
