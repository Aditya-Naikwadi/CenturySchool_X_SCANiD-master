using System;

namespace CenturyRayonSchool.FeesModule
{
    public partial class ModuleSelection1 : System.Web.UI.MasterPage
    {
        public string username = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["userid"] != null)
            {
                username = Session["username"].ToString();
                lblusercode.Text = Session["username"].ToString();
                lblmasterusername.Text = Session["personname"].ToString();

            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        protected void btnlogout_ServerClick(object sender, EventArgs e)
        {
            Session.Abandon();

            Response.Redirect("~/Index.aspx");
        }
    }
}