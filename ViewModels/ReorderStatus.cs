using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class ReorderStatus
    {
        public decimal ReorderQty { get; set; }
        public decimal MinQty { get; set; }
        public decimal MaxQty { get; set; }
        public decimal ClosingQty { get; set; }
        public string ProductName { get; set; }
        public string PrintName { get; set; }
        public string ShadeName { get; set; }
        public string PackingName { get; set; }
    }
}
