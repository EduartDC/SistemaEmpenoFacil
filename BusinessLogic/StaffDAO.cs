using BusinessLogic.Utility;
using DataAcces;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.Data.Entity.Core;
>>>>>>> 0ef6619f57abd0b5b64d28606494fdfefb75c721
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

<<<<<<< HEAD
        public static int modifyStaff(string userName, Staff staffModified)
        {
            var resutl = MessageCode.ERROR_UPDATE;
            if (Utilities.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        var oldStaff = connection.Staffs.Find(userName);
                        oldStaff = staffModified;
                        connection.Entry(oldStaff).State = System.Data.Entity.EntityState.Modified;
                        resutl = connection.SaveChanges();
                        return resutl;
                    }
                }
                catch (DbUpdateException)
                {
                    return resutl;
                }
            }
            return resutl;
        }
=======
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
                        rol = newStaff.rol
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

>>>>>>> 0ef6619f57abd0b5b64d28606494fdfefb75c721
    }
}