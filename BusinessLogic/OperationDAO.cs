using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class OperationDAO
    {
        public static int AddOperation(Operation newOperation)
        {
            var result = MessageCode.ERROR;
            if (Utilitys.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    connection.Operations.Add(newOperation);
                    result = connection.SaveChanges();
                }
            }
            return result;
        }


    }
}
