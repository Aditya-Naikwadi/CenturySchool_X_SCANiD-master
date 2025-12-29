using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.FeesModule
{
    public partial class FeeCancel : System.Web.UI.Page
    {
        DataTable uifeescanceltable = new DataTable();
        Label lblusercode = new Label();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblusercode = (Label)Page.Master.FindControl("lblusercode");
            if (!IsPostBack)
            {
                string year = new FeesModel().setActiveAcademicYear();
                lblacademicyear.Text = year;

                loadFormControl();
                // feesCollectionGrid();


            }

            uifeescanceltable.Columns.Add("Receiptno");
            uifeescanceltable.Columns.Add("Receiptdate");
            uifeescanceltable.Columns.Add("Studentname");
            uifeescanceltable.Columns.Add("std");
            uifeescanceltable.Columns.Add("div");
            uifeescanceltable.Columns.Add("grno");
            uifeescanceltable.Columns.Add("Paymode");
            uifeescanceltable.Columns.Add("Concession");
            uifeescanceltable.Columns.Add("Totalamt");
            uifeescanceltable.Columns.Add("Amtpaid");
            uifeescanceltable.Columns.Add("balanceamt");

        }

        protected void cmbStd_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                DataTable studtable = new DataTable();
                using (con = Connection.getConnection())
                {
                    con.Open();

                    string query = "", select_std = "", select_div = "", academicyear = "";

                    select_std = cmbStd.SelectedValue.ToString();
                    select_div = cmbDiv.SelectedValue.ToString();
                    academicyear = lblacademicyear.Text;

                    if (select_std != "Select Std" && select_div != "Select Div")
                    {
                        query = "select grno,fullname from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' and  (leftstatus IS NULL OR leftstatus = '');";
                        SqlDataAdapter adap = new SqlDataAdapter(query, con);
                        adap.Fill(studtable);

                        studtable.Rows.Add("ALL", "ALL");
                        cmbstudentname.DataSource = studtable;
                        cmbstudentname.DataBind();
                        cmbstudentname.DataTextField = "fullname";
                        cmbstudentname.DataValueField = "grno";
                        cmbstudentname.DataBind();
                        cmbstudentname.SelectedValue = "ALL";


                    }



                }
            }
            catch (Exception ex)
            {
                Log.Error("FeeCancel.cmbStd_SelectedIndexChanged", ex);
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

        protected void cmbDiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                DataTable studtable = new DataTable();
                using (con = Connection.getConnection())
                {
                    con.Open();

                    string query = "", select_std = "", select_div = "", academicyear = "";

                    select_std = cmbStd.SelectedValue.ToString();
                    select_div = cmbDiv.SelectedValue.ToString();
                    academicyear = lblacademicyear.Text;

                    if (select_std != "Select Std" && select_div != "Select Div")
                    {
                        query = "select grno,fullname from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' and  (leftstatus IS NULL OR leftstatus = '');";
                        SqlDataAdapter adap = new SqlDataAdapter(query, con);
                        adap.Fill(studtable);

                        studtable.Rows.Add("ALL", "ALL");
                        cmbstudentname.DataSource = studtable;
                        cmbstudentname.DataBind();
                        cmbstudentname.DataTextField = "fullname";
                        cmbstudentname.DataValueField = "grno";
                        cmbstudentname.DataBind();
                        cmbstudentname.SelectedValue = "ALL";

                    }




                }
            }
            catch (Exception ex)
            {
                Log.Error("FeeCancel.cmbDiv_SelectedIndexChanged", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
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

        protected void FetchData_ServerClick(object sender, EventArgs e)
        {
            fillGridView();
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





                }
            }
            catch (Exception ex)
            {
                Log.Error("FeeCancel.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void GridCollection_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SqlConnection con = null;
            DataTable dttable = new DataTable();
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    if (e.CommandName == "cancelreceipt")
                    {
                        int rownumber = Convert.ToInt32(e.CommandArgument);
                        GridViewRow row = GridCollection.Rows[rownumber];

                        string receiptno = row.Cells[0].Text;
                        string std = row.Cells[3].Text;
                        string grno = row.Cells[5].Text;

                        using (con = Connection.getConnection())
                        {
                            con.Open();

                            string resp = new FeesModel().CancelReceipt(con, std, grno, receiptno);

                            if (resp == "ok")
                            {
                                lblinfomsg.Text = "Fees Receipt Cancelled.";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                            }
                            else
                            {
                                lblalertmessage.Text = resp;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                            }
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                Log.Error("FeeCancel.GridCollection_RowCommand", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }



        }

        public void fillGridView()
        {

            SqlConnection con = null;
            try
            {
                string query = "", select_std = "", select_div = "", select_gr = "", academicyear = "";



                select_std = cmbStd.SelectedValue.ToString();
                select_div = cmbDiv.SelectedValue.ToString();
                select_gr = cmbstudentname.SelectedValue.ToString();
                academicyear = lblacademicyear.Text;

                using (con = Connection.getConnection())
                {
                    con.Open();

                    if (select_gr.Equals("ALL"))
                    {
                        query = "select Distinct Cast(Receiptno as int) as Receiptno,Receiptdate,Studentname,std,div,grno,Paymode,Concession,Totalamt,Amtpaid,balanceamt " +
                                "from View_Studentfees_Receiptreport " +
                                "where academicyear='" + academicyear + "' and std = '" + select_std + "' and div = '" + select_div + "' and (receiptstatus not in ('cancelled') or receiptstatus is null) " +
                                "order by Cast(Receiptno as int) asc;";
                    }
                    else
                    {
                        // query = "select ROLLNO,GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,Academicyear,admissiontype from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' and GRNO='" + select_gr + "'  order by Cast(ROLLNO as int) asc;";

                        query = "select Distinct Cast(Receiptno as int) as Receiptno,Receiptdate,Studentname,std,div,grno,Paymode,Concession,Totalamt,Amtpaid,balanceamt " +
                                "from View_Studentfees_Receiptreport " +
                                "where academicyear='" + academicyear + "' and std = '" + select_std + "' and div = '" + select_div + "' and GRNO='" + select_gr + "' and (receiptstatus not in ('cancelled') or receiptstatus is null) " +
                                "order by Cast(Receiptno as int) asc;";
                    }

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.Fill(uifeescanceltable);



                    GridCollection.DataSource = uifeescanceltable;
                    GridCollection.DataBind();


                }
            }
            catch (Exception ex)
            {
                Log.Error("FeeCancel.fillGridView", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }


            }
        }


    }
}