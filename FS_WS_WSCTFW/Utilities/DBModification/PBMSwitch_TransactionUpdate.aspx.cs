using System;


namespace ClinicianAutomation.Utilities_UI
{
    public partial class PBMSwitch_TransactionUpdate : System.Web.UI.Page
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
            ExtraClasses.PBMSwitch_TransactionUpdate obj = new ExtraClasses.PBMSwitch_TransactionUpdate();
            string input = txt_data.Text.ToString();
            string[] differences = { ",", "\t", "\n", "\r" };
            string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
            string process = "";
            if (rd_payerid.Checked == true)
                process = "Payerid";
            else if (rd_requestid.Checked == true)
                process = "Requestid";
            if (carry.Length > 0)
            {
                txt_richbox.InnerText = obj.Update(carry, ddl_status.SelectedValue, process);
            }
            else
            {
                txt_richbox.InnerText = "No Data for Processing";
            }
        }

        protected void btn_generatequery_Click(object sender, EventArgs e)
        {
            ExtraClasses.PBMSwitch_TransactionUpdate obj = new ExtraClasses.PBMSwitch_TransactionUpdate();
            string input = txt_data.Text.ToString();
            string[] differences = { ",", "\t", "\n", "\r" };
            string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
            string process  = "";
            if (rd_payerid.Checked == true)
                process = "Payerid";
            else if (rd_requestid.Checked == true)
                process = "Requestid";
            if (carry.Length > 0)
            {
                txt_richbox.InnerText = obj.generate_query(carry, ddl_status.SelectedValue, process);
            }
            else
            {
                txt_richbox.InnerText = "No Data for Processing";
            }
        }

        protected void rd_requestid_CheckedChanged(object sender, EventArgs e)
        {
            rd_requestid.Checked = true;
            rd_payerid.Checked = false;
        }

        protected void rd_payerid_CheckedChanged(object sender, EventArgs e)
        {
            rd_payerid.Checked = true;
            rd_requestid.Checked = false;
        }
    }
}