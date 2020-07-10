using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FS_WS_WSCTFW.Helpers
{
    public class BatchProcess
    {
        public static bool createBatchProcess(string fileContent, string VPNName, string ProviderLicense, string ProcessIDentifier, int SQLCMDType)
        {

            try
            {
                
                string OutputResult = "";
                string BatchPath = BatchFIleCaller.getBatchFileGenerationPath();
                OutputResult += Environment.NewLine + BatchPath;

                string filename = BatchFIleCaller.getFilnameforBatchFile("techsupport_", ProviderLicense, ProcessIDentifier);
                OutputResult += Environment.NewLine + filename;
                string fullFileName = BatchPath + "\\" + filename;
                string outputfilename = System.Configuration.ConfigurationManager.AppSettings["EmailReportPath"] + filename;
                OutputResult += Environment.NewLine + outputfilename;

                if (Helpers.BatchFIleCaller.SaveAnyFile(filename, fileContent, BatchPath, false, "SQL"))
                {


                    string server = ClinicianAutomation.ExtraClasses.Connections.run_singlevalue("ERX", "server");
                    string DB = ClinicianAutomation.ExtraClasses.Connections.run_singlevalue("ERX", "database");
                    string username = ClinicianAutomation.ExtraClasses.Connections.run_singlevalue("ERX", "username");
                    string password = ClinicianAutomation.ExtraClasses.Connections.run_singlevalue("ERX", "password");
                    string sqlcmd = Helpers.BatchFIleCaller.GenerateSQLCMDCommand(SQLCMDType, server, DB, username, password, fullFileName, outputfilename, ProcessIDentifier);
                    //string sqlcmd = Helpers.BatchFIleCaller.GenerateSQLCMDCommand(SQLCMDType, "10.156.62.42", "erx", "fazeel", "Dell@123", fullFileName, outputfilename, ProcessIDentifier);


                    OutputResult += Environment.NewLine + sqlcmd;

                    if (Helpers.BatchFIleCaller.SaveAnyFile(filename, sqlcmd, BatchPath, false, "BAT"))
                    {
                        OutputResult += Helpers.BatchFIleCaller.BatchfileExecutorusingVPN(fullFileName + ".BAT", VPNName);
                         FS_WS_WSCTFW.Helpers.BatchFIleCaller.InsertMLog("Generate SQL-BAT Files", "TechSupport_",BatchPath+"\\"+filename ,fileContent + "--" + Environment.NewLine +  sqlcmd,"1",""  );
                    }

                }
                Logger.Info(OutputResult);
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