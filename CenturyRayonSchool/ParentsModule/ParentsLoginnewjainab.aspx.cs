//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.ParentsModule
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string std_sess = "", div_sess = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //std_sess = Session["std"].ToString();
            //div_sess = Session["div"].ToString();
            if (!IsPostBack)
            {
                loadFormControl();
                // basappa dt 16.03.2024
                HttpCookie rememberMeCookie = Request.Cookies["rememberMe"];
                if (Request.Cookies["rememberMe"] != null && Request.Cookies["rememberMe"].Value == "true") // basappa dt 16.03.2024
                {

                    if (Session["username"] != null && Session["password"] != null)
                    {
                        GRNo.Text = Session["username"].ToString();
                        pwd.Text = Session["password"].ToString();
                        cmbStd.SelectedValue = Session["std"].ToString();
                        rememberMeCheckbox.Checked = true;
                    }

                }
            }
        }

        protected void btn1_Login_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            string query = "", personname = "", resp = "", cid = "";
            DataTable dt = new DataTable();
            string GRNO = GRNo.Text.Trim();
            string password = pwd.Text.Trim();
            string year = Academicyear.Text;
            string std = cmbStd.SelectedValue;
            if (string.IsNullOrEmpty(GRNO) || string.IsNullOrEmpty(password))
            {
                lblalertmessage.Text = "Please enter both GR No and Password.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showAlertModal();", true);



                return;
            }

            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "select (mname+' '+lname) as fullname,GRNO,std,ParentPassword,academicyear,cid as userid from studentmaster where academicyear='" + year + "' and ParentPassword='" + password + "' and grno='" + GRNO + "' and std='" + std + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            std = row["std"].ToString();
                            password = row["ParentPassword"].ToString();
                            year = row["academicyear"].ToString();
                            GRNO = row["GRNO"].ToString();
                            personname = row["fullname"].ToString();
                            cid = row["userid"].ToString();
                        }
                        Session["std"] = std;
                        Session["ParentPassword"] = password;
                        Session["academicyear"] = year;
                        Session["fullname"] = personname;
                        Session["GRNO"] = GRNO;
                        Session["userid"] = cid;

                        if (rememberMeCheckbox.Checked) // basappa dt 16.03.2024
                        {
                            HttpCookie rememberMeCookie = new HttpCookie("rememberMe");
                            rememberMeCookie.Value = "true";
                            Response.Cookies.Add(rememberMeCookie);
                            Response.Cookies["rememberMe"].Expires = DateTime.Now.AddDays(30);
                            Session["username"] = GRNo.Text;
                            Session["password"] = pwd.Text;
                        }
                        else
                        {
                            if (Request.Cookies["rememberMe"] != null)
                            {
                                Response.Cookies["rememberMe"].Expires = DateTime.Now.AddDays(-1);
                            }
                            Session.Remove("username");
                            Session.Remove("password");
                        }
                        resp = "ok";
                    }
                    else
                    {
                        resp = "error";
                        lblalertmessage.Text = "Your username and Password is incorrect";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showAlertModal();", true);


                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("ParentsLogin.btn1_Login_Click", ex);
            }
            finally
            {
                if (con != null) con.Close();

                if (dt != null) dt.Dispose();


                if (resp == "ok")
                {
                    Response.Redirect("ParentsDashboard.aspx");
                }
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
                    Academicyear.Text = year;

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
                }
            }
            catch (Exception ex)
            {
                Log.Error("ParentsLogin.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }
    }
}