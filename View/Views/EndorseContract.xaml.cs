using BusinessLogic;
using DataAcces;
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
        private List<Domain.BelongingCreation.Belonging> belongings= new List<Domain.BelongingCreation.Belonging>();

        public EndorseContract()
        {
            InitializeComponent();
            ShowContract(13);
            GetBelongingsOfContract(13);
        }

        private void ConverterImagesFormat()
        {
            Console.WriteLine(belongings.Count());
            for (int i = 0; i < belongings.Count(); i++)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(belongings[i].image);
                bitmap.EndInit();
                belongings[i].imageConverted = bitmap;

            }
        }

        private void GetBelongingsOfContract(int idContract)
        {
            belongings = BelongingDAO.GetBelongingByIdContract(idContract);
            ConverterImagesFormat();
            dataGridArticlesOfContract.ItemsSource = belongings;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowContract(int idContract)
        {
            Contract contract = ContractDAO.GetContract(idContract);
            labelEndorsementAmount.Content = GetAmounts(contract.paymentsEndorsement, contract.endorsementSettlementDates) + " $"; // monto de refrendo buscar el monto de refrendo
            labelLoanAmount.Content = contract.loanAmount.ToString() + " $"; // monto de prestamo
            labelSettlementAmount.Content= GetAmounts(contract.paymentsSettlement, contract.endorsementSettlementDates) + " $"; //monto de liquidacion buscar el monto de liquidacion\
            Customer customer = CustomerDAO.GetCustomer(contract.Customer_idCustomer);
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
                //ContractDAO.ModifyContract(actualContract, actualContract.idContract);
            }
        }

        private void goBackButtonEvent(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
