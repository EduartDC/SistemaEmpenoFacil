using DataAcces;
using System;
using System.Collections.Generic;

using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CustomerDAO
    {
        private static NewLog _log = new NewLog();
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

        public static (int, Customer) FindCustomer(string curp)
        {
            List<Customer> customers = new List<Customer>();
            if (Utilitys.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    customers = connection.Customers.ToList();
                }
                foreach (var customer in customers)
                {
                    if (customer.curp.Equals(curp))
                    {
                        return (1, customer);
                    }
                }
            }
            return (MessageCode.CONNECTION_ERROR, null);
        }

            public static List<Domain.Customer> RecoverCustomers()
            {
                List<Domain.Customer> resultCustomers = new List<Domain.Customer>();
                try
                {
                    using (var database = new ConnectionModel())
                    {
                        var customer = (from Customers in database.Customers
                                        where
                                        Customers.blackList == true
                                        select Customers).ToList();

                        foreach (DataAcces.Customer customer1 in customer)
                        {
                            Domain.Customer newCustomer = new Domain.Customer();
                            newCustomer.idCustomer = customer1.idCustomer;
                            newCustomer.curp = customer1.curp;
                            newCustomer.blackList = customer1.blackList;
                            newCustomer.address = customer1.address;
                            newCustomer.firstName = customer1.firstName;
                            newCustomer.lastName = customer1.lastName;
                            newCustomer.identification = customer1.identification;
                            newCustomer.telephonNumber = customer1.telephonNumber;
                            resultCustomers.Add(newCustomer);
                        }
                    }
                }
                catch (ArgumentNullException ex)
                {
                    _log.Add(ex.ToString());
                }
                catch (EntityException ex)
                {
                    _log.Add(ex.ToString());
                }
                return resultCustomers;
            }
    }
}

