using BusinessLogic;
using Domain;
using Domain.Communitation;
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
using View.Properties;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for ScanCodeView.xaml
    /// </summary>
    public partial class ScanCodeView : Page
    {

        MessageService communication;
        ArticleDomain _article;
        public ScanCodeView()
        {
            InitializeComponent();

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = ErrorManager.ShowQuestion(MessageError.CANCEL_OPERATION);

            if (result == MessageBoxResult.Yes)
            {
                CloseView();
            }
        }
        private void CloseView()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 0;
            window.PrimaryContainer.Effect = blurEffect;
            window.SecundaryContainer.Content = null;
            window.PrimaryContainer.IsHitTestVisible = true;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var result = false;
            var article = _article;
            if (article == null)
            {
                ErrorManager.ShowWarning("Primero busque un articulo disponible");
            }
            else if (!article.stateArticle.Equals("Activo"))
            {
                ErrorManager.ShowWarning("El articulo seleccionado no esta disponible para ser apartado.");
            }
            else
            {
                CloseView();
                communication.ScanCommunication(article);
            }
        }
        public void CommunicacionPages(MessageService communication)
        {

            this.communication = communication;

        }

        private void textCode_KeyDown(object sender, KeyEventArgs e)
        {
            string code = textCode.Text;
            if (!string.IsNullOrEmpty(code) && e.Key == Key.Enter)
            {
                try
                {
                    var article = ArticleDAO.GetArticleDomainByCode(code);
                    if (article.idArticle != 0)
                    {
                        _article = article;
                        SetInformation();
                    }
                    else
                    {
                        ErrorManager.ShowWarning(MessageError.ARTICLE_NOT_FOUND);
                    }
                }
                catch (Exception)
                {
                    ErrorManager.ShowWarning(MessageError.CONNECTION_ERROR);
                }
            }
            else if (string.IsNullOrEmpty(code) && e.Key == Key.Enter)
            {
                ErrorManager.ShowWarning(MessageError.FIELDS_EMPTY);
            }
        }

        private void SetInformation()
        {
            labelIdArticle.Content = "No. Articulo" + _article.idArticle;
            labelName.Content = "Estatus del Articulo:  " + _article.stateArticle;
            labelPrice.Content = "Precio: " + _article.sellingPrice;
        }
    }
}
