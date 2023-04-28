using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class StaffDAO
    {
        public static (int, string) validateUser(string userName, string password)
        {
            if (Utilitys.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    List<Staff> list = connection.Staffs.ToList();

                    foreach (var staff in list)
                    {
                        if (staff.userName.Equals(userName) && staff.password.Equals(password))
                        {
                            return (MessageCode.SUCCESS, staff.rol);
                        }


                    }
                    return (MessageCode.ERROR_USER_NOT_FOUND, MessageError.USER_AND_PASSWORD_NOT_FOUND);
                }
            }
            else
                return (MessageCode.CONNECTION_ERROR, "");
        }
    }
}
