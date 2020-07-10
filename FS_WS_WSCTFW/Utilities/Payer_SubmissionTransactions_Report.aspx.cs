using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace FS_WS_WSCTFW.Utilities
{
    public partial class Payer_SubmissionTransactions_Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                DTEnd.SelectedDate = DateTime.Now;
                DTStart.SelectedDate = DateTime.Now;
                txtLog.Enabled = false;
            }

        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                txtLog.Text = "";
                if (txtPayerLicense.Text.Length > 0)
                {




                    string SQlQuery = "SELECT  [SenderID] 	  ";
                    SQlQuery += "   ,(select name  from Provider p where st.SenderID = p.LicenseID ) as ProviderName       ";
                    SQlQuery += ",[ReceiverID]      ,[TransactionDate]       ,[RecordCount] as NumberofClaims ";
                    SQlQuery += "   ,[CreationDate]    ,[FileName]     ,[DownloadedDate] ";
                    SQlQuery += " FROM [DHPO].[dbo].[SubmissionTransactions] ST ";
                    SQlQuery += "  where ReceiverID = '" + txtPayerLicense.Text + "'  and creationdate between '";
                    SQlQuery += DTStart.SelectedDate.ToString("yyyy-MM-dd");
                    SQlQuery += "' and '";
                    SQlQuery += DTEnd.SelectedDate.ToString("yyyy-MM-dd"); 
                    SQlQuery += "'   order by CreationDate desc";

                    FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(SQlQuery, "eHDF", "Payer_SubmissionTransactions_Report_" + txtPayerLicense.Text, "DHPO", 2);

                    txtLog.Text += " Query file executed successfully  and Results will be email to you shortly !!!" + Environment.NewLine + " -- Note: there will be no emails in case of result file size greater than 7 MB !!! ";
                        



                    //  string useremail = Helpers.EmailHelper.getUsernamefromEmail(User.Identity.Name.ToString());


                    //   string filename = useremail +"_" +DTStart.SelectedDate.Day.ToString() + "_" + DTStart.SelectedDate.Month.ToString()+ "_" + DTStart.SelectedDate.Year.ToString()+"_" + DTEnd.SelectedDate.Day.ToString() + "_" + DTEnd.SelectedDate.Month.ToString()+ "_" + DTEnd.SelectedDate.Year.ToString() +"_"+DateTime.Now.TimeOfDay.ToString("hhmmss") + "_" + txtPayerLicense.Text ;

                    //    {
                    //        if (SQlQuery.Trim().Length > 1)
                    //       {

                    //           string path = System.Configuration.ConfigurationManager.AppSettings["PayerSubmissionReportPath"];
                    //          string pathforTemp = System.Configuration.ConfigurationManager.AppSettings["BatchFileGenerationPath"];
                    //  path = path ;


                    //using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(Server.MapPath("~/Utilities/ClinicianUpdateScript/Script.SQL"), false))

                    //           if (Helpers.BatchFIleCaller.SaveAnyFile(filename, SQlQuery, path, false, "SQL"))
                    //           {

                    //               string batfileContent = Helpers.BatchFIleCaller.GenerateSQLCMDCommand(1,"10.162.176.24", "DHPO", "fshaikh", "Dell@777", path + filename , Helpers.BatchFIleCaller.getEmailAttachmentPath() + filename, "" );


                    //               if (Helpers.BatchFIleCaller.SaveAnyFile(filename, batfileContent, path, false, "BAT"))
                    //            {

                    //              Helpers.BatchFIleCaller.ExecuteBatchFileeHDF(path + filename + ".BAT");
                    //                 txtLog.Text += " Query file executed successfully  and Results will be email to you shortly !!!" + Environment.NewLine + " -- Note: there will be no emails in case of result file size greater than 7 MB !!! ";
                    //              }
                    //}
                    //          else
                    //          {

                    //              txtLog.Text = "Error Saving File !!!!!!!!!!!!!!!!!!";
                    //
                    //            }
                    //        }
                    //    }

                }
                else
                {
                    txtLog.Text = "Please provide valid License!!!";
                }
            }
            catch (Exception)
            {

                throw;
            }
          
        }
    }
}