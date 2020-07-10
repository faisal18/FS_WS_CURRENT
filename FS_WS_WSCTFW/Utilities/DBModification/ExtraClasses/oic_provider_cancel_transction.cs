using System;
using System.Configuration;
using System.IO;

namespace ClinicianAutomation.ExtraClasses
{
    public class oic_provider_cancel_transction
    {
        public string cancel_transaction(string transactionid)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["oic_cancel"] + "Oic_provider_cancel_" + transactionid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            
            try
            {
                string query = generate_query(transactionid);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "Cloud", "_OIC_Prov_Cancel_TRNS_" + transactionid, "OICProvider", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "OICProvider");
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string generate_query(string transactionid)
        {
            string query = "USE OIC.eAuth" +
                "\n declare @TransactionID nvarchar(4000)" +
                "\n set @TransactionID ='" + transactionid.ToString() + "'" +
                "\n PRINT 'Updating the transaction as Canceled'" +
                "\n\n update TOP(1) [OIC.eAuth].[dbo].[Authorization]" +
                "\n set Status = 4" +
                "\n where ID = @TransactionID";
            return query;
        }


    }
}