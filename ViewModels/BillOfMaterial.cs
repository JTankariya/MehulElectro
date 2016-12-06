using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class BillOfMaterial
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ShadeID { get; set; }
        public string ShadeName { get; set; }
        public int BatchUnitID { get; set; }
        public string BatchSize { get; set; }
        public string BOMNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RevisionNo { get; set; }
        public string Remarks { get; set; }
        public bool HasHymenGuage { get; set; }
        public string MinHymenGuage { get; set; }
        public string MaxHymenGuage { get; set; }
        public bool HasPigmentDispersion { get; set; }
        public string ShadeMaching { get; set; }
        public string PanelNumber { get; set; }
        public string RackNumber { get; set; }
        public string Loss { get; set; }
        public decimal WPL { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<BillOfMaterialDetail> details { get; set; }
        public List<BillOfMaterialLabParameters> labParameters { get; set; }
    }
}
