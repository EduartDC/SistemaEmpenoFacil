using BusinessLogic;
using DataAcces;
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
    /// Lógica de interacción para AddPriceBelongingArticle.xaml
    /// </summary>
    public partial class AddPriceBelongingArticle : Page
    {
        int id;
        public AddPriceBelongingArticle(int idBelonging)
        {
            InitializeComponent();
            id=idBelonging;
        }
        private void btn_SetPrice_Click(object sender, RoutedEventArgs e)
        {
            double price = Double.Parse(tbPrice.Text);
            Belonging belonging = new Belonging();
            Staff staff = new Staff();
            double appraisalValue = belonging.appraisalValue;
            if (price <= appraisalValue)
            {
                MessageBox.Show("No se pude asignar un precio menor al monto de avaluo");


            }
            else
            {
                saveBelongingArticle();
                
            }
            MessageBox.Show("Se registró con éxito el articulo");
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 0;
            window.PrimaryContainer.Effect = blurEffect;
            window.SecundaryContainer.Content = null;
            window.PrimaryContainer.IsHitTestVisible = true;

        }
        private void saveBelongingArticle()
        {
            var belongingArticle = new Belongings_Articles();
            var result = MessageCode.ERROR;
            double price = Double.Parse(tbPrice.Text);
            belongingArticle.creationDate = DateTime.Now;
            belongingArticle.sellingPrice = price;
            belongingArticle.stateArticle = "Disponible";
            belongingArticle.idBelonging = id;
            belongingArticle.barCode = createbarCode();
            result = BelongingsArticlesDAO.AddBelongingArticle(belongingArticle);


        }
        private string createbarCode()
        {
            string barCode = "";
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                int randomNumber = random.Next(0, 36);
                char character = Convert.ToChar(randomNumber < 10 ? randomNumber + 48 : randomNumber + 55);
                barCode += character;
                Zen.Barcode.Code128BarcodeDraw barcodeDraw = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;


            }
            return barCode;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 0;
            window.PrimaryContainer.Effect = blurEffect;
            window.SecundaryContainer.Content = null;
            window.PrimaryContainer.IsHitTestVisible = true;
        }
    }
}
