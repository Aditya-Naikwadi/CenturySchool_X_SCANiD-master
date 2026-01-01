//using CenturyRayonSchool.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using SendMailModule;

namespace CenturyRayonSchool.AdmissionModule
{
    public partial class AdmissionForm : System.Web.UI.Page
    {
        public string isFeesAdmin = "";
        public string AdmissionID = "0", bgcss = "";
        string currentYearRange = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (Request.QueryString["AdmissionID"] != null)
            {
                AdmissionID = Request.QueryString["AdmissionID"];
            }
            if (Session["userid"] != null)
            {
                string userid = Session["username"].ToString();
                if (userid == "adminfees")
                {
                    isFeesAdmin = "";
                }
                
            }
            else
            {
                isFeesAdmin = "c-visible";
            }

            if (!IsPostBack)
            {
                checkforadmissionpage();
                loadFormControl();
                if (AdmissionID != "0")
                {
                    //Edit Admission Form
                    GetDetails(AdmissionID);

                    formlbltitle.Text = "Modify Admission Form Details";
                    lblAdmissionid.Text = AdmissionID;
                    bgcss = "bgclass";
                    //GetDetails1(AdmissionID);
                }
                else
                {
                    //New Admission Form
                    formlbltitle.Text = "Add New Admission Form Details";
                    lblAdmissionid.Text = "0";
                    bgcss = "";
                }

             



            }

            division.Visible = false; // hide division coloumn
            SqlConnection con = null;
            using (con = Connection.getConnection())
            {
                con.Open();
                currentYearRange = "SELECT academicyear FROM AdmissionAcademicYear WHERE active='true'";

                SqlCommand cmd1 = new SqlCommand(currentYearRange, con);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    currentYearRange = reader1[0].ToString();
                }
                Session["currentYearRange"] = currentYearRange;
                lblyear.Text = Session["currentYearRange"].ToString();
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
                    DataTable std1 = new DataTable();
                    adap.Fill(std1);
                    std1.Rows.Add("Select Std");
                    siblingStd2.DataSource = std1;
                    siblingStd2.DataBind();
                    siblingStd2.DataTextField = "std";
                    siblingStd2.DataValueField = "std";
                    siblingStd2.DataBind();
                    siblingStd2.SelectedValue = "Select Std";

                    siblingStd1.DataSource = std1;
                    siblingStd1.DataBind();
                    siblingStd1.DataTextField = "std";
                    siblingStd1.DataValueField = "std";
                    siblingStd1.DataBind();
                    siblingStd1.SelectedValue = "Select Std";

                    std.DataSource = std1;
                    std.DataBind();
                    std.DataTextField = "std";
                    std.DataValueField = "std";
                    std.DataBind();
                    std.SelectedValue = "Select Std";
                    //Basappa holiday
                    laststdpassed.DataSource = std1;
                    laststdpassed.DataBind();
                    laststdpassed.DataTextField = "std";
                    laststdpassed.DataValueField = "std";
                    laststdpassed.DataBind();
                    laststdpassed.SelectedValue = "Select Std";

                    query = "select Religion from Religion where Religion not in ('');";
                     adap = new SqlDataAdapter(query, con);
                    DataTable religiontbl = new DataTable();
                    adap.Fill(religiontbl);
                  
                    religion.DataSource = religiontbl;
                    religion.DataBind();
                    religion.DataTextField = "Religion";
                    religion.DataValueField = "Religion";
                    religion.DataBind();
                    religion.SelectedValue = "Select Religion";


                    //Basappa
                    query = "select div from div where div not in ('All');";
                    adap = new SqlDataAdapter(query, con);
                    DataTable div1 = new DataTable();
                    adap.Fill(div1);
                    //div.Items.Insert(0, "Select");

                    div.DataSource = div1;
                    //  div.DataBind(); //basappa
                    div.DataTextField = "div";
                    div.DataValueField = "div";
                    div.DataBind();
                    //div.SelectedValue = "ALL";
                    div.SelectedValue = "A";

                   
                   
                }
            }
            catch (Exception ex)
            {
                Log.Error("AttendanceMark.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        public void checkforadmissionpage()
        {
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {

                    con.Open();
                    string query = "select StartDate,EndDate,Status from Academicyear where startusforadmissionpage = 'true'; ";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    DataTable dtadmissionpage = new DataTable();
                    adap.Fill(dtadmissionpage);

                    if (dtadmissionpage.Rows.Count < 0)
                    {
                        Response.Redirect("Admissionlabel.aspx");
                    }
                    else
                    {

                        string startdate = "", enddate = "";

                        query = "select StartDate,EndDate,Status from Academicyear where startusforadmissionpage = 'true';";
                        SqlCommand cmd1 = new SqlCommand(query, con);

                        cmd1.ExecuteNonQuery();
                        SqlDataReader reader1 = cmd1.ExecuteReader();

                        if (reader1.HasRows)
                        {
                            while (reader1.Read())
                            {
                                startdate = reader1["StartDate"].ToString();
                                enddate = reader1["EndDate"].ToString();

                            }

                        }

                        string currentdate = DateTime.Now.ToString("yyyy-MM-dd");

                        string query1= "select * from Academicyear where startusforadmissionpage = 'true' and '" + currentdate+"' < '"+ startdate + "'; ";

                     
                        SqlDataAdapter adap1 = new SqlDataAdapter(query1, con);
                        DataTable dtstartdate = new DataTable();
                        adap1.Fill(dtstartdate);
                       
                        if (dtstartdate.Rows.Count > 0)
                        {
                            Session["CHECKdate"] = "startdt";
                            Response.Redirect("Admissionlabel.aspx");
                        }

                       string query2 = "select * from Academicyear where  startusforadmissionpage = 'true' and '" + enddate + "' <  '" + currentdate + "'; ";


                        SqlDataAdapter adap2 = new SqlDataAdapter(query2, con);
                        DataTable dtenddate = new DataTable();
                        adap2.Fill(dtenddate);

                        if (dtenddate.Rows.Count > 0)
                        {
                            Session["CHECKdate"] = "enddt";
                            Response.Redirect("Admissionlabel.aspx");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("AdmissionForm.checkforadmissionpage", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void submitAdmissionForm_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }
            try
            {

                int count = 0;
                String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionstring);
                con.Open();

                String query = "";
                string applicationid = "", gender = "",centuryemployee="",status="";

                applicationid = lblAdmissionid.Text;
                //confirmadmission.Text = confirmstatus;
                //pendingadmission.Text = pendingstatus;

                if (chkemployeecheeck.Checked==true)
                {
                    if (ddldepartment.Text=="" )
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter department.')", true);
                        return;
                    }
                    if (txttktno.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter ticketno.')", true);
                        return;
                    }
                }
                if (childPhoto.HasFile)
                {
                    string extension = System.IO.Path.GetExtension(childPhoto.PostedFile.FileName);
                    if ( extension != ".jpg" &&  extension != ".jpeg" && extension != ".png")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid select for childPhoto. Please Select image file only.')", true);
                        return;
                    }
                }

                if (applicationid == "0")
                {
                    query = "Select Count(*), AdmissionID From AdmissionMaster where studentname=@studName and DOB=@DOB and std=@std Group by AdmissionID;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@studName", studName.Text.ToString());
                    cmd.Parameters.AddWithValue("@DOB", DOB.Text.ToString());
                    cmd.Parameters.AddWithValue("@std", std.Text.ToString());
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader[0]);
                        //applicationid = reader[1].ToString();
                        applicationid = reader[2].ToString();
                    }

                    if (radMale.Checked)
                    {
                        gender = "M";
                    }
                    else if (radFemale.Checked)
                    {
                        gender = "F";
                    }

                    //basappa holiday
                    if (chkemployeecheeck.Checked)
                    {
                        centuryemployee = "Yes";
                    }
                    else if (chkNo.Checked)
                    {
                        centuryemployee = "No";
                    }

                    reader.Close();



                    if (count == 0)
                    {
                        AdmissionDetails _admissiondetails = new AdmissionDetails();
                        _admissiondetails.Surname = surName.Text.ToUpper();
                        _admissiondetails.StudentName = studName.Text.ToUpper();
                        _admissiondetails.FatherName = middleName.Text.ToUpper();
                        _admissiondetails.MotherfullName = txtmothername.Text.ToUpper();//basappa holiday
                        _admissiondetails.Gender = gender;
                        _admissiondetails.LastStdPassed = laststdpassed.SelectedValue.ToString();//basappa holiday
                        _admissiondetails.STD = std.SelectedValue.ToString();
                        _admissiondetails.Division = div.SelectedValue.ToString();//basappa
                        _admissiondetails.DOB = DOB.Text;
                        _admissiondetails.PlaceOfBirth = placeOfB.Text;
                        _admissiondetails.Taluka = txttaluka.Text.ToUpper();//basappa holiday
                        _admissiondetails.District = txtdistrict.Text.ToUpper();//basappa holiday
                        _admissiondetails.State = txtstate.Text.ToUpper();//basappa holiday
                        _admissiondetails.Nationality = nationality.Text;
                        _admissiondetails.MotherTounge = motherTounge.Text;
                        _admissiondetails.Religion = religion.Text;
                        _admissiondetails.Caste = txtcaste.Text.ToUpper();//basappa holiday
                        _admissiondetails.Category = txtcategory.Text.ToUpper();//basappa holiday
                        _admissiondetails.AadharNo = txtaddharno.Text.ToUpper();//basappa holiday
                        _admissiondetails.LastSchoolattend = txtlastschoolattend.Text.ToUpper();//basappa holiday
                        _admissiondetails.Sibling1_Grno = grno1.Text;
                        _admissiondetails.Sibling1_Name = siblingName1.Text.ToUpper();
                        _admissiondetails.Sibling1_Std = siblingStd1.SelectedValue.ToString();
                        _admissiondetails.Sibling2_Grno = grno2.Text;
                        _admissiondetails.Sibling2_Name = siblingName2.Text.ToUpper();
                        _admissiondetails.Sibling2_Std = siblingStd2.SelectedValue.ToString();
                        _admissiondetails.Father_Fname = fatherFirstName.Text.ToUpper();
                        _admissiondetails.Father_Surname = fatherLastName.Text.ToUpper();
                        _admissiondetails.Father_Nationality = fatherNationality.Text;
                        _admissiondetails.Father_Occupation = fatherOccupation.Text;
                        _admissiondetails.Father_Qualification = fatherQualification.Text;
                        _admissiondetails.Father_AnnualIncome = fatherIncome.Text;
                        _admissiondetails.Father_PANno = fatherPAN.Text;
                        _admissiondetails.Father_OfficeADD = fatherOfficeAddress.Text;
                        _admissiondetails.Father_Email = fatherEmailAddress.Text;
                        _admissiondetails.Father_Contact = fatherPhoneNumber.Text;
                        _admissiondetails.Father_department = ddldepartment.Text.ToUpper();//basappa holiday
                        _admissiondetails.Father_ticketno = txttktno.Text.ToUpper();//basappa holiday
                        _admissiondetails.iscenturyemployee = centuryemployee;//basappa holiday
                        _admissiondetails.Mother_Fname = motherFirstName.Text.ToUpper();
                        _admissiondetails.Mother_Surname = motherLastName.Text.ToUpper();
                        _admissiondetails.Mother_Nationality = motherNationality.Text;
                        _admissiondetails.Mother_Occupation = motherOccupation.Text;
                        _admissiondetails.Mother_Qualification = motherQualification.Text;
                        _admissiondetails.Mother_AnnualIncome = motherIncome.Text;
                        _admissiondetails.Mother_PANno = motherPAN.Text;
                        _admissiondetails.Mother_OfficeADD = motherOfficeAddress.Text;
                        _admissiondetails.Mother_Email = motherEmailAddress.Text;
                        _admissiondetails.Mother_Contact = motherPhoneNumber.Text;
                        _admissiondetails.Residential_Add = residentialAddress.Text;
                        _admissiondetails.Locality = locality.Text;
                       

                        con.Close();
                        SaveDetails(ref _admissiondetails, con);


                    }
                    else
                    {
                        //student has already filled the form
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                        successMsg.Text = "Student has already filled the form, the Application ID is " + applicationid + "";
                        clearFields();

                    }

                }
                else
                {
                    if (radMale.Checked)
                    {
                        gender = "M";
                    }
                    else if (radFemale.Checked)
                    {
                        gender = "F";
                    }
                    //basappa holiday
                    if (chkemployeecheeck.Checked)
                    {
                        centuryemployee = "Yes";
                    }
                    else if (chkNo.Checked)
                    {
                        centuryemployee = "No";
                    }

                    //basappa
                    AdmissionDetails _admissiondetails = new AdmissionDetails();
                    _admissiondetails.AdmissionID = applicationid;
                    _admissiondetails.Surname = surName.Text;
                    _admissiondetails.StudentName = studName.Text;
                    _admissiondetails.FatherName = middleName.Text;
                    _admissiondetails.MotherfullName = txtmothername.Text;//basappa holiday
                    _admissiondetails.Gender = gender;
                    _admissiondetails.LastStdPassed = laststdpassed.SelectedValue.ToString();//basappa holiday
                    _admissiondetails.STD = std.SelectedValue.ToString();
                    _admissiondetails.DOB = DOB.Text;
                    _admissiondetails.Division = div.SelectedValue.ToString();//basappa
                    _admissiondetails.PlaceOfBirth = placeOfB.Text;
                    _admissiondetails.Taluka = txttaluka.Text;//basappa holiday
                    _admissiondetails.District = txtdistrict.Text;//basappa holiday
                    _admissiondetails.State = txtstate.Text;//basappa holiday
                    _admissiondetails.Nationality = nationality.Text;
                    _admissiondetails.MotherTounge = motherTounge.Text;
                    _admissiondetails.Religion = religion.Text;
                    _admissiondetails.Caste = txtcaste.Text;//basappa holiday
                    _admissiondetails.Category = txtcategory.Text;//basappa holiday
                    _admissiondetails.AadharNo = txtaddharno.Text;//basappa holiday
                    _admissiondetails.LastSchoolattend = txtlastschoolattend.Text;//basappa holiday
                    _admissiondetails.Sibling1_Grno = grno1.Text;
                    _admissiondetails.Sibling1_Name = siblingName1.Text;
                    _admissiondetails.Sibling1_Std = siblingStd1.SelectedValue.ToString();
                    _admissiondetails.Sibling2_Grno = grno2.Text;
                    _admissiondetails.Sibling2_Name = siblingName2.Text;
                    _admissiondetails.Sibling2_Std = siblingStd2.SelectedValue.ToString();
                    _admissiondetails.Father_Fname = fatherFirstName.Text;
                    _admissiondetails.Father_Surname = fatherLastName.Text;
                    _admissiondetails.Father_Nationality = fatherNationality.Text;
                    _admissiondetails.Father_Occupation = fatherOccupation.Text;
                    _admissiondetails.Father_Qualification = fatherQualification.Text;
                    _admissiondetails.Father_AnnualIncome = fatherIncome.Text;
                    _admissiondetails.Father_PANno = fatherPAN.Text;
                    _admissiondetails.Father_OfficeADD = fatherOfficeAddress.Text;
                    _admissiondetails.Father_Email = fatherEmailAddress.Text;
                    _admissiondetails.Father_Contact = fatherPhoneNumber.Text;
                    _admissiondetails.Father_department = ddldepartment.Text;//basappa holiday
                    _admissiondetails.Father_ticketno = txttktno.Text;//basappa holiday
                    _admissiondetails.iscenturyemployee = centuryemployee;//basappa holiday
                    _admissiondetails.Mother_Fname = motherFirstName.Text;
                    _admissiondetails.Mother_Surname = motherLastName.Text;
                    _admissiondetails.Mother_Nationality = motherNationality.Text;
                    _admissiondetails.Mother_Occupation = motherOccupation.Text;
                    _admissiondetails.Mother_Qualification = motherQualification.Text;
                    _admissiondetails.Mother_AnnualIncome = motherIncome.Text;
                    _admissiondetails.Mother_PANno = motherPAN.Text;
                    _admissiondetails.Mother_OfficeADD = motherOfficeAddress.Text;
                    _admissiondetails.Mother_Email = motherEmailAddress.Text;
                    _admissiondetails.Mother_Contact = motherPhoneNumber.Text;
                    _admissiondetails.Residential_Add = residentialAddress.Text;
                    _admissiondetails.Locality = locality.Text;
                  

                    con.Close();

                    UpdateDetails(ref _admissiondetails, con);
                }

            }
            catch (Exception ex)
            {
                Log.Error("AdmissionForm.submitAdmissionForm_Click", ex);
                lblalertmessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
            }

        }

        private string SaveDetails(ref AdmissionDetails _adm, SqlConnection con)
        {
            string status = "";
            if (con.State == ConnectionState.Closed)
            {
                String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionstring);
                con = conn;
                con.Open();
            }
            try
            {
                 string getLastApplicationID = "SELECT AdmissionID FROM AdmissionMaster WHERE AdmissionID=(SELECT max(AdmissionID) FROM AdmissionMaster)";
                //string getLastApplicationID = "SELECT Sno FROM AdmissionMaster WHERE AdmissionID=(SELECT max(Sno) FROM AdmissionMaster)";
                SqlCommand cmd = new SqlCommand(getLastApplicationID, con);
                SqlDataReader reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    _adm.sno = reader[0].ToString();
                //}
                //int admID = int.Parse(_adm.sno) + 1;  //take last id from db and increment by 1
                //_adm.sno = (admID).ToString();sn
                _adm.Date = DateTime.Now.ToString();
                string studentnames = _adm.StudentName;
                string dateinmillisecond =  DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmssff");


                //reader.Close();

                string Photopath_child = "", Photopath_BirthCertificate = "", photopath_ResidentProof = "", photopath_TCProof="", photopath_OtherProof="",
                   photopath_CasteCertificate="";


                Boolean issuccess = UploadFiles(_adm.AdmissionID, studentnames, dateinmillisecond, out Photopath_BirthCertificate, out Photopath_child, out photopath_ResidentProof,out photopath_TCProof,out photopath_OtherProof, out photopath_CasteCertificate);  //save files in folder
                DateTime now = DateTime.Now;
                //string currentYearRange = now.Year.ToString() + "-" + (now.Year + 1).ToString();
               
                if (issuccess)
                {
                    _adm.Photopath_child = Photopath_child;
                    _adm.Photopath_BirthCertificate = Photopath_BirthCertificate;
                    _adm.photopath_ResidentProof = photopath_ResidentProof;
                    _adm.photopath_TCProof = photopath_TCProof;
                    _adm.photopath_OtherProof = photopath_OtherProof;
                    _adm.photopath_CasteCertificate = photopath_CasteCertificate;

                    if (confirmadmission.Checked == true)
                    {
                        status = "Approved";
                    }
                    else if (pendingadmission.Checked == true)
                    {
                        status = "Pending";
                    }

                    if (siblingStd1.Text== "Select Std")
                    {
                        _adm.Sibling1_Std = "";
                    }
                    if (siblingStd2.Text == "Select Std")
                    {
                        _adm.Sibling2_Std="";
                    }
                    //AdmissionID, @AdmissionID,
                    string query = "insert into AdmissionMaster (academicyear,surname,studentname,fathername,Gender, MotherfullName," +
                                "LastSTDpassed,std,DOB, Division,PlaceOfBirth,Taluka, District, State,Nationality,MotherTounge,Religion, " +
                                "Caste, Category, AAdharNo, LastSchoolAttend, Sibling1_grno,Sibling1_Name,Sibling1_std, " +
                                "Sibling2_grno,Sibling2_Name,Sibling2_std, " +
                                "Father_Fname,Father_surname,Father_nationality,Father_Occupation,Father_Qualification, " +
                                "Father_annualIncome,Father_PANno,Father_officeADD,Father_Email,Father_Contact,iscenturyemployee, Father_Department, Father_TicketNo," +
                                "Mother_Fname,Mother_surname,Mother_nationality,Mother_Occupation,Mother_Qualification, " +
                                "Mother_annualIncome,Mother_PANno,Mother_officeADD,Mother_Email,Mother_Contact, " +
                                "Residential_Add,Locality,photopath_child,photopath_BirthCertiFICATE,photopath_residentProof,Date,ApprovalStatus,photopath_TCProof,photopath_OtherProof,Photopath_Castecertificate) " +

                                "values(@academicyear,@Surname,@StudentName,@FatherName,@Gender,@motherfullname,@LastSTDpassed,@STD,@DOB,@division,@PlaceOfBirth, " +
                                "@Taluka,@District, @State,@Nationality,@MotherTounge,@Religion, @Caste,@Category,@AAdharNo,@LastSchoolAttend,@Sibling1_Grno,@Sibling1_Name,@Sibling1_Std,@Sibling2_Grno,@Sibling2_Name,@Sibling2_Std, " +
                                "@Father_Fname,@Father_Surname,@Father_Nationality,@Father_Occupation,@Father_Qualification,@Father_AnnualIncome, " +
                                "@Father_PANno,@Father_OfficeADD,@Father_Email,@Father_Contact,@iscenturyemployee, @Father_Department,@Father_TicketNo," +
                                "@Mother_Fname,@Mother_Surname,@Mother_Nationality,@Mother_Occupation,@Mother_Qualification,@Mother_AnnualIncome, " +
                                "@Mother_PANno,@Mother_OfficeADD,@Mother_Email,@Mother_Contact, " +
                                "@Residential_Add,@Locality,@Photopath_child,@Photopath_BirthCertificate,@photopath_ResidentProof,@Date,@ApprovalStatus,@photopath_TCProof,@photopath_OtherProof,@Photopath_Castecertificate)";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@academicyear", currentYearRange);
                    //cmd.Parameters.AddWithValue("@AdmissionID", _adm.AdmissionID);
                   
                    cmd.Parameters.AddWithValue("@Surname", _adm.Surname);
                    cmd.Parameters.AddWithValue("@StudentName", _adm.StudentName);
                    cmd.Parameters.AddWithValue("@FatherName", _adm.FatherName);
                    cmd.Parameters.AddWithValue("@Gender", _adm.Gender);
                    cmd.Parameters.AddWithValue("@motherfullname", _adm.MotherfullName);

                    cmd.Parameters.AddWithValue("@LastSTDpassed", _adm.LastStdPassed); //basappa holiday
                    

                    cmd.Parameters.AddWithValue("@STD", _adm.STD);
                    cmd.Parameters.AddWithValue("@DOB", _adm.DOB);
                    cmd.Parameters.AddWithValue("@division",_adm.Division);//basappa
                    cmd.Parameters.AddWithValue("@PlaceOfBirth", _adm.PlaceOfBirth);

                    cmd.Parameters.AddWithValue("@Taluka", _adm.Taluka); //basappa holiday
                    cmd.Parameters.AddWithValue("@District", _adm.District); //basappa holiday
                    cmd.Parameters.AddWithValue("@State", _adm.State); //basappa holiday

                    cmd.Parameters.AddWithValue("@Nationality", _adm.Nationality);
                    cmd.Parameters.AddWithValue("@MotherTounge", _adm.MotherTounge);
                    cmd.Parameters.AddWithValue("@Religion", _adm.Religion);
                   
                    cmd.Parameters.AddWithValue("@Caste", _adm.Caste); //basappa holiday
                    cmd.Parameters.AddWithValue("@Category", _adm.Category); //basappa holiday
                    cmd.Parameters.AddWithValue("@AAdharNo", _adm.AadharNo); //basappa holiday
                    cmd.Parameters.AddWithValue("@LastSchoolAttend", _adm.LastSchoolattend); //basappa holiday

                    cmd.Parameters.AddWithValue("@Sibling1_Grno", _adm.Sibling1_Grno);
                    cmd.Parameters.AddWithValue("@Sibling1_Name", _adm.Sibling1_Name);
                    cmd.Parameters.AddWithValue("@Sibling1_Std", _adm.Sibling1_Std);
                    cmd.Parameters.AddWithValue("@Sibling2_Grno", _adm.Sibling2_Grno);
                    cmd.Parameters.AddWithValue("@Sibling2_Name", _adm.Sibling2_Name);
                    cmd.Parameters.AddWithValue("@Sibling2_Std", _adm.Sibling2_Std);
                    cmd.Parameters.AddWithValue("@Father_Fname", _adm.Father_Fname);
                    cmd.Parameters.AddWithValue("@Father_Surname", _adm.Father_Surname);
                    cmd.Parameters.AddWithValue("@Father_Nationality", _adm.Father_Nationality);
                    cmd.Parameters.AddWithValue("@Father_Occupation", _adm.Father_Occupation);
                    cmd.Parameters.AddWithValue("@Father_Qualification", _adm.Father_Qualification);
                    cmd.Parameters.AddWithValue("@Father_AnnualIncome", _adm.Father_AnnualIncome);
                    cmd.Parameters.AddWithValue("@Father_PANno", _adm.Father_PANno);
                    cmd.Parameters.AddWithValue("@Father_OfficeADD", _adm.Father_OfficeADD);
                    cmd.Parameters.AddWithValue("@Father_Email", _adm.Father_Email);
                    cmd.Parameters.AddWithValue("@Father_Contact", _adm.Father_Contact);
                    
                    cmd.Parameters.AddWithValue("@Father_Department", _adm.Father_department); //basappa holiday
                    cmd.Parameters.AddWithValue("@Father_TicketNo", _adm.Father_ticketno); //basappa holiday
                    cmd.Parameters.AddWithValue("@iscenturyemployee", _adm.iscenturyemployee); //basappa holiday


                    cmd.Parameters.AddWithValue("@Mother_Fname", _adm.Mother_Fname);
                    cmd.Parameters.AddWithValue("@Mother_Surname", _adm.Mother_Surname);
                    cmd.Parameters.AddWithValue("@Mother_Nationality", _adm.Mother_Nationality);
                    cmd.Parameters.AddWithValue("@Mother_Occupation", _adm.Mother_Occupation);
                    cmd.Parameters.AddWithValue("@Mother_Qualification", _adm.Mother_Qualification);
                    cmd.Parameters.AddWithValue("@Mother_AnnualIncome", _adm.Mother_AnnualIncome);
                    cmd.Parameters.AddWithValue("@Mother_PANno", _adm.Mother_PANno);
                    cmd.Parameters.AddWithValue("@Mother_OfficeADD", _adm.Mother_OfficeADD);
                    cmd.Parameters.AddWithValue("@Mother_Email", _adm.Mother_Email);
                    cmd.Parameters.AddWithValue("@Mother_Contact", _adm.Mother_Contact);
                    cmd.Parameters.AddWithValue("@Residential_Add", _adm.Residential_Add);
                    cmd.Parameters.AddWithValue("@Locality", _adm.Locality);
                    cmd.Parameters.AddWithValue("@Photopath_child", _adm.Photopath_child);
                    cmd.Parameters.AddWithValue("@Photopath_BirthCertificate", _adm.Photopath_BirthCertificate);
                    cmd.Parameters.AddWithValue("@photopath_ResidentProof", _adm.photopath_ResidentProof);
                    cmd.Parameters.AddWithValue("@Date", _adm.Date);
                    cmd.Parameters.AddWithValue("@photopath_TCProof", _adm.photopath_TCProof);
                    cmd.Parameters.AddWithValue("@photopath_OtherProof", _adm.photopath_OtherProof);
                    cmd.Parameters.AddWithValue("@Photopath_Castecertificate", _adm.photopath_CasteCertificate); //basappa holiday
                    cmd.Parameters.AddWithValue("@ApprovalStatus", status);

                    cmd.ExecuteNonQuery();


                    //string appid = "", studname = "";

                    string appid = "", studname = "", studentname="", msg="",  std="", phoneno="", fatheremail="", year="";
                    // query = "Select AdmissionID,studentname, surname From AdmissionMaster where Date='" + _adm.Date + "' and studentname='" + _adm.StudentName + "' and dob='" + _adm.DOB + "';";
                    // query = "Select AdmissionID,studentname, Surname,  FatherName,  surname, date,std, Father_Contact From AdmissionMaster where Date='" + _adm.Date + "' and studentname='" + _adm.StudentName + "' and dob='" + _adm.DOB + "';";
                    query = "Select AdmissionID,studentname, Surname,  FatherName,  surname, date,std, Father_Contact,Father_Email,AcademicYear From AdmissionMaster where Date=@date and studentname=@studName and dob=@DOB;";
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    cmd1.Parameters.AddWithValue("@studName", _adm.StudentName);
                    cmd1.Parameters.AddWithValue("@DOB", _adm.DOB);
                    cmd1.Parameters.AddWithValue("@date", _adm.Date);
                    cmd1.ExecuteNonQuery();
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    //cmd = new SqlCommand(query, con);
                    //reader = cmd.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {
                            appid = reader1[0].ToString();
                            studname = reader1[1].ToString();
                            //date = reader[7].ToString();
                            //studentname = reader[4].ToString();
                            std= reader1[6].ToString();
                            phoneno= reader1[7].ToString();
                            fatheremail = reader1[8].ToString();
                            year= reader1[9].ToString();
                           
                        }
                        //sedemailnew(_adm.Father_Email, _adm.Mother_Email, appid, studname, _adm.Date);
                        //successMsg.Text = "Dear Parent your admission form with application no:- {#var#} Dated:- {#var#} for STD: {#var#} is submitted for your child {#var#} Century Rayon High School , Regards CENRES"
                        string userid="", password="", senderid = "", entityid = "", templateid = "1707170962400551021";
                        query = "select smsuser, smspassword, senderid,entityid from smssettings where activesms='true'";

                        cmd = new SqlCommand(query, con);
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                userid = reader[0].ToString();
                                password = reader[1].ToString();
                                senderid = reader[2].ToString();
                                entityid = reader[3].ToString();
                            }
                        }
                            msg = "Dear Parent your admission form with application no:- " + appid + " Dated:- " + _adm.Date + " for STD: " + std + " is submitted for your child " + studname + ", Century Rayon High School, Regards "+ senderid + "";
                       // string userid= "scanidbiz", password = "oliq5896OL", senderid = "CENRES ", entityid = "1701169167163824773", templateid = "1707170962400551021";

                        sendNimbusBizSMSNew(userid, password, senderid, phoneno, msg, entityid    , templateid); //basappa
                       
                        /////////Send Mail////////////
                       // string studentName = "",  toAddress2 = "", year = "", useremail = "",query2="";
                       // query2 = "Select studentname, Surname, Father_Email,Mother_Email,AcademicYear from AdmissionMaster where AdmissionID='"+ appid + "'";
                       // SqlCommand cmd2 = new SqlCommand(query2, con);
                       // cmd2 = new SqlCommand(query2, con);
                       //// cmd2.Parameters.AddWithValue("@AdmissionID", appid);
                       // cmd2.ExecuteNonQuery();

                       // SqlDataReader reader2= cmd2.ExecuteReader();
                       // if (reader2.HasRows)
                       // {
                       //     while (reader2.Read())
                       //     {
                       //         studentName = reader2[0].ToString() + " " + reader[1].ToString();
                       //         toAddress2 = reader2[3].ToString();
                       //         useremail = reader2[2].ToString();
                       //         year = reader2[4].ToString();
                       //     }
                       // }

                        string emailsubject = "Successful submission of Admission Form for " + studname + "  Year " + year + " ";
                        string emailbody = " <h3>Dear Parent,</h3>Your admission form with application no:- " + appid + " Dated:- " + _adm.Date + " for STD: " + std + " is submitted for your child " + studname + ", Century Rayon High School. </h3><br/> Regards, <br/><h3>Century Rayon High School, Shahad<h3/> ";
                                 
                        SendMailModule.SendEmail.sendEmailToClient(fatheremail, emailbody, emailsubject);
                        reader.Close();
                        reader1.Close();
                        //////Send Mailend////////

                        con.Close();
                        clearFields();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                       // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal(); setTimeout (function(){window.location.href='AdmissionForm.aspx';},5000);", true);

                        successMsg.Text = "Admission form is successfully saved. <br> Your Application ID is - " + appid;

                        return appid;
                    }
                    else
                    {
                        //could not save data
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                        successMsg.Text = "Couldn't save the data, please try again.";

                        con.Close();
                        return "0";
                    }


                }
                else
                {
                    return "0";
                }


            }
            catch (Exception ex)
            {
                con.Close();
                return "0";
                Log.Error("AdmissionForm.submitAdmissionForm_Click", ex);
                lblalertmessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
            }
        }

        //public AdmissionDetails UploadFiles(AdmissionDetails _adm)
        //{
        //    string path = Server.MapPath("~/Resources/Documents/");
        //    if (birthCertificate.HasFile)
        //    {
        //        string extension = System.IO.Path.GetExtension(birthCertificate.PostedFile.FileName);
        //        string filePath = Server.MapPath("Resources//Documents//" + _adm.AdmissionID + "_BirthCertificate" + extension);
        //        birthCertificate.SaveAs(filePath);
        //        _adm.Photopath_BirthCertificate = "Resources//Documents//" + _adm.AdmissionID + "_BirthCertificate" + extension;
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        //        successMsg.Text = "No file selected for - Birth Certificate";
        //    }

        //    if (childPhoto.HasFile)
        //    {
        //        string extension = System.IO.Path.GetExtension(childPhoto.PostedFile.FileName);
        //        string filePath = Server.MapPath("Resources//Documents//" + _adm.AdmissionID + "_ChildPhoto" + extension);
        //        childPhoto.SaveAs(filePath);
        //        _adm.Photopath_child = "Resources//Documents//" + _adm.AdmissionID + "_ChildPhoto" + extension;
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        //        successMsg.Text = "No file selected for - Child Photo";

        //    }

        //    if (residentialProof.HasFile)
        //    {
        //        string extension = System.IO.Path.GetExtension(residentialProof.PostedFile.FileName);
        //        string filePath = Server.MapPath("Resources//Documents//" + _adm.AdmissionID + "_ResidentialProof" + extension);
        //        residentialProof.SaveAs(filePath);
        //        _adm.photopath_ResidentProof = "Resources//Documents//" + _adm.AdmissionID + "_ResidentialProof" + extension;

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        //        successMsg.Text = "No file selected for - Residential Proof";

        //    }

        //    return _adm;
        //}

        private string UpdateDetails(ref AdmissionDetails _adm, SqlConnection con)
        {
            if (con.State == ConnectionState.Closed)
            {
                String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionstring);
                con = conn;
                con.Open();
                
            }
            try
            {

                string Photopath_child = "", Photopath_BirthCertificate = "", photopath_ResidentProof = "", photopath_OtherProof="", photopath_TCProof="", photopath_CasteCertificate="";
                string studentnames = _adm.StudentName;
                string dateinmillisecond = DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmssff");

                //Boolean issuccess = UpdateUploadFiles(_adm.AdmissionID, out Photopath_BirthCertificate, out Photopath_child, out photopath_ResidentProof,out photopath_TCProof, out photopath_OtherProof);  //save files in folder

                Boolean issuccess = UpdateUploadFiles(_adm.AdmissionID, studentnames, dateinmillisecond, out Photopath_BirthCertificate, out Photopath_child, out photopath_ResidentProof, out photopath_TCProof, out photopath_OtherProof, out photopath_CasteCertificate);

                if (issuccess)
                {
                    SqlCommand cmd = null;
                    _adm.Photopath_child = Photopath_child;
                    _adm.Photopath_BirthCertificate = Photopath_BirthCertificate;
                    _adm.photopath_ResidentProof = photopath_ResidentProof;
                    _adm.photopath_TCProof = photopath_TCProof;
                    _adm.photopath_OtherProof = photopath_OtherProof;
                    _adm.photopath_CasteCertificate = photopath_CasteCertificate;
                    _adm.updatedate = DateTime.Now.ToString();

                    string query = "update AdmissionMaster set Surname=@Surname,StudentName=@StudentName,FatherName=@FatherName,MotherfullName=@MotherfullName," +
                        "Gender=@Gender,LastSTDpassed=@LastSTDpassed,std=@std,dob=@dob,PlaceOfBirth=@PlaceOfBirth,Taluka=@Taluka,District=@District, State=@State,Nationality=@Nationality,MotherTounge=@MotherTounge," +
                        "Religion=@Religion,Caste=@Caste, Category=@Category, AAdharNo=@AAdharNo, LastSchoolAttend=@LastSchoolAttend,sibling1_grno=@sibling1_grno,Sibling1_Name=@Sibling1_Name,sibling1_std=@sibling1_std,Sibling2_Grno=@Sibling2_Grno," +   
                        "Sibling2_Name=@Sibling2_Name,Sibling2_Std=@Sibling2_Std,Father_Fname=@Father_Fname,Father_Surname=@Father_Surname," +
                        "Father_Nationality=@Father_Nationality,Father_Occupation=@Father_Occupation,Father_Qualification=@Father_Qualification,Father_AnnualIncome=@Father_AnnualIncome," +
                        "Father_PANno=@Father_PANno,Father_OfficeADD=@Father_OfficeADD,Father_Email=@Father_Email,Father_Contact=@Father_Contact,iscenturyemployee=@iscenturyemployee,Father_Department=@Father_Department, " +
                        "Father_TicketNo= @Father_TicketNo,Mother_Fname=@Mother_Fname,Mother_Surname=@Mother_Surname,Mother_Nationality=@Mother_Nationality,Mother_Occupation=@Mother_Occupation," +
                        "Mother_Qualification=@Mother_Qualification,Mother_AnnualIncome=@Mother_AnnualIncome,Mother_PANno=@Mother_PANno,Mother_OfficeADD=@Mother_OfficeADD,Mother_Email=@Mother_Email," +
                        "Mother_Contact=@Mother_Contact,Residential_Add=@Residential_Add,Locality=@Locality,Photopath_child=@Photopath_child,Photopath_BirthCertificate=@Photopath_BirthCertificate,photopath_ResidentProof=@photopath_ResidentProof,photopath_TCProof=@photopath_TCProof,photopath_OtherProof=@photopath_OtherProof,Photopath_Castecertificate=@Photopath_Castecertificate,Division=@division,updatedate=@updatedate " + 
                                   "where admissionid=@AdmissionID;";



                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@AdmissionID", _adm.AdmissionID);
                    cmd.Parameters.AddWithValue("@Surname", _adm.Surname);
                    cmd.Parameters.AddWithValue("@StudentName", _adm.StudentName);
                    cmd.Parameters.AddWithValue("@FatherName", _adm.FatherName);
                    cmd.Parameters.AddWithValue("@MotherfullName", _adm.MotherfullName); //basappa holiday
                    cmd.Parameters.AddWithValue("@Gender", _adm.Gender);

                    cmd.Parameters.AddWithValue("@LastSTDpassed", _adm.LastStdPassed); //basappa holiday
                    cmd.Parameters.AddWithValue("@STD", _adm.STD);
                    cmd.Parameters.AddWithValue("@DOB", _adm.DOB);
                    cmd.Parameters.AddWithValue("@division", _adm.Division);//basappa
                    cmd.Parameters.AddWithValue("@PlaceOfBirth", _adm.PlaceOfBirth);

                    cmd.Parameters.AddWithValue("@Taluka", _adm.Taluka); //basappa holiday
                    cmd.Parameters.AddWithValue("@District", _adm.District); //basappa holiday
                    cmd.Parameters.AddWithValue("@State", _adm.State); //basappa holiday
                    cmd.Parameters.AddWithValue("@Nationality", _adm.Nationality);
                    cmd.Parameters.AddWithValue("@MotherTounge", _adm.MotherTounge);
                    cmd.Parameters.AddWithValue("@Religion", _adm.Religion);

                    cmd.Parameters.AddWithValue("@Caste", _adm.Caste); //basappa holiday
                    cmd.Parameters.AddWithValue("@Category", _adm.Category); //basappa holiday
                    cmd.Parameters.AddWithValue("@AAdharNo", _adm.AadharNo); //basappa holiday
                    cmd.Parameters.AddWithValue("@LastSchoolAttend", _adm.LastSchoolattend); //basappa holiday

                    cmd.Parameters.AddWithValue("@Sibling1_Grno", _adm.Sibling1_Grno);
                    cmd.Parameters.AddWithValue("@Sibling1_Name", _adm.Sibling1_Name);
                    cmd.Parameters.AddWithValue("@Sibling1_Std", _adm.Sibling1_Std);
                    cmd.Parameters.AddWithValue("@Sibling2_Grno", _adm.Sibling2_Grno);
                    cmd.Parameters.AddWithValue("@Sibling2_Name", _adm.Sibling2_Name);
                    cmd.Parameters.AddWithValue("@Sibling2_Std", _adm.Sibling2_Std);
                    cmd.Parameters.AddWithValue("@Father_Fname", _adm.Father_Fname);
                    cmd.Parameters.AddWithValue("@Father_Surname", _adm.Father_Surname);
                    cmd.Parameters.AddWithValue("@Father_Nationality", _adm.Father_Nationality);
                    cmd.Parameters.AddWithValue("@Father_Occupation", _adm.Father_Occupation);
                    cmd.Parameters.AddWithValue("@Father_Qualification", _adm.Father_Qualification);
                    cmd.Parameters.AddWithValue("@Father_AnnualIncome", _adm.Father_AnnualIncome);
                    cmd.Parameters.AddWithValue("@Father_PANno", _adm.Father_PANno);
                    cmd.Parameters.AddWithValue("@Father_OfficeADD", _adm.Father_OfficeADD);
                    cmd.Parameters.AddWithValue("@Father_Email", _adm.Father_Email);
                    cmd.Parameters.AddWithValue("@Father_Contact", _adm.Father_Contact);
                    
                    cmd.Parameters.AddWithValue("@iscenturyemployee", _adm.iscenturyemployee); //basappa holiday
                    cmd.Parameters.AddWithValue("@Father_Department", _adm.Father_department); //basappa holiday
                    cmd.Parameters.AddWithValue("@Father_TicketNo", _adm.Father_ticketno); //basappa holiday
                    cmd.Parameters.AddWithValue("@Mother_Fname", _adm.Mother_Fname);
                    cmd.Parameters.AddWithValue("@Mother_Surname", _adm.Mother_Surname);
                    cmd.Parameters.AddWithValue("@Mother_Nationality", _adm.Mother_Nationality);
                    cmd.Parameters.AddWithValue("@Mother_Occupation", _adm.Mother_Occupation);
                    cmd.Parameters.AddWithValue("@Mother_Qualification", _adm.Mother_Qualification);
                    cmd.Parameters.AddWithValue("@Mother_AnnualIncome", _adm.Mother_AnnualIncome);
                    cmd.Parameters.AddWithValue("@Mother_PANno", _adm.Mother_PANno);
                    cmd.Parameters.AddWithValue("@Mother_OfficeADD", _adm.Mother_OfficeADD);
                    cmd.Parameters.AddWithValue("@Mother_Email", _adm.Mother_Email);
                    cmd.Parameters.AddWithValue("@Mother_Contact", _adm.Mother_Contact);
                    cmd.Parameters.AddWithValue("@Residential_Add", _adm.Residential_Add);
                    cmd.Parameters.AddWithValue("@Locality", _adm.Locality);
                    cmd.Parameters.AddWithValue("@Photopath_child", _adm.Photopath_child);
                    cmd.Parameters.AddWithValue("@Photopath_BirthCertificate", _adm.Photopath_BirthCertificate);
                    cmd.Parameters.AddWithValue("@photopath_ResidentProof", _adm.photopath_ResidentProof);
                    cmd.Parameters.AddWithValue("@photopath_TCProof", _adm.photopath_TCProof);
                    cmd.Parameters.AddWithValue("@photopath_OtherProof", _adm.photopath_OtherProof);

                    cmd.Parameters.AddWithValue("@Photopath_Castecertificate", _adm.photopath_CasteCertificate); //basappa holiday
                    cmd.Parameters.AddWithValue("@updatedate", _adm.updatedate);

                    cmd.ExecuteNonQuery();
                 
                    clearFields();/*basappa*/
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal(); setTimeout (function(){window.location.href='AdmissionForm.aspx';},5000);", true);
                    successMsg.Text = "Admission form id :" + _adm.AdmissionID + " updated successfully";

                   
                    return _adm.AdmissionID;
                    


                }
                else
                {
                    return "0";
                }


            }
            catch (Exception ex)
            {
                con.Close();
                return "0";
                Log.Error("AdmissionForm", ex);
                lblalertmessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
            }
        }


        public Boolean UploadFiles(string AdmissionID, string studentnames, string dateinmillisecond, out string birthcertificatefile, out string childphotofile, out string residentialprooffile,out string photopath_TCProof,out string photopath_OtherProof, out string photopath_CasteCertificate)
        {
            birthcertificatefile = ""; childphotofile = ""; residentialprooffile = ""; photopath_TCProof = ""; photopath_OtherProof = ""; photopath_CasteCertificate="";

            Boolean isSuccessfull = true;
            //string simplifiedName = RemoveSpecialCharacters(childPhoto);

            studentnames = Regex.Replace(studentnames, "[^a-zA-Z0-9]+", "") ;

            string path = Server.MapPath("~/Resources/Documents/");
            if (birthCertificate.HasFile)
            {
                string extension = System.IO.Path.GetExtension(birthCertificate.PostedFile.FileName);
              
                string filePath = Server.MapPath("Resources//Documents//"+ studentnames +"_BirthCertificate_"+ dateinmillisecond + extension);
                birthCertificate.SaveAs(filePath);
                birthcertificatefile = "Resources//Documents//" + studentnames +"_BirthCertificate_" + dateinmillisecond + extension;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                successMsg.Text = "No file selected for - Birth Certificate";
                isSuccessfull = false;
            }

            if (childPhoto.HasFile)
            {
                string extension = System.IO.Path.GetExtension(childPhoto.PostedFile.FileName);
               
                string filePath = Server.MapPath("Resources//Documents//"  + studentnames + "_childPhoto_" + dateinmillisecond + extension);
                childPhoto.SaveAs(filePath);
                childphotofile = "Resources//Documents//" + studentnames + "_childPhoto_" + dateinmillisecond + extension;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                successMsg.Text = "No file selected for - Child Photo";
                isSuccessfull = false;

            }
            if (residentialProof.HasFile)
            {
                string extension = System.IO.Path.GetExtension(residentialProof.PostedFile.FileName);
                
                string filePath = Server.MapPath("Resources//Documents//" + studentnames + "_ResidentialProof_" + dateinmillisecond + extension);
                residentialProof.SaveAs(filePath);
                residentialprooffile = "Resources//Documents//" + studentnames + "_ResidentialProof_" + dateinmillisecond + extension;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                successMsg.Text = "No file selected for - Residential Proof";
                isSuccessfull = false;

            }
            if (tcproff.HasFile)
            {
                string extension = System.IO.Path.GetExtension(tcproff.PostedFile.FileName);
                
                string filePath = Server.MapPath("Resources//Documents//" + studentnames + "_TCProof_" + dateinmillisecond + extension);
                tcproff.SaveAs(filePath);
                photopath_TCProof = "Resources//Documents//" + studentnames + "_TCProof_" + dateinmillisecond + extension;

            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //    successMsg.Text = "No file selected for - Transfer Proof";
            //    isSuccessfull = false;
                   
            //}
            if (otherfile.HasFile)
            {
                string extension = System.IO.Path.GetExtension(otherfile.PostedFile.FileName);
                string filePath = Server.MapPath("Resources//Documents//" + studentnames + "_OtherProof_" + dateinmillisecond + extension);
                otherfile.SaveAs(filePath);
                photopath_OtherProof = "Resources//Documents//" + studentnames + "_OtherProof_" + dateinmillisecond + extension;

            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //    successMsg.Text = "No file selected for - Other Proof";
            //    isSuccessfull = false;

            //}

            if (castefile.HasFile)
            {
                string extension = System.IO.Path.GetExtension(castefile.PostedFile.FileName);
                string filePath = Server.MapPath("Resources//Documents//" + studentnames + "_castecertificate_" + dateinmillisecond + extension);
                castefile.SaveAs(filePath);
                photopath_CasteCertificate = "Resources//Documents//" + studentnames + "_castecertificate_" + dateinmillisecond + extension;

            }
            return isSuccessfull;
        }

        public Boolean UpdateUploadFiles(string AdmissionID,string studentnames,  string dateinmillisecond, out string birthcertificatefile, out string childphotofile, out string residentialprooffile, out string photopath_TCProof, out string photopath_OtherProof,  out string photopath_CasteCertificate)
        {

            birthcertificatefile = ""; childphotofile = ""; residentialprooffile = ""; photopath_TCProof = ""; photopath_OtherProof = ""; photopath_CasteCertificate = "";
            studentnames = Regex.Replace(studentnames, "[^a-zA-Z0-9]+", "");
            Boolean isSuccessfull = true;

            string path = Server.MapPath("~/Resources/Documents/");
            if (birthCertificate.HasFile)
            {
                string extension = System.IO.Path.GetExtension(birthCertificate.PostedFile.FileName);
                string filePath = Server.MapPath("Resources//Documents//" + studentnames+ "_BirthCertificate_" + dateinmillisecond + extension);
                birthCertificate.SaveAs(filePath);
                birthcertificatefile = "Resources//Documents//" + studentnames + "_BirthCertificate_" + dateinmillisecond + extension;
            }
            else
            {

                if (lblbirthcertificate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    successMsg.Text = "No file selected for - Birth Certificate";
                    isSuccessfull = false;

                }
                else
                {
                    birthcertificatefile = lblbirthcertificate.Text;
                }

               

            }

            if (childPhoto.HasFile)
            {
                string extension = System.IO.Path.GetExtension(childPhoto.PostedFile.FileName);
                string filePath = Server.MapPath("Resources//Documents//" + studentnames + "_childPhoto_" + dateinmillisecond + extension);
                childPhoto.SaveAs(filePath);
                childphotofile = "Resources//Documents//" + studentnames + "_childPhoto_" + dateinmillisecond + extension;
            }
            else
            {
                //childphotofile = lblphotopath.Text;
                if (lblphotopath.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    successMsg.Text = "No file selected for - Photo Path";
                    isSuccessfull = false;

                }
                else
                {
                    childphotofile = lblphotopath.Text;
                }
               
            }

            if (residentialProof.HasFile)
            {
                string extension = System.IO.Path.GetExtension(residentialProof.PostedFile.FileName);
                string filePath = Server.MapPath("Resources//Documents//" + studentnames + "_ResidentialProof_" + dateinmillisecond + extension);
                residentialProof.SaveAs(filePath);
                residentialprooffile = "Resources//Documents//" + studentnames + "_ResidentialProof_" + dateinmillisecond + extension;

            }
            else
            {

                if (lblresidentialpath.Text=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    successMsg.Text = "No file selected for - Residential Proof";
                    isSuccessfull = false;
                   
                }
                else
                {
                    residentialprooffile = lblresidentialpath.Text;
                }
                
            }
            if (tcproff.HasFile)
            {
                string extension = System.IO.Path.GetExtension(tcproff.PostedFile.FileName);
                string filePath = Server.MapPath("Resources//Documents//" + studentnames + "_TCProof_" + dateinmillisecond + extension);
                tcproff.SaveAs(filePath);
                photopath_TCProof = "Resources//Documents//" + studentnames + "_TCProof_" + dateinmillisecond + extension;

            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                //successMsg.Text = "No file selected for - Transfer Proof";
                //isSuccessfull = false;
               // photopath_TCProof = lbltcpath.Text;

                if (lbltcpath.Text == "")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    //successMsg.Text = "No file selected for - Transfer Certificate";
                    //isSuccessfull = false;

                }
                else
                {
                    photopath_TCProof = lbltcpath.Text;
                }

            }
            if (otherfile.HasFile)
            {
                string extension = System.IO.Path.GetExtension(otherfile.PostedFile.FileName);
                string filePath = Server.MapPath("Resources//Documents//" + studentnames + "_OtherProof_" + dateinmillisecond + extension);
                otherfile.SaveAs(filePath);
                photopath_OtherProof = "Resources//Documents//" + studentnames + "_OtherProof_" + dateinmillisecond + extension;

            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                //successMsg.Text = "No file selected for - Other Proof";
                //isSuccessfull = false;
               // photopath_OtherProof = otherpf.Text;

                if (otherpf.Text == "")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    //successMsg.Text = "No file selected for - Other Proof";
                    //isSuccessfull = false;

                }
                else
                {
                    photopath_OtherProof = otherpf.Text;
                }

            }

            //basappa holiday
            if (castefile.HasFile)
            {
                string extension = System.IO.Path.GetExtension(castefile.PostedFile.FileName);
                string filePath = Server.MapPath("Resources//Documents//" + studentnames + "_castcertificate_" + dateinmillisecond + extension);
                castefile.SaveAs(filePath);
                photopath_OtherProof = "Resources//Documents//" + studentnames + "_castcertificate_" + dateinmillisecond + extension;

            }
            else
            {
               

                if (ccerticate.Text == "")
                {
                   
                }
                else
                {
                    photopath_CasteCertificate = ccerticate.Text;
                }

            }

            return isSuccessfull;
        }


        public void SendEmail(string toAddress1, string toAddress2, string ApplicationID, string studName, string date)
        {
            try
            {
                string htmltext = "<p>Dear Parent your admission form application no:- " + ApplicationID + "  Dated:- " + date + " is submitted successfully.</p>" +
                                 "<p> You will be updated on the status of the same in case the admission is allotted to your child.</p>" +
                                "<p> If in case you do not receive any communication vide this application form kindly consider it as & ldquo; Admission Not Alloted & rdquo;.</p>" +
                                "<p> Any attempt of Direct communication to the school authorities shall by default invalidate the admission.</p>" +
                                "<p> School reserves the right to cancel the allotted admission under unavoidable and special circumstances.</p>" +
                                "<p>You can also track your application status via following link: http://192.168.0.116/admissionmodule/index.aspx </p>";


                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                using (MailMessage mm = new MailMessage(smtpSection.From, toAddress1.Trim()))
                {
                    mm.Subject = "Application Submitted - " + ApplicationID;
                    //mm.Body = "Dear Parent your application form for the child Name- " + studName + " is submitted with ApplicationID- " + ApplicationID + " Please contact school office during school working hours along with the neccessary documents.";
                    mm.Body = htmltext;
                    mm.IsBodyHtml = true;
                    mm.CC.Add(toAddress2.Trim());
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = smtpSection.Network.Host;
                    smtp.EnableSsl = smtpSection.Network.EnableSsl;
                    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                    smtp.Credentials = networkCred;
                    smtp.Port = smtpSection.Network.Port;
                    smtp.Send(mm);
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Email sent.');", true);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void sedemailnew(string toAddress1, string toAddress2, string ApplicationID, string studName, string date)
        {
            try
            {
                string htmltext = "<p>Dear Parent your admission form application no:- " + ApplicationID + "  Dated:- " + date + " is submitted successfully.</p>" +
                                "<p> You will be updated on the status of the same in case the admission is allotted to your child.</p>" +
                               "<p> If in case you do not receive any communication vide this application form kindly consider it as & ldquo; Admission Not Alloted & rdquo;.</p>" +
                               "<p> Any attempt of Direct communication to the school authorities shall by default invalidate the admission.</p>" +
                               "<p> School reserves the right to cancel the allotted admission under unavoidable and special circumstances.</p>" +
                               "<p>You can also track your application status via following link: http://192.168.0.116/admissionmodule/index.aspx </p>";


                MailMessage mailmessage = new MailMessage("info@scanidsystems.com", "jaypandyascanid@gmail.com");
                mailmessage.Subject = "Application Submitted - " + ApplicationID;
                mailmessage.Body = htmltext; //your mail body

                SmtpClient smtpclient = new SmtpClient("smtp.gmail.com", 587);
                smtpclient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "info@scanidsystems.com",
                    Password = "Inf@4682#"
                };
                smtpclient.EnableSsl = true;
                smtpclient.Send(mailmessage);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public static string sendPinacleSMS(string sender, string phno, string msg, string dlttempid)
        {
            try
            {
                string customContent = "{\r\n    \"sender\": \"" + sender + "\",\r\n    \"message\": [\r\n        {\r\n            \"number\": \"" + phno + "\",\r\n            \"text\": \"" + msg + "\"\r\n        }\r\n    ],\r\n    \"messagetype\": \"TXT\",\r\n    \"dlttempid\": \"" + dlttempid + "\"\r\n}";
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.pinnacle.in/index.php/sms/json");
                request.Headers.Add("apikey", "63a616-6e94f4-e523d7-0d5248-c93edf");
                var content = new StringContent(customContent, null, "application/json");
                request.Content = content;


                //to create ssl/tls secure channel
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;

                using (HttpResponseMessage response = client.SendAsync(request).GetAwaiter().GetResult())
                {
                    using (HttpContent content2 = response.Content)
                    {

                        var json = content2.ReadAsStringAsync().GetAwaiter().GetResult();
                        System.Web.Script.Serialization.JavaScriptSerializer _js = new System.Web.Script.Serialization.JavaScriptSerializer();
                        dynamic res = _js.DeserializeObject(json);
                        return res["status"];
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }//09_Aug_23


        public static void sendNimbusBizSMSNew(string userid, string password, string senderid, string phoneno, string msg, string entityid, string templateid)
        {
            Task.Run(() =>
            {
                Task<string> resp = sendNimbusBizSMS(userid, password, senderid, phoneno, msg, entityid, templateid);
                //return resp.Result;

            });
           
        }

        private async static Task<string> sendNimbusBizSMS(string userid, string password, string senderid, string phoneno, string msg, string entityid, string templateid)
        {
            try
            {
                string urlformat = String.Format("http://nimbusit.biz/api/SmsApi/SendBulkApi?UserID={0}&Password={1}&SenderID={2}&Phno={3}&Msg={4}&EntityID={5}&TemplateID={6}&FlashMsg=0",
                                                    userid,
                                                    password,
                                                    senderid,
                                                    phoneno,
                                                    msg,
                                                    entityid,
                                                    templateid);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, urlformat);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string response1 = await response.Content.ReadAsStringAsync();

                dynamic res = JsonConvert.DeserializeObject(response1);

                if (res.Status == "OK")
                    return "Message Success";
                else
                    return response1;

            }
            catch (Exception ex)
            {
                Log.Error("SmsSendClass.sendNimbusBizSMS", ex);
                return ex.Message;
            }
        }



        public void clearFields()
        {
            surName.Text = "";
            studName.Text = "";
            middleName.Text = "";
            txtmothername.Text = "";//basappa hoiday
            laststdpassed.SelectedValue = "Select Std";//basappa hoiday
            std.SelectedValue = "Select Std";
            DOB.Text = "";
            div.SelectedValue = "0";//basappa
            placeOfB.Text = "";
            txttaluka.Text = "";//basappa hoiday
            txtdistrict.Text = "";//basappa hoiday
            txtstate.Text = "";//basappa hoiday
            nationality.Text = "";
            motherTounge.Text = "";
            religion.SelectedValue = "Select Religion";

            txtcaste.Text = "";//basappa hoiday
            txtcategory.Text = "";//basappa hoiday
            txtaddharno.Text = "";//basappa hoiday
            txtlastschoolattend.Text = "";//basappa hoiday
            grno1.Text = "";
            siblingName1.Text = "";
            siblingStd1.SelectedValue = "Select Std";
            grno2.Text = "";
            siblingName2.Text = "";
            siblingStd2.SelectedValue = "Select Std";
            fatherFirstName.Text = "";
            fatherLastName.Text = "";
            fatherNationality.Text = "";
            fatherOccupation.Text = "";
            fatherQualification.Text = "";
            fatherIncome.Text = "";
            fatherPAN.Text = "";
            fatherOfficeAddress.Text = "";
            fatherEmailAddress.Text = "";
            ddldepartment.Text = "";//basappa hoiday
            txttktno.Text = "";//basappa hoiday
            
            fatherPhoneNumber.Text = "";
            motherFirstName.Text = "";
            motherLastName.Text = "";
            motherNationality.Text = "";
            motherOccupation.Text = "";
            motherQualification.Text = "";
            motherIncome.Text = "";
            motherPAN.Text = "";
            motherOfficeAddress.Text = "";
            motherEmailAddress.Text = "";
            motherPhoneNumber.Text = "";
            residentialAddress.Text = "";
            locality.Text = "";

            lblphotopath.Text = "";//basappa
            lblbirthcertificate.Text = "";//basappa
            lblresidentialpath.Text = "";//basappa
            lbltcpath.Text = "";//basappa
            otherpf.Text = "";//basappa

            ccerticate.Text = "";//basappa hoiday
            imgPhoto.ImageUrl = "";
        }

        protected void resetAdmissionFrom_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdmissionForm.aspx");
        }

        public void GetDetails(string AdmissionID)
        {
            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();

            if (AdmissionID != "0" || AdmissionID != "")
            {
                otherproof.Visible = true;
                transfercert.Visible = true;
                A1.Visible = true;
                BirthCert.Visible = true;
                Castefiles.Visible = true;
            }

            string query = "";
            //basappa holiday
            query = "select academicyear,admissionid,Surname,StudentName,FatherName,MotherfullName,Gender,LastSTDpassed,std,dob,PlaceOfBirth,Taluka, District, State,Nationality,MotherTounge,otp,Religion,Caste, Category, AAdharNo, LastSchoolAttend,sibling1_grno,Sibling1_Name,sibling1_std,Sibling2_Grno,Sibling2_Name,Sibling2_Std,Father_Fname,Father_Surname,Father_Nationality,Father_Occupation,Father_Qualification,Father_AnnualIncome,Father_PANno,Father_OfficeADD,Father_Email,Father_Contact,iscenturyemployee,Father_Department, Father_TicketNo,Mother_Fname,Mother_Surname,Mother_Nationality,Mother_Occupation,Mother_Qualification,Mother_AnnualIncome,Mother_PANno,Mother_OfficeADD,Mother_Email,Mother_Contact,Residential_Add,Locality,Photopath_child,Photopath_BirthCertificate,photopath_ResidentProof, photopath_TCProof, photopath_OtherProof,[Date],ApprovalStatus,Photopath_Castecertificate,Division " + //photopath_TCProof, photopath_OtherProof, basappa
                    "from AdmissionMaster " +
                    "where admissionid=@AdmissionID;";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@AdmissionID", AdmissionID);
            cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())  //basappa holiday
                {
                    studName.Text = reader["StudentName"].ToString();
                    middleName.Text = reader["FatherName"].ToString();
                    surName.Text = reader["Surname"].ToString();
                    txtmothername.Text = reader["MotherfullName"].ToString(); //basappa holiday

                    if (reader["Gender"].ToString().Equals("M"))
                    {
                        radMale.Checked = true;
                    }
                    else
                    {
                        radFemale.Checked = true;
                    }

                    //basappa holiday
                    if (reader["iscenturyemployee"].ToString().Equals("Yes"))
                    {
                        chkNo.Checked = false;
                        chkemployeecheeck.Checked = true;
                        pnlemployeepanel.Style["display"] = "block";
                        pnlemployeepanel.Visible = true;
                    }
                    else
                    {
                        chkemployeecheeck.Checked = false;
                        chkNo.Checked = true;
                    }

                    laststdpassed.Text = reader["LastSTDpassed"].ToString();//basappa holiday
                    std.SelectedValue = reader["std"].ToString();
                    DOB.Text = reader["dob"].ToString();
                    placeOfB.Text = reader["PlaceOfBirth"].ToString();

                    txttaluka.Text = reader["Taluka"].ToString();//basappa holiday
                    txtdistrict.Text = reader["District"].ToString();//basappa holiday
                    txtstate.Text = reader["State"].ToString();//basappa holiday
                    nationality.Text = reader["Nationality"].ToString();
                    motherTounge.Text = reader["MotherTounge"].ToString();
                    religion.SelectedValue = reader["Religion"].ToString();

                    txtcaste.Text = reader["Caste"].ToString();//basappa holiday
                    txtcategory.Text = reader["Category"].ToString();//basappa holiday
                    txtaddharno.Text = reader["AAdharNo"].ToString();//basappa holiday
                    txtlastschoolattend.Text = reader["LastSchoolAttend"].ToString();//basappa holiday
                    div.SelectedValue = reader["Division"].ToString();//basappa
                    grno1.Text = reader["sibling1_grno"].ToString();
                    siblingName1.Text = reader["Sibling1_Name"].ToString();
                    siblingStd1.Text = reader["sibling1_std"].ToString();
                    grno2.Text = reader["Sibling2_Grno"].ToString();
                    siblingName2.Text = reader["Sibling2_Name"].ToString();
                    siblingStd2.Text = reader["Sibling2_Std"].ToString();

                    fatherFirstName.Text = reader["Father_Fname"].ToString();
                    fatherLastName.Text = reader["Father_Nationality"].ToString();
                    fatherNationality.Text = reader["Father_Nationality"].ToString();
                    fatherQualification.Text = reader["Father_Qualification"].ToString();
                    fatherOccupation.Text = reader["Father_Occupation"].ToString();
                    fatherIncome.Text = reader["Father_AnnualIncome"].ToString();
                    fatherPAN.Text = reader["Father_PANno"].ToString();
                    fatherOfficeAddress.Text = reader["Father_OfficeADD"].ToString();
                    fatherEmailAddress.Text = reader["Father_Email"].ToString();
                    fatherPhoneNumber.Text = reader["Father_Contact"].ToString();
                    ddldepartment.Text = reader["Father_Department"].ToString();//basappa holiday
                    txttktno.Text = reader["Father_TicketNo"].ToString();//basappa holiday

                    motherFirstName.Text = reader["Mother_Fname"].ToString();
                    motherLastName.Text = reader["Mother_Surname"].ToString();
                    motherNationality.Text = reader["Mother_Nationality"].ToString();
                    motherQualification.Text = reader["Mother_Qualification"].ToString();
                    motherOccupation.Text = reader["Mother_Occupation"].ToString();
                    motherIncome.Text = reader["Mother_AnnualIncome"].ToString();

                    motherPAN.Text = reader["Mother_PANno"].ToString();
                    motherOfficeAddress.Text = reader["Mother_OfficeADD"].ToString();
                    motherEmailAddress.Text = reader["Mother_Email"].ToString();
                    motherPhoneNumber.Text = reader["Mother_Contact"].ToString();
                    residentialAddress.Text = reader["Residential_Add"].ToString();
                    locality.Text = reader["Locality"].ToString();

                    imgPhoto.ImageUrl = reader["Photopath_child"].ToString();

                    lblphotopath.Text = reader["Photopath_child"].ToString();
                    lblbirthcertificate.Text = reader["Photopath_BirthCertificate"].ToString();
                    lblresidentialpath.Text = reader["photopath_ResidentProof"].ToString();

                    lbltcpath.Text = reader["photopath_TCProof"].ToString();//basappa
                    otherpf.Text = reader["photopath_OtherProof"].ToString();//basappa
                    ccerticate.Text = reader["Photopath_Castecertificate"].ToString();//basappa holiday
                    //imgBirthCertificate.ImageUrl= reader["Photopath_BirthCertificate"].ToString();
                    //imgResidential.ImageUrl = reader["photopath_ResidentProof"].ToString();

                    BirthCert.HRef = reader["Photopath_BirthCertificate"].ToString();
                    A1.HRef = reader["photopath_ResidentProof"].ToString();
                    if (reader["photopath_TCProof"].ToString().Length > 0)
                    {
                        transfercert.HRef = reader["photopath_TCProof"].ToString();


                    }
                    else
                    {
                        transfercert.Visible = false;
                    }
                    if (reader["photopath_OtherProof"].ToString().Length > 0)
                    {
                        otherproof.HRef = reader["photopath_OtherProof"].ToString();
                    }
                    else
                    {
                        otherproof.Visible = false;
                    }

                    //basappa holiday
                    if (reader["Photopath_Castecertificate"].ToString().Length > 0)
                    {
                        Castefiles.HRef = reader["Photopath_Castecertificate"].ToString();
                    }
                    else
                    {
                        Castefiles.Visible = false;
                    }
                }
            }
            else
            {
                // statusMsg.Text = "Unable to fetch details for Application ID - " + AdmissionID + "";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            }
        }

        //protected void chkemployeecheeck_SelectedIndexChanged(object sender, EventArgs e)
        //{ //basappa holiday
            
        //}

        //protected void chkemployeecheeck_CheckedChanged(object sender, EventArgs e)
        //{//basappa holiday
        //    if (chkemployeecheeck.Text == "Yes")
        //    {
        //        pnlemployeepanel.Visible = true;
        //        chkNo.Checked = false;
        //    }
            
            
        //}

        //protected void chkNo_CheckedChanged(object sender, EventArgs e)
        //{//basappa holiday
        //    if (chkNo.Text == "No")
        //    {
        //        pnlemployeepanel.Visible = false;
        //        chkemployeecheeck.Checked = false;
        //    }
        //}

        //public void GetDetails1(string AdmissionID)
        //{
        //    String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        //    SqlConnection con = new SqlConnection(connectionstring);
        //    con.Open();

        //    string query = "";
        //    query = "Select Photopath_child,Photopath_BirthCertificate,photopath_ResidentProof,photopath_TCProof,photopath_OtherProof,Division,Date,ApprovalStatus from AdmissionMaster where AdmissionID=@AdmissionID";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    cmd.Parameters.AddWithValue("@AdmissionID", AdmissionID);
        //    cmd.ExecuteNonQuery();
        //    if (AdmissionID !="0" || AdmissionID != "")
        //    {
        //        otherproof.Visible = true;
        //        transfercert.Visible = true;
        //        A1.Visible = true;
        //        BirthCert.Visible = true;
        //    }


        //    SqlDataReader reader = cmd.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {


        //            BirthCert.HRef = reader["Photopath_BirthCertificate"].ToString();
        //            A1  .HRef = reader["photopath_ResidentProof"].ToString();
        //            if (reader["photopath_TCProof"].ToString().Length > 0)
        //            {
        //                transfercert.HRef = reader["photopath_TCProof"].ToString();


        //            }
        //            else
        //            {
        //                transfercert.Visible = false;
        //            }
        //            if (reader["photopath_OtherProof"].ToString().Length > 0)
        //            {
        //                otherproof.HRef = reader["photopath_OtherProof"].ToString();
        //            }
        //            else
        //            {
        //                otherproof.Visible = false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //      //  statusMsg.Text = "Unable to fetch details for Application ID - " + AdmissionID + "";
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        //    }
        //}

        public class AdmissionDetails
        {
           // public string sno { get; set; } = "0";
            public string AcademicYear { get; set; }
            //  public string AdmissionID { get; set; } = "0";
            public string AdmissionID { get; set; }
            public string Surname { get; set; }
            public string StudentName { get; set; }
            public string FatherName { get; set; }

            public string MotherfullName { get; set; } //basappa holiday
            public string Gender { get; set; }

            public string LastStdPassed { get; set; } //basappa holiday
            public string STD { get; set; }
            public string DOB { get; set; }
            public string Division { get; set; }//basappa
            public string PlaceOfBirth { get; set; }

            public string Taluka { get; set; }//basappa holiday

            public string District { get; set; }//basappa holiday
            public string State { get; set; }//basappa holiday
            public string Nationality { get; set; }
            public string MotherTounge { get; set; }
            public string OTP { get; set; }
            public string Religion { get; set; }
            public string Caste { get; set; }//basappa holiday

            public string Category { get; set; }//basappa holiday
            public string AadharNo { get; set; }//basappa holiday
            public string LastSchoolattend { get; set; }//basappa holiday
            public string Sibling1_Grno { get; set; }
            public string Sibling1_Name { get; set; }
            public string Sibling1_Std { get; set; }
            public string Sibling2_Grno { get; set; }
            public string Sibling2_Name { get; set; }
            public string Sibling2_Std { get; set; }
            public string Father_Fname { get; set; }
            public string Father_Surname { get; set; }
            public string Father_Nationality { get; set; }
            public string Father_Occupation { get; set; }
            public string Father_Qualification { get; set; }
            public string Father_AnnualIncome { get; set; }
            public string Father_PANno { get; set; }
            public string Father_OfficeADD { get; set; }
            public string Father_Email { get; set; }
            public string Father_Contact { get; set; } 
           
            public string Father_department { get; set; }//basappa holiday
            public string Father_ticketno { get; set; }//basappa holiday

            public string iscenturyemployee { get; set; }//basappa holiday
            public string Mother_Fname { get; set; }
            public string Mother_Surname { get; set; }
            public string Mother_Nationality { get; set; }
            public string Mother_Occupation { get; set; }
            public string Mother_Qualification { get; set; }
            public string Mother_AnnualIncome { get; set; }
            public string Mother_PANno { get; set; }
            public string Mother_OfficeADD { get; set; }
            public string Mother_Email { get; set; }
            public string Mother_Contact { get; set; }
            public string Residential_Add { get; set; }
            public string Locality { get; set; }
            public string Photopath_child { get; set; }
            public string Photopath_BirthCertificate { get; set; }
            public string photopath_ResidentProof { get; set; }
            public string photopath_TCProof { get; set; }
            public string photopath_OtherProof { get; set; }

            public string photopath_CasteCertificate { get; set; }//basappa holiday
            public string Date { get; set; }

            public string updatedate { get; set; }
        }

    }
}