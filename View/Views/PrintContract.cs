using DataAcces;
using Domain;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Document = iTextSharp.text.Document;

namespace View.Views
{
    public class PrintContract
    {
        public static void NewPrintCotract(ContractDomain contract)
        {
            string date = DateTime.Now.Ticks.ToString();
            string pdfName = string.Format("ContractPawn-{0}.pdf", date);
            string pathBase = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = string.Format("{0}\\pdfs", pathBase);
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
            string fullPath = string.Format("{0}\\{1}", path, pdfName);
            Document doc = new Document(PageSize.LETTER);
            PdfWriter writer = PdfWriter.GetInstance(
                doc, new FileStream(fullPath, FileMode.Create));
            doc.AddTitle(string.Format("ContractPawn{0}", DateTime.Now.Ticks));
            doc.Open();
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Empeño Facil", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);
            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);
            Font _standardFontBold = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD, BaseColor.BLACK);

            Paragraph paragraph = new Paragraph("FOLIO No." + contract.idContract, new Font(Font.FontFamily.HELVETICA, 9));
            paragraph.Alignment = Element.ALIGN_RIGHT;
            doc.Add(paragraph);

            Paragraph dateContract = new Paragraph("Fecha de la celebración del contrato: " + contract.creationDate, new Font(Font.FontFamily.HELVETICA, 9));
            dateContract.Alignment = Element.ALIGN_LEFT;
            doc.Add(dateContract);

            doc.Add(Chunk.NEWLINE);

            PdfPTable tbl_InfoContract = new PdfPTable(4);
            tbl_InfoContract.WidthPercentage = 90;

            PdfPCell c01 = new PdfPCell(new Phrase("Costo Anual Total", _standardFontBold));
            c01.BorderWidth = 0;
            c01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c01.BorderWidthBottom = 0.75f;

            PdfPCell c02 = new PdfPCell(new Phrase("Tasa de Interes Anual", _standardFontBold));
            c02.BorderWidth = 0;
            c02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c02.BorderWidthBottom = 0.75f;

            PdfPCell c03 = new PdfPCell(new Phrase("Monto del Préstamo", _standardFontBold));
            c03.BorderWidth = 0;
            c03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c03.BorderWidthBottom = 0.75f;

            PdfPCell c04 = new PdfPCell(new Phrase("Monto Total a Pagar", _standardFontBold));
            c04.BorderWidth = 0;
            c04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            c04.BorderWidthBottom = 0.75f;

            tbl_InfoContract.AddCell(c01);
            tbl_InfoContract.AddCell(c02);
            tbl_InfoContract.AddCell(c03);
            tbl_InfoContract.AddCell(c04);

            PdfPCell d01 = new PdfPCell(new Phrase(contract.totalAnnualCost, _standardFont));
            d01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            d01.BorderWidth = 0;

            PdfPCell d02 = new PdfPCell(new Phrase(contract.annualInterestRate, _standardFont));
            d02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            d02.BorderWidth = 0;

            PdfPCell d03 = new PdfPCell(new Phrase(""+contract.settlementAmount, _standardFont));
            d03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            d03.BorderWidth = 0;

            PdfPCell d04 = new PdfPCell(new Phrase(contract.paymentsEndorsement, _standardFont));
            d04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            d04.BorderWidth = 0;

            tbl_InfoContract.AddCell(d01);
            tbl_InfoContract.AddCell(d02);
            tbl_InfoContract.AddCell(d03);
            tbl_InfoContract.AddCell(d04);

            doc.Add(tbl_InfoContract);

            doc.Add(Chunk.NEWLINE);

            Paragraph loanTerm = new Paragraph("Plazo del préstamo (Fecha límite para el refrendo o desempeño): " + contract.deadlineDate.AddDays(15), new Font(Font.FontFamily.HELVETICA, 9));
            loanTerm.Alignment = Element.ALIGN_LEFT;
            doc.Add(loanTerm);

            Paragraph endorsements = new Paragraph("Total de Refrendos aplicables: 1", new Font(Font.FontFamily.HELVETICA, 9));
            endorsements.Alignment = Element.ALIGN_LEFT;
            doc.Add(endorsements);

            doc.Add(Chunk.NEWLINE);

            PdfPTable tbl_InfoPayment = new PdfPTable(6);
            tbl_InfoPayment.WidthPercentage = 90;

            PdfPCell p01 = new PdfPCell(new Phrase("Importe del Mutuo", _standardFontBold));
            p01.BorderWidth = 0;
            p01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            p01.BorderWidthBottom = 0.75f;

            PdfPCell p02 = new PdfPCell(new Phrase("Intereses", _standardFontBold));
            p02.BorderWidth = 0;
            p02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            p02.BorderWidthBottom = 0.75f;

            PdfPCell p03 = new PdfPCell(new Phrase("IVA", _standardFontBold));
            p03.BorderWidth = 0;
            p03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            p03.BorderWidthBottom = 0.75f;

            PdfPCell p04 = new PdfPCell(new Phrase("Total a Pagar por Refrendo", _standardFontBold));
            p04.BorderWidth = 0;
            p04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            p04.BorderWidthBottom = 0.75f;

            PdfPCell p05 = new PdfPCell(new Phrase("Total a Pagar por Desempeño", _standardFontBold));
            p05.BorderWidth = 0;
            p05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            p05.BorderWidthBottom = 0.75f;

            PdfPCell p06 = new PdfPCell(new Phrase("Cúando se realizan los pagos", _standardFontBold));
            p06.BorderWidth = 0;
            p06.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            p06.BorderWidthBottom = 0.75f;

            tbl_InfoPayment.AddCell(p01);
            tbl_InfoPayment.AddCell(p02);
            tbl_InfoPayment.AddCell(p03);
            tbl_InfoPayment.AddCell(p04);
            tbl_InfoPayment.AddCell(p05);
            tbl_InfoPayment.AddCell(p06);

            double appraisalAmount = 0;
            contract.Belongings.ForEach(belondings => appraisalAmount += belondings.appraisalValue);

            PdfPCell dp01 = new PdfPCell(new Phrase(""+appraisalAmount, _standardFont));
            dp01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            dp01.BorderWidth = 0;

            PdfPCell dp02 = new PdfPCell(new Phrase(""+contract.interestRate, _standardFont));
            dp02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            dp02.BorderWidth = 0;

            PdfPCell dp03 = new PdfPCell(new Phrase("" + contract.iva, _standardFont));
            dp03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            dp03.BorderWidth = 0;

            PdfPCell dp04 = new PdfPCell(new Phrase("" + contract.paymentsEndorsement, _standardFont));
            dp04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            dp04.BorderWidth = 0;

            PdfPCell dp05 = new PdfPCell(new Phrase("" + contract.paymentsSettlement, _standardFont));
            dp05.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            dp05.BorderWidth = 0;

            PdfPCell dp06 = new PdfPCell(new Phrase(contract.endorsementSettlementDates, _standardFont));
            dp06.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            dp06.BorderWidth = 0;

            tbl_InfoPayment.AddCell(dp01);
            tbl_InfoPayment.AddCell(dp02);
            tbl_InfoPayment.AddCell(dp03);
            tbl_InfoPayment.AddCell(dp04);
            tbl_InfoPayment.AddCell(dp05);
            tbl_InfoPayment.AddCell(dp06);

            doc.Add(tbl_InfoPayment);

            doc.Add(Chunk.NEWLINE);

            PdfPTable tbl_Belondings = new PdfPTable(4);
            tbl_Belondings.WidthPercentage = 90;

            PdfPCell b01 = new PdfPCell(new Phrase("Descripción", _standardFontBold));
            b01.BorderWidth = 0;
            b01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            b01.BorderWidthBottom = 0.75f;

            PdfPCell b02 = new PdfPCell(new Phrase("Caracteristicas", _standardFontBold));
            b02.BorderWidth = 0;
            b02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            b02.BorderWidthBottom = 0.75f;

            PdfPCell b03 = new PdfPCell(new Phrase("Avaluo", _standardFontBold));
            b03.BorderWidth = 0;
            b03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            b03.BorderWidthBottom = 0.75f;

            PdfPCell b04 = new PdfPCell(new Phrase("Préstamo", _standardFontBold));
            b04.BorderWidth = 0;
            b04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            b04.BorderWidthBottom = 0.75f;

            tbl_Belondings.AddCell(b01);
            tbl_Belondings.AddCell(b02);
            tbl_Belondings.AddCell(b03);
            tbl_Belondings.AddCell(b04);


            foreach(Belonging belonging in contract.Belongings)
            {
                PdfPCell bp01 = new PdfPCell(new Phrase(belonging.description, _standardFont));
                bp01.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                dp01.BorderWidth = 0;

                PdfPCell bp02 = new PdfPCell(new Phrase(belonging.characteristics, _standardFont));
                bp02.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                bp02.BorderWidth = 0;

                PdfPCell bp03 = new PdfPCell(new Phrase(""+belonging.appraisalValue, _standardFont));
                bp03.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                bp03.BorderWidth = 0;

                PdfPCell bp04 = new PdfPCell(new Phrase(""+belonging.loanAmount, _standardFont));
                bp04.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                bp04.BorderWidth = 0;

                tbl_Belondings.AddCell(bp01);
                tbl_Belondings.AddCell(bp02);
                tbl_Belondings.AddCell(bp03);
                tbl_Belondings.AddCell(bp04);
            }
            doc.Add(tbl_Belondings);
            doc.Add(Chunk.NEWLINE);

            Paragraph appraisalValue = new Paragraph("Monto de avalúo: $"+appraisalAmount, new Font(Font.FontFamily.HELVETICA, 9));
            appraisalValue.Alignment = Element.ALIGN_LEFT;
            doc.Add(appraisalValue);

            Paragraph porcentaje = new Paragraph("Porcentaje del préstamo sobre el avalúo: " + contract.loanProcentage+"%", new Font(Font.FontFamily.HELVETICA, 9));
            porcentaje.Alignment = Element.ALIGN_LEFT;
            doc.Add(porcentaje);

            Paragraph deadLine = new Paragraph("Fecha limite del finiquito: " + contract.deadlineDate, new Font(Font.FontFamily.HELVETICA, 9));
            deadLine.Alignment = Element.ALIGN_LEFT;
            doc.Add(deadLine);

            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);

            Paragraph DateContrac = new Paragraph("Fecha: " + contract.creationDate, new Font(Font.FontFamily.HELVETICA, 9));
            DateContrac.Alignment = Element.ALIGN_LEFT;
            doc.Add(DateContrac);

            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);


            Paragraph separation = new Paragraph("______________________________________", new Font(Font.FontFamily.HELVETICA, 9));
            separation.Alignment = Element.ALIGN_CENTER;
            doc.Add(separation);

            Paragraph Customer = new Paragraph("Firma del Consumidor" , new Font(Font.FontFamily.HELVETICA, 9));
            Customer.Alignment = Element.ALIGN_CENTER;
            doc.Add(Customer);

            doc.Close();
            writer.Close();

            System.Diagnostics.Process.Start(fullPath);
        }

    }
}
