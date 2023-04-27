using BusinessLogic;
using Domain;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View.Properties;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for ScanCodeView.xaml
    /// </summary>
    public partial class ScanCodeView : Page
    {

        MessageService communication;

        public ScanCodeView()
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para buscar el código
            string code = "1234"; // Este es el código que encontraste
            communication.test(code);
        }
        public void CommunicacionPages(MessageService communication)
        {

            this.communication = communication;

        }


    }
}
