using System;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class oic_cancel_transaction
    {
        public string cancel_transaction(string payerid)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["oic_cancel"] + "Oic_cancel_" + payerid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            try
            {
                string query = generate_query(payerid);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_OIC_Cancel_TRNS_" + payerid, "OICPayer", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "OICPayer");
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
               return ex.Message.ToString();
            }
        }

        public string generate_query(string payerid)
        {
            string query = "USE eAuth" +
                "\n declare @IDPayer nvarchar(4000)" +
                "\n set @IDPayer ='" + payerid.ToString() + "'" +
                "\n PRINT 'Updating the transaction as Cancelled'" +
                "\n\n update TOP(1) [eAuth].[dbo].[AuthorizationTransaction]" +
                "\n set is_cancelled = 1" +
                "\n where id_payer = @IDPayer";
            return query;
        }
    }
}