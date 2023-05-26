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
        public void TestGetSaleById()
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
    }
}
