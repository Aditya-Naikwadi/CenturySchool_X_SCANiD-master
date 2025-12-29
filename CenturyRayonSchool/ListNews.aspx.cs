using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool
{
    public partial class ListNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null && this.Page.Master != null)
                {
                    DataTable News = new DataTable();

                    if (Session["usertype"].ToString().Equals("SuperAdmin"))
                    {
                        News = new NewsEventsModel().GetNewsListSuperAdmin();
                    }
                    else
                    {
                        News = new NewsEventsModel().GetNewsList();
                    }


                    newsgridview.DataSource = News;
                    newsgridview.DataBind();


                    Label lbl = (Label)this.Page.Master.FindControl("admin_username_lbl");
                    lbl.Text = Session["username"].ToString() + " ( " + Session["usertype"].ToString() + " ) ";

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }

        }

        protected void newsgridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                if (e.CommandName == "deletenews")
                {
                    int rownumber = Convert.ToInt32(e.CommandArgument);

                    GridViewRow row = newsgridview.Rows[rownumber];

                    string id = row.Cells[0].Text;

                    string resp = new NewsEventsModel().btndelete(id, Session["username"].ToString());



                    if (resp == "ok")
                    {
                        DataTable News = new NewsEventsModel().GetNewsList();
                        newsgridview.DataSource = null;
                        newsgridview.DataSource = News;
                        newsgridview.DataBind();
                        lblmessage.Text = "News Removed Successfully!!";
                    }
                    else
                    {
                        lblmessage.Text = resp;
                    }




                }
            }

            if (e.CommandName == "editnews")
            {
                int rownumber = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = newsgridview.Rows[rownumber];

                string id = row.Cells[0].Text;
                Response.Redirect("EditNews.aspx?id=" + id);


            }

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


        public Boolean setButtonVisibility(string date)
        {
            if (Session["usertype"].ToString() == "Admin")
            {
                if (date == DateTime.Now.ToString("yyyy/MM/dd"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
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