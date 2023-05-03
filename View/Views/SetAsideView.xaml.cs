using BusinessLogic;
using DataAcces;
using Domain;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for SetAsideView.xaml
    /// </summary>
    public partial class SetAsideView : Page, MessageService
    {


        public SetAsideView()
        {
            InitializeComponent();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddArticle_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;
            ScanCodeView scan = new ScanCodeView();
            scan.CommunicacionPages(this);
            window.SecundaryContainer.Navigate(scan);
            window.PrimaryContainer.IsHitTestVisible = false;
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;
            window.SecundaryContainer.Navigate(new CreateCustomerRecord());
            window.PrimaryContainer.IsHitTestVisible = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            this.Content = null;

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;
            (App.Current as App)._cashOnHand = 1000;
            window.SecundaryContainer.Navigate(new TransactionView(OperationType.OPERATION_SEAL, 658.50, 0));
            window.PrimaryContainer.IsHitTestVisible = false;
        }
        /// <summary>
        /// Metodo que se encarga de la comunicacion entre las paginas
        /// Se activa cuando se escanea un codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="result"></param>
        public void Communication(string code, bool result)
        {
            if (result)
            {

            }
            else
            {
                ErrorManager.ShowError("No pagado");
            }
            Console.WriteLine(code);
        }
    }
}
