﻿using BusinessLogic;
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
using System.IO;
using Path = System.IO.Path;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Page
    {
        BitmapImage[] _images = new BitmapImage[2];
        byte[][] _bytes = new byte[2][];
        List<ImagesIdentification> imagesCustomers;
        int id;
        public CustomerView(int idCustomer)
        {
            InitializeComponent();
            id = idCustomer;
            SetInformation(idCustomer);
            SetImages(idCustomer);
        }

        private void SetImages(int idCustomer)
        {
            imagesCustomers = CustomerDAO.GetImagesCustomer(idCustomer);
            var count = 0;
            foreach (var image in imagesCustomers)
            {
                byte[] imageData = image.imagen;

                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();
                    _images[count] = bitmapImage;
                    count++;
                }

            }
            imgIdentificationOne.Source = _images[0];
            imgIdentificationTwo.Source = _images[1];
        }

        private void SetInformation(int idCustomer)
        {
            var customer = CustomerDAO.GetCustomer(idCustomer);
            textAddress.Text = customer.address;
            textCURP.Text = customer.curp;
            textPhonNomber.Text = customer.telephonNumber.ToString();
            textName.Text = customer.firstName;
            textLastName.Text = customer.lastName;
            comBoxIdentificationType.SelectedIndex = int.Parse(customer.identification);

        }

        private void btnSearchImagen_Click(object sender, RoutedEventArgs e)
        {
            if (_images[0] == null || _images[1] == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Archivos de imagen (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp|Todos los archivos (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    string pathImagen = openFileDialog.FileName;
                    LoadImages(pathImagen);
                }
            }
            else
            {
                ErrorManager.ShowInformation("Elimina una imagen para poder cargar otra.");
            }
        }

        private void LoadImages(string pathImagen)
        {
            if (_images[0] == null)
            {

                BitmapImage bitmapImage = new BitmapImage(new Uri(pathImagen));
                _images[0] = bitmapImage;
                imgIdentificationOne.Source = bitmapImage;
                btnCleanImageOne.IsEnabled = true;
            }
            else
            {
                btnSearch.IsEnabled = false;
                BitmapImage bitmapImage = new BitmapImage(new Uri(pathImagen));
                _images[1] = bitmapImage;
                imgIdentificationTwo.Source = bitmapImage;
                btnCleanImageTwo.IsEnabled = true;
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
            if (_images[0] == null || _images[1] == null)
            {
                ErrorManager.ShowWarning("Es necesario seleccionar las dos imagenes de la identificacion.");
            }
            else if (string.IsNullOrEmpty(textName.Text) || string.IsNullOrEmpty(textLastName.Text) ||
                    string.IsNullOrEmpty(textCURP.Text) || string.IsNullOrEmpty(textAddress.Text) ||
                    comBoxIdentificationType.SelectedItem == null)
            {
                ErrorManager.ShowWarning(MessageError.FIELDS_EMPTY);
            }
            else
            {
                SaveInformation();
                SaveImages();

                ErrorManager.ShowInformation("Se ha guardado la informacion correctamente.");
            }
        }

        private void SaveImages()
        {
            var count = 0;
            foreach (var image in _images)
            {
                BitmapImage bitmapImage = image;
                MemoryStream stream = new MemoryStream();
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(stream);
                byte[] byteArray = stream.ToArray();
                stream.Close();
                _bytes[count] = byteArray;
                count++;
            }

            var pos = 0;
            foreach (var image in imagesCustomers)
            {
                image.imagen = _bytes[pos];
                CustomerDAO.UpdateImageCustomer(image);
                pos++;
            }
        }

        private void SaveInformation()
        {
            Customer newCustomer = new Customer();
            newCustomer.idCustomer = id;
            newCustomer.address = textAddress.Text;
            newCustomer.telephonNumber = int.Parse(textPhonNomber.Text);
            newCustomer.identification = comBoxIdentificationType.SelectedIndex.ToString();
            CustomerDAO.UpdateCustomer(newCustomer);
        }

        private void btnCleanImageOne_Click(object sender, RoutedEventArgs e)
        {

            _images[0] = null;
            imgIdentificationOne.Source = new BitmapImage(new Uri("C:\\Users\\Eduar\\Source\\Repos\\EduartDC\\SistemaEmpenoFacil\\View\\Images\\photo.png"));
            btnSearch.IsEnabled = true;
            btnCleanImageOne.IsEnabled = false;
        }

        private void btnCleanImageTwo_Click(object sender, RoutedEventArgs e)
        {
            _images[1] = null;
            imgIdentificationTwo.Source = new BitmapImage(new Uri("C:\\Users\\Eduar\\Source\\Repos\\EduartDC\\SistemaEmpenoFacil\\View\\Images\\photo.png"));
            btnSearch.IsEnabled = true;
            btnCleanImageTwo.IsEnabled = false;
        }
    }
}
