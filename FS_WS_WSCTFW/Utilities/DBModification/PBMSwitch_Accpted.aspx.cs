using System;


namespace ClinicianAutomation.Utilities_UI
{
    public partial class PBMSwitch_Accpted : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void lbl_generate_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txt_data.Text.ToString();
                string[] differences = { ",", "\t", "\n", "\r" };
                string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                if (carry.Length > 0)
                {
                    ExtraClasses.PBMSwitch_Accepted obj = new ExtraClasses.PBMSwitch_Accepted();
                    txt_richbox.InnerText = obj.generate_query(carry);
                }
                else
                {
                    txt_richbox.InnerText = "Please enter data";
                }
            }
            catch (Exception ex)
            {
                txt_richbox.InnerText = ex.Message;
            }

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txt_data.Text.ToString();
                string[] differences = { ",", "\t", "\n", "\r" };
                string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                if (carry.Length > 0)
                {
                    ExtraClasses.PBMSwitch_Accepted obj = new ExtraClasses.PBMSwitch_Accepted();
                    txt_richbox.InnerText = obj.update(carry);
                }
                else
                {
                    txt_richbox.InnerText = "Please enter data";
                }
            }
            catch (Exception ex)
            {
                txt_richbox.InnerText = ex.Message;
            }
        }
    }
}