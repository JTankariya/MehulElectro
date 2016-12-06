using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class GrindingMaterial
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProcessName { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string MinQty { get; set; }
        public string MaxQty { get; set; }
        public string ReorderQty { get; set; }
        public decimal Qty { get; set; }
        public string StockQty { get; set; }
    }
}
