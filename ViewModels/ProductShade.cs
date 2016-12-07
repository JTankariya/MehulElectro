using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class ProductShade
    {
        public int ID { get; set; }
        public int ShadeID { get; set; }
        public string ShadeName { get; set; }
        public int ProductID { get; set; }
        public int ProductPackingID { get; set; }
        public int PackingID { get; set; }
        public string OpeningQty { get; set; }
        public string OpeningRate { get; set; }
        public string OpeningValue { get; set; }
        public string PartyIDs { get; set; }
        public string ProductPackingTitle { get; set; }
    }
}
