using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class ProductGroupTypeLogic
    {
        public static IEnumerable<ProductGroupType> GetAllProductGroupTypes()
        {
            DataTable dt = DBHelper.GetDataTable("GetAllProductGroupTypes", null, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<ProductGroupType>(dt);
            else
                return null;
        }
    }
}
