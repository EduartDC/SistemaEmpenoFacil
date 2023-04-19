using DataAcces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data;
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
using BusinessLogic;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para ConfigureMetrics.xaml
    /// </summary>
    public partial class ConfigureMetrics : Window
    {
        private NewLog _log = new NewLog();
        public ConfigureMetrics()
        {
            InitializeComponent();
            InitializateCamps(MetricsDAO.recoverMetrics());
        }

        private void InitializateCamps(Metric newMetrics)
        {
            try
            {
                text_InterestRate.Text = newMetrics.interestRate.ToString();
                text_IVA.Text = newMetrics.IVA.ToString();
            }
            catch (NullReferenceException ex)
            {
                _log.Add(ex.ToString());
                text_InterestRate.Text = "";
                text_IVA.Text = "";
            }
        }


        private void button_Salir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button_Registrar_Click(object sender, RoutedEventArgs e)
        {
            label_ErrorIVA.Content = "";
            label_ErrorInterestRate.Content = "";
            if (validateEmptyCamps() && validateFormat())
            {
                switch (MetricsDAO.updateMetrics(text_InterestRate.Text, text_IVA.Text))
                {
                    case 200:
                        MessageBox.Show("No se ha podido conectar con la base de datos, favor de intentarlo más tarde");
                        break;

                    case 400:
                        MessageBox.Show("Error al realizar el registro, favor de intentarlo más tarde");
                        break;

                    case 500:
                        MessageBox.Show("Configuración Exitosa");
                        Close();
                        break;
                }
                
                
            }
        }

        private Boolean validateFormat()
        {
            Boolean resultado = true;
            if (!FormatValidation.ValidateFormat(text_InterestRate.Text, "^[0-9]+$"))
            {
                label_ErrorInterestRate.Content = "Error con el formato, solo se aceptan numeros enteros";
                resultado = false;
            }
            if (!FormatValidation.ValidateFormat(text_IVA.Text, "^[0-9]+$"))
            {
                label_ErrorIVA.Content = "Error con el formato, solo se aceptan numeros enteros";
                resultado = false;
            }
            return resultado;
        }

        private Boolean validateEmptyCamps()
        {
            Boolean resultado = true;
            if (text_InterestRate.Text.Equals(""))
            {
                label_ErrorInterestRate.Content = "Campo vacio, favor de llenarlo";
                resultado = false;
            }
            if (text_IVA.Text.Equals(""))
            {
                label_ErrorIVA.Content = "Campo vacio, favor de llenarlo";
                resultado = false;
            }
            return resultado;
        }
    }
}
