using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Utility;
using DataAcces;
using Domain;

namespace BusinessLogic
{
    public class ArticleDAO
    {
        public static (int, List<Belongings_Articles>) getArticles()
        {
            List<Belongings_Articles> articles = new List<Belongings_Articles>();
            if (Utilities.VerifyConnection())
            {

                using (var connecction = new ConnectionModel())
                {
                    articles = connecction.Belongings_Articles.ToList();
                }

                return (MessageCode.SUCCESS, articles);
            }

            return (MessageCode.CONNECTION_ERROR, null);
        }



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
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
            return articleDomain;
        }

        public static void UpdateArticleState(int idArticle, string state)
        {
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var article = connection.Belongings_Articles.Where(a => a.idArticle == idArticle).FirstOrDefault();
                    if (article != null)
                    {
                        article.stateArticle = state;
                        connection.SaveChanges();
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
        }

    }
}
