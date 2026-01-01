//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.MarksheetModule
{
    public partial class TeacherMapping : System.Web.UI.Page
    {
        DataTable teacherslist = new DataTable();
        Label lblusercode = new Label();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblusercode = (Label)Page.Master.FindControl("lblusercode");
            string year = new FeesModel().setActiveAcademicYear();
            lblacademicyear.Text = year;

            if (!IsPostBack)
            {
                loadFormControl();
                fillGridView();

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
        protected void divCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmbDiv.SelectedValue.ToString().Equals("Select Div"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        protected void trCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmbtrname.SelectedValue.ToString().Equals("Select Teacher Name"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
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

                    query = "select Div From Div where div Not IN ('ALL');";
                    adap = new SqlDataAdapter(query, con);
                    DataTable div = new DataTable();
                    adap.Fill(div);
                    div.Rows.Add("Select Div");
                    cmbDiv.DataSource = div;
                    cmbDiv.DataBind();
                    cmbDiv.DataTextField = "Div";
                    cmbDiv.DataValueField = "Div";
                    cmbDiv.DataBind();
                    cmbDiv.SelectedValue = "Select Div";

                    query = "select UserId,PersonName from Login where UserType='teacher'";
                    adap = new SqlDataAdapter(query, con);
                    DataTable tr = new DataTable();
                    adap.Fill(tr);
                    //tr.Rows.Add("Select Teacher Name");
                    cmbtrname.DataSource = tr;
                    cmbtrname.DataBind();
                    cmbtrname.DataTextField = "PersonName";
                    cmbtrname.DataValueField = "UserId";
                    cmbtrname.DataBind();
                    cmbtrname.SelectedValue = "Select Teacher";




                }
            }
            catch (Exception ex)
            {
                Log.Error("TeacherMapping.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        public void fillGridView()
        {
            DataTable stud_tbl = new DataTable();

            SqlConnection con = null;
            try
            {
                DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                string query = "", select_std = "", select_div = "", select_gr = "", exam = "", feesstatus = "", freeshipamount = "0", ReceiptDate = "";

                string year = "", freeshiptype = "";

                select_std = cmbStd.SelectedValue.ToString();
                select_div = cmbDiv.SelectedValue.ToString();
                year = lblacademicyear.Text;

                using (con = Connection.getConnection())
                {
                    con.Open();


                    query = "select STD,DIV,TeacherName from TeacherMapping where academicyear='" + year + "'  ;";
                    //SqlCommand cmd = new SqlCommand(query, con);
                    //SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    //ad.Fill(stud_tbl);
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(teacherslist);



                    GridCollection.DataSource = teacherslist;
                    GridCollection.DataBind();


                }
            }
            catch (Exception ex)
            {
                Log.Error("TeacherMapping.fillGridView", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
                stud_tbl.Dispose();
                teacherslist.Dispose();
            }
        }

        protected void Savedetais_ServerClick(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                string query = "", select_std = "", select_div = "", trname = "", trid = "", year = "", freeshipamount = "0", ReceiptDate = "";
                string usercode = lblusercode.Text;
                int count = 0;

                year = lblacademicyear.Text;
                select_std = cmbStd.SelectedValue.ToString();
                select_div = cmbDiv.SelectedValue.ToString();
                trname = cmbtrname.SelectedItem.ToString();
                trid = cmbtrname.SelectedValue.ToString();
                using (con = Connection.getConnection())
                {
                    con.Open();


                    query = "Select Count(*) From TeacherMapping where std='" + select_std + "' and div='" + select_div + "' and Academicyear='" + year + "' ;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader[0]);
                    }

                    if (count == 0)
                    {
                        query = "insert into TeacherMapping (teachername,std,div,UserID,Academicyear,createddate,createdby) values ('" + trname + "','" + select_std + "','" + select_div + "','" + trid + "','" + year + "','" + cdt + "','" + usercode + "') ;";

                    }
                    else
                    {
                        query = "update TeacherMapping set teachername='" + trname + "',updateddate='" + cdt + "',updatedby='" + usercode + "',UserID='" + trid + "' where std='" + select_std + "' and div ='" + select_div + "' and Academicyear='" + year + "'";
                    }
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    lblinfomsg.Text = "Data Saved Successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                    fillGridView();
                }
            }
            catch (Exception ex)
            {
                Log.Error("TeacherMapping.Savedetais_ServerClick", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

            }

        }

        protected void GridCollection_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            SqlConnection con = null;

            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    if (e.CommandName == "deleteteacher")
                    {
                        int count = 0;
                        int rownumber = Convert.ToInt32(e.CommandArgument);

                        GridViewRow row = GridCollection.Rows[rownumber];
                        string std = row.Cells[0].Text;
                        string div = row.Cells[1].Text;


                        using (con = Connection.getConnection())
                        {
                            con.Open();
                            string query = "";


                            query = "Delete from TeacherMapping where std='" + std + "' and div='" + div + "';";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();

                            lblinfomsg.Text = "Record Deleted Successfully.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);

                            fillGridView();


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