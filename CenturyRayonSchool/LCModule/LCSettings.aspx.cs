//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.LCModule
{
    public partial class LCSettings : System.Web.UI.Page
    {
        Label lblusercode = new Label();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblusercode = (Label)Page.Master.FindControl("lblusercode");
            string year = new FeesModel().setActiveAcademicYear();
            lblacademicyear.Text = year;
            if (!IsPostBack)
            {
                loadFormControl();

            }
        }
        public void loadFormControl()
        {
            SqlConnection con = null;
            string academicyear = lblacademicyear.Text;
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
                Log.Error("GenrateLC.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
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
        protected void updateprogress_ServerClick(object sender, EventArgs e)
        {
            SqlConnection con = null;
            String query = "", progress = "", conduct = "", std = "", year = "";
            progress = txtprogress.Text;
            conduct = txtconduct.Text;
            std = cmbStd.SelectedValue.ToString();
            year = lblacademicyear.Text;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "update LeavingCertificate set Progress='" + progress + "',conduct='" + conduct + "' where std='" + std + "' and academicyear='" + year + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    query = "update studentmaster set Progress='" + progress + "',conduct='" + conduct + "' where std='" + std + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    lblinfomsg.Text = "Progress And Conduct Updated For" + std;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Settings.updateprogress_ServerClick", ex);
            }
        }

        protected void btnrol_ServerClick(object sender, EventArgs e)
        {
            SqlConnection con = null;
            String query = "", rol = "", std = "", year = "";
            rol = txtrol.Text;

            std = cmbStd.SelectedValue.ToString();
            year = lblacademicyear.Text;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "update LeavingCertificate set Reasonforleaving='" + rol + "' where std='" + std + "' and academicyear='" + year + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    query = "update studentmaster set Reasonforleaving='" + rol + "' where std='" + std + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    lblinfomsg.Text = "Reason Of Leaving Updated For " + std;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Settings.btnrol_ServerClick", ex);
            }
        }

        protected void btnremrk_ServerClick(object sender, EventArgs e)
        {
            SqlConnection con = null;
            String query = "", reamrk = "", std = "", year = "";
            reamrk = txtremrk.Text;

            std = cmbStd.SelectedValue.ToString();
            year = lblacademicyear.Text;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "update LeavingCertificate set remark='" + reamrk + "' where std='" + std + "' and academicyear='" + year + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    query = "update studentmaster set remark='" + reamrk + "' where std='" + std + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    lblinfomsg.Text = "Remarks Updated For " + std;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Settings.btnremrk_ServerClick", ex);
            }
        }

        protected void btnsis_ServerClick(object sender, EventArgs e)
        {
            SqlConnection con = null;
            String query = "", sis = "", std = "", year = "";
            sis = txtsis.Text;

            std = cmbStd.SelectedValue.ToString();
            year = lblacademicyear.Text;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "update LeavingCertificate set stdstudying='" + sis + "' where std='" + std + "' and academicyear='" + year + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    query = "update studentmaster set stdstudying='" + sis + "' where std='" + std + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    lblinfomsg.Text = "Std Studying Updated for Std" + std;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Settings.btnsis_ServerClick", ex);
            }
        }

        protected void btndob_ServerClick(object sender, EventArgs e)
        {
            SqlConnection con = null;
            String query = "", sis = "", std = "", year = "";

            std = cmbStd.SelectedValue.ToString();
            year = lblacademicyear.Text;
            DataTable dbtable = new DataTable();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "select std,grno,dob,dobwords From studentmaster where std='" + std + "' and academicyear='" + year + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(dbtable);

                    foreach (DataRow ro in dbtable.Rows)
                    {

                        string words = Functions.DateToText(Convert.ToDateTime(ro["dob"]), false, false);


                        query = "update studentmaster set dobwords='" + words + "' where grno='" + ro["grno"].ToString() + "' and std='" + ro["std"].ToString() + "' and academicyear='" + year + "';";
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();

                        query = "update LeavingCertificate set dobwords='" + words + "' where grno='" + ro["grno"].ToString() + "' and std='" + ro["std"].ToString() + "' and academicyear='" + year + "';";
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();

                    }
                    lblinfomsg.Text = "Date of birth in words updated successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Settings.btndob_ServerClick", ex);
            }
        }

        protected void btnfreeship_ServerClick(object sender, EventArgs e)
        {
            SqlConnection con = null;
            String query = "", freeship = "", std = "", year = "";
            freeship = txtfree.Text;

            std = cmbStd.SelectedValue.ToString();
            year = lblacademicyear.Text;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "update LeavingCertificate set freeshiptype='" + freeship + "' where std='" + std + "' and academicyear='" + year + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    query = "update studentmaster set freeshiptype='" + freeship + "' where std='" + std + "' and academicyear='" + year + "';";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    lblinfomsg.Text = "Freeship Updated For " + std;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Settings.btnfreeship_ServerClick", ex);
            }
        }
    }
}