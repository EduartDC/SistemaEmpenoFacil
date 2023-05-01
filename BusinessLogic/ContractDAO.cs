using DataAcces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ContractDAO
    {
        private static NewLog _log = new NewLog();

        public static int LiquidateContract(Contract selectedContract)
        {
            var resutl = MessageCode.ERROR;
            if (Utilitys.VerifyConnection())
            {
            }
            return resutl;

        }

        public static (int ,int) RegisterContract(Contract contract)
        {
            int idObject = 0;
            if (Utilitys.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        connection.Contracts.Add(contract);
                        connection.SaveChanges();
                        idObject = contract.idContract;
                    }
                }
                catch (DbUpdateException)
                {
                    return (MessageCode.ERROR, idObject);
                }
            }
            else
                return (MessageCode.CONNECTION_ERROR, idObject);
            return (MessageCode.SUCCESS, idObject);
        }

        public static Contract GetContract(int idContract)
        {
            var result = new Contract();
            if (Utilitys.VerifyConnection())
            {

                using (var connection = new ConnectionModel())
                {
                    result = connection.Contracts.Find(idContract);
                }
            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }
            return result;
        }

        public static List<Domain.CompleteContract> RecoverContracts()
        {
            List<Domain.CompleteContract> resultContracts = new List<Domain.CompleteContract>();
            try
            {
                using (var database = new ConnectionModel())
                {
                    var contract = (from Contract in database.Contracts select Contract).ToList();

                    foreach (DataAcces.Contract contract1 in contract)
                    {
                        Domain.CompleteContract newContract = new Domain.CompleteContract();
                        newContract.idContract = contract1.idContract;
                        newContract.idCustomer = contract1.Customer_idCustomer;
                        newContract.stateContract = contract1.stateContract;
                        Customer newCustomer= new Customer();
                        newCustomer = CustomerDAO.findCustomerById(contract1.Customer_idCustomer);
                        newContract.firstName = newCustomer.firstName;
                        newContract.lastName = newCustomer.lastName;
                        resultContracts.Add(newContract);
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
            return resultContracts;
        }


    }
}
