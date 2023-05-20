using BusinessLogic;
using DataAcces;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    public partial class LiquidateContractView : Page, MessageService
    {
        double _total;
        int _id;
        public LiquidateContractView(int idContract)
        {

            _id = idContract;
            labelValidation.Visibility = Visibility.Hidden;
            SetInformationAsync(idContract);
            InitializeComponent();
        }

        private async Task SetInformationAsync(int idContract)
        {
            try
            {
                var contract = await Task.Run(() => ContractDAO.GetContractsDomainAsync(idContract));
                if (contract == null)
                {
                    ErrorManager.ShowWarning("No se encontro el contrato.");
                }
                else if (contract.stateContract.Equals(StatesContract.CANCELED_CONTRACT) || contract.stateContract.Equals(StatesContract.COMPLETED_CONTRACT))
                {
                    ErrorManager.ShowWarning("Este contrato no es apto para su liquidacion, es posible que ya este liquidado o cancelado.");
                    //regresa a consultar contratos
                }
                else
                {
                    labelDeadLine.Content = "Fecha Limite: " + contract.deadlineDate;
                    labelSatartDate.Content = "Fecha de Inicio: " + contract.creationDate;
                    var time = DateTime.Now - contract.creationDate;
                    labelTime.Content = "Tiempo transcurrido: " + time.Days + " Dias.";
                    labelPayDay.Content = "Fecha de Pago: " + contract.creationDate.Day;
                    labelNameCustomer.Content = "Nombre: " + contract.Customer.firstName + " " + contract.Customer.lastName;
                    labelCURPCustomer.Content = "CURP: " + contract.Customer.curp;
                    tableBelongings.ItemsSource = contract.Belongings;
                    CalculateFee(contract);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.ShowError(ex.Message);
            }
        }
        /// <summary>
        /// Metodo para calcular el total a pagar por la liquidacion
        /// </summary>
        /// <param name="contract">El contrato que se va a liquidar</param>
        private void CalculateFee(ContractDomain contract)
        {
            var interestRate = contract.interestRate;
            double interest = (double)(interestRate / 100.0);
            double iva = 1 + (contract.iva / 100.0);
            var loan = contract.loanAmount;
            var duration = contract.duration;
            var interestAmount = (loan * duration * interest * iva);
            var total = loan + interestAmount;
            _total = total;

            labelSubTotal.Content = "SubTotal: " + loan.ToString("0.00");
            labelIva.Content = "Intereses : " + interestAmount.ToString("0.00");
            labelTotal.Content = "Total: " + total.ToString("0.00");

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void btnLiquidate_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;
            TransactionView newOperation = new TransactionView(OperationType.OPERATION_LIQUIDATE, _total, _id);
            newOperation.CommunicacionPages(this);
            window.SecundaryContainer.Navigate(newOperation);
            window.PrimaryContainer.IsHitTestVisible = false;
        }

        public void Communication(bool result)
        {
            if (result)
            {
                ErrorManager.ShowInformation("El pago se concreto.");
                SaveChanges();
                labelValidation.Visibility = Visibility.Visible;
                btnLiquidate.IsEnabled = false;
            }
            else
            {
                ErrorManager.ShowWarning("El pago no se concreto.");
            }
        }

        private async Task SaveChanges()
        {
            try
            {
                var contract = new ContractDomain();
                contract.idContract = _id;
                contract.stateContract = StatesContract.COMPLETED_CONTRACT;
                contract.settlementAmount = _total;
                ContractDAO.LiquidateContract(contract);
            }
            catch (DbUpdateException)
            {
                ErrorManager.ShowError(MessageError.UPDATE_ERROR);
            }
            catch (Exception)
            {
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
            }
        }

        public void ScanCommunication(ArticleDomain article)
        {
            throw new NotImplementedException();
        }
    }
}
