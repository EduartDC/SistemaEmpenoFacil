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
        private String operationType;
        Domain.ContractDomain contractDomain = new Domain.ContractDomain();

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

        private async void EndorseContractButtonEvent(object sender, RoutedEventArgs e)
        {
            if (actualContract.idContractPrevious != null && (actualContract.stateContract == "Activo" || actualContract.stateContract == "Reactivado"))
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
                actualContract.stateContract = "Activo";
                operationType = "Refrendo";
                LoadDatesEndorsementSettlement();
                BlurEffect blurEffect = new BlurEffect();
                blurEffect.Radius = 5;
                mainWindow.PrimaryContainer.Effect = blurEffect;
                TransactionView newOperation = new TransactionView(OperationType.OPERATION_RENEWAL, endorsementAmount, int.Parse(labelPawnNumber.Content.ToString()));
                newOperation.CommunicacionPages(this);
                mainWindow.SecundaryContainer.Navigate(newOperation);
                mainWindow.PrimaryContainer.IsHitTestVisible = false;
                contractDomain = await ContractDAO.GetContractsDomainAsync(actualContract.idContract);
            }
        }

        private void LoadDatesEndorsementSettlement()
        {
            int duration = actualContract.duration;
            DateTime currentlyDate = DateTime.Now;
            DateTime limitPaymentDate = DateTime.Now.AddMonths(duration).AddDays(15);
            actualContract.deadlineDate = limitPaymentDate;
            actualContract.creationDate = currentlyDate;
            String paymentDates = "";
            for (int i = 1; i <= duration; i++)
            {
                paymentDates += DateTime.Now.AddMonths(i).ToString("d") + ";\n";
            }
            actualContract.endorsementSettlementDates = paymentDates;
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
                    if(operationType.Equals("Refrendo"))
                    {
                        PrintContract.NewPrintCotract(contractDomain);
                    }
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
        }

        public void ScanCommunication(ArticleDomain article)
        {
        }

        private void CancelContractButtonEvent(object sender, RoutedEventArgs e)
        {
            if (actualContract.creationDate.AddHours(24) <= DateTime.Now)
            {
                actualContract.stateContract = "Cancelado";
                actualContract.deadlineDate = DateTime.Now;
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
                TransactionView newOperation = new TransactionView(OperationType.OPERATION_LIQUIDATE, settlementAmount, int.Parse(labelPawnNumber.Content.ToString()));
                newOperation.CommunicacionPages(this);
                mainWindow.SecundaryContainer.Navigate(newOperation);
                mainWindow.PrimaryContainer.IsHitTestVisible = false;
            }
            else
            {
                string message = "No se puede cancelar un contrato despues de 24 horas desde su creacion";
                string messageTitle = "Cancelacion no disponible";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }
        }
    }
}
