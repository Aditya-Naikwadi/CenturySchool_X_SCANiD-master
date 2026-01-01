//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool
{
    public partial class EditNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null && this.Page.Master != null)
                {
                    Label lbl = (Label)this.Page.Master.FindControl("admin_username_lbl");
                    lbl.Text = Session["username"].ToString() + " ( " + Session["usertype"].ToString() + " ) ";

                    if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
                    {

                        string id = Request.QueryString["id"];
                        txtid.Text = id;
                        GetNews(id);
                    }


                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnSaveData_Click(object sender, EventArgs e)
        {
            string resp = "";
            try
            {
                if (Session["username"] != null)
                {
                    DateTime currentdate = TimeZoneClass.getIndianTimeZoneValues();
                    string selecteddate = Convert.ToDateTime(hiddendatetext.Text).ToString("yyyy/MM/dd");
                    string topicname = txttopicname.Text;
                    string topicdesc = txtdescription.InnerText;


                    string absolutefolderpath = Server.MapPath("~/Uploads/news/");
                    string relativepath = "/Uploads/news";

                    NewsEvent en = new NewsEvent();
                    en.Date = selecteddate;
                    en.TopicName = txttopicname.Text;
                    en.TopicDescription = txtdescription.InnerText;
                    en.id = txtid.Text;
                    en.Filename = filename.Text;
                    en.Filepath = filepath.Text;
                    en.updateddate = currentdate.ToString("yyyy/MM/dd HH:mm");
                    en.updatedby = Session["username"].ToString();

                    //Get Files Uploaded
                    if (!Directory.Exists(absolutefolderpath))
                    {
                        Directory.CreateDirectory(absolutefolderpath);
                    }

                    HttpFileCollection _HttpFileCollection = Request.Files;

                    for (int i = 0; i < _HttpFileCollection.Count; i++)
                    {
                        HttpPostedFile _HttpPostedFile = _HttpFileCollection[i];
                        if (_HttpPostedFile.ContentLength > 0)
                        {
                            _HttpPostedFile.SaveAs(absolutefolderpath + "/" + Path.GetFileName(_HttpPostedFile.FileName));

                            //create database object
                            en.Filename = _HttpPostedFile.FileName;
                            en.Filepath = relativepath + "/" + Path.GetFileName(_HttpPostedFile.FileName);

                        };
                    }

                    //update database

                    resp = new NewsEventsModel().saveUpdateNews(en);

                    if (resp == "ok")
                    {

                        txttopicname.Text = "";
                        txtdescription.InnerText = "";
                        lbluploadmessage.Text = "Data Updated Successfully";
                    }
                    else
                    {

                        lbluploadmessage.Text = resp;
                    }



                }
            }
            catch (Exception ex)
            {
                Log.Error("EditNews.btnSaveData_Click", ex);
                lbluploadmessage.Text = ex.Message;
            }
            finally
            {
                if (resp == "ok")
                {
                    Response.Redirect("ListNews.aspx");
                }
            }
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin_News.aspx");
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


                    query = "select CONVERT (varchar(10), cast([Date] as Date), 103) AS [Date] ,topicName,topicDescription,filename,filepath from newsTable where id='" + id + "';";

                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(_newsTable);

                    foreach (DataRow row in _newsTable.Rows)
                    {

                        txtselecteddate.Text = row["Date"].ToString();
                        hiddendatetext.Text = row["Date"].ToString();
                        txttopicname.Text = row["TopicName"].ToString();
                        txtdescription.InnerText = row["TopicDescription"].ToString();
                        filename.Text = row["Filename"].ToString();
                        filepath.Text = row["Filepath"].ToString();
                    }



                }


            }
            catch (Exception ex)
            {
                Log.Error("NewsEventsModel.GetNews", ex);

            }
            finally
            {
                if (con != null) con.Close();
            }
        }
    }
}