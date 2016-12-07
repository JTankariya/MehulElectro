using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class StockLogic
    {
        public static IEnumerable<Stock> GetStockReport(DateTime? FromDate, DateTime? ToDate, string ProductID, string ShadeID, string PackingID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@FromDate", FromDate);
            param.Add("@ToDate", ToDate);
            param.Add("@ProductID", ProductID);
            param.Add("@ShadeID", ShadeID);
            param.Add("@PackingID", PackingID);
            DataTable dt = DBHelper.GetDataTable("StockReport", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<Stock>(dt);
            else
                return null;
        }

        public static IEnumerable<GrindingMaterial> GetGrindingStock(int BatchID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@BatchID", BatchID);

            DataTable dt = DBHelper.GetDataTable("GetGrindingStock", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<GrindingMaterial>(dt);
            else
                return null;
        }

        public static IEnumerable<Stock> GetClosingQty(int ProductID, int ShadeID, string PackingIDs)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ProductID", ProductID);
            param.Add("@ShadeID", ShadeID);
            param.Add("@PackingIDs", PackingIDs);
            DataTable dt = DBHelper.GetDataTable("GetClosingQty", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<Stock>(dt);
            else
                return null;
        }
    }
}
