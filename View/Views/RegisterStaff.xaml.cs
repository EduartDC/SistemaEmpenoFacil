using BusinessLogic.Utility;
using BusinessLogic;
using DataAcces;
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
    /// Lógica de interacción para RegisterStaff.xaml
    /// </summary>
    public partial class RegisterStaff : Page
    {
        public RegisterStaff()
        {
            InitializeComponent();
            comBox_Role.Items.Add("Cajero");
            comBox_Role.Items.Add("Gerente");
            comBox_Role.Items.Add("Administrador");
        }


        private void ClicCancelButton(object sender, RoutedEventArgs e)
        {

        }

        private void ClicRegisterButton(object sender, RoutedEventArgs e)
        {
            if (ValidateFormats())
            {
                Staff newStaff = new Staff();
                newStaff.fisrtName = text_Name.Text;
                newStaff.lastName = text_LastName.Text;
                newStaff.statusStaff = "Activo";
                String passwordEncode = Utilities.Hash(textPassword_Password.Password);
                newStaff.userName = text_UserName.Text;
                newStaff.password = passwordEncode;
                newStaff.rol = comBox_Role.SelectedItem.ToString();
                RegisterNewStaff(newStaff);
            }
        }


        private void RegisterNewStaff(Staff newStaff)
        {
            switch (StaffDAO.RegisterStaff(newStaff))
            {
                case 500:
                    MessageBox.Show("No se ha podido conectar con la base de datos, favor de intentarlo más tarde");
                    break;

                case 400:
                    MessageBox.Show("Error al realizar el registro, favor de intentarlo más tarde");
                    break;

                case 200:
                    MessageBox.Show("Registro Exitoso");
                    break;
            }
        }

        private bool ValidateFormats()
        {
            bool result = true;
            if (!Utilities.ValidateFormat(text_Name.Text, "^[a-zA-Z]+([ \\-][a-zA-Z]+)*$") || !Utilities.ValidateFormat
                (text_LastName.Text, "^[a-zA-Z]+([ \\-][a-zA-Z]+)*$"))
            {
                MessageBox.Show("El nombre y apellido solo acepta letras y sin acentos, favor de verificar");
                result = false;
            }
            if (text_UserName.Text.Equals("") || textPassword_Password.Password.Equals(""))
            {
                MessageBox.Show("Campos vacios, favor de llenar todos los campos");
                result = false;
            }
            return result;
        }
    }
}
