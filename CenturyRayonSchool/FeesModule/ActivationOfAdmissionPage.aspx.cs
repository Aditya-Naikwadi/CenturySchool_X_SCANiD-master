using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.FeesModule
{
    public partial class ActivationOfAdmissionPage : System.Web.UI.Page
    {
        Label lblusercode = new Label();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblusercode = (Label)Page.Master.FindControl("lblusercode");
            if (!IsPostBack)
            {
                string year = new FeesModel().setActiveAcademicYear();
                lblAcademicyear.Text = year;

                BindGrid();


            }
        }

        protected void saveAcademic_ServerClick(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                string status = "true", query = "";
                string startdate = "";
                string enddate = "";
                string startusforadmissionpage = "";
                string EnteredDate = "";
                string year = "";
                string yearcurrent = "";
                using (con = Connection.getConnection())
                {
                    string Date = DateTime.Now.ToString();
                    con.Open();
                    //string academicyear = txtAcademicYear.Text;
                    Boolean isActive = chkIsActive.Checked;
                    string startDateText = txtadmissionstartdate.Text;
                    string endDateText = txtadmissionenddate.Text;
                    DateTime startDate;
                    DateTime endDate;
                    DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                    if (!DateTime.TryParse(startDateText, out startDate) || !DateTime.TryParse(endDateText, out endDate))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid date format.')", true);
                        return;
                    }

                    if (startDate >= endDate)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Start date should be less than end date.')", true);
                        return;
                    }

                    query = "select year from Academicyear where iscurrentyear=1; ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        yearcurrent = reader[0].ToString();
                    }
                    reader.Close();

                    query = "select count(*),year From Academicyear where year='" + yearcurrent + "' Group by year; ";
                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dt = new DataTable();
                        // insert into LogAdmissionPageRights code started

                        query = "select year,startdate,enddate,startusforadmissionpage,EnteredDate from Academicyear where year='" + yearcurrent + "' ";
                        cmd = new SqlCommand(query, con);
                        adap = new SqlDataAdapter(cmd);
                        adap.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                year = row["year"].ToString();
                                startdate = row["startdate"].ToString();
                                enddate = row["enddate"].ToString();
                                startusforadmissionpage = row["startusforadmissionpage"].ToString();
                                EnteredDate = row["EnteredDate"].ToString();
                            }
                        }


                        //add to User logs
                        query = "insert into LogAdmissionPageRights([Username],[logontype],[logondatetime],[StartDate],[EndDate],[status],[EnteredDate],[year]) values(@UserName,@logontype," +
                              "@logondatetime,@StartDate,@EndDate,@Status,@EnteredDate,@year);";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@UserName", Session["username"].ToString());
                        cmd.Parameters.AddWithValue("@logontype", "login");
                        cmd.Parameters.AddWithValue("@logondatetime", cdt.ToString("yyyy/MM/dd hh:mm:ss tt"));
                        cmd.Parameters.AddWithValue("@StartDate", startdate);
                        cmd.Parameters.AddWithValue("@EndDate", enddate);
                        cmd.Parameters.AddWithValue("@Status", startusforadmissionpage);
                        cmd.Parameters.AddWithValue("@EnteredDate", EnteredDate);
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.ExecuteNonQuery();
                        // insert into LogAdmissionPageRights code end

                        //update code start

                        query = "update Academicyear set StartDate=@startDate,EndDate=@EndDate,startusforadmissionpage=@status,EnteredDate=@EnteredDate where year =@yearcurrent;";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@startDate", txtadmissionstartdate.Text);
                        cmd.Parameters.AddWithValue("@EndDate", txtadmissionenddate.Text);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@EnteredDate", Date);
                        cmd.Parameters.AddWithValue("@yearcurrent", yearcurrent);
                        cmd.ExecuteNonQuery();

                        lblinfomsg.Text = "Data updated Successfully.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                        //update code start
                        BindGrid();

                    }

                    else
                    {
                        //insert code strat
                        query = "insert into Academicyear([StartDate],EndDate,[startusforadmissionpage],EnteredDate) values(@startDate,@EndDate,@status,@EnteredDate);";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@startDate", txtadmissionstartdate.Text);
                        cmd.Parameters.AddWithValue("@EndDate", txtadmissionenddate.Text);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@EnteredDate", Date);
                        cmd.ExecuteNonQuery();

                        lblinfomsg.Text = "Data Saved Successfully.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                        //insert code end

                        BindGrid();
                    }

                }

            }
            catch (Exception ex)
            {
                Log.Error("ActivationOfAdmissionPage.saveAcademic_ServerClick", ex);
            }
            finally
            {
                if (con != null)
                {
                    if (con != null) { con.Close(); }
                }
            }
        }

        protected void gridviewdata_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SqlConnection con = null;
            try
            {

                //if (e.CommandName == "deleteAcademic")
                //{
                //    string confirmValue = Request.Form["confirm_value"];
                //    if (confirmValue == "Yes")
                //    {

                //        int rownumber = Convert.ToInt32(e.CommandArgument);
                //        GridViewRow row = gridviewdata.Rows[rownumber];
                //        string srno = row.Cells[0].Text;

                //        using (con = Connection.getConnection())
                //        {
                //            con.Open();
                //            string query = "update AdmissionPageRights set status ='false' where srno='" + srno + "';";
                //            SqlCommand cmd = new SqlCommand(query, con);
                //            cmd.ExecuteNonQuery();

                //            lblinfomsg.Text = "Data Deleted Successfully.";
                //            // lblinfomsg.Text = "Academic Year Deleted Successfully.";
                //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);

                //            BindGrid();
                //        }

                //    }


                //}
                if (e.CommandName == "editAcademic")
                {
                    int rownumber = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gridviewdata.Rows[rownumber];
                    string year = row.Cells[0].Text;

                    using (con = Connection.getConnection())
                    {
                        con.Open();
                        string query = "select startdate,enddate,startusforadmissionpage from  Academicyear where year ='" + year + "'; ";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                txtadmissionstartdate.Text = reader["startdate"].ToString();
                                txtadmissionenddate.Text = reader["enddate"].ToString();

                            }
                        }

                        //lblinfomsg.Text = "Data Deleted Successfully.";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                        //BindGrid();
                    }
                }


            }
            catch (Exception ex)
            {
                Log.Error("ActivationOfAdmissionPage.gridviewdata_RowCommand", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }




        }

        public void BindGrid()
        {
            SqlConnection con = null;
            try
            {
                DataTable dt = new DataTable();
                using (con = Connection.getConnection())
                {




                    con.Open();

                    string query1 = "select year from Academicyear where iscurrentyear=1; ";

                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    SqlDataReader reader1 = cmd1.ExecuteReader();

                    String yearcurrent = "";
                    while (reader1.Read())
                    {
                        yearcurrent = reader1[0].ToString();
                    }
                    //string query = "select Srno, CONCAT(SUBSTRING(StartDate, 9, 2), '-', SUBSTRING(StartDate, 6, 2), '-', SUBSTRING(StartDate, 1, 4)) AS  StartDate, " +
                    //    "CONCAT(SUBSTRING(EndDate, 9, 2), '-', SUBSTRING(EndDate, 6, 2), '-', SUBSTRING(EndDate, 1, 4)) AS  EndDate,[status] From AdmissionPageRights where status='true';";
                    string query = "select  year, CONCAT(SUBSTRING(StartDate, 9, 2), '-', SUBSTRING(StartDate, 6, 2), '-', SUBSTRING(StartDate, 1, 4)) AS  StartDate" +
                        ",CONCAT(SUBSTRING(EndDate, 9, 2), '-', SUBSTRING(EndDate, 6, 2), '-', SUBSTRING(EndDate, 1, 4)) AS  EndDate,startusforadmissionpage from Academicyear where year ='" + yearcurrent + "'; ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(dt);

                    gridviewdata.DataSource = dt;
                    gridviewdata.DataBind();
                }
                txtadmissionenddate.Text = "";
                txtadmissionstartdate.Text = "";

            }
            catch (Exception ex)
            {
                Log.Error("ActivationOfAdmissionPage.BindGrid", ex);

            }
            finally
            {
                if (con != null) { con.Close(); }
            }




        }
    }
}