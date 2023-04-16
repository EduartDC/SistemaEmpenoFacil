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
    }
}
