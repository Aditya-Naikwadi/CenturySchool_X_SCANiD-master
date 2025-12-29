using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.AdmissionModule
{
    public partial class ApplicationDetails : System.Web.UI.Page
    {
        string AdmissionID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["AdmissionID"] != null)
            {
                AdmissionID = Request.QueryString["AdmissionID"];
            }
            else
            {
                Response.Redirect("ApplicationList.aspx");
            }

            if (!IsPostBack)
            {
                siblingHeader.Visible = false;
                hrTag.Visible = false;
                GetDetails(AdmissionID);
            }
        }
        public void GetDetails(string AdmissionID)
        {
            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();

            string query = "";
            query = "Select AcademicYear,AdmissionID,Surname,StudentName,FatherName,MotherfullName,Gender,LastSTDpassed,STD,DOB,PlaceOfBirth,Taluka, District, State,Nationality,MotherTounge,Religion,Caste, Category,AAdharNo,LastSchoolAttend,Sibling1_Grno,Sibling1_Name,Sibling1_Std,Sibling2_Grno,Sibling2_Name,Sibling2_Std,Father_Fname,Father_Surname,Father_Nationality,Father_Occupation,Father_Qualification,Father_AnnualIncome, " +
"Father_PANno,Father_OfficeADD,Father_Email,Father_Contact,iscenturyemployee,Father_Department, Father_TicketNo, Mother_Fname,Mother_Surname,Mother_Nationality,Mother_Occupation,Mother_Qualification,Mother_AnnualIncome,Mother_PANno,Mother_OfficeADD,Mother_Email,Mother_Contact,Residential_Add,Locality,Photopath_child,Photopath_BirthCertificate,photopath_ResidentProof,photopath_TCProof,photopath_OtherProof,Photopath_Castecertificate,Division,Date,ApprovalStatus from AdmissionMaster where AdmissionID=@AdmissionID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@AdmissionID", AdmissionID);
            cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    studName.InnerText += reader["StudentName"].ToString() + " " + reader["Surname"].ToString() + " " + reader["FatherName"].ToString();
                    academicYear.InnerText += reader["AcademicYear"].ToString();
                    appId.InnerText = reader["AdmissionID"].ToString();
                    gender.InnerText += reader["Gender"].ToString() == "M" ? "Male" : "Female";
                    laststandarpassed.InnerText += reader["LastSTDpassed"].ToString();
                    std.InnerText += reader["STD"].ToString();
                    dob.InnerText += Convert.ToDateTime(reader["DOB"].ToString().Replace('/', '-')).ToString("dd/MM/yyyy");
                    POB.InnerText += reader["PlaceOfBirth"].ToString();
                    Taluka.InnerText += reader["Taluka"].ToString();
                    District.InnerText += reader["District"].ToString();
                    State.InnerText += reader["State"].ToString();
                    nationality.InnerText += reader["Nationality"].ToString();
                    motherTounge.InnerText += reader["MotherTounge"].ToString();
                    religion.InnerText += reader["Religion"].ToString();
                    Caste.InnerText += reader["Caste"].ToString();
                    Category.InnerText += reader["Category"].ToString();
                    Aadharno.InnerText += reader["Aadharno"].ToString();
                    LastSchoolAttend.InnerText += reader["LastSchoolAttend"].ToString();
                    isemployee.InnerText += reader["iscenturyemployee"].ToString();
                    Department.InnerText += reader["Father_Department"].ToString();
                    TicketNo.InnerText += reader["Father_TicketNo"].ToString();

                    if (reader["Sibling1_Grno"].ToString().Length > 0 && reader["Sibling1_Name"].ToString().Length > 0 && reader["Sibling1_Std"].ToString().Length > 0)
                    {
                        siblingHeader.Visible = true;
                        hrTag.Visible = true;
                        sbGRNO1.InnerText = "Sibling-1 GRNO - " + reader["Sibling1_Grno"].ToString();
                        sbName1.InnerText = "Sibling-1 Name - " + reader["Sibling1_Name"].ToString();
                        sbSTD1.InnerText = "Sibling-1 STD - " + reader["Sibling1_Std"].ToString();
                    }
                    if (reader["Sibling2_Grno"].ToString().Length > 0 && reader["Sibling2_Name"].ToString().Length > 0 && reader["Sibling2_Std"].ToString().Length > 0)
                    {
                        sbGRNO2.InnerText = "Sibling-2 GRNO - " + reader["Sibling2_Grno"].ToString();
                        sbName2.InnerText = "Sibling-2 Name - " + reader["Sibling2_Name"].ToString();
                        sbSTD2.InnerText = "Sibling-2 STD - " + reader["Sibling2_Std"].ToString();
                    }
          

                fatherName.InnerText += reader["Father_Fname"].ToString() + " " + reader["Father_Surname"].ToString();
                    fatherQualification.InnerText += reader["Father_Qualification"].ToString();
                    fatherOccupation.InnerText += reader["Father_Occupation"].ToString();
                    fatherEmailID.InnerText += reader["Father_Email"].ToString();
                    fatherContact.InnerText += reader["Father_Contact"].ToString();

                    motherName.InnerText += reader["Mother_Fname"].ToString() + " " + reader["Mother_Surname"].ToString();
                    motherQualification.InnerText += reader["Mother_Qualification"].ToString();
                    motherOccupation.InnerText += reader["Mother_Occupation"].ToString();
                    motherEmailID.InnerText += reader["Mother_Email"].ToString();
                    motherContact.InnerText += reader["Mother_Contact"].ToString();

                    address.InnerText += reader["Residential_Add"].ToString();
                    locality.InnerText += reader["Locality"].ToString();
                    childPhoto.ImageUrl = reader["Photopath_child"].ToString();
                    BirthCert.HRef = reader["Photopath_BirthCertificate"].ToString();
                    ResidentialProof.HRef = reader["photopath_ResidentProof"].ToString();
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

                    if (reader["Photopath_Castecertificate"].ToString().Length > 0)
                    {
                        castcerfificate.HRef = reader["Photopath_Castecertificate"].ToString();
                    }
                    else
                    {
                        castcerfificate.Visible = false; 
                    }
                }
            }
            else
            {
                statusMsg.Text = "Unable to fetch details for Application ID - " + AdmissionID + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            }
        }

        protected void btngotolist_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicationDetails.aspx");
        }
    }
}