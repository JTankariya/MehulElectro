using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class DispatchLogic
    {

        public static bool DeleteDispatch(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("DeleteDispatchById", param, true);
            return true;
        }

        public static IEnumerable<Dispatch> GetDispatchByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetDispatchByID", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                var dispatches = DBHelper.ConvertToList<Dispatch>(dt);
                if (dispatches != null)
                {
                    foreach (var dispatch in dispatches)
                    {
                        dispatch.details = GetDispatchDetailByDispatchID(dispatch.ID);
                    }
                }
                return dispatches;
            }
            else
                return null;
        }

        public static List<DispatchDetail> GetDispatchDetailByDispatchID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@DispatchID", ID);
            DataTable dt = DBHelper.GetDataTable("GetDispatchDetailByDispatchID", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<DispatchDetail>(dt);
            else
                return null;
        }

        public static bool SaveDispatch(Dispatch dispatch)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", dispatch.ID);
            param.Add("@DONo", dispatch.DONo);
            param.Add("@OrderID", dispatch.OrderID);
            param.Add("@DODate", dispatch.DODate);
            param.Add("@PartyID", dispatch.PartyID);
            param.Add("@BookingAt", dispatch.BookingAt);
            param.Add("@DeliveryAddressID", dispatch.DeliveryAddressID);
            param.Add("@TransportID", dispatch.TransportID);
            param.Add("@LRNo", dispatch.LRNo);
            param.Add("@LRDate", dispatch.LRDate);
            param.Add("@GatePassNo", dispatch.GatePassNo);
            param.Add("@GatePassDate", dispatch.GatePassDate);
            param.Add("@Remarks", dispatch.Remarks);
            param.Add("@Total", dispatch.Total);
            param.Add("@CreatedBy", dispatch.CreatedBy);
            param.Add("@UpdatedBy", dispatch.UpdatedBy);
            DataTable dt = new DataTable();
            dt.TableName = "[dbo].[DispatchDetail]";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("OrderDetailID", typeof(int));
            dt.Columns.Add("DispatchID", typeof(int));
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("ShadeID", typeof(int));
            dt.Columns.Add("PackingID", typeof(int));
            dt.Columns.Add("Qty", typeof(string));
            dt.Columns.Add("Rate", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            dt.Columns.Add("IsDeleted", typeof(bool));

            if (dispatch.details != null && dispatch.details.Count > 0)
            {
                foreach (var detail in dispatch.details)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["ID"] = detail.ID;
                    dt.Rows[dt.Rows.Count - 1]["OrderDetailID"] = detail.OrderDetailID;
                    dt.Rows[dt.Rows.Count - 1]["DispatchID"] = detail.DispatchID;
                    dt.Rows[dt.Rows.Count - 1]["ProductID"] = detail.ProductID;
                    dt.Rows[dt.Rows.Count - 1]["ShadeID"] = detail.ShadeID;
                    dt.Rows[dt.Rows.Count - 1]["PackingID"] = detail.PackingID;
                    dt.Rows[dt.Rows.Count - 1]["Qty"] = detail.Qty;
                    dt.Rows[dt.Rows.Count - 1]["Rate"] = detail.Rate;
                    dt.Rows[dt.Rows.Count - 1]["Total"] = detail.Total;
                    dt.Rows[dt.Rows.Count - 1]["IsDeleted"] = detail.IsDeleted;
                }
            }

            param.Add("@DispatchDetails", dt);

            if (DBHelper.ExecuteNonQuery("SaveDispatch", param, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<DispatchDetail> GetDispatchDetail(int ID, int OrderID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@DispatchID", ID);
            param.Add("@OrderID", OrderID);
            DataTable dt = DBHelper.GetDataTable("GetDispatchDetail", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<DispatchDetail>(dt);
            else
                return null;
        }

        public static string GetMaxDispatchNo()
        {
            DataTable dt = DBHelper.GetDataTable("GetMaxDispatchNo", null, true);
            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToString(dt.Rows[0]["DispatchNo"]);
            else
                return "1";
        }
    }
}
