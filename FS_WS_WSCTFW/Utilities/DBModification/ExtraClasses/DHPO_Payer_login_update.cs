using System;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class DHPO_Payer_login_update
    {

        public string script(string license, string user, string password)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["dhpo_path"] + "Dhpo_Payer_login_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            try
            {
                string query = generate_query(license, user, password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_DHPO_Payer_Login_Update_" + license, "DHPO", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "DHPO");
                string query_MemberRegister = generate_query_MemberRegister(license, user, password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query_MemberRegister, "eHDF", "_MR_Payer_Login_Update_" + license, "MemberRegister", 2);



                return "Request Processed Successfully for DHPO and Member Register Databases" ;
            }
            catch (Exception ex)
            {
                return "An exception occured " + Environment.NewLine + "" + ex.Message;
            }
        }

        public string script(string license, string password)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["dhpo_path"] + "Dhpo_Payer_login_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(license, password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_DHPO_Payer_Login_Update_" + license, "DHPO", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "DHPO");

                string query_MemberRegister = generate_query_memberregister(license,  password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query_MemberRegister, "eHDF", "_MR_Payer_Login_Update_" + license, "MemberRegister", 2);

                return "Request Processed Successfully for DHPO and Member Register Databases";
            }
            catch (Exception ex)
            {
                return "An exception occured " + Environment.NewLine + "" + ex.Message;
            }
        }

        public string generate_query(string license, string user, string password)
        {
            string query = "USE DHPO" +
                "\nDeclare @License varchar(100)" +
                "\nDeclare @Username varchar(100)" +
                "\nDeclare @Password varchar(100)" +
                "\n\nset @License ='" + license + "'" +
                "\nset @Username ='" + user + "'" +
                "\nset @Password ='" + password + "'" +
                "\n\n Print'Updating DHPO Login'" +
                "\n\nSelect UserName, Password,* from Payers where PayerCode = @license " +
                "\n\nupdate TOP(1) Payers set" +
                "\nusername = @Username ,password = @Password " +
                "\nwhere PayerCode = @license " +
                "\n\nSelect username, password from Payers where PayerCode = @license";
            return query;
        }

        public string generate_query_MemberRegister(string license, string user, string password)
        {
            string query = "USE memberregister" +
                "\nDeclare @License varchar(100)" +
                "\nDeclare @Username varchar(100)" +
                "\nDeclare @Password varchar(100)" +
                "\n\nset @License ='" + license + "'" +
                "\nset @Username ='" + user + "'" +
                "\nset @Password ='" + password + "'" +
                "\n\n Print'Updating memberregister Login'" +
                "\n\nSelect UserName, Password,* from Payers where PayerCode = @license " +
                "\n\nupdate TOP(1) Payers set" +
                "\nusername = @Username ,password = @Password " +
                "\nwhere PayerCode = @license " +
                "\n\nSelect username, password from Payers where PayerCode = @license";
            return query;
        }

        public string generate_query(string license, string password)
        {
            string query = "USE DHPO" +
               "\nDeclare @License varchar(100)" +
               "\nDeclare @Password varchar(100)" +
               "\n\nset @License ='" + license + "'" +
               "\nset @Password ='" + password + "'" +
               "\n\n Print'Updating DHPO Login'" +
               "\n\nSelect UserName, Password,* from Payers where PayerCode = @license " +
               "\n\nupdate TOP(1) Payers set" +
               "\npassword = @Password " +
               "\nwhere PayerCode = @license " +
               "\n\nSelect username, password from Payers where PayerCode = @license";
            return query;
        }

        public string generate_query_memberregister(string license, string password)
        {
            string query = "USE memberregister" +
               "\nDeclare @License varchar(100)" +
               "\nDeclare @Password varchar(100)" +
               "\n\nset @License ='" + license + "'" +
               "\nset @Password ='" + password + "'" +
               "\n\n Print'Updating memberregister Login'" +
               "\n\nSelect UserName, Password,* from Payers where PayerCode = @license " +
               "\n\nupdate TOP(1) Payers set" +
               "\npassword = @Password " +
               "\nwhere PayerCode = @license " +
               "\n\nSelect username, password from Payers where PayerCode = @license";
            return query;
        }
    }
}