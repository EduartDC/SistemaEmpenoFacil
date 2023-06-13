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
using System.Data.Entity.Infrastructure;

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
            comboBoxRole.Items.Add("Cajero");
            comboBoxRole.Items.Add("Gerente");
            comboBoxRole.Items.Add("Administrador");
        }


        private void ClicCancelButton(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void ClicRegisterButton(object sender, RoutedEventArgs e)
        {
            if (ValidateFormats())
            {
                try
                {
                    switch (StaffDAO.ExistStaff(textBoxRFC.Text))
                    {
                        case 200:
                            if (StaffDAO.GetStaffByUserName(textBoxUserName.Text) == null)
                            {
                                NewStaff();
                            }
                            else
                            {
                                MessageBox.Show("El nombre de usuario que intenta registrar ya se encuntra registrado, favor de cambiarlo");
                            }
                            break;
                        case 100:
                            MessageBox.Show("El RFC ingresado ya ha sido registrado en el sistema, favor de registrar un RFC que no este en el sistema");
                            break;
                    }
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Error al validar la existencia del cliente, favor de intentarlo más tarde");
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al conectarse a la base de datos, favor de intentarlo más tarde");
                }
            }
        }

        private void NewStaff()
        {
            Staff newStaff = new Staff();
            newStaff.fisrtName = textBoxName.Text;
            newStaff.lastName = textBoxLastName.Text;
            newStaff.statusStaff = "Activo";
            String passwordEncode = Utilities.Hash(passwordBoxPassword.Password);
            newStaff.userName = textBoxUserName.Text;
            newStaff.password = passwordEncode;
            newStaff.rol = comboBoxRole.SelectedItem.ToString();
            newStaff.rfc = textBoxRFC.Text;
            ValidateRegisterNewStaff(newStaff);
        }

        private void ValidateRegisterNewStaff(Staff newStaff)
        {
            try
            {
                int resultRegister = StaffDAO.RegisterStaff(newStaff);
                if(resultRegister == 200)
                {
                    MessageBox.Show("Registro exitoso ");
                    this.Content = null;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no se ha podido registrar al cliente en el sistema, favor de intentarlo más tarde");
            }
        }



        private bool ValidateFormats()
        {
            bool result = true;
            ResetLabelsError();

            if (!Utilities.ValidateFormat(textBoxName.Text.Trim(), "^[a-zA-Z]+([ \\-][a-zA-Z]+)*$"))
            {
                label_ErrorName.Visibility = Visibility.Visible;
                result = false;
            } 
            if(!Utilities.ValidateFormat(textBoxLastName.Text.Trim(), "^[a-zA-Z]+([ \\-][a-zA-Z]+)*$"))
            {
                label_ErrorLastName.Visibility = Visibility.Visible;
                result = false;
            }
            if(!Utilities.ValidateFormat(textBoxUserName.Text.Trim(), "^[a-zA-Z]+$"))
            {
                label_ErrorUsername.Visibility = Visibility.Visible;
                result=false;
            }
            if (!Utilities.ValidatePassword(passwordBoxPassword.Password.Trim()))
            {
                label_ErrorPassword.Visibility = Visibility.Visible;
                result = false;
            }
            if(comboBoxRole.SelectedIndex == -1)
            {
                MessageBox.Show("Favor de seleccionar el rol del empleado");
            }
            if(!Utilities.ValidateFormat(textBoxRFC.Text.Trim(), "^[A-ZÑ&]{3,4}[0-9]{6}[A-Z0-9]{3}$"))
            {
                label_ErrorRFC.Visibility = Visibility.Visible;
                result = false;
            }
            return result;
        }

        private void ResetLabelsError()
        {
            label_ErrorName.Visibility = Visibility.Hidden;
            label_ErrorLastName.Visibility = Visibility.Hidden;
            label_ErrorUsername.Visibility = Visibility.Hidden;
            label_ErrorPassword.Visibility = Visibility.Hidden;
            label_ErrorRFC.Visibility = Visibility.Hidden;
        }
    }
}
