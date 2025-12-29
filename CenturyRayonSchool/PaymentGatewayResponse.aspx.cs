using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using CenturyRayonSchool.ParentsModule;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CenturyRayonSchool
{
    public partial class PaymentGatewayResponse : System.Web.UI.Page
    {
        string responseData = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RecoverResponsePacket();
            }
        }

        void RecoverResponsePacket()
        {
            string ResponseCode = "";
            string mandatory_fields = "";
            string digitalsignature = "";
            string transection_No = "";
            string paymode = "";

            try //11.ProcessingFeeAmount
            {
                mandatory_fields = HttpContext.Current.Request.Form["mandatory fields"].ToString();
                string[] splitData = mandatory_fields.Split('|');

                //string referenceno = splitData[0];  // (Reference No.)
                //string var2 = splitData[1];  // (Sub Merchant ID)
                //string paid_ammount = splitData[2];  // 10(PG Ammount)
                //string var4 = splitData[3];  // 0(fee receipt id)
                //string receiptdate = splitData[4];  // 2024/05/02(receipt date)
                //string studentname = splitData[5];  // Amarjit Kannaiya Kumar Sharma (Student Name)
                //string grno = splitData[6];  // 20782 (Student Grno)
                //string stddiv = splitData[7];  // II-A (STD-DIV)
                //string year = splitData[8];  // 2023-2024 (Academic year)
                //string mobileno = splitData[9]; // 0 (Mobile No.)

                string referenceno = splitData[0];  // (Reference No.)
                string var2 = splitData[1];  // (Sub Merchant ID)
                string paid_ammount = splitData[2];  // 10(PG Ammount)
                string year = splitData[3];  // 2023-204(year)
                string studentname = splitData[4];  // Amarjit Kannaiya Kumar Sharma (Student Name)
                string grno = splitData[5];  // 20782 (Student Grno)
                string stddiv = splitData[6];  // II-A (STD-DIV)
                string mobileno = splitData[7];  // mobile no




                lbl_paidamnt.Text = paid_ammount;
                lbl_stddiv.Text = stddiv;
                lbl_year.Text = year;
                lbl_grno.Text = grno;
                lbl_referenceno.Text = referenceno;
                lbl_studentname.Text = studentname;
            }
            catch (Exception ex)
            {
                Lbl_error.Text += ex.Message.ToString();
                Log.Error("PaymentgatewayResponse.RecoverResponsePacket.ProcessingFeeAmount", ex);
            }

            try //paymode
            {
                paymode = HttpContext.Current.Request.Form["Payment Mode"].ToString();
                lbl_paymode.Text = paymode;
            }
            catch (Exception ex)
            {
                Lbl_error.Text += ex.Message.ToString();
                Log.Error("PaymentgatewayResponse.RecoverResponsePacket.paymode", ex);
            }
            try//Signature
            {
                digitalsignature = HttpContext.Current.Request.Form["RSV"].ToString();
                lbl_signature.Text = digitalsignature;
            }
            catch (Exception ex)
            {
                Lbl_error.Text += ex.Message.ToString();
                Log.Error("PaymentgatewayResponse.RecoverResponsePacket.Signature", ex);
            }
            try//Unique Transect no 
            {
                transection_No = HttpContext.Current.Request.Form["Unique Ref Number"].ToString();
                lbl_transectionid.Text = transection_No;
            }
            catch (Exception ex)
            {
                Lbl_error.Text += ex.Message.ToString();
                Log.Error("PaymentgatewayResponse.RecoverResponsePacket.Signature", ex);
            }
            try//1.ResponseCode
            {
                ResponseCode = HttpContext.Current.Request.Form["Response Code"].ToString();
                //Freehold_DA.Logger.LogMessageToFile("icici ResponseCode", ResponseCode);
                lbl_response_code.Text = ResponseCode;
                if (ResponseCode != "E000")
                {
                    lbl_Fail.Visible = true;
                    lbl_success.Visible = false;
                    printreceipt.Visible = false;
                    Goto_dashboard.Visible = true;
                    lbl_status.Text = "Transaction Failed.";

                    Log.event_Info("", ResponseCode);
                }
                else
                {
                    lbl_success.Visible = true;
                    lbl_Fail.Visible = false;
                    printreceipt.Visible = true;
                    Goto_dashboard.Visible = true;

                    lbl_status.Text = "Transaction Successful.";
                    Log.event_Info("", ResponseCode);
                    GenrateFees();
                }
                UpdatetransectionTable();
            }
            catch (Exception ex)
            {
                Lbl_error.Text += ex.Message.ToString();
                Log.Error("PaymentgatewayResponse.RecoverResponsePacket.Responsecode", ex);
            }


        }

        public void UpdatetransectionTable()
        {
            SqlConnection con = null;
            string query = "", transection_status = "", signature = "", responsecode = "", referenceno = "", transectionno = "";
            try
            {
                DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                transection_status = lbl_status.Text.ToString();
                signature = lbl_signature.Text.ToString();
                responsecode = lbl_response_code.Text.ToString();
                referenceno = lbl_referenceno.Text.ToString();
                transectionno = lbl_transectionid.Text.ToString();
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "update FeesOnlineTransactionTable set payment_status=@payment_status,signature=@signature,Responsecode=@Responsecode,transaction_id=@transaction_id where ref_id=@ref_id ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@payment_status", transection_status);
                    cmd.Parameters.AddWithValue("@signature", signature);
                    cmd.Parameters.AddWithValue("@Responsecode", responsecode);
                    cmd.Parameters.AddWithValue("@transaction_id", transectionno);
                    cmd.Parameters.AddWithValue("@ref_id", referenceno);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Parentdashboard.UpdatetransectionTable", ex);
                lblalertmessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
            }

        }

        public void GenrateFees()
        {
            Log.event_Info("", "Fee Generation STarted");
            string year = "", stddiv = "", std = "", div = "", grno = "", ammount = "", Responsecode = "", signature = "";
            //string  mandatory_fields = HttpContext.Current.Request.Form["mandatory fields"].ToString();
            //  string[] splitData1 = mandatory_fields.Split('|');


            //  ammount = splitData1[2];  // 10(PG Ammount)
            //  grno = splitData1[6];  // 20782 (Student Grno)
            //  stddiv = splitData1[7];  // II-A (STD-DIV)
            //  year = splitData1[8];  // 2023-2024 (Academic year)

            year = lbl_year.Text.ToString();
            grno = lbl_grno.Text.ToString();
            ammount = lbl_paidamnt.Text.ToString();
            stddiv = lbl_stddiv.Text.ToString();
            Responsecode = lbl_response_code.Text.ToString();

            string[] splitData = stddiv.Split('-');
            std = splitData[0];
            div = splitData[1];
            Log.event_Info("", grno);
            Log.event_Info("", ammount);
            Log.event_Info("", std);
            Log.event_Info("", div);
            Log.event_Info("", year);
            if (Responsecode == "E000")
            {
                Log.event_Info("", Responsecode);
                payfees(std, grno, year, ammount);
            }

        }
        public void payfees(string std, string grno, string year, string ammount)
        {
            DataTable stud_tbl = new DataTable();
            DataTable FeeParticular_table = new DataTable();
            SqlConnection con = null;
            Log.event_Info("", "Pay fees Function Started");
            try
            {
                DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                string query = "", Amtpaid = "", freeshiptype = "", balancefees = "", feesstatus = "", mobile = "", photopath = "", div = "", dob = "", doa = "", contact = "", Freeship = "0", studentname = "";
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "select [std-div],particularname,fee_code,total,Academicyear from FeeParticular where [Std-Div]='" + std + "' and Academicyear='" + year + "';";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    adap.Fill(FeeParticular_table);
                    Log.event_Info("", query);
                    query = "select ROLLNO,GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,Academicyear,admissiontype from studentmaster where std='" + std + "' and grno='" + grno + "' and academicyear='" + year + "'  and (leftstatus IS NULL OR leftstatus = '')  order by Cast(ROLLNO as int) asc;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.Fill(stud_tbl);
                    Log.event_Info("", query);
                    Log.event_Info("", "fee started");
                    foreach (DataRow ro in stud_tbl.Rows)
                    {

                        Amtpaid = "0";
                        freeshiptype = "";
                        double Computer = 0, Interactive = 0, OtherFees = 0, ReAdmissionFees = 0, NewAdmissionFees = 0, Administrative = 0, ELibrary = 0, Total = 0;

                        studentname = ro["StudentName"].ToString();
                        div = ro["div"].ToString();
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

                        // Freeship = freeship.Text;//get fresship

                        double amtpaid = Convert.ToDouble(Total) - Convert.ToDouble(Freeship);

                        //if (ammount == Convert.ToString(amtpaid))
                        //{
                        int count = 0;
                        query = "select count(*) from studentfees where std='" + std + "' and grno='" + grno + "' and academicyear='" + year + "' and (receiptstatus not in ('cancelled') or receiptstatus is null);";
                        cmd = new SqlCommand(query, con);
                        var rcnt = cmd.ExecuteScalar();

                        if (!string.IsNullOrEmpty(rcnt.ToString()))
                        {
                            count = Convert.ToInt32(rcnt);
                        }
                        Log.event_Info("", query);
                        if (count == 0)
                        {


                            long rcptno = loadreceipt2(con);

                            //create first record in studentfees table
                            StudentFees sf = new StudentFees();
                            sf.Receiptno = rcptno.ToString();
                            sf.Receiptdate = cdt.ToString("yyyy/MM/dd").Replace('-', '/');
                            sf.Studentname = studentname;
                            sf.Std = std;
                            sf.Div = div;
                            sf.Grno = grno;
                            sf.Cardid = "-";
                            sf.Paymode = lbl_paymode.Text.ToString().Trim();
                            sf.modeno = "-";
                            sf.Bankname = "-";
                            sf.Branch = "-";
                            sf.Fine = "0";
                            sf.Concession = Freeship;
                            sf.Totalamt = Total.ToString();
                            sf.Amtpaid = amtpaid.ToString("00");
                            sf.balanceamt = "0";
                            sf.ActualFees = Total.ToString();
                            sf.fullfees = "1";
                            sf.Accountname = "-";
                            sf.academicyear = year;
                            sf.period = "-";
                            sf.cashiername = "";
                            sf.freeshiptype = freeshiptype;

                            query = "select Fee_Header from FeeHeader;";
                            adap = new SqlDataAdapter(query, con);
                            DataTable FeeHeader = new DataTable();
                            adap.Fill(FeeHeader);

                            //create fees particular record in ReceiptReport table
                            List<ReceiptReport> listrr = new List<ReceiptReport>();
                            foreach (DataRow row in FeeHeader.Rows)
                            {
                                ReceiptReport rr = new ReceiptReport();
                                rr.Receiptno = sf.Receiptno;
                                switch (row["Fee_Header"].ToString())
                                {
                                    case "Computer":
                                        rr.Particular = row["Fee_Header"].ToString();
                                        rr.Amount = Computer.ToString();
                                        rr.AmtPaid = Computer.ToString();
                                        break;
                                    case "Interactive":
                                        rr.Particular = row["Fee_Header"].ToString();
                                        rr.Amount = Interactive.ToString();
                                        rr.AmtPaid = Interactive.ToString();
                                        break;
                                    case "E Library":
                                        rr.Particular = row["Fee_Header"].ToString();
                                        rr.Amount = ELibrary.ToString();
                                        rr.AmtPaid = ELibrary.ToString();
                                        break;
                                    case "Other Fees":
                                        rr.Particular = row["Fee_Header"].ToString();
                                        rr.Amount = OtherFees.ToString();
                                        rr.AmtPaid = OtherFees.ToString();
                                        break;
                                    case "Re Admission Fees":
                                        rr.Particular = row["Fee_Header"].ToString();
                                        rr.Amount = ReAdmissionFees.ToString();
                                        rr.AmtPaid = ReAdmissionFees.ToString();
                                        break;
                                    case "New Admission Fees":
                                        rr.Particular = row["Fee_Header"].ToString();
                                        rr.Amount = NewAdmissionFees.ToString();
                                        rr.AmtPaid = NewAdmissionFees.ToString();
                                        break;
                                    case "Administrative":
                                        rr.Particular = row["Fee_Header"].ToString();
                                        rr.Amount = Administrative.ToString();
                                        rr.AmtPaid = Administrative.ToString();
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
                            string resp = saveFeesDetails(con, sf, listrr);

                            if (resp != "ok")
                            {
                                throw new Exception(resp);
                            }
                        }
                        //else
                        //{
                        //    throw new Exception("Duplicate Fees Entry Found For Student Name : " + "");
                        //}
                        //}
                    }

                }
                //showFeeReceipt(year, grno, std);
            }
            catch (Exception ex)
            {
                Log.Error("Parentdashboard.payfees", ex);
                lblalertmessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
            }

        }
        public long loadreceipt2(SqlConnection con)
        {

            try
            {
                long receiptno = 0;



                String query = "Select Convert(bigint,[receiptno]) as [receiptno] From StudentFees order by [receiptno] desc";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    receiptno = Convert.ToInt64(reader[0]);
                    break;
                }
                reader.Close();
                receiptno = receiptno + 1;




                return receiptno;
            }
            catch (Exception ex)
            {
                Log.Error("ParentsDashboard.loadreceipt2", ex);
                return 0;
            }


        }
        public string saveFeesDetails(SqlConnection con, StudentFees sf, List<ReceiptReport> listrcpt)
        {
            Log.event_Info("", "FEES SAVING STARTED");
            try
            {
                DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();

                String query = "insert into StudentFees([Receiptno],[Receiptdate],[Studentname],[Std],[Div],[Grno],[Cardid],[Paymode],[modeno],[Bankname],[Branch]," +
                                "[fine],[Concession],[Totalamt],[Amtpaid],[ActualFees],balanceamt,accountname,academicyear,cashiername,freeshiptype) values(@receipt,@rreceiptdate,@studname,@std,@div,@grno,@cardid,@paymode,@modeno,@bank,@branch,@fine," +
                                "@concession,@totalamt,@amtpaid,@actualfees,@balamt,@accountname,@academicyear,@cashier,@freeshiptype)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@receipt", sf.Receiptno);
                cmd.Parameters.AddWithValue("@rreceiptdate", sf.Receiptdate);
                cmd.Parameters.AddWithValue("@studname", sf.Studentname);
                cmd.Parameters.AddWithValue("@std", sf.Std);
                cmd.Parameters.AddWithValue("@div", sf.Div);
                cmd.Parameters.AddWithValue("@grno", sf.Grno);
                cmd.Parameters.AddWithValue("@cardid", sf.Cardid);
                cmd.Parameters.AddWithValue("@paymode", sf.Paymode);
                cmd.Parameters.AddWithValue("@modeno", sf.modeno);
                cmd.Parameters.AddWithValue("@bank", sf.Bankname);
                cmd.Parameters.AddWithValue("@branch", sf.Branch);
                cmd.Parameters.AddWithValue("@fine", sf.Fine);
                cmd.Parameters.AddWithValue("@concession", sf.Concession);
                cmd.Parameters.AddWithValue("@totalamt", sf.Totalamt);
                cmd.Parameters.AddWithValue("@amtpaid", sf.Amtpaid);
                cmd.Parameters.AddWithValue("@actualfees", sf.ActualFees);
                cmd.Parameters.AddWithValue("@balamt", sf.balanceamt);
                cmd.Parameters.AddWithValue("@accountname", sf.Accountname);
                cmd.Parameters.AddWithValue("@academicyear", sf.academicyear);
                cmd.Parameters.AddWithValue("@cashier", sf.cashiername);
                cmd.Parameters.AddWithValue("@freeshiptype", sf.freeshiptype);

                cmd.ExecuteNonQuery();

                if (sf.balanceamt == "0")
                {
                    query = "update StudentFees set fullfees=1 where [grno]=@grno and [std]=@std and academicyear=@academicyear;";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@grno", sf.Grno);
                    cmd.Parameters.AddWithValue("@std", sf.Std);
                    cmd.Parameters.AddWithValue("@academicyear", sf.academicyear);

                    cmd.ExecuteNonQuery();

                    //query = "update FeesOutstanding set status='1' where std='" + stdid.Text + "' and grno='" + grid.Text + "' and academicyear='" + Logininfo._logininfo.academicyear + "';";
                    //cmd = new SqlCommand(query, con);

                    //cmd.ExecuteNonQuery();
                }

                foreach (ReceiptReport rr in listrcpt)
                {
                    query = "insert into ReceiptReport([Receiptno],[Studentname],[std_div],[Grno],[Particular],[Amount],AmtPaid,std,accountname,academicyear) values(@receipt,@studname,@stddiv,@grno,@parti,@amount,@AmtPaid,@std,@account,@academic);";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@receipt", sf.Receiptno);
                    cmd.Parameters.AddWithValue("@studname", sf.Studentname);
                    cmd.Parameters.AddWithValue("@stddiv", sf.Std + " " + sf.Div);

                    cmd.Parameters.AddWithValue("@grno", sf.Grno);

                    cmd.Parameters.AddWithValue("@parti", rr.Particular);
                    cmd.Parameters.AddWithValue("@amount", rr.Amount);

                    cmd.Parameters.AddWithValue("@AmtPaid", rr.AmtPaid);
                    cmd.Parameters.AddWithValue("@std", sf.Std);
                    cmd.Parameters.AddWithValue("@account", sf.Accountname);
                    cmd.Parameters.AddWithValue("@academic", sf.academicyear);

                    cmd.ExecuteNonQuery();
                }

                Log.event_Info("", "FEES SAVING done");
                return "ok";
            }
            catch (Exception ex)
            {
                Log.Error("ParentsDashboard.saveFeesDetails", ex);
                return ex.Message;
            }
        }
        protected void printreceipt_Click(object sender, EventArgs e)
        {
            string year = "", grno = "", std = "", stddiv = "";
            year = lbl_year.Text.ToString();
            grno = lbl_grno.Text.ToString();

            stddiv = lbl_stddiv.Text.ToString();
            string[] splitData = stddiv.Split('-');
            std = splitData[0];
            //div = splitData[1];
            Log.event_Info("", "Receipt Printing Started");
            Log.event_Info("", grno);
            Log.event_Info("", std);
            Log.event_Info("", year);
            showFeeReceipt(year, grno, std);
        }

        public void showFeeReceipt(string academicyear, string grno, string std)
        {
            string fileurl = "";
            SqlConnection con = null;
            DataTable dttable = new DataTable();
            string total = "0", balamt = "0";
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();

                    dttable = new ParentsDashboard().printReceiptSlip(con, academicyear, std, grno);
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
                Log.Error("Printslip.showFeeReceipt", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/FeesModule/Reports"), "Cash_receipt.rpt"));
                rd.SetDataSource(dttable);
                rd.SetParameterValue("Total", total);
                rd.SetParameterValue("balance", balamt);

                // Export report to a stream
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

                // Create a memory stream and copy content from the exported stream
                MemoryStream memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                stream.Close(); // Close the original stream

                // Set response headers for file download
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=FeeReceipt_" + grno + ".pdf");

                // Write the file content to the response
                Response.BinaryWrite(memoryStream.ToArray());
                Response.End();
            }
        }


        protected void Goto_dashboard_Click(object sender, EventArgs e)
        {
            string studentname = "", std = "", grno = "", year = "", userid = "", stddiv = "";
            studentname = lbl_studentname.Text.ToString();
            stddiv = lbl_stddiv.Text.ToString();
            grno = lbl_grno.Text.ToString();
            year = lbl_year.Text.ToString();
            string[] splitData = stddiv.Split('-');
            std = splitData[0];

            Session["std"] = std;
            Session["ParentPassword"] = grno;
            Session["academicyear"] = year;
            Session["fullname"] = studentname;
            Session["GRNO"] = grno;
            Session["userid"] = grno;

            Response.Redirect("ParentsModule/ParentsDashboard.aspx");
        }
    }
}