using System;

namespace ClinicianAutomation
{
    public partial class Submit_Claim : System.Web.UI.Page
    {
        ExtraClasses.submit_claim sc = new ExtraClasses.submit_claim();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }


        }

        protected void submit_Click(object sender, EventArgs e)
        {           
            string result = sc.claim_submit(txt_claimid.Text.ToString());
            txt_richbox.InnerText = result;
        }

        protected void lbl_generate_Click(object sender, EventArgs e)
        {
            txt_richbox.InnerText = sc.generate_query(txt_claimid.Text.ToString());
        }
    }
}