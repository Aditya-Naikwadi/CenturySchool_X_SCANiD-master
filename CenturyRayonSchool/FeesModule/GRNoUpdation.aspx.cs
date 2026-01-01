//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.FeesModule
{
    public partial class GRNoUpdation : System.Web.UI.Page
    {
        DataTable Student = new DataTable();
        Label lblusercode = new Label();
        string div_sess = "", std_sess = "";
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
            Student.Columns.Add("oldGRNO");
            Student.Columns.Add("StudentName");
            Student.Columns.Add("std");
            Student.Columns.Add("div");
            Student.Columns.Add("schoolsection");
            Student.Columns.Add("newgrno");
            Student.Columns.Add("grstats");
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
                    cmbStd.SelectedValue = "Select Std";

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

                    query = "select grno,fullname from studentmaster where std='" + std_sess + "' and div='" + div_sess + "' and academicyear='" + lblacademicyear.Text + "' and (leftstatus IS NULL OR leftstatus = '') ";
                    adap = new SqlDataAdapter(query, con);
                    DataTable studtable = new DataTable();
                    adap.Fill(studtable);

                    studtable.Rows.Add("ALL", "ALL");
                    cmbstudentname.DataSource = studtable;
                    cmbstudentname.DataBind();
                    cmbstudentname.DataTextField = "fullname";
                    cmbstudentname.DataValueField = "grno";
                    cmbstudentname.DataBind();
                    cmbstudentname.SelectedValue = "ALL";
                }
            }
            catch (Exception ex)
            {
                Log.Error("GRNOupdation.loadFormControl", ex);
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
                Log.Error("GRNOupdation.cmbDiv_SelectedIndexChanged", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void getdata_ServerClick(object sender, EventArgs e)
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



                select_std = cmbStd.SelectedValue.ToString();
                select_div = cmbDiv.SelectedValue.ToString();

                year = lblacademicyear.Text.ToString();

                studentname = cmbstudentname.SelectedValue.ToString();
                using (con = Connection.getConnection())
                {
                    con.Open();

                    if (studentname.Equals("ALL"))
                    {
                        query = "select GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,schoolsection from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + year + "' and (leftstatus IS NULL OR leftstatus = '')  order by Cast(ROLLNO as int) asc;";
                    }
                    else
                    {
                        query = "select GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,schoolsection from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + year + "' and grno='" + studentname + "' and (leftstatus IS NULL OR leftstatus = '')  order by Cast(ROLLNO as int) asc;";
                    }
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Student.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
                    }
                    reader.Close();

                    GridCollection.DataSource = Student;
                    GridCollection.DataBind();


                }
            }
            catch (Exception ex)
            {
                Log.Error("GRNoUpdation.fillGridView", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
                stud_tbl.Dispose();
                Student.Dispose();
            }
        }


        protected void newgrno_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            try
            {
                string query = "", oldgrno = "", year = "", std = "", div = "", schoolsection = "";
                int count = 0;
                GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox newgr = (TextBox)row.FindControl("newgrno");
                string newgrno = newgr.Text;
                TextBox stats = (TextBox)row.FindControl("grstats");


                year = lblacademicyear.Text.ToString();

                using (con = Connection.getConnection())
                {
                    con.Open();
                    if (newgrno.All(char.IsDigit))
                    {

                        std = row.Cells[2].Text.ToString();
                        div = row.Cells[3].Text.ToString();
                        oldgrno = row.Cells[0].Text.ToString();
                        schoolsection = row.Cells[4].Text.ToString();

                        query = "select Count(*) as cnt from studentmaster where schoolsection='" + schoolsection + "' and grno='" + newgrno + "' and academicyear='" + year + "';";
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            count = Convert.ToInt32(reader["cnt"]);
                        }
                        reader.Close();
                        if (count == 0)
                        {
                            query = "update studentmaster set grno='" + newgrno + "' where std='" + std + "' and div='" + div + "' and grno='" + oldgrno + "' and academicyear='" + year + "';";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();

                            query = "update studentfees  set grno='" + newgrno + "' where std='" + std + "'  and grno='" + oldgrno + "' and academicyear='" + year + "';";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();

                            query = "update ReceiptReport  set grno='" + newgrno + "' where std='" + std + "' and grno='" + oldgrno + "' and academicyear='" + year + "';";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();

                            query = "update StudentMarksheet  set grno='" + newgrno + "' where std='" + std + "'  and grno='" + oldgrno + "' and academicyear='" + year + "';";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();

                            query = "update StudentMarksheet8TO9  set grno='" + newgrno + "' where std='" + std + "'and grno='" + oldgrno + "' and academicyear='" + year + "';";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();

                            query = "update Attendance  set grno='" + newgrno + "' where std='" + std + "'  and grno='" + oldgrno + "' and academicyear='" + year + "';";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();

                            query = "update remark  set grno='" + newgrno + "' where std='" + std + "'  and grno='" + oldgrno + "' and academicyear='" + year + "';";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();


                            stats.Text = "GRNO Saved";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("GRNoUpdation.newrollno_TextChanged", ex);

            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }






    }
}