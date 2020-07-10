using System;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class EclaimLink_login_update
    {

        public string script(string license, string user, string password)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["eclaimlink_path"] + "ECL_login_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(license, user, password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_Eclaimlink_Login_update_" + license, "ECLAIMLINK", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "ECLAIMLINK");
                return "Request Processed Successfully";
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
            string path = ConfigurationManager.AppSettings["eclaimlink_path"] + "ECL_login_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(license, password);
                //cs.script_create(query, path);
                //cb.batch_create(path, "ECLAIMLINK");
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_EclaimLInk_Login_UPdate_" + license, "ECLAIMLINK", 2);
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
                return "An exception occured " + Environment.NewLine + "" + ex.Message;
            }
        }

        public string generate_query(string license, string user, string password)
        {
            string query = "USE Eclaimlink" +
                "\nDeclare @License varchar(100)" +
                "\nDeclare @Username varchar(100)" +
                "\nDeclare @Password varchar(100)" +
                "\n\nset @License ='" + license + "'" +
                "\nset @Username ='" + user + "'" +
                "\nset @Password ='" + password + "'" +
                "\n\n Print'Updating EclaimLink Login'" +
                "\n\nSelect username, password,* from v_provider_user where providerlicense = @license " +
                "\n\nupdate TOP(1) v_provider_user set" +
                "\nusername = @Username ,password = @Password " +
                "\nwhere providerlicense = @license " +
                "\n\nSelect username, password from v_provider_user where providerlicense = @license";
            return query;
        }

        public string generate_query(string license, string password)
        {
            string query = "USE Eclaimlink" +
               "\nDeclare @License varchar(100)" +
               "\nDeclare @Password varchar(100)" +
               "\n\nset @License ='" + license + "'" +
               "\nset @Password ='" + password + "'" +
               "\n\n Print'Updating EclaimLink Login'" +
               "\n\nSelect username, password,* from v_provider_user where providerlicense = @license " +
               "\n\nupdate TOP(1) v_provider_user set" +
               "\npassword = @Password " +
               "\nwhere providerlicense = @license " +
               "\n\nSelect username, password from v_provider_user where providerlicense = @license";
            return query;
        }


    }
}