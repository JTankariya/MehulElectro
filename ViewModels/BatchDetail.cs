using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class BatchDetail
    {
        public int ID { get; set; }
        public int BatchID { get; set; }
        public int PackingID { get; set; }
        public string PackingName { get; set; }
        public string Qty { get; set; }
    }
}
