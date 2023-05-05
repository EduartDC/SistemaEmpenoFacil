using BusinessLogic;
using DataAcces;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class CustomerDAOTest
    {
        public DataAcces.Customer newCustomer { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {

            newCustomer = new DataAcces.Customer();
            newCustomer.blackList = false;
            newCustomer.firstName = "Test";
            newCustomer.lastName = "Test";
            newCustomer.idCustomer = 1;
            newCustomer.curp = "DCFFT25632DH43Y00";
            newCustomer.address = "San Pedro #405, Queretaro, Qro ";
            newCustomer.telephonNumber = 2256318989335548933;
            newCustomer.identification = "INE";

        }

        [TestMethod]
        public void TestUpdateCustomerSuccess()
        {
            int expectedResult = 1;
            Assert.AreEqual(expectedResult, CustomerDAO.UpdateCustomer(newCustomer));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestUpdateCustomerErrorEmpty()
        {
            int expectedResult = 0;
            newCustomer.idCustomer = 0;
            Assert.AreEqual(expectedResult, CustomerDAO.UpdateCustomer(newCustomer));
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestUpdateCustomerErrorException()
        {
            int expectedResult = 0;
            newCustomer.idCustomer = 0;
            Assert.AreEqual(expectedResult, CustomerDAO.UpdateCustomer(newCustomer));
        }

        [TestMethod]
        public void TestGetCustomerSuccess()
        {
            Assert.IsNotNull(CustomerDAO.GetCustomer(1));
        }

        [TestMethod]
        public void TestGetCustomerError()
        {
            Assert.IsNull(CustomerDAO.GetCustomer(0));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestGetCustomerException()
        {
            Assert.IsNull(CustomerDAO.GetCustomer(0));
        }

        [TestMethod]
        public void TestGetImagesCustomerSuccess()
        {
            List<ImagesIdentification> expectedResult = new List<ImagesIdentification>();
            for (int i = 0; i < 2; i++)
            {
                expectedResult.Add(new ImagesIdentification());
            }

            List<ImagesIdentification> actualResult = CustomerDAO.GetImagesCustomer(12);
            Assert.AreEqual(expectedResult.Count, actualResult.Count);
        }

        [TestMethod]
        public void TestGetImagesCustomerError()
        {
            List<ImagesIdentification> expectedResult = new List<ImagesIdentification>();
            List<ImagesIdentification> actualResult = CustomerDAO.GetImagesCustomer(1);
            Assert.AreEqual(expectedResult.Count, actualResult.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestGetImagesCustomerException()
        {
            List<ImagesIdentification> expectedResult = new List<ImagesIdentification>();
            List<ImagesIdentification> actualResult = CustomerDAO.GetImagesCustomer(0);
            Assert.AreEqual(expectedResult.Count, actualResult.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestUpdateImageCustomerError()
        {
            ImagesIdentification expectedResult = new ImagesIdentification();
            expectedResult.idImagenIdentification = 1;
            expectedResult.imagen = new byte[1];
            expectedResult.Customer_idCustomer = 1;
            Assert.AreEqual(expectedResult, CustomerDAO.UpdateImageCustomer(expectedResult));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestUpdateImageCustomerException()
        {
            ImagesIdentification expectedResult = new ImagesIdentification();
            Assert.AreEqual(expectedResult, CustomerDAO.UpdateImageCustomer(expectedResult));
        }

        [TestMethod]
        public void TestGetCustomerByCURPsuccess()
        {
            Assert.IsNotNull(CustomerDAO.GetCustomerByCURP("DICE52359"));
        }

        [TestMethod]
        public void TestGetCustomerByCURPError()
        {
            Assert.IsNull(CustomerDAO.GetCustomerByCURP("1"));
        }

        [TestMethod]
        public void TestGetCustomerByCURPErrorEmpty()
        {
            Assert.IsNull(CustomerDAO.GetCustomerByCURP(" "));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestGetCustomerByCURPException()
        {
            Assert.IsNull(CustomerDAO.GetCustomerByCURP("1"));
        }
    }
}
