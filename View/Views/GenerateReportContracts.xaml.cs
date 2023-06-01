using BusinessLogic;
using Domain;
using iTextSharp.text.pdf;
using iTextSharp.text;
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
using DataAcces;

namespace View.Views
{
    /// <summary>
    /// Lógica de interacción para GenerateReportContracts.xaml
    /// </summary>
    public partial class GenerateReportContracts : Page
    {
        List<ContractDomain> contracts;

        public GenerateReportContracts()
        {
            InitializeComponent();
        }

        private void ClosePage_btn(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }
        
        private void Btn_CreateReport(object sender, RoutedEventArgs e)
        {

            if (ValidateDates())
            {
                try
                {
                    contracts = ContractDAO.GetContractsByDate(DateTime.Parse(dpDateBegin.Text), DateTime.Parse(dpDateEnd.Text));
                    if (contracts != null)
                    {
                        GenerateReportContact();
                    }
                    else
                    {
                        MessageBox.Show("No se han encontrado contratos registrados en las fechas ingresadas");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al conectarse a la base de datos, favor de intentarlo más tarde");
                }
                
            }
        }

        private bool ValidateDates()
        {
            bool result = true;
            if (!dpDateBegin.SelectedDate.HasValue || !dpDateEnd.SelectedDate.HasValue)
            {
                result = false;
                ErrorManager.ShowError("Favor de seleccionar una fecha de inicio y una fecha de fin para realizar el reporte");
            }
            else if (DateTime.Parse(dpDateEnd.Text) <= DateTime.Parse(dpDateBegin.Text))
            {
                result = false;
                ErrorManager.ShowError("La fecha de filtro minima no puede ser superior al la fecha de filtro superior.Favor de verificar");
            }

            return result;
        }

        private void GenerateReportContact()
        {
            string date = DateTime.Now.Ticks.ToString();
            string pdfName = string.Format("ContractReport-{0}.pdf", date);
            string pathBase = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = string.Format("{0}\\pdfs", pathBase);
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
            string fullPath = string.Format("{0}\\{1}", path, pdfName);
            Document doc = new Document(PageSize.LETTER);
            PdfWriter writer = PdfWriter.GetInstance(
                doc, new FileStream(fullPath, FileMode.Create));
            doc.AddTitle(string.Format("ContractReport{0}", DateTime.Now.Ticks));
            doc.Open();
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Empeño Facil", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);
            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);
            Font _standardFontBold = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD, BaseColor.BLACK);

            doc.Add(new iTextSharp.text.Paragraph("Reporte de contratos", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD)));
            doc.Add(new iTextSharp.text.Paragraph("-----------------------------------------------------------------------------------------------------"));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Fecha de filtro menor:------{0}", dpDateBegin.Text)));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Fecha de filtro superior:---{0}", dpDateEnd.Text)));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Elementos mostrados---------{0} contratos", contracts.Count().ToString())));
            var staff = (App.Current as App)._staffInfo;
            doc.Add(new iTextSharp.text.Paragraph("-----------------------------------------------------------------------------------------------------"));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Fecha de creacion:----------{0}", DateTime.Now.ToString("dd/MM/yyyy"))));
            doc.Add(new iTextSharp.text.Paragraph(string.Format("Gerente en turno:-----------{0} {1}", staff.fisrtName, staff.lastName)));

            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);

            PdfPTable tbl_InfoContracts = new PdfPTable(7);
            tbl_InfoContracts.WidthPercentage = 100;

            PdfPCell c01 = new PdfPCell(new Phrase("Id Contrato", _standardFontBold));
            c01.BorderWidth = 0;
            c01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c01.BorderWidthBottom = 0.75f;

            PdfPCell c02 = new PdfPCell(new Phrase("Nombre Cliente", _standardFontBold));
            c02.BorderWidth = 0;
            c02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c02.BorderWidthBottom = 0.75f;

            PdfPCell c03 = new PdfPCell(new Phrase("CURP", _standardFontBold));
            c03.BorderWidth = 0;
            c03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c03.BorderWidthBottom = 0.75f;

            PdfPCell c04 = new PdfPCell(new Phrase("Fecha de Creación", _standardFontBold));
            c04.BorderWidth = 0;
            c04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c04.BorderWidthBottom = 0.75f;

            PdfPCell c05 = new PdfPCell(new Phrase("Fecha de Termino", _standardFontBold));
            c05.BorderWidth = 0;
            c05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c05.BorderWidthBottom = 0.75f;

            PdfPCell c06 = new PdfPCell(new Phrase("Prendas", _standardFontBold));
            c06.BorderWidth = 0;
            c06.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c06.BorderWidthBottom = 0.75f;

            PdfPCell c07 = new PdfPCell(new Phrase("Monto del Préstamo", _standardFontBold));
            c07.BorderWidth = 0;
            c07.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c07.BorderWidthBottom = 0.75f;

            tbl_InfoContracts.AddCell(c01);
            tbl_InfoContracts.AddCell(c02);
            tbl_InfoContracts.AddCell(c03);
            tbl_InfoContracts.AddCell(c04);
            tbl_InfoContracts.AddCell(c05);
            tbl_InfoContracts.AddCell(c06);
            tbl_InfoContracts.AddCell(c07);

            foreach (ContractDomain newcontract in contracts)
            {
                string listBelondings = "";
                
                PdfPCell d01 = new PdfPCell(new Phrase(""+newcontract.idContract, _standardFont));
                d01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                d01.BorderWidth = 0;

                PdfPCell d02 = new PdfPCell(new Phrase(newcontract.Customer.firstName+" "+newcontract.Customer.lastName, _standardFont));
                d02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                d02.BorderWidth = 0;

                PdfPCell d03 = new PdfPCell(new Phrase(newcontract.Customer.curp, _standardFont));
                d03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                d03.BorderWidth = 0;

                PdfPCell d04 = new PdfPCell(new Phrase(newcontract.creationDate.ToString(), _standardFont));
                d04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                d04.BorderWidth = 0;

                PdfPCell d05 = new PdfPCell(new Phrase(newcontract.deadlineDate.ToString(), _standardFont));
                d05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                d05.BorderWidth = 0;

                foreach(Belonging belonging in newcontract.Belongings)
                {
                    listBelondings += belonging.description + "\n";
                }

                PdfPCell d06 = new PdfPCell(new Phrase(listBelondings, _standardFont));
                d06.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                d06.BorderWidth = 0;

                PdfPCell d07 = new PdfPCell(new Phrase(newcontract.loanAmount.ToString(), _standardFont));
                d05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                d05.BorderWidth = 0;

                tbl_InfoContracts.AddCell(d01);
                tbl_InfoContracts.AddCell(d02);
                tbl_InfoContracts.AddCell(d03);
                tbl_InfoContracts.AddCell(d04);
                tbl_InfoContracts.AddCell(d05);
                tbl_InfoContracts.AddCell(d06);
                tbl_InfoContracts.AddCell(d07);
            }
            doc.Add(tbl_InfoContracts);
            doc.Close();
            writer.Close();
            System.Diagnostics.Process.Start(fullPath);
        }


    }
}
