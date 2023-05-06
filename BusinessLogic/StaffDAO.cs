using BusinessLogic.Utility;
using DataAcces;
using System;
using System.Collections.Generic;
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

    }
}
