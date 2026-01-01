//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;


namespace CenturyRayonSchool
{
    public partial class EventDescripition : System.Web.UI.Page

    {
        public Boolean isDownloadEnable = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
                {

                    string id = Request.QueryString["id"];
                    id1.Text = id;
                    GetEvent(id);
                }


            }

        }
        public void GetEvent(string id)
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


                    query = "select CONVERT (varchar(10), cast([fromdate] as Date), 103) AS [fromdate], CONVERT (varchar(10), cast([ToDate] as Date), 103) AS [ToDate],eventName,eventDescription,filename,filepath,venue,starttime,endtime from eventTable where id='" + id + "';";

                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(_eventTable);

                    foreach (DataRow row in _eventTable.Rows)
                    {

                        Date.Text = row["fromdate"].ToString() + " To " + row["ToDate"].ToString();

                        Ename.Text = row["eventName"].ToString();
                        Edesc.Text = row["eventDescription"].ToString();

                        filpath.Text = row["Filepath"].ToString();
                        eventvenue.Text = row["venue"].ToString();
                        eventtime.Text = row["starttime"].ToString() + " to " + row["endtime"].ToString();
                    }
                    if (filpath.Text.Trim().Length > 0)
                    {
                        isDownloadEnable = true;
                    }



                }


            }
            catch (Exception ex)
            {
                Log.Error("EventDescripition.GetEvent", ex);

            }
            finally
            {
                if (con != null) con.Close();
            }
        }
        protected void downloadevent_Click(object sender, EventArgs e)
        {
            string fpath = filpath.Text;
            string absolutepath = Server.MapPath(fpath);
            FileInfo file = new FileInfo(absolutepath);
            if (File.Exists(absolutepath))
            {
                // Clear Rsponse reference  
                Response.Clear();
                // Add header by specifying file name  
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                // Add header for content length  
                Response.AddHeader("Content-Length", file.Length.ToString());
                // Specify content type  
                Response.ContentType = "text/plain";
                // Clearing flush  
                Response.Flush();
                // Transimiting file  
                Response.TransmitFile(file.FullName);
                Response.End();
            }
            else Label1.Text = "Requested file is not available to download";


        }
    }
}