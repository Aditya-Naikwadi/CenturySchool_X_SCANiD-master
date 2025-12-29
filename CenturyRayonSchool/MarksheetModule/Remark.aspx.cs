using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.MarksheetModule
{
    public partial class Remark : System.Web.UI.Page
    {
        DataTable remark = new DataTable();
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

            remark.Columns.Add("RollNo");
            remark.Columns.Add("GRNO");
            remark.Columns.Add("StudentName");
            remark.Columns.Add("std");
            remark.Columns.Add("div");
            remark.Columns.Add("special");
            remark.Columns.Add("liking");
            remark.Columns.Add("needs");
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
                    cmbDiv.SelectedValue = "Select Div";


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
                Log.Error("Remark.loadFormControl", ex);
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
                string query = "", select_std = "", select_div = "", studentname = "", exam = "", year = "", freeshipamount = "0", ReceiptDate = "";

                string Amtpaid = "", freeshiptype = "";

                select_std = cmbStd.SelectedValue.ToString();
                select_div = cmbDiv.SelectedValue.ToString();
                exam = cmbexam.SelectedValue.ToString();
                studentname = cmbstudentname.SelectedValue.ToString();
                year = lblacademicyear.Text.ToString();
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
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        remark.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
                    }
                    reader.Close();


                    fetchdata(select_std, select_div, exam, con);


                    GridCollection.DataSource = remark;
                    GridCollection.DataBind();


                }
            }
            catch (Exception ex)
            {
                Log.Error("Remark.fillGridView", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
                stud_tbl.Dispose();
                remark.Dispose();
            }
        }

        public void fetchdata(string std, string div, string examname, SqlConnection con)
        {
            try
            {


                string query = "select Rollno,Grno,Studentname,std,div,specialprog,likinghob,needimprov From Remark where std = '" + std + "' and div = '" + div + "' and examname='" + examname + "' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable subjtable = new DataTable();
                da.Fill(subjtable);

                foreach (DataRow ro in remark.Rows)
                {
                    string standard = ro["std"].ToString();
                    string grno = ro["Grno"].ToString();
                    var dr = subjtable.AsEnumerable().Where(x => x.Field<string>("std").Equals(standard) && x.Field<string>("Grno").Equals(grno)).DefaultIfEmpty(null).FirstOrDefault();

                    if (dr != null)
                    {
                        ro["special"] = dr["specialprog"].ToString();
                        ro["liking"] = dr["likinghob"].ToString();
                        ro["needs"] = dr["needimprov"].ToString();

                    }
                    else
                    {
                        ro["std"] = "-";
                        ro["div"] = "-";
                        ro["special"] = "-";
                        ro["liking"] = "-";
                        ro["needs"] = "-";

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Remark.fetchdata", ex);
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

        protected void SaveRemark_ServerClick(object sender, EventArgs e)
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
                    string year = lblacademicyear.Text;

                    foreach (GridViewRow row in GridCollection.Rows)
                    {
                        string rollno = row.Cells[0].Text;
                        string grno = row.Cells[1].Text;
                        string StudentName = row.Cells[2].Text;
                        string special = ((TextBox)row.FindControl("special")).Text;
                        string liking = ((TextBox)row.FindControl("liking")).Text;
                        string needs = ((TextBox)row.FindControl("needs")).Text;


                        query = "delete From Remark where std='" + std + "' and div='" + div + "' and grno='" + grno + "'  and examname='" + exam + "' and academicyear='" + year + "';";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();

                        query = "insert into Remark(rollno,grno,studentname,std,div,examname,[specialprog],[likinghob],[needimprov],createddate,createdby,academicyear) " +
                            "values('" + rollno + "','" + grno + "','" + checkApostrophee(StudentName) + "','" + std + "','" + div + "','" + exam + "','" + checkApostrophee(special) + "','" + checkApostrophee(liking) + "','" + checkApostrophee(needs) + "','" + cdt + "','" + usercode + "','" + year + "');";

                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();

                        lblinfomsg.Text = "Remark  Saved Successfully.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);

                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error("Remark.SaveAttMarks_ServerClick", ex);
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