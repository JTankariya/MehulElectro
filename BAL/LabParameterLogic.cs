using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class LabParameterLogic
    {

        public static IEnumerable<LabParameter> GetLabParameterByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetLabParameterByID", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<LabParameter>(dt);
            else
                return null;
        }

        public static void AddLabParameter(LabParameter labparameter)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", labparameter.ID);
            param.Add("@Name", labparameter.Name.Trim());
            param.Add("@ValueTypeID", labparameter.ValueTypeID);
            DBHelper.ExecuteNonQuery("SaveLabParameter", param, true);
        }

        public static void DeleteLabParameterByID(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("DeleteLabParameterByID", param, true);
        }
    }
}
