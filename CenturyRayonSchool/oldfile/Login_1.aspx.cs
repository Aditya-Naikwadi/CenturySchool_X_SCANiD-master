using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace CenturyRayonSchool
{
    public partial class Login_1 : System.Web.UI.Page
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


                    string query = "select userid,username,usertype,iswebadmin from Login where username='" + Uname.Text + "' and password='" + Upass.Text + "'";

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



                        Session["userid"] = userid;
                        Session["username"] = uname;
                        Session["usertype"] = usertype;
                        Session["listmodules"] = new RightModel().getModuleList(userid);


                        resp = "ok";




                    }
                    else
                    {
                        Label1.Text = "You're username and Password is incorrect";
                        errorMessage.Text = "You're username and Password is incorrect";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showErrorModal()", true);
                        resp = "error";

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
                    if (iswebadmin == "1")
                    {
                        Response.Redirect("FeesModule/Dashboard.aspx");
                    }
                    else
                    {
                        Response.Redirect("Photogallery.aspx");
                    }

                }

            }
        }
    }
}