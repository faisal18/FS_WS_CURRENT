using System;


namespace ClinicianAutomation.Utilities_UI
{
    public partial class PBMM_Cancel_Transaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }
        protected void submit_Click(object sender, EventArgs e)
        {
            ExtraClasses.pbmm_cancel_transaction obj = new ExtraClasses.pbmm_cancel_transaction();
            txt_richbox.InnerText = null;
            if (txt_transactionid.Text != "")
            {
                string input = txt_transactionid.Text.ToString();
                string[] differences = { ",", "\t", "\n", "\r" };
                string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                txt_richbox.InnerText = obj.cancel_transaction(carry); ;
            }
            else
                txt_richbox.InnerText = "Please enter value in the field !!";
        }

        protected void lbl_generate_Click(object sender, EventArgs e)
        {
            ExtraClasses.pbmm_cancel_transaction obj = new ExtraClasses.pbmm_cancel_transaction();
            if (txt_transactionid.Text != "")
            {
                string input = txt_transactionid.Text.ToString();
                string[] differences = { ",", "\t", "\n", "\r" };
                string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                txt_richbox.InnerText = obj.generate_query(carry);
            }
            else
            {
                txt_richbox.InnerText = "Please enter value in the field !!";
            }

        }
    }
}