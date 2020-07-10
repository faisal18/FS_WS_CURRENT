using System;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class get_credentials
    {
        private Object thislock = new object();

        public string get_creds(string[] facilityid, string opt)
        {
            lock (thislock)
            {
                create_script cs = new create_script();
                create_batch cb = new create_batch();
                string path = ConfigurationManager.AppSettings["get_creds"].ToString() + "Get_creds_" + facilityid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                try
                {

//                    string.Join("_", facilityid)
//string.Join("_", facilityid).Substring(0, 20)
                    string query = generate_query(facilityid, opt);
                    string provName = string.Join("_", facilityid);
                    if (provName.Length > 20)
                    {
                        provName = provName.Substring(0, 20);
                    }
                    FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "All", "_getCredentials_" + provName , "", 3);
                    //cs.script_create(query, path);
                    //cb.batch_create(path);

                    return "Request Processed Successfully";

                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
        }

        private string generate_query(string[] facilityid, string opt)
        {
            string query = null;
            Connections con = new Connections();
            if (opt == "provider")
            {



                query = //":connect  10.156.62.42 -U fazeel -P Dell@123" +
                    ":connect " + con.get_inline_connection("ECLAIMLINKPORTAL") +
                "\nUSE EclaimLinkPortal" +
                "\nGO" +
                "\ndeclare @faciltiy table(facilityid nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS)" +
                "\ninsert @faciltiy(facilityid) values " +
                split_array(facilityid) +
                "\nselect 'ECLAIM LINK PORTAL' as 'DB Name',Username,Password,facility_name,License,activated ,userRole  " +
                "\nfrom v_users" +
                "\nwhere license IN (select facilityid from @faciltiy) " +
                "\nGO" +

                "\n\n" + //":connect  10.156.62.42 -U fazeel -P Dell@123" +
                ":connect " + con.get_inline_connection("ECLAIMLINK") +
                "\nUSE Eclaimlink" +
                "\nGO" +
                "\ndeclare @faciltiy table(facilityid nvarchar(100))" +
                "\ninsert @faciltiy(facilityid) values " +
                split_array(facilityid) +
                "\nSelect 'ECLAIM LINK' as 'DB Name',username, password,PO_Username, PO_Password,name,facilityname , isactive, IsActiveFacility, FacilityType , providerlicense, PRovidername, PO_SenderID ,IsAdmin " +
                "\nfrom v_provider_user" +
                "\nwhere ProviderLicense  IN (select facilityid from @faciltiy) " +
                "\nGO" +

                "\n\n" +//":connect  10.156.62.42 -U fazeel -P Dell@123" +
                ":connect " + con.get_inline_connection("ERX") +
                "\nUSE ERX" +
                "\nGO" +
                "\ndeclare @faciltiy table(facilityid nvarchar(100))" +
                "\ninsert @faciltiy(facilityid) values " +
                split_array(facilityid) +
                "\nSelect  'ERX' as 'DB Name',Username, Password,POUsername, POPassword,Name, Active, Deleted, License,ProviderName, [Type] " +
                "\nfrom V_User" +
                "\nwhere license  IN (select facilityid from @faciltiy) " +
                "\nGO" +

                "\n\n" +//":connect  10.162.176.24 -U fshaikh -P Dell@888" +
                ":connect " + con.get_inline_connection("DHPO") +
                "\nUSE DHPO" +
                "\nGO" +
                "\ndeclare @faciltiy table(facilityid nvarchar(100))" +
                "\ninsert @faciltiy(facilityid) values " +
                split_array(facilityid) +
                "\nselect 'DHPO' as 'DB Name', UserName, Password,Name,LicenseID, ProviderType, IsActive, Region " +
                "\nfrom Provider " +
                "\nwhere LicenseID IN (select facilityid from @faciltiy)  " +
                "\nGO" +

                "\n\n:connect" + con.get_inline_connection("OICProvider") + //":connect  10.11.13.163 -U fshaikh -P Dell@123" +

                "\nUSE [OIC.eAuth]" +
                "\nGO" +
                "\ndeclare @faciltiy table(facilityid nvarchar(100))" +
                 "\ninsert @faciltiy(facilityid) values " +
                 split_array(facilityid) +
                "\nSelect 'OIC.eAuth' as 'DB Name',[POUsername],[POPassword],[Name], [Active] " +
                "\nfrom [Provider]" +
                "\nwhere License IN (select facilityid from @faciltiy) " +
                "\nGO" +

                "\n\n" +//":connect  10.5.3.42 -U fshaikh -P Dell@123" +
                ":connect " + con.get_inline_connection("PBMM") +
                "\nUSE PBMM" +
                "\nGO" +
                "\ndeclare @faciltiy table(facilityid nvarchar(100))" +
                "\ninsert @faciltiy(facilityid) values " +
                split_array(facilityid) +
                "\nselect 'PBM Switch' as 'DB Name', post_office_username, post_office_password,license_no,provider_type, pbm_npi_code, TYpe_ID, Region, is_active,sender_license_no,created_dt,updated_dt " +
                "\nfrom provider" +
                "\nwhere license_no IN (select facilityid from @faciltiy) " +
                "\nGO" +
                "\n\n:connect  " + con.get_inline_connection("PBMLINK") +
                "\nUse [PBMLink.NET]" +
                "\ndeclare @faciltiy table(facilityid nvarchar(100))" +
                "\ninsert @faciltiy(facilityid) values " +
                split_array(facilityid) +
                "\nSelect 'PBMLink' as 'DB Name',[Name],[Username],[Password],[Active],[ActiveGroup],[AdminGroup],[DeletedGroup],[Provider],[District],[DistrictName],[License],[POUsername],[POPassword]" +
                "\nfrom [PBMLink.NET].[dbo].[V_User]" +
                "\nwhere [License] IN (select facilityid from @faciltiy) " +
                "\nGo";
            }
            else if (opt == "payer")
            {
                query = //":connect 10.156.62.42  -U fazeel -P Dell@123" +
                ":connect " + con.get_inline_connection("ECLAIMLINKPORTAL") +
                "\nUSE EclaimLinkPortal" +
                "\nGO" +
                "\ndeclare @faciltiy table(facilityid nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS)" +
                "\ninsert @faciltiy(facilityid) values " +
                split_array(facilityid) +
                "\nselect 'ECLAIMLINKPORTAL' as 'DB Name',Username,Password,facility_name,FacilityId,License " +
                "\nfrom [eclaimlinkportal].[dbo].[v_users] " +
                "\nwhere facilityid = (select id from facilities where license IN (select facilityid from @faciltiy)  )" +
                "\nGO" +

                "\n\n" +//":connect 10.5.3.42  -U fshaikh -P Dell@123" +
                ":connect " + con.get_inline_connection("PBMM") +
                "\nUSE PBMM" +
                "\nGO" +
                "\ndeclare @faciltiy table(facilityid nvarchar(100))" +
                "\ninsert @faciltiy(facilityid) values " +
                split_array(facilityid) +
                "\nselect 'PBMM' as 'DB Name',post_office_username, post_office_password,dhpo_username,dhpo_password,is_active,name,license_no,tpa_code,dubai_license_no,tpa_dubai_code,pbm_pcn_code" +
                "\nfrom PAYER " +
                "\nwhere license_no IN (select facilityid from @faciltiy)  OR dubai_license_no IN (select facilityid from @faciltiy)  OR tpa_dubai_code IN (select facilityid from @faciltiy) " +
                "\nGO" +

                "\n\n" +//":connect 10.162.176.24 -U fshaikh -P Dell@777" +
                ":connect " + con.get_inline_connection("DHPO") +
                "\n USE DHPO" +
                "\nGO" +
               "\ndeclare @faciltiy table(facilityid nvarchar(100))" +
                "\ninsert @faciltiy(facilityid) values " +
                split_array(facilityid) +
                "\nSelect 'DHPO' as 'DB Name',PayerCode,PayerName,UserName,Password,PayerTypeID,IsActive " +
                "\nfrom Payers" +
                "\nwhere PayerCode IN (select facilityid from @faciltiy) " +
                "\nGO" +

                "\n\n" +//":connect 10.156.62.42 -U fazeel -P Dell@123" +
                 ":connect " + con.get_inline_connection("ERX") +
                "\nUSE MemberRegister" +
                "\nGO" +
                "\ndeclare @faciltiy table(facilityid nvarchar(100))" +
                "\ninsert @faciltiy(facilityid) values " +
                split_array(facilityid) +
                "\nSelect 'MemberRegister' as 'DB Name',UserName,Password,PayerCode,PayerName,PayerTypeID,IsActive " +
                "\nfrom Payers " +
                "\nwhere PayerCode IN (select facilityid from @faciltiy) " +
                "\nGO" +

                "\n\n" +//":connect  10.5.3.42 -U fshaikh -P Dell@777" +
                ":connect " + con.get_inline_connection("OICProvider") +
                "\nUSE OIC.eAuth" +
                "\ndeclare @faciltiy table(facilityid nvarchar(100))" +
                "\ninsert @faciltiy(facilityid) values " +
                split_array(facilityid) +
                "\nSELECT 'OIC.eAuth', *" +
                //"\nSELECT 'OIC-PROVIDER-PORTAL', [Id],[LmuId],[License],[Name],[Type],[District],[NPI],[Registered],[RegisteredAtPBMSwitch],[PBMSwitchErrorMsg],[Active],[Sender],[POUsername],[POPassword],[Email],[Phone] " +
                "\nFROM [OIC.eAuth].[dbo].[Payer] " +
                "\nwhere License IN (select facilityid from @faciltiy) " +
                "\norder by License desc " +
                "\nGO" + 
                "\n\n:connect  " + con.get_inline_connection("PBMLINK") +
                "\nUse [PBMLink.NET]" +
                "\ndeclare @faciltiy table(facilityid nvarchar(100))" +
                "\ninsert @faciltiy(facilityid) values " +
                split_array(facilityid) +
                "\nSelect 'PBMLink' as 'DB Name',[Name],[Username],[Password],[Active],[ActiveGroup],[AdminGroup],[DeletedGroup],[Provider],[District],[DistrictName],[License],[POUsername],[POPassword]" +
                "\nfrom [PBMLink.NET].[dbo].[V_User]" +
                "\nwhere [License] IN (select facilityid from @faciltiy) " +
                "\nGo"
                ;


            }
            return query;
        }

        string split_array(string[] facilityid)
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