using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicianAutomation.Utilities_UI
{
    public partial class get_ICDsMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void rd_byICD9_CheckedChanged(object sender, EventArgs e)
        {
            rd_byICD9.Checked = true;
            rd_byICD10.Checked = false;
            rd_byBoth.Checked = false;
            div_icd9.Style.Add("display", "block");
            div_icd10.Style.Add("display", "none");
        }

        protected void rd_byICD10_CheckedChanged(object sender, EventArgs e)
        {
            rd_byICD9.Checked = false;
            rd_byICD10.Checked = true;
            rd_byBoth.Checked = false;
            div_icd9.Style.Add("display", "none");
            div_icd10.Style.Add("display", "block");
        }

        protected void rd_byBoth_CheckedChanged(object sender, EventArgs e)
        {
            rd_byICD9.Checked = false;
            rd_byICD10.Checked = false;
            rd_byBoth.Checked = true;
            div_icd9.Style.Add("display", "none");
            div_icd10.Style.Add("display", "none");
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            
            string[] icd9_array;
            string[] icd10_array;
            string[] differences = { ",", "\t", "\n", "\r" };
            ExtraClasses.get_ICDsMapping obj = new ExtraClasses.get_ICDsMapping();
            
            if (rd_byICD9.Checked)
            {
                //process = "icd9";
                if (txt_icd9.Text.Length > 0)
                {
                    string carry = txt_icd9.Text.ToString();
                    icd9_array = carry.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                    txt_richbox.InnerText =  obj.get_icd9(icd9_array);
                }
                else
                {
                    txt_richbox.InnerText = "Please enter data in the ICD 9 field";
                }
            }
            else if (rd_byICD10.Checked)
            {
                //process = "icd10";
                if (txt_icd10.Text.Length > 0)
                {
                    string carry = txt_icd10.Text.ToString();
                    icd10_array = carry.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                    txt_richbox.InnerText = obj.get_icd10(icd10_array);
                }
                else
                {
                    txt_richbox.InnerText = "Please enter date in the ICD 10 field";
                }
            }
            else if (rd_byBoth.Checked)
            {
                //process = "both";
                txt_richbox.InnerText = obj.get_both();
            }


        }

        protected void btn_SaveCSV_Click(object sender, EventArgs e)
        {
            //ExtraClasses.get_ICDsMapping obj = new ExtraClasses.get_ICDsMapping();
            //txt_richbox.InnerText = obj.SaveCSV();
        }
    }
}