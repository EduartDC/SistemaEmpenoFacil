using BusinessLogic;
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
using System.Windows.Shapes;
using View.Properties;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para ConsultContract.xaml
    /// </summary>
    public partial class ConsultContract : Window
    {
        public ConsultContract()
        {
            InitializeComponent();
        }

        private void Btn_Salir_Click(object sender, RoutedEventArgs e)
        {

        }

        private async Task SetInformationAsync(int idContract)
        {
            try
            {
                var contract = await Task.Run(() => ContractDAO.GetContractsDomainAsync(idContract));
                if (contract == null)
                {
                    ErrorManager.ShowWarning("No se encontro el contrato.");
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                ErrorManager.ShowError(ex.Message);
            }
        }
    }
}
