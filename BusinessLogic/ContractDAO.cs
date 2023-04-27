using DataAcces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ContractDAO
    {
        public static int LiquidateContract(Contract selectedContract)
        {
            var resutl = MessageCode.ERROR;
            if (Utilitys.VerifyConnection())
            {
            }
            return resutl;

        }

        public static (int ,int) RegisterContract(Contract contract)
        {
            int idObject = 0;
            if (Utilitys.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        connection.Contracts.Add(contract);
                        connection.SaveChanges();
                        idObject = contract.idContract;
                    }
                }
                catch(DbUpdateException)
                {
                    return (MessageCode.ERROR, idObject);
                }
            }
            else
                return (MessageCode.CONNECTION_ERROR, idObject);
            return (MessageCode.SUCCESS, idObject);
        }

    }
}
