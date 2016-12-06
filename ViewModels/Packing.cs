using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class Packing
    {
        public int ID { get; set; }
        public int PackingID { get; set; }
        public string PackingName { get; set; }
        public string Name { get; set; }
        public string OpeningQty { get; set; }
        public string OpeningRate { get; set; }
        public string OpeningValue { get; set; }
        public int ProductID { get; set; }
        public string ConversionFactorWithLtr { get; set; }
        public string PartyIDs { get; set; }
    }
}
