//using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.MarksheetModule.DataSet;
using CenturyRayonSchool.MarksheetModule.DataSet.ClassTeacherReport;
using CenturyRayonSchool.MarksheetModule.DataSet.ds10;
using CenturyRayonSchool.MarksheetModule.DataSet.ds1to2;
using CenturyRayonSchool.MarksheetModule.Reports;
//using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.MarksheetModule
{
    public partial class ConsolidationReport1to8 : System.Web.UI.Page
    {
        DataTable teacherslist = new DataTable();
        Label lblusercode = new Label();
        public string fileurl = "";
        string std_sess = "", div_sess = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblusercode = (Label)Page.Master.FindControl("lblusercode");
            string year = new FeesModel().setActiveAcademicYear();
            lblacademicyear.Text = year;
            if (Session["std"] != null && Session["div"] != null)
            {
                std_sess = Session["std"].ToString();
                div_sess = Session["div"].ToString();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

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
                    std.Rows.Add("Select Std");
                    cmbStd.DataSource = std;
                    cmb_std.DataSource = std;
                    cmbStd.DataBind();
                    cmb_std.DataBind();
                    cmbStd.DataTextField = "std"; cmb_std.DataTextField = "std";
                    cmbStd.DataValueField = "std"; cmb_std.DataValueField = "std";
                    cmbStd.DataBind(); cmb_std.DataBind();
                    //cmbStd.SelectedValue = "Select Std";
                    if (!string.IsNullOrEmpty(std_sess))
                    {
                        cmbStd.SelectedValue = std_sess;
                        cmb_std.SelectedValue = std_sess;
                        cmbStd.Enabled = false;
                        cmb_std.Enabled = false;

                    }
                    else
                    {
                        // If std_sess is empty, set the default selected value
                        cmbStd.SelectedValue = "Select Std";
                        cmb_std.SelectedValue = "Select Std";
                        cmbStd.Enabled = true;
                        cmb_std.Enabled = true;
                    }
                    query = "select Div From Div where div Not IN ('ALL');";
                    adap = new SqlDataAdapter(query, con);
                    DataTable div = new DataTable();
                    adap.Fill(div);
                    div.Rows.Add("Select Div");
                    cmbDiv.DataSource = div;
                    cmbDiv.DataBind();
                    cmbDiv.DataTextField = "Div";
                    cmbDiv.DataValueField = "Div";
                    cmbDiv.DataBind();

                    cmb_div.DataSource = div;
                    cmb_div.DataBind();
                    cmb_div.DataTextField = "Div";
                    cmb_div.DataValueField = "Div";
                    cmb_div.DataBind();
                    //cmbDiv.SelectedValue = "Select Div";
                    if (!string.IsNullOrEmpty(div_sess))
                    {
                        cmbDiv.SelectedValue = div_sess;

                        cmb_div.SelectedValue = div_sess;
                        cmbDiv.Enabled = false;
                        cmb_div.Enabled = false;
                    }
                    else
                    {
                        // If std_sess is empty, set the default selected value
                        cmbDiv.SelectedValue = "Select Div";
                        cmb_div.SelectedValue = "Select Div";
                        cmbDiv.Enabled = true;
                        cmb_div.Enabled = true;
                    }

                    query = "select distinct examname from ExamMaster;";
                    adap = new SqlDataAdapter(query, con);
                    DataTable exam = new DataTable();
                    adap.Fill(exam);
                    exam.Rows.Add("Select Exam");
                    cmbexam.DataSource = exam;
                    cmbexam.DataBind();
                    cmbexam.DataTextField = "examname";
                    cmbexam.DataValueField = "examname";
                    cmbexam.SelectedValue = "Select Exam";
                    cmbexam.DataBind();

                }
            }
            catch (Exception ex)
            {
                Log.Error("ConsolidationReport1to8.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void printreport_ServerClick(object sender, EventArgs e)
        {
            string std, div;
            std = cmbStd.SelectedValue.ToString();
            div = cmbDiv.SelectedValue.ToString();
            SqlConnection con = null;
            PrimaryConsolidate1to8 report = new PrimaryConsolidate1to8();
            PrimaryConsolidate5to8 report1 = new PrimaryConsolidate5to8();
            Consolidation5_8 report2 = new Consolidation5_8();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    if (std == "I" || std == "II" || std == "III" || std == "IV" || std == "V" || std == "I (SE)" || std == "II (SE)" || std == "III (SE)" || std == "IV (SE)" || std == "V (SE)" || std == "I(Hindi)" || std == "II(Hindi)" || std == "III(Hindi)" || std == "IV(Hindi)" || std == "V(Hindi)")
                    {
                        report = printConsolidatereportItoVIII(con, std, div);
                    }
                    else
                    {
                        report1 = printConsolidatereportVtoVIII(con, std, div);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("ConsolidationReport1to8.printreport_ServerClick", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

                string folderpath = Server.MapPath("MarksheetFile");
                string filename = "ConsolidationReport_" + std + div + ".pdf";
                if (std == "I" || std == "II" || std == "III" || std == "IV" || std == "V" || std == "I (SE)" || std == "II (SE)" || std == "III (SE)" || std == "IV (SE)" || std == "V (SE)" || std == "I(Hindi)" || std == "II(Hindi)" || std == "III(Hindi)" || std == "IV(Hindi)" || std == "V(Hindi)")
                {
                    report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);
                }
                else
                {
                    report1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);
                }
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(Server.MapPath("~/MarksheetModule/MarksheetFile/" + filename));
                Response.End();

            }
        }

        protected void printclassteacherreport_ServerClick(object sender, EventArgs e)
        {
            string std, div;
            std = cmbStd.SelectedValue.ToString();
            div = cmbDiv.SelectedValue.ToString();
            SqlConnection con = null;
            STDV_ClassTeacher report = new STDV_ClassTeacher();
            PrimaryConsolidate5to8 report1 = new PrimaryConsolidate5to8();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    report = showClassTeacherReport(con, std, div);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("ConsolidationReport1to8.printclassteacherreport_ServerClick", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

                // string folderpath = Server.MapPath("MarksheetFile");
                string folderpath = Server.MapPath("~/MarksheetModule/ReportFile");
                string filename = "ClassTeacherReport" + std + div + ".pdf";
                report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                // Response.TransmitFile(Server.MapPath("~/MarksheetModule/MarksheetFile/" + filename));
                Response.TransmitFile(Server.MapPath("~/MarksheetModule/ReportFile/" + filename));
                Response.End();

            }
        }

        protected void btnsubjshet_ServerClick(object sender, EventArgs e)
        {
            string std = "", div = "", exam = "", subject = "";
            std = cmb_std.SelectedValue.ToString();
            div = cmb_div.SelectedValue.ToString();
            exam = cmbexam.SelectedValue.ToString();
            subject = cmbsubject.SelectedValue.ToString();
            SqlConnection con = null;
            STDV_SUBJjsheet report = new STDV_SUBJjsheet();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    report = ShowsubjectSheet(con, std, div, exam, subject);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Consolidation1to8.btnsubjshet_ServerClick.", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

                string folderpath = Server.MapPath("~/MarksheetModule/ReportFile");
                string filename = "SubjectSheet" + "_" + subject + "_" + std + div + ".pdf";
                report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(Server.MapPath("~/MarksheetModule/ReportFile/" + filename));
                Response.End();

            }
        }

        protected void Consolidationnew_ServerClick(object sender, EventArgs e)
        {
            string std, div;
            std = cmbStd.SelectedValue.ToString();
            div = cmbDiv.SelectedValue.ToString();
            SqlConnection con = null;
            Consolidation5_8 report2 = new Consolidation5_8();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    report2 = PrintConsolidation5_8(con, std, div);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("ConsolidationReport1to8.printreport_ServerClick", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

                string folderpath = Server.MapPath("MarksheetFile");
                string filename = "ConsolidationReportNew_" + std + div + ".pdf";

                report2.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(Server.MapPath("~/MarksheetModule/MarksheetFile/" + filename));
                Response.End();

            }
        }


        protected void cmbexam_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = null;
            string select_std = cmb_std.SelectedValue.ToString();
            string select_div = cmb_div.SelectedValue.ToString();
            string exam = cmbexam.SelectedValue.ToString();
            string query = "";
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "select Subject from SubjectMaster where std='" + select_std + "' and Examname='" + exam + "' order by CAST(Srno as int)";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    DataTable subject = new DataTable();
                    adap.Fill(subject);
                    subject.Rows.Add("Select Subject");
                    cmbsubject.DataSource = subject;
                    cmbsubject.DataBind();
                    cmbsubject.DataTextField = "Subject";
                    cmbsubject.DataValueField = "Subject";
                    cmbsubject.SelectedValue = "Select Subject";
                    cmbsubject.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error("ConsolidationReport1to8.cmbexam_SelectedIndexChanged", ex);
            }
        }

        public STDV_SUBJjsheet ShowsubjectSheet(SqlConnection con, string std, string div, string exam, string subject)
        {
            Subjectsheet subjsheet = new Subjectsheet();
            String query = "";
            SqlCommand cmd = null;
            DataTable studtable = new DataTable();
            SqlDataAdapter adap = null;
            List<string> subjectlist = new List<string>();
            SqlDataReader reader = null;
            string year = lblacademicyear.Text;
            try
            {
                query = "select Cast(rollno as int) as rollno,grno,(Lname+' '+fname+' '+MNAME) as studentname,mothername,std,div From studentmaster where std='" + std + "' and div='" + div + "' and academicyear='" + year + "' order by rollno asc;";
                cmd = new SqlCommand(query, con);
                adap = new SqlDataAdapter(cmd);
                adap.Fill(studtable);

                DataTable _subjectmrkstable = new DataTable();
                DataTable _subjectmrkstable1 = new DataTable();
                foreach (DataRow row in studtable.Rows)
                {

                    subshet trpt = new subshet();
                    trpt.Rollno = row[0].ToString();
                    trpt.Grno = row[1].ToString();
                    trpt.studentname = row[2].ToString();
                    trpt.mothername = row[3].ToString();

                    query = "select distinct DailyObser,Orals,practicalexp,activity,project,UnitTest,Selfstudy,Others,Total,summativeorals,summativewritten,summativetotal,grandtotal,finalgrade from StudentMarksheet where STD='" + std + "' AND DIV='" + div + "' AND Subjectname='" + subject + "' And Examname='" + exam + "' and grno='" + trpt.Grno + "' and Academicyear='" + year + "'";
                    cmd = new SqlCommand(query, con);
                    adap = new SqlDataAdapter(cmd);
                    adap.Fill(_subjectmrkstable);
                    foreach (DataRow mrkrow in _subjectmrkstable.Rows)
                    {
                        if (!string.IsNullOrEmpty(mrkrow[0].ToString()) && mrkrow[0].ToString().All(char.IsDigit))
                        {
                            trpt.dobs = Convert.ToInt32(mrkrow[0]).ToString(); ;
                        }
                        else
                        {
                            trpt.dobs = mrkrow[0].ToString();
                        }

                        if (!string.IsNullOrEmpty(mrkrow[1].ToString()) && mrkrow[1].ToString().All(char.IsDigit))
                        {
                            trpt.orlwrk = Convert.ToInt32(mrkrow[1]).ToString();
                        }
                        else
                        {
                            trpt.orlwrk = mrkrow[1].ToString();
                        }

                        if (!string.IsNullOrEmpty(mrkrow[2].ToString()) && mrkrow[2].ToString().All(char.IsDigit))
                        {
                            trpt.prect = Convert.ToInt32(mrkrow[2]).ToString();
                        }
                        else
                        {
                            trpt.prect = mrkrow[2].ToString();
                        }

                        if (!string.IsNullOrEmpty(mrkrow[3].ToString()) && mrkrow[3].ToString().All(char.IsDigit))
                        {
                            trpt.activity = Convert.ToInt32(mrkrow[3]).ToString();
                        }
                        else
                        {
                            trpt.activity = mrkrow[3].ToString();
                        }
                        if (!string.IsNullOrEmpty(mrkrow[4].ToString()) && mrkrow[4].ToString().All(char.IsDigit))
                        {
                            trpt.project = Convert.ToInt32(mrkrow[4]).ToString();
                        }
                        else
                        {
                            trpt.project = mrkrow[4].ToString();
                        }
                        if (!string.IsNullOrEmpty(mrkrow[5].ToString()) && mrkrow[5].ToString().All(char.IsDigit))
                        {
                            trpt.testob = Convert.ToInt32(mrkrow[5]).ToString();
                        }
                        else
                        {
                            trpt.testob = mrkrow[5].ToString();
                        }

                        if (!string.IsNullOrEmpty(mrkrow[6].ToString()) && mrkrow[6].ToString().All(char.IsDigit))
                        {
                            trpt.homecw = Convert.ToInt32(mrkrow[6]).ToString();
                        }
                        else
                        {
                            trpt.homecw = mrkrow[6].ToString();
                        }

                        if (!string.IsNullOrEmpty(mrkrow[7].ToString()) && mrkrow[7].ToString().All(char.IsDigit))
                        {
                            trpt.other = Convert.ToInt32(mrkrow[7]).ToString();
                        }
                        else
                        {
                            trpt.other = mrkrow[7].ToString();
                        }

                        if (!string.IsNullOrEmpty(mrkrow[8].ToString()) && mrkrow[8].ToString().All(char.IsDigit))
                        {
                            trpt.TotalA = Convert.ToInt32(mrkrow[8]).ToString();
                        }
                        else
                        {
                            trpt.TotalA = mrkrow[8].ToString();
                        }

                        if (!string.IsNullOrEmpty(mrkrow[9].ToString()) && mrkrow[9].ToString().All(char.IsDigit))
                        {
                            trpt.oral = Convert.ToInt32(mrkrow[9]).ToString();
                        }
                        else
                        {
                            trpt.oral = mrkrow[9].ToString();
                        }

                        if (!string.IsNullOrEmpty(mrkrow[10].ToString()) && mrkrow[10].ToString().All(char.IsDigit))
                        {
                            trpt.theory = Convert.ToInt32(mrkrow[10]).ToString();
                        }
                        else
                        {
                            trpt.theory = mrkrow[10].ToString();
                        }

                        if (!string.IsNullOrEmpty(mrkrow[11].ToString()) && mrkrow[11].ToString().All(char.IsDigit))
                        {
                            trpt.TotalB = Convert.ToInt32(mrkrow[11]).ToString();
                        }
                        else
                        {
                            trpt.TotalB = mrkrow[11].ToString();
                        }

                        if (!string.IsNullOrEmpty(mrkrow[12].ToString()) && mrkrow[12].ToString().All(char.IsDigit))
                        {
                            trpt.grandtotal = Convert.ToInt32(mrkrow[12]).ToString();
                        }
                        else
                        {
                            trpt.grandtotal = mrkrow[12].ToString();
                        }

                        if (!string.IsNullOrEmpty(mrkrow[13].ToString()))
                        {
                            trpt.grade = mrkrow[13].ToString();
                        }

                    }
                    _subjectmrkstable.Dispose();

                    subjsheet.Tables[0].Rows.Add(trpt.Rollno, trpt.Grno, trpt.studentname, trpt.dobs, trpt.orlwrk, trpt.prect, trpt.activity, trpt.project, trpt.testob, trpt.homecw, trpt.other, trpt.TotalA, trpt.theory, trpt.oral, trpt.TotalB, trpt.grandtotal, trpt.grade);

                }
                studtable.Dispose();
                string teachername = "";
                query = "select teachername From TeacherMapping where std='" + std + "' and div='" + div + "';";
                cmd = new SqlCommand(query, con);
                SqlDataReader rea_der = cmd.ExecuteReader();
                while (rea_der.Read())
                {
                    teachername = rea_der[0].ToString();
                }
                con.Close();

                STDV_SUBJjsheet report = new STDV_SUBJjsheet();
                report.SetDataSource(subjsheet.Tables[0]);
                report.SetParameterValue("std", std);
                report.SetParameterValue("div", div);
                report.SetParameterValue("subjname", subject);
                report.SetParameterValue("tachername", teachername);
                report.SetParameterValue("examtype", exam);
                report.SetParameterValue("year", year);

                return report;
            }
            catch (Exception ex)
            {
                Log.Error("ConsolidationReport1to8.ShowsubjectSheet", ex);
                return null;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        public PrimaryConsolidate1to8 printConsolidatereportItoVIII(SqlConnection con, string std, string div)
        {

            DataTable StudentMarksheet1to8 = new DataTable();
            DataTable _subjectmrkstabl = new DataTable();
            DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
            try
            {
                PrimaryConsolidate _Std1to8Teachersds = new PrimaryConsolidate();
                string select_std = cmbStd.SelectedValue.ToString();
                string select_div = cmbDiv.SelectedValue.ToString();

                string year = new FeesModel().setActiveAcademicYear();
                String query = "", percentage = "";
                SqlCommand cmd = null;
                DataTable studtable = new DataTable();
                SqlDataAdapter adap = null;
                List<string> subjectlist = new List<string>();
                SqlDataReader reader = null;
                string hpe = "0", sg = "0", wtr = "0";
                int presentstu = 0; int presentstu1 = 0;
                string percent = "0", percent1 = "0";
                query = "select Cast(rollno as int) as rollno,grno,(FNAME+' '+MNAME+' '+LNAME) as studentname,std,div From studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '') order by rollno asc;";
                cmd = new SqlCommand(query, con);
                adap = new SqlDataAdapter(cmd);
                adap.Fill(studtable);



                foreach (DataRow row in studtable.Rows)
                {
                    std1to8consold trpt = new std1to8consold();
                    trpt.rollno = row[0].ToString();
                    trpt.grno = row[1].ToString();
                    trpt.studentname = row[2].ToString();
                    trpt.std = row[3].ToString();
                    trpt.div = row[4].ToString();



                    query = "select Cast (srno as int) as srno,Subject From subjectmaster where std='" + trpt.std + "' and examname='First Semester' order by srno asc";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        subjectlist.Add(reader[1].ToString());
                    }
                    reader.Close();

                    query = "select [grandtotal],[finalgrade],Subjectname From studentmarksheet where std='" + trpt.std + "' and examname='First Semester' and grno='" + trpt.grno + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    adap = new SqlDataAdapter(cmd);
                    adap.Fill(StudentMarksheet1to8);



                    foreach (DataRow mrkrow in StudentMarksheet1to8.Rows)
                    {
                        if (mrkrow[2].Equals(SubjectClass.eng) || mrkrow[2].ToString().Trim().Equals(SubjectClass.eng1))
                        {
                            trpt.engmark = mrkrow[0].ToString();
                            trpt.enggrade = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].Equals(SubjectClass.mar))
                        {
                            trpt.marmark = mrkrow[0].ToString();
                            trpt.margrade = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].Equals(SubjectClass.hindi))
                        {
                            trpt.hinmark = mrkrow[0].ToString();
                            trpt.hingrade = mrkrow[1].ToString();
                        }
                        else if (mrkrow[2].Equals(SubjectClass.maths))
                        {
                            trpt.mathmark = mrkrow[0].ToString();
                            trpt.mathgrade = mrkrow[1].ToString();
                        }
                        else if (mrkrow[2].Equals(SubjectClass.evs) || mrkrow[2].Equals(SubjectClass.envs))
                        {
                            trpt.evsmark = mrkrow[0].ToString();
                            trpt.evsgrade = mrkrow[1].ToString();
                        }
                        else if (mrkrow[2].Equals(SubjectClass.art))
                        {
                            trpt.artmark = mrkrow[0].ToString();
                            trpt.artgrade = mrkrow[1].ToString();
                        }
                        else if (mrkrow[2].Equals(SubjectClass.wrk))
                        {
                            trpt.wrkmark = mrkrow[0].ToString();
                            trpt.wrkgrade = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].Equals(SubjectClass.pe) || mrkrow[2].Equals(SubjectClass.pt))
                        {
                            trpt.ptmark = mrkrow[0].ToString();
                            trpt.ptgrade = mrkrow[1].ToString();
                        }
                        else if (mrkrow[2].Equals(SubjectClass.computer))
                        {
                            trpt.compgrade = mrkrow[1].ToString();
                        }


                    }

                    StudentMarksheet1to8.Dispose();

                    //calculate total
                    trpt.total = "0"; double percentageValue = 0, percentageValue1 = 0; int roundedPercentage = 0, roundedPercentage1 = 0;

                    if (trpt.engmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.engmark)).ToString();
                    }
                    if (trpt.marmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.marmark)).ToString();
                    }
                    if (trpt.hinmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.hinmark)).ToString();
                    }
                    if (trpt.mathmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.mathmark)).ToString();
                    }
                    if (trpt.evsmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.evsmark)).ToString();
                    }
                    if (trpt.artmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.artmark)).ToString();
                    }
                    if (trpt.wrkmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.wrkmark)).ToString();
                    }
                    if (trpt.ptmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.ptmark)).ToString();
                    }
                    percentage = "0";
                    if (std == "I" || std == "II" || std == "I (SE)" || std == "II (SE)")
                    {
                        trpt.percentage = ((Convert.ToDouble(trpt.total) * 100) / 600).ToString("0.00");

                        percentageValue = Math.Round((Convert.ToDouble(trpt.total) * 100) / 600, MidpointRounding.AwayFromZero);
                    }
                    else if (std == "III" || std == "IV" || std == "III (SE)" || std == "IV (SE)")
                    {
                        trpt.percentage = ((Convert.ToDouble(trpt.total) * 100) / 700).ToString("0.00");

                        percentageValue = Math.Round((Convert.ToDouble(trpt.total) * 100) / 700, MidpointRounding.AwayFromZero);
                    }
                    else if (std == "V" || std == "V (SE)")
                    {
                        trpt.percentage = ((Convert.ToDouble(trpt.total) * 100) / 800).ToString("0.00");

                        percentageValue = Math.Round((Convert.ToDouble(trpt.total) * 100) / 800, MidpointRounding.AwayFromZero);
                    }



                    //string percent = (Convert.ToInt32(trpt.total) * 100 / 900).ToString();

                    //roundedPercentage = (int)Math.Ceiling(percentageValue); // Round up to nearest whole number
                    //percent = roundedPercentage.ToString();

                    trpt.remark = "Passed";
                    trpt.remark1 = "Passed";
                    query = "select Grade from GradeChart where " + percentageValue + " between minmarks and maxmarks";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        trpt.grade = reader["Grade"].ToString();
                    }
                    reader.Close();
                    query = "select SUM(CAST(june AS decimal) + CAST(july AS decimal) + CAST(aug AS decimal) + CAST(sep AS decimal) + CAST(oct AS decimal)) as total1 from Attendance where std='" + trpt.std + "' and div='" + trpt.div + "' and grno='" + trpt.grno + "' and Academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        presentstu = Convert.ToInt32(reader["total1"]);
                    }
                    reader.Close();




                    query = "select Cast (srno as int) as srno,Subject From subjectmaster where std='" + trpt.std + "' and examname='Second Semester' order by srno asc";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        subjectlist.Add(reader[1].ToString());

                    }
                    reader.Close();

                    query = "select [grandtotal],[finalgrade],Subjectname From studentmarksheet where std='" + trpt.std + "' and examname='Second Semester' and grno='" + trpt.grno + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    adap = new SqlDataAdapter(cmd);
                    adap.Fill(_subjectmrkstabl);



                    foreach (DataRow mrkrow in _subjectmrkstabl.Rows)
                    {
                        if (mrkrow[2].Equals(SubjectClass.eng) || mrkrow[2].ToString().Trim().Equals(SubjectClass.eng1))
                        {
                            trpt.engmark1 = mrkrow[0].ToString();
                            trpt.enggrade1 = mrkrow[1].ToString();
                        }
                        else if (mrkrow[2].Equals(SubjectClass.mar))
                        {
                            trpt.marmark1 = mrkrow[0].ToString();
                            trpt.margrade1 = mrkrow[1].ToString();
                        }
                        else if (mrkrow[2].Equals(SubjectClass.hindi))
                        {
                            trpt.hinmark1 = mrkrow[0].ToString();
                            trpt.hingrade1 = mrkrow[1].ToString();
                        }
                        else if (mrkrow[2].Equals(SubjectClass.maths))
                        {
                            trpt.mathmark1 = mrkrow[0].ToString();
                            trpt.mathgrade1 = mrkrow[1].ToString();
                        }
                        else if (mrkrow[2].Equals(SubjectClass.evs) || mrkrow[2].Equals(SubjectClass.envs))
                        {
                            trpt.evsmark1 = mrkrow[0].ToString();
                            trpt.evsgrade1 = mrkrow[1].ToString();
                        }
                        else if (mrkrow[2].Equals(SubjectClass.art))
                        {
                            trpt.artmark1 = mrkrow[0].ToString();
                            trpt.artgrade1 = mrkrow[1].ToString();
                        }
                        else if (mrkrow[2].Equals(SubjectClass.wrk))
                        {
                            trpt.wrkmark1 = mrkrow[0].ToString();
                            trpt.wrkgrade1 = mrkrow[1].ToString();
                        }
                        else if (mrkrow[2].Equals(SubjectClass.pe) || mrkrow[2].Equals(SubjectClass.pt))
                        {
                            trpt.ptmark1 = mrkrow[0].ToString();
                            trpt.ptgrade1 = mrkrow[1].ToString();
                        }
                        else if (mrkrow[2].Equals(SubjectClass.computer))
                        {
                            trpt.compgrade1 = mrkrow[1].ToString();
                        }
                    }

                    _subjectmrkstabl.Dispose();

                    //calculate total
                    trpt.total1 = "0";

                    if (trpt.engmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.engmark1)).ToString();
                    }
                    if (trpt.marmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.marmark1)).ToString();
                    }
                    if (trpt.hinmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.hinmark1)).ToString();
                    }
                    if (trpt.mathmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.mathmark1)).ToString();
                    }
                    if (trpt.evsmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.evsmark1)).ToString();
                    }
                    if (trpt.artmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.artmark1)).ToString();
                    }
                    if (trpt.wrkmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.wrkmark1)).ToString();
                    }
                    if (trpt.ptmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.ptmark1)).ToString();
                    }

                    if (std == "I" || std == "II" || std == "I (SE)" || std == "II (SE)")
                    {
                        trpt.percentage1 = ((Convert.ToDouble(trpt.total1) * 100) / 600).ToString("0.00");

                        percentageValue1 = Math.Round((Convert.ToDouble(trpt.total1) * 100) / 600, MidpointRounding.AwayFromZero);
                    }
                    else if (std == "III" || std == "IV" || std == "III (SE)" || std == "IV (SE)")
                    {
                        trpt.percentage1 = ((Convert.ToDouble(trpt.total1) * 100) / 700).ToString("0.00");

                        percentageValue1 = Math.Round((Convert.ToDouble(trpt.total1) * 100) / 700, MidpointRounding.AwayFromZero);
                    }
                    else if (std == "V" || std == "V (SE)")
                    {
                        trpt.percentage1 = ((Convert.ToDouble(trpt.total1) * 100) / 800).ToString("0.00");

                        percentageValue1 = Math.Round((Convert.ToDouble(trpt.total1) * 100) / 800, MidpointRounding.AwayFromZero);
                    }

                    //roundedPercentage1 = (int)Math.Ceiling(percentageValue1); // Round up to nearest whole number
                    //percent1 = roundedPercentage1.ToString();

                    query = "select Grade from GradeChart where " + percentageValue1 + " between minmarks and maxmarks";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        trpt.grade1 = reader["Grade"].ToString();
                    }
                    reader.Close();

                    query = "select SUM(CAST(nov AS decimal) + CAST(dec AS decimal) + CAST(jan AS decimal) + CAST(feb AS decimal) + CAST(march AS decimal)+ CAST(april AS decimal)+ CAST(may AS decimal)) AS total2 from Attendance where std='" + trpt.std + "' and div='" + trpt.div + "' and grno='" + trpt.grno + "' and Academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        presentstu1 = Convert.ToInt32(reader["total2"]);
                    }
                    reader.Close();


                    if (trpt.std == "I" || trpt.std == "II" || trpt.std == "III" || trpt.std == "IV" || trpt.std == "V" || trpt.std == "VI" || trpt.std == "VII" || trpt.std == "VIII")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, trpt.engmark, trpt.enggrade, trpt.marmark, trpt.margrade, trpt.hinmark, trpt.hingrade, trpt.mathmark, trpt.mathgrade, trpt.evsmark, trpt.evsgrade, trpt.ssmark, trpt.ssgrade, trpt.artmark, trpt.artgrade, trpt.wrkmark, trpt.wrkgrade, trpt.ptmark, trpt.ptgrade, trpt.compgrade, trpt.total, trpt.percentage, trpt.grade, presentstu, trpt.remark, trpt.engmark1, trpt.enggrade1, trpt.marmark1, trpt.margrade1, trpt.hinmark1, trpt.hingrade1, trpt.mathmark1, trpt.mathgrade1, trpt.evsmark1, trpt.evsgrade1, trpt.ssmark1, trpt.ssgrade1, trpt.artmark1, trpt.artgrade1, trpt.wrkmark1, trpt.wrkgrade1, trpt.ptmark1, trpt.ptgrade1, trpt.compgrade1, trpt.total1, trpt.percentage1, trpt.grade1, presentstu1, trpt.remark1);
                    }
                    if (trpt.std == "I (SE)" || trpt.std == "II (SE)" || trpt.std == "III (SE)" || trpt.std == "IV (SE)" || trpt.std == "V (SE)" || trpt.std == "VI (SE)" || trpt.std == "VII(SE)" || trpt.std == "VIII(SE)" || trpt.std == "I(Hindi)" || trpt.std == "II(Hindi)" || trpt.std == "III(Hindi)" || trpt.std == "IV(Hindi)" || trpt.std == "V(Hindi)" || trpt.std == "VI(Hindi)" || trpt.std == "VII (Hindi)" || trpt.std == "VIII (Hindi)")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, trpt.hinmark, trpt.hingrade, trpt.marmark, trpt.margrade, trpt.engmark, trpt.enggrade, trpt.mathmark, trpt.mathgrade, trpt.evsmark, trpt.evsgrade, trpt.ssmark, trpt.ssgrade, trpt.artmark, trpt.artgrade, trpt.wrkmark, trpt.wrkgrade, trpt.ptmark, trpt.ptgrade, trpt.compgrade, trpt.total, trpt.percentage, trpt.grade, presentstu, trpt.remark, trpt.hinmark1, trpt.hingrade1, trpt.marmark1, trpt.margrade1, trpt.engmark1, trpt.enggrade1, trpt.mathmark1, trpt.mathgrade1, trpt.evsmark1, trpt.evsgrade1, trpt.ssmark1, trpt.ssgrade1, trpt.artmark1, trpt.artgrade1, trpt.wrkmark1, trpt.wrkgrade1, trpt.ptmark1, trpt.ptgrade1, trpt.compgrade1, trpt.total1, trpt.percentage1, trpt.grade1, presentstu1, trpt.remark1);
                    }
                    if (trpt.std == "VIII (Mar)")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, trpt.marmark, trpt.margrade, trpt.hinmark, trpt.hingrade, trpt.engmark, trpt.enggrade, trpt.mathmark, trpt.mathgrade, trpt.evsmark, trpt.evsgrade, trpt.ssmark, trpt.ssgrade, trpt.artmark, trpt.artgrade, trpt.wrkmark, trpt.wrkgrade, trpt.ptmark, trpt.ptgrade, trpt.compgrade, trpt.total, trpt.percentage, trpt.grade, presentstu, trpt.remark, trpt.marmark1, trpt.margrade1, trpt.hinmark1, trpt.hingrade1, trpt.engmark1, trpt.enggrade1, trpt.mathmark1, trpt.mathgrade1, trpt.evsmark1, trpt.evsgrade1, trpt.ssmark1, trpt.ssgrade1, trpt.artmark1, trpt.artgrade1, trpt.wrkmark1, trpt.wrkgrade1, trpt.ptmark1, trpt.ptgrade1, trpt.compgrade1, trpt.total1, trpt.percentage1, trpt.grade1, presentstu1, trpt.remark1);
                    }

                }

                studtable.Dispose();


                PrimaryConsolidate1to8 report = new PrimaryConsolidate1to8();
                report.SetDataSource(_Std1to8Teachersds.Tables[0]);

                return report;

            }
            catch (Exception ex)
            {
                Log.Error("ConsolidationReport1to8.printreport_ServerClick", ex);
                return null;
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }
        public STDV_ClassTeacher showClassTeacherReport(SqlConnection con, string std, string div)
        {

            DataTable stutbl = new DataTable();
            DataTable StudentMarksheet8 = new DataTable();
            DataTable SubjectMaster = new DataTable();
            DataTable remarktbl = new DataTable();
            ClassTeacherReport _evalds = new ClassTeacherReport();
            try
            {
                string query = "";
                string teachername = "";

                query = "Select (Lname+' '+fname+' '+MNAME) as name ,std,div,rollno,grno From studentmaster where std='" + std + "' and div='" + div + "' and academicyear='" + lblacademicyear.Text + "' order by Cast(rollno as int) asc";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(stutbl);

                query = "Select Cast([srno] as int) as srno,[subject],[minmarks],[maxmarks],[check],[grade] " +
                      "From SubjectMaster where std='" + std + "' and examname='Second Semester'  order by Cast(srno as int) asc;";
                cmd = new SqlCommand(query, con);
                adap = new SqlDataAdapter(cmd);
                adap.Fill(SubjectMaster);

                query = "select grno,studentname,std,div,application,conduct" +
                           " from Remark where std='" + std + "' and div='" + div + "' and Examname='II Semester' and Academicyear='" + lblacademicyear.Text + "';";

                cmd = new SqlCommand(query, con);
                adap = new SqlDataAdapter(cmd);
                adap.Fill(remarktbl);

                query = "select teachername From TeacherMapping where std='" + std + "' and div='" + div + "';";//  select teachername From teachersdetails
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    teachername = reader[0].ToString();
                }
                reader.Close();
                EvaluationModel em = new EvaluationModel();
                string gtotalsem1 = "0", gtotalsem2 = "0";
                foreach (DataRow ro in stutbl.Rows)
                {
                    string application = "", conduct = "", progress = "";
                    StudentMarksheet8 = new DataTable();
                    gtotalsem1 = "0"; gtotalsem2 = "0";

                    query = "select rollno,grno,subjectname,examname,DailyObser,Orals,practicalexp,activity,project,UnitTest,selfstudy,Others,Total,summativeorals,  summativewritten, summativetotal, grandtotal, finalgrade " +
                            "from StudentMarksheet " +
                            "where std='" + std + "' and div='" + div + "' and grno='" + ro["grno"].ToString() + "' and Academicyear='" + lblacademicyear.Text + "';";

                    cmd = new SqlCommand(query, con);
                    adap = new SqlDataAdapter(cmd);
                    adap.Fill(StudentMarksheet8);

                    var drwo = remarktbl.AsEnumerable().Where(x => x.Field<string>("grno").Equals(ro["grno"].ToString()));
                    foreach (DataRow d in drwo)
                    {
                        application = d["application"].ToString();
                        conduct = d["conduct"].ToString();
                        progress = d["progress"].ToString();
                    }

                    int i = 1;
                    foreach (DataRow subro in SubjectMaster.Rows)
                    {
                        em = new EvaluationModel();
                        em.rollno = ro["rollno"].ToString();
                        em.grno = ro["grno"].ToString();
                        em.studentname = ro["name"].ToString();


                        em.subject = subro["subject"].ToString();

                        //get first semester marks
                        var drow = StudentMarksheet8.AsEnumerable().Where(x => x.Field<string>("subjectname").Equals(em.subject) && x.Field<string>("examname").Equals("First Semester")).DefaultIfEmpty(null).First();
                        //var drow = StudentMarksheet8.AsEnumerable().Where(x => x.Field<string>("subjectname").Equals(em.subject) && x.Field<string>("examname").Equals("I Semester")).DefaultIfEmpty(null).First();

                        if (drow != null)
                        {
                            em.obs1 = drow["DailyObser"].ToString();
                            em.ow1 = drow["Orals"].ToString();
                            em.prac1 = drow["practicalexp"].ToString();
                            em.activity1 = drow["activity"].ToString();
                            em.project1 = drow["project"].ToString();
                            em.test1 = drow["UnitTest"].ToString();
                            em.hw1 = drow["selfstudy"].ToString();
                            em.other1 = drow["Others"].ToString();
                            em.totalA1 = drow["Total"].ToString();
                            em.theory1 = drow["summativewritten"].ToString();
                            em.oralprac1 = drow["summativeorals"].ToString();
                            em.totalB1 = drow["summativetotal"].ToString();
                            em.AB1 = drow["grandtotal"].ToString();
                            em.grade1 = drow["finalgrade"].ToString();
                        }

                        //get second semester marks
                        // drow = StudentMarksheet8.AsEnumerable().Where(x => x.Field<string>("subjectname").Equals(em.subject) && x.Field<string>("examname").Equals("II Semester")).DefaultIfEmpty(null).First();
                        drow = StudentMarksheet8.AsEnumerable().Where(x => x.Field<string>("subjectname").Equals(em.subject) && x.Field<string>("examname").Equals("Second Semester")).DefaultIfEmpty(null).First();

                        if (drow != null)
                        {
                            em.obs2 = drow["DailyObser"].ToString();
                            em.ow2 = drow["Orals"].ToString();
                            em.prac2 = drow["practicalexp"].ToString();
                            em.activity2 = drow["activity"].ToString();
                            em.project2 = drow["project"].ToString();
                            em.test2 = drow["UnitTest"].ToString();
                            em.hw2 = drow["selfstudy"].ToString();
                            em.other2 = drow["Others"].ToString();
                            em.totalA2 = drow["Total"].ToString();
                            em.theory2 = drow["summativewritten"].ToString();
                            em.oralprac2 = drow["summativeorals"].ToString();
                            em.totalB2 = drow["summativetotal"].ToString();
                            em.AB2 = drow["grandtotal"].ToString();
                            em.grade2 = drow["finalgrade"].ToString();
                        }

                        switch (i)
                        {
                            case 1:
                                em.remark = application; break;
                            case 4:
                                em.remark = conduct; break;
                            case 6:
                                em.remark = progress; break;
                        }

                        if (!string.IsNullOrEmpty(em.AB1) && em.AB1.All(char.IsDigit))
                        {
                            if (em.subject != "COMPUTER")
                                gtotalsem1 = (Convert.ToInt32(gtotalsem1) + Convert.ToInt32(em.AB1)).ToString();
                        }
                        if (!string.IsNullOrEmpty(em.AB2) && em.AB2.All(char.IsDigit) && em.subject != "COMPUTER")
                        {
                            if (em.subject != "COMPUTER")
                                gtotalsem2 = (Convert.ToInt32(gtotalsem2) + Convert.ToInt32(em.AB2)).ToString();
                        }
                        i = i + 1;
                        if (!string.IsNullOrEmpty(em.subject))
                            _evalds.Tables[0].Rows.Add(em.rollno, em.grno, em.studentname, em.subject, em.obs1, em.ow1, em.prac1, em.activity1, em.project1, em.test1, em.hw1, em.other1, em.totalA1, em.theory1, em.oralprac1, em.totalB1, em.AB1, em.grade1, em.obs2, em.ow2, em.prac2, em.activity2, em.project2, em.test2, em.hw2, em.other2, em.totalA2, em.theory2, em.oralprac2, em.totalB2, em.AB2, em.grade2, em.remark);
                    }
                    StudentMarksheet8.Dispose();
                    string gradesem1 = "", percent = "", percentsem2 = "", gradesem2 = "";
                    if (!string.IsNullOrEmpty(gtotalsem1) && gtotalsem1.All(char.IsDigit))
                    {
                        if (std == "I" || std == "II" || std == "I (SE)" || std == "II (SE)")
                        {
                            percent = ((Convert.ToDouble(gtotalsem1) * 100) / 600).ToString("##.##");
                        }
                        else if (std == "III" || std == "IV" || std == "III (SE)" || std == "IV (SE)")
                        {
                            percent = ((Convert.ToDouble(gtotalsem1) * 100) / 700).ToString("##.##");
                        }
                        else if (std == "V" || std == "V (SE)")
                        {
                            percent = ((Convert.ToDouble(gtotalsem1) * 100) / 800).ToString("##.##");
                        }
                        else
                        {
                            percent = ((Convert.ToDouble(gtotalsem1) * 100) / 900).ToString("##.##");
                        }

                    }
                    if (!string.IsNullOrEmpty(gtotalsem2) && gtotalsem2.All(char.IsDigit))
                    {
                        if (std == "I" || std == "II" || std == "I (SE)" || std == "II (SE)")
                        {
                            percentsem2 = ((Convert.ToDouble(gtotalsem2) * 100) / 600).ToString("##.##");
                        }
                        else if (std == "III" || std == "IV" || std == "III (SE)" || std == "IV (SE)")
                        {
                            percentsem2 = ((Convert.ToDouble(gtotalsem2) * 100) / 700).ToString("##.##");
                        }
                        else if (std == "V" || std == "V (SE)")
                        {
                            percentsem2 = ((Convert.ToDouble(gtotalsem2) * 100) / 800).ToString("##.##");
                        }
                        else
                        {
                            percentsem2 = ((Convert.ToDouble(gtotalsem2) * 100) / 900).ToString("##.##");
                        }
                    }
                    query = "select Grade from GradeChart where " + percent + " between minmarks and maxmarks";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        gradesem1 = reader["Grade"].ToString();
                    }
                    reader.Close();
                    query = "select Grade from GradeChart where " + percentsem2 + " between minmarks and maxmarks";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        gradesem2 = reader["Grade"].ToString();
                    }
                    reader.Close();

                    _evalds.Tables[0].Rows.Add(em.rollno, em.grno, em.studentname, "", "", "", "", "", "", "", "", "", "", "Total", gtotalsem1, "Pr.", percent, gradesem1, "", "", "", "", "", "", "", "", "", "Total", gtotalsem2, "Pr.", percentsem2, gradesem2, "");
                }

                STDV_ClassTeacher _rep = new STDV_ClassTeacher();
                _rep.SetDataSource(_evalds.Tables[0]);
                _rep.SetParameterValue("std", std);
                _rep.SetParameterValue("div", div);
                _rep.SetParameterValue("tachername", teachername);
                _rep.SetParameterValue("year", lblacademicyear.Text);

                return _rep;
            }
            catch (Exception ex)
            {
                Log.Error("ConsolidationReport1to8.printclassteacherreport_ServerClick", ex);
                return null;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        public PrimaryConsolidate5to8 printConsolidatereportVtoVIII(SqlConnection con, string std, string div)
        {

            DataTable StudentMarksheet1to8 = new DataTable();
            DataTable _subjectmrkstabl = new DataTable();
            DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
            try
            {
                PrimaryConsolidate _Std1to8Teachersds = new PrimaryConsolidate();
                string select_std = cmbStd.SelectedValue.ToString();
                string select_div = cmbDiv.SelectedValue.ToString();

                string year = new FeesModel().setActiveAcademicYear();
                String query = "", percentage = "";
                SqlCommand cmd = null;
                DataTable studtable = new DataTable();
                SqlDataAdapter adap = null;
                List<string> subjectlist = new List<string>();
                SqlDataReader reader = null;
                string hpe = "0", sg = "0", wtr = "0";
                int presentstu = 0; int presentstu1 = 0;

                query = "select Cast(rollno as int) as rollno,grno,(FNAME+' '+MNAME+' '+LNAME) as studentname,std,div From studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '') order by rollno asc;";
                cmd = new SqlCommand(query, con);
                adap = new SqlDataAdapter(cmd);
                adap.Fill(studtable);



                foreach (DataRow row in studtable.Rows)
                {
                    std1to8consold trpt = new std1to8consold();
                    trpt.rollno = row[0].ToString();
                    trpt.grno = row[1].ToString();
                    trpt.studentname = row[2].ToString();
                    trpt.std = row[3].ToString();
                    trpt.div = row[4].ToString();

                    query = "select Cast (srno as int) as srno,UPPER(Subject) as Subject From subjectmaster where std='" + trpt.std + "' and examname='First Semester' order by srno asc";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        subjectlist.Add(reader[1].ToString());

                    }
                    reader.Close();

                    query = "select [grandtotal],[finalgrade],UPPER(LTRIM(RTRIM(subjectname))) as Subjectname From studentmarksheet where std='" + trpt.std + "' and examname='First Semester' and grno='" + trpt.grno + "';";
                    //query = "select [grandtotal],[finalgrade],Subjectname From studentmarksheet where std='" + trpt.std + "' and examname='First Semester' and grno='26465';";
                    cmd = new SqlCommand(query, con);
                    adap = new SqlDataAdapter(cmd);
                    adap.Fill(StudentMarksheet1to8);



                    foreach (DataRow mrkrow in StudentMarksheet1to8.Rows)
                    {
                        if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.eng))
                        {
                            trpt.engmark = mrkrow[0].ToString();
                            trpt.enggrade = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.mar))
                        {
                            trpt.marmark = mrkrow[0].ToString();
                            trpt.margrade = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.hindi))
                        {
                            trpt.hinmark = mrkrow[0].ToString();
                            trpt.hingrade = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.maths))
                        {
                            trpt.mathmark = mrkrow[0].ToString();
                            trpt.mathgrade = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.sci))
                        {
                            trpt.evsmark = mrkrow[0].ToString();
                            trpt.evsgrade = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.socsci))
                        {
                            trpt.ssmark = mrkrow[0].ToString();
                            trpt.ssgrade = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.art))
                        {
                            trpt.artmark = mrkrow[0].ToString();
                            trpt.artgrade = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.wrk))
                        {
                            trpt.wrkmark = mrkrow[0].ToString();
                            trpt.wrkgrade = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.pe) || mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.pt))
                        {
                            trpt.ptmark = mrkrow[0].ToString();
                            trpt.ptgrade = mrkrow[1].ToString();


                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.computer))
                        {
                            trpt.compgrade = mrkrow[1].ToString();


                        }


                    }

                    StudentMarksheet1to8.Dispose();

                    //calculate total
                    trpt.total = "0";

                    if (trpt.engmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.engmark)).ToString();
                    }
                    if (trpt.marmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.marmark)).ToString();
                    }
                    if (trpt.hinmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.hinmark)).ToString();
                    }
                    if (trpt.mathmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.mathmark)).ToString();
                    }
                    if (trpt.evsmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.evsmark)).ToString();
                    }
                    if (trpt.ssmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.ssmark)).ToString();
                    }
                    if (trpt.artmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.artmark)).ToString();
                    }
                    if (trpt.wrkmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.wrkmark)).ToString();
                    }
                    if (trpt.ptmark.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.ptmark)).ToString();
                    }

                    trpt.percentage = ((Convert.ToDouble(trpt.total) * 100) / 900).ToString("0.00");

                    //string percent = (Convert.ToInt32(trpt.total) * 100 / 900).ToString();
                    double percentageValue = Math.Round((Convert.ToDouble(trpt.total) * 100) / 900, MidpointRounding.AwayFromZero);
                    //int roundedPercentage = (int)Math.Ceiling(percentageValue); // Round up to nearest whole number
                    //string percent = roundedPercentage.ToString();


                    query = "select Grade from GradeChart where " + percentageValue + " between minmarks and maxmarks";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        trpt.grade = reader["Grade"].ToString();
                    }
                    reader.Close();
                    query = "select SUM(CAST(june AS decimal) + CAST(july AS decimal) + CAST(aug AS decimal) + CAST(sep AS decimal) + CAST(oct AS decimal)) AS total1 from Attendance where std='" + trpt.std + "' and div='" + trpt.div + "' and grno='" + trpt.grno + "' and Academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        presentstu = Convert.ToInt32(reader["total1"]);
                    }
                    reader.Close();




                    query = "select Cast (srno as int) as srno,Subject From subjectmaster where std='" + trpt.std + "' and examname='Second Semester' order by srno asc";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        subjectlist.Add(reader[1].ToString());

                    }
                    reader.Close();

                    query = "select [grandtotal],[finalgrade],UPPER(LTRIM(RTRIM(subjectname))) as Subjectname From studentmarksheet where std='" + trpt.std + "' and examname='Second Semester' and grno='" + trpt.grno + "';";
                    cmd = new SqlCommand(query, con);
                    adap = new SqlDataAdapter(cmd);
                    adap.Fill(_subjectmrkstabl);



                    foreach (DataRow mrkrow in _subjectmrkstabl.Rows)
                    {

                        if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.eng))
                        {
                            trpt.engmark1 = mrkrow[0].ToString();
                            trpt.enggrade1 = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.mar))
                        {
                            trpt.marmark1 = mrkrow[0].ToString();
                            trpt.margrade1 = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.hindi))
                        {
                            trpt.hinmark1 = mrkrow[0].ToString();
                            trpt.hingrade1 = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.maths))
                        {
                            trpt.mathmark1 = mrkrow[0].ToString();
                            trpt.mathgrade1 = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.sci))
                        {
                            trpt.evsmark1 = mrkrow[0].ToString();
                            trpt.evsgrade1 = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.socsci))
                        {
                            trpt.ssmark1 = mrkrow[0].ToString();
                            trpt.ssgrade1 = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.art))
                        {
                            trpt.artmark1 = mrkrow[0].ToString();
                            trpt.artgrade1 = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.wrk))
                        {
                            trpt.wrkmark1 = mrkrow[0].ToString();
                            trpt.wrkgrade1 = mrkrow[1].ToString();

                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.pe) || mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.pt))
                        {
                            trpt.ptmark1 = mrkrow[0].ToString();
                            trpt.ptgrade1 = mrkrow[1].ToString();


                        }
                        else if (mrkrow[2].ToString().ToUpper().Trim().Equals(SubjectClass.computer))
                        {
                            trpt.compgrade1 = mrkrow[1].ToString();

                        }
                    }

                    _subjectmrkstabl.Dispose();

                    //calculate total
                    trpt.total1 = "0";

                    if (trpt.engmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.engmark1)).ToString();
                    }
                    if (trpt.marmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.marmark1)).ToString();
                    }
                    if (trpt.hinmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.hinmark1)).ToString();
                    }
                    if (trpt.mathmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.mathmark1)).ToString();
                    }
                    if (trpt.evsmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.evsmark1)).ToString();
                    }
                    if (trpt.ssmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.ssmark1)).ToString();
                    }
                    if (trpt.artmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.artmark1)).ToString();
                    }
                    if (trpt.wrkmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.wrkmark1)).ToString();
                    }
                    if (trpt.ptmark1.All(char.IsDigit))
                    {
                        trpt.total1 = (Convert.ToInt32(trpt.total1) + Convert.ToInt32(trpt.ptmark1)).ToString();
                    }

                    trpt.percentage1 = ((Convert.ToDouble(trpt.total1) * 100) / 900).ToString("0.00");
                    double percentageValue1 = Math.Round((Convert.ToDouble(trpt.total1) * 100) / 900, MidpointRounding.AwayFromZero);
                    //int roundedPercentage1 = (int)Math.Ceiling(percentageValue1); // Round up to nearest whole number
                    //string percent1 = roundedPercentage1.ToString();

                    query = "select Grade from GradeChart where " + percentageValue1 + " between minmarks and maxmarks";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        trpt.grade1 = reader["Grade"].ToString();
                    }
                    reader.Close();

                    query = "select SUM(CAST(nov AS decimal) + CAST(dec AS decimal) + CAST(jan AS decimal) + CAST(feb AS decimal) + CAST(march AS decimal)+ CAST(april AS decimal)+ CAST(may AS decimal)) AS total2 from Attendance where std='" + trpt.std + "' and div='" + trpt.div + "' and grno='" + trpt.grno + "' and Academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        presentstu1 = Convert.ToInt32(reader["total2"]);
                    }
                    reader.Close();
                    trpt.remark = "PASSED";
                    trpt.remark1 = "PASSED";
                    if (trpt.std == "I" || trpt.std == "II" || trpt.std == "III" || trpt.std == "IV" || trpt.std == "V" || trpt.std == "VI" || trpt.std == "VII" || trpt.std == "VIII")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, trpt.engmark, trpt.enggrade, trpt.marmark, trpt.margrade, trpt.hinmark, trpt.hingrade, trpt.mathmark, trpt.mathgrade, trpt.evsmark, trpt.evsgrade, trpt.ssmark, trpt.ssgrade, trpt.artmark, trpt.artgrade, trpt.wrkmark, trpt.wrkgrade, trpt.ptmark, trpt.ptgrade, trpt.compgrade, trpt.total, trpt.percentage, trpt.grade, presentstu, trpt.remark, trpt.engmark1, trpt.enggrade1, trpt.marmark1, trpt.margrade1, trpt.hinmark1, trpt.hingrade1, trpt.mathmark1, trpt.mathgrade1, trpt.evsmark1, trpt.evsgrade1, trpt.ssmark1, trpt.ssgrade1, trpt.artmark1, trpt.artgrade1, trpt.wrkmark1, trpt.wrkgrade1, trpt.ptmark1, trpt.ptgrade1, trpt.compgrade1, trpt.total1, trpt.percentage1, trpt.grade1, presentstu1, trpt.remark1);
                    }
                    if (trpt.std == "I (SE)" || trpt.std == "II (SE)" || trpt.std == "III (SE)" || trpt.std == "IV (SE)" || trpt.std == "V (SE)" || trpt.std == "VI (SE)" || trpt.std == "VII (SE)" || trpt.std == "VIII (SE)" || trpt.std == "I(Hindi)" || trpt.std == "II(Hindi)" || trpt.std == "III(Hindi)" || trpt.std == "IV(Hindi)" || trpt.std == "V(Hindi)" || trpt.std == "VI(Hindi)" || trpt.std == "VII (Hindi)" || trpt.std == "VIII (Hindi)")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, trpt.hinmark, trpt.hingrade, trpt.marmark, trpt.margrade, trpt.engmark, trpt.enggrade, trpt.mathmark, trpt.mathgrade, trpt.evsmark, trpt.evsgrade, trpt.ssmark, trpt.ssgrade, trpt.artmark, trpt.artgrade, trpt.wrkmark, trpt.wrkgrade, trpt.ptmark, trpt.ptgrade, trpt.compgrade, trpt.total, trpt.percentage, trpt.grade, presentstu, trpt.remark, trpt.hinmark1, trpt.hingrade1, trpt.marmark1, trpt.margrade1, trpt.engmark1, trpt.enggrade1, trpt.mathmark1, trpt.mathgrade1, trpt.evsmark1, trpt.evsgrade1, trpt.ssmark1, trpt.ssgrade1, trpt.artmark1, trpt.artgrade1, trpt.wrkmark1, trpt.wrkgrade1, trpt.ptmark1, trpt.ptgrade1, trpt.compgrade1, trpt.total1, trpt.percentage1, trpt.grade1, presentstu1, trpt.remark1);
                    }
                    if (trpt.std == "VIII (Mar)")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, trpt.marmark, trpt.margrade, trpt.hinmark, trpt.hingrade, trpt.engmark, trpt.enggrade, trpt.mathmark, trpt.mathgrade, trpt.evsmark, trpt.evsgrade, trpt.ssmark, trpt.ssgrade, trpt.artmark, trpt.artgrade, trpt.wrkmark, trpt.wrkgrade, trpt.ptmark, trpt.ptgrade, trpt.compgrade, trpt.total, trpt.percentage, trpt.grade, presentstu, trpt.remark, trpt.marmark1, trpt.margrade1, trpt.hinmark1, trpt.hingrade1, trpt.engmark1, trpt.enggrade1, trpt.mathmark1, trpt.mathgrade1, trpt.evsmark1, trpt.evsgrade1, trpt.ssmark1, trpt.ssgrade1, trpt.artmark1, trpt.artgrade1, trpt.wrkmark1, trpt.wrkgrade1, trpt.ptmark1, trpt.ptgrade1, trpt.compgrade1, trpt.total1, trpt.percentage1, trpt.grade1, presentstu1, trpt.remark1);
                    }

                }

                studtable.Dispose();


                PrimaryConsolidate5to8 report = new PrimaryConsolidate5to8();
                report.SetDataSource(_Std1to8Teachersds.Tables[0]);

                return report;

            }
            catch (Exception ex)
            {
                Log.Error("ConsolidationReport1to8.printreport_ServerClick", ex);
                return null;
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }
        public Consolidation5_8 PrintConsolidation5_8(SqlConnection con, string std, string div)
        {

            DataTable StudentMarksheet1to8 = new DataTable();
            DataTable _subjectmrkstabl = new DataTable();
            DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
            try
            {
                consolidatestd9th _Std1to8Teachersds = new consolidatestd9th();
                string select_std = cmbStd.SelectedValue.ToString();
                string select_div = cmbDiv.SelectedValue.ToString();
                string exam = cmbexam.SelectedValue.ToString();

                string year = new FeesModel().setActiveAcademicYear();
                String query = "", percentage = "";
                SqlCommand cmd = null;
                DataTable studtable = new DataTable();
                SqlDataAdapter adap = null;
                List<string> subjectlist = new List<string>();
                SqlDataReader reader = null;
                string hpe = "0", sg = "0", wtr = "0";
                int presentstu = 0; int presentstu1 = 0, failedSubjects = 0;

                query = "select Cast(rollno as int) as rollno,grno,(FNAME+' '+MNAME+' '+LNAME) as studentname,std,div,Convert(nvarchar,cast(dob as date),103) as dob From studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '') order by rollno asc;";
                cmd = new SqlCommand(query, con);
                adap = new SqlDataAdapter(cmd);
                adap.Fill(studtable);

                query = "select Cast (srno as int) as srno,UPPER(Subject) as [Subject] From subjectmaster where std='" + select_std + "' and examname='First Semester' order by srno asc";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    subjectlist.Add(reader[1].ToString());

                }
                reader.Close();


                foreach (DataRow row in studtable.Rows)
                {

                    std9consold trpt = new std9consold();
                    failedSubjects = 0;
                    trpt.rollno = row[0].ToString();
                    trpt.grno = row[1].ToString();
                    trpt.studentname = row[2].ToString();
                    trpt.std = row[3].ToString();
                    trpt.div = row[4].ToString();
                    trpt.dob = row[5].ToString();
                    trpt.examname = "Second Semester";





                    StudentMarksheet1to8 = new DataTable();

                    query = "select finalgrade,summativetotal,UPPER(LTRIM(RTRIM(subjectname))) AS Subjectname From studentmarksheet where std='" + trpt.std + "' and examname='Second Semester' and academicyear='" + year + "' and grno='" + trpt.grno + "';";
                    cmd = new SqlCommand(query, con);
                    adap = new SqlDataAdapter(cmd);
                    adap.Fill(StudentMarksheet1to8);

                    foreach (string str in subjectlist)
                    {
                        double total = 0;
                        if (str.ToUpper().Trim().Equals(SubjectClass.eng))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.eng));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["summativetotal"].ToString().Length > 0 && ro["summativetotal"].ToString().All(char.IsDigit))
                                {
                                    total = total + Convert.ToDouble(ro["summativetotal"]);
                                }
                            }
                            if (total > 0)
                            {
                                trpt.eng = total.ToString();
                                if ((std == "V" || std == "V (SE)") && trpt.eng.ToString().All(char.IsDigit))
                                {
                                    if (Convert.ToInt32(trpt.eng) < 18)
                                    {
                                        failedSubjects++;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(trpt.eng) < 21)
                                    {
                                        failedSubjects++;
                                    }
                                }
                            }
                            else
                            {
                                trpt.eng = "AB";
                            }

                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.mar))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.mar));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["summativetotal"].ToString().Length > 0 && ro["summativetotal"].ToString().All(char.IsDigit))
                                {
                                    total = total + Convert.ToDouble(ro["summativetotal"]);
                                }
                            }
                            if (total > 0)
                            {
                                trpt.mar = total.ToString();
                                if ((std == "V" || std == "V (SE)") && trpt.mar.ToString().All(char.IsDigit))
                                {
                                    if (Convert.ToInt32(trpt.mar) < 18)
                                    {
                                        failedSubjects++;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(trpt.mar) < 21)
                                    {
                                        failedSubjects++;
                                    }
                                }
                            }
                            else
                            {
                                trpt.mar = "AB";
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.hindi))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.hindi));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["summativetotal"].ToString().Length > 0 && ro["summativetotal"].ToString().All(char.IsDigit))
                                {
                                    total = total + Convert.ToDouble(ro["summativetotal"]);
                                }
                            }
                            if (total > 0)
                            {
                                trpt.hin = total.ToString();
                                if ((std == "V" || std == "V (SE)") && trpt.hin.ToString().All(char.IsDigit))
                                {
                                    if (Convert.ToInt32(trpt.hin) < 18)
                                    {
                                        failedSubjects++;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(trpt.hin) < 21)
                                    {
                                        failedSubjects++;
                                    }
                                }
                            }
                            else
                            {
                                trpt.hin = "AB";
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.maths))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.maths));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["summativetotal"].ToString().Length > 0 && ro["summativetotal"].ToString().All(char.IsDigit))
                                {
                                    total = total + Convert.ToDouble(ro["summativetotal"]);
                                }
                            }
                            if (total > 0)
                            {
                                trpt.math = total.ToString();
                                if ((std == "V" || std == "V (SE)") && trpt.math.ToString().All(char.IsDigit))
                                {
                                    if (Convert.ToInt32(trpt.math) < 18)
                                    {
                                        failedSubjects++;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(trpt.math) < 21)
                                    {
                                        failedSubjects++;
                                    }
                                }
                            }
                            else
                            {
                                trpt.math = "AB";
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.sci) || str.ToUpper().Trim().Equals(SubjectClass.evs) || str.ToUpper().Trim().Equals(SubjectClass.envs))
                        {
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.sci) || x.Field<string>("Subjectname").Equals(SubjectClass.evs) || x.Field<string>("Subjectname").Equals(SubjectClass.envs));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["summativetotal"].ToString().Length > 0 && ro["summativetotal"].ToString().All(char.IsDigit))
                                {
                                    total = total + Convert.ToDouble(ro["summativetotal"]);
                                }
                            }
                            if (total > 0)
                            {
                                trpt.sci = total.ToString();
                                if ((std == "V" || std == "V (SE)") && trpt.sci.ToString().All(char.IsDigit))
                                {
                                    if (Convert.ToInt32(trpt.eng) < 18)
                                    {
                                        failedSubjects++;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(trpt.sci) < 21)
                                    {
                                        failedSubjects++;
                                    }
                                }
                            }
                            else
                            {
                                trpt.sci = "AB";
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.socsci))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.socsci));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["summativetotal"].ToString().Length > 0 && ro["summativetotal"].ToString().All(char.IsDigit))
                                {
                                    total = total + Convert.ToDouble(ro["summativetotal"]);
                                }
                            }
                            if (total > 0)
                            {
                                trpt.ss = total.ToString();
                                if ((std == "V" || std == "V (SE)") && trpt.ss.ToString().All(char.IsDigit))
                                {
                                    if (Convert.ToInt32(trpt.ss) < 18)
                                    {
                                        failedSubjects++;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(trpt.ss) < 21)
                                    {
                                        failedSubjects++;
                                    }
                                }
                            }
                            else
                            {
                                trpt.ss = "AB";
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.art))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.art));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["finalgrade"].ToString().Length > 0)
                                {
                                    trpt.art = ro["finalgrade"].ToString();
                                }
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.pe) || str.ToUpper().Trim().Equals(SubjectClass.pt))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.pe) || x.Field<string>("Subjectname").Equals(SubjectClass.pt));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["finalgrade"].ToString().Length > 0)
                                {
                                    trpt.pe = ro["finalgrade"].ToString();
                                }
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.wrk))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.wrk));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["finalgrade"].ToString().Length > 0)
                                {
                                    trpt.js = ro["finalgrade"].ToString();
                                }
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.computer))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.computer));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["finalgrade"].ToString().Length > 0)
                                {
                                    trpt.com = ro["finalgrade"].ToString();
                                }
                            }
                        }

                    }

                    StudentMarksheet1to8.Dispose();

                    //calculate total
                    trpt.total = "0";

                    if (trpt.eng.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.eng)).ToString();
                    }
                    if (trpt.mar.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.mar)).ToString();
                    }
                    if (trpt.hin.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.hin)).ToString();
                    }
                    if (trpt.math.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.math)).ToString();
                    }
                    if (trpt.sci.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.sci)).ToString();
                    }

                    if (trpt.ss.All(char.IsDigit))
                    {
                        trpt.total = (Convert.ToInt32(trpt.total) + Convert.ToInt32(trpt.ss)).ToString();
                    }

                    if (std == "V" || std == "V (SE)")
                    {
                        trpt.percentage = Math.Round((Convert.ToDouble(trpt.total) * 100) / 250, MidpointRounding.AwayFromZero).ToString("0.00");
                    }
                    else
                    {
                        trpt.percentage = Math.Round((Convert.ToDouble(trpt.total) * 100) / 360, MidpointRounding.AwayFromZero).ToString("0.00");
                    }
                    if (failedSubjects > 0)
                    {
                        trpt.remark = "FAIL/PROMOTED";
                    }
                    else
                    {
                        trpt.remark = "PASSED";
                    }

                    query = "select gtotal from Attendance where std='" + select_std + "' and div='" + select_div + "' and grno='" + trpt.grno + "' and Academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        presentstu = Convert.ToInt32(reader["gtotal"]);
                    }
                    reader.Close();


                    query = "select Teachername from TeacherMapping where std='" + select_std + "' and div='" + select_div + "'";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        trpt.trname = reader["Teachername"].ToString();
                    }
                    reader.Close();
                    if (std == "V" || std == "VIII")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, year, exam, trpt.dob, trpt.eng, trpt.mar, trpt.hin, trpt.math, trpt.sci, trpt.ss, trpt.art, trpt.js, trpt.pe, trpt.com, trpt.engf, trpt.marf, trpt.hinf, trpt.scif, trpt.ssf, trpt.mathf, trpt.total, trpt.percentage, presentstu, trpt.remark, trpt.trname);
                    }
                    else if (std == "V (SE)" || std == "VIII (SE)" || std == "VIII (Hindi)")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, year, exam, trpt.dob, trpt.hin, trpt.mar, trpt.eng, trpt.math, trpt.sci, trpt.ss, trpt.art, trpt.js, trpt.pe, trpt.com, trpt.engf, trpt.marf, trpt.hinf, trpt.scif, trpt.ssf, trpt.mathf, trpt.total, trpt.percentage, presentstu, trpt.remark, trpt.trname);
                    }
                    else if (std == "VIII (Mar)")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, year, exam, trpt.dob, trpt.mar, trpt.hin, trpt.eng, trpt.math, trpt.sci, trpt.ss, trpt.art, trpt.js, trpt.pe, trpt.com, trpt.marf, trpt.hinf, trpt.engf, trpt.scif, trpt.ssf, trpt.mathf, trpt.total, trpt.percentage, presentstu, trpt.remark, trpt.trname);
                    }

                }

                studtable.Dispose();


                Consolidation5_8 report = new Consolidation5_8();
                report.SetDataSource(_Std1to8Teachersds.Tables[0]);

                return report;


            }
            catch (Exception ex)
            {
                Log.Error("showaveragesheet9.printreport_ServerClick", ex);
                return null;
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }


        private class subshet
        {
            public subshet()
            {
                dobs = "0";
                orlwrk = "0";
                prect = "0";
                activity = "0";
                project = "0";
                testob = "0";
                homecw = "0";
                other = "0";
                TotalA = "0";
                theory = "0";
                oral = "0";
                TotalB = "0";
                grandtotal = "0";
                grade = "0";
                remark = "-";
            }

            public string Rollno { get; set; }
            public string Grno { get; set; }
            public string studentname { get; set; }
            public string mothername { get; set; }

            public string dobs { get; set; }
            public string orlwrk { get; set; }
            public string prect { get; set; }
            public string activity { get; set; }

            public string project { get; set; }
            public string testob { get; set; }
            public string homecw { get; set; }
            public string other { get; set; }



            public string TotalA { get; set; }
            public string theory { get; set; }
            public string oral { get; set; }
            public string TotalB { get; set; }

            public string grandtotal { get; set; }
            public string grade { get; set; }
            public string remark { get; set; }
            public string sem2writ2 { get; set; }
            public string sem2oral2 { get; set; }



        }
        private class std9consold
        {
            public std9consold()
            {
                eng = "0";
                hin = "0";
                mar = "0";
                math = "0";
                sci = "0";
                ss = "0";
                art = "-";
                js = "-";
                pe = "-";
                com = "-";
                total = "0";
                percentage = "0";
                attend = "0";
                remark = "";

                engf = "";
                marf = "";
                hinf = "";
                mathf = "";
                scif = "";
                ssf = "";


            }

            public string std { get; set; }
            public string div { get; set; }
            public string rollno { get; set; }
            public string grno { get; set; }
            public string studentname { get; set; }
            public string dob { get; set; }
            public string examname { get; set; }
            public string trname { get; set; }
            // trem1
            public string eng { get; set; }
            public string hin { get; set; }
            public string mar { get; set; }
            public string math { get; set; }
            public string sci { get; set; }
            public string ss { get; set; }
            public string art { get; set; }
            public string js { get; set; }
            public string pe { get; set; }
            public string com { get; set; }
            public string total { get; set; }
            public string percentage { get; set; }
            public string attend { get; set; }
            public string remark { get; set; }
            public string engf { get; set; }
            public string marf { get; set; }
            public string hinf { get; set; }
            public string mathf { get; set; }
            public string scif { get; set; }
            public string ssf { get; set; }

        }
        public class EvaluationModel
        {
            public string rollno { get; set; }
            public string grno { get; set; }
            public string studentname { get; set; }
            public string subject { get; set; }
            public string obs1 { get; set; }
            public string ow1 { get; set; }
            public string prac1 { get; set; }
            public string activity1 { get; set; }
            public string project1 { get; set; }
            public string test1 { get; set; }
            public string hw1 { get; set; }
            public string other1 { get; set; }
            public string totalA1 { get; set; }
            public string theory1 { get; set; }
            public string oralprac1 { get; set; }
            public string totalB1 { get; set; }
            public string AB1 { get; set; }
            public string grade1 { get; set; }
            public string obs2 { get; set; }
            public string ow2 { get; set; }
            public string prac2 { get; set; }
            public string activity2 { get; set; }
            public string project2 { get; set; }
            public string test2 { get; set; }
            public string hw2 { get; set; }
            public string other2 { get; set; }
            public string totalA2 { get; set; }
            public string theory2 { get; set; }
            public string oralprac2 { get; set; }
            public string totalB2 { get; set; }
            public string AB2 { get; set; }
            public string grade2 { get; set; }
            public string remark { get; set; }
        }
        private class std1to8consold
        {
            public std1to8consold()
            {
                engmark = "0";
                enggrade = "0";
                hinmark = "0";
                hingrade = "0";
                marmark = "0";
                margrade = "0";
                mathmark = "0";
                mathgrade = "0";
                evsmark = "0";
                evsgrade = "0";
                ssmark = "0";
                ssgrade = "0";
                artmark = "0";
                artgrade = "0";
                wrkmark = "0";
                wrkgrade = "0";
                ptmark = "0";
                ptgrade = "0";
                compgrade = "0";
                total = "0";
                percentage = "0";
                grade = "0";
                attend = "0";
                remark = "0";

                engmark1 = "0";
                enggrade1 = "0";
                hinmark1 = "0";
                hingrade1 = "0";
                marmark1 = "0";
                margrade1 = "0";
                mathmark1 = "0";
                mathgrade1 = "0";
                evsmark1 = "0";
                evsgrade1 = "0";
                ssmark1 = "0";
                ssgrade1 = "0";
                artmark1 = "0";
                artgrade1 = "0";
                wrkmark1 = "0";
                wrkgrade1 = "0";
                ptmark1 = "0";
                ptgrade1 = "0";
                compgrade1 = "0";
                total1 = "0";
                percentage1 = "0";
                grade1 = "0";
                attend1 = "0";
                remark1 = "0";
            }

            public string std { get; set; }
            public string div { get; set; }
            public string rollno { get; set; }
            public string grno { get; set; }
            public string studentname { get; set; }
            // trem1
            public string engmark { get; set; }
            public string enggrade { get; set; }
            public string hinmark { get; set; }
            public string hingrade { get; set; }
            public string marmark { get; set; }
            public string margrade { get; set; }
            public string mathmark { get; set; }
            public string mathgrade { get; set; }
            public string evsmark { get; set; }
            public string evsgrade { get; set; }
            public string ssmark { get; set; }
            public string ssgrade { get; set; }
            public string artmark { get; set; }
            public string artgrade { get; set; }
            public string wrkmark { get; set; }
            public string wrkgrade { get; set; }
            public string ptmark { get; set; }
            public string ptgrade { get; set; }
            public string compgrade { get; set; }
            public string total { get; set; }
            public string percentage { get; set; }
            public string grade { get; set; }
            public string attend { get; set; }
            public string remark { get; set; }

            //term2
            public string engmark1 { get; set; }
            public string enggrade1 { get; set; }
            public string hinmark1 { get; set; }
            public string hingrade1 { get; set; }
            public string marmark1 { get; set; }
            public string margrade1 { get; set; }
            public string mathmark1 { get; set; }
            public string mathgrade1 { get; set; }
            public string evsmark1 { get; set; }
            public string evsgrade1 { get; set; }
            public string ssmark1 { get; set; }
            public string ssgrade1 { get; set; }
            public string artmark1 { get; set; }
            public string artgrade1 { get; set; }
            public string wrkmark1 { get; set; }
            public string wrkgrade1 { get; set; }
            public string ptmark1 { get; set; }
            public string ptgrade1 { get; set; }
            public string compgrade1 { get; set; }
            public string total1 { get; set; }
            public string percentage1 { get; set; }
            public string grade1 { get; set; }
            public string attend1 { get; set; }
            public string remark1 { get; set; }
        }
        public static class SubjectClass
        {
            public static string eng = "ENGLISH";
            public static string eng1 = "English";
            public static string mar = "MARATHI";
            public static string hindi = "HINDI";
            public static string maths = "MATHS";
            public static string sci = "SCIENCE";
            public static string socsci = "S. STUDIES";
            public static string art = "ART";
            public static string computer = "COMPUTER";
            public static string pe = "P.E.";
            public static string js = "JS";
            public static string evs = "E.V.S.";
            public static string envs = "ENVIRONMENTAL STUDIES";
            public static string wrk = "WORK EXP";
            public static string pt = "P.T.";


        }

    }
}