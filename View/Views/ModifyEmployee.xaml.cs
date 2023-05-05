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
        public ModifyEmployee()
        {
            InitializeComponent();
        }

        private void clicModifyButton(object sender, RoutedEventArgs e)
        {

            Staff emplyoee = new Staff();
            emplyoee.fisrtName =textBoxName.Text;
            emplyoee.lastName =textBoxLastName.Text;
            emplyoee.userName =textBoxUserName.Text;
            emplyoee.password = passwordBoxPassword.Password.ToString();
            emplyoee.statusStaff = comboBoxStatus.SelectedItem.ToString();
            emplyoee.rol = comboBoxRole.SelectedItem.ToString();

            string message = "Informacion del empleado: " + emplyoee.fisrtName + " " + emplyoee.lastName
                + "\n" + emplyoee.userName + "\n" + emplyoee.password + "\n" + emplyoee.statusStaff + "\n"
                + emplyoee.rol;
            string messageTitle = "Modificacion exitosa";
            MessageBoxButton messageBoxButton = MessageBoxButton.OK;
            MessageBoxImage messageBoxImage = MessageBoxImage.Information;
            MessageBoxResult messageBox;
            messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
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
    }
}
