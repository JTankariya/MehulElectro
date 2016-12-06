using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class ReadyAvailableOrder
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int ShadeID { get; set; }
        public int PackingID { get; set; }
        public string Qty { get; set; }
        public string Rate { get; set; }
        public string Total { get; set; }
        public bool IsSoftDispatched { get; set; }
        public bool IsMovedToProduction { get; set; }
        public string OrderNo { get; set; }
        public string ProductName { get; set; }
        public string ShadeName { get; set; }
        public string PackingName { get; set; }
        public decimal AvailableQty { get; set; }

        public string PartyName { get; set; }
        public string PartyAddress { get; set; }
        public string TransportName { get; set; }
        public string BookingAt { get; set; }
    }
}
