using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.MarksheetModule.DataSet.ds1to2;
using CenturyRayonSchool.MarksheetModule.Reports;
using CenturyRayonSchool.Model;
using CrystalDecisions.Shared;
using SCANiD_Marksheet.DataSet.ds10;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using attendance = SCANiD_Marksheet.DataSet.ds1to2.attendance;

namespace CenturyRayonSchool.MarksheetModule
{
    public partial class PrintMarksheet : System.Web.UI.Page
    {
        DataTable studetails = new DataTable();
        Label lblusercode = new Label();
        string std_sess = "", div_sess = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblusercode = (Label)Page.Master.FindControl("lblusercode");

            //cmbAcademicyear.SelectedValue = year;
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
                string year = new FeesModel().setActiveAcademicYear();
                lblacademicyear.Text = year;
                //cmbAcademicyear.Text = year;
                loadFormControl(year);
                Session["ReopenDate"] = "";
                Session["Resultdate"] = "";
            }

            if (std_sess == "IX" || std_sess == "IX (Mar)" || std_sess == "IX (Hindi)" || std_sess == "X" || std_sess == "X (Mar)" || std_sess == "X (Hindi)" || std_sess == "V" || std_sess == "V (SE)" || std_sess == "VIII" || std_sess == "VIII (Mar)" || std_sess == "VIII (Hindi)" || std_sess == "VIII (SE)")
            {
                Chk9th.Visible = true;
            }
            else
            {
                Chk9th.Visible = false;
            }
            studetails.Columns.Add("Rollno");
            studetails.Columns.Add("StudentName");
            studetails.Columns.Add("grno");
            studetails.Columns.Add("std");
            studetails.Columns.Add("div");
            studetails.Columns.Add("examname");
            studetails.Columns.Add("academicyear");
        }

        public void loadFormControl(string year)
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
                    adap = new SqlDataAdapter(query, con);
                    DataTable examamaster = new DataTable();
                    adap.Fill(examamaster);
                    examamaster.Rows.Add("Select Exam");
                    cmbexam.DataSource = examamaster;
                    cmbexam.DataBind();
                    cmbexam.DataTextField = "Examname";
                    cmbexam.DataValueField = "Examname";
                    cmbexam.DataBind();
                    cmbexam.SelectedValue = "Select Exam";


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

                    cmbAcademicyear.SelectedValue = year;



                    fillstulist(std_sess, div_sess);
                }
            }
            catch (Exception ex)
            {
                Log.Error("PrintMarksheet.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
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

        protected void checkALL_CheckedChanged(object sender, EventArgs e)
        {
            if (checkALL.Checked == true)
            {
                foreach (GridViewRow row in GridCollection.Rows)
                {
                    ((CheckBox)row.FindControl("chkSelect")).Checked = true;
                }
            }
            else
            {
                foreach (GridViewRow row in GridCollection.Rows)
                {
                    ((CheckBox)row.FindControl("chkSelect")).Checked = false;
                }
            }
        }
        protected void FetchData_ServerClick(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "", select_std = "", select_div = "", examname = "", studentname = "";

                    select_std = cmbStd.SelectedValue.ToString();
                    select_div = cmbDiv.SelectedValue.ToString();
                    examname = cmbexam.SelectedValue.ToString();
                    string year = cmbAcademicyear.SelectedValue.ToString();
                    studentname = cmbstudentname.SelectedValue.ToString();


                    if (studentname.Equals("ALL"))
                    {
                        query = "select Rollno,(fname+' '+LNAME) as StudentName,grno,std,div,Academicyear,admissiontype from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '')   order by Cast(ROLLNO as int) asc;";
                    }
                    else
                    {
                        query = "select Rollno,(fname+' '+LNAME) as StudentName,grno,std,div,Academicyear,admissiontype from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + year + "' and grno='" + studentname + "' and (leftstatus IS NULL OR leftstatus = '') order by Cast(ROLLNO as int) asc;";
                    }
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        studetails.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), examname, reader[5].ToString());
                    }

                    GridCollection.DataSource = studetails;
                    GridCollection.DataBind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Error("PrintMarksheet.FetchData_ServerClick", ex);
            }

        }

        public string getDownloadUrl(string grno, string div, string std, string examname, string year)
        {

            return "/MarksheetModule/DownloadFile.aspx?action=MarksheetPrint&grno=" + grno + "&div=" + div + "&std=" + std + "&examname=" + examname + "&academicyear=" + year;
        }
        public string getDownloadUrlNew(string grno, string div, string std, string examname, string year)
        {
            return "/MarksheetModule/DownloadFile.aspx?action=MarksheetPrint9&grno=" + grno + "&div=" + div + "&std=" + std + "&examname=" + examname + "&academicyear=" + year;
        }
        public string getDownloadUrlALl(string grno, string div, string std, string examname)
        {
            return "/MarksheetModule/DownloadFile.aspx?action=MarksheetPrintAll&grno=" + grno + "&div=" + div + "&std=" + std + "&examname=" + examname;
        }
        public Marksheet1to4 PrintMarksheetPrimary(SqlConnection con, string grno, string std, string div, string examname, string openingdate, string resultdate, string year)
        {
            try
            {
                //string year = lblacademicyear.Text.ToString();
                SqlCommand cmd = null;
                studentdataset studds = new studentdataset();
                attendance attds = new attendance();
                Dictionary<string, GradeValues> dict = new Dictionary<string, GradeValues>();
                List<string> exams = new System.Collections.Generic.List<string>();
                String[] subjparms = new String[11] { "sub1", "sub2", "sub3", "sub4", "sub5", "sub6", "sub7", "sub8", "sub9", "sub10", "sub11" };
                String[] term1subj = new String[11] { "term1grad1", "term1grade2", "term1grade3", "term1grade4", "term1grade5", "term1grade6", "term1grade7", "term1grade8", "term1grade9", "term1grade10", "term1grade11" };
                String[] term2subj = new String[11] { "term2grade1", "term2grade2", "term2grade3", "term2grade4", "term2grade5", "term2grade6", "term2grade7", "term2grade8", "term2grade9", "term2grade10", "term2grade11" };
                Dictionary<string, Remarks> remarksdict = new System.Collections.Generic.Dictionary<string, Remarks>();
                String[] special = new String[] { "specterm1", "specterm2" };
                String[] liking = new String[] { "likingterm1", "likingterm2" };
                String[] improve = new String[] { "improvterm1", "improvterm2" };
                Dictionary<string, int> monthsdict = new System.Collections.Generic.Dictionary<string, int>();

                Marksheet1to4 mkrep = new Marksheet1to4();

                String query = "";
                //year= new FeesModel().setActiveAcademicYear();
                //year = cmbAcademicyear.SelectedValue.ToString();

                query = "Select grno,Format(Cast(dob as date),'dd/MM/yyyy') as dob,rollno,std,div,fname as Name,mname as Fathername,lname as Surname,MOTHERNAME,saralid,aadharcard as uid,photopath From studentmaster where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '');";

                cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(studds.Tables[0]);

                GradeValues gradevalues = null;



                query = "Select  [srno],[Subject] From subjectmaster where std='" + std + "' and examname='" + examname + "' order by cast(srno as int);";
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    gradevalues = new GradeValues();

                    dict.Add(reader[1].ToString(), gradevalues);
                }
                reader.Close();

                query = "Select [Examname] From ExamMaster where std='" + std + "' order by Examorder;";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    exams.Add(reader[0].ToString());
                }
                reader.Close();

                foreach (KeyValuePair<string, GradeValues> keyval in dict)
                {
                    GradeValues grad = new GradeValues();
                    for (int i = 0; i < exams.Count; i++)
                    {

                        query = "Select finalgrade From studentmarksheet where std='" + std + "' and div='" + div + "' and examname='" + exams[i] + "' and grno='" + grno + "' and subjectname='" + keyval.Key + "' and academicyear='" + year + "';";
                        cmd = new SqlCommand(query, con);
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (i == 0)
                                {
                                    grad.Gradeterm1 = reader[0].ToString();
                                }
                                else if (i == 1)
                                {
                                    grad.Gradeterm2 = reader[0].ToString();
                                }
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                grad.Gradeterm1 = "";
                            }
                            else if (i == 1)
                            {
                                grad.Gradeterm2 = "";
                            }
                        }
                        reader.Close();

                        //keyval.Value.Gradeterm1 = grad.Gradeterm1;
                    }

                    keyval.Value.Gradeterm1 = grad.Gradeterm1;

                    if (examname != "First Semester")
                    {
                        keyval.Value.Gradeterm2 = grad.Gradeterm2;
                    }
                    else
                    {
                        keyval.Value.Gradeterm2 = "";
                    }
                }
                for (int i = 0; i < exams.Count; i++)
                {
                    Remarks rem = new Remarks();
                    query = "Select Examname,specialprog,likinghob,needimprov From Remark where std='" + std + "' and div='" + div + "' and Examname='" + exams[i] + "' and grno='" + grno + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rem.special = reader[1].ToString();
                            rem.liking = reader[2].ToString();
                            rem.needsimprov = reader[3].ToString();
                        }
                    }
                    else
                    {
                        rem.special = "";
                        rem.liking = "";
                        rem.needsimprov = "";
                    }

                    if (i == 0)
                    {
                        remarksdict.Add(exams[0], rem);
                    }
                    else
                    {
                        if (examname != "First Semester")
                        {
                            remarksdict.Add(exams[1], rem);
                        }
                        else
                        {
                            remarksdict.Add(exams[1], new Remarks() { special = "", liking = "", needsimprov = "" });
                        }
                    }
                }
                // Load month-wise working days
                // Load month-wise working days
                query = "Select [month],[totaldays] From Workingdays where std='" + std + "';";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    monthsdict.Add(reader[0].ToString(), Convert.ToInt32(reader[1]));
                }
                reader.Close();

                // Calculate total days
                int totaldays1 = monthsdict["Jun"] + monthsdict["Jul"] + monthsdict["Aug"] + monthsdict["Sep"] + monthsdict["Oct"];
                int totaldays2 = monthsdict["Nov"] + monthsdict["Dec"] + monthsdict["Jan"] + monthsdict["Feb"] + monthsdict["Mar"] + monthsdict["Apr"];
                int grandtotal = totaldays1 + totaldays2;

                // Add 'Days' row
                if (examname == "First Semester")
                {
                    attds.Tables[0].Rows.Add("Days",
                        monthsdict["Jun"],
                        monthsdict["Jul"],
                        monthsdict["Aug"],
                        monthsdict["Sep"],
                        monthsdict["Oct"],
                        totaldays1, "", "", "", "", "", "", "", "");
                }
                else
                {
                    attds.Tables[0].Rows.Add("Days",
                        monthsdict["Jun"],
                        monthsdict["Jul"],
                        monthsdict["Aug"],
                        monthsdict["Sep"],
                        monthsdict["Oct"],
                        totaldays1,
                        monthsdict["Nov"],
                        monthsdict["Dec"],
                        monthsdict["Jan"],
                        monthsdict["Feb"],
                        monthsdict["Mar"],
                        monthsdict["Apr"],
                        totaldays2,
                        grandtotal,
                        monthsdict["May"]);
                }

                // Fetch attendance from DB
                query = "Select june,july,aug,sep,oct,total1,nov,dec,jan,feb,march,april,total2,gtotal,may From Attendance where std='" + std + "' and grno='" + grno + "' and academicyear='" + year + "';";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();

                int[] present = new int[15]; // Store present values for Absent calculation

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < 15; i++)
                            present[i] = Convert.ToInt32(reader[i]);

                        if (examname == "First Semester")
                        {
                            attds.Tables[0].Rows.Add("Present",
                                reader[0].ToString(),
                                reader[1].ToString(),
                                reader[2].ToString(),
                                reader[3].ToString(),
                                reader[4].ToString(),
                                reader[5].ToString(), "", "", "", "", "", "", "", "");
                        }
                        else
                        {
                            // Manually calculate Total2 = Nov to Apr
                            int total2 = present[6] + present[7] + present[8] + present[9] + present[10] + present[11];

                            attds.Tables[0].Rows.Add("Present",
                                reader[0].ToString(),  // Jun
                                reader[1].ToString(),  // Jul
                                reader[2].ToString(),  // Aug
                                reader[3].ToString(),  // Sep
                                reader[4].ToString(),  // Oct
                                reader[5].ToString(),  // Total1
                                reader[6].ToString(),  // Nov
                                reader[7].ToString(),  // Dec
                                reader[8].ToString(),  // Jan
                                reader[9].ToString(),  // Feb
                                reader[10].ToString(), // Mar
                                reader[11].ToString(), // Apr
                                total2.ToString(),     // ✅ Corrected Total2
                                reader[13].ToString(), // GrandTotal
                                reader[14].ToString()  // May
                            );
                        }
                    }
                }
                else
                {
                    if (examname == "First Semester")
                    {
                        attds.Tables[0].Rows.Add("Present", "0", "0", "0", "0", "0", "0", "", "", "", "", "", "", "", "");
                    }
                    else
                    {
                        attds.Tables[0].Rows.Add("Present", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    }
                }

                // Add 'Absent' row (Days - Present)
                //if (examname != "First Semester")
                //{
                //    attds.Tables[0].Rows.Add("Absent",
                //        (monthsdict["Jun"] - present[0]).ToString(),
                //        (monthsdict["Jul"] - present[1]).ToString(),
                //        (monthsdict["Aug"] - present[2]).ToString(),
                //        (monthsdict["Sep"] - present[3]).ToString(),
                //        (monthsdict["Oct"] - present[4]).ToString(),
                //        (totaldays1 - present[5]).ToString(),
                //        (monthsdict["Nov"] - present[6]).ToString(),
                //        (monthsdict["Dec"] - present[7]).ToString(),
                //        (monthsdict["Jan"] - present[8]).ToString(),
                //        (monthsdict["Feb"] - present[9]).ToString(),
                //        (monthsdict["Mar"] - present[10]).ToString(),
                //        (monthsdict["Apr"] - present[11]).ToString(),
                //        (totaldays2 - present[12]).ToString(),
                //        (grandtotal - present[13]).ToString(),
                //        (monthsdict["May"] - present[14]).ToString()
                //    );
                //}
                //else
                //{
                //    attds.Tables[0].Rows.Add("Absent", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                //}


                string gradechart = "Grade Chart : ";
                query = "Select Grade,minmarks,maxmarks From gradechart;";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    gradechart += reader[0].ToString() + "(" + reader[1].ToString() + " to " + reader[2].ToString() + ") ";
                }
                reader.Close();

                string hmname = "", teachername = "", newstd = "", newdiv = "";

                //query = "Select [teachername] From TeachersDetails where designation='HM';";
                //cmd = new SqlCommand(query, con);
                //reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    hmname = reader[0].ToString();
                //}

                //reader.Close();

                query = "Select [teachername] From TeacherMapping where std='" + std + "' and div='" + div + "';";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    teachername = reader[0].ToString();
                }

                //reader.Close();

                //query = "Select newstd,newdiv From promotion where std='" + std + "' and div='" + div + "' and grno='" + grno + "';";
                //cmd = new SqlCommand(query, con);
                //reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    newstd = reader[0].ToString();
                //    newdiv = reader[1].ToString();
                //}

                //reader.Close();



                //query = "select year,schoolreopen,resultday1 From academicyear;";
                //cmd = new SqlCommand(query, con);
                //reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    academicyear = reader[0].ToString();
                //    reopen = reader[1].ToString();
                //    resultday = reader[2].ToString();
                //}

                //reader.Close();
                string schoolname = "";

                if (std == "I" || std == "II" || std == "III" || std == "IV" || std == "I (SE)" || std == "II (SE)" || std == "III (SE)" || std == "IV (SE)")
                {
                    schoolname = "CENTURY RAYON PRIMARY SCHOOL , SHAHAD";
                }
                else
                {
                    schoolname = "CENTURY RAYON High SCHOOL , SHAHAD";
                }

                //con.Close();

                //setting datasource values

                mkrep.SetDataSource(studds.Tables[0]);
                //mkrep.Subreports["studentrep.rpt"].SetDataSource(studds.Tables[1]);
                mkrep.Subreports["attendance.rpt"].SetDataSource(attds.Tables[0]);


                //setting parameter values
                int j = 0;
                foreach (KeyValuePair<string, GradeValues> keyval in dict)
                {
                    mkrep.SetParameterValue(subjparms[j], keyval.Key);
                    mkrep.SetParameterValue(term1subj[j], keyval.Value.Gradeterm1);
                    mkrep.SetParameterValue(term2subj[j], keyval.Value.Gradeterm2);
                    j++;
                }

                for (int k = j; k < 11; k++)
                {
                    mkrep.SetParameterValue(subjparms[k], "");
                    mkrep.SetParameterValue(term1subj[k], "");
                    mkrep.SetParameterValue(term2subj[k], "");
                }
                j = 0;
                foreach (KeyValuePair<string, Remarks> keyval in remarksdict)
                {


                    mkrep.SetParameterValue(special[j], keyval.Value.special);
                    mkrep.SetParameterValue(liking[j], keyval.Value.liking);
                    mkrep.SetParameterValue(improve[j], keyval.Value.needsimprov);

                    j++;
                }
                mkrep.SetParameterValue("gradechart", gradechart);

                //mkrep.SetParameterValue("hmname", "");
                mkrep.SetParameterValue("trname", teachername);
                mkrep.SetParameterValue("schoolname", schoolname);
                mkrep.SetParameterValue("std", "");
                mkrep.SetParameterValue("div", "");
                mkrep.SetParameterValue("academicyear", year);
                mkrep.SetParameterValue("openingdate", openingdate);
                mkrep.SetParameterValue("resultday", resultdate);


                return mkrep;
            }

            catch (Exception ex)
            {
                Log.Error("PrintMarksheet.PrintMarksheetPrimary", ex);
                return null;
            }
        }

        public Marksheet1to4Sem1 PrintMarksheetPrimarySem1(SqlConnection con, string grno, string std, string div, string examname, string resultdate, string year)
        {
            try
            {
                //string year = lblacademicyear.Text.ToString();
                SqlCommand cmd = null;
                studentdataset studds = new studentdataset();

                attendance attds = new attendance();
                Dictionary<string, GradeValues> dict = new Dictionary<string, GradeValues>();
                List<string> exams = new System.Collections.Generic.List<string>();
                String[] subjparms = new String[11] { "sub1", "sub2", "sub3", "sub4", "sub5", "sub6", "sub7", "sub8", "sub9", "sub10", "sub11" };
                String[] term1subj = new String[11] { "term1grad1", "term1grade2", "term1grade3", "term1grade4", "term1grade5", "term1grade6", "term1grade7", "term1grade8", "term1grade9", "term1grade10", "term1grade11" };
                // String[] term2subj = new String[11] { "term2grade1", "term2grade2", "term2grade3", "term2grade4", "term2grade5", "term2grade6", "term2grade7", "term2grade8", "term2grade9", "term2grade10", "term2grade11" };
                Dictionary<string, Remarks> remarksdict = new System.Collections.Generic.Dictionary<string, Remarks>();
                String[] special = new String[] { "specterm1" };
                String[] liking = new String[] { "likingterm1" };
                String[] improve = new String[] { "improvterm1" };
                Dictionary<string, int> monthsdict = new System.Collections.Generic.Dictionary<string, int>();

                Marksheet1to4Sem1 mkrep = new Marksheet1to4Sem1();

                String query = "";
                // year = new FeesModel().setActiveAcademicYear();

                query = "Select grno,Format(Cast(dob as date),'dd/MM/yyyy') as dob,rollno,std,div,fname as Name,mname as Fathername,lname as Surname,MOTHERNAME,saralid,aadharcard as uid,photopath From studentmaster where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '');";

                cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);

                GradeValues gradevalues = null;

                query = "Select [srno],[Subject] From subjectmaster where std='" + std + "' and examname='" + examname + "' order by cast(srno as int);";
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    gradevalues = new GradeValues();

                    dict.Add(reader[1].ToString(), gradevalues);
                }
                reader.Close();

                query = "Select [Examname] From ExamMaster where std='" + std + "' order by Examorder;";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    exams.Add(reader[0].ToString());
                }
                reader.Close();

                foreach (KeyValuePair<string, GradeValues> keyval in dict)
                {
                    GradeValues grad = new GradeValues();
                    for (int i = 0; i < exams.Count; i++)
                    {

                        query = "Select finalgrade From studentmarksheet where std='" + std + "' and div='" + div + "' and examname='" + exams[i] + "' and grno='" + grno + "' and subjectname='" + keyval.Key + "' and academicyear='" + year + "';";
                        cmd = new SqlCommand(query, con);
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (i == 0)
                                {
                                    grad.Gradeterm1 = reader[0].ToString();
                                }

                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                grad.Gradeterm1 = "";
                            }

                        }
                        reader.Close();

                        //keyval.Value.Gradeterm1 = grad.Gradeterm1;



                    }

                    keyval.Value.Gradeterm1 = grad.Gradeterm1;

                    //if (examname != "First Semester")
                    //{
                    //    keyval.Value.Gradeterm2 = grad.Gradeterm2;
                    //}
                    //else
                    //{
                    //    keyval.Value.Gradeterm2 = "";
                    //}
                }

                for (int i = 0; i < exams.Count; i++)
                {
                    Remarks rem = new Remarks();
                    query = "Select Examname,specialprog,likinghob,needimprov From Remark where std='" + std + "' and div='" + div + "' and Examname='First Semester' and grno='" + grno + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rem.special = reader[1].ToString();
                            rem.liking = reader[2].ToString();
                            rem.needsimprov = reader[3].ToString();
                            break;
                        }
                    }
                    else
                    {
                        rem.special = "";
                        rem.liking = "";
                        rem.needsimprov = "";
                    }

                    if (i == 0)
                    {
                        remarksdict.Add(exams[0], rem);
                    }
                    else
                    {
                        //if (examname != "First Semester")
                        //{
                        //    remarksdict.Add(exams[1], rem);
                        //}
                        //else
                        //{
                        remarksdict.Add(exams[1], new Remarks() { special = "", liking = "", needsimprov = "" });
                        //}
                    }


                }

                query = "Select [month],[totaldays] From Workingdays where std='" + std + "';";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    monthsdict.Add(reader[0].ToString(), Convert.ToInt32(reader[1]));
                }
                reader.Close();

                int totaldays1 = 0, totaldays2 = 0, grandtotal = 0;
                totaldays1 = monthsdict["Jun"] + monthsdict["Jul"] + monthsdict["Aug"] + monthsdict["Sep"] + monthsdict["Oct"];
                //totaldays2 = monthsdict["Nov"] + monthsdict["Dec"] + monthsdict["Jan"] + monthsdict["Feb"] + monthsdict["Mar"] + monthsdict["Apr"] + monthsdict["May"];
                // grandtotal = totaldays1 + totaldays2;

                if (examname == "First Semester")
                {
                    attds.Tables[0].Rows.Add("Days", monthsdict["Jun"], monthsdict["Jul"], monthsdict["Aug"], monthsdict["Sep"], monthsdict["Oct"], totaldays1);
                }
                //else
                //{
                //    attds.Tables[0].Rows.Add("Days", monthsdict["Jun"], monthsdict["Jul"], monthsdict["Aug"], monthsdict["Sep"], monthsdict["Oct"], totaldays1, monthsdict["Nov"], monthsdict["Dec"], monthsdict["Jan"], monthsdict["Feb"], monthsdict["Mar"], monthsdict["Apr"], totaldays2, grandtotal, monthsdict["May"]);
                //}

                query = "Select june,july,aug,sep,oct,total1 From Attendance where std='" + std + "' and grno='" + grno + "' and academicyear='" + year + "';";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (examname == "First Semester")
                        {
                            attds.Tables[0].Rows.Add("Present", reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
                        }
                        //else
                        //{
                        //    attds.Tables[0].Rows.Add("Present", reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString());
                        //}
                    }
                }
                else
                {
                    if (examname == "First Semester")
                    {
                        attds.Tables[0].Rows.Add("Present", "0", "0", "0", "0", "0", "0");
                    }
                    //else
                    //{
                    //    attds.Tables[0].Rows.Add("Present", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                    //}
                }

                attds.Tables[0].Rows.Add("Absent", "", "", "", "", "", "");


                //calculate absent
                for (int i = 1; i < attds.Tables[0].Columns.Count; i++)
                {
                    int k1 = 0, k2 = 0;
                    if (attds.Tables[0].Rows[0][i].ToString().Length > 0)
                    {
                        k1 = Convert.ToInt32(attds.Tables[0].Rows[0][i]);
                    }
                    if (attds.Tables[0].Rows[1][i].ToString().Length > 0)
                    {
                        k2 = Convert.ToInt32(attds.Tables[0].Rows[1][i]);
                    }

                    if (examname.Equals("First Semester"))
                    {
                        if (i < 7)
                        {
                            attds.Tables[0].Rows[2][i] = k1 - k2;
                        }
                    }
                    else
                    {
                        attds.Tables[0].Rows[2][i] = k1 - k2;
                    }

                }

                string gradechart = "Grade Chart : ";
                query = "Select Grade,minmarks,maxmarks From gradechart;";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    gradechart += reader[0].ToString() + "(" + reader[1].ToString() + " to " + reader[2].ToString() + ") ";
                }
                reader.Close();

                string hmname = "", teachername = "", newstd = "", newdiv = "";

                //query = "Select [teachername] From TeachersDetails where designation='HM';";
                //cmd = new SqlCommand(query, con);
                //reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    hmname = reader[0].ToString();
                //}

                //reader.Close();

                query = "Select [teachername] From TeacherMapping where std='" + std + "' and div='" + div + "';";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    teachername = reader[0].ToString();
                }

                //reader.Close();

                //query = "Select newstd,newdiv From promotion where std='" + std + "' and div='" + div + "' and grno='" + grno + "';";
                //cmd = new SqlCommand(query, con);
                //reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    newstd = reader[0].ToString();
                //    newdiv = reader[1].ToString();
                //}

                //reader.Close();



                //query = "select year,schoolreopen,resultday1 From academicyear;";
                //cmd = new SqlCommand(query, con);
                //reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    academicyear = reader[0].ToString();
                //    reopen = reader[1].ToString();
                //    resultday = reader[2].ToString();
                //}

                //reader.Close();

                string wrkjun = "0", wrkjul = "0", wrkaug = "0", wrksep = "0", wrkoct = "0", wrknov = "0", wrkdec = "0", wrkjan = "0", wrkfeb = "0", wrkmar = "0", wrkapr = "0", prejun = "0", prejul = "0", preaug = "0", presep = "0", preoct = "0", prenov = "0", predec = "0", prejan = "0", prefeb = "0", preapr = "0", premar = "0";
                query = "select month, std, TotalDays from workingdays where std='" + std + "';";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    switch (reader["month"].ToString())
                    {
                        case "Jan":
                            wrkjan = reader["TotalDays"].ToString();
                            break;

                        case "Feb":
                            wrkfeb = reader["TotalDays"].ToString();
                            break;

                        case "Mar":
                            wrkmar = reader["TotalDays"].ToString();
                            break;

                        case "Apr":
                            wrkapr = reader["TotalDays"].ToString();
                            break;
                        case "Jun":
                            wrkjun = reader["TotalDays"].ToString();
                            break;
                        case "Jul":
                            wrkjul = reader["TotalDays"].ToString();
                            break;
                        case "Aug":
                            wrkaug = reader["TotalDays"].ToString();
                            break;
                        case "Sep":
                            wrksep = reader["TotalDays"].ToString();
                            break;
                        case "Oct":
                            wrkoct = reader["TotalDays"].ToString();
                            break;
                        case "Nov":
                            wrknov = reader["TotalDays"].ToString();
                            break;
                        case "Dec":
                            wrkdec = reader["TotalDays"].ToString();
                            break;
                    }

                    //wrkjan = reader["jan"].ToString();

                }
                reader.Close();

                query = "select std,june,july,aug,sep,oct,nov,dec,jan,feb,march,april from Attendance where std='" + std + "' and grno='" + grno + "';";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    prejan = reader["jan"].ToString();
                    prefeb = reader["Feb"].ToString();
                    premar = reader["march"].ToString();
                    preapr = reader["april"].ToString();
                    prejun = reader["june"].ToString();
                    prejul = reader["july"].ToString();
                    preaug = reader["aug"].ToString();
                    presep = reader["sep"].ToString();
                    preoct = reader["oct"].ToString();
                    prenov = reader["nov"].ToString();
                    predec = reader["dec"].ToString();
                }
                string total = "0", presenttotal = "0";


                if (wrkjun.All(char.IsDigit))
                {
                    total = (Convert.ToInt32(total) + Convert.ToInt32(wrkjun)).ToString();
                }
                if (wrkjul.All(char.IsDigit))
                {
                    total = (Convert.ToInt32(total) + Convert.ToInt32(wrkjul)).ToString();
                }
                if (wrkaug.All(char.IsDigit))
                {
                    total = (Convert.ToInt32(total) + Convert.ToInt32(wrkaug)).ToString();
                }
                if (wrksep.All(char.IsDigit))
                {
                    total = (Convert.ToInt32(total) + Convert.ToInt32(wrksep)).ToString();
                }
                if (wrkoct.All(char.IsDigit))
                {
                    total = (Convert.ToInt32(total) + Convert.ToInt32(wrkoct)).ToString();
                }

                if (prejun.All(char.IsDigit))
                {
                    presenttotal = (Convert.ToInt32(presenttotal) + Convert.ToInt32(prejun)).ToString();
                }
                if (prejul.All(char.IsDigit))
                {
                    presenttotal = (Convert.ToInt32(presenttotal) + Convert.ToInt32(prejul)).ToString();
                }
                if (preaug.All(char.IsDigit))
                {
                    presenttotal = (Convert.ToInt32(presenttotal) + Convert.ToInt32(preaug)).ToString();
                }
                if (presep.All(char.IsDigit))
                {
                    presenttotal = (Convert.ToInt32(presenttotal) + Convert.ToInt32(presep)).ToString();
                }
                if (preoct.All(char.IsDigit))
                {
                    presenttotal = (Convert.ToInt32(presenttotal) + Convert.ToInt32(preoct)).ToString();
                }

                string schoolname = "";

                if (std == "I" || std == "II" || std == "III" || std == "IV" || std == "I (SE)" || std == "II (SE)" || std == "III (SE)" || std == "IV (SE)")
                {
                    schoolname = "CENTURY RAYON PRIMARY SCHOOL , SHAHAD";
                }
                else
                {
                    schoolname = "CENTURY RAYON HIGH SCHOOL , SHAHAD";
                }

                //con.Close();

                //setting datasource values

                //  mkrep.SetDataSource(studds.Tables[0]);
                //mkrep.Subreports["studentrep.rpt"].SetDataSource(studds.Tables[1]);
                //mkrep.Subreports["attendanceSem1.rpt"].SetDataSource(attds.Tables[0]);

                //    mkrep.Subreports["attendanceSem1.rpt"].Database.Tables["attendance"].SetDataSource(attds.Tables["attendance"]); 
                //setting parameter values
                int j = 0;
                foreach (KeyValuePair<string, GradeValues> keyval in dict)
                {
                    mkrep.SetParameterValue(subjparms[j], keyval.Key);
                    mkrep.SetParameterValue(term1subj[j], keyval.Value.Gradeterm1);
                    //mkrep.SetParameterValue(term2subj[j], keyval.Value.Gradeterm2);
                    j++;
                }

                for (int k = j; k < 11; k++)
                {
                    mkrep.SetParameterValue(subjparms[k], "");
                    mkrep.SetParameterValue(term1subj[k], "");
                    // mkrep.SetParameterValue(term2subj[k], "");
                }
                j = 0;
                foreach (KeyValuePair<string, Remarks> keyval in remarksdict)
                {


                    mkrep.SetParameterValue(special[j], keyval.Value.special);
                    mkrep.SetParameterValue(liking[j], keyval.Value.liking);
                    mkrep.SetParameterValue(improve[j], keyval.Value.needsimprov);
                    break;

                    //s j++;
                }

                mkrep.SetParameterValue("grno", dt.Rows[0]["grno"].ToString());
                mkrep.SetParameterValue("dob", dt.Rows[0]["dob"].ToString());
                mkrep.SetParameterValue("std", dt.Rows[0]["std"].ToString());
                mkrep.SetParameterValue("div", dt.Rows[0]["div"].ToString());
                mkrep.SetParameterValue("rollno", dt.Rows[0]["rollno"].ToString());
                mkrep.SetParameterValue("uid", dt.Rows[0]["uid"].ToString());
                mkrep.SetParameterValue("name", dt.Rows[0]["Name"].ToString());
                mkrep.SetParameterValue("fname", dt.Rows[0]["FatherName"].ToString());
                mkrep.SetParameterValue("lname", dt.Rows[0]["Surname"].ToString());
                mkrep.SetParameterValue("mothername", dt.Rows[0]["MotherName"].ToString());
                mkrep.SetParameterValue("mothername", dt.Rows[0]["MOTHERNAME"].ToString());
                mkrep.SetParameterValue("saralid", dt.Rows[0]["saralid"].ToString());
                mkrep.SetParameterValue("photopath", dt.Rows[0]["photopath"].ToString());

                mkrep.SetParameterValue("wrkjun", wrkjun);
                mkrep.SetParameterValue("wrkjul", wrkjul);
                mkrep.SetParameterValue("wrkaug", wrkaug);
                mkrep.SetParameterValue("wrksep", wrksep);
                mkrep.SetParameterValue("wrkoct", wrkoct);
                mkrep.SetParameterValue("prejun", prejun);
                mkrep.SetParameterValue("prejul", prejul);
                mkrep.SetParameterValue("preaug", preaug);
                mkrep.SetParameterValue("presep", presep);
                mkrep.SetParameterValue("preoct", preoct);

                mkrep.SetParameterValue("wrktotal", total);
                mkrep.SetParameterValue("presenttotal", presenttotal);

                mkrep.SetParameterValue("gradechart", gradechart);

                //mkrep.SetParameterValue("hmname", "");
                mkrep.SetParameterValue("trname", teachername);
                mkrep.SetParameterValue("schoolname", schoolname);
                // mkrep.SetParameterValue("std", "");
                // mkrep.SetParameterValue("div", "");
                mkrep.SetParameterValue("academicyear", year);
                // mkrep.SetParameterValue("openingdate", "");
                mkrep.SetParameterValue("resultday", resultdate);


                return mkrep;
            }

            catch (Exception ex)
            {


                Log.Error("PrintMarksheet.PrintMarksheetPrimarySem1", ex);
                return null;
            }
        }


        public ExamReportStd8to9 PrintMarksheet8to9(SqlConnection con, string grno, string std, string div, string examname, string openingdate, string year)
        {
            try
            {
                SqlCommand cmd = null;
                SqlDataReader reader = null;
                Dataset10 ds10 = new Dataset10();

                List<string> subjects = new List<string>();


                ExamReportStd8to9 mkrep = new ExamReportStd8to9();

                String query = "", teachername = "";
                //year = new FeesModel().setActiveAcademicYear();
                query = "Select grno,Format(Cast(dob as date),'dd/MM/yyyy') as dob,rollno,std,div,fname as Name,mname as Fathername,lname as Surname,MOTHERNAME,saralid,aadharcard as uid,photopath From studentmaster where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '');";

                cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(ds10.Tables["studentds"]);

                SemesterClassStd9 semvalues = new SemesterClassStd9();

                query = "select Distinct Cast([srno] as int) as [srno],[Subject] From subjectmaster where std='" + std + "' and examname='First Semester' order by srno;";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    subjects.Add(reader[1].ToString());
                }
                reader.Close();

                foreach (String str in subjects)
                {
                    // 1st sem

                    query = "select Written,oralsprac,total From StudentMarksheet8TO9 where examname='First Semester' and subjectname='" + str + "' and grno='" + grno + "' and std='" + std + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (str.Equals(SubjectClass.eng))
                        {

                            semvalues.writeng = reader[0].ToString();
                            semvalues.oraleng = reader[1].ToString();
                        }
                        else if (str.Equals(SubjectClass.mar))
                        {
                            semvalues.writmar = reader[0].ToString();
                            semvalues.oralmar = reader[1].ToString();
                        }
                        else if (str.Equals(SubjectClass.hindi) || str.Equals(SubjectClass.hind))
                        {
                            semvalues.writhindi = reader[0].ToString();
                            semvalues.oralhindi = reader[1].ToString();


                        }
                        else if (str.Equals(SubjectClass.maths))
                        {
                            semvalues.writmath = reader[0].ToString();
                            semvalues.oralmath = reader[1].ToString();
                        }
                        else if (str.Equals(SubjectClass.sci))
                        {
                            semvalues.writsci = reader[0].ToString();
                            semvalues.oralsci = reader[1].ToString();
                        }
                        else if (str.Equals(SubjectClass.socsci) || str.Equals(SubjectClass.ss))
                        {
                            semvalues.writss = reader[0].ToString();
                            semvalues.oralss = reader[1].ToString();

                        }
                        else if (str.Equals(SubjectClass.art))
                        {
                            semvalues.writart = reader[2].ToString();
                            semvalues.oralart = reader[1].ToString();
                        }
                        else if (str.Equals(SubjectClass.js))
                        {
                            semvalues.writjs = reader[2].ToString();
                            semvalues.oraljs = reader[1].ToString();

                        }
                        else if (str.Equals(SubjectClass.pe))
                        {
                            semvalues.writpe = reader[2].ToString();
                            semvalues.oralpe = reader[1].ToString();

                        }
                        else if (str.Equals(SubjectClass.computer))
                        {
                            semvalues.writcomp = reader[2].ToString();
                            semvalues.oralcomp = reader[1].ToString();

                        }


                    }
                    reader.Close();


                }



                query = "select Distinct Cast([srno] as int) as [srno],[Subject] From subjectmaster where std='" + std + "' and examname='Second Semester' order by srno;";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    subjects.Add(reader[1].ToString());
                }
                reader.Close();

                foreach (String str in subjects)
                {


                    // 2nd sem

                    query = "select Written,oralsprac,total From StudentMarksheet8TO9 where examname='Second Semester' and subjectname='" + str + "' and grno='" + grno + "' and std='" + std + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (str.Equals(SubjectClass.eng))
                        {

                            semvalues.writsem2eng = reader[0].ToString();
                            semvalues.oralsem2eng = reader[1].ToString();


                        }
                        else if (str.Equals(SubjectClass.mar))
                        {
                            semvalues.writsem2mar = reader[0].ToString();
                            semvalues.oralsem2mar = reader[1].ToString();


                        }
                        else if (str.Equals(SubjectClass.hindi) || str.Equals(SubjectClass.hind))
                        {
                            semvalues.writsem2hindi = reader[0].ToString();
                            semvalues.oralsem2hindi = reader[1].ToString();


                        }
                        else if (str.Equals(SubjectClass.maths))
                        {
                            semvalues.writsem2math = reader[0].ToString();
                            semvalues.oralsem2math = reader[1].ToString();
                        }
                        else if (str.Equals(SubjectClass.sci))
                        {
                            semvalues.writsem2sci = reader[0].ToString();
                            semvalues.oralsem2sci = reader[1].ToString();
                        }
                        else if (str.Equals(SubjectClass.socsci) || str.Equals(SubjectClass.ss))
                        {
                            semvalues.writsem2ss = reader[0].ToString();
                            semvalues.oralsem2ss = reader[1].ToString();

                        }
                        else if (str.Equals(SubjectClass.art))
                        {
                            semvalues.writsem2art = reader[2].ToString();
                            semvalues.oralsem2art = reader[1].ToString();
                        }
                        else if (str.Equals(SubjectClass.js))
                        {
                            semvalues.writsem2js = reader[2].ToString();
                            semvalues.oralsem2js = reader[1].ToString();

                        }
                        else if (str.Equals(SubjectClass.pe))
                        {
                            semvalues.writsem2pe = reader[2].ToString();
                            semvalues.oralsem2pe = reader[1].ToString();

                        }
                        else if (str.Equals(SubjectClass.computer))
                        {
                            semvalues.writsem2comp = reader[2].ToString();
                            semvalues.oralsem2comp = reader[1].ToString();

                        }


                    }
                    reader.Close();
                }

                string sematt = "0", totalsematt = "0";

                if (examname == "First Semester")
                {
                    query = "select total1 From Attendance where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        sematt = reader[0].ToString();

                    }
                    reader.Close();

                    //For Semester I
                    query = "select SUM(Cast(totaldays as int)) From workingdays where std='" + std + "' and [month] IN('Jun','Jul','Aug','Sep','Oct');";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        totalsematt = reader[0].ToString();
                    }
                    reader.Close();
                }
                else if (examname == "Second Semester")
                {
                    query = "select gtotal From Attendance where std='" + std + "' and div='" + div + "' and grno='" + grno + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        sematt = reader[0].ToString();
                    }
                    reader.Close();

                    //For Semester II
                    query = "select SUM(Cast(totaldays as int)) From workingdays where std='" + std + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        totalsematt = reader[0].ToString();
                    }
                    reader.Close();
                }



                //remarks

                query = "select [application],conduct,remark From Remark where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and examname='" + examname + "' and academicyear='" + year + "';";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    semvalues.application = reader[0].ToString();
                    semvalues.conduct = reader[1].ToString();
                    semvalues.remark = reader[2].ToString();
                }
                reader.Close();




                teachername = "";
                query = "select teachername From TeacherMapping where std='" + std + "' and div='" + div + "';";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    teachername = reader[0].ToString();
                }
                reader.Close();

                //con.Close();


                mkrep.SetDataSource(ds10.Tables["studentds"]);
                mkrep.SetParameterValue("academicyear", year);
                //mkrep.SetParameterValue("examname", examname);
                mkrep.SetParameterValue("teachername", teachername);
                mkrep.SetParameterValue("resultday", openingdate);

                // Semester Exam
                mkrep.SetParameterValue("writeng", semvalues.writeng);
                mkrep.SetParameterValue("writmar", semvalues.writmar);
                mkrep.SetParameterValue("writhindi", semvalues.writhindi);
                mkrep.SetParameterValue("writmath", semvalues.writmath);
                mkrep.SetParameterValue("writsci", semvalues.writsci);
                mkrep.SetParameterValue("writss", semvalues.writss);
                mkrep.SetParameterValue("writart", semvalues.writart);
                mkrep.SetParameterValue("writpe", semvalues.writpe);
                mkrep.SetParameterValue("writcomp", semvalues.writcomp);
                mkrep.SetParameterValue("writjs", semvalues.writjs);

                mkrep.SetParameterValue("oraleng", semvalues.oraleng);
                mkrep.SetParameterValue("oralmar", semvalues.oralmar);
                mkrep.SetParameterValue("oralhindi", semvalues.oralhindi);
                mkrep.SetParameterValue("oralmath", semvalues.oralmath);
                mkrep.SetParameterValue("oralsci", semvalues.oralsci);
                mkrep.SetParameterValue("oralss", semvalues.oralss);
                //mkrep.SetParameterValue("oralart", semvalues.oralart);
                //mkrep.SetParameterValue("oralpe", semvalues.oralpe);
                //mkrep.SetParameterValue("oralcomp", semvalues.oralcomp);
                //mkrep.SetParameterValue("oraljs", semvalues.oraljs);


                mkrep.SetParameterValue("writsem2eng", semvalues.writsem2eng);
                mkrep.SetParameterValue("writsem2mar", semvalues.writsem2mar);
                mkrep.SetParameterValue("writsem2hindi", semvalues.writsem2hindi);
                mkrep.SetParameterValue("writsem2math", semvalues.writsem2math);
                mkrep.SetParameterValue("writsem2sci", semvalues.writsem2sci);
                mkrep.SetParameterValue("writsem2ss", semvalues.writsem2ss);
                mkrep.SetParameterValue("writsem2art", semvalues.writsem2art);
                mkrep.SetParameterValue("writsem2pe", semvalues.writsem2pe);
                mkrep.SetParameterValue("writsem2comp", semvalues.writsem2comp);
                mkrep.SetParameterValue("writsem2js", semvalues.writsem2js);

                mkrep.SetParameterValue("oralsem2eng", semvalues.oralsem2eng);
                mkrep.SetParameterValue("oralsem2mar", semvalues.oralsem2mar);
                mkrep.SetParameterValue("oralsem2hindi", semvalues.oralsem2hindi);
                mkrep.SetParameterValue("oralsem2math", semvalues.oralsem2math);
                mkrep.SetParameterValue("oralsem2sci", semvalues.oralsem2sci);
                mkrep.SetParameterValue("oralsem2ss", semvalues.oralsem2ss);
                //mkrep.SetParameterValue("oralsem2art", semvalues.oralsem2art);
                //mkrep.SetParameterValue("oralsem2pe", semvalues.oralsem2pe);
                //mkrep.SetParameterValue("oralsem2comp", semvalues.oralsem2comp);
                //mkrep.SetParameterValue("oralsem2js", semvalues.oralsem2js);


                mkrep.SetParameterValue("present", sematt);
                mkrep.SetParameterValue("totalatt", totalsematt);





                //remarks
                mkrep.SetParameterValue("application", semvalues.application);
                mkrep.SetParameterValue("conduct", semvalues.conduct);
                mkrep.SetParameterValue("remark", semvalues.remark);

                return mkrep;
            }
            catch (Exception ex)
            {
                Log.Error("PrintMarksheet.PrintMarksheet8to9", ex);
                return null;
            }

        }


        public ExamReportStd8to9Sem1 PrintMarksheet8to9Sem1(SqlConnection con, string grno, string std, string div, string examname, string resultday, string year)
        {
            try
            {
                int failedSubjects = 0;
                SqlCommand cmd = null;
                SqlDataReader reader = null;
                Dataset10 ds10 = new Dataset10();

                List<string> subjects = new List<string>();
                ExamReportStd8to9Sem1 mkrep = new ExamReportStd8to9Sem1();

                String query = "", teachername = "";
                //year = new FeesModel().setActiveAcademicYear();
                query = "Select grno,Format(Cast(dob as date),'dd/MM/yyyy') as dob,rollno,std,div,fname as Name,mname as Fathername,lname as Surname,MOTHERNAME,saralid,aadharcard as uid,photopath,address,mobile as contact From studentmaster where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '');";
                cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(ds10.Tables["studentds"]);

                SemesterClassStd9 semvalues = new SemesterClassStd9();

                query = "select Distinct Cast([srno] as int) as [srno],[Subject] From subjectmaster where std='" + std + "' and examname='First Semester' order by srno;";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    subjects.Add(reader[1].ToString());
                }
                reader.Close();
                failedSubjects = 0;
                foreach (String str in subjects)
                {
                    // 1st sem

                    query = "select Written,oralsprac,total From StudentMarksheet8TO9 where examname='First Semester' and subjectname='" + str + "' and grno='" + grno + "' and std='" + std + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string marksString = reader["total"].ToString();
                        int marks;
                        if (int.TryParse(marksString, out marks))
                        {
                            if (marks < 28)
                            {
                                failedSubjects++;

                                if (str.Equals(SubjectClass.eng))
                                {
                                    semvalues.writeng = reader[0].ToString();
                                    semvalues.feng = "*";
                                    //semvalues.oraleng = reader[1].ToString();
                                }
                                else if (str.Equals(SubjectClass.mar))
                                {
                                    semvalues.writmar = reader[0].ToString();
                                    semvalues.fmar = "*";                                    //semvalues.oralmar = reader[1].ToString();
                                }
                                else if (str.Equals(SubjectClass.hindi) || str.Equals(SubjectClass.hind))
                                {
                                    semvalues.writhindi = reader[0].ToString();
                                    semvalues.fhind = "*";
                                    //semvalues.oralhindi = reader[1].ToString();
                                }
                                else if (str.Equals(SubjectClass.maths))
                                {
                                    semvalues.writmath = reader[0].ToString();
                                    semvalues.fmath = "*";
                                    //semvalues.oralmath = reader[1].ToString();
                                }
                                else if (str.Equals(SubjectClass.sci))
                                {
                                    semvalues.writsci = reader[0].ToString();
                                    semvalues.fsci = "*";
                                    //semvalues.oralsci = reader[1].ToString();
                                }
                                else if (str.Equals(SubjectClass.socsci) || str.Equals(SubjectClass.ss))
                                {
                                    semvalues.writss = reader[0].ToString();
                                    semvalues.fss = "*";
                                    //semvalues.oralss = reader[1].ToString();

                                }
                            }
                            else
                            {
                                if (str.Equals(SubjectClass.eng))
                                {

                                    semvalues.writeng = reader[0].ToString();
                                    //semvalues.oraleng = reader[1].ToString();
                                }
                                else if (str.Equals(SubjectClass.mar))
                                {
                                    semvalues.writmar = reader[0].ToString();
                                    //semvalues.oralmar = reader[1].ToString();
                                }
                                else if (str.Equals(SubjectClass.hindi) || str.Equals(SubjectClass.hind))
                                {
                                    semvalues.writhindi = reader[0].ToString();
                                    //semvalues.oralhindi = reader[1].ToString();


                                }
                                else if (str.Equals(SubjectClass.maths))
                                {
                                    semvalues.writmath = reader[0].ToString();
                                    //semvalues.oralmath = reader[1].ToString();
                                }
                                else if (str.Equals(SubjectClass.sci))
                                {
                                    semvalues.writsci = reader[0].ToString();
                                    //semvalues.oralsci = reader[1].ToString();
                                }
                                else if (str.Equals(SubjectClass.socsci) || str.Equals(SubjectClass.ss))
                                {
                                    semvalues.writss = reader[0].ToString();
                                    //semvalues.oralss = reader[1].ToString();

                                }
                            }
                        }
                        else
                        {
                            if (str.Equals(SubjectClass.eng))
                            {

                                semvalues.writeng = reader[0].ToString();
                                //semvalues.oraleng = reader[1].ToString();
                            }
                            else if (str.Equals(SubjectClass.mar))
                            {
                                semvalues.writmar = reader[0].ToString();
                                //semvalues.oralmar = reader[1].ToString();
                            }
                            else if (str.Equals(SubjectClass.hindi) || str.Equals(SubjectClass.hind))
                            {
                                semvalues.writhindi = reader[0].ToString();
                                //semvalues.oralhindi = reader[1].ToString();


                            }
                            else if (str.Equals(SubjectClass.maths))
                            {
                                semvalues.writmath = reader[0].ToString();
                                //semvalues.oralmath = reader[1].ToString();
                            }
                            else if (str.Equals(SubjectClass.sci))
                            {
                                semvalues.writsci = reader[0].ToString();
                                //semvalues.oralsci = reader[1].ToString();
                            }
                            else if (str.Equals(SubjectClass.socsci) || str.Equals(SubjectClass.ss))
                            {
                                semvalues.writss = reader[0].ToString();
                                //semvalues.oralss = reader[1].ToString();

                            }
                            else if (str.Equals(SubjectClass.art))
                            {
                                semvalues.writart = reader[2].ToString();
                                //semvalues.oralart = reader[1].ToString();
                            }
                            else if (str.Equals(SubjectClass.js))
                            {
                                semvalues.writjs = reader[2].ToString();
                                //semvalues.oraljs = reader[1].ToString();

                            }
                            else if (str.Equals(SubjectClass.pe))
                            {
                                semvalues.writpe = reader[2].ToString();
                                //semvalues.oralpe = reader[1].ToString();
                            }
                            else if (str.Equals(SubjectClass.computer))
                            {
                                semvalues.writcomp = reader[2].ToString();
                                //semvalues.oralcomp = reader[1].ToString();

                            }
                        }


                    }
                    reader.Close();


                }

                string sematt = "0", totalsematt = "0";

                if (examname == "First Semester")
                {
                    query = "select gtotal From Attendance where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        sematt = reader[0].ToString();

                    }
                    reader.Close();

                    //For Semester I
                    query = "select SUM(Cast(totaldays as int)) From workingdays where std='" + std + "' and [month] IN('Jun','Jul','Aug','Sep','Oct','Nov');";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        totalsematt = reader[0].ToString();
                    }
                    reader.Close();
                }

                //remarks

                query = "select [application],conduct,remark From Remark where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and examname='" + examname + "' and academicyear='" + year + "';";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    semvalues.application = reader[0].ToString();
                    semvalues.conduct = reader[1].ToString();
                    semvalues.remark = reader[2].ToString();
                }
                reader.Close();


                string failedremark = "";
                string remark = "";

                if (failedSubjects > 0)
                {
                    //for (int i = 1; i <= failedSubjects; i++)
                    //{
                    //    string failedRemark = "F" + i;
                    //    if (i < failedSubjects) // Add comma for all except the last subject
                    //    {
                    //        remark += failedRemark + ", ";
                    //    }
                    //    else // For the last subject, do not add a comma
                    //    {
                    //        remark += failedRemark;
                    //    }
                    //}
                    remark = "F" + failedSubjects;
                }
                else
                {
                    remark = "PASSED";
                }

                teachername = "";
                query = "select teachername From TeacherMapping where std='" + std + "' and div='" + div + "';";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    teachername = reader[0].ToString();
                }
                reader.Close();

                //con.Close();

                mkrep.SetDataSource(ds10.Tables["studentds"]);
                mkrep.SetParameterValue("academicyear", year);
                //mkrep.SetParameterValue("examname", examname);
                mkrep.SetParameterValue("teachername", teachername);
                mkrep.SetParameterValue("resultday", resultday);

                // Semester Exam
                mkrep.SetParameterValue("writeng", semvalues.writeng);
                mkrep.SetParameterValue("writmar", semvalues.writmar);
                mkrep.SetParameterValue("writhindi", semvalues.writhindi);
                mkrep.SetParameterValue("writmath", semvalues.writmath);
                mkrep.SetParameterValue("writsci", semvalues.writsci);
                mkrep.SetParameterValue("writss", semvalues.writss);
                mkrep.SetParameterValue("writart", semvalues.writart);
                mkrep.SetParameterValue("writpe", semvalues.writpe);
                mkrep.SetParameterValue("writcomp", semvalues.writcomp);
                mkrep.SetParameterValue("writjs", semvalues.writjs);


                mkrep.SetParameterValue("feng", semvalues.feng);
                mkrep.SetParameterValue("fmar", semvalues.fmar);
                mkrep.SetParameterValue("fhind", semvalues.fhind);
                mkrep.SetParameterValue("fmath", semvalues.fmath);
                mkrep.SetParameterValue("fsci", semvalues.fsci);
                mkrep.SetParameterValue("fss", semvalues.fss);
                mkrep.SetParameterValue("FaildRemark", remark);

                //mkrep.SetParameterValue("oraleng", semvalues.oraleng);
                //mkrep.SetParameterValue("oralmar", semvalues.oralmar);
                //mkrep.SetParameterValue("oralhindi", semvalues.oralhindi);
                //mkrep.SetParameterValue("oralmath", semvalues.oralmath);
                //mkrep.SetParameterValue("oralsci", semvalues.oralsci);
                //mkrep.SetParameterValue("oralss", semvalues.oralss);
                //mkrep.SetParameterValue("oralart", semvalues.oralart);
                //mkrep.SetParameterValue("oralpe", semvalues.oralpe);
                //mkrep.SetParameterValue("oralcomp", semvalues.oralcomp);
                //mkrep.SetParameterValue("oraljs", semvalues.oraljs);

                mkrep.SetParameterValue("present", sematt);
                mkrep.SetParameterValue("totalatt", totalsematt);
                //remarks
                mkrep.SetParameterValue("application", semvalues.application);
                mkrep.SetParameterValue("conduct", semvalues.conduct);
                mkrep.SetParameterValue("remark", semvalues.remark);

                return mkrep;
            }
            catch (Exception ex)
            {
                Log.Error("PrintMarksheet.PrintMarksheet8to9Sem1", ex);
                return null;
            }

        }
        public ExamReportStd8to9Sem2 PrintMarksheet9(SqlConnection con, string grno, string std, string div, string examname, string resultday, string year)
        {
            try
            {
                int failedSubjects = 0;
                SqlCommand cmd = null;
                SqlDataReader reader = null;
                Dataset10 ds10 = new Dataset10();
                DataTable studentmarksheet = new DataTable();
                List<string> subjects = new List<string>();
                ExamReportStd8to9Sem2 mkrep = new ExamReportStd8to9Sem2();

                String query = "", teachername = "";
                //year = new FeesModel().setActiveAcademicYear();
                query = "Select grno,Format(Cast(dob as date),'dd/MM/yyyy') as dob,rollno,std,div,fname as Name,mname as Fathername,lname as Surname,MOTHERNAME,saralid,aadharcard as uid,photopath,address,mobile as contact From studentmaster where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '');";
                cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(ds10.Tables["studentds"]);

                SemesterClassStd9 semvalues = new SemesterClassStd9();

                query = "select Distinct Cast([srno] as int) as [srno],UPPER(Subject) as [Subject] From subjectmaster where std='" + std + "' and examname='First Semester' order by srno;";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    subjects.Add(reader[1].ToString());
                }
                reader.Close();
                failedSubjects = 0;

                query = "select UPPER(LTRIM(RTRIM(subjectname))) AS Subjectname,Written,oralsprac,total From StudentMarksheet8TO9 where grno='" + grno + "' and std='" + std + "' and academicyear='" + year + "';";
                cmd = new SqlCommand(query, con);
                adap = new SqlDataAdapter(cmd);
                adap.Fill(studentmarksheet);

                foreach (String str in subjects)
                {
                    double total = 0;
                    if (str.ToUpper().Trim().Equals(SubjectClass.eng))
                    {
                        //check for both exams
                        var datarow = studentmarksheet.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.eng));

                        foreach (DataRow ro in datarow)
                        {
                            if (ro["total"].ToString().Length > 0 && ro["total"].ToString().All(char.IsDigit))
                            {
                                total = total + Convert.ToDouble(ro["total"]);
                            }
                        }
                        if (total > 0)
                        {
                            semvalues.writeng = Math.Round(Convert.ToDouble(total) / 2, MidpointRounding.AwayFromZero).ToString("##");

                            if (Convert.ToInt32(semvalues.writeng) < 35)
                            {
                                semvalues.feng = "*";
                            }
                        }
                        else
                        {
                            semvalues.writeng = "AB";
                        }

                    }
                    if (str.ToUpper().Trim().Equals(SubjectClass.mar))
                    {
                        //check for both exams
                        var datarow = studentmarksheet.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.mar));

                        foreach (DataRow ro in datarow)
                        {
                            if (ro["total"].ToString().Length > 0 && ro["total"].ToString().All(char.IsDigit))
                            {
                                total = total + Convert.ToDouble(ro["total"]);
                            }
                        }
                        if (total > 0)
                        {
                            semvalues.writmar = Math.Round(Convert.ToDouble(total) / 2, MidpointRounding.AwayFromZero).ToString("##");
                            if (Convert.ToInt32(semvalues.writmar) < 35)
                            {
                                semvalues.fmar = "*";
                            }
                        }
                        else
                        {
                            semvalues.writmar = "AB";
                        }
                    }
                    if (str.ToUpper().Trim().Equals(SubjectClass.hind))
                    {
                        //check for both exams
                        var datarow = studentmarksheet.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.hindi) || x.Field<string>("Subjectname").Equals(SubjectClass.hind));

                        foreach (DataRow ro in datarow)
                        {
                            if (ro["total"].ToString().Length > 0 && ro["total"].ToString().All(char.IsDigit))
                            {
                                total = total + Convert.ToDouble(ro["total"]);
                            }
                        }
                        if (total > 0)
                        {
                            semvalues.writhindi = Math.Round(Convert.ToDouble(total) / 2, MidpointRounding.AwayFromZero).ToString("##");
                            if (Convert.ToInt32(semvalues.writhindi) < 35)
                            {
                                semvalues.fhind = "*";
                            }
                        }
                        else
                        {
                            semvalues.writhindi = "AB";
                        }
                    }
                    if (str.ToUpper().Trim().Equals(SubjectClass.maths))
                    {
                        //check for both exams
                        var datarow = studentmarksheet.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.maths));

                        foreach (DataRow ro in datarow)
                        {
                            if (ro["total"].ToString().Length > 0 && ro["total"].ToString().All(char.IsDigit))
                            {
                                total = total + Convert.ToDouble(ro["total"]);
                            }
                        }
                        if (total > 0)
                        {
                            semvalues.writmath = Math.Round(Convert.ToDouble(total) / 2, MidpointRounding.AwayFromZero).ToString("##");
                            if (Convert.ToInt32(semvalues.writmath) < 35)
                            {
                                semvalues.fmath = "*";
                            }
                        }
                        else
                        {
                            semvalues.writmath = "AB";
                        }
                    }
                    if (str.ToUpper().Trim().Equals(SubjectClass.sci))
                    {
                        var datarow = studentmarksheet.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.sci));

                        foreach (DataRow ro in datarow)
                        {
                            if (ro["total"].ToString().Length > 0 && ro["total"].ToString().All(char.IsDigit))
                            {
                                total = total + Convert.ToDouble(ro["total"]);
                            }
                        }
                        if (total > 0)
                        {
                            semvalues.writsci = Math.Round(Convert.ToDouble(total) / 2, MidpointRounding.AwayFromZero).ToString("##");
                            if (Convert.ToInt32(semvalues.writsci) < 35)
                            {
                                semvalues.fsci = "*";
                            }
                        }
                        else
                        {
                            semvalues.writsci = "AB";
                        }
                    }
                    if (str.ToUpper().Trim().Equals(SubjectClass.socsci) || str.ToUpper().Trim().Equals((SubjectClass.ss).ToUpper()))
                    {
                        //check for both exams
                        var datarow = studentmarksheet.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.socsci) || x.Field<string>("Subjectname").Equals((SubjectClass.ss).ToUpper()));

                        foreach (DataRow ro in datarow)
                        {
                            if (ro["total"].ToString().Length > 0 && ro["total"].ToString().All(char.IsDigit))
                            {
                                total = total + Convert.ToDouble(ro["total"]);
                            }
                        }
                        if (total > 0)
                        {
                            semvalues.writss = Math.Round(Convert.ToDouble(total) / 2, MidpointRounding.AwayFromZero).ToString("##");
                            if (Convert.ToInt32(semvalues.writss) < 35)
                            {
                                semvalues.fss = "*";
                            }
                        }
                        else
                        {
                            semvalues.writss = "AB";
                        }
                    }
                    if (str.ToUpper().Trim().Equals(SubjectClass.art))
                    {
                        //check for both exams
                        var datarow = studentmarksheet.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.art));

                        foreach (DataRow ro in datarow)
                        {
                            if (ro["total"].ToString().Length > 0)
                            {
                                semvalues.writart = ro["total"].ToString();
                            }
                        }
                    }
                    if (str.ToUpper().Trim().Equals(SubjectClass.pe))
                    {
                        //check for both exams
                        var datarow = studentmarksheet.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.pe));

                        foreach (DataRow ro in datarow)
                        {
                            if (ro["total"].ToString().Length > 0)
                            {
                                semvalues.writpe = ro["total"].ToString();
                            }
                        }
                    }
                    if (str.ToUpper().Trim().Equals(SubjectClass.js))
                    {
                        //check for both exams
                        var datarow = studentmarksheet.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.js));

                        foreach (DataRow ro in datarow)
                        {
                            if (ro["total"].ToString().Length > 0)
                            {
                                semvalues.writjs = ro["total"].ToString();
                            }
                        }
                    }
                    if (str.ToUpper().Trim().Equals(SubjectClass.computer))
                    {
                        //check for both exams
                        var datarow = studentmarksheet.AsEnumerable().Where(x => x.Field<string>("Subjectname").Equals(SubjectClass.computer));

                        foreach (DataRow ro in datarow)
                        {
                            if (ro["total"].ToString().Length > 0)
                            {
                                semvalues.writcomp = ro["total"].ToString();
                            }
                        }
                    }
                }

                int gtotal = 0;

                if (semvalues.writeng.All(char.IsDigit))
                {
                    gtotal = gtotal + Convert.ToInt32(semvalues.writeng);
                }
                if (semvalues.writmar.All(char.IsDigit))
                {
                    gtotal = gtotal + Convert.ToInt32(semvalues.writmar);
                }
                if (semvalues.writhindi.All(char.IsDigit))
                {
                    gtotal = gtotal + Convert.ToInt32(semvalues.writhindi);
                }
                if (semvalues.writmath.All(char.IsDigit))
                {
                    gtotal = gtotal + Convert.ToInt32(semvalues.writmath);
                }
                if (semvalues.writsci.All(char.IsDigit))
                {
                    gtotal = gtotal + Convert.ToInt32(semvalues.writsci);
                }
                if (semvalues.writss.All(char.IsDigit))
                {
                    gtotal = gtotal + Convert.ToInt32(semvalues.writss);
                }

                string percentage = ((Convert.ToDouble(gtotal) * 100) / 600).ToString("##.##");
                string presentee = "", workingday = "0";
                query = "select gtotal from Attendance where std='" + std + "' and div='" + div + "' and  Grno='" + grno + "' and Academicyear='" + year + "'";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    presentee = reader["gtotal"].ToString();
                }
                reader.Close();
                query = "select sum(cast (TotalDays as int)) as workingday from WorkingDays where std='" + std + "'";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    workingday = reader["workingday"].ToString();
                }
                reader.Close();


                mkrep.SetDataSource(ds10.Tables["studentds"]);
                mkrep.SetParameterValue("academicyear", year);
                // Semester Exam
                mkrep.SetParameterValue("writeng", semvalues.writeng);
                mkrep.SetParameterValue("writmar", semvalues.writmar);
                mkrep.SetParameterValue("writhindi", semvalues.writhindi);
                mkrep.SetParameterValue("writmath", semvalues.writmath);
                mkrep.SetParameterValue("writsci", semvalues.writsci);
                mkrep.SetParameterValue("writss", semvalues.writss);
                mkrep.SetParameterValue("writart", semvalues.writart);
                mkrep.SetParameterValue("writpe", semvalues.writpe);
                mkrep.SetParameterValue("writcomp", semvalues.writcomp);
                mkrep.SetParameterValue("writjs", semvalues.writjs);
                mkrep.SetParameterValue("resultday", resultday);
                mkrep.SetParameterValue("feng", semvalues.feng);
                mkrep.SetParameterValue("fmar", semvalues.fmar);
                mkrep.SetParameterValue("fhind", semvalues.fhind);
                mkrep.SetParameterValue("fmath", semvalues.fmath);
                mkrep.SetParameterValue("fsci", semvalues.fsci);
                mkrep.SetParameterValue("fss", semvalues.fss);
                mkrep.SetParameterValue("present", presentee);
                mkrep.SetParameterValue("gtotal", gtotal);
                mkrep.SetParameterValue("percent", percentage);
                mkrep.SetParameterValue("grade", "");
                mkrep.SetParameterValue("wrkday", workingday);

                return mkrep;
            }
            catch (Exception ex)
            {
                Log.Error("PrintMarksheet.PrintMarksheet9", ex);
                return null;
            }

        }

        public Marksheet5_8 PrintMarksheet5_8(SqlConnection con, string grno, string std, string div, string examname, string openingdate, string resultdate, string year)
        {
            try
            {
                //string year = lblacademicyear.Text.ToString();
                SqlCommand cmd = null;
                studentdataset studds = new studentdataset();
                Dictionary<string, GradeValues> dict = new Dictionary<string, GradeValues>();
                List<string> exams = new System.Collections.Generic.List<string>();
                String[] subjparms = new String[10] { "sub1", "sub2", "sub3", "sub4", "sub5", "sub6", "sub7", "sub8", "sub9", "sub10" };
                String[] term1subj = new String[10] { "term1grade1", "term1grade2", "term1grade3", "term1grade4", "term1grade5", "term1grade6", "term1grade7", "term1grade8", "term1grade9", "term1grade10" };
                String[] term2subj = new String[10] { "term2grade1", "term2grade2", "term2grade3", "term2grade4", "term2grade5", "term2grade6", "term2grade7", "term2grade8", "term2grade9", "term2grade10" };

                Marksheet5_8 mkrep = new Marksheet5_8();

                String query = "";
                // year = new FeesModel().setActiveAcademicYear();

                query = "Select grno,Format(Cast(dob as date),'dd/MM/yyyy') as dob,rollno,std,div,fname as Name,mname as Fathername,lname as Surname,MOTHERNAME,saralid,aadharcard as uid,photopath From studentmaster where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '');";

                cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(studds.Tables[0]);

                GradeValues gradevalues = null;



                query = "Select  [srno],[Subject] From subjectmaster where std='" + std + "' and examname='" + examname + "' order by cast(srno as int);";
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    gradevalues = new GradeValues();

                    dict.Add(reader[1].ToString(), gradevalues);
                }
                reader.Close();

                query = "Select [Examname] From ExamMaster where std='" + std + "' and Examname='" + examname + "' order by Examorder;";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    exams.Add(reader[0].ToString());
                }
                reader.Close();
                int total = 0, percentage = 0, failcount = 0;
                foreach (KeyValuePair<string, GradeValues> keyval in dict)
                {
                    GradeValues grad = new GradeValues();
                    for (int i = 0; i < exams.Count; i++)
                    {

                        query = "Select finalgrade,summativetotal From studentmarksheet where std='" + std + "' and div='" + div + "' and examname='" + exams[i] + "' and grno='" + grno + "' and subjectname='" + keyval.Key + "' and academicyear='" + year + "';";
                        cmd = new SqlCommand(query, con);
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (keyval.Key == "ART" || keyval.Key == "WORK EXP" || keyval.Key == "P.E." || keyval.Key == "COMPUTER" || keyval.Key == "P.T.")
                                {
                                    grad.Gradeterm2 = reader[0].ToString();
                                    //grad.Gradeterm1 = "";
                                }
                                else
                                {
                                    grad.Gradeterm2 = reader[1].ToString();
                                }
                                if ((std == "V" || std == "V (SE)") && grad.Gradeterm2.All(char.IsDigit))
                                {
                                    total = total + Convert.ToInt32(grad.Gradeterm2);

                                    if (Convert.ToInt32(grad.Gradeterm2) < 18)
                                    {
                                        grad.Gradeterm1 = "FAIL/PROMOTED";
                                        failcount++;
                                    }
                                    else
                                    {
                                        grad.Gradeterm1 = "PASS";
                                    }
                                }
                                else if (grad.Gradeterm2.All(char.IsDigit))
                                {
                                    total = total + Convert.ToInt32(grad.Gradeterm2);
                                    if (Convert.ToInt32(grad.Gradeterm2) < 21)
                                    {
                                        grad.Gradeterm1 = "FAIL/PROMOTED";
                                        failcount++;
                                    }
                                    else
                                    {
                                        grad.Gradeterm1 = "PASS";
                                    }
                                }
                            }
                        }
                        else
                        {
                            grad.Gradeterm1 = "";
                            grad.Gradeterm2 = "";
                        }
                        reader.Close();
                    }

                    keyval.Value.Gradeterm1 = grad.Gradeterm1;
                    keyval.Value.Gradeterm2 = grad.Gradeterm2;

                }


                if (std == "V" || std == "V (SE)")
                {
                    percentage = (int)Math.Round((Convert.ToDouble(total) * 100) / 250, MidpointRounding.AwayFromZero);
                }
                else if (std == "VIII" || std == "VIII (Mar)" || std == "VIII (Hindi)" || std == "VIII (SE)")
                {
                    percentage = (int)Math.Round((Convert.ToDouble(total) * 100) / 360, MidpointRounding.AwayFromZero);
                }

                string hmname = "", teachername = "", newstd = "", newdiv = "";

                query = "Select [teachername] From TeacherMapping where std='" + std + "' and div='" + div + "';";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    teachername = reader[0].ToString();
                }

                string schoolname = "";

                if (std == "I" || std == "II" || std == "III" || std == "IV" || std == "I (SE)" || std == "II (SE)" || std == "III (SE)" || std == "IV (SE)")
                {
                    schoolname = "CENTURY RAYON PRIMARY SCHOOL , SHAHAD";
                }
                else
                {
                    schoolname = "CENTURY RAYON High SCHOOL , SHAHAD";
                }

                //con.Close();

                //setting datasource values

                mkrep.SetDataSource(studds.Tables[0]);


                //setting parameter values
                int j = 0;
                foreach (KeyValuePair<string, GradeValues> keyval in dict)
                {
                    mkrep.SetParameterValue(subjparms[j], keyval.Key);
                    mkrep.SetParameterValue(term1subj[j], keyval.Value.Gradeterm1);
                    mkrep.SetParameterValue(term2subj[j], keyval.Value.Gradeterm2);
                    j++;
                }

                for (int k = j; k < 10; k++)
                {
                    mkrep.SetParameterValue(subjparms[k], "");
                    mkrep.SetParameterValue(term1subj[k], "");
                    mkrep.SetParameterValue(term2subj[k], "");
                }

                mkrep.SetParameterValue("schoolname", schoolname);
                if (failcount > 0)
                {
                    mkrep.SetParameterValue("percentage", "-");
                }
                else
                {
                    mkrep.SetParameterValue("percentage", percentage);
                }
                mkrep.SetParameterValue("academicyear", year);
                mkrep.SetParameterValue("trname", teachername);
                mkrep.SetParameterValue("resultday", resultdate);
                mkrep.SetParameterValue("openingdate", openingdate);


                return mkrep;
            }

            catch (Exception ex)
            {
                Log.Error("PrintMarksheet.PrintMarksheet5_8", ex);
                return null;
            }
        }

        public class GradeValues
        {
            public string Gradeterm1 { get; set; }
            public string Gradeterm2 { get; set; }
            public GradeValues()
            {
                Gradeterm1 = "-";
                Gradeterm2 = "-";
            }
        }

        public class Remarks
        {
            public string special { get; set; }
            public string liking { get; set; }
            public string needsimprov { get; set; }

            public Remarks()
            {
                special = "-";
                liking = "-";
                needsimprov = "-";
            }
        }

        public class SemesterClassStd9
        {
            public SemesterClassStd9()
            {
                writeng = "0";
                writmar = "0";
                writhindi = "0";
                writmath = "0";
                writsci = "0";
                writss = "0";
                writart = "0";
                writjs = "0";
                writpe = "0";
                writcomp = "0";

                oraleng = "0";
                oralmar = "0";
                oralhindi = "0";
                oralmath = "0";
                oralsci = "0";
                oralss = "0";
                oralart = "0";
                oraljs = "0";
                oralpe = "0";
                oralcomp = "0";

                // sem2
                writsem2eng = "0";
                writsem2mar = "0";
                writsem2hindi = "0";
                writsem2math = "0";
                writsem2sci = "0";
                writsem2ss = "0";
                writsem2art = "0";
                writsem2js = "0";
                writsem2pe = "0";
                writsem2comp = "0";

                oralsem2eng = "0";
                oralsem2mar = "0";
                oralsem2hindi = "0";
                oralsem2math = "0";
                oralsem2sci = "0";
                oralsem2ss = "0";
                oralsem2art = "0";
                oralsem2js = "0";
                oralsem2pe = "0";
                oralsem2comp = "0";

                present = "-";
                totalatt = "-";
                application = "-";
                conduct = "-";
                remark = "-";
                teachername = "-";
                resultday = "-";
                academicyear = "-";

                feng = "";
                fmar = "";
                fhind = "";
                fmath = "";
                fss = "";
                fsci = "";
            }


            public string feng { get; set; }
            public string fmar { get; set; }
            public string fhind { get; set; }
            public string fss { get; set; }
            public string fsci { get; set; }
            public string fmath { get; set; }
            //Semester I
            public string writeng { get; set; }
            public string writmar { get; set; }
            public string writhindi { get; set; }
            public string writmath { get; set; }
            public string writsci { get; set; }
            public string writss { get; set; }
            public string writart { get; set; }
            public string writjs { get; set; }
            public string writpe { get; set; }
            public string writcomp { get; set; }

            public string oraleng { get; set; }
            public string oralmar { get; set; }
            public string oralhindi { get; set; }
            public string oralmath { get; set; }
            public string oralsci { get; set; }
            public string oralss { get; set; }
            public string oralart { get; set; }
            public string oraljs { get; set; }
            public string oralpe { get; set; }
            public string oralcomp { get; set; }

            // sem2

            public string writsem2eng { get; set; }
            public string writsem2mar { get; set; }
            public string writsem2hindi { get; set; }
            public string writsem2math { get; set; }
            public string writsem2sci { get; set; }
            public string writsem2ss { get; set; }
            public string writsem2art { get; set; }
            public string writsem2js { get; set; }
            public string writsem2pe { get; set; }
            public string writsem2comp { get; set; }


            public string oralsem2eng { get; set; }
            public string oralsem2mar { get; set; }
            public string oralsem2hindi { get; set; }
            public string oralsem2math { get; set; }
            public string oralsem2sci { get; set; }
            public string oralsem2ss { get; set; }
            public string oralsem2art { get; set; }
            public string oralsem2js { get; set; }
            public string oralsem2pe { get; set; }
            public string oralsem2comp { get; set; }



            public string present { get; set; }
            public string totalatt { get; set; }
            public string application { get; set; }
            public string conduct { get; set; }
            public string remark { get; set; }
            public string teachername { get; set; }
            public string resultday { get; set; }
            public string academicyear { get; set; }


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
        }

        protected async void Printall_ServerClick(object sender, EventArgs e)
        {
            string select_std = "", select_div = "", examname = "", grno = "-", filename = "", foldername = "", openingdate = "", resultdate = "", year = "";
            select_std = cmbStd.SelectedValue.ToString();
            select_div = cmbDiv.SelectedValue.ToString();
            examname = cmbexam.SelectedValue.ToString();
            year = cmbAcademicyear.SelectedValue.ToString();
            openingdate = txtreopen.Text.Trim();
            resultdate = txtresult.Text.Trim();
            foldername = select_std + "_" + select_div + "_" + examname;

            filename = await startPrint(select_std, select_div, examname, foldername, openingdate, resultdate, year);

            if (filename != "error")
            {

                Response.Redirect("/MarksheetModule/DownloadFile.aspx?action=MarksheetPrintAll&grno=" + grno + "&div=" + select_div + " &std=" + select_std + "&examname=" + examname + "&filename=" + filename + "&foldername=" + foldername);

            }
            else
            {

                lblalertmessage.Text = "Erorr";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
            }
        }

        public async Task<string> startPrint(string select_std, string select_div, string examname, string foldername, string openingdate, string resultdate, string year)
        {
            string filename = "";
            return await Task.Run(() =>
           {
               try
               {

                   select_std = cmbStd.SelectedValue.ToString();
                   select_div = cmbDiv.SelectedValue.ToString();
                   examname = cmbexam.SelectedValue.ToString();

                   foldername = select_std + "_" + select_div + "_" + examname;
                   filename = printallpdf(select_std, select_div, examname, foldername, openingdate, resultdate, year);


                   return filename;
               }
               catch (Exception ex)
               {
                   Log.Error("PrintMarksheet.startPrint", ex);
                   return "error";
               }
           });
        }

        public string printallpdf(string select_std, string select_div, string examname, string foldername, string openingdate, string resultdate, string year)
        {
            string query = "", filepath = "", filname = "";

            //select_std = cmbStd.SelectedValue.ToString();
            //select_div = cmbDiv.SelectedValue.ToString();
            //examname = cmbexam.SelectedValue.ToString();
            string grno = "";
            string path = Server.MapPath("MarksheetFile");
            string folderpath = path + "\\" + foldername;
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    Marksheet1to4 mkrep = new Marksheet1to4();
                    Marksheet1to4Sem1 mkrep3 = new Marksheet1to4Sem1();
                    ExamReportStd8to9 mkrep1 = new ExamReportStd8to9();
                    ExamReportStd8to9Sem1 mkrep2 = new ExamReportStd8to9Sem1();
                    ExamReportStd8to9Sem2 mkrep4 = new ExamReportStd8to9Sem2();
                    Marksheet5_8 mkrep5 = new Marksheet5_8();

                    //foldername = select_std + "_" + select_div + "_" + examname;
                    if (Directory.Exists(folderpath))
                    {
                        DeleteDirectory(folderpath);
                    }
                    path += "\\" + foldername;

                    Directory.CreateDirectory(path);

                    foreach (GridViewRow row in GridCollection.Rows)
                    {
                        grno = row.Cells[1].Text;
                        if (((CheckBox)row.FindControl("chkSelect")).Checked)
                        {
                            if (Chk9th.Checked)
                            {
                                if (select_std == "IX" || select_std == "IX (Mar)" || select_std == "IX (Hindi)" || select_std == "X" || select_std == "X (Mar)" || select_std == "X (Hindi)")
                                {
                                    mkrep4 = PrintMarksheet9(con, grno, select_std, select_div, examname, resultdate, year);

                                    filepath = path + @"\" + row.Cells[1].Text.ToString() + ".pdf";
                                    if (File.Exists(filepath))
                                    {
                                        File.SetAttributes(filepath, FileAttributes.Normal);
                                        File.Delete(filepath);
                                    }
                                    mkrep4.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);
                                    mkrep4.Close();
                                    mkrep4.Dispose();//new code
                                    Log.Info(grno + " " + " Pdf Created");
                                }
                                else if (select_std == "V" || select_std == "V (SE)" || select_std == "VIII" || select_std == "VIII (Mar)" || select_std == "VIII (Hindi)" || select_std == "VIII (SE)")
                                {
                                    mkrep5 = PrintMarksheet5_8(con, grno, select_std, select_div, examname, openingdate, resultdate, year);

                                    filepath = path + @"\" + row.Cells[1].Text.ToString() + ".pdf";
                                    if (File.Exists(filepath))
                                    {
                                        File.SetAttributes(filepath, FileAttributes.Normal);
                                        File.Delete(filepath);
                                    }
                                    mkrep5.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);
                                    mkrep5.Close();
                                    mkrep5.Dispose();//new code
                                    Log.Info(grno + " " + " Pdf Created");
                                }
                            }
                            else if (select_std == "IX" || select_std == "IX (Mar)" || select_std == "IX (Hindi)" || select_std == "X" || select_std == "X (Mar)" || select_std == "X (Hindi)")
                            {

                                if (examname == "First Semester")
                                {
                                    mkrep2 = PrintMarksheet8to9Sem1(con, grno, select_std, select_div, examname, resultdate, year);

                                    filepath = path + @"\" + row.Cells[1].Text.ToString() + ".pdf";
                                    if (File.Exists(filepath))
                                    {

                                        File.SetAttributes(filepath, FileAttributes.Normal);
                                        File.Delete(filepath);
                                    }
                                    mkrep2.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);
                                    mkrep2.Close();
                                    mkrep2.Dispose();//new code
                                    Log.Info(grno + " " + " Pdf Created");
                                }
                                else if (examname == "Second Semester")
                                {
                                    mkrep1 = PrintMarksheet8to9(con, grno, select_std, select_div, examname, openingdate, year);

                                    filepath = path + @"\" + row.Cells[1].Text.ToString() + ".pdf";
                                    if (File.Exists(filepath))
                                    {
                                        File.SetAttributes(filepath, FileAttributes.Normal);
                                        File.Delete(filepath);
                                    }
                                    mkrep1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);
                                    mkrep1.Close();
                                    mkrep1.Dispose();//new code
                                    Log.Info(grno + " " + " Pdf Created");
                                }

                            }

                            else
                            {
                                if (examname == "First Semester")
                                {
                                    mkrep3 = PrintMarksheetPrimarySem1(con, grno, select_std, select_div, examname, resultdate, year);

                                    filepath = path + @"\" + row.Cells[1].Text.ToString() + ".pdf";
                                    if (File.Exists(filepath))
                                    {

                                        File.SetAttributes(filepath, FileAttributes.Normal);
                                        File.Delete(filepath);
                                    }
                                    //mkrep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);

                                    //mkrep.Close();
                                    //mkrep.Dispose();//new code

                                    // Export the report to a stream (e.g., MemoryStream)
                                    Stream stream = (Stream)mkrep3.ExportToStream(ExportFormatType.PortableDocFormat);
                                    // Save the stream to a file
                                    using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
                                    {
                                        stream.CopyTo(fileStream);
                                    }
                                    // Clean up resources
                                    mkrep3.Close();
                                    mkrep3.Dispose();
                                    stream.Close();
                                    stream.Dispose();

                                    Log.Info(grno + " " + " Pdf Created");
                                }
                                else if (examname == "Second Semester")
                                {
                                    mkrep = PrintMarksheetPrimary(con, grno, select_std, select_div, examname, openingdate, resultdate, year);

                                    filepath = path + @"\" + row.Cells[1].Text.ToString() + ".pdf";
                                    if (File.Exists(filepath))
                                    {

                                        File.SetAttributes(filepath, FileAttributes.Normal);
                                        File.Delete(filepath);
                                    }
                                    //mkrep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);

                                    //mkrep.Close();
                                    //mkrep.Dispose();//new code

                                    // Export the report to a stream (e.g., MemoryStream)
                                    Stream stream = (Stream)mkrep.ExportToStream(ExportFormatType.PortableDocFormat);
                                    // Save the stream to a file
                                    using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
                                    {
                                        stream.CopyTo(fileStream);
                                    }
                                    // Clean up resources
                                    mkrep.Close();
                                    mkrep.Dispose();
                                    stream.Close();
                                    stream.Dispose();

                                    Log.Info(grno + " " + " Pdf Created");
                                }
                            }
                        }

                    }

                    Pdfmerge pdfmerge = new Pdfmerge();

                    string[] filearray = pdfmerge.getAllpdffiles(path);

                    filepath = path + "\\" + select_std + "_" + select_div + "_Regular.pdf";

                    filname = select_std + "_" + select_div + "_Regular.pdf";
                    if (File.Exists(filepath))
                    {
                        File.SetAttributes(filepath, FileAttributes.Normal);
                        File.Delete(filepath);

                    }

                    pdfmerge.MergePDF(filepath, filearray);

                    pdfmerge.Dispose();
                    Log.Info("File Successfully Merged");
                    lblinfomsg.Text = "File Created Successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                }
                return filname;
            }
            catch (Exception ex)
            {
                Log.Error("PrintMarksheet.printallpdf", ex);
                return "error";
            }
        }

        private void DeleteDirectory(string path)
        {
            // Delete all files from the Directory  
            foreach (string filename in Directory.GetFiles(path))
            {
                File.Delete(filename);
            }
            // Check all child Directories and delete files  
            foreach (string subfolder in Directory.GetDirectories(path))
            {
                DeleteDirectory(subfolder);
            }
            Directory.Delete(path);
            Log.Info("Directory deleted successfully");
        }

        protected void txtreopen_TextChanged(object sender, EventArgs e)
        {
            Session["ReopenDate"] = txtreopen.Text.Trim();
        }

        protected void GridCollection_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string select_std = "";
            select_std = cmbStd.SelectedValue.ToString();
            if (select_std == "V" || select_std == "V (SE)" || select_std == "VIII" || select_std == "VIII (Mar)" || select_std == "VIII (Hindi)" || select_std == "VIII (SE)" || select_std == "IX" || select_std == "IX (Mar)" || select_std == "IX (Hindi)" || select_std == "X" || select_std == "X (Mar)" || select_std == "X (Hindi)")
            {
                e.Row.Cells[8].Visible = true;
            }
            else
            {
                e.Row.Cells[8].Visible = false;
            }

        }

        protected void txtresult_TextChanged(object sender, EventArgs e)
        {
            Session["Resultdate"] = txtresult.Text.Trim();
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