using FS_WS_WSCTFW.Helpers;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ClinicianAutomation.ExtraClasses
{
    public class erx_po_update
    {
        public string script(string license, string user, string password)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["erx_path"] + "ERX_PO_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(license,user,password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_ERX_PO_Update_" + license, "ERX", 2);
               // cs.script_create(query, path);
               //cb.batch_create(path,"ERX");
                return "Request Processed Successfully";
            }
            catch(Exception ex)
            {
                return "An exception occured " + ex.Message;
            }
        }

        public string script(string license,string password)
        {
            create_script cs = new create_script();
            create_batch cb = new create_batch();
            string path = ConfigurationManager.AppSettings["erx_path"] + "ERX_PO_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(license,password);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "_ERX_PO_Update_" + "eHDF", license, "ERX", 2);
                //cs.script_create(query, path);
                //cb.batch_create(path,"ERX");
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
                return "An exception occured " + ex.Message;
            }
        }

        public string script(string license)
        {
            string path = ConfigurationManager.AppSettings["erx_path"] + "ERX_PO_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string query = generate_query(license);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "_ERX_PO_Update_" + "eHDF", license, "ERX", 2);

                return "Request Processed Successfully";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string generate_query(string license, string user, string password)
        {
            string query = " SET QUOTED_IDENTIFIER ON " + Environment.NewLine + "  USE erx" +
                 "\nDeclare @License varchar(100)" +
                 "\nDeclare @Username varchar(100)" +
                 "\nDeclare @Password varchar(100)" +
                 "\n\nset @License ='" + license + "'" +
                 "\nset @Username ='" + user + "'" +
                 "\nset @Password ='" + password + "'" +
                 "\n\n Print'Updating ERX PO'" +
                 "\n\nSelect Pousername, popassword,* from provider where license = @license " +
                 "\n\nupdate TOP(1) Provider set" +
                 "\npousername = @Username ,popassword = @Password " +
                 "\nwhere license = @license " +
                 "\n\nSelect Pousername, popassword from provider where license = @license";
            return query;
        }

        public string generate_query(string license, string password)
        {
            string query = " SET QUOTED_IDENTIFIER ON " + Environment.NewLine + "  USE erx" +
                "\nDeclare @License varchar(100)" +
                "\nDeclare @Username varchar(100)" +
                "\nDeclare @Password varchar(100)" +
                "\n\nset @License ='" + license + "'" +
                "\nset @Password ='" + password + "'" +
                "\n\n Print'Updating ERX PO'" +
                "\n\nSelect Pousername, popassword,* from provider where license = @license " +
                "\n\nupdate TOP(1) Provider set" +
                "\npopassword = @Password " +
                "\nwhere license = @license " +
                "\n\nSelect Pousername, popassword from provider where license = @license";
            return query;
        }

        public string generate_query(string license)
        {
            string username = string.Empty;
            string password = string.Empty;

            DataTable dt = GetDatafromDHPO(license);
            if(dt != null)
            {
                username = dt.Rows[0]["UserName"].ToString();
                password = dt.Rows[0]["Password"].ToString();
            }


            string query = " SET QUOTED_IDENTIFIER ON " + Environment.NewLine + "  USE erx" +
                 "\nDeclare @License varchar(100)" +
                 "\nDeclare @Username varchar(100)" +
                 "\nDeclare @Password varchar(100)" +
                 "\n\nset @License ='" + license + "'" +
                 "\nset @Username ='" + username + "'" +
                 "\nset @Password ='" + password + "'" +
                 "\n\n Print'Updating ERX PO'" +
                 "\n\nSelect Pousername, popassword,* from provider where license = @license " +
                 "\n\nupdate TOP(1) Provider set" +
                 "\npousername = @Username ,popassword = @Password " +
                 "\nwhere license = @license " +
                 "\n\nSelect Pousername, popassword from provider where license = @license";

            return query;
        }

        private DataTable GetDatafromDHPO(string license)
        {
            try
            {
                string connection = "Data Source=" + Connections.run_singlevalue("DHPO", "server") + ";Initial Catalog=" + Connections.run_singlevalue("DHPO", "database") + ";User ID=" + Connections.run_singlevalue("DHPO", "username") + ";Password=" + Connections.run_singlevalue("DHPO", "password");
                string query = "USE DHPO\nSELECT [LicenseID],[UserName],[Password] FROM [DHPO].[dbo].[Provider] WHERE [LicenseID] = '" + license + "' ";

                DataTable dt = Execute_Query(connection, query);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private static DataTable Execute_Query(string connection, string query)
        {
            DataTable dt = new DataTable();
            Logger.Info("Select Query Execution in progress..");
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.SelectCommand.CommandTimeout = 1800;
                            da.Fill(dt);
                        }
                        con.Close();
                    }
                }
                Logger.Info("Query executed successfully");
                return dt;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }

    }
}