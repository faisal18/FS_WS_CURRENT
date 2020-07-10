using ClinicianAutomation.ExtraClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FS_WS_WSCTFW.Utilities.DBModification.ExtraClasses
{
    public static class EclaimParser_Account
    {
        public static string register_account(string[] providerID,string status)
        {
            try
            {
                Connections con = new Connections();
                string query = "";
                if (status.ToUpper() == "ACTIVATE")
                {
                    query = account_activate(providerID);
                    
                }
                else if (status.ToUpper() == "DEACTIVATE")
                {
                    query = account_deactivate(providerID);
                }

                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "Anyconnect", "_EclaimParser_Account_", "ECLAIMPARSER", 2);
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string account_deactivate(string[] providerID)
        {
            string query = "UPDATE top(1) [eclaimparser].[dbo].[FacilityUser] \n" +
            "SET \n" +
            "[IsApproved] =0 \n" +
            ",[comment] = 'FS _ UPdated User _ ' + cast(GETDATE() as varchar(100))     \n" +
            "where [IsApproved] = 1 \n" +
            "and [IsApproved] is not null \n" +
            "and [providerid] in ( \n" +
            "--Select * from FacilityUser where providerid in ( \n" +
            "Select providerid from Facility where LicenseID in (" + split_array(providerID) + ")) ";

            return query;
        }

        private static string account_activate(string[] providerID)
        {
            string query = "UPDATE top(1) [eclaimparser].[dbo].[FacilityUser] \n" +
            "SET \n" +
            "[IsApproved] =1 \n" +
            ",[comment] = 'FS _ UPdated User _ ' + cast(GETDATE() as varchar(100))     \n" +
            "where [IsApproved] = 0 \n" +
            "and [IsApproved] is not null \n" +
            "and [providerid] in ( \n" +
            "--Select * from FacilityUser where providerid in ( \n" +
            "Select providerid from Facility where LicenseID in (" + split_array(providerID) + ")) ";

            return query;
        }

        static string split_array(string[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach(string datum in data)
            {
                sb.Append("'" + datum + "',");
            }

            return Convert.ToString(sb).Remove(sb.Length - 1);
        }


    }
}