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
using Document = iTextSharp.text.Document;
using View.Properties;
using DataAcces;

namespace View.Views
{
    public class CreateTickets
    {

        public static void TicketProfitCustomer(int idCustomer, string articleCustomer)
        {
            DataAcces.Customer customer = CustomerDAO.GetCustomer(idCustomer);
            //List<ArticleDomain> articles = ArticleDAO.getArticlesById(idArticles);
            float ticketWidth = 80f;  // Ancho del ticket en mm
            float ticketHeight = 150f;
            float ticketWidthPoints = ticketWidth * 2.83465f;
            float ticketHeightPoints = ticketHeight * 2.83465f;
            string date = DateTime.Now.Ticks.ToString();
            string pdfName = string.Format("TicketProfitCustomer-{0}.pdf", date);
            string pathBase = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = string.Format("{0}\\pdfs", pathBase);
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
            string fullPath = string.Format("{0}\\{1}", path, pdfName);
            Document doc = new Document(new Rectangle(ticketWidthPoints, ticketHeightPoints));
            PdfWriter writer = PdfWriter.GetInstance(
                doc, new FileStream(fullPath, FileMode.Create));
            doc.AddTitle(string.Format("TicketProfitCustomer{0}", DateTime.Now.Ticks));
            doc.Open();
            //editando encabezados
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Empeño Facil", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);
            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 4, Font.NORMAL, BaseColor.BLACK);
            Font _standardFontBold = new Font(Font.FontFamily.HELVETICA, 4, Font.BOLD, BaseColor.BLACK);

            // doc.Add( new iTextSharp.text.Paragraph("Av. Manuel Avila Camacho 291 a Esq,\n Jose Azueta, Jose Cardel,\n91030 Xalapa-Enriquez,Ver.", new Font(Font.FontFamily.HELVETICA, 8)));
            Paragraph paragraph = new Paragraph("Av. Manuel Avila Camacho 291 a Esq,\n Jose Azueta, Jose Cardel,\n91030 Xalapa-Enriquez,Ver.", new Font(Font.FontFamily.HELVETICA, 9));
            paragraph.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph);

            Paragraph spaceInfo = new Paragraph("----------------------------------------------------------", new Font(Font.FontFamily.HELVETICA, 8));
            spaceInfo.Alignment = Element.ALIGN_CENTER;
            doc.Add(spaceInfo);

            Paragraph typeOp = new Paragraph("COMPROBANTE DE GANANCIA\n*INFORMACION CLIENTE*", new Font(Font.FontFamily.HELVETICA, 8));
            typeOp.Alignment = Element.ALIGN_CENTER;
            doc.Add(typeOp);

            Paragraph name = new Paragraph("NOMBRE      :" + customer.firstName.ToUpper() + " " + customer.lastName.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8));
            //name.Alignment = Element.ALIGN_CENTER;
            doc.Add(name);

            Paragraph curp = new Paragraph("CURP           :" + customer.curp.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8));
            //curp.Alignment = Element.ALIGN_CENTER;
            doc.Add(curp);

            doc.Add(spaceInfo);

            Paragraph infoArticle = new Paragraph("*INFORMACION ARTICULOS*", new Font(Font.FontFamily.HELVETICA, 8));
            infoArticle.Alignment = Element.ALIGN_CENTER;
            doc.Add(infoArticle);

            Paragraph articles = new Paragraph(articleCustomer.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(articles);

            doc.Add(spaceInfo);

            Paragraph dateInfo = new Paragraph("FECHA              : " + DateTime.Now.ToString("dd/MM/yyyy"), new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(dateInfo);

            Paragraph total = new Paragraph("TOTAL              : $" + customer.cumulativeProfit, new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(total);

            doc.Close();
            writer.Close();
            System.Diagnostics.Process.Start(fullPath);
        }


        public void TicketSales(int idCustomer, int idSale, List<int> idArticles)
        {
            int resultArticle = 0;
            int resultSale = 0;

            List<ArticleDomain> articles = new List<ArticleDomain>();
            DataAcces.Sale sale = new DataAcces.Sale();
            DataAcces.Customer customer = CustomerDAO.GetCustomer(idCustomer);
            (resultArticle, articles) = ArticleDAO.GetArticlesListById(idArticles);
            (resultSale, sale) = SaleDAO.getSaleById(idSale);
            if (resultArticle == MessageCode.SUCCESS && resultSale == MessageCode.SUCCESS)
            {
                CreateSaleTicket(articles, sale, customer);
            }
            else
                ErrorManager.ShowError(MessageError.CONNECTION_ERROR);
        }
        
        private void CreateSaleTicket(List<ArticleDomain> articles, Sale sale, DataAcces.Customer customer)
        {
            float ticketWidth = 80f;  // Ancho del ticket en mm
            float ticketHeight = 150f;
            float ticketWidthPoints = ticketWidth * 2.83465f;
            float ticketHeightPoints = ticketHeight * 2.83465f;
            string date = DateTime.Now.Ticks.ToString();
            string pdfName = string.Format("TicketSale-{0}.pdf", date);
            string pathBase = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = string.Format("{0}\\pdfs", pathBase);
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
            string fullPath = string.Format("{0}\\{1}", path, pdfName);
            Document doc = new Document(new Rectangle(ticketWidthPoints, ticketHeightPoints));
            PdfWriter writer = PdfWriter.GetInstance(
                doc, new FileStream(fullPath, FileMode.Create));
            doc.AddTitle(string.Format("TicketSale{0}", DateTime.Now.Ticks));
            doc.Open();
            //editando encabezados
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Empeño Facil", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);
            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 4, Font.NORMAL, BaseColor.BLACK);
            Font _standardFontBold = new Font(Font.FontFamily.HELVETICA, 4, Font.BOLD, BaseColor.BLACK);

            // doc.Add( new iTextSharp.text.Paragraph("Av. Manuel Avila Camacho 291 a Esq,\n Jose Azueta, Jose Cardel,\n91030 Xalapa-Enriquez,Ver.", new Font(Font.FontFamily.HELVETICA, 8)));
            Paragraph paragraph = new Paragraph("Av. Manuel Avila Camacho 291 a Esq,\n Jose Azueta, Jose Cardel,\n91030 Xalapa-Enriquez,Ver.", new Font(Font.FontFamily.HELVETICA, 9));
            paragraph.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph);

            Paragraph spaceInfo = new Paragraph("----------------------------------------------------------", new Font(Font.FontFamily.HELVETICA, 8));
            spaceInfo.Alignment = Element.ALIGN_CENTER;
            doc.Add(spaceInfo);

            Paragraph typeOp = new Paragraph("COMPROBANTE DE VENTA\n*INFORMACION CLIENTE*", new Font(Font.FontFamily.HELVETICA, 8));
            typeOp.Alignment = Element.ALIGN_CENTER;
            doc.Add(typeOp);

            Paragraph name = new Paragraph("NOMBRE      :" + customer.firstName.ToUpper() + " " + customer.lastName.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8));
            //name.Alignment = Element.ALIGN_CENTER;
            doc.Add(name);

            Paragraph curp = new Paragraph("CURP           :" + customer.curp.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8));
            //curp.Alignment = Element.ALIGN_CENTER;
            doc.Add(curp);

            doc.Add(spaceInfo);

            Paragraph infoArticle = new Paragraph("*INFORMACION ARTICULOS*", new Font(Font.FontFamily.HELVETICA, 8));
            infoArticle.Alignment = Element.ALIGN_CENTER;
            doc.Add(infoArticle);

            foreach (var util in articles)
            {
                Paragraph articleName = new Paragraph("- " + util.description.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8));
                // infoArticle.Alignment = Element.ALIGN_CENTER;
                doc.Add(articleName);
            }

            doc.Add(spaceInfo);

            Paragraph dateInfo = new Paragraph("FECHA              : " + DateTime.Now.ToString("dd/MM/yyyy"), new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(dateInfo);

            Paragraph subtotal = new Paragraph("SUBTOTAL           : $" + sale.total.ToString("0.00"), new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(subtotal);

            Paragraph total = new Paragraph("TOTAL              : $" + sale.subtotal.ToString("0.00"), new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(total);

            doc.Close();
            writer.Close();
            System.Diagnostics.Process.Start(fullPath);

        }

        public static void TicketSetAside(int idCustomer, int idSetAside, List<int> idArticles)
        {
            int resultArticles = 0;
            int resultSetAside = 0;
            List<ArticleDomain> articles = new List<ArticleDomain>();
            SetAside setAside = new SetAside();
            DataAcces.Customer customer = CustomerDAO.GetCustomer(idCustomer);
            (resultArticles, articles) = ArticleDAO.GetArticlesListById(idArticles);
            (resultSetAside, setAside) = SetAsideDAO.GetAsideById(idSetAside);
            if (resultArticles == MessageCode.SUCCESS && resultSetAside == MessageCode.SUCCESS)
                CreateSetAsideTicket(articles, setAside, customer);

        }

        private static void CreateSetAsideTicket(List<ArticleDomain> articles, SetAside setAside, DataAcces.Customer customer)
        {
            float ticketWidth = 80f;  // Ancho del ticket en mm
            float ticketHeight = 150f;
            float ticketWidthPoints = ticketWidth * 2.83465f;
            float ticketHeightPoints = ticketHeight * 2.83465f;
            string date = DateTime.Now.Ticks.ToString();
            string pdfName = string.Format("TicketSetAside-{0}.pdf", date);
            string pathBase = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = string.Format("{0}\\pdfs", pathBase);
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
            string fullPath = string.Format("{0}\\{1}", path, pdfName);
            Document doc = new Document(new Rectangle(ticketWidthPoints, ticketHeightPoints));
            PdfWriter writer = PdfWriter.GetInstance(
                doc, new FileStream(fullPath, FileMode.Create));
            doc.AddTitle(string.Format("TicketSetAside{0}", DateTime.Now.Ticks));
            doc.Open();
            //editando encabezados
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Empeño Facil", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);
            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 4, Font.NORMAL, BaseColor.BLACK);
            Font _standardFontBold = new Font(Font.FontFamily.HELVETICA, 4, Font.BOLD, BaseColor.BLACK);

            // doc.Add( new iTextSharp.text.Paragraph("Av. Manuel Avila Camacho 291 a Esq,\n Jose Azueta, Jose Cardel,\n91030 Xalapa-Enriquez,Ver.", new Font(Font.FontFamily.HELVETICA, 8)));
            Paragraph paragraph = new Paragraph("Av. Manuel Avila Camacho 291 a Esq,\n Jose Azueta, Jose Cardel,\n91030 Xalapa-Enriquez,Ver.", new Font(Font.FontFamily.HELVETICA, 9));
            paragraph.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph);

            Paragraph spaceInfo = new Paragraph("----------------------------------------------------------", new Font(Font.FontFamily.HELVETICA, 8));
            spaceInfo.Alignment = Element.ALIGN_CENTER;
            doc.Add(spaceInfo);

            if (setAside.reaminingAmount > 0)
            {
                Paragraph typeOp = new Paragraph("COMPROBANTE DE APARTADO\n*INFORMACION CLIENTE*", new Font(Font.FontFamily.HELVETICA, 8));
                typeOp.Alignment = Element.ALIGN_CENTER;
                doc.Add(typeOp);
            }
            else
            {
                Paragraph typeOp = new Paragraph("COMPROBANTE DE LIQUIDACION DE APARTADO\n*INFORMACION CLIENTE*", new Font(Font.FontFamily.HELVETICA, 8));
                typeOp.Alignment = Element.ALIGN_CENTER;
                doc.Add(typeOp);
            }


            Paragraph name = new Paragraph("NOMBRE      :" + customer.firstName.ToUpper() + " " + customer.lastName.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8));
            //name.Alignment = Element.ALIGN_CENTER;
            doc.Add(name);

            Paragraph curp = new Paragraph("CURP           :" + customer.curp.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8));
            //curp.Alignment = Element.ALIGN_CENTER;
            doc.Add(curp);

            doc.Add(spaceInfo);

            Paragraph infoArticle = new Paragraph("*INFORMACION ARTICULOS*", new Font(Font.FontFamily.HELVETICA, 8));
            infoArticle.Alignment = Element.ALIGN_CENTER;
            doc.Add(infoArticle);
            Console.WriteLine("cantidad de articulos: " + articles.Count);
            foreach (var util in articles)
            {

                Paragraph articleName = new Paragraph("- " + util.description.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8));
                // infoArticle.Alignment = Element.ALIGN_CENTER;
                doc.Add(articleName);
            }

            doc.Add(spaceInfo);

            Paragraph dateInfo = new Paragraph("FECHA              : " + DateTime.Now.ToString("dd/MM/yyyy"), new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(dateInfo);


            Paragraph dateCreateSetAside = new Paragraph("FECHA DE APARTADO  : " + setAside.creationDate.ToString("dd/MM/yyyy"), new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(dateCreateSetAside);

            Paragraph dateDeadline = new Paragraph("FECHA LIMITE       : " + setAside.deadlineDate.ToString("dd/MM/yyyy"), new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(dateDeadline);

            Paragraph total = new Paragraph("TOTAL              : $" + setAside.totalAmount.ToString("0.00"), new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(total);

            Paragraph remaining = new Paragraph("RESTANTE           : $" + setAside.reaminingAmount.ToString("0.00"), new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(remaining);

            double payAmount = setAside.totalAmount - setAside.reaminingAmount; 
            Paragraph pay = new Paragraph("ANTICIPO           : $" + payAmount.ToString("0.00"), new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(pay);

            doc.Close();
            writer.Close();
            System.Diagnostics.Process.Start(fullPath);
        }

        public static  async void TicketLiquidateContract(int idContract, double payAmount)
        {
            Console.WriteLine("ticket");
            ContractDomain contract =  await  ContractDAO.GetContractsDomainAsync(idContract);
            if(contract != null)
            {
                CreateLiquisateContractTicket(contract, payAmount);
            }
          
        }

        private static void CreateLiquisateContractTicket(ContractDomain contract, double payAmount)
        {
            float ticketWidth = 80f;  // Ancho del ticket en mm
            float ticketHeight = 150f;
            float ticketWidthPoints = ticketWidth * 2.83465f;
            float ticketHeightPoints = ticketHeight * 2.83465f;
            string date = DateTime.Now.Ticks.ToString();
            string pdfName = string.Format("TicketLiquidateContract-{0}.pdf", date);
            string pathBase = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = string.Format("{0}\\pdfs", pathBase);
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
            string fullPath = string.Format("{0}\\{1}", path, pdfName);
            Document doc = new Document(new Rectangle(ticketWidthPoints, ticketHeightPoints));
            PdfWriter writer = PdfWriter.GetInstance(
                doc, new FileStream(fullPath, FileMode.Create));
            doc.AddTitle(string.Format("TicketLiquidateCOntract{0}", DateTime.Now.Ticks));
            doc.Open();
            //editando encabezados
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Empeño Facil", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);
            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 4, Font.NORMAL, BaseColor.BLACK);
            Font _standardFontBold = new Font(Font.FontFamily.HELVETICA, 4, Font.BOLD, BaseColor.BLACK);

            // doc.Add( new iTextSharp.text.Paragraph("Av. Manuel Avila Camacho 291 a Esq,\n Jose Azueta, Jose Cardel,\n91030 Xalapa-Enriquez,Ver.", new Font(Font.FontFamily.HELVETICA, 8)));
            Paragraph paragraph = new Paragraph("Av. Manuel Avila Camacho 291 a Esq,\n Jose Azueta, Jose Cardel,\n91030 Xalapa-Enriquez,Ver.", new Font(Font.FontFamily.HELVETICA, 9));
            paragraph.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph);

            Paragraph spaceInfo = new Paragraph("----------------------------------------------------------", new Font(Font.FontFamily.HELVETICA, 8));
            spaceInfo.Alignment = Element.ALIGN_CENTER;
            doc.Add(spaceInfo);

            Paragraph typeOp = new Paragraph("COMPROBANTE DE LIQUIDACION DE PRESTAMO\n*INFORMACION CLIENTE*", new Font(Font.FontFamily.HELVETICA, 8));
            typeOp.Alignment = Element.ALIGN_CENTER;
            doc.Add(typeOp);

            Paragraph name = new Paragraph("NOMBRE      :" + contract.Customer.firstName.ToUpper() + " " + contract.Customer.lastName.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8));
            //name.Alignment = Element.ALIGN_CENTER;
            doc.Add(name);

            Paragraph curp = new Paragraph("CURP           :" + contract.Customer.curp.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8));
            //curp.Alignment = Element.ALIGN_CENTER;
            doc.Add(curp);

            doc.Add(spaceInfo);

            Paragraph infoArticle = new Paragraph("*INFORMACION ARTICULOS*", new Font(Font.FontFamily.HELVETICA, 8));
            infoArticle.Alignment = Element.ALIGN_CENTER;
            doc.Add(infoArticle);

            foreach (var util in contract.Belongings)
            {
                Paragraph articleName = new Paragraph("- " + util.description.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8));
                // infoArticle.Alignment = Element.ALIGN_CENTER;
                doc.Add(articleName);
            }

            doc.Add(spaceInfo);

            Paragraph dateInfo = new Paragraph("FECHA              : " + DateTime.Now.ToString("dd/MM/yyyy"), new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(dateInfo);

            Paragraph idContract = new Paragraph("NUMERO DE CONTRATO : " + contract.idContract, new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(idContract);

            Paragraph pay = new Paragraph("PAGO                : " + payAmount.ToString("0.00"), new Font(Font.FontFamily.HELVETICA, 8));
            // articles.Alignment = Element.ALIGN_CENTER;
            doc.Add(pay);
            doc.Close();
            writer.Close();
            System.Diagnostics.Process.Start(fullPath);

        }
    }
}
