/*
 * Autor:Jonathan Hernandez Martínez
 */
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer;
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
using View.Properties;

namespace View.Views
{
    public partial class GenerateSalesReport : Page
    {
        private List<string> filter = new List<string> {"Selecciona una opcion","Articulos en stock", "articulos vendidos"};
        public GenerateSalesReport()
        {
            InitializeComponent();
            LoadFilter();
            LoadDates();
        }

        private void LoadDates()
        {
            dpStartDate.SelectedDate = DateTime.Now;
            dpEndDate.SelectedDate = DateTime.Now;
        }

        private void LoadFilter()
        {
            cbFilter.ItemsSource = filter;
            cbFilter.SelectedIndex = 0;
        }

        private void ClosePage_btn(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void Btn_CreateReport(object sender, RoutedEventArgs e)
        {
            if(ValidateDatePickers())
            {
                if (CheckFilter())
                {
                    Btn_CreateReport();
                    MessageBox.Show("Report");
                }
            }
        }

        private void Btn_CreateReport()
        {
            //try
            //{
            //    //ReportDocument report = new ReportDocument();//import de cristal reports
            //    //report.Load("View\\ArticlesStockReport.rpt");
            //    ArticlesStockReport report = new ArticlesStockReport();
            //    //report.

            //}catch(IOException)
            //{
            //    MessageBox.Show("Ruta no encontrada");
            //}
        }

        private bool ValidateDatePickers()
        {
            if (dpStartDate.SelectedDate < dpEndDate.SelectedDate)
                return true;
            else
                ErrorManager.ShowError("Fechas invalidas. la fecha de inicio debe ser menor a la fecha de fin");
            return false;
        }

        private bool CheckFilter()
        {
            if (cbFilter.SelectedIndex >= 1)
                return true;
            else
                ErrorManager.ShowError("Favore de seleccionar un filtro para realizar el reporte");
            return false;
        }
    }
}
