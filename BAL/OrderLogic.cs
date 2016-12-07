using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class OrderLogic
    {
        public static IEnumerable<Order> GetOrderByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetOrderByID", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                var orders = DBHelper.ConvertToList<Order>(dt);
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        order.orderDetail = GetOrderDetailByOrderID(order.ID);
                    }
                }
                return orders;
            }
            else
                return null;
        }

        public static IEnumerable<Order> CheckDuplicateOrderNo(int ID, string OrderNo)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            param.Add("@OrderNo", OrderNo);
            DataTable dt = DBHelper.GetDataTable("CheckDuplicateOrderNo", param, true);

            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToList<Order>(dt);
            }
            else
                return null;
        }

        public static IEnumerable<Order> GetClosedOrders()
        {
            DataTable dt = DBHelper.GetDataTable("GetClosedOrders", null, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                var orders = DBHelper.ConvertToList<Order>(dt);
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        order.orderDetail = GetOrderDetailByOrderID(order.ID);
                    }
                }
                return orders;
            }
            else
                return null;
        }

        public static List<OrderDetail> GetOrderDetailByOrderID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@OrderID", ID);
            DataTable dt = DBHelper.GetDataTable("GetOrderDetailByOrderID", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<OrderDetail>(dt);
            else
                return null;
        }

        public static bool SaveOrder(Order order)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", order.ID);
            param.Add("@OrderNo", order.OrderNo);
            param.Add("@OrderDate", order.OrderDate);
            param.Add("@PartyOrderNo", order.PartyOrderNo);
            param.Add("@PartyOrderDate", order.PartyOrderDate);
            param.Add("@PartyID", order.PartyID);
            param.Add("@BookingAt", order.BookingAt);
            param.Add("@PartyBookingCity", order.PartyBookingCity);
            param.Add("@DeliveryAddressID", order.DeliveryAddressID);
            param.Add("@TransportID", order.TransportID);
            param.Add("@PaymentNarr", order.PaymentNarr);
            param.Add("@DeliveryNarr", order.DeliveryNarr);
            param.Add("@BillingNarr", order.BillingNarr);
            param.Add("@Remarks1", order.Remarks1);
            param.Add("@Remarks2", order.Remarks2);
            param.Add("@Total", order.Total);
            param.Add("@IsClosed", order.IsClosed);
            param.Add("@CreatedBy", order.CreatedBy);
            param.Add("@UpdatedBy", order.UpdatedBy);

            DataTable dt = new DataTable();
            dt.TableName = "[dbo].[OrderDetail]";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("OrderID", typeof(int));
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("ShadeID", typeof(int));
            dt.Columns.Add("PackingID", typeof(int));
            dt.Columns.Add("Qty", typeof(string));
            dt.Columns.Add("Rate", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            dt.Columns.Add("IsSoftDispatched", typeof(bool));
            dt.Columns.Add("IsMovedToProduction", typeof(bool));
            dt.Columns.Add("TransactionLevel", typeof(string));

            if (order.orderDetail != null && order.orderDetail.Count > 0)
            {
                foreach (var detail in order.orderDetail)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["ID"] = detail.ID;
                    dt.Rows[dt.Rows.Count - 1]["OrderID"] = detail.OrderID;
                    dt.Rows[dt.Rows.Count - 1]["ProductID"] = detail.ProductID;
                    dt.Rows[dt.Rows.Count - 1]["ShadeID"] = detail.ShadeID;
                    dt.Rows[dt.Rows.Count - 1]["PackingID"] = detail.PackingID;
                    dt.Rows[dt.Rows.Count - 1]["Qty"] = detail.Qty;
                    dt.Rows[dt.Rows.Count - 1]["Rate"] = detail.Rate;
                    dt.Rows[dt.Rows.Count - 1]["Total"] = detail.Total;
                    dt.Rows[dt.Rows.Count - 1]["IsSoftDispatched"] = false;
                    dt.Rows[dt.Rows.Count - 1]["IsMovedToProduction"] = false;
                    dt.Rows[dt.Rows.Count - 1]["TransactionLevel"] = detail.TransactionLevel;
                }
            }

            param.Add("@OrderDetails", dt);

            if (DBHelper.ExecuteNonQuery("SaveOrder", param, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void DeleteOrderByID(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("DeleteOrderByID", param, true);
        }

        public static void MoveToProduction(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("MoveItemToProduction", param, true);
        }

        public static void CloseOrder(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("CloseOrder", param, true);
        }

        public static void ReopenOrder(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("ReopenOrder", param, true);
        }

        public static void MoveToDispatch(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("MoveItemToDispatch", param, true);
        }

        public static string GetMaxOrderNo()
        {
            DataTable dt = DBHelper.GetDataTable("GetMaxOrderNo", null, true);
            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToString(dt.Rows[0]["OrderNo"]);
            else
                return "1";
        }
    }
}
