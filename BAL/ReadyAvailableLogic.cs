using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class ReadyAvailableLogic
    {
        public static IEnumerable<ReadyAvailableOrder> GetAllOrderTransactionStatus()
        {
            DataTable dt = DBHelper.GetDataTable("GetAllOrderTransactionStatus", null, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToEnumerable<ReadyAvailableOrder>(dt);
            }
            else
                return null;
        }
    }
}
