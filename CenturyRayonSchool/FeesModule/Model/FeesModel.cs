using CenturyRayonSchool.FeesModule.Reports;
//using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CenturyRayonSchool.FeesModule.Model
{
    public class FeesModel
    {

        public string setActiveAcademicYear()
        {
            SqlConnection con = null;
            string year = "";
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "select [year] from Academicyear where [iscurrentyear]='1';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        year = reader[0].ToString();
                    }
                    reader.Close();

                }
                return year;
            }
            catch (Exception ex)
            {
                Log.Error("FeesModel.setActiveAcademicYear", ex);
                return "Error:Academic Year Not Found";
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        public DataTable GetAcademicYearList(SqlConnection con)
        {
            DataTable academictbl = new DataTable();
            try
            {
                string query = "select [year] from Academicyear order by [status];";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(academictbl);
                academictbl.Rows.Add("Select Academic Year");

                return academictbl;
            }
            catch (Exception ex)
            {
                Log.Error("FeesModel.GetAcademicYearList", ex);
                return academictbl;
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
                Log.Error("FeesModel.loadreceipt2", ex);
                return 0;
            }
            finally
            {

            }

        }


        public string saveFeesDetails(SqlConnection con, StudentFees sf, List<ReceiptReport> listrcpt)
        {
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


                return "ok";
            }
            catch (Exception ex)
            {
                Log.Error("FeesModel.saveFeesDetails", ex);
                return ex.Message;
            }
        }


        public DataTable printReceiptSlip(SqlConnection con, string academciyear, string std, string grno)
        {
            ReceiptDS feestable = new ReceiptDS();
            try
            {
                string total = "0", balamt = "0";
                String query = "Select vsr.Receiptno,vsr.Studentname,(vsr.std+' '+vsr.div) as std_div,vsr.grno,vsr.particular,vsr.paymode,vsr.modeno,vsr.bankname,vsr.branch,CONVERT(VARCHAR(10),Cast(vsr.receiptdate as Date), 103) as receiptdate,vsr.FeeHeadr_AliasName, " +
                               "('Fees Paid Vide ' + Paymode + '  No.' + modeno + '  Bank Name:' + Bankname + '  Branch Name:' + Branch + '  Amount:''  Dated:' + Receiptdate) AS ChqStatement,vsr.amtpaid,vsr.period,vsr.cashiername,vsr.academicyear,vsr.std,vsr.balanceamt,vsr.amount " +
                               "From View_StudentFees_Receiptreport as vsr " +
                               "where grno='" + grno + "' and vsr.std='" + std + "' and vsr.academicyear='" + academciyear + "' and (vsr.receiptstatus not in ('cancelled') or vsr.receiptstatus is null);";
                SqlDataAdapter adap = new SqlDataAdapter(query, con);
                adap.Fill(feestable.Tables[0]);

                //foreach(DataRow ro in feestable.Tables[0].Rows)
                //{
                //    total = ro["amtpaid"].ToString();
                //    balamt = ro["balanceamt"].ToString();
                //    break;
                //}


                //Cash_receipt cr = new Cash_receipt();
                //cr.SetDataSource(feestable.Tables[0]);
                //cr.SetParameterValue("Total", total);
                //cr.SetParameterValue("balance", balamt);

                return feestable.Tables[0];
            }
            catch (Exception ex)
            {
                Log.Error("FeesModel.printReceiptSlip", ex);
                return null;
            }
        }

        public string CancelReceipt(SqlConnection con, string std, string grno, string receiptno)
        {
            try
            {
                string query = "Update StudentFees set ReceiptStatus = 'cancelled', Reason ='-',chequecleardate='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "',fullfees='0' where [Receiptno]=@receipt and std=@std and grno=@grno;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@receipt", receiptno);
                cmd.Parameters.AddWithValue("@std", std);
                cmd.Parameters.AddWithValue("@grno", grno);
                cmd.ExecuteNonQuery();

                query = "update receiptreport set receiptstatus='cancelled' where [Receiptno]=@receipt and std=@std and grno=@grno;";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@receipt", receiptno);
                cmd.Parameters.AddWithValue("@std", std);
                cmd.Parameters.AddWithValue("@grno", grno);
                cmd.ExecuteNonQuery();

                return "ok";

            }
            catch (Exception ex)
            {
                Log.Error("FeesModel.CancelReceipt", ex);
                return ex.Message;
            }
        }


    }


    public class StudentFees
    {
        public string Receiptno { get; set; }
        public string Receiptdate { get; set; }
        public string Studentname { get; set; }
        public string Std { get; set; }
        public string Div { get; set; }
        public string Grno { get; set; }
        public string Cardid { get; set; }
        public string Paymode { get; set; }
        public string modeno { get; set; }
        public string Bankname { get; set; }
        public string Branch { get; set; }
        public string Fine { get; set; }
        public string Concession { get; set; }

        public string Totalamt { get; set; }
        public string Amtpaid { get; set; }
        public string balanceamt { get; set; }
        public string fullfees { get; set; }
        public string ActualFees { get; set; }
        public string receiptstatus { get; set; }

        public string reason { get; set; }
        public string chequecleardate { get; set; }
        public string fine_bounce { get; set; }
        public string fine_bounce_rcpt { get; set; }

        public string Accountname { get; set; }
        public string academicyear { get; set; }
        public string period { get; set; }
        public string cashiername { get; set; }

        public string freeshiptype { get; set; }

    }

    public class ReceiptReport
    {
        public string Receiptno { get; set; }
        public string Studentname { get; set; }

        public string std_div { get; set; }
        public string Grno { get; set; }
        public string Particular { get; set; }
        public string Amount { get; set; }
        public string AmtPaid { get; set; }
        public string std { get; set; }
        public string receiptstatus { get; set; }
        public string Accountname { get; set; }
        public string period { get; set; }
        public string academicyear { get; set; }


    }
}