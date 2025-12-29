using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace CenturyRayonSchool.ParentsModule
{
    public partial class ParentsForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //RequiredFieldValidator3.Visible = true;
                //pwd.Visible = true;
                //Confirmpwd.Visible = true;
                loadFormControl();

            }
        }
        //protected void stdCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    if (cmbStd.SelectedValue.ToString().Equals("Select Std"))
        //    {
        //        args.IsValid = false;
        //    }
        //    else
        //    {
        //        args.IsValid = true;
        //    }
        //}

        public void loadFormControl()
        {
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string year = "";
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

                    query = "select year from academicyear where iscurrentyear='1'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        year = reader["year"].ToString();
                    }
                    reader.Close();
                    Academic.Text = year;

                    cmbStd.SelectedValue = "Select Std";

                }
            }
            catch (Exception ex)
            {
                Log.Error("ParentsForgotPassword.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        //protected void Confirmpwd_TextChanged(object sender, EventArgs e)
        //{
        //    if(Confirmpwd.Text == pwd.Text)
        //    {

        //    }
        //    else
        //    {
        //        Pwd_NotMatch.Text = "Password Does Not Match";
        //        Confirmpwd.Text = "";
        //        pwd.Text = "";
        //        Pwd_NotMatch.ForeColor = System.Drawing.Color.Red;
        //        RequiredFieldValidator3.Visible = false;
        //    }
        //}

        protected void btn_ChangePassword_Click(object sender, EventArgs e)
        {
            SqlConnection con = null; int count = 0;
            string query = "", resp = "", cid = "";
            DataTable dt = new DataTable();
            string GRNO = GRNo.Text;
            string password = pwd.Text;
            string year = Academic.Text;
            string confirmpwd = Confirmpwd.Text;
            string std = cmbStd.SelectedValue;
            if (Confirmpwd.Text == pwd.Text) // basappa dt 16.03.2024
            {
                try
                {
                    using (con = Connection.getConnection())
                    {
                        con.Open();
                        query = "select Count(*) from studentmaster where GRNO='" + GRNO + "' and academicyear='" + year + "' and std='" + std + "'";
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            count = Convert.ToInt32(reader[0]);
                        }
                        reader.Close();

                        if (count > 0)
                        {
                            query = "update studentmaster set parentpassword='" + confirmpwd + "' where  GRNO='" + GRNO + "' and academicyear='" + year + "' and std='" + std + "'";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            empty();

                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Password Change Sucessfully');", true);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);

                            //lblalertmessage.Text = "Password Change Sucessfully";
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                        }
                        else
                        {
                            empty();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select proper Grno or standard.');", true);// basappa dt 16.03.2024
                            //lblalertmessage.Text = "Your GRNO OR STD is incorrect";
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                        }
                    }
                }

                catch (Exception ex)
                {
                    Log.Error("ParentsForgotPassword.btn_ChangePassword_Click", ex);
                    lblalertmessage.Text = "Your GRNO OR STD is incorrect" + ex;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Password and Confirm password should match.');", true);// basappa dt 16.03.2024
                //lblalertmessage.Text = "Password and Confirm password should match." + ex;

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                empty();
            }
        }
        protected void empty()// basappa dt 16.03.2024
        {
            cmbStd.SelectedValue = "Select Std";
            GRNo.Text = "";
            pwd.Text = "";
            Confirmpwd.Text = "";
        }
    }

}