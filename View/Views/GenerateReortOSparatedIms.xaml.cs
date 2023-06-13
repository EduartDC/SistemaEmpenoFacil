using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.util;
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
using DataAcces;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Document = iTextSharp.text.Document;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para GenerateReortOSparatedIms.xaml
    /// </summary>
    public partial class GenerateReortOSparatedIms : Page
    {
        public GenerateReortOSparatedIms()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (startDateDatePicker.SelectedDate == null || endDateDatePicker.SelectedDate == null)
            {
                string message = "Selecicone por favor la fecha de inicio y la fecha de fin para generar el reporte";
                string messageTitle = "Fechas no seleccionadas";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
            }
            else
            {
                List<SetAside> setAsides = GetSetAsideArticles();
                if(setAsides == null || setAsides.Count() == 0)
                {
                    string message = "No se han encontrado articulos apartados en el rango de fechas especificado"
                    + "\nel sistema no creara un reporte vacio";
                    string messageTitle = "No hay articulos apartado";
                    MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                    MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                    MessageBoxResult messageBox;
                    messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                }
                else
                {
                    string fulpathPdf = GenerateSetAsideArticlesReport();
                    string message = "Se ha generado con exito el reporte de articulos apartados del periodo especificado"
                    + "\nen breve podra visualizar el reporte generado";
                    string messageTitle = "Reporte generado con exito";
                    MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                    MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                    MessageBoxResult messageBox;
                    messageBox = MessageBox.Show(message, messageTitle, messageBoxButton, messageBoxImage, MessageBoxResult.Yes);
                    System.Diagnostics.Process.Start(fulpathPdf);
                }
            }
        }

        private void GoBackButtonEvent(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private List<SetAside> GetSetAsideArticles()
        {
            DateTime startDate = (DateTime) startDateDatePicker.SelectedDate;
            DateTime endDate = (DateTime) endDateDatePicker.SelectedDate;
            List<SetAside> setAsides= SetAsideDAO.GetSedAsidesByDate(startDate, endDate);
            return setAsides;
        }

        private string GenerateSetAsideArticlesReport()
        {

            List<SetAside> setAsides = GetSetAsideArticles();

            DateTime startDate= (DateTime) startDateDatePicker.SelectedDate;
            DateTime endDate = (DateTime) endDateDatePicker.SelectedDate;

            string pdfname = string.Format("Reporte apartados {0}-{1}.pdf", startDate.ToString("dd-MM-yyyy"), endDate.ToString("dd-MM-yyyy"));
            string pathbase = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = string.Format("{0}\\pdfs\\reportesApartados", pathbase);
            bool exists = Directory.Exists(path);
            if (!exists)
            {
                Directory.CreateDirectory(path);
            }
            string fullPath = string.Format("{0}\\{1}", path, pdfname);
            Document document = new Document(PageSize.LETTER);
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(fullPath, FileMode.Create));
            document.AddTitle(string.Format("Reporte de articulos apartados de {0} a {1}", startDateDatePicker.SelectedDate, endDateDatePicker.SelectedDate));
            document.Open();

            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Empeño facil", new Font(Font.FontFamily.HELVETICA, 23, Font.BOLD));
            document.Add(title);

            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);
            Font _standardFontBold = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD, BaseColor.BLACK);

            document.Add(new iTextSharp.text.Paragraph("Reporte de articulos apartados", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD)));
            document.Add(new iTextSharp.text.Paragraph("-----------------------------------------------------------------------------------------------------"));
            document.Add(new iTextSharp.text.Paragraph(string.Format("Fecha de inicio de busqueda:------{0}", startDateDatePicker.SelectedDate?.ToString("dd/MM/yyyy"))));
            document.Add(new iTextSharp.text.Paragraph(string.Format("Fecha de fin de busqueda:---{0}", endDateDatePicker.SelectedDate?.ToString("dd/MM/yyyy"))));
            document.Add(new iTextSharp.text.Paragraph(string.Format("Elementos mostrados---------{0} articulos", setAsides.Count.ToString())));
            document.Add(new iTextSharp.text.Paragraph(string.Format("Fecha de creacion:----------{0}", DateTime.Now.ToString())));

            document.Add(Chunk.NEWLINE);
            document.Add(Chunk.NEWLINE);

            PdfPTable tableSetAsides = new PdfPTable(8);
            tableSetAsides.WidthPercentage = 95;

            PdfPCell cell1 = new PdfPCell(new Phrase("Numero de apartado", _standardFontBold));
            cell1.BorderWidth = 0;
            cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell1.BorderWidthBottom = 0.75f;

            PdfPCell cell2 = new PdfPCell(new Phrase("Fecha de apartado", _standardFontBold));
            cell2.BorderWidth = 0;
            cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell2.BorderWidthBottom = 0.75f;

            PdfPCell cell3 = new PdfPCell(new Phrase("Fecha de vencimiento", _standardFontBold));
            cell3.BorderWidth = 0;
            cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell3.BorderWidthBottom = 0.75f;

            PdfPCell cell4 = new PdfPCell(new Phrase("Monto total", _standardFontBold));
            cell4.BorderWidth = 0;
            cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell4.BorderWidthBottom = 0.75f;

            PdfPCell cell5 = new PdfPCell(new Phrase("Monto restante", _standardFontBold));
            cell5.BorderWidth = 0;
            cell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell5.BorderWidthBottom = 0.75f;

            PdfPCell cell6 = new PdfPCell(new Phrase("Porcentaje", _standardFontBold));
            cell6.BorderWidth = 0;
            cell6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell6.BorderWidthBottom = 0.75f;

            PdfPCell cell7 = new PdfPCell(new Phrase("Numero de cliente asociado", _standardFontBold));
            cell7.BorderWidth = 0;
            cell7.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell7.BorderWidthBottom = 0.75f;

            PdfPCell cell8 = new PdfPCell(new Phrase("Estado del apartado", _standardFontBold));
            cell8.BorderWidth = 0;
            cell8.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell8.BorderWidthBottom = 0.75f;

            tableSetAsides.AddCell(cell1);
            tableSetAsides.AddCell(cell2);
            tableSetAsides.AddCell(cell3);
            tableSetAsides.AddCell(cell4);
            tableSetAsides.AddCell(cell5);
            tableSetAsides.AddCell(cell6);
            tableSetAsides.AddCell(cell7);
            tableSetAsides.AddCell(cell8);

            foreach(SetAside setAside in setAsides)
            {
                PdfPCell cellIdSetAside = new PdfPCell(new Phrase(setAside.idSetAside.ToString()));
                cellIdSetAside.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellIdSetAside.BorderWidth = 0;

                PdfPCell cellCreationDate = new PdfPCell(new Phrase(setAside.creationDate.ToString()));
                cellCreationDate.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellCreationDate.BorderWidth = 0;

                PdfPCell cellEndDate = new PdfPCell(new Phrase(setAside.deadlineDate.ToString()));
                cellEndDate.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellEndDate.BorderWidth = 0;

                PdfPCell cellTotalAmount = new PdfPCell(new Phrase(setAside.totalAmount.ToString()));
                cellTotalAmount.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellTotalAmount.BorderWidth = 0;

                PdfPCell cellRemainingAmount = new PdfPCell(new Phrase(setAside.reaminingAmount.ToString()));
                cellRemainingAmount.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellRemainingAmount.BorderWidth = 0;

                PdfPCell cellPercentage = new PdfPCell(new Phrase(setAside.percentage));
                cellPercentage.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPercentage.BorderWidth = 0;

                PdfPCell cellIdCustomer = new PdfPCell(new Phrase(setAside.Customer_idCustomer.ToString()));
                cellIdCustomer.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellIdCustomer.BorderWidth = 0;

                PdfPCell cellState = new PdfPCell(new Phrase(setAside.stateAside));
                cellState.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellState.BorderWidth = 0;

                tableSetAsides.AddCell(cellIdSetAside);
                tableSetAsides.AddCell(cellCreationDate);
                tableSetAsides.AddCell(cellEndDate);
                tableSetAsides.AddCell(cellTotalAmount);
                tableSetAsides.AddCell(cellRemainingAmount);
                tableSetAsides.AddCell(cellPercentage);
                tableSetAsides.AddCell(cellIdCustomer);
                tableSetAsides.AddCell(cellState);
            }

            document.Add(tableSetAsides);
            document.Close();
            pdfWriter.Close();
            return fullPath;
        }
    }
}
