using System;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class get_turnaroundtime
    {

        public string script(string license, string startdate, string enddate, int status)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["member"] + "TAT_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            try
            {
                string query = generate_query(license, startdate, enddate, status);

                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "_GetTurnAroundTime_" + license, "PBMM", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "DHPO");
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
                return "An exception occured " + Environment.NewLine + "" + ex.Message;
            }
        }

        public string generate_query(string license, string startdate, string enddate, int status)
        {
            string query = "--------------------------------------------------------------------------------------- \n" +
                    "/* Script Started */ \n" +
                    "--------------------------------------------------------------------------------------- \n" +
                    "  \n" +
                    "Declare @Payercode varchar(100) \n" +
                    "Declare @StartDate datetime \n" +
                    "Declare @ENdDate datetime \n" +
                    "Declare @Details int \n" +
                    "  \n" +
                    "\n\nset @Details = " + status +
                    "\nSet @Payercode = '" + license + "'" +
                    "\nSet @StartDate = '" + startdate + "'" +
                    "\nset @ENdDate = '" + enddate + "'" +
                    "  \n" +
                    "/*  Script Details   */ \n" +
                    "  \n" +
                    "if (@Details=1) \n" +
                    "Begin \n" +
                    "  \n" +
                    "--select * from payer \n" +
                    "--where tpa_dubai_code =  @Payercode \n" +
                    "  \n" +
                    "SELECT \n" +
                    "t.id, t.request_id , \n" +
                    "(select license_no from [PBMM].[dbo].[PROVIDER] p where t.provider_id = p.id ) as Provider_license, \n" +
                    "REPLACE(REPLACE(REPLACE(REPLACE((select name from [PBMM].[dbo].[PROVIDER] p where t.provider_id = p.id ), CHAR(13), ''), CHAR(10), ''),',',';'),'|',' ')as [Provider_Name] , \n" +
                    "(select member_no from [PBMM].[dbo].[MEMBER] m where t.member_id = m.id ) as Member_No, \n" +
                    "(select [group_id] from [PBMM].[dbo].[MEMBER] m where t.member_id = m.id ) as Member_Group_ID, \n" +
                    "(select count([authorization_id]) from pbmm.dbo.prior_auth_activity ac where t.id = ac.[authorization_id] ) as Drugs_Count , \n" +
                    "t.created_dt as Request_Date, \n" +
                    "a.created_dt as Authorization_Date , \n" +
                    "datediff(ss,a.created_dt,t.created_dt) as Time_in_Seconds , \n" +
                    "datediff(mi,a.created_dt,t.created_dt) as Time_in_Mintues \n" +
                    " ,(select [name] from TRANSACTION_STATE TS with (nolock) where ts.id = t.state_id) \n" +
                    "FROM \n" +
                    "[PBMM].[dbo].[AUTHORIZATION_TRANSACTION] t with (nolock) , \n" +
                    "pbmm.dbo.[PRIOR_AUTHORIZATION] a with (nolock) \n" +
                    "where \n" +
                    "t.id = a.id \n" +
                    "and state_id != 5 \n" +
                    "and t.created_dt > @StartDate \n" +
                    "and t.created_dt < @ENdDate \n" +
                    "and t.payer_id in (select ID from payer where tpa_dubai_code =  @Payercode) \n" +
                    "and (select count([authorization_id]) from pbmm.dbo.prior_auth_activity ac where t.id = ac.[authorization_id])> 0 \n" +
                    "order by t.id desc \n" +
                    "end \n" +
                    "else \n" +
                    "begin \n" +
                    "  \n" +
                    "  \n" +
                    "SELECT \n" +
                    "count(*) FROM \n" +
                    "[PBMM].[dbo].[AUTHORIZATION_TRANSACTION] t with (nolock) , \n" +
                    "pbmm.dbo.[PRIOR_AUTHORIZATION] a  with (nolock) \n" +
                    "where \n" +
                    "t.id = a.id \n" +
                    "and state_id != 5 \n" +
                    "and t.created_dt between @StartDate and @ENdDate" +
                    //"and t.created_dt >= @StartDate \n" +
                    //"and t.created_dt <= @ENdDate \n" +
                    "and t.payer_id in (select ID from payer  with (nolock) where tpa_dubai_code =  @Payercode) \n" +
                    "and (select count([authorization_id]) from pbmm.dbo.prior_auth_activity ac  with (nolock) where t.id = ac.[authorization_id]) > 0 \n" +
                    "--order by t.id desc \n" +
                    "end \n" +
                    "  \n" +
                    "--------------------------------------------------------------------------------------- \n" +
                    "/* Script Ends */ \n" +
                    "-------------------------------------------------------------------------------------- \n";





            return query;
        }
    }
}