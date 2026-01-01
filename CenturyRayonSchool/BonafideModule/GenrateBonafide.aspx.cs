using CenturyRayonSchool.BonafideModule.DSBonafide;
using CenturyRayonSchool.BonafideModule.Reports;
//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.BonafideModule
{
    public partial class GenrateBonafide : System.Web.UI.Page
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
                loadFormControl();

            }
            studentlist.Columns.Add("cardid");
            studentlist.Columns.Add("student");
            studentlist.Columns.Add("std");
            studentlist.Columns.Add("div");
            studentlist.Columns.Add("grno");
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
                Log.Error("GenrateBonafide.loadFormControl", ex);
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

                    string query = "", select_std = "", select_div = "", academicyear = "";

                    select_std = cmbStd.SelectedValue.ToString();
                    select_div = cmbDiv.SelectedValue.ToString();
                    academicyear = lblacademicyear.Text;

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
                Log.Error("GenrateBonafide.cmbDiv_SelectedIndexChanged", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }
        protected void searchstudent_ServerClick(object sender, EventArgs e)
        {
            string query = "", select_std = "", select_div = "", academicyear = "", grno = "";

            select_std = cmbStd.SelectedValue.ToString();
            select_div = cmbDiv.SelectedValue.ToString();
            academicyear = lblacademicyear.Text;
            grno = cmbstudentname.SelectedValue.ToString();
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "Select [cardid],[grno],[Lname],[Fname],[Mname],[Mothername],[Dob],[saralid],[aadharcard],[std],[religion],[caste],[subcaste],[shiftname],[div],[photopath],[nationality],[placeofbirth],[dobwords],accountname,doa,schoolsection From studentmaster where std='" + select_std + "' and div='" + select_div + "' and grno='" + grno + "' and academicyear='" + lblacademicyear.Text + "';";
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
                        txtshift.Text = reader[13].ToString();
                        txtdiv.Text = reader[14].ToString();
                        txtnational.Text = reader[16].ToString();
                        txtpob.Text = reader[17].ToString();
                        dobwords.Text = reader[18].ToString();
                        txtaccount.Text = reader[19].ToString();
                        txtdoa.Text = reader["doa"].ToString().Replace('-', '/');
                        txtsection.Text = reader["schoolsection"].ToString();

                        reader.Close();
                        //if (dobwords.Length == 0)
                        //{
                        //    dobwords = DateToText(txtdob.Text, false, false);
                        //}


                        query = "select reasonforleaving From LeavingCertificate where std='" + select_std + "' and div='" + select_div + "' and grno='" + grno + "' and academicyear='" + academicyear + "';";
                        cmd = new SqlCommand(query, con);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            txtreark.Text = reader[0].ToString();
                        }
                        reader.Close();



                        con.Close();
                    }

                    reader.Close();


                }

            }
            catch (Exception ex)
            {
                Log.Error("GenrateBonafide.searchstudent_ServerClick", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }

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


        protected void SaveBonafide_ServerClick(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con = Connection.getConnection();
                con.Open();
                String query = "", cdt = "";
                cdt = DateTime.Now.ToString();
                query = "Select Count(*) From bonafide;";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    txtsrno.Text = (Convert.ToInt32(reader[0]) + 1).ToString();
                }
                reader.Close();

                //query = "update studentmaster set fname='"+txtstud.Text+"',mname='"+txtfather.Text+"',lname='"+txtsurname.Text+"',dob='"+dobcal.Value.ToString("yyyy/MM/dd")+"',caste='"+cmbcaste.Text+"',religion='"+cmbreligion.Text+"',category='"+cmbcategory.Text+"',mothername='"+txtmother.Text+"',subcaste='"+cmbsubcaste.Text+"',saralid='"+txtsaral.Text+"',aadharcard='"+txtaadhar.Text+"',placeofbirth='"+txtplace.Text+"',dobwords='"+txtdobwords.Text+"' "+ 
                //        "where std='"+cmbstd.Text+"' and grno='"+txtgrno.Text+"';";
                //cmd = new SqlCommand(query, con);
                //cmd.ExecuteNonQuery();


                query = "insert into bonafide(serialno,cardid,grno,[LNAME],[FNAME],[MNAME],mothername,religion,caste,subcaste,nationality,[DOB],placeofbirth,saralid,[aadharcard],issuedate,shiftname,std,div,photopath,dobwords,accountname,academicyear,remark,doa,schoolsection,category,createddate,createdby,validto) " +
                        "values('" + txtsrno.Text + "','" + txtcrdid.Text + "','" + txtgrno.Text + "','" + txtsurname.Text + "','" + txtstuname.Text + "','" + txtfather.Text + "','" + txtmother.Text + "','" + textrelgon.Text + "','" + txtcast.Text + "','" + txtsubcast.Text + "','" + txtnational.Text + "'," +
                        "'" + txtdob.Text.ToString() + "','" + txtpob.Text + "','" + Txtsaral.Text + "','" + Txtaadhar.Text + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + txtshift.Text + "','" + txtstd.Text + "','" + txtdiv.Text + "','','" + dobwords.Text + "','" + txtaccount.Text + "','" + lblacademicyear.Text + "','" + txtreark.Text + "','" + txtdoa.Text + "','" + txtsection.Text + "','" + txtcateg.Text + "','" + cdt + "','" + lblusercode.Text + "','" + txtbona.Text + "');";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                // resetLayout();
                lblinfomsg.Text = "Bonafide Generated Successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
            }
            catch (Exception ex)
            {
                Log.Error("GenrateBonafide.SaveBonafide_ServerClick", ex);
            }
        }

        public void resetLayout()
        {
            txtsrno.Text = "";
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
            txtbona.Text = "";
            txtnational.Text = "";
            txtaccount.Text = "";
            txtdob.Text = "";
            txtpob.Text = "";
            txtshift.Text = "";
            dobwords.Text = "";
            txtdoa.Text = "";
            txtreark.Text = "";


        }

        protected void PrintBonafide_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string query = "", select_std = "", select_div = "", academicyear = "", grno = "";

                select_std = cmbStd.SelectedValue.ToString();
                select_div = cmbDiv.SelectedValue.ToString();
                academicyear = lblacademicyear.Text;
                grno = cmbstudentname.SelectedValue.ToString();

                showbonafideReportCenturyForDomicile(select_std, select_div, grno);



            }
            catch (Exception ex)
            {
                Log.Error("GenrateBonafide.PrintBonafide_ServerClick", ex);
            }
        }


        public void showbonafideReportCenturyForDomicile(string std, string div, string grno)
        {
            try
            {
                //for century school
                string academicyear = lblacademicyear.Text;
                SqlConnection con = Connection.getConnection();
                con.Open();
                BonafideRepCRS bonafide = new BonafideRepCRS();

                String query = "";
                query = "Select Top 1 b.serialno,b.cardid,b.grno,b.Lname as surname,b.fname as studentname,b.mname as fathername,b.mothername,b.religion,b.caste,b.subcaste,b.nationality,CONVERT(VARCHAR(10),Cast(b.dob as Date), 103) as dateofbirth,b.placeofbirth,b.saralid,b.aadharcard as aadharno,b.shiftname,b.std,b.div,(b.Lname +' '+b.fname+' '+b.mname+' '+b.mothername) as fullname,b.dobwords,b.academicyear,b.remark,('CJHS-'+b.academicyear+'/'+b.serialno) as serialno,CONVERT(VARCHAR(10),Cast(b.issuedate as Date), 103) as issuedate,CONVERT(VARCHAR(10),Cast(b.doa as Date), 103) as doa,b.schoolsection,b.validto " +
                        "From  bonafide as b " +
                        "where b.STD='" + std + "' and b.DIV='" + div + "' and b.grno='" + grno + "' and academicyear='" + academicyear + "' order by  Cast(b.serialno as int) desc;";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                Bonafideds _bonafideds = new Bonafideds();
                adap.Fill(_bonafideds.Tables[0]);

                bonafide.SetDataSource(_bonafideds.Tables[0]);
                con.Close();
                //bonafide.SetParameterValue("todate", DateTime.Now.ToString("dd/MM/yyyy"));
                //bonafide.SetParameterValue("prigrno", "-");
                //bonafide.SetParameterValue("kggrno", "-");
                string folderpath = Server.MapPath("BonafideFile");
                string filename = "Bonafide_" + std + div + "_" + grno + ".pdf";

                bonafide.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(Server.MapPath("~/BonafideModule/BonafideFile/" + filename));
                Response.End();
            }
            catch (Exception ex)
            {
                Log.Error("GenrateBonafide.showbonafideReportCenturyForDomicile", ex);
            }
        }

    }
}