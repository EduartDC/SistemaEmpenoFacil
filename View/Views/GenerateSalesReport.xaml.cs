/*
 * Autor: Jonathan Hernandez Martinez
 */
using BusinessLogic;
using Domain;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
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
using View.Properties;
using Document = iTextSharp.text.Document;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para GenerateSalesReport.xaml
    /// </summary>
    public partial class GenerateSalesReport : Page
    {
        private List<string> filter = new List<string> { "Selecciona una opcion", "Articulos en stock", "articulos vendidos" };
        List<ArticleDomain> articlesStockList = new List<ArticleDomain>();
        public GenerateSalesReport()
        {
            InitializeComponent();
            LoadFilter();
        }

        private void LoadFilter()
        {
            cbFilter.ItemsSource = filter;
            cbFilter.SelectedIndex = 0;

        }

        private void ClosePage_btn(object sender, RoutedEventArgs e)
        {

            this.Content = null;
        }

        private void Btn_CreateReport(object sender, RoutedEventArgs e)
        {

            if (CheckDatePickers() && CheckComboBoxIsSelected())//adentro falta otro if para seleccionar el tipo de reporte
            {
                int result = 0;
                (result, articlesStockList) = ArticleDAO.GetArticlesStockToReport(DateTime.Parse(dpDateBegin.Text), DateTime.Parse(dpDateEnd.Text));
                if (result == MessageCode.CONNECTION_ERROR)
                {
                    ErrorManager.ShowWarning(MessageError.CONNECTION_ERROR);
                }
                else if (result == MessageCode.ERROR_UPDATE)
                {
                    ErrorManager.ShowWarning(MessageError.CONNECTION_ERROR);
                }
                else if (result == MessageCode.SUCCESS)
                {
                    GenerateReportArticlesStock();
                    MessageBox.Show("reporte creado, falta imprimir");
                }
            }
        }

        private void GenerateReportArticlesStock()
        {
            string pdfName = string.Format("ReporteStock-{0}.pdf","xd");
            string pathBase = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = string.Format("{0}\\pdfs", pathBase);
            bool exists = Directory.Exists(path);
            if(!exists)
                Directory.CreateDirectory(path);
            string fullPath = string.Format("{0}\\{1}",path,pdfName);
            Document doc = new Document(PageSize.LETTER);
            PdfWriter writer = PdfWriter.GetInstance(
                doc, new FileStream(fullPath, FileMode.Create));
            doc.AddTitle("ReporteStock" + DateTime.Now);
            doc.Open();
            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);
            Font _standardFontBold = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD, BaseColor.BLACK);
            
            //formateando reporte
            doc.Add(new iTextSharp.text.Paragraph("articulos en stock"));
            doc.Add(Chunk.NEWLINE);


            //formateando tabla
            PdfPTable tbArticles = new PdfPTable(6);
            tbArticles.WidthPercentage = 90;

            //encabezados de tabla
            PdfPCell c01 = new PdfPCell(new Phrase("estado", _standardFontBold));
            c01.BorderWidth = 0;
            c01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c01.BorderWidthBottom = 0.75f;

            PdfPCell c02 = new PdfPCell(new Phrase("descripcion", _standardFontBold));
            c01.BorderWidth = 0;
            c01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c01.BorderWidthBottom = 0.75f;

            PdfPCell c03 = new PdfPCell(new Phrase("valor apreciado", _standardFontBold));
            c01.BorderWidth = 0;
            c01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c01.BorderWidthBottom = 0.75f;

            PdfPCell c04 = new PdfPCell(new Phrase("Valor de prestamo", _standardFontBold));
            c01.BorderWidth = 0;
            c01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c01.BorderWidthBottom = 0.75f;

            PdfPCell c05 = new PdfPCell(new Phrase("Valor de venta", _standardFontBold));
            c01.BorderWidth = 0;
            c01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c01.BorderWidthBottom = 0.75f;

            PdfPCell c06 = new PdfPCell(new Phrase("Fecha de comercializacion", _standardFontBold));
            c01.BorderWidth = 0;
            c01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c01.BorderWidthBottom = 0.75f;

            tbArticles.AddCell(c01);
            tbArticles.AddCell(c02);
            tbArticles.AddCell(c03);
            tbArticles.AddCell(c04);
            tbArticles.AddCell(c05);
            tbArticles.AddCell(c06);

            //agregando filas a tabla
            foreach(var util in articlesStockList)
            {
                PdfPCell cellState = new PdfPCell(new Phrase(util.stateArticle));
                cellState.BorderWidth = 0;
                PdfPCell cellDescription = new PdfPCell(new Phrase(util.description));
                cellDescription.BorderWidth = 0;
                PdfPCell cellAppraisalAmount = new PdfPCell(new Phrase(util.appraisalValue.ToString()));
                cellAppraisalAmount.BorderWidth = 0;
                PdfPCell cellLoanAmount = new PdfPCell(new Phrase(util.loanAmount.ToString()));
                cellLoanAmount.BorderWidth = 0;
                PdfPCell cellSellingPrice = new PdfPCell(new Phrase(util.sellingPrice.ToString()));
                cellSellingPrice.BorderWidth = 0;
                PdfPCell cellCreationDate = new PdfPCell(new Phrase(util.createDate.ToString()));
                cellCreationDate.BorderWidth = 0;

                tbArticles.AddCell(cellState);
                tbArticles.AddCell(cellDescription);
                tbArticles.AddCell(cellAppraisalAmount);
                tbArticles.AddCell(cellLoanAmount);
                tbArticles.AddCell(cellSellingPrice);
                tbArticles.AddCell(cellCreationDate);
            }
            doc.Add(tbArticles);
            doc.Close();
            writer.Close();
            
        }

        private bool CheckComboBoxIsSelected()
        {
            if (cbFilter.SelectedIndex == 0)
                return false;
            else
            {
                ErrorManager.ShowError("Favor de seleccionar una tipo de reporte para su creacion");
                return true;

            }

        }

        private bool CheckDatePickers()
        {
            if (dpDateBegin.SelectedDate.HasValue && dpDateEnd.SelectedDate.HasValue)
                return true;
            else
            {
                ErrorManager.ShowError("Favor de seleccionar una fecha de inicio y una fecha de fin para realizar el filtro ");
                return false;
            }
        }
    }
}
