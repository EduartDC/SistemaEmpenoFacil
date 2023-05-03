using BusinessLogic.Utility;
using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class SetAsideDAO
    {
        public static (int, int) CreateSetAside(SetAside newAside)
        {
            var result = MessageCode.ERROR;
            var idSetAside = 0;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    connection.SetAsides.Add(newAside);
                    result = connection.SaveChanges();
                    idSetAside = newAside.idSetAside;
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
            return (idSetAside, result);
        }

        public static void AddArticlesInSetAside(List<ArticlesSetAside> list)
        {
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    foreach (var item in list)
                    {
                        connection.ArticlesSetAsides.Add(item);
                        connection.SaveChanges();
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
        }
        public static void UpdateSetAsideState(string state, int idSetAside)
        {
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var article = connection.SetAsides.Where(a => a.idSetAside == idSetAside).FirstOrDefault();
                    if (article != null)
                    {
                        article.stateAside = state;
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
