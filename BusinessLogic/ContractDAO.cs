using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    internal class ContractDAO
    {
        public static int LiquidateContract(Contract selectedContract )
        {
            var resutl = CodeError.ERROR;
            if(Utilitys.VerifyConnection())
            {

            }
            return resutl;
        }
    }
}
