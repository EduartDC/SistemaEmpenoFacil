﻿using BusinessLogic.Utility;
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
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CustomerDAO
    {

        public static (int, int) AddCustomer(Customer newCustomer)
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
            catch (Exception)
            {
                throw new Exception();
            }
            return (result, idCustomer);
        }
        //cide
        public static int UpdateCustomer(Customer selectedCustomer)
        {
            var result = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var customer = connection.Customers.Find(selectedCustomer.idCustomer);

                    customer.address = selectedCustomer.address;
                    customer.identification = selectedCustomer.identification;
                    customer.telephonNumber = selectedCustomer.telephonNumber;
                    result = connection.SaveChanges();

                }

            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }
            return result;
        }
        //cide
        public static Customer GetCustomer(int id)
        {
            Customer customer = new Customer();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    customer = connection.Customers.Find(id);
                }
            }
            else
            {
                throw new Exception("No hay conexión a la base de datos");
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
            catch (DbUpdateException )
            {
                throw new DbUpdateException();
            }
            catch (Exception )
            {
                throw new Exception();
            }
            return result;
        }
        public static int AddImagecostumer(ImagesIdentification newImage)
        {
            var result = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
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
        //cide
        public static List<ImagesIdentification> GetImagesCustomer(int id)
        {
            List<ImagesIdentification> images = new List<ImagesIdentification>();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    images = connection.ImagesIdentifications.Where(x => x.Customer_idCustomer == id).ToList();
                }
            }
            else
            {
                throw new Exception("No hay conexión a la base de datos");
            }
            return images;
        }
        //cide
        public static int UpdateImageCustomer(ImagesIdentification selectedImage)
        {
            var result = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var image = connection.ImagesIdentifications.Find(selectedImage.idImagenIdentification);

                    image.imagen = selectedImage.imagen;
                    connection.SaveChanges();
                    result = MessageCode.SUCCESS;
                }
            }
            else
            {
                throw new Exception("No hay conexión a la base de datos");
            }
            return result;
        }
        public static (int, Customer) FindCustomer(string curp)
        {
            //List<Customer> customers = new List<Customer>();
            //if (Utilities.VerifyConnection())
            //{
            //    using (var connection = new ConnectionModel())
            //    {
            //        customers = connection.Customers.ToList();
            //    }
            //    foreach (var customer in customers)
            //    {
            //        if (customer.curp.Equals(curp))
            //        {
            //            return (1, customer);
            //        }
            //    }
            //}
            //return (MessageCode.CONNECTION_ERROR, null);
            int result = 0;
            Customer customer = null;
            try
            {
                using (var connection = new ConnectionModel() )
                {
                    customer  = connection.Customers.Where(a => a.curp .Equals(curp)).FirstOrDefault();
                    if (customer!= null)
                        result = MessageCode.SUCCESS;
                    else
                        result = MessageCode.ERROR_USER_NOT_FOUND;
                }
            }
            catch
            {
                result = MessageCode.CONNECTION_ERROR;

            }

            return (result,customer);
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
            catch (ArgumentNullException )
            {
                throw new ArgumentNullException();
            }
            catch (Exception )
            {
                throw new Exception();
            }
            return resultCustomers;
        }

        public static int ExistCustomer(string curpObtine)
        {
            int result = 500;
            try
            {
                using (var dataBase = new ConnectionModel())
                {
                    var existCustomer = (from Customer in dataBase.Customers
                                         where Customer.curp.Equals(curpObtine)
                                         select Customer).Count();
                    if (existCustomer > 0)
                    {
                        result = 200;
                    }
                }
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
            catch(Exception )
            {
                throw new Exception();
            }
            return result;
        }

        public static int ChangeStatusBlackList(string curpObtine)
        {
            int result = 500;
            int resultValue = 0;
            try
            {
                using (var database = new ConnectionModel())
                {
                    var updateStatusBlackList = database.Customers.First(u => u.curp == curpObtine);
                    if(updateStatusBlackList.blackList == false)
                    {
                        updateStatusBlackList.blackList = true;
                        resultValue = database.SaveChanges();
                    }
                    
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
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
            return result;
        }

        public static Customer FindCustomerById(int id)
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
                
            }
            catch (InvalidOperationException ex)
            {
                
            }
            return customer;
        }
        //cide
        public static DataAcces.Customer GetCustomerByCURP(string CURP)
        {
            var result = new DataAcces.Customer();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    result = connection.Customers.Where(x => x.curp == CURP).FirstOrDefault();
                }
            }
            else
            {
                throw new Exception("No hay conexion a la base de datos");
            }
            return result;
        }

        public static (int, List<Domain.CustomerProfitDomain>) GetCustomersProfit()
        {
            List<Domain.CustomerProfitDomain> CustomersProfitList = new List<Domain.CustomerProfitDomain>();

            List<Customer> list = new List<Customer>();
            if (Utilities.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {//recuperando todos los articulos vendidos
                        var result = (from contract in connection.Contracts
                                      join belonging in connection.Belongings
                                        on contract.idContract equals belonging.Contract_idContract
                                      join article in connection.Belongings_Articles
                                        on belonging.idBelonging equals article.idBelonging
                                      join customer in connection.Customers
                                        on contract.Customer_idCustomer equals customer.idCustomer
                                      select new
                                      {
                                          Contract = contract,
                                          Belonging = belonging,
                                          Belongings_Articles = article,
                                          Customer = customer
                                      }).ToList();
                        Console.WriteLine(result.Count);
                        foreach (var util in result)
                        {
                            //                        for (int i = 0; i < result.Count(); i++)
                            if (CustomersProfitList.Count() == 0)//el primer objeto de la lista
                            {
                                DateTime limitDate = DateTime.Now.AddYears(-1);
                                if (util.Belongings_Articles.creationDate >= limitDate)
                                {
                                    if (util.Belongings_Articles.customerProfit > 0 && double.Parse(util.Customer.cumulativeProfit) > 0)
                                    {
                                        Domain.CustomerProfitDomain newCustomerProfit = new Domain.CustomerProfitDomain();
                                        newCustomerProfit.idCustomer = util.Customer.idCustomer;
                                        newCustomerProfit.curp = util.Customer.curp;
                                        newCustomerProfit.firstname = util.Customer.firstName;
                                        newCustomerProfit.lastName = util.Customer.lastName;
                                        newCustomerProfit.profitCustomer = float.Parse(util.Customer.cumulativeProfit);
                                        newCustomerProfit.articlesProfit += "-" + "[" + util.Belonging.idBelonging + "]" + util.Belonging.description + "\n";
                                        CustomersProfitList.Add(newCustomerProfit);
                                    }

                                }
                            }
                            else//verificar que no exista el cliente, o agregarlo a uno existente
                            {
                                bool customerFinded = false;
                                foreach (var obj in CustomersProfitList)
                                {
                                    if (util.Customer.idCustomer == obj.idCustomer)//verificar existencia
                                        customerFinded = true;
                                }
                                if (customerFinded)//agregarlo a existente
                                {
                                    for (int i = 0; i < CustomersProfitList.Count; i++)
                                    {
                                        if (util.Customer.idCustomer == CustomersProfitList[i].idCustomer && util.Belongings_Articles.customerProfit > 0)
                                            CustomersProfitList[i].articlesProfit += "-" + "[" + util.Belonging.idBelonging + "]" + util.Belonging.description + "\n";

                                    }
                                }
                                else//registrarlo
                                {
                                    Domain.CustomerProfitDomain newCustomerProfit = new Domain.CustomerProfitDomain();
                                    newCustomerProfit.idCustomer = util.Customer.idCustomer;
                                    newCustomerProfit.curp = util.Customer.curp;
                                    newCustomerProfit.firstname = util.Customer.firstName;
                                    newCustomerProfit.lastName = util.Customer.lastName;
                                    newCustomerProfit.profitCustomer = float.Parse(util.Customer.cumulativeProfit);
                                    newCustomerProfit.articlesProfit += "-" + "[" + util.Belonging.idBelonging + "]" + util.Belonging.description + "\n";
                                    if (util.Belongings_Articles.customerProfit > 0)

                                        CustomersProfitList.Add(newCustomerProfit);
                                }
                            }
                        }
                    }
                    return (MessageCode.SUCCESS, CustomersProfitList);
                }
                catch(Exception)
                {
                    return (MessageCode.CONNECTION_ERROR, null);
                }
            }
            else
                return (MessageCode.CONNECTION_ERROR, null);
        }
        //Rafa
        public static  List<Domain.Customer> RecoverAllCustomers()
        {
            //NewLog _log = new NewLog();
            List<Domain.Customer> resultCustomers = new List<Domain.Customer>();
            try
            {
                using (var database = new ConnectionModel())
                {
                    var customer = (from Customers in database.Customers
                                    
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
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
            return resultCustomers;
        }

        public static int GiveProfitCustomer(int idCustomer)
        {
            int result = 0;
            if (Utilities.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        Customer customerFinded = connection.Customers.FirstOrDefault(a => a.idCustomer == idCustomer);
                        customerFinded.cumulativeProfit = "0";
                        connection.SaveChanges();
                        result = MessageCode.SUCCESS;
                    }
                }
                catch(Exception)
                {
                    result = MessageCode.CONNECTION_ERROR;

                }
            }
            else
                result = MessageCode.CONNECTION_ERROR;
            return result;
        }
    }
}

