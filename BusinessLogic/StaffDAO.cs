using BusinessLogic.Utility;
using DataAcces;
using System;
using System.Collections.Generic;
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
    }
}