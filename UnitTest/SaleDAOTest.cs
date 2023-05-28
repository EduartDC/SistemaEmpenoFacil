using BusinessLogic;
using DataAcces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class SaleDAOTest
    {
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
            int saleCode = 2;
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
            int saleCode = 2;
            int responseCode;
            Sale sale = new Sale();
            (responseCode, sale) = SaleDAO.getSaleById(saleCode);
            Assert.AreEqual(null, sale);
        }
    }
}
