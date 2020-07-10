using System;

namespace ClinicianAutomation
{
    public partial class OIC_Cancel_Transaction : System.Web.UI.Page
    {
        ExtraClasses.oic_cancel_transaction oic_cancel = new ExtraClasses.oic_cancel_transaction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

        }
        protected void submit_Click(object sender, EventArgs e)
        {
            
            if (txt_payerid.Text != "")
            {
                string result = oic_cancel.cancel_transaction(txt_payerid.Text.ToString());
                txt_richbox.InnerText = result;
            }
            else
                txt_richbox.InnerText = "Please enter value in the field !!";
        }

        protected void lbl_generate_Click(object sender, EventArgs e)
        {
            txt_richbox.InnerText = oic_cancel.generate_query(txt_payerid.Text.ToString());
        }
    }
}