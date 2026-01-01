//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CenturyRayonSchool.LCModule
{
    public partial class BulkLCGenerate : System.Web.UI.Page
    {
        DataTable studetails = new DataTable();
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

            studetails.Columns.Add("Rollno");
            studetails.Columns.Add("StudentName");
            studetails.Columns.Add("Grno");
            studetails.Columns.Add("Section");
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

                    dateofleaving.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace('-', '/');
                }
            }
            catch (Exception ex)
            {
                Log.Error("BulkLCGenrate.loadFormControl", ex);
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
        protected void GetData_ServerClick(object sender, EventArgs e)
        {
            String std = "", div = "", query = "", year = "";
            std = cmbStd.SelectedValue.ToString();
            div = cmbDiv.SelectedValue.ToString();
            year = lblacademicyear.Text;
            fillgridview(std, div, year);
        }

        public void fillgridview(string std, string div, string year)
        {
            int countlc = 0;
            studetails.Rows.Clear();
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "Select rollno,fullname,grno,schoolsection From studentmaster where std='" + std + "' and div='" + div + "' and academicyear='" + year + "' order by cast(rollno as int) asc;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        countlc = CheckifLCGenerated(std, reader[2].ToString());
                        if (countlc == 0)
                        {
                            studetails.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                        }
                        else
                        {
                            //int rowIndex = studetails.Rows.Count - 1;
                            //foreach (DataRow row in studetails.Rows)
                            //{
                            //    row.Enabled = false;
                            //}
                        }

                    }
                    GridCollection.DataSource = studetails;
                    GridCollection.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error("BulkLCGenrate.fillgridview", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        public static int CheckifLCGenerated(string std, string grno)
        {
            int count = 0;
            SqlConnection con = Connection.getConnection();
            con.Open();
            string query = "select Count(*) From LeavingCertificate where std='" + std + "' and grno='" + grno + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                count = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            con.Close();
            return count;
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
        protected void Button1_ServerClick(object sender, EventArgs e)
        {
            String std = "", div = "", query = "", year = "", grno = "", section = "";
            std = cmbStd.SelectedValue.ToString();
            div = cmbDiv.SelectedValue.ToString();
            year = lblacademicyear.Text;
            SqlConnection con = null;
            string cdt = DateTime.Now.ToString();
            string usercode = lblusercode.Text;
            int count = 0;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    foreach (GridViewRow row in GridCollection.Rows)
                    {
                        if (((CheckBox)row.FindControl("chkSelect")).Checked == true)
                        {
                            StudentMaster stm = new StudentMaster();
                            grno = row.Cells[2].Text;
                            section = row.Cells[3].Text;
                            SqlCommand cmd = null;
                            long lcno = 0;

                            //for century school
                            if (section == "Secondary" || section == "Secondary (Hindi)" || section == "Secondary (Marathi)")
                            {
                                string section1 = "'Secondary','Secondary (Hindi)','Secondary (Marathi)'";

                                lcno = GetLCCountAcademicYear(section1, year);
                            }
                            else
                            {
                                lcno = GetLCCountAcademicYear("'" + section + "'", year);
                            }

                            stm.lcno = lcno.ToString();
                            query = "Select [cardid],[grno],[Lname] as [surname],[Fname] as [studentname],[Mname] as [fathername],[Mothername],[Dob],[saralid],[aadharcard],[std],[religion],[caste],[subcaste],[category],[doa],[placeofbirth],[birthtaluka],[birthdistrict],[birthstate],[birthcountry],[mothertongue],dobwords,admissionstd,nationality,lastschool,progress,conduct,reasonforleaving,remark,schoolsection,div,rollno,academicyear,stdstudying,stdstudyingInWords,photopath,apaar_id,pen_no" +
                                "" +
                                " " +
                                "From studentmaster where std='" + std + "' and div='" + div + "' and grno='" + grno + "'and academicyear='" + year + "';";
                            cmd = new SqlCommand(query, con);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {

                                stm.grno = reader[1].ToString();
                                stm.lname = reader[2].ToString();
                                stm.fname = reader[3].ToString();
                                stm.mname = reader[4].ToString();
                                stm.mothername = reader[5].ToString();
                                stm.dob = Convert.ToDateTime(reader[6]).ToString("yyyy/MM/dd");
                                stm.saralid = reader[7].ToString();
                                stm.aadharcard = reader[8].ToString();
                                stm.std = reader[9].ToString();
                                stm.religion = reader[10].ToString();
                                stm.caste = reader[11].ToString();
                                stm.subcaste = reader[12].ToString();
                                stm.category = reader[13].ToString();
                                stm.doa = Convert.ToDateTime(reader[14]).ToString("yyyy/MM/dd");
                                stm.placeofbirth = reader[15].ToString();
                                stm.birthtaluka = reader[16].ToString();
                                stm.birthdistrict = reader[17].ToString();
                                stm.birthstate = reader[18].ToString();
                                stm.birthcountry = reader[19].ToString();
                                stm.mothertongue = reader[20].ToString();
                                stm.dobwords = reader[21].ToString();
                                stm.admissionstd = reader[22].ToString();
                                stm.nationality = reader[23].ToString();
                                stm.lastschool = checkApostrophee(reader[24].ToString());
                                stm.progress = reader[25].ToString();
                                stm.conduct = reader[26].ToString();
                                stm.reasonofleaving = reader[27].ToString();
                                stm.remark = reader[28].ToString();
                                stm.schoolsection = reader[29].ToString();
                                stm.div = reader[30].ToString();
                                stm.rollno = reader[31].ToString();
                                stm.dateofleaving = dateofleaving.Text;
                                stm.academicyear = reader[32].ToString();
                                stm.stdstudying = reader[33].ToString();
                                stm.stdstudyingInWords = reader["stdstudyingInWords"].ToString();
                                stm.photopath = reader["photopath"].ToString();
                                stm.apaar_id = reader["apaar_id"].ToString();
                                stm.pen_no = reader["pen_no"].ToString();

                                if (stm.dobwords.Trim().Length == 0)
                                {
                                    stm.dobwords = Functions.DateToText(Convert.ToDateTime(reader[6]), false, false);
                                }
                            }
                            reader.Close();

                            //stm.stdstudying = LCfunctions.setStdStudying(stm.std,stm.doa,con) ;
                            query = "select Count (*) from LeavingCertificate where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "'";
                            cmd = new SqlCommand(query, con);
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                count = Convert.ToInt32(reader[0]);

                            }
                            if (count == 0)
                            {
                                query = "insert into [LeavingCertificate](Lcno,[FNAME],[MNAME],[LNAME],[MOTHERNAME],[STD],[GRNO],[DOB],[RELIGION],[CASTE],[subcaste],[CATEGORY],[DOA],[saralid],[aadharcard],[placeofbirth],[birthtaluka],[birthdistrict],[birthstate],[birthcountry],[mothertongue],[Nationality],[Lastschool],[Progress],[DateofLeaving],[Reasonforleaving],[conduct],[remark],dobwords,admissionstd,schoolsection,div,rollno,academicyear,stdstudying,stdstudyingInWords,photopath,apaar_id,pen_no,createddate,createdby) " +
                                       "values('" + stm.lcno + "','" + stm.fname + "','" + stm.mname + "','" + stm.lname + "','" + stm.mothername + "','" + std + "','" + grno + "','" + stm.dob + "','" + stm.religion + "','" + stm.caste + "','" + stm.subcaste + "','" + stm.category + "','" + stm.doa + "','" + stm.saralid + "','" + stm.aadharcard + "','" + stm.placeofbirth + "','" + stm.birthtaluka + "','" + stm.birthdistrict + "','" + stm.birthstate + "','" + stm.birthcountry + "','" + stm.mothertongue + "','" + stm.nationality + "','" + stm.lastschool + "','" + stm.progress + "','" + stm.dateofleaving + "','" + stm.reasonofleaving + "','" + stm.conduct + "','" + stm.remark + "','" + stm.dobwords + "','" + stm.admissionstd + "','" + stm.schoolsection + "','" + stm.div + "','" + stm.rollno + "','" + stm.academicyear + "','" + stm.stdstudying + "','" + stm.stdstudyingInWords + "','" + stm.photopath + "','" + stm.apaar_id + "','" + stm.pen_no + "','" + cdt + "','" + usercode + "')";
                                cmd = new SqlCommand(query, con);
                                cmd.ExecuteNonQuery();

                            }
                            else
                            {
                                query = "update studentmaster set  lcno='" + stm.lcno + "',leftstatus='Yes',dobwords='" + stm.dobwords + "',updateddate='" + cdt + "',updatedby='" + usercode + "' where std='" + std + "' and grno='" + grno + "'and apaar_id = '" + stm.apaar_id + "' and pen_no = '" + stm.pen_no + "' and academicyear='" + year + "';";
                                cmd = new SqlCommand(query, con);
                                cmd.ExecuteNonQuery();
                            }

                        lblinfomsg.Text = "LC Generated Successfully.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                            fillgridview(std, div, year);
                        }
                    }

                    con.Close();
                }


            }
            catch (Exception ex)
            {
                Log.Error("BulkLCGenrate.Button1_ServerClick", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }
        public String checkApostrophee(String textname)
        {
            String text = "";
            char[] arrtext = textname.ToCharArray();
            for (int i = 0; i < arrtext.Length; i++)
            {
                if (arrtext[i].Equals('\''))
                {
                    text += "''";
                    //MessageBox.Show(text);
                }
                else
                {
                    text += arrtext[i];
                }

            }

            return text;
        }
        public class StudentMaster
        {
            public string grno { get; set; }
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
            public string academicyear { get; set; }
            public string stdstudying { get; set; }
            public string stdstudyingInWords { get; set; }
            public string photopath { get; set; }
            public string apaar_id { get; set; }
            public string pen_no { get; set; }
        }

    }
}