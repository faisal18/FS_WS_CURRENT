using System;
using System.Net.Mail;

namespace ClinicianAutomation.ExtraClasses
{
    public class EmailSender
    {

        public string subject(int a, string license)
        {
            string subj_cont;
            if (a == 1)
            {
                return subj_cont = "Clinician:" + license.ToString() + " has been updated";
            }
            else if (a == 0)
            {
                return subj_cont = "Clinician:" + license.ToString() + " is inActive and Updated";
            }
            else if (a == 3)
            {
                return subj_cont = "Clinician:" + license.ToString() + " exists and inActive";
            }
            else if (a == 4)
            {
                return subj_cont = "Clinician:" + license.ToString() + " exists and Active";
            }
            else if(a == 5)
            {
                return subj_cont = "Clinician:" + license.ToString() + " is OLD license";
            }
            else
                return "fail";
        }

        public string body(int a,string license,string username,string password)
        {
            string body_cont;
            if (a == 1)
            {
                return body_cont = "Dear Support,\n\nThe Clinician having license:" + license.ToString() + " Username:" + username.ToString() + " Password:" + password.ToString() + " has been updated\nThe time required to complete the process is 10-15minutes.";
            }
            else if (a == 0)
            {
                return body_cont = "Dear Support,\n\nThe Clinician having license:" + license.ToString() + " is inActive in LMU\nRegardless,It has been updated.\nThe time required to complete the process is 10-15minutes.";
            }
           
            else if (a == 3)
            {
                return body_cont = "Dear Support,\n\nThe Clinician having license:" + license.ToString() + " exist in eClaimLink.\nBut it is inActive in LMU.\nPlease update it in LMU";
            }
            else if (a == 4)
            {
                return body_cont = "Dear Support,\n\nThe Clinician having license:" + license.ToString() + " exist in eClaimLink,also, it is Active in LMU.Please ask them to hard reset their browser\nif they are not able to log in";
            }
            else if(a == 5)
            {
                return body_cont = "Dear Support,\n\nThe Clinician having license:" + license.ToString() + " is an OLD license and has been nullified.\nRun it after 5 minutes to update the credentials to new license ";
            }
            else
            {
                return "fail";
            }
        }

        public string SendEmail(int a1,string license1,string username1,string password1)
        {

            try
            {
                string body_cont = body(a1,license1,username1,password1);
                string subj_cont = subject(a1, license1);
               
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("fansari@dimensions-healthcare.com");
                mail.To.Add("fansari@dimensions-healthcare.com");
                
                mail.Subject = subj_cont.ToString();
                mail.Body = body_cont.ToString();
                
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("fansari@dimensions-healthcare.com", "cHEmiSTRY18IMS");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return "Succes";
              
            }

            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}