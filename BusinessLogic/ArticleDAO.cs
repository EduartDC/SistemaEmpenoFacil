using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcces;

namespace BusinessLogic
{
    public class ArticleDAO
    {
        public static (int, List<Belongings_Articles>) getArticles()
        {
            List<Belongings_Articles> articles = new List<Belongings_Articles>();
            if (Utilitys.VerifyConnection())
            {
                //using (var connecction = new ConnectionModel())
                //{
                //    articles = connecction.Belongings_Articles.ToList();
                //}
                return (MessageCode.SUCCESS, articles);
            }

            return (MessageCode.CONNECTION_ERROR, null);
        }
    }
}
