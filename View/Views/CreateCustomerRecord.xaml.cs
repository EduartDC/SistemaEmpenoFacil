using BusinessLogic;
using DataAcces;
using Microsoft.SqlServer.Server;
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

        private bool imageOneValidation = false;
        private bool imageTwoValidation = false;
        private List<byte[]> imagesBytes = new List<byte[]>();
        private BitmapImage imageOneCopy;
        private BitmapImage imageTwoCopy;

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
            if (ValidateCamps())
            {
                Domain.Customer customer = new Domain.Customer
                {
                    firstName = text_Name.Text.Trim(),
                    lastName = text_LastName.Text.Trim(),
                    curp = text_CURP.Text.Trim(),
                    blackList = false,
                    telephonNumber = long.Parse(text_telephonNumber.Text.Trim()),
                    address = text_Address.Text.Trim(),
                    identification = comBox_Identification.SelectedItem.ToString()
                };

                AddCustomer(customer);
            }
        }

        private void AddCustomer(Domain.Customer customer)
        {
            (int result, int idCustomer) = CustomerDAO.AddCustomer(customer);
            if (result == 200)
            {
                convertToBytes(imageOneCopy);
                convertToBytes(imageTwoCopy);
                ImagesIdentification imageOne = new ImagesIdentification();
                imageOne.imagen = imagesBytes[0];
                imageOne.Customer_idCustomer = idCustomer;
                ImagesIdentification imageTwo = new ImagesIdentification();
                imageTwo.imagen = imagesBytes[1];
                imageTwo.Customer_idCustomer = idCustomer;
                List<ImagesIdentification> imagesIdentifications = new List<ImagesIdentification>
                {
                    imageOne,
                    imageTwo
                };
                if (CustomerDAO.AddTwoImageIdentification(imagesIdentifications) == 200)
                {
                    MessageBox.Show("Cliente registrado con exito");
                }
                else
                {
                    MessageBox.Show("Error al registrar las imagenes en la base de datos");
                }
            }
            else
            {
                MessageBox.Show("Error al registrar al cliente en la base de datos");
            }
            
        }

        private void convertToBytes(BitmapImage bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(stream);
                ImagesIdentification imagesIdentification = new ImagesIdentification();
                imagesBytes.Add(stream.ToArray());
            }
        }

        private void Btn_AddImage1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            bool? result = ofd.ShowDialog();
            if (result == true)
            {
                string rute = ofd.FileName;
                Image imgOne = new Image();
                BitmapImage bitMap = new BitmapImage();
                bitMap.BeginInit();
                bitMap.UriSource = new Uri(rute);
                bitMap.EndInit();
                componentImageOne.Source = bitMap;
                imageOneValidation = true;
                imageOneCopy = bitMap;
            }

            }

        private void Btn_AddImage2_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            bool? result = ofd.ShowDialog();
            if (result == true)
            {
                string rute = ofd.FileName;
                Image imgOne = new Image();
                BitmapImage bitMap = new BitmapImage();
                bitMap.BeginInit();
                bitMap.UriSource = new Uri(rute);
                bitMap.EndInit();
                componentImageTwo.Source = bitMap;
                imageTwoValidation = true;
                imageTwoCopy = bitMap;
            }
        }



        private bool ValidateCamps()
        {
            label_ErrorName.Visibility = Visibility.Hidden;
            label_ErrorLastName.Visibility = Visibility.Hidden;
            label_ErrorAddress.Visibility = Visibility.Hidden;
            label_ErrorCurp.Visibility = Visibility.Hidden;
            label_ErrorTelephonNumber.Visibility = Visibility.Hidden;
            label_ErrorIdentification.Visibility = Visibility.Hidden;
            bool result = true;
            if (!FormatValidation.ValidateFormat(text_Name.Text.Trim(), "^[a-zA-Z]+([ \\-][a-zA-Z]+)*$"))
            {
                result = false;
                label_ErrorName.Visibility = Visibility.Visible;
            }
            if(!FormatValidation.ValidateFormat(text_LastName.Text.Trim(), "^[a-zA-Z]+([ \\-][a-zA-Z]+)*$"))
            {
                result = false;
                label_ErrorLastName.Visibility = Visibility.Visible;
            }
            if(!FormatValidation.ValidateFormat(text_CURP.Text.Trim(), "^[A-Z]{4}[0-9]{6}[H,M][A-Z]{5}[0-9]{2}$"))
            {
                result = false;
                label_ErrorCurp.Visibility = Visibility.Visible;
            }
            if(!FormatValidation.ValidateFormat(text_Address.Text.Trim(), "^[0-9]+\\s+([a-zA-Z]+\\s)*[a-zA-Z]+$"))
            {
                result = false;
                label_ErrorAddress.Visibility = Visibility.Visible;
            }
            if(!FormatValidation.ValidateFormat(text_telephonNumber.Text.Trim(), "^[0-9]+$"))
            {
                result = false;
                label_ErrorTelephonNumber.Visibility = Visibility.Visible;
            }
            if (comBox_Identification.SelectedIndex == -1)
            {
                result = false;
                label_ErrorIdentification.Visibility = Visibility.Visible;
            }
            if(!imageOneValidation || !imageTwoValidation)
            {
                result = false;
                MessageBox.Show("Favor de agregar las imagenes de la identificación del cliente");
            }
            return result;
        }
        
    }
}
