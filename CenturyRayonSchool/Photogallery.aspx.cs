//using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool
{
    public partial class Photogallery1 : System.Web.UI.Page
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                try
                {
                    //upload image files

                    string galleryname = txtgalleryname.Text;
                    string gallerydescp = txtgalldescp.Text;
                    string foldername = galleryname.Replace(" ", "_");
                    List<PhotoGalleryTbl> listphoto = new List<PhotoGalleryTbl>();


                    string absolutefolderpath = Server.MapPath("~/Uploads/Photogallery/" + foldername);
                    string relativepath = "/Uploads/Photogallery/" + foldername;

                    if (!Directory.Exists(absolutefolderpath))
                    {
                        Directory.CreateDirectory(absolutefolderpath);
                    }

                    HttpFileCollection _HttpFileCollection = Request.Files;

                    if (_HttpFileCollection.Count > 0)
                    {
                        if (_HttpFileCollection[0].ContentLength > 0)
                        {


                            for (int i = 0; i < _HttpFileCollection.Count; i++)
                            {
                                HttpPostedFile _HttpPostedFile = _HttpFileCollection[i];
                                if (_HttpPostedFile.ContentLength > 0)
                                {
                                    _HttpPostedFile.SaveAs(absolutefolderpath + "/" + Path.GetFileName(_HttpPostedFile.FileName));

                                    //create database object
                                    listphoto.Add(new PhotoGalleryTbl()
                                    {
                                        galleryname = galleryname,
                                        description = gallerydescp,
                                        filename = Path.GetFileName(_HttpPostedFile.FileName),
                                        filepath = relativepath + "/" + Path.GetFileName(_HttpPostedFile.FileName)

                                    });
                                };
                            }

                            string resp = new PhotoGalleryModel().updatePhotogallery(listphoto, Session["username"].ToString());

                            if (resp == "ok")
                            {


                                err_label_fileimage.Visible = false;
                                txtgalleryname.Text = "";
                                lbluploadmessage.Text = "Images Uploaded Successfully";
                            }
                            else
                            {
                                err_message.Visible = true;
                                err_message.Text = resp;
                                lbluploadmessage.Text = resp;
                            }




                        }
                        else
                        {
                            err_label_fileimage.Visible = true;
                            lbluploadmessage.Text = "No files found";
                        }
                    }
                    else
                    {
                        err_label_fileimage.Visible = true;
                        lbluploadmessage.Text = "No files found";
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Photogallery.btnSave_Click", ex);
                    lbluploadmessage.Text = ex.Message;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }




    }
}
