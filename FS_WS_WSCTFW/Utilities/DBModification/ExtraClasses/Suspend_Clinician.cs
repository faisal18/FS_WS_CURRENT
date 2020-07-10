using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FS_WS_WSCTFW.Utilities.DBModification.ExtraClasses
{
    public class Suspend_Clinician
    {
        public string Run(string license, string start_date, string end_date)
        {
            try
            {
                string query = Generate_Query(license, start_date, end_date);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_ERX_PO_Update_" + license, "DHPO", 2);
                return "Request Processed Successfully";
            }
            catch (Exception ex)
            {
                return "An exception occured !\n" + ex.Message;
            }
        }

        private string Generate_Query(string license, string start_date, string end_date)
        {
            try
            {
                string query = "use dhpo  \n" +
                                 " \n" +
                                 "declare @ClinicianLicense varchar (100) \n" +
                                 "declare @SuspensionStartDate date \n" +
                                 "declare @SuspensionEndDate date \n" +
                                 " \n" +
                                 "set @ClinicianLicense = '" + license + "' \n" +
                                 "set @SuspensionStartDate = '" + start_date + "' \n" +
                                 "set @SuspensionEndDate = '" + end_date + "' \n" +
                                 " \n" +
                                 "select top 100 * from clinicians \n" +
                                 "where clinicianLicense = @ClinicianLicense \n" +
                                 " \n" +
                                 " \n" +
                                 "update top (1) clinicians \n" +
                                 "set  \n" +
                                 "SuspensionStartDate = @SuspensionStartDate \n" +
                                 ", \n" +
                                 "SuspensionEndDate = @SuspensionEndDate \n" +
                                 "where clinicianLicense = @ClinicianLicense \n" +
                                 "and ClinicianId = (select clinicianid from Clinicians where clinicianLicense = @ClinicianLicense) \n" +
                                 " \n" +
                                 " \n" +
                                 "select top 100 * from clinicians \n" +
                                 "where clinicianLicense = @ClinicianLicense ";
                return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}