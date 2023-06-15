using BusinessLogic;
using BusinessLogic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
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


        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            if (Utilities.ValidateFormat(text_CURP.Text.Trim(), "^[A-Z]{4}\\d{6}[HM]{1}[A-Z]{6}[0-9]{1}$")) 
            {
                try
                {
                    int resultFindCustomer = CustomerDAO.ExistCustomer(text_CURP.Text);
                    if (resultFindCustomer == 200)
                    {
                        ChangeStatus();
                    }
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Cliente no encontrado, favor de agregar un numero de cliente existente");
                }
                catch (Exception)
                {
                    MessageBox.Show("Error con la base de datos, favor de intentarlo más tarde ");
                }
            }
            else
            {
                MessageBox.Show("Favor de corroborar el formado del CURP ingresado");
            }
            
        }

        private void ChangeStatus()
        {
            try
            {
                int resultChangeStatus = CustomerDAO.ChangeStatusBlackList(text_CURP.Text.Trim());
                if (resultChangeStatus == 200)
                {
                    MessageBox.Show("Se agrego un nuevo cliente a la lista negra");
                    Close();
                }
                else if (resultChangeStatus == 502)
                {
                    MessageBox.Show("El cliente que desea agregar ya se encuentra en la lista negra");
                }
            } 
            catch (ArgumentNullException)
            {
                MessageBox.Show("Cliente no encontrado");
            }
            catch (Exception)
            {
                MessageBox.Show("Error al agregar al cliente a la lista negra, favor de intentarlo más tarde");
            }
        }
           
        

        private void Btn_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
