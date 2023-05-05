using BusinessLogic;
using BusinessLogic.Utility;
using DataAcces;
using Domain;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View.Properties;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for AuthorizationView.xaml
    /// </summary>
    public partial class AuthorizationView : Page
    {
        MessageService communication;
        public AuthorizationView()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = ErrorManager.ShowQuestion(MessageError.CANCEL_OPERATION);

            if (result == MessageBoxResult.Yes)
            {
                ClosePage();

            }
        }

        private void ClosePage()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 0;
            window.PrimaryContainer.Effect = blurEffect;
            window.SecundaryContainer.Content = null;
            window.PrimaryContainer.IsHitTestVisible = true;
        }

        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {

            var userName = textUser.Text;
            var password = textPassword.Password;


            if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                ErrorManager.ShowWarning(MessageError.FIELDS_EMPTY);
            }
            else
            {
                var result = ValidationStaff(userName, password);
                if (result)
                {
                    communication.Communication(result);
                }
            }
        }

        private bool ValidationStaff(string userName, string password)
        {
            var result = false;
            try
            {
                var infoStaff = StaffDAO.LogingStaff(userName, password);
                if (infoStaff == null)
                {
                    ErrorManager.ShowWarning(MessageError.USER_NOT_FOUND);
                }
                else if (!infoStaff.userName.Equals(textUser.Text) || !infoStaff.password.Equals(textPassword.Password))
                {
                    ErrorManager.ShowWarning(MessageError.USER_NOT_FOUND);
                }
                else
                {
                    ErrorManager.ShowInformation(MessageError.USER_FOUND);
                    result = true;
                    ClosePage();
                }
            }
            catch (Exception)
            {
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
            }
            return result;
        }

        public void CommunicacionPages(MessageService communication)
        {

            this.communication = communication;

        }

        private void textUser_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text, 0) && !Utilities.ValidateInput(e.Text))
            {
                e.Handled = true;
            }
        }

        private void textPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text, 0) && !Utilities.ValidateInput(e.Text))
            {
                e.Handled = true;
            }
        }
    }
}
