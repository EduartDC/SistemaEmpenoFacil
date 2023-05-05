using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public static (int, List<ArticleDomain>) getArticles()
        {
            List<Belongings_Articles> articles = new List<Belongings_Articles>();//lista para la conexion
            List<ArticleDomain> articlesDomain = new List<ArticleDomain>();//lista purgada
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    //recuperar lista
                    articles = connection.Belongings_Articles.ToList();
                    //purga de articulos
                    foreach (var item in articles)
                    {
                        ArticleDomain newArticle = new ArticleDomain();

                        //atributos de Belonging
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
                        DateTime createDate = item.creationDate;
                        string date = createDate.ToString("MM/dd/yyyy");
                        newArticle.createDate = DateTime.Parse(date);



                        articlesDomain.Add(newArticle);

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
    }
}
