using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.FeesModule
{
    public partial class StudentMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string year = new FeesModel().setActiveAcademicYear();
                lblAcademicyear.Text = year;

                loadFormControl();

                if (Request.QueryString["mode"] != null && Request.QueryString["mode"].Equals("add"))
                {
                    lblpagetitle.InnerText = "Add Student Data";
                    txtgrno.ReadOnly = false;
                }
                else if (Request.QueryString["mode"] != null && Request.QueryString["mode"].Equals("edit"))
                {
                    lblpagetitle.InnerText = "Edit Student Data";
                    txtgrno.ReadOnly = true;
                    string std = Request.QueryString["std"].ToString();
                    string grno = Request.QueryString["grno"].ToString();
                    string academicyear = lblAcademicyear.Text;
                    fetchStudentDetails(std, grno, academicyear);
                }

            }

           


            

            
                
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (!Page.IsValid) return;

            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "select Count(*) from studentmaster where std='"+cmb_std.Text+"' and grno='"+txtgrno.Text+"' and academicyear='"+lblAcademicyear.Text+ "' and (leftstatus IS NULL OR leftstatus = '');";
                    SqlCommand cmd = new SqlCommand(query, con);
                    var rcnt = cmd.ExecuteScalar();
                    if (!String.IsNullOrEmpty(rcnt.ToString()))
                    {
                        if (Convert.ToInt32(rcnt.ToString()) == 0)
                        {
                            addSave();
                        }
                        else
                        {
                            updatesave();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Error("StudentMaster.btn_save_Click", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }


        public string addSave()
        {
            SqlConnection con = null;
            try
            {
                    int smmchk=0,contcid=0;
                string leftstat = "", accountname="",fullname="", dobwords="",query="";

                    if (chk_sms.Checked == true)
                    {
                        smmchk = 1;
                    }
                    else
                    {
                        smmchk = 0;
                    }


                   
                   using(con = Connection.getConnection())
                    {
                            con.Open();
                   
                            int rollno;
                            if (txtrollno.Text == "")
                            {
                                rollno = 0;
                            }
                            else
                            {
                                rollno = Convert.ToInt32(txtrollno.Text);
                            }

                    if (chk_left.Checked == true)
                    {
                        leftstat = "Yes";
                    }
                    dobwords = Functions.DateToText(Convert.ToDateTime(lbldobtxt.Text), false, false);

                    fullname = txt_surname.Text + " " + txt_firstname.Text + " " + txt_fathername.Text + " " + txt_mothername.Text;

                    accountname = cmbaccountname.SelectedValue.ToString();
                    int newcid = 0;
                    query = "SELECT MAX(CAST(cid AS INT)) FROM studentmaster WHERE TRY_CAST(cid AS INT) IS NOT NULL";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        contcid = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();

                    newcid = contcid+1;

                    query = "insert into studentmaster([LNAME],[FNAME],[MNAME],[MOTHERNAME],[STD],[DIV],[ROLLNO],[GRNO],[SHIFTNAME],[GENDER],[DOB],[BLOODGROUP],[RELIGION],[CASTE],[SUBCASTE],[CATEGORY],[MOBILE],[CONTACT2],[ADDRESS],[STATE],[EMAIL],[DOA],[SMS],[PHOTOPATH],[CARDID],[FULLNAME],[City],[saralid],[aadharcard],[bankname],[bankacc],[admissiontype],[subjects],[freeshiptype],[placeofbirth],[birthtaluka],[birthdistrict],[birthstate],[birthcountry],[mothertongue],nationality,admissionstd,lastschool,dobwords,schoolsection,leftstatus,academicyear,house,feesinstallment,accountname,uniformid,cid,apaar_id,pen_no)" +
                                    "values(@LNAME,@FNAME,@MNAME,@MOTHERNAME,@STD,@DIV,@ROLLNO,@GRNO,@SHIFTNAME,@GENDER,@DOB,@BLOODGROUP,@RELIGION,@CASTE,@SUBCASTE,@CATEGORY,@MOBILE,@CONTACT2,@ADDRESS,@STATE,@EMAIL,@DOA,@SMS,@PHOTOPATH,@CARDID,@FULLNAME,@City,@saralid,@aadharcard,@bankname,@bankacc,@admissiontype,@subjects,@freeshiptype,@placeofbirth,@birthtaluka,@birthdistrict,@birthstate,@birthcountry,@mothertongue,@nationality,@admissionstd,@lastschool,@dobwords,@schoolsection,@leftstatus,@academicyear,@house,@feesinstallment,@accountname,@uniformid,@cid,@apaar_id,@pen_no);";

                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@LNAME", txt_surname.Text);
                    cmd.Parameters.AddWithValue("@FNAME", txt_firstname.Text);
                    cmd.Parameters.AddWithValue("@MNAME", txt_fathername.Text);
                    cmd.Parameters.AddWithValue("@MOTHERNAME", txt_mothername.Text);
                    cmd.Parameters.AddWithValue("@STD", cmb_std.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@DIV", cmb_div.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@ROLLNO",rollno);
                    cmd.Parameters.AddWithValue("@GRNO", txtgrno.Text);
                    cmd.Parameters.AddWithValue("@SHIFTNAME",cmb_shiftname.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@GENDER",cmb_gender.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@DOB", lbldobtxt.Text);
                    cmd.Parameters.AddWithValue("@BLOODGROUP",cmb_bloodgroup.SelectedValue.ToString());
                    //cmd.Parameters.AddWithValue("@RELIGION",cmb_religon.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RELIGION", txt_religon.Text);
                    //cmd.Parameters.AddWithValue("@CASTE",cmb_caste.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CASTE", txt_Caste.Text);
                    cmd.Parameters.AddWithValue("@SUBCASTE", txt_subcaste.Text);
                    //cmd.Parameters.AddWithValue("@SUBCASTE",cmb_subcaste.SelectedValue.ToString());
                    //cmd.Parameters.AddWithValue("@CATEGORY",cmb_category.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CATEGORY", txt_Category.Text);
                    cmd.Parameters.AddWithValue("@MOBILE",txt_contact1.Text);
                    cmd.Parameters.AddWithValue("@CONTACT2",txt_contact2.Text);
                    cmd.Parameters.AddWithValue("@ADDRESS",txt_address.InnerText.ToString());
                    cmd.Parameters.AddWithValue("@STATE",cmb_state.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@EMAIL",txt_email.Text);
                    cmd.Parameters.AddWithValue("@DOA", lblAdmissiondatetxt.Text);
                    cmd.Parameters.AddWithValue("@SMS", smmchk);
                    cmd.Parameters.AddWithValue("@PHOTOPATH","-");
                    cmd.Parameters.AddWithValue("@CARDID",txt_rfid.Text);
                    cmd.Parameters.AddWithValue("@FULLNAME",fullname);
                    cmd.Parameters.AddWithValue("@City",txt_city.Text);
                    cmd.Parameters.AddWithValue("@saralid",txt_saralid.Text);
                    cmd.Parameters.AddWithValue("@aadharcard",txt_aadhar.Text);
                    cmd.Parameters.AddWithValue("@bankname","");
                    cmd.Parameters.AddWithValue("@bankacc","");
                    cmd.Parameters.AddWithValue("@admissiontype",cmb_admission_type.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@subjects",cmbsubject.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@freeshiptype",cmbfreeship.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@placeofbirth",txtbirthplace.Text);
                    cmd.Parameters.AddWithValue("@birthtaluka", txttaluka.Text);
                    cmd.Parameters.AddWithValue("@birthdistrict", txtdistrict.Text);
                    cmd.Parameters.AddWithValue("@birthstate",cmbbirthstate.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@birthcountry",txtbirthcountry.Text);
                    cmd.Parameters.AddWithValue("@mothertongue",txtmothertongue.Text);
                    cmd.Parameters.AddWithValue("@nationality",cmbnationality.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@admissionstd",cmbadmstd.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@lastschool",txtlastschool.Text);
                    cmd.Parameters.AddWithValue("@dobwords",dobwords);
                    cmd.Parameters.AddWithValue("@schoolsection",cmb_section.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@leftstatus",leftstat);
                    cmd.Parameters.AddWithValue("@academicyear", lblAcademicyear.Text);
                    cmd.Parameters.AddWithValue("@house",cmbhouse.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@feesinstallment",cmbinstallment.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@accountname",cmbaccountname.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@uniformid",txt_uniformid.Text);
                    cmd.Parameters.AddWithValue("@cid",newcid);
                    cmd.Parameters.AddWithValue("@apaar_id", txtapaarid.Text);
                    cmd.Parameters.AddWithValue("@pen_no", txtpenno.Text);
                    cmd.ExecuteNonQuery();
                }

                lblinfomsg.Text = "Student Data Saved Successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                return "ok";
                    
            }
            catch (Exception ex)
            {
                Log.Error("StudentMaster.addSave", ex);

                lblalertmessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                return ex.Message;
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }


        public string updatesave()
        {
            SqlConnection con = null;
            try
            {
                String query = "", leftstat = "";
                String query2 = "";
                String query3 = "";
                
                string dobwords = "", fullname="";
                string accountname = "";

                dobwords = Functions.DateToText(Convert.ToDateTime(lbldobtxt.Text), false, false);
                
                    int smmchk;
                    if (chk_sms.Checked == true)
                    {
                        smmchk = 1;
                    }
                    else
                    {
                        smmchk = 0;
                    }

                using (con = Connection.getConnection())
                {
                    con.Open();
                    fullname = txt_surname.Text + " " + txt_firstname.Text + " " + txt_fathername.Text + " " + txt_mothername.Text;

                    //String grno2 = grno.Text;
                    String std = cmb_std.SelectedValue.ToString();
                    String div = cmb_div.SelectedValue.ToString();
                    int rollno;
                    if (txtrollno.Text == "")
                    {
                        rollno = 0;
                    }
                    else
                    {
                        rollno = Convert.ToInt32(txtrollno.Text);
                    }

                    if (chk_left.Checked == true)
                    {
                        leftstat = "Yes";
                    }

                    accountname = cmbaccountname.Text;

                    //save updated data
                    query = "update studentmaster " +
                            "set fname=@fname,Mname=@Mname,Lname=@Lname,std=@std,div=@div,rollno=@rollno,grno=@grno,gender=@gender,dob=@dob,bloodgroup=@bloodgroup,caste=@caste,religion=@religion,category=@category,address=@address,city=@city,state=@state " +
                    ",mothername=@mothername,mobile=@mobile,email=@email,shiftname=@shiftname,doa=@doa,CARDID=@CARDID,sms=@sms,fullname=@fullname,subcaste=@subcaste,contact2=@contact2,ispromoted=@ispromoted,saralid=@saralid,aadharcard=@aadharcard,bankname=@bankname,bankacc=@bankacc " +
                    ",freeshiptype=@freeshiptype,admissiontype=@admissiontype,subjects=@subjects,[placeofbirth]=@placeofbirth,[birthtaluka]=@birthtaluka,[birthdistrict]=@birthdistrict,[birthstate]=@birthstate,[birthcountry]=@birthcountry,[mothertongue]=@mothertongue,nationality=@nationality,admissionstd=@admissionstd,lastschool=@lastschool,dobwords=@dobwords,schoolsection=@schoolsection,leftstatus=@leftstatus,house=@house,feesinstallment=@feesinstallment,accountname=@accountname,uniformid=@uniformid,apaar_id=@apaar_id,pen_no=@pen_no " +
                    "where std=@oldstd and grno=@oldgrno and academicyear=@academicyear;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@LNAME", txt_surname.Text);
                    cmd.Parameters.AddWithValue("@FNAME", txt_firstname.Text);
                    cmd.Parameters.AddWithValue("@MNAME", txt_fathername.Text);
                    cmd.Parameters.AddWithValue("@MOTHERNAME", txt_mothername.Text);
                    cmd.Parameters.AddWithValue("@STD", cmb_std.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@DIV", cmb_div.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@ROLLNO", rollno);
                    cmd.Parameters.AddWithValue("@GRNO", txtgrno.Text);
                    cmd.Parameters.AddWithValue("@SHIFTNAME", cmb_shiftname.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@GENDER", cmb_gender.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@DOB", lbldobtxt.Text);
                    cmd.Parameters.AddWithValue("@BLOODGROUP", cmb_bloodgroup.SelectedValue.ToString());
                    //cmd.Parameters.AddWithValue("@RELIGION", cmb_religon.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RELIGION", txt_religon.Text);
                    //cmd.Parameters.AddWithValue("@CASTE", cmb_caste.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CASTE", txt_Caste.Text);
                    //cmd.Parameters.AddWithValue("@SUBCASTE", cmb_subcaste.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@SUBCASTE", txt_subcaste.Text);
                    //cmd.Parameters.AddWithValue("@CATEGORY", cmb_category.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CATEGORY", txt_Category.Text);
                    cmd.Parameters.AddWithValue("@MOBILE", txt_contact1.Text);
                    cmd.Parameters.AddWithValue("@CONTACT2", txt_contact2.Text);
                    cmd.Parameters.AddWithValue("@ADDRESS", txt_address.InnerText.ToString());
                    cmd.Parameters.AddWithValue("@STATE", cmb_state.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@EMAIL", txt_email.Text);
                    cmd.Parameters.AddWithValue("@DOA", lblAdmissiondatetxt.Text);
                    cmd.Parameters.AddWithValue("@SMS", smmchk);
                    //cmd.Parameters.AddWithValue("@PHOTOPATH", "-");
                    cmd.Parameters.AddWithValue("@CARDID", txt_rfid.Text);
                    cmd.Parameters.AddWithValue("@FULLNAME", fullname);
                    cmd.Parameters.AddWithValue("@City", txt_city.Text);
                    //cmd.Parameters.AddWithValue("@photo", "");
                    cmd.Parameters.AddWithValue("@saralid", txt_saralid.Text);
                    cmd.Parameters.AddWithValue("@aadharcard", txt_aadhar.Text);
                    cmd.Parameters.AddWithValue("@bankname", "");
                    cmd.Parameters.AddWithValue("@bankacc", "");
                    cmd.Parameters.AddWithValue("@admissiontype", cmb_admission_type.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@subjects", cmbsubject.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@freeshiptype", cmbfreeship.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@placeofbirth", txtbirthplace.Text);
                    cmd.Parameters.AddWithValue("@birthtaluka", txttaluka.Text);
                    cmd.Parameters.AddWithValue("@birthdistrict", txtdistrict.Text);
                    cmd.Parameters.AddWithValue("@birthstate", cmbbirthstate.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@birthcountry", txtbirthcountry.Text);
                    cmd.Parameters.AddWithValue("@mothertongue", txtmothertongue.Text);
                    cmd.Parameters.AddWithValue("@nationality", cmbnationality.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@admissionstd", cmbadmstd.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@lastschool", txtlastschool.Text);
                    cmd.Parameters.AddWithValue("@dobwords", dobwords);
                    cmd.Parameters.AddWithValue("@schoolsection", cmb_section.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@leftstatus", leftstat);
                    cmd.Parameters.AddWithValue("@academicyear", lblAcademicyear.Text);
                    cmd.Parameters.AddWithValue("@house", cmbhouse.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@feesinstallment", cmbinstallment.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@accountname", cmbaccountname.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@uniformid", txt_uniformid.Text);
                    cmd.Parameters.AddWithValue("@ispromoted", cmbispromted.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@oldstd",lbloldstd.Text);
                    cmd.Parameters.AddWithValue("@oldgrno",lbloldgrno.Text);
                    cmd.Parameters.AddWithValue("@apaar_id", txtapaarid.Text);
                    cmd.Parameters.AddWithValue("@pen_no", txtpenno.Text);
                    
                    cmd.ExecuteNonQuery();

                    //update left student
                    if (cmb_std.Text == "LEFT")
                    {
                        query = "insert into LEFTSTUDENTS([LNAME],[FNAME],[MNAME],[MOTHERNAME],[STD],[DIV],[ROLLNO],[GRNO],[SHIFTNAME],[GENDER],[DOB],[BLOODGROUP],[RELIGION],[CASTE],[SUBCASTE],[CATEGORY],[MOBILE],[CONTACT2],[ADDRESS],[STATE],[EMAIL],[DOA],[SMS],[PHOTOPATH],[CARDID],[FULLNAME],[City],[photo],[saralid],[aadharcard],[bankname],[bankacc],[admissiontype],[subjects],[freeshiptype],[placeofbirth],[birthtaluka],[birthdistrict],[birthstate],[birthcountry],[mothertongue],nationality,admissionstd,lastschool,dobwords,schoolsection,leftstatus,academicyear,house,feesinstallment,accountname,uniformid)" +
                               "values(@LNAME,@FNAME,@MNAME,@MOTHERNAME,@STD,@DIV,@ROLLNO,@GRNO,@SHIFTNAME,@GENDER,@DOB,@BLOODGROUP,@RELIGION,@CASTE,@SUBCASTE,@CATEGORY,@MOBILE,@CONTACT2,@ADDRESS,@STATE,@EMAIL,@DOA,@SMS,@PHOTOPATH,@CARDID,@FULLNAME,@City,@photo,@saralid,@aadharcard,@bankname,@bankacc,@admissiontype,@subjects,@freeshiptype,@placeofbirth,@birthtaluka,@birthdistrict,@birthstate,@birthcountry,@mothertongue,@nationality,@admissionstd,@lastschool,@dobwords,@schoolsection,@leftstatus,@academicyear,@house,@feesinstallment,@accountname,@uniformid);";

                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@LNAME", txt_surname.Text);
                        cmd.Parameters.AddWithValue("@FNAME", txt_firstname.Text);
                        cmd.Parameters.AddWithValue("@MNAME", txt_fathername.Text);
                        cmd.Parameters.AddWithValue("@MOTHERNAME", txt_mothername.Text);
                        cmd.Parameters.AddWithValue("@STD", cmb_std.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@DIV", cmb_div.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@ROLLNO", rollno);
                        cmd.Parameters.AddWithValue("@GRNO", txtgrno.Text);
                        cmd.Parameters.AddWithValue("@SHIFTNAME", cmb_shiftname.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@GENDER", cmb_gender.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@DOB", lbldobtxt.Text);
                        cmd.Parameters.AddWithValue("@BLOODGROUP", cmb_bloodgroup.SelectedValue.ToString());
                        //cmd.Parameters.AddWithValue("@RELIGION", cmb_religon.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@RELIGION", txt_religon.Text);
                        //cmd.Parameters.AddWithValue("@CASTE", cmb_caste.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@CASTE", txt_Caste.Text);
                        //cmd.Parameters.AddWithValue("@SUBCASTE", cmb_subcaste.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@SUBCASTE", txt_subcaste.Text);
                        //cmd.Parameters.AddWithValue("@CATEGORY", cmb_category.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@CATEGORY", txt_Category.Text);
                        cmd.Parameters.AddWithValue("@MOBILE", txt_contact1.Text);
                        cmd.Parameters.AddWithValue("@CONTACT2", txt_contact2.Text);
                        cmd.Parameters.AddWithValue("@ADDRESS", txt_address.InnerText.ToString());
                        cmd.Parameters.AddWithValue("@STATE", cmb_state.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@EMAIL", txt_email.Text);
                        cmd.Parameters.AddWithValue("@DOA", lblAdmissiondatetxt.Text);
                        cmd.Parameters.AddWithValue("@SMS", smmchk);
                        cmd.Parameters.AddWithValue("@PHOTOPATH", "-");
                        cmd.Parameters.AddWithValue("@CARDID", txt_rfid.Text);
                        cmd.Parameters.AddWithValue("@FULLNAME", fullname);
                        cmd.Parameters.AddWithValue("@City", txt_city.Text);
                        cmd.Parameters.AddWithValue("@photo", "");
                        cmd.Parameters.AddWithValue("@saralid", txt_saralid.Text);
                        cmd.Parameters.AddWithValue("@aadharcard", txt_aadhar.Text);
                        cmd.Parameters.AddWithValue("@bankname", "");
                        cmd.Parameters.AddWithValue("@bankacc", "");
                        cmd.Parameters.AddWithValue("@admissiontype", cmb_admission_type.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@subjects", cmbsubject.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@freeshiptype", cmbfreeship.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@placeofbirth", txtbirthplace.Text);
                        cmd.Parameters.AddWithValue("@birthtaluka", txttaluka.Text);
                        cmd.Parameters.AddWithValue("@birthdistrict", txtdistrict.Text);
                        cmd.Parameters.AddWithValue("@birthstate", cmbbirthstate.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@birthcountry", txtbirthcountry.Text);
                        cmd.Parameters.AddWithValue("@mothertongue", txtmothertongue.Text);
                        cmd.Parameters.AddWithValue("@nationality", cmbnationality.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@admissionstd", cmbadmstd.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@lastschool", txtlastschool.Text);
                        cmd.Parameters.AddWithValue("@dobwords", dobwords);
                        cmd.Parameters.AddWithValue("@schoolsection", cmb_section.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@leftstatus", leftstat);
                        cmd.Parameters.AddWithValue("@academicyear", lblAcademicyear.Text);
                        cmd.Parameters.AddWithValue("@house", cmbhouse.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@feesinstallment", cmbinstallment.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@accountname", cmbaccountname.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@uniformid", txt_uniformid.Text);
                        cmd.ExecuteNonQuery();

                        query2 = "delete From studentmaster where std='" + lbloldstd.Text + "' And div='" + lblolddiv.Text + "' And [grno]='" + lbloldgrno.Text + "';";

                        cmd = new SqlCommand(query, con);

                        cmd.ExecuteNonQuery();

                    }

                    //query = "update studentfees  set grno=@GRNO where std=@STD  and grno=@oldgrno and academicyear=@academicyear ";
                    //cmd = new SqlCommand(query, con);
                    //cmd.Parameters.AddWithValue("@GRNO", txtgrno.Text);
                    //cmd.Parameters.AddWithValue("@STD", cmb_std.SelectedValue.ToString());
                    //cmd.Parameters.AddWithValue("@oldgrno", lbloldgrno.Text);
                    //cmd.Parameters.AddWithValue("@academicyear", lblAcademicyear.Text);
                    //cmd.ExecuteNonQuery();

                    //query = "update ReceiptReport set grno=@GRNO where std=@STD  and grno=@oldgrno and academicyear=@academicyear ";
                    //cmd = new SqlCommand(query, con);
                    //cmd.Parameters.AddWithValue("@GRNO", txtgrno.Text);
                    //cmd.Parameters.AddWithValue("@STD", cmb_std.SelectedValue.ToString());
                    //cmd.Parameters.AddWithValue("@oldgrno", lbloldgrno.Text);
                    //cmd.Parameters.AddWithValue("@academicyear", lblAcademicyear.Text);
                    //cmd.ExecuteNonQuery();

                }

                lblinfomsg.Text = "Student Data Updated Successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);


              

                return "ok";

                    //string result = "";
                    //StudentClass _stud = new StudentClass();
                    //if (schoolid == 1 || schoolid == 4 || schoolid == 2)
                    //{
                    //    result = _stud.updateGrnoInFeesModule(oldgrno, grno.Text, cmbstd.Text);

                    //    if (!result.Equals("ok"))
                    //    {
                    //        MessageBox.Show(result);
                    //    }

                    //}

                    //if (schoolid == 1)
                    //{
                    //    //For Mkes school
                    //    result = _stud.updateGrnoInMarksheetModule(oldgrno, grno.Text, cmbstd.Text);
                    //    if (!result.Equals("ok"))
                    //    {
                    //        MessageBox.Show(result);
                    //    }
                    //}
                    //else if (schoolid == 2)
                    //{
                    //    //For St Haris school
                    //    result = _stud.updateGrnoInMarksheetModuleStharis(oldgrno, grno.Text, cmbstd.Text);
                    //    if (!result.Equals("ok"))
                    //    {
                    //        MessageBox.Show(result);
                    //    }
                    //}
                    //else if (schoolid == 4)
                    //{
                    //    //For Mahila Samiti School

                    //}

                    //Update Online Database if Exist

                    //if (Logininfo.appservername != null && Logininfo.appservername.Length > 0)
                    //{
                    //    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                    //    TextInfo ti = cultureInfo.TextInfo;

                    //    StudentClass _studentclass = new StudentClass();
                    //    _studentclass.LNAME = ti.ToTitleCase(sur.ToLower());
                    //    _studentclass.FNAME = ti.ToTitleCase(name.ToLower());
                    //    _studentclass.MNAME = ti.ToTitleCase(father.ToLower());
                    //    _studentclass.MOTHERNAME = ti.ToTitleCase(mother.ToLower());
                    //    _studentclass.fullname = _studentclass.FNAME + " " + _studentclass.MNAME + " " + _studentclass.LNAME + " " + _studentclass.MOTHERNAME;
                    //    _studentclass.STD = cmbstd.Text;
                    //    _studentclass.DIV = cmbdiv.Text;
                    //    _studentclass.ROLLNO = rollno.ToString();
                    //    _studentclass.GRNO = grno.Text;
                    //    _studentclass.SHIFTNAME = cmbshift.Text;
                    //    _studentclass.GENDER = cmbgender.Text;
                    //    _studentclass.DOB = Convert.ToDateTime(dobcal.Value).ToString("yyyy/MM/dd");
                    //    _studentclass.MOBILE = contact1.Text;
                    //    _studentclass.contact2 = contact2.Text;
                    //    _studentclass.ADDRESS = address.Text;
                    //    _studentclass.CARDID = cid.Text;




                    //    UpdateOnline _updateonline = new UpdateOnline();
                    //    try
                    //    {
                    //        _updateonline.upDateStudentData(_studentclass, oldstd, oldgrno);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        // MessageBox.Show(ex.Message);
                    //    }

                    //}

                
            }
            catch (Exception ex)
            {
                Log.Error("StudentMaster.updatesave()", ex);
                lblalertmessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                return ex.Message;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        protected void err_std_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmb_std.SelectedValue.ToString().Equals("Select Std"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void err_div_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmb_div.SelectedValue.ToString().Equals("Select Div"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void err_shiftname_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmb_shiftname.SelectedValue.ToString().Equals("0"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void err_section_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmb_section.SelectedValue.ToString().Equals("0"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void err_gender_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmb_gender.SelectedValue.ToString().Equals("0"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void err_admistype_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmb_admission_type.SelectedValue.ToString().Equals("0"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void btnResetData_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("StudentMaster.aspx?mode=add");
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
                    std.Rows.Add("-");
                    cmb_std.DataSource = std;
                    cmb_std.DataTextField = "std";
                    cmb_std.DataValueField = "std";
                    cmb_std.DataBind();
                    cmb_std.SelectedValue = "Select Std";

                    cmbadmstd.DataSource = std;
                    cmbadmstd.DataTextField = "std";
                    cmbadmstd.DataValueField = "std";
                    cmbadmstd.DataBind();
                    cmbadmstd.SelectedValue = "Select Std";

                    std.Dispose();

                    query = "select Div From Div where div not in ('ALL')";
                    adap = new SqlDataAdapter(query, con);
                    DataTable div = new DataTable();
                    adap.Fill(div);
                    div.Rows.Add("Select Div");
                    div.Rows.Add("-");
                    cmb_div.DataSource = div;
                   
                    cmb_div.DataTextField = "Div";
                    cmb_div.DataValueField = "Div";
                    cmb_div.DataBind();
                    cmb_div.SelectedValue = "Select Div";
                    div.Dispose();

                    query = "Select shiftname from shiftmaster order by shiftname;";
                    DataTable ds5 = new DataTable();
                    adap = new SqlDataAdapter(query, con);
                    adap.Fill(ds5);
                    ds5.Rows.Add("Select Shift");
                    ds5.Rows.Add("-");
                    cmb_shiftname.DataSource = ds5;
                    cmb_shiftname.DataTextField = "shiftname";
                    cmb_shiftname.DataValueField = "shiftname";
                    cmb_shiftname.DataBind();
                    cmb_shiftname.SelectedValue = "Select Shift";
                    ds5.Dispose();

                    //query = "Select caste from Caste order by caste;";
                    //DataTable ds6 = new DataTable();
                    //adap = new SqlDataAdapter(query, con);
                   
                    //adap.Fill(ds6);
                    //ds6.Rows.Add("Select Caste");
                    //ds6.Rows.Add("-");
                    //cmb_caste.DataSource = ds6;
                    //cmb_caste.DataTextField = "caste";
                    //cmb_caste.DataValueField = "caste";
                    //cmb_caste.DataBind();
                    //cmb_caste.SelectedValue = "Select Caste";
                    //ds6.Dispose();

                    //query = "Select subcaste from SubCaste order by subcaste;";
                    //DataTable ds7 = new DataTable();
                    //adap = new SqlDataAdapter(query, con);
                   
                    //adap.Fill(ds7);
                    //ds7.Rows.Add("Select SubCaste");
                    //ds7.Rows.Add("-");
                    //cmb_subcaste.DataSource = ds7;
                    //cmb_subcaste.DataTextField = "subcaste";
                    //cmb_subcaste.DataValueField = "subcaste";
                    //cmb_subcaste.DataBind();
                    //cmb_subcaste.SelectedValue = "Select SubCaste";
                    //ds7.Dispose();


                    //query = "Select category from category order by category;";
                    //DataTable ds8 = new DataTable();
                    //adap = new SqlDataAdapter(query, con);
                  
                    //adap.Fill(ds8);
                    //ds8.Rows.Add("Select Category");
                    //ds8.Rows.Add("-");
                    //cmb_category.DataSource = ds8;
                    //cmb_category.DataTextField = "category";
                    //cmb_category.DataValueField = "category";
                    //cmb_category.DataBind();
                    //cmb_category.SelectedValue = "Select Category";
                    //ds8.Dispose();

                    //query = "Select religion from religion order by religion;";
                    //DataTable ds9 = new DataTable();
                    //adap = new SqlDataAdapter(query, con);
                    
                    //adap.Fill(ds9);
                    //ds9.Rows.Add("Select Religion");
                    //ds9.Rows.Add("-");
                    //cmb_religon.DataSource = ds9;
                    //cmb_religon.DataTextField = "religion";
                    //cmb_religon.DataValueField = "religion";
                    //cmb_religon.DataBind();
                    //cmb_religon.SelectedValue = "Select Religion";
                    //ds9.Dispose();


                    query = "select sectionname From [SchoolSectionMaster];";
                    DataTable ds10 = new DataTable();
                    adap = new SqlDataAdapter(query, con);
                    adap.Fill(ds10);
                    ds10.Rows.Add("Select Section");
                    ds10.Rows.Add("-");
                    cmb_section.DataSource = ds10;
                    cmb_section.DataTextField = "sectionname";
                    cmb_section.DataValueField = "sectionname";
                    cmb_section.DataBind();
                    cmb_section.SelectedValue = "Select Section";
                    ds10.Dispose();




                }
            }
            catch (Exception ex)
            {
                Log.Error("StudentMaster.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }


        public void fetchStudentDetails(string std,string grno,string academicyear)
        {
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    int smschk = 0;
                    string leftstat = "0";
                    string query = "Select [LNAME],[FNAME],[MNAME],[MOTHERNAME],[STD],[DIV],[ROLLNO],[GRNO],[SHIFTNAME],[GENDER],[DOB],[BLOODGROUP],[RELIGION],[CASTE],[SUBCASTE],[CATEGORY],[MOBILE],[CONTACT2],[ADDRESS],[STATE],[EMAIL],[DOA],[SMS],[PHOTOPATH],[CARDID],[City],[photo],[saralid],[aadharcard],[bankname],[bankacc],[studentid],[admissiontype],[subjects],[freeshiptype],[ispromoted],[cid],[placeofbirth],[birthtaluka],[birthdistrict],[birthstate],[birthcountry],[mothertongue],nationality,admissionstd,lastschool,schoolsection,leftstatus,house,feesinstallment,lcno,accountname,uniformid,apaar_id,pen_no" +
                       " From studentmaster where [GRNO]='" + grno + "' And [Std]='" + std + "' and academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '');";
                   
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        txt_surname.Text = reader[0].ToString();
                        txt_firstname.Text = reader[1].ToString();
                        txt_fathername.Text = reader[2].ToString();
                        txt_mothername.Text = reader[3].ToString();
                        cmb_std.SelectedValue = reader[4].ToString();
                        lbloldstd.Text= reader[4].ToString();
                        cmb_div.SelectedValue = reader[5].ToString();
                        lblolddiv.Text= reader[5].ToString();
                        txtrollno.Text = reader[6].ToString();
                        
                        txtgrno.Text = reader[7].ToString();
                        lbloldgrno.Text = reader[7].ToString();//global variable set for grno
                        cmb_shiftname.SelectedValue = reader[8].ToString();
                        cmb_gender.SelectedValue = reader[9].ToString();
                        lbldobtxt.Text = Convert.ToDateTime(reader[10]).ToString("yyyy/MM/dd");
                        cmb_bloodgroup.SelectedValue = reader[11].ToString();
                        //cmb_religon.SelectedValue = reader[12].ToString();
                        txt_religon.Text = reader[12].ToString();
                        //cmb_caste.SelectedValue = reader[13].ToString();
                        txt_Caste.Text = reader[13].ToString();
                        //cmb_subcaste.SelectedValue = reader[14].ToString();
                        txt_subcaste.Text = reader[14].ToString();
                        //cmb_category.SelectedValue = reader[15].ToString();
                        txt_Category.Text = reader[15].ToString();
                        txt_contact1.Text = reader[16].ToString();
                        txt_contact2.Text = reader[17].ToString();
                        txt_address.InnerText = reader[18].ToString();
                        cmb_state.SelectedValue = reader[19].ToString();
                        txt_email.Text = reader[20].ToString();
                        lblAdmissiondatetxt.Text = Convert.ToDateTime(reader[21].ToString().Replace('/','-')).ToString("yyyy/MM/dd");
                        smschk = Convert.ToInt32(reader[22]);
                        if (smschk == 1)
                        {
                            chk_sms.Checked = true;
                        }
                        else
                        {
                            chk_sms.Checked = false;
                        }

                        txt_rfid.Text = reader[24].ToString();
                        txt_city.Text = reader[25].ToString();
                        txt_saralid.Text = reader[27].ToString();
                        txt_aadhar.Text = reader[28].ToString();
                        
                        txt_studentid.Text = reader[31].ToString();
                        cmb_admission_type.SelectedValue = reader[32].ToString();
                        cmbsubject.SelectedValue = reader[33].ToString();
                        cmbfreeship.SelectedValue = reader[34].ToString();
                        cmbispromted.SelectedValue = reader[35].ToString();
                        
                        txtbirthplace.Text = reader[37].ToString();
                        txttaluka.Text = reader[38].ToString();
                        txtdistrict.Text = reader[39].ToString();
                        cmbbirthstate.SelectedValue = reader[40].ToString();
                        txtbirthcountry.Text = reader[41].ToString();
                        txtmothertongue.Text = reader[42].ToString();
                        cmbnationality.SelectedValue = reader[43].ToString();
                        cmbadmstd.SelectedValue = reader[44].ToString();
                        txtlastschool.Text = reader[45].ToString();
                        cmb_section.SelectedValue = reader[46].ToString();
                        leftstat = reader[47].ToString();
                        if (leftstat == "Yes")
                        {
                            chk_left.Checked = true;
                            
                        }
                        else
                        {
                            chk_left.Checked = false;
                            
                        }
                        cmbhouse.SelectedValue = reader[48].ToString();

                        if (reader[49].ToString().Length > 0)
                            cmbinstallment.SelectedValue = reader[49].ToString();

                        cmbaccountname.SelectedValue = reader["accountname"].ToString();

                        txt_uniformid.Text = reader["uniformid"].ToString();
                        txtapaarid.Text = reader["apaar_id"].ToString();
                        txtpenno.Text = reader["pen_no"].ToString();

                    }
                    reader.Close();

                }
            }
            catch(Exception ex)
            {
                Log.Error("StudentMaster.fetchStudentDetails", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }
    }
}