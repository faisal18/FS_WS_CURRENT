using System;

namespace ClinicianAutomation
{
    public partial class OIC_Provider_Cancel_Transaction : System.Web.UI.Page
    {
        ExtraClasses.oic_provider_cancel_transction oic_prov_cancel = new ExtraClasses.oic_provider_cancel_transction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            
            if (txt_transactionid.Text != "")
            {
                string result = oic_prov_cancel.cancel_transaction(txt_transactionid.Text.ToString());
                txt_richbox.InnerText = result;
            }
            else
                txt_richbox.InnerText = "Please enter value in the field !!";
        }

        protected void lbl_generate_Click(object sender, EventArgs e)
        {
            txt_richbox.InnerText = oic_prov_cancel.generate_query(txt_transactionid.Text.ToString());
        }
    }
}