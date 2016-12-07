using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class ShadeLogic
    {
        public static IEnumerable<ProductShade> GetShadeByProductID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetShadeByProductID", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<ProductShade>(dt);
            else
                return null;
        }

        public static IEnumerable<Shade> GetShadeByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetShadeByID", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<Shade>(dt);
            else
                return null;
        }

        public static void AddShade(Shade shade)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", shade.ID);
            param.Add("@Name", shade.Name.Trim());
            DBHelper.ExecuteNonQuery("SaveShade", param, true);
        }

        public static bool DeleteShadeByID(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataSet ds = DBHelper.GetDataSet("DeleteShadeByID", param, true);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 1)
            {
                return false;
            }
            else
            {
                if (ds != null && ds.Tables != null && (ds.Tables[0].Rows.Count > 1 || ds.Tables[0].Columns.Count > 1))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
