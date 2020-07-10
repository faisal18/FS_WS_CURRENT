using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ClinicianAutomation.ExtraClasses
{
    public class PBMSwitch_TransactionUpdate
    {
        public string Update(string[] data,string status,string process)
        {
            try
            {
                string query = generate_query(data, status, process);

                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", data[0], "PBMM", 2);
                return "File created successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string generate_query(string[] data,string status,string process)
        {
            try
            {
                string query ="";
                if (process == "Payerid")
                {
                     query = "\n\ndeclare @datatable table(data nvarchar(200))" +
                                    "\ninsert @datatable(data) values" +
                                     split_array(data) +
                                    "\nupdate top (1) AUTHORIZATION_TRANSACTION" +
                                    "\nset state_id = " + status +
                                    "\nwhere request_id in" +
                                    "\n(" +
                                    "\nselect request_id from " +
                                    "\nAUTHORIZATION_TRANSACTION at with (nolock) " +
                                    "\nINNER JOIN[dbo].[PRIOR_AUTHORIZATION] PA  with (nolock) on  AT.id = PA.ID" +
                                    "\nwhere pa.ID_payer in (select data from @datatable )" +
                                    "\n)" +
                                    "\nand state_id = 2"+
                                    "\nGo";
                }
                else if(process == "Requestid")
                {
                    query =  "\n\ndeclare @datatable table(data nvarchar(200))" +
                                    "\ninsert @datatable(data) values" +
                                     split_array(data) +
                                    "\n"+
                                    "\nupdate top (1) AUTHORIZATION_TRANSACTION" +
                                    "\nset state_id = " + status +
                                    "\nwhere request_id in (select data from @datatable )" +
                                    "\nand state_id = 2"+
                                    "\nGo";
                }
                return query;
            }
            catch (Exception ex)
            {
                return ex.Message;
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
    }
}