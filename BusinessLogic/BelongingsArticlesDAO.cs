using BusinessLogic.Utility;
using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace BusinessLogic
{
    public class BelongingsArticlesDAO
    {
        public static List<Belongings_Articles> GetBelongings_Articles_Selled(int idSale)
        {
            List<Belongings_Articles> belongings_ArticlesSelled = new List<Belongings_Articles>();
            if (Utilities.VerifyConnection())
            {
                using(var connection = new ConnectionModel())
                {
                    belongings_ArticlesSelled = connection.Belongings_Articles.Where(belongings_Articles => belongings_Articles.Sale_idSale == idSale).ToList();

                }
            }
            else
            {
                throw new Exception("Error de conexion");
            }
            return belongings_ArticlesSelled;
        }

        public static Domain.ArticleDomain GetBelonging_Article(int idArticle)
        {
            Domain.ArticleDomain articleDomain = null;
            Belongings_Articles belongings_Articles = new Belongings_Articles();
            if(Utilities.VerifyConnection())
            {
                using(var connection = new ConnectionModel())
                {
                    belongings_Articles = connection.Belongings_Articles.Where(util => util.idArticle == idArticle).FirstOrDefault();
                    articleDomain = new Domain.ArticleDomain();
                    articleDomain.idArticle = belongings_Articles.idArticle;
                    articleDomain.barCode = belongings_Articles.barCode;
                    articleDomain.sellingPrice = belongings_Articles.sellingPrice;
                    articleDomain.stateArticle = belongings_Articles.stateArticle;
                    articleDomain.customerProfit = belongings_Articles.customerProfit;
                    articleDomain.storeProfit = belongings_Articles.storeProfit;
                    articleDomain.idBelonging = belongings_Articles.idBelonging;
                    
                    Belonging belonging = new Belonging();
                    belonging = connection.Belongings.Find(articleDomain.idBelonging);
                    articleDomain.appraisalValue = belonging.appraisalValue;
                    articleDomain.category = belonging.category;
                    articleDomain.description = belonging.description;
                    articleDomain.characteristics = belonging.characteristics;
                    articleDomain.loanAmount = belonging.loanAmount;
                    articleDomain.serialNumber = belonging.serialNumber;

                    var imageInfo = connection.ImagesBelongings.Where(util => util.Belonging_idBelonging == articleDomain.idBelonging).FirstOrDefault();
                    if (imageInfo != null)
                    {
                        articleDomain.imageOne = imageInfo.imagen;
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexion");
            }
            return articleDomain;
        }

        public static int ModifyBelonging_Article(int idArticle, int idSale, double storeProfit)
        {
            int resultOperation = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                using(var connection = new ConnectionModel())
                {
                    var article = connection.Belongings_Articles.Find(idArticle);
                    article.stateArticle = "Vendido";
                    article.Sale_idSale= idSale;
                    article.storeProfit = storeProfit;
                    resultOperation = connection.SaveChanges();
                }
            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }
            return resultOperation;
        }

    }
}
