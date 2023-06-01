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
        public void TestGetFindArticles()//pasó
        {
            List<ArticleDomain> articles = new List<ArticleDomain>();
            int resultCode = MessageCode.SUCCESS;
            var result = ArticleDAO.getArticles();
            Boolean articlesBool = result.Item2.Count > 0;
            Assert.AreEqual((resultCode, true),(result.Item1,articlesBool) );
        }
        
        //Jonathan
        //CU find articles
        [TestMethod]
        public void  TestGetArticlesErrorConnection()//pasó
        {
            int expectedResult = MessageCode.CONNECTION_ERROR;
            var result = ArticleDAO.getArticles();
            Assert.AreEqual((expectedResult, result.Item2),result);
        }

        //Jonathan
        //CU-crear ticket venta
        [TestMethod]
        public void TestGetArticlesById()//pasó
        {
            int resultCode = MessageCode.SUCCESS;
            List<Belongings_Articles> articles = new List<Belongings_Articles>();
            List<int>  articlesId = new List<int> {1};
            var result = ArticleDAO.GetArticlesListById(articlesId);
            Assert.AreEqual((resultCode,result.Item2),result);
        }

        //Jonathan
        //CU-crear ticket venta
        [TestMethod]
        public void TestGetArticlesByIdConnectionError()//pasó
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
        public void testGetSaleList()//pasó
        {
            int resultCode = MessageCode.SUCCESS;
            DateTime end = DateTime.Now;
            DateTime Begin = DateTime.Now.AddMonths(-1);
            List<SaledArticle> articles = new List<SaledArticle>();
            var result = ArticleDAO.GetSaledArticlesToReport(Begin, end);
            bool articlesBool = result.Item2.Count > 0;
            Assert.AreEqual((resultCode,true),(result.Item1,articlesBool));
        }

        //Jonathan
        //Cu Crear reporte de ventas
        [TestMethod]
        public void testGetSaleListToReportConnectionError()//pasó
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
        public void testGetArticlesStockToReport() //pasó
        {
            int resultCode = MessageCode.SUCCESS;
            DateTime end = DateTime.Now;
            DateTime Begin = DateTime.Now.AddMonths(-1);
            List<SaledArticle> articles = new List<SaledArticle>();
            var result = ArticleDAO.GetSaledArticlesToReport(Begin, end);
            
            Assert.AreEqual((resultCode), result.Item1);
        }

        //Jonathan
        //Cu Crear reporte de ventas
        [TestMethod]
        public void testGetArticlesStockToReportConnectionError()//pasó
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
        public void TestGiveProfitArticlesToCustomer()//pasó
        {
            int code = MessageCode.SUCCESS;//no hay clientes con ganancia por lo que dara Error_update
            Assert.AreEqual(code, ArticleDAO.GiveProfitArticlesToCustomer(1));
        }

        //jonathan
        //Cu dar ganancia a cliente
        [TestMethod]
        public void TestGiveProfitArticlesToCustomerConnectionError()//pasó
        {
            int code = MessageCode.CONNECTION_ERROR;
            var result = ArticleDAO.GiveProfitArticlesToCustomer(1);
            Assert.AreEqual(code, result);
        }

        //[TestMethod]
        //public void TestGetFindArticles()
        //{
        //    int resultCode = MessageCode.SUCCESS;
        //    Assert.AreEqual((resultCode, List<ArticleDomain>));
        //}

        [TestMethod]
        public void TestRecoverSellingPriceSuccess()
        {
            Assert.AreEqual(6500, ArticleDAO.RecoverSellingPrice(6));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestRecoverSellingPriceException()
        {
            Assert.AreEqual(-1, ArticleDAO.RecoverSellingPrice(6));
        }

        [TestMethod]
        public void TestModifySellingPriceSuccess()
        {
            Assert.AreEqual(200, ArticleDAO.ModifySellingPrice(7, 3000));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestModifySellingPriceException()
        {
            Assert.AreEqual(500, ArticleDAO.ModifySellingPrice(7, 3000));
        }

        [TestMethod]
        public void TestGetBelonging_Article()
        {
            Domain.ArticleDomain articleDomain = BelongingsArticlesDAO.GetBelonging_Article(8);
            Assert.AreNotEqual(null, articleDomain);
        }

        [TestMethod]
        public void TestGetBelonging_ArticleFailed()
        {
            Domain.ArticleDomain articleDomain = BelongingsArticlesDAO.GetBelonging_Article(0);
            Assert.AreEqual(null, articleDomain);
        }

        [TestMethod]
        public void TestModifyBelonging_Article()
        {
            int idArticle = 11;
            int idSale = 11;
            double storeProfit = 232;
            int expectedResult = 1;
            int resultOperation = BelongingsArticlesDAO.ModifyBelonging_Article(idArticle, idSale, storeProfit);
            Assert.AreEqual(expectedResult, resultOperation);

        }
    }
}
