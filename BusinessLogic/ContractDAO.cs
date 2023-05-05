using DataAcces;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ContractDAO
    {
        public static int LiquidateContract(ContractDomain selectedContract)
        {
            var result = MessageCode.ERROR;
            try
            {
                if (Utilitys.VerifyConnection())
                {
                    using (var connection = new ConnectionModel())
                    {
                        var contract = connection.Contracts.Find(selectedContract.idContract);
                        contract.stateContract = selectedContract.stateContract;
                        contract.settlementAmount = selectedContract.settlementAmount;
                        result = connection.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException)
            {
                result = MessageCode.ERROR_UPDATE;
            }
            catch (Exception)
            {
                result = MessageCode.CONNECTION_ERROR;
            }
            return result;
        }

        public static (int, int) RegisterContract(Contract contract)
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

        //<<<<<<< HEAD
        public static int ModifyContract(Contract contract, int idContract)
        {
            var result = MessageCode.ERROR;
            if (Utilitys.VerifyConnection())
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
//=======
        public static async Task<ContractDomain> GetContractsDomainAsync(int idContrac)
        {
            if (!Utilitys.VerifyConnection())
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }

            using (var connection = new ConnectionModel())
            {
                var contract = await connection.Contracts.FindAsync(idContrac);

                if (contract == null)
                {
                    return null;
                }

                var contractDomain = new ContractDomain();
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

                return contractDomain;
            }
//>>>>>>> 45b1da3c38323e286c1a098bbd6674afaf12ca02
        }

    }
}
