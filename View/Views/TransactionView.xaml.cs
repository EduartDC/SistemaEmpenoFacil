using BusinessLogic;
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
        public TransactionView(int operation, double amount)
        {
            InitializeComponent();
            _amount = amount;
            var date = DateTime.Now.ToString("dd/MM/yyyy");
            var time = DateTime.Now.ToString("hh:mm:ss");
            labelTotal.Text = "Monto Total: $" + amount;
            labelDate.Content = "Fecha: " + date;
            labelTime.Content = "Hora: " + time;
            labelBranch.Content = "Sucursal:  Av. Americas";

            Dictionary<int, Tuple<string, bool, bool>> operationMappings = new Dictionary<int, Tuple<string, bool, bool>>()
                {
                { MessageCode.OPERATION_SEAL, new Tuple<string, bool, bool>("Venta", true, false) },
                    { MessageCode.OPERATION_SETASIDE, new Tuple<string, bool, bool>("Apartado", true, false) },
                        { MessageCode.OPERATION_LOAND, new Tuple<string, bool, bool>("Prestamo", false, true) },
                            { MessageCode.OPERATION_PROFIT, new Tuple<string, bool, bool>("Pago de ganancia", false, true) }
                                };

            if (operationMappings.ContainsKey(operation))
            {
                var operationTuple = operationMappings[operation];
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
            } //validacion de cantidades de dinero
            else
            {
                try
                {
                    var amountReceived = double.Parse(textAmountReceived.Text);
                    double cange = amountReceived - _amount;
                    textChange.Text = cange.ToString();
                }
                catch (FormatException ex)
                {
                    ErrorManager.ShowWarning(MessageError.DECIMAL_FORMAT_ERROR);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {


            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;

            // Quitar el efecto de desenfoque del marco principal
            //frame1.Effect = null
            window.SecundaryContainer.Navigate(new CustomerView());

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
