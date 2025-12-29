using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Web.Configuration;

namespace CenturyRayonSchool.Model
{
    public class Connection
    {

        public static SqlConnection getConnection()
        {

            string connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            return con;
        }





        public static string GetExcelConnectionString(String path)
        {
            try
            {
                Dictionary<string, string> props = new Dictionary<string, string>();

                // XLSX - Excel 2007, 2010, 2012, 2013
                props["Provider"] = "Microsoft.ACE.OLEDB.12.0";
                props["Data Source"] = path;
                props["Extended Properties"] = "\"Excel 12.0 XML";
                props["HDR"] = "YES";
                props["IMEX"] = "0\";";

                // XLS - Excel 2003 and Older
                //props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
                //props["Extended Properties"] = "Excel 8.0";
                //props["Data Source"] = "C:\\MyExcel.xls";

                StringBuilder sb = new StringBuilder();

                foreach (KeyValuePair<string, string> prop in props)
                {
                    sb.Append(prop.Key);
                    sb.Append('=');
                    sb.Append(prop.Value);
                    sb.Append(';');
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                Log.Error("Connection.GetExcelConnectionString", ex);
                return null;
            }

        }


        public static string GetMonthsName(int id)
        {
            switch (id)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return "";
            }

        }

        public static void GetFirstAndLastDate(out string firstdate, out string lastdate)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            firstdate = startDate.ToString("yyyy/MM/dd").Replace('-', '/');
            lastdate = endDate.ToString("yyyy/MM/dd").Replace('-', '/');

        }

        public static void logoutfun(string userid, string username, string logontype)
        {
            SqlConnection con = null;
            string resp = "error";
            try
            {

                DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "insert into UserLoginLog(date1,UserId,UserName,logontype,logondatetime) values(@date1,@UserId,@UserName,@logontype,@logondatetime);";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@date1", cdt.ToString("yyyy/MM/dd"));
                    cmd.Parameters.AddWithValue("@UserId", userid);
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@logontype", logontype);
                    cmd.Parameters.AddWithValue("@logondatetime", cdt.ToString("yyyy/MM/dd hh:mm:ss tt"));
                    cmd.ExecuteNonQuery();


                }

                resp = "ok";
            }
            catch (Exception ex)
            {
                Log.Error("AdminMaster.logoutfun", ex);
                resp = "error";
            }
            finally
            {
                if (con != null) con.Close();


            }
        }
    }
}
