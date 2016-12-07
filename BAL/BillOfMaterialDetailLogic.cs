using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class BillOfMaterialDetailLogic
    {
        public static List<BillOfMaterialDetail> GetRawMaterialDetails(int ProductID, int ShadeID, string RevisionNo)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ProductID", ProductID);
            param.Add("@ShadeID", ShadeID);
            param.Add("@RevisionNo", RevisionNo);
            DataTable dt = DBHelper.GetDataTable("GetBOMLastRevision", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<BillOfMaterialDetail>(dt);
            else
                return null;
        }
    }
}
