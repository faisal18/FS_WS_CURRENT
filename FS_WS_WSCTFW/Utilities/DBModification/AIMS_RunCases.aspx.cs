using System;
using System.Configuration;

namespace ClinicianAutomation.Utilities_UI
{
    public partial class AIMS_RunCases : System.Web.UI.Page
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
                if (txt_filedirectory.Text != "" || txt_emailid.Text != "")
                {
                    string file_path = txt_filedirectory.Text;
                    string email_id = txt_emailid.Text;
                    ExtraClasses.AIMS_RunCases cases = new ExtraClasses.AIMS_RunCases();
                    txt_filedirectory.Text = null;
                    lbl_status.Text = cases.run_case(file_path,email_id);
                }
                else
                {
                    lbl_status.Text = "Please enter data in both boxes";
                }
            }
            catch(Exception ex)
            {
                lbl_status.Text = ex.Message;
            }
        }
    }
}