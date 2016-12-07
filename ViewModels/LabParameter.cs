using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class LabParameter
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ValueTypeID { get; set; }
        public string ValueTypeName { get; set; }
    }
}
