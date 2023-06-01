using BusinessLogic;
using BusinessLogic.Utility;
using DataAcces;
using Domain;
using Domain.BelongingCreation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
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


        //jonathan
        //Cu crear contrato
        [TestMethod]
        public void TestCreateContractConnectionError()
        {
            int code = MessageCode.CONNECTION_ERROR;
            DataAcces.Contract contract = new DataAcces.Contract();
            var resultContract = ContractDAO.RegisterContract(contract);
            Assert.AreEqual((code, 0), resultContract);
        }
        [TestMethod]
        public void TestGetContractSucess()
        {
            int idContract = 13;
            DataAcces.Contract contract = ContractDAO.GetContract(idContract);
            Assert.AreNotEqual(null, contract);
        }

        [TestMethod]
        public void TestGetContractFailed()
        {
            int idContract = 0;
            DataAcces.Contract contract = ContractDAO.GetContract(idContract);
            Assert.AreEqual(null, contract);
        }

        [TestMethod]
        public void TestModifyContract()
        {
            DataAcces.Contract contract = new DataAcces.Contract();
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

        [TestMethod]
        public void TestRecoverContractsByDatesSuccess()
        {
            Assert.IsNotNull(ContractDAO.GetContractsByDate(DateTime.Parse("01/05/2023 12:00:00 a. m."), DateTime.Parse("30/05/2023 12:00:00 a. m.")));
        }

        [TestMethod]
        public void TestRecoverContractsByDatesNoContracts()
        {
            Assert.IsNull(ContractDAO.GetContractsByDate(DateTime.Parse("01/06/2023 12:00:00 a. m."), DateTime.Parse("30/06/2023 12:00:00 a. m.")));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestRecoverContractsByDatesException()
        {
            Assert.IsNull(ContractDAO.GetContractsByDate(DateTime.Parse("01/05/2023 12:00:00 a. m."), DateTime.Parse("30/05/2023 12:00:00 a. m.")));

        }


        //Jonathan
        //CU crear contrato
        [TestMethod]
        public void testCreateContract()
        {
            DataAcces.Contract contractTemp= new DataAcces.Contract();
            contractTemp.loanAmount = 3000;
            contractTemp.deadlineDate = DateTime.Parse("2023-08-26 11:22:20.383");
            contractTemp.creationDate = DateTime.Now;
            contractTemp.stateContract = "Pendiente";
            contractTemp.iva = 16;
            contractTemp.interestRate = 8;
            contractTemp.renewalFee = 0;
            contractTemp.settlementAmount = 0;
            contractTemp.duration = 3;
            contractTemp.Customer_idCustomer = 1;
            contractTemp.endorsementSettlementDates = "26/06/2023; 26/07/2023; 26/08/2023;";
            contractTemp.paymentsSettlement = "4260; 5040; 5820;";
            contractTemp.paymentsEndorsement = "1260; 2040; 2820;";
            var result = ContractDAO.RegisterContract(contractTemp); //devuelver id 24 de contrato
            Assert.AreEqual((24,MessageCode.SUCCESS),result);
        }

        //Jonathan
        //CU crear contrato
        [TestMethod]
        public void testCreateContractConnectionError()
        {
            DataAcces.Contract contractTemp = new DataAcces.Contract();
            contractTemp.loanAmount = 3000;
            contractTemp.deadlineDate = DateTime.Parse("2023-08-26 11:22:20.383");
            contractTemp.creationDate = DateTime.Now;
            contractTemp.stateContract = "Pendiente";
            contractTemp.iva = 16;
            contractTemp.interestRate = 8;
            contractTemp.renewalFee = 0;
            contractTemp.settlementAmount = 0;
            contractTemp.duration = 3;
            contractTemp.Customer_idCustomer = 1;
            contractTemp.endorsementSettlementDates = "26/06/2023; 26/07/2023; 26/08/2023;";
            contractTemp.paymentsSettlement = "4260; 5040; 5820;";
            contractTemp.paymentsEndorsement = "1260; 2040; 2820;";
            var result = ContractDAO.RegisterContract(contractTemp); //devuelver id 24 de contrato
            Assert.AreEqual(MessageCode.CONNECTION_ERROR, result.Item1);
        }

        //Jonathan
        //CU crear contratos
        [TestMethod]
        public void testRegisterBelongings()
        {
            List<DataAcces.Belonging> belongingList = new List<DataAcces.Belonging>();
            DataAcces.Belonging util = new DataAcces.Belonging();
            util.category = "Informatica";
            util.characteristics = "8gb de ram, ssd256 gb,procesador i3";
            util.serialNumber = "80000";
            util.appraisalValue = 2000;
            util.loanAmount = 1500;
            util.description = "laptop acer";
            util.Contract_idContract = 24;
            util.model = null;
            belongingList.Add(util);
            var result = BelongingDAO.SaveBelongings(belongingList);
            Assert.AreEqual((MessageCode.SUCCESS,1),(result.Item1,result.Item2.Count));
        }


        //Jonathan
        //CU crear contratos
        [TestMethod]
        public void testRegisterBelongingsConnectionError()
        {
            List<DataAcces.Belonging> belongingList = new List<DataAcces.Belonging>();
            DataAcces.Belonging util = new DataAcces.Belonging();
            util.category = "Informatica";
            util.characteristics = "8gb de ram, ssd256 gb,procesador i3";
            util.serialNumber = "80000";
            util.appraisalValue = 2000;
            util.loanAmount = 1500;
            util.description = "laptop acer";
            util.Contract_idContract = 24;
            util.model = null;
            belongingList.Add(util);
            var result = BelongingDAO.SaveBelongings(belongingList);
            Assert.AreEqual(MessageCode.CONNECTION_ERROR, result.Item1);
        }

        //Jonathan
        //CU crear contrato
        [TestMethod]
        public void SaveimagesBelongings()
        {
            List<ImagesBelonging> imageList = new List<ImagesBelonging>();

            string path = "C:/Users/jon10/OneDrive/Imágenes/IMG/Wallpapers/chemms";
            byte[] bytes;

            try
            {
                bytes = File.ReadAllBytes(path);
                ImagesBelonging image = new ImagesBelonging();
                image.imagen = bytes;
                image.Belonging_idBelonging = 24;
                var result = BelongingDAO.SaveimagesBelongings(imageList);
                Assert.AreEqual((MessageCode.SUCCESS,1),(result.Item1,result.Item2.Count));
            }
            catch (IOException e)
            {
                Console.WriteLine("Error al leer el archivo: " + e.Message);
            }
        }

        //Jonathan
        //CU crear contrato
        [TestMethod]
        public void SaveimagesBelongingsConnectionError()
        {
            List<ImagesBelonging> imageList = new List<ImagesBelonging>();

            string path = "C:/Users/jon10/OneDrive/Imágenes/IMG/Wallpapers/chemms";
            byte[] bytes;

            try
            {
                bytes = File.ReadAllBytes(path);
                ImagesBelonging image = new ImagesBelonging();
                image.imagen = bytes;
                image.Belonging_idBelonging = 24;
                var result = BelongingDAO.SaveimagesBelongings(imageList);
                Assert.AreEqual(MessageCode.CONNECTION_ERROR, result.Item1);
            }
            catch (IOException e)
            {
                Console.WriteLine("Error al leer el archivo: " + e.Message);
            }
        }


        [TestMethod]
        public void TestReactiveContract()
        {
            int idContract =9 ;
            var contract = ContractDAO.ReactiveContract(idContract);
            int expectedResult = 1;
            Assert.AreEqual(expectedResult, contract);
        }
        [TestMethod]
        [ExpectedException(typeof (NullReferenceException))]
        public void TestReactiveContractFailed()
        {
            int idContract = 0;
            var contract = ContractDAO.ReactiveContract(idContract);
            int expectedResult = 0;
            Assert.AreEqual(expectedResult, contract);
        }

    }
}
