//using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace CenturyRayonSchool
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn1_Login_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            string resp = "error";
            DataTable dt = new DataTable();
            string iswebadmin = "";
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string uname = "";
                    string userid = "";
                    string usertype = "";
                    string personname = "";
                    string std = "";
                    string div = "";
                    string year = "";

                    string query = "select userid,username,usertype,iswebadmin,personname from Login where username='" + Uname.Text + "' and password='" + Upass.Text + "'";

                    SqlCommand cmd = new SqlCommand(query, con);
                    //select [UserId] [UserName] [Password] [CreatedDate] [CreateedBy] [UserType] from Login where UserName='" + Uname.Text + "' and Password='" + Upass.Text + "'", con
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);


                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            uname = row["UserName"].ToString();
                            userid = row["UserId"].ToString();
                            usertype = row["UserType"].ToString();
                            iswebadmin = row["iswebadmin"].ToString();
                            personname = row["personname"].ToString();
                        }





                        DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();

                        //add to User logs
                        query = "insert into UserLoginLog([date1],[UserId],[UserName],[logontype],[logondatetime]) values(@date1,@UserId,@UserName,@logontype,@logondatetime);";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@date1", cdt.ToString("yyyy/MM/dd"));
                        cmd.Parameters.AddWithValue("@UserId", userid);
                        cmd.Parameters.AddWithValue("@UserName", uname);
                        cmd.Parameters.AddWithValue("@logontype", "login");
                        cmd.Parameters.AddWithValue("@logondatetime", cdt.ToString("yyyy/MM/dd hh:mm:ss tt"));
                        cmd.ExecuteNonQuery();

                        query = "select year from academicyear where  iscurrentyear='1'  ";
                        cmd = new SqlCommand(query, con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            year = reader["year"].ToString();
                        }
                        reader.Close();

                        query = "select std,div from TeacherMapping where UserId='" + userid + "' and Academicyear ='"+ year + "' ";
                        cmd = new SqlCommand(query, con);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            std = reader["std"].ToString();
                            div = reader["div"].ToString();
                        }
                        reader.Close();


                        Session["userid"] = userid;
                        Session["username"] = uname;
                        Session["usertype"] = usertype;
                        Session["personname"] = personname;
                        Session["std"] = std;
                        Session["div"] = div;
                        Session["listmodules"] = new RightModel().getModuleList(userid);


                        if (userid == "138")
                        {
                            resp = "ok1";
                        }
                        else if (userid == "139")
                        {
                            resp = "ok2";
                        }
                        else
                        {
                            resp = "ok";

                        }


                    }
                    else
                    {
                        resp = "error";
                        lblalertmessage.Text = "Your username and Password is incorrect";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);


                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("Login.btn1_Login_Click", ex);
            }
            finally
            {
                if (con != null) con.Close();

                if (dt != null) dt.Dispose();

                if (resp == "ok")
                {
                    if (iswebadmin == "0")
                    {
                        Response.Redirect("FeesModule/SelectModule.aspx");
                    }
                    else
                    {
                        Response.Redirect("Photogallery.aspx");
                    }

                }
                else if (resp == "ok1")
                {
                    if (iswebadmin == "0")
                    {
                        Response.Redirect("MarksheetModule/Dashboard.aspx");
                    }
                    else
                    {
                        Response.Redirect("Photogallery.aspx");
                    }

                }
                else if (resp == "ok2")
                {
                    if (iswebadmin == "0")
                    {
                        Response.Redirect("MarksheetModule/Dashboard.aspx");
                    }
                    else
                    {
                        Response.Redirect("Photogallery.aspx");
                    }

                }

            }
        }

        // Show the forgot password panel and hide the login panel
        protected void lnkForgotPassword_Click(object sender, EventArgs e)
        {
            LoginPanel.Visible = false;
            ForgotPasswordPanel.Visible = true;
            lnkForgotPassword.Visible = false;
        }

        // Show the login panel and hide the forgot password panel
        protected void btnBack_Click(object sender, EventArgs e)
        {
            LoginPanel.Visible = true;
            ForgotPasswordPanel.Visible = false;
            lnkForgotPassword.Visible = true;

            // Clear the forgot password fields
            txtForgotUsername.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
        }

        // Process the password reset
        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                string username = txtForgotUsername.Text.Trim();
                string newPassword = txtNewPassword.Text.Trim();

                using (con = Connection.getConnection())
                {
                    con.Open();

                    // Check if the username exists
                    string checkQuery = "SELECT COUNT(*) FROM Login WHERE username='" + username + "'";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    int userCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (userCount == 0)
                    {
                        lblalertmessage.Text = "Username not found. Please check and try again.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                        return;
                    }

                    // Update the password
                    string updateQuery = "UPDATE Login SET password='" + newPassword + "' WHERE username='" + username + "'";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Password updated successfully
                        lblalertmessage.Text = "Password has been reset successfully. Please login with your new password.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);

                        // Clear fields and show login panel
                        txtForgotUsername.Text = "";
                        txtNewPassword.Text = "";
                        txtConfirmPassword.Text = "";
                        LoginPanel.Visible = true;
                        ForgotPasswordPanel.Visible = false;
                        lnkForgotPassword.Visible = true;
                    }
                    else
                    {
                        lblalertmessage.Text = "Error resetting password. Please try again.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblalertmessage.Text = "An error occurred: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                Log.Error("Login.btnResetPassword_Click", ex);
            }
            finally
            {
                if (con != null) con.Close();
            }
        }
    }
}