using CenturyRayonSchool.Model;
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

namespace CenturyRayonSchool.AdmissionModule
{
    public partial class SearchForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OTPSection.Visible = false;
            }
        }
        protected void GoToModify_Click(object sender, EventArgs e)
        {
            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();

            String query = "";
            string OTP = "";
            query = "Select * from AdmissionMaster where AdmissionID=@AdmissionID and OTP=@OTP";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@AdmissionID", searchApplicationNumber.Text);
            cmd.Parameters.AddWithValue("@OTP", otp.Text);
            cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                Session["AdmissionID"] = "Modify";
                Response.Redirect("AdmissionForm.aspx?AdmissionID=" + searchApplicationNumber.Text);
            }
            else
            {
                statusMsg.Text = "Invalid OTP for Application ID - " + searchApplicationNumber.Text + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            }

        }

        protected void SearchAndGoToModifyForm_Click(object sender, EventArgs e)
        {
            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();

            String query = "";
            string status = "", studName = "", toAddress1 = "", toAddress2 = "", Father_Contact = "", fatheremail = "";
            query = "Select ApprovalStatus,AdmissionID, studentname, Surname, Father_Email,Mother_Email,Father_Contact from AdmissionMaster where AdmissionID=@AdmissionID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@AdmissionID", searchApplicationNumber.Text);
            cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    status = reader[0].ToString();
                    studName = reader[2].ToString() + " " + reader[3].ToString();
                    fatheremail = reader[4].ToString();
                    toAddress2 = reader[5].ToString();
                    Father_Contact = reader[6].ToString();
                }
                if (status == "Pending")
                {
                    //send OTP allow to modify
                    //SendEmail(toAddress1, toAddress2, searchApplicationNumber.Text, studName);
                    //statusMsg.Text = "OTP has been sent on registered Email Address for Application ID- " + searchApplicationNumber.Text + "";
                    string userid = "", password = "", senderid = "", entityid = "", templateid = "1707170996781220037", msg = "";
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
                    // msg = "OTP has been sent on registered Email Address for Application ID-" + searchApplicationNumber.Text + "";
                    statusMsg.Text = "OTP has been sent on registered mobile no / Email for Application ID-" + searchApplicationNumber.Text + "";
                    //    generate OTP
                    string OTP = GenerateOTP(searchApplicationNumber.Text);
                    msg = "OTP to modify your Application Form No. " + searchApplicationNumber.Text + " is " + OTP+". Century Rayon High School, Regards CENRES";
                    sendNimbusBizSMSNew(userid, password, senderid, Father_Contact, msg, entityid, templateid); //basappa

                    string emailsubject = "OTP for Admission Form.";
                    string emailbody = " <h3>Dear Parent,</h3>OTP to modify your Application Form No. - " + searchApplicationNumber.Text + " is " + OTP + " Century Rayon High School. </h3><br/> Regards, <br/><h  3> CENRES<h3/> ";

                    SendMailModule.SendEmail.sendEmailToClient(fatheremail, emailbody, emailsubject);
                    reader.Close();
                    con.Close();
                    //statusMsg.Text = "OTP has been sent on registered Email Address for Application ID- " + searchApplicationNumber.Text + "";
                    OTPSection.Visible = true;
                    searchApplicationNumber.ReadOnly = true;
                    SearchAndGoToModifyForm.Enabled = false;
                    SearchAndGoToModifyForm.CssClass = "btn btn-info";
                }
                else if (status == "Approved")
                {
                    //msg = "OTP has been sent on registered mobile no for Application ID-" + searchApplicationNumber.Text + "";
                    statusMsg.Text = "Cannot modify application details for ID- " + searchApplicationNumber.Text + ", as application is already Approved.";
                }
                else if (status == "Rejected")
                {
                    statusMsg.Text = "Your application ID- " + searchApplicationNumber.Text + ", has ben rejected please contact school office.";
                }
            }
            else
            {
                statusMsg.Text = "Invalid Application ID - " + searchApplicationNumber.Text + "";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
          
        }


        //public static string sendNimbusBizSMSNew(string userid, string password, string senderid, string phoneno, string msg, string entityid, string templateid)
        //{
        //    Task<string> resp = sendNimbusBizSMS(userid, password, senderid, phoneno, msg, entityid, templateid);
        //    return resp.Result;
        //}
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

        public void SendEmail(string toAddress1, string toAddress2, string ApplicationID, string studName)
        {
            try
            {
                //generate OTP
                //string OTP = GenerateOTP(ApplicationID);
                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                using (MailMessage mm = new MailMessage(smtpSection.From, toAddress1.Trim()))
                {
                    mm.Subject = "OTP for Application ID - " + ApplicationID;
                    //  mm.Body = "Dear " + studName + ", OTP to modify the application form is - " + OTP + "";
                    mm.IsBodyHtml = false;
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

        public string GenerateOTP(string ApplicationID)
        {
            string otp = "";
            int min = 100000;
            int max = 999999;
            Random r = new Random();
            otp = r.Next(min, max).ToString();

            //insert otp for that app no in db
            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            string query = "Update AdmissionMaster set OTP = @OTP where AdmissionID = " + ApplicationID + "";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@OTP", otp);
            cmd.ExecuteNonQuery();

            con.Close();
            return otp;
        }
    }
}