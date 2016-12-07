using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class Inward
    {
        public int ID { get; set; }
        public string InwardNo { get; set; }
        public DateTime InwardDate { get; set; }
        public int PartyID { get; set; }
        public string PartyName { get; set; }
        public string PaymentNarr { get; set; }
        public string DeliveryNarr { get; set; }
        public string BillingNarr { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public string Total { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public IList<InwardDetail> inwardDetail { get; set; }
    }
}
