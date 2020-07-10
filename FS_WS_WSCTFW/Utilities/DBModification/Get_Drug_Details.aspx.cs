using System;

namespace FS_WS_WSCTFW.Utilities.DBModification
{
    public partial class Get_Drug_Details : System.Web.UI.Page
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
            string[] differences = { ",", "\t", "\n", "\r" };
            
            try
            {
                string input = txt_drug_id.Text.ToString();
                string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                ExtraClasses.Get_Drug_Details yo = new ExtraClasses.Get_Drug_Details();
                txt_richbox.InnerText = yo.Run_Process(carry);

            }
            catch(Exception ex)
            {
                txt_richbox.InnerText = "Exception Occured !\n" + ex;
            }
        }
    }
}