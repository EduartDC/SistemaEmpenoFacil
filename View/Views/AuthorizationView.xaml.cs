using BusinessLogic;
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
        public AuthorizationView()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = ErrorManager.ShowQuestion(MessageError.CANCEL_OPERATION);

            if (result == MessageBoxResult.Yes)
            {
                var window = (MainWindow)Application.Current.MainWindow;
                BlurEffect blurEffect = new BlurEffect();
                blurEffect.Radius = 0;
                window.PrimaryContainer.Effect = blurEffect;
                window.SecundaryContainer.Content = null;
                window.PrimaryContainer.IsHitTestVisible = true;
            }
        }

        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
