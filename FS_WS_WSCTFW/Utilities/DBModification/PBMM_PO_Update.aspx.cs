using System;

namespace ClinicianAutomation
{
    public partial class PBMM_PO_Update_aspx : System.Web.UI.Page
    {
        ExtraClasses.Pbmm_Po_update pbm_po = new ExtraClasses.Pbmm_Po_update();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            if (rd_all.Checked == true)
            {
                if (txt_username.Text != "" && txt_license.Text != "" && txt_password.Text != "")
                {
                    string message = pbm_po.script(txt_license.Text.ToString(), txt_username.Text.ToString(), txt_password.Text.ToString());
                    txt_richbox.InnerText = message.ToString();
                }
                else
                    txt_richbox.InnerText = "Please fill all fields";
            }
            else if (rd_passwod.Checked == true)
            {
                if (txt_license.Text != "" && txt_password.Text != "")
                {
                    string message = pbm_po.script(txt_license.Text.ToString(), txt_password.Text.ToString());
                    txt_richbox.InnerText = message.ToString();
                }
                else
                    txt_richbox.InnerText = "Please fill both fields";
            }
            else
                txt_richbox.InnerText = "Please select an option";
        }

        protected void rd_all_CheckedChanged(object sender, EventArgs e)
        {
            txt_license.Enabled = true;
            txt_password.Enabled = true;
            txt_username.Enabled = true;
            rd_passwod.Checked = false;
            txt_richbox.InnerText = null;
        }

        protected void rd_passwod_CheckedChanged(object sender, EventArgs e)
        {
            rd_all.Checked = false;
            txt_username.Enabled = false;
            txt_richbox.InnerText = null;
        }

        protected void lbl_generate_Click(object sender, EventArgs e)
        {
            if (rd_all.Checked == true)
            {
                txt_richbox.InnerText = pbm_po.generate_query(txt_license.Text.ToString(), txt_username.Text.ToString(), txt_password.Text.ToString());
            }
            else if (rd_passwod.Checked == true)
            {
                txt_richbox.InnerText = pbm_po.generate_query(txt_license.Text.ToString(), txt_password.Text.ToString());
            }
        }
    }
}