using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Configuration;

namespace CenturyRayonSchool.AdmissionModule
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AdminLoginBtn_Click(object sender, EventArgs e)
        {
            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            //con = conn;
            con.Open();
            string userame = "", password = "", query = "";
            query = "select username, password  from Login  where username='adminfees'";
            SqlCommand cmd1 = new SqlCommand(query, con);

           cmd1.ExecuteScalar();

            SqlDataReader reader = cmd1.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userame = reader[0].ToString();
                    password = reader[1].ToString();

                }
            }

            //string AdminEmailID = "jeromedj800@gmail.com";
            //string AdminPassword = "123456";
            //if (adminEmailID.Text == AdminEmailID && adminPassword.Text == AdminPassword)
            if (adminEmailID.Text == userame && adminPassword.Text == password)
            {
                Response.Redirect("ApplicationList.aspx");
            }
            else
            {
                adminPassword.Text = "";
                adminEmailID.Text = "";
                lblMessage.Text = "Invalid credentials, please enter valid Email ID and Password";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);

            }
        }

        //basappa

    }
}