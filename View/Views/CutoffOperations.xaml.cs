using BusinessLogic;
using DataAcces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for CutoffOperations.xaml
    /// </summary>
    public partial class CutoffOperations : Page, MessageService
    {
        List<OperationDomain> _list;
        public CutoffOperations()
        {
            InitializeComponent();

            SetInformation();
        }

        private async Task SetInformation()
        {
            var staff = (App.Current as App)._staffInfo;
            _list = await Task.Run(() => OperationDAO.GetAllOperationsByDate(DateTime.Now, 1));
            tableOperations.ItemsSource = _list;
            labelIdStaff.Content = "No. de Empleado: " + staff.idStaff;
            labelNameStaff.Content = "Nombre de Empleado: " + staff.fisrtName + " " + staff.lastName;
            labelDate.Content = "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy");
            labelTime.Content = DateTime.Now.ToString("hh:mm:ss tt");
            labelCountOperations.Content = "Cantidad de Operaciones: " + _list.Count.ToString();
            CalculateCachOnHand();
        }

        private void CalculateCachOnHand()
        {
            double startAmount = 1000;
            double totalAmount = 0.0;
            double totalCange = 0.0;
            double totalReceivedAmount = 0.0;
            foreach (var item in _list)
            {
                if (item.concept.Equals("Inicio de Turno"))
                {
                    startAmount = item.receivedAmount;
                }
                else
                {
                    totalAmount += item.paymentAmount;
                    totalCange += item.changeAmount;
                    totalReceivedAmount += item.receivedAmount;
                }
            }
            double totalCashOnHand = (startAmount + totalAmount) - totalCange;
            labelTotalCashOnHand.Content = "Total en Caja: " + totalCashOnHand;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }
        private void btnCutoff_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;
            (App.Current as App)._cashOnHand = 1000;
            AuthorizationView newOperation = new AuthorizationView();
            newOperation.CommunicacionPages(this);
            window.SecundaryContainer.Navigate(newOperation);
            window.PrimaryContainer.IsHitTestVisible = false;
        }

        public void Communication(ArticleDomain code, bool result)
        {
            if (result)
            {
                (App.Current as App)._staffShift = false;
                ErrorManager.ShowInformation(MessageError.CUTOFF_SUCCESS);
            }
        }
    }
}
