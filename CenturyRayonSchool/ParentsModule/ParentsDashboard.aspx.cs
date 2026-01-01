using CenturyRayonSchool.FeesModule.Reports;
//using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;

namespace CenturyRayonSchool.ParentsModule
{
    public partial class ParentsDashboard : System.Web.UI.Page
    {
        string std = "", grno = "", year = "", userid;
        public string filepath = "";
        public string studentbirthday = "";
        public string staffbirthday = "";
        public string upcommingevent = "";
        public Boolean isEvent = true;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["GRNO"] != null)
            {
                fullname.Text = Session["fullname"].ToString();
                std = Session["std"].ToString();
                grno = Session["GRNO"].ToString();
                year = Session["academicyear"].ToString();
                userid = Session["userid"].ToString();
                lbl_year.Text = year;
            }
            else
            {
                Response.Redirect("~/ParentsModule/ParentsLogin.aspx");
            }
            if (!IsPostBack)
            {
                GetStudentDetails(std, grno, year);
                GetStudentFeesDetails(std, grno, year);

                DataTable News = new NewsEventsModel().GetNewsList();

                ListViewNews.DataSource = News;
                ListViewNews.DataBind();

                DataTable Event = new EventModel().GetUpCommingEventList();

                List<TodayBirthday> listbirth = new IndexPageModel().getTodayBirthday();

                List<Eventbl> listevent = new EventModel().Get_Last4_UpCommingEventList();
                if (listevent.Count == 0)
                {
                    isEvent = false;
                }

                foreach (TodayBirthday tb in listbirth)
                {
                    if (tb.isstaff)
                    {
                        staffbirthday += "<li style=\"font-size:17px;font-family:sans-serif; margin:10px;\">" +
                                        "<a class=\"hyper\"  style=\"color:white;\">" + tb.fullname + "</a>" +
                                        "</li>";
                    }
                    else
                    {
                        studentbirthday += "<li style=\"font-size:17px;font-family:sans-serif; margin:10px;\">" +
                                        "<a class=\"hyper\"  style=\"color:white;\">" + tb.fullname + " " + tb.std + "-" + tb.div + "</a>" +
                                        "</li>";
                    }
                }


                ListViewEvent.DataSource = Event;
                ListViewEvent.DataBind();

                int i = 1;
                foreach (Eventbl etb in listevent)
                {
                    string nicdark = "", day = "", month = "", year = "";
                    DateTime dt = Convert.ToDateTime(etb.startDate);

                    month = Connection.GetMonthsName(dt.Month);

                    switch (i)
                    {
                        case 1:
                            nicdark = "nicdark_bg_green"; i = i + 1;
                            break;
                        case 2:
                            nicdark = "nicdark_bg_violet"; i = i + 1;
                            break;
                        case 3:
                            nicdark = "nicdark_bg_yellow"; i = i + 1;
                            break;
                        case 4:
                            nicdark = "nicdark_bg_orange"; i = i + 1;
                            break;

                    }



                    upcommingevent += "<div class=\"grid grid_3\">" +
                                       "<!--archive1-->" +
                                       "<div class=\"nicdark_archive1 " + nicdark + " nicdark_radius nicdark_shadow\">" +
                                        "<a href = \"EventDescripition.aspx?id=" + etb.id + "\" class=\"nicdark_btn nicdark_bg_greydark white medium nicdark_radius a-link nicdark_absolute_left\" style=\"margin-top:0px; margin-left:0px; padding: 5px 10px;\">" + dt.Day + "<br/><small>" + month.Substring(0, 3) + "</small></a>" +
                                        "<img alt = \"\"  src=\"" + etb.thumbnailimgpath + "\"  style=\"height:200px\"/>" +
                                        "<div class=\"nicdark_textevidence nicdark_bg_greydark\">" +
                        "<h4 class=\"white nicdark_margin20\">" + etb.eventName + "</h4>" +
                "</div>" +
                "<div class=\"nicdark_margin20\">" +
                    "<h5 class=\"white\" style='color:black;'><i class=\"icon-pin-outline\"></i>" + etb.venue + "</h5>" +
                    "<div class=\"nicdark_space10\"></div>" +
                    "<h5 class=\"white\" style='color:black;'><i class=\"icon-clock-1\"></i> " + etb.starttime + " To " + etb.endtime + "</h5>" +
                    "<div class=\"nicdark_space20\"></div>" +
                    "<div class=\"nicdark_divider left small\" style='color:white;'><span class=\"nicdark_bg_white nicdark_radius\"></span></div>" +
                    "<div class=\"nicdark_space20\"></div>" +
                    "<p class=\"white\" style='color:black;'>" + etb.eventDescription + "</p>" +
                    "<div class=\"nicdark_space20\"></div>" +
                "</div>" +
            "</div>" +
            "<!--archive1-->" +
        "</div>";




                }

            }
            string amount = "";
            amount = balanceamnt.Text;
            if (string.IsNullOrEmpty(amount) || !amount.Any(char.IsDigit) || amount.All(char.IsDigit) && amount.Equals("0"))
            {
                //paynow.Visible = false;
                //paynow.Visible = true;

                printreceipt.Visible = true;
                paynow_online.Visible = false;
            }
            else
            {
                printreceipt.Visible = false;
                //paynow.Visible = false;
                //paynow.Visible = true;
                paynow_online.Visible = true;
            }

        }

        protected void printreceipt_Click(object sender, EventArgs e)
        {
            GenerateReceipt();
        }
        private void GenerateReceipt()
        {
            //string receipt = getDownloadUrl(year, grno, std);
            //// Construct JavaScript to open the URL in a new tab
            //string script = "window.open('" + receipt + "', '_blank');";
            //// Register the script to execute on client side
            //ClientScript.RegisterStartupScript(this.GetType(), "OpenNewTabScript", script, true);

            string receipt = getDownloadUrl(year, grno, std);
            Response.Redirect(receipt);
        }

        protected void paynow_Click(object sender, EventArgs e)
        {
            payfees(std, grno, year);
        }


        public string getDownloadUrl(string academicyear, string grno, string std)
        {
            return "/ParentsModule/Printslip.aspx?action=rcpt&academicyear=" + academicyear + "&grno=" + grno + "&std=" + std;
        }

        public void GetStudentDetails(string std, string grno, string year)
        {
            SqlConnection con = null;
            try
            {
                string query = "", sname = "", fname = "", mname = "", rollno = "", mobile = "", photopath = "", div = "", dob = "", doa = "", contact = "", add = "", cid = "";
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "select (FNAME+' '+MNAME+' '+LNAME) as fullname , ROLLNO,GRNO,std,div,MNAME,MOTHERNAME,MOBILE,webphotopath,CONVERT(varchar(10), CONVERT(date, DOA, 111), 103) as DOA,CONVERT(varchar(10), CONVERT(date, DOB, 111), 103) as DOB,contact2,address,cid from studentmaster where std='" + std + "' and grno='" + grno + "' and academicyear='" + year + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        sname = reader["fullname"].ToString();
                        rollno = reader["ROLLNO"].ToString();
                        fname = reader["MNAME"].ToString();
                        mname = reader["MOTHERNAME"].ToString();
                        mobile = reader["MOBILE"].ToString();
                        photopath = reader["webphotopath"].ToString();
                        div = reader["div"].ToString();
                        doa = reader["DOA"].ToString();
                        dob = reader["DOB"].ToString();
                        contact = reader["contact2"].ToString();
                        add = reader["address"].ToString();
                        cid = reader["cid"].ToString();

                    }
                    reader.Close();
                    StudentName.Text = sname;
                    mobileno.Text = mobile;
                    father.Text = fname;
                    mother.Text = mname;
                    standared.Text = std;
                    division.Text = div;
                    GR.Text = grno;
                    RN.Text = rollno;
                    picturepath.ImageUrl = photopath;
                    dateofadmission.Text = doa;
                    Dateofbirth.Text = dob;
                    mobileno2.Text = contact;
                    address.Text = add;
                    txtcid.Text = cid;


                }
            }
            catch (Exception ex)
            {
                Log.Error("ParentsDashboard.GetStudentDetails", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        public void GetStudentFeesDetails(string std, string grno, string year)
        {
            DataTable stud_tbl = new DataTable();
            DataTable FeeParticular_table = new DataTable();
            SqlConnection con = null;
            try
            {
                string query = "", Amtpaid = "", freeshiptype = "", balancefees = "", feesstatus = "", Receiptno = "", photopath = "", div = "", dob = "", doa = "", contact = "", add = "";
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "select [std-div],particularname,fee_code,total,Academicyear from FeeParticular where [Std-Div]='" + std + "' and Academicyear='" + year + "';";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    adap.Fill(FeeParticular_table);

                    query = "select ROLLNO,GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,Academicyear,admissiontype from studentmaster where std='" + std + "' and grno='" + grno + "' and academicyear='" + year + "'  and (leftstatus IS NULL OR leftstatus = '')  order by Cast(ROLLNO as int) asc;";
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
                        query = "select Receiptno,ReceiptDate,Totalamt,Amtpaid,freeshiptype,count(*) as cnt from studentfees where std=@std and grno=@grno and academicyear=@academicyear and fullfees='1' and (receiptstatus not in ('cancelled') or receiptstatus is null) Group By Totalamt,Amtpaid,freeshiptype,ReceiptDate,Receiptno;";
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
                                Receiptno = reader["Receiptno"].ToString();
                            }
                        }
                        reader.Close();

                        balancefees = (Convert.ToDouble(Total) - Convert.ToDouble(Amtpaid)).ToString();
                        if (balancefees == "0")
                        {
                            feesstatus = "Full Fees Paid And Receipt No. is " + Receiptno;
                        }
                        else
                        {
                            feesstatus = "Due Fees " + balancefees;
                        }
                        totlfee.Text = Total.ToString();
                        amntpaid.Text = Amtpaid;
                        balanceamnt.Text = balancefees;
                        status.Text = feesstatus;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("ParentsDashboard.GetStudentFeesDetails", ex);
                lblalertmessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
            }
            finally
            {
                if (con != null) { con.Close(); }
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
                Log.event_Info("", query);
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

        public void payfees(string std, string grno, string year)
        {
            DataTable stud_tbl = new DataTable();
            DataTable FeeParticular_table = new DataTable();
            SqlConnection con = null;
            try
            {
                DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                string query = "", Amtpaid = "", freeshiptype = "", balancefees = "", feesstatus = "", mobile = "", photopath = "", div = "", dob = "", doa = "", contact = "", Freeship = "", studentname = "";
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "select [std-div],particularname,fee_code,total,Academicyear from FeeParticular where [Std-Div]='" + std + "' and Academicyear='" + year + "';";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    adap.Fill(FeeParticular_table);

                    query = "select ROLLNO,GRNO,(fname+' '+LNAME) as StudentName,STD,DIV,Academicyear,admissiontype from studentmaster where std='" + std + "' and grno='" + grno + "' and academicyear='" + year + "'  and (leftstatus IS NULL OR leftstatus = '')  order by Cast(ROLLNO as int) asc;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.Fill(stud_tbl);

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

                        Freeship = freeship.Text;

                        double amtpaid = Convert.ToDouble(Total) - Convert.ToDouble(Freeship);
                        int count = 0;

                        query = "select count(*) from studentfees where std='" + std + "' and grno='" + grno + "' and academicyear='" + year + "' and (receiptstatus not in ('cancelled') or receiptstatus is null);";
                        cmd = new SqlCommand(query, con);
                        var rcnt = cmd.ExecuteScalar();

                        if (!string.IsNullOrEmpty(rcnt.ToString()))
                        {
                            count = Convert.ToInt32(rcnt);
                        }
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
                            sf.Paymode = "Online Banking";
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
                        else
                        {
                            throw new Exception("Duplicate Fees Entry Found For Student Name : " + StudentName);
                        }
                    }

                }
                GenerateReceipt();
            }
            catch (Exception ex)
            {
                Log.Error("Parentdashboard.payfees", ex);
                lblalertmessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
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
                Log.Error("ParentsDashboard.saveFeesDetails", ex);
                return ex.Message;
            }
        }


        protected void paynow_online_Click(object sender, EventArgs e)
        {
            string query = "";
            Log.event_Info("paynow onlinee", "Payment Started");
            SqlConnection con = null;
            try
            {
                DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                //Create Payment Gateway Data
                PaymentGatewayModule _pm = new PaymentGatewayModule();
                //string returnurl = "http://127.0.0.1:83/PaymentGatewayResponse.aspx";
                string returnurl = "https://centuryrayonhighschool.com/PaymentGatewayResponse.aspx";
                string returnurl_encrypt = _pm.encryptFile(returnurl, _pm.secret_key);
                TransactionMode _tm = new TransactionMode();
                _tm.merchantid = "382644";
                _tm.merchantid_encrypt = _pm.encryptFile(_tm.merchantid, _pm.secret_key);
                //_tm.reference_no = "123";
                //_tm.reference_no_encrypt = _pm.encryptFile(_tm.reference_no, _pm.secret_key);
                _tm.sub_merchantid = "45";
                _tm.sub_merchantid_encrypt = _pm.encryptFile(_tm.sub_merchantid, _pm.secret_key);
                _tm.pgamount = totlfee.Text.ToString();
                //_tm.pgamount ="10";
                _tm.pgamount_encrypt = _pm.encryptFile(_tm.pgamount, _pm.secret_key);
                _tm.fee_rcptid = "0";
                _tm.fee_rcptid_encrypt = _pm.encryptFile(_tm.fee_rcptid, _pm.secret_key);
                _tm.receiptdate = cdt.ToString("yyyy/MM/dd").Replace('-', '/');
                _tm.receiptdate_encrypt = _pm.encryptFile(_tm.receiptdate, _pm.secret_key);
                _tm.studentname = StudentName.Text.ToString();
                _tm.studentname_encrypt = _pm.encryptFile(_tm.studentname, _pm.secret_key);
                _tm.grno = GR.Text.ToString();
                _tm.grno_encrypt = _pm.encryptFile(_tm.grno, _pm.secret_key);
                _tm.cid = txtcid.Text.ToString();
                _tm.std = standared.Text.ToString();
                _tm.div = division.Text.ToString();
                _tm.stddiv_encrypt = _pm.encryptFile(_tm.stddiv, _pm.secret_key);
                _tm.academicyear = lbl_year.Text.ToString();
                _tm.academicyear_encrypt = _pm.encryptFile(_tm.academicyear, _pm.secret_key);
                _tm.mobileno = mobileno.Text.ToString();
                _tm.mobileno_encrypt = _pm.encryptFile(_tm.mobileno, _pm.secret_key);
                _tm.emailid = "abcd@gmail.com";
                _tm.emailid_encrypt = _pm.encryptFile(_tm.emailid, _pm.secret_key);
                _tm.mandatory_field_encrypt = _pm.encryptFile(_tm.mandatory_field_New, _pm.secret_key);


                //save data in stored procedure OnlineTransactionTable
                using (con = Connection.getConnection())
                {
                    con.Open();

                    //cmd.Parameters.Add("@Merchantid", SqlDbType.NVarChar,500).Value = _tm.merchantid;
                    //cmd.Parameters.Add("@Merchantid_encrypt", SqlDbType.NVarChar,500).Value = _tm.merchantid_encrypt;
                    //cmd.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = cdt.ToString("yyyy/MM/dd HH:mm:ss");
                    //cmd.Parameters.Add("@cid", SqlDbType.Int).Value = _tm.cid;
                    //cmd.Parameters.Add("@std", SqlDbType.NVarChar,500).Value = _tm.std;
                    //cmd.Parameters.Add("@div", SqlDbType.NVarChar,500).Value = _tm.div;
                    //cmd.Parameters.Add("@stddiv_encrypt", SqlDbType.NVarChar,500).Value = _tm.stddiv_encrypt;
                    //cmd.Parameters.Add("@grno", SqlDbType.NVarChar,500).Value = _tm.grno;
                    //cmd.Parameters.Add("@grno_encrypt", SqlDbType.NVarChar,500).Value = _tm.grno_encrypt;
                    //cmd.Parameters.Add("@studentname", SqlDbType.NVarChar,500).Value = _tm.studentname;
                    //cmd.Parameters.Add("@studentname_encrypt", SqlDbType.NVarChar,500).Value = _tm.studentname_encrypt;
                    //cmd.Parameters.Add("@mobileno", SqlDbType.NVarChar,500).Value = _tm.mobileno;
                    //cmd.Parameters.Add("@mobileno_encrypt", SqlDbType.NVarChar,500).Value = _tm.mobileno_encrypt;
                    //cmd.Parameters.Add("@academicyear", SqlDbType.NVarChar,500).Value = _tm.academicyear;
                    //cmd.Parameters.Add("@academicyear_encrypt", SqlDbType.NVarChar,500).Value = _tm.academicyear_encrypt;
                    //cmd.Parameters.Add("@fees_amt", SqlDbType.NVarChar,500).Value = _tm.pgamount;
                    //cmd.Parameters.Add("@fees_amt_encrypted", SqlDbType.NVarChar,500).Value = _tm.pgamount_encrypt;
                    //cmd.Parameters.Add("@submerchant_id", SqlDbType.NVarChar,500).Value = _tm.sub_merchantid;
                    //cmd.Parameters.Add("@submerchant_id_encrypted", SqlDbType.NVarChar,500).Value = _tm.sub_merchantid_encrypt;
                    //cmd.Parameters.Add("@Mandatoryfield", SqlDbType.NVarChar,500).Value = _tm.mandatory_field;
                    //cmd.Parameters.Add("@Mandatoryfield_encrypt", SqlDbType.NVarChar,500).Value = _tm.mandatory_field_encrypt;
                    //cmd.Parameters.Add("@created_datetime", SqlDbType.NVarChar,500).Value = cdt.ToString("yyyy/MM/dd HH:mm:ss");
                    //cmd.Parameters.Add("@created_by", SqlDbType.NVarChar,500).Value = lbl_username.Text;
                    SqlCommand cmd = new SqlCommand("SP_ADDTransectionDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Merchantid", _tm.merchantid);
                    cmd.Parameters.AddWithValue("@Merchantid_encrypt", _tm.merchantid_encrypt);
                    cmd.Parameters.AddWithValue("@Datetime", cdt.ToString("yyyy/MM/dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@cid", _tm.cid);
                    cmd.Parameters.AddWithValue("@std", _tm.std);
                    cmd.Parameters.AddWithValue("@div", _tm.div);
                    cmd.Parameters.AddWithValue("@stddiv_encrypt", _tm.stddiv_encrypt);
                    cmd.Parameters.AddWithValue("@grno", _tm.grno);
                    cmd.Parameters.AddWithValue("@grno_encrypt", _tm.grno_encrypt);
                    cmd.Parameters.AddWithValue("@studentname", _tm.studentname);
                    cmd.Parameters.AddWithValue("@studentname_encrypt", _tm.studentname_encrypt);
                    cmd.Parameters.AddWithValue("@mobileno", _tm.mobileno);
                    cmd.Parameters.AddWithValue("@mobileno_encrypt", _tm.mobileno_encrypt);
                    cmd.Parameters.AddWithValue("@academicyear", _tm.academicyear);
                    cmd.Parameters.AddWithValue("@academicyear_encrypt", _tm.academicyear_encrypt);
                    cmd.Parameters.AddWithValue("@fees_amt", _tm.pgamount);
                    cmd.Parameters.AddWithValue("@fees_amt_encrypted", _tm.pgamount_encrypt);
                    cmd.Parameters.AddWithValue("@submerchant_id", _tm.sub_merchantid);
                    cmd.Parameters.AddWithValue("@submerchant_id_encrypted", _tm.sub_merchantid_encrypt);
                    cmd.Parameters.AddWithValue("@Mandatoryfield", _tm.mandatory_field_New);
                    cmd.Parameters.AddWithValue("@Mandatoryfield_encrypt", _tm.mandatory_field_encrypt);
                    cmd.Parameters.AddWithValue("@created_datetime", cdt.ToString("yyyy/MM/dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@created_by", _tm.grno);

                    cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ERRORMESSAGE", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                    //cmd.Parameters.Add("@ISSUEFOUND", SqlDbType.Int).Value = ParameterDirection.Output;
                    cmd.Parameters.Add("@lstidentity", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    string ERRORMESSAGE = cmd.Parameters["@ERRORMESSAGE"].Value.ToString();
                    int result = Convert.ToInt32(cmd.Parameters["@result"].Value);
                    //ISSUEFOUND = Convert.ToInt32(cmd.Parameters["@ISSUEFOUND"].Value);
                    int lstidentity = Convert.ToInt32(cmd.Parameters["@lstidentity"].Value);

                    //update the reference no after storeing in the database table.
                    if (result == 1)
                    {
                        _tm.reference_no = lstidentity.ToString();
                        _tm.reference_no_encrypt = _pm.encryptFile(_tm.reference_no, _pm.secret_key);
                        _tm.mandatory_field_encrypt = _pm.encryptFile(_tm.mandatory_field_New, _pm.secret_key);

                        query = "update FeesOnlineTransactionTable set ref_encrypt=@ref_encrypt,Mandatoryfield=@Mandatoryfield,Mandatoryfield_encrypted=@Mandatoryfield_encrypted where ref_id=@ref_id ";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ref_encrypt", _tm.reference_no_encrypt);
                        cmd.Parameters.AddWithValue("@Mandatoryfield", _tm.mandatory_field_New);
                        cmd.Parameters.AddWithValue("@Mandatoryfield_encrypted", _tm.mandatory_field_encrypt);
                        cmd.Parameters.AddWithValue("@ref_id", _tm.reference_no);
                        cmd.ExecuteNonQuery();

                        NameValueCollection data = new NameValueCollection();
                        data.Add("merchantid", _tm.merchantid);
                        data.Add("mandatory fields", _tm.mandatory_field_encrypt);
                        data.Add("optional fields", "");
                        data.Add("returnurl", returnurl_encrypt);
                        data.Add("Reference No", _tm.reference_no_encrypt);
                        data.Add("submerchantid", _tm.sub_merchantid_encrypt);
                        data.Add("transaction amount", _tm.pgamount_encrypt);
                        data.Add("paymode", "HI3YPktYCKMM8Su77qNSxg==");

                        //pass to the eazypay request url 
                        // HttpHelper.RedirectAndPOST(this.Page, "https://eazypayuat.icicibank.com/EazyPG", data);
                        HttpHelper.RedirectAndPOST(this.Page, "https://eazypay.icicibank.com/EazyPG", data);
                    }
                    else
                    {
                        throw new Exception(ERRORMESSAGE);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("ParentsDashboard.paynow_online_Click", ex);

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
}