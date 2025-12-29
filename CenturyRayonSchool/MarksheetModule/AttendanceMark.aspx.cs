using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.MarksheetModule
{
    public partial class AttendanceMark : System.Web.UI.Page
    {
        DataTable attmarks = new DataTable();
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

            attmarks.Columns.Add("RollNo");
            attmarks.Columns.Add("GRNO");
            attmarks.Columns.Add("StudentName");
            attmarks.Columns.Add("std");
            attmarks.Columns.Add("div");
            attmarks.Columns.Add("jun");
            attmarks.Columns.Add("jul");
            attmarks.Columns.Add("aug");
            attmarks.Columns.Add("sep");
            attmarks.Columns.Add("oct");
            attmarks.Columns.Add("total");
            attmarks.Columns.Add("nov");
            attmarks.Columns.Add("dec");
            attmarks.Columns.Add("jan");
            attmarks.Columns.Add("feb");
            attmarks.Columns.Add("mar");
            attmarks.Columns.Add("apr");
            attmarks.Columns.Add("may");
            attmarks.Columns.Add("total1");
            attmarks.Columns.Add("grandtotal");
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
                    //cmbStd.SelectedValue = "Select Std";
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
                    fillstulist(std_sess, div_sess);


                }
            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMark.loadFormControl", ex);
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
                    academicyear = lblacademicyear.Text;

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
                string query = "", select_std = "", select_div = "", stuname = "", exam = "", year = "", freeshipamount = "0", ReceiptDate = "";

                string Amtpaid = "", freeshiptype = "";

                select_std = cmbStd.SelectedValue.ToString();
                select_div = cmbDiv.SelectedValue.ToString();
                exam = cmbexam.SelectedValue.ToString();
                stuname = cmbstudentname.SelectedValue.ToString();
                year = lblacademicyear.Text.ToString();
                using (con = Connection.getConnection())
                {
                    con.Open();

                    if (stuname.Equals("ALL"))
                    {
                        query = "select ROLLNO,GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,Academicyear,admissiontype from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '') order by Cast(ROLLNO as int) asc;";
                    }
                    else
                    {
                        query = "select ROLLNO,GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,Academicyear,admissiontype from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + year + "' and grno='" + stuname + "' and (leftstatus IS NULL OR leftstatus = '') order by Cast(ROLLNO as int) asc;";
                    }
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        attmarks.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
                    }
                    reader.Close();


                    fetchdata(select_std, select_div, exam, con);


                    GridCollection.DataSource = attmarks;
                    GridCollection.DataBind();


                }
            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMark.fillGridView", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
                stud_tbl.Dispose();
                attmarks.Dispose();
            }
        }

        public void fetchdata(string std, string div, string examname, SqlConnection con)
        {
            try
            {


                string query = "select Rollno,Grno,Studentname,std,div,june,july,aug,sep,oct,total1,nov,dec,jan,feb,march,april,may,total2,gtotal From Attendance where std = '" + std + "' and div = '" + div + "' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable subjtable = new DataTable();
                da.Fill(subjtable);

                foreach (DataRow ro in attmarks.Rows)
                {
                    string standard = ro["std"].ToString();
                    string grno = ro["Grno"].ToString();
                    var dr = subjtable.AsEnumerable().Where(x => x.Field<string>("std").Equals(standard) && x.Field<string>("Grno").Equals(grno)).DefaultIfEmpty(null).FirstOrDefault();

                    if (dr != null)
                    {
                        ro["jun"] = dr["june"].ToString();
                        ro["jul"] = dr["july"].ToString();
                        ro["aug"] = dr["aug"].ToString();
                        ro["sep"] = dr["sep"].ToString();
                        ro["oct"] = dr["oct"].ToString();
                        ro["total"] = dr["total1"].ToString();
                        ro["nov"] = dr["nov"].ToString();
                        ro["dec"] = dr["dec"].ToString();
                        ro["jan"] = dr["jan"].ToString();
                        ro["feb"] = dr["feb"].ToString();
                        ro["mar"] = dr["march"].ToString();
                        ro["apr"] = dr["april"].ToString();
                        ro["may"] = dr["may"].ToString();
                        ro["total1"] = dr["total2"].ToString();
                        ro["grandtotal"] = dr["gtotal"].ToString();
                    }
                    else
                    {
                        ro["std"] = "0";
                        ro["div"] = "0";
                        ro["jun"] = "0";
                        ro["jul"] = "0";
                        ro["aug"] = "0";
                        ro["sep"] = "0";
                        ro["oct"] = "0";
                        ro["total"] = "0";
                        ro["nov"] = "0";
                        ro["dec"] = "0";
                        ro["jan"] = "0";
                        ro["feb"] = "0";
                        ro["mar"] = "0";
                        ro["apr"] = "0";
                        ro["may"] = "0";
                        ro["total1"] = "0";
                        ro["grandtotal"] = "0";
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Error("AttendanceMArk.fetchdata", ex);
            }
        }

        protected void June_TextChanged(object sender, EventArgs e)
        {
            string total = "0";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox junTextBox = (TextBox)row.FindControl("jun");
                TextBox julTextBox = (TextBox)row.FindControl("jul");
                TextBox augTextBox = (TextBox)row.FindControl("aug");
                TextBox sepTextBox = (TextBox)row.FindControl("sep");
                TextBox octTextBox = (TextBox)row.FindControl("oct");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");



                string jun =junTextBox.Text;
                string jul =julTextBox.Text;
                string aug = augTextBox.Text;
                string sep = sepTextBox.Text;
                string oct = octTextBox.Text;




                if (jun.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jun)).ToString("00");
                }
                if (jul.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jul)).ToString("00");
                }
                if (aug.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(aug)).ToString("00");
                }
                if (sep.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(sep)).ToString("00");
                }
                if (oct.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(oct)).ToString("00");
                }


                if (jun.ToLower().Equals("ab") && jul.ToLower().Equals("ab") && aug.ToLower().Equals("ab") && sep.ToLower().Equals("ab") && oct.ToLower().Equals("ab"))
                {
                    totalTextBox.Text = "AB";
                }
                else
                {
                    totalTextBox.Text = total.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMArk.June_TextChanged", ex);
            }
        }

        protected void July_TextChanged(object sender, EventArgs e)
        {
            string total = "0";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox junTextBox = (TextBox)row.FindControl("jun");
                TextBox julTextBox = (TextBox)row.FindControl("jul");
                TextBox augTextBox = (TextBox)row.FindControl("aug");
                TextBox sepTextBox = (TextBox)row.FindControl("sep");
                TextBox octTextBox = (TextBox)row.FindControl("oct");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");



                string jun = junTextBox.Text;
                string jul = julTextBox.Text;
                string aug = augTextBox.Text;
                string sep = sepTextBox.Text;
                string oct = octTextBox.Text;




                if (jun.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jun)).ToString("00");
                }
                if (jul.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jul)).ToString("00");
                }
                if (aug.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(aug)).ToString("00");
                }
                if (sep.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(sep)).ToString("00");
                }
                if (oct.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(oct)).ToString("00");
                }


                if (jun.ToLower().Equals("ab") && jul.ToLower().Equals("ab") && aug.ToLower().Equals("ab") && sep.ToLower().Equals("ab") && oct.ToLower().Equals("ab"))
                {
                    totalTextBox.Text = "AB";
                }
                else
                {
                    totalTextBox.Text = total.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMArk.July_TextChanged", ex);
            }
        }

        protected void August_TextChanged(object sender, EventArgs e)
        {
            string total = "0";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox junTextBox = (TextBox)row.FindControl("jun");
                TextBox julTextBox = (TextBox)row.FindControl("jul");
                TextBox augTextBox = (TextBox)row.FindControl("aug");
                TextBox sepTextBox = (TextBox)row.FindControl("sep");
                TextBox octTextBox = (TextBox)row.FindControl("oct");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");



                string jun = junTextBox.Text;
                string jul = julTextBox.Text;
                string aug = augTextBox.Text;
                string sep = sepTextBox.Text;
                string oct = octTextBox.Text;




                if (jun.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jun)).ToString("00");
                }
                if (jul.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jul)).ToString("00");
                }
                if (aug.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(aug)).ToString("00");
                }
                if (sep.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(sep)).ToString("00");
                }
                if (oct.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(oct)).ToString("00");
                }


                if (jun.ToLower().Equals("ab") && jul.ToLower().Equals("ab") && aug.ToLower().Equals("ab") && sep.ToLower().Equals("ab") && oct.ToLower().Equals("ab"))
                {
                    totalTextBox.Text = "AB";
                }
                else
                {
                    totalTextBox.Text = total.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMArk.August_TextChanged", ex);
            }
        }

        protected void sept_TextChanged(object sender, EventArgs e)
        {
            string total = "0";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox junTextBox = (TextBox)row.FindControl("jun");
                TextBox julTextBox = (TextBox)row.FindControl("jul");
                TextBox augTextBox = (TextBox)row.FindControl("aug");
                TextBox sepTextBox = (TextBox)row.FindControl("sep");
                TextBox octTextBox = (TextBox)row.FindControl("oct");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");



                string jun = junTextBox.Text;
                string jul = julTextBox.Text;
                string aug = augTextBox.Text;
                string sep = sepTextBox.Text;
                string oct = octTextBox.Text;




                if (jun.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jun)).ToString("00");
                }
                if (jul.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jul)).ToString("00");
                }
                if (aug.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(aug)).ToString("00");
                }
                if (sep.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(sep)).ToString("00");
                }
                if (oct.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(oct)).ToString("00");
                }


                if (jun.ToLower().Equals("ab") && jul.ToLower().Equals("ab") && aug.ToLower().Equals("ab") && sep.ToLower().Equals("ab") && oct.ToLower().Equals("ab"))
                {
                    totalTextBox.Text = "AB";
                }
                else
                {
                    totalTextBox.Text = total.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMArk.sept_TextChanged", ex);
            }
        }

        protected void oct_TextChanged(object sender, EventArgs e)
        {
            string total = "0";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox junTextBox = (TextBox)row.FindControl("jun");
                TextBox julTextBox = (TextBox)row.FindControl("jul");
                TextBox augTextBox = (TextBox)row.FindControl("aug");
                TextBox sepTextBox = (TextBox)row.FindControl("sep");
                TextBox octTextBox = (TextBox)row.FindControl("oct");
                TextBox totalTextBox = (TextBox)row.FindControl("Total");

                string jun = junTextBox.Text;
                string jul = julTextBox.Text;
                string aug = augTextBox.Text;
                string sep = sepTextBox.Text;
                string oct = octTextBox.Text;

                if (jun.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jun)).ToString("00");
                }
                if (jul.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jul)).ToString("00");
                }
                if (aug.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(aug)).ToString("00");
                }
                if (sep.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(sep)).ToString("00");
                }
                if (oct.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(oct)).ToString("00");
                }


                if (jun.ToLower().Equals("ab") && jul.ToLower().Equals("ab") && aug.ToLower().Equals("ab") && sep.ToLower().Equals("ab") && oct.ToLower().Equals("ab"))
                {
                    totalTextBox.Text = "AB";
                }
                else
                {
                    totalTextBox.Text = total.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMArk.oct_TextChanged", ex);
            }
        }

        protected void total_TextChanged(object sender, EventArgs e)
        {

        }

        protected void nov_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal="0";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox novTextBox = (TextBox)row.FindControl("nov");
                TextBox decTextBox = (TextBox)row.FindControl("dec");
                TextBox janTextBox = (TextBox)row.FindControl("jan");
                TextBox febTextBox = (TextBox)row.FindControl("feb");
                TextBox marTextBox = (TextBox)row.FindControl("mar");
                TextBox aprTextBox = (TextBox)row.FindControl("apr");
                TextBox mayTextBox = (TextBox)row.FindControl("may");
                TextBox total1TextBox = (TextBox)row.FindControl("total1");
                TextBox totalTextBox = (TextBox)row.FindControl("total");
                TextBox grantotalTextBox = (TextBox)row.FindControl("grandtotal");



                string nov = novTextBox.Text;
                string dec = decTextBox.Text;
                string jan = janTextBox.Text;
                string feb = febTextBox.Text;
                string mar = marTextBox.Text;
                string apr = aprTextBox.Text;
                string may = mayTextBox.Text;
                string total1 = totalTextBox.Text;



                if (nov.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(nov)).ToString("00");
                }
                if (dec.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dec)).ToString("00");
                }
                if (jan.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jan)).ToString("00");
                }
                if (feb.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(feb)).ToString("00");
                }
                if (mar.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(mar)).ToString("00");
                }
                if (apr.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(apr)).ToString("00");
                }
                if (may.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(may)).ToString("00");
                }

                if (nov.ToLower().Equals("ab") && dec.ToLower().Equals("ab") && jan.ToLower().Equals("ab") && feb.ToLower().Equals("ab") && mar.ToLower().Equals("ab") && apr.ToLower().Equals("ab") && may.ToLower().Equals("ab"))
                {
                    total1TextBox.Text = "AB";
                }
                else
                {
                    total1TextBox.Text = total.ToString();
                }

                if(total1.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total1)).ToString("00");
                }
                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }

                if(total.ToLower().Equals("ab") && total1.ToLower().Equals("ab"))
                {
                    grantotalTextBox.Text = "AB";
                }
                else
                {
                    grantotalTextBox.Text = grandtotal.ToString();
                }


            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMArk.nov_TextChanged", ex);
            }
        }

        protected void December_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox novTextBox = (TextBox)row.FindControl("nov");
                TextBox decTextBox = (TextBox)row.FindControl("dec");
                TextBox janTextBox = (TextBox)row.FindControl("jan");
                TextBox febTextBox = (TextBox)row.FindControl("feb");
                TextBox marTextBox = (TextBox)row.FindControl("mar");
                TextBox aprTextBox = (TextBox)row.FindControl("apr");
                TextBox mayTextBox = (TextBox)row.FindControl("may");
                TextBox total1TextBox = (TextBox)row.FindControl("total1");
                TextBox totalTextBox = (TextBox)row.FindControl("total");
                TextBox grantotalTextBox = (TextBox)row.FindControl("grandtotal");



                string nov = novTextBox.Text;
                string dec = decTextBox.Text;
                string jan = janTextBox.Text;
                string feb = febTextBox.Text;
                string mar = marTextBox.Text;
                string apr = aprTextBox.Text;
                string may = mayTextBox.Text;
                string total1 = totalTextBox.Text;



                if (nov.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(nov)).ToString("00");
                }
                if (dec.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dec)).ToString("00");
                }
                if (jan.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jan)).ToString("00");
                }
                if (feb.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(feb)).ToString("00");
                }
                if (mar.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(mar)).ToString("00");
                }
                if (apr.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(apr)).ToString("00");
                }
                if (may.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(may)).ToString("00");
                }

                if (nov.ToLower().Equals("ab") && dec.ToLower().Equals("ab") && jan.ToLower().Equals("ab") && feb.ToLower().Equals("ab") && mar.ToLower().Equals("ab") && apr.ToLower().Equals("ab") && may.ToLower().Equals("ab"))
                {
                    total1TextBox.Text = "AB";
                }
                else
                {
                    total1TextBox.Text = total.ToString();
                }

                if (total1.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total1)).ToString("00");
                }
                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && total1.ToLower().Equals("ab"))
                {
                    grantotalTextBox.Text = "AB";
                }
                else
                {
                    grantotalTextBox.Text = grandtotal.ToString();
                }


            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMArk.december_TextChanged", ex);
            }
        }

        protected void jan_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox novTextBox = (TextBox)row.FindControl("nov");
                TextBox decTextBox = (TextBox)row.FindControl("dec");
                TextBox janTextBox = (TextBox)row.FindControl("jan");
                TextBox febTextBox = (TextBox)row.FindControl("feb");
                TextBox marTextBox = (TextBox)row.FindControl("mar");
                TextBox aprTextBox = (TextBox)row.FindControl("apr");
                TextBox mayTextBox = (TextBox)row.FindControl("may");
                TextBox total1TextBox = (TextBox)row.FindControl("total1");
                TextBox totalTextBox = (TextBox)row.FindControl("total");
                TextBox grantotalTextBox = (TextBox)row.FindControl("grandtotal");



                string nov = novTextBox.Text;
                string dec = decTextBox.Text;
                string jan = janTextBox.Text;
                string feb = febTextBox.Text;
                string mar = marTextBox.Text;
                string apr = aprTextBox.Text;
                string may = mayTextBox.Text;
                string total1 = totalTextBox.Text;



                if (nov.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(nov)).ToString("00");
                }
                if (dec.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dec)).ToString("00");
                }
                if (jan.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jan)).ToString("00");
                }
                if (feb.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(feb)).ToString("00");
                }
                if (mar.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(mar)).ToString("00");
                }
                if (apr.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(apr)).ToString("00");
                }
                if (may.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(may)).ToString("00");
                }

                if (nov.ToLower().Equals("ab") && dec.ToLower().Equals("ab") && jan.ToLower().Equals("ab") && feb.ToLower().Equals("ab") && mar.ToLower().Equals("ab") && apr.ToLower().Equals("ab") && may.ToLower().Equals("ab"))
                {
                    total1TextBox.Text = "AB";
                }
                else
                {
                    total1TextBox.Text = total.ToString();
                }

                if (total1.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total1)).ToString("00");
                }
                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && total1.ToLower().Equals("ab"))
                {
                    grantotalTextBox.Text = "AB";
                }
                else
                {
                    grantotalTextBox.Text = grandtotal.ToString();
                }


            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMArk.jan_TextChanged", ex);
            }
        }

        protected void feb_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox novTextBox = (TextBox)row.FindControl("nov");
                TextBox decTextBox = (TextBox)row.FindControl("dec");
                TextBox janTextBox = (TextBox)row.FindControl("jan");
                TextBox febTextBox = (TextBox)row.FindControl("feb");
                TextBox marTextBox = (TextBox)row.FindControl("mar");
                TextBox aprTextBox = (TextBox)row.FindControl("apr");
                TextBox mayTextBox = (TextBox)row.FindControl("may");
                TextBox total1TextBox = (TextBox)row.FindControl("total1");
                TextBox totalTextBox = (TextBox)row.FindControl("total");
                TextBox grantotalTextBox = (TextBox)row.FindControl("grandtotal");



                string nov = novTextBox.Text;
                string dec = decTextBox.Text;
                string jan = janTextBox.Text;
                string feb = febTextBox.Text;
                string mar = marTextBox.Text;
                string apr = aprTextBox.Text;
                string may = mayTextBox.Text;
                string total1 = totalTextBox.Text;



                if (nov.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(nov)).ToString("00");
                }
                if (dec.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dec)).ToString("00");
                }
                if (jan.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jan)).ToString("00");
                }
                if (feb.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(feb)).ToString("00");
                }
                if (mar.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(mar)).ToString("00");
                }
                if (apr.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(apr)).ToString("00");
                }
                if (may.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(may)).ToString("00");
                }

                if (nov.ToLower().Equals("ab") && dec.ToLower().Equals("ab") && jan.ToLower().Equals("ab") && feb.ToLower().Equals("ab") && mar.ToLower().Equals("ab") && apr.ToLower().Equals("ab") && may.ToLower().Equals("ab"))
                {
                    total1TextBox.Text = "AB";
                }
                else
                {
                    total1TextBox.Text = total.ToString();
                }

                if (total1.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total1)).ToString("00");
                }
                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && total1.ToLower().Equals("ab"))
                {
                    grantotalTextBox.Text = "AB";
                }
                else
                {
                    grantotalTextBox.Text = grandtotal.ToString();
                }


            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMArk.feb_TextChanged", ex);
            }
        }

        protected void mar_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox novTextBox = (TextBox)row.FindControl("nov");
                TextBox decTextBox = (TextBox)row.FindControl("dec");
                TextBox janTextBox = (TextBox)row.FindControl("jan");
                TextBox febTextBox = (TextBox)row.FindControl("feb");
                TextBox marTextBox = (TextBox)row.FindControl("mar");
                TextBox aprTextBox = (TextBox)row.FindControl("apr");
                TextBox mayTextBox = (TextBox)row.FindControl("may");
                TextBox total1TextBox = (TextBox)row.FindControl("total1");
                TextBox totalTextBox = (TextBox)row.FindControl("total");
                TextBox grantotalTextBox = (TextBox)row.FindControl("grandtotal");



                string nov = novTextBox.Text;
                string dec = decTextBox.Text;
                string jan = janTextBox.Text;
                string feb = febTextBox.Text;
                string mar = marTextBox.Text;
                string apr = aprTextBox.Text;
                string may = mayTextBox.Text;
                string total1 = totalTextBox.Text;



                if (nov.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(nov)).ToString("00");
                }
                if (dec.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dec)).ToString("00");
                }
                if (jan.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jan)).ToString("00");
                }
                if (feb.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(feb)).ToString("00");
                }
                if (mar.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(mar)).ToString("00");
                }
                if (apr.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(apr)).ToString("00");
                }
                if (may.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(may)).ToString("00");
                }

                if (nov.ToLower().Equals("ab") && dec.ToLower().Equals("ab") && jan.ToLower().Equals("ab") && feb.ToLower().Equals("ab") && mar.ToLower().Equals("ab") && apr.ToLower().Equals("ab") && may.ToLower().Equals("ab"))
                {
                    total1TextBox.Text = "AB";
                }
                else
                {
                    total1TextBox.Text = total.ToString();
                }

                if (total1.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total1)).ToString("00");
                }
                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && total1.ToLower().Equals("ab"))
                {
                    grantotalTextBox.Text = "AB";
                }
                else
                {
                    grantotalTextBox.Text = grandtotal.ToString();
                }


            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMArk.mar_TextChanged", ex);
            }
        }

        protected void apr_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox novTextBox = (TextBox)row.FindControl("nov");
                TextBox decTextBox = (TextBox)row.FindControl("dec");
                TextBox janTextBox = (TextBox)row.FindControl("jan");
                TextBox febTextBox = (TextBox)row.FindControl("feb");
                TextBox marTextBox = (TextBox)row.FindControl("mar");
                TextBox aprTextBox = (TextBox)row.FindControl("apr");
                TextBox mayTextBox = (TextBox)row.FindControl("may");
                TextBox total1TextBox = (TextBox)row.FindControl("total1");
                TextBox totalTextBox = (TextBox)row.FindControl("total");
                TextBox grantotalTextBox = (TextBox)row.FindControl("grandtotal");



                string nov = novTextBox.Text;
                string dec = decTextBox.Text;
                string jan = janTextBox.Text;
                string feb = febTextBox.Text;
                string mar = marTextBox.Text;
                string apr = aprTextBox.Text;
                string may = mayTextBox.Text;
                string total1 = totalTextBox.Text;



                if (nov.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(nov)).ToString("00");
                }
                if (dec.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dec)).ToString("00");
                }
                if (jan.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jan)).ToString("00");
                }
                if (feb.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(feb)).ToString("00");
                }
                if (mar.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(mar)).ToString("00");
                }
                if (apr.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(apr)).ToString("00");
                }
                if (may.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(may)).ToString("00");
                }

                if (nov.ToLower().Equals("ab") && dec.ToLower().Equals("ab") && jan.ToLower().Equals("ab") && feb.ToLower().Equals("ab") && mar.ToLower().Equals("ab") && apr.ToLower().Equals("ab") && may.ToLower().Equals("ab"))
                {
                    total1TextBox.Text = "AB";
                }
                else
                {
                    total1TextBox.Text = total.ToString();
                }

                if (total1.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total1)).ToString("00");
                }
                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && total1.ToLower().Equals("ab"))
                {
                    grantotalTextBox.Text = "AB";
                }
                else
                {
                    grantotalTextBox.Text = grandtotal.ToString();
                }


            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMArk.apr_TextChanged", ex);
            }
        }

        protected void may_TextChanged(object sender, EventArgs e)
        {
            string total = "0", grandtotal = "0";
            try
            {
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox novTextBox = (TextBox)row.FindControl("nov");
                TextBox decTextBox = (TextBox)row.FindControl("dec");
                TextBox janTextBox = (TextBox)row.FindControl("jan");
                TextBox febTextBox = (TextBox)row.FindControl("feb");
                TextBox marTextBox = (TextBox)row.FindControl("mar");
                TextBox aprTextBox = (TextBox)row.FindControl("apr");
                TextBox mayTextBox = (TextBox)row.FindControl("may");
                TextBox total1TextBox = (TextBox)row.FindControl("total1");
                TextBox totalTextBox = (TextBox)row.FindControl("total");
                TextBox grantotalTextBox = (TextBox)row.FindControl("grandtotal");



                string nov = novTextBox.Text;
                string dec = decTextBox.Text;
                string jan = janTextBox.Text;
                string feb = febTextBox.Text;
                string mar = marTextBox.Text;
                string apr = aprTextBox.Text;
                string may = mayTextBox.Text;
                string total1 = totalTextBox.Text;



                if (nov.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(nov)).ToString("00");
                }
                if (dec.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(dec)).ToString("00");
                }
                if (jan.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(jan)).ToString("00");
                }
                if (feb.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(feb)).ToString("00");
                }
                if (mar.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(mar)).ToString("00");
                }
                if (apr.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(apr)).ToString("00");
                }
                if (may.All(char.IsDigit))
                {
                    total = (Convert.ToDouble(total) + Convert.ToDouble(may)).ToString("00");
                }

                if (nov.ToLower().Equals("ab") && dec.ToLower().Equals("ab") && jan.ToLower().Equals("ab") && feb.ToLower().Equals("ab") && mar.ToLower().Equals("ab") && apr.ToLower().Equals("ab") && may.ToLower().Equals("ab"))
                {
                    total1TextBox.Text = "AB";
                }
                else
                {
                    total1TextBox.Text = total.ToString();
                }

                if (total1.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total1)).ToString("00");
                }
                if (total.All(char.IsDigit))
                {
                    grandtotal = (Convert.ToDouble(grandtotal) + Convert.ToDouble(total)).ToString("00");
                }

                if (total.ToLower().Equals("ab") && total1.ToLower().Equals("ab"))
                {
                    grantotalTextBox.Text = "AB";
                }
                else
                {
                    grantotalTextBox.Text = grandtotal.ToString();
                }


            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMArk.may_TextChanged", ex);
            }
        }

        protected void total1_TextChanged(object sender, EventArgs e)
        {

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


        protected void SaveAttMarks_ServerClick(object sender, EventArgs e)
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
                    string std = cmbStd.SelectedValue.ToString();
                    string div = cmbDiv.SelectedValue.ToString();
                    string exam = cmbexam.SelectedValue.ToString();


                    foreach (GridViewRow row in GridCollection.Rows)
                    {
                        string rollno = row.Cells[0].Text;
                        string grno = row.Cells[1].Text;
                        string StudentName = row.Cells[2].Text;
                        string jun = ((TextBox)row.FindControl("jun")).Text;
                        string jul = ((TextBox)row.FindControl("jul")).Text;
                        string aug = ((TextBox)row.FindControl("aug")).Text;
                        string sep = ((TextBox)row.FindControl("sep")).Text;
                        string oct = ((TextBox)row.FindControl("oct")).Text;
                        string total = ((TextBox)row.FindControl("total")).Text;
                        string nov = ((TextBox)row.FindControl("nov")).Text;
                        string dec = ((TextBox)row.FindControl("dec")).Text;
                        string jan = ((TextBox)row.FindControl("jan")).Text;
                        string  feb = ((TextBox)row.FindControl("feb")).Text;
                        string mar = ((TextBox)row.FindControl("mar")).Text;
                        string apr = ((TextBox)row.FindControl("apr")).Text;
                        string may = ((TextBox)row.FindControl("may")).Text;
                        string total1 = ((TextBox)row.FindControl("total")).Text;
                        string gtotal = ((TextBox)row.FindControl("grandtotal")).Text;

                        query = "Select Count(*) From Attendance where std='" + std + "' and div='" + div + "' and grno='"+grno+"';";
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            count = Convert.ToInt32(reader[0]);
                        }
                        reader.Close();

                        if (count == 0)
                        {
                            query = "insert into Attendance(rollno,grno,studentname,std,div,june,july,aug,sep,oct,total1,nov,dec,jan,feb,march,april,may,total2,gtotal,createddate,createdby,Academicyear) "
                            + "values('" + rollno + "','" + grno + "','" + checkApostrophee(StudentName) + "','" + std + "','" + div + "','" + jun + "','" + jul + "','" + aug + "','" + sep + "','" + oct + "','" + total + "','" + nov + "','" + dec + "','" + jan + "'," +
                            "'" + feb + "','" + mar + "','" + apr + "','" + may + "','" + total1 + "','" + gtotal + "','" + cdt + "','" + usercode + "','" + lblacademicyear.Text + "');";

                        }
                        else
                        {
                            query = "update Attendance set rollno='" + rollno + "',grno='" + grno + "',studentname='" + checkApostrophee(StudentName) + "',std='" + std + "',div='" + div + "',june='" + jun + "'" +
                            ",july='" + jul + "',aug='" + aug + "',sep='" + sep + "',oct='" + oct + "',total1='" + total + "',nov='" + nov + "',dec='" + dec + "'" +
                            ",jan='" + jan + "',feb='" + feb + "',march='" + mar + "',april='" + apr + "',may='" + may + "',total2='" + total1 + "',gtotal='" + gtotal + "', updateddate='" + cdt + "', updatedby='" + usercode + "'" +
                            " where std='" + std + "' and grno='" + grno + "' and Academicyear='" + lblacademicyear.Text + "';";


                        }
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();

                        lblinfomsg.Text = "attendance  Saved Successfully.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);

                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error("attendancemarks.SaveAttMarks_ServerClick", ex);
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
                    academicyear = lblacademicyear.Text;

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