using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class PartyGroupLogic
    {
        public static IEnumerable<PartyGroup> GetAllPartyGroup()
        {
            DataTable dt = DBHelper.GetDataTable("GetAllPartyGroup", null, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<PartyGroup>(dt);
            else
                return null;
        }
    }
}
