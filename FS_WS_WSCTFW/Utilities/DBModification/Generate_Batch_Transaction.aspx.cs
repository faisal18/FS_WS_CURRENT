using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicianAutomation.Utilities_UI
{
    public partial class Generate_Batch_Transaction : System.Web.UI.Page
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
                if (txt_dubailicenseno.Text.Length > 0)
                {
                    string data = "";
                    string process = "";
                    if (rd_byIDPayer.Checked)
                    {
                        data = txt_idpayer.Text.ToString();
                        process = "transactionid";
                    }
                    else if (rd_byTransactionID.Checked)
                    {
                        data = txt_transactionid.Text.ToString();
                        process = "idpayer";
                    }

                    string[] differences = { ",", "\t", "\n", "\r" };

                    string[] data_array = data.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                   
                    ExtraClasses.Generate_Batch_Transaction obj = new ExtraClasses.Generate_Batch_Transaction();
                    txt_richbox.InnerText = obj.generate_batch(Convert.ToInt32(ddl_region.SelectedValue), Convert.ToInt32(ddl_trans_type.SelectedValue),data_array,txt_dubailicenseno.Text,process);
                }
                else
                {
                    txt_richbox.InnerText = "Please enter data";
                }
            }
            catch (Exception ex)
            {
                txt_richbox.InnerText = ex.Message;
            }
           
        }

        protected void rd_byTransactionID_CheckedChanged(object sender, EventArgs e)
        {
            rd_byTransactionID.Checked = true;
            rd_byIDPayer.Checked = false;
            div_transaction.Style.Add("display", "block");
            div_payer.Style.Add("display", "none");
        }

        protected void rd_byIDPayer_CheckedChanged(object sender, EventArgs e)
        {
            rd_byIDPayer.Checked = true;
            rd_byTransactionID.Checked = false;
            div_transaction.Style.Add("display", "none");
            div_payer.Style.Add("display", "block");
        }
    }
}