using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
    public class ExceptionLogic
    {
        public static bool AddException(ExceptionParameters parameters)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ErrorId", parameters.ErrorId);
            param.Add("@Application", parameters.Application);
            param.Add("@Host", parameters.Host);
            param.Add("@Type", parameters.Type);
            param.Add("@Message", parameters.Message);
            param.Add("@Source", parameters.Source);
            param.Add("@User", parameters.User);
            param.Add("@StatusCode", parameters.StatusCode);
            param.Add("@TimeUTC", parameters.TimeUtc);
            param.Add("@AllXml", parameters.AllXml);
            DBHelper.ExecuteNonQuery("LogException", param, true);
            return true;
        }
    }
}
