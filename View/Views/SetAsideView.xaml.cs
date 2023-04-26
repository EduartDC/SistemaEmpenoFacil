using BusinessLogic;
using DataAcces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for SetAsideView.xaml
    /// </summary>
    public partial class SetAsideView : Page
    {
        public SetAsideView()
        {
            InitializeComponent();

        }

        private void itemHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void itemExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;
            (App.Current as App)._cashOnHand = 1000;
            window.SecundaryContainer.Navigate(new TransactionView(MessageCode.OPERATION_SEAL, 658.50));
            //window.SecundaryContainer.Navigate(new CustomerView(6));
            window.PrimaryContainer.IsHitTestVisible = false;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddArticle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }



        /*private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        ImagesIdentification newImage = new ImagesIdentification();
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Archivos de imagen (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp|Todos los archivos (*.*)|*.*";
        if (openFileDialog.ShowDialog() == true)
        {

        string pathImagen = openFileDialog.FileName;
        BitmapImage bitmapImage = new BitmapImage(new Uri(pathImagen));
        byte[] byteArray;

        using (MemoryStream stream = new MemoryStream())
        {
        // Crear un codificador de imagen a partir del BitmapImage
        BitmapEncoder encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

        // Codificar la imagen y escribir los bytes en el MemoryStream
        encoder.Save(stream);
        byteArray = stream.ToArray();
        newImage.imagen = byteArray;
        newImage.Customer_idCustomer = 6;
        }
        }
        CustomerDAO.AddImagecostumer(newImage);
        }*/
    }
}
