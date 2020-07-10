using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FS_WS_WSCTFW.Utilities.DBModification
{
    public partial class Suspend_Clinician : System.Web.UI.Page
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
                if (txt_Clincian.Text.Length > 0)
                {
                    ExtraClasses.Suspend_Clinician yo = new ExtraClasses.Suspend_Clinician();
                    txt_richbox.InnerText = yo.Run(txt_Clincian.Text, txt_startdate.Text, txt_enddate.Text);
                }
            }
            catch(Exception ex)
            {
                txt_richbox.InnerText = "An exception occured!\n " + ex;
            }
        }
    }
}