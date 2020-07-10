using System;
using System.Configuration;


namespace ClinicianAutomation.ExtraClasses
{
    public class DHPO_login_update
    {

        public string script(string license, string user, string password)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["dhpo_path"] + "Dhpo_login_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            try
            {
                string query = generate_query(license, user, password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_DHPO_Login_Update_"+license, "DHPO", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "DHPO");
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
                return "An exception occured " + ex.Message;
            }
        }

        public string script(string license, string password)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["dhpo_path"] + "Dhpo_login_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(license, password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_DHPO_LOGIN_UPDATE_" + license, "DHPO", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "DHPO");
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
                return "An exception occured " + ex.Message;
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
                "\n\nSelect UserName, Password,* from Provider where LicenseID = @license " +
                "\n\nupdate TOP(1) Provider set" +
                "\nusername = @Username ,password = @Password " +
                "\nwhere LicenseID = @license " +
                "\n\nSelect username, password from Provider where LicenseID = @license";
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
               "\n\nSelect UserName, Password,* from Provider where LicenseID = @license " +
               "\n\nupdate TOP(1) Provider set" +
               "\npassword = @Password " +
               "\nwhere LicenseID = @license " +
               "\n\nSelect username, password from Provider where LicenseID = @license";
            return query;
        }


    }
}