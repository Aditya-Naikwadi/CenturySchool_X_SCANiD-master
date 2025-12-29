using CenturyRayonSchool.Model;
using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool
{
    public partial class Admin_News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null && this.Page.Master != null)
                {
                    Label lbl = (Label)this.Page.Master.FindControl("admin_username_lbl");
                    lbl.Text = Session["username"].ToString() + " ( " + Session["usertype"].ToString() + " ) ";
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        protected void btnsavedata_Click(object sender, EventArgs e)
        {
            string resp = "";
            try
            {
                if (Session["username"] != null)
                {
                    DateTime currentdate = TimeZoneClass.getIndianTimeZoneValues();
                    string selecteddate = Convert.ToDateTime(hiddendatetext.Text).ToString("yyyy/MM/dd").Replace("-", "/");
                    string topicname = txttopicname.Text;
                    string topicdesc = txtdescription.InnerText;

                    string absolutefolderpath = Server.MapPath("~/Uploads/news/");
                    string relativepath = "/Uploads/news";

                    NewsEvent ne = new NewsEvent();

                    ne.Date = selecteddate;
                    ne.TopicName = topicname;
                    ne.TopicDescription = topicdesc;
                    ne.CreatedDate = currentdate.ToString("yyyy/MM/dd HH:mm").Replace("-", "/");
                    ne.CreatedBy = Session["username"].ToString();
                    ne.Filepath = "";
                    ne.Filename = "";

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
                            ne.Filename = _HttpPostedFile.FileName;
                            ne.Filepath = relativepath + "/" + Path.GetFileName(_HttpPostedFile.FileName);

                        };
                    }

                    //update database

                    resp = new NewsEventsModel().saveAddNews(ne);

                    if (resp == "ok")
                    {

                        txttopicname.Text = "";
                        txtdescription.InnerText = "";
                        lbluploadmessage.Text = "Data Saved Successfully";
                    }
                    else
                    {

                        lbluploadmessage.Text = resp;
                    }



                }
            }
            catch (Exception ex)
            {
                Log.Error("Admin_News.btnsavedata_Click", ex);
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
            Response.Redirect(Request.RawUrl);
        }
    }
}