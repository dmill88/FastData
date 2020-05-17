using System;
using System.Collections.Generic;
using System.Text;

namespace FastData.Logic
{
    public class SalesOrder
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int SalespersonPersonID { get; set; }
        public int ContactPersonID { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
