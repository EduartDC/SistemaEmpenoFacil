using DataAcces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CustomerDAO
    {

        public static int AddCustomer(Customer newCustomer)
        {
            var result = MessageCode.ERROR;
            if (Utilitys.VerifyConnection())
            {

            }
            return result;
        }

        public static int UpdateCustomer(Customer selectedCustomer)
        {
            var result = MessageCode.ERROR;
            if (Utilitys.VerifyConnection())
            {

            }
            return result;
        }
        public static int DeleteCustomer(Customer selectedCustomer)
        {
            var result = MessageCode.ERROR;
            if (Utilitys.VerifyConnection())
            {

            }
            return result;
        }

        public static (int, Customer) FindCustomer(string curp) {
            List <Customer> customers = new List<Customer>();
            if (Utilitys.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                     customers = connection.Customers.ToList();
                }
                foreach(var customer in customers) {
                    if(customer.curp.Equals(curp))
                    {
                        return (1, customer);
                    }
                }
            }
            return (MessageCode.CONNECTION_ERROR, null);
        }
    }
}
