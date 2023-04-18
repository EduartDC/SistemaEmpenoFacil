using BusinessLogic;
using DataAcces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for TransactionView.xaml
    /// </summary>
    public partial class TransactionView : Page
    {
        double _amount;
        int _operation;

        Dictionary<int, Tuple<string, bool, bool>> operationMappings = new Dictionary<int, Tuple<string, bool, bool>>()
        {
                { MessageCode.OPERATION_SEAL, new Tuple<string, bool, bool>("Venta", true, false) },
                    { MessageCode.OPERATION_SETASIDE, new Tuple<string, bool, bool>("Apartado", true, false) },
                        { MessageCode.OPERATION_LOAND, new Tuple<string, bool, bool>("Prestamo", false, false) },
                            { MessageCode.OPERATION_PROFIT, new Tuple<string, bool, bool>("Pago de ganancia", false, false) }
        };

        public TransactionView(int operation, double amount, int id)
        {
            InitializeComponent();
            _amount = amount;
            _operation = operation;
            var date = DateTime.Now.ToString("dd/MM/yyyy");
            var time = DateTime.Now.ToString("hh:mm:ss");
            labelTotal.Text = "Monto Total: $" + amount;
            labelDate.Content = "Fecha: " + date;
            labelTime.Content = "Hora: " + time;
            labelBranch.Content = "Sucursal:  Av. Americas";

            if (operationMappings.ContainsKey(operation))
            {
                var operationTuple = operationMappings[operation];
                if (operation == MessageCode.OPERATION_LOAND || operation == MessageCode.OPERATION_PROFIT)
                {
                    textChange.Text = amount.ToString();
                }
                labelTypeOperation.Content = "Tipo de Operacion: " + operationTuple.Item1;
                labelAmount.Content = "Monto de " + operationTuple.Item1 + ": $" + amount;
                textAmountReceived.IsEnabled = operationTuple.Item2;
                textChange.IsEnabled = operationTuple.Item3;
            }

        }



        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            string text = textAmountReceived.Text;

            if (string.IsNullOrEmpty(text))
            {
                ErrorManager.ShowWarning(MessageError.FIELDS_EMPTY);
            }
            else if (!text.Contains("."))
            {
                ErrorManager.ShowWarning(MessageError.DECIMAL_FORMAT_ERROR);
                textAmountReceived.Focus();
            }
            else
            {
                var amountReceived = ParseAmount(text);
                SetOperation(amountReceived);

            }
        }

        private int SaveOperation(double change, double amountReceived)
        {
            var operationType = _operation;
            var operation = new Operation();
            var result = MessageCode.ERROR;

            if (operationType == MessageCode.OPERATION_LOAND || operationType == MessageCode.OPERATION_PROFIT)
            {
                operation.paymentAmount = 0.0;
                operation.changeAmount = change;
                operation.receivedAmount = amountReceived;
                operation.operationDate = DateTime.Now;
                operation.Staff_idStaff = 1;
                result = OperationDAO.AddOperation(operation);
            }
            else if (operationType == MessageCode.OPERATION_SETASIDE || operationType == MessageCode.OPERATION_SEAL)
            {
                operation.paymentAmount = _amount;
                operation.changeAmount = change;
                operation.receivedAmount = amountReceived;
                operation.operationDate = DateTime.Now;
                operation.Staff_idStaff = 1;
                result = OperationDAO.AddOperation(operation);
            }
            return result;
        }

        private double ParseAmount(string text)
        {
            var amountReceived = 0.0;
            try
            {
                amountReceived = double.Parse(text);
            }
            catch (FormatException ex)
            {
                ErrorManager.ShowWarning(MessageError.DECIMAL_FORMAT_ERROR);
            }
            return amountReceived;
        }

        private void SetOperation(double amountReceived)
        {
            var operationType = _operation;
            if (operationType == MessageCode.OPERATION_LOAND || operationType == MessageCode.OPERATION_PROFIT)
            {
                double change = _amount;
                if (SaveOperation(change, amountReceived) == MessageCode.ERROR)
                {
                    ErrorManager.ShowWarning(MessageError.ERROR_ADD_OPERATION);
                }
                else if ((App.Current as App)._cashOnHand > change)
                {
                    (App.Current as App)._cashOnHand -= change;
                    textChange.Text = change.ToString();
                    ErrorManager.ShowInformation("El Cambio es de: " + change.ToString());

                }
                else
                {
                    //llamar caso de uso para ingresar dinero
                }
            }
            else if (operationType == MessageCode.OPERATION_SETASIDE || operationType == MessageCode.OPERATION_SEAL)
            {
                double change = amountReceived - _amount;

                if (SaveOperation(change, amountReceived) == MessageCode.ERROR)
                {
                    ErrorManager.ShowWarning(MessageError.ERROR_ADD_OPERATION);
                }
                else if ((App.Current as App)._cashOnHand > change)
                {
                    (App.Current as App)._cashOnHand += _amount;
                    (App.Current as App)._cashOnHand -= change;
                    textChange.Text = change.ToString();
                    ErrorManager.ShowInformation("El Cambio es de: " + change.ToString());
                    SaveOperation(change, amountReceived);
                }
                else
                {
                    //llamar caso de uso para ingresar dinero
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = ErrorManager.ShowQuestion(MessageError.CANCEL_OPERATION);

            if (result == MessageBoxResult.Yes)
            {
                //cerrar ventana
            }

        }

        private void textAmountReceived_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string inputText = e.Text;

            if (!string.IsNullOrEmpty(inputText) && !decimal.TryParse(inputText, out _) && inputText != ".")
            {
                e.Handled = true;
            }
        }

        private void textAmountReceived_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = string.Empty;
        }
    }
}
