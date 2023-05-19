using BusinessLogic;
using BusinessLogic.Utility;
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

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para ConsultBlackList1.xaml
    /// </summary>
    public partial class ConsultBlackList1 : Page
    {

        private NewLog _log = new NewLog();
        private List<Domain.Customer> customersList = new List<Domain.Customer>();
        private List<string> _listNamesCustomers = new List<string>();
        private List<int> _listNumberCustomers = new List<int>();
        private List<string> _listCurpsCustomers = new List<string>();

        public ConsultBlackList1()
        {
            InitializeComponent();
            comBox_TypeSearch.Items.Add("Numero del cliente");
            comBox_TypeSearch.Items.Add("Nombre del cliente");
            comBox_TypeSearch.Items.Add("CURP");
            InitializeTable();
        }

        private void InitializeTable()
        {
            customersList = CustomerDAO.RecoverCustomers();
            customersList.ForEach(customer => _listNamesCustomers.Add(customer.firstName));
            customersList.ForEach(customer => _listNumberCustomers.Add(customer.idCustomer));
            customersList.ForEach(customer => _listCurpsCustomers.Add(customer.curp));
            tableCustomers.ItemsSource = customersList;
            if (_listNamesCustomers.Count == 0)
            {
<<<<<<< HEAD
               MessageBox.Show("Error al recuperar los registros de la base de datos, favor de intentarlo más tarde");
=======
                MessageBox.Show("Error al recuperar los registros de la base de datos, favor de intentarlo más tarde");
>>>>>>> 09aecce4aba545e277b6176d78454a5d1e80abbb
                this.Content = null;
            }
        }



        private void SearchByName()
        {
            if (!String.IsNullOrEmpty(text_SearchBy.Text.Trim()))
            {
                List<Domain.Customer> CustomersEquals = (_listNamesCustomers.Where(stringName =>
                stringName.StartsWith(text_SearchBy.Text.Trim())).Select(stringName =>
                customersList.Find(customerFind => customerFind.firstName.Contains(stringName)))).ToList();
                tableCustomers.ItemsSource = CustomersEquals;
            }
            else if (text_SearchBy.Text.Trim() == "")
            {
                tableCustomers.ItemsSource = customersList;
            }
        }

        private void SearchByNumber()
        {
            if (!String.IsNullOrEmpty(text_SearchBy.Text.Trim()))
            {
                int readNumer = int.Parse(text_SearchBy.Text.Trim());
                List<Domain.Customer> CustomersEquals = (_listNumberCustomers.Where(intNumber =>
                intNumber.Equals(readNumer))).Select(intNumber => customersList.Find(customerFind =>
                customerFind.idCustomer.Equals(intNumber))).ToList();
                tableCustomers.ItemsSource = CustomersEquals;
            }
            else if (text_SearchBy.Text.Trim() == "")
            {
                tableCustomers.ItemsSource = customersList;
            }
        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            tableCustomers.ItemsSource = customersList;
            switch (comBox_TypeSearch.SelectedIndex)
            {
                case -1:
                    MessageBox.Show("Favor de seleccionar el tipo de dato con el que desea buscar al cliente");
                    break;

                case 0:
                    if (!Utilities.ValidateFormat(text_SearchBy.Text.Trim(), "^[0-9]+$"))
                    {
                        MessageBox.Show("Solo se aceptan numeros para esta busqueda");
                    }
                    else
                    {
                        SearchByNumber();
                    }
                    break;

                case 1:
                    if (!Utilities.ValidateFormat(text_SearchBy.Text.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\\s]+$"))
                    {
                        MessageBox.Show("Solo se aceptan letras para esta busqueda");
                    }
                    else
                    {
                        SearchByName();
                    }
                    break;
                case 2:
                    if (!Utilities.ValidateFormat(text_SearchBy.Text.Trim(), "^[A-Z0-9]+$"))
                    {
                        MessageBox.Show("Solo se aceptan letras mayusculas y numeros para esta busqueda");
                    }
                    else
                    {
                        SearchByCRUP();
                    }
                    break;
            }
        }

        private void SearchByCRUP()
        {
            if (!String.IsNullOrEmpty(text_SearchBy.Text.Trim()))
            {
                List<Domain.Customer> CustomersEquals = (_listCurpsCustomers.Where(stringCurp =>
                stringCurp.StartsWith(text_SearchBy.Text.Trim())).Select(stringCurp =>
                customersList.Find(customerFind => customerFind.curp.Contains(stringCurp)))).ToList();
                tableCustomers.ItemsSource = CustomersEquals;
            }
            else if (text_SearchBy.Text.Trim() == "")
            {
                tableCustomers.ItemsSource = customersList;
            }
        }

        private void Btn_Restore_Click(object sender, RoutedEventArgs e)
        {
            text_SearchBy.Text = "";
            _listNumberCustomers.Clear();
            _listNamesCustomers.Clear();
            _listCurpsCustomers.Clear();
            InitializeTable();
        }

        private void Btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerBlackList addCustomerBlackList = new AddCustomerBlackList();
            addCustomerBlackList.ShowDialog();
        }
    }
}
