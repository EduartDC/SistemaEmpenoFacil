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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View.Properties;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for LiquidateContractView.xaml
    /// </summary>
    public partial class LiquidateContractView : Page
    {
        double _total;
        public LiquidateContractView(int idContract)
        {
            InitializeComponent();
            SetInformation(idContract);
        }

        private void SetInformation(int idContract)
        {
            try
            {
                //Contract information
                var contract = ContractDAO.GetContract(idContract);
                if (contract != null)
                {
                    labelDeadLine.Content = contract.deadlineDate;
                    labelSatartDate.Content = contract.creationDate;
                    var time = DateTime.Now - contract.creationDate;
                    labelTime.Content = "Tiempo transcurrido: " + time.Days;
                    labelPayDay.Content = "Fecha de Pago: " + contract.creationDate.Day;

                    //Customer information
                    var customer = CustomerDAO.GetCustomer(contract.Customer_idCustomer);
                    if (customer != null)
                    {
                        labelNameCustomer.Content = customer.firstName + " " + customer.lastName;
                        labelCURPCustomer.Content = customer.curp;
                    }

                    //Articles information
                    var belongings = BelongingDAO.GetAllBelongingsFromContract(contract.idContract);
                    if (belongings != null)
                    {
                        tableBelongings.ItemsSource = belongings;

                    }
                    CalculateFee(contract);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.ShowError(ex.Message);
            }

        }

        private void CalculateFee(Contract contract)
        {
            var interestRate = contract.interestRate;
            double interest = (double)(interestRate / 100.0);
            double iva = 1 + (contract.iva / 100.0);
            var loan = 1500;//contract.loanAmount;
            var duration = 6;// contract.duration;
            var interestAmount = (loan * duration * interest * iva);
            var total = loan + interestAmount;

            labelSubTotal.Content = loan.ToString("0.00");
            labelIva.Content = interestAmount.ToString("0.00");
            labelTotal.Content = total.ToString("0.00");

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLiquidate_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;
            (App.Current as App)._cashOnHand = 1000;
            window.SecundaryContainer.Navigate(new TransactionView(MessageCode.ope, _total));
            window.PrimaryContainer.IsHitTestVisible = false;
        }
    }
}
