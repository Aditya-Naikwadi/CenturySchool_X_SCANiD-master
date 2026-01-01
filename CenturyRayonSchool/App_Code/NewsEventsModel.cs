using System;
using System.Data;
using System.Data.SqlClient;

namespace CenturyRayonSchool.Model
{
    public class NewsEventsModel
    {

        public string saveAddNews(NewsEvent _news)
        {
            SqlConnection con = null;
            try
            {



                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = null;
                    DateTime dt = TimeZoneClass.getIndianTimeZoneValues();


                    query = "insert into NewsTable([Date],[TopicName],[TopicDescription],[Filename],[Filepath],[CreatedDate],[CreatedBy],[status]) values(@Date,@TopicName,@TopicDescription,@Filename,@Filepath,@CreatedDate,@CreatedBy,@status);";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Date", _news.Date);
                    cmd.Parameters.AddWithValue("@TopicName", _news.TopicName);
                    cmd.Parameters.AddWithValue("@TopicDescription", _news.TopicDescription);
                    cmd.Parameters.AddWithValue("@Filename", _news.Filename);
                    cmd.Parameters.AddWithValue("@Filepath", _news.Filepath);
                    cmd.Parameters.AddWithValue("@CreatedDate", _news.CreatedDate);
                    cmd.Parameters.AddWithValue("@CreatedBy", _news.CreatedBy);
                    cmd.Parameters.AddWithValue("@status", "active");
                    cmd.ExecuteNonQuery();


                }

                return "ok";
            }
            catch (Exception ex)
            {
                Log.Error("NewsEventsModel.saveAddNews", ex);
                return ex.Message;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }


        public string saveUpdateNews(NewsEvent _editnews)
        {
            SqlConnection con = null;
            try
            {



                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";

                    SqlCommand cmd = null;
                    DateTime dt = TimeZoneClass.getIndianTimeZoneValues();

                    query = "update newstable set Date=@Date, TopicName=@TopicName, TopicDescription=@TopicDescription, Filename=@Filename, Filepath=@Filepath, updateddate=@updateddate , updatedby=@updatedby where id=@id ";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Date", _editnews.Date);
                    cmd.Parameters.AddWithValue("@TopicName", _editnews.TopicName);
                    cmd.Parameters.AddWithValue("@TopicDescription", _editnews.TopicDescription);
                    cmd.Parameters.AddWithValue("@Filename", _editnews.Filename);
                    cmd.Parameters.AddWithValue("@Filepath", _editnews.Filepath);
                    cmd.Parameters.AddWithValue("@updateddate", _editnews.updateddate);
                    cmd.Parameters.AddWithValue("@updatedby", _editnews.updatedby);
                    cmd.Parameters.AddWithValue("@id", _editnews.id);
                    cmd.ExecuteNonQuery();


                }

                return "ok";
            }
            catch (Exception ex)
            {
                Log.Error("NewsEventsModel.saveUpdateNews", ex);
                return ex.Message;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public DataTable GetNewsList()
        {
            SqlConnection con = null;
            DataTable _newsTable = new DataTable();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = null;
                    DateTime currdate = TimeZoneClass.getIndianTimeZoneValues();
                    DateTime prevdate = currdate.AddDays(-10);
                    query = "select CONVERT (varchar(10), cast([Date] as Date), 103) AS [Date] ,id,topicName,topicDescription,filename,filepath,createddate " +
                        "from newsTable " +
                        "where [Date] between '" + prevdate.ToString("yyyy/MM/dd").Replace('-', '/') + "' and '" + currdate.ToString("yyyy/MM/dd").Replace('-', '/') + "' and status='active' " +
                        "ORDER BY Date DESC;";

                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(_newsTable);



                }

                return _newsTable;
            }
            catch (Exception ex)
            {
                Log.Error("NewsEventsModel.GetNewsList", ex);
                return _newsTable;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public DataTable GetNewsListSuperAdmin()
        {
            SqlConnection con = null;
            DataTable _newsTable = new DataTable();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = null;
                    DateTime currdate = TimeZoneClass.getIndianTimeZoneValues();
                    DateTime prevdate = currdate.AddDays(-100);
                    query = "select CONVERT (varchar(10), cast([Date] as Date), 103) AS [Date] ,id,topicName,topicDescription,filename,filepath,createddate " +
                        "from newsTable " +
                        "ORDER BY Date DESC;";

                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(_newsTable);



                }

                return _newsTable;
            }
            catch (Exception ex)
            {
                Log.Error("NewsEventsModel.GetNewsListSuperAdmin", ex);
                return _newsTable;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public string btndelete(string id, string usercode)
        {
            SqlConnection con = null;
            try
            {

                string query = "";
                using (con = Connection.getConnection())
                {
                    con.Open();
                    // query = "Delete from newsTable where id=@id";
                    DateTime dt = TimeZoneClass.getIndianTimeZoneValues();

                    query = "update newstable set status='deleted',updateddate=@updateddate , updatedby=@updatedby where id=@id;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@updateddate", dt.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/"));
                    cmd.Parameters.AddWithValue("@updatedby", usercode);

                    cmd.ExecuteNonQuery();

                }

                return "ok";

            }
            catch (Exception ex)
            {
                Log.Error("NewsEventsModel.btndelete", ex);
                return ex.Message;
            }
            finally
            {
                if (con != null) con.Close();
            }

        }

    }

    public class NewsEvent
    {
        public string id { get; set; }
        public string Date { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
        public string Filename { get; set; }
        public string Filepath { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string updateddate { get; set; }
        public string updatedby { get; set; }

    }

}