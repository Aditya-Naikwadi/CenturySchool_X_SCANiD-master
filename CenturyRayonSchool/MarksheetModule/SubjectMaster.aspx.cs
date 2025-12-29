using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CenturyRayonSchool.MarksheetModule
{
    public partial class SubjectMaster : System.Web.UI.Page
    {
        DataTable subjectmaster = new DataTable();
        Label lblusercode = new Label();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblusercode = (Label)Page.Master.FindControl("lblusercode");
            string year = new FeesModel().setActiveAcademicYear();
            lblAcademicyear.Text = year;

            if (!IsPostBack)
            {
                loadFormControl();

            }

            subjectmaster.Columns.Add("Srno");
            subjectmaster.Columns.Add("Subject");
            subjectmaster.Columns.Add("minmarks");
            subjectmaster.Columns.Add("maxmarks");
            subjectmaster.Columns.Add("check");
            subjectmaster.Columns.Add("grade");
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
                    cmbstd.DataSource = std;
                    cmbstd.DataBind();
                    cmbstd.DataTextField = "std";
                    cmbstd.DataValueField = "std";
                    cmbstd.DataBind();
                    cmbstd.SelectedValue = "Select Std";




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


                    SubjectCollection.DataSource = subjectmaster;
                    SubjectCollection.DataBind();

                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMaster.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void ShowSubj_ServerClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                fillGridView();
            }
        }

        public void fillGridView()
        {

            DataTable subject_table = new DataTable();
            SqlConnection con = null;
            try
            {
                DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                string query = "", select_std = "", select_exam = "";


                select_std = cmbstd.SelectedValue.ToString();
                select_exam = cmbexam.SelectedValue.ToString();

                using (con = Connection.getConnection())
                {
                    con.Open();

                    fetchdata(cmbstd.Text, cmbexam.Text, con);

                    query = "Select [subject] From subjects where [subject] NOT IN (Select [subject] From SubjectMaster where std='" + cmbstd.Text + "' and examname='" + cmbexam.Text + "');";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        subjectmaster.Rows.Add("0", reader[0].ToString(), "0", "0");
                    }

                    reader.Close();
                    SubjectCollection.DataSource = subjectmaster;
                    SubjectCollection.DataBind();

                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMaster.fillGridView", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

                subject_table.Dispose();
            }
        }

        public void fetchdata(string std, string examname, SqlConnection con)
        {
            Boolean check, grade;
            string query = "Select Cast([srno] as int) as srno,[subject],[minmarks],[maxmarks],[check],[grade] From SubjectMaster where std='" + std + "' and examname='" + examname + "' order by srno asc;";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (reader[4].ToString() == "1")
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
                if (reader[5].ToString() == "1")
                {
                    grade = true;
                }
                else
                {
                    grade = false;
                }

                subjectmaster.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), check, grade);
                //SubjectCollection.DataSource = subjectmaster;
                //SubjectCollection.DataBind();
            }

            reader.Close();

        }

        protected void Savesubj_ServerClick(object sender, EventArgs e)
        {
            string usercode = lblusercode.Text;
            SqlConnection con = null;
            try
            {

                using (con = Connection.getConnection())
                {
                    con.Open();
                    String query = "", grade = "", check = "";
                    SqlCommand cmd = null;
                    DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();

                    query = "Delete From subjectMaster where std='" + cmbstd.Text + "' and examname='" + cmbexam.Text + "';";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    foreach (GridViewRow row in SubjectCollection.Rows)
                    {
                        grade = "";
                        if (((CheckBox)row.FindControl("chkSelect")).Checked)
                        {
                            if (((CheckBox)row.FindControl("gradeSelect")).Checked)
                            {
                                grade = "1";
                            }
                            string SRNO = ((TextBox)row.FindControl("txtsrno")).Text;
                            string minmarks = ((TextBox)row.FindControl("txtminmarks")).Text;
                            string maxmarks = ((TextBox)row.FindControl("txtmaxmarks")).Text;

                            query = "insert into SubjectMaster([srno],[subject],[std],[examname],[minmarks],[maxmarks],[check],[grade],[CreatedDate],[CreatedBy]) " +
                                    "values('" + SRNO + "','" + row.Cells[1].Text.ToString() + "','" + cmbstd.Text + "','" + cmbexam.Text + "','" + minmarks + "','" + maxmarks + "','1','" + grade + "','" + cdt + "','" + usercode + "');";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();

                            lblinfomsg.Text = "Subjects Saved Successfully.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                Log.Error("SubjectMaster.Savesubj_ServerClick", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }


            }

        }

        public Boolean setChecked(string status)
        {
            if (status == "False")
            {
                return false;
            }
            else if (status == "True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Boolean setgradeCheck(string status)
        {
            if (status == "False")
            {
                return false;
            }
            else if (status == "True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void txtsrno_TextChanged(object sender, EventArgs e)
        {

        }
    }
}