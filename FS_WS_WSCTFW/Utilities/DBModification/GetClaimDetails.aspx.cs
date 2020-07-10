using System;


namespace ClinicianAutomation.Utilities_UI
{
    public partial class GetClaimDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
          ///  txtFullLog.ReadOnly = true;

        }

        protected void btn_claimdetails_Click(object sender, EventArgs e)
        {
            ExtraClasses.get_claim_detials obj = new ExtraClasses.get_claim_detials();
            if(txt_data.Text.ToString() != null)
            {
                string input = txt_data.Text.ToString();
                string[] differences = { ",", "\t", "\n", "\r" };
                string process = null;
                try
                {
                    //if (rd_byClaimId.Checked == true)
                    //    process = "Claim";
                    //else if (rd_byPayerId.Checked == true)
                    //    process = "Payer";
                    //else if (rd_byTransactionId.Checked == true)
                    //    process = "Transaction";
                    process = rd_list_process.SelectedValue;
                    
                    string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                    string result = obj.getclaimdetails(carry,process);
                    txt_richbox.InnerText = result;
                }
                catch(Exception ex)
                {
                    txt_richbox.InnerText = "Exception Occured :" + ex;
                }
            }


        }

        protected void lbl_generate_Click(object sender, EventArgs e)
        {
            ExtraClasses.get_claim_detials obj = new ExtraClasses.get_claim_detials();
            if (txt_data.Text.ToString() != null)
            {
                string input = txt_data.Text.ToString();
                string[] differences = { ",", "\t", "\n", "\r" };
                string process = null;
                try
                {
                    //if (rd_byClaimId.Checked == true)
                    //    process = "Claim";
                    //else if (rd_byPayerId.Checked == true)
                    //    process = "Payer";
                    //else if (rd_byTransactionId.Checked == true)
                    //    process = "Transaction";

                    process = rd_list_process.SelectedValue;

                    string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                    string result = obj.generate_query(carry, process);
                    txt_richbox.InnerText = result;
                }
                catch (Exception ex)
                {
                    txt_richbox.InnerText = "Exception Occured :" + ex;
                }
            }

        }

        //protected void rd_byTransactionId_CheckedChanged(object sender, EventArgs e)
        //{
        //    rd_byTransactionId.Checked = true;
        //    rd_byPayerId.Checked = false;
        //    rd_byClaimId.Checked = false;
        //}

        //protected void rd_byPayerId_CheckedChanged(object sender, EventArgs e)
        //{
        //    rd_byTransactionId.Checked = false;
        //    rd_byPayerId.Checked = true;
        //    rd_byClaimId.Checked = false;
        //}

        //protected void rd_byClaimId_CheckedChanged(object sender, EventArgs e)
        //{
        //    rd_byTransactionId.Checked = false;
        //    rd_byPayerId.Checked = false;
        //    rd_byClaimId.Checked = true;
        //}
    }
}