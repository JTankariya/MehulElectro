using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class BillOfMaterialDetail
    {
        public int ID { get; set; }
        public int BillOfMaterialID { get; set; }
        public int ProcessID { get; set; }
        public int ProductID { get; set; }
        public string QtyKG { get; set; }

    }
}
