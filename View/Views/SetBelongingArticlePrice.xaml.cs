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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para SetBelongingArticlePrice.xaml
    /// </summary>
    public partial class SetBelongingArticlePrice : Window
    {
        int id;
        public SetBelongingArticlePrice(int idBelongingArticle)
        {
            InitializeComponent();
            id=idBelongingArticle;

        }

        

        private void btn_SetPrice_Click(object sender, RoutedEventArgs e)
        {
            saveBelongingArticle();
            Console.WriteLine(id);
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
                
            }
            return barCode;
        }

    }
}
