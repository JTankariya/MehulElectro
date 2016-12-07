using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class PackingProcessLogic
    {
        public static IEnumerable<Batch> GetPackingBatches()
        {

            DataTable dt = DBHelper.GetDataTable("GetPackingBatches", null, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToEnumerable<Batch>(dt);
            }
            else
                return null;
        }

        public static IEnumerable<Batch> GetBatchProductShadePacking(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@BatchID", ID);
            DataTable dt = DBHelper.GetDataTable("GetBatchProductShadePacking", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<Batch>(dt);
            else
                return null;
        }

        public static bool Done(string ID, List<Batch> packingDetails)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@ID", ID);
            DataTable dt = new DataTable();
            dt.Columns.Add("PackingID", typeof(int));
            dt.Columns.Add("PackingProductID", typeof(int));
            dt.Columns.Add("ProductionQty", typeof(string)).MaxLength = 100;
            if (packingDetails != null)
            {
                foreach (var p in packingDetails)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["PackingID"] = p.PackingID;
                    dt.Rows[dt.Rows.Count - 1]["PackingProductID"] = p.PackingProductID;
                    dt.Rows[dt.Rows.Count - 1]["ProductionQty"] = p.ProductionQty;
                }
            }
            dt.TableName = "PackingParameter";
            parameter.Add("@PackingParameter", dt);

            if (DBHelper.ExecuteNonQuery("SaveBatchPacking", parameter, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
