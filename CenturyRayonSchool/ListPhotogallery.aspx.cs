using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool
{
    public partial class ListPhotogallery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null && this.Page.Master != null)
                {
                    DataTable photot = new DataTable();
                    if (Session["usertype"].ToString() == "Admin")
                    {
                        //photot = new PhotoGalleryModel().GetPhotoGalleryList("all", DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/'));
                        photot = new PhotoGalleryModel().GetPhotoGalleryList("all", "all");
                    }
                    else if ((Session["usertype"].ToString() == "SuperAdmin"))
                    {
                        photot = new PhotoGalleryModel().GetPhotoGalleryList("all", "all");
                    }

                    photogridview.DataSource = photot;
                    photogridview.DataBind();

                    List<PhotoGalleryMenu> gallerytbl = new List<PhotoGalleryMenu>();
                    gallerytbl = new PhotoGalleryModel().GetPhotoGalleryMenuItems();
                    gallerytbl.Add(new PhotoGalleryMenu() { galleryname = "ALL" });

                    filtergalleryname.DataTextField = "galleryname";
                    filtergalleryname.DataValueField = "galleryname";
                    filtergalleryname.DataSource = gallerytbl;
                    filtergalleryname.DataBind();
                    filtergalleryname.SelectedValue = "ALL";

                    Label lbl = (Label)this.Page.Master.FindControl("admin_username_lbl");
                    lbl.Text = Session["username"].ToString() + " ( " + Session["usertype"].ToString() + " ) ";

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }

        }

        protected void photogridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                if (e.CommandName == "removeImage")
                {
                    int rownumber = Convert.ToInt32(e.CommandArgument);

                    GridViewRow row = photogridview.Rows[rownumber];

                    string id = row.Cells[0].Text;

                    string resp = new PhotoGalleryModel().removeImageFromGallery(id);



                    if (resp == "ok")
                    {
                        DataTable photot = new PhotoGalleryModel().GetPhotoGalleryList("all");


                        photogridview.DataSource = null;
                        photogridview.DataSource = photot;
                        photogridview.DataBind();
                        lblmessage.Text = "Image removed successfully.";
                    }
                    else
                    {
                        lblmessage.Text = resp;
                    }




                }
            }

        }
        public Boolean setButtonVisibility(string createddate)
        {
            if (Session["usertype"].ToString() == "Admin")
            {
                if (createddate == DateTime.Now.ToString("yyyy/MM/dd"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }




        }

        protected void disablebtn_Click(object sender, EventArgs e)
        {
            string galleryname = filtergalleryname.SelectedValue.ToString();
            string resp = new PhotoGalleryModel().DisableGallery(galleryname);
            if (resp == "ok")
            {
                lblmessage.Text = "Gallery Disabled successfully.";
            }
            else
            {
                lblmessage.Text = "Error: " + resp;
            }

        }
    }
}