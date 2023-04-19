using DataAcces;
using System;
using System.Collections.Generic;
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

        public static Metric GetMetrics()
        {
            Metric newMetric = new Metric();
            try
            {
                using (var dataBase = new ConnectionModel())
                {
                    Metric metrics = dataBase.Metrics.First();
                    if (metrics != null)
                    {
                        newMetric.IVA = metrics.IVA;
                        newMetric.interestRate = metrics.interestRate;
                    }
                } 
            }
            catch (Exception ex)
            {
                newMetric = null;
                return newMetric;
            }
            return newMetric;
        }

    }
}
