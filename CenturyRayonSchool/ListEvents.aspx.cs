using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool
{
    public partial class ListEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null && this.Page.Master != null)
                {
                    DataTable Event = new DataTable();
                    if (Session["usertype"].ToString().Equals("SuperAdmin"))
                    {
                        Event = new EventModel().GetEventListSuperAdmin();
                    }
                    else
                    {
                        Event = new EventModel().GetEventList();
                    }


                    eventgridview.DataSource = Event;
                    eventgridview.DataBind();


                    Label lbl = (Label)this.Page.Master.FindControl("admin_username_lbl");
                    lbl.Text = Session["username"].ToString() + " ( " + Session["usertype"].ToString() + " ) ";

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }

        }
        protected void eventgridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                if (e.CommandName == "deleteevent")
                {
                    int rownumber = Convert.ToInt32(e.CommandArgument);

                    GridViewRow row = eventgridview.Rows[rownumber];

                    string id = row.Cells[0].Text;



                    string resp = new EventModel().btndelete(id, Session["username"].ToString());



                    if (resp == "ok")
                    {
                        DataTable Event = new EventModel().GetEventList();
                        eventgridview.DataSource = null;
                        eventgridview.DataSource = Event;
                        eventgridview.DataBind();
                        lblmessage.Text = "Event Removed Successfully!!";
                    }
                    else
                    {
                        lblmessage.Text = resp;
                    }

                }
            }


            if (e.CommandName == "editevent")
            {
                int rownumber = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = eventgridview.Rows[rownumber];

                string id = row.Cells[0].Text;
                Response.Redirect("EditEvents.aspx?id=" + id);


            }

            if (e.CommandName == "downloadfile")
            {
                int rownumber = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = newsgridview.DataKeys[rownumber]..Values[0];

                string path = eventgridview.DataKeys[rownumber].Values[0].ToString();

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
                if (date == DateTime.Now.ToString("yyyy/MM/dd").Replace("-", "/"))
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