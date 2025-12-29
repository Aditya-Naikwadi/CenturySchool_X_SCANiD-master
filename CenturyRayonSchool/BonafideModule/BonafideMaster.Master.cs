using System;
using System.Collections.Generic;

namespace CenturyRayonSchool.BonafideModule
{
    public partial class BonafideMaster : System.Web.UI.MasterPage
    {
        public List<string> listmodules = new List<string>();
        public string username = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] != null)
            {
                username = Session["username"].ToString();
                lblusercode.Text = Session["username"].ToString();
                lblmasterusername.Text = Session["personname"].ToString();
                listmodules = (List<string>)Session["listmodules"];
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