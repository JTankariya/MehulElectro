using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class ProductGroup
    {
        public int ID { get; set; }
        public int ProductGroupTypeID { get; set; }
        public string Name { get; set; }
        public string ProductGroupType { get; set; }   
    }
}
