using System;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class erx_cancel_transaction
    {
        public string cancel_transaction(string authourizationid)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["erx_path"] + authourizationid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(authourizationid);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_eRX_Cancel_TRNS_" + authourizationid, "ERX", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path,"ERX");
                return "Request Processed Successfully";
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string generate_query(string authourizationid)
        {
            string query = "USE ERX" +
                   "\n declare @Authorization_ID nvarchar(4000)" +
                   "\n set @Authorization_ID ='" + authourizationid.ToString() + "'" +
                   "\n PRINT 'Updating the transaction as Cancelled'" +
                   "\n\n update TOP(1) [AUTHORIZATION]" +
                   "\n set [status] = 4" +
                   "\n where ID = @Authorization_ID ";
            return query;
        }
    }
}