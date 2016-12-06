using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class PartyLogic
    {
        public static IEnumerable<Party> GetPartyByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetPartyByID", param, true);

            if (dt != null && dt.Rows.Count > 0)
            {
                var parties =DBHelper.ConvertToList<Party>(dt);
                if (parties != null)
                {
                    foreach (var party in parties)
                    {
                        party.Addresses = PartyAddressLogic.GetPartyAddress(party.ID);
                    }
                }
                return parties;
            }
            else
                return null;
        }

        public static IEnumerable<Party> GetOrderedParties()
        {

            DataTable dt = DBHelper.GetDataTable("GetOrderedParties", null, true);

            if (dt != null && dt.Rows.Count > 0)
            {
                var parties = DBHelper.ConvertToList<Party>(dt);
                if (parties != null)
                {
                    foreach (var party in parties)
                    {
                        party.Addresses = PartyAddressLogic.GetPartyAddress(party.ID);
                    }
                }
                return parties;
            }
            else
                return null;
        }

        public static void DeletePartyByID(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("DeletePartyById", param, true);
        }

        public static bool SaveParty(Party partyModel)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", partyModel.ID);
            param.Add("@PartyGroupID", partyModel.PartyGroupID);
            param.Add("@Name", partyModel.Name);
            param.Add("@ContactPerson", partyModel.ContactPerson);
            param.Add("@MobileNo", partyModel.MobileNo);
            param.Add("@TINNo", partyModel.TINNo);
            param.Add("@CSTNo", partyModel.CSTNo);
            param.Add("@EmailId", partyModel.EmailId);
            param.Add("@LICNo", partyModel.LICNo);
            param.Add("@PANNo", partyModel.PANNo);
            param.Add("@CreatedBy", partyModel.CreatedBy);
            param.Add("@UpdatedBy", partyModel.UpdatedBy);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string)).MaxLength = 100;
            dt.Columns.Add("IsDefault", typeof(bool));
            dt.Columns.Add("Address1", typeof(string)).MaxLength = 100;
            dt.Columns.Add("Address2", typeof(string)).MaxLength = 100;
            dt.Columns.Add("Address3", typeof(string)).MaxLength = 100;
            dt.Columns.Add("City", typeof(string)).MaxLength = 100;
            dt.Columns.Add("State", typeof(string)).MaxLength = 100;
            dt.Columns.Add("Phone1", typeof(string)).MaxLength = 100;
            dt.Columns.Add("Phone2", typeof(string)).MaxLength = 100;
            dt.Columns.Add("Mobile1", typeof(string)).MaxLength = 100;
            dt.Columns.Add("Mobile2", typeof(string)).MaxLength = 100;
            dt.Columns.Add("TransportID", typeof(int));
            dt.Columns.Add("BookingAt", typeof(string)).MaxLength = 100;
            dt.Columns.Add("PartyID", typeof(int));

            if (partyModel.Addresses != null && partyModel.Addresses.Count > 0)
            {
                foreach (var add in partyModel.Addresses)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["ID"] = add.ID;
                    dt.Rows[dt.Rows.Count - 1]["Name"] = add.Name;
                    dt.Rows[dt.Rows.Count - 1]["IsDefault"] = add.IsDefault;
                    dt.Rows[dt.Rows.Count - 1]["Address1"] = add.Address1;
                    dt.Rows[dt.Rows.Count - 1]["Address2"] = add.Address2;
                    dt.Rows[dt.Rows.Count - 1]["Address3"] = add.Address3;
                    dt.Rows[dt.Rows.Count - 1]["City"] = add.City;
                    dt.Rows[dt.Rows.Count - 1]["State"] = add.State;
                    dt.Rows[dt.Rows.Count - 1]["Phone1"] = add.Phone1;
                    dt.Rows[dt.Rows.Count - 1]["Phone2"] = add.Phone2;
                    dt.Rows[dt.Rows.Count - 1]["Mobile1"] = add.Mobile1;
                    dt.Rows[dt.Rows.Count - 1]["Mobile2"] = add.Mobile2;
                    dt.Rows[dt.Rows.Count - 1]["TransportID"] = add.TransportID;
                    dt.Rows[dt.Rows.Count - 1]["BookingAt"] = add.BookingAt;
                    dt.Rows[dt.Rows.Count - 1]["PartyID"] = add.PartyID;
                }
            }
            dt.TableName = "[dbo].[PartyAddress]";
            param.Add("@Addresses", dt);
            if (DBHelper.ExecuteNonQuery("AddParty", param, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static IEnumerable<Order> GetOrders(int PartyID)
        {
            return OrderLogic.GetOrderByID(0).Where(x => x.PartyID == PartyID);
        }
    }
}
