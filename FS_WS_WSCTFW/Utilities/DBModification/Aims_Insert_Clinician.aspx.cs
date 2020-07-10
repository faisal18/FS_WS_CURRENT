using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FS_WS_WSCTFW.Utilities.DBModification
{
	public partial class Aims_Insert_Clinician : System.Web.UI.Page
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
                if(txt_Licence.Text.Length>1 && txt_Name.Text.Length>1 && txt_LicenseEndDate.Text.Length > 1 )
                {
                    ExtraClasses.Aims_Insert_Clinician obj = new ExtraClasses.Aims_Insert_Clinician();
                    lbl_message.InnerText =  obj.Execute(txt_Licence.Text,txt_Name.Text, txt_Username.Text, txt_Password.Text, txt_LicenseStartDate.Text, txt_LicenseEndDate.Text,ddl_Enviorment.SelectedValue,ddl_source.SelectedValue);
                }
                else
                {
                    lbl_message.InnerText = "Please enter values in all fields";
                }
            }
            catch (Exception ex)
            {
                lbl_message.InnerText = ex.Message;
            }
        }
    }
}