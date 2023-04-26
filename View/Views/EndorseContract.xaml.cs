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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para EndorseContract.xaml
    /// </summary>
    public partial class EndorseContract : Page
    {
        public EndorseContract()
        {
            InitializeComponent();
            ShowContract("123456");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowContract(string idContract)
        {
            Contract contract = ContractDAO.GetContract(idContract);
            labelEndorsementAmount.Content = GetAmounts(contract.paymentsEndorsement); // monto de refrendo buscar el monto de refrendo
            labelLoanAmount.Content = contract.loanAmount.ToString(); // monto de prestamo
            labelSettlementAmount.Content= GetAmounts(contract.paymentsSettlement); //monto de liquidacion buscar el monto de liquidacion
            labelClientName.Content = contract.Customer.firstName + " " +contract.Customer.lastName;
            labelPawnNumber.Content = contract.idContract;
        }

        private string GetAmounts(string amounts)
        {
            string[] listAmounts = amounts.Split(';');
            return listAmounts[0];
        }

        private void EndorseContractButtonEvent(object sender, RoutedEventArgs e)
        {

        }

        private void goBackButtonEvent(object sender, RoutedEventArgs e)
        {

        }
    }
}
