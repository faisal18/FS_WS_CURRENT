using System;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class erx_payer_login_update
    {
        public string script(string license, string user, string password)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["erx_path"] + "ERX_Payer_login_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(license, user, password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF","_ERX_Payer_Login_Update_"+ license, "ERX", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path, "DHPO");
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
            string path = ConfigurationManager.AppSettings["erx_path"] + "ERX_Payer_login_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(license, password);
                cs.script_create(query, path);
                cb.batch_create(path, "ERX");
                return "File created successfully in directory " + Environment.NewLine + "" + path + ".sql".ToString();
            }
            catch (Exception ex)
            {
                return "An exception occured " + Environment.NewLine + "" + ex.Message;
            }
        }

        public string generate_query(string license, string user, string password)
        {
            string query = "USE erx" +
                "\nDeclare @License varchar(100)" +
                "\nDeclare @Username varchar(100)" +
                "\nDeclare @Password varchar(100)" +
                "\n\nset @License ='" + license + "'" +
                "\nset @Username ='" + user + "'" +
                "\nset @Password ='" + password + "'" +
                "\n\n Print'Updating ERX Login'" +
                "\n\nSelect username, password,* from Payers where PayerCode = @license " +
                "\n\nupdate TOP(1) Payers set" +
                "\nusername = @Username ,password = @Password " +
                "\nwhere PayerCode = @license " +
                "\n\nSelect username, password from Payers where PayerCode = @license";
            return query;
        }

        public string generate_query(string license, string password)
        {
            string query = "USE erx" +
                "\nDeclare @License varchar(100)" +
                "\nDeclare @Password varchar(100)" +
                "\n\nset @License ='" + license + "'" +
                "\nset @Password ='" + password + "'" +
                "\n\n Print'Updating ERX Login'" +
                "\n\nSelect username, password,* from Payers where PayerCode = @license " +
                "\n\nupdate TOP(1) Payers set" +
                "\npassword = @Password " +
                "\nwhere PayerCode = @license " +
                "\n\nSelect username, password from Payers where PayerCode = @license";
            return query;
        }
    }
}