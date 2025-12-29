using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace CenturyRayonSchool.AdmissionModule
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            statusMsg.Visible = false;
            downloadForm.Visible = false;
        }
        protected void getDetails_Click(object sender, EventArgs e)
        {
            int count = 0;
            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();

            String query = "";
            string status = "";
            query = "Select Count(*), ApprovalStatus from AdmissionMaster where AdmissionID=@AdmissionID group by ApprovalStatus";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@AdmissionID", applicationNumber.Text);
            cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                count = Convert.ToInt32(reader[0]);
                status = reader[1].ToString();
            }
            if (count > 0)
            {
                statusMsg.Visible = true;

                if (status == "Pending")
                {
                    statusMsg.Text = "Status of application - " + applicationNumber.Text + " is Under Process";
                }
                else
                {
                    statusMsg.Text = "Status of application - " + applicationNumber.Text + " is " + status + "";
                }

                downloadForm.Visible = true;
                getDetails.Visible = false;
            }
            else
            {
                statusMsg.Visible = true;
                getDetails.Visible = false;
                statusMsg.Text = "Invalid Application ID - " + applicationNumber.Text + "";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }

        protected void downloadForm_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
                using (con = new SqlConnection(connectionstring))
                {
                    con.Open();

                    //string query = "select ID,academicyear,admissionid,Surname,StudentName,FatherName,Gender,std,dob,PlaceOfBirth,Nationality,MotherTounge,otp,Religion,sibling1_grno,Sibling1_Name,sibling1_std,Sibling2_Grno,Sibling2_Name,Sibling2_Std,Father_Fname,Father_Surname,Father_Nationality,Father_Occupation,Father_Qualification,Father_AnnualIncome,Father_PANno,Father_OfficeADD,Father_Email,Father_Contact,Mother_Fname,Mother_Surname,Mother_Nationality,Mother_Occupation,Mother_Qualification,Mother_AnnualIncome,Mother_PANno,Mother_OfficeADD,Mother_Email,Mother_Contact,Residential_Add,Locality,Photopath_child,Photopath_BirthCertificate,photopath_ResidentProof,[Date],ApprovalStatus " +
                    //               "from AdmissionMaster " +
                    //                "where admissionid='" + applicationNumber.Text + "';";
                    string query = "select academicyear,admissionid,Surname,StudentName,FatherName,Gender,std,dob,PlaceOfBirth,Nationality,MotherTounge,otp,Religion,sibling1_grno,Sibling1_Name,sibling1_std,Sibling2_Grno,Sibling2_Name,Sibling2_Std,Father_Fname,Father_Surname,Father_Nationality,Father_Occupation,Father_Qualification,Father_AnnualIncome,Father_PANno,Father_OfficeADD,Father_Email,Father_Contact,Mother_Fname,Mother_Surname,Mother_Nationality,Mother_Occupation,Mother_Qualification,Mother_AnnualIncome,Mother_PANno,Mother_OfficeADD,Mother_Email,Mother_Contact,Residential_Add,Locality,Photopath_child,Photopath_BirthCertificate,photopath_ResidentProof,[Date],ApprovalStatus " +
                                  "from AdmissionMaster " +
                                   "where admissionid='" + applicationNumber.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    CenturyRayonSchool.AdmissionModule.CRReports.Dataset.AdmissionForm _ds = new CRReports.Dataset.AdmissionForm();
                    //BirlaAdmissionModule.CRReports.Dataset.AdmissionForm _ds = new BirlaAdmissionModule.CRReports.Dataset.AdmissionForm();
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    //adap.Fill(_ds.Tables[0]);
                    adap.Fill(_ds.Tables[0]);

                    ReportDocument rd = new ReportDocument();
                    rd.Load(Path.Combine(Server.MapPath("~/AdmissionModule/CRReports"), "AdmissionForm.rpt"));
                    //rd.SetDataSource(_ds.Tables[0]);
                    rd.SetDataSource(_ds.Tables[0]);

                    string folderpath = Server.MapPath("~/AdmissionModule/DownloadFile");
                    string filename = "AdmissionForm_" + applicationNumber.Text + ".pdf";

                    rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);

                    rd.Dispose();
                    rd.Close();
                    Response.ContentType = "Application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                    Response.TransmitFile(Server.MapPath("~/AdmissionModule/DownloadFile/" + filename));
                    Response.End();

                }


            }
            catch (Exception ex)
            {
               
            }
            finally
            {
                if (con != null) { con.Close(); }
            }



        }

        protected void closeModal_Click(object sender, EventArgs e)
        {
            applicationNumber.Text = "";
            getDetails.Visible = true;
        }
    }
}