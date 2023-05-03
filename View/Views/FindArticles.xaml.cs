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
using System.Windows.Controls.Primitives;

namespace View.Views
{
    /*
     * NOTA: tengo que buscar ARTICULO, PRENDA,
     */
    public partial class FindArticles : Page
    {
        private List<Belongings_Articles> articlesList;
        private Boolean datePickerEnabled = true;

        public FindArticles()
        {
            InitializeComponent();
            articlesList = new List<Belongings_Articles>();
            LoadArticles2();
            LoadCategories();
        }

        //ORIGINAL
        private void LoadArticles()
        {
            int statusCode;
            if (Utilitys.VerifyConnection())
            {
                (statusCode, articlesList) = ArticleDAO.getArticles();
                if(statusCode == MessageCode.CONNECTION_ERROR)
                {
                    ErrorManager.ShowWarning(MessageError.CONNECTION_ERROR);
                    this.Content = null;
                }else
                {
                    LoadTable();
                }
            }
            else
            {
                ErrorManager.ShowWarning(MessageError.CONNECTION_ERROR);
                this.Content = null;
            }
        }

       

        private void LoadCategories()
        {
            List<string> categoriesList = new List<string> { "Selecciona una categoria", "Joyeria", "Relojeria", "Herramientas" };
            cbCategory.ItemsSource = categoriesList;
            cbCategory.SelectedIndex = 0;


        }

        private void LoadTable()
        {
            dgArticles.Items.Clear();
            dgArticles.ItemsSource = articlesList;
            //dgArticles.RowHeight = double.NaN;//tamaño de filas automaticas al contenido
        }

        private void Btn_EditArticle(object sender, RoutedEventArgs e)
        {
            if (dgArticles.SelectedIndex >= 0)
            {
                // Container.NavigationService.Navigate(new EditArticle(articles[dgArticles.SelectedIndex]))
            }
            else
                ErrorManager.ShowError(MessageError.ITEM_NOT_SELECTED);
        }



        private void btn_CleanFilters(object sender, RoutedEventArgs e)
        {
            tbSearchField.Clear();
            cbDateEnabled.IsChecked = false;
            cbCategory.SelectedIndex = 0;

        }

        private void btn_FilterArticles(object sender, RoutedEventArgs e)
        {
            string filter = tbSearchField.Text;
            var result = dgArticles.Items.Cast<Belongings_Articles>().Where(item => item.barCode.Contains(filter));
            result = dgArticles.Items.Cast<Belongings_Articles>().Where(item => item.Belonging.serialNumber.Contains(filter));
            // if(cbCategory.SelectedIndex)
            //miDataGrid.ItemsSource = datosFiltrados;
        }

        private void Cb_CalendarEnabled(object sender, RoutedEventArgs e)
        {
            if (cbDateEnabled.IsChecked == true)
                dpDate.IsEnabled = true;
            else
                dpDate.IsEnabled = false;

        }

        private void bt_dateUnabled(object sender, RoutedEventArgs e)
        {
            if (cbDateEnabled.IsChecked == true)
                dpDate.IsEnabled = true;
            else
                dpDate.IsEnabled = false;
        }
    }
}
