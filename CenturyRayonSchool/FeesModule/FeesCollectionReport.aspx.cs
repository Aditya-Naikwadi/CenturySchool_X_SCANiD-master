//using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.FeesModule.Reports;
//using CenturyRayonSchool.Model;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.FeesModule
{
    public partial class FeesCollectionReport : System.Web.UI.Page
    {
        DataTable uifeescollectiontable = new DataTable();
        Label lblusercode = new Label();
        string div_sess = "", std_sess = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblusercode = (Label)Page.Master.FindControl("lblusercode");

            if (Session["std"] != null && Session["div"] != null)
            {
                std_sess = Session["std"].ToString();
                div_sess = Session["div"].ToString();

            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                string year = new FeesModel().setActiveAcademicYear();
                lblacademicyear.Text = year;

                loadFormControl();
                // feesCollectionGrid();


            }
            uifeescollectiontable.Columns.Add("srno2");
            uifeescollectiontable.Columns.Add("paymode");
            uifeescollectiontable.Columns.Add("receiptno");
            uifeescollectiontable.Columns.Add("accountname");
            uifeescollectiontable.Columns.Add("std");
            uifeescollectiontable.Columns.Add("div");
            uifeescollectiontable.Columns.Add("grno");
            uifeescollectiontable.Columns.Add("studentname");
            uifeescollectiontable.Columns.Add("receiptdate");
            uifeescollectiontable.Columns.Add("amountpaid");
            uifeescollectiontable.Columns.Add("fine");
            uifeescollectiontable.Columns.Add("modeno");
            uifeescollectiontable.Columns.Add("bankname");
            uifeescollectiontable.Columns.Add("branch");

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

        public void loadFormControl()
        {
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "select std from std;";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    DataTable std = new DataTable();
                    adap.Fill(std);
                    std.Rows.Add("Select Std");
                    cmbStd.DataSource = std;
                    cmbStd.DataBind();
                    cmbStd.DataTextField = "std";
                    cmbStd.DataValueField = "std";
                    cmbStd.DataBind();
                    cmbStd.SelectedValue = "Select Div";

                    if (!string.IsNullOrEmpty(std_sess))
                    {
                        cmbStd.SelectedValue = std_sess;
                        cmbStd.Enabled = false;

                    }
                    else
                    {
                        // If std_sess is empty, set the default selected value
                        cmbStd.SelectedValue = "Select Std";
                        cmbStd.Enabled = true;
                    }

                    query = "select Div From Div";
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

                    if (!string.IsNullOrEmpty(div_sess))
                    {
                        cmbDiv.SelectedValue = div_sess;
                        cmbDiv.Enabled = false;
                    }
                    else
                    {
                        // If std_sess is empty, set the default selected value
                        cmbDiv.SelectedValue = "Select Div";
                        cmbDiv.Enabled = true;
                    }

                    query = "select grno,fullname from studentmaster where std='" + std_sess + "' and div='" + div_sess + "' and academicyear='" + lblacademicyear.Text + "' and (leftstatus IS NULL OR leftstatus = '') ";
                    adap = new SqlDataAdapter(query, con);
                    DataTable studtable = new DataTable();
                    adap.Fill(studtable);

                    studtable.Rows.Add("ALL", "ALL");
                    cmbstudentname.DataSource = studtable;
                    cmbstudentname.DataBind();
                    cmbstudentname.DataTextField = "fullname";
                    cmbstudentname.DataValueField = "grno";
                    cmbstudentname.DataBind();
                    cmbstudentname.SelectedValue = "ALL";


                    GridCollection.DataSource = uifeescollectiontable;
                    GridCollection.DataBind();

                    asptxtfromdate.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace('-', '/');
                    asptxttodate.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace('-', '/');

                }
            }
            catch (Exception ex)
            {
                Log.Error("FeesCollection.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }



        protected void FetchData_ServerClick(object sender, EventArgs e)
        {
            fillGridView();
        }

        public void fillGridView()
        {


            try
            {
                string select_std = "", select_div = "", select_gr = "", academicyear = "", fromdate = "", todate = "";

                select_std = cmbStd.SelectedValue.ToString();
                select_div = cmbDiv.SelectedValue.ToString();
                select_gr = cmbstudentname.SelectedValue.ToString();
                academicyear = lblacademicyear.Text;

                fromdate = asptxtfromdate.Text;
                todate = asptxttodate.Text;



                showFeesCollection2(fromdate, todate, select_std, select_div, select_gr, academicyear);


                GridCollection.DataSource = uifeescollectiontable;
                GridCollection.DataBind();


            }
            catch (Exception ex)
            {
                Log.Error("FeesCollectionReport.fillGridView", ex);
            }
            finally
            {

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
                        query = "select grno,fullname from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '');";
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
                Log.Error("FeesCollection.cmbDiv_SelectedIndexChanged", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }


        public System.Drawing.Color setForeColor(string status)
        {
            if (status == "Pending")
            {
                return System.Drawing.Color.Red;
            }
            else if (status == "Paid")
            {
                return System.Drawing.Color.Green;
            }
            else
            {
                return System.Drawing.Color.Red;
            }
        }

        public Boolean setChecked(string status)
        {
            if (status == "Pending")
            {
                return false;
            }
            else if (status == "Paid")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean setEnabled(string status)
        {
            if (status == "Pending")
            {
                return true;
            }
            else if (status == "Paid")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void txtFreeship_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txtFreeship1 = (TextBox)currentRow.FindControl("txtFreeship");
            Label lbltotal = (Label)currentRow.FindControl("lbltotalfees");

            double freeshipamt = 0, totalamt = 0;

            freeshipamt = Convert.ToDouble(txtFreeship1.Text);
            totalamt = Convert.ToDouble(lbltotal.Text);

            if (freeshipamt > totalamt)
            {
                txtFreeship1.Text = "0";
                lblalertmessage.Text = "Freeship Amount Cannot be greater than total amount";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
            }


        }



        protected void GridCollection_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SqlConnection con = null;
            DataTable dttable = new DataTable();
            string total = "0", balamt = "0", grno = "";
            try
            {
                if (e.CommandName == "printreceipt")
                {
                    int rownumber = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GridCollection.Rows[rownumber];

                    string std = row.Cells[3].Text;
                    string academciyear = row.Cells[5].Text;
                    grno = row.Cells[1].Text;

                    using (con = Connection.getConnection())
                    {
                        con.Open();

                        dttable = new FeesModel().printReceiptSlip(con, academciyear, std, grno);
                        foreach (DataRow ro in dttable.Rows)
                        {
                            total = ro["amtpaid"].ToString();
                            balamt = ro["balanceamt"].ToString();
                            break;
                        }



                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("FeesCollection.GridCollection_RowCommand", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/FeesModule/Reports"), "Cash_receipt.rpt"));
                rd.SetDataSource(dttable);
                rd.SetParameterValue("Total", total);
                rd.SetParameterValue("balance", balamt);

                string folderpath = Server.MapPath("DownloadFile");
                string filename = "FeeReceipt_" + grno + ".pdf";

                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);

                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(Server.MapPath("~/FeesModule/DownloadFile/" + filename));
                Response.End();
            }
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
                        query = "select grno,fullname from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '');";
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
                Log.Error("FeesCollection.cmbDiv_SelectedIndexChanged", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }




        private string showFeesCollection2(string fromdate, string todate, string std, string div, string grno, string academicyear)
        {
            SqlConnection con = null;
            int Gtotalpaid = 0;
            try
            {
                uifeescollectiontable.Rows.Clear();
                String query = "", filter = "";
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "select ROW_NUMBER() OVER (ORDER BY Receiptdate) AS srno2,paymode,Cast(receiptno as int) as receiptno,accountname,std,div,grno,studentname,Convert(varchar(20),cast([Receiptdate] as date),103) as receiptdate,amtpaid as amountpaid,fine,modeno,bankname,branch " +
                       " From studentfees where [ReceiptDate] between '" + fromdate + "' and '" + todate + "' and (receiptstatus is NULL) and academicyear='" + academicyear + "' ";

                    if (std.ToLower() != "all")
                    {

                        filter += " and std='" + std + "' ";

                    }

                    if (div.ToLower() != "all")
                    {

                        filter += " and div='" + div + "' ";


                    }

                    if (grno.ToLower() != "all" && grno.Length > 0)
                    {
                        filter += " and grno='" + grno + "' ";
                    }


                    query = query + filter;

                    query = query + " order by cast([Receiptdate] as date) asc;";


                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    adap.Fill(uifeescollectiontable);

                    query = "SELECT distinct " +
                            "SUM(CAST(amtpaid AS int)) OVER() AS TotalAmtpaid " +
                            " From studentfees where [ReceiptDate] between '" + fromdate + "' and '" + todate + "' and (receiptstatus is NULL) and academicyear='" + academicyear + "' ";
                    if (std.ToLower() != "all")
                    {

                        filter += " and std='" + std + "' ";

                    }

                    if (div.ToLower() != "all")
                    {

                        filter += " and div='" + div + "' ";


                    }

                    if (grno.ToLower() != "all" && grno.Length > 0)
                    {
                        filter += " and grno='" + grno + "' ";
                    }


                    query = query + filter;
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Gtotalpaid = Convert.ToInt32(reader["TotalAmtpaid"]);
                    }
                    reader.Close();

                    lbltotalstudents.Text = uifeescollectiontable.Rows.Count.ToString();
                    lblgtotalpaid.Text = Gtotalpaid.ToString();
                }

                return "ok";


            }
            catch (Exception ex)
            {
                Log.Error("FeesCollectionReport.showFeesCollection2", ex);
                return ex.Message;
            }
            finally
            {
                if (con != null) { con.Close(); }
            }



        }

        protected void btnprint_ServerClick(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                string query = "", filter = "";
                string select_std = "", select_div = "", select_gr = "", academicyear = "", fromdate = "", todate = "";

                select_std = cmbStd.SelectedValue.ToString();
                select_div = cmbDiv.SelectedValue.ToString();
                select_gr = cmbstudentname.SelectedValue.ToString();
                academicyear = lblacademicyear.Text;

                fromdate = asptxtfromdate.Text;
                todate = asptxttodate.Text;

                FeeCollect _fcds = new FeeCollect();
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "select ROW_NUMBER() OVER (ORDER BY Receiptdate) AS srno2,paymode,Cast(receiptno as int) as Receiptno,accountname,std,div,grno,studentname,Convert(varchar(20),cast([Receiptdate] as date),103) as ReceiptDate,amtpaid as amtpaid,fine,modeno,bankname,branch " +
                       " From studentfees where [ReceiptDate] between '" + fromdate + "' and '" + todate + "' and (receiptstatus is NULL) and academicyear='" + academicyear + "' ";

                    if (select_std.ToLower() != "all")
                    {

                        filter += " and std='" + select_std + "' ";

                    }

                    if (select_div.ToLower() != "all")
                    {

                        filter += " and div='" + select_div + "' ";


                    }

                    if (select_gr.ToLower() != "all" && select_gr.Length > 0)
                    {
                        filter += " and grno='" + select_gr + "' ";
                    }


                    query = query + filter;

                    query = query + " order by cast([Receiptdate] as date) asc;";

                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    adap.Fill(_fcds.Tables[0]);

                    ReportDocument rd = new ReportDocument();
                    rd.Load(Path.Combine(Server.MapPath("~/FeesModule/Reports"), "Feescollection.rpt"));
                    rd.SetDataSource(_fcds.Tables[0]);
                    rd.SetParameterValue("from", fromdate);
                    rd.SetParameterValue("to", todate);

                    string folderpath = Server.MapPath("DownloadFile");
                    string filename = "Feescollection.pdf";

                    rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);

                    Response.ContentType = "Application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                    Response.TransmitFile(Server.MapPath("~/FeesModule/DownloadFile/" + filename));
                    Response.End();
                }


            }
            catch (Exception ex)
            {
                Log.Error("FeesCollectionReport.btnprint_ServerClick", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }

        }
    }
}