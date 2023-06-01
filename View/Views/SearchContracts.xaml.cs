using BusinessLogic;
using BusinessLogic.Utility;
using DataAcces;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
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
    /// Lógica de interacción para SearchContracts.xaml
    /// </summary>
    public partial class SearchContracts : Page, MessageService
    {
        private List<Domain.CompleteContract> contractList = new List<Domain.CompleteContract>();
        private List<string> _listNamesCustomers = new List<string>();
        private List<int> _listNumberContracts = new List<int>();
        private int idContract;

        public SearchContracts()
        {
            InitializeComponent();
            comBox_TypeSearch.Items.Add("Numero del contrato");
            comBox_TypeSearch.Items.Add("Nombre del cliente");
            try
            {
                InitializeTable();
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("No existen contratos registrados en la base de datos");
                this.Content = null;
            }
            catch (Exception)
            {
                MessageBox.Show("No se ha podido conectar a la base de datos, favor de intentarlo más tarde");
                this.Content = null;
            }
        }

        private void InitializeTable()
        {
            contractList = ContractDAO.RecoverContracts();
            contractList.ForEach(customer => _listNamesCustomers.Add(customer.firstName));
            contractList.ForEach(customer => _listNumberContracts.Add(customer.idContract));
            tableCustomers.ItemsSource = contractList;
            if(contractList.Count == 0)
            {
                MessageBox.Show("Error al recuperar los registros de la base de datos, favor de intentarlo más tarde");
                this.Content = null;
            }
        }


        private void Button_Reactivate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                var row = DataGridRow.GetRowContainingElement(btn);
                var item = row.Item;
                if (item != null && tableCustomers.Items.Contains(item))
                {

                    
                    Domain.CompleteContract contractCompleted= (Domain.CompleteContract)item;
                    DataAcces.Contract contract = ContractDAO.GetContract(contractCompleted.idContract);
                    idContract = contract.idContract;
                    DateTime dateTime = DateTime.Now;

                    TimeSpan difference = contract.deadlineDate.Subtract(dateTime);
                    
                    if (contract.stateContract == StatesContract.CANCELED_CONTRACT && dateTime.Subtract(contract.deadlineDate).TotalHours < 24)
                    {
                        Operation operation = new Operation();
                        OperationType operationType = new OperationType();
                        var window = (MainWindow)Application.Current.MainWindow; ;
                        BlurEffect blurEffect = new BlurEffect();
                        blurEffect.Radius = 5;
                        window.PrimaryContainer.Effect = blurEffect;
                        TransactionView newOperation = new TransactionView(OperationType.OPERATION_LOAND, contract.settlementAmount, contract.idContract);
                        newOperation.CommunicacionPages(this);
                        window.SecundaryContainer.Navigate(newOperation);
                        window.PrimaryContainer.IsHitTestVisible = false;

                        

                    }
                    else
                    {
                        MessageBox.Show(MessageError.ERROR_RENOVATION_CONTRACT);
                    }
                    
                   
                }
            }
        }

        private void SearchByName()
        {
            if (!String.IsNullOrEmpty(text_SearchBy.Text.Trim()))
            {
                List<Domain.CompleteContract> CustomersEquals = (_listNamesCustomers.Where(stringName =>
                stringName.StartsWith(text_SearchBy.Text.Trim())).Select(stringName =>
                contractList.Find(customerFind => customerFind.firstName.Contains(stringName)))).ToList();
                tableCustomers.ItemsSource = CustomersEquals;
            }
            else if (text_SearchBy.Text.Trim() == "")
            {
                tableCustomers.ItemsSource = contractList;
            }
        }

        private void SearchByNumber()
        {
            if (!String.IsNullOrEmpty(text_SearchBy.Text.Trim()))
            {
                int readNumer = int.Parse(text_SearchBy.Text.Trim());
                List<Domain.CompleteContract> CustomersEquals = (_listNumberContracts.Where(intNumber =>
                intNumber.Equals(readNumer))).Select(intNumber => contractList.Find(customerFind =>
                customerFind.idContract.Equals(intNumber))).ToList();
                tableCustomers.ItemsSource = CustomersEquals;
            }
            else if (text_SearchBy.Text.Trim() == "")
            {
                tableCustomers.ItemsSource = contractList;
            }
        }

        private void Button_Liquidate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if(btn != null)
            {
                var row = DataGridRow.GetRowContainingElement(btn);
                var item = row.Item;
                if(item != null && tableCustomers.Items.Contains(item))
                {
                    var contractSelected = tableCustomers.SelectedItem as CompleteContract;
                    var window = (MainWindow)Application.Current.MainWindow;
                    var menu = new MenuView();
                    menu.Container.NavigationService.Navigate(new LiquidateContractView(contractSelected.idContract));
                    window.PrimaryContainer.NavigationService.Navigate(menu);
                }
            }
        }

        private void Button_Endorsement_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                var row = DataGridRow.GetRowContainingElement(btn);
                var item = row.Item;
                if (item != null && tableCustomers.Items.Contains(item))
                {
                    var contractSelected = tableCustomers.SelectedItem as CompleteContract;
                    var window = (MainWindow)Application.Current.MainWindow;
                    var menu = new MenuView();
                    menu.Container.NavigationService.Navigate(new EndorseContract(contractSelected.idContract));
                    window.PrimaryContainer.NavigationService.Navigate(menu);
                }
            }
        }

        private void Btn_Restore_Click(object sender, RoutedEventArgs e)
        {
            text_SearchBy.Text = "";
            _listNumberContracts.Clear();
            _listNamesCustomers.Clear();
            InitializeTable();
        }

        private void Btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            tableCustomers.ItemsSource = contractList;
            switch (comBox_TypeSearch.SelectedIndex)
            {
                case -1:
                    MessageBox.Show("Favor de seleccionar el tipo de dato con el que desea buscar al cliente");
                    break;

                case 0:
                    if (!Utilities.ValidateFormat(text_SearchBy.Text, "^[0-9]+$"))
                    {
                        MessageBox.Show("Solo se aceptan numeros para esta busqueda");
                    }
                    else
                    {
                        SearchByNumber();
                    }
                    break;

                case 1:
                    if (!Utilities.ValidateFormat(text_SearchBy.Text, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\\s]+$"))
                    {
                        MessageBox.Show("Solo se aceptan letras para esta busqueda");
                    }
                    else
                    {
                        SearchByName();
                    }
                    break;
            }
        }
        private void Button_Consult_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                var row = DataGridRow.GetRowContainingElement(btn);
                var item = row.Item;
                if (item != null && tableCustomers.Items.Contains(item))
                {
                    var contractSelected = tableCustomers.SelectedItem as CompleteContract;
                    var window = (MainWindow)Application.Current.MainWindow;
                    ConsultContract consultContract = new ConsultContract(contractSelected.idContract);
                    consultContract.ShowDialog();
                }
            }
        }

        public void Communication(bool result)
        {
            if (result == true)
            {
                ContractDAO.ReactiveContract(idContract);
            }
            else
            {
                MessageBox.Show(MessageError.CANCEL_OPERATION);
            }
        }

        public void ScanCommunication(ArticleDomain article)
        {
            throw new NotImplementedException();
        }
    }
}
