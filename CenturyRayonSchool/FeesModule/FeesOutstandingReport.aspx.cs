using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.FeesModule
{
    public partial class FeesOutstandingReport : System.Web.UI.Page
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

            uifeescollectiontable.Columns.Add("rollno");
            uifeescollectiontable.Columns.Add("std");
            uifeescollectiontable.Columns.Add("div");
            uifeescollectiontable.Columns.Add("grno");
            uifeescollectiontable.Columns.Add("studentname");
            uifeescollectiontable.Columns.Add("bookedfees");
            uifeescollectiontable.Columns.Add("paidfees");
            uifeescollectiontable.Columns.Add("outstandingfees");

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
                    string query = "select std from std where std not in ('LEFT');";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    DataTable std = new DataTable();
                    adap.Fill(std);
                    std.Rows.Add("Select Std");
                    cmbStd.DataSource = std;
                    cmbStd.DataBind();
                    cmbStd.DataTextField = "std";
                    cmbStd.DataValueField = "std";
                    cmbStd.DataBind();
                    cmbStd.SelectedValue = "ALL";
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
                    query = "select Div From Div;";
                    adap = new SqlDataAdapter(query, con);
                    DataTable div = new DataTable();
                    adap.Fill(div);
                    div.Rows.Add("Select Div");
                    cmbDiv.DataSource = div;
                    cmbDiv.DataBind();
                    cmbDiv.DataTextField = "Div";
                    cmbDiv.DataValueField = "Div";
                    cmbDiv.DataBind();
                    cmbDiv.SelectedValue = "ALL";


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

                    asptxtfromdate.Text = new DateTime(2022, 4, 1).ToString("yyyy/MM/dd").Replace('-', '/');
                    asptxttodate.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace('-', '/');

                }
            }
            catch (Exception ex)
            {
                Log.Error("FeesOutstandingReport.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }



        protected void FetchData_ServerClick(object sender, EventArgs e)
        {
            fillGridView(false);
        }

        public void fillGridView(Boolean ispdf)
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



                showOutstandingFees(select_std, select_div, fromdate, todate, select_gr, academicyear);


                if (ispdf == false)
                {
                    GridCollection.DataSource = uifeescollectiontable;
                    GridCollection.DataBind();
                }
                else
                {
                    //download pdf file

                    ReportDocument rd = new ReportDocument();
                    rd.Load(Path.Combine(Server.MapPath("~/FeesModule/Reports"), "OutstandingReport.rpt"));
                    rd.SetDataSource(uifeescollectiontable);
                    rd.SetParameterValue("fromdate", fromdate);
                    rd.SetParameterValue("todate", todate);

                    string folderpath = Server.MapPath("DownloadFile");
                    string filename = "FeeOutstandingReport.pdf";

                    rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);

                    Response.ContentType = "Application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                    Response.TransmitFile(Server.MapPath("~/FeesModule/DownloadFile/" + filename));
                    Response.End();


                }


            }
            catch (Exception ex)
            {
                Log.Error("FeesOutstandingReport.fillGridView", ex);
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



        protected void btnprint_ServerClick(object sender, EventArgs e)
        {
            fillGridView(true);
        }

        public void showOutstandingFees(string std, string div, string fromdate, string todate, string grno, string academicyear)
        {
            uifeescollectiontable.Rows.Clear();

            SqlConnection con = null;
            DataTable FeeParticular = new DataTable();
            Dictionary<string, string> feestotal = new Dictionary<string, string>();
            Dictionary<string, string> newadmission = new Dictionary<string, string>();
            Dictionary<string, string> readmission = new Dictionary<string, string>();
            Dictionary<string, string> administrative = new Dictionary<string, string>();
            try
            {
                using (con = Connection.getConnection())
                {

                    con.Open();
                    string bookedamt = "0", balanceamt = "0", newadmssionfee = "0", readmissionfee = "0", administrativefee = "0";

                    string query = "";
                    if (std.ToLower().Equals("all") && div.ToLower().Equals("all"))
                    {
                        query = "select rollno,std,div,grno,fullname as studentname,admissiontype From studentmaster where academicyear='" + academicyear + "'and (leftstatus IS NULL OR leftstatus = '') order by std asc,div asc,Cast(ROLLNO as int) asc;";
                    }
                    else if (div.ToLower().Equals("all"))
                    {
                        query = "select rollno,std,div,grno,fullname as studentname,admissiontype From studentmaster where std='" + std + "' and academicyear='" + academicyear + "'and (leftstatus IS NULL OR leftstatus = '') order by std asc,div asc,Cast(ROLLNO as int) asc;";
                    }
                    else
                    {
                        query = "select rollno,std,div,grno,fullname as studentname,admissiontype From studentmaster where std='" + std + "' and div='" + div + "' and academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '') order by std asc,div asc,Cast(ROLLNO as int) asc;";
                    }
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(uifeescollectiontable);
                    SqlDataReader reader = null;

                    query = "select [Std-Div],ParticularName,Total from FeeParticular where academicyear='" + academicyear + "' and [Std-Div]='" + std + "';";
                    cmd = new SqlCommand(query, con);
                    adap = new SqlDataAdapter(cmd);
                    adap.Fill(FeeParticular);

                    //get new admissionfee
                    var drow1 = FeeParticular.AsEnumerable().Where(x => x.Field<string>("ParticularName").Equals("New Admission Fees"));
                    foreach (DataRow r in drow1)
                    {
                        newadmission.Add(r["Std-Div"].ToString(), r["Total"].ToString());
                    }

                    //get re admission fee
                    drow1 = FeeParticular.AsEnumerable().Where(x => x.Field<string>("ParticularName").Equals("Re Admission Fees"));
                    foreach (DataRow r in drow1)
                    {
                        readmission.Add(r["Std-Div"].ToString(), r["Total"].ToString());
                    }

                    //get administrative fee
                    drow1 = FeeParticular.AsEnumerable().Where(x => x.Field<string>("ParticularName").Equals("Administrative"));
                    foreach (DataRow r in drow1)
                    {
                        administrative.Add(r["Std-Div"].ToString(), r["Total"].ToString());
                    }

                    foreach (DataRow rows in uifeescollectiontable.Rows)
                    {
                        string paidamt = "0", concessionamt = "0";
                        bookedamt = "0";
                        var datrows = FeeParticular.AsEnumerable().Where(x => x.Field<string>("Std-Div").Equals(rows["std"].ToString()));

                        if (feestotal.ContainsKey(rows["std"].ToString()))
                        {
                            bookedamt = feestotal[rows["std"].ToString()];

                        }
                        else
                        {
                            foreach (DataRow ro in datrows)
                            {
                                bookedamt = (Convert.ToInt32(bookedamt) + Convert.ToInt32(ro["Total"])).ToString();

                            }
                            feestotal.Add(std, bookedamt);

                        }

                        if (rows["admissiontype"].ToString().Equals("newadmission"))
                        {
                            //remove readmission fee from total bookedamt
                            //only newadmission and administrative fees will be kept

                            if (readmission.ContainsKey(rows["std"].ToString()))
                            {
                                readmissionfee = readmission[rows["std"].ToString()];
                            }

                            bookedamt = (Convert.ToInt32(bookedamt) - Convert.ToInt32(readmissionfee)).ToString();

                        }
                        else if (rows["admissiontype"].ToString().Equals("readmission"))
                        {
                            //remove newadmissionfee and administrative fees
                            //only readmissionfees will be kept
                            if (newadmission.ContainsKey(rows["std"].ToString()))
                            {
                                newadmssionfee = newadmission[rows["std"].ToString()];
                            }

                            if (administrative.ContainsKey(rows["std"].ToString()))
                            {
                                administrativefee = administrative[rows["std"].ToString()];
                            }



                            bookedamt = (Convert.ToInt32(bookedamt) - Convert.ToInt32(newadmssionfee) - Convert.ToInt32(administrativefee)).ToString();
                        }
                        else if (rows["admissiontype"].ToString().Equals("regular"))
                        {
                            //remove newadmissionfee ,administrativefee and readmissionfee
                            if (newadmission.ContainsKey(rows["std"].ToString()))
                            {
                                newadmssionfee = newadmission[rows["std"].ToString()];
                            }
                            if (readmission.ContainsKey(rows["std"].ToString()))
                            {
                                readmissionfee = readmission[rows["std"].ToString()];
                            }
                            if (administrative.ContainsKey(rows["std"].ToString()))
                            {
                                administrativefee = administrative[rows["std"].ToString()];
                            }

                            bookedamt = (Convert.ToInt32(bookedamt) - Convert.ToInt32(newadmssionfee) - Convert.ToInt32(readmissionfee) - Convert.ToInt32(administrativefee)).ToString();
                        }



                        query = "select sum(Cast(amtpaid as int)) From StudentFees where std='" + rows["std"].ToString() + "' and grno='" + rows["grno"].ToString() + "' and  (receiptstatus is null) and receiptdate between '" + fromdate + "' and '" + todate + "' and academicyear='" + academicyear + "';";
                        cmd = new SqlCommand(query, con);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            paidamt = reader[0].ToString();
                        }
                        reader.Close();

                        query = "select sum(Cast(Concession as int)) From StudentFees where std='" + rows["std"].ToString() + "' and grno='" + rows["grno"].ToString() + "' and  (receiptstatus is null) and receiptdate between '" + fromdate + "' and '" + todate + "' and academicyear='" + academicyear + "';";
                        cmd = new SqlCommand(query, con);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            concessionamt = reader[0].ToString();
                        }
                        reader.Close();


                        rows["bookedfees"] = bookedamt.ToString();
                        if (paidamt.Length == 0)
                        {
                            paidamt = "0";
                        }
                        rows["paidfees"] = paidamt.ToString();

                        if (concessionamt.Length == 0)
                        {
                            concessionamt = "0";
                        }

                        if (Convert.ToInt32(concessionamt) > 0)
                        {
                            rows["paidfees"] = "Concession ( " + concessionamt + " )";
                        }

                        balanceamt = (Convert.ToInt32(bookedamt) - Convert.ToInt32(paidamt) - Convert.ToInt32(concessionamt)).ToString();


                        rows["outstandingfees"] = balanceamt;

                    }

                    for (int i = uifeescollectiontable.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = uifeescollectiontable.Rows[i];
                        if (dr["outstandingfees"] != DBNull.Value && Convert.ToInt32(dr["outstandingfees"]) == 0)
                        {
                            dr.Delete();
                        }
                    }

                    uifeescollectiontable.AcceptChanges(); // Optional to commit the changes
                    lbltotalstudents.Text = uifeescollectiontable.Rows.Count.ToString();
                    //if (uifeescollectiontable.Rows.Count > 0)
                    //{
                    //    try
                    //    {
                    //        //DataRow[] dtr = uifeescollectiontable.Select("outstandingfees=0");
                    //        //foreach (var drow in dtr)
                    //        //{
                    //        //    //drow.Delete();
                    //        //    uifeescollectiontable.Rows.Remove(drow);

                    //        //}
                    //        ////  uifeescollectiontable.AcceptChanges();




                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Log.Error("FEESOutstandingReport.showOutstandingFees", ex);
                    //    }
                    //}


                }

            }
            catch (Exception ex)
            {
                Log.Error("FeesOutstandingReport.showOutstandingFees", ex);
            }

        }

    }
}