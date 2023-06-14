using BusinessLogic.Utility;
using DataAcces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class StaffDAO
    {
        //cide
        public static Staff LogingStaff(string userName, string password)
        {
            var resutl = new Staff();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    resutl = connection.Staffs.Where(s => s.userName == userName && s.password == password).FirstOrDefault();
                }
            }
            else
            {
                throw new Exception("Error de conexión");
            }
            return resutl;
        }

        public static int ModifyStaff(int userId, Staff staffModified)
        {
            var resutl = MessageCode.ERROR_UPDATE;
            if (Utilities.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        var oldStaff = connection.Staffs.Find(userId);
                        oldStaff.fisrtName = staffModified.fisrtName;
                        oldStaff.lastName = staffModified.lastName;
                        oldStaff.userName = staffModified.userName;
                        oldStaff.password = staffModified.password;
                        oldStaff.statusStaff = staffModified.statusStaff;
                        oldStaff.rol = staffModified.rol;
                        resutl = connection.SaveChanges();
                    }
                }
                catch (DbUpdateException)
                {
                    resutl = MessageCode.ERROR_UPDATE;
                }
            }
            return resutl;
        }
        public static int RegisterStaff(Staff newStaff)
        {
            int result = 200;
            try
            {
                using (var dataBaseOne = new ConnectionModel())
                {
                    var newMetrics = dataBaseOne.Staffs.Add(new Staff()
                    {
                        fisrtName = newStaff.fisrtName,
                        lastName = newStaff.lastName,
                        statusStaff = newStaff.statusStaff,
                        userName = newStaff.userName,
                        password = newStaff.password,
                        rol = newStaff.rol,
                        rfc = newStaff.rfc
                    });
                    dataBaseOne.SaveChanges();
                }
            }
            catch (Exception)
            {
                result = 500;
                throw new Exception();
            }
            return result;
        }

        

        public static Staff GetStaff(int idUser)
        {
            var result = new Staff();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {

                    result = connection.Staffs.Find(idUser);
                }
            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }
            return result;
        }

        public static int ExistStaff(string rfcRecived)
        {
            int result = 200;
            try
            {
                using (var dataBase = new ConnectionModel())
                {
                    var staffExist = (from Staff in dataBase.Staffs
                                      where Staff.rfc.Equals(rfcRecived)
                                      select Staff).Count();
                    if (staffExist > 0)
                    {
                        result = 100;
                    }
                }
            }
            catch (Exception)
            {
                result = 400;
                throw new Exception();
            }
            return result;
        }

        public static Staff GetStaffByUserName(string username)
        {
            Staff result = new Staff();
            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    try
                    {
                        result = connection.Staffs.Where(staff => staff.userName == username).First();
                    }
                    catch (InvalidOperationException)
                    {
                        result = null;
                    }
                }
            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }
            return result;
        }

        public static List<Staff> GetAllStaff()
        {
            List<Staff> result = new List<Staff>();
            if(Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    result = (from staff in connection.Staffs select staff).ToList();
                }
            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }
            return result;
        }
    }
}