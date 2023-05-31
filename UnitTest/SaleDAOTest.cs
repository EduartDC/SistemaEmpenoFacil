using BusinessLogic;
using DataAcces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace UnitTest
{
    [TestClass]
    public class SaleDAOTest
    {


        //CU imprimir tickets
        //Jonathan
        [TestMethod]
        public void TestGetSaleByID()
        {
            int id = 1;
            int result = MessageCode.SUCCESS;
            Sale sale = new Sale();
            Assert.AreEqual((result, sale), SaleDAO.getSaleById(id));
        }

        //CU imprimir tickets
        //Jonathan
        [TestMethod]
        public void TestGetSaleByIdErrorConnection()
        {
            int id = 1;
            int result = MessageCode.CONNECTION_ERROR;
            Sale sale = new Sale();
            Assert.AreEqual((result, sale), SaleDAO.getSaleById(id));
        }

        [TestMethod]
        public void TestGetSalesByDate()
        {
            int expectedResult = 1;
            DateTime dateTime = new DateTime(2023,5,8);
            List<Sale> sales = SaleDAO.GetSales(dateTime);
            Assert.AreEqual(expectedResult, sales.Count);
        }

        [TestMethod]
        public void TestGetSalesByDateFailed()
        {
            int expectedResult = 0;
            DateTime dateTime = new DateTime(2023, 4, 10);
            List<Sale> sales = SaleDAO.GetSales(dateTime);
            Assert.AreEqual(expectedResult, sales.Count);
        }

        [TestMethod]
        public void TestGetSaleByCode()
        {
            int expectedResult = 1;
            int saleCode = 1;
            Sale sale = SaleDAO.GetSaleBySaleCode(saleCode);
            Assert.AreEqual(expectedResult, sale.idSale);
        }

        [TestMethod]
        public void TestGetSaleByCodeFailed()
        {
            int saleCode = 0;
            Sale sale = SaleDAO.GetSaleBySaleCode(saleCode);
            Assert.AreEqual(null, sale);
        }

        [TestMethod]
        public void TestGetSaleById()
        {
            int expectedResult = 1;
            int saleCode = 1;
            int responseCode;
            Sale sale = new Sale();
            (responseCode, sale) = SaleDAO.getSaleById(saleCode);
            Assert.AreEqual(expectedResult, sale.idSale);
        }

        [TestMethod]
        public void TestGetSaleByIdFailed()
        {
            int saleCode = 0;
            int responseCode;
            Sale sale = new Sale();
            (responseCode, sale) = SaleDAO.getSaleById(saleCode);
            Assert.AreEqual(null, sale);
        }

        [TestMethod]
        public void TestMakeSale()
        {
            int expectedResult = 13;
            Sale sale = new Sale();
            sale.total = 5000;
            sale.subtotal = 5000;
            sale.saleDate = DateTime.Now;
            sale.discount = "0";
            sale.Customer_idCustomer = 24;
            int responseCode, idSale;
            (responseCode, idSale) = SaleDAO.MakeSale(sale);
            Assert.AreEqual(expectedResult, idSale);
        }
    }
}
