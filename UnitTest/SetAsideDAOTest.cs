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

        [TestMethod]
        public void TestAddArticlesInSetAsideSuccess()
        {
            List<ArticlesSetAside> list = new List<ArticlesSetAside>();
            ArticlesSetAside articlesSetAside = new ArticlesSetAside();
            articlesSetAside.Article_idBelonging = 8;
            articlesSetAside.SetAside_idSetAside = 9;
            list.Add(articlesSetAside);

            int expectedResult = 1;
            Assert.AreEqual(expectedResult, SetAsideDAO.AddArticlesInSetAside(list));
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void TestAddArticlesInSetAsideError()
        {
            List<ArticlesSetAside> list = new List<ArticlesSetAside>();
            ArticlesSetAside articlesSetAside = new ArticlesSetAside();
            articlesSetAside.Article_idBelonging = 1;
            articlesSetAside.SetAside_idSetAside = 1;
            list.Add(articlesSetAside);

            int expectedResult = 0;
            Assert.AreEqual(expectedResult, SetAsideDAO.AddArticlesInSetAside(list));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddArticlesInSetAsideException()
        {
            List<ArticlesSetAside> list = new List<ArticlesSetAside>();
            ArticlesSetAside articlesSetAside = new ArticlesSetAside();
            articlesSetAside.Article_idBelonging = 1;
            articlesSetAside.SetAside_idSetAside = 1;
            list.Add(articlesSetAside);

            int expectedResult = 0;
            Assert.AreEqual(expectedResult, SetAsideDAO.AddArticlesInSetAside(list));
        }

        [TestMethod]
        public void TestUpdateSetAsideStateSuccess()
        {
            int expectedResult = 1;
            Assert.AreEqual(expectedResult, SetAsideDAO.UpdateSetAsideState(StatesAside.ACTIVED_ASIDE, 9));
        }

        [TestMethod]
        public void TestUpdateSetAsideStateError()
        {
            int expectedResult = 0;
            Assert.AreEqual(expectedResult, SetAsideDAO.UpdateSetAsideState(StatesAside.ACTIVED_ASIDE, 1));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestUpdateSetAsideStateException()
        {
            int expectedResult = 0;
            Assert.AreEqual(expectedResult, SetAsideDAO.UpdateSetAsideState("", 0));
        }

        //Jonathan
        //Cu Crear tickets
        [TestMethod]
        public void testGetSetasideById()
        {
            int expectedResult = MessageCode.SUCCESS;
            SetAside setAside = new SetAside();
            var a =new  SetAside();
            Assert.AreEqual((expectedResult, a),SetAsideDAO.GetAsideById(7));
        }

        //Jonathan
        //Cu Crear tickets
        [TestMethod]
        public void testGetSetasideByIdConnectionError()
        {
            int expectedResult = MessageCode.CONNECTION_ERROR;
            SetAside setAside = new SetAside();
            var result = SetAsideDAO.GetAsideById(7);
            Assert.AreEqual((expectedResult, result.Item2), result);
        }

        [TestMethod]
        public void TestGetSedAsidesByDate()
        {
            int expectedResult = 15;
            DateTime startDate = new DateTime(2023, 5, 2);
            DateTime endDate = new DateTime(2023, 5, 13);
            List<SetAside> setAsides = SetAsideDAO.GetSedAsidesByDate(startDate, endDate);
            Assert.AreEqual(expectedResult, setAsides.Count);
        }

        [TestMethod]
        public void TestGetSedAsidesByDateNull()
        {
            DateTime startDate = new DateTime(2023, 3, 23);
            DateTime endDate = new DateTime(2023, 3, 17);
            List<SetAside> setAsides = SetAsideDAO.GetSedAsidesByDate(startDate, endDate);
            Assert.AreEqual(0, setAsides.Count);
        }
    }
}
