using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class EmployeeTypeLogic
    {
        public static IEnumerable<EmployeeType> GetAllEmployeeTypes()
        {
            DataTable dt = DBHelper.GetDataTable("GetAllEmployeeTypes", null, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<EmployeeType>(dt);
            else
                return null;
        }
    }
}
