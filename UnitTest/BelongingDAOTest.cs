using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class BelongingDAOTest
    {
        [TestMethod]
        public void TestGetBelongingByIdContractSuccess()
        {
            int idContract = 13;
            int expectedResult = 2;
            List<Domain.BelongingCreation.Belonging> belongings;
            belongings = BelongingDAO.GetBelongingByIdContract(idContract);
            Assert.AreEqual(expectedResult, belongings.Count);
        }

        [TestMethod]
        public void TestGetBelongingByIdContractFailed()
        {
            int idContract = 0;
            List<Domain.BelongingCreation.Belonging> belongings;
            belongings = BelongingDAO.GetBelongingByIdContract(idContract);
            Assert.AreEqual(0, belongings.Count);
        }
    }
}
