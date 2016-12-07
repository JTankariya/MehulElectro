using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class BillOfMaterialLogic
    {

        public static IEnumerable<BillOfMaterial> GetBillOfMaterialByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetBillOfMaterialByID", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                var boms = DBHelper.ConvertToList<BillOfMaterial>(dt);
                if (boms != null)
                {
                    foreach (var bom in boms)
                    {
                        bom.details = BillOfMaterialDetailLogic.GetRawMaterialDetails(bom.ProductID, bom.ShadeID, "");
                        bom.labParameters = BillOfMaterialLabParametersLogic.GetLabParamaters(bom.ID);
                    }
                }
                return boms;
            }
            else
                return null;
        }

        public static int GetRevisionNo(int ProductID, int ShadeID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ProductID", ProductID);
            param.Add("@ShadeID", ShadeID);
            DataTable dt = DBHelper.GetDataTable("GetBillOfMaterialGetRevisionNo", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["RevisionNo"]) + 1;
            else
                return 1;
        }

        public static bool SaveBillOfMaterial(BillOfMaterial billOfMaterial)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", billOfMaterial.ID);
            param.Add("@ProductID", billOfMaterial.ProductID);
            param.Add("@ShadeID", billOfMaterial.ShadeID);
            param.Add("@BatchUnitID", billOfMaterial.BatchUnitID);
            param.Add("@BatchSize", billOfMaterial.BatchSize);
            param.Add("@BOMNo", billOfMaterial.BOMNo);
            param.Add("@RevisionNo", billOfMaterial.RevisionNo);
            param.Add("@Remarks", billOfMaterial.Remarks);
            param.Add("@HasHymenGuage", billOfMaterial.HasHymenGuage);
            param.Add("@MinHymenGuage", billOfMaterial.MinHymenGuage);
            param.Add("@MaxHymenGuage", billOfMaterial.MaxHymenGuage);
            param.Add("@HasPigmentDispersion", billOfMaterial.HasPigmentDispersion);
            param.Add("@ShadeMaching", billOfMaterial.ShadeMaching);
            param.Add("@PanelNumber", billOfMaterial.PanelNumber);
            param.Add("@RackNumber", billOfMaterial.RackNumber);
            param.Add("@CreatedBy", billOfMaterial.CreatedBy);
            param.Add("@UpdatedBy", billOfMaterial.UpdatedBy);
            param.Add("@Loss", billOfMaterial.Loss);
            param.Add("@WPL", billOfMaterial.WPL);
            if (billOfMaterial.details != null && billOfMaterial.details.Count > 0)
            {
                var rm = new DataTable();
                rm.TableName = "[dbo].[BOMRawMaterial]";
                rm.Columns.Add("ID", typeof(int));
                rm.Columns.Add("ProductID", typeof(int));
                rm.Columns.Add("QtyKG", typeof(string));
                rm.Columns.Add("BillOfMaterialID", typeof(int));
                rm.Columns.Add("ProcessID", typeof(int));
                foreach (var detail in billOfMaterial.details)
                {
                    rm.Rows.Add();
                    rm.Rows[rm.Rows.Count - 1]["ID"] = detail.ID;
                    rm.Rows[rm.Rows.Count - 1]["ProductID"] = detail.ProductID;
                    rm.Rows[rm.Rows.Count - 1]["QtyKG"] = detail.QtyKG;
                    rm.Rows[rm.Rows.Count - 1]["BillOfMaterialID"] = detail.BillOfMaterialID;
                    rm.Rows[rm.Rows.Count - 1]["ProcessID"] = detail.ProcessID;
                }
                param.Add("@RawMaterials", rm);
            }

            if (billOfMaterial.labParameters != null && billOfMaterial.labParameters.Count > 0)
            {
                var rm = new DataTable();
                rm.TableName = "[dbo].[BOMLabParamater]";
                rm.Columns.Add("ID", typeof(int));
                rm.Columns.Add("BillOfMaterialID", typeof(int));
                rm.Columns.Add("ParameterID", typeof(int));
                rm.Columns.Add("Standard", typeof(string));
                rm.Columns.Add("Min", typeof(string));
                rm.Columns.Add("Max", typeof(string));
                rm.Columns.Add("Unit", typeof(string));

                foreach (var labparam in billOfMaterial.labParameters)
                {
                    rm.Rows.Add();
                    rm.Rows[rm.Rows.Count - 1]["ID"] = labparam.ID;
                    rm.Rows[rm.Rows.Count - 1]["BillOfMaterialID"] = labparam.BillOfMaterialID;
                    rm.Rows[rm.Rows.Count - 1]["ParameterID"] = labparam.ParameterID;
                    rm.Rows[rm.Rows.Count - 1]["Standard"] = labparam.Standard;
                    rm.Rows[rm.Rows.Count - 1]["Min"] = labparam.Min;
                    rm.Rows[rm.Rows.Count - 1]["Max"] = labparam.Max;
                    rm.Rows[rm.Rows.Count - 1]["Unit"] = labparam.Unit;
                }

                param.Add("@LabParameters", rm);
            }
            if (DBHelper.ExecuteNonQuery("SaveBOM", param, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<BillOfMaterialDetail> GetLastRevision(int ProductID, int ShadeID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ProductID", ProductID);
            param.Add("@ShadeID", ShadeID);
            param.Add("@RevisionNo", "");
            DataTable dt = DBHelper.GetDataTable("GetBOMLastRevision", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<BillOfMaterialDetail>(dt);
            else
                return null;
        }



        public static void DeleteBillOfMaterialByID(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("DeleteBillOfMaterialByID", param, true);
        }
    }
}
