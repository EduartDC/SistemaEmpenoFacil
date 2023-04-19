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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for SetAsideView.xaml
    /// </summary>
    public partial class SetAsideView : Page
    {
        public SetAsideView()
        {
            InitializeComponent();

        }

        private void itemHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void itemExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            window.PrimaryContainer.Effect = blurEffect;
            //window.SecundaryContainer.Navigate(new TransactionView(0, 0.0));
            window.SecundaryContainer.Navigate(new CustomerView());
            window.PrimaryContainer.IsHitTestVisible = false;
        }
    }
}
