using System;

namespace ClinicianAutomation
{
    public partial class ERX_Cancel_Transaction : System.Web.UI.Page
    {

        ExtraClasses.erx_cancel_transaction erx_cancel = new ExtraClasses.erx_cancel_transaction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            
            if (txt_authourizationid.Text != "")
            {
                string result = erx_cancel.cancel_transaction(txt_authourizationid.Text.ToString());
                txt_richbox.InnerText = result;
            }
            else
                txt_richbox.InnerText = "Please enter value in the field !!";
        }

        protected void lbl_generate_Click(object sender, EventArgs e)
        {
            txt_richbox.InnerText = erx_cancel.generate_query(txt_authourizationid.Text.ToString());
        }

    }
}