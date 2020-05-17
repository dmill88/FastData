using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FastData.Util;

namespace FastData.Logic
{

    public class SampleSearches
    {
        public IEnumerable<SalesOrder> SalesOrders { get; }
        public Dictionary<(int OrderId, int CustomerId), SalesOrder> SalesOrderDictionary { get; }
        public Dictionary<(int OrderId, int CustomerId, DateTime OrderDate), SalesOrder> SalesOrderDictionary2 { get; }


        public SampleSearches()
        {
            CSVService csvService = new CSVService();
            
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SampleData\SalesOrders.csv");
            //path = @"F:\Sample Data Files\SalesOrders.csv";
            SalesOrders = csvService.OpenAndParseCsvFileAsync<SalesOrder>(path);
            SalesOrderDictionary = SalesOrders.ToDictionary(i => (i.OrderID, i.CustomerID));
            SalesOrderDictionary2 = SalesOrders.ToDictionary(i => (i.OrderID, i.CustomerID, i.OrderDate));
        }

        public SalesOrder GetSalesOrderByDictionary(int orderId, int customerId)
        {
            SalesOrder salesOrder = SalesOrderDictionary.ContainsKey((orderId, customerId)) ? SalesOrderDictionary[(orderId, customerId)] : null;
            return salesOrder;
        }

        public SalesOrder GetSalesOrderByDictionary(int orderId, int customerId, DateTime orderDate)
        {
            SalesOrder salesOrder = SalesOrderDictionary2.ContainsKey((orderId, customerId, orderDate)) ? SalesOrderDictionary2[(orderId, customerId, orderDate)] : null;
            return salesOrder;
        }

        public SalesOrder GetSalesOrderByLinq(int orderId, int customerId)
        {
            SalesOrder salesOrder = SalesOrders.FirstOrDefault(i => i.OrderID == orderId && i.CustomerID == customerId);
            return salesOrder;
        }

        public SalesOrder GetSalesOrderByLinq(int orderId, int customerId, DateTime orderDate)
        {
            SalesOrder salesOrder = SalesOrders.FirstOrDefault(i => i.OrderID == orderId && i.CustomerID == customerId && i.OrderDate == orderDate);
            return salesOrder;
        }

        public SalesOrder GetSalesOrderByCode(int orderId, int customerId)
        {
            SalesOrder salesOrder = null;
            foreach(var o in SalesOrders)
            {
                if (o.OrderID == orderId && o.CustomerID == customerId)
                {
                    salesOrder = o;
                    break;
                }
            }
            return salesOrder;
        }

        public SalesOrder GetSalesOrderByCode(int orderId, int customerId, DateTime orderDate)
        {
            SalesOrder salesOrder = null;
            foreach(var o in SalesOrders)
            {
                if (o.OrderID == orderId && o.CustomerID == customerId && o.OrderDate == orderDate)
                {
                    salesOrder = o;
                    break;
                }
            }
            return salesOrder;
        }

    }
}
