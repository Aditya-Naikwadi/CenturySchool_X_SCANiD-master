//using CenturyRayonSchool.Model;
using System;
using System.Data.SqlClient;
using System.Net.Mail;

namespace SendMailModule
{
    public static class SendEmail
    {
        private static string frommail = "";
        private static string frommailpassword = "";
        private static int emailport = 0;
        private static string hostname = "";


        private static void setEmail()
        {
            SqlConnection con = null;
            //SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "select emailid,[password],port,hostname from EmailSetting;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        frommail = reader["emailid"].ToString();
                        frommailpassword = reader["password"].ToString();
                        emailport = Convert.ToInt32(reader["port"]);
                        hostname = reader["hostname"].ToString();
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Log.Error("SendEmail.setEmail", ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public static void sendEmailToClient(string useremail, string emailbody, string emailsubject)
        {
            try
            {
                setEmail();
                if (useremail.Contains("@") && useremail.Length > 0 && useremail != "-")
                {

                    if (frommail.Length > 0)
                    {


                        MailMessage mailmessage = new MailMessage(frommail, useremail);
                        mailmessage.Subject = emailsubject;
                        mailmessage.Body = emailbody;
                        mailmessage.IsBodyHtml = true;

                        SmtpClient smtpclient = new SmtpClient(hostname, emailport);

                        smtpclient.UseDefaultCredentials = false;
                        smtpclient.Credentials = new System.Net.NetworkCredential()
                        {
                            UserName = frommail,
                            Password = frommailpassword
                        };
                        smtpclient.EnableSsl = true;

                        smtpclient.Send(mailmessage);
                    }
                    else
                    {
                        throw new Exception("Sender's Email ID Not set for the system.");
                    }


                }

            }
            catch (Exception ex)
            {
                Log.Error("SendEmail.sendEmailToClient", ex);
            }
        }


        //public static void sendEmailToClientMailKit(string useremail,string emailbody,string emailsubject)
        //{
        //    try
        //    {
        //        setEmail();

        //        if (useremail.Contains("@") && useremail.Length > 0 && useremail != "-")
        //        {

        //            if (frommail.Length > 0)
        //            {
        //                var email = new MimeMessage();
        //                email.From.Add(new MailboxAddress("Sender", useremail));
        //                email.To.Add(new MailboxAddress("Receiver", frommail));

        //                email.Subject = emailsubject;
        //                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        //                {
        //                    Text= emailbody
        //                };

        //                using(var smtp=new MailKit.Net.Smtp.SmtpClient())
        //                {
        //                    smtp.Connect(hostname, emailport, true);
        //                    smtp.Authenticate(frommail, frommailpassword);
        //                    smtp.Send(email);
        //                    smtp.Disconnect(true);
        //                }


        //            }
        //            else
        //            {
        //                throw new Exception("Sender's Email ID Not set for the system.");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("SendEmail.sendEmailToClientMailKit", ex);
        //    }
        //}

    }
}