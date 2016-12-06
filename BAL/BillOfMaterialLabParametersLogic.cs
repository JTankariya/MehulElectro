using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class BillOfMaterialLabParametersLogic
    {

        public static List<BillOfMaterialLabParameters> GetLabParamaters(int BillOfMaterialID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@BillOfMaterialID", BillOfMaterialID);

            DataTable dt = DBHelper.GetDataTable("GetBillOfMaterialLabParamaters", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<BillOfMaterialLabParameters>(dt);
            else
                return null;
        }
        public static IEnumerable<BillOfMaterialLabParameters> GetLabParameterByProductAndShadeID(int ProductID, int ShadeID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ProductID", ProductID);
            param.Add("@ShadeID", ShadeID);
            DataTable dt = DBHelper.GetDataTable("GetLabParameterByProductAndShadeID", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<BillOfMaterialLabParameters>(dt);
            else
                return null;
        }
    }
}
