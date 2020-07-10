using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FS_WS_WSCTFW.Utilities.DBModification
{
    public partial class Transaction_TAT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            string data = string.Empty;
            string[] differences = { ",", "\t", "\n", "\r" };
            try
            {


                if (txt_transactionids.Text != null)
                {
                    if (txt_transactionids.Text.Length > 0)
                    {
                        data = txt_transactionids.Text;
                    }
                }
                else if (txt_datetime.Text != null)
                {
                    if (txt_datetime.Text.Length > 0)
                    {
                        data = txt_datetime.Text;
                    }
                }


                string[] carry = data.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                lbl_result.Text = ExtraClasses.Transaction_TAT.GetTransactionTat(rd_tat_method.SelectedValue, carry);
            }
            catch (Exception ex)
            {
                lbl_result.Text = ex.Message;
            }
        }

        protected void rd_tat_method_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_datetime.Visible = false;
            pnl_transactionsid.Visible = false;

            switch (rd_tat_method.SelectedValue)
            {
                case "prod_bydate":
                    pnl_datetime.Visible = true;
                    break;
                case "prod_bytransaction":
                    pnl_transactionsid.Visible = true;
                    break;
                case "qa_bydate":
                    pnl_datetime.Visible = true;
                    break;
                case "qa_bytransaction":
                    pnl_transactionsid.Visible = true;
                    break;
            }
        }
    }
}