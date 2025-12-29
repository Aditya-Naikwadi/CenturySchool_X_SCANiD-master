using CenturyRayonSchool.FeesModule.Model;
using System;
using System.Collections.Generic;

namespace CenturyRayonSchool.FeesModule
{
    public partial class Dashboard : System.Web.UI.Page
    {
        public List<string> listmodules = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string year = new FeesModel().setActiveAcademicYear();
                lblAcademicyear.Text = year;

                if (Session["userid"] != null)
                {
                    lbldisplayusername.Text = Session["personname"].ToString();

                    listmodules = (List<string>)Session["listmodules"];

                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }


            }

        }

        public Boolean checkStatus(string modulename, List<string> listmodule)
        {
            if (listmodule.Contains(modulename))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void goto_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("SelectModule.aspx");
        }
    }
}