using System;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class get_claim_detials
    {
        public string getclaimdetails(string[] data, string process)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["misc"] + "GetClaimDetails_" + data[0] + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(data, process);
                if (process != "DHPO")
                {
                    FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "_GetTransactionDetails_", "PBMM", 2);
                }
                else if (process == "DHPO")
                {
                    FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "_GetTransactionDetails_", "DHPO", 2);
                }
                //cs.script_create(query, path);
                //cb.batch_create(path, "PBMM");
                return "Request Processed Successfully";
                //return "File created successfully in directory " + Environment.NewLine + "" + path + ".sql".ToString();

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string generate_query(string[] data, string process)
        {
            try
            {


                string query = string.Empty;
                if (process != "DHPO")
                {
                    query = "\ndeclare @datatable table(data nvarchar(200))";


                    foreach (string item in data)
                    {
                        query += "\ninsert @datatable(data) values('" + item + "')";
                    }


                    query +=                            "\n\nUSE PBMM\n\nselect at.request_id,at.state_id,(select name from TRANSACTION_STATE ts where ts.id = at.state_id)as status,at.created_dt as Transaction_date, (select license_no from PROVIDER P where p.id = at.provider_id) as Provider,prh.receiver_ID," +
                    "\nat.member_id,at.validation_error, " +
                    "\npa.ID_payer,pa.result,pa.denial_code,pa.download_batch_id as PA_Download_Batch_ID," +
                    "\npr.type,pr.member_ID,pr.emirates_id,pr.download_batch_id as PR_Download_Batch_ID,pr.download_canceled_batch_id  as PR_Download_Cancel_Batch_ID," +
                    "\nprd.type,prd.code,PRD.mappedICD10_code," +
                    "\nprh.record_count,prh.xml_File_ID,prh.xml_file_name as PR_XML_FILENAME,PAH.xml_File_ID as PA_XML_ID,POC.prior_Request_sent,poc.prior_auth_downloaded," +
                    "\nPAH.xml_file_name as PA_XML_FILENAME,POc.prior_auth_sent,poc.prior_auth_downloaded,poc.claim_sub_sent,poc.claim_sub_file_name" +
                    "\n,PAA.activity_id,paa.type,PAA.drug_code,paa.drug_quantity,paa.drug_list_price,paa.drug_net,paa.drug_gross" +
                    "\n,paa.pbm_claim_id,paa.pbm_rx_ref_no,paa.denial_code,paa.pbm_code, poc.last_trans_error_msg,POC.is_pbm_link" +
                    "\n, CS.*  \n" +
                    "\nfrom AUTHORIZATION_TRANSACTION at with (nolock) " +
                    "\nleft  JOIN[dbo].[PRIOR_AUTHORIZATION] PA  with (nolock) on  AT.id = PA.ID" +
                    "\nLeft JOIN [dbo].[PRIOR_REQUEST] PR  with (nolock) on AT.ID = PR.ID" +
                    "\nLeft JOIN prior_request_diagnosis PRD  with (nolock) on PRD.Prior_Request_ID = PR.ID" +
                    "\nLeft JOIN [dbo].[PRIOR_REQUEST_HEADER]  PRH  with (nolock) on  PRH.Prior_Request_ID = PR.ID" +
                    "\n  left join [dbo].[claim_submission] CS with (nolock) on CS.ID = AT.ID \n" +

                    "\nLeft JOIN PRIOR_AUTH_HEADER PAH  with (nolock) on PAH.authorization_ID = PA.ID" +
                    "\nLeft JOIN PRIOR_AUTH_ACTIVITY PAA  with (nolock) on PAA.Authorization_ID = PA.ID " +
                    "\nLeft JOIN [POST_OFFICE_COMM] POC  with (nolock) on AT.id = POC.trans_id";
                    switch (process)
                    {
                        case "Transaction":
                            query += "\n\nwhere AT.request_id in  ( select data from @datatable ) order by AT.ID";
                            break;
                        case "Payer":
                            query += "\n\nwhere pa.ID_payer in   ( select data from @datatable )";
                            break;
                        case "Claim":
                            query += "\n\nwhere paa.pbm_claim_id in (select data from @datatable )";
                            break;
                    }
                }
                else
                {
                    query = "USE DHPO\nDeclare @TransactionIDs table(T_ID nvarchar(100)) \n" +
                    "insert @TransactionIDs(T_ID) values \n" +
                    split_array(data) +
                    "\n\nSelect sc.claimcode as ClaimCode,st.filename as ClaimFilename,rt.filename as RemittanceFilename ,sc.payerid,st.senderid,st.receiverid \n" +
                    ",rt.senderid , rt.receiverid \n" +
                    "from submissionclaims SC  \n" +
                    "inner join submissiontransactions st on sc.submissionid = st.transactionid \n" +
                    "inner  join remittanceclaims RC on SC.submissionid = sc.submissionid and sc.claimcode = RC.claimcode \n" +
                    "inner join remittancetransactions rt  on rc.remittanceid = rt.transactionid \n" +
                    "where sc.claimcode in (select T_ID from @TransactionIDs)\n";
                }

                return query;
            }

            catch (Exception ex)
            {
                return ex.ToString();
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