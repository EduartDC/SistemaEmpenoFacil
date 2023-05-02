using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class StaffDAO
    {
        public static Staff LogingStaff(string userName, string password)
        {
            var resutl = new Staff();
            if (Utilitys.VerifyConnection())
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

    }
}
