using System;

namespace FS_WS_WSCTFW.Utilities.DBModification
{
    public partial class Insert_Clinician_Dhpo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
          
            try
            {
                string enviorment = rd_enviorment.SelectedValue;

                string query = string.Empty;
                if (enviorment == "DHPO")
                {
                    query = "INSERT INTO [DHPO].[dbo].[Clinicians]([ClinicianLicense],[ClinicianName],[Gender],[FacilityName],[Location],[LicenseStartDate],[LicenseEndDate],[Source],[IsActive],[UserName],[Password],[SpecialtyId],[SpecialtyGroup]) values (";
                }
                else if(enviorment == "QA")
                {
                    query = "INSERT INTO [DHPO_TEST].[dbo].[Clinicians]([ClinicianLicense],[ClinicianName],[Gender],[FacilityName],[Location],[LicenseStartDate],[LicenseEndDate],[Source],[IsActive],[UserName],[Password],[SpecialtyId],[SpecialtyGroup]) values (";
                }
                string[] data = {txt_ClinicianLicense.Text, txt_ClinicianName.Text, ddl_gender.SelectedValue, txt_FacilityName.Text, txt_Location.Text,
                    txt_LicenseStartDate.Text,txt_LicenseEndDate.Text,txt_Source.Text,ddl_isActive.SelectedValue,txt_UserName.Text,txt_Password.Text,
                    txt_SpecialityId.Text,txt_SpecialityGroup.Text
                };

                string nulled_query = Nullchecker(data);
                query += nulled_query;

              
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_InsertClinician_" + txt_ClinicianLicense.Text, enviorment, 2);
                //FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_InsertClinician_" + txt_ClinicianLicense.Text, "DHPO", 2);
                lbl_result.Text = "Request Processed Successfully";

            }
            catch (Exception ex)
            {
                lbl_result.Text = "Exception Occured !\n" + ex;
            }
        }

        private string Nullchecker(string[] data)
        {
            string query = string.Empty;
            try
            {
                foreach (string dataum in data)
                {
                    if (dataum.Length>0)
                    {
                        query += "'" + dataum + "',";
                    }
                    else
                    {
                        query += "NULL,";
                    }
                }
                

                query = query.Remove(query.Length - 1, 1);
                query += ")";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return query;
        }
    }
}