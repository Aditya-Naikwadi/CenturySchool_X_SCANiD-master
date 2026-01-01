//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;


namespace CenturyRayonSchool
{
    public partial class NewsDescripition : System.Web.UI.Page
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
                    GetNews(id);
                }




            }

        }

        public void GetNews(string id)
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


                    query = "select CONVERT (varchar(10), cast([Date] as Date), 103) AS [Date] ,TopicName,TopicDescription,filename,filepath from newsTable where id='" + id + "' ;";

                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(_newsTable);

                    foreach (DataRow row in _newsTable.Rows)
                    {

                        Date.Text = row["Date"].ToString();

                        Tname.Text = row["TopicName"].ToString();
                        Tdesc.Text = row["TopicDescription"].ToString();

                        filpath.Text = row["filepath"].ToString();
                    }


                    if (filpath.Text.Trim().Length > 0)
                    {
                        isDownloadEnable = true;
                    }



                }


            }
            catch (Exception ex)
            {
                Log.Error("NewsDescripition.GetEvent", ex);

            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        protected void download_Click(object sender, EventArgs e)
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

        }
    }
}