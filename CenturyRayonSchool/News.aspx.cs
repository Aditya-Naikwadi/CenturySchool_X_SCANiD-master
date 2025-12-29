using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace CenturyRayonSchool
{
    public partial class News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                DataTable News = new NewsEventsModel().GetNewsList();

                newsgridview.DataSource = News;
                newsgridview.DataBind();
                DateTime currentdate = TimeZoneClass.getIndianTimeZoneValues();
            }

        }

        protected void newsgridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "downloadfile")
            {
                int rownumber = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = newsgridview.DataKeys[rownumber]..Values[0];

                string path = newsgridview.DataKeys[rownumber].Values[0].ToString();

                string linkurl = Server.MapPath(path);
                WebClient req = new WebClient();

                FileInfo file1 = new FileInfo(linkurl);

                HttpResponse response = HttpContext.Current.Response;

                if (file1.Exists)
                {
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.AddHeader("Content-Disposition", "attachment;filename=\"" + Path.GetFileName(linkurl) + "\"");
                    byte[] data = req.DownloadData(linkurl);
                    response.BinaryWrite(data);
                    response.End();
                }


            }
        }
        public Boolean setVisiblelabel(string filepath)
        {
            if (filepath.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}