﻿using BusinessLogic;
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

        private Contract actualContract = null;
        public EndorseContract()
        {
            InitializeComponent();
            ShowContract(9);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowContract(int idContract)
        {
            Contract contract = ContractDAO.GetContract(idContract);
            labelEndorsementAmount.Content = GetAmounts(contract.paymentsEndorsement); // monto de refrendo buscar el monto de refrendo
            labelLoanAmount.Content = contract.loanAmount.ToString() + " $"; // monto de prestamo
            labelSettlementAmount.Content= GetAmounts(contract.paymentsSettlement); //monto de liquidacion buscar el monto de liquidacion\
            Customer customer = CustomerDAO.GetCustomer(contract.Customer_idCustomer);
            labelClientName.Content = customer.firstName + " " + customer.lastName;
            labelPawnNumber.Content = contract.idContract;
            actualContract = contract;
        }

        private string GetAmounts(string amounts)
        {
            string[] listAmounts = amounts.Split(';');
            return listAmounts[0];
        }

        private void EndorseContractButtonEvent(object sender, RoutedEventArgs e)
        {
            if (actualContract.idContractPrevious != null)
            {
                string message = "Ya se ha refrendado anteiormente este contrato, solo dispone de liquidacion";
                string messageTitle = "No se puede volver a refrendar";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage= MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }
            else
            {
                ContractDAO.ModifyContract(actualContract, actualContract.idContract);
            }
        }

        private void goBackButtonEvent(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
