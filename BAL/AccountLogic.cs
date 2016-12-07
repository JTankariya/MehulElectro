using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class AccountLogic
    {
        public static Employee CheckUserByUserNameAndPassword(string UserName, string Password)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@UserName", UserName);
            param.Add("@Password", StringCipher.Encrypt(Password));
            DataTable dt = DBHelper.GetDataTable("CheckUserByUserNameAndPassword", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<Employee>(dt).FirstOrDefault();
            else
                return null;
        }
    }
}
