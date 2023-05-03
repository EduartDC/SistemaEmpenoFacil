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

    public partial class FindArticles : Page
    {
        private List<Belongings_Articles> articles = new List<Belongings_Articles>();
        private Boolean datePickerEnabled = true;

        public FindArticles()
        {
            InitializeComponent();
            LoadArticles();
            LoadCategories();
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
        private void LoadCategories()
        {
            List<string> categoriesList = new List<string> { "Selecciona una categoria", "Joyeria", "Relojeria", "Herramientas" };
            cbCategory.ItemsSource = categoriesList;
            cbCategory.SelectedIndex = 0;
        }

        private void LoadTable()
        {
            dgArticles.Items.Clear();
            dgArticles.ItemsSource = articles;
            dgArticles.RowHeight = double.NaN;//tamaño de filas automaticas al contenido
        }

        private void Btn_EditArticle(object sender, RoutedEventArgs e)
        {
            if (SelectedItem())
            {
                // Container.NavigationService.Navigate(new EditArticle(articles[dgArticles.SelectedIndex]))
            }
            else
                ErrorManager.ShowError(MessageError.ITEM_NOT_SELECTED);
        }

        private bool SelectedItem()
        {
            if (dgArticles.SelectedIndex >= 0)
                return true;
            else
                return false;
        }

        private void btn_CleanFilters(object sender, RoutedEventArgs e)
        {
            LoadArticles();
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

            dpDate.IsEnabled = false;
            datePickerEnabled = false;

        }

        private void bt_dateUnabled(object sender, RoutedEventArgs e)
        {
            dpDate.IsEnabled = true;
            datePickerEnabled = true;
        }
    }
}
