using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class DispatchDetail
    {
        public int ID { get; set; }
        public int DispatchID { get; set; }
        public int OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ShadeID { get; set; }
        public string ShadeName { get; set; }
        public int PackingID { get; set; }
        public string PackingName { get; set; }
        public string Qty { get; set; }
        public string Rate { get; set; }
        public string Total { get; set; }
        public bool IsDeleted { get; set; }
    }
}
