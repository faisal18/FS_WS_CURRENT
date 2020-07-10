using System;
using System.Configuration;
using System.Text;
using System.IO;
namespace ClinicianAutomation.ExtraClasses
{
    public class get_ICDsMapping
    {
        public string get_icd9(string[] icd9)
        {
            try
            {
                string query = generate_query_icd9(icd9);
                execute_query(query);
                return "File created successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        public string get_icd10(string[] icd10)
        {
            try
            {
                string query = generate_query_icd10(icd10);
                execute_query(query);
                return "File created successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        public string get_both()
        {
            try
            {
                string query = generate_query_both();
                execute_query(query);
                return "File created successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }


        public string SaveCSV()
        {
            try
            {
                string query = generate_query_both();
                execute_query(query);
                return "File created successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        private string generate_query_icd9(string[] data)
        {
            try
            {
                string query = "select * from ICD10ICD9_MAPPING where ICD9cm in (" + split_array(data) + ")";
                return query;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string generate_query_icd10(string[] data)
        {
            try
            {
                string query = "select * from ICD10ICD9_MAPPING where ICD10cm  in (" + split_array(data) + ")";
                return query;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string generate_query_both()
        {
            try
            {
                string query = "Select * from ICD10ICD9_MAPPING order by ICD9cm, icd10cm ";
                return query;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void execute_query(string query)
        {
           
            FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "Get_ICD_Mapping_", "PBMM", 2);
        }

        private void SaveCSVFile(string query)
        {
            //string path = ConfigurationManager.AppSettings["ICD910Mapping"].ToString();
            //File.Exists
            //FS_WS_WSCTFW.Helpers.BatchFIleCaller.SaveAnyFile("", fileContent, BatchPath, false, "SQL")
            ////FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "Get_ICD_Mapping_", "PBMM", 2);
        }

        string split_array(string[] data)
        {
            try
            {
                //string concat = null;
                StringBuilder cot = new StringBuilder();
                foreach (string s in data)
                {
                       cot.Append(string.Format("'" + s.Trim() + "',"));
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