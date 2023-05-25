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
    /// Lógica de interacción para GenerateReortOSparatedIms.xaml
    /// </summary>
    public partial class GenerateReortOSparatedIms : Page
    {
        public GenerateReortOSparatedIms()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (startDateDatePicker.SelectedDate == null || endDateDatePicker.SelectedDate == null)
            {
                string message = "Selecicone por favor la fecha de inicio y la fecha de fin para generar el reporte";
                string messageTitle = "Fechas no seleccionadas";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }
            else
            {
                string message = "Se ha generado con exito el reporte de articulos apartados del periodo especificado"
                + " acontinuacion se descargara el reporte en tu ordenador";
                string messageTitle = "Reporte generado con exito";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }
        }

        private void GoBackButtonEvent(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void GenerateSetAsideArticlesReport()
        {

        }
    }
}
