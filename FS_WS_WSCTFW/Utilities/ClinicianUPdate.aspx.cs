using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace FS_WS_WSCTFW.Utilities
{
    public partial class ClinicianUPdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    Response.Redirect("~/Account/Login.aspx");
            //}
            //txtFullLog.ReadOnly = true;
        }

        protected void rdOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFullLog.Text = "";

            if (rdOperations.SelectedValue == "Update Password")
            {
                txtClinUserName.Enabled = false;
                TextClinLicNew.Enabled = false;
                txtClinPass.Enabled = true;
                pnl_sources.Visible = false;


            }
            else
                 if (rdOperations.SelectedValue == "Update Username and Password")
            {
                TextClinLicNew.Enabled = false;
                txtClinUserName.Enabled = true;
                txtClinPass.Enabled = true;
                pnl_sources.Visible = false;

            }
            else
                 if (rdOperations.SelectedValue == "Set to Null")
            {

                TextClinLicNew.Enabled = false;
                txtClinUserName.Enabled = false;
                txtClinPass.Enabled = false;
                pnl_sources.Visible = false;
            }
            else
                if (rdOperations.SelectedValue == "rd_Source")
            {
                TextClinLicNew.Enabled = false;
                txtClinUserName.Enabled = false;
                txtClinPass.Enabled = false;
                pnl_sources.Visible = true;
            }

        }

        protected void btnGenerateQuery_Click(object sender, EventArgs e)
        {
            txtFullLog.Text = "";
            string finalQuery = "";

            try
            {

                if (txtCliniLic.Text.Trim().Length < 1)
                {
                    txtFullLog.Text = "Clinician License Field should not be empty";
                }
                else
                {
                    //------------------------------------------------------ To Check 
                    #region AllChecks

                    if (rdOperations.SelectedValue == "Update Password" && txtClinPass.Text.Length > 0)
                    {

                        finalQuery += Environment.NewLine;
                        finalQuery += " Print 'Updating Clinician Password for License - " + txtCliniLic.Text + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += Environment.NewLine;
                        finalQuery += " Update Clinicians ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Set  ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " [password] = '" + txtClinPass.Text.Trim() + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Where  ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " clinicianlicense =  '" + txtCliniLic.Text.Trim() + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += " GO ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Print 'Query Executed of Clinician Password update for License - " + txtCliniLic.Text + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += "-------------------------------------------------------------------------------------------------";
                        finalQuery += Environment.NewLine;



                    }
                    else
                    if (rdOperations.SelectedValue == "Update Password" && txtClinPass.Text.Length < 1)
                    {
                        txtFullLog.Text = "Password Cannot be Empty!!!";
                    }
                    else
                        if (rdOperations.SelectedValue == "Update Username and Password" && txtClinUserName.Text.Trim().Length > 0 && txtClinPass.Text.Trim().Length > 0)
                    {


                        finalQuery += Environment.NewLine;
                        finalQuery += " Print 'Updating Clinician Password for License- " + txtCliniLic.Text + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += Environment.NewLine;
                        finalQuery += " Update  top (1) Clinicians ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Set  ";
                        finalQuery += Environment.NewLine;
                        finalQuery += "  [username] = '" + txtClinUserName.Text.Trim() + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += "  , ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " [password] = '" + txtClinPass.Text.Trim() + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Where  ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " clinicianlicense =  '" + txtCliniLic.Text.Trim() + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += " GO ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Print 'Query Executed of Clinician Password update for License - " + txtCliniLic.Text + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += "-------------------------------------------------------------------------------------------------";
                        finalQuery += Environment.NewLine;


                    }
                    else
                          if (rdOperations.SelectedValue == "Update Username and Password" && txtClinUserName.Text.Trim().Length < 1 && txtClinPass.Text.Trim().Length < 1)
                    {
                        txtFullLog.Text = "Username and Password Cannot be Empty!!!";
                    }

                    else
                          if (rdOperations.SelectedValue == "Update Username and Password" && txtClinUserName.Text.Trim().Length < 1 && txtClinPass.Text.Trim().Length > 0)
                    {
                        txtFullLog.Text = "Username Cannot be Empty!!!";
                    }

                    else
                          if (rdOperations.SelectedValue == "Update Username and Password" && txtClinUserName.Text.Trim().Length > 0 && txtClinPass.Text.Trim().Length < 1)
                    {
                        txtFullLog.Text = "Password Cannot be Empty!!!";
                    }

                    else
                        if (rdOperations.SelectedValue == "Set to Null")
                    {

                        finalQuery += Environment.NewLine;
                        finalQuery += " Print 'Updating Clinician Password for License- " + txtCliniLic.Text + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += Environment.NewLine;
                        finalQuery += " Update top (1)  Clinicians ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Set  ";
                        finalQuery += Environment.NewLine;
                        finalQuery += "  [username] = Null";
                        finalQuery += Environment.NewLine;
                        finalQuery += "  , ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " [password] = Null";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Where  ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " clinicianlicense =  '" + txtCliniLic.Text.Trim() + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += " GO ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Print 'Query Executed of Clinician Password update for License - " + txtCliniLic.Text + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += "-------------------------------------------------------------------------------------------------";
                        finalQuery += Environment.NewLine;

                    }
                    else if (rdOperations.SelectedValue == "rd_Source")
                    {
                        if (txtCliniLic.Text.Length > 0)
                        {
                            finalQuery += "Update top (1) Clinicians\nSet\n[Source] = '" + ddl_sources.SelectedValue +
                           "'\n where clinicianlicense =  '" + txtCliniLic.Text.Trim() + "'\nGO\nPrint 'Query Executed of Clinician Password update for License - " + txtCliniLic.Text + "'\n" +
                           "-------------------------------------------------------------------------------------------------\n";
                        }
                    }

                    #endregion

                }

                txtFullLog.Text = txtFullLog.Text + Environment.NewLine + finalQuery;
            }
            catch (Exception ex)
            {
                txtFullLog.Text = "Exception Occured !\n" + ex.Message;
            }

        }

        protected void btnClearQuery_Click(object sender, EventArgs e)
        {
            txtFullLog.Text = "";
        }

        protected void btnSaveScript_Click(object sender, EventArgs e)
        {



            string finalQuery = "";
            txtFullLog.Text = "";

            try
            {

                if (txtCliniLic.Text.Trim().Length < 1)
                {
                    txtFullLog.Text = "Clinician License Field should not be empty";
                }
                else
                {
                    //------------------------------------------------------ To Check 
                    #region AllChecks

                    if (rdOperations.SelectedValue == "Update Password" && txtClinPass.Text.Length > 0)
                    {

                        finalQuery += Environment.NewLine;
                        finalQuery += " Print 'Updating Clinician Password for License - " + txtCliniLic.Text + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += Environment.NewLine;
                        finalQuery += " Update  top (1) Clinicians ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Set  ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " [password] = '" + txtClinPass.Text.Trim() + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Where  ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " clinicianlicense =  '" + txtCliniLic.Text.Trim() + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += " GO ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Print 'Query Executed of Clinician Password update for License - " + txtCliniLic.Text + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += "-------------------------------------------------------------------------------------------------";
                        finalQuery += Environment.NewLine;



                    }
                    else
                    if (rdOperations.SelectedValue == "Update Password" && txtClinPass.Text.Length < 1)
                    {
                        txtFullLog.Text = "Password Cannot be Empty!!!";
                    }
                    else
                        if (rdOperations.SelectedValue == "Update Username and Password" && txtClinUserName.Text.Trim().Length > 0 && txtClinPass.Text.Trim().Length > 0)
                    {


                        finalQuery += Environment.NewLine;
                        finalQuery += " Print 'Updating Clinician Password for License- " + txtCliniLic.Text + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += Environment.NewLine;
                        finalQuery += " Update top (1)  Clinicians ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Set  ";
                        finalQuery += Environment.NewLine;
                        finalQuery += "  [username] = '" + txtClinUserName.Text.Trim() + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += "  , ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " [password] = '" + txtClinPass.Text.Trim() + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Where  ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " clinicianlicense =  '" + txtCliniLic.Text.Trim() + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += " GO ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Print 'Query Executed of Clinician Password update for License - " + txtCliniLic.Text + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += "-------------------------------------------------------------------------------------------------";
                        finalQuery += Environment.NewLine;


                    }
                    else
                          if (rdOperations.SelectedValue == "Update Username and Password" && txtClinUserName.Text.Trim().Length < 1 && txtClinPass.Text.Trim().Length < 1)
                    {
                        txtFullLog.Text = "Username and Password Cannot be Empty!!!";
                    }

                    else
                          if (rdOperations.SelectedValue == "Update Username and Password" && txtClinUserName.Text.Trim().Length < 1 && txtClinPass.Text.Trim().Length > 0)
                    {
                        txtFullLog.Text = "Username Cannot be Empty!!!";
                    }

                    else
                          if (rdOperations.SelectedValue == "Update Username and Password" && txtClinUserName.Text.Trim().Length > 0 && txtClinPass.Text.Trim().Length < 1)
                    {
                        txtFullLog.Text = "Password Cannot be Empty!!!";
                    }

                    else
                        if (rdOperations.SelectedValue == "Set to Null")
                    {

                        finalQuery += Environment.NewLine;
                        finalQuery += " Print 'Updating Clinician Password for License- " + txtCliniLic.Text + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += Environment.NewLine;
                        finalQuery += " Update  top (1) Clinicians ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Set  ";
                        finalQuery += Environment.NewLine;
                        finalQuery += "  [username] = Null";
                        finalQuery += Environment.NewLine;
                        finalQuery += "  , ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " [password] = Null";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Where  ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " clinicianlicense =  '" + txtCliniLic.Text.Trim() + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += " GO ";
                        finalQuery += Environment.NewLine;
                        finalQuery += " Print 'Query Executed of Clinician Password update for License - " + txtCliniLic.Text + "'";
                        finalQuery += Environment.NewLine;
                        finalQuery += "-------------------------------------------------------------------------------------------------";
                        finalQuery += Environment.NewLine;

                    }
                    else
                        if (rdOperations.SelectedValue == "rd_Source")
                    {
                        if (txtCliniLic.Text.Length > 0)
                        {
                            finalQuery += "Update top (1) Clinicians\nSet\n[Source] = '" + ddl_sources.SelectedValue +
                           "'\n where clinicianlicense =  '" + txtCliniLic.Text.Trim() + "'\nGO\nPrint 'Query Executed of Clinician Password update for License - " + txtCliniLic.Text + "'\n" +
                           "-------------------------------------------------------------------------------------------------\n";
                        }
                    }

                    #endregion

                }

                if (txtFullLog.Text.Trim().Length < 1)
                {

                    //string path = System.Configuration.ConfigurationManager.AppSettings["ClinicianPath"];
                    //path = path + "Script.SQL";


                    ////using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(Server.MapPath("~/Utilities/ClinicianUpdateScript/Script.SQL"), false))

                    //if (!System.IO.File.Exists(path))
                    //{
                    //    using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(path, false))
                    //    {
                    //        _testData.WriteLine(finalQuery); // Write the file.
                    //    }
                    //    FS_WS_WSCTFW.Helpers.BatchFIleCaller.InsertMLog("ClincianUpdate", User.Identity.GetUserId(), "ClinicianFile", finalQuery, "0", "");

                    //    txtFullLog.Text = "file save successfully";
                    //}
                    //else
                    //{

                    //    txtFullLog.Text = "Another Script Already Schedule for Execution, Please Retry after Some time!!!";
                    //    //txtFullLog.Text = "Another Script Already Schedule for Execution, Created: " + Environment.NewLine + System.IO.File.GetCreationTime(path).ToString() + Environment.NewLine + "Please Retry after Some time!!!";
                    //    FS_WS_WSCTFW.Helpers.BatchFIleCaller.InsertMLog("ClincianUpdate", User.Identity.GetUserId(), "", finalQuery, "0", "File Already Exist, Cannot create new file.");

                    //}

                    FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(finalQuery, "eHDF", "_ClinicianUpdate_" + txtCliniLic.Text, "DHPO", 2);



                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                txtFullLog.Text = "Exception Occured !\n" + ex.Message;
            }
        }
    }
}