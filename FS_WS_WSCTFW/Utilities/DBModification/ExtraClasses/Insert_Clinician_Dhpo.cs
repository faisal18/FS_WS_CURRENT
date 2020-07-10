using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FS_WS_WSCTFW.Utilities.DBModification.ExtraClasses
{
    public class Insert_Clinician_Dhpo
    {
        public string InsertClinician(string[] cliniciandata)
        {
            string query = GenerateQuery(cliniciandata);
            //FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_ClinicianDetail_" + cliniciandata[0], "DHPO", 2);
            return "Request processes successfully";
        }

        private static string GenerateQuery(string[] data)
        {
            string query = "INSERT INTO [DHPO].[dbo].[Clinicians]([ClinicianLicense],[ClinicianName],[Gender],[FacilityName],[Location],[LicenseStartDate],[LicenseEndDate],[Source],[IsActive],[UserName],[Password],[SpecialtyId],[SpecialtyGroup]) values (";
            for (int i = 0; i < data.Length; i++)
            {
                if (i != 2 && i != 8)
                {
                    query += "'" + data[i] + "',";
                }
                else
                {
                    query += data[i] + ",";
                }
            }

            query = query.TrimEnd(',');
            query += ")";

            return query;
        }
    }
}