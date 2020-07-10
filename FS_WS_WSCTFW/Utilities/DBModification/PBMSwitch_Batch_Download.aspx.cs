using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicianAutomation.Utilities_UI
{
    public partial class PBMSwitch_Batch_Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void btn_generate_query_Click(object sender, EventArgs e)
        {
            if(txt_batch_licenses.Text.Length > 0 )
            {
                string input = txt_batch_licenses.Text.ToString();
                string[] differences = { ",", "\t", "\n", "\r" };
                string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                ExtraClasses.PBMSwitch_Batch_Download obj = new ExtraClasses.PBMSwitch_Batch_Download();
                txt_richbox.InnerText = obj.generate_query(carry);
            }
            else
            {
                txt_richbox.InnerText = "Please enter data";
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (txt_batch_licenses.Text.Length > 0)
            {
                string input = txt_batch_licenses.Text.ToString();
                string[] differences = { ",", "\t", "\n", "\r" };
                string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                ExtraClasses.PBMSwitch_Batch_Download obj = new ExtraClasses.PBMSwitch_Batch_Download();
                txt_richbox.InnerText = obj.update(carry);
            }
            else
            {
                txt_richbox.InnerText = "Please enter data";
            }
        }
    }
}