using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class InwardDetail
    {
        public int ID { get; set; }
        public int InwardID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Qty { get; set; }
        public string Rate { get; set; }
        public string Total { get; set; }
        public bool IsDeleted { get; set; }
    }
}
