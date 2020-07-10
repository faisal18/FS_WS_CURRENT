using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FS_WS_WSCTFW.Utilities.DBModification.ExtraClasses
{
    public class Get_Clinician_Details
    {
        public string Run_Process(string[] data)
        {
            try
            {
                string query = Generate_Query(data);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_ClinicianDetail_" + data[0], "DHPO", 2);
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
                return "Exception Occured !\n" + ex;
            }

        }

        private string Generate_Query(string[] drugs)
        {

            ClinicianAutomation.ExtraClasses.Connections con = new ClinicianAutomation.ExtraClasses.Connections();
            string query = "USE [DHPO] \n" +
                            "declare @clinician table(data varchar(200)) \n" +
                            "Insert  @clinician (data) values \n" +
                            split_array(drugs) +
                            " \n " +
                            "Select * from [DHPO].[dbo].[Clinicians] where [ClinicianLicense] in (select DATA from @clinician )  \n " +
                            "\n GO \n";

            return query;

        }

        static string split_array(string[] data)
        {
            string concat = null;
            foreach (string s in data)
            {
                concat += "('" + s + "'),";
            }
            return concat.Remove(concat.Length - 1);
        }

    }
}