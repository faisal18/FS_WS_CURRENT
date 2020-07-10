using System;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class pbmm_cancel_transaction
    {
        public string cancel_transaction(string[] transactionid)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["misc"] + "PBMSwitch_Cancel_" + transactionid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {

                string query = generate_query(transactionid);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "_PBM_Cancel_TRNS_", "PBMM", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "PBMM");
                return "Request Processed Successfully";
                //return "File created successfully in directory " + Environment.NewLine + "" + path + ".sql".ToString();

            }
            catch (Exception ex)
            {
                return "An exception occured " + Environment.NewLine + "" + ex.Message;
            }
        }

        public string generate_query(string[] transactionid)
        {
            try
            {
                string query = "\nUSE [PBMM]" +
                                "\ndeclare @datatable table(data nvarchar(200))" +
                                "\ninsert @datatable(data) values" +
                                split_array(transactionid) +

                                "\nupdate top(1) AUTHORIZATION_TRANSACTION " +
                                "\nset state_id = 8 " +
                                "\nwhere  id in (select AT.id from AUTHORIZATION_TRANSACTION at  with (nolock) " +
                                "\nwhere request_id in (select data from @datatable) " +
                                "\nand at.state_id = 3) ";
                return query;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        string split_array(string[] data)
        {
            string concat = null;
            foreach (string s in data)
            {
                concat += "('" + s + "'),";
            }
            return concat.Remove(concat.Length - 1);
        }
    }
}