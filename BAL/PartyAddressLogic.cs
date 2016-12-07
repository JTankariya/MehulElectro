using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class PartyAddressLogic
    {

        public static List<PartyAddress> GetPartyAddress(int partyId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", partyId);
            DataTable dt = DBHelper.GetDataTable("GetAddressByPartyID", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<PartyAddress>(dt);
            else
                return null;
        }
    }
}
