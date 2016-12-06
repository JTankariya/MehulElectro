using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class Order
    {
        public int ID { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string PartyOrderNo { get; set; }
        public DateTime? PartyOrderDate { get; set; }
        public int PartyID { get; set; }
        public string PartyName { get; set; }
        public int DeliveryAddressID { get; set; }
        public string DeliveryAddress { get; set; }
        public int TransportID { get; set; }
        public string Transport { get; set; }
        public string BookingAt { get; set; }
        public string PartyBookingCity { get; set; }
        public string PaymentNarr { get; set; }
        public string DeliveryNarr { get; set; }
        public string BillingNarr { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public string Total { get; set; }
        public bool IsClosed { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public IList<OrderDetail> orderDetail { get; set; }
    }
}
