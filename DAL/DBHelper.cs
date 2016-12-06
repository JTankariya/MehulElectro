
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBHelper
    {
        //public static string cs = @"server=204.11.58.166;user id=shahsrag_dhruvin;password=dms@1001_;database=shahsrag_expertmobile";
        //public static string cs = @"server=204.11.58.166;user id=shahsrag_dhruvin;password=dms@1001_;database=shahsrag_tempmobile";
        public static string cs = Convert.ToString(ConfigurationManager.AppSettings["DBPath"]);

        public static int ExecuteNonQuery(string Query, Dictionary<string, object> parameters = null, bool IsStoredProcedure = false)
        {
            int Result = 0;
            using (SqlConnection conn = new SqlConnection(cs))
            {

                SqlCommand cmd = new SqlCommand(Query, conn);

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> kvp in parameters)
                    {
                        if (kvp.Value != null && kvp.Value.GetType() == typeof(DataTable))
                        {
                            var par = new SqlParameter(kvp.Key, kvp.Value);
                            par.TypeName = ((DataTable)kvp.Value).TableName;
                            par.SqlDbType = SqlDbType.Structured;
                            cmd.Parameters.Add(par);
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter(kvp.Key, (kvp.Value != null ? kvp.Value : DBNull.Value)));
                        }
                    }
                }
                if (IsStoredProcedure)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                Result = cmd.ExecuteNonQuery();
                conn.Close();

            }
            return Result;
        }

        public static int ExecuteNonQuery(string spName, params object[] parameterValues)
        {
            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                ////pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                //SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName, false);

                ////assign the provided values to these parameters based on parameter order
                //AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(spName, (SqlParameter[])parameterValues);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteNonQuery(spName);
            }
        }

        public static DataTable GetDataTable(string Query, Dictionary<string, object> parameters = null, bool IsStoredProcedure = false, int TableIndex = 0)
        {
            //string cs = @"server=204.11.58.166;user id=shahsrag_dhruvin;password=dms@1001_;database=shahsrag_expertmobile";

            using (SqlConnection conn = new SqlConnection(cs))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                DataSet ds = new DataSet();

                SqlCommand cmd = new SqlCommand(Query, conn);
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> kvp in parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(kvp.Key, (kvp.Value == null ? DBNull.Value : kvp.Value)));
                    }
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                if (IsStoredProcedure)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                adp.Fill(ds);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                if (TableIndex == 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return ds.Tables[TableIndex];
                }
            }
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {

            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName.ToLower())
                .ToList();

            var properties = typeof(T).GetProperties();

            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();

                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                        pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : row[pro.Name], null);
                }

                return objT;
            }).ToList();
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static IEnumerable<T> ConvertToEnumerable<T>(DataTable dt)
        {

            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName.ToLower())
                .ToList();

            var properties = typeof(T).GetProperties();

            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();

                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        if (row[pro.Name].ToString().ToUpper() == "TRUE")
                        {
                            pro.SetValue(objT, true, null);
                        }
                        else if (row[pro.Name].ToString().ToUpper() == "FALSE")
                        {
                            pro.SetValue(objT, false, null);
                        }
                        else
                        {
                            pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : row[pro.Name], null);
                        }
                    }

                }

                return objT;
            }).AsEnumerable();

        }

        public static string CheckEscape(string variable)
        {
            return (!string.IsNullOrEmpty(variable) ? variable.Replace("'", "''") : variable);
        }

        public static DataSet GetDataSet(string Query, Dictionary<string, object> parameters = null, bool IsStoredProcedure = false)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                DataSet ds = new DataSet();

                SqlCommand cmd = new SqlCommand(Query, conn);
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> kvp in parameters)
                        cmd.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                if (IsStoredProcedure)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                adp.Fill(ds);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return ds;



            }
        }
    }
}
