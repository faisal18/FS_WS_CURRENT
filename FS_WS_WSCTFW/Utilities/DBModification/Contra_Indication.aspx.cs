using System;


namespace ClinicianAutomation
{
    public partial class Contra_Indication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
           // txtFullLog.ReadOnly = true;
        }
        ExtraClasses.contra_indication_query obj_contra = new ExtraClasses.contra_indication_query();

        protected void btn_data_submit_Click(object sender, EventArgs e)
        {
            if (txt_data.Text.ToString() != null)
            {

                string input = txt_data.Text.ToString();
                string[] differences = { ",", "\t", "\n", "\r" };
                string process = null;
                try
                {

                    if (rd_IndicationByDiagnosisCode.Checked == true)
                        process = "IndicationByDiagnosisCode";
                    else if (rd_IndicationByDrugCode.Checked == true)
                        process = "IndicationByDrugCode";
                    else if (rd_IndicationByClaimId.Checked == true)
                        process = "IndicationByClaimId";
                    else if (rd_ContraIndicationByDiagnosisCode.Checked == true)
                        process = "ContraIndicationByDiagnosisCode";
                    else if (rd_ContraIndicationByDrugCode.Checked == true)
                        process = "ContraIndicationByDrugCode";
                    else if (rd_ContraIndicationByClaimId.Checked == true)
                        process = "ContraIndicationByClaimId";

                    string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                    string message = obj_contra.contra_indication(carry,process);
                    txt_richbox.InnerText = message.ToString();
                }
                catch (Exception ex)
                { txt_richbox.InnerText = ex.Message.ToString(); }
            }
        }

        protected void lbl_generate_Click(object sender, EventArgs e)
        {
            if (txt_data.Text.ToString() != null)
            {
                string input = txt_data.Text.ToString();
                string[] differences = { ",", "\t", "\n", "\r" };
                string process = null;
                try
                {
                    if (rd_IndicationByDiagnosisCode.Checked == true)
                        process = "IndicationByDiagnosisCode";
                    else if (rd_IndicationByDrugCode.Checked == true)
                        process = "IndicationByDrugCode";
                    else if (rd_IndicationByClaimId.Checked == true)
                        process = "IndicationByClaimId";
                    else if (rd_ContraIndicationByDiagnosisCode.Checked == true)
                        process = "ContraIndicationByDiagnosisCode";
                    else if (rd_ContraIndicationByDrugCode.Checked == true)
                        process = "ContraIndicationByDrugCode";
                    else if (rd_ContraIndicationByClaimId.Checked == true)
                        process = "ContraIndicationByClaimId";
                    

                    string[] carry = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                    string query = obj_contra.generate_query(carry,process);
                    txt_richbox.InnerText = query;
                }
                catch (Exception ex)
                { txt_richbox.InnerText = ex.Message.ToString(); }
            }
        }

       
        protected void rd_ContraIndicationByClaimId_CheckedChanged(object sender, EventArgs e)
        {
            rd_IndicationByDiagnosisCode.Checked = false;
            rd_IndicationByDrugCode.Checked = false;
            rd_IndicationByClaimId.Checked = false;
            rd_ContraIndicationByDiagnosisCode.Checked = false;
            rd_ContraIndicationByDrugCode.Checked = false;
            rd_ContraIndicationByClaimId.Checked = true;
        }

        protected void rd_ContraIndicationByDrugCode_CheckedChanged(object sender, EventArgs e)
        {
            rd_IndicationByDiagnosisCode.Checked = false;
            rd_IndicationByDrugCode.Checked = false;
            rd_IndicationByClaimId.Checked = false;
            rd_ContraIndicationByDiagnosisCode.Checked = false;
            rd_ContraIndicationByDrugCode.Checked = true;
            rd_ContraIndicationByClaimId.Checked = false;
        }

        protected void rd_ContraIndicationByDiagnosisCode_CheckedChanged(object sender, EventArgs e)
        {
            rd_IndicationByDiagnosisCode.Checked = false;
            rd_IndicationByDrugCode.Checked = false;
            rd_IndicationByClaimId.Checked = false;
            rd_ContraIndicationByDiagnosisCode.Checked = true;
            rd_ContraIndicationByDrugCode.Checked = false;
            rd_ContraIndicationByClaimId.Checked = false;
        }

        protected void rd_IndicationByClaimId_CheckedChanged(object sender, EventArgs e)
        {
            rd_IndicationByDiagnosisCode.Checked = false;
            rd_IndicationByDrugCode.Checked = false;
            rd_IndicationByClaimId.Checked = true;
            rd_ContraIndicationByDiagnosisCode.Checked = false;
            rd_ContraIndicationByDrugCode.Checked = false;
            rd_ContraIndicationByClaimId.Checked = false;
        }

        protected void rd_IndicationByDrugCode_CheckedChanged(object sender, EventArgs e)
        {
            rd_IndicationByDiagnosisCode.Checked = false;
            rd_IndicationByDrugCode.Checked = true;
            rd_IndicationByClaimId.Checked = false;
            rd_ContraIndicationByDiagnosisCode.Checked = false;
            rd_ContraIndicationByDrugCode.Checked = false;
            rd_ContraIndicationByClaimId.Checked = false;
        }

        protected void rd_IndicationByDiagnosisCode_CheckedChanged(object sender, EventArgs e)
        {
            rd_IndicationByDiagnosisCode.Checked = true;
            rd_IndicationByDrugCode.Checked = false;
            rd_IndicationByClaimId.Checked = false;
            rd_ContraIndicationByDiagnosisCode.Checked = false;
            rd_ContraIndicationByDrugCode.Checked = false;
            rd_ContraIndicationByClaimId.Checked = false;
        }

        
    }
}