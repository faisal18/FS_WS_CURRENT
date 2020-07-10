using System;
using System.Configuration;


namespace ClinicianAutomation.ExtraClasses
{
    public class oic_process_transaction
    {
        public string process_transaction(string payerid, string auth_date, string tat)
        {
            //create_script cs = new create_script();
            //create_batch cb = new create_batch();
            //string path = ConfigurationManager.AppSettings["oic_process"] + "Oic_process_" + payerid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            try
            {
                string query = generate_query(payerid, auth_date, tat);
                //cs.script_create(query, path);
                //cb.batch_create(path, "OICPayer");
                //return "File created successfully in directory "+Environment.NewLine+"" + path + ".sql".ToString();

                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_OIC_PROCESS_TRNS_" + payerid + "_" + tat, "OICPayer", 2);

                
                return "File created successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string generate_query(string payerid, string auth_date, string tat)
        {
            double time = int.Parse(tat);
            time = time * 60000;
            string query = "USE eAuth" +
                "\n declare @IDPayer nvarchar(4000)" +
                "\n declare @Authorizattion_Date nvarchar(4000)" +
                "\n declare @TAT nvarchar(4000)" +
                "\n set @IDPayer ='" + payerid.ToString() + "'" +
                "\n set @Authorizattion_Date ='" + auth_date.ToString() + "'" +
                "\n set @TAT ='" + time + "'" +
                "\n PRINT 'Updating the transaction as Processed'" +
                "\n\n update TOP(1) [eAuth].[dbo].[AuthorizationTransaction]" +
                "\n set authorizattion_date = @Authorizattion_Date, tat = @TAT , is_authorized = 1 " +
                "\n where id_payer = @IDPayer and is_authorized = 0 ";
            return query;
        }

        public string update_tat(string idpayer,string auth_date,string tat)
        {
            try
            {
                string query = update_tat_query(idpayer, auth_date, tat);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_OIC_PROCESS_TRNS_" + idpayer + "_" + tat, "OICPayer", 2);

                //FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", idpayer, "PBMM", 3);
                return "File created successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        public string update_tat_query(string idpayer,string auth_date,string tat)
        {
            try
            {
                double time = int.Parse(tat);
                time = time * 60000;
                string query = "USE eAuth" +
                    "\n declare @IDPayer nvarchar(4000)" +
                    "\n declare @Authorizattion_Date nvarchar(4000)" +
                    "\n declare @TAT nvarchar(4000)" +
                    "\n set @IDPayer ='" + idpayer.ToString() + "'" +
                    "\n set @Authorizattion_Date ='" + auth_date.ToString() + "'" +
                    "\n set @TAT ='" + time + "'" +
                    "\n PRINT 'Updating the transaction as Processed'" +
                    "\n\n update TOP(1) [eAuth].[dbo].[AuthorizationTransaction]" +
                    "\n set authorizattion_date = @Authorizattion_Date, tat = @TAT " +
                    "\n where id_payer = @IDPayer and is_authorized = 1 ";
                return query;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }
    }
}