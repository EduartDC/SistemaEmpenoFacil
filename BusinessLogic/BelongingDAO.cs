using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BelongingDAO
    {
        public static int ReleaseBelongings()
        {
            var returl = MessageCode.ERROR;

            if (Utilitys.VerifyConnection())
            {

            }
            return returl;
        }

        public static List<Belonging> GetAllBelongingsFromContract(int idContract)
        {
            List<Belonging> belongings = new List<Belonging>();
            if (Utilitys.VerifyConnection())
            {
                using (var connection = new ConnectionModel())
                {
                    belongings = connection.Belongings.Where(b => b.Contract_idContract == idContract).ToList();
                }
            }
            else
            {
                throw new Exception(MessageError.CONNECTION_ERROR);
            }
            return belongings;
        }
    }
}
