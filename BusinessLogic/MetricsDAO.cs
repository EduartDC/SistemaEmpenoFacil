using DataAcces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class MetricsDAO
    {
        public static Metric RecoverMetrics()
        {
            NewLog _log = new NewLog();
            Metric metric = new Metric();
            try
            {
                using (var dataBase = new ConnectionModel())
                {
                    Metric metricsObtained = dataBase.Metrics.First();
                    if (metricsObtained != null)
                    {
                        metric.IVA = metricsObtained.IVA;
                        metric.interestRate = metricsObtained.interestRate;
                    }
                }
            }
            catch (DataException ex)
            {
                _log.Add(ex.ToString());
                metric.IVA = "";
                metric.interestRate = "";
            }
            catch (InvalidOperationException ex)
            {
                _log.Add(ex.ToString());
                metric.IVA = "";
                metric.interestRate = "";
            }

            return metric;
        }

        public static int UpdateMetrics(string interestRate, string IVA)
        {
            int result = 200;
            NewLog _log = new NewLog();
            try
            {

                using (var dataBase = new ConnectionModel())
                {
                    var updateMetric = dataBase.Metrics.First();
                    updateMetric.interestRate = interestRate;
                    updateMetric.IVA = IVA;
                    dataBase.SaveChanges();

                }
            }
            catch (DataException ex)
            {
                result = 500;
                _log.Add(ex.ToString());
            }
            catch (InvalidOperationException ex)
            {
                _log.Add(ex.ToString());
                result = 400;
            }
            return result;
        }

        public static int RegisterMetrics(string newInterestRate, string newIVA)
        {
            int result = 200;
            NewLog _log = new NewLog();
            try
            {
                using (var dataBase1 = new ConnectionModel())
                {
                    var newMetrics = dataBase1.Metrics.Add(new Metric()
                    {
                        interestRate = newInterestRate,
                        IVA = newIVA
                    });
                    dataBase1.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                result = 500;
                _log.Add(ex.ToString());
            }
            catch (EntityException ex)
            {
                result=400;
                _log.Add(ex.ToString());
            }
            return result;
        }
    }
}
