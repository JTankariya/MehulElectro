using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class TransportLogic
    {

        public static IEnumerable<Transport> GetTransportByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetTransportByID", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<Transport>(dt);
            else
                return null;
        }

        public static void AddTransport(Transport transport)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", transport.ID);
            param.Add("@Name", transport.Name);
            param.Add("@Address", transport.Address);
            param.Add("@ContactPerson", transport.ContactPerson);
            param.Add("@Phone", transport.Phone);
            param.Add("@Mobile", transport.Mobile);
            DBHelper.ExecuteNonQuery("AddTransport", param, true);
        }

        public static void DeleteTransportByID(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("DeleteTransportByID", param, true);
        }
    }
}
