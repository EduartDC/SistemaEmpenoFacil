using BusinessLogic;
using BusinessLogic.Utility;
using DataAcces;
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
    public class ArticleDAOTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            //se crean los objetos que se van a utilizar en los test
        }
        [TestMethod]
        public void TestGetArticleDomainByCodeSuccess()
        {
            Assert.IsNotNull(ArticleDAO.GetArticleDomainByCode("666"));
        }

        [TestMethod]
        public void TestGetArticleDomainByCodeError()
        {
            Assert.IsNull(ArticleDAO.GetArticleDomainByCode("000"));
        }

        [TestMethod]
        public void TestGetArticleDomainByCodeErrorEmpty()
        {
            ArticleDomain expectedResult = new ArticleDomain();
            Assert.AreEqual(expectedResult, ArticleDAO.GetArticleDomainByCode(""));
        }

        [TestMethod]
        public void TestGetArticleDomainByCodeErrorLimit()
        {
            ArticleDomain expectedResult = new ArticleDomain();
            Assert.AreEqual(expectedResult, ArticleDAO.GetArticleDomainByCode(" 000000000000d4eesee/**+0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestGetArticleDomainByCodeConnectionException()
        {
            ArticleDomain expectedResult = new ArticleDomain();
            Assert.AreNotEqual(expectedResult, ArticleDAO.GetArticleDomainByCode(""));
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


        //Jonathan
        //CU16 findArticles
        [TestMethod]
        public void TestGetFindArticles()
        {
            List<ArticleDomain> articles = new List<ArticleDomain>();
            int resultCode = MessageCode.SUCCESS;
            Assert.AreEqual((resultCode, articles), ArticleDAO.getArticles());
        }

        [TestMethod]
        public void  TestGetArticlesErrorConnection()
        {
            int expectedResult = MessageCode.CONNECTION_ERROR;
            Assert.AreEqual((expectedResult, null), ArticleDAO.getArticles());
        }

        //Jonathan
        //CUcrearticket venta
        public void TestGetArticlesById()
        {
            int resultCode = MessageCode.SUCCESS;
            List<Belongings_Articles> articles = new List<Belongings_Articles>();
            List<int>  articlesId = new List<int> {1,2,3};
            Assert.AreEqual((resultCode,));
        }
        
    }
}
