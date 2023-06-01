using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Utility;
using DataAcces;
using Domain;

namespace BusinessLogic
{
    public class ArticleDAO
    {
        public static (int, List<ArticleDomain>) getArticles()
        {
            List<Belongings_Articles> articles = new List<Belongings_Articles>();//lista para la conexion
            List<ArticleDomain> articlesDomain = new List<ArticleDomain>();//lista purgada
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    articles = connection.Belongings_Articles.Where(a => a.stateArticle.Equals("Disponible")).ToList();
                    foreach (var item in articles)
                    {
                        ArticleDomain newArticle = new ArticleDomain();
                        newArticle.idBelonging = item.Belonging.idBelonging;
                        newArticle.appraisalValue = item.Belonging.appraisalValue;
                        newArticle.category = item.Belonging.category;
                        newArticle.description = item.Belonging.description;
                        newArticle.characteristics = item.Belonging.characteristics;
                        newArticle.loanAmount = item.Belonging.loanAmount;
                        newArticle.serialNumber = item.Belonging.serialNumber;
                        newArticle.idContract = item.Belonging.Contract_idContract;
                        //atributos de Article
                        newArticle.idArticle = item.idArticle;
                        newArticle.barCode = item.barCode;
                        newArticle.sellingPrice = item.sellingPrice;
                        newArticle.stateArticle = item.stateArticle;
                        newArticle.customerProfit = item.customerProfit;
                        newArticle.storeProfit = item.storeProfit;
                        string createDate = item.creationDate.ToString("dd/MM/yyyy");
                        newArticle.createDate = DateTime.Parse(createDate);
                        articlesDomain.Add(newArticle);
                        using (var imgConnection = new ConnectionModel())
                        {
                            var imageInfo = imgConnection.ImagesBelongings.Where(util => util.idImagenBelonging == item.idBelonging).FirstOrDefault();
                            if (imageInfo != null)
                            {
                                newArticle.imageOne = imageInfo.imagen;
                            }

                        }
                    }
                }
                return (MessageCode.SUCCESS, articlesDomain);
            }
            return (MessageCode.CONNECTION_ERROR, null);
        }

        //cide
        public static ArticleDomain GetArticleDomainByCode(string code)
        {
            ArticleDomain articleDomain = new ArticleDomain();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var article = connection.Belongings_Articles.Where(a => a.barCode == code).FirstOrDefault();
                    if (article != null)
                    {
                        articleDomain.idArticle = article.idArticle;
                        articleDomain.barCode = article.barCode;
                        articleDomain.description = article.Belonging.description;
                        articleDomain.serialNumber = article.Belonging.serialNumber;
                        articleDomain.sellingPrice = article.sellingPrice;
                        articleDomain.appraisalValue = article.Belonging.appraisalValue;
                        articleDomain.category = article.Belonging.category;
                        articleDomain.characteristics = article.Belonging.characteristics;
                        articleDomain.idBelonging = article.idBelonging;
                        articleDomain.loanAmount = article.Belonging.loanAmount;
                        articleDomain.stateArticle = article.stateArticle;
                    }
                    else
                    {
                        articleDomain = null;
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
            return articleDomain;
        }
        //cide
        public static int UpdateArticleState(int idArticle, string state)
        {
            var resutl = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var article = connection.Belongings_Articles.Where(a => a.idBelonging == idArticle).FirstOrDefault();
                    if (article != null)
                    {
                        article.stateArticle = state;
                        resutl = connection.SaveChanges();
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
            return resutl;
        }

        public static int GiveProfitArticlesToCustomer(int idCustomer)
        {
            int result = 0;
            List<Belongings_Articles> articlesList = new List<Belongings_Articles>();
            if (Utilities.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        articlesList = connection.Belongings_Articles.Where(a => a.Belonging.Contract.Customer_idCustomer == idCustomer).ToList();
                        for(int i = 0; i < articlesList.Count(); i++)
                        {
                            Console.WriteLine("Cliente: " + articlesList[i].Belonging.Contract.Customer_idCustomer);
                            if (articlesList[i].customerProfit > 0)//CAMBIAR A >, el == lo puse para pruebas de ticket
                                articlesList[i].customerProfit = 0;
                        }
                        int changes = connection.SaveChanges();
                        if (changes > 0)
                            result = MessageCode.SUCCESS;
                        else
                            result = MessageCode.ERROR_UPDATE;
                        Console.WriteLine("Cambios: " + changes);

                    }
                }
                catch (DbUpdateException )
                {
                    result = MessageCode.ERROR_UPDATE;
                }
            }
            else
                result = MessageCode.CONNECTION_ERROR;
            return result;
        }

        public static (int, List<ArticleDomain>) GetArticlesStockToReport(DateTime dateBegin, DateTime dateEnd)
        {
            int result = 0;
            List<ArticleDomain> articlesStock = new List<ArticleDomain>();

            if (Utilities.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        var list = connection.Belongings_Articles.Where(a => a.stateArticle == StatesArticle.SALE_ARTICLE).ToList();

                        foreach (var util in list)
                        {
                            string dateString = util.creationDate.ToString("dd/MM/yyyy");
                            Console.WriteLine("---" + dateString);
                            DateTime dateCreation = DateTime.Parse(dateString);
                            if (dateCreation >= dateBegin && dateCreation <= dateEnd)
                            {
                                ArticleDomain articleTemp = new ArticleDomain();
                                articleTemp.idArticle = util.idArticle;
                                articleTemp.createDate = dateCreation;
                                articleTemp.description = util.Belonging.description;
                                articleTemp.barCode = util.barCode;
                                articleTemp.idContract = util.Belonging.Contract_idContract;
                                articleTemp.stateArticle = util.stateArticle;
                                articleTemp.appraisalValue = util.Belonging.appraisalValue;
                                articleTemp.loanAmount = util.Belonging.loanAmount;
                                articleTemp.sellingPrice = util.sellingPrice;
                                articleTemp.category = util.Belonging.category;
                                articlesStock.Add(articleTemp);
                            }
                        }
                    }
                    result = MessageCode.SUCCESS;
                }
                catch (DbUpdateException)
                {
                    result = MessageCode.ERROR_UPDATE;
                }
            }
            else
                result = MessageCode.CONNECTION_ERROR;
            return (result, articlesStock);
        }

        public static (int, List<SaledArticle>) GetSaledArticlesToReport(DateTime dateBegin, DateTime dateEnd)
        {
            int result = 0;
            List<SaledArticle> articlesSales = new List<SaledArticle>();

            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var sales = connection.Sales.Where(a => a.saleDate >= dateBegin && a.saleDate <= dateEnd).ToList();
                    foreach (var util in sales)
                    {
                        foreach (var obj in util.Belongings_Articles)
                        {
                            SaledArticle saledArticle = new SaledArticle();
                            saledArticle.idSale = util.idSale;
                            saledArticle.saleDate = util.saleDate;


                            saledArticle.article.stateArticle = obj.stateArticle;
                            saledArticle.article.loanAmount = obj.Belonging.loanAmount;
                            saledArticle.article.barCode = obj.barCode;
                            saledArticle.article.createDate = obj.creationDate;
                            saledArticle.article.description = obj.Belonging.description;
                            saledArticle.article.sellingPrice = obj.sellingPrice;
                            saledArticle.customer = util.Customer.firstName + " " + util.Customer.lastName;

                            articlesSales.Add(saledArticle);
                        }
                    }
                    result = MessageCode.SUCCESS;

                }
            }
            else
                result = MessageCode.CONNECTION_ERROR;

            return (result, articlesSales);
        }

        public static  List<ArticleDomain> getArticlesById(List<int> idArticles)
        {
            int result = 0;
            List<ArticleDomain> articles = new List<ArticleDomain>();

            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var articlesFinded = connection.Belongings_Articles.Where(a => idArticles.Contains(a.idArticle)).ToList();
                    foreach (var util in articlesFinded)
                    {
                        ArticleDomain articleTemp = new ArticleDomain();
                        articleTemp.idArticle = util.idArticle;
                        articleTemp.description = util.Belonging.description;

                    }
                }
            }
            else
                result = MessageCode.CONNECTION_ERROR;
            result = MessageCode.SUCCESS;
            return  articles;
        }

        public static (int, List<ArticleDomain>) GetArticlesListById(List<int> idArticles)
        {
            int result = 0;
            List<ArticleDomain> articles = new List<ArticleDomain>(); 
            if( Utilities.VerifyConnection())
            {
                using ( var connection = new ConnectionModel() )
                {
                    var articlesList = connection.Belongings_Articles.Where( a => idArticles.Contains( a.idArticle ) ).ToList() ;
                    foreach (var util in articlesList)
                    {
                        ArticleDomain articleTemp = new ArticleDomain();
                        articleTemp.description = util.Belonging.description;
                    }
                
                }
                result = MessageCode.SUCCESS;
            }else
                result = MessageCode.CONNECTION_ERROR;

            return (result ,articles);
        }

        public static double RecoverSellingPrice(int id)
        {
            double resultSellingPrice = -1;
            try
            {
                using ( var connection = new ConnectionModel() )
                {
                    var articleObtained = (from Belongings_Articles in connection.Belongings_Articles
                                           where
                                           Belongings_Articles.idArticle == id
                                           select Belongings_Articles);
                    foreach(Belongings_Articles articles in articleObtained)
                    {
                        resultSellingPrice = articles.sellingPrice;
                    }
                            

                }
            }
            catch ( Exception)
            {
                throw new Exception();
            }

            return resultSellingPrice;
        }

        public static int ModifySellingPrice(int id, double newSellingPrice)
        {
            int result = 500;
            try
            {
                using (var connection = new ConnectionModel())
                {
                    var articleObtained = (from Belongings_Articles in connection.Belongings_Articles
                                           where
                                           Belongings_Articles.idArticle == id
                                           select Belongings_Articles);
                    articleObtained.First().sellingPrice = newSellingPrice;
                    connection.SaveChanges();
                    result = 200;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return result;
        }
        //Rafa
        public static List<ArticleDomain> GetArticlesListByIdSetAside(int idSetAside)
        {
            int result = 0;
            List<ArticleDomain> articles = new List<ArticleDomain>();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    
                    var articlesList = connection.ArticlesSetAsides.Where(a => a.SetAside_idSetAside == idSetAside).ToList();
                    foreach (ArticlesSetAside util in articlesList)
                    {
                        var belongingArticle = connection.Belongings_Articles.Find(util.Article_idBelonging);
                        ArticleDomain articleTemp = new ArticleDomain();
                        articleTemp.idArticle = util.idArticlesSetAside;
                        articleTemp.stateArticle = belongingArticle.stateArticle;
                        articleTemp.idSetAside = util.SetAside_idSetAside;
                        articleTemp.idBelonging = util.Article_idBelonging;
                        articles.Add(articleTemp);
                        
                        
                    }

                }
                result = MessageCode.SUCCESS;
            }
            else {
                result = MessageCode.CONNECTION_ERROR;
            }
                

            return articles;
        }

    }
}
