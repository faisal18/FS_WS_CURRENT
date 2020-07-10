using System.Collections.Generic;
using System.IO;
using System;
using System.Configuration;
using System.Text;
using System.Diagnostics;
using System.Linq;

namespace ClinicianAutomation.ExtraClasses
{
    public class contra_indication_edit
    {

        public string Create_Csv(string process, string[] data, bool pbmm, bool pbmuat)
        {
            string tmppath = ConfigurationManager.AppSettings["ContraIndicationEdit"];
            string CI_FDBDatabase = ConfigurationManager.AppSettings["ContraIndicationEdit_FDBServer"];
            if (File.Exists(tmppath + "FS_MyTestTable_Temp.csv"))
            {
                File.Delete(tmppath +"FS_MyTestTable_Temp.csv");
            }

            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string csv_result = null;
            string ProcessResult = " --- Indication Edit Process Started ---" + Environment.NewLine;
            try
            {
               // string CI_FDBDatabase = ConfigurationManager.AppSettings["ContraIndicationEdit_FDBServer"];
                string path = ConfigurationManager.AppSettings["ContraIndicationEdit"] + "Indication_Edit_" + data[0] + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                switch (process)
                {
                    case "CISC":
                        csv_result = CSV_CISC_Query(data);
                        break;
                    case "CIDC":
                        csv_result = CSV_CIDC_Query(data);
                        break;
                    case "ISC":
                        csv_result = CSV_ISC_Query(data);
                        break;
                    case "IDC":
                        csv_result = CSV_IDC_Query(data);
                        break;
                }

                // FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(csv_result, "HyperV", data[0], "Localhost", 2);
                string scriptPath =  cs.script_create(csv_result, path);
                
                ProcessResult += "--- Indication CSV generation query created ---" + Environment.NewLine;
                FS_WS_WSCTFW.Helpers.BatchFIleCaller.ExecuteBatchFile(cb.batch_create_Indication(path, "Localhost"));
                ProcessResult += " --- Indication CSV generation query executed  from FDB Database  ---" + Environment.NewLine;
                Insert_Query(data, path, process);
                ProcessResult += "--- Insert queries generated for each Drug Code --- " + Environment.NewLine;
                if (pbmm == true && pbmuat == false)
                {

                    // cb.batch_create(path, "PBMM");
                    FS_WS_WSCTFW.Helpers.BatchFIleCaller.ExecuteBatchFile(cb.batch_create(path, "PBMM"));
                    //FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(insert_result, "HyperV", data[0], "PBMM", 3);
                    ProcessResult += "  --- Batch Executed for Production Database (PBMM) as selected    " + Environment.NewLine;
                }
                else if (pbmm == false && pbmuat == true)
                {
                    //  cb.batch_create(path, "PBMUAT");
                    FS_WS_WSCTFW.Helpers.BatchFIleCaller.ExecuteBatchFile(cb.batch_create(path, "PBMUAT"));
                    //FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(insert_result, "HyperV", data[0], "PBMUAT", 3);
                    ProcessResult += "  --- Batch Executed for UAT Database (PBMM) as selected    " + Environment.NewLine;
                }
                else if (pbmm == true && pbmuat == true)
                {

                    //  cb.batch_create(path, "PBMUAT");
                    FS_WS_WSCTFW.Helpers.BatchFIleCaller.ExecuteBatchFile(cb.batch_create(path, "PBMUAT"));
                    ProcessResult += "  --- Batch Executed for Production Database (PBMM) as selected    " + Environment.NewLine;

                    //  cb.batch_create(path, "PBMM");
                    FS_WS_WSCTFW.Helpers.BatchFIleCaller.ExecuteBatchFile(cb.batch_create(path, "PBMM"));
                    ProcessResult += "  --- Batch Executed for UAT Database (PBMM) as selected    " + Environment.NewLine;
                    //FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(insert_result, "HyperV", data[0], "PBMM", 3);

                    //FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(insert_result, "HyperV", data[0], "PBMUAT", 3);
                }

                else if (pbmm == false && pbmuat == false)
                {
                    ProcessResult += "  --- No Options selected.. Files generated successfully but not executed -----    " + Environment.NewLine;

                    FS_WS_WSCTFW.Helpers.Logger.Info(" --- No Options selected.. Files generated successfully but not executed -----");
                    //  cb.batch_create(path, "PBMUAT");
                    // FS_WS_WSCTFW.Helpers.BatchFIleCaller.ExecuteBatchFile(cb.batch_create(path, "PBMUAT"));


                    // cb.batch_create(path, "PBMM");
                    // FS_WS_WSCTFW.Helpers.BatchFIleCaller.ExecuteBatchFile(cb.batch_create(path, "PBMM"));

                    //FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(insert_result, "HyperV", data[0], "PBMM", 3);

                    //FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(insert_result, "HyperV", data[0], "PBMUAT", 3);
                }


                //cb.batch_create(path, "PBMM");
                //Process.Start(cb.batch_create(path , "PBMUAT"));
                ProcessResult += "  ---   Indication Edit Process Completed   ----" + Environment.NewLine;
                string outputpath = ConfigurationManager.AppSettings["EmailReportPath"];
                FS_WS_WSCTFW.Helpers.BatchFIleCaller.SaveAnyFile("TechSupport_Indication_Edit_Process_Result", ProcessResult, outputpath, true, "TXT");
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
                return "An exception occured " + Environment.NewLine + "" + ex.Message;
            }
        }

        public string CSV_CISC_Query(string[] data)
        {
            string CI_FDBDatabase = ConfigurationManager.AppSettings["ContraIndicationEdit_FDBServer"];
            string tmppath = ConfigurationManager.AppSettings["ContraIndicationEdit"];
            string query = "";
             query = "\nuse [master]" +
           "\nif exists (select * from sys.objects where name = 'FS_MyTestTable_Temp' and type = 'u')" +
           "\ndrop table FS_MyTestTable_Temp" +
           "\nGO" +

           "\n\ndeclare @datatable table(data nvarchar(200) COLLATE Arabic_CI_AS)" +
           "\ninsert @datatable(data) values" +
            split_array(data) +
           "\n\nselect * into [Master].DBO.FS_MyTestTable_Temp" +

           "\n\nfrom  " + CI_FDBDatabase + ".medd.dbo.FDB_DRUG_CONTRAINDICATION_ICD9_VW where scientific_code in" +
           "\n(select data from @datatable)" +
           "\nGO" +
           //"\n select * from[Master].DBO.FS_MyTestTable_Temp " +
           //"\nGO" +
           "\n!!sqlcmd -S localhost -d master -E  -Q" +
           "\"set nocount on; select * from[Master].DBO.FS_MyTestTable_Temp\" -s \",\" -o \"" + tmppath + "FS_MyTestTable_Temp.csv\" " +
           "\nGO";
            return query;
        }

        public string CSV_CIDC_Query(string[] data)
        {
            string CI_FDBDatabase = ConfigurationManager.AppSettings["ContraIndicationEdit_FDBServer"];
            string tmppath = ConfigurationManager.AppSettings["ContraIndicationEdit"];
            string query = "";
             query = "\nuse [master]" +
           "\nif exists (select * from sys.objects where name = 'FS_MyTestTable_Temp' and type = 'u')" +
           "\ndrop table FS_MyTestTable_Temp" +
           "\nGO" +

           "\n\ndeclare @datatable table(data nvarchar(200) COLLATE Arabic_CI_AS)" +
           "\ninsert @datatable(data) values" +
            split_array(data) +
           "\n\nselect * into [Master].DBO.FS_MyTestTable_Temp" +

           "\n\nfrom  " + CI_FDBDatabase + ".medd.dbo.FDB_DRUG_CONTRAINDICATION_ICD9_VW where DRUG_ID in" +
           "\n(select data from @datatable)" +
           "\nGO" +
           //"\n select * from[Master].DBO.FS_MyTestTable_Temp " +
           //"\nGO" +
           "\n!!sqlcmd -S localhost -d master -E  -Q" +
           "\"set nocount on; select * from[Master].DBO.FS_MyTestTable_Temp\" -s \",\" -o \"" + tmppath + "FS_MyTestTable_Temp.csv\" " +
           "\nGO";
            return query;
        }

        public string CSV_ISC_Query(string[] data)
        {
            string CI_FDBDatabase = ConfigurationManager.AppSettings["ContraIndicationEdit_FDBServer"];
            string tmppath = ConfigurationManager.AppSettings["ContraIndicationEdit"];
            string query = "\nuse [master]" +
            "\nif exists (select * from sys.objects where name = 'FS_MyTestTable_Temp' and type = 'u')" +
            "\ndrop table FS_MyTestTable_Temp" +
            "\nGO" +

            "\n\ndeclare @datatable table(data nvarchar(200) COLLATE Arabic_CI_AS)" +
            "\ninsert @datatable(data) values" +
             split_array(data) +
            "\n\nselect * into [Master].DBO.FS_MyTestTable_Temp" +

            "\n\nfrom  "+CI_FDBDatabase+".medd.dbo.FDB_DRUG_INDICATION_ICD9_VW where scientific_code in" +
            "\n(select data from @datatable)" +
            "\nGO" +
            //"\n select * from[Master].DBO.FS_MyTestTable_Temp " +
            //"\nGO" +
            "\n!!sqlcmd -S localhost -d master -E  -Q" +
            "\"set nocount on; select * from[Master].DBO.FS_MyTestTable_Temp\" -s \",\" -o \"" + tmppath + "FS_MyTestTable_Temp.csv\" " +
            "\nGO";
            return query;
        }

        public string CSV_IDC_Query(string[] data)
        {
            string CI_FDBDatabase = ConfigurationManager.AppSettings["ContraIndicationEdit_FDBServer"];
            string tmppath = ConfigurationManager.AppSettings["ContraIndicationEdit"];
            string query = "\nuse [master]" +
            "\nif exists (select * from sys.objects where name = 'FS_MyTestTable_Temp' and type = 'u')" +
            "\ndrop table FS_MyTestTable_Temp" +
            "\nGO" +

            "\n\ndeclare @datatable table(data nvarchar(200) COLLATE Arabic_CI_AS)" +
            "\ninsert @datatable(data) values" +
            split_array(data) +
            "\n\nselect * into [Master].DBO.FS_MyTestTable_Temp" +

            "\n\nfrom  "+CI_FDBDatabase+".medd.dbo.FDB_DRUG_INDICATION_ICD9_VW where drug_id in" +
            "\n(select data from @datatable)" +
            "\nGO" +
            //"\nselect * from [Master].DBO.FS_MyTestTable_Temp" +

            //"\n\nupdate  [Master].DBO.FS_MyTestTable_Temp set drug_id = rtrim(LTRIM(RTRIM(drug_id)))" +
            //"\nGO" +
            //"\nupdate  [Master].DBO.FS_MyTestTable_Temp set Scientific_code = rtrim(LTRIM(RTRIM(Scientific_code)))" +
            //"\nGO" +
            //"\nupdate  [Master].DBO.FS_MyTestTable_Temp set DIAGNOSIS_CODE = rtrim(LTRIM(RTRIM(DIAGNOSIS_CODE)))" +
            //"\nGO" +

            "\n!!sqlcmd -S localhost -d master -E  -Q" +
            "\"set nocount on; select * from [Master].DBO.FS_MyTestTable_Temp\" -s \",\" -o \"" + tmppath + "FS_MyTestTable_Temp.csv\" " +
            "\nGO";
            return query;
        }

        public string Insert_Query(string[] data, string path, string process)
        {
            List<string> Drugid_list = new List<string>();
            List<string> Diag_list = new List<string>();
            string CI_FDBDatabase = ConfigurationManager.AppSettings["ContraIndicationEdit_FDBServer"];
            string CSVpath = ConfigurationManager.AppSettings["ContraIndicationEdit"];
            try
            {
                using (var fs = File.OpenRead(CSVpath + @"FS_MyTestTable_Temp.csv"))
                using (var reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        Drugid_list.Add(values[0]);
                        Diag_list.Add(values[2]);
                    }
                }
                string F1 = path + ".sql";

                using (StreamWriter text = new StreamWriter(F1))
                {
                    text.AutoFlush = true;


                    if (process == "ISC" || process == "IDC")
                    {
                        string backupdb = " [PBMM_Data_Backup].[dbo].[FS_Indication_Edit_Backup_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]  ";
                        StringBuilder backupquery = new StringBuilder();
                        backupquery.Append("\n print ' ****** script Start time : ' + cast(getdate() as varchar(100))  ");

                        backupquery.Append("\n PRINT ' -- Taking Backup Drug Indication Table '  ");
                        backupquery.Append("\n Select *  into "+backupdb+"   from pbmm.dbo.DRUG_INDICATION " );
                        backupquery.Append("\nGO\n\n");
                        text.Write(backupquery);

                        List<string> Drug_List_Sort = new List<string>();
                        Drug_List_Sort = Drugid_list.Distinct().ToList();
                        //text.Write("Delete from pbmm.dbo.DRUG_INDICATION where drug_code in (select rtrim(ltrim(Drug_id))  from[#FS_MyTestTable_Temp] GO \n");
                        //text.Write(string.Format("\ndeclare @datatable table(data nvarchar(200))" +
                        //"\ninsert @datatable(data) values\n" +
                        //split_array(Drugid_list.ToArray()) +
                        //"\nDelete from pbmm.dbo.DRUG_INDICATION where drug_code" +
                        //"\nin (select data from @datatable)\nGO\n\n"));
                        for (int i = 0; i < Drug_List_Sort.Count; i++)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("PRINT 'Deleting Drug Code: " + Drug_List_Sort[i].Trim() + "'");
                            sb.Append("\nDelete from pbmm.dbo.DRUG_INDICATION where drug_code = '" + Drug_List_Sort[i].Trim() + "'");
                            sb.Append("\nGO\n\n");
                            text.Write(sb);
                        }
                        // text.Close();

                        for (int i = 2; i < Drugid_list.Count; i++)
                        {
                            StringBuilder sb = new StringBuilder();
                            //sb.Append("PRINT 'Deleting Drug Code: " + Drugid_list[i].Trim() + "'");
                            //sb.Append("\nDelete from pbmm.dbo.DRUG_INDICATION where drug_code = " + Drugid_list[i].Trim());

                            sb.Append("PRINT 'Inserting Drug and Diagnosis: " + Drugid_list[i].Trim() + "," + Diag_list[i].Trim() + "'\n");
                            sb.Append("Insert into [PBMM].[dbo].[DRUG_INDICATION]([drug_code],[diagnosis_code],[updated_date],wave_ad,wave_du , wave_NE) \n ");
                            sb.Append("Values ('" + Drugid_list[i].Trim() + "','" + Diag_list[i].Trim() + "',CURRENT_TIMESTAMP,53,2,2) \nGO\n");
                           
                            text.Write(sb);
                        }
                        text.Write(" \n print ' ****** script End time : ' + cast(getdate() as varchar(100))  ");

                        text.Close();
                    }
                    else if (process == "CISC" || process == "CIDC")
                    {
                        string backupdb = " [PBMM_Data_Backup].[dbo].[FS_Contra_Indication_Edit_Backup_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]  ";
                        StringBuilder backupquery = new StringBuilder();
                        backupquery.Append("\n  print ' ****** script Start time : ' + cast(getdate() as varchar(100))  ");

                        backupquery.Append("\n PRINT ' -- Taking Backup Drug Contra Indication Table '  ");
                        backupquery.Append("\n Select *  into " + backupdb + "   from pbmm.dbo.drug_contra_indication ");
                        backupquery.Append("\nGO\n\n");
                        text.Write(backupquery);
                        //text.Write("Delete from pbmm.dbo.drug_contra_indication where drug_code in (select rtrim(ltrim(Drug_id))  from[#FS_MyTestTable_Temp] GO \n");
                        //text.Write(string.Format("\ndeclare @datatable table(data nvarchar(200))" +
                        //"\ninsert @datatable(data) values\n" +
                        //split_array(Drugid_list.ToArray()) +
                        //"\nDelete from pbmm.dbo.drug_contra_indication where drug_code \n" +
                        //"\nin (select data from @datatable)  GO \n"));
                        List<string> Drug_List_Sort = new List<string>();
                        Drug_List_Sort = Drugid_list.Distinct().ToList();

                        for (int i = 0; i < Drug_List_Sort.Count; i++)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("PRINT 'Deleting Drug Code: " + Drug_List_Sort[i].Trim() + "'");
                            sb.Append("\nDelete from pbmm.dbo.drug_contra_indication where drug_code = '" + Drug_List_Sort[i].Trim() + "'");
                            sb.Append("\nGO\n\n");
                            text.Write(sb);
                        }
                      //  text.Close();
                        for (int i = 2; i < Drugid_list.Count; i++)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("PRINT 'Inserting Drug and Diagnosis: " + Drugid_list[i].Trim() + "," + Diag_list[i].Trim() + "'\n");
                            sb.Append("insert into PBMM.dbo.DRUG_CONTRA_INDICATION([drug_code],[diagnosis_code],[severity],[updated_date]) \n ");
                            sb.Append("Values ('" + Drugid_list[i].Trim() + "','" + Diag_list[i].Trim() + "',1,CURRENT_TIMESTAMP) \nGO\n");
                       
                            text.Write(sb);
                        }
                        text.Write(" \n print ' ****** script End time : ' + cast(getdate() as varchar(100))  ");

                        text.Close();
                    }


                }

                return F1;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        string split_array(string[] data)
        {
            try
            {
                //string concat = null;
                StringBuilder cot = new StringBuilder();
                foreach (string s in data)
                {
                    //concat += "('" + s.Trim() + "'),";
                    if (s != "DRUG_ID    " && s != "-----------")
                    {
                        cot.Append(string.Format("('" + s.Trim() + "'),"));
                    }
                }
                //return concat.Remove(concat.Length - 1);
                return Convert.ToString(cot).Remove(cot.Length - 1);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        string split_array_WithoutBracket(string[] data)
        {
            try
            {
                //string concat = null;
                StringBuilder cot = new StringBuilder();
                foreach (string s in data)
                {
                    //concat += "('" + s.Trim() + "'),";
                    if (s != "DRUG_ID    " && s != "-----------")
                    {
                        cot.Append(string.Format("'" + s.Trim() + "',"));
                    }
                }
                //return concat.Remove(concat.Length - 1);
                return Convert.ToString(cot).Remove(cot.Length - 1);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}