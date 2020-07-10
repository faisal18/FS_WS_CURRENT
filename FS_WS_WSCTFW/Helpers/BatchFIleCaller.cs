using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;



namespace FS_WS_WSCTFW.Helpers
{
    class BatchFIleCaller
    {



        public BatchFIleCaller()
        {



        }

        public static string getBatchFileGenerationPath()
        {
            try
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["BatchFileGenerationPath"];

                return path;
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                return null;
            }

        }

        public static string getFilnameforBatchFile (string Username = "techsupport_", string ProviderLicense = "", string providerUsername = "")
        {
            string filename = Username + "_" + ProviderLicense + "_" + providerUsername + "_" + DateTime.Now.ToString("yyMMdd-hhmmss");

            return filename;

        }
        public static string getEmailAttachmentPath()
        {
            try
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["EmailReportPath"];

                return path;
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                return null;
            }

        }

        public static string CallBatchFile(string username, string appName)
        {
            try
            {


                Hashtable filePaths = new Hashtable();
                filePaths.Add("PBMSWITCH-Production", @"PBMSwitch-Prod.Bat");
                filePaths.Add("PBMSWITCH-UAT", @"PBMSwitch-UAT.Bat");
                filePaths.Add("PBMLink-Production", @"PBMLink-Prod.Bat");
                filePaths.Add("PBMLink-UAT", @"PBMLink-UAT.Bat");
                filePaths.Add("DHPO-Web-1-Prod", @"DHPO-Prod-Web-1.Bat");
                filePaths.Add("DHPO-Web-2-Prod", @"DHPO-Prod-Web-2.Bat");
                filePaths.Add("DHPO-DB", @"DHPO-Prod-DB.Bat");
                filePaths.Add("eRxApps-UAT", @"eRxApps-UAT.Bat");
                filePaths.Add("eRxApps-Prod", @"eRxApps-Prod.Bat");
                filePaths.Add("CEED-Production", @"CEED-Prod.Bat");
                filePaths.Add("CEED-UAT", @"CEED-UAT.Bat");
                filePaths.Add("eClaimParser-Production", @"eClaimParser-Prod.Bat");
                filePaths.Add("eClaimParser-UAT", @"eClaimParser-UAT.Bat");
                filePaths.Add("ClearingHouse-eClaimExpress-Production", @"ClearingHouse-eClaimExpress-Prod.Bat");
                filePaths.Add("ClearingHouse-eClaimExpress-UAT", @"ClearingHouse-eClaimExpress-UAT.Bat");
                filePaths.Add("LMU-Production", @"LMU-Prod.Bat");
                filePaths.Add("LMU-UAT", @"LMU-UAT.Bat");
                filePaths.Add("eClaimlink-Prod", @"eClaimlink-Prod.Bat");
                filePaths.Add("eClaimlink-UAT", @"eClaimlink-UAT.Bat");
                filePaths.Add("FTP", @"FTP.Bat");
                filePaths.Add("RSA", @"Asp_RSA.Bat");
                filePaths.Add("UNRSA", @"Asp_UNRSA.Bat");
                string executionDetails = DateTime.Now + " - Restarting Service for " + appName;

                //  string path = @"..\Utilities\PBMSi";
                //string path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + @"\FS_WS_WSCTFW\Utilities\ServerMaintenance\Executor\";
                string path = System.Configuration.ConfigurationManager.AppSettings["BatchPath"];
                path = path + filePaths[appName];
                //   path = path.Substring(path.Length - path.Length + 6);
                //Helpers.Logger.Info(path);
                var processInfo = new ProcessStartInfo(path, "/c ");

                //path.Substring(path.Length-7)
                processInfo.CreateNoWindow = false;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardOutput = true;

                var process = Process.Start(processInfo);


                executionDetails += Environment.NewLine;

                process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                     executionDetails += Environment.NewLine + e.Data;
                process.BeginOutputReadLine();

                process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                          executionDetails += Environment.NewLine + e.Data;
                process.BeginErrorReadLine();

                process.WaitForExit();

                //  Console.WriteLine("ExitCode: {0}", process.ExitCode);
                if (process.ExitCode == 0 && !executionDetails.Contains("disabled") && executionDetails.Contains("STATE		  : 4  RUNNING"))
                {
                    executionDetails = executionDetails + Environment.NewLine + DateTime.Now + " - Successfully Restarted \n" + process.ExitCode.ToString();
                    InsertMLog(appName, username, path, executionDetails, process.ExitCode.ToString(), "");
                    //Helpers.EmailHelper.SendEmailtoSpecific("[" + appName + "]" + "_[" + username + "]_" , path + Environment.NewLine+ executionDetails);
                }
                else
                {
                    executionDetails = executionDetails + Environment.NewLine + DateTime.Now + " - Error in Executing Command, Please contact administrator now. \n " + process.ExitCode.ToString();

                    InsertMLog(appName, username, path, "", process.ExitCode.ToString(), executionDetails);
                    //Helpers.EmailHelper.SendEmailtoSpecific("[" + appName + "]" + "_[" + username + "]_", path + Environment.NewLine + executionDetails);
                }


                process.Close();



                return executionDetails;

            }
            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
                return null;
            }

        }



        public static string CallBatchReader(string username, string appName)
        {
            try
            {


                Hashtable filePaths = new Hashtable();
                filePaths.Add("PBMSWITCH-Production", @"PBMSwitch-Prod.Bat");
                filePaths.Add("PBMSWITCH-UAT", @"PBMSwitch-UAT.Bat");
                filePaths.Add("PBMLink-Production", @"PBMLink-Prod.Bat");
                filePaths.Add("PBMLink-UAT", @"PBMLink-UAT.Bat");
                filePaths.Add("DHPO-Web-1-Prod", @"DHPO-Prod-Web-1.Bat");
                filePaths.Add("DHPO-Web-2-Prod", @"DHPO-Prod-Web-2.Bat");
                filePaths.Add("eRxApps-UAT", @"eRxApps-UAT.Bat");
                filePaths.Add("eRxApps-Prod", @"eRxApps-Prod.Bat");
                filePaths.Add("CEED-Production", @"CEED-Prod.Bat");
                filePaths.Add("CEED-UAT", @"CEED-UAT.Bat");
                filePaths.Add("eClaimParser-Production", @"eClaimParser-Prod.Bat");
                filePaths.Add("eClaimParser-UAT", @"eClaimParser-UAT.Bat");
                filePaths.Add("ClearingHouse-eClaimExpress-Production", @"ClearingHouse-eClaimExpress-Prod.Bat");
                filePaths.Add("ClearingHouse-eClaimExpress-UAT", @"ClearingHouse-eClaimExpress-UAT.Bat");
                filePaths.Add("LMU-Production", @"LMU-Prod.Bat");
                filePaths.Add("LMU-UAT", @"LMU-UAT.Bat");
                filePaths.Add("eClaimlink-Prod", @"eClaimlink-Prod.Bat");
                filePaths.Add("eClaimlink-UAT", @"eClaimlink-UAT.Bat");
                filePaths.Add("FTP", @"FTP.Bat");
                string executionDetails = DateTime.Now + " - Restarting Service for " + appName;

                //  string path = @"..\Utilities\PBMSi";
                //string path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + @"\FS_WS_WSCTFW\Utilities\ServerMaintenance\Executor\";
                string path = System.Configuration.ConfigurationManager.AppSettings["BatchPath"];
                path = path + filePaths[appName];






                // Create the ProcessInfo object
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("cmd.exe", " /c " + path + " ");
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardInput = true;
                psi.RedirectStandardError = true;
                psi.WorkingDirectory = "c:\\tmp\\";
                psi.Arguments = "/user:Administrator \"cmd /K \"";

                // Start the process
                System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi);



                // Open the batch file for reading
                System.IO.StreamReader strm = System.IO.File.OpenText(path);


                // Attach the output for reading
                System.IO.StreamReader sOut = proc.StandardOutput;


                // Attach the in for writing
                System.IO.StreamWriter sIn = proc.StandardInput;



                // Write each line of the batch file to standard input
                while (strm.Peek() != -1)
                {
                    sIn.WriteLine(strm.ReadLine());
                }


                strm.Close();


                // Exit CMD.EXE
                //      string stEchoFmt = "#{0} run successfully. Exiting";


                //         sIn.WriteLine(String.Format(stEchoFmt, path));
                sIn.WriteLine("EXIT");


                // Close the process
                proc.Close();


                // Read the sOut to a string.
                string results = sOut.ReadToEnd().Trim();



                // Close the io Streams;
                sIn.Close();
                sOut.Close();



                // Write out the results.
                //  string fmtStdOut = "< font face = courier size = 0 >{ 0}</ font >";
                //     this.Response.Write(String.Format(fmtStdOut, results.Replace(System.Environment.NewLine, “< br >”)));


                executionDetails += Environment.NewLine;
                executionDetails += results;


                executionDetails += Environment.NewLine;
                executionDetails += "1";



                if (executionDetails.Contains("STATE		  : 4  RUNNING"))
                {
                    executionDetails = executionDetails + Environment.NewLine + DateTime.Now + " - Successfully Restarted \n" + "1";
                    InsertMLog(appName, username, path, executionDetails, "1", "");
                }
                else
                {
                    executionDetails = executionDetails + Environment.NewLine + DateTime.Now + " - Error in Executing Command, Please contact administrator now. \n " + "0";

                    InsertMLog(appName, username, path, "", "0", executionDetails);
                }



















                return DeleteLines(executionDetails, 5, false);

            }
            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
                return null;
            }

        }



        public static void InsertMLog(string appName, string username, string Apppath, string logDetails, string logStatus, string ErrorDetails)
        {
            try
            {
                Models.MaintenanceLogging Mlog = new Models.MaintenanceLogging();

                Mlog.ApplicationName = appName;
                Mlog.CreatedBy = username;
                Mlog.AppPath = Apppath;
                Mlog.CreatedDate = DateTime.Now;
                Mlog.MaintenanceLogDetails = logDetails;
                Mlog.Status = logStatus;
                Mlog.ErrorDetails = ErrorDetails;



                Models.ApplicationDbContext db = new Models.ApplicationDbContext();

                db.MLogging.Add(Mlog);
                int result = db.SaveChanges();

                Helpers.EmailHelper.SendEmailtoSpecific("Techsupport_[" + appName + "]" + "_[" + username + "]_" + logStatus, Apppath + Environment.NewLine + logDetails + ErrorDetails);
                Logger.Info("InsertMLog" + Environment.NewLine + logDetails);
            }
            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
                Helpers.EmailHelper.SendEmailtoSpecific("Techsupport_ERROR OCCURED in application: [" + appName + "]" + "_[" + username + "]_" + logStatus, Apppath + Environment.NewLine + logDetails + ErrorDetails);

            }

        }


        public static string DeleteLines(string stringToRemoveLinesFrom, int numberOfLinesToRemove, bool startFromBottom = false)
        {
            string toReturn = "";
            string[] allLines = stringToRemoveLinesFrom.Split(
                    separator: Environment.NewLine.ToCharArray(),
                    options: StringSplitOptions.RemoveEmptyEntries);
            if (startFromBottom)
                toReturn = String.Join(Environment.NewLine, allLines.Take(allLines.Length - numberOfLinesToRemove));
            else
                toReturn = String.Join(Environment.NewLine, allLines.Skip(numberOfLinesToRemove));
            return toReturn;
        }



        public static bool SaveAnyFile(string filename, string FileContent, string Fullpath, bool append, string fileextension)
        {

            try
            {
                string path = Fullpath + filename + "." + fileextension;
                bool FileResults = false;



                if (FileContent.Length > 0 && filename.Length > 0 && Fullpath.Length > 0 && fileextension.Length > 0)
                {



                    if (!System.IO.File.Exists(path))
                    {
                        using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(path, append))
                        {
                            _testData.WriteLine(FileContent); // Write the file.
                            _testData.Close();
                            _testData.Dispose();
                        }

                        FileResults = true;
                    }

                }
                else
                {
                    FileResults = false;
                }
                

                return FileResults;

            }
            catch (Exception)
            {

                return false;

            }
        }

        public static string BatchfileExecutorusingVPN(string filename, string VPNName)
        {
            try
            {
                string Results = "";
                Results += Environment.NewLine;



                if (VPNName == "" || VPNName == string.Empty || VPNName == null)
                {



                }
                else
                  if (VPNName.ToUpper (System.Globalization.CultureInfo.CurrentCulture) == "ALL" || VPNName == "All" || VPNName == "all")
                {
                    Results = ExecuteBatchFileAll(filename);

                }
                else
                 if (VPNName == "cloud" || VPNName == "CLOUD" || VPNName == "Cloud")
                {
                    Results = ExecuteBatchFileCloud(filename);

                }
                else
                  if (VPNName == "HyperV" || VPNName == "Hyperv" || VPNName == "hyperv")
                {
                    Results = ExecuteBatchFileHyperV(filename);
                }

                else
                 if (VPNName == "ehdf" || VPNName == "eHDF" || VPNName == "EHDF")
                {

                    Results = ExecuteBatchFileeHDF(filename);
                }
                else
                {
                    Results = ExecuteBatchFile(filename);
                }

                InsertMLog("BatchfileExecutorusingVPN", VPNName, filename, Results, "1", "0");

                return Results;
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                return null;
            }

        }


        public static string ExecuteBatchFile(string filename)
        {
            try
            {



                string executionDetails = DateTime.Now + " - Executing Service !!!!!! ";

                //  string path = @"..\Utilities\PBMSi";
                //string path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + @"\FS_WS_WSCTFW\Utilities\ServerMaintenance\Executor\";
                //string path = System.Configuration.ConfigurationManager.AppSettings["BatchPath"];
                //path = path + filePaths[appName];

                string path = filename;

                //   path = path.Substring(path.Length - path.Length + 6);
                //Helpers.Logger.Info(path);
                var processInfo = new ProcessStartInfo(path, "/c ");

                //path.Substring(path.Length-7)
                processInfo.CreateNoWindow = false;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardOutput = true;

                var process = Process.Start(processInfo);


                executionDetails += Environment.NewLine;

                process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                     executionDetails += Environment.NewLine + e.Data;
                process.BeginOutputReadLine();

                process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                          executionDetails += Environment.NewLine + e.Data;
                process.BeginErrorReadLine();

                process.WaitForExit();

                ////  Console.WriteLine("ExitCode: {0}", process.ExitCode);
                //if (process.ExitCode == 0 && !executionDetails.Contains("disabled") && executionDetails.Contains("STATE		  : 4  RUNNING"))
                //{
                //    executionDetails = executionDetails + Environment.NewLine + DateTime.Now + " - Successfully Restarted \n" + process.ExitCode.ToString();
                //}
                //else
                //{
                //    executionDetails = executionDetails + Environment.NewLine + DateTime.Now + " - Error in Executing Command, Please contact administrator now. \n " + process.ExitCode.ToString();

                //}


                process.Close();
                //    process.Kill();

                //       GC.Collect();
                Logger.Info(executionDetails);
                return executionDetails;

            }
            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
                return null;
            }

            finally
            {
                //    GC.Collect();
            }

        }

        
        public static string ExecuteBatchFileeHDF(string filename)
        {
            try
            {
                string outputfilename = System.Configuration.ConfigurationManager.AppSettings["eHDFVPNPath"];
                string Results = ExecuteBatchFile(outputfilename);
                Results += Environment.NewLine;
                Results += ExecuteBatchFile(filename);
                outputfilename = outputfilename.Substring(0, outputfilename.Length - 4);
                //outputfilename = outputfilename.Replace(".BAT", "");
                Results += ExecuteBatchFile(outputfilename + "_Disconnect.BAT");

                InsertMLog("ExecuteBatchFileeHDF",  "", filename, Results, "Success", "");


                return Results;
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                return null;
            }

        }

        public static string ExecuteBatchFileHyperV(string filename)
        {
            try
            {
                string outputfilename = System.Configuration.ConfigurationManager.AppSettings["HyperVVPNPath"];
                string Results = ExecuteBatchFile(outputfilename);
                Results += Environment.NewLine;
                Results += ExecuteBatchFile(filename);
                outputfilename = outputfilename.Substring(0, outputfilename.Length - 4);
                //outputfilename = outputfilename.Replace(".BAT", "");
                Results += ExecuteBatchFile(outputfilename + "_Disconnect.BAT");



                return Results;
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                return null;
            }

        }

        public static string ExecuteBatchFileCloud(string filename)
        {
            try
            {
                string outputfilename = System.Configuration.ConfigurationManager.AppSettings["CloudVPNPath"];
                string Results = ExecuteBatchFile(outputfilename);
                Results += Environment.NewLine;
                Results += ExecuteBatchFile(filename);
                outputfilename = outputfilename.Substring(0, outputfilename.Length - 4);
                //outputfilename = outputfilename.Replace(".BAT", "");
                Results += ExecuteBatchFile(outputfilename + "_Disconnect.BAT");



                return Results;
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                return null;
            }

        }


        public static string ExecuteBatchFileAll(string filename)
        {
            try
            {
                string Results = "";
                string eHdffilename = "";
                string HyperVfilename = "";
                string Cloudfilename = "";

                eHdffilename = System.Configuration.ConfigurationManager.AppSettings["eHDFVPNPath"];
                HyperVfilename = System.Configuration.ConfigurationManager.AppSettings["HyperVVPNPath"];
                Cloudfilename = System.Configuration.ConfigurationManager.AppSettings["CloudVPNPath"];


                //Results += ExecuteBatchFile(eHdffilename);
                //Results += Environment.NewLine;
                //Results += ExecuteBatchFile(HyperVfilename);
                //Results += Environment.NewLine;
                //Results += ExecuteBatchFile(Cloudfilename);


                Results += Environment.NewLine;
                Results += ExecuteBatchFile(filename);
                Results += Environment.NewLine;

                eHdffilename = eHdffilename.Substring(0, eHdffilename.Length - 4);
                HyperVfilename = HyperVfilename.Substring(0, HyperVfilename.Length - 4);
                Cloudfilename = Cloudfilename.Substring(0, Cloudfilename.Length - 4);
                //outputfilename = outputfilename.Replace(".BAT", "");
                //Results += ExecuteBatchFile(eHdffilename + "_Disconnect.BAT");
                //Results += Environment.NewLine;
                //Results += ExecuteBatchFile(HyperVfilename + "_Disconnect.BAT");
                //Results += Environment.NewLine;
                //Results += ExecuteBatchFile(Cloudfilename + "_Disconnect.BAT");
                //Results += Environment.NewLine;

                

                return Results;
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
                return null;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CmdType"> 1  will check for all other parameters, 
        /// 2. for Connection object
        /// 3. connection details will be in SQL file</param>
        /// <param name="ServerName"></param>
        /// <param name="DBName"></param>
        /// <param name="usrname"></param>
        /// <param name="passwrd"></param>
        /// <param name="SQLFIleName"></param>
        /// <param name="OutPUtFIlename"></param>
        /// <returns></returns>
        public static string GenerateSQLCMDCommand(int CmdType, string ServerName, string DBName, string usrname, string passwrd, string SQLFIleName, string OutPUtFIlename, string ProcessIdentifier)
        {
            try
            {
                //sqlcmd -S DB-Server1 -d PbmPayer -i "SQL_Command_Automation_ADNIC_Monthly.sql" -o "Out/membersyyyyMMdd.csv" -W  -s"," -h-1
                // 
                string executionDetails = "sqlcmd  ";
                if (CmdType == 1)

                {
                    executionDetails += " -S " + ServerName;
                    executionDetails += " -d " + DBName;
                    executionDetails += " -U " + usrname;
                    executionDetails += " -P " + passwrd;
                    executionDetails += " -i \"" + SQLFIleName + ".SQL\"";
                    executionDetails += " -o \"" + OutPUtFIlename + ".CSV\"";
                    executionDetails += " -W " + "";
                    executionDetails += " -s" + "\"" + "," + "\"";
                    executionDetails += " -h-1 " + " ";
                    executionDetails += Environment.NewLine;
                }
                else if (CmdType == 2)
                {
                    ClinicianAutomation.ExtraClasses.Connections conn = new ClinicianAutomation.ExtraClasses.Connections();
                    //       string argument = String.Format("SQLCMD {0} -i \"{1}\" -o \"{2}\" -W -s,-h-1", conn.run_connection(ProcessIdentifier), path_file + ".sql", output_file);

                    executionDetails += conn.run_connection(ProcessIdentifier);

                    executionDetails += " -i \"" + SQLFIleName + ".SQL\"";
                    executionDetails += " -o \"" + OutPUtFIlename + ".CSV\"";
                    executionDetails += " -W " + "";
                    executionDetails += " -s" + "" + "," + "";
                    executionDetails += "-h-1 " + " ";
                    executionDetails += Environment.NewLine;

                }
                else if (CmdType == 3)
                {
                   

                    executionDetails += " -i \"" + SQLFIleName + ".SQL\"";
                    executionDetails += " -o \"" + OutPUtFIlename + ".CSV\"";
                    executionDetails += " -W " + "";
                    executionDetails += " -s" + "" + "," + "";
                    executionDetails += "-h-1 " + " ";
                    executionDetails += Environment.NewLine;

                }



                    return executionDetails;

            }
            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
                return null;
            }

        }


        public static bool deleteFile(string filename)

        {

            try
            {
                if (System.IO.File.Exists(filename))
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    System.IO.FileInfo f = new System.IO.FileInfo(filename);
                    f.Delete();
                    //     System.IO.File. Delete(filename);


                }

                return true;
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                return false;
            }


        }

        public static bool deleteFolder(string filename)

        {

            try
            {
                if (System.IO.Directory.Exists("C:\\tmp\\123"))
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    System.IO.DirectoryInfo d = new System.IO.DirectoryInfo("c:\\tmp\\123");
                    d.Delete(true);
                    //     System.IO.File. Delete(filename);


                }

                return true;
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                return false;
            }


        }

    }
}
