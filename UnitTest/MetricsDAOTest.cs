using BusinessLogic;
using DataAcces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
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
            MetricsForTest metricsExpected = new MetricsForTest(1,"20", "16");
            Assert.AreEqual(metricsExpected.interestRate, metricsForTest.interestRate);
            Assert.AreEqual(metricsExpected.IVA, metricsForTest.IVA);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestRecoverMetricsFailure() 
        {
            Metric metric = MetricsDAO.RecoverMetrics();
            MetricsForTest metricsForTest = new MetricsForTest(metric.idMetrics, metric.interestRate, metric.IVA);
            MetricsForTest metricsExpected = new MetricsForTest();
            Assert.AreEqual(metricsExpected.interestRate, metricsForTest.interestRate);
            Assert.AreEqual(metricsExpected.IVA, metricsForTest.IVA);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRecoverMetricsFailureInvalidOperation()
        {
            Metric metric = MetricsDAO.RecoverMetrics();
            MetricsForTest metricsForTest = new MetricsForTest(metric.idMetrics, metric.interestRate, metric.IVA);
            MetricsForTest metricsExpected = new MetricsForTest();
            Assert.AreEqual(metricsExpected.interestRate, metricsForTest.interestRate);
            Assert.AreEqual(metricsExpected.IVA, metricsForTest.IVA);
        }

        [TestMethod]
        public void TestRegisterMetricsSuccess()
        {
            int resultForTest = MetricsDAO.RegisterMetrics("20", "16");
            int resultExpected = 200;
            Assert.AreEqual(resultExpected, resultForTest);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestRegisterMetricsFailure()
        {
            int resultForTest = MetricsDAO.RegisterMetrics("20", "16");
            int resultExpected = 400;
            Assert.AreEqual(resultExpected, resultForTest);
        }

        [TestMethod]
        public void TestUpdateMetricsSuccess()
        {
            int resultForTest = MetricsDAO.UpdateMetrics("16", "20");
            int resultExpected = 200;
            Assert.AreEqual(resultExpected, resultForTest);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestUpdateMetricsFailure()
        {
            int resultForTest = MetricsDAO.RegisterMetrics("20", "16");
            int resultExpected = 500;
            Assert.AreEqual(resultExpected, resultForTest);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestUpdateMetricsFailureInvalidOperation()
        {
            int resultForTest = MetricsDAO.RegisterMetrics("20", "16");
            int resultExpected = 400;
            Assert.AreEqual(resultExpected, resultForTest);
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
