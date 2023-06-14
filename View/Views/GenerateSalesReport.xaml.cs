/*
 * Autor: Jonathan Hernandez Martinez
 */
using BusinessLogic;
using ceTe.DynamicPDF.Printing;
using Domain;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices.ComTypes;
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
using static iTextSharp.text.pdf.AcroFields;
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
        List<SaledArticle> SaledArticlesList = new List<SaledArticle>();
        public GenerateSalesReport()
        {
            InitializeComponent();
            LoadFilter();
        }

        private void LoadFilter()
        {
            cbFilter.ItemsSource = filter;
            cbFilter.SelectedIndex = 0;
            dpDateEnd.Text = DateTime.Now.ToString("dd/MM/yyyy");
            cbFilter.SelectedIndex = 0;

        }

        private void ClosePage_btn(object sender, RoutedEventArgs e)
        {

            this.Content = null;
        }

        private void Btn_CreateReport(object sender, RoutedEventArgs e)
        {
            if (CheckDatePickers())//adentro falta otro if para seleccionar el tipo de reporte
                if (CheckComboBoxIsSelected())
                    if (ValidateDates())
                        if (cbFilter.SelectedIndex == 1)
                            GetStockList();
                        else if (cbFilter.SelectedIndex == 2)
                            GetSaleList();
        }

        private bool ValidateDates()
        {
            if (DateTime.Parse(dpDateBegin.Text) <= DateTime.Parse(dpDateEnd.Text))
                return true;
            ErrorManager.ShowError("La fecha de filtro minima no puede ser superior al la fecha de filtro superior.Favor de verificar");
            return false;
        }

        private void GetSaleList()
        {
            int result = 0;
            SaledArticlesList.Clear();
            (result, SaledArticlesList) = ArticleDAO.GetSaledArticlesToReport(DateTime.Parse(dpDateBegin.Text), DateTime.Parse(dpDateEnd.Text));
            if (result == MessageCode.CONNECTION_ERROR)
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
            else if (result == MessageCode.SUCCESS) 
            GenerateReportSaledArticles();
        }



        public void GetStockList()
        {
            articlesStockList.Clear();
            int result = 0;
            (result, articlesStockList) = ArticleDAO.GetArticlesStockToReport(DateTime.Parse(dpDateBegin.Text), DateTime.Parse(dpDateEnd.Text));
            if (result == MessageCode.CONNECTION_ERROR)
            {
                ErrorManager.ShowWarning(MessageError.CONNECTION_ERROR);
            }
            else if (result == MessageCode.SUCCESS)
            {
                GenerateReportArticlesStock();
                MessageBox.Show("reporte creado");
            }
        }

        private void GenerateReportSaledArticles()
        {
            string date = DateTime.Now.Ticks.ToString();
            string pdfName = string.Format("ReporteStock-{0}.pdf", date);
            string pathBase = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = string.Format("{0}\\pdfs", pathBase);
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
            string fullPath = string.Format("{0}\\{1}", path, pdfName);
            Document doc = new Document(PageSize.LETTER);
            PdfWriter writer = PdfWriter.GetInstance(
                doc, new FileStream(fullPath, FileMode.Create));
            doc.AddTitle(string.Format("Reporte articulos stock{0}", DateTime.Now.Ticks));
            doc.Open();
            //editando encabezados
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Empeño Facil", new Font(Font.FontFamily.HELVETICA, 23, Font.BOLD));
            doc.Add(title);

            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);
            Font _standardFontBold = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD, BaseColor.BLACK);

            //formateando reporte
            doc.Add(new iTextSharp.text.Paragraph("Reporte de articulos vendidos", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD)));
            doc.Add(new iTextSharp.text.Paragraph("-----------------------------------------------------------------------------------------------------"));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Fecha de filtro menor:------{0}", dpDateBegin.Text)));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Fecha de filtro superior:---{0}", dpDateEnd.Text)));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Elementos mostrados---------{0} articulos", SaledArticlesList.Count().ToString())));
            var staff = (App.Current as App)._staffInfo;
            doc.Add(new iTextSharp.text.Paragraph("-----------------------------------------------------------------------------------------------------"));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Fecha de creacion:----------{0}", DateTime.Now.ToString("dd/MM/yyyy"))));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Gerente en turno:-----------{0} {1}", staff.fisrtName, staff.lastName)));


            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);

            PdfPTable tbArticles = new PdfPTable(6);
            tbArticles.WidthPercentage = 95;

            PdfPCell c01 = new PdfPCell(new Phrase("Estado", _standardFontBold));
            c01.BorderWidth = 0;
            c01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c01.BorderWidthBottom = 0.75f;

            PdfPCell c02 = new PdfPCell(new Phrase("Fecha de venta", _standardFontBold));
            c02.BorderWidth = 0;
            c02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c02.BorderWidthBottom = 0.75f;

            PdfPCell c03 = new PdfPCell(new Phrase("Cliente", _standardFontBold));
            c03.BorderWidth = 0;
            c03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c03.BorderWidthBottom = 0.75f;

            PdfPCell c04 = new PdfPCell(new Phrase("Articulo", _standardFontBold));
            c04.BorderWidth = 0;
            c04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c04.BorderWidthBottom = 0.75f;

            PdfPCell c05 = new PdfPCell(new Phrase("Precio de adquisicion", _standardFontBold));
            c05.BorderWidth = 0;
            c05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c05.BorderWidthBottom = 0.75f;

            PdfPCell c06 = new PdfPCell(new Phrase("Precio de venta", _standardFontBold));
            c06.BorderWidth = 0;
            c06.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c06.BorderWidthBottom = 0.75f;

            tbArticles.AddCell(c01);
            tbArticles.AddCell(c02);
            tbArticles.AddCell(c03);
            tbArticles.AddCell(c04);
            tbArticles.AddCell(c05);
            tbArticles.AddCell(c06);

            //cliente
            //articul
            //loan
            //venta precio
            foreach (var util in SaledArticlesList)
            {
                PdfPCell cellState = new PdfPCell(new Phrase(util.article.stateArticle));
                cellState.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellState.BorderWidth = 0;

                PdfPCell cellDate = new PdfPCell(new Phrase(util.saleDate.ToString("dd/MM/yyyy")));
                cellDate.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellDate.BorderWidth = 0;

                PdfPCell cellCustomer = new PdfPCell(new Phrase(util.customer));
                cellCustomer.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCustomer.BorderWidth = 0;

                PdfPCell cellArticle = new PdfPCell(new Phrase(util.article.description));
                cellArticle.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellArticle.BorderWidth = 0;

                PdfPCell cellLoan = new PdfPCell(new Phrase(util.article.loanAmount.ToString()));
                cellLoan.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellLoan.BorderWidth = 0;

                PdfPCell cellSellingPrice = new PdfPCell(new Phrase(util.article.sellingPrice.ToString()));
                cellSellingPrice.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellSellingPrice.BorderWidth = 0;

                tbArticles.AddCell(cellState);
                tbArticles.AddCell(cellDate);
                tbArticles.AddCell(cellCustomer);
                tbArticles.AddCell(cellArticle);
                tbArticles.AddCell(cellLoan);
                tbArticles.AddCell(cellSellingPrice);
            }
            doc.Add(tbArticles);
            doc.Close();
            writer.Close();
            System.Diagnostics.Process.Start(fullPath);
        }

        private void GenerateReportArticlesStock()
        {
            string date = DateTime.Now.Ticks.ToString();
            string pdfName = string.Format("ReporteStock-{0}.pdf", date);
            string pathBase = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = string.Format("{0}\\pdfs", pathBase);
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
            string fullPath = string.Format("{0}\\{1}", path, pdfName);
            Document doc = new Document(PageSize.LETTER);
            PdfWriter writer = PdfWriter.GetInstance(
                doc, new FileStream(fullPath, FileMode.Create));
            doc.AddTitle(string.Format("Reporte articulos stock{0}", DateTime.Now.Ticks));
            doc.Open();
            //editando encabezados
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Empeño Facil", new Font(Font.FontFamily.HELVETICA, 23, Font.BOLD));
            doc.Add(title);

            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);
            Font _standardFontBold = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD, BaseColor.BLACK);

            //formateando reporte
            doc.Add(new iTextSharp.text.Paragraph("Reporte de articulos en stock", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD)));
            doc.Add(new iTextSharp.text.Paragraph("-----------------------------------------------------------------------------------------------------"));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Fecha de filtro menor:------{0}", dpDateBegin.Text)));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Fecha de filtro superior:---{0}", dpDateEnd.Text)));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Elementos mostrados---------{0} articulos", articlesStockList.Count().ToString())));
            var staff = (App.Current as App)._staffInfo;
            doc.Add(new iTextSharp.text.Paragraph("-----------------------------------------------------------------------------------------------------"));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Fecha de creacion:----------{0}", DateTime.Now.ToString("dd/MM/yyyy"))));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Gerente en turno:-----------{0} {1}", staff.fisrtName, staff.lastName)));


            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);

            //formateando tabla
            PdfPTable tbArticles = new PdfPTable(9);
            tbArticles.WidthPercentage = 95;

            //encabezados de tabla
            PdfPCell c01 = new PdfPCell(new Phrase("estado", _standardFontBold));
            c01.BorderWidth = 0;
            c01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c01.BorderWidthBottom = 0.75f;

            PdfPCell c02 = new PdfPCell(new Phrase("descripcion", _standardFontBold));
            c02.BorderWidth = 0;
            c02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c02.BorderWidthBottom = 0.75f;

            PdfPCell c09 = new PdfPCell(new Phrase("Categoria", _standardFontBold));
            c09.BorderWidth = 0;
            c09.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c09.BorderWidthBottom = 0.75f;

            PdfPCell c03 = new PdfPCell(new Phrase("valor apreciado", _standardFontBold));
            c03.BorderWidth = 0;
            c03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c03.BorderWidthBottom = 0.75f;

            PdfPCell c04 = new PdfPCell(new Phrase("Valor de prestamo", _standardFontBold));
            c04.BorderWidth = 0;
            c04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c04.BorderWidthBottom = 0.75f;

            PdfPCell c05 = new PdfPCell(new Phrase("Valor de venta", _standardFontBold));
            c05.BorderWidth = 0;
            c05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c05.BorderWidthBottom = 0.75f;

            PdfPCell c06 = new PdfPCell(new Phrase("Fecha de comercializacion", _standardFontBold));
            c06.BorderWidth = 0;
            c06.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c06.BorderWidthBottom = 0.75f;

            PdfPCell c07 = new PdfPCell(new Phrase("Numero de barras", _standardFontBold));
            c07.BorderWidth = 0;
            c07.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c07.BorderWidthBottom = 0.75f;

            PdfPCell c08 = new PdfPCell(new Phrase("Numero de bolsa", _standardFontBold));
            c08.BorderWidth = 0;
            c08.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c08.BorderWidthBottom = 0.75f;


            tbArticles.AddCell(c01);
            tbArticles.AddCell(c02);
            tbArticles.AddCell(c09);
            tbArticles.AddCell(c07);
            tbArticles.AddCell(c08);
            tbArticles.AddCell(c03);
            tbArticles.AddCell(c04);
            tbArticles.AddCell(c05);
            tbArticles.AddCell(c06);

            //agregando filas a tabla
            foreach (var util in articlesStockList)
            {
                PdfPCell cellState = new PdfPCell(new Phrase(util.stateArticle));
                cellState.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellState.BorderWidth = 0;

                PdfPCell cellDescription = new PdfPCell(new Phrase(util.description));
                cellDescription.BorderWidth = 0;
                cellDescription.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                PdfPCell cellAppraisalAmount = new PdfPCell(new Phrase("$ " + util.appraisalValue.ToString()));
                cellAppraisalAmount.BorderWidth = 0;
                cellAppraisalAmount.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                PdfPCell cellLoanAmount = new PdfPCell(new Phrase("$ " + util.loanAmount.ToString()));
                cellLoanAmount.BorderWidth = 0;
                cellLoanAmount.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                PdfPCell cellSellingPrice = new PdfPCell(new Phrase("$ " + util.sellingPrice.ToString()));
                cellSellingPrice.BorderWidth = 0;
                cellSellingPrice.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                PdfPCell cellCreationDate = new PdfPCell(new Phrase(util.createDate.ToString("dd/MM/yyyy")));
                cellCreationDate.BorderWidth = 0;
                cellCreationDate.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                PdfPCell cellCategory = new PdfPCell(new Phrase(util.category));
                cellCreationDate.BorderWidth = 0;
                cellCategory.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                PdfPCell cellBarCode = new PdfPCell(new Phrase(util.barCode));
                cellCreationDate.BorderWidth = 0;
                cellBarCode.HorizontalAlignment = PdfPCell.ALIGN_CENTER;


                PdfPCell cellContratId = new PdfPCell(new Phrase(util.idContract.ToString()));
                cellContratId.BorderWidth = 0;
                cellContratId.HorizontalAlignment = PdfPCell.ALIGN_CENTER;


                tbArticles.AddCell(cellState);
                tbArticles.AddCell(cellDescription);
                tbArticles.AddCell(cellCategory);
                tbArticles.AddCell(cellBarCode);
                tbArticles.AddCell(cellContratId);
                tbArticles.AddCell(cellAppraisalAmount);
                tbArticles.AddCell(cellLoanAmount);
                tbArticles.AddCell(cellSellingPrice);
                tbArticles.AddCell(cellCreationDate);
            }
            doc.Add(tbArticles);
            doc.Close();
            writer.Close();
            // PrintPDF(fullPath);
            System.Diagnostics.Process.Start(fullPath);
        }




        public void PrintPDF(string pdfPath)
        {
            //try
            //{
            //    PrinterSettings printerSettings = new PrinterSettings();
            //    string defaultPrinterName = printerSettings.PrinterName;
            //    string pathConverted = pdfPath.Replace("\\", "\\\\");
            //    Console.WriteLine("RUTA: " + pathConverted);
            //    ProcessStartInfo psi = new ProcessStartInfo
            //    {
            //        FileName = "AcroRd32.exe",
            //        Arguments = $"/t \"{pathConverted}\" \"{defaultPrinterName}\""
            //    };

            //    Process.Start(psi);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error al imprimir el archivo PDF: " + ex.Message + "\n");
            //}
            try
            {
                PrinterSettings printerSettings = new PrinterSettings();
                string defaultPrinterName = printerSettings.PrinterName;
                string pathConverted = pdfPath.Replace("\\", "\\\\");
                Console.WriteLine("RUTA: " + pathConverted);
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "AcroRd32.exe",
                    Arguments = $"/t \"{pathConverted}\" \"{defaultPrinterName}\""
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir el archivo PDF: " + ex.Message);
            }


        }


        private bool CheckComboBoxIsSelected()
        {
            if (cbFilter.SelectedIndex == 0)
            {
                ErrorManager.ShowError("Favor de seleccionar una tipo de reporte para su creacion");
                return false;
            }
            else
                return true;
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

        private void SelectionComboBox(object sender, SelectionChangedEventArgs e)
        {
            if (cbFilter.SelectedIndex == 0)
            {
                lbBegin.Content = "Fecha de inicio de busqueda";
                lbEnd.Content = "Fecha de fin de busqueda";
            }
            else if (cbFilter.SelectedIndex == 1)
            {
                lbBegin.Content = "Fecha de comercializacion menor";
                lbEnd.Content = "Fecha de comercizalicacion mayor";
            }
            else if (cbFilter.SelectedIndex == 2)
            {
                lbBegin.Content = "Fecha de venta menor";
                lbEnd.Content = "Fecha de venta mayor";
            }

        }
    }
}
