using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class ProductGroupLogic
    {
        public static IEnumerable<ProductGroup> GetProductGroupByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetProductGroupByID", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<ProductGroup>(dt);
            else
                return null;
        }

        public static void DeleteProductGroupByID(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("DeleteProductGroupById", param, true);
        }

        public static void AddProductGroup(ProductGroup productGroup)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", productGroup.ID);
            param.Add("@Name", productGroup.Name);
            param.Add("@ProductGroupTypeID", productGroup.ProductGroupTypeID);
            DBHelper.ExecuteNonQuery("AddProductGroup", param, true);
        }
    }
}
