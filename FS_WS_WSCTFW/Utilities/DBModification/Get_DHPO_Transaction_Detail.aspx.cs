using System;

namespace FS_WS_WSCTFW.Utilities.DBModification
{
    public partial class Get_DHPO_Transaction_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    Response.Redirect("~/Account/Login.aspx");
            //}
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if(txt_data.Text.Length > 0 )
            {
                string[] differences = { ",", "\t", "\n", "\r" };
                string input = txt_data.Text.ToString();
                string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                txt_richbox.InnerText = ExtraClasses.Get_DHPO_Transaction_Detail.Execute_Process(carry, rd_list_process.SelectedValue.ToString());
            }
            else
            {
                txt_richbox.InnerText = "Please enter data";
            }
        }
    }
}