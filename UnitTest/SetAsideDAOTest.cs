using BusinessLogic;
using BusinessLogic.Utility;
using DataAcces;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{

    [TestClass]
    public class SetAsideDAOTest
    {

        private SetAside newSetAside { get; set; }
        [TestInitialize]
        public void TestInitialize()
        {

            //se crean los objetos que se van a utilizar en los test
            newSetAside = new SetAside();
            newSetAside.creationDate = DateTime.Now;
            newSetAside.deadlineDate = DateTime.Now;
            newSetAside.Customer_idCustomer = 1;
            newSetAside.totalAmount = 1111;
            newSetAside.stateAside = "Test";
            newSetAside.percentage = "10";
            newSetAside.reaminingAmount = 1111;


        }
        [TestMethod]
        public void TestCreateSetAsideSuccess()
        {
            (int expectedResultOne, int expectedResultTwo) = (15, 1);
            Assert.AreEqual((expectedResultOne, expectedResultTwo), SetAsideDAO.CreateSetAside(newSetAside));
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void TestCreateSetAsideErrorCustomerNull()
        {
            (int expectedResultOne, int expectedResultTwo) = (0, 0);
            newSetAside.Customer_idCustomer = 0;
            Assert.AreEqual((expectedResultOne, expectedResultTwo), SetAsideDAO.CreateSetAside(newSetAside));
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void TestCreateSetAsideErrorEmpty()
        {
            (int expectedResultOne, int expectedResultTwo) = (0, 0);
            var newSetAsideEmpty = new SetAside();
            Assert.AreEqual((expectedResultOne, expectedResultTwo), SetAsideDAO.CreateSetAside(newSetAsideEmpty));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestCreateSetAsideException()
        {
            (int expectedResultOne, int expectedResultTwo) = (0, 0);
            Assert.AreEqual((expectedResultOne, expectedResultTwo), SetAsideDAO.CreateSetAside(new SetAside()));
        }
    }
}
