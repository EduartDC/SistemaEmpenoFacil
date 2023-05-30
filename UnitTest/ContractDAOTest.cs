using BusinessLogic;
using DataAcces;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class ContractDAOTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            //se crean los objetos que se van a utilizar en los test

        }
        [TestMethod]
        public void TestLiquidateContractSuccess()
        {
            ContractDomain contractDomain = new ContractDomain();
            contractDomain.idContract = 11;
            contractDomain.stateContract = "Activo";
            contractDomain.settlementAmount = 10000;
            int expectedResult = 1;
            Assert.AreEqual(expectedResult, ContractDAO.LiquidateContract(contractDomain));
        }
        [TestMethod]
        public void TestLiquidateContractError()
        {
            ContractDomain contractDomain = new ContractDomain();
            contractDomain.idContract = 1;
            contractDomain.stateContract = "Liquidado";
            contractDomain.settlementAmount = 10000;

            int expectedResult = 0;
            Assert.AreEqual(expectedResult, ContractDAO.LiquidateContract(contractDomain));
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestLiquidateContractException()
        {
            ContractDomain contractDomain = new ContractDomain();
            contractDomain.idContract = 0;
            contractDomain.stateContract = "Liquidado";
            contractDomain.settlementAmount = 10000;

            int expectedResult = 0;
            Assert.AreEqual(expectedResult, ContractDAO.LiquidateContract(contractDomain));
        }

        [TestMethod]
        public async Task TestGetContractsDomainAsyncSuccess()
        {
            Assert.IsNotNull(await Task.Run(() => ContractDAO.GetContractsDomainAsync(9)));
        }

        [TestMethod]
        public async Task TestGetContractsDomainAsyncError()
        {

            Assert.IsNull(await Task.Run(() => ContractDAO.GetContractsDomainAsync(1)));
        }

        [TestMethod]
        public async Task TestGetContractsDomainAsyncException()
        {

            Assert.IsNull(await Task.Run(() => ContractDAO.GetContractsDomainAsync(0)));
        }

        [TestMethod]
        public void TestRecoverContractsSuccess()
        {
            List<Domain.CompleteContract> resultContracts = ContractDAO.RecoverContracts();
            int resultObtined = resultContracts.Count;
            Assert.AreEqual(11,resultObtined );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRecoverContractsErrorArgumentNullException()
        {
            List<Domain.CompleteContract> completeContracts = ContractDAO.RecoverContracts();
            int resultObtined = completeContracts.Count;
            Assert.AreEqual(0, resultObtined);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestRecoverContractsError()
        {
            List<Domain.CompleteContract> completeContracts = ContractDAO.RecoverContracts();
            int resultObtined = completeContracts.Count;
            Assert.AreEqual(0, resultObtined);
        }

        [TestMethod]
        public void TestGetContractSucess()
        {
            int idContract = 13;
            Contract contract = ContractDAO.GetContract(idContract);
            Assert.AreNotEqual(null, contract);
        }

        [TestMethod]
        public void TestGetContractFailed()
        {
            int idContract = 0;
            Contract contract = ContractDAO.GetContract(idContract);
            Assert.AreEqual(null, contract);
        }

        [TestMethod]
        public void TestModifyContract()
        {
            Contract contract = new Contract();
            contract.duration = 6;
            contract.loanAmount = 6500;
            contract.deadlineDate = DateTime.Now.AddMonths(contract.duration).AddDays(15);
            contract.creationDate = DateTime.Now;
            contract.stateContract = "Activo";
            contract.iva = 16;
            contract.interestRate = 8;
            contract.renewalFee = 0;
            contract.settlementAmount= 0;
            contract.Customer_idCustomer = 14;
            contract.endorsementSettlementDates = "- 29/07/2023;- 29/08/2023;- 29/09/2023;";
            contract.paymentsSettlement = "3720;3960;4200;";
            contract.paymentsEndorsement = "720;960;1200;";
            contract.totalAnnualCost = "8";
            int idContract = 14;
            int ExpectedResult = 1;
            int response = ContractDAO.ModifyContract(contract, idContract);
            Assert.AreEqual(ExpectedResult, response);
        }
    }
}
