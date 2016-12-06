using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class InwardLogic
    {
        public static IEnumerable<Inward> GetInwardByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetInwardByID", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                var Inwards = DBHelper.ConvertToList<Inward>(dt);
                if (Inwards != null)
                {
                    foreach (var Inward in Inwards)
                    {
                        Inward.inwardDetail = GetInwardDetailByInwardID(Inward.ID);
                    }
                }
                return Inwards;
            }
            else
                return null;
        }

        public static List<InwardDetail> GetInwardDetailByInwardID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@InwardID", ID);
            DataTable dt = DBHelper.GetDataTable("GetInwardDetailByInwardID", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<InwardDetail>(dt);
            else
                return null;
        }

        public static bool SaveInward(Inward Inward)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", Inward.ID);
            param.Add("@InwardNo", Inward.InwardNo);
            param.Add("@InwardDate", Inward.InwardDate);
            param.Add("@PartyID", Inward.PartyID);
            param.Add("@PaymentNarr", Inward.PaymentNarr);
            param.Add("@DeliveryNarr", Inward.DeliveryNarr);
            param.Add("@BillingNarr", Inward.BillingNarr);
            param.Add("@Remarks1", Inward.Remarks1);
            param.Add("@Remarks2", Inward.Remarks2);
            param.Add("@Total", Inward.Total);
            param.Add("@CreatedBy", Inward.CreatedBy);
            param.Add("@UpdatedBy", Inward.UpdatedBy);

            DataTable dt = new DataTable();
            dt.TableName = "[dbo].[InwardDetail]";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("InwardID", typeof(int));
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("Qty", typeof(string));
            dt.Columns.Add("Rate", typeof(string));
            dt.Columns.Add("Total", typeof(string));

            if (Inward.inwardDetail != null && Inward.inwardDetail.Count > 0)
            {
                foreach (var detail in Inward.inwardDetail)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["ID"] = detail.ID;
                    dt.Rows[dt.Rows.Count - 1]["InwardID"] = detail.InwardID;
                    dt.Rows[dt.Rows.Count - 1]["ProductID"] = detail.ProductID;
                    dt.Rows[dt.Rows.Count - 1]["Qty"] = (detail.IsDeleted ? "0" : detail.Qty);
                    dt.Rows[dt.Rows.Count - 1]["Rate"] = detail.Rate;
                    dt.Rows[dt.Rows.Count - 1]["Total"] = detail.Total;
                }
            }

            param.Add("@InwardDetails", dt);

            if (DBHelper.ExecuteNonQuery("SaveInward", param, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void DeleteInwardByID(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("DeleteInwardByID", param, true);
        }

    }
}
