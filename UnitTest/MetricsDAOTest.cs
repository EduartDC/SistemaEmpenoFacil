using BusinessLogic;
using DataAcces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class MetricsDAOTest
    {
        [TestMethod]
        public void TestRecoverMetricsSuccess()
        {
            Metric metric = MetricsDAO.RecoverMetrics();
            MetricsForTest metricsForTest = new MetricsForTest(metric.idMetrics, metric.interestRate, metric.IVA);
            MetricsForTest metricsExpected = new MetricsForTest(12, "52", "58");
            Assert.AreEqual(metricsExpected, metricsForTest);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestRecoverMetricsFailure() 
        {
            Metric metric = MetricsDAO.RecoverMetrics();
            MetricsForTest metricsForTest = new MetricsForTest(metric.idMetrics, metric.interestRate, metric.IVA);
            MetricsForTest metricsExpected = new MetricsForTest();
            Assert.AreEqual(metricsExpected, metricsForTest);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRecoverMetricsFailureInvalidOperation()
        {
            Metric metric = MetricsDAO.RecoverMetrics();
            MetricsForTest metricsForTest = new MetricsForTest(metric.idMetrics, metric.interestRate, metric.IVA);
            MetricsForTest metricsExpected = new MetricsForTest();
            Assert.AreEqual(metricsExpected, metricsForTest);
        }

        [TestMethod]
        public void TestUpdateMetricsSuccess()
        {

        }

    }

    public class MetricsForTest
    {
        public int idMetrics { get; set; }

        public string interestRate { get; set; }

        public string IVA { get; set; }

        public MetricsForTest()
        {
            idMetrics = 0;
            interestRate = "";
            IVA = "";
        }

        public MetricsForTest(int idMetrics, string interestRate, string IVA)
        {
            this.idMetrics = idMetrics;
            this.interestRate = interestRate;
            this.IVA = IVA;
        }
    }
}
