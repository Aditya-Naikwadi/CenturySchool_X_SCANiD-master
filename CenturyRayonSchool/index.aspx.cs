using CenturyRayonSchool.Model;
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
                        staffbirthday += $"<li style='font-size:16px; margin:6px 0; list-style:none;'>{cakeIcon}{tb.fullname}</li>";
                    }
                    else
                    {
                        studentbirthday += $"<li style='font-size:16px; margin:6px 0; list-style:none;'>{cakeIcon}{tb.fullname} {tb.std}-{tb.div}</li>";
                    }
                }

                litStudentBirthday.Text = studentbirthday;
                litStaffBirthday.Text = staffbirthday;

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