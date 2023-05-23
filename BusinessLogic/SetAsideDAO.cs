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
        //cide
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
        //cide
        public static int AddArticlesInSetAside(List<ArticlesSetAside> list)
        {
            var result = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    foreach (var item in list)
                    {
                        connection.ArticlesSetAsides.Add(item);
                        result = connection.SaveChanges();
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
            return result;
        }
        //cide
        public static int UpdateSetAsideState(string state, int idSetAside)
        {
            var result = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var article = connection.SetAsides.Where(a => a.idSetAside == idSetAside).FirstOrDefault();
                    if (article != null)
                    {
                        article.stateAside = state;
                        result = connection.SaveChanges();
                    }
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
            return result;
        }

        public static List<SetAside> GetSedAsidesByDate(DateTime startDate, DateTime endDate)
        {
            List<SetAside> setAsides = new List<SetAside>();
            if(Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    //setAsides = connection.SetAsides.Where(setAside => setAside.creationDate.CompareTo);
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
            return setAsides;
        }
    }
}
