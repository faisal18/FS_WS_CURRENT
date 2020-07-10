using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace FS_WS_WSCTFW.Utilities.Script
{
    public partial class EmailScriptExecutor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string SMTPSendEmail = System.Configuration.ConfigurationManager.AppSettings["SMTPSendEmail"];

                int sendemailactive = int.Parse(SMTPSendEmail);

                //  bool emailSendingActive = Convert.ToBoolean(SMTPSendEmail);
                bool emailSendingActive = Convert.ToBoolean(sendemailactive);


                if (emailSendingActive == true)
                {
                    string path = System.Configuration.ConfigurationManager.AppSettings["EmailReportPath"];

                    if (Directory.Exists(path))
                    {

                        string[] Filenames = Directory.GetFiles(path);
                        if (Filenames.Length > 0)
                        {
                            System.Threading.Tasks.Parallel.ForEach(Filenames, (file) =>
                            // foreach (string file in Filenames)
                            {
                                string filenameonly = Path.GetFileName(file);
                                char[] dataChar = new char[] { '_' };
                                string[] filenameSplitter = filenameonly.Split(dataChar, StringSplitOptions.RemoveEmptyEntries);
                                if (filenameSplitter.Length > 0)
                                {

                                    string DimensionEmail = System.Configuration.ConfigurationManager.AppSettings["EmailDomain"];
                                    string emailAddress = filenameSplitter[0];
                                    emailAddress += DimensionEmail;

                                    string compressedPath = Helpers.CompressionHelper.CompressZip(file);
                                    File.Delete(file);



                                    if (Helpers.EmailHelper.SendEmailwithAttachment(filenameonly, compressedPath, emailAddress, compressedPath))
                                    {
                                        string newpath = System.Configuration.ConfigurationManager.AppSettings["EmailArchiveFolder"];
                                        if (Directory.Exists(newpath))
                                        {

                                        }
                                        else
                                        {
                                            Directory.CreateDirectory(newpath);
                                        }


                                        string sourceFIle = compressedPath;
                                        string destFile = newpath + Path.GetFileName(sourceFIle);
                                        File.Copy(sourceFIle, destFile, true);
                                    // Helpers.BatchFIleCaller.deleteFolder(sourceFIle);
                                    File.Delete(sourceFIle);
                                    //     File.Delete(file);






                                }



                                }


                            }
                            );
                        }
                        else
                        {
                            Helpers.Logger.Info(" !!! No Files found for Email !!! ");
                        }
                    }
                    else
                    {
                        Helpers.Logger.Error("---- Directory Does NOt Exist ---- " + path);
                    }

                    txtEmailLog.Text = "All Email Sent!!!!";
                }
                else
                {
                    txtEmailLog.Text = "Email cannot be sent, please check configurations. !!!!";
                }
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                //return null;
            }
        }
    }
}