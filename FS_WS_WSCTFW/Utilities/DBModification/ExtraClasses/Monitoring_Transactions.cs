using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using FS_WS_WSCTFW.Helpers;

namespace ClinicianAutomation.ExtraClasses
{
    public class Monitoring_Transactions
    {
        private static string Automation_ConnectionString = "Data Source=" + Connections.run_singlevalue("Automation", "server") + ";Initial Catalog=" + Connections.run_singlevalue("Automation", "database") + ";User ID=" + Connections.run_singlevalue("Automation", "username") + ";Password=" + Connections.run_singlevalue("Automation", "password");

        public static DataTable GetTransactionsCount()
        {
            try
            {
                Logger.Info("Get Transactions Count");
                string query = System.IO.File.ReadAllText(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/GraphQueries/Automation_GET_Tran_Count.sql"));
                return Execute_Query(Automation_ConnectionString, query);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
        public static DataTable GetAppStatus()
        {
            try
            {
                Logger.Info("Get Application Statuses");
                string query = System.IO.File.ReadAllText(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/GraphQueries/Automation_GET_App_Statuses.sql"));
                return Execute_Query(Automation_ConnectionString, query);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
        public static DataTable GetPBMERXCount()
        {
            DataTable dt = new DataTable();
            try
            {
                Logger.Info("Get PBM ERX");
                string query = System.IO.File.ReadAllText(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/GraphQueries/Automation_GET_PBMERX.sql"));
                return Execute_Query(Automation_ConnectionString, query);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
        public static DataTable GetPBMERX_Pending_Count()
        {
            DataTable dt = new DataTable();
            try
            {
                Logger.Info("Get PBM ERX");
                string query = System.IO.File.ReadAllText(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/GraphQueries/Automation_GET_PBMERX_Pending.sql"));
                return Execute_Query(Automation_ConnectionString, query);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
        public static DataTable GetDHPOCount()
        {
            DataTable dt = new DataTable();
            try
            {
                Logger.Info("Get DHPO");
                string query = System.IO.File.ReadAllText(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/GraphQueries/Automation_GET_DHPO.sql"));
                return Execute_Query(Automation_ConnectionString, query);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }


        private static DataTable Execute_Query(string connection, string query)
        {
            DataTable dt = new DataTable();
            Logger.Info("Select Query Execution in progress..");
            Logger.Info("Connection: " + connection);
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
        private static bool Insert_Query(string connection, string query)
        {
            bool result = false;
            try
            {
                Logger.Info("INSERT Query in progress");
                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        con.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            result = true;
                            Logger.Info("Query executed successfully");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result = false;
            }
            return result;
        }

    }
}