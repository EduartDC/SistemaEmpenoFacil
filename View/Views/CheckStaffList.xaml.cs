using BusinessLogic;
using DataAcces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Lógica de interacción para CheckStaffList.xaml
    /// </summary>
    public partial class CheckStaffList : Page
    {
        public CheckStaffList()
        {
            InitializeComponent();
            GetAllStaff();
        }

        public void GetAllStaff()
        {
            dataGridStaff.Items.Clear();
            List<Staff> staffList = new List<Staff>();
            staffList = StaffDAO.GetAllStaff();
            foreach (Staff staff in staffList)
            {
                dataGridStaff.Items.Add(staff);
            }
        }

        private void SelectStaffEvent(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Staff staffSelected = dataGridStaff.SelectedItem as Staff;
                firstNameLabel.Content = staffSelected.fisrtName;
                lastNameLabel.Content = staffSelected.lastName;
                userNameLabel.Content = staffSelected.userName;
                statusLabel.Content = staffSelected.statusStaff;
                rolLabel.Content = staffSelected.rol;
                rfcLabel.Content = staffSelected.rfc;
            }
            catch (NullReferenceException)
            {
                dataGridStaff.SelectedItem= null;
            }
        }

        private void GoBackButtonEvent(object sender, RoutedEventArgs e)
        {
            this.Content= null;
        }

        private void ModifyEmployeeButtonEvent(object sender, RoutedEventArgs e)
        {
            if(dataGridStaff.SelectedItem != null)
            {
                Staff selectedStaff = dataGridStaff.SelectedItem as Staff;
                ModifyEmployee modifyEmployee = new ModifyEmployee(selectedStaff.idStaff);
                CheckStaffList contentOfPage = new CheckStaffList();
                this.NavigationService.Navigate(modifyEmployee);
            }
            else
            {
                string message = "No se ha seleccionado un empleado para su modificacion";
                string messageTitle = "Seleccion vacia";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }
        }

        private void UpdateInformationButtonEvent(object sender, RoutedEventArgs e)
        {
            GetAllStaff();
            firstNameLabel.Content = null;
            lastNameLabel.Content = null;
            userNameLabel.Content = null;
            statusLabel.Content = null;
            rolLabel.Content = null;
            rfcLabel.Content = null;
        }
    }
}
