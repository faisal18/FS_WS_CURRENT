using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FS_WS_WSCTFW.Utilities
{
    public partial class BucketMappingGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            tbBackupTabName.Enabled = false;
            if (!IsCallback)
            {
                tbBackupTabName.Text = "DIAGNOSIS_LIST_FS_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_BAK";
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            txtGeneratedScript.Text = "";
            string sSql = "";
            if (chcktblBackup.Checked)
            {
                sSql = "\r\n select * into [PBMM_Data_Backup].[DBO].[" + tbBackupTabName.Text + "] from Diagnosis_list\r\n";

                txtGeneratedScript.Text = txtGeneratedScript.Text + " -- Backup Table before Execution the Script in Table  " + tbBackupTabName.Text + Environment.NewLine;
            }
            txtGeneratedScript.Text = txtGeneratedScript.Text + sSql;

            if (txtICDCodes.Text.Length < 1 || tbProvider.Text.Length < 1)
            {
                //     Response.Write("<Script> alert(' ICD or Provider or PCN values missing '); </ Script >");
                if (chckGeneral.Checked == false && tbPCN.Text.Length < 1)
                {
                    lblStatus.Text = "Please check Provider, PCN and ICD values. ";
                }
                else
                {
                    lblStatus.Text = "Please check Provider and ICD values. ";
                }
            }
            else
            {
                txtGeneratedScript.Text += Environment.NewLine;
                txtGeneratedScript.Text += "-- Payer ID : " + tbProvider.Text + Environment.NewLine;
                txtGeneratedScript.Text += "-- Type ID : " + tbType.Text + Environment.NewLine;
                txtGeneratedScript.Text += "-- PCN : " + tbPCN.Text + Environment.NewLine;
        //        txtGeneratedScript.Text += "Type ID : " + tbType.Text + Environment.NewLine;


                char[] dataChar = new char[] { '\r', '\n', ',', ' ' };

                string[] ICDCodes = ICDCodes = txtICDCodes.Text.Split(dataChar, StringSplitOptions.RemoveEmptyEntries);

                if (checkICDConversion.Checked == true && ICD10.Checked)
                {



                    txtGeneratedScript.Text = txtGeneratedScript.Text + "\r\n -- Total ICDs Provided in ICD10: " + ICDCodes.Length;

                    string[] ICDList = ParseICDs(ICDCodes, true);

                    if (ICDList.Length > 0)
                    {
                        ICDCodes = ICDList;
                    }

                    txtGeneratedScript.Text = txtGeneratedScript.Text + "\r\n -- Total ICDs found in ICD9 : " + ICDCodes.Length;


                }
                else
                   if (checkICDConversion.Checked == true && ICD9.Checked)
                {
                    txtGeneratedScript.Text = txtGeneratedScript.Text + "\r\n -- Total ICDs Provided in ICD9: " + ICDCodes.Length;

                    string[] ICDList = ParseICDs(ICDCodes, false);

                    if (ICDList.Length > 0)
                    {
                        ICDCodes = ICDList;
                    }
                    txtGeneratedScript.Text = txtGeneratedScript.Text + "\r\n -- Total ICDs found in ICD10:  " + ICDCodes.Length;

                }
                else
                {

                    txtGeneratedScript.Text = txtGeneratedScript.Text + "\r\n -- Total ICDs Provided " + ICDCodes.Length;

                }

                if (checkResetBucket.Checked == true && chckGeneral.Checked == false)
                {
                    txtGeneratedScript.Text = txtGeneratedScript.Text + "\r\n   -- It will Delete all the ICDs related to given Type. Make sure it is NOT harmful " + Environment.NewLine;
                    txtGeneratedScript.Text = txtGeneratedScript.Text + "\r\n   delete from DIAGNOSIS_LIST where  payer_Id = '" + tbProvider.Text + "' and type = '" + tbType.Text + "'" + Environment.NewLine;


                }
                foreach (string code in ICDCodes)
                {



                    if (chckGeneral.Checked == true)
                    {

                        if (checkReset.Checked == false)
                        {
                            string forgeneral = "" + Environment.NewLine;
                            forgeneral += "Print ' Checking for Code - " + code + "  '" + Environment.NewLine;
                            forgeneral += "if exists(Select * from DIAGNOSIS_LIST where Payer_ID = '" + tbProvider.Text + "' and code = '" + code + "')" + Environment.NewLine;
                            forgeneral += "Begin" + Environment.NewLine;
                            forgeneral += " Print ' - - Record Exists - - '" + Environment.NewLine;
                            forgeneral += "delete from DIAGNOSIS_LIST where  payer_Id = '" + tbProvider.Text + "' and code = '" + code + "'" + Environment.NewLine;
                            forgeneral += "End" + Environment.NewLine;
                            forgeneral += "else" + Environment.NewLine;
                            forgeneral += "Begin" + Environment.NewLine;
                            forgeneral += " Print ' - - Record Not Found - - '" + Environment.NewLine;
                            forgeneral += "End" + Environment.NewLine;
                            forgeneral += "GO" + Environment.NewLine;

                            txtGeneratedScript.Text = txtGeneratedScript.Text + "\r\n" + forgeneral;
                        }
                        else
                        {
                            string forgeneral = "" + Environment.NewLine;
                            forgeneral += "Print ' Checking for Code - " + code + "  '" + Environment.NewLine;
                            //forgeneral += "if exists(Select * from DIAGNOSIS_LIST where Payer_ID = '" + tbProvider.Text + "' and code = '" + code + "')" + Environment.NewLine;
                            //forgeneral += "Begin" + Environment.NewLine;
                            //forgeneral += " Print ' - - Record Exists - - '" + Environment.NewLine;
                            forgeneral += "delete from DIAGNOSIS_LIST where  payer_Id = '" + tbProvider.Text + "' and code = '" + code + "'" + Environment.NewLine;
                            //forgeneral += "End" + Environment.NewLine;
                            //forgeneral += "else" + Environment.NewLine;
                            //forgeneral += "Begin" + Environment.NewLine;
                            //forgeneral += " Print ' - - Record Not Found - - '" + Environment.NewLine;
                            //forgeneral += "End" + Environment.NewLine;
                            forgeneral += "GO" + Environment.NewLine;

                            txtGeneratedScript.Text = txtGeneratedScript.Text + "\r\n" + forgeneral;
                        }


                    }
                    else
                    {

                        if (checkReset.Checked == false)
                        {
                            string forgeneral = "" + Environment.NewLine;
                            forgeneral += "Print ' Checking for Code - " + code + "  '" + Environment.NewLine;
                            forgeneral += "if exists(Select * from DIAGNOSIS_LIST where Payer_ID = '" + tbProvider.Text + "' and code = '" + code + "')" + Environment.NewLine;
                            forgeneral += "Begin" + Environment.NewLine;
                            forgeneral += "Print ' - - Record Exists - - '" + Environment.NewLine;
                            forgeneral += "delete from DIAGNOSIS_LIST where  payer_Id = '" + tbProvider.Text + "' and code = '" + code + "'" + Environment.NewLine;
                            forgeneral += "Insert Into  DIAGNOSIS_LIST( [payer_id] ,[code] ,[type] ,[PCN]) VALUES " + Environment.NewLine;
                            forgeneral += "('" + tbProvider.Text + "','" + code + "','" + tbType.Text + "','" + tbPCN.Text + "')" + Environment.NewLine;
                            forgeneral += "End" + Environment.NewLine;
                            forgeneral += "else" + Environment.NewLine;
                            forgeneral += "Begin" + Environment.NewLine;
                            forgeneral += "Print ' - - Record Not Found - - '" + Environment.NewLine;
                            forgeneral += "Insert Into  DIAGNOSIS_LIST( [payer_id] ,[code] ,[type] ,[PCN]) VALUES " + Environment.NewLine;
                            forgeneral += "('" + tbProvider.Text + "','" + code + "','" + tbType.Text + "','" + tbPCN.Text + "')" + Environment.NewLine;
                            forgeneral += "End" + Environment.NewLine;
                            forgeneral += "GO" + Environment.NewLine;
                            txtGeneratedScript.Text = txtGeneratedScript.Text + "\r\n" + forgeneral;
                        }
                        else
                        {




                            string forgeneral = "" + Environment.NewLine;

                            if (checkResetBucket.Checked == false)
                            {
                                // forgeneral += "delete from DIAGNOSIS_LIST where  payer_Id = '" + tbProvider.Text + "' and code = '" + code + "'" + Environment.NewLine;
                                forgeneral += "Print ' Checking for Code - " + code + "  '" + Environment.NewLine;
                                //forgeneral += "if exists(Select * from DIAGNOSIS_LIST where Payer_ID = '" + tbProvider.Text + "' and code = '" + code + "')" + Environment.NewLine;
                                //forgeneral += "Begin" + Environment.NewLine;
                                //forgeneral += "Print ' - - Record Exists - - '" + Environment.NewLine;
                                forgeneral += "delete from DIAGNOSIS_LIST where  payer_Id = '" + tbProvider.Text + "' and code = '" + code + "'" + Environment.NewLine;
                                //forgeneral += "Insert Into  DIAGNOSIS_LIST( [payer_id] ,[code] ,[type] ,[PCN]) VALUES " + Environment.NewLine;
                                //forgeneral += "('" + tbProvider.Text + "','" + code + "','" + tbType.Text + "','" + tbPCN.Text + "')" + Environment.NewLine;
                                //forgeneral += "End" + Environment.NewLine;
                                //forgeneral += "else" + Environment.NewLine;
                                //forgeneral += "Begin" + Environment.NewLine;
                                //forgeneral += "Print ' - - Record Not Found - - '" + Environment.NewLine;
                                forgeneral += "Insert Into  DIAGNOSIS_LIST( [payer_id] ,[code] ,[type] ,[PCN]) VALUES " + Environment.NewLine;
                                forgeneral += "('" + tbProvider.Text + "','" + code + "','" + tbType.Text + "','" + tbPCN.Text + "')" + Environment.NewLine;
                                //forgeneral += "End" + Environment.NewLine;
                                forgeneral += "GO" + Environment.NewLine;
                            }
                            else
                            {
                                // forgeneral += "delete from DIAGNOSIS_LIST where  payer_Id = '" + tbProvider.Text + "' and code = '" + code + "'" + Environment.NewLine;
                                forgeneral += "Print ' Checking for Code - " + code + "  '" + Environment.NewLine;
                                //forgeneral += "if exists(Select * from DIAGNOSIS_LIST where Payer_ID = '" + tbProvider.Text + "' and code = '" + code + "')" + Environment.NewLine;
                                //forgeneral += "Begin" + Environment.NewLine;
                                //forgeneral += "Print ' - - Record Exists - - '" + Environment.NewLine;
                                //forgeneral += "delete from DIAGNOSIS_LIST where  payer_Id = '" + tbProvider.Text + "' and code = '" + code + "'" + Environment.NewLine;
                                //forgeneral += "Insert Into  DIAGNOSIS_LIST( [payer_id] ,[code] ,[type] ,[PCN]) VALUES " + Environment.NewLine;
                                //forgeneral += "('" + tbProvider.Text + "','" + code + "','" + tbType.Text + "','" + tbPCN.Text + "')" + Environment.NewLine;
                                //forgeneral += "End" + Environment.NewLine;
                                //forgeneral += "else" + Environment.NewLine;
                                //forgeneral += "Begin" + Environment.NewLine;
                                //forgeneral += "Print ' - - Record Not Found - - '" + Environment.NewLine;
                                forgeneral += "Insert Into  DIAGNOSIS_LIST( [payer_id] ,[code] ,[type] ,[PCN]) VALUES " + Environment.NewLine;
                                forgeneral += "('" + tbProvider.Text + "','" + code + "','" + tbType.Text + "','" + tbPCN.Text + "')" + Environment.NewLine;
                                //forgeneral += "End" + Environment.NewLine;
                                forgeneral += "GO" + Environment.NewLine;

                            }
                            txtGeneratedScript.Text = txtGeneratedScript.Text + "\r\n" + forgeneral;
                        }

                    }
}
                    FS_WS_WSCTFW.Helpers.BatchFIleCaller.InsertMLog("BucketMappingFile", "Fazeel", "", txtGeneratedScript.Text, "0", "Bucket Mapping file.");


                
            }

        }

        protected void chckGeneral_CheckedChanged(object sender, EventArgs e)
        {
            if (chckGeneral.Checked == true)
            {
                tbType.Enabled = false;
            }
            else
            {
                tbType.Enabled = true;
            }
        }

        protected void btnICDConver_Click(object sender, EventArgs e)
        {

            //     getICDDictionary();

        }

        public static Dictionary<int, Helpers.ICDs> getICDDictionary()
        {


            return Helpers.CSVHelper.getCSVinMemory();


        }

        public static System.Collections.ArrayList ConvertICD(Dictionary<int, Helpers.ICDs> ICDDict, string ICDCode, bool isICD10)
        {
            return Helpers.CSVHelper.ConvertICD(ICDDict, ICDCode, isICD10);
        }


        public static string[] ParseICDs(string[] ICDs, bool isICD10)
        {

            try
            {

                System.Collections.ArrayList ICDAl = new System.Collections.ArrayList();

                Dictionary<int, Helpers.ICDs> ICDDict = getICDDictionary();

                foreach (var item in ICDs)
                {
                    System.Collections.ArrayList ICD9COdes = new System.Collections.ArrayList();
                    if (isICD10)
                    {
                        ICD9COdes = ConvertICD(ICDDict, item, true);
                    }

                    else
                    {
                        ICD9COdes = ConvertICD(ICDDict, item, false);
                    }
                    ICDAl.AddRange(ICD9COdes);

                }

           //     ICDAl = RemoveDuplicates(ICDAl);
                       return (string[])ICDAl.ToArray(typeof(string));
          //      return ICDAldistinct.ToList();

            }
            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
                return null;
            }
        }

        public static System.Collections.ArrayList RemoveDuplicates (System.Collections.ArrayList ICDAl)
        {

            try
            {
                System.Collections.ArrayList distinctAl = new System.Collections.ArrayList();

                foreach (string aString in ICDAl)
                {
                    if (!distinctAl.Contains(aString))
                    {
                        distinctAl.Add(aString);
                    }
                }

                return distinctAl;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected void btnSaveNewFile_Click(object sender, EventArgs e)
        {
            if (txtGeneratedScript.Text.Trim().Length > 1)
            {

                string path = System.Configuration.ConfigurationManager.AppSettings["ProviderPath"];
                string filename = "fshaikh_"+tbBackupTabName.Text + "_Script.SQL";
                path = path + filename;

               
                //using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(Server.MapPath("~/Utilities/ClinicianUpdateScript/Script.SQL"), false))

                if (!System.IO.File.Exists(path))
                {
                    using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(path, false))
                    {
                        _testData.WriteLine(txtGeneratedScript.Text); // Write the file.
                    }
                    FS_WS_WSCTFW.Helpers.BatchFIleCaller.InsertMLog("BucketMappingFile", "Fazeel", "", txtGeneratedScript.Text, "0", "Bucket Mapping file saved successfully");

                    txtGeneratedScript.Text = "-- file saved successfully";
                    lblFIlename.Text = filename;
                }
                else
                {
                    txtGeneratedScript.Text += Environment.NewLine + " -- File Exists for update. !!!! ";

                    //  txtFullLog.Text = "Another Script Already Schedule for Execution, Please Retry after Some time!!!";
                    //txtFullLog.Text = "Another Script Already Schedule for Execution, Created: " + Environment.NewLine + System.IO.File.GetCreationTime(path).ToString() + Environment.NewLine + "Please Retry after Some time!!!";
                    FS_WS_WSCTFW.Helpers.BatchFIleCaller.InsertMLog("BucketMappingFile", "Fazeel", "", txtGeneratedScript.Text, "0", "Error in saving new file for Bucket Mapping");

                }
            }
            else
            {

            }
        }

        protected void btnAppendExistingFile_Click(object sender, EventArgs e)
        {
            if (txtGeneratedScript.Text.Trim().Length > 1)
            {

                string path = System.Configuration.ConfigurationManager.AppSettings["ProviderPath"];
                string filename = lblFIlename.Text;
                path = path + filename;


                if (filename.Length < 1)
                {

                }
                else
                {
                    //using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(Server.MapPath("~/Utilities/ClinicianUpdateScript/Script.SQL"), false))

                    if (System.IO.File.Exists(path))
                    {
                        using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(path, true))
                        {
                            txtGeneratedScript.Text += Environment.NewLine + "-- Appending File --";
                            _testData.WriteLine(txtGeneratedScript.Text); // Write the file.
                        }
                        FS_WS_WSCTFW.Helpers.BatchFIleCaller.InsertMLog("BucketMappingFile", "Fazeel", "", txtGeneratedScript.Text, "0", "Bucket Mapping file Updated");

                        txtGeneratedScript.Text = "-- file updated successfully";
                    }
                    else
                    {
                        txtGeneratedScript.Text += Environment.NewLine + " -- File Doesnot Exists for update. !!!! ";
                        //    txtFullLog.Text = "Another Script Already Schedule for Execution, Please Retry after Some time!!!";
                        //txtFullLog.Text = "Another Script Already Schedule for Execution, Created: " + Environment.NewLine + System.IO.File.GetCreationTime(path).ToString() + Environment.NewLine + "Please Retry after Some time!!!";
                        FS_WS_WSCTFW.Helpers.BatchFIleCaller.InsertMLog("BucketMappingFile", "Fazeel", "", txtGeneratedScript.Text, "0", "Error Updating File.");

                    }
                }
            }
            else
            {

            }
        }

        protected void btnFInalScrip_Click(object sender, EventArgs e)
        {
            string pathfrom = System.Configuration.ConfigurationManager.AppSettings["ProviderPath"];
            string filename = lblFIlename.Text;
            pathfrom = pathfrom + filename;


            string pathto = System.Configuration.ConfigurationManager.AppSettings["EmailReportPath"];
            string targetfilename = lblFIlename.Text;
            pathto = pathto + targetfilename;

            FS_WS_WSCTFW.Helpers.FileHelper.filecopy(pathfrom, pathto);


            


        }
    }
}