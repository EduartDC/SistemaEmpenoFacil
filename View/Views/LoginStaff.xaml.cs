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
using System.Windows.Shapes;
using View.Properties;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para LoginStaff.xaml
    /// </summary>
    public partial class LoginStaff : Window
    {
        public LoginStaff()
        {
            InitializeComponent();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            if (txtBoxUserName.Text == "" || txtBoxPassword.Password == "")
            {
                ErrorManager.ShowWarning(MessageError.FIELDS_EMPTY);

            }
            else
            {
                (int code, Staff staff) = StaffDAO.validateStaff(txtBoxUserName.Text, txtBoxPassword.Password);

                if (code == MessageCode.SUCCESS)
                {
                    MainWindow mainWindow = new MainWindow();

                    mainWindow.staffReceiver(staff);

                    //aqui va el de meter dinero
                    Console.WriteLine("Login " + staff.rol);
                    //staffCommunication.refreshStaff(staff);
                    mainWindow.Show();

                    this.Close();
                }
                else if (code == MessageCode.ERROR_USER_NOT_FOUND)
                {
                    ErrorManager.ShowError(MessageError.ERROR_USER_NOT_FOUND);
                }

                else
                {
                    ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
                }

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
    

