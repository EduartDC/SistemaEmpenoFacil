using BusinessLogic;
using DataAcces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
        int _id;

        Dictionary<int, Tuple<string, bool, bool>> operationMappings = new Dictionary<int, Tuple<string, bool, bool>>()
        {
                { OperationType.OPERATION_SEAL, new Tuple<string, bool, bool>("Venta", true, false) },
                    { OperationType.OPERATION_SETASIDE, new Tuple<string, bool, bool>("Apartado", true, false) },
                        { OperationType.OPERATION_LOAND, new Tuple<string, bool, bool>("Prestamo", false, false) },
                            { OperationType.OPERATION_PROFIT, new Tuple<string, bool, bool>("Pago de ganancia", false, false) },
                                { OperationType.OPERATION_LIQUIDATE, new Tuple<string, bool, bool>("Liquidacion de contrato", true, false)},
                                    { OperationType.OPERATION_RENEWAL, new Tuple < string, bool, bool >("Renovacion de contrato", true, false)             }
        };

        public TransactionView(int operation, double amount, int id)
        {
            InitializeComponent();
            _amount = amount;
            _operation = operation;
            _id = id;
            var date = DateTime.Now.ToString("dd/MM/yyyy");
            var time = DateTime.Now.ToString("hh:mm:ss");
            labelTotal.Text = "Monto Total: $" + amount;
            labelDate.Content = "Fecha: " + date;
            labelTime.Content = "Hora: " + time;
            labelBranch.Content = "Sucursal:  Av. Americas";

            if (operationMappings.ContainsKey(operation))
            {
                var operationTuple = operationMappings[operation];
                if (operation == OperationType.OPERATION_LOAND || operation == OperationType.OPERATION_PROFIT)
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
            try
            {


                switch (operationType)
                {
                    case OperationType.OPERATION_SEAL:

                        operation.paymentAmount = _amount;
                        operation.changeAmount = change;
                        operation.receivedAmount = amountReceived;
                        operation.operationDate = DateTime.Now;
                        operation.Staff_idStaff = (App.Current as App)._staffInfo.idStaff;
                        operation.Sale_idSale = _id;
                        result = OperationDAO.AddOperation(operation);
                        break;
                    case OperationType.OPERATION_SETASIDE:

                        operation.paymentAmount = _amount;
                        operation.changeAmount = change;
                        operation.receivedAmount = amountReceived;
                        operation.operationDate = DateTime.Now;
                        operation.Staff_idStaff = (App.Current as App)._staffInfo.idStaff;
                        operation.SetAside_idSetAside = _id;
                        result = OperationDAO.AddOperation(operation);
                        break;
                    case OperationType.OPERATION_LOAND:

                        operation.paymentAmount = _amount;
                        operation.changeAmount = change;
                        operation.receivedAmount = amountReceived;
                        operation.operationDate = DateTime.Now;
                        operation.Staff_idStaff = (App.Current as App)._staffInfo.idStaff;
                        operation.Contract_idContract = _id;
                        result = OperationDAO.AddOperation(operation);
                        break;
                    case OperationType.OPERATION_LIQUIDATE:

                        operation.paymentAmount = _amount;
                        operation.changeAmount = change;
                        operation.receivedAmount = amountReceived;
                        operation.operationDate = DateTime.Now;
                        operation.Staff_idStaff = (App.Current as App)._staffInfo.idStaff;
                        operation.Contract_idContract = _id;
                        result = OperationDAO.AddOperation(operation);
                        break;
                    case OperationType.OPERATION_RENEWAL:

                        operation.paymentAmount = _amount;
                        operation.changeAmount = change;
                        operation.receivedAmount = amountReceived;
                        operation.operationDate = DateTime.Now;
                        operation.Staff_idStaff = (App.Current as App)._staffInfo.idStaff;
                        operation.Contract_idContract = _id;
                        result = OperationDAO.AddOperation(operation);
                        break;
                    default:

                        operation.paymentAmount = _amount;
                        operation.changeAmount = change;
                        //concept
                        operation.receivedAmount = amountReceived;
                        operation.operationDate = DateTime.Now;
                        operation.Staff_idStaff = (App.Current as App)._staffInfo.idStaff;
                        result = OperationDAO.AddOperation(operation);
                        break;
                }
            }
            catch
            {
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
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
            if (operationType == OperationType.OPERATION_LOAND || operationType == OperationType.OPERATION_PROFIT)
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
            else if (operationType == OperationType.OPERATION_SETASIDE || operationType == OperationType.OPERATION_SEAL)
            {
                double change = amountReceived - _amount;
                if (change < 0)
                {
                    ErrorManager.ShowWarning(MessageError.INSUFFICIENT_AMOUNT);

                }
                else if (SaveOperation(change, amountReceived) == MessageCode.ERROR)
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
                var window = (MainWindow)Application.Current.MainWindow;
                BlurEffect blurEffect = new BlurEffect();
                blurEffect.Radius = 0;
                window.PrimaryContainer.Effect = blurEffect;
                window.SecundaryContainer.Content = null;
                window.PrimaryContainer.IsHitTestVisible = true;
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
