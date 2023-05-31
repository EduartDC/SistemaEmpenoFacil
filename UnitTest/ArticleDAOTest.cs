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
            var result = ArticleDAO.getArticles();
            Assert.AreEqual((expectedResult, result.Item2),result);
        }

        //Jonathan
        //CU-crear ticket venta
        [TestMethod]
        public void TestGetArticlesById()
        {
            int resultCode = MessageCode.SUCCESS;
            List<Belongings_Articles> articles = new List<Belongings_Articles>();
            List<int>  articlesId = new List<int> {1};
            Assert.AreEqual((resultCode,articles),ArticleDAO.GetArticlesListById(articlesId));
        }

        //Jonathan
        //CU-crear ticket venta
        [TestMethod]
        public void TestGetArticlesByIdConnectionError()
        {
            int resultCode = MessageCode.CONNECTION_ERROR;
            List<Belongings_Articles> articles = new List<Belongings_Articles>();
            List<int> articlesId = new List<int> { 1 };
            var result = ArticleDAO.GetArticlesListById(articlesId);
            Assert.AreEqual((resultCode, result.Item2), result);
        }

        //Jonathan
        //Cu Crear reporte de ventas
        [TestMethod]
        public void testGetSaleList()
        {
            int resultCode = MessageCode.SUCCESS;
            DateTime end = DateTime.Now;
            DateTime Begin = DateTime.Now.AddMonths(-1);
            List<SaledArticle> articles = new List<SaledArticle>();
            Assert.Equals((resultCode, articles), ArticleDAO.GetSaledArticlesToReport(Begin, end));
        }

        //Jonathan
        //Cu Crear reporte de ventas
        [TestMethod]
        public void testGetSaleListToReportConnectionError()
        {
            int resultCode = MessageCode.CONNECTION_ERROR;
            DateTime end = DateTime.Now;
            DateTime Begin = DateTime.Now.AddMonths(-1);
            List<SaledArticle> articles = new List<SaledArticle>();
            var result = ArticleDAO.GetSaledArticlesToReport(Begin, end);
            Assert.AreEqual((resultCode, result.Item2), result); 
        }

        //Jonathan
        //Cu Crear reporte de ventas
        [TestMethod]
        public void testGetArticlesStockToReport()
        {
            int resultCode = MessageCode.SUCCESS;
            DateTime end = DateTime.Now;
            DateTime Begin = DateTime.Now.AddMonths(-1);
            List<SaledArticle> articles = new List<SaledArticle>();
            Assert.Equals((resultCode, articles), ArticleDAO.GetSaledArticlesToReport(Begin, end));
        }

        //Jonathan
        //Cu Crear reporte de ventas
        [TestMethod]
        public void testGetArticlesStockToReportConnectionError()
        {
            int resultCode = MessageCode.CONNECTION_ERROR;
            DateTime end = DateTime.Now;
            DateTime Begin = DateTime.Now.AddMonths(-1);
            List<SaledArticle> articles = new List<SaledArticle>();
            var result = ArticleDAO.GetSaledArticlesToReport(Begin, end);
            Assert.AreEqual((resultCode, result.Item2),result);
        }

        //jonathan
        //Cu dar ganancia a cliente
        [TestMethod]
        public void TestGiveProfitArticlesToCustomer()
        {
            int code = MessageCode.SUCCESS;
            Assert.Equals(code, ArticleDAO.GiveProfitArticlesToCustomer(1));
        }

        //jonathan
        //Cu dar ganancia a cliente
        [TestMethod]
        public void TestGiveProfitArticlesToCustomerConnectionError()
        {
            int code = MessageCode.CONNECTION_ERROR;
            var result = ArticleDAO.GiveProfitArticlesToCustomer(1);
            Assert.AreEqual(code, result);
        }
    }
}
