using System;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class contra_indication_query
    {

        public string contra_indication(string[] data, string process)
        {

            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["member"] + "ContraIndication_" + data[0] + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {

                string query = generate_query(data, process);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "_GET_ContraIndiaction_", "PBMM", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "PBMM");
                return "Request Processed Successfully";
                //return "File created successfully in directory " + Environment.NewLine + "" + path + ".sql".ToString();

            }
            catch (Exception ex)
            {
                return "An exception occured " + Environment.NewLine + "" + ex.Message;
            }
        }

        public string generate_query(string[] data, string process)
        {
            string query = null;
            switch (process)
            {
                case "ContraIndicationByClaimId":
                    query ="\nUSE[PBMM]" +

                        "\nif object_id('tempdb..##TempDiagnosis') is not null" +
                        "\ndrop table ##TempDiagnosis" +
                        "\nif object_id('tempdb..##TempActivity') is not null" +
                        "\n drop table ##TempActivity" +

                        "\ndeclare @datatable table(data nvarchar(200))" +
                        "\ninsert @datatable(data) values" +
                        split_array(data) +

                        "\nSElect * into ##TempActivity from PRIOR_AUTH_ACTIVITY with (nolock) where pbm_claim_id in ( select data from @datatable)" +
                        "\nSElect * into ##TempDiagnosis from PRIOR_REQUEST_DIAGNOSIS with (nolock) where prior_request_id in ( select authorization_id from ##TempActivity)" +
                        "\nselect 'Drug Contra Indication' as Title, *from DRUG_CONTRA_INDICATION with (nolock) where drug_code in (select drug_id from ##TempActivity)" +
                        "\nand diagnosis_code in (select code from ##TempDiagnosis)  " +
                        "\ngo";
                    break;

                case "ContraIndicationByDrugCode":
                    query = "\nUSE [PBMM]" +
                            "\ndeclare @datatable table(data nvarchar(200))" +
                            "\ninsert @datatable(data) values" +
                            split_array(data) +

                            "\n--Select Contraindicated diagnosis code against the drug code" +
                            "\nselect * from DRUG_CONTRA_INDICATION with (nolock)" +
                            "\nwhere drug_code in (select data from @datatable)";
                    break;

                case "ContraIndicationByDiagnosisCode":
                    query = "\nUSE [PBMM]" +
                            "\ndeclare @datatable table(data nvarchar(200))" +
                            "\ninsert @datatable(data) values" +
                            split_array(data) +

                            "\n--Select Contraindicated drug code against the code diagnosis" +
                            "\nselect * from DRUG_CONTRA_INDICATION with (nolock)" +
                            "\nwhere diagnosis_code in (select data from @datatable)";
                    break;

                case "IndicationByClaimId":
                    query = "\nUSE[PBMM]" +

                            "\nif object_id('tempdb..##TempDiagnosis') is not null" +
                               "\ndrop table ##TempDiagnosis" +
                            "\nif object_id('tempdb..##TempActivity') is not null" +
                               "\ndrop table ##TempActivity" +

                            "\ndeclare @datatable table(data nvarchar(200))" +
                            "\ninsert @datatable(data)values" +
                            split_array(data) +

                            "\nSelect * into ##TempActivity from PRIOR_AUTH_ACTIVITY with (nolock) where pbm_claim_id in ( select data from @datatable)" +
                            "\nSelect * into ##TempDiagnosis from PRIOR_REQUEST_DIAGNOSIS with (nolock) where prior_request_id in ( select authorization_id from ##TempActivity)" +
                            "\nSelect 'Drug Indication' as Title, * from DRUG_INDICATION with (nolock) where drug_code in (select drug_id from ##TempActivity)" +
                            "\nand diagnosis_code in (select code from ##TempDiagnosis)  " +
                            "\ngo";
                    break;

                case "IndicationByDrugCode":
                    query = "\nUSE [PBMM]" +
                            "\ndeclare @datatable table(data nvarchar(200))" +
                            "\ninsert @datatable(data) values" +
                            split_array(data) +

                            "\n--Select Indicated diagnosis code against the drug code" +
                            "\nselect * from DRUG_INDICATION with (nolock) where drug_code in (select data from @datatable)";
                    break;

                case "IndicationByDiagnosisCode":
                    query = "\nUSE [PBMM]" +
                          "\ndeclare @datatable table(data nvarchar(200))" +
                          "\ninsert @datatable(data) values" +
                          split_array(data) +

                          "\n--Select Indicated drug code against the code diagnosis" +
                          "\nselect * from DRUG_INDICATION with (nolock) where diagnosis_code in (select data from @datatable)";
                    break;
            }
            return query;
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
