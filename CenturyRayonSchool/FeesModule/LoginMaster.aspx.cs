//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.FeesModule
{
    public partial class LoginMaster : System.Web.UI.Page
    {
        Label lblusercode = new Label();
        protected void Page_Load(object sender, EventArgs e)
        {
            string year = new FeesModel().setActiveAcademicYear();
            lblacademicyear.Text = year;

            lblusercode = (Label)Page.Master.FindControl("lblusercode");

            if (!IsPostBack)
            {
                LoadGrid();
                loadFormControl();

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

                    string query = "select distinct UserType from login where UserType <> 'NULL';";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    DataTable ustype = new DataTable();
                    adap.Fill(ustype);
                    ustype.Rows.Add("Select UserType");
                    cmbustype.DataSource = ustype;
                    cmbustype.DataBind();
                    cmbustype.DataTextField = "UserType";
                    cmbustype.DataValueField = "UserType";
                    cmbustype.DataBind();
                    cmbustype.SelectedValue = "Select UserType";

                    query = "select distinct iswebadmin from login where UserType <> 'NULL';";
                    adap = new SqlDataAdapter(query, con);
                    DataTable isweb = new DataTable();
                    adap.Fill(isweb);
                    isweb.Rows.Add("Select Iswebadmin");
                    cmbwebadmin.DataSource = isweb;
                    cmbwebadmin.DataBind();
                    cmbwebadmin.DataTextField = "iswebadmin";
                    cmbwebadmin.DataValueField = "iswebadmin";
                    cmbwebadmin.DataBind();
                    cmbwebadmin.SelectedValue = "Select Iswebadmin";




                }
            }
            catch (Exception ex)
            {
                Log.Error("LoginMaster.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        public void LoadGrid()
        {
            SqlConnection con = null;
            try
            {
                DataTable dt = new DataTable();
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "select UserId,UserName,Password,UserType,iswebadmin,PersonName from login";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(dt);

                    gridHeaders.DataSource = dt;
                    gridHeaders.DataBind();
                }


            }
            catch (Exception ex)
            {
                Log.Error("LoadGrid", ex);

            }
            finally
            {
                if (con != null) { con.Close(); }
            }




        }


        protected void ustypeCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmbustype.SelectedValue.ToString().Equals("Select User Type"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void webCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmbwebadmin.SelectedValue.ToString().Equals("Select Iswebadmin"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void Savedetais_ServerClick(object sender, EventArgs e)
        {
            SqlConnection con = null;
            string query = "", username = "", password = "", personname = "", iswebadmin = "", usertype = "", cdt = "", usercode = "", userid = "";
            int count = 0;
            username = txtusrname.Text.ToString(); ;
            password = txtpass.Text.ToString();
            personname = txtprsoname.Text.ToString();
            iswebadmin = cmbwebadmin.SelectedValue.ToString();
            usertype = cmbustype.SelectedValue.ToString();
            cdt = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            usercode = lblusercode.Text.ToString();
            userid = lblusrid.Text.ToString();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();

                    query = "select Count(*) from Login where UserName='" + username + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();

                    if (count == 0)
                    {
                        query = "insert into Login (UserName,Password,UserType,iswebadmin,PersonName,CreatedDate,CreatedBy)" +
                                "values ('" + username + "','" + password + "','" + usertype + "','" + iswebadmin + "','" + personname + "','" + cdt + "','" + usercode + "')";
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteReader();

                        lblinfomsg.Text = "Saved  Successfully.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                    }
                    else
                    {
                        query = "Update Login Set PersonName='" + personname + "',password='" + password + "' where UserId='" + userid + "' and username='" + username + "'";
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteReader();

                        lblinfomsg.Text = "Updated  Successfully.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                    }

                    LoadGrid();

                    con.Close();


                }


            }
            catch (Exception ex)
            {
                Log.Error("LoadGrid", ex);

            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void gridHeaders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];

            if (e.CommandName == "editlogin")
            {
                int rownumber = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = gridHeaders.Rows[rownumber];
                string userid = row.Cells[0].Text;
                string username = row.Cells[1].Text;
                string password = row.Cells[2].Text;
                string usertype = row.Cells[3].Text;
                string isweb = row.Cells[4].Text;
                string personname = row.Cells[5].Text;

                lblusrid.Text = userid;
                txtusrname.Text = username;
                txtpass.Text = password;
                cmbustype.SelectedValue = usertype;
                cmbwebadmin.SelectedValue = isweb;
                txtprsoname.Text = personname;

                txtusrname.Enabled = false;
                cmbustype.Enabled = false;
                cmbwebadmin.Enabled = false;

            }
        }

        protected void btnreset_ServerClick(object sender, EventArgs e)
        {
            txtusrname.Enabled = true;
            cmbustype.Enabled = true;
            cmbwebadmin.Enabled = true;

            lblusrid.Text = "";
            txtusrname.Text = "";
            txtpass.Text = "";
            cmbustype.SelectedValue = "Select UserType";
            cmbwebadmin.SelectedValue = "Select Iswebadmin";
            txtprsoname.Text = "";
        }
    }
}