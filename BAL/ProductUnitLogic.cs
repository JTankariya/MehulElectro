using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class ProductUnitLogic
    {

        public static IEnumerable<ProductUnit> GetProductUnitByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetProductUnitByID", param, true);

            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToList<ProductUnit>(dt);
            }
            else
                return null;
        }
    }
}
