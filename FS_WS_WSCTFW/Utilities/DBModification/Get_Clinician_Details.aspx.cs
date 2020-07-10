using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FS_WS_WSCTFW.Utilities.DBModification
{
    public partial class Get_Clinician_Details : System.Web.UI.Page
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
                string input = txt_license.Text.ToString();
                string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                ExtraClasses.Get_Clinician_Details yo = new ExtraClasses.Get_Clinician_Details();
                txt_richbox.InnerText = yo.Run_Process(carry);

            }
            catch (Exception ex)
            {
                txt_richbox.InnerText = "Exception Occured !\n" + ex;
            }
        }
    }
}