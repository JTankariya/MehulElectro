using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class ProductPartyLogic
    {
        public static List<ProductParty> GetPartiesForProduct(int ProductID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ProductID", ProductID);
            DataTable dt = DBHelper.GetDataTable("GetPartiesForProduct", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<ProductParty>(dt);
            else
                return null;
        }
    }
}
