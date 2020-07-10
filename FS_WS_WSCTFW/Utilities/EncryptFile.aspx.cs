using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace FS_WS_WSCTFW.Utilities
{
    public partial class EncryptFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void btnEncrypt_Click(object sender, EventArgs e)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["ConnectionsXML"];

            FS_WS_WSCTFW.Helpers.FileEncryption.encryptFile(path);
        }

        protected void btnDecrypt_Click(object sender, EventArgs e)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["ConnectionsXML"];

            FS_WS_WSCTFW.Helpers.FileEncryption.DecryptFile(path);
        
        }

        protected void btnRSA_Click(object sender, EventArgs e)
        {
            string userid = User.Identity.GetUserId();
            string Text = Helpers.BatchFIleCaller.CallBatchFile(userid, "RSA");
        }

        protected void btnunRSA_Click(object sender, EventArgs e)
        {
            string userid = User.Identity.GetUserId();
            string Text = Helpers.BatchFIleCaller.CallBatchFile(userid, "UNRSA");

        }
    }
}