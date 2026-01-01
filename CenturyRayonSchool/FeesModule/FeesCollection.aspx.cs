//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using CenturyRayonSchool.Properties;
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
    public partial class FeesCollection : System.Web.UI.Page
    {
        DataTable uifeescollectiontable = new DataTable();
        Label lblusercode = new Label();
        public string isFeesAdmin = "", std_sess = "", div_sess = "";

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

                cmbAcademicyear.Text = year;

                // feesCollectionGrid();

            }

            if (Session["userid"] != null)
            {
                string userid = Session["username"].ToString();
                if (userid != "adminfees")
                {
                    isFeesAdmin = "c-visible";
                }
                else
                {
                    isFeesAdmin = "";
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }


            uifeescollectiontable.Columns.Add("RollNo");
            uifeescollectiontable.Columns.Add("GRNO");
            uifeescollectiontable.Columns.Add("StudentName");
            uifeescollectiontable.Columns.Add("STD");
            uifeescollectiontable.Columns.Add("DIV");
            uifeescollectiontable.Columns.Add("Academicyear");
            uifeescollectiontable.Columns.Add("admissiontype");
            uifeescollectiontable.Columns.Add("Computer");
            uifeescollectiontable.Columns.Add("Interactive");
            uifeescollectiontable.Columns.Add("ELibrary");
            uifeescollectiontable.Columns.Add("OtherFees");
            uifeescollectiontable.Columns.Add("ReAdmissionFees");
            uifeescollectiontable.Columns.Add("NewAdmissionFees");
            uifeescollectiontable.Columns.Add("Administrative");
            uifeescollectiontable.Columns.Add("AmountPaid");
            uifeescollectiontable.Columns.Add("Total");
            uifeescollectiontable.Columns.Add("feestatus");
            uifeescollectiontable.Columns.Add("freeshipamount");
            uifeescollectiontable.Columns.Add("freeshiptype");
            uifeescollectiontable.Columns.Add("ReceiptDate");
        }

        //public void feesCollectionGrid()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.AddRange(new DataColumn[13] { new DataColumn("RollNo"), new DataColumn("GRNO"),
        //    new DataColumn("StudentName"), new DataColumn("ComputerFees"),
        //    new DataColumn("InteractiveFees"),new DataColumn("Library"), new DataColumn("OtherFees"),
        //    new DataColumn("ReAdmission"), new DataColumn("NewAdmission"), new DataColumn("Administrative"), new DataColumn("ELibraryFees"),new DataColumn("formfee"),
        //    new DataColumn("TotalAmount")});
        //    dt.Rows.Add("1", "1234", "Insiya Kanchwala", "300", "300", "900", "599", "300", "300", "599", "3890", "0", "0");

        //    GridCollection.DataSource = dt;
        //    GridCollection.DataBind();
        //}

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
                    string query = "select std from std where std not in ('ALL','LEFT');";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    DataTable std = new DataTable();
                    adap.Fill(std);
                    std.Rows.Add("ALL");
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
                        cmbStd.SelectedValue = "ALL";
                        cmbStd.Enabled = true;
                    }

                    query = "select Div From Div where div Not IN ('ALL');";
                    adap = new SqlDataAdapter(query, con);
                    DataTable div = new DataTable();
                    adap.Fill(div);
                    div.Rows.Add("ALL");
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
                        cmbDiv.SelectedValue = "ALL";
                        cmbDiv.Enabled = true;
                    }

                    query = "select [year] From Academicyear order by [status] asc;";
                    adap = new SqlDataAdapter(query, con);
                    DataTable academicyear = new DataTable();
                    adap.Fill(academicyear);
                    academicyear.Rows.Add("Select Year");
                    cmbAcademicyear.DataSource = academicyear;
                    cmbAcademicyear.DataBind();
                    cmbAcademicyear.DataTextField = "year";
                    cmbAcademicyear.DataValueField = "year";
                    cmbAcademicyear.DataBind();
                    cmbAcademicyear.SelectedValue = "Select Year";

                    if (std_sess == "" && div_sess == "")
                    {

                        query = "select grno,fullname from studentmaster where  academicyear='" + lblacademicyear.Text + "' and (leftstatus IS NULL OR leftstatus = '') ";
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
                    }
                    else
                    {
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
                    }
                    GridCollection.DataSource = uifeescollectiontable;
                    GridCollection.DataBind();

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

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            //IF ANY CHANGES ARE In EXCISTING SOLUTION ALSO CHANGE IN PARENT MODULE ON PARENT DASHBOARD
            SqlConnection con = null;
            DataTable FeeHeader = new DataTable();
            try
            {
                DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "select Fee_Header from FeeHeader;";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    adap.Fill(FeeHeader);

                    FeesModel fm = new FeesModel();
                    foreach (GridViewRow row in GridCollection.Rows)
                    {
                        string feesstatus = ((Label)row.FindControl("lblfeesstatus")).Text;

                        if (((CheckBox)row.FindControl("chkSelect")).Checked && feesstatus == "Pending")
                        {
                            string rollno = row.Cells[0].Text;
                            string grno = row.Cells[1].Text;
                            string StudentName = row.Cells[2].Text;
                            string STD = row.Cells[3].Text;
                            string DIV = row.Cells[4].Text;
                            string Academicyear = row.Cells[5].Text;
                            string admissiontype = row.Cells[6].Text;
                            string Computer = row.Cells[7].Text;
                            string Interactive = row.Cells[8].Text;
                            string ELibrary = row.Cells[9].Text;
                            string OtherFees = row.Cells[10].Text;
                            string ReAdmissionFees = row.Cells[11].Text;
                            string NewAdmissionFees = row.Cells[12].Text;
                            string Administrative = row.Cells[13].Text;



                            string Freeship = ((TextBox)row.FindControl("txtFreeship")).Text;
                            string Total = ((Label)row.FindControl("lbltotalfees")).Text;
                            string amountpaid = ((Label)row.FindControl("lblAmountpaid")).Text;

                            string receiptdate = ((TextBox)row.FindControl("ReceiptDate")).Text;
                            receiptdate = receiptdate.Replace('-', '/');

                            RadioButton freeship1 = ((RadioButton)row.FindControl("radioFreeship"));
                            RadioButton freeship25 = ((RadioButton)row.FindControl("radio25freeship"));

                            string freeshiptype = "";

                            if (freeship1.Checked)
                            {
                                freeshiptype = "Freeship";
                            }
                            else if (freeship25.Checked)
                            {
                                freeshiptype = "25%";
                            }

                            double amtpaid = Convert.ToDouble(Total) - Convert.ToDouble(Freeship);

                            int count = 0;
                            query = "select count(*) from studentfees where std='" + STD + "' and grno='" + grno + "' and academicyear='" + Academicyear + "' and (receiptstatus not in ('cancelled') or receiptstatus is null);";
                            SqlCommand cmd = new SqlCommand(query, con);
                            var rcnt = cmd.ExecuteScalar();

                            if (!string.IsNullOrEmpty(rcnt.ToString()))
                            {
                                count = Convert.ToInt32(rcnt);

                            }


                            if (count == 0)
                            {
                                long rcptno = fm.loadreceipt2(con);

                                //create first record in studentfees table
                                StudentFees sf = new StudentFees();
                                sf.Receiptno = rcptno.ToString();
                                //sf.Receiptdate = cdt.ToString("yyyy/MM/dd").Replace('-', '/');
                                sf.Receiptdate = receiptdate;
                                sf.Studentname = StudentName;
                                sf.Std = STD;
                                sf.Div = DIV;
                                sf.Grno = grno;
                                sf.Cardid = "-";
                                sf.Paymode = "Cash";
                                sf.modeno = "-";
                                sf.Bankname = "-";
                                sf.Branch = "-";
                                sf.Fine = "0";
                                sf.Concession = Freeship;
                                sf.Totalamt = Total;
                                sf.Amtpaid = amtpaid.ToString("00");
                                sf.balanceamt = "0";
                                sf.ActualFees = Total;
                                sf.fullfees = "1";
                                sf.Accountname = "-";
                                sf.academicyear = cmbAcademicyear.Text;
                                sf.period = "-";
                                sf.cashiername = lblusercode.Text;
                                sf.freeshiptype = freeshiptype;


                                //create fees particular record in ReceiptReport table
                                List<ReceiptReport> listrr = new List<ReceiptReport>();
                                foreach (DataRow ro in FeeHeader.Rows)
                                {
                                    ReceiptReport rr = new ReceiptReport();
                                    rr.Receiptno = sf.Receiptno;
                                    switch (ro["Fee_Header"].ToString())
                                    {
                                        case "Computer":
                                            rr.Particular = ro["Fee_Header"].ToString();
                                            rr.Amount = Computer;
                                            rr.AmtPaid = Computer;
                                            break;
                                        case "Interactive":
                                            rr.Particular = ro["Fee_Header"].ToString();
                                            rr.Amount = Interactive;
                                            rr.AmtPaid = Interactive;
                                            break;
                                        case "E Library":
                                            rr.Particular = ro["Fee_Header"].ToString();
                                            rr.Amount = ELibrary;
                                            rr.AmtPaid = ELibrary;
                                            break;
                                        case "Other Fees":
                                            rr.Particular = ro["Fee_Header"].ToString();
                                            rr.Amount = OtherFees;
                                            rr.AmtPaid = OtherFees;
                                            break;
                                        case "Re Admission Fees":
                                            rr.Particular = ro["Fee_Header"].ToString();
                                            rr.Amount = ReAdmissionFees;
                                            rr.AmtPaid = ReAdmissionFees;
                                            break;
                                        case "New Admission Fees":
                                            rr.Particular = ro["Fee_Header"].ToString();
                                            rr.Amount = NewAdmissionFees;
                                            rr.AmtPaid = NewAdmissionFees;
                                            break;
                                        case "Administrative":
                                            rr.Particular = ro["Fee_Header"].ToString();
                                            rr.Amount = Administrative;
                                            rr.AmtPaid = Administrative;
                                            break;



                                    }

                                    if (rr.Amount != null && rr.Amount != "0")
                                    {
                                        listrr.Add(rr);
                                    }

                                }

                                //check for concession given
                                if (Freeship != null && Freeship != "0")
                                {
                                    ReceiptReport rr = new ReceiptReport();
                                    rr.Receiptno = sf.Receiptno;
                                    rr.Particular = "Concession";
                                    rr.Amount = Freeship;
                                    rr.AmtPaid = Freeship;


                                    listrr.Add(rr);

                                }


                                //save the entry to the database
                                string resp = fm.saveFeesDetails(con, sf, listrr);

                                if (resp != "ok")
                                {
                                    throw new Exception(resp);
                                }

                            }
                            else
                            {
                                throw new Exception("Duplicate Fees Entry Found For Student Name : " + StudentName);
                            }

                        }
                    }



                }

                lblinfomsg.Text = "Fees Saved Successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);

            }
            catch (Exception ex)
            {
                Log.Error("FeesCollection.btnSave_ServerClick", ex);

                lblalertmessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
            }
            finally
            {
                if (con != null) { con.Close(); }

                fillGridView();
            }
        }

        protected void FetchData_ServerClick(object sender, EventArgs e)
        {


            fillGridView();
        }

        public void fillGridView()
        {
            DataTable stud_tbl = new DataTable();
            DataTable FeeParticular_table = new DataTable();
            SqlConnection con = null;
            try
            {
                DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                string query = "", select_std = "", select_div = "", select_gr = "", academicyear = "", feesstatus = "", freeshipamount = "0", ReceiptDate = "";

                string Amtpaid = "", freeshiptype = "";

                select_std = cmbStd.SelectedValue.ToString();
                select_div = cmbDiv.SelectedValue.ToString();
                select_gr = cmbstudentname.SelectedValue.ToString();
                academicyear = cmbAcademicyear.Text;

                using (con = Connection.getConnection())
                {
                    con.Open();

                    query = "select [std-div],particularname,fee_code,total,Academicyear from FeeParticular where Academicyear='" + academicyear + "';";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    adap.Fill(FeeParticular_table);


                    if (select_gr.Equals("ALL") && select_std.Equals("ALL") && select_div.Equals("ALL"))
                    {
                        query = "select ROLLNO,GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,Academicyear,admissiontype from studentmaster where  academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '') order by Cast(ROLLNO as int) asc ";
                    }
                    else if (select_gr.Equals("ALL") && select_div.Equals("ALL"))
                    {
                        query = "select ROLLNO,GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,Academicyear,admissiontype from studentmaster where std='" + select_std + "'  and academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '')  order by Cast(ROLLNO as int) asc;";
                    }
                    else if (select_gr.Equals("ALL") && !select_std.Equals("ALL") && !select_div.Equals("ALL"))
                    {
                        query = "select ROLLNO,GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,Academicyear,admissiontype from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '')  order by Cast(ROLLNO as int) asc;";
                    }
                    else
                    {
                        query = "select ROLLNO,GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,Academicyear,admissiontype from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' and GRNO='" + select_gr + "' and (leftstatus IS NULL OR leftstatus = '')  order by Cast(ROLLNO as int) asc;";
                    }

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.Fill(stud_tbl);
                    foreach (DataRow ro in stud_tbl.Rows)
                    {
                        Amtpaid = "0";
                        freeshiptype = "";
                        double Computer = 0, Interactive = 0, OtherFees = 0, ReAdmissionFees = 0, NewAdmissionFees = 0, Administrative = 0, ELibrary = 0, Total = 0;

                        //fetch computer fees
                        var dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("Computer")).DefaultIfEmpty(null).FirstOrDefault();

                        if (dr != null)
                        {
                            Computer = Convert.ToDouble(dr["total"]);
                        }

                        //fetch Interactive fees
                        dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("Interactive")).DefaultIfEmpty(null).FirstOrDefault();

                        if (dr != null)
                        {
                            Interactive = Convert.ToDouble(dr["total"]);
                        }


                        //fetch Other Fees fees
                        dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("Other Fees")).DefaultIfEmpty(null).FirstOrDefault();

                        if (dr != null)
                        {
                            OtherFees = Convert.ToDouble(dr["total"]);
                        }


                        if (ro["admissiontype"].ToString().Equals("readmission"))
                        {
                            //fetch Re Admission Fees
                            dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("Re Admission Fees")).DefaultIfEmpty(null).FirstOrDefault();

                            if (dr != null)
                            {
                                ReAdmissionFees = Convert.ToDouble(dr["total"]);
                            }

                        }


                        if (ro["admissiontype"].ToString().Equals("newadmission"))
                        {
                            //fetch New Admission Fees 
                            dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("New Admission Fees")).DefaultIfEmpty(null).FirstOrDefault();

                            if (dr != null)
                            {
                                NewAdmissionFees = Convert.ToDouble(dr["total"]);
                            }

                            //fetch Administrative 
                            dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("Administrative")).DefaultIfEmpty(null).FirstOrDefault();

                            if (dr != null)
                            {
                                Administrative = Convert.ToDouble(dr["total"]);
                            }


                        }

                        //fetch E Library 
                        dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("E Library")).DefaultIfEmpty(null).FirstOrDefault();

                        if (dr != null)
                        {
                            ELibrary = Convert.ToDouble(dr["total"]);
                        }

                        Total = Computer + Interactive + OtherFees + ReAdmissionFees + NewAdmissionFees + Administrative + ELibrary;

                        //check for full fees paid
                        query = "select ReceiptDate,Totalamt,Amtpaid,freeshiptype,count(*) as cnt from studentfees where std=@std and grno=@grno and academicyear=@academicyear and fullfees='1' and (receiptstatus not in ('cancelled') or receiptstatus is null) Group By Totalamt,Amtpaid,freeshiptype,ReceiptDate;";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@std", ro["STD"].ToString());
                        cmd.Parameters.AddWithValue("@grno", ro["GRNO"].ToString());
                        cmd.Parameters.AddWithValue("@academicyear", ro["Academicyear"].ToString());
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Amtpaid = reader["Amtpaid"].ToString();
                                freeshiptype = reader["freeshiptype"].ToString();

                                if (Convert.ToInt32(reader["cnt"]) > 0)
                                {
                                    feesstatus = "Paid";
                                }
                                else
                                {
                                    feesstatus = "Pending";
                                }

                                ReceiptDate = reader["ReceiptDate"].ToString().Replace("/", "-");

                            }
                            reader.Close();

                        }
                        else
                        {
                            feesstatus = "Pending";
                            ReceiptDate = cdt.ToString("yyyy/MM/dd").Replace("/", "-");
                        }

                        reader.Close();




                        freeshipamount = "0";
                        //check for any conncession given
                        query = "select amount from ReceiptReport where std=@std and grno=@grno and academicyear=@academicyear and Particular='Concession' and (receiptstatus not in ('cancelled') or receiptstatus is null);";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@std", ro["STD"].ToString());
                        cmd.Parameters.AddWithValue("@grno", ro["GRNO"].ToString());
                        cmd.Parameters.AddWithValue("@academicyear", ro["Academicyear"].ToString());
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            freeshipamount = reader["amount"].ToString();
                        }
                        reader.Close();

                        uifeescollectiontable.Rows.Add(ro["ROLLNO"].ToString(), ro["GRNO"].ToString(), ro["StudentName"].ToString(), ro["STD"].ToString(), ro["DIV"].ToString(), ro["Academicyear"].ToString(), ro["admissiontype"].ToString(), Computer, Interactive, ELibrary, OtherFees, ReAdmissionFees, NewAdmissionFees, Administrative, Amtpaid, Total, feesstatus, freeshipamount, freeshiptype, ReceiptDate);
                    }

                    GridCollection.DataSource = uifeescollectiontable;
                    GridCollection.DataBind();


                    lbltotalstudents.Text = stud_tbl.Rows.Count.ToString();

                    int paidcont = uifeescollectiontable.AsEnumerable().Where(x => x.Field<string>("feestatus").Equals("Paid")).Count();

                    lblpaidstud.Text = paidcont.ToString();
                    lblunpaidstud.Text = (Convert.ToInt32(lbltotalstudents.Text) - paidcont).ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Error("FeesCollection.fillGridView", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
                stud_tbl.Dispose();
                FeeParticular_table.Dispose();
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
                    academicyear = cmbAcademicyear.Text;

                    if (select_std != "Select Std" && select_div != "Select Div")
                    {
                        query = "select grno,fullname from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '') ";
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
                    else
                    {
                        query = "select grno,fullname from studentmaster where academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '') ";
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

        public Boolean setRadioButtonChecked(string freeshiptype, string type)
        {
            if (freeshiptype == "Freeship" && type == "freeship")
            {
                return true;
            }
            else if (freeshiptype == "25%" && type == "25%")
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

        public string setDisabled(string status)
        {
            if (status == "Pending")
            {
                return "";
            }
            else if (status == "Paid")
            {
                return "disabled";
            }
            else
            {
                return "disabled";
            }
        }


        protected void txtFreeship_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txtFreeship1 = (TextBox)currentRow.FindControl("txtFreeship");
            Label lbltotal = (Label)currentRow.FindControl("lbltotalfees");
            Label lblAmountpaid = (Label)currentRow.FindControl("lblAmountpaid");

            double freeshipamt = 0, totalamt = 0;

            freeshipamt = Convert.ToDouble(txtFreeship1.Text);
            totalamt = Convert.ToDouble(lbltotal.Text);

            if (freeshipamt > totalamt)
            {
                txtFreeship1.Text = "0";
                lblalertmessage.Text = "Freeship Amount Cannot be greater than total amount";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
            }
            else
            {
                lblAmountpaid.Text = (totalamt - freeshipamt).ToString("00");
            }

        }

        protected void checkALL_CheckedChanged(object sender, EventArgs e)
        {
            if (checkALL.Checked == true)
            {
                foreach (GridViewRow row in GridCollection.Rows)
                {
                    ((CheckBox)row.FindControl("chkSelect")).Checked = true;
                }
            }
            else
            {
                foreach (GridViewRow row in GridCollection.Rows)
                {
                    ((CheckBox)row.FindControl("chkSelect")).Checked = false;
                }
            }
        }

        protected void GridCollection_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SqlConnection con = null;
            DataTable dttable = new DataTable();
            string total = "0", balamt = "0", grno = "";
            try
            {
                //if (e.CommandName == "printreceipt")
                //{
                //    int rownumber = Convert.ToInt32(e.CommandArgument);
                //    GridViewRow row = GridCollection.Rows[rownumber];

                //    string std = row.Cells[3].Text;
                //    string academciyear = row.Cells[5].Text;
                //    grno = row.Cells[1].Text;

                //    using (con = Connection.getConnection())
                //    {
                //        con.Open();

                //        dttable = new FeesModel().printReceiptSlip(con, academciyear, std, grno);
                //        foreach (DataRow ro in dttable.Rows)
                //        {
                //            total = ro["amtpaid"].ToString();
                //            balamt = ro["balanceamt"].ToString();
                //            break;
                //        }



                //    }

                //}
            }
            catch (Exception ex)
            {
                Log.Error("FeesCollection.GridCollection_RowCommand", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

                //ReportDocument rd = new ReportDocument();
                //rd.Load(Path.Combine(Server.MapPath("~/FeesModule/Reports"), "Cash_receipt.rpt"));
                //rd.SetDataSource(dttable);
                //rd.SetParameterValue("Total", total);
                //rd.SetParameterValue("balance", balamt);

                //string folderpath = Server.MapPath("DownloadFile");
                //string filename = "FeeReceipt_" + grno + ".pdf";

                //rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);

                //Response.ContentType = "Application/pdf";
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                //Response.TransmitFile(Server.MapPath("~/FeesModule/DownloadFile/" + filename));
                //Response.End();

                //Response.Redirect("DownloadFile.aspx");
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
                    academicyear = cmbAcademicyear.Text;

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
                    else
                    {
                        query = "select grno,fullname from studentmaster where academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '');";
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
                Log.Error("FeesCollection.cmbStd_SelectedIndexChanged", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void btnrefresh_ServerClick(object sender, EventArgs e)
        {
            fillGridView();
        }

        protected void radioFreeship_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((RadioButton)sender).Parent.Parent;
            TextBox txtFreeship1 = (TextBox)currentRow.FindControl("txtFreeship");

            Label lbltotal = (Label)currentRow.FindControl("lbltotalfees");
            Label lblAmountpaid = (Label)currentRow.FindControl("lblAmountpaid");
            CheckBox chkselect = (CheckBox)currentRow.FindControl("chkSelect");

            txtFreeship1.Enabled = true;
            txtFreeship1.Text = "0";
            lblAmountpaid.Text = lbltotal.Text;
            chkselect.Checked = true;

        }

        protected void radio25freeship_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((RadioButton)sender).Parent.Parent;
            TextBox txtFreeship1 = (TextBox)currentRow.FindControl("txtFreeship");

            Label lbltotal = (Label)currentRow.FindControl("lbltotalfees");
            Label lblAmountpaid = (Label)currentRow.FindControl("lblAmountpaid");
            CheckBox chkselect = (CheckBox)currentRow.FindControl("chkSelect");

            txtFreeship1.Enabled = false;
            txtFreeship1.Text = lbltotal.Text;
            lblAmountpaid.Text = "0";
            chkselect.Checked = true;


        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((CheckBox)sender).Parent.Parent;
            TextBox txtFreeship1 = (TextBox)currentRow.FindControl("txtFreeship");
            RadioButton radioFreeship = (RadioButton)currentRow.FindControl("radioFreeship");
            RadioButton radio25freeship = (RadioButton)currentRow.FindControl("radio25freeship");
            CheckBox chkselect = (CheckBox)currentRow.FindControl("chkSelect");
            Label lbltotal = (Label)currentRow.FindControl("lbltotalfees");
            Label lblAmountpaid = (Label)currentRow.FindControl("lblAmountpaid");

            if (!chkselect.Checked)
            {
                radioFreeship.Checked = false;
                radio25freeship.Checked = false;
                txtFreeship1.Text = "0";
                txtFreeship1.Enabled = false;
                lblAmountpaid.Text = "0";
            }


        }

        protected void cmbAcademicyear_SelectedIndexChanged(object sender, EventArgs e)
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
                    academicyear = cmbAcademicyear.Text;

                    if (select_std != "Select Std" && select_div != "Select Div")
                    {
                        query = "select grno,fullname from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '') ";
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

        protected void printbuttonrcpt_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            DataTable dttable = new DataTable();
            string total = "0", balamt = "0", grno = "";
            try
            {
                GridViewRow currentRow = (GridViewRow)((Button)sender).Parent.Parent;

                string std = currentRow.Cells[3].Text;
                string academciyear = currentRow.Cells[5].Text;
                grno = currentRow.Cells[1].Text;

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
            catch (Exception ex)
            {
                Log.Error("FeesCollection.printbuttonrcpt_Click", ex);
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

                string weburl = Settings.Default.weburl;



                //Response.ContentType = "Application/pdf";
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                //Response.TransmitFile(Server.MapPath("~/FeesModule/DownloadFile/" + filename));
                //Response.End();


                // Response.Write("<script> window.open('DownloadFile.aspx','_blank'); </script>");
            }

        }


        public string getDownloadUrl(string academicyear, string grno, string std)
        {
            return "/FeesModule/DownloadFile.aspx?action=rcpt&academicyear=" + academicyear + "&grno=" + grno + "&std=" + std;
        }
    }
}