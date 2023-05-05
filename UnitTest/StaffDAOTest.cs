using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class StaffDAOTest
    {
        [TestMethod]
        public void TestLogingStaffSuccess()
        {
            Assert.IsNotNull(StaffDAO.LogingStaff("admin", "admin"));
        }

        [TestMethod]
        public void TestLogingStaffErrorUserName()
        {
            Assert.IsNull(StaffDAO.LogingStaff(" ", "admin"));
        }

        [TestMethod]
        public void TestLogingStaffErrorPassword()
        {
            Assert.IsNull(StaffDAO.LogingStaff("admin ", ""));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestLogingStaffErrorException()
        {
            Assert.IsNull(StaffDAO.LogingStaff(" ", ""));
        }
    }
}
