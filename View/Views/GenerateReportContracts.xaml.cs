using BusinessLogic;
using Domain;
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
using View.Properties;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para GenerateReportContracts.xaml
    /// </summary>
    public partial class GenerateReportContracts : Page
    {
        List<ContractDomain> contracts = new List<ContractDomain>();

        public GenerateReportContracts()
        {
            InitializeComponent();
        }

        private void ClosePage_btn(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }
        
        private void Btn_CreateReport(object sender, RoutedEventArgs e)
        {
            if (ValidateDates())
            {
                try
                {
                    contracts = ContractDAO.GetContractsByDate(DateTime.Parse(dpDateBegin.Text), DateTime.Parse(dpDateEnd.Text));
                    if (contracts != null)
                    {
                        GenerateReportContact();
                    }
                    else
                    {
                        MessageBox.Show("No se han encontrado contratos registrados en las fechas registradas");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al conectarse a la base de datos, favor de intentarlo más tarde");
                }
                
            }
        }

        private void GenerateReportContact()
        {
            
        }

        private bool ValidateDates()
        {
            bool result = true;
            if (!dpDateBegin.SelectedDate.HasValue && !dpDateEnd.SelectedDate.HasValue)
            {
                result = false;
                ErrorManager.ShowError("Favor de seleccionar una fecha de inicio y una fecha de fin para realizar el filtro ");
            }
            if (DateTime.Parse(dpDateEnd.Text) <= DateTime.Parse(dpDateBegin.Text))
            {
                result = false;
                ErrorManager.ShowError("La fecha de filtro minima no puede ser superior al la fecha de filtro superior.Favor de verificar");
            }
            return result;
        }
    }
}
