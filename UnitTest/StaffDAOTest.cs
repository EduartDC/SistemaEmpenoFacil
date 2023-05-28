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
        [TestMethod]
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

        [TestMethod]
        public void TestRegisterStaff()
        {
            Staff staff = new Staff();
            staff.fisrtName = "David";
            staff.lastName = "Hernandez Rivera";
            staff.userName = "REV285";
            staff.password = "HALOcea206-";
            staff.statusStaff = "Activo";
            staff.rol = "Administrador";
            Assert.AreEqual(200, StaffDAO.RegisterStaff(staff));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestRegisterStaffException()
        {
            Staff staff = new Staff();
            staff.fisrtName = "David";
            staff.lastName = "Hernandez Rivera";
            staff.userName = "REV285";
            staff.password = "HALOcea206-";
            staff.statusStaff = "Activo";
            staff.rol = "Administrador";
            Assert.AreEqual(500, StaffDAO.RegisterStaff(staff));
        }



        [TestMethod]
        public void TestExistStaffSuccess()
        {
            Assert.AreEqual(200, StaffDAO.ExistStaff("ASDSDFSDF58"));
        }

        [TestMethod]
        public void TestStaffExistInDataBase()
        {
            Assert.AreEqual(100, StaffDAO.ExistStaff("PAVA0110026WA"));
        }

        [TestMethod]
        public void TestExistStaffError()
        {
            Assert.AreEqual(400, StaffDAO.ExistStaff("ASDSDFSDF58"));
        }


        [TestMethod]
        public void TestGetStaffByUserName()
        {
            Assert.IsNotNull(StaffDAO.GetStaffByUserName("Pana"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetStaffByUsernameInvalidOperation()
        {
            Assert.IsNull(StaffDAO.GetStaffByUserName("Nuevo"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestGetStaffByUsernameException()
        {
            Assert.IsNull(StaffDAO.GetStaffByUserName("Nuevo"));
        }

    }
}
