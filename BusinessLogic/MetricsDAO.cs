﻿using DataAcces;
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
        static NewLog _log = new NewLog();
        public static Metric recoverMetrics()
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

        public static int updateMetrics(string interestRate, string IVA)
        {
            int result = 500;
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
                
                result = 200;
                _log.Add(ex.ToString());
            }
            catch (InvalidOperationException ex)
            {
                _log.Add(ex.ToString());
                result = registerMetrics(interestRate, IVA);
            }
            return result;
        }

        private static int registerMetrics(string newInterestRate, string newIVA)
        {
            int result = 500;
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
                result = 200;
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