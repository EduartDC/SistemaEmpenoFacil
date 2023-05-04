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
    public class OperationDAOTest
    {
        private Operation newOperation { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            //se crean los objetos que se van a utilizar en los test
            newOperation = new Operation();
            newOperation.paymentAmount = 345;
            newOperation.changeAmount = 5;
            newOperation.concept = "Test";
            newOperation.receivedAmount = 500;
            newOperation.operationDate = DateTime.Now;
            newOperation.Staff_idStaff = 1;

        }

        [TestMethod]
        public void TestAddOperationSuccess()
        {
            int expectedResult = 1;
            Assert.AreEqual(expectedResult, OperationDAO.AddOperation(newOperation));
        }
    }
}
