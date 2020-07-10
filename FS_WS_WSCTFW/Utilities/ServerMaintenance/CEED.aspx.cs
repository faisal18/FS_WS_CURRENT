using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace FS_WS_WSCTFW.Utilities.ServerMaintenance
{
    public partial class CEED : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void btnRstrtIIS_Click(object sender, EventArgs e)
        {

            string userid = User.Identity.GetUserId();



            //txtStatus.Text = Helpers.BatchFIleCaller.CallBatchFile( userid, "PBMSWITCH-PROD");
            txtStatus.Text = Helpers.BatchFIleCaller.CallBatchFile(userid, "CEED-UAT");
            if (txtStatus.Text != "")
            {
                int StatusCode = int.Parse(txtStatus.Text.Split('\n').Reverse().Take(1).ToArray()[0]);

                if (StatusCode == 0)
                {
                    lblStatus.Text = "SUCCESS";
                    lblStatus.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblStatus.Text = "FAILED";
                    lblStatus.BackColor = System.Drawing.Color.Red;


                }
            }
            else
            {

            }

        }



        protected void btnPBMProd_Click(object sender, EventArgs e)
        {
            string userid = User.Identity.GetUserId();



            //txtStatus.Text = Helpers.BatchFIleCaller.CallBatchFile( userid, "PBMSWITCH-PROD");
            txtStatus.Text = Helpers.BatchFIleCaller.CallBatchFile(userid, "CEED-Production");
            if (txtStatus.Text != "")
            {
                int StatusCode = int.Parse(txtStatus.Text.Split('\n').Reverse().Take(1).ToArray()[0]);

                if (StatusCode == 0)
                {
                    lblStatus.Text = "SUCCESS";
                    lblStatus.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblStatus.Text = "FAILED";
                    lblStatus.BackColor = System.Drawing.Color.Red;


                }
            }
            else
            {

            }



        }

    }
}