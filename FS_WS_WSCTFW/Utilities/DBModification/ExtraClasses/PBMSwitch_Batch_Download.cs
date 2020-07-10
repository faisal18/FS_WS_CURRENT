using System;
using System.Text;

namespace ClinicianAutomation.ExtraClasses
{
    public class PBMSwitch_Batch_Download
    {
        public string update(string[] batchid)
        {
            try
            {
                string query = generate_query(batchid);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "PBMSwitch_Batch_Download_" + batchid[0], "PBMM", 2);
                return "File created successfully!";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string generate_query(string[] data)
        {
            try
            {
                string query = "\ndeclare @datatable table(data nvarchar(200))" +
                "\ninsert @datatable(data) values" +
                split_array(data) +
                "\nselect  " +
                "\n'Batch_Download_Table'" +
                "\n," +
                "\nBDT.* " +
                "\n,'Autorization_Transaction_Table'" +
                "\n," +
                "\nAT.* " +
                "\n" +
                "\n,'PRIOR_AUTHORIZATION_Table'" +
                "\n," +
                "\nPA.* " +
                "\n,'PRIOR_REQUEST_Table'" +
                "\n," +
                "\nPR.* " +
                "\n,'PRIOR_REQUEST_Cancellation_Table'" +
                "\n," +
                "\nPRC.* " +
                "\n,'CLAIM_SUBMISSION_Table'" +
                "\n," +
                "\nCS.* " +
                "\n" +
                "\n--,* " +
                "\nfrom BATCH_DOWNLOAD_TRANS BDT with(Nolock) " +
                "\nleft join PRIOR_AUTHORIZATION PA with (nolock) on PA.download_batch_id = BDT.batch_id" +
                "\nleft join PRIOR_REQUEST PR with (nolock) on PR.download_batch_id = BDT.batch_id" +
                "\nleft join CLAIM_SUBMISSION CS with (nolock) on CS.download_batch_id = bdt.batch_id" +
                "\nleft join PRIOR_REQUEST PRC with (nolock) on PRc.download_canceled_batch_id = bdt.batch_id" +
                "\nleft join AUTHORIZATION_TRANSACTION AT with (nolock) on AT.id = PA.id or at.id = pr.id or at.id = prc.id or at.id = cs.id" +
                "\n--left join AUTHORIZATION_TRANSACTION AT with (nolock) on AT.id = PR.id" +
                "\n--left join AUTHORIZATION_TRANSACTION AT with (nolock) on AT.id = PRC.id" +
                "\n--left join AUTHORIZATION_TRANSACTION AT with (nolock) on AT.id = CS.id" +
                "\n" +
                "\nwhere batch_id in (select data from @datatable)" ;
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