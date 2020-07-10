using System;

namespace ClinicianAutomation
{
    public partial class get_credentials : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            //txtFullLog.ReadOnly = true;
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            ExtraClasses.get_credentials gc = new ExtraClasses.get_credentials();
            string result = "";
            string[] differences = { ",", "\t", "\n", "\r" };

            try
            {
                if (rd_provider.Checked == true)
                {
                    string input = txt_providerid.Text.ToString();
                    string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                    result = gc.get_creds(carry, "provider");
                }
                else if (rd_payer.Checked == true)
                {
                    string input = txt_payerid.Text.ToString();
                    string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                    result = gc.get_creds(carry, "payer");
                }
                else
                    result = "Please select an option";
                lbl_response.Text = result;
            }
            catch(Exception ex)
            { lbl_response.Text = ex.Message.ToString(); }
        }

        protected void rd_provider_CheckedChanged(object sender, EventArgs e)
        {
            txt_payerid.Enabled = false;
            txt_providerid.Enabled = true;
            rd_payer.Checked = false;
            txt_payerid.Text = "";
        }

        protected void rd_payer_CheckedChanged(object sender, EventArgs e)
        {
            txt_providerid.Enabled = false;
            txt_payerid.Enabled = true;
            rd_provider.Checked = false;
            txt_providerid.Text = "";
        }
    }
}