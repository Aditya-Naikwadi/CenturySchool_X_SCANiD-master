//using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.MarksheetModule.DataSet.ds10;
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
    public partial class Consolidate9th : System.Web.UI.Page
    {
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

        protected void examCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmbexam.SelectedValue.ToString().Equals("Select Exam"))
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
                    cmbStd.DataBind();
                    cmbStd.DataTextField = "std";
                    cmbStd.DataValueField = "std";
                    cmbStd.DataBind();
                    // cmbStd.SelectedValue = "Select Std";
                    if (!string.IsNullOrEmpty(std_sess))
                    {
                        cmbStd.SelectedValue = std_sess;
                        cmbStd.Enabled = false;

                    }
                    else
                    {
                        // If std_sess is empty, set the default selected value
                        cmbStd.SelectedValue = "Select Std";
                        cmbStd.Enabled = true;
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
                    // cmbDiv.SelectedValue = "Select Div";

                    if (!string.IsNullOrEmpty(div_sess))
                    {
                        cmbDiv.SelectedValue = div_sess;
                        cmbDiv.Enabled = false;
                    }
                    else
                    {
                        // If std_sess is empty, set the default selected value
                        cmbDiv.SelectedValue = "Select Div";
                        cmbDiv.Enabled = true;
                    }

                    query = "select distinct [Examname] From ExamMaster;";
                    adap = new SqlDataAdapter(query, con);
                    DataTable examamaster = new DataTable();
                    adap.Fill(examamaster);
                    examamaster.Rows.Add("Select Exam");
                    examamaster.Rows.Add("Average");
                    cmbexam.DataSource = examamaster;
                    cmbexam.DataBind();
                    cmbexam.DataTextField = "Examname";
                    cmbexam.DataValueField = "Examname";
                    cmbexam.DataBind();
                    cmbexam.SelectedValue = "Select Exam";

                }
            }
            catch (Exception ex)
            {
                Log.Error("Consolidate9th.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }


        public SecondaryConsolidate9 printConsolidatereportIXtoX(SqlConnection con, string std, string div)
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

                query = "select Cast(rollno as int) as rollno,grno,(fullname) as studentname,std,div,Convert(nvarchar,cast(dob as date),103) as dob From studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '') order by rollno asc;";
                cmd = new SqlCommand(query, con);
                adap = new SqlDataAdapter(cmd);
                adap.Fill(studtable);



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
                    trpt.examname = exam;



                    query = "select Cast (srno as int) as srno,Subject From subjectmaster where std='" + select_std + "' and examname='" + exam + "' order by srno asc";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        subjectlist.Add(reader[1].ToString());

                    }
                    reader.Close();

                    StudentMarksheet1to8 = new DataTable();

                    query = "select Written,oralsprac,total,Subjectname From studentmarksheet8to9 where std='" + trpt.std + "' and examname='" + exam + "' and grno='" + trpt.grno + "';";
                    cmd = new SqlCommand(query, con);
                    adap = new SqlDataAdapter(cmd);
                    adap.Fill(StudentMarksheet1to8);

                    foreach (DataRow mrkrow in StudentMarksheet1to8.Rows)
                    {
                        if (mrkrow[3].Equals(SubjectClass.eng))
                        {
                            trpt.eng = mrkrow[2].ToString();
                            if (trpt.eng.All(char.IsDigit))
                            {
                                if (Convert.ToInt32(trpt.eng) < 28)
                                {
                                    trpt.engf = "*";
                                    failedSubjects++;
                                }
                            }
                        }
                        else if (mrkrow[3].Equals(SubjectClass.mar))
                        {
                            trpt.mar = mrkrow[2].ToString();
                            if (trpt.mar.All(char.IsDigit))
                            {
                                if (Convert.ToInt32(trpt.mar) < 28)
                                {
                                    trpt.marf = "*";
                                    failedSubjects++;
                                }
                            }
                        }
                        else if (mrkrow[3].Equals(SubjectClass.hindi) || mrkrow[3].Equals(SubjectClass.hind))
                        {
                            trpt.hin = mrkrow[2].ToString();
                            if (trpt.hin.All(char.IsDigit))
                            {
                                if (Convert.ToInt32(trpt.hin) < 28)
                                {
                                    trpt.hinf = "*";
                                    failedSubjects++;
                                }
                            }
                        }
                        else if (mrkrow[3].Equals(SubjectClass.maths))
                        {
                            trpt.math = mrkrow[2].ToString();
                            if (trpt.math.All(char.IsDigit))
                            {
                                if (Convert.ToInt32(trpt.math) < 28)
                                {
                                    trpt.mathf = "*";
                                    failedSubjects++;
                                }
                            }
                        }
                        else if (mrkrow[3].Equals(SubjectClass.sci))
                        {
                            trpt.sci = mrkrow[2].ToString();
                            if (trpt.sci.All(char.IsDigit))
                            {
                                if (Convert.ToInt32(trpt.sci) < 28)
                                {
                                    trpt.scif = "*";
                                    failedSubjects++;
                                }
                            }
                        }

                        else if (mrkrow[3].Equals(SubjectClass.socsci) || mrkrow[3].Equals(SubjectClass.ss))
                        {
                            trpt.ss = mrkrow[2].ToString();
                            if (trpt.ss.All(char.IsDigit))
                            {
                                if (Convert.ToInt32(trpt.ss) < 28)
                                {
                                    trpt.ssf = "*";
                                    failedSubjects++;
                                }
                            }
                        }
                        else if (mrkrow[3].Equals(SubjectClass.art))
                        {
                            trpt.art = mrkrow[2].ToString();
                        }
                        else if (mrkrow[3].Equals(SubjectClass.pe))
                        {
                            trpt.pe = mrkrow[2].ToString();
                        }
                        else if (mrkrow[3].Equals(SubjectClass.computer))
                        {
                            trpt.com = mrkrow[2].ToString();
                        }
                        else if (mrkrow[3].Equals(SubjectClass.js))
                        {
                            trpt.js = mrkrow[2].ToString();
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

                    trpt.percentage = ((Convert.ToDouble(trpt.total) * 100) / 480).ToString("0.00");
                    if (failedSubjects > 0)
                    {
                        trpt.remark += "F" + failedSubjects;
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
                    if (std == "X" || std == "IX")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, year, exam, trpt.dob, trpt.eng, trpt.mar, trpt.hin, trpt.math, trpt.sci, trpt.ss, trpt.art, trpt.js, trpt.pe, trpt.com, trpt.engf, trpt.marf, trpt.hinf, trpt.scif, trpt.ssf, trpt.mathf, trpt.total, trpt.percentage, presentstu, trpt.remark, trpt.trname);
                    }
                    else if (std == "IX (Mar)" || std == "X (Mar)")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, year, exam, trpt.dob, trpt.mar, trpt.hin, trpt.eng, trpt.math, trpt.sci, trpt.ss, trpt.art, trpt.js, trpt.pe, trpt.com, trpt.marf, trpt.hinf, trpt.engf, trpt.scif, trpt.ssf, trpt.mathf, trpt.total, trpt.percentage, presentstu, trpt.remark, trpt.trname);
                    }
                    else if (std == "IX (Hindi)" || std == "X (Hindi)")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, year, exam, trpt.dob, trpt.hin, trpt.mar, trpt.eng, trpt.math, trpt.sci, trpt.ss, trpt.art, trpt.js, trpt.pe, trpt.com, trpt.hinf, trpt.marf, trpt.engf, trpt.scif, trpt.ssf, trpt.mathf, trpt.total, trpt.percentage, presentstu, trpt.remark, trpt.trname);
                    }
                }

                studtable.Dispose();


                SecondaryConsolidate9 report = new SecondaryConsolidate9();
                report.SetDataSource(_Std1to8Teachersds.Tables[0]);

                return report;


            }
            catch (Exception ex)
            {
                Log.Error("Consolidate9th.printreport_ServerClick", ex);
                return null;
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void printreport_ServerClick(object sender, EventArgs e)
        {

            string std, div, exa, examname;
            std = cmbStd.SelectedValue.ToString();
            div = cmbDiv.SelectedValue.ToString();
            examname = cmbexam.SelectedValue.ToString();
            SqlConnection con = null;
            SecondaryConsolidate9 report = new SecondaryConsolidate9();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    if (examname == "Average")
                    {
                        report = showaveragesheet9(con, std, div);
                    }
                    else
                    {
                        report = printConsolidatereportIXtoX(con, std, div);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("ConsolidationReport9th.printreport_ServerClick", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

                string folderpath = Server.MapPath("MarksheetFile");
                string filename = "ConsolidationReport_" + std + div + examname + ".pdf";

                report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(Server.MapPath("~/MarksheetModule/MarksheetFile/" + filename));
                Response.End();
            }

        }

        public SecondaryConsolidate9 showaveragesheet9(SqlConnection con, string std, string div)
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

                query = "select Cast(rollno as int) as rollno,grno,(fullname) as studentname,std,div,Convert(nvarchar,cast(dob as date),103) as dob From studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '') order by rollno asc;";
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
                    trpt.examname = exam;





                    StudentMarksheet1to8 = new DataTable();

                    query = "select Written,oralsprac,total,UPPER(LTRIM(RTRIM(subjectname))) AS Subjectname From studentmarksheet8to9 where std='" + trpt.std + "' and academicyear='" + year + "' and grno='" + trpt.grno + "';";
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
                                if (ro["total"].ToString().Length > 0 && ro["total"].ToString().All(char.IsDigit))
                                {
                                    total = total + Convert.ToDouble(ro["total"]);
                                }
                            }
                            if (total > 0)
                            {
                                trpt.eng = Math.Round(Convert.ToDouble(total) / 2, MidpointRounding.AwayFromZero).ToString("##");

                                if (Convert.ToInt32(trpt.eng) < 35)
                                {
                                    trpt.engf = "*";
                                    failedSubjects++;
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
                                if (ro["total"].ToString().Length > 0 && ro["total"].ToString().All(char.IsDigit))
                                {
                                    total = total + Convert.ToDouble(ro["total"]);
                                }
                            }
                            if (total > 0)
                            {
                                trpt.mar = Math.Round(Convert.ToDouble(total) / 2, MidpointRounding.AwayFromZero).ToString("##");
                                if (Convert.ToInt32(trpt.mar) < 35)
                                {
                                    trpt.marf = "*";
                                    failedSubjects++;
                                }
                            }
                            else
                            {
                                trpt.mar = "AB";
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.hind))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.hindi) || x.Field<string>("Subjectname").Equals(SubjectClass.hind));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["total"].ToString().Length > 0 && ro["total"].ToString().All(char.IsDigit))
                                {
                                    total = total + Convert.ToDouble(ro["total"]);
                                }
                            }
                            if (total > 0)
                            {
                                trpt.hin = Math.Round(Convert.ToDouble(total) / 2, MidpointRounding.AwayFromZero).ToString("##");
                                if (Convert.ToInt32(trpt.hin) < 35)
                                {
                                    trpt.hinf = "*";
                                    failedSubjects++;
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
                                if (ro["total"].ToString().Length > 0 && ro["total"].ToString().All(char.IsDigit))
                                {
                                    total = total + Convert.ToDouble(ro["total"]);
                                }
                            }
                            if (total > 0)
                            {
                                trpt.math = Math.Round(Convert.ToDouble(total) / 2, MidpointRounding.AwayFromZero).ToString("##");
                                if (Convert.ToInt32(trpt.math) < 35)
                                {
                                    trpt.mathf = "*";
                                    failedSubjects++;
                                }
                            }
                            else
                            {
                                trpt.math = "AB";
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.sci))
                        {
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.sci));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["total"].ToString().Length > 0 && ro["total"].ToString().All(char.IsDigit))
                                {
                                    total = total + Convert.ToDouble(ro["total"]);
                                }
                            }
                            if (total > 0)
                            {
                                trpt.sci = Math.Round(Convert.ToDouble(total) / 2, MidpointRounding.AwayFromZero).ToString("##");
                                if (Convert.ToInt32(trpt.sci) < 35)
                                {
                                    trpt.scif = "*";
                                    failedSubjects++;
                                }
                            }
                            else
                            {
                                trpt.sci = "AB";
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.socsci) || str.ToUpper().Trim().Equals((SubjectClass.ss).ToUpper()))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.socsci) || x.Field<string>("Subjectname").Equals((SubjectClass.ss).ToUpper()));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["total"].ToString().Length > 0 && ro["total"].ToString().All(char.IsDigit))
                                {
                                    total = total + Convert.ToDouble(ro["total"]);
                                }
                            }
                            if (total > 0)
                            {
                                trpt.ss = Math.Round(Convert.ToDouble(total) / 2, MidpointRounding.AwayFromZero).ToString("##");
                                if (Convert.ToInt32(trpt.ss) < 35)
                                {
                                    trpt.ssf = "*";
                                    failedSubjects++;
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
                                if (ro["total"].ToString().Length > 0)
                                {
                                    trpt.art = ro["total"].ToString();
                                }
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.pe))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.pe));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["total"].ToString().Length > 0)
                                {
                                    trpt.pe = ro["total"].ToString();
                                }
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.js))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.js));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["total"].ToString().Length > 0)
                                {
                                    trpt.js = ro["total"].ToString();
                                }
                            }
                        }
                        if (str.ToUpper().Trim().Equals(SubjectClass.computer))
                        {
                            //check for both exams
                            var datarow = StudentMarksheet1to8.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.computer));

                            foreach (DataRow ro in datarow)
                            {
                                if (ro["total"].ToString().Length > 0)
                                {
                                    trpt.com = ro["total"].ToString();
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

                    trpt.percentage = ((Convert.ToDouble(trpt.total) * 100) / 600).ToString("0.00");

                    if (failedSubjects > 0)
                    {
                        trpt.remark += "F" + failedSubjects;
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
                    if (std == "X" || std == "IX")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, year, exam, trpt.dob, trpt.eng, trpt.mar, trpt.hin, trpt.math, trpt.sci, trpt.ss, trpt.art, trpt.js, trpt.pe, trpt.com, trpt.engf, trpt.marf, trpt.hinf, trpt.scif, trpt.ssf, trpt.mathf, trpt.total, trpt.percentage, presentstu, trpt.remark, trpt.trname);
                    }
                    else if (std == "IX (Mar)" || std == "X (Mar)")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, year, exam, trpt.dob, trpt.mar, trpt.hin, trpt.eng, trpt.math, trpt.sci, trpt.ss, trpt.art, trpt.js, trpt.pe, trpt.com, trpt.marf, trpt.hinf, trpt.engf, trpt.scif, trpt.ssf, trpt.mathf, trpt.total, trpt.percentage, presentstu, trpt.remark, trpt.trname);
                    }
                    else if (std == "IX (Hindi)" || std == "X (Hindi)")
                    {
                        _Std1to8Teachersds.Tables[0].Rows.Add(trpt.std, trpt.div, trpt.rollno, trpt.grno, trpt.studentname, year, exam, trpt.dob, trpt.hin, trpt.mar, trpt.eng, trpt.math, trpt.sci, trpt.ss, trpt.art, trpt.js, trpt.pe, trpt.com, trpt.hinf, trpt.marf, trpt.engf, trpt.scif, trpt.ssf, trpt.mathf, trpt.total, trpt.percentage, presentstu, trpt.remark, trpt.trname);
                    }
                }

                studtable.Dispose();


                SecondaryConsolidate9 report = new SecondaryConsolidate9();
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

        public static class SubjectClass
        {
            public static string eng = "ENGLISH";
            public static string mar = "MARATHI";
            public static string hindi = "Hindi";
            public static string hind = "HINDI";
            public static string maths = "MATHS";
            public static string sci = "SCIENCE";
            public static string socsci = "S. STUDIES";
            public static string ss = "S.S. Studies";
            public static string art = "ART";
            public static string computer = "COMPUTER";
            public static string pe = "P.E.";
            public static string js = "JS";
            public static string evs = "E.V.S.";
            public static string wrk = "WORK EXP";
            public static string pt = "P.T.";


        }
    }
}