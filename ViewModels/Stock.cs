using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class Stock
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string PrintName { get; set; }
        public string ShadeName { get; set; }
        public decimal OpeningQty { get; set; }
        public decimal OpeningRate { get; set; }
        public decimal INQty { get; set; }
        public decimal OUTQty { get; set; }
        public decimal ClosingQty { get; set; }
        public string PackingName { get; set; }
        public int ShadeID { get; set; }
        public int PackingID { get; set; }
        public int OrderID { get; set; }
        public int InwardID { get; set; }
        public int DispatchID { get; set; }
        public int BatchID { get; set; }
        public decimal Qty { get; set; }
        public bool InOut { get; set; }
        public DateTime InOutDate { get; set; }
    }
}
