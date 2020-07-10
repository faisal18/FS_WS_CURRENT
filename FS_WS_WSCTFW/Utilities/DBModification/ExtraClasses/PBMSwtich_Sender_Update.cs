using System;


namespace ClinicianAutomation.ExtraClasses
{
    public class PBMSwtich_Sender_Update
    {
        public string update(string provider_license, string sender_license)
        {
            try
            {
                string query = generate_query(provider_license, sender_license);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "HyperV", "PBMSwtich_Sender_Update_" +provider_license, "PBMM", 2);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string generate_query(string provider_license, string sender_license)
        {
            try
            {
                string query = "\nselect  * from PROVIDER where license_no = '" + provider_license + "'" +
                                "\nupdate top (1)" +
                                "\nPROVIDER" +
                                "\nset sender_license_no = '" + sender_license + "'" +
                                "\nwhere license_no = '" + provider_license + "'";
                return query;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}