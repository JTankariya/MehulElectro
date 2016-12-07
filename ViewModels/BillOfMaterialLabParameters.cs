using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class BillOfMaterialLabParameters
    {
        public int ID { get; set; }
        public int BillOfMaterialID { get; set; }
        public int ParameterID { get; set; }
        public string LabParameterName { get; set; }
        public string Standard { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Unit { get; set; }
    }
}
