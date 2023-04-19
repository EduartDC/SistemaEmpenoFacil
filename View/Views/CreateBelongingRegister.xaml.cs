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

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para CreateBelongingRegister.xaml
    /// </summary>
    public partial class CreateBelongingRegister : Window
    {
        private List<Domain.BelongingCreation.Belonging> belongingsList =  new List<Domain.BelongingCreation.Belonging>();
        Communication communication;

        //alamcenes de las imagenes para return Crear contratos
        private List<Boolean> imageList = new List<Boolean> { false, false, false, false };//indicador de foto cargada
        private int belongingSaved = 0;//indicador de prenda
        private List<BitmapImage> bitmapSavedList = new List<BitmapImage>(); //almacena de fotos
        private Boolean edicion = false;
        private int posicionEdicion;
        public CreateBelongingRegister()
        {
            InitializeComponent();



        }

        private void LoadEdition()
        {
            //verificar prenda a cargar

            componentImageOne.Source = bitmapSavedList[posicionEdicion];
            componentImageTwo.Source = bitmapSavedList[posicionEdicion + 1];
            componentImageThree.Source = bitmapSavedList[posicionEdicion + 2];
            componentImageFour.Source = bitmapSavedList[posicionEdicion + 3];

            int positionComboBox = 0;
            for (int i = 0; i < cbCategory.Items.Count; i++)
            {
                cbCategory.SelectedIndex = i;
                if (cbCategory.SelectedItem.Equals(belongingsList[posicionEdicion].Category))
                {
                    MessageBox.Show("prenda seleccionada" + cbCategory.SelectedItem);
                    positionComboBox = cbCategory.SelectedIndex;
                }
            }

            cbCategory.SelectedIndex = positionComboBox;

            tbDescription.Text = belongingsList[posicionEdicion].GenericDescription;
            tbFeature.Text = belongingsList[posicionEdicion].Features;
            tbApraisalAmount.Text = belongingsList[posicionEdicion].ApraisalAmount.ToString();
            tbModel.Text = belongingsList[posicionEdicion].Model;
            tbSerialNumber.Text = belongingsList[posicionEdicion].SerialNumber;
            tbLoanAmount.Text = belongingsList[posicionEdicion].LoanAmount.ToString();
            imageList[0] = true;
            imageList[1] = true;

            imageList[2] = true;

            imageList[3] = true;

            btnLoadImage.IsEnabled = false;
        }

        private void LoadCategories()
        {
            List<string> categoriesList = new List<string> { "Selecciona una categoria", "Joyeria", "Relojeria", "Herramientas" };
            cbCategory.ItemsSource = categoriesList;
            cbCategory.SelectedIndex = 0;
        }

        public void CommunicacionPages(Communication communication, List<Domain.BelongingCreation.Belonging> belongingList, List<BitmapImage> bitmapImgList, Boolean edicion, int posicionEdicion)
        {
            bitmapSavedList = bitmapImgList;
            this.belongingsList = belongingList;
            this.communication = communication;

            this.edicion = edicion;
            this.posicionEdicion = posicionEdicion;

            if (edicion == false)
            {
                imageList.Add(false);
                LoadCategories();
            }
            else
            {
                LoadCategories();
                LoadEdition();
            }
        }

        private void ClicClosePage(object sender, RoutedEventArgs e)
        {

            ClosePage();


        }

        private void ClosePage()
        {
            communication.refreshBelongings(belongingsList, bitmapSavedList);
            this.Close();
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

                //componentImageOne.Source = bitMapOne;

                if (!edicion)
                {
                    if (belongingSaved < 5)
                    {
                        if (!imageList[0])
                        {
                            componentImageOne.Source = bitMap; //carga
                            bitmapSavedList.Add(bitMap);//guarda
                            imageList[0] = true;
                        }
                        else
                        if (!imageList[1])
                        {
                            componentImageTwo.Source = bitMap;
                            bitmapSavedList.Add(bitMap);
                            imageList[1] = true;
                        }
                        else
                        if (!imageList[2])
                        {
                            componentImageThree.Source = bitMap;
                            bitmapSavedList.Add(bitMap);
                            imageList[2] = true;
                        }
                        else
                        if (!imageList[3])
                        {
                            componentImageFour.Source = bitMap;
                            bitmapSavedList.Add(bitMap);
                            imageList[3] = true;
                        }
                        if (imageList[1] && imageList[2] && imageList[3] && imageList[0])
                            btnLoadImage.IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Maximo de prendas alcenzada");

                    }
                }
                else
                {

                    switch (posicionEdicion)
                    {
                        case 0:
                            if (!imageList[0])
                            {
                                componentImageOne.Source = bitMap;
                                bitmapSavedList[0] = bitMap;
                                imageList[0] = true;
                            }
                            if (!imageList[1])
                            {
                                componentImageTwo.Source = bitMap;
                                bitmapSavedList[1] = bitMap;
                                imageList[1] = true;
                            }
                            if (!imageList[2])
                            {
                                componentImageThree.Source = bitMap;
                                bitmapSavedList[2] = bitMap;
                                imageList[2] = true;
                            }
                            if (!imageList[3])
                            {
                                componentImageFour.Source = bitMap;
                                bitmapSavedList[3] = bitMap;
                                imageList[3] = true;
                            }
                            break;
                        case 1:
                            if (!imageList[0])
                            {
                                componentImageOne.Source = bitMap;
                                bitmapSavedList[4] = bitMap;
                                imageList[0] = true;
                            }
                            if (!imageList[1])
                            {
                                componentImageTwo.Source = bitMap;
                                bitmapSavedList[5] = bitMap;
                                imageList[1] = true;
                            }
                            if (!imageList[2])
                            {
                                componentImageThree.Source = bitMap;
                                bitmapSavedList[6] = bitMap;
                                imageList[2] = true;
                            }
                            if (!imageList[3])
                            {
                                componentImageFour.Source = bitMap;
                                bitmapSavedList[7] = bitMap;
                                imageList[3] = true;
                            }
                            break;
                        case 2:
                            if (!imageList[0])
                            {
                                componentImageOne.Source = bitMap;
                                bitmapSavedList[8] = bitMap;
                                imageList[0] = true;
                            }
                            if (!imageList[1])
                            {
                                componentImageTwo.Source = bitMap;
                                bitmapSavedList[9] = bitMap;
                                imageList[1] = true;
                            }
                            if (!imageList[2])
                            {
                                componentImageThree.Source = bitMap;
                                bitmapSavedList[10] = bitMap;
                                imageList[2] = true;
                            }
                            if (!imageList[3])
                            {
                                componentImageFour.Source = bitMap;
                                bitmapSavedList[11] = bitMap;
                                imageList[3] = true;
                            }
                            break;
                        case 3:
                            if (!imageList[0])
                            {
                                componentImageOne.Source = bitMap;
                                bitmapSavedList[12] = bitMap;
                                imageList[0] = true;
                            }
                            if (!imageList[1])
                            {
                                componentImageTwo.Source = bitMap;
                                bitmapSavedList[13] = bitMap;
                                imageList[1] = true;
                            }
                            if (!imageList[2])
                            {
                                componentImageThree.Source = bitMap;
                                bitmapSavedList[14] = bitMap;
                                imageList[2] = true;
                            }
                            if (!imageList[3])
                            {
                                componentImageFour.Source = bitMap;
                                bitmapSavedList[15] = bitMap;
                                imageList[3] = true;
                            }
                            break;
                        case 4:
                            if (!imageList[0])
                            {
                                componentImageOne.Source = bitMap;
                                bitmapSavedList[16] = bitMap;
                                imageList[0] = true;
                            }
                            if (!imageList[1])
                            {
                                componentImageTwo.Source = bitMap;
                                bitmapSavedList[17] = bitMap;
                                imageList[1] = true;
                            }
                            if (!imageList[2])
                            {
                                componentImageThree.Source = bitMap;
                                bitmapSavedList[18] = bitMap;
                                imageList[2] = true;
                            }
                            if (!imageList[3])
                            {
                                componentImageFour.Source = bitMap;
                                bitmapSavedList[19] = bitMap;
                                imageList[3] = true;
                            }
                            break;

                            if (imageList[3])
                                btnLoadImage.IsEnabled = false;
                    }

                }


            }
        }

        private void LoadDefaultImage(int imageComponent)
        {

            Image imgOne = new Image();
            BitmapImage bitMapOne = new BitmapImage();
            bitMapOne.BeginInit();
            bitMapOne.UriSource = new Uri("pack://application:,,,/Images/plus.png");
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

        private void ClickDeleteImgOne(object sender, RoutedEventArgs e)
        {
            imageList[0] = false;
            LoadDefaultImage(1);

        }

        private void ClickDeleteImgTwo(object sender, RoutedEventArgs e)
        {
            imageList[1] = false;
            LoadDefaultImage(2);
        }

        private void ClickDeleteImgThree(object sender, RoutedEventArgs e)
        {

            imageList[2] = false;
            LoadDefaultImage(3);
        }

        private void ClickDeleteImgFour(object sender, RoutedEventArgs e)
        {

            imageList[3] = false;
            LoadDefaultImage(4);
        }

        private void ClickSaveBelonging(object sender, RoutedEventArgs e)
        {
            if (CheckComponents())
            {

                if (!edicion)
                    belongingSaved++;

                imageList[0] = false;
                imageList[1] = false;
                imageList[2] = false;
                imageList[3] = false;
                SaveInfoBelonging();
                LoadDefaultImage(1);
                LoadDefaultImage(2);
                LoadDefaultImage(3);
                LoadDefaultImage(4);
                MessageBox.Show("Prenda guardada correctamente");



            }
        }

        private void ModifyInfo()
        {

        }

        private Boolean CheckComponents()
        {
            if (cbCategory.SelectedIndex > 0 || !string.IsNullOrEmpty(tbDescription.Text) ||
                !string.IsNullOrEmpty(tbFeature.Text) || !string.IsNullOrEmpty(tbSerialNumber.Text) ||
                !string.IsNullOrEmpty(tbModel.Text) || !string.IsNullOrEmpty(tbApraisalAmount.Text) ||
                !string.IsNullOrEmpty(tbLoanAmount.Text))
            {
                if (imageList[0] && imageList[1] && imageList[2] && imageList[3])
                {
                    if (IsNumeric(tbApraisalAmount.Text) && IsNumeric(tbLoanAmount.Text))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Valores no numericos en montos");
                    }
                }
                else
                {
                    MessageBox.Show("Se necesitan 4 imagenes");
                    //MessageBox.Show("" + imageList[0] + imageList[1] + imageList[2] + imageList[3]);
                }
            }
            else
            {
                MessageBox.Show("Campos vacios");
            }
            return false;

        }

        private bool IsNumeric(string texto)
        {
            Regex regex = new Regex("[^0-9.-]+"); // Patrón que solo permite números enteros, decimales y negativos
            return !regex.IsMatch(texto); // Devuelve true si el texto solo contiene números, de lo contrario devuelve false.
        }

        private void SaveInfoBelonging()
        {
            Domain.BelongingCreation.Belonging newBelonging = new Domain.BelongingCreation.Belonging();
            newBelonging.Category = cbCategory.SelectedItem.ToString();
            newBelonging.GenericDescription = tbDescription.Text;
            newBelonging.Features = tbFeature.Text;
            newBelonging.SerialNumber = tbSerialNumber.Text;
            newBelonging.Model = tbModel.Text;
            newBelonging.ApraisalAmount = int.Parse(tbApraisalAmount.Text);
            newBelonging.LoanAmount = int.Parse(tbLoanAmount.Text);
            newBelonging.PorcentLoan = ((int.Parse(tbApraisalAmount.Text)) * 100) / (int.Parse(tbLoanAmount.Text));
            if (!edicion)
            {
                belongingsList.Add(newBelonging);
                CleanComponents();
            }
            else
            {
                belongingsList[posicionEdicion] = newBelonging;
                ClosePage();
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
            tbLoanAmount.Text = "";
        }
    }
}
