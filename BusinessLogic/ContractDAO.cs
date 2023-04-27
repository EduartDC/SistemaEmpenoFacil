﻿using DataAcces;
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

        public static int RegisterContract(Contract contract)
        {
            if (Utilitys.VerifyConnection())
            {
                try
                {
                    using (var connection = new ConnectionModel())
                    {
                        connection.Contracts.Add(contract);
                        connection.SaveChanges();
                    }
                }
                catch(DbUpdateException)
                {
                    return MessageCode.ERROR;
                }
            }
            else
                return MessageCode.CONNECTION_ERROR;
            return MessageCode.SUCCESS;
        }

        public static Contract GetContract(int idContract)
        {
            Contract contract= null;
            if (Utilitys.VerifyConnection())
            {
                try
                {
                    using(var connection = new ConnectionModel())
                    {
                        contract = connection.Contracts.Find(idContract);
                    }
                }
                catch(DbUpdateException)
                {
                    return contract;
                }
            }
            return contract;
        }

        public static int ModifyContract(Contract contract, int idContract)
        {
            var result = MessageCode.ERROR;
            if (Utilitys.VerifyConnection())
            {
                try{
                    using(var connection = new ConnectionModel())
                    {
                        var oldContract  = connection.Contracts.Find(idContract);
                        oldContract = contract;
                        connection.Entry(oldContract).State = System.Data.Entity.EntityState.Modified;
                        result = connection.SaveChanges();
                        return result;
                    }
                }
                catch(DbUpdateException)
                {
                    return result;
                }
            }
            return result;
        }

    }
}
