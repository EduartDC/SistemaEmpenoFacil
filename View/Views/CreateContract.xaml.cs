using DataAcces;
using Domain.Communitation;
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
    /// Lógica de interacción para CreateContract.xaml
    /// </summary>
    public partial class CreateContract : Page  , Communication
    {
        private List<Domain.BelongingCreation.Belonging> belongingList = new List<Domain.BelongingCreation.Belonging>();
        private List<BitmapImage> bitmapImgList = new List<BitmapImage>();
        public CreateContract()
        {
            InitializeComponent();

        }






        //Incluye a CU06-Crear registro prendario
        private void ClicCreatePledgeRegister(object sender, RoutedEventArgs e)
        {
            CreateBelongingRegister createBelongingRegister = new CreateBelongingRegister();
            createBelongingRegister.CommunicacionPages(this, belongingList, bitmapImgList, false, dgBelongings.SelectedIndex);
            createBelongingRegister.ShowDialog();
        }

        //Metodo de interface

        public void refreshBelongings(List<Domain.BelongingCreation.Belonging> belongingsList, List<BitmapImage> bitmapImgList)
        {
            // MessageBox.Show("" + belongingList.Count);
            this.belongingList = belongingList;
            dgBelongings.ItemsSource = this.belongingList;
            dgBelongings.Items.Refresh();
            this.bitmapImgList = bitmapImgList;
        }

        private void ClickDeleteBelonging(object sender, RoutedEventArgs e)
        {
            //1-eliminar de prenda
            //2-eliminar de imagen
            //3recorrer elementos de imagen si es necesario(excepto si es el 4)

            //1 debo cambiar de lugar el 1 con el 2 y 3 creo
            if (dgBelongings.SelectedIndex >= 0)
            {

                //imagen
                if (dgBelongings.SelectedIndex == 0)
                {
                    for (int i = 0; i < 4; i++)
                        bitmapImgList.RemoveAt(0);
                }
                if (dgBelongings.SelectedIndex == 1)
                {
                    for (int i = 0; i < 4; i++)
                        bitmapImgList.RemoveAt(4);
                }
                if (dgBelongings.SelectedIndex == 2)
                {
                    for (int i = 0; i < 4; i++)
                        bitmapImgList.RemoveAt(8);
                }
                if (dgBelongings.SelectedIndex == 3)
                {
                    for (int i = 0; i < 4; i++)
                        bitmapImgList.RemoveAt(12);
                }
                if (dgBelongings.SelectedIndex == 4)
                {
                    for (int i = 0; i < 4; i++)
                        bitmapImgList.RemoveAt(16);
                }

                //prenda
                belongingList.RemoveAt(dgBelongings.SelectedIndex);
                dgBelongings.Items.Refresh();


            }
            else
                MessageBox.Show("fila no seleccionada");
        }

        private void ClickModifyBelonging(object sender, RoutedEventArgs e)
        {
            if (dgBelongings.SelectedIndex >= 0)
            {
                // MessageBox.Show("MODIFY");
                CreateBelongingRegister createBelongingRegister = new CreateBelongingRegister();
                createBelongingRegister.CommunicacionPages(this, belongingList, bitmapImgList, true, dgBelongings.SelectedIndex);
                createBelongingRegister.ShowDialog();
            }
            else
                MessageBox.Show("fila no seleccionada");
        }


    }
}
