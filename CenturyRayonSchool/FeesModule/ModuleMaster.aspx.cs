//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.FeesModule
{
    public partial class ModuleMaster : System.Web.UI.Page
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

                    string query = "select distinct ModuleName from ModuleMaster;";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    DataTable module = new DataTable();
                    adap.Fill(module);
                    module.Rows.Add("Select ModuleName");
                    listboxModule.DataSource = module;
                    listboxModule.DataBind();
                    listboxModule.DataTextField = "ModuleName";
                    listboxModule.DataValueField = "ModuleName";
                    listboxModule.DataBind();
                    listboxModule.SelectedValue = "Select ModuleName";

                    query = "select UserId,UserName from Login;";
                    adap = new SqlDataAdapter(query, con);
                    DataTable uid = new DataTable();
                    adap.Fill(uid);
                    //uid.Rows.Add("Select User Name");
                    cmbusr.DataSource = uid;
                    cmbusr.DataBind();
                    cmbusr.DataTextField = "UserName";
                    cmbusr.DataValueField = "UserId";
                    cmbusr.DataBind();
                    cmbusr.SelectedValue = "Select User Name";

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
                    string query = "select userid,UserName,modulename from LoginModule order by userid ";
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



        protected void usrnmeCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmbusr.SelectedValue.ToString().Equals("Select User Name"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void moduleCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (listboxModule.SelectedValue.ToString().Equals("Select ModuleName"))
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
            string username = cmbusr.SelectedItem.Text;
            string userid = cmbusr.SelectedValue;
            // Get the selected items from the listboxModule
            foreach (ListItem item in listboxModule.Items)
            {
                if (item.Selected)
                {
                    // Save or process the selected item (item.Text or item.Value) as needed
                    string selectedItemValue = item.Value;
                    SaveData(username, userid, selectedItemValue);
                }
            }

        }
        private void SaveData(string username, string userid, string selectedItemValue)
        {
            SqlConnection con = null; int count = 0;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "select Count(*) from Loginmodule where userid='" + userid + "' and modulename='" + selectedItemValue + "' and UserName='" + username + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader readr = cmd.ExecuteReader();
                    while (readr.Read())
                    {
                        count = Convert.ToInt32(readr[0]);
                    }
                    readr.Close();
                    if (selectedItemValue != "Select ModuleName")
                    {
                        if (count == 0)
                        {

                            query = "insert into Loginmodule (userid,UserName,modulename) values ('" + userid + "','" + username + "','" + selectedItemValue + "')";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();

                            LoadGrid();
                            lblinfomsg.Text = "Saved  Successfully.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                        }
                        else
                        {
                            LoadGrid();
                            lblalertmessage.Text = "Already Access Given.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                            //Response.Redirect(Request.RawUrl);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Log.Error("ModuleMaster.SaveData", ex);
                Log.Info($"Saving Data - Text: {username}, Value: {selectedItemValue}");
            }
        }
    }
}