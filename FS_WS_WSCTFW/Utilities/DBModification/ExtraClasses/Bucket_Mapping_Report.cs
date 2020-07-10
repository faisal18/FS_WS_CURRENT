using System;


namespace ClinicianAutomation.ExtraClasses
{
    public class Bucket_Mapping_Report
    {
        public string update(string dubai_license)
        {
            try
            {
                string query = generate_query(dubai_license);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "Bucket_Mapping_Report_"+dubai_license, "PBMM", 2);
                return "File created successfully!";
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string generate_query(string dubai_license)
        {
            try
            {
                string query = "\nSelect " +
                                "\n(Select [name] from payer p where p.id = dl.payer_id ) As Payer " +
                                "\n--,dl.code as [ICD CODE] " +
                                "\n,(select '-') +dl.code+ (select '-') as [ICD CODE] " +
                                "\n,(Select replace ([DIAGNOSIS CODE],',','-') from [ICD9CM_2011] icd where dl.code = icd.[DIAGNOSIS CODE] )  as [DIAGNOSIS CODE NAME] " +
                                "\n,(Select replace ([LONG DESCRIPTION],',','-') from [ICD9CM_2011] icd where dl.code = icd.[DIAGNOSIS CODE] ) as [LONG DESCRIPTION] " +
                                "\n,(Select replace ([SHORT DESCRIPTION],',','-') from [ICD9CM_2011] icd where dl.code = icd.[DIAGNOSIS CODE] ) as [SHORT DESCRIPTION] " +
                                "\n--,(select '-') +(Select top 1 Icd9cm from ICD10ICD9_MAPPING map where dl.code = map.Icd9cm ) + (select '-')as ICD9 " +
                                "\n--  ,(select '-') +(Select top 1 [icd10cm] from ICD10ICD9_MAPPING map where dl.code = map.[icd10cm] ) + (select '-')as ICD10 " +
                                "\n,(select '-') +(Select top 1 Icd9cm from ICD10ICD9_MAPPING map where dl.code = map.[icd10cm] ) + (select '-')as ICD9Mapping " +
                                "\n,(select '-') +(Select top 1 [icd10cm] from ICD10ICD9_MAPPING map where dl.code = map.Icd9cm )+ (select '-') as ICD10Mapping " +
                                "\n " +
                                "\n--,dl.[type] " +
                                "\n--,dl.[pcn] " +
                                "\n,(Select top 1 [description] from pcn pcn where pcn.id = dl.[type] ) as Bucket " +
                                "\nFROM  diagnosis_list dl with (nolock) " +
                                "\nwhere DL.payer_id in ( " +
                                "\nselect bucket_mapping_id from payer where dubai_license_no = '" + dubai_license + "'  " +
                                "\n) " +
                                "\n-- and dl.[code] is not null " +
                                "\norder by bucket,dl.code " +
                                "\n--order by dl.code";
                return query;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}