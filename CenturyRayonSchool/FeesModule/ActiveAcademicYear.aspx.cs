using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.FeesModule
{
    public partial class ActiveAcademicYear : System.Web.UI.Page
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

                using (con = Connection.getConnection())
                {
                    con.Open();
                    string academicyear = txtAcademicYear.Text;
                    Boolean isActive = chkIsActive.Checked;
                    int iscurrentyear = 0;
                    string query = "select Count(*) From Academicyear where [year]='" + academicyear + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    var rcnt = cmd.ExecuteScalar();
                    if (!string.IsNullOrEmpty(rcnt.ToString()))
                    {
                        if (Convert.ToInt32(rcnt.ToString()) == 0)
                        {

                            if (isActive)
                            {
                                iscurrentyear = 1;
                                query = "update Academicyear set iscurrentyear='0';";
                                cmd = new SqlCommand(query, con);
                                cmd.ExecuteNonQuery();
                            }

                            int srno = 0;
                            query = "select Top 1 [status] From Academicyear order by [status] desc;";
                            cmd = new SqlCommand(query, con);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                srno = Convert.ToInt32(reader[0]);
                            }
                            reader.Close();
                            srno = srno + 1;

                            query = "insert into Academicyear([year],[status],iscurrentyear) values(@year,@status,@iscurrentyear);";
                            cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@year", academicyear);
                            cmd.Parameters.AddWithValue("@status", srno.ToString());
                            cmd.Parameters.AddWithValue("@iscurrentyear", iscurrentyear.ToString());
                            cmd.ExecuteNonQuery();

                            BindGrid();

                        }


                    }

                }

            }
            catch (Exception ex)
            {
                Log.Error("ActiveAcademicYear.saveAcademic_ServerClick", ex);
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

                if (e.CommandName == "deleteAcademic")
                {
                    string confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "Yes")
                    {

                        int rownumber = Convert.ToInt32(e.CommandArgument);
                        GridViewRow row = gridviewdata.Rows[rownumber];
                        string academicyear = row.Cells[1].Text;

                        using (con = Connection.getConnection())
                        {
                            con.Open();
                            string query = "Delete from Academicyear where [year]='" + academicyear + "';";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();

                            lblinfomsg.Text = "Academic Year Deleted Successfully.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);

                            BindGrid();
                        }

                    }


                }


            }
            catch (Exception ex)
            {
                Log.Error("ActiveAcademicYear.gridviewdata_RowCommand", ex);
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
                    string query = "select [status],[year],iscurrentyear From Academicyear;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(dt);

                    gridviewdata.DataSource = dt;
                    gridviewdata.DataBind();
                }


            }
            catch (Exception ex)
            {
                Log.Error("ActiveAcademicYear.BindGrid", ex);

            }
            finally
            {
                if (con != null) { con.Close(); }
            }




        }
    }
}