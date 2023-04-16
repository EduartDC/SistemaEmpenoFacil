using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    internal class OperationDAO
    {
        public static int AddOperation(Operation newOperation)
        {
            var result = MessageCode.ERROR;
            if (Utilitys.VerifyConnection())
            {

            }
            return result;
        }


    }
}
