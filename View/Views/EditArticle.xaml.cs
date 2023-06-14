using BusinessLogic;
using BusinessLogic.Utility;
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
using System.Windows.Shapes;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para EditArticle.xaml
    /// </summary>
    public partial class EditArticle : Window
    {
        int _idArticle;
        private Communication communication;
        public EditArticle(int idArticle,Communication communication)
        {
            InitializeComponent();
            _idArticle = idArticle;
            InitializeTextBox();
            this.communication = communication;
        }

        private void InitializeTextBox()
        {
            try
            {
                text_SellingPrice.Text =""+ ArticleDAO.RecoverSellingPrice(_idArticle);
            }
            catch(Exception)
            {
                MessageBox.Show("Error al intentar conectarse a la base de datos, favor de intentarlo más tarde");
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_Modify_Click(object sender, RoutedEventArgs e)
        {
            if (!Utilities.ValidateFormat(text_SellingPrice.Text, "^[0-9]+$"))
            {
                MessageBox.Show("Solo se aceptan numeros en el campo 'Precio de venta', favor de verificar la información");
            }
            else
            {
                try
                {
                    double sellingPriceModify = double.Parse(text_SellingPrice.Text);
                    int resultModify = ArticleDAO.ModifySellingPrice(_idArticle, sellingPriceModify);
                    if (resultModify == 200)
                    {
                        MessageBox.Show("Se ha modificado el precio de venta del articulo de manera exitosa");
                        communication.refreshArticles();
                        Close();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hubo un error en la base de datos al hacer la modificación del articulo, favor de intentarlo más tarde");
                }
                
            }
        }
    }
}
