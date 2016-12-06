using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class Party
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNo { get; set; }
        public string CSTNo { get; set; }
        public string LICNo { get; set; }
        public string PANNo { get; set; }
        public string TINNo { get; set; }
        public int PartyGroupID { get; set; }
        public string PartyGroup { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<PartyAddress> Addresses { get; set; }
    }
}
