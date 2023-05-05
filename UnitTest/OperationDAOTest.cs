using BusinessLogic;
using DataAcces;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void TestAddOperationErrorEmpty()
        {
            int expectedResult = 0;
            Assert.AreEqual(expectedResult, OperationDAO.AddOperation(new Operation()));
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void TestAddOperationError()
        {
            int expectedResult = 0;
            newOperation.Staff_idStaff = 0;
            Assert.AreEqual(expectedResult, OperationDAO.AddOperation(newOperation));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddOperationException()
        {
            int expectedResult = 0;
            newOperation.Staff_idStaff = 0;
            Assert.AreEqual(expectedResult, OperationDAO.AddOperation(newOperation));
        }

        [TestMethod]
        public void TestAddOperationErrorLimit()
        {
            int expectedResult = 1;
            newOperation.paymentAmount = 3450000000000000000;
            Assert.AreEqual(expectedResult, OperationDAO.AddOperation(newOperation));
        }

        [TestMethod]
        public async Task TestGetAllOperationsByDateSuccessAsync()
        {
            List<OperationDomain> expectedResult = new List<OperationDomain>();
            for (int i = 0; i < 6; i++)
            {
                expectedResult.Add(new OperationDomain());
            }

            List<OperationDomain> actualResult = await Task.Run(() => OperationDAO.GetAllOperationsByDate(DateTime.Now, 1));

            Assert.AreEqual(expectedResult.Count, actualResult.Count);
        }

        [TestMethod]
        public async Task TestGetAllOperationsByDateSuccessError()
        {
            List<OperationDomain> expectedResult = new List<OperationDomain>();
            List<OperationDomain> actualResult = await Task.Run(() => OperationDAO.GetAllOperationsByDate(DateTime.Now, 0));
            Assert.AreEqual(expectedResult.Count, actualResult.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task TestGetAllOperationsByDateSuccessException()
        {
            List<OperationDomain> expectedResult = new List<OperationDomain>();
            List<OperationDomain> actualResult = await Task.Run(() => OperationDAO.GetAllOperationsByDate(DateTime.Now, 2));
            Assert.AreEqual(expectedResult.Count, actualResult.Count);
        }
    }
}
