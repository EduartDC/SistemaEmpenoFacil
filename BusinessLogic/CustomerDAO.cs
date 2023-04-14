using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CustomerDAO
    {
        
        public static int AddCustomer(Customer newCustomer)
        {
            var result = CodeError.ERROR;
            if (Utilitys.VerifyConnection())
            {
                
            }
            return result;
        }

        public static int UpdateCustomer(Customer selectedCustomer)
        {
            var result = CodeError.ERROR;
            if(Utilitys.VerifyConnection())
            {

            }
            return result;
        }
        public static int DeleteCustomer(Customer selectedCustomer)
        {
            var result = CodeError.ERROR;
            if (Utilitys.VerifyConnection())
            {

            }
            return result;
        }
    }
}
