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
using DataAcces;
using View.Properties;

namespace View.Views
{

    public partial class CreateContract : Page, Communication
    {
        private bool customerSucces = false;
        private Metric metrics;
        private List<Domain.BelongingCreation.Belonging> belongingList = new List<Domain.BelongingCreation.Belonging>();
        private List<BitmapImage> bitmapImgList = new List<BitmapImage>();


        private DateTime currentlyDate;//fecha actual
        private DateTime comercializationDate;//fecha de comercializacion-NO ESTA EN LA BD
        private DateTime limitPaymentDate;
        private int totalAppraiseAmmount = 0;//total de avaluo
        private string paymentDates = "";
        private int totalLoan = 0;



        public CreateContract()
        {
            InitializeComponent();
            BlockTextBoxes();
            // LoadMetrics();
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
            metrics = ContractDAO.GetMetrics();
            if (metrics != null)
            {
                tbInterests.Text = metrics.interestRate + "%";
                tbIVA.Text = metrics.IVA + "%";
            }
            else
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
        }

        private void BtnFindCustomer(object sender, RoutedEventArgs e)
        {
            int operationCode;
            Customer customer = null;
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

        private void ComboBoxListener(object sender, SelectionChangedEventArgs e)
        {
            currentlyDate = DateTime.Now;
            comercializationDate = DateTime.Now.AddMonths((int)cbTerm.SelectedItem).AddDays(15);
            limitPaymentDate = DateTime.Now.AddMonths((int)cbTerm.SelectedItem);

            tbDateComercialization.Text = comercializationDate.ToString();
            tbPaymentLimit.Text = limitPaymentDate.ToString();

            DateTime dateTime = DateTime.Now;
            paymentDates = "FECHAS DE REFRENDOS-FINIQUITOS \n";
            for (int i = 1; i <= (int)cbTerm.SelectedItem; i++)
            {
                paymentDates += "- " + DateTime.Now.AddMonths(i).ToString("d") + ";\n";
            }

            tbDateEndorsementSettlement.Text = paymentDates.ToString();

            foreach (Domain.BelongingCreation.Belonging b  in belongingList) 
            {
                totalLoan += b.LoanAmount;
            }
            tbLoanAmount.Text = totalLoan.ToString();

            

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
