using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class ShadingLogic
    {
        public static IEnumerable<Batch> GetShadingBatches()
        {
            DataTable dt = DBHelper.GetDataTable("GetShadingBatches", null, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToEnumerable<Batch>(dt);
            }
            else
                return null;
        }

        public static bool Done(Batch batch)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", batch.ID);
            if (DBHelper.ExecuteNonQuery("SaveShading", param, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Start(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            if (DBHelper.ExecuteNonQuery("StartShading", param, true) > 0)
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
