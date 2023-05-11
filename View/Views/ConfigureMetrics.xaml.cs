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
using BusinessLogic.Utility;

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
            InitializateCamps(MetricsDAO.RecoverMetrics());
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



        private Boolean ValidateFormat()
        {
            Boolean resultado = true;
            if (!Utilities.ValidateFormat(text_InterestRate.Text, "^[0-9]+$"))
            {
                label_ErrorInterestRate.Content = "Error con el formato, solo se aceptan numeros enteros";
                resultado = false;
            }
            if (!Utilities.ValidateFormat(text_IVA.Text, "^[0-9]+$"))
            {
                label_ErrorIVA.Content = "Error con el formato, solo se aceptan numeros enteros";
                resultado = false;
            }
            return resultado;
        }

        private Boolean ValidateEmptyCamps()
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



        private void Button_Registrar_Click(object sender, RoutedEventArgs e)
        {
            label_ErrorIVA.Content = "";
            label_ErrorInterestRate.Content = "";
            if (ValidateEmptyCamps() && ValidateFormat())
            {
                int resultUpdate = MetricsDAO.UpdateMetrics(text_InterestRate.Text, text_IVA.Text);
                switch (resultUpdate)
                {
                    case 500:
                        MessageBox.Show("No se ha podido conectar con la base de datos, favor de intentarlo más tarde");
                        break;

                    case 400:
                        MessageBox.Show("No se ha encontrado Metricas para editar en la base de datos, realizando registro de las metricas");
                        break;

                    case 200:
                        MessageBox.Show("Configuración Exitosa");
                        Close();
                        break;

                }
                if (resultUpdate == 400)
                {
                    registerMetrics();
                }


            }
        }

        private void registerMetrics()
        {
            switch(MetricsDAO.RegisterMetrics(text_InterestRate.Text, text_IVA.Text))
            {
                case 500:
                    MessageBox.Show("No se ha podido conectar con la base de datos, favor de intentarlo más tarde");
                    break;
                case 400:
                    MessageBox.Show("No se ha podido registrar las metricas en la base de datos, favor de intentarlo mas tarde");
                    break;
                case 200:
                    MessageBox.Show("Registro de las metricas exitosa");
                    Close();
                    break;
            }
        }

        private void Button_Salir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
