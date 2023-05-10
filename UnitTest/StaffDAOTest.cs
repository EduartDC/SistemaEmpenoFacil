using BusinessLogic;
using DataAcces;
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

        [TestMethod]
        public void TestGetStaffSuccess()
        {
            Assert.IsNotNull(StaffDAO.GetStaff(4));
        }

        public void TestGetStaffError()
        {
            Assert.IsNull(StaffDAO.GetStaff(-1));
        }

        [TestMethod]
        public void TestModifyStaffSuccess()
        {
            Staff staff = new Staff();
            staff.idStaff = 4;
            staff.fisrtName = "Hector David";
            staff.lastName = "Madrid Rivera";
            staff.userName = "REV09";
            staff.password = "HALOcea206-";
            staff.statusStaff = "Activo";
            staff.rol = "Cajero";
            Assert.AreEqual(1, StaffDAO.ModifyStaff(staff.idStaff, staff));
        }

        [TestMethod]
        public void TestModifyStaffErrorUpdate()
        {
            Staff staff = new Staff();
            staff.idStaff = -1;
            staff.fisrtName = "Hector David";
            staff.lastName = "Madrid Rivera";
            staff.userName = "REV09";
            staff.password = "HALOcea206-";
            staff.statusStaff = "Activo";
            staff.rol = "Cajero";
            Assert.AreEqual(300, StaffDAO.ModifyStaff(staff.idStaff, staff));
        }

        [TestMethod]
        public void TestModifyStaffErrorException()
        {
            Staff staff = new Staff();
            staff.idStaff = -1;
            staff.fisrtName = "Hector David";
            staff.lastName = "Madrid Rivera";
            staff.userName = "REV09";
            staff.password = "HALOcea206-";
            staff.statusStaff = "Activo";
            staff.rol = "Cajero";
            Assert.AreEqual(0, StaffDAO.ModifyStaff(staff.idStaff, staff));
        }
    }
}
