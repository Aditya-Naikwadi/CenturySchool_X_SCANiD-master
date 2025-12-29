using CenturyRayonSchool.Model;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool
{
    public partial class Admin_Event : System.Web.UI.Page
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

        protected void btnreset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
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

                    string absolutefolderpath = Server.MapPath("~/Uploads/events/");
                    string relativepath = "/Uploads/events";

                    string thumb_absolutefolderpath = Server.MapPath("~/Uploads/events/thumbnails");
                    string thumb_relativepath = "/Uploads/events/thumbnails";

                    Eventbl ev = new Eventbl();

                    ev.startDate = startdate;
                    ev.endDate = enddate;
                    ev.eventName = eventname;
                    ev.eventDescription = eventdesc;
                    ev.CreatedDate = currentdate.ToString("yyyy/MM/dd HH:mm").Replace("-", "/");
                    ev.CreatedBy = Session["username"].ToString();
                    ev.Filepath = "";
                    ev.Filename = "";
                    ev.thumbnailimg = "upcoming_events.png";
                    ev.thumbnailimgpath = thumb_relativepath + "/upcoming_events.png";
                    ev.venue = eventvenue;
                    ev.starttime = starttime;
                    ev.endtime = endtime;

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
                                ev.Filename = postfiles.FileName;
                                ev.Filepath = relativepath + "/" + Path.GetFileName(postfiles.FileName);
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
                                ev.thumbnailimg = postfiles1.FileName;
                                ev.thumbnailimgpath = thumb_relativepath + "/" + Path.GetFileName(postfiles1.FileName);
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
                    //        ev.Filename = _HttpPostedFile.FileName;
                    //        ev.Filepath = relativepath + "/" + Path.GetFileName(_HttpPostedFile.FileName);

                    //    };
                    //}




                    //update database

                    resp = new EventModel().updateEvent(ev);

                    if (resp == "ok")
                    {

                        txteventname.Text = "";
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
                Log.Error("Admin_Event.btnSaveData_Click", ex);
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

    }
}