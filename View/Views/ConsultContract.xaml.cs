using BusinessLogic;
using DataAcces;
using Domain;
using Org.BouncyCastle.Crypto.Encodings;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    /// Lógica de interacción para ConsultContract.xaml
    /// </summary>
    public partial class ConsultContract : Window
    {

        int _idContract;
        ContractDomain contract1;
        

        public ConsultContract(int idContract)
        {
            _idContract = idContract;
            SetInformationAsync(_idContract);
            InitializeComponent();
        }

        private void Btn_Salir_Click(object sender, RoutedEventArgs e)
        {
             Close();
        }

        private async Task SetInformationAsync(int idContract)
        {
            try
            {
                var contract = await Task.Run(() => ContractDAO.GetContractsDomainAsync(idContract));
                contract1 = contract;
                if (contract == null)
                {
                    ErrorManager.ShowWarning("No se encontro el contrato.");
                }
                else
                {
                    double appraisalAmount=0;
                    contract.Belongings.ForEach(belondings => appraisalAmount += belondings.appraisalValue);
                    tbAppraisalAmount.Text = "" + appraisalAmount;
                    tbNameCustomer.Text = contract.Customer.firstName + " " + contract.Customer.lastName;
                    tbCustomerCurp.Text = contract.Customer.curp;
                    dgBelongings.ItemsSource = contract.Belongings;
                    tbLoanPorcentage.Text = contract.loanProcentage;
                    tbDateComercialization.Text = "" + contract.deadlineDate.AddDays(15);
                    tbPaymentLimit.Text = "" + contract.deadlineDate;
                    tbLoanAmount.Text = "" + contract.loanAmount;
                    tbInterests.Text = "" + contract.interestRate;
                    tbIVA.Text = "" + contract.iva;
                    tbAnnualInterestRate.Text = "" + contract.annualInterestRate;
                    tbDateEndorsementSettlement.Text = contract.endorsementSettlementDates.ToString();
                    tbTotalPerformancePay.Text = contract.paymentsEndorsement;
                    tbTotalPaymentForEndorsement.Text = contract.paymentsSettlement.ToString();
                    BlockTextBox();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.ShowError(ex.Message);
            }
        }

        private void BlockTextBox()
        {
            tbPercentage.IsEnabled = false;
            dgBelongings.IsEnabled = false;
            tbCustomerCurp.IsEnabled = false;
            tbNameCustomer.IsEnabled = false;
            tbAppraisalAmount.IsEnabled = false;
            tbLoanPorcentage.IsEnabled = false;
            tbInterests.IsEnabled = false;
            tbIVA.IsEnabled = false;
            tbTotalEndorsement.IsEnabled = false;
            tbDateComercialization.IsEnabled = false;
            tbLoanAmount.IsEnabled = false;
            tbPaymentLimit.IsEnabled = false;
            tbDateEndorsementSettlement.IsEnabled = false;
            tbAnnualInterestRate.IsEnabled = false;
            tbTotalPaymentForEndorsement.IsEnabled = false;
            tbTotalPerformancePay.IsEnabled = false;
        }

        private void Btn_PrintContract_Click(object sender, RoutedEventArgs e)
        {
            PrintContract.NewPrintCotract(contract1);
        }
    }
}
