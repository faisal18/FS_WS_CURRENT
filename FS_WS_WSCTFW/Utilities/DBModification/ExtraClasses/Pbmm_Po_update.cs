using System;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class Pbmm_Po_update
    {
        public string script(string license, string user, string password)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["pbmm_path"] + "PBMM_login_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(license, user, password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "_PBMM_PO_UPDATE_" + license, "PBMM", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "PBMM");
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
            string path = ConfigurationManager.AppSettings["pbmm_path"] + "PBMM_login_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(license, password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "_PBM_PO_UPDATE_" + license, "PBMM", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "PBMM");
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
                return "An exception occured " + Environment.NewLine + "" + ex.Message;
            }
        }

        public string generate_query(string license, string user, string password)
        {
            string query = "USE PBMM" +
                "\nDeclare @License varchar(100)" +
                "\nDeclare @Username varchar(100)" +
                "\nDeclare @Password varchar(100)" +
                "\n\nset @License ='" + license + "'" +
                "\nset @Username ='" + user + "'" +
                "\nset @Password ='" + password + "'" +
                "\n\n Print'Updating PBMM Login'" +
                "\n\nSelect post_office_username, post_office_password,* from provider where license_no = @license " +
                "\n\nupdate TOP(1) Provider set" +
                "\npost_office_username = @Username ,post_office_password = @Password " +
                "\nwhere license_no = @license " +
                "\n\nSelect post_office_username, post_office_password from provider where license_no = @license";
            return query;
        }

        public string generate_query(string license, string password)
        {
            string query = "USE PBMM" +
                "\nDeclare @License varchar(100)" +
                "\nDeclare @Password varchar(100)" +
                "\n\nset @License ='" + license + "'" +
                "\nset @Password ='" + password + "'" +
                "\n\n Print'Updating PBMM Login'" +
                "\n\nSelect post_office_username, post_office_password ,* from provider where license_no = @license " +
                "\n\nupdate TOP(1) Provider set" +
                "\npost_office_password = @Password " +
                "\nwhere license_no = @license " +
                "\n\nSelect post_office_username, post_office_password  from provider where license_no = @license";
            return query;
        }

    }
}