using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ClinicianAutomation.ExtraClasses
{
    public class Generate_Batch_Transaction
    {

        public string generate_batch(int region, int type, string[] data, string dubailicense, string process)
        {
            try
            {
                Guid uniq = Guid.NewGuid();

                string query = search(region, type, data, dubailicense, process);
                query += update(uniq.ToString(), type);
                query += insert(uniq.ToString(), type, region, dubailicense);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "Generate_Batch_Transaction_" + data[0], "PBMM", 2);
                return query;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        private string search(int region, int type, string[] data, string dubailicense, string process)
        {
            try
            {
                string query = "\nUSE PBMM" +
                "\ndeclare @datatable table(data nvarchar(200))";

                foreach (string item in data)
                {
                    query += "\ninsert @datatable(data) values('" + item + "')";
                }
                

                //"\ninsert @datatable(idpayer) values(" + split_array(idpayer) + ")" +
                query +=
                "\nSelect distinct payer_id from [BATCH_DOWNLOAD_TRANS] where payer_id in " +
                "\n(select ID from payer " +
                "\nwhere tpa_dubai_code = '" + dubailicense + "')" +
                 "\n-- \n " +
                "\n\nSelect " +
                "\nat.ID " +
                "\n,at.request_id" +
                "\n,(select dubai_license_no from PAYER P where P.id = at.payer_id ) as TRNSPayer" +
                "\n,at.state_id as TRNS_State" +
                "\n--,* " +
                "\ninto #TNSID " +
                "\nfrom AUTHORIZATION_TRANSACTION AT with (nolock) " +
                "\nleft join PRIOR_AUTHORIZATION PA with (nolock) on AT.ID = PA.ID " +
                "\nleft join PRIOR_REQUEST PR with (nolock) on AT.ID = PR.ID " +
                "\nleft join CLAIM_SUBMISSION CS with (nolock) on AT.ID=CS.id " +
                "\nleft join PRIOR_REQUEST PRC with (nolock) on at.id = PRc.ID " +
                "\nwhere ";
                if (process == "transactionid")
                {
                    query += "\nat.request_ID in  (  select data from @datatable  ) ";
                }
                else if (process == "idpayer")
                {
                    query += "\npa.id_payer in (select data from @datatable) ";
                }
                query += "\n -- and at.state_id in (3,4) " +
                "\nand at.Payer_id in (" +
                "\nSelect distinct payer_id from [BATCH_DOWNLOAD_TRANS] where payer_id in " +
                "\n(select ID from payer " +
                "\nwhere tpa_dubai_code = '" + dubailicense + "'))" +
                  "\n-- \n " +
                "\nselect *   from #TNSID \n";
                return query;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        private string update(string uniq, int type)
        {
            try
            {
                string query = "\n \n                 declare @transactionCount1 int " +
                    "\n\n             set  @transactionCount1 = (select count(*) from #TNSID )" +
                    "\n\n if @transactionCount1 > 0 "+
                    "\n\nBegin" +
                    "\n\nPrint ' Transaction found for Batch Generation UPdate '" ;
                switch (type)
                {
                    case 1:
                        query += "\n--\nupdate PRIOR_REQUEST " +
                                "\nset download_batch_id = '" + uniq + "'" +
                                "\nwhere id in (select id from #TNSID) " +
                                "\nand (download_batch_id IN ('', '0') OR download_batch_id IS NULL)" +

                                "\n--\nselect id,download_batch_id from PRIOR_REQUEST " +
                                "\nwhere id in (select ID from #TNSID)";
                        break;
                    case 2:
                        query += "\n--\nupdate PRIOR_AUTHORIZATION " +
                                "\nset download_batch_id = '" + uniq + "'" +
                                "\nwhere id in (select id from #TNSID) " +
                                "\nand (download_batch_id IN ('', '0') OR download_batch_id IS NULL)"+

                                "\n--\nselect id,download_batch_id from PRIOR_AUTHORIZATION " +
                                "\nwhere id in (select ID from #TNSID)";
                        break;
                    case 3:
                        query += "\n--\nupdate CLAIM_SUBMISSION " +
                              "\nset download_batch_id = '" + uniq + "'" +
                              "\nwhere id in (select id from #TNSID) " +
                              "\nand (download_batch_id IN ('', '0') OR download_batch_id IS NULL)"+

                              "\n--\nselect id,download_batch_id from CLAIM_SUBMISSION " +
                              "\nwhere id in (select ID from #TNSID)";
                        break;
                    case 4:
                        query += "\n--\nupdate PRIOR_REQUEST " +
                               "\nset download_canceled_batch_id  = '" + uniq + "'" +
                               "\nwhere id in (select id from #TNSID) " +
                               "\nand (download_canceled_batch_id IN ('', '0') OR download_canceled_batch_id IS NULL)" +

                               "\n--\nselect id,download_canceled_batch_id from PRIOR_REQUEST " +
                               "\nwhere id in (select ID from #TNSID)";
                        break;

                }
              query  +=  "\n\nend \n\n else \n \n Begin \n \n Print ' No Transaction found for Batch Generation update' \n \n end \n ";

                return query;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        private string insert(string uniq, int type, int region, string dubailicense)
        {
            try
            {

string query =  "\n \n                 declare @transactionCount int " +
                    "\n\n             set  @transactionCount = (select count(*) from #TNSID )" +
                    "\n\n if @transactionCount > 0 " + 
                    "\n\nBegin" +
                    "\n\nPrint ' Transaction found for Batch Generation'" + 
                 "\n\nINSERT INTO [dbo].[BATCH_DOWNLOAD_TRANS]" +
                                "\n([batch_id],[transaction_type],[region],[date_time],[is_downloaded],[payer_id])" +
                                "\nVALUES" +
                                "\n('" + uniq + "'," + type + "," + region + ",getdate(),0,(Select distinct payer_id from [BATCH_DOWNLOAD_TRANS]" +
                                "\nwhere payer_id in (select ID from payer where tpa_dubai_code = '" + dubailicense + "')))" +
                                "\n--\nSelect * from BATCH_DOWNLOAD_TRANS where batch_id = '" + uniq + "'" +
                                "\n\nend \n\n else \n \n Begin \n \n Print ' No Transaction found for Batch Generation' \n \n end \n ";
                return query;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
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