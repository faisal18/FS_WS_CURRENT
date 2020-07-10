using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FS_WS_WSCTFW.Utilities.DBModification
{
    public partial class EclaimParser_Account : System.Web.UI.Page
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
            try
            {
                string input = txt_providerlicense.Text.ToString();
                string[] differences = { ",", "\t", "\n", "\r" };
                string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                txt_richbox.InnerText = ExtraClasses.EclaimParser_Account.register_account(carry, rd_list.SelectedValue);
            }
            catch(Exception ex)
            {
                txt_richbox.InnerText =  ex.Message;
            }
        }
    }
}