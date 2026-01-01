////using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace CenturyRayonSchool
{
    public partial class index : System.Web.UI.Page
    {
        public string filepath = "";
        public string studentbirthday = "";
        public string staffbirthday = "";
        public string upcommingevent = "";
        public Boolean isEvent = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /* Reference controls removed from .aspx
                DataTable News = new NewsEventsModel().GetNewsList();
                ListViewNews.DataSource = News;
                ListViewNews.DataBind();

                DataTable Event = new EventModel().GetUpCommingEventList();
                ListViewEvent.DataSource = Event;
                ListViewEvent.DataBind();

                List<TodayBirthday> listbirth = new IndexPageModel().getTodayBirthday();

                foreach (TodayBirthday tb in listbirth)
                {
                    string cakeIcon = "<i class='bi bi-cake2 text-warning me-2'></i>";

                    if (tb.isstaff)
                    {
                        staffbirthday += string.Format("<li style='font-size:16px; margin:6px 0; list-style:none;'>{0}{1}</li>", cakeIcon, tb.fullname);
                    }
                    else
                    {
                        studentbirthday += string.Format("<li style='font-size:16px; margin:6px 0; list-style:none;'>{0}{1} {2}-{3}</li>", cakeIcon, tb.fullname, tb.std, tb.div);
                    }
                }

                litStudentBirthday.Text = studentbirthday;
                litStaffBirthday.Text = staffbirthday;
                */

            }
        }

        public string showUrl(string msg)
        {
            if (msg.Length == 0)
            {
                return "#";
            }
            else
            {
                return msg;
            }
        }
    }
}