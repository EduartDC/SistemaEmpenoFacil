using BusinessLogic;
using DataAcces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View.Properties;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Page
    {
        public CustomerView()
        {
            InitializeComponent();

        }

        private void btnSearchImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {

                string rutaImagen = openFileDialog.FileName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(rutaImagen));
                imgPreview.Source = bitmapImage;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = ErrorManager.ShowQuestion(MessageError.CANCEL_OPERATION);

            if (result == MessageBoxResult.Yes)
            {
                var window = (MainWindow)Application.Current.MainWindow;
                BlurEffect blurEffect = new BlurEffect();
                blurEffect.Radius = 0;
                window.PrimaryContainer.Effect = blurEffect;
                window.SecundaryContainer.Content = null;
                window.PrimaryContainer.IsHitTestVisible = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCleanImageOne_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCleanImageTwo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
