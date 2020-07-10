using System;


namespace ClinicianAutomation.ExtraClasses
{
    public class Transactions_Detail_Report
    {
        

        private string run_it(string query)
        {
            try
            {
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "Transactions_Detail_Report" , "PBMM", 2);
                return "File Created Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string update_byAll(string[] dubai_license,string[] provider_license,string start_date,string end_date,string process)
        {
            try
            {
                string query = "";
                if(process == "detail")
                {
                    query = generate_query_byAll(dubai_license, provider_license, start_date, end_date);
                }
                else if(process == "count")
                {
                    query = generate_query_count_byAll(dubai_license, provider_license, start_date, end_date);
                }
                return run_it(query);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string update_byProviderLicense(string[] provider_license, string start_date, string end_date,string process)
        {
            try
            {
                string query = "";
                if (process == "detail")
                {
                    query = generate_query_byProviderLicense(provider_license, start_date, end_date);
                }
                else if (process == "count")
                {
                    query = generate_query_count_byProviderLicense(provider_license, start_date, end_date);
                }
                return run_it(query);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string update_byDubaiLicense(string[] dubai_license_no, string start_date, string end_date,string process)
        {
            try
            {
                string query = "";
                if (process == "detail")
                {
                    query = generate_query_byDubaiLicense(dubai_license_no, start_date, end_date);
                }
                else if (process == "count")
                {
                    query = generate_query_count_byDubaiLicense(dubai_license_no, start_date, end_date);
                }
                return run_it(query);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string update_byDate(string start_date, string end_date,string process)
        {
            try
            {
                string query = "";
                if (process == "detail")
                {
                    query = generate_query_byDate(start_date, end_date);
                }
                else if (process == "count")
                {
                    query = generate_query_count_byDate(start_date, end_date);
                }
                return run_it(query);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string generate_query_byAll(string[] dubai_license_no, string[] provider_license, string start_date, string end_date)
        {
            string query = "\n\ndeclare @datatable table(provider_license nvarchar(200),dubai_license_no nvarchar(200))" +
                           "\ninsert @datatable(provider_license) values";


            foreach (string item in provider_license)
            {
                query += "\ninsert @datatable(provider_license) values('" + item + "')";
            }


            query += "\ninsert @datatable(dubai_license_no) values";


            foreach (string item in dubai_license_no)
            {
                query += "\ninsert @datatable(dubai_license_no) values('" + item + "')";
            }


            query += "\n\nSELECT  " +
                            //"\n--count(*) " +
                            //"\n--TOP 10000 " +
                            "\nrequest_id as 'Transaction ID' " +
                            "\n--, pr.member_id " +
                            "\n,(select  top 1 group_id + '-' + division_code from member m where member_no = pr.member_Id order by m.last_update) as PolicyName " +
                            "\n,(Select name from PAYER P where p.id = at.payer_id ) as InsuranceCompanyName " +
                            "\n,(select name from PROVIDER P where P.id = at.provider_id) as 'Clinic-PharmacyName' " +
                            "\n,at.created_dt as 'Date of Prescription'   " +
                            "\n,(select  top 1 first_name + '-' + last_name from member where member_no = pr.member_Id) as MemberName " +
                            "\n, (select top 1 description from diagnosis_list DL inner join PCN P on DL.Type = P.ID where dl.pcn = at.pcn_code  and dl.payer_id = at.payer_id) as Benefit " +
                            "\n,paa.clinician as 'Prescribing doctor name' " +
                            "\n,PRD.code " +
                            "\n,paa.drug_quantity as Quantity " +
                            "\n,paa.Drug_code as 'Authorize Code' " +
                            "\n,(select top 1 trade_name  from drug_code_ref where haad_code = paa.Drug_code ) as 'Authorized Description' " +
                            "\n,PA.ID_payer as 'Authorized ID Payer' " +
                            "\n,paa.Payment_Amount as 'Authorized Payment' " +
                            "\n,paa.patient_Share as 'Authorized Patient Share' " +
                            "\n,paa.denial_code as 'Authorized Rejection Code' " +
                            "\n,paad.comment as 'Authorized Comment' " +
                            "\n,paa.dur_msg as 'Authorized DUR Message' " +
                            "\n,paa.pbm_claim_id as 'MI Reference No' " +
                            "\nFROM [PBMM].[dbo].[AUTHORIZATION_TRANSACTION] AT with (nolock) " +
                            "\ninner join[dbo].[PRIOR_AUTHORIZATION] PA WITH (NOLOCK) on  AT.id = PA.ID " +
                            "\ninner join [dbo].[PRIOR_REQUEST] PR WITH (NOLOCK) on AT.ID = PR.ID " +
                            "\ninner join [dbo].[PRIOR_REQUEST_HEADER]  PRH WITH (NOLOCK) on  PRH.Prior_Request_ID = PR.ID " +
                            "\ninner join [dbo].[PRIOR_REQUEST_diagnosis]  PRD WITH (NOLOCK) on  PRD.Prior_Request_ID = PR.ID " +
                            "\ninner join PRIOR_AUTH_HEADER PAH WITH (NOLOCK) on PAH.authorization_ID = PA.ID " +
                            "\ninner join PRIOR_AUTH_ACTIVITY PAA WITH (NOLOCK) on PAA.Authorization_ID = PA.ID " +
                            "\nleft outer join PRIOR_AUTH_ACTIVITY_DENIAL PAAD WITH (NOLOCK) on PAAD.Authorization_id = pa.id " +
                            //"\n--inner join [POST_OFFICE_COMM] POC on AT.id = POC.trans_id " +
                            "\nwhere " +

                            //Provider License
                            "\nat.Provider_id in (Select ID from provider where License_no in (select provider_license from @datatable)) " +
                            "\nand " +

                            //Dates
                            "\nat.created_dt between '" + start_date + "' and '" + end_date + "' " +

                            //Dubai License
                            "\nand at.payer_id in (Select id from payer where dubai_license_no in (select dubai_license_no from @datatable)) " +
                            "\norder by at.created_dt";
            return query;
        }
        public string generate_query_byProviderLicense(string[] provider_license, string start_date, string end_date)
        {
            string query = "\n\ndeclare @datatable table(provider_license nvarchar(200))";


            foreach (string item in provider_license)
            {
                query += "\ninsert @datatable(provider_license) values('" + item + "')";
            }


            query += "\n\nSELECT  " +
                            //"\n--count(*) " +
                            //"\n--TOP 10000 " +
                            "\nrequest_id as 'Transaction ID' " +
                            "\n--, pr.member_id " +
                            "\n,(select  top 1 group_id + '-' + division_code from member m where member_no = pr.member_Id order by m.last_update) as PolicyName " +
                            "\n,(Select name from PAYER P where p.id = at.payer_id ) as InsuranceCompanyName " +
                            "\n,(select name from PROVIDER P where P.id = at.provider_id) as 'Clinic-PharmacyName' " +
                            "\n,at.created_dt as 'Date of Prescription'   " +
                            "\n,(select  top 1 first_name + '-' + last_name from member where member_no = pr.member_Id) as MemberName " +
                            "\n, (select top 1 description from diagnosis_list DL inner join PCN P on DL.Type = P.ID where dl.pcn = at.pcn_code  and dl.payer_id = at.payer_id) as Benefit " +
                            "\n,paa.clinician as 'Prescribing doctor name' " +
                            "\n,PRD.code " +
                            "\n,paa.drug_quantity as Quantity " +
                            "\n,paa.Drug_code as 'Authorize Code' " +
                            "\n,(select top 1 trade_name  from drug_code_ref where haad_code = paa.Drug_code ) as 'Authorized Description' " +
                            "\n,PA.ID_payer as 'Authorized ID Payer' " +
                            "\n,paa.Payment_Amount as 'Authorized Payment' " +
                            "\n,paa.patient_Share as 'Authorized Patient Share' " +
                            "\n,paa.denial_code as 'Authorized Rejection Code' " +
                            "\n,paad.comment as 'Authorized Comment' " +
                            "\n,paa.dur_msg as 'Authorized DUR Message' " +
                            "\n,paa.pbm_claim_id as 'MI Reference No' " +
                            "\nFROM [PBMM].[dbo].[AUTHORIZATION_TRANSACTION] AT with (nolock) " +
                            "\ninner join[dbo].[PRIOR_AUTHORIZATION] PA WITH (NOLOCK) on  AT.id = PA.ID " +
                            "\ninner join [dbo].[PRIOR_REQUEST] PR WITH (NOLOCK) on AT.ID = PR.ID " +
                            "\ninner join [dbo].[PRIOR_REQUEST_HEADER]  PRH WITH (NOLOCK) on  PRH.Prior_Request_ID = PR.ID " +
                            "\ninner join [dbo].[PRIOR_REQUEST_diagnosis]  PRD WITH (NOLOCK) on  PRD.Prior_Request_ID = PR.ID " +
                            "\ninner join PRIOR_AUTH_HEADER PAH WITH (NOLOCK) on PAH.authorization_ID = PA.ID " +
                            "\ninner join PRIOR_AUTH_ACTIVITY PAA WITH (NOLOCK) on PAA.Authorization_ID = PA.ID " +
                            "\nleft outer join PRIOR_AUTH_ACTIVITY_DENIAL PAAD WITH (NOLOCK) on PAAD.Authorization_id = pa.id " +
                            //"\n--inner join [POST_OFFICE_COMM] POC on AT.id = POC.trans_id " +
                            "\nwhere " +

                            //Provider License
                            "\nat.Provider_id in (Select ID from provider where License_no in (select provider_license from @datatable)) " +
                            "\nand " +

                            //Dates
                            "\nat.created_dt between '" + start_date + "' and '" + end_date + "' "+
                            "\norder by at.created_dt";

            //Dubai License
            //"\nand at.payer_id in (Select id from payer where dubai_license_no in (select dubai_license_no from @datatable)) ";
            return query;
        }
        public string generate_query_byDubaiLicense(string[] dubai_license_no,string start_date, string end_date)
        {
            string query = "\n\ndeclare @datatable table(dubai_license_no nvarchar(200))";


            foreach (string item in dubai_license_no)
            {
                query += "\ninsert @datatable(dubai_license_no) values('" + item + "')";
            }


            query += "\n\nSELECT  " +
                            "\nrequest_id as 'Transaction ID' " +
                            "\n--, pr.member_id " +
                            "\n,(select  top 1 group_id + '-' + division_code from member m where member_no = pr.member_Id order by m.last_update) as PolicyName " +
                            "\n,(Select name from PAYER P where p.id = at.payer_id ) as InsuranceCompanyName " +
                            "\n,(select name from PROVIDER P where P.id = at.provider_id) as 'Clinic-PharmacyName' " +
                            "\n,at.created_dt as 'Date of Prescription'   " +
                            "\n,(select  top 1 first_name + '-' + last_name from member where member_no = pr.member_Id) as MemberName " +
                            "\n, (select top 1 description from diagnosis_list DL inner join PCN P on DL.Type = P.ID where dl.pcn = at.pcn_code  and dl.payer_id = at.payer_id) as Benefit " +
                            "\n,paa.clinician as 'Prescribing doctor name' " +
                            "\n,PRD.code " +
                            "\n,paa.drug_quantity as Quantity " +
                            "\n,paa.Drug_code as 'Authorize Code' " +
                            "\n,(select top 1 trade_name  from drug_code_ref where haad_code = paa.Drug_code ) as 'Authorized Description' " +
                            "\n,PA.ID_payer as 'Authorized ID Payer' " +
                            "\n,paa.Payment_Amount as 'Authorized Payment' " +
                            "\n,paa.patient_Share as 'Authorized Patient Share' " +
                            "\n,paa.denial_code as 'Authorized Rejection Code' " +
                            "\n,paad.comment as 'Authorized Comment' " +
                            "\n,paa.dur_msg as 'Authorized DUR Message' " +
                            "\n,paa.pbm_claim_id as 'MI Reference No' " +
                            "\nFROM [PBMM].[dbo].[AUTHORIZATION_TRANSACTION] AT with (nolock) " +
                            "\ninner join[dbo].[PRIOR_AUTHORIZATION] PA WITH (NOLOCK) on  AT.id = PA.ID " +
                            "\ninner join [dbo].[PRIOR_REQUEST] PR WITH (NOLOCK) on AT.ID = PR.ID " +
                            "\ninner join [dbo].[PRIOR_REQUEST_HEADER]  PRH WITH (NOLOCK) on  PRH.Prior_Request_ID = PR.ID " +
                            "\ninner join [dbo].[PRIOR_REQUEST_diagnosis]  PRD WITH (NOLOCK) on  PRD.Prior_Request_ID = PR.ID " +
                            "\ninner join PRIOR_AUTH_HEADER PAH WITH (NOLOCK) on PAH.authorization_ID = PA.ID " +
                            "\ninner join PRIOR_AUTH_ACTIVITY PAA WITH (NOLOCK) on PAA.Authorization_ID = PA.ID " +
                            "\nleft outer join PRIOR_AUTH_ACTIVITY_DENIAL PAAD WITH (NOLOCK) on PAAD.Authorization_id = pa.id " +
                            //"\n--inner join [POST_OFFICE_COMM] POC on AT.id = POC.trans_id " +
                            "\nwhere " +

                            //Provider License
                            //"\nat.Provider_id in (Select ID from provider where License_no in (select provider_license from @datatable)) " +
                            //"\nand " +

                            //Dates
                            "\nat.created_dt between '" + start_date + "' and '" + end_date + "' " +

                            //Dubai License
                            "\nand at.payer_id in (Select id from payer where dubai_license_no in (select dubai_license_no from @datatable)) " +
                            "\norder by at.created_dt";
            return query;
        }
        public string generate_query_byDate(string start_date,string end_date)
        {
            string query = //"\n\ndeclare @datatable table(dubai_license_no nvarchar(200))" +
                           //"\ninsert @datatable(provider_license) values" +
                           //split_array(provider_license) +
                           //"\ninsert @datatable(dubai_license_no) values" +
                           //split_array(dubai_license_no) +

                          "\n\nSELECT  " +
                          "\nrequest_id as 'Transaction ID' " +
                          "\n--, pr.member_id " +
                          "\n,(select  top 1 group_id + '-' + division_code from member m where member_no = pr.member_Id order by m.last_update) as PolicyName " +
                          "\n,(Select name from PAYER P where p.id = at.payer_id ) as InsuranceCompanyName " +
                          "\n,(select name from PROVIDER P where P.id = at.provider_id) as 'Clinic-PharmacyName' " +
                          "\n,at.created_dt as 'Date of Prescription'   " +
                          "\n,(select  top 1 first_name + '-' + last_name from member where member_no = pr.member_Id) as MemberName " +
                          "\n, (select top 1 description from diagnosis_list DL inner join PCN P on DL.Type = P.ID where dl.pcn = at.pcn_code  and dl.payer_id = at.payer_id) as Benefit " +
                          "\n,paa.clinician as 'Prescribing doctor name' " +
                          "\n,PRD.code " +
                          "\n,paa.drug_quantity as Quantity " +
                          "\n,paa.Drug_code as 'Authorize Code' " +
                          "\n,(select top 1 trade_name  from drug_code_ref where haad_code = paa.Drug_code ) as 'Authorized Description' " +
                          "\n,PA.ID_payer as 'Authorized ID Payer' " +
                          "\n,paa.Payment_Amount as 'Authorized Payment' " +
                          "\n,paa.patient_Share as 'Authorized Patient Share' " +
                          "\n,paa.denial_code as 'Authorized Rejection Code' " +
                          "\n,paad.comment as 'Authorized Comment' " +
                          "\n,paa.dur_msg as 'Authorized DUR Message' " +
                          "\n,paa.pbm_claim_id as 'MI Reference No' " +
                          "\nFROM [PBMM].[dbo].[AUTHORIZATION_TRANSACTION] AT with (nolock) " +
                          "\ninner join[dbo].[PRIOR_AUTHORIZATION] PA WITH (NOLOCK) on  AT.id = PA.ID " +
                          "\ninner join [dbo].[PRIOR_REQUEST] PR WITH (NOLOCK) on AT.ID = PR.ID " +
                          "\ninner join [dbo].[PRIOR_REQUEST_HEADER]  PRH WITH (NOLOCK) on  PRH.Prior_Request_ID = PR.ID " +
                          "\ninner join [dbo].[PRIOR_REQUEST_diagnosis]  PRD WITH (NOLOCK) on  PRD.Prior_Request_ID = PR.ID " +
                          "\ninner join PRIOR_AUTH_HEADER PAH WITH (NOLOCK) on PAH.authorization_ID = PA.ID " +
                          "\ninner join PRIOR_AUTH_ACTIVITY PAA WITH (NOLOCK) on PAA.Authorization_ID = PA.ID " +
                          "\nleft outer join PRIOR_AUTH_ACTIVITY_DENIAL PAAD WITH (NOLOCK) on PAAD.Authorization_id = pa.id " +
                          //"\n--inner join [POST_OFFICE_COMM] POC on AT.id = POC.trans_id " +
                          "\nwhere " +

                          //Provider License
                          //"\nat.Provider_id in (Select ID from provider where License_no in (select provider_license from @datatable)) " +
                          //"\nand " +

                          //Dates
                          "\nat.created_dt between '" + start_date + "' and '" + end_date + "' " +
                            "\norder by at.created_dt";

            //Dubai License
            //"\nand at.payer_id in (Select id from payer where dubai_license_no in (select dubai_license_no from @datatable)) ";
            return query;
        }

        public string generate_query_count_byAll(string[] dubai_license_no, string[] provider_license, string start_date, string end_date)
        {
            string query = "\n\ndeclare @datatable table(provider_license nvarchar(200),dubai_license_no nvarchar(200))" +
                           "\ninsert @datatable(provider_license) values";


            foreach (string item in provider_license)
            {
                query += "\ninsert @datatable(provider_license) values('" + item + "')";
            }


            query += "\ninsert @datatable(dubai_license_no) values";


            foreach (string item in dubai_license_no)
            {
                query += "\ninsert @datatable(dubai_license_no) values('" + item + "')";
            }


            query += "\n\nSELECT  " +
                           "\ncount(*) " +
                        
                           "\nFROM [PBMM].[dbo].[AUTHORIZATION_TRANSACTION] AT with (nolock) " +
                           "\ninner join[dbo].[PRIOR_AUTHORIZATION] PA WITH (NOLOCK) on  AT.id = PA.ID " +
                           "\ninner join [dbo].[PRIOR_REQUEST] PR WITH (NOLOCK) on AT.ID = PR.ID " +
                           "\ninner join [dbo].[PRIOR_REQUEST_HEADER]  PRH WITH (NOLOCK) on  PRH.Prior_Request_ID = PR.ID " +
                           "\ninner join [dbo].[PRIOR_REQUEST_diagnosis]  PRD WITH (NOLOCK) on  PRD.Prior_Request_ID = PR.ID " +
                           "\ninner join PRIOR_AUTH_HEADER PAH WITH (NOLOCK) on PAH.authorization_ID = PA.ID " +
                           "\ninner join PRIOR_AUTH_ACTIVITY PAA WITH (NOLOCK) on PAA.Authorization_ID = PA.ID " +
                           "\nleft outer join PRIOR_AUTH_ACTIVITY_DENIAL PAAD WITH (NOLOCK) on PAAD.Authorization_id = pa.id " +
                           //"\n--inner join [POST_OFFICE_COMM] POC on AT.id = POC.trans_id " +
                           "\nwhere " +

                            //Provider License
                            "\nat.Provider_id in (Select ID from provider where License_no in (select provider_license from @datatable)) " +
                            "\nand " +

                            //Dates
                            "\nat.created_dt between '" + start_date + "' and '" + end_date + "' " +

                            //Dubai License
                            "\nand at.payer_id in (Select id from payer where dubai_license_no in (select dubai_license_no from @datatable)) " +
                            //"\norder by at.created_dt" +
                            "";
            return query;
        }
        public string generate_query_count_byProviderLicense(string[] provider_license, string start_date, string end_date)
        {
            string query = "\n\ndeclare @datatable table(provider_license nvarchar(200))";


            foreach (string item in provider_license)
            {
                query += "\ninsert @datatable(provider_license) values('" + item + "')";
            }


            query += "\n\nSELECT  " +
                           "\ncount(*) " +

                           "\nFROM [PBMM].[dbo].[AUTHORIZATION_TRANSACTION] AT with (nolock) " +
                           "\ninner join[dbo].[PRIOR_AUTHORIZATION] PA WITH (NOLOCK) on  AT.id = PA.ID " +
                           "\ninner join [dbo].[PRIOR_REQUEST] PR WITH (NOLOCK) on AT.ID = PR.ID " +
                           "\ninner join [dbo].[PRIOR_REQUEST_HEADER]  PRH WITH (NOLOCK) on  PRH.Prior_Request_ID = PR.ID " +
                           "\ninner join [dbo].[PRIOR_REQUEST_diagnosis]  PRD WITH (NOLOCK) on  PRD.Prior_Request_ID = PR.ID " +
                           "\ninner join PRIOR_AUTH_HEADER PAH WITH (NOLOCK) on PAH.authorization_ID = PA.ID " +
                           "\ninner join PRIOR_AUTH_ACTIVITY PAA WITH (NOLOCK) on PAA.Authorization_ID = PA.ID " +
                           "\nleft outer join PRIOR_AUTH_ACTIVITY_DENIAL PAAD WITH (NOLOCK) on PAAD.Authorization_id = pa.id " +
                           //"\n--inner join [POST_OFFICE_COMM] POC on AT.id = POC.trans_id " +
                           "\nwhere " +

                            //Provider License
                            "\nat.Provider_id in (Select ID from provider where License_no in (select provider_license from @datatable)) " +
                            "\nand " +

                            //Dates
                            "\nat.created_dt between '" + start_date + "' and '" + end_date + "' " +
                            //"\norder by at.created_dt" +
                            "";

            //Dubai License
            //"\nand at.payer_id in (Select id from payer where dubai_license_no in (select dubai_license_no from @datatable)) ";
            return query;
        }
        public string generate_query_count_byDubaiLicense(string[] dubai_license_no, string start_date, string end_date)
        {
            string query = "\n\ndeclare @datatable table(dubai_license_no nvarchar(200))";


                     foreach (string item in dubai_license_no)
            {
                query += "\ninsert @datatable(dubai_license_no) values('" + item + "')";
            }


          query +=  "\n\nSELECT  " +
                           "\ncount(*) " +

                           "\nFROM [PBMM].[dbo].[AUTHORIZATION_TRANSACTION] AT with (nolock) " +
                           "\ninner join[dbo].[PRIOR_AUTHORIZATION] PA WITH (NOLOCK) on  AT.id = PA.ID " +
                           "\ninner join [dbo].[PRIOR_REQUEST] PR WITH (NOLOCK) on AT.ID = PR.ID " +
                           "\ninner join [dbo].[PRIOR_REQUEST_HEADER]  PRH WITH (NOLOCK) on  PRH.Prior_Request_ID = PR.ID " +
                           "\ninner join [dbo].[PRIOR_REQUEST_diagnosis]  PRD WITH (NOLOCK) on  PRD.Prior_Request_ID = PR.ID " +
                           "\ninner join PRIOR_AUTH_HEADER PAH WITH (NOLOCK) on PAH.authorization_ID = PA.ID " +
                           "\ninner join PRIOR_AUTH_ACTIVITY PAA WITH (NOLOCK) on PAA.Authorization_ID = PA.ID " +
                           "\nleft outer join PRIOR_AUTH_ACTIVITY_DENIAL PAAD WITH (NOLOCK) on PAAD.Authorization_id = pa.id " +
                           //"\n--inner join [POST_OFFICE_COMM] POC on AT.id = POC.trans_id " +
                           "\nwhere " +

                            ////Provider License
                            //"\nat.Provider_id in (Select ID from provider where License_no in (select provider_license from @datatable)) " +
                            //"\nand " +

                            //Dates
                            "\nat.created_dt between '" + start_date + "' and '" + end_date + "' " +

                            //Dubai License
                            "\nand at.payer_id in (Select id from payer where dubai_license_no in (select dubai_license_no from @datatable)) " +
                            //"\norder by at.created_dt"
                            "";
            return query;
        }
        public string generate_query_count_byDate(string start_date,string end_date)
        {
            string query = //"\n\ndeclare @datatable table(dubai_license_no nvarchar(200))" +

                          // "\ninsert @datatable(dubai_license_no) values" +
                          //split_array(dubai_license_no) +

                          "\n\nSELECT  " +
                          "\ncount(*) " +

                          "\nFROM [PBMM].[dbo].[AUTHORIZATION_TRANSACTION] AT with (nolock) " +
                          "\ninner join[dbo].[PRIOR_AUTHORIZATION] PA WITH (NOLOCK) on  AT.id = PA.ID " +
                          "\ninner join [dbo].[PRIOR_REQUEST] PR WITH (NOLOCK) on AT.ID = PR.ID " +
                          "\ninner join [dbo].[PRIOR_REQUEST_HEADER]  PRH WITH (NOLOCK) on  PRH.Prior_Request_ID = PR.ID " +
                          "\ninner join [dbo].[PRIOR_REQUEST_diagnosis]  PRD WITH (NOLOCK) on  PRD.Prior_Request_ID = PR.ID " +
                          "\ninner join PRIOR_AUTH_HEADER PAH WITH (NOLOCK) on PAH.authorization_ID = PA.ID " +
                          "\ninner join PRIOR_AUTH_ACTIVITY PAA WITH (NOLOCK) on PAA.Authorization_ID = PA.ID " +
                          "\nleft outer join PRIOR_AUTH_ACTIVITY_DENIAL PAAD WITH (NOLOCK) on PAAD.Authorization_id = pa.id " +
                          //"\n--inner join [POST_OFFICE_COMM] POC on AT.id = POC.trans_id " +
                          "\nwhere " +

                           ////Provider License
                           //"\nat.Provider_id in (Select ID from provider where License_no in (select provider_license from @datatable)) " +
                           //"\nand " +

                           //Dates
                           "\nat.created_dt between '" + start_date + "' and '" + end_date + "' " +
                            //"\norder by at.created_dt"
                            "";

            //Dubai License
            //"\nand at.payer_id in (Select id from payer where dubai_license_no in (select dubai_license_no from @datatable)) ";
            return query;
        }

        

        string split_array(string[] data)
        {
            try
            {
                string concat = null;
                foreach (string s in data)
                {
                    concat += "('" + s + "'),";
                }
                return concat.Remove(concat.Length - 1);

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        int count(string[] data)
        {
            int count = 0;
            foreach (string s in data)
            {
                count++;
            }
            return count;
        }
    }
}