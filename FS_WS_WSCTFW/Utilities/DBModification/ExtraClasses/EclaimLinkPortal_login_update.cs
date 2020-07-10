using System;
using System.Configuration;


namespace ClinicianAutomation.ExtraClasses
{
    public class EclaimLinkPortal_login_update
    {

        public string script(string license, string user, string password)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["eclaimlinkportal_path"] + "ECLP_login_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(license, user, password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_EclaimPortal_Login_update_" + license, "ECLAIMLINKPORTAL", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "ECLAIMLINKPORTAL");
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
            string path = ConfigurationManager.AppSettings["eclaimlinkportal_path"] + "ECLP_login_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(license, password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_EclaimPortal_login_update_" + license, "ECLAIMLINKPORTAL", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "ECLAIMLINKPORTAL");
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
                return "An exception occured " + Environment.NewLine + "" + ex.Message;
            }
        }

        public string generate_query(string license, string user, string password)
        {
            string query = "USE EclaimLinkPortal" +
               "\nDeclare @License varchar(100)" +
               "\nDeclare @Username varchar(100)" +
               "\nDeclare @Password varchar(100)" +
               "\n\nset @License ='" + license + "'" +
               "\nset @Username ='" + user + "'" +
               "\nset @Password ='" + password + "'" +
               "\n\n Print'Updating EclaimLink Portal Login'" +
               "\n\nSelect username, password,* from v_users where license = @license " +
               "\n\nupdate TOP(1) v_users set" +
               "\nusername = @Username ,password = @Password " +
               "\nwhere license = @license " +
               "\n\nSelect username, password from v_users where license = @license";
            return query;
        }

        public string generate_query(string license, string password)
        {
            string query = "USE EclaimLinkPortal" +
                "\nDeclare @License varchar(100)" +
                "\nDeclare @Password varchar(100)" +
                "\n\nset @License ='" + license + "'" +
                "\nset @Password ='" + password + "'" +
                "\n\n Print'Updating EclaimLink Login'" +
                "\n\nSelect username, password,* from v_users where license = @license " +
                "\n\nupdate TOP(1) v_users set" +
                "\npassword = @Password " +
                "\nwhere license = @license " +
                "\n\nSelect username, password from v_users where license = @license";
            return query;
        }

    }
}