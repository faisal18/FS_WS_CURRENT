using System;

namespace ClinicianAutomation
{
    public partial class Member_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        ExtraClasses.get_member_details get_member = new ExtraClasses.get_member_details();

        protected void btn_memberdetail_Click(object sender, EventArgs e)
        {
            if(txt_memberid.Text.ToString() !=null)
            {
                string input = txt_memberid.Text.ToString();
                string[] differences = { ",","\t","\n","\r" };
                try
                {
                    string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                    string message = get_member.memberdetails(carry);
                    txt_richbox.InnerText = message.ToString();
                }
                catch(Exception ex)
                { txt_richbox.InnerText = ex.Message.ToString(); }
            }
        }
    }
}