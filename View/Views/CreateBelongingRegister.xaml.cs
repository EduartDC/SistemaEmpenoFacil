/*
 * Autor: Jonathan Hernandez Martinez
 */
using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Domain.Communitation;
using Domain.BelongingCreation;
using View.Properties;

namespace View.Views
{

    public partial class CreateBelongingRegister : Window
    {
        private Communication communication;
        private List<Domain.BelongingCreation.Belonging> belongingsList = new List<Domain.BelongingCreation.Belonging>();
        private bool edition;
        private int idEtition;
        private List<bool> imageBool = new List<bool>();
        private List<BitmapImage> imageTemp = new List<BitmapImage>();
        public CreateBelongingRegister()
        {
            InitializeComponent();
        }

        public void CommunicacionPages(Communication communication, List<Domain.BelongingCreation.Belonging> belongingsList, bool edition, int idEdition)
        {
            this.communication = communication;
            this.idEtition = idEdition;
            this.belongingsList = belongingsList;
            this.edition = edition;
            CheckOperation();
        }

        private void CheckOperation()
        {
            if (edition)
            {
                LoadCategories();
                LoadInformationEdition();
            }
            else
            {
                LoadCategories();
                for (int i = 0; i < 4; i++)
                    imageBool.Add(false);
            }
        }

        private void LoadInformationEdition()
        {
            int positionComboBox = 0;
            for (int i = 0; i < cbCategory.Items.Count; i++)
            {
                cbCategory.SelectedIndex = i;
                if (cbCategory.SelectedItem.Equals(belongingsList[idEtition].Category))
                {
                    positionComboBox = cbCategory.SelectedIndex;
                }
            }

            cbCategory.SelectedIndex = positionComboBox;
            tbDescription.Text = belongingsList[idEtition].GenericDescription;
            tbFeature.Text = belongingsList[idEtition].Features;
            tbApraisalAmount.Text = belongingsList[idEtition].LoanAmount.ToString();
            tbModel.Text = belongingsList[idEtition].Model;
            tbSerialNumber.Text = belongingsList[idEtition].SerialNumber;
            tbMaxValue.Text = belongingsList[idEtition].ApraisalAmount.ToString();
            componentImageOne.Source = belongingsList[idEtition].imagesBitmap[0];
            componentImageTwo.Source = belongingsList[idEtition].imagesBitmap[1];
            componentImageThree.Source = belongingsList[idEtition].imagesBitmap[2];
            componentImageFour.Source = belongingsList[idEtition].imagesBitmap[3];
            imageBool.Add(true);
            imageBool.Add(true);
            imageBool.Add(true);
            imageBool.Add(true);
            btnLoadImage.IsEnabled = false;
        }

        private void LoadCategories()
        {
            List<string> categoriesList = new List<string> { "Selecciona una categoria", "Joyería", "Relojería", "Herramientas","Electrónica", "Línea blanca","Instrumentos" };
            cbCategory.ItemsSource = categoriesList;
            cbCategory.SelectedIndex = 0;
        }

        private void ClickLoadImage(object sender, RoutedEventArgs e)
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

                if (!imageBool[0])
                {
                    imageBool[0] = true;
                    componentImageOne.Source = bitMap;
                }
                else if (!imageBool[1])
                {
                    imageBool[1] = true;
                    componentImageTwo.Source = bitMap;
                }
                else if (!imageBool[2])
                {
                    imageBool[2] = true;
                    componentImageThree.Source = bitMap;
                }
                else if (!imageBool[3])
                {
                    imageBool[3] = true;
                    componentImageFour.Source = bitMap;
                }

                if (imageBool[0] && imageBool[1] && imageBool[2] && imageBool[3])
                {
                    btnLoadImage.IsEnabled = false;
                }
            }
        }

        private void ClickDeleteImgOne(object sender, RoutedEventArgs e)
        {
            LoadDefaultImage(1);
            imageBool[0] = false;
        }
        private void ClickDeleteImgTwo(object sender, RoutedEventArgs e)
        {
            LoadDefaultImage(2);
            imageBool[1] = false;
        }

        private void ClickDeleteImgThree(object sender, RoutedEventArgs e)
        {
            LoadDefaultImage(3);
            imageBool[2] = false;
        }

        private void ClickDeleteImgFour(object sender, RoutedEventArgs e)
        {
            LoadDefaultImage(4);
            imageBool[3] = false;
        }

        private void LoadDefaultImage(int imageComponent)
        {
            Image imgOne = new Image();
            BitmapImage bitMapOne = new BitmapImage();
            bitMapOne.BeginInit();
            bitMapOne.UriSource = new Uri("pack://application:,,,/Icons/photo.png");
            bitMapOne.EndInit();
            if (imageComponent == 1)
                componentImageOne.Source = bitMapOne;
            else if (imageComponent == 2)
                componentImageTwo.Source = bitMapOne;
            else if (imageComponent == 3)
                componentImageThree.Source = bitMapOne;
            else if (imageComponent == 4)
                componentImageFour.Source = bitMapOne;

            btnLoadImage.IsEnabled = true;
        }

        private void ClickSaveBelonging(object sender, RoutedEventArgs e)
        {

            if (cbCategory.SelectedIndex <=0 ||
                string.IsNullOrEmpty(tbDescription.Text) ||
                string.IsNullOrEmpty(tbFeature.Text) ||
                string.IsNullOrEmpty(tbMaxValue.Text) ||
                string.IsNullOrEmpty(tbApraisalAmount.Text ) ||
                string.IsNullOrEmpty(tbSerialNumber.Text)
                )
                ErrorManager.ShowError("Campos vacios");
            else
            {
                if (IsNumeric(tbApraisalAmount.Text) && IsNumeric(tbMaxValue.Text))
                {
                    if (double.Parse(tbApraisalAmount.Text) > 0 && double.Parse(tbMaxValue.Text) > 0)
                    {
                        if (float.Parse(tbMaxValue.Text) > float.Parse(tbApraisalAmount.Text))
                        {
                            if (imageBool[0] && imageBool[1] && imageBool[2] && imageBool[3])
                            {
                                if (!edition)
                                    //if (belongingsList.Count() <= 5)
                                    SaveBelongingInList();
                                //else
                                //  ErrorManager.ShowError("maximo de prendas alcanzada");
                                else
                                    SaveBelongingEdition();
                            }
                        }
                        else
                            ErrorManager.ShowError("El valor de avaluo debe ser menor o igual al valor estimado del costo real de la prenda");
                    }
                    else
                        ErrorManager.ShowError("No se admiten valores negativos. Favor de verificar");
                    
                }
                else
                    ErrorManager.ShowError("Los valores de prestamo y valor maximo deben ser numericos");
            }
        }

        private void SaveBelongingEdition()
        {
            Domain.BelongingCreation.Belonging newBelonging = new Domain.BelongingCreation.Belonging();
            newBelonging.Category = cbCategory.SelectedItem.ToString();
            newBelonging.GenericDescription = tbDescription.Text;
            newBelonging.Features = tbFeature.Text;
            newBelonging.SerialNumber = tbSerialNumber.Text;
            newBelonging.Model = tbModel.Text;
            newBelonging.ApraisalAmount = int.Parse(tbMaxValue.Text);
            newBelonging.LoanAmount = int.Parse(tbApraisalAmount.Text);
            decimal resultTemp= ((decimal.Parse(tbApraisalAmount.Text)) * 100) / (decimal.Parse(tbMaxValue.Text));
            float loan = (float)resultTemp;
            newBelonging.PorcentLoan =float.Parse( loan.ToString("0.00"));
            imageTemp.Add(componentImageOne.Source as BitmapImage);
            imageTemp.Add(componentImageTwo.Source as BitmapImage);
            imageTemp.Add(componentImageThree.Source as BitmapImage);
            imageTemp.Add(componentImageFour.Source as BitmapImage);
            newBelonging.imagesBitmap.Add(imageTemp[0]);
            newBelonging.imagesBitmap.Add(imageTemp[1]);
            newBelonging.imagesBitmap.Add(imageTemp[2]);
            newBelonging.imagesBitmap.Add(imageTemp[3]);
            
            if (edition)
            {
                belongingsList[idEtition] = newBelonging;
                CleanComponents();
                MessageBox.Show("Prenda temporal modificada con exito ");
                communication.refreshBelongings(belongingsList);
                this.Close();
            }
        }

        private void SaveBelongingInList()
        {
            Domain.BelongingCreation.Belonging newBelonging = new Domain.BelongingCreation.Belonging();
            newBelonging.Category = cbCategory.SelectedItem.ToString();
            newBelonging.GenericDescription = tbDescription.Text;
            newBelonging.Features = tbFeature.Text;
            newBelonging.SerialNumber = tbSerialNumber.Text;
            newBelonging.Model = tbModel.Text;
            newBelonging.ApraisalAmount = int.Parse(tbMaxValue.Text);
            newBelonging.LoanAmount = int.Parse(tbApraisalAmount.Text);
            newBelonging.PorcentLoan = ((float.Parse(tbApraisalAmount.Text)) * 100) / (float.Parse(tbMaxValue.Text));//no esta en la BD
            imageTemp.Add(componentImageOne.Source as BitmapImage);
            imageTemp.Add(componentImageTwo.Source as BitmapImage);
            imageTemp.Add(componentImageThree.Source as BitmapImage);
            imageTemp.Add(componentImageFour.Source as BitmapImage);
            newBelonging.imagesBitmap.Add(imageTemp[0]);
            newBelonging.imagesBitmap.Add(imageTemp[1]);
            newBelonging.imagesBitmap.Add(imageTemp[2]);
            newBelonging.imagesBitmap.Add(imageTemp[3]);
            if (!edition)
            {
                belongingsList.Add(newBelonging);
                CleanComponents();
                MessageBox.Show("Prendada  temporal guardada con exito ");     
            }   
        }

        private void CleanComponents()
        {
            cbCategory.SelectedIndex = 0;
            tbDescription.Text = "";
            tbFeature.Text = "";
            tbSerialNumber.Text = "";
            tbModel.Text = "";
            tbApraisalAmount.Text = "";
            tbMaxValue.Text = "";
            imageTemp.Clear();
            LoadDefaultImage(1);
            LoadDefaultImage(2);
            LoadDefaultImage(3);
            LoadDefaultImage(4);
            imageBool[0]= false;
            imageBool[1]= false;
            imageBool[2]= false;
            imageBool[3]= false;
            imageTemp.Clear();
            btnLoadImage.IsEnabled = true;
        }

        private bool IsNumeric(string texto)
        {
            Regex regex = new Regex("[^0-9.-]+"); 
            return !regex.IsMatch(texto); 
        }

        private void ClicClosePage(object sender, RoutedEventArgs e)
        {
            if(!edition)
            {
                communication.refreshBelongings(belongingsList);
                this.Close();
            }else
                this.Close();
        }
    }
}
