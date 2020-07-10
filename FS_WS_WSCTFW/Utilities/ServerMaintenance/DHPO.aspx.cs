using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace FS_WS_WSCTFW.Utilities.ServerMaintenance
{
    public partial class DHPO : System.Web.UI.Page
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

            string useremail = Helpers.EmailHelper.getUsernamefromEmail(User.Identity.Name.ToString());
            string BatchFileName =  Helpers.BatchFIleCaller.getFilnameforBatchFile(useremail);

            StringBuilder BTContent = new StringBuilder();

            BTContent.AppendLine();
            BTContent.AppendLine("c:");
            BTContent.AppendLine("cd\\ ");
            BTContent.AppendLine("cd PSTools");
            BTContent.AppendLine("Psservice.exe \\10.162.176.24 - u dh.local\\Fazeel.sheikh - p Abcd1234$$ restart Spooler");
            BTContent.AppendLine("");
            BTContent.AppendLine("");
            BTContent.AppendLine("");


            



            //txtStatus.Text = Helpers.BatchFIleCaller.CallBatchFile( userid, "PBMSWITCH-PROD");
            txtStatus.Text = Helpers.BatchFIleCaller.CallBatchFile(userid, "DHPO-Web-1-Prod");
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
            txtStatus.Text = Helpers.BatchFIleCaller.CallBatchFile(userid, "DHPO-Web-2-Prod");

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

        protected void btnDHPODB_Click(object sender, EventArgs e)
        {


            string userid = User.Identity.GetUserId();



            //txtStatus.Text = Helpers.BatchFIleCaller.CallBatchFile( userid, "PBMSWITCH-PROD");
            txtStatus.Text = Helpers.BatchFIleCaller.CallBatchFile(userid, "DHPO-DB");

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