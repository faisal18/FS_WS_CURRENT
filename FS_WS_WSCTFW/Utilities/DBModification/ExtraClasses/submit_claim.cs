using System;
using System.Configuration;


namespace ClinicianAutomation.ExtraClasses
{
    public class submit_claim
    {
        public string claim_submit(string claimid)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["claim_submit"] + "Submit_claim_" + claimid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(claimid);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "eClaim_ProviderLink_ClaimUPdate", "ECLAIMLINK", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "ECLAIMLINK");
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string generate_query(string claimid)
        {
            string query = "USE ECLAIMLINK" +
                "\n DECLARE @claim_code nvarchar(100)" +
                "\n SET @claim_code='" + claimid + "'" +
                "\n update TOP(1) Provider_submission " +
                "\n set isSent = 1 " +
                "\n where code = @claim_code";
            return query;
        }

    }
}