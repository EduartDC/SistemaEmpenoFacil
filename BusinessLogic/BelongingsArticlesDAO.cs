using BusinessLogic.Utility;
using DataAcces;
using System;
using System.Collections.Generic;
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
    }
}
