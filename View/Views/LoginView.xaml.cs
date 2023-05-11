using BusinessLogic;
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
using View.Properties;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Staff userLogin = GetInfoUser();
            if (string.IsNullOrEmpty(userLogin.userName) && string.IsNullOrEmpty(userLogin.password))
            {
                ErrorManager.ShowWarning(MessageError.FIELDS_EMPTY);
            }
            else if (!VerifyUserInfo(userLogin))
            {
                ErrorManager.ShowInformation(MessageError.USER_AND_PASSWORD_NOT_FOUND);
            }
            else
            {
                var window = (MainWindow)App.Current.MainWindow;
                window.PrimaryContainer.Navigate(new MenuView());

            }
        }

        private Staff GetInfoUser()
        {
            var userName = textUserName.Text;
            var password = textPassword.Password;
            Staff user = new Staff();

            user.userName = userName;
            user.password = password;

            return user;
        }

        private bool VerifyUserInfo(Staff userLoginInfo)
        {
            var result = false;
            try
            {
                Staff user = StaffDAO.LogingStaff(userLoginInfo.userName, userLoginInfo.password);
                if (user != null && user.userName.Equals(userLoginInfo.userName) && user.password.Equals(userLoginInfo.password))
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
            }

            return result;
        }
    }
}
