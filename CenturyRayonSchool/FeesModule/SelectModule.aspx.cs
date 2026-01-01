//using CenturyRayonSchool.FeesModule.Model;
using System;

namespace CenturyRayonSchool.FeesModule
{
    public partial class SelectModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string year = new FeesModel().setActiveAcademicYear();
                lblAcademicyear.Text = year;

                if (Session["userid"] != null)
                {
                    lbldisplayusername.Text = Session["personname"].ToString();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }


            }
        }

        protected void feemodule_ServerClick(object sender, EventArgs e)
        {
            // Specify the URL you want to redirect to
            //string redirectUrl = "Dashboard.aspx";

            // Open the URL in a new tab
            //Response.Write("<script>window.open('" + redirectUrl + "','_blank');</script>");
            Response.Redirect("Dashboard.aspx");
        }

        protected void resultmodle_ServerClick(object sender, EventArgs e)
        {
            // Specify the URL you want to redirect to
            //string redirectUrl = ResolveUrl("~/MarksheetModule/Dashboard.aspx");

            // Open the URL in a new tab
            //Response.Write("<script>window.open('" + redirectUrl + "','_blank');</script>");
            Response.Redirect("~/MarksheetModule/Dashboard.aspx");
        }
    }
}