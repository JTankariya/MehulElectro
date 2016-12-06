using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProductGroupID { get; set; }
        public int RawMaterialTypeID { get; set; }
        public string ProductGroupName { get; set; }
        public string PrintName { get; set; }
        public int ProductUnitID { get; set; }
        public string ProuctUnitName { get; set; }
        public bool HasShade { get; set; }
        public bool HasPacking { get; set; }
        public string OpeningQty { get; set; }
        public string MinQty { get; set; }
        public string MaxQty { get; set; }
        public string ReorderQty { get; set; }
        public string OpeningRate { get; set; }
        public string OpeningValue { get; set; }
        public string Gravity { get; set; }
        public string SolidGravity { get; set; }
        public string SolidPercentage { get; set; }
        public string BatchMin { get; set; }
        public string BatchIdeal { get; set; }
        public string BatchMax { get; set; }
        public int BatchUnitID { get; set; }
        public string SalesRate { get; set; }
        public string PurchaseRate { get; set; }
        public string PartyIDs { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<Packing> Packings { get; set; }
        public List<ProductShade> Shades { get; set; }
        public List<ProductParty> Parties { get; set; }
    }
}
