//using CenturyRayonSchool.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.AdmissionModule
{
    public partial class Admissionlabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Session["startdate"]= string.Empty;

            if (Session["CHECKdate"].ToString() != string.Empty || Session["CHECKdate"].ToString() != null)
            {

                if (Session["CHECKdate"].ToString() == "startdt")
                {
                    lblstarted.Visible = true;
                    Session["startdate"] = "";
                }
               

                else if (Session["CHECKdate"].ToString() == "enddt")
                {
                    //if (Session["startend"].ToString() == "enddt")
                    //{
                    lblended.Visible = true;
                    Session["startend"] = "";
                    // }
                }
            }
            //////////
            /// if (Session["startdate"].ToString()==string.Empty || Session["startdate"].ToString() == null)
            //{


            //}
            //else
            //{
            //    if (Session["startdate"].ToString() == "startdt")
            //    {
            //        lblstarted.Visible = true;
            //        Session["startdate"] = "";
            //    }
            //}

            //if (Session["startend"].ToString() == string.Empty || Session["startend"].ToString() == null)
            //{

            //}
            //else
            //{
            //    if (Session["startend"].ToString() == "enddt")
            //    {
            //        lblended.Visible = true;
            //        Session["startend"] = "";
            //    }
            //}
        }
      
    }
}