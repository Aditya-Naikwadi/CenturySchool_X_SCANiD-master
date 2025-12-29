using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CenturyRayonSchool
{
    public class EventModel
    {
        public string updateEvent(Eventbl _event)
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


                    query = "insert into eventTable([FromDate],[ToDate],[eventName],[eventDescription],[Filename],[Filepath],[CreatedDate],[CreatedBy],[status],[thumbnailimg],[thumbnailimgpath],venue,starttime,endtime) " +
                        "values(@fromDate,@toDate,@eventName,@eventDescription,@Filename,@Filepath,@CreatedDate,@CreatedBy,@status,@thumbnailimg,@thumbnailimgpath,@venue,@starttime,@endtime);";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@fromDate", _event.startDate);
                    cmd.Parameters.AddWithValue("@toDate", _event.endDate);
                    cmd.Parameters.AddWithValue("@eventName", _event.eventName);
                    cmd.Parameters.AddWithValue("@eventDescription", _event.eventDescription);
                    cmd.Parameters.AddWithValue("@Filename", _event.Filename);
                    cmd.Parameters.AddWithValue("@Filepath", _event.Filepath);
                    cmd.Parameters.AddWithValue("@CreatedDate", _event.CreatedDate);
                    cmd.Parameters.AddWithValue("@CreatedBy", _event.CreatedBy);
                    cmd.Parameters.AddWithValue("@status", "active");

                    cmd.Parameters.AddWithValue("@thumbnailimg", _event.thumbnailimg);
                    cmd.Parameters.AddWithValue("@thumbnailimgpath", _event.thumbnailimgpath);
                    cmd.Parameters.AddWithValue("@venue", _event.venue);
                    cmd.Parameters.AddWithValue("@starttime", _event.starttime);
                    cmd.Parameters.AddWithValue("@endtime", _event.endtime);

                    cmd.ExecuteNonQuery();


                }

                return "ok";
            }
            catch (Exception ex)
            {
                Log.Error("EventModel.updateEvent", ex);
                return ex.Message;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }


        public DataTable GetEventList()
        {
            SqlConnection con = null;
            DataTable _eventTable = new DataTable();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = null;

                    DateTime currdate = TimeZoneClass.getIndianTimeZoneValues();
                    //DateTime prevdate = currdate.AddDays(-10);

                    query = "select CONVERT (varchar(10), cast([Date] as Date), 103) AS [Date] ,id,eventName,eventDescription,filename,filepath,createddate " +
                        "from eventTable " +
                        "where CONVERT (varchar(10), cast(createddate as Date), 103)='" + currdate.ToString("dd/MM/yyyy").Replace("-", "/") + "' and status='active' " +
                        "ORDER BY Date DESC;";

                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(_eventTable);



                }

                return _eventTable;
            }
            catch (Exception ex)
            {
                Log.Error("EventModel.GetEventList", ex);
                return _eventTable;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }


        public DataTable GetEventListSuperAdmin()
        {
            SqlConnection con = null;
            DataTable _eventTable = new DataTable();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = null;

                    DateTime currdate = TimeZoneClass.getIndianTimeZoneValues();
                    DateTime prevdate = currdate.AddDays(-10);

                    //query = "select CONVERT (varchar(10), cast([Date] as Date), 103) AS [Date] ,id,eventName,eventDescription,filename,filepath,createddate " +
                    //    "from eventTable where status='active' " +
                    //    "ORDER BY Date DESC;";

                    query = "select CONVERT (varchar(10), cast([fromdate] as Date), 103) AS [fromdate], CONVERT (varchar(10), cast([ToDate] as Date), 103) AS [ToDate],id,eventName,eventDescription,filename,filepath,createddate " +
                            "from eventTable where status = 'active' " +
                            "ORDER BY fromdate DESC;";

                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(_eventTable);



                }

                return _eventTable;
            }
            catch (Exception ex)
            {
                Log.Error("EventModel.GetEventListSuperAdmin", ex);
                return _eventTable;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public DataTable GetUpCommingEventList()
        {
            SqlConnection con = null;
            DataTable _eventTable = new DataTable();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = null;

                    DateTime currdate = TimeZoneClass.getIndianTimeZoneValues();
                    //DateTime prevdate = currdate.AddDays(-10);

                    //query = "select CONVERT (varchar(10), cast([Date] as Date), 103) AS [Date] ,id,eventName,eventDescription,filename,filepath,createddate " +
                    //    "from eventTable " +
                    //    "where [Date]>='"+ currdate.ToString("yyyy/MM/dd").Replace("-","/") + "' and status='active' " +
                    //    "ORDER BY Date DESC;";

                    query = "select CONVERT (varchar(10), cast([fromdate] as Date), 103) AS [fromdate], CONVERT (varchar(10), cast([ToDate] as Date), 103) AS [ToDate],id,eventName,eventDescription,filename,filepath,createddate " +
                            "from eventTable " +
                            "where '" + currdate.ToString("yyyy/MM/dd").Replace("-", "/") + "' Between fromdate and todate and status = 'active' " +
                            "ORDER BY[fromdate] DESC";

                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(_eventTable);



                }

                return _eventTable;
            }
            catch (Exception ex)
            {
                Log.Error("EventModel.GetUpCommingEventList", ex);
                return _eventTable;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public List<Eventbl> Get_Last4_UpCommingEventList()
        {
            SqlConnection con = null;
            List<Eventbl> listevent = new List<Eventbl>();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    DateTime dt = TimeZoneClass.getIndianTimeZoneValues();
                    string query = "select Top 4 [FromDate],[ToDate],id,eventName,eventDescription,filename,filepath,createddate,[thumbnailimg],[thumbnailimgpath],venue,starttime,endtime " +
                                    "from eventTable " +
                                    "where status='active' " +
                                    "ORDER BY [FromDate] DESC;";

                    //string query = "select Top 4 CONVERT (varchar(10), cast([Date] as Date), 103) AS [Date] ,id,eventName,eventDescription,filename,filepath,createddate,[thumbnailimg],[thumbnailimgpath],venue,starttime,endtime " +
                    //                "from eventTable " +
                    //                "where Date>='"+ dt.ToString("yyyy/MM/dd") + "' and status='active' " +
                    //                "ORDER BY Date DESC;";

                    SqlCommand cmd = null;

                    cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listevent.Add(new Eventbl()
                        {
                            startDate = reader["FromDate"].ToString(),
                            endDate = reader["ToDate"].ToString(),
                            id = reader["id"].ToString(),
                            eventName = reader["eventName"].ToString(),
                            eventDescription = reader["eventDescription"].ToString(),
                            Filename = reader["filename"].ToString(),
                            Filepath = reader["filepath"].ToString(),
                            CreatedDate = reader["createddate"].ToString(),
                            thumbnailimg = reader["thumbnailimg"].ToString(),
                            thumbnailimgpath = reader["thumbnailimgpath"].ToString(),
                            venue = reader["venue"].ToString(),
                            starttime = reader["starttime"].ToString(),
                            endtime = reader["endtime"].ToString()
                        });
                    }
                    reader.Close();




                }

                return listevent;
            }
            catch (Exception ex)
            {
                Log.Error("EventModel.Get_Last4_UpCommingEventList", ex);
                return listevent;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public string btndelete(string id, string userid)
        {
            SqlConnection con = null;
            try
            {

                string query = "";
                using (con = Connection.getConnection())
                {
                    con.Open();
                    //query = "Delete from eventTable where id=@id";
                    DateTime dt = TimeZoneClass.getIndianTimeZoneValues();

                    query = "update eventtable set [status]='deleted',updateddate=@updateddate , updatedby=@updatedby where id=@id;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@updateddate", dt.ToString("yyyy/MM/dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@updatedby", userid);

                    cmd.ExecuteNonQuery();

                }

                return "ok";

            }
            catch (Exception ex)
            {
                Log.Error("EventModel.btndelete", ex);
                return ex.Message;
            }
            finally
            {
                if (con != null) con.Close();
            }

        }
        public string saveUpdateEvent(Eventbl _editevent)
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

                    query = "update eventtable set [FromDate]=@FromDate,[ToDate]=@ToDate, eventName=@eventName, eventDescription=@eventDescription, Filename=@Filename,Filepath=@Filepath,updateddate=@updateddate,updatedby=@updatedby,[thumbnailimg]=@thumbnailimg,[thumbnailimgpath]=@thumbnailimgpath,venue=@venue,starttime=@starttime,endtime=@endtime where id=@id;";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@FromDate", _editevent.startDate);
                    cmd.Parameters.AddWithValue("@ToDate", _editevent.endDate);
                    cmd.Parameters.AddWithValue("@eventName", _editevent.eventName);
                    cmd.Parameters.AddWithValue("@eventDescription", _editevent.eventDescription);
                    cmd.Parameters.AddWithValue("@Filename", _editevent.Filename);
                    cmd.Parameters.AddWithValue("@Filepath", _editevent.Filepath);
                    cmd.Parameters.AddWithValue("@updateddate", _editevent.updateddate);
                    cmd.Parameters.AddWithValue("@updatedby", _editevent.updatedby);
                    cmd.Parameters.AddWithValue("@id", _editevent.id);

                    cmd.Parameters.AddWithValue("@thumbnailimg", _editevent.thumbnailimg);
                    cmd.Parameters.AddWithValue("@thumbnailimgpath", _editevent.thumbnailimgpath);
                    cmd.Parameters.AddWithValue("@venue", _editevent.venue);
                    cmd.Parameters.AddWithValue("@starttime", _editevent.starttime);
                    cmd.Parameters.AddWithValue("@endtime", _editevent.endtime);
                    cmd.ExecuteNonQuery();


                }

                return "ok";
            }
            catch (Exception ex)
            {
                Log.Error("EventModel.saveUpdateEvent", ex);
                return ex.Message;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }
    }
    public class Eventbl
    {
        public string id { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string eventName { get; set; }
        public string eventDescription { get; set; }
        public string Filename { get; set; }
        public string Filepath { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string updateddate { get; set; }
        public string updatedby { get; set; }

        public string status { get; set; }

        public string thumbnailimg { get; set; }
        public string thumbnailimgpath { get; set; }
        public string venue { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }


    }
}