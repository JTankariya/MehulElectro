using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class Batch
    {
        public int ID { get; set; }
        public string BatchNo { get; set; }
        public int BatchUnitID { get; set; }
        public string BatchUnit { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ShadeID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ShadeName { get; set; }
        public string ProductionQty { get; set; }
        public bool IsGrindingDone { get; set; }
        public bool HasHymenGuage { get; set; }
        public string MinHymenGuage { get; set; }
        public string MaxHymenGuage { get; set; }
        public bool HasPigmentDispersion { get; set; }
        public bool IsShadingDone { get; set; }
        public bool IsQCDone { get; set; }
        public int PackingProductID { get; set; }
        public bool IsPackingDone { get; set; }
        public string BatchMin { get; set; }
        public decimal OrderedQty { get; set; }
        public decimal WPL { get; set; }
        public string BatchMax { get; set; }
        public string BatchIdeal { get; set; }
        public int PackingID { get; set; }
        public string PackingName { get; set; }
        public string PanelNumber { get; set; }
        public string RackNumber { get; set; }
        public string ShadeMaching { get; set; }
        public string ConversionFactorWithLtr { get; set; }

        public DateTime? GrindingStartDate { get; set; }
        public DateTime? GrindingDoneDate { get; set; }
        public DateTime? ShadingStartDate { get; set; }
        public DateTime? ShadingDoneDate { get; set; }
        public DateTime? QCStartDate { get; set; }
        public DateTime? QCDoneDate { get; set; }
        public DateTime? PackingStartDate { get; set; }
        public DateTime? PackingDoneDate { get; set; }
        public string GrindingRemarks { get; set; }
        public string ShadingRemarks { get; set; }
        public string QCRemarks { get; set; }
        public string PackingRemarks { get; set; }
        public List<BatchDetail> BatchPackings { get; set; }
    }
}
