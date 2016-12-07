using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class RawMaterialTypeLogic
    {
        public static IEnumerable<RawMaterialType> RawMaterialTypeByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetRawMaterialTypeByID", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<RawMaterialType>(dt);
            else
                return null;
        }
    }
}
