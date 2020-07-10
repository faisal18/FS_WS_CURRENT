using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FS_WS_WSCTFW.Helpers;

namespace ClinicianAutomation.ExtraClasses
{
    public class Monitoring_UploadData
    {

        private static string Automation_ConnectionString = "Data Source=" + Connections.run_singlevalue("Automation", "server") + ";Initial Catalog=" + Connections.run_singlevalue("Automation", "database") + ";User ID=" + Connections.run_singlevalue("Automation", "username") + ";Password=" + Connections.run_singlevalue("Automation", "password") + ";Connection Timeout=30;";
        private static string PBMLink_ConnectionString = "Data Source=" + Connections.run_singlevalue("PBMLINK", "server") + ";Initial Catalog=" + Connections.run_singlevalue("PBMLINK", "database") + ";User ID=" + Connections.run_singlevalue("PBMLINK", "username") + ";Password=" + Connections.run_singlevalue("PBMLINK", "password") + ";Connection Timeout=30;";
        private static string ERX_ConnectionString = "Data Source=" + Connections.run_singlevalue("ERX", "server") + ";Initial Catalog=" + Connections.run_singlevalue("ERX", "database") + ";User ID=" + Connections.run_singlevalue("ERX", "username") + ";Password=" + Connections.run_singlevalue("ERX", "password") + ";Connection Timeout=30;";
        private static string DHPO_ConnectionString = "Data Source=" + Connections.run_singlevalue("DHPO", "server") + ";Initial Catalog=" + Connections.run_singlevalue("DHPO", "database") + ";User ID=" + Connections.run_singlevalue("DHPO", "username") + ";Password=" + Connections.run_singlevalue("DHPO", "password");


        public static bool InsertFrom_PBM()
        {
            bool result = false;
            string query = System.IO.File.ReadAllText(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/GraphQueries/GET_PBMLink_Short.sql"));
            Logger.Info("Insert From PBM");
            try
            {
                DataTable dt = Execute_Query(PBMLink_ConnectionString, query);
                if (dt.Rows.Count > 0)
                {
                    int Total_Pending = (int)dt.Rows[0]["PBMLink_Pending_Count"];
                    int Total_Processed = (int)dt.Rows[0]["PBMLink_Processed_Count"];
                    int Total_Payer = (int)dt.Rows[0]["PBMLink_PBM_Payer_Processed_Count"];

                    string pbm_query_insert = "USE [FS_WS_WSCTFW] Insert into [Monitoring_TransactionCount] ([ApplicationName],[CheckingTime],[Count])" +
                        " values ('PBMLink_Pending_Count',GETDATE()," + Total_Pending + ")," +
                        " ('PBMLink_Processed_Count',GETDATE()," + Total_Processed + ")," +
                        " ('PBMLink_PBM_Payer_Processed_Count',GETDATE()," + Total_Payer + ")," +
                        " ('PBMLink_Total_Count',GETDATE()," + (Total_Pending + Total_Processed) + ")";

                    result = Insert_Query(Automation_ConnectionString, pbm_query_insert);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return result;
        }
        public static bool InsertFrom_ERX()
        {
            bool result = false;
            string query = System.IO.File.ReadAllText(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/GraphQueries/GET_ERX_Short.sql"));
            Logger.Info("Insert From ERX");
            try
            {
                DataTable dt = Execute_Query(ERX_ConnectionString, query);
                if (dt.Rows.Count > 0)
                {
                    int Total_Pending = (int)dt.Rows[0]["ERX_Pending_Count"];
                    int Total_Processed = (int)dt.Rows[0]["ERX_Processed_Count"];
                    int Total_Payer = (int)dt.Rows[0]["ERX_Payer_Processed_Count"];

                    string erx_query_insert = "USE [FS_WS_WSCTFW] Insert into [Monitoring_TransactionCount] ([ApplicationName],[CheckingTime],[Count])" +
                        " values ('ERX_Pending_Count',GETDATE()," + Total_Pending + ")," +
                        " ('ERX_Processed_Count',GETDATE()," + Total_Processed + ")," +
                        " ('ERX_Payer_Processed_Count',GETDATE()," + Total_Payer + ")," +
                        " ('ERX_Total_Count',GETDATE()," + (Total_Pending + Total_Processed) + ")";

                    result = Insert_Query(Automation_ConnectionString, erx_query_insert);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return result;
        }
        public static bool InsertFrom_DHPO()
        {
            bool result = false;
            string query = System.IO.File.ReadAllText(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/GraphQueries/GET_DHPO_Faisal.sql"));
            Logger.Info("Insert From DHPO");
            try
            {
                DataTable dt = Execute_Query(DHPO_ConnectionString, query);
                if (dt.Rows.Count > 0)
                {
                    int[] data = new int[6];
                    data[0] = (int)dt.Rows[0]["DHPO-Total_PR"];
                    data[1] = (int)dt.Rows[0]["DHPO-PR_NotDownloaded"];
                    data[2] = (int)dt.Rows[0]["DHPO-Total_PA"];
                    data[3] = (int)dt.Rows[0]["DHPO-PA_NotDownloaded"];
                    data[4] = (int)dt.Rows[0]["DHPO-Total_CS"];
                    data[5] = (int)dt.Rows[0]["DHPO-CS_NotDownloaded"];

                    string dhpo_insert_query = "USE [FS_WS_WSCTFW] Insert into [Monitoring_TransactionCount] ([ApplicationName],[CheckingTime],[Count])" +
                        " values ('DHPO-Total_PR',GETDATE()," + data[0] + " )" +
                        " , ('DHPO-PR_NotDownloaded',GETDATE()," + data[1] + "  )" +
                        " , ('DHPO-PR_Downloaded',GETDATE()," + (data[0] - data[1]) + "  )" +
                        " , ('DHPO-Total_PA',GETDATE()," + data[2] + "  )" +
                        " , ('DHPO-PA_NotDownloaded',GETDATE()," + data[3] + "  )" +
                        " , ('DHPO-PA_Downloaded',GETDATE()," + (data[2] - data[3]) + "  )" +
                        " , ('DHPO-Total_CS',GETDATE()," + data[4] + "  )" +
                        " , ('DHPO-CS_NotDownloaded',GETDATE()," + data[5] + "  )" +
                        " , ('DHPO-CS_Downloaded',GETDATE()," + (data[4] - data[5]) + "  )";

                    result = Insert_Query(Automation_ConnectionString, dhpo_insert_query);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return result;
        }

        public static bool InsertFrom_PBM_Pending()
        {
            bool result = false;
            string query = System.IO.File.ReadAllText(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/GraphQueries/GET_PBMLink_Pending.sql"));
            Logger.Info("Insert From PBM Pending");
            try
            {
                DataTable dt = Execute_Query(PBMLink_ConnectionString, query);
                if (dt.Rows.Count > 0)
                {
                    int[] data = new int[2];
                    data[0] = (int)dt.Rows[0]["PBMLink_Payers_Pending_Count"];
                    data[1] = (int)dt.Rows[0]["PBMLink_NonPayers_Pending_Count"];

                    string dhpo_insert_query = "USE [FS_WS_WSCTFW] Insert into [Monitoring_TransactionCount] ([ApplicationName],[CheckingTime],[Count])" +
                        " values ('PBMLink_Payers_Pending_Count',GETDATE()," + data[0] + " )" +
                        " , ('PBMLink_NonPayers_Pending_Count',GETDATE()," + data[1] + "  )";

                    result = Insert_Query(Automation_ConnectionString, dhpo_insert_query);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return result;
        }
        public static bool InsertFrom_ERX_Pending()
        {
            bool result = false;
            string query = System.IO.File.ReadAllText(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/GraphQueries/GET_ERX_PENDING.sql"));
            Logger.Info("Insert From ERX Pending");
            try
            {
                DataTable dt = Execute_Query(ERX_ConnectionString, query);
                if (dt.Rows.Count > 0)
                {
                    int[] data = new int[2];
                    data[0] = (int)dt.Rows[0]["ERX_Payers_Pending_Count"];
                    data[1] = (int)dt.Rows[0]["ERX_NonPayers_Pending_Count"];
                    string dhpo_insert_query = "USE [FS_WS_WSCTFW] Insert into [Monitoring_TransactionCount] ([ApplicationName],[CheckingTime],[Count])" +
                        " values ('ERX_Payers_Pending_Count',GETDATE()," + data[0] + " )" +
                        " , ('ERX_NonPayers_Pending_Count',GETDATE()," + data[1] + "  )";

                    result = Insert_Query(Automation_ConnectionString, dhpo_insert_query);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return result;
        }

        private static DataTable Execute_Query(string connection, string query)
        {
            DataTable dt = new DataTable();
            try
            {
                Logger.Info("SELECT query in progress");
                Logger.Info("Connection: " + connection);

                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.SelectCommand.CommandTimeout = 1800;
                            da.Fill(dt);
                            Logger.Info("Query executed successfully");
                        }
                        con.Close();
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }
        private static bool Insert_Query(string connection, string query)
        {
            bool result = false;
            try
            {
                Logger.Info("Insert query in progress");
                Logger.Info("Connection: " + connection);
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
                Logger.Error(ex.Message);
                result = false;
            }
            return result;
        }


    }
}