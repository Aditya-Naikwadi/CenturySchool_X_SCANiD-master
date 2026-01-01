//using CenturyRayonSchool.FeesModule.Model;
using System;
using System.Collections.Generic;

namespace CenturyRayonSchool.MarksheetModule
{
    public partial class Dashboard : System.Web.UI.Page
    {
        public List<string> listmodules = new List<string>();
        string std = "", div = "";
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
                    std = Session["std"].ToString();
                    div = Session["div"].ToString();
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

            Response.Redirect("~/FeesModule/SelectModule.aspx");
        }
    }
}