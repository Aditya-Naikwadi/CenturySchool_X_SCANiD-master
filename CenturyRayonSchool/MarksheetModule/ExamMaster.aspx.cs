//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace CenturyRayonSchool.MarksheetModule
{
    public partial class ExamMaster : System.Web.UI.Page
    {
        Label lblusercode = new Label();
        protected void Page_Load(object sender, EventArgs e)
        {
            string year = new FeesModel().setActiveAcademicYear();
            lblAcademicyear.Text = year;

            lblusercode = (Label)Page.Master.FindControl("lblusercode");

            if (!IsPostBack)
            {
                loadFormControl();
                LoadGrid();
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
                    cmbstd.DataSource = std;
                    cmbstd.DataBind();
                    cmbstd.DataTextField = "std";
                    cmbstd.DataValueField = "std";
                    cmbstd.DataBind();
                    cmbstd.SelectedValue = "Select Std";



                }
            }
            catch (Exception ex)
            {
                Log.Error("ExamMaster.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void stdCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmbstd.SelectedValue.ToString().Equals("Select Std"))
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
            if (month.SelectedValue.ToString().Equals("Select Month"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void SaveExam_ServerClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SqlConnection con = null;
                try
                {
                    using (con = Connection.getConnection())
                    {
                        con.Open();
                        if (txtexexamorder.Text.Length == 0)
                        {
                            throw new Exception("Please give the order of exam");
                        }
                        else if (cmbstd.Text.Length == 0)
                        {
                            throw new Exception("Select the Std");
                        }
                        else if (TextExamName.Text.Length == 0)
                        {
                            throw new Exception("Enter exam name");
                        }
                        else if (month.Text.Length == 0)
                        {
                            throw new Exception("Enter exam month");
                        }
                        else
                        {
                            DateTime cdt = DateTime.Now;
                            string usercode = lblusercode.Text;
                            int count = 0; String query = "", std = "";
                            std = cmbstd.SelectedValue.ToString();

                            query = "Select Count(*) from ExamMaster where std='" + std + "' and Examname='" + TextExamName.Text + "' ";
                            SqlCommand cmd = new SqlCommand(query, con);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                count = Convert.ToInt32(reader[0]);
                            }
                            reader.Close();

                            if (count == 0)
                            {
                                query = "insert into ExamMaster([Examorder],[Examname],[Month],[std],[CreatedDate],CreatedBy) values(@order,@examname,@month,@std,'" + cdt + "','" + usercode + "');";
                                cmd = new SqlCommand(query, con);
                                cmd.Parameters.AddWithValue("@order", txtexexamorder.Text);
                                cmd.Parameters.AddWithValue("@examname", TextExamName.Text);
                                cmd.Parameters.AddWithValue("@month", month.Text);
                                cmd.Parameters.AddWithValue("@std", std);

                                cmd.ExecuteNonQuery();
                                lblinfomsg.Text = "Exam Saved Successfully.";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                            }
                            else
                            {
                                query = "update ExamMaster set [Examorder]=@order,[Examname]=@examname,[Month]=@month,[std]=@std,updatedDate='" + cdt + "',updatedby='" + usercode + "' where std='" + std + "' and Examname='" + TextExamName.Text + "'";
                                cmd = new SqlCommand(query, con);
                                cmd.Parameters.AddWithValue("@order", txtexexamorder.Text);
                                cmd.Parameters.AddWithValue("@examname", TextExamName.Text);
                                cmd.Parameters.AddWithValue("@month", month.Text);
                                cmd.Parameters.AddWithValue("@std", std);
                                cmd.ExecuteNonQuery();

                                lblinfomsg.Text = "Exam Updated Successfully.";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);

                            }



                            LoadGrid();

                            con.Close();

                        }
                    }

                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Exam name already exist, try another name", "Exam Master");
                        Log.Error("ExamMaster.SaveExam_ServerClick", ex);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message, "Exam Master");
                        Log.Error("ExamMaster.SaveExam_ServerClick", ex);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Log.Error("ExamMaster.SaveExam_ServerClick", ex);
                }
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
                    string query = "Select [Examorder],[std],[Examname],[Month] From ExamMaster";
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

        protected void gridHeaders_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            SqlConnection con = null;

            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    if (e.CommandName == "deleteExam")
                    {
                        int count = 0;
                        int rownumber = Convert.ToInt32(e.CommandArgument);

                        GridViewRow row = gridHeaders.Rows[rownumber];
                        string std = row.Cells[1].Text;
                        string Examname = row.Cells[2].Text;

                        using (con = Connection.getConnection())
                        {
                            con.Open();
                            string query = "";


                            query = "Delete from ExamMaster where std='" + std + "' and Examname='" + Examname + "';";
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
    }
}