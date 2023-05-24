using DataAcces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class MetricsDAO
    {
        public static Metric RecoverMetrics()
        {
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
            catch (InvalidOperationException )
            {
                metric.interestRate = "";
                metric.IVA = "";
                throw new InvalidOperationException();
            }
            catch (Exception)
            {
                metric.interestRate = "";
                metric.IVA = "";
                throw new Exception();
                
            }
            
            return metric;
        }

        public static int UpdateMetrics(string interestRate, string IVA)
        {
            int result = 200;
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
            catch (InvalidOperationException )
            {
                result = 400;
                throw new InvalidOperationException();
            }
            catch (Exception)
            {
                result = 500;
                throw new Exception();
            }
            return result;
        }

        public static int RegisterMetrics(string newInterestRate, string newIVA)
        {
            int result = 200;
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
            catch (Exception ex)
            {
                result=400;
                throw new Exception();
            }
            return result;
        }
    }
}
