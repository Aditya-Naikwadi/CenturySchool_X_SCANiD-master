using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.ComponentModel;
using OfficeOpenXml;
using System.Web;

namespace CenturyRayonSchool.AdmissionModule
{
    public partial class ApplicationList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }
        private void BindGrid()
        {
            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            {
                using (SqlCommand cmd = new SqlCommand("Select AdmissionID, studentname, Surname, STD, Date, ApprovalStatus from AdmissionMaster"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            admissionList.DataSource = dt;
                            admissionList.DataBind();
                        }
                    }
                }
            }
        }

        protected void admissionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = admissionList.Rows[rowIndex];
            //set status=approved in DB
            string ApplicationID = row.Cells[0].Text;
            string studName = row.Cells[1].Text + " " + row.Cells[2].Text;
            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            if (e.CommandName == "confirm")
            {
                string query = "Update AdmissionMaster set ApprovalStatus = @ApprovalStatus where AdmissionID = " + ApplicationID + "";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ApprovalStatus", "Approved");
                cmd.ExecuteNonQuery();

                statusMsg.Text = "Application approved for - " + studName + "";
                row.Cells[5].Enabled = false;
                //e.Row.Cells[3].BackColor = Color.Red;
                row.Cells[4].BackColor = Color.Green;
                BindGrid();

                string studentName = "", toAddress1 = "", toAddress2 = "", year = "", useremail = "", std = "", phoneno = "";
                query = "Select studentname, Surname, Father_Email,Mother_Email,AcademicYear,FatherName, STD,father_contact from AdmissionMaster where AdmissionID=@AdmissionID";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AdmissionID", ApplicationID);
                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        studentName = reader[0].ToString() + " " + reader[1].ToString() + " " + reader[5].ToString();
                        toAddress2 = reader[3].ToString();
                        useremail = reader[2].ToString();
                        year = reader[4].ToString();
                        std = reader[6].ToString();
                        phoneno = reader[7].ToString();
                    }
                }

                string emailsubject = "Successfull Submission of Admission Form for " + studentName + "  Year " + year + " ";
                string emailbody = " <h3>Dear Parent,</h3> Your Application for Admission Form No. " + ApplicationID + " for child " + studentName + ", Std " + std + " , Academic Year: " + year + "  is confirmed. Kindly contact school office. Century Rayon High School </h3><br/> Regards, <br/><h3>CENRES<h3/> ";

                SendMailModule.SendEmail.sendEmailToClient(useremail, emailbody, emailsubject);
                //SendEmail(toAddress1, toAddress2, ApplicationID, studentName);

                string userid = "", password = "", senderid = "", entityid = "", templateid = "1707170996818556135", msg = "";
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
                msg = "Dear Parent, Your Application for Admission Form No. " + ApplicationID + " for child " + studentName + ", Std " + std + " , Academic Year: " + year + "  is confirmed. Kindly contact school office. Century Rayon High School Regards CENRES";
                // string userid= "scanidbiz", password = "oliq5896OL", senderid = "CENRES ", entityid = "1701169167163824773", templateid = "1707170962400551021";

                sendNimbusBizSMSNew(userid, password, senderid, phoneno, msg, entityid, templateid); //basappa
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                con.Close();
            }

            if (e.CommandName == "reject")
            {
                string query = "Update AdmissionMaster set ApprovalStatus = @ApprovalStatus where AdmissionID = " + ApplicationID + "";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ApprovalStatus", "Rejected");
                cmd.ExecuteNonQuery();

                statusMsg.Text = "Application Rejected for - " + studName + "";
                row.Cells[5].Enabled = false;
                row.Cells[4].BackColor = Color.Red;
                BindGrid();

                string studentName = "", toAddress1 = "", toAddress2 = "", year = "", useremail = "";
                query = "Select studentname, Surname, Father_Email,Mother_Email, AcademicYear from AdmissionMaster where AdmissionID=@AdmissionID";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AdmissionID", ApplicationID);
                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        studentName = reader[0].ToString() + " " + reader[1].ToString();
                        toAddress2 = reader[3].ToString();
                        useremail = reader[2].ToString();
                        year = reader[4].ToString();
                    }
                }

                // string emailsubject = "Rejection of Admission Form for "+studentName+"  Year " + year + " ";
                // string emailbody = " <h3>Dear Parent,</h3> Your application has been Rejected by admin.</h3><br/> Regards, <br/><h3>Century Rayon High School, Shahad<h3/> ";
                //                  
                //  SendMailModule.SendEmail.sendEmailToClient(useremail, emailbody, emailsubject);
                //SendRejectionEmail(toAddress1, toAddress2, ApplicationID, studentName);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);

                con.Close();


            }

            if (e.CommandName == "View")
            {
                Response.Redirect("ApplicationDetails.aspx?AdmissionID=" + ApplicationID);
            }
        }


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
                //Log.Error("SmsSendClass.sendNimbusBizSMS", ex);
                return ex.Message;
            }
        }


        //public void SendEmail(string toAddress1, string toAddress2, string ApplicationID, string studName)
        //{
        //    try
        //    {
        //        SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        //        using (MailMessage mm = new MailMessage(smtpSection.From, toAddress1.Trim()))
        //        {
        //            mm.Subject = "Status of Application ID - " + ApplicationID;
        //            mm.Body = "Dear " + studName + ", your application has been Approved by admin.";
        //            mm.IsBodyHtml = false;
        //            mm.CC.Add(toAddress2.Trim());
        //            SmtpClient smtp = new SmtpClient();
        //            smtp.Host = smtpSection.Network.Host;
        //            smtp.EnableSsl = smtpSection.Network.EnableSsl;
        //            NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
        //            smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
        //            smtp.Credentials = networkCred;
        //            smtp.Port = smtpSection.Network.Port;
        //            smtp.Send(mm);
        //            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Email sent.');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}


        //public static void sendEmailToClient(string useremail, string emailbody, string emailsubject)
        //{
        //    try
        //    {
        //        SendEmail.setEmail();
        //        if (useremail.Contains("@") && useremail.Length > 0 && useremail != "-")
        //        {

        //            if (frommail.Length > 0)
        //            {


        //                MailMessage mailmessage = new MailMessage(frommail, useremail);
        //                mailmessage.Subject = emailsubject;
        //                mailmessage.Body = emailbody;
        //                mailmessage.IsBodyHtml = true;

        //                SmtpClient smtpclient = new SmtpClient(hostname, emailport);

        //                smtpclient.UseDefaultCredentials = false;
        //                smtpclient.Credentials = new System.Net.NetworkCredential()
        //                {
        //                    UserName = frommail,
        //                    Password = frommailpassword
        //                };
        //                smtpclient.EnableSsl = true;

        //                smtpclient.Send(mailmessage);
        //            }
        //            else
        //            {
        //                throw new Exception("Sender's Email ID Not set for the system.");
        //            }


        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("SendEmail.sendEmailToClient", ex);
        //    }
        //}

        public void SendRejectionEmail(string toAddress1, string toAddress2, string ApplicationID, string studName)
        {
            try
            {
                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                using (MailMessage mm = new MailMessage(smtpSection.From, toAddress1.Trim()))
                {
                    mm.Subject = "Status of Application ID - " + ApplicationID;
                    mm.Body = "Dear " + studName + ", your application has been Rejected by admin.";
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

        protected void GenExcel_Click(object sender, EventArgs e)
        {
            //basappa holiday
            DataTable dt = new DataTable();
            if (chkApproved.Checked == true)
            {
                String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionstring);
                con.Open();
                {
                    using (SqlCommand cmd = new SqlCommand("Select AdmissionID,AcademicYear, Division, studentname, Surname, STD, Date, ApprovalStatus from AdmissionMaster where ApprovalStatus ='Approved'"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            //using ()
                            //{
                            sda.Fill(dt);
                            //}
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    string sourcefileaddress = Server.MapPath("~/AdmissionModule/TemplateExcelFile/" + "ApprovalAdmissionList.xlsx");
                    string destinationfileaddress = Server.MapPath("~/AdmissionModule/Excel File/" + chkApproved.Text + ".xlsx");

                    File.Copy(sourcefileaddress, destinationfileaddress, true);

                    using (ExcelPackage excel = new ExcelPackage(new FileInfo(destinationfileaddress)))
                    {
                        ExcelWorksheet worksheet = excel.Workbook.Worksheets[0];
                        int startRow = 2;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            worksheet.Cells[startRow + i, 1].Value = dt.Rows[i]["studentname"].ToString();
                            worksheet.Cells[startRow + i, 4].Value = dt.Rows[i]["Division"].ToString();
                            worksheet.Cells[startRow + i, 2].Value = dt.Rows[i]["Surname"].ToString();
                            worksheet.Cells[startRow + i, 3].Value = dt.Rows[i]["STD"].ToString();
                            worksheet.Cells[startRow + i, 7].Value = dt.Rows[i]["Date"].ToString();
                            worksheet.Cells[startRow + i, 8].Value = dt.Rows[i]["AcademicYear"].ToString();
                            worksheet.Cells[startRow + i, 9].Value = dt.Rows[i]["AdmissionID"].ToString();
                            //worksheet.Cells[startRow + i, 8].Value = dt.Rows[i]["ApprovalStatus"].ToString();
                        }
                        HttpResponse response = HttpContext.Current.Response;
                        response.ClearContent();
                        response.ClearHeaders();
                        response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        response.AppendHeader("Content-Disposition", "attachment;filename=\"" + chkApproved.Text + "ApplicationList.xlsx\"");
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            excel.SaveAs(memoryStream);
                            memoryStream.Position = 0;
                            memoryStream.CopyTo(response.OutputStream);
                        }

                        response.Flush();
                        response.End();

                    }
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record found.')", true);
                }

            }
            else if (chkReject.Checked == true)
            {
                DataTable dtreject = new DataTable();

                String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionstring);
                con.Open();
                {
                    using (SqlCommand cmd = new SqlCommand("Select AdmissionID,Division, studentname, Surname, STD, Date, ApprovalStatus from AdmissionMaster where ApprovalStatus ='Rejected'"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            //using ()
                            //{
                            sda.Fill(dtreject);
                            //}
                        }
                    }
                }

                if (dtreject.Rows.Count > 0)
                {
                    string sourcefileaddress = Server.MapPath("~/AdmissionModule/TemplateExcelFile/" + "RejectAdmissionList.xlsx");
                    string destinationfileaddress = Server.MapPath("~/AdmissionModule/Excel File/" + chkReject.Text + ".xlsx");

                    File.Copy(sourcefileaddress, destinationfileaddress, true);

                    using (ExcelPackage excel = new ExcelPackage(new FileInfo(destinationfileaddress)))
                    {
                        ExcelWorksheet worksheet = excel.Workbook.Worksheets[0];
                        int startRow = 2;
                        for (int i = 0; i < dtreject.Rows.Count; i++)
                        {
                            worksheet.Cells[startRow + i, 1].Value = dtreject.Rows[i]["studentname"].ToString();
                            worksheet.Cells[startRow + i, 4].Value = dtreject.Rows[i]["Division"].ToString();
                            worksheet.Cells[startRow + i, 2].Value = dtreject.Rows[i]["Surname"].ToString();
                            worksheet.Cells[startRow + i, 3].Value = dtreject.Rows[i]["STD"].ToString();
                            worksheet.Cells[startRow + i, 5].Value = dtreject.Rows[i]["Date"].ToString();
                            //worksheet.Cells[startRow + i, 8].Value = dtreject.Rows[i]["ApprovalStatus"].ToString();
                        }
                        HttpResponse response = HttpContext.Current.Response;
                        response.ClearContent();
                        response.ClearHeaders();
                        response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        response.AppendHeader("Content-Disposition", "attachment;filename=\" " + chkReject.Text + "ApplicationList.xlsx\"");
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            excel.SaveAs(memoryStream);
                            memoryStream.Position = 0;
                            memoryStream.CopyTo(response.OutputStream);
                        }

                        response.Flush();
                        response.End();

                    }
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record found.')", true);
                }
            }
            else
            {


                //Response.ClearContent();
                //Response.AppendHeader("content-disposition", "attachment; filename=AdmissionList.xlsx");
                //Response.ContentType = "application/excel";
                //System.IO.StringWriter stringWriter = new System.IO.StringWriter();
                //HtmlTextWriter htw = new HtmlTextWriter(stringWriter);
                //admissionList.HeaderRow.Cells[6].Visible = false;
                //admissionList.HeaderRow.Cells[7].Visible = false;
                //for (int i = 0; i < admissionList.Rows.Count; i++)
                //{
                //    GridViewRow row = admissionList.Rows[i];
                //    row.Cells[6].Visible = false;
                //    row.Cells[7].Visible = false;
                //}
                //admissionList.RenderControl(htw);
                //Response.Write(stringWriter.ToString());
                //Response.End();
                DataTable dtall = new DataTable();

                String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionstring);
                con.Open();
                {
                    using (SqlCommand cmd = new SqlCommand("Select AdmissionID,Division, studentname, Surname, STD, Date, ApprovalStatus from AdmissionMaster"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dtall);
                        }
                    }
                }

                if (dtall.Rows.Count > 0)
                {
                    string sourcefileaddress = Server.MapPath("~/AdmissionModule/TemplateExcelFile/" + "AdmissionList.xlsx");
                    string destinationfileaddress = Server.MapPath("~/AdmissionModule/Excel File/AdmissionList.xlsx");

                    File.Copy(sourcefileaddress, destinationfileaddress, true);

                    using (ExcelPackage excel = new ExcelPackage(new FileInfo(destinationfileaddress)))
                    {
                        ExcelWorksheet worksheet = excel.Workbook.Worksheets[0];
                        int startRow = 2;
                        for (int i = 0; i < dtall.Rows.Count; i++)
                        {
                            worksheet.Cells[startRow + i, 1].Value = dtall.Rows[i]["AdmissionID"].ToString();
                            worksheet.Cells[startRow + i, 2].Value = dtall.Rows[i]["studentname"].ToString();
                            worksheet.Cells[startRow + i, 3].Value = dtall.Rows[i]["Surname"].ToString();
                            worksheet.Cells[startRow + i, 4].Value = dtall.Rows[i]["STD"].ToString();
                            worksheet.Cells[startRow + i, 5].Value = dtall.Rows[i]["Division"].ToString();
                            worksheet.Cells[startRow + i, 6].Value = dtall.Rows[i]["Date"].ToString();
                            worksheet.Cells[startRow + i, 7].Value = dtall.Rows[i]["ApprovalStatus"].ToString();
                            //worksheet.Cells[startRow + i, 8].Value = dtreject.Rows[i]["ApprovalStatus"].ToString();
                        }
                        HttpResponse response = HttpContext.Current.Response;
                        response.ClearContent();
                        response.ClearHeaders();
                        response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        response.AppendHeader("Content-Disposition", "attachment;filename=\" ApplicationList.xlsx\"");
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            excel.SaveAs(memoryStream);
                            memoryStream.Position = 0;
                            memoryStream.CopyTo(response.OutputStream);
                        }

                        response.Flush();
                        response.End();

                    }
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record found.')", true);
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void admissionList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TableCell statusCell = e.Row.Cells[5];
                string aprrovalstatus = DataBinder.Eval(e.Row.DataItem, "ApprovalStatus") as string;
                if (!string.IsNullOrEmpty(aprrovalstatus))
                {
                    if (aprrovalstatus.Equals("Approved", StringComparison.OrdinalIgnoreCase))
                    {
                        e.Row.Cells[5].BackColor = System.Drawing.Color.Green;
                        //statusCell.BackColor = System.Drawing.Color.Green;
                    }
                    else if (aprrovalstatus.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
                    {
                        e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void btnmasterclick_Click(object sender, EventArgs e)
        {
            Response.Redirect("AcadamicYearMaster.aspx");
        }

        protected void btnUploadAprrovallist_Click(object sender, EventArgs e)//basappa holiday
        {
            if (FileUploadControl.HasFile)
            {
                try
                {
                    HttpPostedFile uploadedFile = FileUploadControl.PostedFile;

                    // Check if the uploaded file is an Excel file
                    if (Path.GetExtension(uploadedFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        DataTable dt = LoadExcelIntoDataTable(uploadedFile.InputStream);
                        if (dt.Columns.Contains("Grno") && dt.Columns.Contains("AdmissionType"))
                        {


                            if (dt.Rows.Count > 0)
                            {
                               
                                foreach (DataRow row in dt.Rows)
                                {
                                    //int academicYear = Convert.ToInt32(row["Acadamic Year"]);
                                    string academicYear = row["Acadamic Year"].ToString();
                                    string AdmissionID = row["AdmissionID"].ToString();
                                    string grno = row["Grno"].ToString();
                                    string studentname = row["Student Name"].ToString();
                                    string Surname = row["Surname"].ToString();
                                    string std = row["STD"].ToString();
                                    string div = row["Div"].ToString();
                                    string AdmissionType = row["AdmissionType"].ToString();
                                    string SubmissionDate = row["Submission Date"].ToString();

                                    bool dataExists = CheckDataInSQL(academicYear, grno);


                                    if (dataExists)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Gr no aleardy present, cannot insert data.')", true);
                                        return;

                                    }
                                    else
                                    {

                                        InsertData(academicYear, AdmissionID, grno, AdmissionType, div); //,grno, studentname, Surname, std, div, AdmissionType, SubmissionDate
                                                                                                         // UpdateData(academicYear, grno); 
                                    }

                                }

                            }

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select downloaded approval excel file only.')", true);

                        }

                    }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select valid excel file.')", true);

                        }
                   
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('An error occurred while processing the file. Error:.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select excel file first.')", true);
                return;
            }

        }


        private bool CheckDataInSQL(string academicYear, string grno)
        {
            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            string query = "SELECT COUNT(*) FROM studentmaster WHERE AcademicYear = @AcademicYear AND grno = @grno";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@AcademicYear", academicYear);
                command.Parameters.AddWithValue("@grno", grno);
                int count = Convert.ToInt32(command.ExecuteScalar());

                return count > 0;
            }
        }

        private DataTable LoadExcelIntoDataTable(Stream stream)
        {
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                DataTable dt = new DataTable();
                foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    dt.Columns.Add(firstRowCell.Text.Trim());
                }
                for (int rowNumber = 2; rowNumber <= worksheet.Dimension.End.Row; rowNumber++)
                {
                    var row = worksheet.Cells[rowNumber, 1, rowNumber, worksheet.Dimension.End.Column];
                    var newRow = dt.Rows.Add();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return dt;
            }
        }



        private void InsertData(string academicYear, string AdmissionID, string grno, string AdmissionType, string div) // string grno, string studentname, string Surname, string std, string div, string AdmissionType, string SubmissionDate
        {
            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            string AcademicYear = "", Surname = "", StudentName = "", FatherName = "", MotherfullName = "", gender = "", standard = "", dob = "", PlaceOfBirth = "", Taluka = "", District = "", Photopath_child = "",
                State = "", Nationality = "", MotherTounge = "", Religion = "", Caste = "", Category = "", AAdharNo = "", Residential_Add = "", Locality = "", iscenturyemployee="",Father_Department = "",Father_TicketNo = "";
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string query2 = "Select AcademicYear,Surname,StudentName, FatherName,MotherfullName,gender, std,dob,PlaceOfBirth,Taluka," +
                "District,State,Nationality,MotherTounge,Religion,Caste,Category,AAdharNo,Residential_Add,Locality,Photopath_child,iscenturyemployee,Father_Department,Father_TicketNo " +
                "from AdmissionMaster where AdmissionID = " + AdmissionID + "  and AcademicYear = '" + academicYear + "' ";

            SqlCommand cmd1 = new SqlCommand(query2, con);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                AcademicYear = reader1["AcademicYear"].ToString();
                Surname = reader1["Surname"].ToString();
                StudentName = reader1["StudentName"].ToString();

                FatherName = reader1["FatherName"].ToString();
                MotherfullName = reader1["MotherfullName"].ToString();
                gender = reader1["gender"].ToString();
                standard = reader1["std"].ToString();
                dob = reader1["dob"].ToString();
                PlaceOfBirth = reader1["PlaceOfBirth"].ToString();
                Taluka = reader1["Taluka"].ToString();
                District = reader1["District"].ToString();
                State = reader1["State"].ToString();
                Nationality = reader1["Nationality"].ToString();
                MotherTounge = reader1["MotherTounge"].ToString();
                Religion = reader1["Religion"].ToString();
                Caste = reader1["Caste"].ToString();
                Category = reader1["Category"].ToString();
                AAdharNo = reader1["AAdharNo"].ToString();
                Residential_Add = reader1["Residential_Add"].ToString();
                Locality = reader1["Locality"].ToString();
                Photopath_child = reader1["Photopath_child"].ToString();
                iscenturyemployee = reader1["iscenturyemployee"].ToString();
                Father_Department = reader1["Father_Department"].ToString();
                Father_TicketNo = reader1["Father_TicketNo"].ToString();
            }
            reader1.Close();

            string query = "insert into studentmaster (FNAME,FATHERNAME,LNAME,MOTHERNAME, GENDER, STD,Div,GRNO,DOB,placeofbirth, birthtaluka, birthdistrict," +
                "birthstate, Nationality,mothertongue, RELIGION, CASTE, CATEGORY, aadharcard,ADDRESS,CITY, PhotoPath, AcademicYear,entrydate,admissiontype,ROLLNO,cid, iscenturyemployee,Father_Department,Father_TicketNo) " +
                "values(@StudentName,@FatherName,@Surname,@MotherfullName,@gender,@standard,@div,@grno,@dob,@PlaceOfBirth,@Taluka,@District,@State," +
                "@Nationality,@MotherTounge,@Religion,@Caste,@Category,@AAdharNo,@Residential_Add,@Locality,@Photopath_child,@academicyear,@submisiiondate,@admissiontype,@rollno,@contcid,@iscenturyemployee,@Father_Department,@Father_TicketNo)";
            string rollno = "0";

            int newcid = 0, contcid = 0;
            string query1 = "SELECT MAX(CAST(cid AS INT)) FROM studentmaster";
            SqlCommand cmd = new SqlCommand(query1, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                contcid = Convert.ToInt32(reader[0]);
            }
            reader.Close();

            newcid = contcid + 1;

            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@StudentName", StudentName);
                command.Parameters.AddWithValue("@FatherName", FatherName);
                command.Parameters.AddWithValue("@Surname", Surname);

                command.Parameters.AddWithValue("@MotherfullName", MotherfullName);
                command.Parameters.AddWithValue("@gender", gender);

                command.Parameters.AddWithValue("@standard", standard);
                command.Parameters.AddWithValue("@div", div);
                command.Parameters.AddWithValue("@AcademicYear", AcademicYear);
                command.Parameters.AddWithValue("@grno", grno);
                command.Parameters.AddWithValue("@dob", dob);
                command.Parameters.AddWithValue("@PlaceOfBirth", PlaceOfBirth);
                command.Parameters.AddWithValue("@Taluka", Taluka);
                command.Parameters.AddWithValue("@District", District);
                command.Parameters.AddWithValue("@State", State);
                command.Parameters.AddWithValue("@Nationality", Nationality);
                command.Parameters.AddWithValue("@MotherTounge", MotherTounge);
                command.Parameters.AddWithValue("@Religion", Religion);

                command.Parameters.AddWithValue("@Caste", Caste);
                command.Parameters.AddWithValue("@Category", Category);
                command.Parameters.AddWithValue("@AAdharNo", AAdharNo);
                command.Parameters.AddWithValue("@Residential_Add", Residential_Add);
                command.Parameters.AddWithValue("@Locality", Locality);
                command.Parameters.AddWithValue("@Photopath_child", Photopath_child);

                command.Parameters.AddWithValue("@submisiiondate", date);
                command.Parameters.AddWithValue("@rollno", rollno);
                command.Parameters.AddWithValue("@admissiontype", AdmissionType);
                command.Parameters.AddWithValue("@contcid", newcid);
                command.Parameters.AddWithValue("@Father_TicketNo", Father_TicketNo);
                command.Parameters.AddWithValue("@Father_Department", Father_Department); 
                command.Parameters.AddWithValue("@iscenturyemployee", iscenturyemployee);
                command.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data inserted successfully.')", true);

            }
            con.Close();
        }

        private void UpdateData(string academicYear, string grno)
        {
            String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            string query = "update AdmissionMaster set FNAME=@StudentName,MNAME=@Surname,STD=@std,Div=@div,GRNO=@grno, AcademicYear=@academicyear where FNAME=@StudentName and AcademicYear=@academicyear) ";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@AcademicYear", academicYear);
                command.Parameters.AddWithValue("@ID", grno);
                command.ExecuteNonQuery();
            }
        }

     

        protected void btndownload_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                String connectionstring = WebConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
                ReportDocument rd = new ReportDocument();
                using (con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    string status = "Approved";
                    string currentyear = Session["currentYearRange"].ToString();
                    //string query = "select ID,academicyear,admissionid,Surname,StudentName,FatherName,Gender,std,dob,PlaceOfBirth,Nationality,MotherTounge,otp,Religion,sibling1_grno,Sibling1_Name,sibling1_std,Sibling2_Grno,Sibling2_Name,Sibling2_Std,Father_Fname,Father_Surname,Father_Nationality,Father_Occupation,Father_Qualification,Father_AnnualIncome,Father_PANno,Father_OfficeADD,Father_Email,Father_Contact,Mother_Fname,Mother_Surname,Mother_Nationality,Mother_Occupation,Mother_Qualification,Mother_AnnualIncome,Mother_PANno,Mother_OfficeADD,Mother_Email,Mother_Contact,Residential_Add,Locality,Photopath_child,Photopath_BirthCertificate,photopath_ResidentProof,[Date],ApprovalStatus " +
                    //               "from AdmissionMaster " +
                    //                "where admissionid='" + applicationNumber.Text + "';";
                    string query = "select academicyear,admissionid,Surname,StudentName,FatherName,MotherfullName,Gender,LastSTDpassed,std,dob,PlaceOfBirth,Nationality,MotherTounge,otp,Religion,sibling1_grno,Sibling1_Name,sibling1_std,Sibling2_Grno,Sibling2_Name,Sibling2_Std,Father_Fname,Father_Surname,Father_Nationality,Father_Occupation,Father_Qualification,Father_AnnualIncome,Father_PANno,Father_OfficeADD,Father_Email,Father_Contact," +
                        "Mother_Fname,Mother_Surname,Mother_Nationality,Mother_Occupation,Mother_Qualification,Mother_AnnualIncome,Mother_PANno,Mother_OfficeADD,Mother_Email,Mother_Contact,Residential_Add,Locality,Photopath_child,Photopath_BirthCertificate,photopath_ResidentProof,[Date],ApprovalStatus,iscenturyemployee,Father_Department,Father_TicketNo,Taluka,District,State,Nationality,MotherTounge,Caste,Category,AAdharNo,LastSchoolAttend " +
                                  "from AdmissionMaster " +
                                   "where AcademicYear=@AcademicYear and ApprovalStatus=@ApprovalStatus;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@AcademicYear", currentyear);
                    cmd.Parameters.AddWithValue("@ApprovalStatus", status);
                    SqlDataAdapter adap1 = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    adap1.Fill(dt1);
                    if (dt1.Rows.Count>0)
                    {
                        CenturyRayonSchool.AdmissionModule.CRReports.Dataset.AdmissionForm _ds = new CRReports.Dataset.AdmissionForm();
                        //BirlaAdmissionModule.CRReports.Dataset.AdmissionForm _ds = new BirlaAdmissionModule.CRReports.Dataset.AdmissionForm();
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        //adap.Fill(_ds.Tables[0]);
                        adap.Fill(_ds.Tables[0]);

                        
                        rd.Load(Path.Combine(Server.MapPath("~/AdmissionModule/CRReports"), "AdmissionForm.rpt"));
                        //rd.SetDataSource(_ds.Tables[0]);
                        rd.SetDataSource(_ds.Tables[0]);

                        string folderpath = Server.MapPath("~/AdmissionModule/DownloadFile");
                        string filename = "AdmissionForm_" + status + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmssff") + ".pdf";

                        rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);

                        rd.Dispose();
                        rd.Close();
                        Response.ContentType = "Application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                        Response.TransmitFile(Server.MapPath("~/AdmissionModule/DownloadFile/" + filename));
                        Response.End();

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No record found to download.')", true);

                    }

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
    }
}
