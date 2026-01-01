//using CenturyRayonSchool.Model;
using System;

namespace CenturyRayonSchool
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void logoutanch_Click(object sender, EventArgs e)
        {
            if (Session["userid"] != null)
            {

                Connection.logoutfun(Session["userid"].ToString(), Session["username"].ToString(), "logout");

            }
            Response.Redirect("Index.aspx");
        }
    }
}