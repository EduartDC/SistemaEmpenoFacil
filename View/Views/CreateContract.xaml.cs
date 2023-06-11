/* KD
* Autor:Jonathan Hernandez Martinez
*/

using DataAcces;
using Domain.Communitation;
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
using BusinessLogic;
using View.Properties;
using System.IO;
using Domain;
using System.Windows.Media.Effects;

namespace View.Views
{

    public partial class CreateContract : Page, Communication, MessageService
    {
        private DataAcces.Customer customer = null;
        private bool customerSucces = false;
        private Metric metrics;
        private List<Domain.BelongingCreation.Belonging> belongingList = new List<Domain.BelongingCreation.Belonging>();
        private List<byte[]> byteImages = new List<byte[]>();

        private DateTime currentlyDate;//fecha actual
        private DateTime comercializationDate;//fecha de comercializacion-NO ESTA EN LA BD
        private DateTime limitPaymentDate;
        private double totalAppraiseAmmount = 0;//total de avaluo
        private string paymentDates = "";
        private double totalLoan = 0;

        private string totalPaymentForEndorsement = "";// total de pago por refrendo

        //parametros en caso de cancelacionn o algun error
        private int idContractSaved = 0;
        private List<int> idBelongingsSaved = new List<int>();
        private List<int> idImagesSaved = new List<int>();

        public CreateContract()
        {
            InitializeComponent();
            BlockTextBoxes();
            LoadMetrics();
            LoadDates();
            tbAppraisalAmount.Text = "";
        }

        private void LoadDates()
        {
            List<int> dates = new List<int> { 1, 3, 6, 9, 12 };
            cbTerm.ItemsSource = dates;

        }
        public void EnabledComboBoxDates()
        {
            if (belongingList.Count >= 1)
                cbTerm.IsEnabled = true;
            else
            {
                cbTerm.SelectedIndex = -1;
                cbTerm.IsEnabled = false;
                tbDateEndorsementSettlement.Clear();
                tbTotalPaymentForEndorsement.Clear();
            }
        }
        private void BlockTextBoxes()
        {
            tbNameCustomer.IsEnabled = false;
            tbAppraisalAmount.IsEnabled = false;
            tbLoanPorcentage.IsEnabled = false;
            tbInterests.IsEnabled = false;
            tbIVA.IsEnabled = false;
            tbTotalEndorsement.IsEnabled = false;
            tbDateComercialization.IsEnabled = false;
            tbLoanAmount.IsEnabled = false;
            tbPaymentLimit.IsEnabled = false;
            //tbMountPaymentTerm.IsEnabled = false;
            tbDateEndorsementSettlement.IsEnabled = false;
            //tbTotalAnnualCost.IsEnabled = false;
            tbAnnualInterestRate.IsEnabled = false;
            tbTotalPaymentForEndorsement.IsEnabled = false;
            tbTotalPerformancePay.IsEnabled = false;
        }

        //Incluye a CU06-Crear registro prendario
        private void ClicCreatePledgeRegister(object sender, RoutedEventArgs e)
        {
            CreateBelongingRegister createBelongingRegister = new CreateBelongingRegister();
            createBelongingRegister.CommunicacionPages(this, belongingList, false, dgBelongings.SelectedIndex);
            createBelongingRegister.ShowDialog();
        }

        //Metodo de interface
        public void refreshBelongings(List<Domain.BelongingCreation.Belonging> belongingsList)
        {
            this.belongingList = belongingsList;
            dgBelongings.ItemsSource = this.belongingList;
            dgBelongings.Items.Refresh();
            LoadFields();
            EnabledComboBoxDates();
            CalculatesAppraisalAndLoanAmount();
            if (cbTerm.SelectedIndex >= 0)
            {
                LoadTotalPaymentForEndorsement();
                LoadTotalPaymentoForSettlement();
            }
        }

        private void LoadFields()
        {

            totalAppraiseAmmount = 0;
            if (belongingList.Count > 0)
            {
                for (int i = 0; i < belongingList.Count; i++)
                    totalAppraiseAmmount += belongingList[i].ApraisalAmount;
                // tbAppraisalAmount.Text = totalAppraiseAmmount.ToString();
            }
        }

        private void ClickDeleteBelonging(object sender, RoutedEventArgs e)
        {
            if (dgBelongings.SelectedIndex >= 0)
            {
                MessageBoxResult resultMessage = MessageBox.Show("¿Está seguro que desea elimnar la prenda de la lista?, una vez eliminada no se podra recuperar", "Eliminar prenda", MessageBoxButton.YesNo);
                if (resultMessage == MessageBoxResult.Yes)
                {
                    belongingList.RemoveAt(dgBelongings.SelectedIndex);
                    dgBelongings.Items.Refresh();
                    LoadFields();
                    EnabledComboBoxDates();
                }

            }
            else
                MessageBox.Show("Para eliminar una prenda, favor de seleccionarla de la lista");
        }

        private void ClickModifyBelonging(object sender, RoutedEventArgs e)
        {

            if (dgBelongings.SelectedIndex >= 0)
            {
                CreateBelongingRegister createBelongingRegister = new CreateBelongingRegister();
                createBelongingRegister.CommunicacionPages(this, belongingList, true, dgBelongings.SelectedIndex);
                createBelongingRegister.ShowDialog();
            }
            else
                MessageBox.Show("fila no seleccionada");
        }

        private void LoadMetrics()
        {
            metrics = MetricsDAO.RecoverMetrics();
            if (metrics != null)
            {
                tbInterests.Text = metrics.interestRate + "%";
                tbIVA.Text = metrics.IVA + "%";
                tbAnnualInterestRate.Text = "" + (int.Parse(metrics.IVA) / 2);
            }
            else
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
        }

        private void BtnFindCustomer(object sender, RoutedEventArgs e)
        {
            int operationCode;
            if (!string.IsNullOrEmpty(tbCustomerCurp.Text))
            {
                (operationCode, customer) = CustomerDAO.FindCustomer(tbCustomerCurp.Text);
                if (operationCode == MessageCode.SUCCESS)
                {
                    if (customer.blackList == false)
                    {
                        tbNameCustomer.Text = customer.firstName.ToUpper() + " " + customer.lastName.ToUpper();
                    }
                    else
                    {
                        ErrorManager.ShowError(MessageError.CUSTOMER_IN_BLACK_LIST);
                        //enviar a menu
                    }
                }
                else if (operationCode == MessageCode.ERROR_USER_NOT_FOUND)
                    ErrorManager.ShowError(MessageError.ERROR_USER_NOT_FOUND);
                else if (operationCode == MessageCode.CONNECTION_ERROR)
                    ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
            }
            else
            {
                ErrorManager.ShowError(MessageError.CUSTOMER_NOT_INTRODUCED);
            }
        }

        private void ComboBoxListener(object sender, SelectionChangedEventArgs e)
        {
            if (cbTerm.SelectedIndex >= 0)
            {
                LoadDatesEndorsementSettlement();
                if (belongingList.Count >= 1)
                {
                    LoadTotalPaymentForEndorsement();
                    LoadTotalPaymentoForSettlement();
                }
            }

        }

        private void LoadTotalPaymentoForSettlement()
        {
            /*
            *  OPERACION:  capital + (capital * (plazoMeses *intereses*1 + (iva)))
            */
            int term = int.Parse(cbTerm.SelectedItem.ToString());
            string info = "";
            tbDollarPay.Text = "";
            for (int i = 1; i <= term; i++)
            {
                float loan = float.Parse(tbLoanAmount.Text);//capital
                                                            //i plazo
                float interestRate = float.Parse(metrics.interestRate) / 100;//intereses
                float iva = float.Parse(metrics.IVA) / 100;//iva
                float result = loan + (loan * (i * interestRate * 1 + (iva)));
                info += result + ";\n";
                tbDollarPay.Text += "$" + "\n";
            }

            tbTotalPerformancePay.Text = info;
        }

        private void LoadDatesEndorsementSettlement()
        {
            totalLoan = 0;
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
            CalculateLoanPorcentage();
        }

        private void CalculatesAppraisalAndLoanAmount()
        {
            double loanAmount = 0;
            double appraisalAmount = 0;
            foreach (Domain.BelongingCreation.Belonging b in belongingList)
            {
                loanAmount += b.LoanAmount;
                appraisalAmount += b.ApraisalAmount;
            }
            tbLoanAmount.Text = loanAmount.ToString();
            tbAppraisalAmount.Text = appraisalAmount.ToString();
            CalculateLoanPorcentage();
        }

        private void CalculateLoanPorcentage()
        {
            float loan = float.Parse(tbLoanAmount.Text);
            float appraisal = float.Parse(tbAppraisalAmount.Text);
            double result = (loan * 100) / appraisal;
            tbLoanPorcentage.Text = result.ToString("0.00");
        }

        private void LoadTotalPaymentForEndorsement()//pagos de finiquito
        {
            /*
            *  OPERACION:  capital * (plazoMeses *intereses* 1 + (iva))
            */
            tbDollarPerformance.Text = "";
            tbTotalPaymentForEndorsement.Text = "";
            int term = int.Parse(cbTerm.SelectedItem.ToString());
            string info = "";
            for (int i = 1; i <= term; i++)
            {
                float loan = float.Parse(tbLoanAmount.Text);//capital
                                                            //i plazo
                float interestRate = float.Parse(metrics.interestRate) / 100;//intereses
                float iva = float.Parse(metrics.IVA) / 100;//iva
                float result = loan * (i * interestRate * 1 + (iva));
                info += result + ";\n";
                tbDollarPerformance.Text += "$" + "\n";
            }
            tbTotalPaymentForEndorsement.Text = "";
            tbTotalPaymentForEndorsement.Text = info;
        }

        private void Btn_CreateContract(object sender, RoutedEventArgs e)
        {
            if (tbNameCustomer.Text != "" && cbTerm.SelectedIndex >= 0 && belongingList.Count > 0)
                RegisterContract();
            else
                ErrorManager.ShowError(MessageError.FIELDS_EMPTY);
        }

        private void RegisterContract()
        {
            Contract newContract = new Contract();
            newContract.loanAmount = float.Parse(tbLoanAmount.Text);//loanAmount
            newContract.deadlineDate = limitPaymentDate;//deadlineDate
            newContract.creationDate = currentlyDate;//CreationDate
            newContract.stateContract = BusinessLogic.StatesContract.PENDING_CONTRACT;
            newContract.iva = int.Parse(metrics.IVA);
            newContract.interestRate = int.Parse(metrics.interestRate);
            newContract.renewalFee = 0;//lo hace otro //renawalFee
            newContract.settlementAmount = 0;//lo hace otro //settlementAmount
            newContract.Customer_idCustomer = customer.idCustomer;//Customer_idCustomer
            newContract.endorsementSettlementDates = tbDateEndorsementSettlement.Text;
            newContract.paymentsSettlement = tbTotalPerformancePay.Text;//tbTotalPaymentForEndorsement
            newContract.paymentsEndorsement = tbTotalPaymentForEndorsement.Text;
            newContract.loanProcentage = tbLoanPorcentage.Text;
            //newContract.totalAnnualCost = tbTotalAnnualCost.Text;
            newContract.annualInterestRate = tbAnnualInterestRate.Text;
            newContract.duration = int.Parse(cbTerm.SelectedItem.ToString());
            SaveInfo(newContract);
        }

        private void SaveInfo(Contract contrat)
        {
            (int operationResult, int idContract) = ContractDAO.RegisterContract(contrat);
            if (operationResult == MessageCode.CONNECTION_ERROR)
            {
                ErrorManager.ShowWarning(MessageError.CONNECTION_ERROR);
            }
            else
            if (operationResult == MessageCode.ERROR)
                ErrorManager.ShowError(MessageError.ERROR_ADD_OPERATION);
            else
            {
                idContractSaved = idContract;
                SaveBelongings(idContract);
            }
        }

        private void SaveBelongings(int idContract)
        {

            List<Belonging> belongingsPrepared = new List<Belonging>();

            foreach (var belonging in belongingList)
            {
                Belonging util = new Belonging();
                util.category = belonging.Category;
                util.characteristics = belonging.Features;
                util.serialNumber = belonging.SerialNumber;
                util.appraisalValue = belonging.ApraisalAmount;
                util.loanAmount = belonging.LoanAmount;
                util.description = belonging.GenericDescription;
                util.Contract_idContract = idContract;
                util.model = belonging.Model;
                belongingsPrepared.Add(util);
            }
            (int result, List<int> idBelongings) = BelongingDAO.SaveBelongings(belongingsPrepared);
            if (result == MessageCode.CONNECTION_ERROR)
            {
                ErrorManager.ShowWarning(MessageError.CONNECTION_ERROR);
                //cerrar frame
            }
            else
            {
                idBelongingsSaved = idBelongings;
                SaveImageBelongings(idBelongings);
            }
        }

        private void SaveImageBelongings(List<int> idBelongingsSaved)
        {
            List<ImagesBelonging> imagesPrepared = new List<ImagesBelonging>();
            ConvertImagesToBytes();
            for (int i = 0; i < idBelongingsSaved.Count(); i++)//recorrer lista de id
            {
                for (int j = 0; j < 4; j++)//recorrer prendas
                {
                    Console.WriteLine("numero de id de prenda: " + idBelongingsSaved[i]);
                    ImagesBelonging imagesBelonging = new ImagesBelonging();
                    imagesBelonging.Belonging_idBelonging = idBelongingsSaved[i];
                    imagesBelonging.imagen = belongingList[i].imagesBytes[j];
                    imagesPrepared.Add(imagesBelonging);
                }
            }
            (int result, List<int> idImages) = BelongingDAO.SaveimagesBelongings(imagesPrepared);
            if (result == MessageCode.CONNECTION_ERROR)
                ErrorManager.ShowWarning(MessageError.CONNECTION_ERROR);
            else if (result == MessageCode.SUCCESS)
            {
                MessageBox.Show("IMAGENES REGISTRADAS");
                idImagesSaved = idImages;
                CallTransaction();
            }
            else
                ErrorManager.ShowWarning("Error critico");
        }

        private void CallTransaction() //llamado a metodo de dircio
        {
            //MainWindow mainWindow = new MainWindow();
            //copiar de setAside 292
            MainWindow mainWindow = null;

            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    mainWindow = window as MainWindow;
                    break;
                }
            }
            //var window = Application.Current.MainWindow as MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            mainWindow.PrimaryContainer.Effect = blurEffect;
            //campos estaticos para pruebas
            (App.Current as App)._cashOnHand = 100000;
            TransactionView newOperation = new TransactionView(OperationType.OPERATION_LOAND, double.Parse(tbLoanAmount.Text), idContractSaved);
            newOperation.CommunicacionPages(this);
            mainWindow.SecundaryContainer.Navigate(newOperation);
            mainWindow.PrimaryContainer.IsHitTestVisible = false;
        }

        private void ConvertImagesToBytes()
        {
            for (int i = 0; i < belongingList.Count(); i++)
            {
                for (int j = 0; j < 4; j++)
                    using (MemoryStream stream = new MemoryStream())
                    {
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(belongingList[i].imagesBitmap[j]));
                        encoder.Save(stream);
                        belongingList[i].imagesBytes.Add(stream.ToArray());
                    }
            }
        }

        //metodo de interfaz par transactionView
        public async void Communication(bool result)
        {
            if (result)
            {
                int resultActive = ContractDAO.activeContract(idContractSaved);

                Console.WriteLine(resultActive);
                if (resultActive == MessageCode.SUCCESS)
                {
                    MessageBox.Show("Contrato Creado exitosamente");
                    PrintContract.NewPrintCotract(await ContractDAO.GetContractsDomainAsync(idContractSaved));

                }
            }
            else
            {
                int resultimg = BelongingDAO.DeletePendingImages(idImagesSaved);
                int resultBelonging = BelongingDAO.DeletePendingBelongings(idBelongingsSaved);
                int resultContract = ContractDAO.DeletePendingContrat(idContractSaved);
                if (resultimg == MessageCode.SUCCESS && resultBelonging == MessageCode.SUCCESS && resultContract == MessageCode.SUCCESS)
                    MessageBox.Show("Contrato cancelado exitosamente");

            }
        }

        //metodo de interfaz TransactionView que no se usa
        public void ScanCommunication(ArticleDomain article)
        {
        }

        private void Borrar(object sender, RoutedEventArgs e)
        {




            var mainWindow = Application.Current.MainWindow as MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            mainWindow.PrimaryContainer.Effect = blurEffect;
            //campos estaticos para pruebas
            (App.Current as App)._cashOnHand = 1000;
            TransactionView newOperation = new TransactionView(OperationType.OPERATION_LOAND, 1000, 13);
            newOperation.CommunicacionPages(this);
            mainWindow.SecundaryContainer.Navigate(newOperation);
            mainWindow.PrimaryContainer.IsHitTestVisible = false;
        }

        private void dgBelongings_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void Btn_RegisterCustomer(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            mainWindow.PrimaryContainer.Effect = blurEffect;
            //campos estaticos para pruebas
            (App.Current as App)._cashOnHand = 1000;
            CreateCustomerRecord registerCustomer = new CreateCustomerRecord();
            mainWindow.SecundaryContainer.Navigate(registerCustomer);
            mainWindow.PrimaryContainer.IsHitTestVisible = false;
        }

        private void ClickCancelOperation(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultMessage = MessageBox.Show("¿Desea cancelar la operación?, cualquier información introducida se perderá", "Confirmación", MessageBoxButton.OKCancel);
            if (resultMessage == MessageBoxResult.OK)
                this.Content = null;

        }
    }
}
