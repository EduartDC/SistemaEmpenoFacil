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
using Domain;

namespace View.Views
{

    public partial class FindArticles : Page
    {
        private List<ArticleDomain> articles = new List<ArticleDomain>();
        private Boolean datePickerEnabled = true;
        private List<ArticleDomain> filteredArticles = new List<ArticleDomain>();

        public FindArticles()
        {
            InitializeComponent();
            LoadArticles();
            LoadCategories();
            LoadDate();
        }

        private void LoadDate()
        {
            dpDate.Text = DateTime.Now.ToString();
        }

        private void LoadArticles()
        {
            int code = 0;
            articles.Clear();
            (code, articles) = ArticleDAO.getArticles();

            if (code == MessageCode.SUCCESS)
            {
                LoadTable(true);
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

        //True:ListaOriginal - False:listafiltrada
        private void LoadTable(bool operation)
        {
            if (operation)
            {
                dgArticles.Items.Clear();
                dgArticles.ItemsSource = articles;
            }
            else
            {
                dgArticles.ItemsSource = null;
                dgArticles.ItemsSource = filteredArticles;
            }


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
            tbSearchField.Clear();
            cbDateEnabled.IsChecked = false;
            cbCategory.SelectedIndex = 0;
            dgArticles.ItemsSource = null;
            dgArticles.ItemsSource = articles;
            cbCategory.IsEnabled = false;
            cbCategoryEnabled.IsChecked = false;
        }

        private void btn_FilterArticles(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSearchField.Text) || cbDateEnabled.IsChecked == true || cbCategoryEnabled.IsChecked == true)
            {
                filteredArticles.Clear();
                if (!string.IsNullOrEmpty(tbSearchField.Text))
                    FilterTextBoxSearch();
                if (cbCategoryEnabled.IsChecked == true)
                    FilterComboBoxCategory();
                FilterDates();
                LoadTable(false);
            }
        }

        private void FilterTextBoxSearch()
        {
            if (!string.IsNullOrEmpty(tbSearchField.Text))
            {
                foreach (var util in articles)
                {
                    if (util.description.Contains(tbSearchField.Text) ||
                        util.idContract.ToString().Equals(tbSearchField.Text) ||
                        util.barCode.Equals(tbSearchField.Text))
                    {
                        filteredArticles.Add(util);

                    }
                }

            }
        }

        private void FilterComboBoxCategory()
        {

            if (cbCategory.SelectedIndex > 0 && string.IsNullOrEmpty(tbSearchField.Text))
            {
                foreach (var util in articles)
                {
                    bool result = true;
                    for (int i = 0; i < filteredArticles.Count(); i++)
                    {
                        if (util.idArticle == filteredArticles[i].idArticle)// ya existe en lista filtrada
                            result = false;
                    }
                    if (result)
                    {
                        if (util.category.Equals(cbCategory.SelectedItem.ToString()))
                            filteredArticles.Add(util);
                    }
                }

            }
        }

        private void FilterDates()
        {
            if (cbDateEnabled.IsChecked == true)
            {
                List<ArticleDomain> copyArticles = new List<ArticleDomain>();
                if (filteredArticles.Count() == 0)
                    filteredArticles = articles;
                for (int i = 0; i < filteredArticles.Count(); i++)
                {
                    ArticleDomain util = filteredArticles[i];
                    if (util.createDate >= DateTime.Parse(dpDate.Text))
                    {
                        copyArticles.Add(util);
                    }
                }
                filteredArticles = copyArticles;
            }
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

        private void Cb_CategoryEnabled(object sender, RoutedEventArgs e)
        {
            tbSearchField.Clear();
            cbCategory.IsEnabled = true;
            tbSearchField.IsEnabled = false;
        }

        private void bt_CategoryUnabled(object sender, RoutedEventArgs e)
        {

            cbCategory.IsEnabled = false;
            tbSearchField.IsEnabled = true;
        }
    }
}
