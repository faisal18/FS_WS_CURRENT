using System;

namespace ClinicianAutomation
{
    public partial class Clinician_update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        ExtraClasses.clin_logic clog = new ExtraClasses.clin_logic();
       
        protected void submit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_license.Text.ToString()) && !string.IsNullOrEmpty(txt_username.Text.ToString()) && !string.IsNullOrEmpty(txt_password.Text.ToString()))
            {
                string result = clog.clnician_logic(txt_license.Text.ToString(), txt_username.Text.ToString(), txt_password.Text.ToString());
                lbl_response.Text = result;
            }
            else
                lbl_response.Text = "Enter values in all fields !!";

        }

        protected void btn_clear_Click(object sender, EventArgs e)
        {
            txt_license.Text = null;
            txt_password.Text = null;
            txt_username.Text = null;
            lbl_response.Text = null;
        }
    }
}