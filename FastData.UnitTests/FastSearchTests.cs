using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FastData.Logic;
using System.Diagnostics;
using System;

namespace FastData.UnitTests
{
    [TestClass]
    public class FastSearchTests
    {
        public SampleSearches SearchLogic { get; set; } = null;
        public List<(int OrderId, int CustomerId, DateTime OrderDate)> TestParameters = new List<(int OrderId, int CustomerId, DateTime OrderDate)>();

        public int CustomerId { get; set; }

        [TestInitializeAttribute]
        public void Init()
        {
            SearchLogic = new FastData.Logic.SampleSearches();
            PopulateTest3Params();
        }


        private void RunLookups(Func<int, int, DateTime, SalesOrder> searchFunction)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1; i++)
            {
                foreach (var p in TestParameters)
                {
                    SalesOrder salesOrder = searchFunction(p.OrderId, p.CustomerId, p.OrderDate);
                    Assert.IsNotNull(salesOrder);
                }
            }
            sw.Stop();
            Debug.WriteLine($"Execution time: {sw.ElapsedMilliseconds}");
        }

        [TestMethod]
        public void TestGetSalesOrderByLinq()
        {
            RunLookups(SearchLogic.GetSalesOrderByLinq);
        }

        [TestMethod]
        public void TestGetSalesOrderByCode()
        {
            RunLookups(SearchLogic.GetSalesOrderByCode);
        }

        [TestMethod]
        public void TestGetSalesOrderByDictionary()
        {
            RunLookups(SearchLogic.GetSalesOrderByDictionary);
        }

        private void PopulateTest3Params()
        {
            foreach(var saleOrder in SearchLogic.SalesOrders)
            {
                TestParameters.Add((saleOrder.OrderID, saleOrder.CustomerID, saleOrder.OrderDate));
            }
        }

    }
}
