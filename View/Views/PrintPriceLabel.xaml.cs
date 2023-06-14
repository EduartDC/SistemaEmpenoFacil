using BusinessLogic;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using Document = iTextSharp.text.Document;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View.Properties;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Xml.Linq;
using ceTe.DynamicPDF.Printing;
using System.Diagnostics;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para PrintPriceLabel.xaml
    /// </summary>
    public partial class PrintPriceLabel : Page
    {
        private List<ArticleDomain> articles = new List<ArticleDomain>();
        private Boolean datePickerEnabled = true;
        private ICollectionView collectionView;
        public PrintPriceLabel()
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
                
                LoadTable();
            }
            else
            if (code == MessageCode.CONNECTION_ERROR)
            {
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
                this.Content = null;
            }
        }

       

        private void LoadCategories()
        {
            List<string> categoriesList = new List<string> { "Selecciona una categoria", "Joyeria", "Relojeria", "Informatica", "Electrodomesticos", "linea blanca", "Instrumentos", "Herramientas", "Telefonia" };
            cbCategory.ItemsSource = categoriesList;
            cbCategory.SelectedIndex = 0;
        }

        //True:ListaOriginal - False:listafiltrada
        private void LoadTable()
        {
            collectionView = CollectionViewSource.GetDefaultView(articles);
            collectionView.Filter = (item) => true;
            dgArticles.ItemsSource = collectionView;
            

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
            
        }

        private void btn_FilterArticles(object sender, RoutedEventArgs e)
        {

            FilterTable();
            

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
                        article.description.Contains(filterTb) ||
                        article.idContract.ToString().Equals(filterTb) ||
                        article.serialNumber.Equals(filterTb) ||
                        article.barCode.Equals(filterTb);

                    }
                    return false;
                };
            }
            if (cbCategory.SelectedIndex >= 1 && string.IsNullOrEmpty(tbSearchField.Text))//solo categoria
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
                        article.description.Contains(filterTb) &&
                        article.category.Equals(filterCategory) ||
                        article.serialNumber.Equals(filterTb) &&
                        article.category.Equals(filterCategory) ||
                        article.barCode.Equals(filterTb) &&
                        article.category.Equals(filterCategory) ||
                        article.idContract.ToString().Equals(filterTb) &&
                        article.category.Equals(filterCategory);

                    }
                    return false;
                };
            }
            if (cbDateEnabled.IsChecked == true && string.IsNullOrEmpty(tbSearchField.Text) && cbCategory.SelectedIndex == 0)
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
                        article.description.Contains(filterTb) &&
                        article.createDate >= DateTime.Parse(dpDate.Text) ||
                        article.idContract.ToString().Equals(filterTb) &&
                        article.createDate >= DateTime.Parse(dpDate.Text) ||
                        article.barCode.Equals(filterTb) &&
                        article.createDate >= DateTime.Parse(dpDate.Text) ||
                        article.serialNumber.Equals(filterTb) &&
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
                        article.description.Contains(filterTb) &&
                        article.category.Equals(cbCategory.SelectedItem.ToString()) &&
                        article.createDate >= DateTime.Parse(dpDate.Text) ||
                        article.idContract.ToString().Equals(filterTb) &&
                        article.category.Equals(cbCategory.SelectedItem.ToString()) &&
                        article.createDate >= DateTime.Parse(dpDate.Text) ||
                        article.barCode.Equals(filterTb) &&
                        article.category.Equals(cbCategory.SelectedItem.ToString()) &&
                        article.createDate >= DateTime.Parse(dpDate.Text) ||
                        article.serialNumber.Equals(filterTb) &&
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

        private void Btn_PrintLabel(object sender, RoutedEventArgs e)
        {

            //string date = DateTime.Now.Ticks.ToString();
            //string pdfName = string.Format("EtiquetasArtiuclos-{0}.pdf", date);
            //string pathBase = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            //string path = string.Format("{0}\\pdfs", pathBase);
            //bool exists = Directory.Exists(path);
            //if (!exists)
            //    Directory.CreateDirectory(path);
            //string fullPath = string.Format("{0}\\{1}", path, pdfName);
            //Document doc = new Document(PageSize.LETTER);
            //PdfWriter writer = PdfWriter.GetInstance(
            //    doc, new FileStream(fullPath, FileMode.Create));
            //doc.Open();
            //doc.Close();
            //writer.Close();
            //System.Diagnostics.Process.Start(fullPath);

            //foreach (var article in articles)
            //{
            //    Zen.Barcode.Code128BarcodeDraw draw = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;

            //    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(draw.Draw(article.barCode, 60));
            //    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bitmap, System.Drawing.Imaging.ImageFormat.Bmp);

            //}
            print();


        }
        
        private void print()
        {

            Document document = new Document(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("etiqueta.pdf", FileMode.Create));
            document.Open();

            PdfContentByte cb = writer.DirectContent;
            float x = 150f;
            float y = 750f;
            float width = 140f;
            float height = 55f;

            foreach (var article in articles)
            {
                cb.Rectangle(x, y, width, height);
                cb.Stroke();


                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.SetFontAndSize(bf, 12);

                cb.BeginText();
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, article.description, x + 5f, y + 35f, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, article.characteristics, x + 5f, y + 20f, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "$" + article.sellingPrice.ToString(), x + 5f, y + 5, 0);
                cb.EndText();

                Zen.Barcode.Code128BarcodeDraw draw = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(draw.Draw(article.barCode, 60));
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bitmap, System.Drawing.Imaging.ImageFormat.Bmp);
                img.ScaleAbsolute(50f, 50f);
                img.SetAbsolutePosition(x + 85f, y +3f);
                document.Add(img);

                //document.NewPage();
                x += width + 10f;
            }

            document.Close();
            Process.Start("etiqueta.pdf");
        }

       
    }
}