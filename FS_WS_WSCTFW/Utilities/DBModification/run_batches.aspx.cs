using System;

namespace ClinicianAutomation
{
    public partial class run_batches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        ExtraClasses.automation_workfow cb = new ExtraClasses.automation_workfow();

        protected void btn_pbmlink_Click(object sender, EventArgs e)
        {
            lbl_response.Text = cb.run_batch("PBMPriorRequet");

        }

        protected void btn_eclaim_Click(object sender, EventArgs e)
        {
            lbl_response.Text = cb.run_batch("ClaimsGenerate");
        }

        protected void btn_erxPharmacy_Click(object sender, EventArgs e)
        {
            lbl_response.Text = cb.run_batch("ERXPharmacyPriorRequest");
        }

        protected void btn_erxclinician_Click(object sender, EventArgs e)
        {
            lbl_response.Text = cb.run_batch("ERXClinicianPriorRequest");
        }

        protected void btn_erxAuthourization_Click(object sender, EventArgs e)
        {
            lbl_response.Text = cb.run_batch("EAuthorizationPriorRequest");
        }

        protected void btn_oicProvider_Click(object sender, EventArgs e)
        {
            lbl_response.Text = cb.run_batch("OICProviderPriorRequest");
        }
    }
}