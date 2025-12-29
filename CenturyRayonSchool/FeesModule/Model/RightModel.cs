using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CenturyRayonSchool.FeesModule.Model
{
    public class RightModel
    {

        public List<string> getModuleList(string userid)
        {
            List<string> listmodule = new List<string>();
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "select modulename  from [LoginModule] where userid='" + userid + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listmodule.Add(reader["modulename"].ToString());
                    }
                    reader.Close();
                }

                return listmodule;
            }
            catch (Exception ex)
            {
                Log.Error("RightModel.getModuleList", ex);
                return listmodule;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


    }
}