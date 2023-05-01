using DataAcces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CustomerDAO
    {
        private static NewLog _log = new NewLog();
        public static (int,int) AddCustomer(Customer newCustomer)
        {
            int result = 500;
            int idCustomer = 0;
            try
            {
                using (var database = new ConnectionModel())
                {
                    var addNewCustomer = database.Customers.Add(new Customer()
                    {
                        firstName = newCustomer.firstName,
                        lastName = newCustomer.lastName,
                        curp = newCustomer.curp,
                        blackList = newCustomer.blackList,
                        telephonNumber = newCustomer.telephonNumber,
                        address = newCustomer.address,
                        cumulativeProfit = "0",
                        identification = newCustomer.identification
                    });
                    database.SaveChanges();
                    idCustomer = addNewCustomer.idCustomer;
                    result = 200;
                }
                
            }
            catch (DbUpdateException ex)
            {
                _log.Add(ex.ToString());
            }
            catch (EntityException ex)
            {
                _log.Add(ex.ToString());
            }                   
            return (result, idCustomer);
        }

        public static int UpdateCustomer(Customer selectedCustomer)
        {
            var result = MessageCode.ERROR;
            if (Utilitys.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var customer = connection.Customers.Find(selectedCustomer.idCustomer);
                    if (customer.address.Equals(selectedCustomer.address) ||
                    customer.identification.Equals(selectedCustomer.identification) ||
                    customer.telephonNumber.Equals(selectedCustomer.telephonNumber))
                    {
                        customer.address = selectedCustomer.address;
                        customer.identification = selectedCustomer.identification;
                        customer.telephonNumber = selectedCustomer.telephonNumber;
                    }
                    connection.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                    result = connection.SaveChanges();
                }
            }
            return result;
        }

        public static Customer GetCustomer(int id)
        {
            Customer customer = new Customer();
            if (Utilitys.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    customer = connection.Customers.Find(id);
                }
            }
            return customer;
        }


        public static int AddTwoImageIdentification(List<ImagesIdentification> imagesIdentifications)
        {
            int result = 500;

            try
            {
                using (var database = new ConnectionModel())
                {
                    database.ImagesIdentifications.AddRange(imagesIdentifications);
                    database.SaveChanges();
                    result = 200;
                }
            }
            catch (DbUpdateException ex)
            {
                _log.Add(ex.ToString());
            }
            catch (EntityException ex)
            {
                _log.Add(ex.ToString());
            }
            return result;
        } 
        public static int AddImagecostumer(ImagesIdentification newImage)
        {
            var result = MessageCode.ERROR;
            if (Utilitys.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    connection.ImagesIdentifications.Add(newImage);
                    connection.SaveChanges();
                    result = MessageCode.SUCCESS;
                }
            }
            return result;
        }
        public static List<ImagesIdentification> GetImagesCustomer(int id)
        {
            List<ImagesIdentification> images = new List<ImagesIdentification>();
            if (Utilitys.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    images = connection.ImagesIdentifications.Where(x => x.Customer_idCustomer == id).ToList();
                }
            }
            return images;
        }
        public static int UpdateImageCustomer(ImagesIdentification selectedImage)
        {
            var result = MessageCode.ERROR;
            if (Utilitys.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var image = connection.ImagesIdentifications.Find(selectedImage.idImagenIdentification);

                    image.imagen = selectedImage.imagen;
                    connection.SaveChanges();
                    result = MessageCode.SUCCESS;
                }
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
                        newCustomer.telephonNumber = (int)customer1.telephonNumber;
                        resultCustomers.Add(newCustomer);
                    }
                }
            }
            catch (SqlException ex)
            {
                _log.Add(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                _log.Add(ex.ToString());
            }
            catch (DataException ex)
            {
                _log.Add(ex.ToString());
            }
            return resultCustomers;
        }

        public static int existCustomer(int id)
        {
            int result = 500;
            try
            {
                using (var dataBase = new ConnectionModel())
                {
                    var existCustomer = (from Customer in dataBase.Customers
                                         where Customer.idCustomer.Equals(id)
                                         select Customer).Count();
                    if (existCustomer > 0)
                    {
                        result = 200;
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                _log.Add(ex.ToString());
            }
            catch (SqlException ex)
            {
                result = 505;
                _log.Add(ex.ToString());
            }
            catch (EntityException ex)
            {
                _log.Add(ex.ToString());
            }
            return result;
        }

        public static int changeStatusBlackList(int id)
        {
            int result = 500;
            try
            {
                using (var database = new ConnectionModel())
                {
                    var updateStatusBlackList = database.Customers.First(u => u.idCustomer == id);
                    updateStatusBlackList.blackList = true;
                    int resultValue = database.SaveChanges();
                    if (resultValue > 0)
                    {
                        result = 200;
                    }
                    else
                    {
                        result = 502;
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                _log.Add(ex.ToString());
            }
            catch (DbUpdateException ex)
            {
                _log.Add(ex.ToString());
            }
            catch (EntityException ex)
            {
                _log.Add(ex.ToString());
            }
            return result;
        }

        public static Customer findCustomerById(int id)
        {
            Customer customer = new Customer();
            try
            {
                using (var connection = new ConnectionModel())
                {
                    customer = connection.Customers.Find(id);
                }
            }
            catch (DataException ex)
            {
                _log.Add(ex.ToString());
            }
            catch (InvalidOperationException ex)
            {
                _log.Add(ex.ToString());
            }
            return customer;
        }

    }
}

