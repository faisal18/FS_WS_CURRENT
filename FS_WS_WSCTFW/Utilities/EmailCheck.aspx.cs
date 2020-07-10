using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace FS_WS_WSCTFW.Utilities
{
    public partial class EmailCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void btnCheckEmailTest_Click(object sender, EventArgs e)
        {
            Helpers.EmailHelper.SendTestEmail();
        }
    }
}