using BusinessLogic.Utility;
using DataAcces;
using Domain;
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

        //cide
        public static int LiquidateContract(ContractDomain selectedContract)
        {
            var result = MessageCode.ERROR;

            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    var contract = connection.Contracts.Find(selectedContract.idContract);
                    if (contract != null)
                    {
                        contract.stateContract = selectedContract.stateContract;
                        contract.settlementAmount = selectedContract.settlementAmount;
                        result = connection.SaveChanges();
                    }

                }
            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }
            return result;
        }

        public static (int, int) RegisterContract(Contract contract)
        {
            int idObject = 0;
            if (Utilities.VerifyConnection())
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

        public static int ModifyContract(Contract contract, int idContract)
        {
            var result = MessageCode.ERROR;
            if (Utilities.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        var oldContract = connection.Contracts.Find(idContract);
                        oldContract = contract;
                        connection.Entry(oldContract).State = System.Data.Entity.EntityState.Modified;
                        result = connection.SaveChanges();
                        return result;
                    }
                }
                catch (DbUpdateException)
                {
                    return result;
                }
            }
            return result;
        }

        public static async Task<ContractDomain> GetContractsDomainAsync(int idContrac)
        {
            if (Utilities.VerifyConnection())
            {

                using (var connection = new ConnectionModel())
                {
                    var contractDomain = new ContractDomain();
                    var contract = await connection.Contracts.FindAsync(idContrac);
                    if (contract != null)
                    {
                        contractDomain.idContract = contract.idContract;
                        contractDomain.loanAmount = contract.loanAmount;
                        contractDomain.idContractPrevious = contract.idContractPrevious;
                        contractDomain.deadlineDate = contract.deadlineDate;
                        contractDomain.creationDate = contract.creationDate;
                        contractDomain.stateContract = contract.stateContract;
                        contractDomain.iva = contract.iva;
                        contractDomain.interestRate = contract.interestRate;
                        contractDomain.renewalFee = contract.renewalFee;
                        contractDomain.settlementAmount = contract.settlementAmount;
                        contractDomain.duration = contract.duration;
                        contractDomain.Customer_idCustomer = contract.Customer_idCustomer;
                        contractDomain.endorsementSettlementDates = contract.endorsementSettlementDates;
                        contractDomain.paymentsSettlement = contract.paymentsSettlement;
                        contractDomain.paymentsEndorsement = contract.paymentsEndorsement;
                        contractDomain.loanProcentage = contract.loanProcentage;
                        contractDomain.totalAnnualCost = contract.totalAnnualCost;
                        contractDomain.annualInterestRate = contract.annualInterestRate;
                        contractDomain.Belongings = contract.Belongings.ToList();
                        contractDomain.Operations = contract.Operations.ToList();
                        contractDomain.Customer = contract.Customer;
                    }
                    else
                    {
                        contractDomain = null;
                    }

                    return contractDomain;
                }
            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }
        }

        public static List<Domain.CompleteContract> RecoverContracts()
        {
            NewLog _log = new NewLog();
            List<Domain.CompleteContract> resultContracts = new List<Domain.CompleteContract>();
            try
            {
                using (var database = new ConnectionModel())
                {
                    var contract = (from Contract in database.Contracts select Contract).ToList();

                    foreach (DataAcces.Contract contractOne in contract)
                    {
                        Domain.CompleteContract newContract = new Domain.CompleteContract();
                        newContract.idContract = contractOne.idContract;
                        newContract.idCustomer = contractOne.Customer_idCustomer;
                        newContract.stateContract = contractOne.stateContract;
                        DataAcces.Customer newCustomer = new DataAcces.Customer();
                        newCustomer = CustomerDAO.FindCustomerById(contractOne.Customer_idCustomer);
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

        public static Contract GetContract(int idContract)
        {
            var result = new Contract();
            if (Utilities.VerifyConnection())
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

        public static int DeletePendingContrat(int idContract)
        {
            int result = 0;
            if (Utilities.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        var contractTemp = connection.Contracts.Find(idContract);
                        connection.Contracts.Remove(contractTemp);
                        connection.SaveChanges();
                        result = MessageCode.SUCCESS;
                    }
                }
                catch (ArgumentException)
                {
                    result = MessageCode.ERROR_UPDATE;
                }
            }
            else
                result = MessageCode.CONNECTION_ERROR;
            return result;
        }

        public static int activeContract(int idContract)
        {
            int result = 0;
            if(Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    Contract temp = connection.Contracts.Find(idContract);

                    temp.stateContract = StatesContract.ACTIVED_CONTRACT;
                    connection.SaveChanges();
                    result = MessageCode.SUCCESS;

                }
            }else
                result = MessageCode.CONNECTION_ERROR;
            return result;
        }

    }
}

