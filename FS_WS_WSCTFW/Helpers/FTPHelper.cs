using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSCP;
using System.IO;
using System.Net;

namespace FS_WS_WSCTFW.Helpers
{
    class FTPHelper
    {



        public static void FTPDownloader(string ftpSourceFilePath, string userName, string password, string localDestinationFilePath)
        {
            try
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];

                FtpWebRequest request = CreateFtpWebRequest(ftpSourceFilePath, userName, password, true);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                Stream reader = request.GetResponse().GetResponseStream();
                FileStream fileStream = new FileStream(localDestinationFilePath, FileMode.Create);

                while (true)
                {
                    bytesRead = reader.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }



        public static string FTPLister(string ftpSourceFilePath, string userName, string password, string localDestinationFilePath)
        {
            try
            {

                string txtLog = "";
                FtpWebRequest request = CreateFtpWebRequest(ftpSourceFilePath, userName, password, true);


                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;


                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);


                txtLog += txtLog + reader.ReadToEnd();

                txtLog += txtLog + "Directory List Complete " + response.StatusDescription;

                reader.Close();
                response.Close();

                return txtLog;
            }
            catch (Exception)
            {

                throw;
            }

        }



        public static FtpWebRequest CreateFtpWebRequest(string ftpDirectoryPath, string userName, string password, bool keepAlive = false)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpDirectoryPath));

                //Set proxy to null. Under current configuration if this option is not set then the proxy that is used will get an html response from the web content gateway (firewall monitoring system)
                request.Proxy = null;
                //   request.EnableSsl = true;
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = keepAlive;
                request.Timeout = 30000;
                request.Credentials = new NetworkCredential(userName, password);

                return request;
            }
            catch (Exception)
            {

                throw;
            }


        }


        public static Session winSCPConnection(string FTPHost, string FTPuserName, string FTPpassword)
        {
            try
            {


                SessionOptions newSession = new SessionOptions();
                newSession.FtpMode = FtpMode.Passive;
                newSession.Protocol = Protocol.Sftp;
                newSession.FtpSecure = FtpSecure.None;



                newSession.HostName = FTPHost;
                newSession.UserName = FTPuserName;
                newSession.Password = FTPpassword;
                newSession.GiveUpSecurityAndAcceptAnySshHostKey = true;



                Session newFTpSession = new WinSCP.Session();
                newFTpSession.Open(newSession);
               
                if(newFTpSession.Opened)
                {

                    return newFTpSession;

                }   
                else
                {

                    return null;
                }             
                
            }
            catch (Exception err)
            {
                Helpers.Logger.Error(err);
                return null;
            }


        }


        public static string GetDirectoryListing(Session FTPConnection, string DirectoryPath)
        {
            try
            {

                string fileName = "";


                RemoteDirectoryInfo newREmoteDirectoryInfo = FTPConnection.ListDirectory(DirectoryPath);


                foreach (RemoteFileInfo item in newREmoteDirectoryInfo.Files)
                {
                    fileName += item.FullName + Environment.NewLine;
                }



                return fileName;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public static string GetFiles(Session FTPConnection, string DirectoryPath, string LocalPath)
        {
            try
            {

                string fileName = "";

                TransferOptions tOptions = new TransferOptions();
                tOptions.TransferMode = TransferMode.Binary;
                tOptions.OverwriteMode = OverwriteMode.Overwrite;


                TransferOperationResult transferResult;
                transferResult = FTPConnection.GetFiles(DirectoryPath, LocalPath, false, tOptions);

                // Throw on any error
                transferResult.Check();

                // Print results
                foreach (TransferEventArgs transfer in transferResult.Transfers)
                {
                    fileName += Environment.NewLine;
                    fileName += "Download of " + transfer.FileName + " succeeded";

                }


                fileName += Environment.NewLine;
                return fileName;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public static string UploadFiles(Session FTPConnection, string DirectoryPath, string LocalPath)
        {
            try
            {

                string fileName = "";

                TransferOptions tOptions = new TransferOptions();
                tOptions.TransferMode = TransferMode.Binary;
                tOptions.OverwriteMode = OverwriteMode.Overwrite;


                TransferOperationResult transferResult;
                transferResult = FTPConnection.PutFiles(LocalPath, DirectoryPath, false, tOptions);

                // Throw on any error
                transferResult.Check();

                // Print results
                foreach (TransferEventArgs transfer in transferResult.Transfers)
                {
                    fileName += Environment.NewLine;
                    fileName += "Upload of " + transfer.FileName + " succeeded";

                }


                fileName += Environment.NewLine;
                return fileName;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public static string CreateDirectory(Session FTPConnection, string DirectoryPath, string FolderName)
        {
            try
            {

                string fileName = "";




                FTPConnection.CreateDirectory(DirectoryPath + FolderName);


                // Print results


                fileName += "Directory Created Successfully";
                fileName += Environment.NewLine;
                return fileName;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public static string DeleteFile(Session FTPConnection, string DirectoryPath, string FileNametoDelete)
        {
            try
            {

                string fileName = "";




                RemovalOperationResult FileREmove = FTPConnection.RemoveFiles(DirectoryPath + FileNametoDelete);

                if (FileREmove.IsSuccess)
                {
                    // Print results


                    fileName += "File Deleted Successfully";
                    fileName += Environment.NewLine;

                }
                else
                {
                    fileName += "File Deletion Failed";
                    fileName += Environment.NewLine;


                }

                return fileName;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public static string DeleteFolder(Session FTPConnection, string DirectoryPath, string FolderName, bool DeleteifNonEmpty)
        {
            try
            {

                string fileName = "";




                RemovalOperationResult FileREmove = FTPConnection.RemoveFiles(DirectoryPath + FolderName);

                if (FileREmove.IsSuccess)
                {
                    // Print results


                    fileName += "Folder Deleted Successfully";
                    fileName += Environment.NewLine;

                }
                else
                {
                    fileName += "File Deletion Failed";
                    fileName += Environment.NewLine;


                }

                return fileName;
            }
            catch (Exception)
            {

                throw;
            }


        }




























    }
}
