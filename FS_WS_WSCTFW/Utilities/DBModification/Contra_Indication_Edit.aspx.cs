using System;

namespace ClinicianAutomation.Utilities_UI
{
    public partial class Contra_Indication_Edit : System.Web.UI.Page
    {

        protected void Page_init(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 3600;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
          //  Server.ScriptTimeout = 3600;
            Session.Timeout = 3600;
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            ExtraClasses.contra_indication_edit cie = new ExtraClasses.contra_indication_edit();
            try
            {
                if (txt_data.Text != "")
                {
                    string input = txt_data.Text.ToString();
                    string[] differences = { ",", "\t", "\n", "\r" };
                    string process = null;
                    string csv = null;

                    if (ContraIndicaitonByScientificCode.Checked == true)
                        process = "CISC";
                    else if (ContraIndicationByDrugCode.Checked == true)
                        process = "CIDC";
                    else if (IndicationByScientificCode.Checked == true)
                        process = "ISC";
                    else if (IndicationByDrugCode.Checked == true)
                        process = "IDC";

                    string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                    if (chk_pbmm.Checked == true && chk_pbmuat.Checked == false)
                    {
                        csv = cie.Create_Csv(process, carry, true, false);
                        txt_richbox.InnerText = csv;
                    }
                    else if (chk_pbmm.Checked == false && chk_pbmuat.Checked == true)
                    {
                        csv = cie.Create_Csv(process, carry, false,true);
                        txt_richbox.InnerText = csv;
                    }
                    else if(chk_pbmm.Checked == true && chk_pbmuat.Checked == true)
                    {
                        csv = cie.Create_Csv(process, carry, true, true);
                        txt_richbox.InnerText = csv;
                    }
                    else
                    {
                        csv = cie.Create_Csv(process, carry, false, false);
                        txt_richbox.InnerText = csv;
                        //txt_richbox.InnerText = "Please check any of the boxes ";
                    }

                    
                }
                else
                {
                    txt_richbox.InnerText = "Please insert values in the data field";
                }
            }

            catch (Exception ex)
            {
                lbl_data.Text = "Exception Occured !" + ex.Message;
            }
        }

        protected void ContraIndicaitonByScientificCode_CheckedChanged(object sender, EventArgs e)
        {
            ContraIndicaitonByScientificCode.Checked = true;
            ContraIndicationByDrugCode.Checked = false;
            IndicationByDrugCode.Checked = false;
            IndicationByScientificCode.Checked = false;
        }

        protected void ContraIndicationByDrugCode_CheckedChanged(object sender, EventArgs e)
        {
            ContraIndicaitonByScientificCode.Checked = false;
            ContraIndicationByDrugCode.Checked = true;
            IndicationByDrugCode.Checked = false;
            IndicationByScientificCode.Checked = false;
        }

        protected void IndicationByScientificCode_CheckedChanged(object sender, EventArgs e)
        {
            ContraIndicaitonByScientificCode.Checked = false;
            ContraIndicationByDrugCode.Checked = false;
            IndicationByDrugCode.Checked = false;
            IndicationByScientificCode.Checked = true;
        }

        protected void IndicationByDrugCode_CheckedChanged(object sender, EventArgs e)
        {
            ContraIndicaitonByScientificCode.Checked = false;
            ContraIndicationByDrugCode.Checked = false;
            IndicationByDrugCode.Checked = true;
            IndicationByScientificCode.Checked = false;
        }
    }
}