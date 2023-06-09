﻿using BusinessLogic.Utility;
using DataAcces;
using Domain;
using Domain.BelongingCreation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BelongingsArticlesDAO
    {
        public static List<Belongings_Articles> GetBelongings_Articles_Selled(int idSale)
        {
            List<Belongings_Articles> belongings_ArticlesSelled = new List<Belongings_Articles>();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
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

        public static Domain.ArticleDomain GetBelonging_Article(string barCode)
        {
            Domain.ArticleDomain articleDomain = null;
            Belongings_Articles belongings_Articles = new Belongings_Articles();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    try
                    {
                        belongings_Articles = connection.Belongings_Articles.Where(util => util.barCode == barCode).FirstOrDefault();
                        articleDomain = new Domain.ArticleDomain();
                        articleDomain.idArticle = belongings_Articles.idArticle;
                        articleDomain.barCode = belongings_Articles.barCode;
                        articleDomain.sellingPrice = belongings_Articles.sellingPrice;
                        articleDomain.stateArticle = belongings_Articles.stateArticle;
                        articleDomain.customerProfit = belongings_Articles.customerProfit;
                        articleDomain.storeProfit = belongings_Articles.storeProfit;
                        articleDomain.idBelonging = belongings_Articles.idBelonging;

                        DataAcces.Belonging belonging = new DataAcces.Belonging();
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
                    catch (NullReferenceException)
                    {
                        articleDomain = null;
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexion");
            }
            return articleDomain;
        }

        public static int AddBelongingArticle(Belongings_Articles newBelongingArticle)
        {
            var result = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    try
                    {
                        connection.Belongings_Articles.Add(newBelongingArticle);
                        result = connection.SaveChanges();
                    }
                    catch (DbEntityValidationException)
                    {
                        throw new DbEntityValidationException();
                    }
                }
            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }
            return result;
        }

        public static int ModifyBelonging_Article(int idArticle, int idSale, double storeProfit, double finalSellingPrice)
        {
            int resultOperation = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var article = connection.Belongings_Articles.Where(util => util.idArticle == idArticle).FirstOrDefault();
                    article.stateArticle = "Vendido";
                    article.Sale_idSale = idSale;
                    article.storeProfit = storeProfit;
                    article.customerProfit = finalSellingPrice - (finalSellingPrice * .6);
                    resultOperation = connection.SaveChanges();
                }
            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }

            return resultOperation;
        }

        public static int ModifyBelonging_ArticleSetAside(int idArticle, int idSale, double storeProfit)
        {
            int resultOperation = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var article = connection.Belongings_Articles.Where(util => util.idArticle == idArticle).FirstOrDefault();
                    article.stateArticle = "Vendido";
                    article.Sale_idSale = idSale;
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
