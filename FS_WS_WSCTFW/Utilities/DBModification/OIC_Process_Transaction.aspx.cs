using System;
using System.Timers;

namespace ClinicianAutomation
{
    public partial class OIC_Process_Transaction : System.Web.UI.Page
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
            ExtraClasses.oic_process_transaction opt = new ExtraClasses.oic_process_transaction();
            if (txt_auth_Date.Text != "" && txt_Payerid.Text != "" && txt_tat.Text != "")
            {
                string result = "";
                if (rd_new.Checked)
                {
                    result = opt.process_transaction(txt_Payerid.Text.ToString(), txt_auth_Date.Text.ToString(), txt_tat.Text.ToString());
                }
                else if (rd_processed.Checked)
                {
                    result = opt.update_tat(txt_Payerid.Text.ToString(), txt_auth_Date.Text.ToString(), txt_tat.Text.ToString());
                    
                }
                txt_richbox.InnerText = result;
            }
            else
                txt_richbox.InnerText = "Please enter values in all fields !!";
        }

        protected void lbl_generate_Click(object sender, EventArgs e)
        {
            ExtraClasses.oic_process_transaction opt = new ExtraClasses.oic_process_transaction();
            string result = "";
            if (rd_new.Checked)
            {
                result = opt.generate_query(txt_Payerid.Text.ToString(), txt_auth_Date.Text.ToString(), txt_tat.Text.ToString());
            }
            else if (rd_processed.Checked)
            {
                result = opt.update_tat_query(txt_Payerid.Text.ToString(), txt_auth_Date.Text.ToString(), txt_tat.Text.ToString());
                
            }
            txt_richbox.InnerText = result;
        }

        protected void rd_processed_CheckedChanged(object sender, EventArgs e)
        {
            rd_processed.Checked = true;
            rd_new.Checked = false;
        }

        protected void rd_new_CheckedChanged(object sender, EventArgs e)
        {
            rd_processed.Checked = false;
            rd_new.Checked = true;
        }
    }
}