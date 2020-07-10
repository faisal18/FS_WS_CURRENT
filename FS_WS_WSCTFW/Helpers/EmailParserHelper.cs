using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using EAGetMail; //add EAGetMail namespace
using System.Configuration;
using OpenPop.Pop3;


namespace FS_WS_WSCTFW.Helpers
{
    public class EmailParserHelper
    {
        public static void FetchEmail(string emailAddress, string emailPassword)
        {
            try
            {
                string popEmail = ConfigurationManager.AppSettings["POPEmail"];
                int FetchEmail = int.Parse(popEmail);
                bool getEmails = Convert.ToBoolean((FetchEmail));
            string POPCOnnectionServer = ConfigurationManager.AppSettings["POPCOnnectionServer"];
            string POPPort = ConfigurationManager.AppSettings["POPPort"];
            string POPUsername = ConfigurationManager.AppSettings["POPUsername"];
            string POPPassword = ConfigurationManager.AppSettings["POPPassword"];
            string POPInputFolder = ConfigurationManager.AppSettings["POPInputFolder"];
            string POPDefaultEmailAddress = ConfigurationManager.AppSettings["POPDefaultEmailAddress"];
            string emailtoCheck = "";
            string EmailtoCheckPassword = "";
            string[] EmailList = null;

            if (emailAddress == "")
            {
                emailtoCheck = POPDefaultEmailAddress;
                EmailtoCheckPassword = POPPassword;
            }
            else
            {
                emailtoCheck = emailAddress;
                EmailtoCheckPassword = emailPassword;
            }




            // Create a folder named "inbox" under current directory
            // to save the email retrieved.
            string curpath = Directory.GetCurrentDirectory();
            string mailbox = String.Format("{0}\\inbox", curpath);

            // If the folder is not existed, create it.
            if (!Directory.Exists(POPInputFolder))
            {
                Directory.CreateDirectory(POPInputFolder);
            }
 
            MailServer oServer = new MailServer(POPCOnnectionServer, emailtoCheck, EmailtoCheckPassword, ServerProtocol.Pop3);
            oServer.SSLConnection = true;
            oServer.Port = int.Parse(POPPort);
            MailClient oClient = new MailClient("TryIt");

            // If your POP3 server requires SSL connection,
            // Please add the following codes:
            // oServer.SSLConnection = true;
            // oServer.Port = 995;

           
                oClient.Connect(oServer);
                MailInfo[] infos = oClient.GetMailInfos();

                for (int i = 0; i < infos.Length; i++)
                {
                    MailInfo info = infos[i];
                    Mail oMail = oClient.GetMail(info);
                    Logger.Info(" Index: " + info.Index + "; Size: " + info.Size + "; UIDL: " + info.UIDL + "; From : " + oMail.From + ";  Subject : " + oMail.Subject+ ";  Date REceived  : " + oMail.ReceivedDate.ToString() + Environment.NewLine);
               
                    // Receive email from POP3 server
                  


                    //Console.WriteLine("From: {0}", oMail.From.ToString());
                    //Console.WriteLine("Subject: {0}\r\n", oMail.Subject);

                    // Generate an email file name based on date time.
                    //System.DateTime d = System.DateTime.Now;
                    //System.Globalization.CultureInfo cur = new
                    //    System.Globalization.CultureInfo("en-US");
                    //string sdate = d.ToString("yyyyMMddHHmmss", cur);
                    //string fileName = String.Format("{0}\\{1}{2}{3}.eml",
                    //    mailbox, sdate, d.Millisecond.ToString("d3"), i);

                    // Save email to local disk
                   // oMail.SaveAs(fileName, true);

                    // Mark email as deleted from POP3 server.
                    // oClient.Delete(info);
                  //  EmailList[i] = " Index: " + info.Index + "; Size: " + info.Size + "; UIDL: " + info.UIDL + "; From : " + oMail.From + ";  Subject : " + oMail.Subject + Environment.NewLine;



}

                    // Quit and purge emails marked as deleted from POP3 server.
                    oClient.Quit();

                    
                
              //  return EmailList;
            }
            catch (Exception ep)
            {
                FS_WS_WSCTFW.Helpers.Logger.Error(ep);
               // return null;
            }

        }

        public static void FetchEmailOpenPop (string EmailAdress, string emailPassword)
        {
            try
            {
                string popEmail = ConfigurationManager.AppSettings["POPEmail"];
                int FetchEmail = int.Parse(popEmail);
                bool getEmails = Convert.ToBoolean((FetchEmail));
                string POPCOnnectionServer = ConfigurationManager.AppSettings["POPCOnnectionServer"];
                string POPPort = ConfigurationManager.AppSettings["POPPort"];
                string POPUsername = ConfigurationManager.AppSettings["POPUsername"];
                string POPPassword = ConfigurationManager.AppSettings["POPPassword"];
                string POPInputFolder = ConfigurationManager.AppSettings["POPInputFolder"];
                string POPDefaultEmailAddress = ConfigurationManager.AppSettings["POPDefaultEmailAddress"];
                string emailtoCheck = "";
                string EmailtoCheckPassword = "";
                string[] EmailList = null;

                if (EmailAdress == "")
                {
                    emailtoCheck = POPDefaultEmailAddress;
                    EmailtoCheckPassword = POPPassword;
                }
                else
                {
                    emailtoCheck = EmailAdress;
                    EmailtoCheckPassword = emailPassword;
                }


                using (Pop3Client client = new Pop3Client())
                {
                    client.Connect(POPCOnnectionServer, int.Parse(POPPort), true); //For SSL                
                    client.Authenticate("recent:"+ emailtoCheck, EmailtoCheckPassword, AuthenticationMethod.UsernameAndPassword);

                    int messageCount = client.GetMessageCount();
                    for (int i = messageCount; i > 0; i--)
                    {

                        if (client.GetMessageHeaders(i).From.Address == "fshaikh@dimensions-healthcare.com" && client.GetMessageHeaders(i).Subject.ToUpper().StartsWith("TECHSUPPORT_"))
                        {
                            //Logger.Info(" From: " + (client.GetMessage(i).Headers.From).DisplayName);
                        }
                        else
                        {
                            List<OpenPop.Mime.Header.RfcMailAddress> ToEmailAddresses = new List<OpenPop.Mime.Header.RfcMailAddress>();

                            ToEmailAddresses.AddRange(client.GetMessageHeaders(i).To);
                            ToEmailAddresses.AddRange(client.GetMessageHeaders(i).Cc);
                            ToEmailAddresses.AddRange(client.GetMessageHeaders(i).Bcc);
                            string emailaddressto = "";
                            foreach (OpenPop.Mime.Header.RfcMailAddress email in ToEmailAddresses)
                            {
                                if (email.Address == "techsupport@dimensions-healthcare.com")
                                {
                                    emailaddressto += email.MailAddress + ";";
                                }
                            }

                            Logger.Info(" From: " + (client.GetMessage(i).Headers.From).DisplayName + "; Subject : " + client.GetMessage(i).Headers.Subject + ";  Date REceived  : " + client.GetMessage(i).Headers.DateSent + ";  To address: " + emailaddressto + Environment.NewLine);


                            //  table.Rows.Add(client.GetMessage(i).Headers.Subject, client.GetMessage(i).Headers.DateSent);
                            //string msdId = client.GetMessage(i).Headers.MessageId;
                            //OpenPop.Mime.Message msg = client.GetMessage(i);
                            //OpenPop.Mime.MessagePart plainTextPart = msg.FindFirstPlainTextVersion();
                            //string message = plainTextPart.GetBodyAsText();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

        }

    }
}