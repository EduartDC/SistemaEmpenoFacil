using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace View.Properties
{
    public class ErrorManager
    {
        public static void ShowError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowWarning(string mensaje)
        {
            MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static void ShowInformation(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static MessageBoxResult ShowQuestion(string mensaje)
        {
            MessageBoxResult result = MessageBox.Show(mensaje, "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result;
        }
    }
}
