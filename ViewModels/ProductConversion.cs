using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class ProductConversion
    {
        public int ID { get; set; }
        public int DocNo { get; set; }
        public DateTime DocDate { get; set; }
        public int FromProductId { get; set; }
        public string FromProductName { get; set; }
        public int ToProductId { get; set; }
        public string ToProductName { get; set; }
        public int FromShadeId { get; set; }
        public string FromShadeName { get; set; }
        public int ToShadeId { get; set; }
        public string ToShadeName { get; set; }
        public int FromPackingId { get; set; }
        public string FromPackingName { get; set; }
        public int ToPackingId { get; set; }
        public string ToPackingName { get; set; }
        public decimal FromConvertQty { get; set; }
        public decimal ToConvertQty { get; set; }
    }
}
