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
    /// Lógica de interacción para StartShift.xaml
    /// </summary>
    public partial class StartShift : Page
    {
        double _amount;
        int _operation;
        public StartShift()
        {
            InitializeComponent();
            var staff = (App.Current as App)._staffInfo;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string text = textAmountReceived.Text;
            double amount = Double.Parse(textAmountReceived.Text);
            if (text.Length > 6)
            {
                ErrorManager.ShowWarning(MessageError.AMOUNT_RECEIVED_ERROR);
            }
            else if (string.IsNullOrEmpty(text))
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
                saveOperation();
                var window = (MainWindow)App.Current.MainWindow;
                window.PrimaryContainer.Navigate(new MenuView());

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 0;
            window.PrimaryContainer.Effect = blurEffect;
            window.SecundaryContainer.Content = null;
            window.PrimaryContainer.IsHitTestVisible = true;
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

        private void saveOperation()
        {
            var operationType = _operation;
            var operation = new Operation();
            var result = MessageCode.ERROR;
            double amount = Double.Parse(textAmountReceived.Text);
            operation.receivedAmount = amount;
            operation.operationDate = DateTime.Now;
            operation.concept = "Importe inicial";
            operation.Staff_idStaff = (App.Current as App)._staffInfo.idStaff;
            result = OperationDAO.AddOperation(operation);
            (App.Current as App)._cashOnHand += amount;

            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 0;
            window.PrimaryContainer.Effect = blurEffect;
            window.SecundaryContainer.Content = null;
            window.PrimaryContainer.IsHitTestVisible = true;
        }
       
        private double ParseAmount(string text)
        {
            var amountReceived = 0.0;
            try
            {
                amountReceived = double.Parse(text);
            }
            catch (FormatException)
            {
                ErrorManager.ShowWarning(MessageError.DECIMAL_FORMAT_ERROR);
            }
            return amountReceived;
        }


    }
}
