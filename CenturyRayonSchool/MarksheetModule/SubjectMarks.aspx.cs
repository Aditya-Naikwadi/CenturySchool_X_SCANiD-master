using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.MarksheetModule
{
    public partial class SubjectMarks : System.Web.UI.Page
    {
        DataTable subjectmarks = new DataTable();
        Label lblusercode = new Label();
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

            subjectmarks.Columns.Add("RollNo");
            subjectmarks.Columns.Add("GRNO");
            subjectmarks.Columns.Add("StudentName");
            subjectmarks.Columns.Add("std");
            subjectmarks.Columns.Add("div");
            subjectmarks.Columns.Add("DailyObser");
            subjectmarks.Columns.Add("Orals");
            subjectmarks.Columns.Add("PracExp");
            subjectmarks.Columns.Add("Activity");
            subjectmarks.Columns.Add("Project");
            subjectmarks.Columns.Add("Unittest");
            subjectmarks.Columns.Add("Selfstudy");
            subjectmarks.Columns.Add("others");
            subjectmarks.Columns.Add("Total");
            subjectmarks.Columns.Add("sumorals");
            subjectmarks.Columns.Add("sumwritten");
            subjectmarks.Columns.Add("sumtotal");
            subjectmarks.Columns.Add("GrandTotal");
            subjectmarks.Columns.Add("Grade");
        }

        public void loadFormControl()
        {
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();

                    string query;

                    query = "select std from std where std not in ('ALL','LEFT');";
                    SqlDataAdapter adap1 = new SqlDataAdapter(query, con);
                    DataTable std = new DataTable();
                    adap1.Fill(std);
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
                    adap1 = new SqlDataAdapter(query, con);
                    DataTable div = new DataTable();
                    adap1.Fill(div);
                    div.Rows.Add("Select Div");
                    cmbDiv.DataSource = div;
                    cmbDiv.DataBind();
                    cmbDiv.DataTextField = "Div";
                    cmbDiv.DataValueField = "Div";
                    cmbDiv.DataBind();
                    //cmbDiv.SelectedValue = "Select Div";

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
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    DataTable examamaster = new DataTable();
                    adap.Fill(examamaster);
                    examamaster.Rows.Add("Select Exam");
                    cmbexam.DataSource = examamaster;
                    cmbexam.DataBind();
                    cmbexam.DataTextField = "Examname";
                    cmbexam.DataValueField = "Examname";
                    cmbexam.DataBind();
                    cmbexam.SelectedValue = "Select Exam";

                    fillstulist(std_sess, div_sess);

                    query = "select [year] From Academicyear order by [status] asc;";
                    adap = new SqlDataAdapter(query, con);
                    DataTable academicyear = new DataTable();
                    adap.Fill(academicyear);
                    academicyear.Rows.Add("Select Year");
                    cmbAcademicyear.DataSource = academicyear;
                    cmbAcademicyear.DataBind();
                    cmbAcademicyear.DataTextField = "year";
                    cmbAcademicyear.DataValueField = "year";
                    cmbAcademicyear.DataBind();
                    cmbAcademicyear.SelectedValue = "Select Year";

                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
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

        protected void cmbexam_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                DataTable subjecttable = new DataTable();
                using (con = Connection.getConnection())
                {
                    con.Open();

                    string query = "", select_std = "", select_div = "", examname = "";

                    select_std = cmbStd.SelectedValue.ToString();
                    select_div = cmbDiv.SelectedValue.ToString();
                    examname = cmbexam.SelectedValue.ToString();

                    if (select_std != "Select Std" && select_div != "Select Div" && examname != "Select Exam")
                    {
                        query = "Select Cast([srno] as int) as srno,[subject] from subjectmaster where std='" + select_std + "'  and examname='" + examname + "'  order by srno;";
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cmbsubject.Items.Add(reader[1].ToString());
                        }
                        reader.Close();

                    }



                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.cmbexam_SelectedIndexChanged", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void FetchData_ServerClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                fillGridView();
            }
        }

        public void fillGridView()
        {
            DataTable stud_tbl = new DataTable();

            SqlConnection con = null;
            try
            {
                DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                string query = "", select_std = "", select_div = "", year = "", exam = "", grno = "", studentname = "0", ReceiptDate = "";

                string Amtpaid = "", freeshiptype = "";

                select_std = cmbStd.SelectedValue.ToString();
                select_div = cmbDiv.SelectedValue.ToString();
                exam = cmbexam.SelectedValue.ToString();
                year = cmbAcademicyear.SelectedValue.ToString();

                studentname = cmbstudentname.SelectedValue.ToString();
                using (con = Connection.getConnection())
                {
                    con.Open();

                    if (studentname.Equals("ALL"))
                    {
                        query = "select ROLLNO,GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,Academicyear,admissiontype from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '') order by Cast(ROLLNO as int) asc;";
                    }
                    else
                    {
                        query = "select ROLLNO,GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,Academicyear,admissiontype from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + year + "' and grno='" + studentname + "' and (leftstatus IS NULL OR leftstatus = '') order by Cast(ROLLNO as int) asc;";
                    }
                    //SqlCommand cmd = new SqlCommand(query, con);
                    //SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    //ad.Fill(stud_tbl);
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        subjectmarks.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
                    }
                    reader.Close();


                    fetchdata(select_std, select_div, exam, con);


                    GridCollection.DataSource = subjectmarks;
                    GridCollection.DataBind();


                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.fillGridView", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
                stud_tbl.Dispose();
                subjectmarks.Dispose();
            }
        }

        public void fillstulist(string select_std, string select_div)
        {
            SqlConnection con = null;
            try
            {
                DataTable studtable = new DataTable();
                using (con = Connection.getConnection())
                {
                    con.Open();

                    string query = "", academicyear = "";

                    select_std = cmbStd.SelectedValue.ToString();
                    select_div = cmbDiv.SelectedValue.ToString();
                    academicyear = cmbAcademicyear.SelectedValue.ToString();

                    if (select_std != "Select Std" && select_div != "Select Div")
                    {
                        query = "select grno,fullname,(grno +' / '+ fullname) as stuname from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '');";
                        SqlDataAdapter adap = new SqlDataAdapter(query, con);
                        adap.Fill(studtable);

                        studtable.Rows.Add("ALL", "ALL", "ALL");
                        cmbstudentname.DataSource = studtable;
                        cmbstudentname.DataBind();
                        cmbstudentname.DataTextField = "stuname";
                        cmbstudentname.DataValueField = "grno";
                        cmbstudentname.DataBind();
                        cmbstudentname.SelectedValue = "ALL";

                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.fillstulist", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        public void fetchdata(string std, string div, string examname, SqlConnection con)
        {
            string year = cmbAcademicyear.SelectedValue.ToString();

            string query = "Select Rollno,Grno,Studentname,std,div,[DailyObser],[Orals],[practicalexp],[activity],[project],[UnitTest],[Selfstudy],[Others],[Total],[summativeorals],[summativewritten],[summativetotal],[grandtotal],[finalgrade] From studentmarksheet where std = '" + std + "' and div = '" + div + "' and Examname = '" + examname + "'  and subjectname = '" + cmbsubject.Text + "' and academicyear='" + year + "'; ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable subjtable = new DataTable();
            da.Fill(subjtable);



            foreach (DataRow ro in subjectmarks.Rows)
            {
                string standard = ro["std"].ToString();
                string grno = ro["Grno"].ToString();
                var dr = subjtable.AsEnumerable().Where(x => x.Field<string>("std").Equals(standard) && x.Field<string>("Grno").Equals(grno)).DefaultIfEmpty(null).FirstOrDefault();

                if (dr != null)
                {

                    ro["DailyObser"] = dr["DailyObser"].ToString();
                    ro["Orals"] = dr["Orals"].ToString();
                    ro["PracExp"] = dr["practicalexp"].ToString();
                    ro["Activity"] = dr["activity"].ToString();
                    ro["Project"] = dr["project"].ToString();
                    ro["Unittest"] = dr["UnitTest"].ToString();
                    ro["Selfstudy"] = dr["Selfstudy"].ToString();
                    ro["others"] = dr["Others"].ToString();
                    ro["Total"] = dr["Total"].ToString();
                    ro["sumorals"] = dr["summativeorals"].ToString();
                    ro["sumwritten"] = dr["summativewritten"].ToString();
                    ro["sumtotal"] = dr["summativetotal"].ToString();
                    ro["GrandTotal"] = dr["grandtotal"].ToString();
                    ro["Grade"] = dr["finalgrade"].ToString();
                }
                else
                {
                    ro["std"] = "0";
                    ro["div"] = "0";
                    ro["DailyObser"] = "0";
                    ro["Orals"] = "0";
                    ro["PracExp"] = "0";
                    ro["Activity"] = "0";
                    ro["Project"] = "0";
                    ro["Unittest"] = "0";
                    ro["Selfstudy"] = "0";
                    ro["others"] = "0";
                    ro["Total"] = "0";
                    ro["sumorals"] = "0";
                    ro["sumwritten"] = "0";
                    ro["sumtotal"] = "0";
                    ro["GrandTotal"] = "0";
                    ro["Grade"] = "0";
                }
            }

        }



        protected void DailyObser_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0", grade = "";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox dailyObservationTextBox = (TextBox)row.FindControl("DailyObser");
                TextBox oralsTextBox = (TextBox)row.FindControl("Orals");
                TextBox pracExpTextBox = (TextBox)row.FindControl("PracExp");
                TextBox activityTextBox = (TextBox)row.FindControl("Activity");
                TextBox projectTextBox = (TextBox)row.FindControl("Project");
                TextBox unitTestTextBox = (TextBox)row.FindControl("Unittest");
                TextBox selfStudyTextBox = (TextBox)row.FindControl("Selfstudy");
                TextBox othersTextBox = (TextBox)row.FindControl("Others");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");

                TextBox sumtotalTextBox = (TextBox)row.FindControl("sumtotal");
                TextBox grandtotalTextBox = (TextBox)row.FindControl("GrandTotal");
                TextBox gradeTextBox = (TextBox)row.FindControl("Grade");
                row.FindControl("DailyObser").Focus();

                string dailyObservation = dailyObservationTextBox.Text;
                string orals = oralsTextBox.Text;
                string pracExp = pracExpTextBox.Text;
                string activity = activityTextBox.Text;
                string project = projectTextBox.Text;
                string unitTest = unitTestTextBox.Text;
                string selfStudy = selfStudyTextBox.Text;
                string others = othersTextBox.Text;

                string sumtotal = sumtotalTextBox.Text;

                if (dailyObservation.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dailyObservation)).ToString("00");
                }
                if (orals.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(orals)).ToString("00");
                }
                if (pracExp.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(pracExp)).ToString("00");
                }
                if (activity.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(activity)).ToString("00");
                }
                if (project.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(project)).ToString("00");
                }
                if (unitTest.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(unitTest)).ToString("00");
                }
                if (selfStudy.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(selfStudy)).ToString("00");
                }
                if (others.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(others)).ToString("00");
                }

                if (dailyObservation.ToLower().Equals("ab") && orals.ToLower().Equals("ab") && pracExp.ToLower().Equals("ab") && activity.ToLower().Equals("ab") && project.ToLower().Equals("ab") && unitTest.ToLower().Equals("ab") && selfStudy.ToLower().Equals("ab") && others.ToLower().Equals("ab"))
                {
                    totalTextBox.Text = "AB";
                }
                else
                {
                    totalTextBox.Text = total.ToString();
                }

                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }
                if (sumtotal.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(sumtotal)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && sumtotal.ToLower().Equals("ab"))
                {

                    grandtotalTextBox.Text = "AB";
                    gradeTextBox.Text = "AB";
                }
                else
                {

                    grandtotalTextBox.Text = grandtotal.ToString();
                    grade = findgrade(grandtotal);
                    gradeTextBox.Text = grade;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.DailyObser_TextChanged", ex);
            }
        }

        protected void Orals_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0", grade = "";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox dailyObservationTextBox = (TextBox)row.FindControl("DailyObser");
                TextBox oralsTextBox = (TextBox)row.FindControl("Orals");
                TextBox pracExpTextBox = (TextBox)row.FindControl("PracExp");
                TextBox activityTextBox = (TextBox)row.FindControl("Activity");
                TextBox projectTextBox = (TextBox)row.FindControl("Project");
                TextBox unitTestTextBox = (TextBox)row.FindControl("Unittest");
                TextBox selfStudyTextBox = (TextBox)row.FindControl("Selfstudy");
                TextBox othersTextBox = (TextBox)row.FindControl("Others");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");
                TextBox sumtotalTextBox = (TextBox)row.FindControl("sumtotal");
                TextBox grandtotalTextBox = (TextBox)row.FindControl("GrandTotal");
                TextBox gradeTextBox = (TextBox)row.FindControl("Grade");
                row.FindControl("Orals").Focus();

                string dailyObservation = dailyObservationTextBox.Text;
                string orals = oralsTextBox.Text;
                string pracExp = pracExpTextBox.Text;
                string activity = activityTextBox.Text;
                string project = projectTextBox.Text;
                string unitTest = unitTestTextBox.Text;
                string selfStudy = selfStudyTextBox.Text;
                string others = othersTextBox.Text;
                string sumtotal = sumtotalTextBox.Text;

                if (dailyObservation.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dailyObservation)).ToString("00");
                }
                if (orals.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(orals)).ToString("00");
                }
                if (pracExp.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(pracExp)).ToString("00");
                }
                if (activity.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(activity)).ToString("00");
                }
                if (project.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(project)).ToString("00");
                }
                if (unitTest.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(unitTest)).ToString("00");
                }
                if (selfStudy.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(selfStudy)).ToString("00");
                }
                if (others.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(others)).ToString("00");
                }

                if (dailyObservation.ToLower().Equals("ab") && orals.ToLower().Equals("ab") && pracExp.ToLower().Equals("ab") && activity.ToLower().Equals("ab") && project.ToLower().Equals("ab") && unitTest.ToLower().Equals("ab") && selfStudy.ToLower().Equals("ab") && others.ToLower().Equals("ab"))
                {
                    totalTextBox.Text = "AB";
                }
                else
                {
                    totalTextBox.Text = total.ToString();
                }
                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }
                if (sumtotal.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(sumtotal)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && sumtotal.ToLower().Equals("ab"))
                {

                    grandtotalTextBox.Text = "AB";
                    gradeTextBox.Text = "AB";
                }
                else
                {

                    grandtotalTextBox.Text = grandtotal.ToString();
                    grade = findgrade(grandtotal);
                    gradeTextBox.Text = grade;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.Orals_TextChanged", ex);
            }
        }

        protected void PracExp_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0", grade = "";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox dailyObservationTextBox = (TextBox)row.FindControl("DailyObser");
                TextBox oralsTextBox = (TextBox)row.FindControl("Orals");
                TextBox pracExpTextBox = (TextBox)row.FindControl("PracExp");
                TextBox activityTextBox = (TextBox)row.FindControl("Activity");
                TextBox projectTextBox = (TextBox)row.FindControl("Project");
                TextBox unitTestTextBox = (TextBox)row.FindControl("Unittest");
                TextBox selfStudyTextBox = (TextBox)row.FindControl("Selfstudy");
                TextBox othersTextBox = (TextBox)row.FindControl("Others");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");
                TextBox sumtotalTextBox = (TextBox)row.FindControl("sumtotal");
                TextBox grandtotalTextBox = (TextBox)row.FindControl("GrandTotal");
                TextBox gradeTextBox = (TextBox)row.FindControl("Grade");
                row.FindControl("PracExp").Focus();

                string dailyObservation = dailyObservationTextBox.Text;
                string orals = oralsTextBox.Text;
                string pracExp = pracExpTextBox.Text;
                string activity = activityTextBox.Text;
                string project = projectTextBox.Text;
                string unitTest = unitTestTextBox.Text;
                string selfStudy = selfStudyTextBox.Text;
                string others = othersTextBox.Text;
                string sumtotal = sumtotalTextBox.Text;

                if (dailyObservation.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dailyObservation)).ToString("00");
                }
                if (orals.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(orals)).ToString("00");
                }
                if (pracExp.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(pracExp)).ToString("00");
                }
                if (activity.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(activity)).ToString("00");
                }
                if (project.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(project)).ToString("00");
                }
                if (unitTest.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(unitTest)).ToString("00");
                }
                if (selfStudy.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(selfStudy)).ToString("00");
                }
                if (others.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(others)).ToString("00");
                }

                if (dailyObservation.ToLower().Equals("ab") && orals.ToLower().Equals("ab") && pracExp.ToLower().Equals("ab") && activity.ToLower().Equals("ab") && project.ToLower().Equals("ab") && unitTest.ToLower().Equals("ab") && selfStudy.ToLower().Equals("ab") && others.ToLower().Equals("ab"))
                {
                    totalTextBox.Text = "AB";
                }
                else
                {
                    totalTextBox.Text = total.ToString();
                }

                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }
                if (sumtotal.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(sumtotal)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && sumtotal.ToLower().Equals("ab"))
                {

                    grandtotalTextBox.Text = "AB";
                    gradeTextBox.Text = "AB";
                }
                else
                {

                    grandtotalTextBox.Text = grandtotal.ToString();
                    grade = findgrade(grandtotal);
                    gradeTextBox.Text = grade;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.PracExp_TextChanged", ex);
            }
        }

        protected void Activity_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0", grade = "";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox dailyObservationTextBox = (TextBox)row.FindControl("DailyObser");
                TextBox oralsTextBox = (TextBox)row.FindControl("Orals");
                TextBox pracExpTextBox = (TextBox)row.FindControl("PracExp");
                TextBox activityTextBox = (TextBox)row.FindControl("Activity");
                TextBox projectTextBox = (TextBox)row.FindControl("Project");
                TextBox unitTestTextBox = (TextBox)row.FindControl("Unittest");
                TextBox selfStudyTextBox = (TextBox)row.FindControl("Selfstudy");
                TextBox othersTextBox = (TextBox)row.FindControl("Others");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");

                TextBox sumtotalTextBox = (TextBox)row.FindControl("sumtotal");
                TextBox grandtotalTextBox = (TextBox)row.FindControl("GrandTotal");
                TextBox gradeTextBox = (TextBox)row.FindControl("Grade");

                row.FindControl("Activity").Focus();

                string dailyObservation = dailyObservationTextBox.Text;
                string orals = oralsTextBox.Text;
                string pracExp = pracExpTextBox.Text;
                string activity = activityTextBox.Text;
                string project = projectTextBox.Text;
                string unitTest = unitTestTextBox.Text;
                string selfStudy = selfStudyTextBox.Text;
                string others = othersTextBox.Text;

                string sumtotal = sumtotalTextBox.Text;

                if (dailyObservation.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dailyObservation)).ToString("00");
                }
                if (orals.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(orals)).ToString("00");
                }
                if (pracExp.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(pracExp)).ToString("00");
                }
                if (activity.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(activity)).ToString("00");
                }
                if (project.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(project)).ToString("00");
                }
                if (unitTest.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(unitTest)).ToString("00");
                }
                if (selfStudy.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(selfStudy)).ToString("00");
                }
                if (others.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(others)).ToString("00");
                }

                if (dailyObservation.ToLower().Equals("ab") && orals.ToLower().Equals("ab") && pracExp.ToLower().Equals("ab") && activity.ToLower().Equals("ab") && project.ToLower().Equals("ab") && unitTest.ToLower().Equals("ab") && selfStudy.ToLower().Equals("ab") && others.ToLower().Equals("ab"))
                {
                    totalTextBox.Text = "AB";
                }
                else
                {
                    totalTextBox.Text = total.ToString();
                }

                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }
                if (sumtotal.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(sumtotal)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && sumtotal.ToLower().Equals("ab"))
                {

                    grandtotalTextBox.Text = "AB";
                    gradeTextBox.Text = "AB";
                }
                else
                {

                    grandtotalTextBox.Text = grandtotal.ToString();
                    grade = findgrade(grandtotal);
                    gradeTextBox.Text = grade;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.Activity_TextChanged", ex);
            }
        }

        protected void Project_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0", grade = "";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox dailyObservationTextBox = (TextBox)row.FindControl("DailyObser");
                TextBox oralsTextBox = (TextBox)row.FindControl("Orals");
                TextBox pracExpTextBox = (TextBox)row.FindControl("PracExp");
                TextBox activityTextBox = (TextBox)row.FindControl("Activity");
                TextBox projectTextBox = (TextBox)row.FindControl("Project");
                TextBox unitTestTextBox = (TextBox)row.FindControl("Unittest");
                TextBox selfStudyTextBox = (TextBox)row.FindControl("Selfstudy");
                TextBox othersTextBox = (TextBox)row.FindControl("Others");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");

                TextBox sumtotalTextBox = (TextBox)row.FindControl("sumtotal");
                TextBox grandtotalTextBox = (TextBox)row.FindControl("GrandTotal");
                TextBox gradeTextBox = (TextBox)row.FindControl("Grade");

                row.FindControl("Project").Focus();

                string dailyObservation = dailyObservationTextBox.Text;
                string orals = oralsTextBox.Text;
                string pracExp = pracExpTextBox.Text;
                string activity = activityTextBox.Text;
                string project = projectTextBox.Text;
                string unitTest = unitTestTextBox.Text;
                string selfStudy = selfStudyTextBox.Text;
                string others = othersTextBox.Text;

                string sumtotal = sumtotalTextBox.Text;


                if (dailyObservation.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dailyObservation)).ToString("00");
                }
                if (orals.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(orals)).ToString("00");
                }
                if (pracExp.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(pracExp)).ToString("00");
                }
                if (activity.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(activity)).ToString("00");
                }
                if (project.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(project)).ToString("00");
                }
                if (unitTest.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(unitTest)).ToString("00");
                }
                if (selfStudy.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(selfStudy)).ToString("00");
                }
                if (others.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(others)).ToString("00");
                }

                if (dailyObservation.ToLower().Equals("ab") && orals.ToLower().Equals("ab") && pracExp.ToLower().Equals("ab") && activity.ToLower().Equals("ab") && project.ToLower().Equals("ab") && unitTest.ToLower().Equals("ab") && selfStudy.ToLower().Equals("ab") && others.ToLower().Equals("ab"))
                {
                    totalTextBox.Text = "AB";
                }
                else
                {
                    totalTextBox.Text = total.ToString();
                }


                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }
                if (sumtotal.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(sumtotal)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && sumtotal.ToLower().Equals("ab"))
                {

                    grandtotalTextBox.Text = "AB";
                    gradeTextBox.Text = "AB";
                }
                else
                {

                    grandtotalTextBox.Text = grandtotal.ToString();
                    grade = findgrade(grandtotal);
                    gradeTextBox.Text = grade;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.Project_TextChanged", ex);
            }
        }

        protected void Unittest_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0", grade = "";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox dailyObservationTextBox = (TextBox)row.FindControl("DailyObser");
                TextBox oralsTextBox = (TextBox)row.FindControl("Orals");
                TextBox pracExpTextBox = (TextBox)row.FindControl("PracExp");
                TextBox activityTextBox = (TextBox)row.FindControl("Activity");
                TextBox projectTextBox = (TextBox)row.FindControl("Project");
                TextBox unitTestTextBox = (TextBox)row.FindControl("Unittest");
                TextBox selfStudyTextBox = (TextBox)row.FindControl("Selfstudy");
                TextBox othersTextBox = (TextBox)row.FindControl("Others");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");

                TextBox sumtotalTextBox = (TextBox)row.FindControl("sumtotal");
                TextBox grandtotalTextBox = (TextBox)row.FindControl("GrandTotal");
                TextBox gradeTextBox = (TextBox)row.FindControl("Grade");

                row.FindControl("Unittest").Focus();

                string dailyObservation = dailyObservationTextBox.Text;
                string orals = oralsTextBox.Text;
                string pracExp = pracExpTextBox.Text;
                string activity = activityTextBox.Text;
                string project = projectTextBox.Text;
                string unitTest = unitTestTextBox.Text;
                string selfStudy = selfStudyTextBox.Text;
                string others = othersTextBox.Text;

                string sumtotal = sumtotalTextBox.Text;


                if (dailyObservation.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dailyObservation)).ToString("00");
                }
                if (orals.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(orals)).ToString("00");
                }
                if (pracExp.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(pracExp)).ToString("00");
                }
                if (activity.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(activity)).ToString("00");
                }
                if (project.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(project)).ToString("00");
                }
                if (unitTest.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(unitTest)).ToString("00");
                }
                if (selfStudy.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(selfStudy)).ToString("00");
                }
                if (others.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(others)).ToString("00");
                }

                if (dailyObservation.ToLower().Equals("ab") && orals.ToLower().Equals("ab") && pracExp.ToLower().Equals("ab") && activity.ToLower().Equals("ab") && project.ToLower().Equals("ab") && unitTest.ToLower().Equals("ab") && selfStudy.ToLower().Equals("ab") && others.ToLower().Equals("ab"))
                {
                    totalTextBox.Text = "AB";
                }
                else
                {
                    totalTextBox.Text = total.ToString();
                }

                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }
                if (sumtotal.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(sumtotal)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && sumtotal.ToLower().Equals("ab"))
                {

                    grandtotalTextBox.Text = "AB";
                    gradeTextBox.Text = "AB";
                }
                else
                {

                    grandtotalTextBox.Text = grandtotal.ToString();
                    grade = findgrade(grandtotal);
                    gradeTextBox.Text = grade;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.Unittest_TextChanged", ex);
            }
        }

        protected void Selfstudy_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0", grade = "";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox dailyObservationTextBox = (TextBox)row.FindControl("DailyObser");
                TextBox oralsTextBox = (TextBox)row.FindControl("Orals");
                TextBox pracExpTextBox = (TextBox)row.FindControl("PracExp");
                TextBox activityTextBox = (TextBox)row.FindControl("Activity");
                TextBox projectTextBox = (TextBox)row.FindControl("Project");
                TextBox unitTestTextBox = (TextBox)row.FindControl("Unittest");
                TextBox selfStudyTextBox = (TextBox)row.FindControl("Selfstudy");
                TextBox othersTextBox = (TextBox)row.FindControl("Others");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");


                TextBox sumtotalTextBox = (TextBox)row.FindControl("sumtotal");
                TextBox grandtotalTextBox = (TextBox)row.FindControl("GrandTotal");
                TextBox gradeTextBox = (TextBox)row.FindControl("Grade");


                row.FindControl("Selfstudy").Focus();

                string dailyObservation = dailyObservationTextBox.Text;
                string orals = oralsTextBox.Text;
                string pracExp = pracExpTextBox.Text;
                string activity = activityTextBox.Text;
                string project = projectTextBox.Text;
                string unitTest = unitTestTextBox.Text;
                string selfStudy = selfStudyTextBox.Text;
                string others = othersTextBox.Text;

                string sumtotal = sumtotalTextBox.Text;



                if (dailyObservation.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dailyObservation)).ToString("00");
                }
                if (orals.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(orals)).ToString("00");
                }
                if (pracExp.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(pracExp)).ToString("00");
                }
                if (activity.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(activity)).ToString("00");
                }
                if (project.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(project)).ToString("00");
                }
                if (unitTest.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(unitTest)).ToString("00");
                }
                if (selfStudy.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(selfStudy)).ToString("00");
                }
                if (others.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(others)).ToString("00");
                }

                if (dailyObservation.ToLower().Equals("ab") && orals.ToLower().Equals("ab") && pracExp.ToLower().Equals("ab") && activity.ToLower().Equals("ab") && project.ToLower().Equals("ab") && unitTest.ToLower().Equals("ab") && selfStudy.ToLower().Equals("ab") && others.ToLower().Equals("ab"))
                {
                    totalTextBox.Text = "AB";
                }
                else
                {
                    totalTextBox.Text = total.ToString();
                }

                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }
                if (sumtotal.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(sumtotal)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && sumtotal.ToLower().Equals("ab"))
                {

                    grandtotalTextBox.Text = "AB";
                    gradeTextBox.Text = "AB";
                }
                else
                {

                    grandtotalTextBox.Text = grandtotal.ToString();
                    grade = findgrade(grandtotal);
                    gradeTextBox.Text = grade;
                }

            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.Selfstudy_TextChanged", ex);
            }
        }

        protected void Others_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0", grade = "";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox dailyObservationTextBox = (TextBox)row.FindControl("DailyObser");
                TextBox oralsTextBox = (TextBox)row.FindControl("Orals");
                TextBox pracExpTextBox = (TextBox)row.FindControl("PracExp");
                TextBox activityTextBox = (TextBox)row.FindControl("Activity");
                TextBox projectTextBox = (TextBox)row.FindControl("Project");
                TextBox unitTestTextBox = (TextBox)row.FindControl("Unittest");
                TextBox selfStudyTextBox = (TextBox)row.FindControl("Selfstudy");
                TextBox othersTextBox = (TextBox)row.FindControl("Others");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");

                TextBox sumtotalTextBox = (TextBox)row.FindControl("sumtotal");
                TextBox grandtotalTextBox = (TextBox)row.FindControl("GrandTotal");
                TextBox gradeTextBox = (TextBox)row.FindControl("Grade");

                row.FindControl("Others").Focus();

                string dailyObservation = dailyObservationTextBox.Text;
                string orals = oralsTextBox.Text;
                string pracExp = pracExpTextBox.Text;
                string activity = activityTextBox.Text;
                string project = projectTextBox.Text;
                string unitTest = unitTestTextBox.Text;
                string selfStudy = selfStudyTextBox.Text;
                string others = othersTextBox.Text;

                string sumtotal = sumtotalTextBox.Text;

                if (dailyObservation.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dailyObservation)).ToString("00");
                }
                if (orals.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(orals)).ToString("00");
                }
                if (pracExp.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(pracExp)).ToString("00");
                }
                if (activity.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(activity)).ToString("00");
                }
                if (project.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(project)).ToString("00");
                }
                if (unitTest.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(unitTest)).ToString("00");
                }
                if (selfStudy.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(selfStudy)).ToString("00");
                }
                if (others.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(others)).ToString("00");
                }

                if (dailyObservation.ToLower().Equals("ab") && orals.ToLower().Equals("ab") && pracExp.ToLower().Equals("ab") && activity.ToLower().Equals("ab") && project.ToLower().Equals("ab") && unitTest.ToLower().Equals("ab") && selfStudy.ToLower().Equals("ab") && others.ToLower().Equals("ab"))
                {
                    totalTextBox.Text = "AB";
                }
                else
                {
                    totalTextBox.Text = total.ToString();
                }


                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }
                if (sumtotal.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(sumtotal)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && sumtotal.ToLower().Equals("ab"))
                {

                    grandtotalTextBox.Text = "AB";
                    gradeTextBox.Text = "AB";
                }
                else
                {

                    grandtotalTextBox.Text = grandtotal.ToString();
                    grade = findgrade(grandtotal);
                    gradeTextBox.Text = grade;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.Others_TextChanged", ex);
            }
        }


        protected void sumorals_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0", sumtotal = "0", grade = "";

            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox sumoralTextBox = (TextBox)row.FindControl("sumorals");
                TextBox sumwrittenTextBox = (TextBox)row.FindControl("sumwritten");
                TextBox sumtotalTextBox = (TextBox)row.FindControl("sumtotal");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");
                TextBox grandtotalTextBox = (TextBox)row.FindControl("GrandTotal");
                TextBox gradeTextBox = (TextBox)row.FindControl("Grade");


                row.FindControl("sumorals").Focus();

                string sumoral = sumoralTextBox.Text;
                string sumwriten = sumwrittenTextBox.Text;
                string formative_total = totalTextBox.Text;



                if (sumoral.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(sumoral)).ToString("00");
                }
                if (sumwriten.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(sumwriten)).ToString("00");
                }

                if (sumoral.ToLower().Equals("ab") && sumwriten.ToLower().Equals("ab"))
                {
                    sumtotal = "AB";
                    sumtotalTextBox.Text = "AB";
                }
                else
                {
                    sumtotal = total.ToString();
                    sumtotalTextBox.Text = total.ToString();
                }

                if (formative_total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(formative_total)).ToString("00");
                }
                if (sumtotal.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(sumtotal)).ToString("00");
                }

                if (formative_total.ToLower().Equals("ab") && sumtotal.ToLower().Equals("ab"))
                {

                    grandtotalTextBox.Text = "AB";
                    gradeTextBox.Text = "AB";
                }
                else
                {

                    grandtotalTextBox.Text = grandtotal.ToString();
                    grade = findgrade(grandtotal);
                    gradeTextBox.Text = grade;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.sumorals_TextChanged", ex);
            }
        }

        protected void sumwritten_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0", sumtotal = "0", grade = "";

            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox sumoralTextBox = (TextBox)row.FindControl("sumorals");
                TextBox sumwrittenTextBox = (TextBox)row.FindControl("sumwritten");
                TextBox sumtotalTextBox = (TextBox)row.FindControl("sumtotal");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");
                TextBox grandtotalTextBox = (TextBox)row.FindControl("GrandTotal");
                TextBox gradeTextBox = (TextBox)row.FindControl("Grade");

                row.FindControl("sumwritten").Focus();


                string sumoral = sumoralTextBox.Text;
                string sumwriten = sumwrittenTextBox.Text;
                string formative_total = totalTextBox.Text;



                if (sumoral.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(sumoral)).ToString("00");
                }
                if (sumwriten.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(sumwriten)).ToString("00");
                }

                if (sumoral.ToLower().Equals("ab") && sumwriten.ToLower().Equals("ab"))
                {
                    sumtotal = "AB";
                    sumtotalTextBox.Text = "AB";
                }
                else
                {
                    sumtotal = total.ToString();
                    sumtotalTextBox.Text = total.ToString();
                }

                if (formative_total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(formative_total)).ToString("00");
                }
                if (sumtotal.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(sumtotal)).ToString("00");
                }

                if (formative_total.ToLower().Equals("ab") && sumtotal.ToLower().Equals("ab"))
                {

                    grandtotalTextBox.Text = "AB";
                    gradeTextBox.Text = "AB";
                }
                else
                {

                    grandtotalTextBox.Text = grandtotal.ToString();
                    grade = findgrade(grandtotal);
                    gradeTextBox.Text = grade;
                }


            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.sumorals_TextChanged", ex);
            }
        }


        public string findgrade(string grandtotal)
        {
            SqlConnection con = new SqlConnection();
            try
            {
                string query = "", grade = "";


                using (con = Connection.getConnection())
                {
                    con.Open();
                    if (grandtotal != "AB")
                    {
                        query = "Select [grade] From GradeChart where " + grandtotal + " Between minmarks and maxmarks;";
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            grade = reader[0].ToString();

                        }
                        reader.Close();
                    }
                    else
                    {
                        grade = "AB";
                    }
                }
                return grade;
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.finalgrade", ex);
                return ex.Message;
            }
            finally
            {
                if (con != null) { con.Close(); }


            }
        }

        protected void SaveMarks_ServerClick(object sender, EventArgs e)
        {
            string usercode = lblusercode.Text;
            SqlConnection con = new SqlConnection();
            try
            {
                string query = ""; int count = 0;
                DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string select_std = cmbStd.SelectedValue.ToString();
                    string select_div = cmbDiv.SelectedValue.ToString();
                    string exam = cmbexam.SelectedValue.ToString();
                    string subject = cmbsubject.SelectedValue.ToString();
                    string year = cmbAcademicyear.SelectedValue.ToString();

                    foreach (GridViewRow row in GridCollection.Rows)
                    {
                        string rollno = row.Cells[0].Text;
                        string grno = row.Cells[1].Text;
                        string StudentName = row.Cells[2].Text;
                        string dailyObservationTextBox = ((TextBox)row.FindControl("DailyObser")).Text;
                        string oralsTextBox = ((TextBox)row.FindControl("Orals")).Text;
                        string pracExpTextBox = ((TextBox)row.FindControl("PracExp")).Text;
                        string activityTextBox = ((TextBox)row.FindControl("Activity")).Text;
                        string projectTextBox = ((TextBox)row.FindControl("Project")).Text;
                        string unitTestTextBox = ((TextBox)row.FindControl("Unittest")).Text;
                        string selfStudyTextBox = ((TextBox)row.FindControl("Selfstudy")).Text;
                        string othersTextBox = ((TextBox)row.FindControl("Others")).Text;
                        string totalTextBox = ((TextBox)row.FindControl("Total")).Text;
                        string sumoralTextBox = ((TextBox)row.FindControl("sumorals")).Text;
                        string sumwrittenTextBox = ((TextBox)row.FindControl("sumwritten")).Text;
                        string sumtotalTextBox = ((TextBox)row.FindControl("sumtotal")).Text;
                        string grandtotalTextBox = ((TextBox)row.FindControl("GrandTotal")).Text;
                        string gradeTextBox = ((TextBox)row.FindControl("Grade")).Text;

                        query = "Select Count(*) From Studentmarksheet where std='" + select_std + "' and div='" + select_div + "' and examname='" + exam + "' and subjectname='" + subject + "' and grno='" + grno + "' and academicyear='" + year + "';";
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            count = Convert.ToInt32(reader[0]);
                        }

                        if (count == 0)
                        {
                            query = "insert into StudentMarksheet (rollno,grno,studentname,std,div,Subjectname,Examname,[DailyObser],[Orals],[practicalexp],[activity],[project],[UnitTest],[Selfstudy],[Others],[Total],[summativeorals],[summativewritten],[summativetotal],[grandtotal],[finalgrade],[CreatedDate],[CreatedBy],[academicyear])" +
                                " values('" + rollno + "','" + grno + "','" + checkApostrophee(StudentName) + "','" + select_std + "','" + select_div + "','" + subject + "','" + exam + "','" + dailyObservationTextBox + "','" + oralsTextBox + "','" + pracExpTextBox + "','" + activityTextBox + "'" +
                            ",'" + projectTextBox + "','" + unitTestTextBox + "','" + selfStudyTextBox + "','" + othersTextBox + "','" + totalTextBox + "','" + sumoralTextBox + "','" + sumwrittenTextBox + "','" + sumtotalTextBox + "','" + grandtotalTextBox + "','" + gradeTextBox + "','" + cdt + "','" + usercode + "','" + year + "');";

                        }
                        else
                        {
                            query = "update StudentMarksheet set rollno='" + rollno + "',grno='" + grno + "',studentname='" + checkApostrophee(StudentName) + "',std='" + select_std + "',div='" + select_div + "',subjectname='" + subject + "',examname='" + exam + "'," +
                                "DailyObser='" + dailyObservationTextBox + "',[Orals]='" + oralsTextBox + "',[practicalexp]='" + pracExpTextBox + "',[activity]='" + activityTextBox + "',[project]='" + projectTextBox + "',[UnitTest]='" + unitTestTextBox + "',[Selfstudy]='" + selfStudyTextBox + "',[Others]='" + othersTextBox + "',[Total]='" + totalTextBox + "',[summativeorals]='" + sumoralTextBox + "',[summativewritten]='" + sumwrittenTextBox + "',[summativetotal]='" + sumtotalTextBox + "',[grandtotal]='" + grandtotalTextBox + "',[finalgrade]='" + gradeTextBox + "',[UpdatedDate]='" + cdt + "',[updatedBy]='" + usercode + "' " +
                                "where std='" + select_std + "' and div='" + select_div + "' and examname='" + exam + "' and subjectname='" + subject + "' and grno='" + grno + "' and academicyear='" + year + "';";

                        }
                        cmd = new SqlCommand(query, con);
                        // cmd.Parameters.AddWithValue("@studname", row.Cells[1].Value.ToString());
                        cmd.ExecuteNonQuery();

                        lblinfomsg.Text = "Subjects Marks Saved Successfully.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);

                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.SaveMarks_ServerClick", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        public static String checkApostrophee(String textname)
        {

            String text = "";
            char[] arrtext = textname.ToCharArray();
            for (int i = 0; i < arrtext.Length; i++)
            {
                if (arrtext[i].Equals('\''))
                {
                    text += "''";
                    //MessageBox.Show(text);
                }
                else
                {
                    text += arrtext[i];
                }

            }

            return text;
        }

        protected void Generate_excel_ServerClick(object sender, EventArgs e)
        {
            ExcelPackage excel = new ExcelPackage();
            SqlConnection con = new SqlConnection();
            try
            {
                List<string> subjectarr = new List<string>();

                Dictionary<string, DataTable> subjectdictionay = new Dictionary<string, DataTable>();

                string select_std = cmbStd.SelectedValue.ToString();
                string select_div = cmbDiv.SelectedValue.ToString();
                string exam = cmbexam.SelectedValue.ToString();

                string[] columnarr = new string[] { "Daily Observation", "Orals", "Practical/Experiments", "Activity", "Project", "Unit Test (Written)", "Self Study/Class Work", "Others", "Total @ 70", "Summative Orals @ 10", "Summative Written @ 20", "Summative Total @ 30", "Grand Total @ 100", "Final Grade" };
                using (con = Connection.getConnection())
                {
                    con.Open();

                    String query = "Select [subject] From SubjectMaster where std='" + select_std + "' and Examname='" + exam + "' order by srno desc;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        subjectarr.Add(reader[0].ToString());
                    }
                    reader.Close();

                    string folderpath = "/MarksheetModule/DownloadExcel/ ";
                    string abs_path = Server.MapPath(folderpath);

                    for (int k = subjectarr.Count - 1; k >= 0; k--)
                    {
                        DataTable subject = new DataTable();
                        subject = generateSubjectmarks(con, select_std, select_div, columnarr, exam, subjectarr[k], abs_path);
                        subjectdictionay.Add(subjectarr[k], subject);
                    }

                    DataTable attendance = new DataTable();
                    attendance = generateAttendance(con, select_std, select_div, abs_path, exam);
                    subjectdictionay.Add("Attendance", attendance);

                    DataTable remark = new DataTable();
                    remark = generateRemark(con, select_std, select_div, abs_path, exam);
                    subjectdictionay.Add("Remark", remark);

                    // generate excel using eppls

                    excel = GenerateExcelFromDataTable(subjectdictionay, select_std, select_div, exam, abs_path, columnarr);



                    using (var memoryStream = new MemoryStream())
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        //here i have set filname as Students.xlsx

                        Response.AddHeader("content-disposition", "attachment;  filename=" + select_std + "-" + select_div + "-" + exam + ".xlsx");
                        excel.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }


                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.Generate_excel_ServerClick", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        public DataTable generateSubjectmarks(SqlConnection con, string std, string div, string[] columnarr, string examname, string subjectname, string filepath)
        {
            DataTable dbtable = new DataTable();
            string year = cmbAcademicyear.SelectedValue.ToString();
            try
            {
                ExcelPackage excel = new ExcelPackage();

                DataTable maindbtable = new DataTable();



                String query = "select Cast(rollno as int) as Rollno,studentname as [StudentName],grno as [Grno],Dailyobser as [" + columnarr[0] + "],orals as [" + columnarr[1] + "],practicalexp as [" + columnarr[2] + "],activity as [" + columnarr[3] + "],project as [" + columnarr[4] + "],UnitTest as [" + columnarr[5] + "],Selfstudy as [" + columnarr[6] + "],others as [" + columnarr[7] + "],total as [" + columnarr[8] + "],summativeorals as [" + columnarr[9] + "],summativewritten as [" + columnarr[10] + "],summativetotal as [" + columnarr[11] + "],grandtotal as [" + columnarr[12] + "],finalgrade as [" + columnarr[13] + "],std,div,examname,subjectname  " +
                                "From StudentMarksheet where std='" + std + "' and div='" + div + "' and examname='" + examname + "' and Subjectname='" + subjectname + "' and academicyear='" + year + "' Order By Rollno ASC;";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(dbtable);
                if (dbtable.Rows.Count == 0)
                {
                    query = "Select Cast(rollno as int) as Rollno,(fname+' '+LNAME) as StudentName,grno From studentmaster where std='" + std + "' and div='" + div + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '') Order By Rollno ASC;";
                    cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dbtable.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", std, div, examname, subjectname);
                    }
                    reader.Close();
                }


                return dbtable;
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.generateSubjectmarks", ex);
                return dbtable;
            }

        }

        public DataTable generateAttendance(SqlConnection con, string std, string div, string filepath, string examname)
        {
            DataTable maindbtable = new DataTable();
            string year = cmbAcademicyear.SelectedValue.ToString();
            try
            {
                String query = "Select Cast(rollno as int) as Rollno,StudentName,Grno,June,July,Aug as [August],sep as [September],oct as [October],total1 as [Total],nov as [November],dec as [December],jan as [January],feb as [February],March,April,May,total2 as [Total],GTotal,std,div From Attendance where std='" + std + "' and div='" + div + "' and academicyear='" + year + "' Order By Rollno ASC;";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(maindbtable);
                if (maindbtable.Rows.Count == 0)
                {
                    query = "Select Cast(rollno as int) as Rollno,(fname+' '+LNAME) as StudentName,grno From studentmaster where std='" + std + "' and div='" + div + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '') Order By Rollno ASC;";
                    cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        maindbtable.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", std, div);
                    }
                    reader.Close();


                }

                return maindbtable;
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.generateAttendance", ex);
                return maindbtable;
            }

        }

        public DataTable generateRemark(SqlConnection con, string std, string div, string filepath, string examname)
        {
            DataTable maindbtable = new DataTable();
            string year = cmbAcademicyear.SelectedValue.ToString();
            try
            {
                String query = "Select Cast(rollno as int) as Rollno,Studentname,Grno,specialprog as [Special Progress],likinghob as [Liking & Hobby],needimprov as [Needs Improvement],Std,Div,Examname From Remark where std='" + std + "' and div='" + div + "' and Examname='" + examname + "' and academicyear='" + year + "' Order By Rollno ASC;";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(maindbtable);
                if (maindbtable.Rows.Count == 0)
                {
                    query = "Select Cast(rollno as int) as Rollno,(fname+' '+LNAME) as StudentName,grno From studentmaster where std='" + std + "' and div='" + div + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '') Order By Rollno ASC;";
                    cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        maindbtable.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), "-", "-", "-", std, div, examname);
                    }
                    reader.Close();


                }

                return maindbtable;
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.generateAttendance", ex);
                return maindbtable;
            }

        }

        public ExcelPackage GenerateExcelFromDataTable(Dictionary<string, DataTable> subject, string std, string div, string examname, string filepath, string[] columnarr)
        {
            try
            {
                ExcelPackage excel = new ExcelPackage();

                foreach (KeyValuePair<string, DataTable> subj in subject)
                {
                    string subjectname = subj.Key;
                    DataTable subjtabel = subj.Value;


                    var workSheet = excel.Workbook.Worksheets.Add(subjectname);

                    if (subjectname != "Attendance" && subjectname != "Remark")
                    {

                        int i = 2; int k = 1;
                        using (ExcelRange Rng = workSheet.Cells[1, 1, 1, 21])
                        {
                            //Rng.Value = "Text Color & Background Color";
                            //Rng.Merge = true;
                            Rng.Style.Font.Bold = true;
                            //Rng.Style.Font.Color.SetColor(Color.Red);
                            Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            Rng.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        }

                        for (int j = 0; j < subjtabel.Columns.Count; j++)
                        {
                            //Add row header in excel

                            workSheet.Cells[1, k].Value = subjtabel.Columns[j].ColumnName;
                            k++;

                        }
                        foreach (DataRow ro in subjtabel.Rows)
                        {
                            workSheet.Cells[i, 1].Value = ro["Rollno"].ToString();
                            workSheet.Cells[i, 2].Value = ro["StudentName"].ToString();
                            workSheet.Cells[i, 3].Value = ro["grno"].ToString();
                            workSheet.Cells[i, 4].Value = ro[columnarr[0]].ToString();
                            workSheet.Cells[i, 5].Value = ro[columnarr[1]].ToString();
                            workSheet.Cells[i, 6].Value = ro[columnarr[2]].ToString();
                            workSheet.Cells[i, 7].Value = ro[columnarr[3]].ToString();
                            workSheet.Cells[i, 8].Value = ro[columnarr[4]].ToString();
                            workSheet.Cells[i, 9].Value = ro[columnarr[5]].ToString();
                            workSheet.Cells[i, 10].Value = ro[columnarr[6]].ToString();
                            workSheet.Cells[i, 11].Value = ro[columnarr[7]].ToString();
                            workSheet.Cells[i, 12].Value = ro[columnarr[8]].ToString();
                            workSheet.Cells[i, 13].Value = ro[columnarr[9]].ToString();
                            workSheet.Cells[i, 14].Value = ro[columnarr[10]].ToString();
                            workSheet.Cells[i, 15].Value = ro[columnarr[11]].ToString();
                            workSheet.Cells[i, 16].Value = ro[columnarr[12]].ToString();
                            workSheet.Cells[i, 17].Value = ro[columnarr[13]].ToString();
                            workSheet.Cells[i, 18].Value = ro["std"].ToString();
                            workSheet.Cells[i, 19].Value = ro["div"].ToString();
                            workSheet.Cells[i, 20].Value = ro["examname"].ToString();
                            workSheet.Cells[i, 21].Value = ro["subjectname"].ToString();

                            i++;
                        }
                    }
                    else if (subjectname == "Attendance")
                    {
                        int i = 2; int k = 1;
                        using (ExcelRange Rng = workSheet.Cells[1, 1, 1, 20])
                        {
                            //Rng.Value = "Text Color & Background Color";
                            //Rng.Merge = true;
                            Rng.Style.Font.Bold = true;
                            //Rng.Style.Font.Color.SetColor(Color.Red);
                            Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            Rng.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        }

                        for (int j = 0; j < subjtabel.Columns.Count; j++)
                        {
                            //Add row header in excel

                            workSheet.Cells[1, k].Value = subjtabel.Columns[j].ColumnName;
                            k++;

                        }
                        foreach (DataRow ro in subjtabel.Rows)
                        {
                            workSheet.Cells[i, 1].Value = ro["Rollno"].ToString();
                            workSheet.Cells[i, 2].Value = ro["StudentName"].ToString();
                            workSheet.Cells[i, 3].Value = ro["grno"].ToString();
                            workSheet.Cells[i, 4].Value = ro["June"].ToString();
                            workSheet.Cells[i, 5].Value = ro["July"].ToString();
                            workSheet.Cells[i, 6].Value = ro["August"].ToString();
                            workSheet.Cells[i, 7].Value = ro["September"].ToString();
                            workSheet.Cells[i, 8].Value = ro["October"].ToString();
                            workSheet.Cells[i, 9].Value = ro["Total"].ToString();
                            workSheet.Cells[i, 10].Value = ro["November"].ToString();
                            workSheet.Cells[i, 11].Value = ro["December"].ToString();
                            workSheet.Cells[i, 12].Value = ro["January"].ToString();
                            workSheet.Cells[i, 13].Value = ro["February"].ToString();
                            workSheet.Cells[i, 14].Value = ro["March"].ToString();
                            workSheet.Cells[i, 15].Value = ro["April"].ToString();
                            workSheet.Cells[i, 16].Value = ro["May"].ToString();
                            workSheet.Cells[i, 17].Value = ro["Total"].ToString();
                            workSheet.Cells[i, 18].Value = ro["GTotal"].ToString();
                            workSheet.Cells[i, 19].Value = ro["std"].ToString();
                            workSheet.Cells[i, 20].Value = ro["div"].ToString();


                            i++;
                        }
                    }
                    else if (subjectname == "Remark")
                    {
                        int i = 2; int k = 1;
                        using (ExcelRange Rng = workSheet.Cells[1, 1, 1, 9])
                        {
                            //Rng.Value = "Text Color & Background Color";
                            //Rng.Merge = true;
                            Rng.Style.Font.Bold = true;
                            //Rng.Style.Font.Color.SetColor(Color.Red);
                            Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            Rng.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        }

                        for (int j = 0; j < subjtabel.Columns.Count; j++)
                        {
                            //Add row header in excel

                            workSheet.Cells[1, k].Value = subjtabel.Columns[j].ColumnName;
                            k++;

                        }
                        foreach (DataRow ro in subjtabel.Rows)
                        {
                            workSheet.Cells[i, 1].Value = ro["Rollno"].ToString();
                            workSheet.Cells[i, 2].Value = ro["StudentName"].ToString();
                            workSheet.Cells[i, 3].Value = ro["grno"].ToString();
                            workSheet.Cells[i, 4].Value = ro["Special Progress"].ToString();
                            workSheet.Cells[i, 5].Value = ro["Liking & Hobby"].ToString();
                            workSheet.Cells[i, 6].Value = ro["Needs Improvement"].ToString();
                            workSheet.Cells[i, 7].Value = ro["Std"].ToString();
                            workSheet.Cells[i, 8].Value = ro["Div"].ToString();
                            workSheet.Cells[i, 9].Value = ro["Examname"].ToString();
                            i++;
                        }
                    }

                }


                return excel;
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.GenerateExcelFromDataTable", ex);
                return null;
            }
        }

        protected void btnUploadExcel_ServerClick(object sender, EventArgs e)
        {
            try
            {

                if (FileUpload1.HasFile)
                {
                    string folderpath = Server.MapPath("~/MarksheetModule/Excel");
                    FileUpload1.SaveAs(folderpath + "//" + FileUpload1.FileName);

                    string filepath = folderpath + "//" + FileUpload1.FileName;

                    string resp = uploadMarksExcel(filepath);

                    if (resp == "ok")
                    {
                        lblinfomsg.Text = "Student Marks Excel Uploaded Successfully.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                    }
                    else
                    {
                        lblalertmessage.Text = resp;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                    }

                }
                else
                {
                    //Label1.Text = "No File Uploaded.";
                    lblalertmessage.Text = "No File Uploaded.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.btnUploadExcel_ServerClick", ex);
            }
        }

        public string uploadMarksExcel(String filepath)
        {

            Log.Info("uploadexcel process started");
            DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
            string usercode = lblusercode.Text;
            string academicyear = cmbAcademicyear.SelectedValue.ToString();
            SqlConnection con = null;
            string error_response = "";

            try
            {

                Boolean entry = true;
                if (filepath.Length > 0)
                {
                    ExcelPackage package = null;
                    using (package = new ExcelPackage(filepath))
                    {
                        using (con = Connection.getConnection())
                        {
                            con.Open();

                            int sheetCount = package.Workbook.Worksheets.Count;
                            for (int i = 0; i < sheetCount; i++)
                            {
                                string worksheetNames = "";
                                ExcelWorksheet worksheet = package.Workbook.Worksheets[i];

                                worksheetNames = worksheet.Name + ", ";

                                if (!string.IsNullOrEmpty(worksheetNames))
                                {
                                    worksheetNames = worksheetNames.TrimEnd(',', ' ');
                                }

                                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                                int rowCount = worksheet.Dimension.End.Row;     //get row count

                                if (worksheetNames == "Attendance")
                                {
                                    string subject = "", rollno = "", studentname = "", excelgrno = "", excelexam = "", excelstd = "", exceldiv = "", query = "";
                                    string[] marks = new string[20];


                                    for (int k = 2; k <= rowCount; k++)
                                    {
                                        if (string.IsNullOrEmpty(worksheet.Cells[k, 3].Value.ToString()))
                                            break;
                                        rollno = Convert.ToString(worksheet.Cells[k, 1].Value);
                                        studentname = Convert.ToString(worksheet.Cells[k, 2].Value);
                                        excelgrno = Convert.ToString(worksheet.Cells[k, 3].Value);

                                        marks[0] = Convert.ToString(worksheet.Cells[k, 4].Value);
                                        marks[1] = Convert.ToString(worksheet.Cells[k, 5].Value);
                                        marks[2] = Convert.ToString(worksheet.Cells[k, 6].Value);
                                        marks[3] = Convert.ToString(worksheet.Cells[k, 7].Value);
                                        marks[4] = Convert.ToString(worksheet.Cells[k, 8].Value);

                                        //total sem 1 attendance
                                        marks[5] = (Convert.ToInt32(marks[0]) + Convert.ToInt32(marks[1]) + Convert.ToInt32(marks[2]) + Convert.ToInt32(marks[3]) + Convert.ToInt32(marks[4])).ToString();

                                        marks[6] = Convert.ToString(worksheet.Cells[k, 10].Value);
                                        marks[7] = Convert.ToString(worksheet.Cells[k, 11].Value);
                                        marks[8] = Convert.ToString(worksheet.Cells[k, 12].Value);
                                        marks[9] = Convert.ToString(worksheet.Cells[k, 13].Value);
                                        marks[10] = Convert.ToString(worksheet.Cells[k, 14].Value);
                                        marks[11] = Convert.ToString(worksheet.Cells[k, 15].Value);
                                        marks[12] = Convert.ToString(worksheet.Cells[k, 16].Value);

                                        //total sem2 attendance
                                        marks[13] = (Convert.ToInt32(marks[6]) + Convert.ToInt32(marks[7]) + Convert.ToInt32(marks[8]) + Convert.ToInt32(marks[9]) + Convert.ToInt32(marks[10]) + Convert.ToInt32(marks[11]) + Convert.ToInt32(marks[12])).ToString();

                                        marks[14] = (Convert.ToInt32(marks[5]) + Convert.ToInt32(marks[13])).ToString();

                                        excelstd = Convert.ToString(worksheet.Cells[k, 19].Value);
                                        exceldiv = Convert.ToString(worksheet.Cells[k, 20].Value);

                                        int colnum = 0;

                                        for (int l = 0; l < 15; l++)
                                        {
                                            if (l == 0)
                                            {
                                                colnum = 4;
                                            }
                                            if (marks[l] == null || marks[l].Length == 0)
                                            {
                                                Log.Info("Empty value at " + k.ToString() + " Row " + colnum + " Column : " + worksheetNames);
                                                error_response += ("Empty value at " + k.ToString() + " Row " + colnum + " Column : " + worksheetNames);
                                            }

                                            colnum = colnum + 1;
                                        }

                                        int count = 0;
                                        query = "Select Count(*) From Attendance where std='" + excelstd + "' and div='" + exceldiv + "' and grno='" + excelgrno + "' and academicyear='" + academicyear + "';";
                                        SqlCommand cmd = new SqlCommand(query, con);
                                        SqlDataReader reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            count = Convert.ToInt32(reader[0]);
                                        }
                                        reader.Close();

                                        if (count == 0)
                                        {
                                            query = "insert into Attendance(rollno,grno,studentname,std,div,june,july,aug,sep,oct,total1,nov,dec,jan,feb,march,april,may,total2,gtotal,CreatedDate,CreatedBy,academicyear) "
                                            + "values('" + rollno + "','" + excelgrno + "','" + checkApostrophee(studentname) + "','" + excelstd + "','" + exceldiv + "'," +
                                            "'" + marks[0] + "','" + marks[1] + "','" + marks[2] + "','" + marks[3] + "','" + marks[4] + "'," +
                                            "'" + marks[5] + "','" + marks[6] + "','" + marks[7] + "','" + marks[8] + "','" + marks[9] + "','" + marks[10] + "'," +
                                            "'" + marks[11] + "','" + marks[12] + "','" + marks[13] + "','" + marks[14] + "','" + cdt + "','" + usercode + "','" + academicyear + "');";
                                        }
                                        else
                                        {
                                            query = "update Attendance set rollno='" + rollno + "',grno='" + excelgrno + "',studentname='" + checkApostrophee(studentname) + "',std='" + excelstd + "',div='" + exceldiv + "',june='" + marks[0] + "'" +
                                            ",july='" + marks[1] + "',aug='" + marks[2] + "',sep='" + marks[3] + "',oct='" + marks[4] + "',total1='" + marks[5] + "',nov='" + marks[6] + "',dec='" + marks[7] + "'" +
                                            ",jan='" + marks[8] + "',feb='" + marks[9] + "',march='" + marks[10] + "',april='" + marks[11] + "',may='" + marks[12] + "',total2='" + marks[13] + "',gtotal='" + marks[14] + "',updateddate='" + cdt + "',updatedby='" + usercode + "' " +
                                            " where std='" + excelstd + "' and grno='" + excelgrno + "' and academicyear='" + academicyear + "';";
                                        }

                                        cmd = new SqlCommand(query, con);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                else if (worksheetNames == "Remark")
                                {
                                    string subject = "", rollno = "", studentname = "", excelgrno = "", excelexam = "", excelstd = "", exceldiv = "", query = "";
                                    string[] marks = new string[20];


                                    for (int k = 2; k <= rowCount; k++)
                                    {
                                        if (string.IsNullOrEmpty(worksheet.Cells[k, 3].Value.ToString()))
                                            break;
                                        rollno = Convert.ToString(worksheet.Cells[k, 1].Value);
                                        studentname = Convert.ToString(worksheet.Cells[k, 2].Value);
                                        excelgrno = Convert.ToString(worksheet.Cells[k, 3].Value);



                                        marks[0] = Convert.ToString(worksheet.Cells[k, 4].Value);
                                        marks[1] = Convert.ToString(worksheet.Cells[k, 5].Value);
                                        marks[2] = Convert.ToString(worksheet.Cells[k, 6].Value);

                                        excelstd = Convert.ToString(worksheet.Cells[k, 7].Value);
                                        exceldiv = Convert.ToString(worksheet.Cells[k, 8].Value);
                                        excelexam = Convert.ToString(worksheet.Cells[k, 9].Value);

                                        int colnum = 0;

                                        for (int l = 0; l < 3; l++)
                                        {
                                            if (l == 0)
                                            {
                                                colnum = 4;
                                            }
                                            if (marks[l] == null || marks[l].Length == 0)
                                            {
                                                Log.Info("Empty value at " + k.ToString() + " Row " + colnum + " Column : " + worksheetNames);
                                                error_response += ("Empty value at " + k.ToString() + " Row " + colnum + " Column : " + worksheetNames);
                                            }

                                            colnum = colnum + 1;
                                        }


                                        query = "Delete From Remark where std='" + excelstd + "' and div='" + exceldiv + "' and examname='" + excelexam + "' and grno='" + excelgrno + "' and academicyear='" + academicyear + "';";
                                        SqlCommand cmd = new SqlCommand(query, con);
                                        cmd.ExecuteNonQuery();

                                        query = "insert into Remark(rollno,grno,studentname,std,div,examname,[specialprog],[likinghob],[needimprov],[CreatedDate],[CreatedBy],academicyear) " +
                                                 "values('" + rollno + "','" + excelgrno + "','" + checkApostrophee(studentname) + "','" + excelstd + "','" + exceldiv + "','" + excelexam + "','" + checkApostrophee(marks[0]) + "','" + checkApostrophee(marks[1]) + "','" + checkApostrophee(marks[2]) + "','" + cdt + "','" + usercode + "','" + academicyear + "');";
                                        cmd = new SqlCommand(query, con);
                                        cmd.ExecuteNonQuery();
                                    }

                                }
                                else
                                {
                                    string subject = "", rollno = "", studentname = "", excelgrno = "", excelexam = "", excelstd = "", exceldiv = "", query = "";

                                    for (int k = 2; k <= rowCount; k++)
                                    {
                                        string[] marks = new string[20];

                                        if (string.IsNullOrEmpty(worksheet.Cells[k, 3].Value.ToString()))
                                            break;



                                        rollno = Convert.ToString(worksheet.Cells[k, 1].Value);
                                        studentname = Convert.ToString(worksheet.Cells[k, 2].Value);
                                        excelgrno = Convert.ToString(worksheet.Cells[k, 3].Value);

                                        subject = Convert.ToString(worksheet.Cells[k, 21].Value);
                                        excelexam = Convert.ToString(worksheet.Cells[k, 20].Value);
                                        excelstd = Convert.ToString(worksheet.Cells[k, 18].Value);
                                        exceldiv = Convert.ToString(worksheet.Cells[k, 19].Value);

                                        marks[0] = Convert.ToString(worksheet.Cells[k, 4].Value);
                                        marks[1] = Convert.ToString(worksheet.Cells[k, 5].Value);
                                        marks[2] = Convert.ToString(worksheet.Cells[k, 6].Value);
                                        marks[3] = Convert.ToString(worksheet.Cells[k, 7].Value);
                                        marks[4] = Convert.ToString(worksheet.Cells[k, 8].Value);
                                        marks[5] = Convert.ToString(worksheet.Cells[k, 9].Value);
                                        marks[6] = Convert.ToString(worksheet.Cells[k, 10].Value);
                                        marks[7] = Convert.ToString(worksheet.Cells[k, 11].Value);

                                        //total of 70

                                        marks[8] = "0";

                                        if (marks[0].All(char.IsDigit))
                                        {
                                            marks[8] = (Convert.ToDouble(marks[8]) + Convert.ToDouble(marks[0])).ToString("00");
                                        }
                                        if (marks[1].All(char.IsDigit))
                                        {
                                            marks[8] = (Convert.ToDouble(marks[8]) + Convert.ToDouble(marks[1])).ToString("00");
                                        }
                                        if (marks[2].All(char.IsDigit))
                                        {
                                            marks[8] = (Convert.ToDouble(marks[8]) + Convert.ToDouble(marks[2])).ToString("00");
                                        }
                                        if (marks[3].All(char.IsDigit))
                                        {
                                            marks[8] = (Convert.ToDouble(marks[8]) + Convert.ToDouble(marks[3])).ToString("00");
                                        }
                                        if (marks[4].All(char.IsDigit))
                                        {
                                            marks[8] = (Convert.ToDouble(marks[8]) + Convert.ToDouble(marks[4])).ToString("00");
                                        }
                                        if (marks[5].All(char.IsDigit))
                                        {
                                            marks[8] = (Convert.ToDouble(marks[8]) + Convert.ToDouble(marks[5])).ToString("00");
                                        }
                                        if (marks[6].All(char.IsDigit))
                                        {
                                            marks[8] = (Convert.ToDouble(marks[8]) + Convert.ToDouble(marks[6])).ToString("00");
                                        }
                                        if (marks[7].All(char.IsDigit))
                                        {
                                            marks[8] = (Convert.ToDouble(marks[8]) + Convert.ToDouble(marks[7])).ToString("00");
                                        }

                                        if (marks[0].ToLower().Equals("ab") && marks[1].ToLower().Equals("ab") && marks[2].ToLower().Equals("ab") && marks[3].ToLower().Equals("ab") && marks[4].ToLower().Equals("ab") && marks[5].ToLower().Equals("ab") && marks[6].ToLower().Equals("ab") && marks[7].ToLower().Equals("ab"))
                                        {
                                            marks[8] = "AB";
                                        }


                                        marks[9] = Convert.ToString(worksheet.Cells[k, 13].Value);
                                        marks[10] = Convert.ToString(worksheet.Cells[k, 14].Value);


                                        // total 30
                                        marks[11] = "0";

                                        if (marks[9].All(char.IsDigit))
                                        {
                                            marks[11] = (Convert.ToDouble(marks[11]) + Convert.ToDouble(marks[9])).ToString("00");
                                        }
                                        if (marks[10].All(char.IsDigit))
                                        {
                                            marks[11] = (Convert.ToDouble(marks[11]) + Convert.ToDouble(marks[10])).ToString("00");
                                        }


                                        if (marks[9].ToLower().Equals("ab") && marks[10].ToLower().Equals("ab"))
                                        {
                                            marks[11] = "AB";
                                        }

                                        //total of 100
                                        marks[12] = "0";
                                        if (marks[8].All(char.IsDigit))
                                        {
                                            marks[12] = (Convert.ToDouble(marks[12]) + Convert.ToDouble(marks[8])).ToString();
                                        }
                                        if (marks[11].All(char.IsDigit))
                                        {
                                            marks[12] = (Convert.ToDouble(marks[12]) + Convert.ToDouble(marks[11])).ToString();
                                        }

                                        if (marks[8].ToLower().Equals("ab") && marks[11].ToLower().Equals("ab"))
                                        {
                                            marks[12] = "AB";
                                        }

                                        //FinalGrade            
                                        SqlCommand cmd = null;
                                        SqlDataReader reader = null;
                                        marks[13] = Convert.ToString(worksheet.Cells[k, 17].Value);

                                        if (marks[12] != "AB")
                                        {
                                            query = "Select [grade] From GradeChart where " + marks[12] + " Between minmarks and maxmarks;";
                                            cmd = new SqlCommand(query, con);
                                            reader = cmd.ExecuteReader();
                                            while (reader.Read())
                                            {
                                                marks[13] = reader[0].ToString();

                                            }
                                            reader.Close();
                                        }
                                        else
                                        {
                                            marks[13] = "AB";
                                        }

                                        int colnum = 0;

                                        for (int l = 0; l < 14; l++)
                                        {
                                            if (l == 0)
                                            {
                                                colnum = 4;
                                            }
                                            if (marks[l] == null || marks[l].Length == 0)
                                            {
                                                Log.Info("Empty value at " + k.ToString() + " Row " + colnum + " Column : " + worksheetNames);
                                                error_response += ("Empty value at " + k.ToString() + " Row " + colnum + " Column : " + worksheetNames);
                                            }


                                            colnum = colnum + 1;
                                        }

                                        int count = 0;

                                        query = "Select Count(*) From studentmarksheet where std='" + excelstd + "' and div='" + exceldiv + "' and examname='" + excelexam + "' and subjectname='" + subject + "' and grno='" + excelgrno + "' and academicyear='" + academicyear + "';";
                                        cmd = new SqlCommand(query, con);
                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            count = Convert.ToInt32(reader[0]);
                                        }
                                        reader.Close();

                                        if (count == 0)
                                        {
                                            query = "insert into  studentmarksheet (rollno,grno,studentname,std,div,Subjectname,Examname,[DailyObser],[Orals],[practicalexp],[activity],[project],[UnitTest],[Selfstudy],[Others],[Total],[summativeorals],[summativewritten],[summativetotal],[grandtotal],[finalgrade],[Createddate],[CreatedBy],academicyear)" +
                                                " values('" + rollno + "','" + excelgrno + "','" + checkApostrophee(studentname) + "','" + excelstd + "','" + exceldiv + "','" + subject + "','" + excelexam + "','" + marks[0] + "','" + marks[1] + "','" + marks[2] + "','" + marks[3] + "'" +
                                            ",'" + marks[4] + "','" + marks[5] + "','" + marks[6] + "','" + marks[7] + "','" + marks[8] + "','" + marks[9] + "','" + marks[10] + "','" + marks[11] + "','" + marks[12] + "','" + marks[13] + "','" + cdt + "','" + usercode + "','" + academicyear + "');";

                                        }
                                        else
                                        {
                                            query = "update  studentmarksheet set rollno='" + rollno + "',grno='" + excelgrno + "',studentname='" + checkApostrophee(studentname) + "',std='" + excelstd + "',div='" + exceldiv + "',subjectname='" + subject + "',examname='" + excelexam + "'," +
                                                "DailyObser='" + marks[0] + "',[Orals]='" + marks[1] + "',[practicalexp]='" + marks[2] + "',[activity]='" + marks[3] + "',[project]='" + marks[4] + "',[UnitTest]='" + marks[5] + "',[Selfstudy]='" + marks[6] + "',[Others]='" + marks[7] + "',[Total]='" + marks[8] + "',[summativeorals]='" + marks[9] + "',[summativewritten]='" + marks[10] + "',[summativetotal]='" + marks[11] + "',[grandtotal]='" + marks[12] + "',[finalgrade]='" + marks[13] + "',[updateddate]='" + cdt + "',[updatedby]='" + usercode + "' " +
                                                "where std='" + excelstd + "' and div='" + exceldiv + "' and examname='" + excelexam + "' and subjectname='" + subject + "' and grno='" + excelgrno + "' and academicyear='" + academicyear + "';";

                                        }
                                        cmd = new SqlCommand(query, con);
                                        // cmd.Parameters.AddWithValue("@studname", row.Cells[1].Value.ToString());
                                        cmd.ExecuteNonQuery();

                                    }
                                }

                            }
                        }
                    }

                }
                Log.Info("uploadStudent process completed");
                if (error_response.Length == 0)
                    return "ok";
                else
                    return error_response;
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.uploadMarksExcel", ex);
                return ex.Message;

            }
            finally
            {
                if (con != null) { con.Close(); }
            }

        }

        protected void cmbAcademicyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                DataTable studtable = new DataTable();
                using (con = Connection.getConnection())
                {
                    con.Open();

                    string query = "", select_std = "", select_div = "", academicyear = "";

                    select_std = cmbStd.SelectedValue.ToString();
                    select_div = cmbDiv.SelectedValue.ToString();
                    academicyear = cmbAcademicyear.SelectedValue.ToString();

                    if (select_std != "Select Std" && select_div != "Select Div")
                    {
                        query = "select grno,fullname,(grno +' / '+ fullname) as stuname from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '');";
                        SqlDataAdapter adap = new SqlDataAdapter(query, con);
                        adap.Fill(studtable);

                        studtable.Rows.Add("ALL", "ALL", "ALL");
                        cmbstudentname.DataSource = studtable;
                        cmbstudentname.DataBind();
                        cmbstudentname.DataTextField = "stuname";
                        cmbstudentname.DataValueField = "grno";
                        cmbstudentname.DataBind();
                        cmbstudentname.SelectedValue = "ALL";

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.cmbDiv_SelectedIndexChanged", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void cmbDiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                DataTable studtable = new DataTable();
                using (con = Connection.getConnection())
                {
                    con.Open();

                    string query = "", select_std = "", select_div = "", academicyear = "";

                    select_std = cmbStd.SelectedValue.ToString();
                    select_div = cmbDiv.SelectedValue.ToString();
                    academicyear = cmbAcademicyear.SelectedValue.ToString();

                    if (select_std != "Select Std" && select_div != "Select Div")
                    {
                        query = "select grno,fullname,(grno +' / '+ fullname) as stuname from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '');";
                        SqlDataAdapter adap = new SqlDataAdapter(query, con);
                        adap.Fill(studtable);

                        studtable.Rows.Add("ALL", "ALL", "ALL");
                        cmbstudentname.DataSource = studtable;
                        cmbstudentname.DataBind();
                        cmbstudentname.DataTextField = "stuname";
                        cmbstudentname.DataValueField = "grno";
                        cmbstudentname.DataBind();
                        cmbstudentname.SelectedValue = "ALL";

                    }




                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.cmbDiv_SelectedIndexChanged", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }




    }
}