using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace CenturyRayonSchool.MarksheetModule
{
    public partial class SubjectList : System.Web.UI.Page
    {
        Label lblusercode = new Label();
        protected void Page_Load(object sender, EventArgs e)
        {
            string year = new FeesModel().setActiveAcademicYear();
            lblAcademicyear.Text = year;

            lblusercode = (Label)Page.Master.FindControl("lblusercode");

            if (!IsPostBack)
            {
                LoadGrid();

            }
        }

        protected void SaveSubject_ServerClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SqlConnection con = null;
                try
                {
                    using (con = Connection.getConnection())
                    {
                        con.Open();
                        if (txtsubj.Text.Length == 0)
                        {
                            throw new Exception("Please give the order of exam");
                        }
                        else
                        {
                            DateTime cdt = DateTime.Now;
                            string usercode = lblusercode.Text;
                            int count = 0; String query = "", std = "";

                            query = "Select Count(*) from Subjects where subject='" + txtsubj.Text + "' ";
                            SqlCommand cmd = new SqlCommand(query, con);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                count = Convert.ToInt32(reader[0]);
                            }
                            reader.Close();

                            if (count == 0)
                            {
                                query = "insert into subjects(subject,CreatedDate,CreatedBy) values('" + txtsubj.Text + "','" + cdt + "','" + usercode + "')";
                                Log.Info(query);
                                cmd = new SqlCommand(query, con);
                                cmd.ExecuteNonQuery();
                                lblinfomsg.Text = "Subject Saved Successfully.";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                            }
                            else
                            {
                                query = "update subjects set [subject]=@subject,updatedDate='" + cdt + "',updatedby='" + usercode + "' where subject='" + txtsubj.Text + "' ";
                                cmd = new SqlCommand(query, con);
                                cmd.Parameters.AddWithValue("@subject", txtsubj.Text);
                                cmd.ExecuteNonQuery();

                                lblinfomsg.Text = "Subject Updated Successfully.";
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
                        Log.Error("ExamMaster.SaveSubject_ServerClick", ex);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message, "Exam Master");
                        Log.Error("ExamMaster.SaveSubject_ServerClick", ex);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Log.Error("ExamMaster.SaveSubject_ServerClick", ex);
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
                    string query = "Select [Subject] From Subjects";
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
                    if (e.CommandName == "deleteSubject")
                    {
                        int count = 0;
                        int rownumber = Convert.ToInt32(e.CommandArgument);

                        GridViewRow row = gridHeaders.Rows[rownumber];
                        string subj = row.Cells[0].Text;


                        using (con = Connection.getConnection())
                        {
                            con.Open();
                            string query = "";


                            query = "Delete from subjects where subject='" + subj + "';";
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