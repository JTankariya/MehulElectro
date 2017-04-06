using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class ReorderLogic
    {
        public static IEnumerable<ReorderStatus> GetReorderStatus()
        {
            DataTable dt = DBHelper.GetDataTable("ReorderStatus", null, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToEnumerable<ReorderStatus>(dt);
            }
            else
                return null;
        }
    }
}
