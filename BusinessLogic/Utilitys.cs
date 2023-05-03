using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Utilitys
    {
        public static bool VerifyConnection()
        {
            var result = false;
            var connection = new ConnectionModel();
            try
            {
                connection.Database.Connection.Open();
                connection.Database.Connection.Close();

                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
