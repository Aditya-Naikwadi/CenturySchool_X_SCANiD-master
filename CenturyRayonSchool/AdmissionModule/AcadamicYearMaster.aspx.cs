using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;

namespace CenturyRayonSchool.AdmissionModule
{
    public partial class AcadamicYearMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            {
                using (SqlCommand cmd = new SqlCommand("Select id, academicyear, active, createdatetime from AdmissionAcademicYear order by id desc  "))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);



                            if (dt.Rows.Count <= 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No record Present')", true);
                            }
                            else
                            {
                                gvacadamicyearList.DataSource = dt;
                                gvacadamicyearList.DataBind();
                            }
                            lbltotal.InnerText = gvacadamicyearList.Rows.Count.ToString();
                        }
                    }
                }
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtacadmicyear.Text==""|| txtacadmicyear.Text == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Acadamic year.')", true);
                    chkyear.Checked = false;
                    return;
                }
                else
                {

                
                string status = "", date = "";
                String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionstring);
                con.Open();


                if (chkyear.Checked == true)
                {
                    status = "True";
                }
                else
                {
                    status = "False";
                }
                date = DateTime.Now.ToString();
               // string query = "";
                //query = "";
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM AdmissionAcademicYear WHERE  academicyear = @year", con);
                cmd1.Parameters.AddWithValue("@year", txtacadmicyear.Text);
                int totalyear = (int)cmd1.ExecuteScalar();
                if (totalyear > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Academic Year " + txtacadmicyear.Text + " already present in table. Please approve Academic Year')", true);
                    return;
                }
                SqlCommand cmd = new SqlCommand();
                string query = "insert into AdmissionAcademicYear (academicyear,active,createdatetime) " +
                            "values(@academicyear,@status,@datetime)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@academicyear", txtacadmicyear.Text);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@datetime", date);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Inserted sucessfully')", true);
                this.BindGrid();
                txtacadmicyear.Text = "";
                chkyear.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Something Went wrong.Please contact adminstrative')", true);
            }
        }

        protected void acadamicyearList_RowCommand(object sender, GridViewCommandEventArgs e)
            {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvacadamicyearList.Rows[rowIndex];
            string yearID = row.Cells[0].Text;

            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            if (e.CommandName == "Deactivate")
            {
                string query = "Update AdmissionAcademicYear set active = @ApprovalStatus where id = " + yearID + "";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ApprovalStatus", "False");
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Year Deactivate sucessfully')", true);
                BindGrid();
            }

            else if (e.CommandName == "Activate")
            {
                string query = "Update AdmissionAcademicYear set active = @ApprovalStatus where id = " + yearID + "";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ApprovalStatus", "True");
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Year Activate sucessfully')", true);
                BindGrid();
            }
        }

        protected void gvacadamicyearList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string approvalstatus = DataBinder.Eval(e.Row.DataItem, "active") as string;

                Button btnActivate = e.Row.FindControl("btnactive") as Button;
                Button btnDeactivate = e.Row.FindControl("btndeactive") as Button;

                if (btnActivate != null && btnDeactivate != null)
                {
                    if (approvalstatus.Equals("True", StringComparison.OrdinalIgnoreCase))
                    {
                        btnActivate.Visible = false;
                    }
                    else
                    {
                        btnDeactivate.Visible = false;
                    }
                }
            }
        }
    }
}