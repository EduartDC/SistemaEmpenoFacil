﻿/*
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
using System.ComponentModel;
using System.IO;
using Domain.Communitation;

namespace View.Views
{

    public partial class FindArticles : Page, Communication
    {
        private List<ArticleDomain> articles = new List<ArticleDomain>();
        private Boolean datePickerEnabled = true;
        private ICollectionView collectionView;

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

        private   void LoadArticles() 
        {
            articles.Clear();
            int code = 0;
            articles.Clear();
            (code, articles) =  ArticleDAO.getArticles() ;

            if (code == MessageCode.SUCCESS)
            {
                ConverterImagesFormat();
                LoadTable();
            }
            else
            if (code == MessageCode.CONNECTION_ERROR)
            {
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
                this.Content = null;
            }
        }

        private void ConverterImagesFormat()

        {
            for(int i = 0; i< articles.Count(); i++)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(articles[i].imageOne);
                bitmap.EndInit();
                articles[i].imageConverted = bitmap;
                
            }
        }

        private void LoadCategories()
        {
            List<string> categoriesList = new List<string> { "Selecciona una categoria", "Joyería", "Relojería", "Herramientas", "Electrónica", "Línea blanca", "Instrumentos" };
            cbCategory.ItemsSource = categoriesList;
            cbCategory.SelectedIndex = 0;
        }

        private void LoadTable()
        {
            collectionView = CollectionViewSource.GetDefaultView(articles);
            collectionView.Filter = (item) => true;
            dgArticles.ItemsSource = collectionView;
            CountArticles();

        }

        private void Btn_EditArticle(object sender, RoutedEventArgs e)
        {

            if (SelectedItem())
            {
                var idArticle = dgArticles.SelectedItem as ArticleDomain;
                EditArticle editArticle = new EditArticle(idArticle.idArticle,this);
                editArticle.ShowDialog();
                
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
            dpDate.Text = DateTime.Now.ToString();
            if (string.IsNullOrEmpty(tbSearchField.Text) && cbCategory.SelectedIndex == 0 && cbDateEnabled.IsChecked == false)
            {
                collectionView.Filter = (item) =>
                {
                    Domain.ArticleDomain article = item as Domain.ArticleDomain;
                    if (article != null)
                    {
                        return
                        string.IsNullOrEmpty(tbSearchField.Text) &&
                        cbCategory.SelectedIndex == 0 &&
                        cbDateEnabled.IsChecked == false;
                    }
                    return false;
                };
            }
            CountArticles();
        }

        private void btn_FilterArticles(object sender, RoutedEventArgs e)
        {

            FilterTable();
            CountArticles();   

        }

        private void CountArticles()
        {
            lbCountArticles.Content = dgArticles.Items.Count;
        }

        private void FilterTable()
        {
            string filterTb = tbSearchField.Text;
            string filterCategory = cbCategory.SelectedItem.ToString();
            if (string.IsNullOrEmpty(tbSearchField.Text) && cbCategory.SelectedIndex == 0 && cbDateEnabled.IsChecked == false)
            {
                collectionView.Filter = (item) =>
                {
                    Domain.ArticleDomain article = item as Domain.ArticleDomain;
                    if (article != null)
                    {
                        return
                        string.IsNullOrEmpty(filterTb) &&
                        cbCategory.SelectedIndex == 0 &&
                        cbDateEnabled.IsChecked == false;
                    }
                    return false;
                };
            }
            if (!string.IsNullOrEmpty(tbSearchField.Text) && cbCategory.SelectedIndex <= 0)
            {
                collectionView.Filter = (item) =>
                {
                    Domain.ArticleDomain article = item as Domain.ArticleDomain;
                    if (article != null)
                    {
                        return string.IsNullOrEmpty(filterTb) ||
                        article.description.ToUpper().Contains(filterTb.ToUpper()) ||
                        article.idContract.ToString().Equals(filterTb) ||
                        article.serialNumber.ToUpper().Contains(filterTb.ToUpper()) ||
                        article.barCode.ToUpper().Contains(filterTb.ToUpper());

                    }
                    return false;
                };
            }
            if (cbCategory.SelectedIndex >= 1 && string.IsNullOrEmpty(tbSearchField.Text))
            {
                collectionView.Filter = (item) =>
                {
                    Domain.ArticleDomain article = item as Domain.ArticleDomain;
                    if (article != null)
                    {
                        return

                        article.category.Equals(filterCategory);


                    }
                    return false;
                };
            }
            if (!string.IsNullOrEmpty(tbSearchField.Text) && cbCategory.SelectedIndex >= 1)
            {
                collectionView.Filter = (item) =>
                {
                    Domain.ArticleDomain article = item as Domain.ArticleDomain;
                    if (article != null)
                    {
                        return
                        article.description.ToUpper().Contains(filterTb.ToUpper()) &&
                        article.category.Equals(filterCategory) ||
                        article.serialNumber.ToUpper().Contains(filterTb.ToUpper()) &&
                        article.category.Equals(filterCategory) ||
                        article.barCode.ToUpper().Contains(filterTb.ToUpper()) &&
                        article.category.Equals(filterCategory) ||
                        article.idContract.ToString().Equals(filterTb) &&
                        article.category.Equals(filterCategory);

                    }
                    return false;
                };
            }
            if(cbDateEnabled.IsChecked == true && string.IsNullOrEmpty(tbSearchField.Text) && cbCategory.SelectedIndex == 0)
            {
                DateTime selectedDate = DateTime.Parse(dpDate.Text);
                collectionView.Filter = (item) =>
                {
                    Domain.ArticleDomain article = item as Domain.ArticleDomain;
                    DateTime dateArticle = DateTime.Parse(article.createDate.ToString());
                    if (article != null)
                    {
                        return
                        dateArticle >= selectedDate;
                    }
                    return false;
                };
            }
            if (cbDateEnabled.IsChecked == true && !string.IsNullOrEmpty(tbSearchField.Text) && cbCategory.SelectedIndex == 0)
            {
                DateTime selectedDate = DateTime.Parse(dpDate.Text);
                collectionView.Filter = (item) =>
                {
                    Domain.ArticleDomain article = item as Domain.ArticleDomain;
                    DateTime dateArticle = DateTime.Parse(article.createDate.ToString());
                    if (article != null)
                    {
                        return
                        article.description.ToUpper().Contains(filterTb.ToUpper()) && 
                        article.createDate >= DateTime.Parse(dpDate.Text) ||
                        article.idContract.ToString().Equals(filterTb) &&
                        article.createDate >= DateTime.Parse(dpDate.Text) ||
                        article.barCode.ToUpper().Contains(filterTb.ToUpper()) &&
                        article.createDate >= DateTime.Parse(dpDate.Text) ||
                        article.serialNumber.ToUpper().Contains(filterTb.ToUpper()) &&
                        article.createDate >= DateTime.Parse(dpDate.Text);
                    }
                    return false;
                };
            }
            if (cbDateEnabled.IsChecked == true && string.IsNullOrEmpty(tbSearchField.Text) && cbCategory.SelectedIndex >= 1)
            {
                collectionView.Filter = (item) =>
                {
                    Domain.ArticleDomain article = item as Domain.ArticleDomain;
                    DateTime dateArticle = DateTime.Parse(article.createDate.ToString());
                    if (article != null)
                    {
                        return
                        article.category.Equals(filterCategory) &&
                        article.createDate >= DateTime.Parse(dpDate.Text);
                    }
                    return false;
                };
            }
            if (cbDateEnabled.IsChecked == true && !string.IsNullOrEmpty(tbSearchField.Text) && cbCategory.SelectedIndex >= 1)
            {
                collectionView.Filter = (item) =>
                {
                    Domain.ArticleDomain article = item as Domain.ArticleDomain;
                    DateTime dateArticle = DateTime.Parse(article.createDate.ToString());
                    if (article != null)
                    {
                        return
                        article.description.ToUpper().Contains(filterTb.ToUpper()) &&
                        article.category.Equals(cbCategory.SelectedItem.ToString()) &&
                        article.createDate >= DateTime.Parse(dpDate.Text) ||
                        article.idContract.ToString().Equals(filterTb) &&
                        article.category.Equals(cbCategory.SelectedItem.ToString()) &&
                        article.createDate >= DateTime.Parse(dpDate.Text) ||
                        article.barCode.ToUpper().Contains(filterTb.ToUpper()) &&
                        article.category.Equals(cbCategory.SelectedItem.ToString()) &&
                        article.createDate >= DateTime.Parse(dpDate.Text) ||
                        article.serialNumber.ToUpper().Contains(filterTb.ToUpper()) &&
                        article.category.Equals(cbCategory.SelectedItem.ToString()) &&
                        article.createDate >= DateTime.Parse(dpDate.Text);
                    }
                    return false;
                };
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        public void refreshBelongings(List<Domain.BelongingCreation.Belonging> belongingsList)//metodo de interfaz no util en este cu
        {
            
        }

        public void refreshArticles()
        {
            LoadArticles();
        }
    }
}
