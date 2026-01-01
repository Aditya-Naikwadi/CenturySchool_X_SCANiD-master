//using CenturyRayonSchool.FeesModule.Model;
using System;

namespace CenturyRayonSchool.LCModule
{
    public partial class LCDashboard : System.Web.UI.Page
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