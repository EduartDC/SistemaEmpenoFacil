﻿using BusinessLogic;
using BusinessLogic.Utility;
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
    /// Lógica de interacción para ModifyEmployee.xaml
    /// </summary>
    public partial class ModifyEmployee : Page
    {

        private Staff actualStaff = null;
        public ModifyEmployee()
        {
            InitializeComponent();
            AddStatusComboBoxElements();
            AddRoleComboxBoxElements();
            showStaffInformation(4);
        }

        private void clicModifyButton(object sender, RoutedEventArgs e)
        {
            Staff emplyoee = new Staff();
            emplyoee.fisrtName = textBoxName.Text;
            emplyoee.lastName = textBoxLastName.Text;
            emplyoee.userName =textBoxUserName.Text;
            emplyoee.password = passwordBoxPassword.Password.ToString();
            try
            {
                emplyoee.statusStaff = comboBoxStatus.SelectedItem.ToString();
                emplyoee.rol = comboBoxRole.SelectedItem.ToString();
            }
            catch (NullReferenceException) 
            {
                emplyoee.statusStaff = "";
                emplyoee.rol = "";
            }
            if (emplyoee.fisrtName.Length != 0 && emplyoee.lastName.Length != 0 && emplyoee.userName.Length != 0
                && emplyoee.statusStaff.Length != 0 && emplyoee.rol.Length != 0)
            {

                if (Utilities.ValidateInput(emplyoee.fisrtName) && Utilities.ValidateInput(emplyoee.lastName) &&
                    Utilities.ValidateInput(emplyoee.userName))
                {
                    if(emplyoee.password.Length == 0)
                    {
                        emplyoee.password = actualStaff.password;
                        labelInvalidInformation.Content = string.Empty;
                        int successfulModification = StaffDAO.ModifyStaff(actualStaff.idStaff, emplyoee);
                        if (successfulModification != 300 && successfulModification != 0)
                        {
                            string message = "La informacion ha sido actualizada correctamente";
                            string messageTitle = "Modificacion exitosa";
                            MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                            MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                            MessageBoxResult messageBox;
                            messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                        }
                        else
                        {
                            string message = "No se pudo actualizar la informacion correctamente";
                            string messageTitle = "Error inesperado";
                            MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                            MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                            MessageBoxResult messageBox;
                            messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                        }
                    }
                    else
                    {
                        if (Utilities.ValidatePassword(emplyoee.password))
                        {
                            labelInvalidInformation.Content = string.Empty;
                            emplyoee.password = passwordBoxPassword.Password.ToString();
                            int successfulModification = StaffDAO.ModifyStaff(actualStaff.idStaff, emplyoee);
                            if(successfulModification != 300 && successfulModification != 0)
                            {
                                string message = "La informacion ha sido actualizada correctamente";
                                string messageTitle = "Modificacion exitosa";
                                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                                MessageBoxResult messageBox;
                                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                            }
                            else
                            {
                                string message = "No se pudo actualizar la informacion correctamente";
                                string messageTitle = "Error inesperado";
                                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                                MessageBoxResult messageBox;
                                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                            }

                        }
                        else
                        {
                            labelInvalidInformation.Content = "Contraseña no valida";
                        }
                    }
                }
                else
                {
                    labelInvalidInformation.Content = "Informacion invalida en 1 o mas campos por favor corrija";
                }
            } else 
            {
                string message = "No se puede completar la operacion porque uno o mas campos se encuentran vacios por favor complete el formulario";
                string messageTitle = "Campos de informacion vacios";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }
        }

        private void ClicCancelButton(object sender, RoutedEventArgs e)
        {
            string message = "Esta seguro que desea cancelar la operacion?\n\n"
                + "No se realizara ningun cambio en su informacion si desea cancelar esta operacion";
            string messageTitle = "Cancelar Operacion";
            MessageBoxButton messageBoxButton = MessageBoxButton.YesNo;
            MessageBoxImage messageBoxImage = MessageBoxImage.Question;
            MessageBoxResult messageBox;
            messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
        }

        private void AddStatusComboBoxElements()
        {
            string[] statusList = { "Activo", "Bloqueado" };
            comboBoxStatus.Items.Clear();
            foreach(string status in statusList){
                comboBoxStatus.Items.Add(status);
            }
        }

        private void AddRoleComboxBoxElements()
        {
            string[] roleList = { "Gerente", "Administrador", "Cajero" };
            comboBoxRole.Items.Clear();
            foreach (string role in roleList)
            {
                comboBoxRole.Items.Add(role);
            }
        }

        private void showStaffInformation(int idUser)
        {
            Staff staff = StaffDAO.GetStaff(idUser);
            textBoxName.Text = staff.fisrtName;
            textBoxLastName.Text = staff.lastName;
            textBoxUserName.Text = staff.userName;
            actualStaff = staff;
            comboBoxRole.SelectedItem = actualStaff.rol;
            comboBoxStatus.SelectedItem = actualStaff.statusStaff;
            rfcLabel.Content = staff.rfc;
        }
    }
}
