//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.BonafideModule
{
    public partial class BulkBonafide : System.Web.UI.Page
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

                    //dateofleaving.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace('-', '/');
                }
            }
            catch (Exception ex)
            {
                Log.Error("BulkBonafide.loadFormControl", ex);
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

                        studetails.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                    }
                    GridCollection.DataSource = studetails;
                    GridCollection.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error("BulkBonafide.fillgridview", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void btnbulkbona_ServerClick(object sender, EventArgs e)
        {
            try
            {

                String std = "", div = "", query = "", year = "", grno = "", section = "";
                std = cmbStd.SelectedValue.ToString();
                div = cmbDiv.SelectedValue.ToString();
                year = lblacademicyear.Text;
                SqlConnection con = null;
                string cdt = DateTime.Now.ToString();
                string usercode = lblusercode.Text;

                con = Connection.getConnection();
                con.Open();

                SqlCommand cmd = null;


                foreach (GridViewRow row in GridCollection.Rows)
                {
                    if (((CheckBox)row.FindControl("chkSelect")).Checked == true)
                    {
                        long bonafidecount = GetBonafideCount();

                        StudentMasterBona stm = new StudentMasterBona();


                        grno = row.Cells[2].Text;


                        stm.bonafideno = bonafidecount.ToString();
                        query = "Select [cardid],[grno],[Lname] as [surname],[Fname] as [studentname],[Mname] as [fathername],[Mothername],[Dob],[saralid],[aadharcard],[std],[religion],[caste],[subcaste],[category],[doa],[placeofbirth],[birthtaluka],[birthdistrict],[birthstate],[birthcountry],[mothertongue],dobwords,admissionstd,nationality,lastschool,progress,conduct,reasonforleaving,remark,schoolsection,div,rollno,academicyear,stdstudying,shiftname,accountname From studentmaster where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "';";
                        cmd = new SqlCommand(query, con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            stm.cardid = reader[0].ToString();
                            stm.grno = reader[1].ToString();
                            stm.lname = reader[2].ToString();
                            stm.fname = reader[3].ToString();
                            stm.mname = reader[4].ToString();
                            stm.mothername = reader[5].ToString();
                            try
                            {
                                stm.dob = Convert.ToDateTime(reader[6]).ToString("yyyy/MM/dd");
                            }
                            catch (Exception ex) { }
                            stm.saralid = reader[7].ToString();
                            stm.aadharcard = reader[8].ToString();
                            stm.std = reader[9].ToString();
                            stm.religion = reader[10].ToString();
                            stm.caste = reader[11].ToString();
                            stm.subcaste = reader[12].ToString();
                            stm.category = reader[13].ToString();
                            try
                            {
                                stm.doa = Convert.ToDateTime(reader[14]).ToString("yyyy/MM/dd");
                            }
                            catch (Exception ex) { }

                            stm.placeofbirth = reader[15].ToString();
                            stm.birthtaluka = reader[16].ToString();
                            stm.birthdistrict = reader[17].ToString();
                            stm.birthstate = reader[18].ToString();
                            stm.birthcountry = reader[19].ToString();
                            stm.mothertongue = reader[20].ToString();
                            stm.dobwords = reader[21].ToString();
                            stm.admissionstd = reader[22].ToString();
                            stm.nationality = reader[23].ToString();
                            stm.lastschool = reader[24].ToString();
                            stm.progress = reader[25].ToString();
                            stm.conduct = reader[26].ToString();
                            stm.reasonofleaving = reader[27].ToString();
                            stm.remark = reader[28].ToString();
                            stm.schoolsection = reader[29].ToString();
                            stm.div = reader[30].ToString();
                            stm.rollno = reader[31].ToString();
                            try
                            {
                                stm.dateofleaving = DateTime.Now.ToString("yyyy/MM/dd");
                            }
                            catch (Exception ex) { }
                            stm.academicyear = reader[32].ToString();
                            stm.stdstudying = reader[33].ToString();
                            stm.shiftname = reader[34].ToString();
                            stm.accountname = reader[35].ToString();
                        }
                        reader.Close();

                        stm.remark = "";
                        query = "select reasonforleaving From LeavingCertificate where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "';";
                        cmd = new SqlCommand(query, con);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            stm.remark = reader[0].ToString();
                        }
                        reader.Close();


                        query = "insert into bonafide(serialno,cardid,grno,[LNAME],[FNAME],[MNAME],mothername,religion,caste,subcaste,nationality,[DOB],placeofbirth,saralid,[aadharcard],issuedate,shiftname,std,div,photopath,dobwords,accountname,academicyear,remark,category,createddate,createdby) " +
                                "values('" + stm.bonafideno + "','" + stm.cardid + "','" + stm.grno + "','" + stm.lname + "','" + stm.fname + "','" + stm.mname + "','" + stm.mothername + "','" + stm.religion + "','" + stm.caste + "','" + stm.subcaste + "','" + stm.nationality + "'," +
                                "'" + stm.dob + "','" + stm.placeofbirth + "','" + stm.saralid + "','" + stm.aadharcard + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + stm.shiftname + "','" + stm.std + "','" + stm.div + "','','" + stm.dobwords + "','" + stm.accountname + "','" + year + "','" + stm.remark + "','" + stm.category + "','" + cdt + "','" + usercode + "');";
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();


                        lblinfomsg.Text = "Bonafide Generated Successfully.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                    }



                }


                con.Close();

            }
            catch (Exception ex)
            {
                Log.Error("BulkBonafide.btnbulkbona_ServerClick", ex);
            }
        }


        public static long GetBonafideCount()
        {
            long count = 0;
            SqlConnection con = Connection.getConnection();
            con.Open();
            String query = "";
            query = "Select Count(*) From bonafide;";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                count = Convert.ToInt64(reader[0]);
            }
            reader.Close();
            count = count + 1;
            con.Close();
            return count;

        }
        private class StudentMasterBona
        {



            public string grno = "";
            public string fname = "";
            public string mname = "";
            public string lname = "";
            public string dob = "";
            public string caste = "";
            public string religion = "";
            public string category = "";
            public string mothername = "";
            public string doa = "";
            public string subcaste = "";
            public string saralid = "";
            public string aadharcard = "";
            public string placeofbirth = "";
            public string birthtaluka = "";
            public string birthdistrict = "";
            public string birthstate = "";
            public string birthcountry = "";
            public string mothertongue = "";
            public string nationality = "";
            public string lastschool = "";
            public string progress = "";
            public string dateofleaving = "";
            public string reasonofleaving = "";
            public string bonafideno = "";
            public string conduct = "";
            public string remark = "";
            public string dobwords = "";
            public string admissionstd = "";
            public string std = "";
            public string schoolsection = "";
            public string div = "";
            public string rollno = "";
            public string academicyear = "";
            public string stdstudying = "";
            public string cardid = "";
            public string shiftname = "";
            public string accountname = "";


        }

    }
}