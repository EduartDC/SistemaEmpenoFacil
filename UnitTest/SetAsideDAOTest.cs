using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class SetAsideDAOTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

            //se crean los objetos que se van a utilizar en los test
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]  //espera una excepcion
        public void TestUpdateArticleStateSuccess()
        {

            //int expectedResult = 0
            //Assert.AreEqual(expectedResult, objectManagerService.MatchingFriends(" ").Count())
        }
    }
}
