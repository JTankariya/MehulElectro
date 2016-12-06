using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
    public class ExceptionParameters
    {
        public string ErrorId { get; set; }
        public string Application { get; set; }
        public string Host { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public int StatusCode { get; set; }
        public DateTime TimeUtc { get; set; }
        public string AllXml { get; set; }

        public ExceptionParameters(Exception e)
        {
            ErrorId = Guid.NewGuid().ToString();
            Application = System.AppDomain.CurrentDomain.FriendlyName;
            Host = System.Environment.MachineName.ToString();
            Type = e.GetType().ToString();
            Source = e.Source.ToString();
            Message = e.Message.ToString();
            User = "";
            StatusCode = 500;
            TimeUtc = DateTime.Now;
            AllXml = e.StackTrace;
        }
    }
}
