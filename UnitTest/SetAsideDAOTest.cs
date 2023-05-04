using BusinessLogic;
using BusinessLogic.Utility;
using Domain;
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
            //Assert.IsNotNull(objectManagerService.UserConnect(objectPlayer));
            //Assert.IsNull(objectManagerService.UserConnect(playerServer).userName);
            //Assert.AreNotEqual
        }
        [TestMethod]
        public void TestUpdateArticleStateSuccess()
        {
            int expectedResult = 1;
            Assert.AreEqual(expectedResult, ArticleDAO.UpdateArticleState(8, StatesArticle.ASIDE_ARTICLE));
        }

        [TestMethod]
        public void TestUpdateArticleStateError()
        {
            int expectedResult = 0;
            Assert.AreEqual(expectedResult, ArticleDAO.UpdateArticleState(1, StatesArticle.PENDING_ARTICLE));
        }

        [TestMethod]
        public void TestUpdateArticleStateErrorLimit()
        {
            int expectedResult = 0;
            Assert.AreEqual(expectedResult, ArticleDAO.UpdateArticleState(1000000000, StatesArticle.PENDING_ARTICLE));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestUpdateArticleStateConnectionException()
        {
            int expectedResult = 0;
            Assert.AreEqual(expectedResult, ArticleDAO.UpdateArticleState(0, ""));
        }

        [TestMethod]
        public void TestGetArticleDomainByCodeErrorEmpty()
        {
            ArticleDomain expectedResult = new ArticleDomain();
            Assert.AreEqual(expectedResult, ArticleDAO.GetArticleDomainByCode(""));
        }

        [TestMethod]
        public void TestGetArticleDomainByCodeError()
        {
            ArticleDomain expectedResult = new ArticleDomain();
            Assert.AreEqual(expectedResult, ArticleDAO.GetArticleDomainByCode(" 000000000000d4eesee/**+"));
        }
        [TestMethod]
        public void TestGetArticleDomainByCodeErrorLimit()
        {
            ArticleDomain expectedResult = new ArticleDomain();
            Assert.AreEqual(expectedResult, ArticleDAO.GetArticleDomainByCode(" 000000000000d4eesee/**+0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"));
        }

        [TestMethod]
        public void TestGetArticleDomainByCodeSuccess()
        {
            ArticleDomain expectedResult = new ArticleDomain();
            Assert.AreNotEqual(expectedResult, ArticleDAO.GetArticleDomainByCode("666"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestGetArticleDomainByCodeConnectionException()
        {
            ArticleDomain expectedResult = new ArticleDomain();
            Assert.AreNotEqual(expectedResult, ArticleDAO.GetArticleDomainByCode(" "));
        }
    }
}
