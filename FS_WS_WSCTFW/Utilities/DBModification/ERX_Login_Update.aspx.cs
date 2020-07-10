using System;

namespace ClinicianAutomation
{
    public partial class ERX_Login_Update : System.Web.UI.Page
    {
        ExtraClasses.erx_login_update erx_log = new ExtraClasses.erx_login_update();

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
                    string message = erx_log.script(txt_license.Text.ToString(), txt_username.Text.ToString(), txt_password.Text.ToString());
                    txt_richbox.InnerText = message.ToString();
                }
                else
                    txt_richbox.InnerText = "Please fill all fields";
            }
            else if (rd_passwod.Checked == true)
            {
                if (txt_license.Text != "" && txt_password.Text != "")
                {
                    string message = erx_log.script(txt_license.Text.ToString(), txt_password.Text.ToString());
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
                txt_richbox.InnerText = erx_log.generate_query(txt_license.Text.ToString(), txt_username.Text.ToString(), txt_password.Text.ToString());
            }
            else if (rd_passwod.Checked == true)
            {
                txt_richbox.InnerText = erx_log.generate_query(txt_license.Text.ToString(), txt_password.Text.ToString());
            }
        }
    }
}