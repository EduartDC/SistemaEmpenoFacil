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

namespace View.Views
{

    public partial class CreateContract : Page, Communication
    {
        private Customer customer = null;
        private bool customerSucces = false;
        private Metric metrics;
        private List<Domain.BelongingCreation.Belonging> belongingList = new List<Domain.BelongingCreation.Belonging>();
        private List<BitmapImage> bitmapImgList = new List<BitmapImage>();
        //private List<BitmapImage> bitmapUtil = new List<BitmapImage>();//lista usada para pasar a byte y eliminar de lista sin alterar la original
        private List<byte[]> byteImages = new List<byte[]>();

        private DateTime currentlyDate;//fecha actual
        private DateTime comercializationDate;//fecha de comercializacion-NO ESTA EN LA BD
        private DateTime limitPaymentDate;
        private int totalAppraiseAmmount = 0;//total de avaluo
        private string paymentDates = "";
        private int totalLoan = 0;

        private string totalPaymentForEndorsement = "";// total de pago por refrendo



        public CreateContract()
        {
            InitializeComponent();
            BlockTextBoxes();
            LoadMetrics();
            LoadDates();

        }

        private void LoadDates()
        {
            List<int> dates = new List<int> { 1, 3, 6, 9, 12 };
            cbTerm.ItemsSource = dates;

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
            tbMountPaymentTerm.IsEnabled = false;
            tbDateEndorsementSettlement.IsEnabled = false;
            tbTotalAnnualCost.IsEnabled = false;
            tbAnnualInterestRate.IsEnabled = false;
            tbTotalPaymentForEndorsement.IsEnabled = false;
            tbTotalPerformancePay.IsEnabled = false;
        }

        //Incluye a CU06-Crear registro prendario
        private void ClicCreatePledgeRegister(object sender, RoutedEventArgs e)
        {
            CreateBelongingRegister createBelongingRegister = new CreateBelongingRegister();
            createBelongingRegister.CommunicacionPages(this, belongingList, bitmapImgList, false, dgBelongings.SelectedIndex);
            createBelongingRegister.ShowDialog();
        }

        //Metodo de interface
        public void refreshBelongings(List<Domain.BelongingCreation.Belonging> belongingsList, List<BitmapImage> bitmapImgList)
        {
            this.belongingList = belongingList;
            dgBelongings.ItemsSource = this.belongingList;
            dgBelongings.Items.Refresh();
            this.bitmapImgList = bitmapImgList;
            LoadFields();
        }

        private void LoadFields()
        {

            totalAppraiseAmmount = 0;
            if (belongingList.Count > 0)
            {
                for (int i = 0; i < belongingList.Count; i++)
                    totalAppraiseAmmount += belongingList[i].ApraisalAmount;
                tbAppraisalAmount.Text = totalAppraiseAmmount.ToString();
            }

        }

        private void ClickDeleteBelonging(object sender, RoutedEventArgs e)
        {
            if (dgBelongings.SelectedIndex >= 0)
            {
                if (dgBelongings.SelectedIndex == 0)
                {
                    for (int i = 0; i < 4; i++)
                        bitmapImgList.RemoveAt(0);
                }
                if (dgBelongings.SelectedIndex == 1)
                {
                    for (int i = 0; i < 4; i++)
                        bitmapImgList.RemoveAt(4);
                }
                if (dgBelongings.SelectedIndex == 2)
                {
                    for (int i = 0; i < 4; i++)
                        bitmapImgList.RemoveAt(8);
                }
                if (dgBelongings.SelectedIndex == 3)
                {
                    for (int i = 0; i < 4; i++)
                        bitmapImgList.RemoveAt(12);
                }
                if (dgBelongings.SelectedIndex == 4)
                {
                    for (int i = 0; i < 4; i++)
                        bitmapImgList.RemoveAt(16);
                }

                belongingList.RemoveAt(dgBelongings.SelectedIndex);
                dgBelongings.Items.Refresh();
                LoadFields();

            }
            else
                MessageBox.Show("fila no seleccionada");
        }

        private void ClickModifyBelonging(object sender, RoutedEventArgs e)
        {

            if (dgBelongings.SelectedIndex >= 0)
            {
                CreateBelongingRegister createBelongingRegister = new CreateBelongingRegister();
                createBelongingRegister.CommunicacionPages(this, belongingList, bitmapImgList, true, dgBelongings.SelectedIndex);
                createBelongingRegister.ShowDialog();
            }
            else
                MessageBox.Show("fila no seleccionada");
        }

        private void LoadMetrics()
        {
            metrics = MetricsDAO.recoverMetrics();
            if (metrics != null)
            {
                tbInterests.Text = metrics.interestRate + "%";
                tbIVA.Text = metrics.IVA + "%";
                tbTotalAnnualCost.Text = "" + (int.Parse(metrics.IVA) / 2);
            }
            else
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
        }

        private void BtnFindCustomer(object sender, RoutedEventArgs e)
        {
            int operationCode;
            if (tbCustomerCurp.Text != "")
            {
                (operationCode, customer) = CustomerDAO.FindCustomer(tbCustomerCurp.Text);
                if (operationCode == 1)
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
                else
                    ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
            }
            else
            {
                ErrorManager.ShowError(MessageError.CUSTOMER_NOT_INTRODUCED);
            }


        }

        private void ComboBoxListener(object sender, SelectionChangedEventArgs e)
        {
            LoadDatesEndorsementSettlement();
            LoadTotalPaymentForEndorsement();

            // TotalPaymentForEndorsement
        }

        private void LoadDatesEndorsementSettlement()
        {
            currentlyDate = DateTime.Now;
            comercializationDate = DateTime.Now.AddMonths((int)cbTerm.SelectedItem).AddDays(15);
            limitPaymentDate = DateTime.Now.AddMonths((int)cbTerm.SelectedItem);

            tbDateComercialization.Text = comercializationDate.ToString();
            tbPaymentLimit.Text = limitPaymentDate.ToString();

            DateTime dateTime = DateTime.Now;
            //paymentDates = "FECHAS DE REFRENDOS-FINIQUITOS \n";
            for (int i = 1; i <= (int)cbTerm.SelectedItem; i++)
            {
                paymentDates += "- " + DateTime.Now.AddMonths(i).ToString("d") + ";\n";
            }

            tbDateEndorsementSettlement.Text = paymentDates.ToString();

            foreach (Domain.BelongingCreation.Belonging b in belongingList)
            {
                totalLoan += b.LoanAmount;
            }
            tbLoanAmount.Text = totalLoan.ToString();
        }

        private void LoadTotalPaymentForEndorsement()
        {
            // float valor = (total);  
            // totalPaymentForEndorsement =  
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
            //idContractPrevious
            newContract.deadlineDate = limitPaymentDate;//deadlineDate
            newContract.creationDate = currentlyDate;//CreationDate
            newContract.stateContract = "active";//state
            newContract.iva = int.Parse(metrics.IVA);
            newContract.interestRate = int.Parse(metrics.interestRate);
            newContract.renewalFee = 0;//lo hace otro //renawalFee
            newContract.settlementAmount = 0;//lo hace otro //settlementAmount
            newContract.Customer_idCustomer = customer.idCustomer;//Customer_idCustomer
            newContract.endorsementSettlementDates = tbDateEndorsementSettlement.Text;
            newContract.paymentsSettlement = "Pagos1";//tbTotalPaymentForEndorsement
            newContract.paymentsEndorsement = "Pagos1";// tbTotalPerformancePay
            SaveInfo(newContract);
            /*
             * faltaria guardar en la base un string para:
             *      guardar todas las fechas de pagos de refrendo/liquidacion,
             *      todas los pagos de refrendo y 
             *      todos lo pagos de liquidacion
             */

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
                //bitmapUtil = bitmapImgList;
                //for(int i=0; i<belongingsPrepared.Count; i++)
                ConvertBitmapToBytes();
                SaveImageBelongings(idBelongings);
            }
        }

        private void SaveImageBelongings(List<int> idBelongingsSaved)
        {
            List<ImagesBelonging> imgBelongingsPrepared = new List<ImagesBelonging>();
            List<byte[]> imagesClone = byteImages;
            
            for(int i = 0; i < idBelongingsSaved.Count;i++) 
            {
                for (int j = 0; j < 4; j++)
                {
                    ImagesBelonging util = new ImagesBelonging();
                    util.imagen = imagesClone[0];
                    util.Belonging_idBelonging = idBelongingsSaved[i];
                    imgBelongingsPrepared.Add(util);
                    imagesClone.RemoveAt(0);
                }
            }
            int result = BelongingDAO.SaveimagesBelongings(imgBelongingsPrepared);
            if (result == MessageCode.CONNECTION_ERROR)
            {
                ErrorManager.ShowWarning(MessageError.CONNECTION_ERROR);
            }
            else
                if (result == MessageCode.SUCCESS)
            {
                MessageBox.Show("IMAGENES REGISTRADAS");
            }
            else
            {
                ErrorManager.ShowWarning("ALGO SALIO MAL");
            }
        }

        private void ConvertBitmapToBytes(/*int position*/)
        {
            for (int i = 0; i < bitmapImgList.Count; i++)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImgList[i]));
                    encoder.Save(stream);
                    byteImages.Add(stream.ToArray());
                }
            }

        }






        //private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    TextBox textBox = (TextBox)sender;
        //    if (textBox.Text == "Introduce tu texto aquí")
        //    {
        //        textBox.Text = "";
        //        textBox.Foreground = Brushes.Black;
        //    }
        //}

        //private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    TextBox textBox = (TextBox)sender;
        //    if (string.IsNullOrWhiteSpace(textBox.Text))
        //    {
        //        textBox.Text = "Introduce tu texto aquí";
        //        textBox.Foreground = Brushes.Gray;
        //    }
        //}



    }
}
