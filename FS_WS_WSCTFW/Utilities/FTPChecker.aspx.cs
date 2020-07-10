using System;
using System.IO;
using System.Net;
using WinSCP;
using Microsoft.AspNet.Identity;
using System.Linq;


namespace FS_WS_WSCTFW.Utilities
{
    public partial class FTPChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            //   btnFTPCaller_Click(sender, e);
        }

        protected void btnFTPCaller_Click(object sender, EventArgs e)
        {

            txtFTPLog.Text = "";

            string ftpServerIP = System.Configuration.ConfigurationManager.AppSettings["FTPHost"];
            string ftpUserName = System.Configuration.ConfigurationManager.AppSettings["FTPUsername"];
            string ftpPassword = System.Configuration.ConfigurationManager.AppSettings["FTPPassword"];
            string FTPLocalPath = System.Configuration.ConfigurationManager.AppSettings["FTPLocalPath"];

            Session FTPConnection = Helpers.FTPHelper.winSCPConnection(ftpServerIP, ftpUserName, ftpPassword);

            if (FTPConnection != null)
            {
                txtFTPLog.Text += Helpers.FTPHelper.GetFiles(FTPConnection, "/", FTPLocalPath);
                txtFTPLog.Text += Helpers.FTPHelper.CreateDirectory(FTPConnection, "/", "CheckFTP");
                txtFTPLog.Text += Helpers.FTPHelper.GetDirectoryListing(FTPConnection, "/CheckFTP");
                txtFTPLog.Text += Helpers.FTPHelper.UploadFiles(FTPConnection, "/CheckFTP/", FTPLocalPath + "\\*");
                txtFTPLog.Text += Helpers.FTPHelper.GetFiles(FTPConnection, "/CheckFTP/", FTPLocalPath);
             //   txtFTPLog.Text += Helpers.FTPHelper.DeleteFile(FTPConnection, "/CheckFTP/", "2.jpg");
                txtFTPLog.Text += Helpers.FTPHelper.DeleteFolder(FTPConnection, "/", "CheckFTP/", true);
                FTPConnection.Close();
            }

            else
            {
                txtFTPLog.Text = "FTP Not Connected !!!";
        } }
          

        protected void btnFTPRestart_Click(object sender, EventArgs e)
        {
            string userid = User.Identity.GetUserId();


            txtFTPLog.Text = "";
            //txtStatus.Text = Helpers.BatchFIleCaller.CallBatchFile( userid, "PBMSWITCH-PROD");
            txtFTPLog.Text = Helpers.BatchFIleCaller.CallBatchFile(userid, "FTP");
            if (txtFTPLog.Text != "")
            {
                int StatusCode = int.Parse(txtFTPLog.Text.Split('\n').Reverse().Take(1).ToArray()[0]);

                if (StatusCode == 0)
                {
                    txtFTPLog.Text += Environment.NewLine;
                   txtFTPLog.Text += "SUCCESS";
                    txtFTPLog.BackColor = System.Drawing.Color.Green;
                    txtFTPLog.Text += Environment.NewLine;
                }
                else
                {
                    txtFTPLog.Text += Environment.NewLine;
                    txtFTPLog.Text += "FAILED";
                    txtFTPLog.BackColor = System.Drawing.Color.Red;
                    txtFTPLog.Text += Environment.NewLine;

                }
            }
            else
            {

            }
        }
    }
}
