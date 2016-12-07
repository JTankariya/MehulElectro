using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class EmployeeLogic
    {
        public static IEnumerable<Employee> GetEmployeeByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetEmployeeByID", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<Employee>(dt);
            else
                return null;
        }

        public static void DeleteEmployeeByID(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("DeleteEmployeeById", param, true);
        }

        public static void AddEmployee(Employee employee)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", employee.ID);
            param.Add("@Name", employee.Name);
            param.Add("@EmailId", employee.EmailId);
            param.Add("@MobileNo", employee.MobileNo);
            param.Add("@UserName", employee.UserName);
            param.Add("@Password", StringCipher.Encrypt(employee.Password));
            param.Add("@Type", (employee.Type == null ? "" : employee.Type));
            param.Add("@PhotoPath", (employee.PhotoPath == null ? "" : employee.PhotoPath));
            param.Add("@CreatedBy", employee.CreatedBy);
            param.Add("@UpdatedBy", employee.UpdatedBy);
            DBHelper.ExecuteNonQuery("AddEmployee", param, true);
        }
    }
}
