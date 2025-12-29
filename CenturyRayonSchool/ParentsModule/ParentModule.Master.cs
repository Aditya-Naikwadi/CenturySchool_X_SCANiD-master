using System;

namespace CenturyRayonSchool.ParentsModule
{
    public partial class ParentModule : System.Web.UI.MasterPage
    {
        public string std = "", password = "", year = "", personname = "", GRNO = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["GRNO"] != null)
            {
                lblstd.Text = Session["std"].ToString();
                password = Session["ParentPassword"].ToString();
                lblyear.Text = Session["academicyear"].ToString();
                lblusername.Text = Session["fullname"].ToString();
                lblgrno.Text = Session["GRNO"].ToString();
                lblcid.Text = Session["userid"].ToString();
                lblAcademicyear.Text = lblyear.Text;
            }
            else
            {
                Response.Redirect("~/ParentsModule/ParentsLogin.aspx");
            }

        }
    }
}