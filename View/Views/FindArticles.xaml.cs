/*
 * Autor: Jonathan Hernandez Martinez
 */
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLogic;
using View.Properties;

namespace View.Views
{

    public partial class FindArticles : Page
    {
        private List<Belongings_Articles> articles = new List<Belongings_Articles>();
        public FindArticles()
        {
            InitializeComponent();
            LoadArticles();
        }

        private void LoadArticles()
        {
            int code = 0;
            articles.Clear();
            (code, articles) = ArticleDAO.getArticles();
            MessageBox.Show(code.ToString());
            if (code == MessageCode.SUCCESS)
            {
                LoadTable();
            }
            else
            if (code == MessageCode.CONNECTION_ERROR)
            {
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
                //codigo para cerrar page
            }
        }

        private void LoadTable()
        {
            dgArticles.Items.Clear();
            dgArticles.ItemsSource = articles;
        }
    }
}
