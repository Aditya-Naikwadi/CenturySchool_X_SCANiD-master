//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool
{
    public partial class EditEvents : System.Web.UI.Page
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
                        GetEvent(id);
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
                    string startdate = Convert.ToDateTime(hiddenstartdatetext.Text).ToString("yyyy/MM/dd").Replace("-", "/");
                    string enddate = Convert.ToDateTime(hiddenenddatetext.Text).ToString("yyyy/MM/dd").Replace("-", "/");
                    string eventname = txteventname.Text;
                    string eventdesc = txtdescription.InnerText;
                    string eventvenue = txteventvenue.Text;
                    string starttime = txtStarttime.Text;
                    string endtime = txtEndtime.Text;

                    string absolutefolderpath = Server.MapPath("~/Uploads/news/");
                    string relativepath = "/Uploads/news";

                    string thumb_absolutefolderpath = Server.MapPath("~/Uploads/events/thumbnails");
                    string thumb_relativepath = "/Uploads/events/thumbnails";

                    Eventbl en = new Eventbl();
                    en.startDate = startdate;
                    en.endDate = enddate;
                    en.eventName = txteventname.Text;
                    en.eventDescription = txtdescription.InnerText;
                    en.id = txtid.Text;
                    en.Filename = filename.Text;
                    en.Filepath = filepath.Text;
                    en.updateddate = currentdate.ToString("yyyy/MM/dd HH:mm");
                    en.updatedby = Session["username"].ToString();
                    en.thumbnailimg = thumb_filename.Text;
                    en.thumbnailimgpath = thumb_filepath.Text;
                    en.venue = eventvenue;
                    en.starttime = starttime;
                    en.endtime = endtime;

                    //Get Files Uploaded
                    if (!Directory.Exists(absolutefolderpath))
                    {
                        Directory.CreateDirectory(absolutefolderpath);
                    }

                    if (!Directory.Exists(thumb_absolutefolderpath))
                    {
                        Directory.CreateDirectory(thumb_absolutefolderpath);
                    }

                    //upload any files required
                    if (Fileupload2.PostedFiles.Count() > 0)
                    {
                        foreach (HttpPostedFile postfiles in Fileupload2.PostedFiles)
                        {
                            if (postfiles.FileName.Length > 0)
                            {
                                Fileupload2.SaveAs(absolutefolderpath + "/" + Path.GetFileName(postfiles.FileName));

                                //create database object
                                en.Filename = postfiles.FileName;
                                en.Filepath = relativepath + "/" + Path.GetFileName(postfiles.FileName);
                            }
                        }
                    }

                    //upload thumbnail image
                    if (thumbFileupload1.PostedFiles.Count() > 0)
                    {
                        foreach (HttpPostedFile postfiles1 in thumbFileupload1.PostedFiles)
                        {
                            if (postfiles1.FileName.Length > 0)
                            {
                                thumbFileupload1.SaveAs(thumb_absolutefolderpath + "/" + Path.GetFileName(postfiles1.FileName));

                                //create database object
                                en.thumbnailimg = postfiles1.FileName;
                                en.thumbnailimgpath = thumb_relativepath + "/" + Path.GetFileName(postfiles1.FileName);
                            }

                        }
                    }

                    //HttpFileCollection _HttpFileCollection = Request.Files;

                    //for (int i = 0; i < _HttpFileCollection.Count; i++)
                    //{
                    //    HttpPostedFile _HttpPostedFile = _HttpFileCollection[i];
                    //    if (_HttpPostedFile.ContentLength > 0)
                    //    {
                    //        _HttpPostedFile.SaveAs(absolutefolderpath + "/" + Path.GetFileName(_HttpPostedFile.FileName));

                    //        //create database object
                    //        en.Filename = _HttpPostedFile.FileName;
                    //        en.Filepath = relativepath + "/" + Path.GetFileName(_HttpPostedFile.FileName);

                    //    };
                    //}

                    //update database

                    resp = new EventModel().saveUpdateEvent(en);

                    if (resp == "ok")
                    {

                        txteventname.Text = "";
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
                Log.Error("EditEvents.btnSaveData_Click", ex);
                lbluploadmessage.Text = ex.Message;
            }
            finally
            {
                if (resp == "ok")
                {
                    Response.Redirect("ListEvents.aspx");
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


                    query = "select CONVERT(varchar(10),cast([FromDate] as Date),103) AS [FromDate],CONVERT(varchar(10),cast([ToDate] as Date),103) AS [ToDate],eventName,eventDescription,filename,filepath,[thumbnailimg],[thumbnailimgpath],venue,starttime,endtime from eventTable where id='" + id + "';";

                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(_eventTable);

                    foreach (DataRow row in _eventTable.Rows)
                    {

                        txtstartdate.Text = row["FromDate"].ToString();
                        hiddenstartdatetext.Text = row["FromDate"].ToString();

                        txtenddate.Text = row["ToDate"].ToString();
                        hiddenenddatetext.Text = row["ToDate"].ToString();

                        txteventname.Text = row["eventName"].ToString();
                        txtdescription.InnerText = row["eventDescription"].ToString();
                        filename.Text = row["Filename"].ToString();
                        filepath.Text = row["Filepath"].ToString();
                        thumb_filename.Text = row["thumbnailimg"].ToString();
                        thumb_filepath.Text = row["thumbnailimgpath"].ToString();
                        txteventvenue.Text = row["venue"].ToString();
                        txtStarttime.Text = row["starttime"].ToString();
                        txtEndtime.Text = row["endtime"].ToString();
                    }



                }


            }
            catch (Exception ex)
            {
                Log.Error("EventsModel.GetEvent", ex);

            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin_Event.aspx");
        }
    }
}