//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.FeesModule
{
    public partial class FeesHeader : System.Web.UI.Page
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

        public void BindGrid()
        {
            SqlConnection con = null;
            try
            {
                DataTable dt = new DataTable();
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "select Fee_Code,Fee_Header From FeeHeader;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(dt);

                    gridHeaders.DataSource = dt;
                    gridHeaders.DataBind();
                }


            }
            catch (Exception ex)
            {
                Log.Error("BindGrid", ex);

            }
            finally
            {
                if (con != null) { con.Close(); }
            }




        }

        protected void SaveHeader_ServerClick(object sender, EventArgs e)
        {
            SqlConnection con = null;
            Boolean iserror = false;
            string message = "";
            try
            {
                if (txtFeesHeader.Text.Length == 0)
                {
                    message = "Please Enter Fees Header";
                    iserror = true;
                }

                if (iserror == false)
                {
                    using (con = Connection.getConnection())
                    {
                        con.Open();
                        DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                        string query = "";
                        SqlCommand cmd = null;

                        string usercode = lblusercode.Text;
                        string feesheadername = txtFeesHeader.Text;

                        if (lblfeecode.Text.Equals("0"))
                        {
                            int feescode = 0;
                            int count = 0;
                            //Add new Fees Header

                            query = "select Count(*) from FeeHeader where fee_header='" + feesheadername + "';";
                            cmd = new SqlCommand(query, con);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                count = Convert.ToInt32(reader[0]);
                            }
                            reader.Close();

                            if (count == 0)
                            {
                                query = "select Top 1 Fee_Code From FeeHeader order by Cast(Fee_Code as int) desc;";
                                cmd = new SqlCommand(query, con);
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    feescode = Convert.ToInt32(reader[0]);
                                }
                                reader.Close();

                                feescode = feescode + 1;


                                query = "insert into FeeHeader(Fee_Code,Fee_Header,createdby,createddatetime) values(@Fee_Code,@Fee_Header,@createdby,@createddatetime);";
                                cmd = new SqlCommand(query, con);
                                cmd.Parameters.AddWithValue("@Fee_Code", feescode.ToString());
                                cmd.Parameters.AddWithValue("@Fee_Header", feesheadername);
                                cmd.Parameters.AddWithValue("@createdby", usercode);
                                cmd.Parameters.AddWithValue("@createddatetime", cdt.ToString("yyyy/MM/dd HH:mm:ss"));
                                cmd.ExecuteNonQuery();

                                //open modal
                                lblinfomsg.Text = "Fees Header Saved Successfully.";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);

                            }
                            else
                            {
                                //open modal
                                lblalertmessage.Text = "Duplicate Fees Header Found.";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                            }

                        }
                        else
                        {
                            //Update Fees Header




                        }

                    }

                }
                else
                {
                    lblalertmessage.Text = message;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                }

            }
            catch (Exception ex)
            {
                Log.Error("FeesHeader.SaveHeader_ServerClick", ex);
            }
            finally
            {
                BindGrid();
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
                    if (e.CommandName == "editfeesheader")
                    {

                    }
                    if (e.CommandName == "deletefeesheader")
                    {
                        int count = 0;
                        int rownumber = Convert.ToInt32(e.CommandArgument);

                        GridViewRow row = gridHeaders.Rows[rownumber];
                        string id = row.Cells[0].Text;
                        string feeheader = row.Cells[1].Text;

                        using (con = Connection.getConnection())
                        {
                            con.Open();
                            string query = "Select Count(*) from FeeParticular where particularname='" + feeheader + "';";
                            SqlCommand cmd = new SqlCommand(query, con);
                            var rcnt = cmd.ExecuteScalar();
                            if (!string.IsNullOrEmpty(rcnt.ToString()))
                            {
                                count = Convert.ToInt32(rcnt.ToString());
                            }

                            if (count == 0)
                            {
                                query = "Delete from FeeHeader where Fee_Header='" + feeheader + "';";
                                cmd = new SqlCommand(query, con);
                                cmd.ExecuteNonQuery();

                                BindGrid();
                            }
                            else
                            {
                                lblalertmessage.Text = "Fee Header already exist in Fees Particular and Fees Receipt. Kindly create new header.";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                            }

                        }


                    }
                }


            }
            catch (Exception ex)
            {
                if (con != null) { con.Close(); }
            }



        }
    }
}