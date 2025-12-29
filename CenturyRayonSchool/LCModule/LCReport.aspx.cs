using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.LCModule.Dataset;
using CenturyRayonSchool.LCModule.Reports;
using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.LCModule
{
    public partial class LCReport : System.Web.UI.Page
    {
        Label lblusercode = new Label();
        public string fileurl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblusercode = (Label)Page.Master.FindControl("lblusercode");
            string year = new FeesModel().setActiveAcademicYear();
            lblacademicyear.Text = year;

            if (!IsPostBack)
            {
                loadFormControl();

            }
        }
        protected void stdCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmbStd.SelectedValue.ToString().Equals("Select Std"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        protected void divCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmbDiv.SelectedValue.ToString().Equals("Select Div"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        public void loadFormControl()
        {
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "select std from std where std not in ('ALL','LEFT');";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    DataTable std = new DataTable();
                    adap.Fill(std);
                    std.Rows.Add("ALL");
                    std.Rows.Add("Select Std");                
                                
                    cmbStd.DataSource = std;
                    cmbStd.DataBind();
                    cmbStd.DataTextField = "std";
                    cmbStd.DataValueField = "std";
                    cmbStd.DataBind();
                    cmbStd.SelectedValue = "Select Std";

                    query = "select Div From Div where div Not IN ('ALL');";
                    adap = new SqlDataAdapter(query, con);
                    DataTable div = new DataTable();
                    adap.Fill(div);
                    div.Rows.Add("ALL");
                    div.Rows.Add("Select Div");
                
                    cmbDiv.DataSource = div;
                    cmbDiv.DataBind();
                    cmbDiv.DataTextField = "Div";
                    cmbDiv.DataValueField = "Div";
                    cmbDiv.DataBind();
                    cmbDiv.SelectedValue = "Select Div";

                    asptxtfromdate.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace('-', '/');
                    asptxttodate.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace('-', '/');
                }
            }
            catch (Exception ex)
            {
                Log.Error("LCReport.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        public Lcregister PrintLCReport(SqlConnection con, string std, string div, string fromdate, string todate)
        {

            Lcregister report = new Lcregister();
            DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
            try
            {
                Lcregisterds lcds = new Lcregisterds();
                //string select_std = cmbStd.SelectedValue.ToString();
                //string select_div = cmbDiv.SelectedValue.ToString();
                DateTime from = DateTime.Parse(fromdate);
                DateTime to = DateTime.Parse(todate);
                string fromDateFormatted = from.ToString("dd-MM-yyyy");
                string toDateFormatted = to.ToString("dd-MM-yyyy");



                string year = new FeesModel().setActiveAcademicYear();
                String query = "", percentage = "";
                if (std == "ALL" && div == "ALL")
                {
                    //query = "Select std,div,grno,(fname+' '+mname+' '+Lname) as studentname,dateofleaving as lcdate,lcno,remark as lcremark,academicyear " +
                    //               "From LeavingCertificate " +
                    //               "where academicyear='" + year + "' and TRY_CAST(CreatedDate as datetime) between '" + fromdate + "' and '" + todate + "' order by TRY_CAST(CreatedDate as datetime) asc;";
                    //
                    query = "SET DATEFORMAT dmy; " +
                    "SELECT std, div, grno, (fname + ' ' + mname + ' ' + Lname) AS studentname, " +
                    "dateofleaving AS lcdate, lcno, remark AS lcremark, academicyear " +
                    "FROM LeavingCertificate " +
                    "WHERE TRY_CAST(CreatedDate AS datetime) BETWEEN '" + fromDateFormatted + " 00:00:00' AND '" + toDateFormatted + " 23:59:59' " +
                    "ORDER BY TRY_CAST(CreatedDate AS datetime);";



                }

                else if (div == "ALL")
                {
                    //query = "Select std,div,grno,(fname+' '+mname+' '+Lname) as studentname,dateofleaving as lcdate,lcno,remark as lcremark,academicyear " +
                    //        "From LeavingCertificate " +
                    //        "where std='" + std + "' and academicyear='" + year + "' and TRY_CAST(CreatedDate as datetime) between '" + fromdate + "' and '" + todate + "' order by TRY_CAST(CreatedDate as datetime) asc;";
                    //
                    query = "SET DATEFORMAT dmy; " +
                    "SELECT std, div, grno, (fname + ' ' + mname + ' ' + Lname) AS studentname, " +
                    "dateofleaving AS lcdate, lcno, remark AS lcremark, academicyear " +
                    "FROM LeavingCertificate " +
                    "WHERE std = '" + std + "' " +
                    "AND TRY_CAST(CreatedDate AS datetime) BETWEEN TRY_CAST('" + fromDateFormatted + " 00:00:00' AS datetime) " +
                    "AND TRY_CAST('" + toDateFormatted + " 23:59:59' AS datetime) " +
                    "ORDER BY TRY_CAST(CreatedDate AS datetime) ASC;";


                }

                else
                {
                    //query = "Select std,div,grno,(fname+' '+mname+' '+Lname) as studentname,dateofleaving as lcdate,lcno,remark as lcremark,academicyear " +
                    //         "From LeavingCertificate " +
                    //         "where std='" + std + "' and div='" + div + "' and academicyear='" + year + "' and TRY_CAST(CreatedDate as datetime) between '" + fromdate + "' and '" + todate + "' order by TRY_CAST(CreatedDate AS datetime) asc;";
                    query = "SET DATEFORMAT dmy; " +
                    "SELECT std, div, grno, (fname + ' ' + mname + ' ' + Lname) AS studentname, " +
                    "dateofleaving AS lcdate, lcno, remark AS lcremark, academicyear " +
                    "FROM LeavingCertificate " +
                    "WHERE std = '" + std + "'and div='" + div + "' " +
                    "AND TRY_CAST(CreatedDate AS datetime) BETWEEN TRY_CAST('" + fromDateFormatted + " 00:00:00' AS datetime) " +
                    "AND TRY_CAST('" + toDateFormatted + " 23:59:59' AS datetime) " +
                    "ORDER BY TRY_CAST(CreatedDate AS datetime) ASC;";


                }

                SqlCommand cmd = new SqlCommand(query, con);
                Lcregisterds lcregister = new Lcregisterds();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(lcregister.Tables[0]);

                report.SetDataSource(lcregister.Tables["lcregister"]);

                report.SetParameterValue("academicyear", year);
                return report;

            }
            catch (Exception ex)
            {
                Log.Error("LCreport.PrintLCReport", ex);
                return null;
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void printreport_ServerClick(object sender, EventArgs e)
        {
            string std = "", div = "", fromdate = "", todate = "";
            std = cmbStd.SelectedValue.ToString();
            div = cmbDiv.SelectedValue.ToString();
            fromdate = asptxtfromdate.Text;
            todate = asptxttodate.Text;

            SqlConnection con = null;
            Lcregister report = new Lcregister();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    if (std == "Select Std")
                    {
                        std = "ALL";
                    }
                    if (div == "Select Div")
                    {
                        div = "ALL";
                    }
                    report = PrintLCReport(con, std, div, fromdate, todate);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("LCReport.printreport_ServerClick", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

                string folderpath = Server.MapPath("LCFile");
                string filename = "LCReport_" + std + div + ".pdf";

                report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(Server.MapPath("~/LCModule/LCFile/" + filename));
                Response.End();
            }
        }
    }
}