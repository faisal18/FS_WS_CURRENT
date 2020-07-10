using System;

namespace FS_WS_WSCTFW.Utilities.DBModification.ExtraClasses
{
    public static class Get_DHPO_Transaction_Detail
    {
        public static string Execute_Process(string[] data,string process)
        {
            try
            {
                string query = Generate_Query(data, process);
                string result = string.Empty;

                if (FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_DHPO_Login_Update_", "DHPO", 2))
                {
                    result = "Process run successfully";
                }
                else
                {
                    result = "Failiure occured running the process";
                }

                return result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        private static string Generate_Query(string[] data,string process)
        {
            string query = string.Empty;
            switch (process)
            {
                case "byClaimId":
                    query = "USE DHPO\n\n" +
                    "\nDECLARE @data_table table(data  varchar(100))\n" +
                    "Insert @data_table(data) values \n" +
                    split_array(data) +
                    "\nselect 'Dispensed' as 'Dispensed',pr.payerid as Receiver, prt.senderID,pr.authorizationtype,PR.Authorizationcode, pr.Submissionid as PR_Transactionnumber, prt.transactiondate, \n" +
                    "pr.memberid, pr.isauthorized as Authorized,\n" +
                    "prt.isviewd as Downloaded,prt.Filename As PR_Filename,prt.FileId as PR_FileID,PRT.downloadeddate,\n" +
                    "pa.IDpayer,pa.Result,pa.denialcode,pa.comments,pa.Authorizationstart,pa.creationdate,\n" +
                    "pat.filename as PA_FileName,pat.fileiD as PA_FileID,pat.downloadeddate,\n" +
                    "sc.ProviderID As Provider,sc.Submissionid as CS_transactionNumber,sc.claimcode,sc.memberid,st.RecordCount,sc.isRemitted,st.isViewd as Downloaded,st.Filename as CS_Filename,\n" +
                    "st.FileID as CS_FileID, sc.Creationdate,\n" +
                    "rct.SenderID as Payer,ra.remittanceID as RA_Transaction_number,ra.PaymentReference, ra.denialcode,RA.idPayer,ra.Datesettlement, rct.Creationdate as RA_uploadedDate, \n" +
                    "rct.isviewd as RA_Downloaded,RCT.Filename as RA_FileName, RCT.FileID as RA_FileID, rct.DownloadedDate\n" +
                    "from PriorRequestAuthorizations PR with (nolock) \n" +
                    "left join priorrequesttransactions prt with (nolock) on pr.submissionid = prt.transactionid\n" +
                    "Left join [PriorAuthorizationAuthorization] PA with (nolock) on pr.Authorizationcode = pa.Authorizationcode\n" +
                    "left join PriorAuthorizationtransaction pat with (nolock) on pa.priorauthorizationid = pat.transactionid\n" +
                    "left join [SubmissionClaims] sc with (nolock) on  pr.Authorizationcode = sc.claimcode\n" +
                    "left join submissiontransactions st with (nolock) on sc.submissionid = st.transactionid\n" +
                    "left join remittanceclaims RA with (nolock) on sc.claimcode = ra.claimcode\n" +
                    "left join remittancetransactions RCT with (nolock) on ra.remittanceid = rct.transactionid\n" +
                    "where PR.Authorizationcode in (select data from @data_table)\n" +
                    "GO\n" +

                    "\nDECLARE @data_table table(data  varchar(100))\n" +
                    "Insert @data_table(data) values \n" +
                     split_array(data) +
                    "select 'Direct Submission' as 'Direct Submission',sc.ProviderID As Provider,sc.Submissionid as CS_transactionNumber,sc.claimcode,sc.memberid,st.RecordCount,sc.isRemitted,st.isViewd as Downloaded,st.Filename as CS_Filename,\n" +
                    "st.FileID as CS_FileID, sc.Creationdate,\n" +
                    "rct.SenderID as Payer,ra.remittanceID as RA_Transaction_number,ra.PaymentReference, ra.denialcode,RA.idPayer,ra.Datesettlement, rct.Creationdate as RA_uploadedDate, \n" +
                    "rct.isviewd as RA_Downloaded,RCT.Filename as RA_FileName, RCT.FileID as RA_FileID, rct.DownloadedDate\n" +
                    "from [SubmissionClaims] sc with (nolock) \n" +
                    "left join submissiontransactions st with (nolock) on sc.submissionid = st.transactionid\n" +
                    "left join remittanceclaims RA with (nolock) on sc.claimcode = ra.claimcode\n" +
                    "left join remittancetransactions RCT with (nolock) on ra.remittanceid = rct.transactionid\n" +
                    "where SC.claimcode  in (select data from @data_table)\n" +
                    "GO\n" +

                    "\nDECLARE @data_table table(data  varchar(100))\n" +
                    "Insert @data_table(data) values \n" +
                    split_array(data) +
                    "\nselect 'Resubmission' as 'Resubmission',sc.ProviderID,sc.PayerID,sc.Submissionid ,sc.Claimcode,st.RecordCount,sc.Memberid,SC.Creationdate,sc.isRemitted, \n" +
                    "st.isViewd as Downloaded, st.Downloadeddate, st.Filename as CS_FileName \n" +
                    "--,ra.Remittanceid,ra.ProviderID,ra.PaymentReference,ra.DenialCode,ra.IDPayer,ra.DateSettlement,ra.CreationDate \n" +
                    "from [SubmissionClaims]  SC with (nolock)   \n" +
                    "left join submissiontransactions st with (nolock) on st.transactionid = sc.submissionid \n" +
                    "--right join remittanceclaims RA with (nolock) on  ra.claimcode  in (select data from @data_table)  \n" +
                    "where sc.claimcode in (select data from @data_table) \n" +
                    "order by sc.providerid \n" +
                    "GO\n" +

                    "\nDECLARE @data_table table(data  varchar(100))\n" +
                    "Insert @data_table(data) values \n" +
                    split_array(data) +
                    "\nselect 'Remittance' as 'Remittance', Rct.SenderID,ra.ProviderID,ra.Remittanceid,ra.Claimcode,\n" +
                    "rct.RecordCount,ra.PaymentReference,ra.DenialCode,ra.IDPayer,ra.DateSettlement,ra.CreationDate,\n" +
                    "rct.isViewd as Downloaded,rct.DownloadedDate,rct.FileName As Remittance_Filename\n" +
                    "from remittanceclaims  ra  with (nolock)\n" +
                    "left join remittancetransactions RCT with (nolock) on ra.remittanceid = rct.transactionid\n" +
                    "where ra.Claimcode in (select claimcode from [SubmissionClaims]  where claimcode  in (select data from @data_table) ) \n" ;
                    break;

                case "byFilename":
                    query = "";
                    break;
            }

            return query;
        }

        private static string split_array(string[] data)
        {
            string concat = string.Empty;
            foreach(string s in data)
            {
                concat += "('" + s + "'),";
            }
            return concat.Remove(concat.Length - 1);
        }
    }
}