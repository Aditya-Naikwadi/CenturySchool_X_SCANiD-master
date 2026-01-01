//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CenturyRayonSchool.LCModule
{
    public partial class GenrateLC : System.Web.UI.Page
    {
        DataTable studentlist = new DataTable();
        Label lblusercode = new Label();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblusercode = (Label)Page.Master.FindControl("lblusercode");
            string year = new FeesModel().setActiveAcademicYear();
            lblacademicyear.Text = year;
            if (!IsPostBack)
            {
                loadFormControl(year);

            }
            studentlist.Columns.Add("cardid");
            studentlist.Columns.Add("student");
            studentlist.Columns.Add("std");
            studentlist.Columns.Add("div");
            studentlist.Columns.Add("grno");
            studentlist.Columns.Add("academicyear");
        }

        public void loadFormControlold()
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

                    dateofleaving.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace('-', '/');

              
                 
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
        public void loadFormControl(string year)
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
                    cmbAcademicyear.SelectedValue = year;


                }
            }
            catch (Exception ex)
            {
                Log.Error("LCPrint.loadFormControl", ex);
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
        protected void cmbDiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                DataTable studtable = new DataTable();
                using (con = Connection.getConnection())
                {
                    con.Open();

                    string query = "", select_std = "", select_div = "", academicyear = "", select_academicyear="";

                    select_std = cmbStd.SelectedValue.ToString();
                    select_div = cmbDiv.SelectedValue.ToString();
                    academicyear = cmbAcademicyear.SelectedValue.ToString();
                    select_academicyear = cmbAcademicyear.SelectedValue.ToString();
                    if (select_std != "Select Std" && select_div != "Select Div")
                    {
                        query = "select grno,fullname,(grno +' / '+ fullname) as stuname from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "';";
                        SqlDataAdapter adap = new SqlDataAdapter(query, con);
                        adap.Fill(studtable);

                        studtable.Rows.Add("ALL", "ALL", "ALL");
                        cmbstudentname.DataSource = studtable;
                        cmbstudentname.DataBind();
                        cmbstudentname.DataTextField = "stuname";
                        cmbstudentname.DataValueField = "grno";
                        cmbstudentname.DataBind();
                        cmbstudentname.SelectedValue = "ALL";

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("GenrateLC.cmbDiv_SelectedIndexChanged", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }
        protected void searchstudent_ServerClick(object sender, EventArgs e)
        {
            string query = "", select_std = "", select_div = "", academicyear = "", grno = "", select_academicyear="";

            select_std = cmbStd.SelectedValue.ToString();
            select_div = cmbDiv.SelectedValue.ToString();
            academicyear = cmbAcademicyear.SelectedValue.ToString();
            select_academicyear = cmbAcademicyear.SelectedValue.ToString();
            grno = cmbstudentname.SelectedValue.ToString();
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "Select [cardid],[grno],[Lname] as [surname],[Fname] as [studentname],[Mname] as [fathername],[Mothername],[Dob],[saralid],[aadharcard],[std],[religion],[caste],[subcaste],[category],[doa],[placeofbirth],[birthtaluka],[birthdistrict],[birthstate],[birthcountry],[mothertongue],dobwords,admissionstd,nationality,lastschool,progress,conduct,reasonforleaving,remark,schoolsection,div,lcno,rollno,stdstudying,[freeshiptype],apaar_id,pen_no From StudentMaster where std='" + select_std + "' and div='" + select_div + "' and grno='" + grno + "' and academicyear='" + academicyear + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        txtcrdid.Text = reader[0].ToString();
                        txtgrno.Text = reader[1].ToString();
                        txtsurname.Text = reader[2].ToString();
                        txtstuname.Text = reader[3].ToString();
                        txtfather.Text = reader[4].ToString();
                        txtmother.Text = reader[5].ToString();
                        txtdob.Text = reader[6].ToString().Replace('-', '/');

                        Txtsaral.Text = reader[7].ToString();
                        Txtaadhar.Text = reader[8].ToString();

                        txtstd.Text = reader[9].ToString();
                        textrelgon.Text = reader[10].ToString();
                        txtcast.Text = reader[11].ToString();
                        txtsubcast.Text = reader[12].ToString();
                        txtcateg.Text = reader[13].ToString();
                        txtdoa.Text = reader[14].ToString().Replace('-', '/');

                        txtpob.Text = reader[15].ToString();
                        txttluka.Text = reader[16].ToString();
                        txtdist.Text = reader[17].ToString();
                        txtstat.Text = reader[18].ToString();
                        txtconunt.Text = reader[19].ToString();
                        txtmothertong.Text = reader[20].ToString();

                        dobwords.Text = reader[21].ToString();
                        txtadmiinstd.Text = reader[22].ToString();
                        txtnational.Text = reader[23].ToString();
                        txtlstschool.Text = reader[24].ToString();

                        txtprogress.Text = reader[25].ToString();
                        txtcond.Text = reader[26].ToString();
                        txtrol.Text = reader[27].ToString();
                        txtreark.Text = reader[28].ToString();
                        txtsection.Text = reader[29].ToString();
                        txtdiv.Text = reader[30].ToString();
                        txtlcno.Text = reader[31].ToString();
                        txtrollno.Text = reader[32].ToString();

                        txtsis.Text = reader[33].ToString();
                        txtfreeship.Text = reader[34].ToString();
                        txtapaarid.Text = reader["apaar_id"].ToString();
                        txtpenno.Text = reader["pen_no"].ToString();

                    }

                    reader.Close();

                    if (txtsis.Text.Length == 0)
                    {
                        txtsis.Text = setStdStudying(txtstd.Text, txtdoa.Text.ToString(), con);
                    }
                    //dobwords.Text = DateToText(txtdob.Text.ToSt, false, false);
                }

            }
            catch (Exception ex)
            {
                Log.Error("GenrateLC.searchstudent_ServerClick", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }

        }

        public static string setStdStudying(string std, string doa, SqlConnection con)
        {
            string str = "", stdx = "", monthx = "", yearx = "";
            DateTime date = Convert.ToDateTime(doa);
            String format = "Std.{0}. Since {1} {2}.";

            if (std.Equals("XII-Commerce"))
            {
                stdx = "XII (Twelfth) Commerce";
            }
            else if (std.Equals("XII-Science"))
            {
                stdx = "XII (Science) Commerce";
            }
            else if (std.Equals("X"))
            {
                stdx = "X (Tenth)";
            }
            else
            {
                stdx = std;
            }

            monthx = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
            yearx = date.Year.ToString();

            str = String.Format(format, stdx, monthx, yearx);


            return str;

        }

        public static string DateToText(DateTime dt, bool includeTime, bool isUK)
        {
            string[] ordinals =
        {
           "First",
           "Second",
           "Third",
           "Fourth",
           "Fifth",
           "Sixth",
           "Seventh",
           "Eighth",
           "Ninth",
           "Tenth",
           "Eleventh",
           "Twelfth",
           "Thirteenth",
           "Fourteenth",
           "Fifteenth",
           "Sixteenth",
           "Seventeenth",
           "Eighteenth",
           "Nineteenth",
           "Twentieth",
           "Twenty First",
           "Twenty Second",
           "Twenty Third",
           "Twenty Fourth",
           "Twenty Fifth",
           "Twenty Sixth",
           "Twenty Seventh",
           "Twenty Eighth",
           "Twenty Ninth",
           "Thirtieth",
           "Thirty First"
        };
            int day = dt.Day;
            int month = dt.Month;
            int year = dt.Year;
            DateTime dtm = new DateTime(1, month, 1);
            string date;
            if (isUK)
            {
                date = "The " + ordinals[day - 1] + " of " + dtm.ToString("MMMM") + " " + NumberToText(year, true);
            }
            else
            {
                date = ordinals[day - 1] + " " + dtm.ToString("MMMM") + " " + NumberToText(year, false);
            }
            if (includeTime)
            {
                int hour = dt.Hour;
                int minute = dt.Minute;
                string ap = "AM";
                if (hour >= 12)
                {
                    ap = "PM";
                    hour = hour - 12;
                }
                if (hour == 0) hour = 12;
                string time = NumberToText(hour, false);
                if (minute > 0) time += " " + NumberToText(minute, false);
                time += " " + ap;
                date += ", " + time;
            }
            return date;
        }

        public static string NumberToText(int number, bool isUK)
        {
            if (number == 0) return "Zero";
            string and = isUK ? "and " : ""; // deals with UK or US numbering
            if (number == -2147483648) return "Minus Two Billion One Hundred " + and +
            "Forty Seven Million Four Hundred " + and + "Eighty Three Thousand " +
            "Six Hundred " + and + "Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Million ", "Billion " };
            num[0] = number % 1000;           // units
            num[1] = number / 1000;
            num[2] = number / 1000000;
            num[1] = num[1] - 1000 * num[2];  // thousands
            num[3] = number / 1000000000;     // billions
            num[2] = num[2] - 1000 * num[3];  // millions
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10;              // ones
                t = num[i] / 10;
                h = num[i] / 100;             // hundreds
                t = t - 10 * h;               // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i < first) sb.Append(and);
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }



        protected void SaveLC_ServerClick(object sender, EventArgs e)
        {
            string academicyear = cmbAcademicyear.SelectedValue.ToString();
            
            try
            {
                Boolean newlc = false;
                StudentMaster stm = new StudentMaster();
                stm.saralid = Txtsaral.Text;
                stm.aadharcard = Txtaadhar.Text;
                stm.fname = txtstuname.Text;
                stm.mname = txtfather.Text;
                stm.lname = txtsurname.Text;
                stm.mothername = txtmother.Text;
                stm.religion = textrelgon.Text;
                stm.caste = txtcast.Text;
                stm.subcaste = txtsubcast.Text;
                stm.category = txtcateg.Text;
                stm.nationality = txtnational.Text;
                stm.mothertongue = txtmothertong.Text;
                stm.dob = txtdob.Text.ToString().Replace('-', '/');
                stm.placeofbirth = txtpob.Text;
                stm.birthtaluka = txttluka.Text;
                stm.birthdistrict = txtdist.Text;
                stm.birthstate = txtstat.Text;
                stm.birthcountry = txtconunt.Text;
                stm.lastschool = txtlstschool.Text;
                stm.doa = txtdoa.Text.ToString().Replace('-', '/');
                stm.dateofleaving = dateofleaving.Text.ToString().Replace('-', '/');
                stm.progress = txtprogress.Text;
                stm.conduct = txtcond.Text;
                stm.reasonofleaving = txtrol.Text;
                stm.remark = txtreark.Text;
                stm.dobwords = dobwords.Text;
                stm.admissionstd = txtadmiinstd.Text;
                stm.schoolsection = txtsection.Text;
                stm.div = txtdiv.Text;
                stm.rollno = txtrollno.Text;
                stm.stdstudying = txtsis.Text;
                stm.apaar_id = txtapaarid.Text;
                stm.pen_no = txtpenno.Text;

                stm.lcno = txtlcno.Text;
                stm.freeship = txtfreeship.Text;
                if (stm.lcno.Length == 0 || stm.lcno == "-")
                {
                    long lcount = 0;
                    //for century school
                    if (stm.schoolsection == "Secondary" || stm.schoolsection == "Secondary (Hindi)" || stm.schoolsection == "Secondary (Marathi)")
                    {
                        string section1 = "'Secondary','Secondary (Hindi)','Secondary (Marathi)'";

                        lcount = GetLCCountAcademicYear(section1, academicyear);
                    }
                    else
                    {
                        lcount = GetLCCountAcademicYear("'" + stm.schoolsection + "'", academicyear);
                    }

                    stm.lcno = lcount.ToString();
                    newlc = true;
                }
                else
                {
                    newlc = false;
                }

                saveLC(txtstd.Text, txtgrno.Text, stm, newlc);
                resetLayout();
                lblinfomsg.Text = "LC Generated Successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
            }
            catch (Exception ex)
            {
                Log.Error("GenrateLC.SaveLC_ServerClick", ex);
            }

        }

        private void saveLC(string std, string grno, StudentMaster stm, Boolean newlcrecord)
        {
            SqlConnection con = Connection.getConnection();
            con.Open();
            String query = "";
            SqlCommand cmd = null;
            string cdt = DateTime.Now.ToString();
            string usercode = lblusercode.Text;
            string academicyear = cmbAcademicyear.SelectedValue.ToString();
            if (newlcrecord)
            {
                query = "insert into [LeavingCertificate](Lcno,[FNAME],[MNAME],[LNAME],[MOTHERNAME],[STD],[GRNO],[DOB],[RELIGION],[CASTE],[subcaste],[CATEGORY],[DOA],[saralid],[aadharcard],[placeofbirth],[birthtaluka],[birthdistrict],[birthstate],[birthcountry],[mothertongue],[Nationality],[Lastschool],[Progress],[DateofLeaving],[Reasonforleaving],[conduct],[remark],dobwords,admissionstd,schoolsection,rollno,div,stdstudying,academicyear,[freeshiptype],createddate,createdby,apaar_id,pen_no) " +
                        "values('" + stm.lcno + "','" + stm.fname + "','" + stm.mname + "','" + stm.lname + "','" + stm.mothername + "','" + std + "','" + grno + "','" + stm.dob + "','" + stm.religion + "','" + stm.caste + "','" + stm.subcaste + "','" + stm.category + "','" + stm.doa + "','" + stm.saralid + "','" + stm.aadharcard + "','" + stm.placeofbirth + "','" + stm.birthtaluka + "','" + stm.birthdistrict + "','" + stm.birthstate + "','" + stm.birthcountry + "','" + stm.mothertongue + "','" + stm.nationality + "','" + stm.lastschool + "','" + stm.progress + "','" + stm.dateofleaving + "','" + stm.reasonofleaving + "','" + stm.conduct + "','" + stm.remark + "','" + stm.dobwords + "','" + stm.admissionstd + "','" + stm.schoolsection + "','" + stm.rollno + "','" + stm.div + "','" + stm.stdstudying + "','" + academicyear + "','" + stm.freeship + "','" + cdt + "','" + usercode + "','"+stm.apaar_id+"','"+stm.pen_no+"')";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                query = "update [LeavingCertificate] set fname='" + stm.fname + "',mname='" + stm.mname + "',lname='" + stm.lname + "',dob='" + stm.dob + "',caste='" + stm.caste + "',religion='" + stm.religion + "',category='" + stm.category + "',mothername='" + stm.mothername + "',doa='" + stm.doa + "',subcaste='" + stm.subcaste + "',saralid='" + stm.saralid + "',aadharcard='" + stm.aadharcard + "',placeofbirth='" + stm.placeofbirth + "',birthtaluka='" + stm.birthtaluka + "',birthdistrict='" + stm.birthdistrict + "',birthstate='" + stm.birthstate + "',birthcountry='" + stm.birthcountry + "',mothertongue='" + stm.mothertongue + "',nationality='" + stm.nationality + "',lastschool='" + stm.lastschool + "', progress='" + stm.progress + "',dateofleaving='" + stm.dateofleaving + "',reasonforleaving='" + stm.reasonofleaving + "',conduct='" + stm.conduct + "',remark='" + stm.remark + "',dobwords='" + stm.dobwords + "',admissionstd='" + stm.admissionstd + "',schoolsection='" + stm.schoolsection + "',rollno='" + stm.rollno + "',div='" + stm.div + "',stdstudying='" + stm.stdstudying + "',[freeshiptype]='" + stm.freeship + "',updateddate='" + cdt + "',updatedby='" + usercode + "',apaar_id='"+stm.apaar_id+ "',pen_no='"+stm.pen_no+"' " +
                         "where std='" + std + "' and grno='" + grno + "' and lcno='" + stm.lcno + "' and academicyear='" + academicyear + "';";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                query = "update [Studentmaster] set fname='" + stm.fname + "',mname='" + stm.mname + "',lname='" + stm.lname + "',dob='" + stm.dob + "',caste='" + stm.caste + "',religion='" + stm.religion + "',category='" + stm.category + "',mothername='" + stm.mothername + "',doa='" + stm.doa + "',subcaste='" + stm.subcaste + "',saralid='" + stm.saralid + "',aadharcard='" + stm.aadharcard + "',placeofbirth='" + stm.placeofbirth + "',birthtaluka='" + stm.birthtaluka + "',birthdistrict='" + stm.birthdistrict + "',birthstate='" + stm.birthstate + "',birthcountry='" + stm.birthcountry + "',mothertongue='" + stm.mothertongue + "',nationality='" + stm.nationality + "',lastschool='" + stm.lastschool + "', progress='" + stm.progress + "',dateofleaving='" + stm.dateofleaving + "',reasonforleaving='" + stm.reasonofleaving + "',conduct='" + stm.conduct + "',remark='" + stm.remark + "',dobwords='" + stm.dobwords + "',admissionstd='" + stm.admissionstd + "',schoolsection='" + stm.schoolsection + "',rollno='" + stm.rollno + "',div='" + stm.div + "',stdstudying='" + stm.stdstudying + "',[freeshiptype]='" + stm.freeship + "',updateddate='" + cdt + "',updatedby='" + usercode + "',apaar_id='" + stm.apaar_id + "',pen_no='" + stm.pen_no + "' " +
                         "where std='" + std + "' and grno='" + grno + "' and academicyear='" + academicyear + "';";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }



            query = "update studentmaster set lastschool='" + stm.lastschool + "', progress='" + stm.progress + "',dateofleaving='" + stm.dateofleaving + "',reasonforleaving='" + stm.reasonofleaving + "',lcno='" + stm.lcno + "',leftstatus='Yes',conduct='" + stm.conduct + "',remark='" + stm.remark + "',apaar_id = '" + stm.apaar_id + "', pen_no = '" + stm.pen_no + "', stdstudying='" + stm.stdstudying + "' " +
                    "where std='" + std + "' and grno='" + grno + "' and academicyear='" + academicyear + "';";
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();





            //query = "insert into leavingcertificate(Lcno,grno,std,stdstudying,placeofbirth,birthtaluka,birthdistrict,birthstate,birthcountry,mothertongue,nationality,lastschool,progress,dateofleaving,reasonforleaving,conduct,remark) " +
            //    "values('" + stm.lcno + "','" + grno + "','" + std + "','" + std + "','" + stm.placeofbirth + "','" + stm.birthtaluka + "','" + stm.birthdistrict + "','" + stm.birthstate + "','" + stm.birthcountry + "','" + stm.mothertongue + "','" + stm.nationality + "','" + stm.lastschool + "','" + stm.progress + "','" + stm.dateofleaving + "','" + stm.reasonofleaving + "','"+stm.conduct+"','"+stm.remark+"')";
            // cmd = new SqlCommand(query, con);
            //cmd.ExecuteNonQuery();



            con.Close();
        }

        public static long GetLCCountAcademicYear(string section, string academicyear)
        {
            long count = 0;
            SqlConnection con = Connection.getConnection();
            con.Open();
            String query = "select Cast(Lcno as int) as Lcno From Leavingcertificate where schoolsection IN ( " + section + ") and academicyear='" + academicyear + "' order by Lcno desc;";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                count = Convert.ToInt64(reader[0]);
                break;
            }
            reader.Close();
            count = count + 1;
            con.Close();
            return count;
        }

        public class StudentMaster
        {
            public string fname { get; set; }
            public string mname { get; set; }
            public string lname { get; set; }
            public string dob { get; set; }
            public string caste { get; set; }
            public string religion { get; set; }
            public string category { get; set; }
            public string mothername { get; set; }
            public string doa { get; set; }
            public string subcaste { get; set; }
            public string saralid { get; set; }
            public string aadharcard { get; set; }
            public string placeofbirth { get; set; }
            public string birthtaluka { get; set; }
            public string birthdistrict { get; set; }
            public string birthstate { get; set; }
            public string birthcountry { get; set; }
            public string mothertongue { get; set; }
            public string nationality { get; set; }
            public string lastschool { get; set; }
            public string progress { get; set; }
            public string dateofleaving { get; set; }
            public string reasonofleaving { get; set; }
            public string lcno { get; set; }
            public string conduct { get; set; }
            public string remark { get; set; }
            public string dobwords { get; set; }
            public string admissionstd { get; set; }
            public string std { get; set; }
            public string schoolsection { get; set; }
            public string div { get; set; }
            public string rollno { get; set; }
            public string stdstudying { get; set; }
            public string freeship { get; set; }
            public string apaar_id { get; set; }
            public string pen_no { get; set; }
        }
        public void resetLayout()
        {
            txtlcno.Text = "";
            txtcrdid.Text = "";
            txtgrno.Text = "";
            txtstd.Text = "";
            Txtaadhar.Text = "";
            Txtsaral.Text = "";
            txtsurname.Text = "";
            txtstuname.Text = "";
            txtfather.Text = "";
            txtmother.Text = "";
            txtdiv.Text = "";
            txtsection.Text = "";
            txtrollno.Text = "";
            textrelgon.Text = "";
            txtcast.Text = "";
            txtsubcast.Text = "";
            txtcateg.Text = "";

            txtnational.Text = "";
            txtmothertong.Text = "";
            txtdob.Text = "";
            txtpob.Text = "";
            txttluka.Text = "";
            txtdist.Text = "";
            txtstat.Text = "";
            txtconunt.Text = "";
            txtlstschool.Text = "";
            txtadmiinstd.Text = "";
            txtprogress.Text = "";
            txtcond.Text = "";
            txtsis.Text = "";
            txtrol.Text = "";
            txtreark.Text = "";
            txtpenno.Text = "";
            txtapaarid.Text = "";

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
                    academicyear = cmbAcademicyear.SelectedValue.ToString();

                    if (select_std != "Select Std" && select_div != "Select Div")
                    {
                        query = "select grno,fullname,(grno +' / '+ fullname) as stuname from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' ;";
                        SqlDataAdapter adap = new SqlDataAdapter(query, con);
                        adap.Fill(studtable);

                        studtable.Rows.Add("ALL", "ALL", "ALL");
                        cmbstudentname.DataSource = studtable;
                        cmbstudentname.DataBind();
                        cmbstudentname.DataTextField = "stuname";
                        cmbstudentname.DataValueField = "grno";
                        cmbstudentname.DataBind();
                        cmbstudentname.SelectedValue = "ALL";

                    }




                }
            }
            catch (Exception ex)
            {
                Log.Error("SubjectMarks.cmbDiv_SelectedIndexChanged", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

      
    }
}