using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class ProductParty
    {
        public int ID { get; set;}
        public int ProductID { get; set; }
        public int ShadeID { get; set; }
        public int ProductShadeID { get; set; }
        public int PartyID { get; set; }
        public string PartyName { get; set; }
        public int ProductPackingID { get; set; }
    }
}
