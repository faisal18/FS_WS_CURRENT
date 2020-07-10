using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FS_WS_WSCTFW.Utilities.DBModification.ExtraClasses
{
    public class Get_Drug_Details
    {
        public string Run_Process(string[] drugs)
        {
            try
            {
                string query = Generate_Query(drugs);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "All", "_GetDrugDetails", "", 3);
                return "Request Processed Successfully";
            }
            catch(Exception ex)
            {
                return "Exception Occured !\n" + ex;
            }

        }
 
        private string Generate_Query(string[] drugs)
        {
            
            ClinicianAutomation.ExtraClasses.Connections con = new ClinicianAutomation.ExtraClasses.Connections();
            string query =  "\n:connect " + con.get_inline_connection("PBMM") +
                            "\n use PBMM \n" +
                            "declare @drug table(data varchar(200)) \n" +
                            "Insert  @drug (data) values \n" +
                            split_array(drugs) +
                            " \n " +
                            "select 'PBMM' as 'Database',effective_from,effective_To,dha_effective_to,dha_is_valid,is_valid,haad_code,ddc_code,trade_name,price from DRUG_CODE_REF where ddc_code in (select DATA from @drug )  \n " +
                            "select 'PBMM' as 'Database',effective_To,is_valid,dha_effective_to,dha_is_valid,haad_code,ddc_code,trade_name,price,effective_from from DRUG_CODE_REF where haad_code in (select DATA from @drug )  \n " +
                            "select 'PBMM-Region Setting' as 'Database', * from drug_region_setting where code in (select DATA from @drug )  \n " +
                                "\n GO \n" +
                            "\n---Integrated\n" +
                            "\n" +
                            ":connect " + con.get_inline_connection("PBMM") +
                            "\n use erx_db \n " +
                            "declare @drug table(data varchar(200)) \n" +
                            "Insert  @drug (data) values \n" +
                            split_array(drugs) +
                            "\n" +
                            "select 'ERX DB' as 'Database',effective_from,effective_to,is_valid,updated_dt,* from DRUG_CODE_REF where ddc_code in (select DATA from @drug )  \n" +
                            "\nGO\n " +
                            "\n--DHPO \n " +
                            ":connect " + con.get_inline_connection("DHPO") +
                            "\n use DHPO \n " +
                            "declare @drug table(data varchar(200)) \n" +
                            "Insert  @drug (data) values \n" +
                            split_array(drugs) +
                            " \n" +
                            "select 'DHPO' as 'Database',ValidTo,Status,* from tblcodes where ccode in (select DATA from @drug )  \n GO \n" +
                            "--eRx \n " +
                            ":connect " + con.get_inline_connection("ERX") +
                            "\n use erx \n " +
                            "declare @drug table(data varchar(200)) \n" +
                            "Insert  @drug (data) values \n" +
                            split_array(drugs) +
                            " \n " +
                            "select 'ERX'  as 'Database',Activefrom, Activeto,(select Name from activitystatus where activitystatus.id = activitybysource.status) as [Status],Code, \n" +
                            "(select name from source where source.id = activitybysource.source) as [Source], Price \n " +
                            "from activitybysource where code  in (select DATA from @drug )  \n " +
                            "\n" +
                            "\n GO \n" +
                            " --- eclaimlink \n " +
                            ":connect " + con.get_inline_connection("ECLAIMLINK") +
                            "\n use eclaimlink \n " +
                            "declare @drug table(data varchar(200)) \n " +
                            "Insert  @drug (data) values \n " +
                            split_array(drugs) +
                            " \n " +
                            "select 'ECLAIMLINK' as 'Database',* from ddc where DDCcode  in (select DATA from @drug )  \n " +
                            "select 'ECLAIMLINK' as 'Database',isActive,DeleteEffectiveDate,* from lookup_drug where code in (select DATA from @drug )  \n " +
                            " \n" +
                            "\n GO \n" +
                            "-- pbmlink \n" +
                            ":connect " + con.get_inline_connection("PBMLINK") +
                            "\n USE [PBMLINK.NET] \n " +
                            "declare @drug table(data varchar(200)) \n " +
                            "Insert  @drug (data) values \n " +
                            split_array(drugs) +
                            " \n " +
                            "select 'PBMLINK.NET' as 'Database',(select Name from activitystatus where activitystatus.id = activitybysource.status) as [Status],Activefrom, Activeto,Code,\n " +
                            "(select name from source where source.id = activitybysource.source) as [Source], Price \n " +
                            " from activitybysource where code  in (select DATA from @drug )  \n GO \n ";

            return query;

        }

        static string split_array(string[] facilityid)
        {
            string concat = null;
            foreach (string s in facilityid)
            {
                concat += "('" + s + "'),";
            }
            return concat.Remove(concat.Length - 1);
        }
    }
}