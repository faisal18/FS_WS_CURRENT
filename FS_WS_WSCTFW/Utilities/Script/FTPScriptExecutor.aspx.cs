using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WinSCP;
using System.ServiceProcess;


namespace FS_WS_WSCTFW.Utilities.Script
{
    public partial class FTPScriptExecutor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                ReStartService("Spooler");

                txtFTPLog.Text = "";
                bool isFailed = false;
                string ftpServerIP = System.Configuration.ConfigurationManager.AppSettings["FTPHost"];
                string ftpUserName = System.Configuration.ConfigurationManager.AppSettings["FTPUsername"];
                string ftpPassword = System.Configuration.ConfigurationManager.AppSettings["FTPPassword"];
                string FTPLocalPath = System.Configuration.ConfigurationManager.AppSettings["FTPLocalPath"];
                string FTPRetryCount = System.Configuration.ConfigurationManager.AppSettings["FTPConnectionRetryCount"];
                string FTPInterval = System.Configuration.ConfigurationManager.AppSettings["FTPRetryIntervalinMilliseconds"];


                txtFTPLog.Text += Environment.NewLine;




                for (int i = 0; i < int.Parse(FTPRetryCount); i++)
                {
                    txtFTPLog.Text += Environment.NewLine + "......................................";
                    txtFTPLog.Text += Environment.NewLine + " Connection Attempt -- " + i + "  ...  ";


                    Session FTPConnection = Helpers.FTPHelper.winSCPConnection(ftpServerIP, ftpUserName, ftpPassword);

                    if (FTPConnection == null)
                    {

                        txtFTPLog.Text += Environment.NewLine;
                        txtFTPLog.Text += " !!! Failed !!!";
                        txtFTPLog.Text += Environment.NewLine;

                        System.Threading.Thread.Sleep(int.Parse(FTPInterval));
                        isFailed = true;

                    }
                    else
                    {
                        txtFTPLog.Text += Environment.NewLine;
                        txtFTPLog.Text += "--- Success ---";
                        isFailed = false;
                        break;
                    }
                }
                if (isFailed)

                {
                    txtFTPLog.Text += Environment.NewLine + "--------------------Restarting  FTP Service ---------------------- ";
                    txtFTPLog.Text += Environment.NewLine + Helpers.BatchFIleCaller.CallBatchFile("FTP_Restarter", "FTP");
                    //  Helpers.BatchFIleCaller.InsertMLog("FTPScriptExecution", "FTPSCRIPT Executor", "FTP Script", "", "-1", txtFTPLog.Text);
                    ReStartService("Spooler");
                }
                else
                {
                    Helpers.BatchFIleCaller.InsertMLog("FTPScriptExecution", "FTPSCRIPT Executor", "FTP Script", txtFTPLog.Text, "0", "");
                }
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
            }




        }



        public static bool ReStartService ( string ServiceName )
        {
            ServiceController myService = new ServiceController();
            myService.ServiceName = ServiceName;

            myService.Stop();
            myService.WaitForStatus(ServiceControllerStatus.Stopped);

            myService.Start();
            myService.WaitForStatus(ServiceControllerStatus.Running);


            //string svcStatus = myService.Status.ToString();
            //if (svcStatus == "Running")
            //{
            //    myService.Stop();
            //}
            //else if (svcStatus == "Stopped")
            //{
            //    myService.Start();
            //}
            //else
            //{
            //    myService.Stop();
            //}




            return false;

        }


    }



}

//txtFTPLog.Text = Helpers.FTPHelper.CreateDirectory(FTPConnection, "/", "CheckFTP");
//txtFTPLog.Text = Helpers.FTPHelper.GetDirectoryListing(FTPConnection, "/CheckFTP");
//txtFTPLog.Text += Helpers.FTPHelper.UploadFiles(FTPConnection, "/CheckFTP/", FTPLocalPath + "\\*");
//txtFTPLog.Text += Helpers.FTPHelper.GetFiles(FTPConnection, "/CheckFTP/", FTPLocalPath);
//txtFTPLog.Text += Helpers.FTPHelper.DeleteFile(FTPConnection, "/CheckFTP/", "2.jpg");
//txtFTPLog.Text += Helpers.FTPHelper.DeleteFolder(FTPConnection, "/", "CheckFTP/", true);
//FTPConnection.Close();






