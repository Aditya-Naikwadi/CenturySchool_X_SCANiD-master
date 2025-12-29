using CenturyRayonSchool.FeesModule.Model;
using System;

namespace CenturyRayonSchool.BonafideModule
{
    public partial class BonafideDashboard : System.Web.UI.Page
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
    }
}