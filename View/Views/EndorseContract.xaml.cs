using BusinessLogic;
using DataAcces;
using Domain;
using Domain.BelongingCreation;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para EndorseContract.xaml
    /// </summary>
    public partial class EndorseContract : Page, MessageService
    {

        private Contract actualContract = null;
        private List<Domain.BelongingCreation.Belonging> belongings= new List<Domain.BelongingCreation.Belonging>();
        private double settlementAmount;
        private double endorsementAmount;

        public EndorseContract(int IdContract)
        {
            InitializeComponent();
            ShowContract(IdContract);
            GetBelongingsOfContract(IdContract);
        }

        private void ConverterImagesFormat()
        {
            Console.WriteLine(belongings.Count());
            for (int i = 0; i < belongings.Count(); i++)
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = new MemoryStream(belongings[i].image);
                    bitmap.EndInit();
                    belongings[i].imageConverted = bitmap;
                }
                catch (ArgumentNullException)
                {
                    belongings[i].imageConverted = null;
                }

            }
        }

        private void GetBelongingsOfContract(int idContract)
        {
            belongings = BelongingDAO.GetBelongingByIdContract(idContract);
            ConverterImagesFormat();
            dataGridArticlesOfContract.ItemsSource = belongings;

        }

        private void ShowContract(int idContract)
        {
            Contract contract = ContractDAO.GetContract(idContract);
            endorsementAmount = double.Parse(GetAmounts(contract.paymentsEndorsement, contract.endorsementSettlementDates));
            labelEndorsementAmount.Content = endorsementAmount + " $"; // monto de refrendo buscar el monto de refrendo
            labelLoanAmount.Content = contract.loanAmount.ToString() + " $"; // monto de prestamo
            settlementAmount = double.Parse(GetAmounts(contract.paymentsSettlement, contract.endorsementSettlementDates));
            labelSettlementAmount.Content= settlementAmount + " $"; //monto de liquidacion buscar el monto de liquidacion\
            DataAcces.Customer customer = CustomerDAO.GetCustomer(contract.Customer_idCustomer);
            labelClientName.Content = customer.firstName + " " + customer.lastName;
            labelPawnNumber.Content = contract.idContract;
            actualContract = contract;
        }

        private string GetAmounts(string amounts, string endorsementSettlementsDates)
        {
            string[] listDates = endorsementSettlementsDates.Split(';');
            string[] listAmounts = amounts.Split(';');
            DateTime actualDate = DateTime.Now;
            int indexAmount = 0;
            for(int i=0; i<listDates.Length; i++)
            {
                int result = DateTime.Compare(DateTime.Parse(listDates[i]), actualDate);
                if(result == 0)
                {
                    indexAmount = i;
                    break;
                }else if(result > 0 && indexAmount > 0)
                {
                    indexAmount = i - 1;
                    break;
                }else if (result > 0 && indexAmount == 0)
                {
                    indexAmount = i;
                    break;
                }
            }
            //DateTime deadlineDate = actualContract.deadlineDate;
            return listAmounts[indexAmount];
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
                MainWindow mainWindow = null;
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        mainWindow = window as MainWindow;
                        break;
                    }
                }

                actualContract.idContractPrevious = actualContract.idContract;
                actualContract.creationDate = DateTime.Now;
                //actualContract.deadlineDate
                actualContract.stateContract = "Activo";
                //actualContract.endorsementSettlementDates
                //actualContract.paymentsSettlement
                //actualContract.paymentsEndorsement

                BlurEffect blurEffect = new BlurEffect();
                blurEffect.Radius = 5;
                mainWindow.PrimaryContainer.Effect = blurEffect;
                (App.Current as App)._cashOnHand = 100000;
                TransactionView newOperation = new TransactionView(OperationType.OPERATION_SEAL, endorsementAmount, int.Parse(labelPawnNumber.Content.ToString()));
                newOperation.CommunicacionPages(this);
                mainWindow.SecundaryContainer.Navigate(newOperation);
                mainWindow.PrimaryContainer.IsHitTestVisible = false;

            }
        }

        private void LoadDatesEndorsementSettlement()
        {
            /*totalLoan = 0;
            tbDateEndorsementSettlement.Text = "";
            currentlyDate = DateTime.Now;
            comercializationDate = DateTime.Now.AddMonths((int)cbTerm.SelectedItem).AddDays(15);
            limitPaymentDate = DateTime.Now.AddMonths((int)cbTerm.SelectedItem);

            tbDateComercialization.Text = comercializationDate.ToString();
            tbPaymentLimit.Text = limitPaymentDate.ToString();
            paymentDates = "";
            for (int i = 1; i <= (int)cbTerm.SelectedItem; i++)
            {
                paymentDates += DateTime.Now.AddMonths(i).ToString("d") + ";\n";
            }

            tbDateEndorsementSettlement.Text = paymentDates.ToString();
            double totalAppraisal = 0;
            foreach (Domain.BelongingCreation.Belonging b in belongingList)
            {
                totalLoan += b.LoanAmount;
                totalAppraisal += b.ApraisalAmount;
            }
            tbLoanAmount.Text = totalLoan.ToString();
            tbAppraisalAmount.Text = totalAppraisal.ToString();
            CalculateLoanPorcentage();*/
        }

        private void goBackButtonEvent(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        public void Communication(bool result)
        {
            if (result) 
            {
                int operationResult = ContractDAO.ModifyContract(actualContract, actualContract.idContract);
                if (operationResult != 0 && operationResult != 300)
                {
                    string message = "Se ha realizado la operacion correctamente";
                    string messageTitle = "Operacion completada";
                    MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                    MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                    MessageBoxResult messageBox;
                    messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                    this.NavigationService.GoBack();
                }
                else
                {
                    string message = "Actualizacion no completada \n\n No se ha podido actualizar la informacion del contrato correctamente";
                    string messageTitle = "Operacion no completada";
                    MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                    MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                    MessageBoxResult messageBox;
                    messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                }
            }
            else
            {
                string message = "No se pudo completar la operacion \n\n ocurrio un error inesperado durante la operacion y no se realizo ningun cambio";
                string messageTitle = "Operacion no realizada";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }
            throw new NotImplementedException();
        }

        public void ScanCommunication(ArticleDomain article)
        {
        }

        private void CancelContractButtonEvent(object sender, RoutedEventArgs e)
        {
            actualContract.stateContract = "Cancelado";
            MainWindow mainWindow = null;
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    mainWindow = window as MainWindow;
                    break;
                }
            }
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            mainWindow.PrimaryContainer.Effect = blurEffect;
            (App.Current as App)._cashOnHand = 100000;
            TransactionView newOperation = new TransactionView(OperationType.OPERATION_LIQUIDATE, settlementAmount, int.Parse(labelPawnNumber.Content.ToString()));
            newOperation.CommunicacionPages(this);
            mainWindow.SecundaryContainer.Navigate(newOperation);
            mainWindow.PrimaryContainer.IsHitTestVisible = false;

        }
    }
}
