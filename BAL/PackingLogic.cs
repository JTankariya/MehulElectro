using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class PackingLogic
    {
        public static IEnumerable<Packing> GetPackingByProductID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetPackingByProductID", param, true);

            if (dt != null && dt.Rows.Count > 0)
            {
               return DBHelper.ConvertToList<Packing>(dt);
            }
            else
                return null;
        }
        public static IEnumerable<Packing> GetPackingByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetPackingByID", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<Packing>(dt);
            else
                return null;
        }

        public static void DeletePackingByID(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("DeletePackingById", param, true);
        }

        public static void AddPacking(Packing packing)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", packing.ID);
            param.Add("@Name", packing.Name.Trim());
            param.Add("@ConversionFactor", packing.ConversionFactorWithLtr.Trim());
            DBHelper.ExecuteNonQuery("AddPacking", param, true);
        }



        public static bool CheckReference(int ProductID, int PackingID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ProductID", ProductID);
            param.Add("@PackingID", PackingID);
            DataSet dt = DBHelper.GetDataSet("CheckPackingReference", param, true);

            if (dt != null && dt.Tables.Count > 0)
            {
                foreach (DataTable table in dt.Tables)
                {
                    if (table.Rows.Count > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
                return false;
        }

        public static bool Start(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            if (DBHelper.ExecuteNonQuery("StartPacking", param, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
