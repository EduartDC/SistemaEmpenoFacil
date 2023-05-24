using BusinessLogic;
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
    }
}
