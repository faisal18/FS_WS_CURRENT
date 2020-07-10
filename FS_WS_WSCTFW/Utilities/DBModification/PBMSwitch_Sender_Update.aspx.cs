using System;


namespace ClinicianAutomation.Utilities_UI
{
    public partial class PBMSwitch_Sender_Update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void btn_generate_query_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_providerlicense.Text != "" && txt_senderLicense.Text != "")
                {
                    ExtraClasses.PBMSwtich_Sender_Update obj = new ExtraClasses.PBMSwtich_Sender_Update();
                    txt_richbox.InnerText = obj.generate_query(txt_providerlicense.Text, txt_senderLicense.Text);
                }
            }
            catch(Exception ex)
            {
                txt_richbox.InnerText = ex.Message;
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_providerlicense.Text != "" && txt_senderLicense.Text != "")
                {
                    ExtraClasses.PBMSwtich_Sender_Update obj = new ExtraClasses.PBMSwtich_Sender_Update();
                    txt_richbox.InnerText = obj.update(txt_providerlicense.Text, txt_senderLicense.Text);
                }
            }
            catch (Exception ex)
            {
                txt_richbox.InnerText = ex.Message;
            }
        }
    }
}