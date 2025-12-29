using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CenturyRayonSchool.Model
{
    public class IndexPageModel
    {

        public List<TodayBirthday> getTodayBirthday()
        {
            SqlConnection con = null;
            List<TodayBirthday> listtoday = new List<TodayBirthday>();
            try
            {
                string query = "";
                using (con = Connection.getConnection())
                {
                    con.Open();
                    //Get students Birthday
                    query = "Select [DOB],[Fullname],[std],[div],[Mobile],[Contact2],[cardid] " +
                            "From studentmaster " +
                            "where DATEPART(d, DOB)= DATEPART(d, GetDate()) and DATEPART(m, DOB)= DATEPART(m, Getdate());";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listtoday.Add(new TodayBirthday()
                        {
                            std = reader["std"].ToString(),
                            div = reader["div"].ToString(),
                            fullname = reader["Fullname"].ToString(),
                            isstaff = false
                        });
                    }
                    reader.Close();

                    //Get staff Birthday

                    query = "Select[DOB],[fname],[Mobile],[Contact2],[cardid],[Shiftname],[Div] " +
                            "From staffmaster " +
                            "where DATEPART(d, DOB)= DATEPART(d, GetDate()) and DATEPART(m, DOB)= DATEPART(m, Getdate());";

                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listtoday.Add(new TodayBirthday()
                        {

                            fullname = reader["fname"].ToString(),
                            isstaff = true
                        });
                    }
                    reader.Close();
                }

                return listtoday;
            }
            catch (Exception ex)
            {
                Log.Error("IndexPageModel.getTodayBirthday", ex);
                return listtoday;
            }
        }


    }

    public class TodayBirthday
    {
        public string std { get; set; }
        public string div { get; set; }
        public string fullname { get; set; }
        public Boolean isstaff { get; set; }

    }
}