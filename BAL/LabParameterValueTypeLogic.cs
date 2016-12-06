using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class LabParameterValueTypeLogic
    {

        public static IEnumerable<LabParameterValueType> GetLabParameterTypes()
        {
            DataTable dt = DBHelper.GetDataTable("GetLabParameterTypes", null, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<LabParameterValueType>(dt);
            else
                return null;
        }
    }
}
