using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class BatchLogic
    {
        public static IEnumerable<Batch> GetBatchAllocationData()
        {
            DataTable dt = DBHelper.GetDataTable("GetBatchAllocationData", null, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToEnumerable<Batch>(dt);
            }
            else
                return null;
        }
        public static IEnumerable<BatchDetail> GetBatchDetails(int BatchID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@BatchID", BatchID);
            DataTable dt = DBHelper.GetDataTable("GetBatchDetails", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToEnumerable<BatchDetail>(dt);
            }
            else
                return null;
        }

        public static IEnumerable<Batch> GetBatchStatus()
        {
            DataTable dt = DBHelper.GetDataTable("GetBatchStatus", null, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToEnumerable<Batch>(dt);
            }
            else
                return null;
        }

        public static bool Save(Batch batch)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@BatchNo", batch.BatchNo);
            param.Add("@ProductID", batch.ProductID);
            param.Add("@ShadeID", batch.ShadeID);
            param.Add("@ProductionQty", batch.ProductionQty);
            //param.Add("@EmployeeID", batch.EmployeeID);
            param.Add("@CreatedBy", batch.CreatedBy);
            param.Add("@OrderedQty", batch.OrderedQty);
            DataTable dt = new DataTable();
            dt.Columns.Add("PackingID", typeof(int));
            dt.Columns.Add("Qty", typeof(string)).MaxLength = 100;
            if (batch.BatchPackings != null && batch.BatchPackings.Count > 0)
            {
                foreach (var b in batch.BatchPackings)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["PackingID"] = b.PackingID;
                    dt.Rows[dt.Rows.Count - 1]["Qty"] = b.Qty;
                }
            }
            dt.TableName = "[dbo].[BatchPacking]";
            param.Add("@BatchPackings", dt);
            if (DBHelper.ExecuteNonQuery("SaveBatch", param, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckBatchNo(string BatchNo)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@BatchNo", BatchNo);
            DataTable dt = DBHelper.GetDataTable("CheckBatchNo", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public static Batch GetBatchByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetBatchByID", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToEnumerable<Batch>(dt).FirstOrDefault();
            }
            else
                return null;
        }
    }
}
