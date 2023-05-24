using BusinessLogic;
using DataAcces;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace UnitTest
{
    [TestClass]
    public class CustomerDAOTest
    {
        public DataAcces.Customer newCustomer { get; set; }
        public List<ImagesIdentification> imagesIdentifications { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {

            newCustomer = new DataAcces.Customer();
            imagesIdentifications = new List<ImagesIdentification>();
            newCustomer.blackList = false;
            newCustomer.firstName = "Test";
            newCustomer.lastName = "Test";
            newCustomer.idCustomer = 1;
            newCustomer.curp = "DCFFT25632DH43Y00";
            newCustomer.address = "San Pedro #405, Queretaro, Qro ";
            newCustomer.telephonNumber = 2256318989335548933;
            newCustomer.identification = "INE";
            string imageRuteOne = "C:/Users/super/source/repos/WindowsFormsApp1/NewEmpeños/SistemaEmpenoFacil/View/Icons/budget.png";
            byte[] bytesImagenOne = File.ReadAllBytes(imageRuteOne);
            ImagesIdentification imagesIdentificationOne = new ImagesIdentification()
            {
                imagen = bytesImagenOne,
                Customer_idCustomer = 20
            };
            string imageRuteTwo = "C:/Users/super/source/repos/WindowsFormsApp1/NewEmpeños/SistemaEmpenoFacil/View/Icons/Sale.png";
            byte[] bytesImagenTwo = File.ReadAllBytes(imageRuteTwo);
            ImagesIdentification imagesIdentificationTwo = new ImagesIdentification()
            {
                imagen = bytesImagenTwo,
                Customer_idCustomer = 20
            };
            imagesIdentifications.Add(imagesIdentificationOne);
            imagesIdentifications.Add(imagesIdentificationTwo);
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

        [TestMethod]
        public void TestRecoverCustomersSuccess()
        {
            List<Domain.Customer> resultCustomers = CustomerDAO.RecoverCustomers();
            int resultObtined = resultCustomers.Count;
            Assert.AreEqual(7, resultObtined);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRecoverCustomersErrorArgumentNullException()
        {
            List<Domain.Customer> resultCustomers = CustomerDAO.RecoverCustomers();
            int resultObtined = resultCustomers.Count;
            Assert.AreEqual(0, resultObtined);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestRecoverCustomersError()
        {
            List<Domain.Customer> resultCustomers = CustomerDAO.RecoverCustomers();
            int resultObtined = resultCustomers.Count;
            Assert.AreEqual(0, resultObtined);
        }

        [TestMethod]
        public void TestExistCustomerSuccess()
        {
            int resultObtined = CustomerDAO.ExistCustomer("PAVA021001HVZNLXA0");
            Assert.AreEqual(200, resultObtined);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExistCustomerErrorArgumentNullException()
        {
            int resultObtined = CustomerDAO.ExistCustomer(null);
            Assert.AreEqual(500, resultObtined);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestExistCustomerError()
        {
            int resultObtined = CustomerDAO.ExistCustomer("PAVA021001HVZNLXA0");
            Assert.AreEqual(500, resultObtined);
        }

        [TestMethod]
        public void TestChangeStatusSuccess()
        {
            int resultObtined = CustomerDAO.ChangeStatusBlackList("MAHE021025HUJNKIQ7");
            Assert.AreEqual(200, resultObtined);
        }

        [TestMethod]
        public void TestChangeStatusClientInBlackList()
        {
            int resultObtined = CustomerDAO.ChangeStatusBlackList("MAHE021025HUJNKIQ7");
            Assert.AreEqual(502, resultObtined);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestChangeStatusErrorArgumentNullException()
        {
            int resultObtined = CustomerDAO.ChangeStatusBlackList(null);
            Assert.AreEqual(500, resultObtined);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestChangeStatusCustomerError()
        {
            int resultObtined = CustomerDAO.ChangeStatusBlackList("PAVA021001HVZNLXA0");
            Assert.AreEqual(500, resultObtined);
        }

        [TestMethod]
        public void TestAddCustomerSuccess()
        {
            DataAcces.Customer customer = new DataAcces.Customer()
            {
                firstName = "Lorena",
                lastName = "Martinez Hernandez",
                curp = "MAHE010203MIKJANH9",
                blackList = false,
                telephonNumber = 2281568457,
                address = "78 de la calle nueva",
                cumulativeProfit = "0",
                identification = "INE"
            };
            (int resultObtined, int idCustomerObtined) = CustomerDAO.AddCustomer(customer);
            Assert.AreEqual(200, resultObtined);
            Assert.AreEqual(23, idCustomerObtined);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddCustomerError()
        {
            DataAcces.Customer customer = new DataAcces.Customer()
            {
                firstName = "Lorena",
                lastName = "Martinez Hernandez",
                curp = "MAHE010203MIKJANH9",
                blackList = false,
                telephonNumber = 2281568457,
                address = "78 de la calle nueva",
                cumulativeProfit = "0",
                identification = "INE"
            };
            (int resultObtined, int idCustomerObtined) = CustomerDAO.AddCustomer(customer);
            Assert.AreEqual(500, resultObtined);
            Assert.AreEqual(0, idCustomerObtined);
        }


        [TestMethod]
        public void TestAddTwoImageIdentificationSuccess()
        {
            Assert.AreEqual(200, CustomerDAO.AddTwoImageIdentification(imagesIdentifications));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddTwoImageIdentificationException()
        {
            Assert.AreEqual(500, CustomerDAO.AddTwoImageIdentification(imagesIdentifications));
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void TestAddTwoImageIdentification()
        {
            Assert.AreEqual(500, CustomerDAO.AddTwoImageIdentification(imagesIdentifications));
        }

    }
}
