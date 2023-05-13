using BusinessLogic.Utility;
using DataAcces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
//<<<<<<< HEAD
//=======
using System.Data.Entity.Core;
//>>>>>>> 0ef6619f57abd0b5b64d28606494fdfefb75c721
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

        //<<<<<<< HEAD
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
                    resutl = MessageCode.ERROR;
                }
            }
            return resutl;
        }
        //=======
        public static int RegisterStaff(Staff newStaff)
        {
            int result = 200;
            NewLog _log = new NewLog();
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
            catch (DbUpdateException ex)
            {
                result = 500;
                _log.Add(ex.ToString());
            }
            catch (EntityException ex)
            {
                result = 400;
                _log.Add(ex.ToString());
            }
            return result;
        }

        public static (int, Staff) validateStaff(string userName, string password)
        {

            if (Utilities.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    List<Staff> list = connection.Staffs.ToList();

                    foreach (var staff in list)
                    {
                        if (staff.userName.Equals(userName) && staff.password.Equals(password))
                        {
                            return (MessageCode.SUCCESS, staff);
                        }

                    }
                    return (MessageCode.ERROR_USER_NOT_FOUND, null);

                }
            }
            else
            {
                return (MessageCode.CONNECTION_ERROR, null);
            }
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
            NewLog _log = new NewLog();
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
            catch (ArgumentNullException ex)
            {
                _log.Add(ex.ToString());
            }
            catch (EntityException ex)
            {
                _log.Add(ex.ToString());
                result = 400;
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
                        result= null;
                    }
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