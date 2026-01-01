//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.MarksheetModule
{
    public partial class WorkingDays : System.Web.UI.Page
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

        protected void gridHeaders_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            SqlConnection con = null;

            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    if (e.CommandName == "deletedays")
                    {
                        int count = 0;
                        int rownumber = Convert.ToInt32(e.CommandArgument);

                        GridViewRow row = gridHeaders.Rows[rownumber];
                        string std = row.Cells[0].Text;
                        string month = row.Cells[1].Text;
                        string total = row.Cells[2].Text;


                        using (con = Connection.getConnection())
                        {
                            con.Open();
                            string query = "";


                            query = "Delete from WorkingDays where std='" + std + "' and month='" + month + "' and totaldays='" + total + "';";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();

                            LoadGrid();


                        }


                    }
                }


            }
            catch (Exception ex)
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
                    string query = "Select [std],[Month],[TotalDays] From WorkingDays";
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



                }
            }
            catch (Exception ex)
            {
                Log.Error("PrintMarksheet.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void SaveData_ServerClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SqlConnection con = null;
                try
                {
                    using (con = Connection.getConnection())
                    {
                        con.Open();


                        DateTime cdt = DateTime.Now;
                        string usercode = lblusercode.Text;
                        int count = 0; String query = "", std = "", monthname = "", total = "";
                        std = cmbStd.SelectedValue.ToString();
                        monthname = month.SelectedValue.ToString();
                        total = txtworkday.Text;
                        query = "Select Count(*) from WorkingDays  where std='" + std + "' and month='" + monthname + "' ";
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            count = Convert.ToInt32(reader[0]);
                        }
                        reader.Close();

                        if (count == 0)
                        {
                            query = "insert into workingdays(std,month,totaldays,CreatedDate,CreatedBy) values('" + std + "','" + monthname + "','" + total + "','" + cdt + "','" + usercode + "');";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteReader();

                            lblinfomsg.Text = "Saved  Successfully.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                        }
                        else
                        {
                            query = "update WorkingDays set totaldays='" + total + "',updateddate='" + cdt + "',updatedby='" + usercode + "' where std='" + std + "' and month='" + monthname + "';";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteReader();
                            lblinfomsg.Text = "Updated  Successfully.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                        }



                        LoadGrid();

                        con.Close();


                    }

                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {

                        Log.Error("WorkingDays.SaveData_ServerClick", ex);
                    }
                    else
                    {

                        Log.Error("WorkingDays.SaveData_ServerClick", ex);
                    }

                }
                catch (Exception ex)
                {

                    Log.Error("WorkingDays.SaveData_ServerClick", ex);
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

        protected void MonthCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmbStd.SelectedValue.ToString().Equals("Select Month"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

    }
}