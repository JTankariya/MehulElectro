using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class Dispatch
    {
        public int ID { get; set; }
        public string DONo { get; set; }
        public int OrderID { get; set; }
        public Order order { get; set; }
        public DateTime DODate { get; set; }
        public int PartyID { get; set; }
        public string PartyName { get; set; }
        public int DeliveryAddressID { get; set; }
        public int TransportID { get; set; }
        public string BookingAt { get; set; }
        public string LRNo { get; set; }
        public DateTime? LRDate { get; set; }
        public string GatePassNo { get; set; }
        public DateTime? GatePassDate { get; set; }
        public string Remarks { get; set; }
        public string Total { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public List<DispatchDetail> details { get; set; }

    }
}
